using raincoat.Infrastructures.Repositories;
using System.Diagnostics;
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
            foreach (string stroke in Regex.Split(strokes, @"((?<!\\)<[^<>]+>|\\[^<>]+)"))
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
            uint flags = 0;
            bool isExtendedKey = false;

            // 修飾キーの処理
            if (stroke.StartsWith("<S-") || stroke.StartsWith("<Shift>"))
            {
                flags |= KEYEVENTF_EXTENDEDKEY;
                keybd_event((byte)0x10, 0, 0, 0); // SHIFT down
            }
            if (stroke.StartsWith("<C-") || stroke.StartsWith("<Ctrl>"))
            {
                flags |= KEYEVENTF_EXTENDEDKEY;
                keybd_event((byte)0x11, 0, 0, 0); // CTRL down
            }
            if (stroke.StartsWith("<A-") || stroke.StartsWith("<Alt>"))
            {
                flags |= KEYEVENTF_EXTENDEDKEY;
                keybd_event((byte)0x12, 0, 0, 0); // ALT down
            }

            // キーコードの解析
            string keyPart = stroke
                .Substring(stroke.IndexOf('-') + 1)
                .Replace("<", string.Empty)
                .Replace(">", string.Empty);

            Debug.WriteLine($"{stroke} => {keyPart}");
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
                    // 記号の処理
                    virtualKey = this.virtualKeyRepository.GetVirtualKey(c);
                    flags |= KEYEVENTF_EXTENDEDKEY;
                }
            }
            else
            {
                isExtendedKey = true;
                virtualKey = this.virtualKeyRepository.GetVirtualKey(keyPart);
            }

            keybd_event(virtualKey, 0, flags | (isExtendedKey ? KEYEVENTF_EXTENDEDKEY : (uint)0), 0);
            keybd_event(virtualKey, 0, flags | KEYEVENTF_KEYUP | (isExtendedKey ? KEYEVENTF_EXTENDEDKEY : (uint)0), 0);

            // 修飾キーの解放
            if (stroke.StartsWith("<S-") || stroke.StartsWith("<Shift>"))
            {
                keybd_event((byte)0x10, 0, KEYEVENTF_KEYUP, 0); // SHIFT up
            }
            if (stroke.StartsWith("<C-") || stroke.StartsWith("<Ctrl>"))
            {
                keybd_event((byte)0x11, 0, KEYEVENTF_KEYUP, 0); // CTRL up
            }
            if (stroke.StartsWith("<A-") || stroke.StartsWith("<Alt>"))
            {
                keybd_event((byte)0x12, 0, KEYEVENTF_KEYUP, 0); // ALT up
            }
        }
    }
}
