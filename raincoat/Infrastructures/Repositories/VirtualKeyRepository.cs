namespace raincoat.Infrastructures.Repositories
{
    public class VirtualKeyRepository : IRepository
    {
        private readonly HashSet<char> symbolsRequiringShift = new HashSet<char>
        {
            '!',
            '"',
            '#',
            '$',
            '%',
            '&',
            '(',
            ')',
            '*',
            '+',
            '<',
            '=',
            '>',
            '?',
            '\'',
            '_',
            '`',
            '{',
            '}',
            '~',
        };

        private readonly Dictionary<char, byte> charToVirtualKey = new Dictionary<char, byte>
        {
            { ' ', 0x20 },
            { '!', 0x31 },
            { '"', 0x32 },
            { '#', 0x33 },
            { '$', 0x34 },
            { '%', 0x35 },
            { '&', 0x36 },
            { '(', 0x38 },
            { ')', 0x39 },
            { '*', 0xBA },
            { '+', 0xBB },
            { ',', 0xBC },
            { '-', 0xBD },
            { '.', 0xBE },
            { '/', 0xBF },
            { ':', 0xBA },
            { ';', 0xBB },
            { '<', 0xBC },
            { '=', 0xBD },
            { '>', 0xBE },
            { '?', 0xBF },
            { '@', 0xC0 },
            { '[', 0xDB },
            { '\'', 0x37 },
            { '\\', 0xE2 },
            { ']', 0xDD },
            { '^', 0xDE },
            { '_', 0xE2 },
            { '`', 0xC0 },
            { '{', 0xDB },
            { '|', 0xDC },
            { '}', 0xDD },
            { '~', 0xDE },
        };

        private static Dictionary<string, byte> stringToVirtualKey = new Dictionary<string, byte>
        {
            { "F1", 0x70 },
            { "F2", 0x71 },
            { "F3", 0x72 },
            { "F4", 0x73 },
            { "F5", 0x74 },
            { "F6", 0x75 },
            { "F7", 0x76 },
            { "F8", 0x77 },
            { "F9", 0x78 },
            { "F10", 0x79 },
            { "F11", 0x7A },
            { "F12", 0x7B },
            { "Insert", 0x2D },
            { "Delete", 0x2E },
            { "Home", 0x24 },
            { "End", 0x23 },
            { "PageUp", 0x21 },
            { "PageDown", 0x22 },
            { "Up", 0x26 },
            { "Down", 0x28 },
            { "Left", 0x25 },
            { "Right", 0x27 }
        };

        public byte GetVirtualKey(char c)
        {
            if (charToVirtualKey.TryGetValue(c, out byte virtualKey))
            {
                return virtualKey;
            }

            if (char.IsLetter(c))
            {
                return (byte)char.ToUpper(c);
            }

            if (char.IsNumber(c))
            {
                return (byte)(c + 0x30);
            }

            throw new ArgumentException($"Invalid character: {c}");
        }

        public byte GetVirtualKey(string keyName)
        {
            if (stringToVirtualKey.TryGetValue(keyName, out byte virtualKey))
            {
                return virtualKey;
            }

            throw new ArgumentException($"Invalid key name: {keyName}");
        }

        public bool IsShiftRequiredForSymbol(char c)
        {
            return this.symbolsRequiringShift.Contains(c) || char.IsUpper(c);
        }
    }
}
