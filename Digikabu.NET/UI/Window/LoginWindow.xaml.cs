using System.Windows;

namespace Digikabu.NET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mvm = null;
        public MainWindow()
        {
            mvm = (MainViewModel)FindResource("mvm");
            DataContext = mvm;
            InitializeComponent();
            SetupEvents();
        }

        private void SetupEvents()
        {
            mvm.MessageBoxRequest += OnMessageBoxRequest;
        }

        private void OnMessageBoxRequest(object sender, MvvmMessageBoxEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(e.MessageBoxText, e.Caption, e.OnlyOk ? MessageBoxButton.OK : MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);

            e.ResultAction?.Invoke(result == MessageBoxResult.OK || result == MessageBoxResult.Yes);
        }
    }
}
