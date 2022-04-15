using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BIMDesignAutomation
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            // Путь к каталогу приложения
            string utilsFolderPath = @"C:\ProgramData\Autodesk\Revit\Addins\2022\BIMDesignAutomation\";

            #region вкладка "BIMDesignAutomation"

            // Создание вкладки ленты
            string tabName = "BIMDesignAutomation";
            application.CreateRibbonTab(tabName);

            #region панель "Архитектура"

            // Создание панели Архитектура
            var panel = application.CreateRibbonPanel(tabName, "Архитектура");

            #region кнопка "Автоматическое создание помещений"

            // Создание кнопки 
            var buttonInfo = new PushButtonData("AutoCreateRoomsWithRoomTags", "Автоматическое создание помещений",
                Path.Combine(utilsFolderPath, "CreateRoomsWithTags.dll"),
                "CreateRoomsWithTags.Main");

            // Назначение изображения для кнопки
            Uri uriImage1 = new Uri(@"C:\ProgramData\Autodesk\Revit\Addins\2022\BIMDesignAutomation\Images\CreateRoomsWithTags_32.png", UriKind.Absolute);
            BitmapImage largeImage1 = new BitmapImage(uriImage1);
            buttonInfo.LargeImage = largeImage1;

            //Добавление кнопки на панель
            panel.AddItem(buttonInfo);

            #endregion

            #endregion

            #endregion

            return Result.Succeeded;
        }
    }
}
