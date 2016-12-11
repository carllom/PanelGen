namespace PanelGen.Cli
{
    public interface IDraw
    {
        void MoveTo(float x, float y);
        void LineTo(float x, float y);
        void ArcTo(float x, float y);
    }
}
