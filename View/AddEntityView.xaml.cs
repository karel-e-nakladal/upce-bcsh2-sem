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
using WpfApp1.ViewModel;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for AddEntityView.xaml
    /// </summary>
    public partial class AddEntityView : Window
    {
        public AddEntityView()
        {
            InitializeComponent();
            DataContext = new AddEntityViewModel();
        }

        private void Ok(Object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel(Object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
