using System;
using System.Collections.Generic;
using System.Text;

namespace DemoParser.Models.Structs
{
    public struct CommandInfo
    {
        public Split[] Splits { get; }

        public CommandInfo(Split[] splits)
        {
            Splits = splits;
        }
    }
}
