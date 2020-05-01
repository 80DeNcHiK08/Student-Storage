using StudentStorage.WPF.ViewModels;
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
using System.Windows.Shapes;

namespace StudentStorage.WPF.Views
{
    /// <summary>
    /// Interaction logic for ScoolarshipList.xaml
    /// </summary>
    public partial class ScoolarshipList : Window
    {
        private GroupViewModel Group { get; set; }
        public ScoolarshipList(GroupViewModel group)
        {
            InitializeComponent();
            DataContext = this;
            Group = group;
            this.Title = "Scoolarship list for \"" + group.Name +"\"";
            TreeViewAll.ItemsSource = Group.Students;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
