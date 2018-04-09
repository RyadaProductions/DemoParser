namespace DemoParser.Models
{
    public class Header
    {
        /// <summary>
        /// Should be HL2DEMO
        /// </summary>
        public string Demofilestamp { get; set; }

        /// <summary>
        /// Should be DEMO_PROTOCOL
        /// </summary>
        public int Demoprotocol { get; set; }

        /// <summary>
        /// Should be PROTOCOL_VERSION
        /// </summary>
        public int Networkprotocol { get; set; }

        /// <summary>
        /// Name of server
        /// </summary>
        public string Servername { get; set; }

        /// <summary>
        /// Name of client who recorded the game
        /// </summary>
        public string Clientname { get; set; }

        /// <summary>
        /// Name of map
        /// </summary>
        public string Mapname { get; set; }

        /// <summary>
        /// Name of game directory (com_gamedir)
        /// </summary>
        public string Gamedirectory { get; set; }

        /// <summary>
        /// Time of track
        /// </summary>
        public float PlaybackTime { get; set; }

        /// <summary>
        /// # of ticks in track
        /// </summary>
        public int PlaybackTicks { get; set; }

        /// <summary>
        /// # of frames in track
        /// </summary>
        public int PlaybackFrames { get; set; }

        /// <summary>
        /// length of sigondata in bytes
        /// </summary>
        public int Signonlength { get; set; }
    }
}
