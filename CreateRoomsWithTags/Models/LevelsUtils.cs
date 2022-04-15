using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateRoomsWithTags
{
    public class LevelsUtils
    {
        public static List<Level> GetLevels(ExternalCommandData commandData)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;
            List<Level> levels = new FilteredElementCollector(doc)
                                                .OfClass(typeof(Level))
                                                .Cast<Level>()
                                                .ToList();
            return levels;
        }

        public static List<Level> GetLevelsAboveZero(ExternalCommandData commandData)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;
            List<Level> levels = new FilteredElementCollector(doc)
                                                .OfClass(typeof(Level))
                                                .Cast<Level>()
                                                .Where(x => x.Elevation >= 0)
                                                .ToList();
            return levels;
        }


        public static List<ElementId> SortLevelsByElevation(List<Level> levels)
        {
            for (int i = 0; i < levels.Count - 1; i++)
            {
                for (int j = i + 1; j < levels.Count; j++)
                {
                    if (levels[i].Elevation > levels[j].Elevation)
                    {
                        var t = levels[i];
                        levels[i] = levels[j];
                        levels[j] = t;
                    }
                }
            }
            List<ElementId> levelList = new List<ElementId>();
            foreach (var l in levels)
            {
                levelList.Add(l.Id);
            }
            return levelList;
        }

    }
}
