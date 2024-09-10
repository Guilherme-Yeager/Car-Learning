using System.Collections.Generic;
using System.Xml;
using static StateController;

public class Xml
{
    public void SalvarTabelaQ(List<State> states)
    {
        XmlTextWriter w = new XmlTextWriter(@"..\Car-Learning\Assets\Tabela\tabela.xml", null);
        w.Formatting = Formatting.Indented;
        w.WriteRaw("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>");
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
            w.WriteElementString("Reinforcement", s.Reforco.ToString());
            w.WriteEndElement();
        }
        w.WriteStartElement("Epoch");
        w.WriteElementString("Quantity", instace.Epocas.ToString());
        w.WriteEndElement();
        w.WriteEndElement();
        w.WriteComment("Verenice :)");
        w.Close();
    }
}
