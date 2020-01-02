using System.IO;

namespace PanelGen.Cli.FileFormats
{
    public interface IPanelGenFileStrategy
    {
        PanelGenProject Read(Stream s);
        void Write(Stream s, PanelGenProject  pgp);

        //void Visit(PanelGenProject pgp);
        //void Visit(PanelComponent pc);

        //void Visit(PanelStock ps);
        //void Visit(Tool t);

        //void Visit(CircularPocket cp);
        //void Visit(Dial d);
        //void Visit(PolyLine pl);
        //void Visit(RectangularPocket rp);
        //void Visit(Text txt);
    }
}
