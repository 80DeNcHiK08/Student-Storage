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
    /// Interaction logic for Info.xaml
    /// </summary>
    public partial class Info : Window
    {
        public Info(StudentViewModel student)
        {
            InitializeComponent();

            NameLabel.Content = student.ConcatedName;
            BDLabel.Content = new DateTime(int.Parse(student.BirthDate.Split('.')[2].Substring(0, student.BirthDate.Split('.')[2].IndexOf(' '))), int.Parse(student.BirthDate.Split('.')[1]), int.Parse(student.BirthDate.Split('.')[0])).Date.ToString() + " (" + student.Age + " years old)";
            FNLabel.Content = student.Parent.Parent.Name;
            GNLabel.Content = student.Parent.Name;
            AMLabel.Content = student.AM;
            SSLabel.Content += "\"" + student.SponsorType + "\"";
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
