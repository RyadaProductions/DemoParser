namespace DemoParser.Models.Structs
{
    public unsafe struct Split
    {
        public int Flags { get; }

        // original origin/viewangles
        public Vector ViewOrigin { get; }
        public QAngle ViewAngles { get; }
        public QAngle LocalViewAngles { get; }

        // Resampled origin/viewangles
        public Vector ViewOrigin2 { get; }
        public QAngle ViewAngles2 { get; }
        public QAngle LocalViewAngles2 { get; }

        public Split(int flags, Vector viewOrigin, QAngle viewAngles, QAngle localViewAngles, Vector viewOrigin2, QAngle viewAngles2, QAngle localViewAngles2)
        {
            Flags = flags;

            ViewOrigin = viewOrigin;
            ViewAngles = viewAngles;
            LocalViewAngles = localViewAngles;

            ViewOrigin2 = viewOrigin2;
            ViewAngles2 = viewAngles2;
            LocalViewAngles2 = localViewAngles2;
        }
    }
}
