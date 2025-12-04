using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.ViewModel
{
    public class AddEntityViewModel : ObservableObject 
    {
        public RichTextEditorViewModel RichTextEditor { get; set; } = new();

        public AddEntityViewModel()
        {

        }



    }
}
