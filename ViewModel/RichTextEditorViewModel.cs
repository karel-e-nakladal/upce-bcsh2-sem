using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.DataType.Entities;
using WpfApp1.DataType.Contents;
using WpfApp1.View;
using System.Reflection.Metadata;
using System.Windows.Documents;

namespace WpfApp1.ViewModel
{
    public partial class RichTextEditorViewModel : ObservableObject
    {
        [ObservableProperty]
        private FlowDocument document = new();

        public IRelayCommand HeaderCommand { get; }
        public IRelayCommand ParagraphCommand { get; }
        public IRelayCommand ImageCommand { get; }
        public IRelayCommand LinkCommand { get; }

        public EntityPage content { get; set; }

        public RichTextEditorViewModel()
        {
            content = new();
            HeaderCommand = new RelayCommand(Header);
            ParagraphCommand = new RelayCommand(Paragraph);
            ImageCommand = new RelayCommand(Image);
            LinkCommand = new RelayCommand(Link);
        }

        public void SetDefaultValue(EntityPage newContent)
        {
            content = newContent;
            Update();
        }

        private void Update()
        {
            Document.Blocks.Clear();
            Document.Blocks.AddRange(content.Build());
        }

        private void Header()
        {
            var dia = new AddHeaderView();

            if(dia.ShowDialog() == true)
            {
                content.Content.Add(new PageBlock()
                {
                    Type = ContentType.Heading,
                    Text = dia.Header.Text
                });
            }
            Update();
        }

        private void Paragraph()
        {
            var dia = new AddParagraphView();

            if (dia.ShowDialog() == true)
            {
                content.Content.Add(new PageBlock()
                {
                    Type = ContentType.Paragraph,
                    Text = dia.Paragraph.Text
                });
            }
            Update();
        }

        private void Image()
        {
            var dia = new AddImageView();

            if (dia.ShowDialog() == true)
            {
                content.Content.Add(new PageBlock()
                {
                    Type = ContentType.Image,
                    Text = dia.Name.Text,
                    Path = dia.Name.Text,
                });
            }
            Update();
        }

        private void Link()
        {
            var dia = new AddLinkView();

            if (dia.ShowDialog() == true)
            {
                content.Content.Add(new PageBlock()
                {
                    Type = ContentType.Link,
                    Text = dia.Name.Text,
                    Url = dia.ReadableId.Text
                });
            }
            Update();
        }
    }
}
