using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.View;

namespace WpfApp1.ViewModel
{
    public partial class RichTextEditorViewModel : ObservableObject
    {

        public IRelayCommand HeaderCommand { get; }
        public IRelayCommand ParagraphCommand { get; }
        public IRelayCommand ImageCommand { get; }
        public IRelayCommand LinkCommand { get; }

        public Page content = new();

        public RichTextEditorViewModel()
        {
            HeaderCommand = new RelayCommand(Header);
            ParagraphCommand = new RelayCommand(Paragraph);
            ImageCommand = new RelayCommand(Image);
            LinkCommand = new RelayCommand(Link);
        }
        private void Header()
        {
            var dia = new AddHeaderView();

            if(dia.ShowDialog() == true)
            {

            }
        }

        private void Paragraph()
        {
            var dia = new AddParagraphView();

            if (dia.ShowDialog() == true)
            {

            }
        }

        private void Image()
        {
            var dia = new AddImageView();

            if (dia.ShowDialog() == true)
            {

            }
        }

        private void Link()
        {
            var dia = new AddLinkView();

            if (dia.ShowDialog() == true)
            {

            }
        }
    }
}
