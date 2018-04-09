using DemoParser.Helpers;
using DemoParser.Models;
using DemoParser.Models.Structs;

namespace DemoParser.Extensions
{
    public static class StructExtensions
    {
        public static unsafe Header ConvertToClass(this DemoHeader structHeader)
        {
            var header = new Header
            {
                Demofilestamp = FixedByteArrayHelper.ConvertToString(structHeader.Demofilestamp),
                Clientname = FixedByteArrayHelper.ConvertToString(structHeader.Clientname),
                Gamedirectory = FixedByteArrayHelper.ConvertToString(structHeader.Gamedirectory),
                Mapname = FixedByteArrayHelper.ConvertToString(structHeader.Mapname),
                Servername = FixedByteArrayHelper.ConvertToString(structHeader.Servername),
                Demoprotocol = structHeader.Demoprotocol,
                Networkprotocol = structHeader.Networkprotocol,
                PlaybackFrames = structHeader.PlaybackFrames,
                PlaybackTime = structHeader.PlaybackTime,
                PlaybackTicks = structHeader.PlaybackTicks,
                Signonlength = structHeader.Signonlength
            };
            
            return header;
        }
    }
}
