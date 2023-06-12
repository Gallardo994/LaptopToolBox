using CustomControls;
using GHelper.Updates.UI;
using Ninject;

namespace GHelper.Updates
{
    public partial class UpdatesWindow : RForm, IUpdatesWindow
    {
        private readonly IUpdatesChecker _updatesChecker;
        private readonly IUpdatesScheduler _updatesScheduler;
        private readonly IModelInfoProvider _modelInfoProvider;
        
        [Inject]
        public UpdatesWindow(IUpdatesChecker updatesChecker,
            IUpdatesScheduler updatesScheduler,
            IModelInfoProvider modelInfoProvider)
        {
            _updatesChecker = updatesChecker;
            _updatesScheduler = updatesScheduler;
            _modelInfoProvider = modelInfoProvider;
            
            InitializeComponent();
            InitTheme();
            
            Text = $@"{Properties.Strings.BiosAndDriverUpdates}: {_modelInfoProvider.Model} {_modelInfoProvider.Bios}";

            tableUpdates.DataSource = _updatesChecker.AllUpdates;

            //Hide();
        }

        public void SetState(bool state)
        {
            if (state)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }
        
        private void Refresh_Pressed(object? sender, EventArgs e)
        {
            tableUpdates.DataSource = null;
            tableUpdates.DataSource = _updatesChecker.AllUpdates;
        }
    }
}
