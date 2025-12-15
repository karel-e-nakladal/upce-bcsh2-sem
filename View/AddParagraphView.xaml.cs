using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for AddParagraphView.xaml
    /// </summary>
    public partial class AddParagraphView : Window
    {
        public AddParagraphView(PageBlock? data = null)
        {
            InitializeComponent();

            if (data is not null && data.Type == ContentType.Paragraph)
            {
                Paragraph.Text = (data.Text);
            }
        }
        private void Ok(Object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
