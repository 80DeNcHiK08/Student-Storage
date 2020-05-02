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
        private GroupViewModel Group { get; set; } = new GroupViewModel();
        public ScoolarshipList(GroupViewModel group)
        {
            InitializeComponent();
            DataContext = this;
            this.Title = "Scoolarship list for \"" + group.Name +"\"";
            foreach (var stud in group.Students)
            {
                if (stud.SponsorType != "none")
                    Group.Students.Add(stud);
            }
            TreeViewAll.ItemsSource = Group.Students;
        }

        private void Resize(object sender, RoutedEventArgs e)
        {
            //TreeViewAll.Height = this.ActualHeight - 85;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
