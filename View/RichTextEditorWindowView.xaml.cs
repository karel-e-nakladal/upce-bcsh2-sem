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
using WpfApp1.Model.DataType.Contents;
using WpfApp1.Model.DataType.Entities;
using WpfApp1.ViewModel;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for RichTextEditorWindowView.xaml
    /// </summary>
    public partial class RichTextEditorWindowView : Window
    {
        public RichTextEditorWindowView(Entity owner)
        {
            InitializeComponent();
            DataContext = new RichTextEditorWindowViewModel(owner);
        }

        private void Ok(Object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
