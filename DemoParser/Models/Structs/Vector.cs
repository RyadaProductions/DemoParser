namespace DemoParser.Models.Structs
{
    /// <summary>
    /// 3d space representation.
    /// </summary>
    public struct Vector
    {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public Vector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
