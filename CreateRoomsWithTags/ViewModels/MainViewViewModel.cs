using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateRoomsWithTags
{
    class MainViewViewModel
    {
        private ExternalCommandData _commandData;

        public DelegateCommand SaveCommand { get; }
        public List<Level> Levels { get; }
        public List<RoomTagType> RoomTagTypes { get; }
        public List<Phase> Phases { get; }
        public Level SelectedLevel { get; set; }
        public RoomTagType SelectedTagType { get; set; }
        public Phase SelectedPhase { get; set; }

        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            Levels = LevelsUtils.GetLevels(commandData);
            RoomTagTypes = TagsUtils.GetRoomTagTypes(commandData);
            Phases = ViewUtils.GetPhases(commandData);
            SaveCommand = new DelegateCommand(OnSaveCommand);
        }

        private void OnSaveCommand()
        {
            _ = RoomsUtils.CreateRooms(_commandData, SelectedLevel, SelectedPhase); //создание помещений на выбранном уровне и выбранной стадии
            _ = TagsUtils.SetRoomTagType(_commandData, SelectedTagType, SelectedLevel); //назначить маркам помещений выбранный тип марки

            RaiseCloseRequest();
        }


        public event EventHandler CloseRequest;
        private void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }

    }
}
