using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateRoomsWithTags
{
    class RoomsUtils
    {
        public static List<Room> CreateRooms(ExternalCommandData commandData, Level level, Phase phase)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            List<Room> rooms = new List<Room>();
            List<ElementId> levelIds = LevelsUtils.SortLevelsByElevation(LevelsUtils.GetLevelsAboveZero(commandData));
            int lNum = levelIds.IndexOf(level.Id) + 1;

            using (var t = new Transaction(doc, "Создание помещений"))
            {
                t.Start();

                ICollection<ElementId> elementIds = doc.Create.NewRooms2(level, phase);
                foreach (var e in elementIds)
                {
                    Room room = doc.GetElement(e) as Room;
                    room.LookupParameter("Номер этажа").Set(lNum);
                    int rNum = (elementIds as List<ElementId>).IndexOf(e) + 1;
                    room.LookupParameter("Номер на этаже").Set(String.Format("{0:D2}", rNum));
                    rooms.Add(room);
                }

                t.Commit();
            }
            return rooms;
        }

        public static XYZ GetRoomPoint(ExternalCommandData commandData, Room room)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            LocationPoint location = room.Location as LocationPoint;
            XYZ point = location.Point;
            return point;
        }

    }
}
