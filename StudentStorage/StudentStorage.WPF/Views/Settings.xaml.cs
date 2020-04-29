using System.Windows;
using System.Windows.Media;

namespace StudentStorage.WPF.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            this.DataContext = MainWindow.DataContextProperty;
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ApplyClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BGColorPick(object sender, RoutedEventArgs e)
        {
            ColorPicker cpPage = new ColorPicker("BGColor", ((SolidColorBrush)BGColorButton.Background).Color)
            {
                Owner = this
            };

            cpPage.Resources["BorderColor"] = Resources["BorderColor"];
            cpPage.Resources["ButtonColor"] = Resources["ButtonColor"];
            cpPage.Resources["BGColor"] = Resources["BGColor"];
            cpPage.Resources["TextColor"] = Resources["TextColor"];
            cpPage.Resources["FontStyle"] = Resources["FontStyle"];
            cpPage.Resources["FontFamily"] = Resources["FontFamily"];
            cpPage.Resources["FontSizeSmall"] = Resources["FontSizeSmall"];
            cpPage.Resources["FontSizeMedium"] = Resources["FontSizeMedium"];
            cpPage.Resources["FontSizeLarge"] = Resources["FontSizeLarge"];

            cpPage.ShowDialog();
            Owner.Resources["BGColor"] = this.Resources["BGColor"];
            Owner.Resources["ButtonColor"] = this.Resources["BGColor"];
            Owner.Resources["TextColor"] = this.Resources["TextColor"];
            Owner.Resources["BorderColor"] = this.Resources["BorderColor"];
            ThemeSelector.SelectedIndex = 2;
        }

        private void TextColorPick(object sender, RoutedEventArgs e)
        {
            ColorPicker cpPage = new ColorPicker("TextColor", ((SolidColorBrush)TextColorButton.Background).Color)
            {
                Owner = this
            };

            cpPage.Resources["BorderColor"] = Resources["BorderColor"];
            cpPage.Resources["ButtonColor"] = Resources["ButtonColor"];
            cpPage.Resources["BGColor"] = Resources["BGColor"];
            cpPage.Resources["TextColor"] = Resources["TextColor"];
            cpPage.Resources["FontStyle"] = Resources["FontStyle"];
            cpPage.Resources["FontFamily"] = Resources["FontFamily"];
            cpPage.Resources["FontSizeSmall"] = Resources["FontSizeSmall"];
            cpPage.Resources["FontSizeMedium"] = Resources["FontSizeMedium"];
            cpPage.Resources["FontSizeLarge"] = Resources["FontSizeLarge"];

            cpPage.ShowDialog();
            Owner.Resources["TextColor"] = this.Resources["TextColor"];
            ThemeSelector.SelectedIndex = 2;
        }

        private void ThemeChanged(object sender, RoutedEventArgs e)
        {
            if (Owner == null)
                return;
            if (ThemeSelector.SelectedIndex == 2)
                return;

            if (ThemeSelector.SelectedIndex == 0)
            {
                Owner.Resources["BGColor"] = Resources["BGColor"] = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                Owner.Resources["ButtonColor"] = Resources["ButtonColor"] = new SolidColorBrush(Color.FromRgb(221, 221, 221));
                Owner.Resources["TextColor"] = Resources["TextColor"] = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                Owner.Resources["BorderColor"] = Resources["BorderColor"] = new SolidColorBrush(Color.FromRgb(51, 51, 51));
            }
            else if (ThemeSelector.SelectedIndex == 1)
            {
                Owner.Resources["BGColor"] = Resources["BGColor"] = new SolidColorBrush(Color.FromRgb(51, 51, 51));
                Owner.Resources["ButtonColor"] = Resources["ButtonColor"] = new SolidColorBrush(Color.FromRgb(51, 51, 51));
                Owner.Resources["TextColor"] = Resources["TextColor"] = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                Owner.Resources["BorderColor"] = Resources["BorderColor"] = new SolidColorBrush(Color.FromRgb(102, 102, 102));
            }
        }

        private void FontChanged(object sender, RoutedEventArgs e)
        {
            if (Owner == null)
                return;
            Owner.Resources["FontFamily"] = Resources["FontFamily"] = new FontFamily(FontFamilySelector.SelectedItem.ToString().Substring(FontFamilySelector.SelectedItem.ToString().IndexOf(':') + 2));
        }

        private void FontSizeChanged(object sender, RoutedEventArgs e)
        {
            if (Owner == null)
                return;
            switch (FontSizeSelector.SelectedIndex)
            {
                case 0:
                    Owner.Resources["FontSizeSmall"] = Resources["FontSizeSmall"] = (double)12;
                    Owner.Resources["FontSizeMedium"] = Resources["FontSizeMedium"] = (double)16;
                    Owner.Resources["FontSizeLarge"] = Resources["FontSizeLarge"] = (double)20;
                    break;
                case 1:
                    Owner.Resources["FontSizeSmall"] = Resources["FontSizeSmall"] = (double)14;
                    Owner.Resources["FontSizeMedium"] = Resources["FontSizeMedium"] = (double)18;
                    Owner.Resources["FontSizeLarge"] = Resources["FontSizeLarge"] = (double)22;
                    break;
                case 2:
                    Owner.Resources["FontSizeSmall"] = Resources["FontSizeSmall"] = (double)16;
                    Owner.Resources["FontSizeMedium"] = Resources["FontSizeMedium"] = (double)18;
                    Owner.Resources["FontSizeLarge"] = Resources["FontSizeLarge"] = (double)22;
                    break;
                default: break;
            }
        }

        private void STypeChanged(object sender, RoutedEventArgs e)
        {
            MainWindow.SerializationType = SerializationTypeSelector.SelectedValue.ToString().Substring(SerializationTypeSelector.SelectedValue.ToString().LastIndexOf(':') + 2);
        }
    }
}