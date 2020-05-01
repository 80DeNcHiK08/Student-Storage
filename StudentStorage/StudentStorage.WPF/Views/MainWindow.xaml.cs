using Microsoft.Win32;
using StudentStorage.Collection;
using StudentStorage.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;

namespace StudentStorage.WPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public BinaryTree<int, Faculty<int, Group<int, Student>>> Collection { get; set; } = new BinaryTree<int, Faculty<int, Group<int, Student>>>();
        private List<FacultyViewModel> _CollectionView = new List<FacultyViewModel>();
        public List<FacultyViewModel> CollectionView
        {
            get { return _CollectionView; }
            set
            {
                _CollectionView = value;
                OnPropertyChanged("CollectionView");
            }
        }
        private List<FacultyViewModel> _CollectionSResultsView = new List<FacultyViewModel>();
        public List<FacultyViewModel> CollectionSResultsView
        {
            get { return _CollectionSResultsView; }
            set
            {
                _CollectionSResultsView = value;
                OnPropertyChanged("CollectionSResultsView");
            }
        }
        private StudentViewModel _SelectedStudent = new StudentViewModel();
        public StudentViewModel SelectedStudent
        {
            get { return _SelectedStudent; }
            set
            {
                _SelectedStudent = value;
                OnPropertyChanged("SelectedStudent");
            }
        }
        private GroupViewModel _SelectedGroup = new GroupViewModel();
        public GroupViewModel SelectedGroup
        {
            get { return _SelectedGroup; }
            set
            {
                _SelectedGroup = value;
                OnPropertyChanged("SelectedGroup");
            }
        }
        private FacultyViewModel _SelectedFaculty = new FacultyViewModel();
        public FacultyViewModel SelectedFaculty
        {
            get { return _SelectedFaculty; }
            set
            {
                _SelectedFaculty = value;
                OnPropertyChanged("SelectedFaculty");
            }
        }
        private Object _SelectedItem;
        public Object SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }
        private List<ScoolarshipRule> _Rules = new List<ScoolarshipRule>();
        public List<ScoolarshipRule> Rules
        {
            get { return _Rules; }
            set
            {
                _Rules = value;
                OnPropertyChanged("Rules");
            }
        }
        public static string SerializationType = "Binary";
        private DateTime CurrentDate = DateTime.Now;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Rules = new List<ScoolarshipRule>()
            {
                new ScoolarshipRule("Default scoolarship", 60, 40),
                new ScoolarshipRule("Increased scoolarship", 95, 10)
            };

            TreeViewAll.ItemsSource = CollectionView;
            TreeViewSResult.ItemsSource = CollectionSResultsView;
        }

        private void TreeViewSelected(object sender, RoutedEventArgs e)
        {
            TreeViewItem tvi = e.OriginalSource as TreeViewItem;
            if(tvi != null)
            {
                if (tvi.DataContext.GetType().Name == SelectedFaculty.GetType().Name)
                {
                    Add_Info_Button.Content = "_Add group...";
                    Delete_Button.Content = "_Delete faculty";
                    Modify_Button.Content = "_Modify faculty...";
                    Delete_Button.IsEnabled = true;
                    Delete_Button.Opacity = 1;
                    Modify_Button.Opacity = 1;
                    Modify_Button.IsEnabled = true;

                    SelectedItem = (FacultyViewModel)tvi.DataContext;
                    SelectedFaculty = (FacultyViewModel)tvi.DataContext;
                    bool hasstuds = false;
                    foreach(var group in SelectedFaculty.Groups)
                    {
                        if (group.Students.Count != 0)
                            hasstuds = true;
                    }
                    AM_TB.Content = (hasstuds) ? "Faculty average mark: " + SelectedFaculty.AM
                        : "";
                }
                if (tvi.DataContext.GetType().Name == SelectedGroup.GetType().Name)
                {
                    Add_Info_Button.Content = "_Add student...";
                    Delete_Button.Content = "_Delete group";
                    Modify_Button.Content = "_Modify group...";
                    Delete_Button.IsEnabled = true;
                    Delete_Button.Opacity = 1;
                    Modify_Button.Opacity = 1;
                    Modify_Button.IsEnabled = true;

                    SelectedItem = (GroupViewModel)tvi.DataContext;
                    SelectedGroup = (GroupViewModel)tvi.DataContext;
                    AM_TB.Content = (SelectedGroup.Students.Count != 0) ? "Group average mark: " + SelectedGroup.AM : "";
                }
                if (tvi.DataContext.GetType().Name == SelectedStudent.GetType().Name)
                {
                    Add_Info_Button.Content = "_More info...";
                    Delete_Button.Content = "_Delete student";
                    Modify_Button.Content = "_Modify student...";
                    Delete_Button.IsEnabled = true;
                    Delete_Button.Opacity = 1;
                    Modify_Button.Opacity = 1;
                    Modify_Button.IsEnabled = true;


                    SelectedItem = (StudentViewModel)tvi.DataContext;
                    SelectedStudent = (StudentViewModel)tvi.DataContext;
                    AM_TB.Content = "Student average mark: " + SelectedStudent.AM;
                }
            }
        }

        private void UnselectItem(object sender, RoutedEventArgs e)
        {
            TreeView treeView = sender as TreeView;
            if(treeView != null)
            {
                treeView.Focus();
                SelectedItem = null;

                Add_Info_Button.Content = "_Add faculty...";
                Delete_Button.Content = "_Delete";
                Modify_Button.Content = "_Modify...";
                Delete_Button.IsEnabled = false;
                Delete_Button.Opacity = 0.7;
                Modify_Button.Opacity = 0.7;
                Modify_Button.IsEnabled = false;

                AM_TB.Content = "";
            }
        }

        public void UpdateView()
        {
            CollectionView = new List<FacultyViewModel>();
            Rules = Rules.OrderByDescending(x => x.MinMark).ToList();
            foreach (var c in Collection)
            {
                FacultyViewModel f = new FacultyViewModel(c.Value, Rules);
                f.Key = c.Key;
                CollectionView.Add(f);
            }
            TreeViewAll.ItemsSource = null;
            TreeViewAll.ItemsSource = CollectionView;
            Delete_All_Button.IsEnabled = true;
            Delete_All_Button.Opacity = 1;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            switch (SerializationType)
            {
                case "Binary":
                    sfd.DefaultExt = "dat";
                    sfd.AddExtension = true;
                    break;
                case "XML":
                    sfd.DefaultExt = "xml";
                    sfd.AddExtension = true;
                    break;
                default:
                    break;
            }

            sfd.ShowDialog();
            try
            {
                using (FileStream fs = new FileStream(sfd.FileName, FileMode.OpenOrCreate))
                {
                    switch (SerializationType)
                    {
                        case "Binary":
                            BinaryFormatter formatter = new BinaryFormatter();
                            formatter.Serialize(fs, Collection);
                            break;
                        case "XML":
                            XmlSerializer xmlformatter = new XmlSerializer(typeof(BinaryTree<int, Faculty<int, Group<int, Student>>>));
                            xmlformatter.Serialize(fs, Collection);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorPage errorPage = new ErrorPage(ex.Message);
                errorPage.ShowDialog();
            }
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            try
            {
                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open))
                {
                    switch (SerializationType)
                    {
                        case "Binary":
                            BinaryFormatter formatter = new BinaryFormatter();
                            Collection = (BinaryTree<int, Faculty<int, Group<int, Student>>>)formatter.Deserialize(fs);
                            break;
                        case "XML":
                            XmlSerializer xmlformatter = new XmlSerializer(typeof(BinaryTree<int, Faculty<int, Group<int, Student>>>));
                            Collection = (BinaryTree<int, Faculty<int, Group<int, Student>>>)xmlformatter.Deserialize(fs);
                            break;
                        default:
                            break;
                    }
                }
                UpdateView();
            }
            catch (Exception ex)
            {
                ErrorPage errorPage = new ErrorPage(ex.Message);
                errorPage.ShowDialog();
            }
        }

        private void AboutShow(object sender, RoutedEventArgs e)
        {
            About aboutPage = new About();
            aboutPage.Resources["BorderColor"] = Resources["BorderColor"];
            aboutPage.Resources["ButtonColor"] = Resources["ButtonColor"];
            aboutPage.Resources["BGColor"] = Resources["BGColor"];
            aboutPage.Resources["TextColor"] = Resources["TextColor"];
            aboutPage.Resources["FontStyle"] = Resources["FontStyle"];
            aboutPage.Resources["FontFamily"] = Resources["FontFamily"];
            aboutPage.Resources["FontSizeSmall"] = Resources["FontSizeSmall"];
            aboutPage.Resources["FontSizeMedium"] = Resources["FontSizeMedium"];
            aboutPage.Resources["FontSizeLarge"] = Resources["FontSizeLarge"];

            aboutPage.Show();
        }

        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            Settings settingsPage = new Settings() { Owner = this };
            settingsPage.Resources["BorderColor"] = Resources["BorderColor"];
            settingsPage.Resources["ButtonColor"] = Resources["ButtonColor"];
            settingsPage.Resources["BGColor"] = Resources["BGColor"];
            settingsPage.Resources["TextColor"] = Resources["TextColor"];
            settingsPage.Resources["FontStyle"] = Resources["FontStyle"];
            settingsPage.Resources["FontFamily"] = Resources["FontFamily"];
            settingsPage.Resources["FontSizeSmall"] = Resources["FontSizeSmall"];
            settingsPage.Resources["FontSizeMedium"] = Resources["FontSizeMedium"];
            settingsPage.Resources["FontSizeLarge"] = Resources["FontSizeLarge"];

            settingsPage.Show();
        }

        private void SetScoolarshipRules(object sender, RoutedEventArgs e)
        {
            ScoolarshipRules scoolarshipRules = new ScoolarshipRules(Rules)
            {
                Owner = this
            };
            scoolarshipRules.Resources["BorderColor"] = Resources["BorderColor"];
            scoolarshipRules.Resources["ButtonColor"] = Resources["ButtonColor"];
            scoolarshipRules.Resources["BGColor"] = Resources["BGColor"];
            scoolarshipRules.Resources["TextColor"] = Resources["TextColor"];
            scoolarshipRules.Resources["FontStyle"] = Resources["FontStyle"];
            scoolarshipRules.Resources["FontFamily"] = Resources["FontFamily"];
            scoolarshipRules.Resources["FontSizeSmall"] = Resources["FontSizeSmall"];
            scoolarshipRules.Resources["FontSizeMedium"] = Resources["FontSizeMedium"];
            scoolarshipRules.Resources["FontSizeLarge"] = Resources["FontSizeLarge"];

            scoolarshipRules.Show();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if(SelectedItem == null)
            {
                InputPage ip = new InputPage(new FacultyViewModel(), "Add");
                ip.Owner = this;

                ip.Resources["BorderColor"] = Resources["BorderColor"];
                ip.Resources["ButtonColor"] = Resources["ButtonColor"];
                ip.Resources["BGColor"] = Resources["BGColor"];
                ip.Resources["TextColor"] = Resources["TextColor"];
                ip.Resources["FontStyle"] = Resources["FontStyle"];
                ip.Resources["FontFamily"] = Resources["FontFamily"];
                ip.Resources["FontSizeSmall"] = Resources["FontSizeSmall"];
                ip.Resources["FontSizeMedium"] = Resources["FontSizeMedium"];
                ip.Resources["FontSizeLarge"] = Resources["FontSizeLarge"];

                ip.Show();

                TreeViewAll.ItemsSource = null;
                TreeViewAll.ItemsSource = CollectionView;
                Delete_All_Button.IsEnabled = true;
                Delete_All_Button.Opacity = 1;
                return;
            }

            if(SelectedItem.GetType().Name == SelectedFaculty.GetType().Name)
            {
                SelectedFaculty = (FacultyViewModel)SelectedItem;
                InputPage ip = new InputPage(new GroupViewModel() { Parent = SelectedFaculty }, "Add");
                ip.Owner = this;

                ip.Resources["BorderColor"] = Resources["BorderColor"];
                ip.Resources["ButtonColor"] = Resources["ButtonColor"];
                ip.Resources["BGColor"] = Resources["BGColor"];
                ip.Resources["TextColor"] = Resources["TextColor"];
                ip.Resources["FontStyle"] = Resources["FontStyle"];
                ip.Resources["FontFamily"] = Resources["FontFamily"];
                ip.Resources["FontSizeSmall"] = Resources["FontSizeSmall"];
                ip.Resources["FontSizeMedium"] = Resources["FontSizeMedium"];
                ip.Resources["FontSizeLarge"] = Resources["FontSizeLarge"];

                ip.Show();
                TreeViewAll.ItemsSource = null;
                TreeViewAll.ItemsSource = CollectionView;
                return;
            }

            if (SelectedItem.GetType().Name == SelectedGroup.GetType().Name)
            {
                SelectedGroup = (GroupViewModel)SelectedItem;
                StudentInput si = new StudentInput("Add new student to group \"" + SelectedGroup.Name + "\"");
                si.Owner = this;

                si.Resources["BorderColor"] = Resources["BorderColor"];
                si.Resources["ButtonColor"] = Resources["ButtonColor"];
                si.Resources["BGColor"] = Resources["BGColor"];
                si.Resources["TextColor"] = Resources["TextColor"];
                si.Resources["FontStyle"] = Resources["FontStyle"];
                si.Resources["FontFamily"] = Resources["FontFamily"];
                si.Resources["FontSizeSmall"] = Resources["FontSizeSmall"];
                si.Resources["FontSizeMedium"] = Resources["FontSizeMedium"];
                si.Resources["FontSizeLarge"] = Resources["FontSizeLarge"];

                si.Show();
                TreeViewAll.ItemsSource = null;
                TreeViewAll.ItemsSource = CollectionView;
                return;
            }

            if (SelectedItem.GetType().Name == SelectedStudent.GetType().Name)
            {
                SelectedStudent = (StudentViewModel)SelectedItem;
                Info si = new Info(SelectedStudent);
                si.Resources["BorderColor"] = Resources["BorderColor"];
                si.Resources["ButtonColor"] = Resources["ButtonColor"];
                si.Resources["BGColor"] = Resources["BGColor"];
                si.Resources["TextColor"] = Resources["TextColor"];
                si.Resources["FontStyle"] = Resources["FontStyle"];
                si.Resources["FontFamily"] = Resources["FontFamily"];
                si.Resources["FontSizeSmall"] = Resources["FontSizeSmall"];
                si.Resources["FontSizeMedium"] = Resources["FontSizeMedium"];
                si.Resources["FontSizeLarge"] = Resources["FontSizeLarge"];

                si.Show();
                return;
            }
        }

        private void DeleteCurrent(object sender, RoutedEventArgs e)
        {
            if (SelectedItem.GetType().Name == SelectedFaculty.GetType().Name)
            {
                SelectedFaculty = (FacultyViewModel)SelectedItem;
                Collection.Remove(SelectedFaculty.Key, true);
                CollectionView.Remove(SelectedFaculty);
            }

            if (SelectedItem.GetType().Name == SelectedGroup.GetType().Name)
            {
                SelectedGroup = (GroupViewModel)SelectedItem;
                Faculty<int, Group<int, Student>> faculty;
                Collection.TryGetValue(SelectedGroup.Parent.Key, out faculty, true);
                faculty.Remove(SelectedGroup.Key, true);
                SelectedGroup.Parent.Groups.Remove(SelectedGroup);
            }

            if (SelectedItem.GetType().Name == SelectedStudent.GetType().Name)
            {
                SelectedStudent = (StudentViewModel)SelectedItem;
                Faculty<int, Group<int, Student>> faculty;
                Collection.TryGetValue(SelectedStudent.Parent.Parent.Key, out faculty, true);
                Group<int, Student> group;
                faculty.TryGetValue(SelectedStudent.Parent.Key, out group, true);
                group.Remove(SelectedStudent.Key, true);
                SelectedStudent.Parent.Students.Remove(SelectedStudent);
            }

            TreeViewAll.ItemsSource = null;
            TreeViewAll.ItemsSource = CollectionView;
        }

        private void ModifyCurrent(object sender, RoutedEventArgs e)
        {
            if (SelectedItem.GetType().Name == SelectedFaculty.GetType().Name)
            {
                InputPage ip = new InputPage((FacultyViewModel)SelectedItem, "Modify");
                ip.Owner = this;
                ip.Resources["BorderColor"] = Resources["BorderColor"];
                ip.Resources["ButtonColor"] = Resources["ButtonColor"];
                ip.Resources["BGColor"] = Resources["BGColor"];
                ip.Resources["TextColor"] = Resources["TextColor"];
                ip.Resources["FontStyle"] = Resources["FontStyle"];
                ip.Resources["FontFamily"] = Resources["FontFamily"];
                ip.Resources["FontSizeSmall"] = Resources["FontSizeSmall"];
                ip.Resources["FontSizeMedium"] = Resources["FontSizeMedium"];
                ip.Resources["FontSizeLarge"] = Resources["FontSizeLarge"];

                ip.Show();

                TreeViewAll.ItemsSource = null;
                TreeViewAll.ItemsSource = CollectionView;
            }

            if (SelectedItem.GetType().Name == SelectedGroup.GetType().Name)
            {
                InputPage ip = new InputPage((GroupViewModel)SelectedItem, "Modify");
                ip.Owner = this;
                ip.Resources["BorderColor"] = Resources["BorderColor"];
                ip.Resources["ButtonColor"] = Resources["ButtonColor"];
                ip.Resources["BGColor"] = Resources["BGColor"];
                ip.Resources["TextColor"] = Resources["TextColor"];
                ip.Resources["FontStyle"] = Resources["FontStyle"];
                ip.Resources["FontFamily"] = Resources["FontFamily"];
                ip.Resources["FontSizeSmall"] = Resources["FontSizeSmall"];
                ip.Resources["FontSizeMedium"] = Resources["FontSizeMedium"];
                ip.Resources["FontSizeLarge"] = Resources["FontSizeLarge"];

                ip.Show();

                TreeViewAll.ItemsSource = null;
                TreeViewAll.ItemsSource = CollectionView;
            }

            if (SelectedItem.GetType().Name == SelectedStudent.GetType().Name)
            {
                SelectedStudent = (StudentViewModel)SelectedItem;
                StudentInput si = new StudentInput((StudentViewModel)SelectedItem);
                si.Owner = this;
                si.Resources["BorderColor"] = Resources["BorderColor"];
                si.Resources["ButtonColor"] = Resources["ButtonColor"];
                si.Resources["BGColor"] = Resources["BGColor"];
                si.Resources["TextColor"] = Resources["TextColor"];
                si.Resources["FontStyle"] = Resources["FontStyle"];
                si.Resources["FontFamily"] = Resources["FontFamily"];
                si.Resources["FontSizeSmall"] = Resources["FontSizeSmall"];
                si.Resources["FontSizeMedium"] = Resources["FontSizeMedium"];
                si.Resources["FontSizeLarge"] = Resources["FontSizeLarge"];

                si.Show();

                TreeViewAll.ItemsSource = null;
                TreeViewAll.ItemsSource = CollectionView;
            }
        }

        private void DeleteAll(object sender, RoutedEventArgs e)
        {
            Collection.Clear();
            CollectionView.Clear();
            CollectionSResultsView.Clear();
            TreeViewAll.ItemsSource = TreeViewSResult.ItemsSource = null;
            Add_Info_Button.Content = "_Add faculty...";
            Delete_Button.Content = "_Delete";
            Modify_Button.Content = "_Modify...";
            Delete_All_Button.IsEnabled = false;
            Delete_All_Button.Opacity = 0.7;
            Delete_Button.IsEnabled = false;
            Delete_Button.Opacity = 0.7;
            Modify_Button.Opacity = 0.7;
            Modify_Button.IsEnabled = false;
            AM_TB.Content = "";
        }

        private void Find(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if(((System.Windows.Controls.TextBox)sender).Text == string.Empty)
            {
                TreeViewSResult.ItemsSource = null;
                return;
            }
            CollectionSResultsView = new List<FacultyViewModel>();
            bool hasStudRes = false, hasGroupRes = false;

            foreach (var faculty in CollectionView)
            {
                FacultyViewModel fvm = new FacultyViewModel();
                fvm.Name = faculty.Name;
                foreach (var group in faculty.Groups)
                {
                    GroupViewModel gvm = new GroupViewModel();
                    gvm.Name = group.Name;
                    foreach (var student in group.Students)
                    {
                        if (student.ConcatedName.ToLower().Contains(((System.Windows.Controls.TextBox)sender).Text.ToLower()))
                        {
                            gvm.Students.Add(student);
                            hasStudRes = true;
                        }
                    }
                    if(hasStudRes)
                    {
                        fvm.Groups.Add(gvm);
                        continue;
                    }
                    if (group.Name.ToLower().Contains(((System.Windows.Controls.TextBox)sender).Text.ToLower()))
                    {
                        fvm.Groups.Add(gvm);
                    }
                }
                if(hasGroupRes || hasStudRes)
                {
                    CollectionSResultsView.Add(fvm);
                    continue;
                }
                if (faculty.Name.ToLower().Contains(((System.Windows.Controls.TextBox)sender).Text.ToLower()))
                {
                    CollectionSResultsView.Add(fvm);
                }
            }
            TreeViewSResult.ItemsSource = CollectionSResultsView;
        }
    }

    public static class CustomCommands
    {
        public static RoutedUICommand ExitClick =
            new RoutedUICommand("ExitClick", 
                                "ExitClick", 
                                typeof(MainWindow),
                                new InputGestureCollection()
                                {
                                    new KeyGesture(Key.Escape, ModifierKeys.Control)
                                });
        public static RoutedUICommand AboutShow =
            new RoutedUICommand("AboutShow",
                                "AboutShow",
                                typeof(MainWindow),
                                new InputGestureCollection()
                                {
                                    new KeyGesture(Key.I, ModifierKeys.Control)
                                });
        public static RoutedUICommand OpenSettings =
            new RoutedUICommand("OpenSettings",
                                "OpenSettings",
                                typeof(MainWindow),
                                new InputGestureCollection()
                                {
                                    new KeyGesture(Key.OemComma, ModifierKeys.Control, "Ctrl+,")
                                });

        public static RoutedUICommand SetScoolarshipRules =
            new RoutedUICommand("SetScoolarshipRules",
                                "SetScoolarshipRules",
                                typeof(MainWindow),
                                new InputGestureCollection()
                                {
                                    new KeyGesture(Key.Y, ModifierKeys.Control)
                                });
        /*public static RoutedUICommand GetScoolarshipList = 
            new RoutedUICommand("GetScoolarshipList",
                                "GetScoolarshipList",
                                typeof(MainWindow),
                                new InputGestureCollection()
                                {
                                    new KeyGesture(Key.U, ModifierKeys.Control)
                                });*/
    }
}
