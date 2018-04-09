using System;
using System.Collections.Generic;
using System.Text;

namespace DemoParser.Models.Structs
{
    public struct DemoInfo
    {
        public Split[] Splits { get; }

        public DemoInfo(Split[] splits)
        {
            Splits = splits;
        }
    }
}
