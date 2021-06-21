using System.Linq;

namespace MailSender.Services
{
    public static class TextEncoder
    {
        public static string Encode(string str, int Key = 1)
        {
            return new(str.Select(c => (char) (c + Key)).ToArray());
        }

        public static string Decode(string str, int Key = 1)
        {
            return new(str.Select(c => (char)(c - Key)).ToArray());
        }
    }
}
