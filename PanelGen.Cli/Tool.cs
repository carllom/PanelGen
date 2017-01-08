namespace PanelGen.Cli
{
    public class Tool
    {
        public int number;
        public float diameter; // Tool diameter
        public float zStep; // Max z-step when doing multipass holes
        public float radius => diameter/2;

        public override string ToString()
        {
            return $"#{number} [{diameter};{zStep}]";
        }
    }
}
