
using System.Runtime.InteropServices;
using System.Text;

namespace raincoat.Domains.Services
{
    public class ActiveWindowService : IActiveWindowService
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        public string GetActiveWindowTitle()
        {
            var hWnd = GetForegroundWindow();
            if (hWnd == IntPtr.Zero)
            {
                return "";
            }

            var length = GetWindowTextLength(hWnd);
            if (length == 0)
            {
                return "";
            }

            var builder = new StringBuilder(length + 1);
            GetWindowText(hWnd, builder, builder.Capacity);

            return builder.ToString();
        }
    }
}
