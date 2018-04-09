namespace DemoParser.Enums
{
    public enum DemoCmds : byte
    {
            // it's a startup message, process as fast as possible
            Signon = 1,
            // it's a normal network packet that we stored off
            Packet,
            // sync client clock to demo tick
            Synctick,
            // console command
            Consolecmd,
            // user input command
            Usercmd,
            // network data tables
            Datatables,
            // end of time.
            Stop,
            // a blob of binary data understood by a callback function
            Customdata,

            Stringtables,

            // Last command
            Lastcmd = Stringtables
    }
}
