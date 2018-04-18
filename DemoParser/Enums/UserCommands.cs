using System;
using System.Collections.Generic;
using System.Text;

namespace DemoParser.Enums
{
    public enum UserCommands
    {
        Nop = 0,
        Disconnect = 1,
        File = 2,
        SplitScreenUser = 3,
        Tick = 4,
        StringCmd = 5,
        SetConVar = 6,
        SignonState = 7,
        ServerInfo = 8,
        SendTable = 9,
        ClassInfo = 10,
        SetPause = 11,
        CreateStringTable = 12,
        UpdateStringTable = 13,
        VoiceInit = 14,
        VoiceData = 15,
        Print = 16,
        Sounds = 17,
        SetView = 18,
        FixAngle = 19,
        CrosshairAngle = 20,
        BspDecal = 21,
        SplitScreen = 22,
        UserMessage = 23,
        EntityMessage = 24,
        GameEvent = 25,
        PacketEntities = 26,
        TempEntities = 27,
        Prefetch = 28,
        Menu = 29,
        GameEventList = 30,
        GetCvarValue = 31,
        PaintmapData = 33,
        CmdKeyValues = 34,
    }
}
