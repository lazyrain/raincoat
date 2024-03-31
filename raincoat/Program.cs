namespace raincoat
{
    internal static class Program
    {
        static Mutex mutex = new Mutex(true, "lazyrain-raincoat");
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!mutex.WaitOne(TimeSpan.Zero, true))
            {
                return;
            }
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Config());

            // アプリケーション終了時にmutexを解放する
            mutex.ReleaseMutex();
        }
    }
}