using raincoat.Infrastructures.Repositories;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace raincoat.UseCases.Skills
{
    internal class ReduceKeystrokes : IUseCase<SkillInputPack, SkillOutputPack>
    {
        private readonly VirtualKeyRepository virtualKeyRepository = new();

        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        const int KEYEVENTF_EXTENDEDKEY = 0x0001;
        const int KEYEVENTF_KEYUP = 0x0002;

        public SkillOutputPack Execute(SkillInputPack input)
        {
            var strokes = input.Argument;
            foreach (string stroke in Regex.Split(strokes, @"((?<!\\)<[CASM]-[^<>]+>)")) // 修飾キーの組み合わせ対応
            {
                if (stroke.StartsWith("<"))
                {
                    this.SimulateSpecialKey(stroke);
                    continue;
                }

                foreach (char c in stroke)
                {
                    if (c != '\\')
                    {
                        this.SimulateKeyPress(c);
                    }
                }
            }

            return new SkillOutputPack();
        }

        private void SimulateKeyPress(char c)
        {
            byte virtualKey;
            bool isShiftRequired = virtualKeyRepository.IsShiftRequiredForSymbol(c);

            if (isShiftRequired)
            {
                virtualKey = this.virtualKeyRepository.GetVirtualKey(c);
                keybd_event((byte)0x10, 0, 0, 0); // SHIFT down
                keybd_event(virtualKey, 0, KEYEVENTF_EXTENDEDKEY, 0);
                keybd_event(virtualKey, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
                keybd_event((byte)0x10, 0, KEYEVENTF_KEYUP, 0); // SHIFT up
            }
            else
            {
                virtualKey = this.virtualKeyRepository.GetVirtualKey(char.ToUpper(c));
                keybd_event(virtualKey, 0, KEYEVENTF_EXTENDEDKEY, 0);
                keybd_event(virtualKey, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            }
        }

        private void SimulateSpecialKey(string stroke)
        {
            var modifierKeys = new Dictionary<string, byte>
            {
                { "C", 0x11 }, // Ctrl
                { "A", 0x12 }, // Alt
                { "S", 0x10 }, // Shift
                { "M", 0x5B }  // Meta (Windowsキー)
            };

            var pressedModifiers = new List<byte>();

            // 修飾キーの解析
            Match match = Regex.Match(stroke, @"<([CASM-]+)-(.+?)>");
            if (!match.Success)
            {
                return;
            }

            string modifiers = match.Groups[1].Value;
            string keyPart = match.Groups[2].Value;

            // 修飾キーを押す
            foreach (char mod in modifiers)
            {
                if (modifierKeys.TryGetValue(mod.ToString(), out byte keyCode))
                {
                    keybd_event(keyCode, 0, 0, 0); // Key Down
                    pressedModifiers.Add(keyCode);
                }
            }

            // メインキーを押す
            byte virtualKey;
            if (keyPart.Length == 1)
            {
                char c = keyPart[0];
                if (char.IsLetter(c))
                {
                    virtualKey = (byte)char.ToUpper(c);
                }
                else
                {
                    virtualKey = this.virtualKeyRepository.GetVirtualKey(c);
                }
            }
            else
            {
                virtualKey = this.virtualKeyRepository.GetVirtualKey(keyPart);
            }

            keybd_event(virtualKey, 0, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(virtualKey, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);

            // 修飾キーを解放する
            foreach (byte keyCode in pressedModifiers)
            {
                keybd_event(keyCode, 0, KEYEVENTF_KEYUP, 0); // Key Up
            }
        }
    }
}
