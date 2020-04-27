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
        public List<FacultyViewModel> CollectionView { get; set; }
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

            Random rand = new Random();
            //Testing
            Student[] students = new Student[10];
            Group<int, Student> group = new Group<int, Student>("Group 1");
            for(int i = 0; i < 10; i++)
            {
                students[i] = new Student("LName" + i, "FName" + i, "SName" + i, CurrentDate, float.Parse((50 + rand.NextDouble() * 50).ToString()));
                group.Add(students[i]);
            }
            Faculty<int, Group<int, Student>> faculty = new Faculty<int, Group<int, Student>>("Faculty 1");
            faculty.Add(group);
            Collection.Add(faculty);

            CollectionView = new List<FacultyViewModel>()
            {
                new FacultyViewModel()
                {
                    Name = "Faculty 1",
                    Groups = new ObservableCollection<GroupViewModel>()
                    {
                        new GroupViewModel()
                        {
                            Name = "Group 1.1",
                            Students = new ObservableCollection<StudentViewModel>()
                            {
                                new StudentViewModel(students[0]),
                                new StudentViewModel(students[1]),
                                new StudentViewModel(students[2])
                            }
                        },
                        new GroupViewModel()
                        {
                            Name = "Group 1.2",
                            Students = new ObservableCollection<StudentViewModel>()
                            {
                                new StudentViewModel(students[3]),
                                new StudentViewModel(students[4]),
                                new StudentViewModel(students[5]),
                                new StudentViewModel(students[6])
                            }
                        }
                    }
                },
                new FacultyViewModel()
                {
                    Name = "Faculty 2",
                    Groups = new ObservableCollection<GroupViewModel>()
                    {
                        new GroupViewModel()
                        {
                            Name = "Group 2.2",
                            Students = new ObservableCollection<StudentViewModel>()
                            {
                                new StudentViewModel(students[7]),
                                new StudentViewModel(students[8]),
                                new StudentViewModel(students[9])
                            }
                        }
                    }
                }
            };

            TreeViewAll.ItemsSource = CollectionView;
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
