using System.Windows;
using System.Windows.Forms; 

namespace BackupAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var config = ConfigManager.GetConfigs();
            sourceTxt.Text = config.SourceFolder;
            targetTxt.Text = config.TargetFolder;
        }


        private void SourceBtn_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sourceTxt.Text = dialog.SelectedPath; 
            }
        }

        private void TargetBtn_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                targetTxt.Text = dialog.SelectedPath; 
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var pathConfig = new PathConfig
            {
                SourceFolder = sourceTxt.Text,
                TargetFolder = targetTxt.Text
            };

            ConfigManager.UpdatePathConfig(pathConfig);
            System.Windows.MessageBox.Show("Successfully saved path"); 
        }
    }
}
