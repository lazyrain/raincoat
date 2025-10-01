using System.Diagnostics;
using System.Text.RegularExpressions;

namespace raincoat.UseCases.Triggers
{
    public class MonitorActiveWindow : IUseCase<MonitorActiveWindowInputPack, MonitorActiveWindowOutputPack>
    {
        private const int MillisecondsDelay = 1000;
        private bool _isMonitoring = false;
        private readonly object _lock = new object();
        private CancellationTokenSource _cancellationTokenSource;

        public MonitorActiveWindowOutputPack Execute(MonitorActiveWindowInputPack input)
        {
            lock (_lock)
            {
                if (_isMonitoring)
                {
                    return new MonitorActiveWindowOutputPack();
                }

                _cancellationTokenSource = new CancellationTokenSource();
                Task.Run(() => MonitorLoop(input, _cancellationTokenSource.Token));
                _isMonitoring = true;
            }

            return new MonitorActiveWindowOutputPack();
        }

        public void Stop()
        {
            lock (_lock)
            {
                if (!_isMonitoring)
                {
                    return;
                }

                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
                _isMonitoring = false;
            }
        }

        private async Task MonitorLoop(MonitorActiveWindowInputPack input, CancellationToken token)
        {
            string lastMatchedTitle = string.Empty;

            while (!token.IsCancellationRequested)
            {
                try
                {
                    var activeTitle = input.WindowService.GetActiveWindowTitle();
                    Debug.WriteLine($"[MonitorLoop] Active window title: '{activeTitle}'");

                    if (!string.IsNullOrEmpty(activeTitle))
                    {
                        // Avoid re-triggering for the same window repeatedly
                        if (activeTitle == lastMatchedTitle)
                        {
                            Debug.WriteLine($"[MonitorLoop] Skipping duplicate window: '{activeTitle}'");
                            await Task.Delay(MillisecondsDelay, token);
                            continue;
                        }

                        bool commandExecuted = false;
                        foreach (var command in input.Config.KeyCommands)
                        {
                            Debug.WriteLine($"[MonitorLoop] Checking command: IsWindowTrigger={command.IsWindowTrigger}, TriggerWindowTitle='{command.TriggerWindowTitle}'");

                            if (command.IsWindowTrigger && !string.IsNullOrEmpty(command.TriggerWindowTitle))
                            {
                                bool isMatch = false;
                                try
                                {
                                    isMatch = Regex.IsMatch(activeTitle, command.TriggerWindowTitle);
                                }
                                catch (ArgumentException ex)
                                {
                                    Debug.WriteLine($"[MonitorLoop] Invalid regex pattern '{command.TriggerWindowTitle}': {ex.Message}");
                                }
                                Debug.WriteLine($"[MonitorLoop] Regex check: '{activeTitle}' matches '{command.TriggerWindowTitle}' = {isMatch}");

                                if (isMatch)
                                {
                                    Debug.WriteLine($"[MonitorLoop] Window trigger matched: {activeTitle} for command {command.ButtonName}");

                                    try
                                    {
                                        input.SkillService.Execute(command.SkillType, command.Argument, input.Config.ConnectionSetting, input.ObsService);
                                        Debug.WriteLine($"[MonitorLoop] Successfully executed command");
                                    }
                                    catch (Exception executeEx)
                                    {
                                        Debug.WriteLine($"[MonitorLoop] Error executing command: {executeEx.Message}");
                                    }

                                    lastMatchedTitle = activeTitle; // Mark as triggered
                                    commandExecuted = true;
                                    break; // Process only the first matched command
                                }
                            }
                        }

                        // Clear lastMatchedTitle only if current window doesn't match any trigger
                        if (!commandExecuted)
                        {
                            if (!string.IsNullOrEmpty(lastMatchedTitle))
                            {
                                Debug.WriteLine($"[MonitorLoop] Clearing lastMatchedTitle, no commands executed for '{activeTitle}'");
                            }
                            lastMatchedTitle = string.Empty;
                        }
                    }
                    else
                    {
                        Debug.WriteLine($"[MonitorLoop] Active window title is null or empty");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[MonitorLoop] Error in MonitorLoop: {ex.Message}");
                }

                try
                {
                    await Task.Delay(MillisecondsDelay, token); // Check every 1000ms
                }
                catch (TaskCanceledException)
                {
                    Debug.WriteLine($"[MonitorLoop] Task cancelled, exiting loop");
                    break; // Exit loop when cancellation is requested
                }
            }

            Debug.WriteLine($"[MonitorLoop] MonitorLoop ended");
        }
    }
}