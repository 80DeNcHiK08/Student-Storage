using StudentStorage.WPF.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace StudentStorage.WPF.Views
{
    /// <summary>
    /// Interaction logic for ScoolarshipRules.xaml
    /// </summary>
    public partial class ScoolarshipRules : Window, INotifyPropertyChanged
    {
        public ScoolarshipRule SelectedRule;
        public event PropertyChangedEventHandler PropertyChanged;
        public List<ScoolarshipRule> Rules { get; set; }
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public ScoolarshipRules(List<ScoolarshipRule> rules)
        {
            InitializeComponent();
            DataContext = this;

            RulesList.ItemsSource = rules;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void UpdateView()
        {
            RulesList.ItemsSource = null;
            RulesList.ItemsSource = ((MainWindow)Owner).Rules;
        }

        private void AddRule(object sender, RoutedEventArgs e)
        {
            RuleInput ri = new RuleInput();
            ri.Owner = this;
            ri.Resources["BorderColor"] = Resources["BorderColor"];
            ri.Resources["ButtonColor"] = Resources["ButtonColor"];
            ri.Resources["BGColor"] = Resources["BGColor"];
            ri.Resources["TextColor"] = Resources["TextColor"];
            ri.Resources["FontStyle"] = Resources["FontStyle"];
            ri.Resources["FontFamily"] = Resources["FontFamily"];
            ri.Resources["FontSizeSmall"] = Resources["FontSizeSmall"];
            ri.Resources["FontSizeMedium"] = Resources["FontSizeMedium"];
            ri.Resources["FontSizeLarge"] = Resources["FontSizeLarge"];
            
            ri.Show();
        }

        private void ModifyRule(object sender, RoutedEventArgs e)
        {
            RuleInput ri = new RuleInput(SelectedRule);
            ri.Owner = this;
            ri.Resources["BorderColor"] = Resources["BorderColor"];
            ri.Resources["ButtonColor"] = Resources["ButtonColor"];
            ri.Resources["BGColor"] = Resources["BGColor"];
            ri.Resources["TextColor"] = Resources["TextColor"];
            ri.Resources["FontStyle"] = Resources["FontStyle"];
            ri.Resources["FontFamily"] = Resources["FontFamily"];
            ri.Resources["FontSizeSmall"] = Resources["FontSizeSmall"];
            ri.Resources["FontSizeMedium"] = Resources["FontSizeMedium"];
            ri.Resources["FontSizeLarge"] = Resources["FontSizeLarge"];
            
            ri.Show();
        }
        private void DeleteRule(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Owner).Rules.Remove(SelectedRule);
            RulesList.ItemsSource = null;
            RulesList.ItemsSource = ((MainWindow)Owner).Rules;
            SelectedRule = null;
            ModifyButton.IsEnabled = false;
            ModifyButton.Opacity = 0.7;
            DeleteButton.IsEnabled = false;
            DeleteButton.Opacity = 0.7;
        }

        private void DeleteAll(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Owner).Rules.Clear();
            RulesList.ItemsSource = null;
        }

        private void RulesListSelected(object sender, RoutedEventArgs e)
        {
            var lvi = RulesList.SelectedItem as ScoolarshipRule;
            if (lvi != null)
            {
                SelectedRule = lvi;

                ModifyButton.IsEnabled = true;
                ModifyButton.Opacity = 1;
                DeleteButton.IsEnabled = true;
                DeleteButton.Opacity = 1;
            }
        }
    }
}
