using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model.DataType.Contents;
using WpfApp1.Model.DataType.Entities;

namespace WpfApp1.ViewModel
{
    public partial class RichTextEditorWindowViewModel : ObservableObject
    {

        public RichTextEditorViewModel RichTextEditor { get; set; } = new();

        public RichTextEditorWindowViewModel(Entity? owner)
        {
            RichTextEditor.SetOwner(owner);
        }
    }
}
