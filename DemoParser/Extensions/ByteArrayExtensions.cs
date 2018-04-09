using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DemoParser.Extensions
{
    public static class ByteArrayExtensions
    {
        public static unsafe T ToImpl<T>(this byte[] buffer)
        {
            fixed (byte* b = &buffer[0])
                return Unsafe.ReadUnaligned<T>(b);
        }

        public static unsafe T ToImplSpan<T>(this Span<byte> buffer)
        {
            fixed (byte* b = &buffer[0])
                return Unsafe.ReadUnaligned<T>(b);
        }
    }
}
