using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace raincoat.UseCases.Skills
{
    internal class ReduceKeystrokes : IUseCase<SkillInputPack, SkillOutputPack>
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        const int KEYEVENTF_EXTENDEDKEY = 0x0001;
        const int KEYEVENTF_KEYUP = 0x0002;

        public SkillOutputPack Execute(SkillInputPack input)
        {
            var strokes = input.Argument;
            foreach (string stroke in Regex.Split(strokes, @"(?=<[^>]+>)"))
            {
                if (stroke.StartsWith("<"))
                {
                    SimulateSpecialKey(stroke);
                }
                else
                {
                    foreach (char c in stroke)
                    {
                        SimulateKeyPress(c);
                    }
                }
            }

            return new SkillOutputPack();
        }

        private void SimulateKeyPress(char c)
        {
            byte virtualKey = (byte)char.ToUpper(c);
            keybd_event(virtualKey, 0, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(virtualKey, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
        }

        private void SimulateSpecialKey(string stroke)
        {
            byte virtualKey = 0;
            uint flags = 0;
            bool isExtendedKey = false;

            // 修飾キーの処理
            if (stroke.StartsWith("<S-"))
            {
                flags |= KEYEVENTF_EXTENDEDKEY;
                keybd_event((byte)0x10, 0, 0, 0); // SHIFT down
            }
            if (stroke.StartsWith("<C-"))
            {
                flags |= KEYEVENTF_EXTENDEDKEY;
                keybd_event((byte)0x11, 0, 0, 0); // CTRL down
            }

            // キーコードの解析
            string keyPart = stroke.Substring(stroke.IndexOf('-') + 1, stroke.Length - stroke.IndexOf('-') - 2);
            if (keyPart.Length == 1)
            {
                virtualKey = (byte)char.ToUpper(keyPart[0]);
            }
            else
            {
                isExtendedKey = true;
                switch (keyPart)
                {
                    case "F1": virtualKey = 0x70; break;
                        // 他の特殊キーの場合はここに追加
                }
            }

            keybd_event(virtualKey, 0, flags | (isExtendedKey ? KEYEVENTF_EXTENDEDKEY : (uint)0), 0);
            keybd_event(virtualKey, 0, flags | KEYEVENTF_KEYUP | (isExtendedKey ? KEYEVENTF_EXTENDEDKEY : (uint)0), 0);

            // 修飾キーの解放
            if (stroke.StartsWith("<S-"))
            {
                keybd_event((byte)0x10, 0, KEYEVENTF_KEYUP, 0); // SHIFT up
            }
            if (stroke.StartsWith("<C-"))
            {
                keybd_event((byte)0x11, 0, KEYEVENTF_KEYUP, 0); // CTRL up
            }
        }
    }
}
