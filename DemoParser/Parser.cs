using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Timers;
using DemoParser.Enums;
using DemoParser.Extensions;
using DemoParser.Models;
using DemoParser.Models.Structs;

namespace DemoParser
{
    public sealed class Parser
    {
        private int _currentTick;

        private BinaryReader _binaryReader;

        private const string DEMO_HEADER_ID = "HL2DEMO";
        private const int DEMO_PROTOCOL = 4;


        public unsafe Header ParseHeader(string filePath)
        {
            //TODO: DO NOT FORGET TO DISPOSE AFTER FINISHING TO USE IT
            _binaryReader = new BinaryReader(new BufferedStream(File.Open(filePath, FileMode.Open)));

            _binaryReader.BaseStream.Seek(0, SeekOrigin.End);
            var length = _binaryReader.BaseStream.Position;
            _binaryReader.BaseStream.Seek(0, SeekOrigin.Begin);

            if (length < sizeof(DemoHeader))
            {
                _binaryReader.Close();
                throw new FileLoadException("Corrupt header file detected, aborting the parsing process");
            }

            var headerBytes = new byte[sizeof(DemoHeader)];
            _binaryReader.Read(headerBytes, 0, sizeof(DemoHeader));
            fixed (byte* headerPtr = headerBytes)
            {
                var headerStruct = Unsafe.ReadUnaligned<DemoHeader>(headerPtr);
                var header = headerStruct.ConvertToClass();

                if (header.Demofilestamp == DEMO_HEADER_ID && header.Demoprotocol == DEMO_PROTOCOL) return header;

                _binaryReader.Close();
                _binaryReader.Dispose();
                throw new FileLoadException("Corrupt header file detected, aborting the parsing process");
            }
        }

        public void PlayDemoFile(string filePath)
        {
            var header = ParseHeader(filePath);

            if (header.Equals(default(Header))) throw new InvalidOperationException("The header needs to be parsed before trying to play the demo.");

            while (true) ParseTick();
        }

        private void ParseTick()
        {
            var command = (DemoCmds)_binaryReader.ReadByte();

            var ingameTick = (int)_binaryReader.ReadUInt32(); // tick number
            _binaryReader.ReadByte(); // player slot

            _currentTick++; // = TickNum;

            switch (command)
            {
                case DemoCmds.Synctick:
                    break;
                case DemoCmds.Stop:
                    return;
                case DemoCmds.Consolecmd:
                    _binaryReader.ReadRawData(null, _binaryReader.ReadInt32() * 8);
                    break;
                case DemoCmds.Datatables:
                    _binaryReader.ReadRawData(null, _binaryReader.ReadInt32() * 8);
                    //TODO: parse DataTable packet
                    //TODO: Map weapons and bind all entities
                    break;
                case DemoCmds.Stringtables:
                    _binaryReader.ReadRawData(null, _binaryReader.ReadInt32() * 8);
                    //TODO: ParseHeader stringtable packet
                    break;
                case DemoCmds.Usercmd:
                    _binaryReader.ReadUInt32();
                    _binaryReader.ReadRawData(null, _binaryReader.ReadInt32() * 8);
                    break;
                case DemoCmds.Signon:
                case DemoCmds.Packet:
                    HandleDemoPacket();
                    break;
                default:
                    throw new Exception("Can't handle Demo-Command " + command);
            }
        }

        public unsafe void HandleDemoPacket()
        {
            var info = new DemoInfo(new Split[2]);
            int dummy = 0;
            fixed (byte* payload = new byte[262144 - 4])
            {

                _binaryReader.ReadCmdInfo(ref info);
                _binaryReader.ReadSequenceInfo(ref dummy, ref dummy);

                var timer = new Timer();
                if (timer)
                {

                }

                var test = ReadProtobufVarInt();
                //Todo: implement the rest of HandleDemoPacket()
            }
        }


        private const uint MSB_1 = 0x00000080;
        private const uint MSB_2 = 0x00008000;
        private const uint MSB_3 = 0x00800000;
        private const uint MSB_4 = 0x80000000;

        private const uint MSK_1 = 0x0000007F;
        private const uint MSK_2 = 0x00007F00;
        private const uint MSK_3 = 0x007F0000;
        private const uint MSK_4 = 0x7F000000;

        public int ReadProtobufVarInt()
        {
            // Start by overflowingly reading 32 bits.
            // Reading beyond the buffer contents is safe in this case,
            // because the sled ensures that we stay inside of the buffer.
            var buf = _binaryReader.ReadInt32();
            _binaryReader.BaseStream.Position--;

            // always take the first bytes; others if necessary
            var result = buf & MSK_1;
            if ((buf & MSB_1) == 0)
            {
                _binaryReader.BaseStream.Position += 1 * 8;
                return (int)result;
            }
            result |= (buf & MSK_2) >> 1;
            if ((buf & MSB_2) == 0)
            {

                _binaryReader.BaseStream.Position += 2 * 8;
                return (int)result;
            }

            result |= (buf & MSK_3) >> 2;
            if ((buf & MSB_3) == 0)
            {
                _binaryReader.BaseStream.Position += 3 * 8;
                return (int)result;
            }
            result |= (buf & MSK_4) >> 3;
            if ((buf & MSB_4) != 0) _binaryReader.BaseStream.Position += 4 * 8;

            return unchecked((int)result);
        }
    }
}
