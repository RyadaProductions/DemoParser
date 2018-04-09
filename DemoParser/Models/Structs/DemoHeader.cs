namespace DemoParser.Models.Structs
{
    public unsafe struct DemoHeader
    {
        // Should be HL2DEMO
        public fixed byte Demofilestamp[8];
        // Should be DEMO_PROTOCOL
        public int Demoprotocol;
        // Should be PROTOCOL_VERSION
        public int Networkprotocol;
        // Name of server
        public fixed byte Servername[260];
        // Name of client who recorded the game
        public fixed byte Clientname[260];
        // Name of map
        public fixed byte Mapname[260];
        // Name of game directory (com_gamedir)
        public fixed byte Gamedirectory[260];
        // Time of track
        public float PlaybackTime;
        // # of ticks in track
        public int PlaybackTicks;
        // # of frames in track
        public int PlaybackFrames;
        // length of sigondata in bytes
        public int Signonlength;
    }
}
