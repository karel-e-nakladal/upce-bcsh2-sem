using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Model.DataType.Entities;

namespace WpfApp1.ViewModel
{
    public partial class AddEntityViewModel : ObservableObject 
    {
        
        public RichTextEditorViewModel RichTextEditor { get; set; } = new();

        [ObservableProperty]
        public ObservableCollection<string> typeOptions = new(
            Enum.GetNames(typeof(EntityType))
            );

        [ObservableProperty]
        public ObservablePropertyAttribute selectedType;

        public AddEntityViewModel()
        {

        }



    }
}
