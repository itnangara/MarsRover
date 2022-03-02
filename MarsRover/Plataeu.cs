namespace MarsRover
{
    /*
      the plateu is defined in this class
      */

    public class SurfaceSize
    {
        public int Width { get; }
        public int Height { get; }

        public SurfaceSize(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }

    public class Plataeu : ILandingSurface
    {
        public SurfaceSize Size { get; private set; }
        
        public void Define(int width, int height)
        {
            Size = new SurfaceSize(width, height);
        }
    }
}