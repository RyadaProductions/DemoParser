using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DemoParser.Models.Structs;

namespace DemoParser.Extensions
{
    public static class BinaryReaderExtensions
    {
        public static int ReadRawData(this BinaryReader reader, byte[] buffer, int length)
        {
            var size = reader.ReadInt32();

            if (length < size) return -1;
            if (buffer != null)
            {
                buffer = reader.ReadBytes(size);
            }
            else
            {
                // move position and ignore
                reader.BaseStream.Position += size;
            }

            return size;
        }

        public static void ReadCmdInfo(this BinaryReader reader, ref CommandInfo info)
        {
            info.Splits[0] = ReadSplit(ref reader);
            info.Splits[1] = ReadSplit(ref reader);
        }

        private static unsafe Split ReadSplit(ref BinaryReader reader)
        {
            var buffer = reader.ReadBytes(sizeof(Split));
            return buffer.ToImpl<Split>();
        }

        public static void ReadSequenceInfo(this BinaryReader reader, ref int sequenceNumberIn, ref int sequenceNumberOut)
        {
            sequenceNumberIn = reader.ReadInt32();
            sequenceNumberOut = reader.ReadInt32();
        }
    }
}
