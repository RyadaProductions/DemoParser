namespace DemoParser.Models.Structs
{
    /// <summary>
    /// Valve please why do you need this instead of just using another vector
    /// </summary>
    public struct QAngle
    {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public QAngle(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
