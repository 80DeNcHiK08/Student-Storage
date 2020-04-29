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
    /// Interaction logic for StudentInput.xaml
    /// </summary>
    public partial class StudentInput : Window
    {
        private string Mode = "Add";
        public StudentInput()
        {
            InitializeComponent();
            this.Title = "Add new student";
            DataContext = this;
        }

        public StudentInput(StudentViewModel student)
        {
            InitializeComponent();
            this.Title = student.ConcatedName + " - info";
            this.Mode = "Modify";
            this.Title = "Modify selected student";
            AddNextButton.Visibility = Visibility.Hidden;
            StudentLName.Text = student.ConcatedName.Split(' ')[0];
            StudentFName.Text = student.ConcatedName.Split(' ')[1];
            StudentSName.Text = student.ConcatedName.Split(' ')[2];
            StudentBDate.Value = new DateTime(int.Parse(student.BirthDate.Split('.')[2].Substring(0, student.BirthDate.Split('.')[2].IndexOf(' '))), int.Parse(student.BirthDate.Split('.')[1]), int.Parse(student.BirthDate.Split('.')[0]));
            StudentAM.Value = Double.Parse(student.AM);
        }

        private void AddOrModify()
        {
            Student newstud = new Student(StudentFName.Text, StudentLName.Text, StudentSName.Text, StudentBDate.Value.GetValueOrDefault(), float.Parse(StudentAM.Text));
            if (this.Mode == "Add")
            {
                Faculty<int, Group<int, Student>> faculty;
                ((MainWindow)Owner).Collection.TryGetValue(((MainWindow)Owner).SelectedGroup.Parent.Key, out faculty, true);
                Group<int, Student> group;
                faculty.TryGetValue(((MainWindow)Owner).SelectedGroup.Key, out group, true);
                if (group.Count == 0)
                    group.Add(1, newstud);
                else
                    group.Add(group.MaxValue.Key + 1, newstud);

                ((MainWindow)Owner).SelectedGroup.Students.Add(new StudentViewModel(newstud, ((MainWindow)Owner).SelectedGroup, ((MainWindow)Owner).Rules));
            }
            if (this.Mode == "Modify")
            {
                Faculty<int, Group<int, Student>> faculty;
                ((MainWindow)Owner).Collection.TryGetValue(((MainWindow)Owner).SelectedStudent.Parent.Parent.Key, out faculty, true);
                Group<int, Student> group;
                faculty.TryGetValue(((MainWindow)Owner).SelectedStudent.Parent.Key, out group, true);
                Student oldstud;
                group.TryGetValue(((MainWindow)Owner).SelectedStudent.Key, out oldstud, true);
                oldstud.LastName = StudentLName.Text;
                oldstud.FirstName = StudentFName.Text;
                oldstud.MiddleName = StudentSName.Text;
                oldstud.MiddleGrade = float.Parse(StudentAM.Text);
                oldstud.BirthDate = StudentBDate.Value.GetValueOrDefault();

                ((MainWindow)Owner).SelectedStudent.Parent.Students.Remove(((MainWindow)Owner).SelectedStudent);
                ((MainWindow)Owner).SelectedStudent.Parent.Students.Add(new StudentViewModel(oldstud, ((MainWindow)Owner).SelectedStudent.Parent, ((MainWindow)Owner).Rules));
            }
        }

        private void AddNext(object sender, RoutedEventArgs e)
        {
            AddOrModify();

            StudentBDate.Value = null;
            StudentAM.Value = null;
            StudentFName.Text = "";
            StudentLName.Text = "";
            StudentSName.Text = "";

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
            if(FieldsFilledCorrectly())
            {
                AddButton.IsEnabled = true;
                AddButton.Opacity = 1;
                AddNextButton.IsEnabled = true;
                AddNextButton.Opacity = 1;
            }
            else
            {
                AddButton.IsEnabled = false;
                AddButton.Opacity = 0.7;
                AddNextButton.IsEnabled = false;
                AddNextButton.Opacity = 0.7;
            }
        }

        private bool FieldsFilledCorrectly()
        {
            if (StudentBDate.Value == null || StudentAM.Value == null || StudentFName.Text == string.Empty || StudentLName.Text == string.Empty || StudentSName.Text == string.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
