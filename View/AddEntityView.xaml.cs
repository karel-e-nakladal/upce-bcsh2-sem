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
using WpfApp1.Model.DataType.Entities.RealEntities;
using WpfApp1.ViewModel;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for AddEntityView.xaml
    /// </summary>
    public partial class AddEntityView : Window
    {
        public AddEntityView(RealEntity? data = null)
        {
            InitializeComponent();
            DataContext = new AddEntityViewModel(data);
        }

        private void Ok(Object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void OnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

    }
}
