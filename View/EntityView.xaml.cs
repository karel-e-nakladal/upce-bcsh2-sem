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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.ViewModel;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for EntityView.xaml
    /// </summary>
    public partial class EntityView : Page
    {
        public EntityView(int id)
        {
            InitializeComponent();
            DataContext = new EntityViewModel(id);
        }
        public EntityView(string readableId)
        {
            InitializeComponent();
            DataContext = new EntityViewModel(readableId);
        }
    }
}
