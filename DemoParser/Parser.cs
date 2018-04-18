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
                    break;
                case DemoCmds.Datatables:
                    //TODO: parse DataTable packet
                    //TODO: Map weapons and bind all entities
                    break;
                case DemoCmds.Stringtables:
                    //TODO: ParseHeader stringtable packet
                    break;
                case DemoCmds.Usercmd:
                    break;
                case DemoCmds.Signon:
                case DemoCmds.Packet:
                    HandleDemoPacket();
                    break;
                default:
                    throw new Exception("Can't handle Demo-Command " + command);
            }
        }

        public void HandleDemoPacket()
        {
            var info = new CommandInfo(new Split[2]);
            var dummy = 0;

            _binaryReader.ReadCmdInfo(ref info);
            _binaryReader.ReadSequenceInfo(ref dummy, ref dummy);

            var length = _binaryReader.ReadInt32();

            var chunk = _binaryReader.ReadBytes(length).AsSpan();
            var offset = 0;

            while (offset < chunk.Length)
            {
                //TODO: this needs to be bitoperations instead of straight up 4 bytes
                var cmd = chunk.Slice(offset, 4).ToImplSpan<int>();
                offset += 4;
                var size = chunk.Slice(offset, 4).ToImplSpan<int>();
                offset += 4;

                var messageBytes = chunk.Slice(offset, size);
                offset += length;

                var messageType = (UserCommands)cmd;

                ParseMessageBuffer(messageType, messageBytes);


            }
            //Todo: implement the rest of HandleDemoPacket()
        }

        private unsafe void ParseMessageBuffer(UserCommands messageType, ReadOnlySpan<byte> buffer)
        {
            switch (messageType)
            {
                case UserCommands.PacketEntities:
                    break;
                case UserCommands.GameEventList:
                    break;
                case UserCommands.GameEvent:
                    break;
                case UserCommands.CreateStringTable:
                    break;
                case UserCommands.UpdateStringTable:
                    break;
                case UserCommands.Tick:
                    break;
                case UserCommands.UserMessage:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(messageType), messageType, null);
            }
        }
    }
}
