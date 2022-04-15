using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateRoomsWithTags
{
    class TagsUtils
    {
        #region PipeTags

        public static List<FamilySymbol> GetPipeTagTypes(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var familySymbols = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_PipeTags)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .ToList();
            return familySymbols;

        }

        #endregion

        #region RoomTags

        public static List<RoomTagType> GetRoomTagTypes(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            List<RoomTagType> roomTagTypes = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_RoomTags)
                .WhereElementIsElementType()
                .Cast<RoomTagType>()
                .ToList();
            return roomTagTypes;

        }

        public static List<RoomTag> GetRoomTagsOnLevel(ExternalCommandData commandData, Level level)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var roomTags = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_RoomTags)
                .WhereElementIsNotElementType()
                .Where(x => (doc.GetElement(x.OwnerViewId) as View).GenLevel.Id  == level.Id)
                .Cast<RoomTag>()
                .ToList();

            return roomTags;

        }

        public static RoomTag SetRoomTagType(ExternalCommandData commandData, RoomTagType roomTagType, Level level)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            List<RoomTag> roomTags = GetRoomTagsOnLevel(commandData, level);

            using (var t = new Transaction(doc, "Изменение типа марки"))
            {
                t.Start();

                foreach (var rt in roomTags)
                {
                    rt.RoomTagType = roomTagType;
                }

                t.Commit();
            }
            return null;
        }

        #endregion

    }
}
