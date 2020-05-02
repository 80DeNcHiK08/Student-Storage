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
    /// Interaction logic for RuleInput.xaml
    /// </summary> 
    public partial class RuleInput : Window
    {
        private string Mode = "Add";
        public RuleInput()
        {
            InitializeComponent();
            this.Title = "Add new rule";
        }

        public RuleInput(ScoolarshipRule rule)
        {
            InitializeComponent();
            this.Mode = "Modify";
            this.Title = "Modify rule \"" + rule.Name + "\" ";
            AddNextButton.Visibility = Visibility.Hidden;
            NameField.Text = rule.Name;
            Mark.Value = double.Parse(rule.MinMark.ToString());
            Percent.Value = double.Parse(rule.PercToReach.ToString());
            NameField.Focus();
        }

        private void AddOrModify()
        {
            if (this.Mode == "Modify")
            {
                ((MainWindow)((ScoolarshipRules)Owner).Owner).Rules.Remove(((ScoolarshipRules)Owner).SelectedRule);
            }

             ScoolarshipRule rule = new ScoolarshipRule(NameField.Text, float.Parse(Mark.Text), float.Parse(Percent.Text));
             ((MainWindow)((ScoolarshipRules)Owner).Owner).Rules.Add(rule);

            ((ScoolarshipRules)Owner).UpdateView();
            ((ScoolarshipRules)Owner).DeleteAllButton.IsEnabled = true;
            ((ScoolarshipRules)Owner).DeleteAllButton.Opacity = 0.7;
            ((MainWindow)((ScoolarshipRules)Owner).Owner).UpdateView();
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
            if (NameField.Text != string.Empty && Mark.Value != null && Percent.Value != null && NumberFieldsCorrect())
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

        private bool NumberFieldsCorrect()
        {
            double a, b;
            if (!Double.TryParse(Percent.Text, out a) || !Double.TryParse(Mark.Text, out b))
                return false;
            return (NameField.Text == string.Empty || a < 0 || a > 100 || b < 0 || b > 100) ? false : true;
        }

        private void SelectText(object sender, RoutedEventArgs e)
        {
            var item = e.OriginalSource as UIElement;
            if (item != null)
            {
                var textBox = (TextBox)item;
                textBox.SelectAll();
            }
        }
    }
}
