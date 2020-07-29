using System.Collections.Generic;
using System.Linq;
using static System.Console;
using BasicXml.Library.Interfaces.Publications;

namespace BasicXml.Console
{
    public class BasicViewer
    {
        public void ShowPublicationElements(IEnumerable<Publication> elements)
        {
            elements.Select((e, index) => new
            {
                index = ++index,
                info = e
            })
                .ToList()
                .ForEach(e => WriteLine($"{e.index}: {e.info}"));
        }

        public void ShowWrongElements(IEnumerable<Publication> elements)
        {
            if (elements==null)
            {
                WriteLine("Has not wrong elements");
            }
            else
            {
                elements.Select((p, index) => new
                    {
                        index = ++index,
                        Info = p != null ? p.ToString() : "Empty"
                    })
                    .ToList()
                    .ForEach(e => WriteLine($"{e.index}: {e.Info}"));
            }
        }
    }
}