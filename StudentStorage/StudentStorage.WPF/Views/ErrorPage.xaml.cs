using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentStorage.WPF.Views
{
    /// <summary>
    /// Interaction logic for ErrorPage.xaml
    /// </summary>
    public partial class ErrorPage : Window
    {
        public ErrorPage(string errorMessage)
        {
            InitializeComponent();
            ErrorMessage.Text = errorMessage;
        }

        private void OKClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
