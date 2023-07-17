using LaptopToolBox.Helpers;

namespace LaptopToolBox.AppWindows
{
    public sealed partial class ToastWindow
    {
        private byte _alpha;

        public byte Alpha
        {
            get => _alpha;
            set
            {
                _alpha = value;
                WindowHelper.SetWindowAlpha(this, _alpha);
            }
        }

        public ToastWindow()
        {
            InitializeComponent();
        }
    }
}
