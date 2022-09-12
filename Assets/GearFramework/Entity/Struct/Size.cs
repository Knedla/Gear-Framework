using System;

namespace GearFramework.Entity
{
    [Serializable]
    readonly public struct Size
    {
        public int Width { get; }
        public int Height { get; }
        public int Area => Width * Height;
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public override string ToString() => $"{Width} {Height}";
    }
}
