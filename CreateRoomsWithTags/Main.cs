using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateRoomsWithTags
{
    [Transaction(TransactionMode.Manual)] 
    public class Main : IExternalCommand  // реализуем интерфейс IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var window = new MainView(commandData);
            window.ShowDialog(); //открываем диалоговое окно
            return Result.Succeeded;
        }
    }
}
