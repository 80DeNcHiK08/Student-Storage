using StudentStorage.Collection;
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
    /// Interaction logic for InputPage.xaml
    /// </summary>
    public partial class InputPage : Window
    {
        private string Mode = "Add";
        private string CurrentType;
        public InputPage(FacultyViewModel faculty, string mode)
        {
            InitializeComponent();
            this.Mode = mode;
            Name_Label.Content = "Faculty name: ";
            this.CurrentType = "Faculty";
            if(Mode == "Add")
            {
                this.Title = "Add new faculty";
                AddNextButton.Content = "Add next faculty";
                AddButton.Content = "Save faculty";
            }

            if(Mode == "Modify")
            {
                this.Title = "Modify \"" + faculty.Name + "\" faculty";
                AddNextButton.Visibility = Visibility.Hidden;
                AddButton.Content = "Save faculty";
                NameField.Text = faculty.Name;
            }
        }

        public InputPage(GroupViewModel group, string mode)
        {
            InitializeComponent();
            this.Mode = mode;
            Name_Label.Content = "Group name: ";
            this.CurrentType = "Group";
            if (Mode == "Add")
            {
                this.Title = "Add new group to \"" + group.Parent.Name + "\"";
                AddNextButton.Content = "Add next group";
                AddButton.Content = "Save group";
            }

            if (Mode == "Modify")
            {
                this.Title = "Modify \"" + group.Name + "\" group";
                AddNextButton.Visibility = Visibility.Hidden;
                AddButton.Content = "Save group";
                NameField.Text = group.Name;
            }
        }
        private void AddOrModify()
        {
            if (this.Mode == "Add")
            {
                if (this.CurrentType == "Faculty")
                {
                    var faculty = new Faculty<int, Group<int, Student>>(NameField.Text);
                    int newkey = 0;
                    if (((MainWindow)Owner).Collection.Count == 0)
                        newkey = 1;
                    else
                        newkey = ((MainWindow)Owner).Collection.MaxValue.Key + 1;

                    ((MainWindow)Owner).Collection.Add(newkey, faculty);
                    var fvm = new FacultyViewModel(faculty);
                    fvm.Key = newkey;
                    ((MainWindow)Owner).CollectionView.Add(fvm);
                }
                else
                {
                    Faculty<int, Group<int, Student>> faculty;
                    ((MainWindow)Owner).Collection.TryGetValue(((MainWindow)Owner).SelectedFaculty.Key, out faculty, true);
                    var group = new Group<int, Student>(NameField.Text);
                    int newkey = 0;
                    if (faculty.Count == 0)
                        newkey = 1;
                    else
                        newkey = faculty.MaxValue.Key + 1;

                    faculty.Add(newkey, group);
                    var gvm = new GroupViewModel(group, ((MainWindow)Owner).SelectedFaculty);
                    gvm.Key = newkey;
                    ((MainWindow)Owner).SelectedFaculty.Groups.Add(gvm);
                }
            }
            if (this.Mode == "Modify")
            {
                if (this.CurrentType == "Faculty")
                {
                    Faculty<int, Group<int, Student>> faculty;
                    ((MainWindow)Owner).Collection.TryGetValue(((MainWindow)Owner).SelectedFaculty.Key, out faculty, true);
                    faculty.FacultyName = NameField.Text;

                    ((MainWindow)Owner).CollectionView[((MainWindow)Owner).CollectionView.IndexOf(((MainWindow)Owner).SelectedFaculty)].Name = faculty.FacultyName;
                }
                else
                {
                    Faculty<int, Group<int, Student>> faculty;
                    ((MainWindow)Owner).Collection.TryGetValue(((MainWindow)Owner).SelectedGroup.Parent.Key, out faculty, true);
                    Group<int, Student> group;
                    faculty.TryGetValue(((MainWindow)Owner).SelectedGroup.Key, out group, true);
                    group.GroupName = NameField.Text;

                    ((MainWindow)Owner).CollectionView[((MainWindow)Owner).CollectionView.IndexOf(((MainWindow)Owner).SelectedGroup.Parent)].Groups[((MainWindow)Owner).CollectionView[((MainWindow)Owner).CollectionView.IndexOf(((MainWindow)Owner).SelectedGroup.Parent)].Groups.IndexOf(((MainWindow)Owner).SelectedGroup)].Name = NameField.Text;
                }
            }

            ((MainWindow)Owner).TreeViewAll.ItemsSource = null;
            ((MainWindow)Owner).TreeViewAll.ItemsSource = ((MainWindow)Owner).CollectionView;
        }


        private void AddNext(object sender, RoutedEventArgs e)
        {
            AddOrModify();
            
            NameField.Text = "";

            AddButton.IsEnabled = false;
            AddButton.Opacity = 0.7;
            AddNextButton.IsEnabled = false;
            AddNextButton.Opacity = 0.7;
        }

        private void SaveCurrent(object sender, RoutedEventArgs e)
        {
            AddOrModify();
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ValueChanged(object sender, RoutedEventArgs e)
        {
            if (NameField.Text != string.Empty)
            {
                AddButton.IsEnabled = true;
                AddButton.Opacity = 1;
                AddNextButton.IsEnabled = true;
                AddNextButton.Opacity = 1;
            } else
            {
                AddButton.IsEnabled = false;
                AddButton.Opacity = 0.7;
                AddNextButton.IsEnabled = false;
                AddNextButton.Opacity = 0.7;
            }
        }
    }
}
