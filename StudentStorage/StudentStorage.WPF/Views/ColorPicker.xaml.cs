using System.Windows;
using System.Windows.Media;

namespace StudentStorage.WPF.Views
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : Window
    {
        private string resourceName;
        public ColorPicker()
        {
            InitializeComponent();
        }
        public ColorPicker(string resourceName, Color oldColor)
        {
            this.resourceName = resourceName;
            InitializeComponent();
            ChooseColor.SelectedColor = oldColor;
        }

        public void OKClick(object sender, RoutedEventArgs e)
        {
            Color newColor = ChooseColor.SelectedColor.Value;
            Owner.Resources[resourceName] = new SolidColorBrush(newColor);
            if (resourceName == "BGColor")
            {
                Color contrastColor = Color.FromArgb(newColor.A, (byte)(0xFF - newColor.R), (byte)(0xFF - newColor.G), (byte)(0xFF - newColor.B));
                Owner.Resources["ButtonColor"] = new SolidColorBrush(newColor);
                Owner.Resources["TextColor"] = new SolidColorBrush(contrastColor);
                Owner.Resources["BorderColor"] = new SolidColorBrush(contrastColor);
            }

            Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
