using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateRoomsWithTags
{
    class ViewUtils
    {
        public static List<Phase> GetPhases(ExternalCommandData commandData)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            List<Phase> phases = new List<Phase>();
            for (int i = 0; i < doc.Phases.Size; i++)
            {
                phases.Add(doc.Phases.get_Item(i));
            }
            return phases;
        }

    }
}
