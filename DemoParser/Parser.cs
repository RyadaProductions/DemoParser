using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
            var headerStruct = headerBytes.ToImpl<DemoHeader>();
            var header = headerStruct.ConvertToClass();

            if (header.Demofilestamp == DEMO_HEADER_ID && header.Demoprotocol == DEMO_PROTOCOL) return header;

            _binaryReader.Close();
            _binaryReader.Dispose();
            throw new FileLoadException("Corrupt header file detected, aborting the parsing process");
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

                ReadBitData(payload, 262144 - 4);
                //Todo: implement the rest of HandleDemoPacket()
            }
        }

        private unsafe void ReadBitData(byte* dataPtr, int bytes, int bits = -1)
        {
            var overflow = false;
            var dataBits = -1;
            var dataBytes = 0;
            StartReading(dataPtr, bytes, 0, bits);
        }

        private unsafe void StartReading(byte* dataPtr, int bytes, int startBit, int bits)
        {
            Debug.Assert(((uint)dataPtr & 3) == 0);
            var data = dataPtr + 4;
            var dataIn = data;
            var dataBytes = bytes;
            int dataBits;

            if (bits == -1)
            {
                dataBits = bytes << 3;
            }
            else
            {
                dataBits = bits;
            }
            //TODO: m_pBufferEnd = reinterpret_cast< uint32 const * > ( reinterpret_cast< unsigned char const * >( m_pData ) + nBytes );
        }
    }
}
