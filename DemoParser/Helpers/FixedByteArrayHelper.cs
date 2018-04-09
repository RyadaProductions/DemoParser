using System.Text;

namespace DemoParser.Helpers
{
    public static class FixedByteArrayHelper
    {
        public static unsafe string ConvertToString(byte* ptr)
        {
            var bytes = new byte[128];
            var index = 0;
            for (var counter = ptr; *counter != 0; counter++)
            {
                bytes[index++] = *counter;
            }

            return Encoding.ASCII.GetString(bytes, 0, 128).Replace("\0", string.Empty);
        }
    }
}
