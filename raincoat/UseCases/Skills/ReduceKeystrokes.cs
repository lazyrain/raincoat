using System.Diagnostics;
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
            bool isUpperCase = char.IsUpper(c);
            bool isLowerCase = char.IsLower(c);

            if (isUpperCase)
            {
                virtualKey = (byte)c;
                keybd_event((byte)0x10, 0, 0, 0); // SHIFT down
                keybd_event(virtualKey, 0, KEYEVENTF_EXTENDEDKEY, 0);
                keybd_event(virtualKey, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
                keybd_event((byte)0x10, 0, KEYEVENTF_KEYUP, 0); // SHIFT up
            }
            else if (isLowerCase)
            {
                virtualKey = (byte)char.ToUpper(c);
                keybd_event(virtualKey, 0, KEYEVENTF_EXTENDEDKEY, 0);
                keybd_event(virtualKey, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            }
            else
            {
                bool isDownShift;
                // 記号の処理
                switch (c)
                {
                    case ':': virtualKey = 0xBA; isDownShift = false; break;
                    case ';': virtualKey = 0xBB; isDownShift = false; break;
                    case ',': virtualKey = 0xBC; isDownShift = false; break;
                    case '-': virtualKey = 0xBD; isDownShift = false; break;
                    case '.': virtualKey = 0xBE; isDownShift = false; break;
                    case '/': virtualKey = 0xBF; isDownShift = false; break;
                    case '@': virtualKey = 0xC0; isDownShift = false; break;

                    case '*': virtualKey = 0xBA; isDownShift = true; break;
                    case '+': virtualKey = 0xBB; isDownShift = true; break;
                    case '<': virtualKey = 0xBC; isDownShift = true; break;
                    case '=': virtualKey = 0xBD; isDownShift = true; break;
                    case '>': virtualKey = 0xBE; isDownShift = true; break;
                    case '?': virtualKey = 0xBF; isDownShift = true; break;
                    case '`': virtualKey = 0xC0; isDownShift = true; break;

                    case '!': virtualKey = 0x31; isDownShift = true; break;
                    case '"': virtualKey = 0x32; isDownShift = true; break;
                    case '#': virtualKey = 0x33; isDownShift = true; break;
                    case '$': virtualKey = 0x34; isDownShift = true; break;
                    case '%': virtualKey = 0x35; isDownShift = true; break;
                    case '&': virtualKey = 0x36; isDownShift = true; break;
                    case '\'': virtualKey = 0x37; isDownShift = true; break;
                    case '(': virtualKey = 0x38; isDownShift = true; break;
                    case ')': virtualKey = 0x39; isDownShift = true; break;

                    default: throw new ArgumentException("変なキーが設定されています。");
                }
                if (isDownShift)
                {
                    keybd_event(0x10, 0, 0, 0); // SHIFT down
                    keybd_event(virtualKey, 0, KEYEVENTF_EXTENDEDKEY, 0);
                    keybd_event(virtualKey, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
                    keybd_event(0x10, 0, KEYEVENTF_KEYUP, 0); // SHIFT up
                }
                else
                {
                    keybd_event(virtualKey, 0, KEYEVENTF_EXTENDEDKEY, 0);
                    keybd_event(virtualKey, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
                }
            }
        }

        private void SimulateSpecialKey(string stroke)
        {
            byte virtualKey = 0;
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
                    switch (c)
                    {
                        case '<': virtualKey = 0xDB; break;
                        case '>': virtualKey = 0xDC; break;
                        case '?': virtualKey = 0xBF; break;
                        case ',': virtualKey = 0xBC; break;
                        case '.': virtualKey = 0xBE; break;
                        case '/': virtualKey = 0xBF; break;
                        case ';': virtualKey = 0xBA; break;
                        case '\'': virtualKey = 0xDE; break;
                        case '"': virtualKey = 0xDD; break;
                        case '[': virtualKey = 0xDB; break;
                        case ']': virtualKey = 0xDD; break;
                        case '{': virtualKey = 0xDB; break;
                        case '}': virtualKey = 0xDD; break;
                        case '(': virtualKey = 0xDB; break;
                        case ')': virtualKey = 0xDD; break;
                        case '|': virtualKey = 0xDC; break;
                        case '\\': virtualKey = 0xDC; break;
                        case '`': virtualKey = 0xC0; break;
                        case '~': virtualKey = 0xC0; break;
                        case '-': virtualKey = 0xBD; break;
                        case '=': virtualKey = 0xBB; break;
                        case '+': virtualKey = 0xBB; break;
                    }
                    flags |= KEYEVENTF_EXTENDEDKEY;
                }
            }
            else
            {
                isExtendedKey = true;
                switch (keyPart)
                {
                    case "F1": virtualKey = 0x70; break;
                    case "F2": virtualKey = 0x71; break;
                    case "F3": virtualKey = 0x72; break;
                    case "F4": virtualKey = 0x73; break;
                    case "F5": virtualKey = 0x74; break;
                    case "F6": virtualKey = 0x75; break;
                    case "F7": virtualKey = 0x76; break;
                    case "F8": virtualKey = 0x77; break;
                    case "F9": virtualKey = 0x78; break;
                    case "F10": virtualKey = 0x79; break;
                    case "F11": virtualKey = 0x7A; break;
                    case "F12": virtualKey = 0x7B; break;
                    case "Insert": virtualKey = 0x2D; break;
                    case "Delete": virtualKey = 0x2E; break;
                    case "Home": virtualKey = 0x24; break;
                    case "End": virtualKey = 0x23; break;
                    case "PageUp": virtualKey = 0x21; break;
                    case "PageDown": virtualKey = 0x22; break;
                    case "Up": virtualKey = 0x26; break;
                    case "Down": virtualKey = 0x28; break;
                    case "Left": virtualKey = 0x25; break;
                    case "Right": virtualKey = 0x27; break;
                }
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
