using System.Collections.Generic;
using System.Xml;
using static StateController;

public class Xml
{
    public void SalvarTabelaQ(List<State> states)
    {
        XmlTextWriter w = new XmlTextWriter(@"..\Car-Learning\Assets\Tabela\tabela.xml", null);
        w.Formatting = Formatting.Indented;
        w.WriteComment("Verenice :)");
        w.WriteStartElement("States");
        foreach (State s in states)
        {
            w.WriteStartElement("State");
            // Atributos
            w.WriteAttributeString("name", s.gameObject.name);
            if (s.isStateInicial)
            {
                w.WriteAttributeString("initial", "Initial");
            }
            if (s.isStateFinal)
            {
                w.WriteAttributeString("final", "Final");
            }
            // Tag filhas
            w.WriteElementString("Up", s.up.ToString());
            w.WriteElementString("Bottom", s.bottom.ToString());
            w.WriteElementString("Left", s.left.ToString());
            w.WriteElementString("Right", s.right.ToString());
            w.WriteElementString("Reforco", s.Reforco.ToString());
            w.WriteEndElement();
        }
        w.WriteStartElement("Epoca");
        w.WriteElementString("Quantidade", instace.GetEpocas.ToString());
        w.WriteEndElement();
        w.WriteEndElement();
        w.Close();
    }
}
