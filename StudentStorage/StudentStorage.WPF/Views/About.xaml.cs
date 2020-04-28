using System.Reflection;
using System.Windows;

namespace StudentStorage.WPF.Views
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
#pragma warning disable CS0436 // Type conflicts with imported type
            WPFNameLabel.Text = AssemblyInfo.Constants.TITLE + " (GUI for lab 2-3)";
            WPFCopyrightLabel.Text = AssemblyInfo.Constants.COPYRIGHT + " All Rights reserved.";
            WPFVersionLabel.Text = "Version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
#pragma warning restore CS0436 // Type conflicts with imported type
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
