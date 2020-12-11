using Digikabu.NET.Persistence;

namespace Digikabu.NET.UI.Window
{
    /// <summary>
    /// Interaktionslogik für MasterWindow.xaml
    /// </summary>
    public partial class MasterWindow : System.Windows.Window
    {
        public MasterWindow()
        {
            InitializeComponent();
            tb.Text = DigiCon.GetScheduleOfTodayAsync().Result;
        }
    }
}
