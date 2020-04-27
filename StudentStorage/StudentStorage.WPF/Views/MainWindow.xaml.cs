using StudentStorage.Collection;
using StudentStorage.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private StudentViewModel _SelectedStudent;
        public StudentViewModel SelectedStudent
        {
            get { return _SelectedStudent; }
            set
            {
                _SelectedStudent = value;
                OnPropertyChanged("SelectedStudent");
            }
        }
        private GroupViewModel _SelectedGroup;
        public GroupViewModel SelectedGroup
        {
            get { return _SelectedGroup; }
            set
            {
                _SelectedGroup = value;
                OnPropertyChanged("SelectedGroup");
            }
        }
        private FacultyViewModel _SelectedFaculty;
        public FacultyViewModel SelectedFaculty
        {
            get { return _SelectedFaculty; }
            set
            {
                _SelectedFaculty = value;
                OnPropertyChanged("SelectedFaculty");
            }
        }
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

            #region testing values
            //Testing
            Random rand = new Random();
            Faculty<int, Group<int, Student>> f1 = new Faculty<int, Group<int, Student>>("Faculty 1");
            Faculty<int, Group<int, Student>> f2 = new Faculty<int, Group<int, Student>>("Faculty 2");
            Group<int, Student>[] groups = new Group<int, Student>[5];

            for(int j = 0; j < groups.Length; j++)
            {
                groups[j] = new Group<int, Student>("Group " + j);
                int count = rand.Next(3, 12);
                for (int i = 0; i < count; i++)
                {
                    DateTime start = new DateTime(1997, 1, 1);
                    DateTime end = new DateTime(2001, 1, 1);
                    int range = (end - start).Days;
                    DateTime birthdate = start.AddDays(rand.Next(range));
                    Student s = new Student("FName" + i, "LName" + i, "SName" + i, birthdate, float.Parse((50 + rand.NextDouble() * 50).ToString()));
                    groups[j].Add(i, s);
                }
            }

            f1.Add(0, groups[0]);
            f1.Add(1, groups[1]);
            f1.Add(2,groups[2]);
            f2.Add(3,groups[3]);
            f2.Add(4,groups[4]);

            Collection.Add(0, f1);
            Collection.Add(1, f2);

            CollectionView = new List<FacultyViewModel>();
            foreach(var c in Collection)
            {
                FacultyViewModel f = new FacultyViewModel(c.Value);
                f.Key = c.Key;
                CollectionView.Add(f);
            }
            #endregion
            
            TreeViewAll.ItemsSource = CollectionView;
            TreeViewSResult.ItemsSource = CollectionSResultsView;
        }

        private void TreeViewSelected(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateView()
        {

        }

        private void Save(object sender, RoutedEventArgs e)
        {

        }

        private void Load(object sender, RoutedEventArgs e)
        {

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
            ScoolarshipRules scoolarshipRules = new ScoolarshipRules();
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
        private void GetScoolarshipList(object sender, RoutedEventArgs e)
        {
            ScoolarshipList scoolarshipList = new ScoolarshipList();
            scoolarshipList.Resources["BorderColor"] = Resources["BorderColor"];
            scoolarshipList.Resources["ButtonColor"] = Resources["ButtonColor"];
            scoolarshipList.Resources["BGColor"] = Resources["BGColor"];
            scoolarshipList.Resources["TextColor"] = Resources["TextColor"];
            scoolarshipList.Resources["FontStyle"] = Resources["FontStyle"];
            scoolarshipList.Resources["FontFamily"] = Resources["FontFamily"];
            scoolarshipList.Resources["FontSizeSmall"] = Resources["FontSizeSmall"];
            scoolarshipList.Resources["FontSizeMedium"] = Resources["FontSizeMedium"];
            scoolarshipList.Resources["FontSizeLarge"] = Resources["FontSizeLarge"];

            scoolarshipList.Show();
        }

        private void Add(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteCurrent(object sender, RoutedEventArgs e)
        {

        }

        private void ModifyCurrent(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteAll(object sender, RoutedEventArgs e)
        {
            Collection.Clear();
            CollectionView.Clear();
            CollectionSResultsView.Clear();
            TreeViewAll.ItemsSource = TreeViewSResult.ItemsSource = null;
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
        public static RoutedUICommand GetScoolarshipList = 
            new RoutedUICommand("GetScoolarshipList",
                                "GetScoolarshipList",
                                typeof(MainWindow),
                                new InputGestureCollection()
                                {
                                    new KeyGesture(Key.U, ModifierKeys.Control)
                                });
    }
}
