using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using WpfApp1.DataType;
using WpfApp1.Model.DataType.Contents;
using WpfApp1.View;

namespace WpfApp1.ViewModel
{
    public partial class RichTextEditorViewModel : ObservableObject
    {
        [ObservableProperty]
        private FlowDocument document = new();
        
        [ObservableProperty]
        private PageBlock selectedBlock;
        
        public IRelayCommand HeaderCommand { get; }
        public IRelayCommand ParagraphCommand { get; }
        public IRelayCommand ImageCommand { get; }
        public IRelayCommand LinkCommand { get; }


        private EntityPage content;

        public RichTextEditorViewModel()
        {
            content = new();
            HeaderCommand = new RelayCommand(HeaderC);
            ParagraphCommand = new RelayCommand(ParagraphC);
            ImageCommand = new RelayCommand(ImageC);
            LinkCommand = new RelayCommand(LinkC);
        }

        public void SetDefaultValue(EntityPage newContent)
        {
            content = newContent;
            Update();
        }

        public EntityPage GetContnet()
        {
            return content;
        }

        private void Update()
        {
            Document.Blocks.Clear();
            Document.Blocks.AddRange(
                content.BuildEditable(EditBlock)
            );
        }

        private void EditBlock(PageBlock block, int index)
        {
            switch(block.Type)
            {
                case ContentType.Heading:
                    Header(block, index);
                    break;
                case ContentType.Paragraph:
                    Paragraph(block, index);
                    break;
                case ContentType.Image:
                    Image(block, index);
                    break;
                case ContentType.Link:
                    Link(block, index);
                    break;
            }
        }


        private void HeaderC()
        {
            Header();
        }
        private void Header(PageBlock? data = null, int? index = null)
        {
            var dia = new AddHeaderView(data);

            var position = Mouse.GetPosition(Manager.GetInstance().MainWindow);
            var point = Manager.GetInstance().MainWindow.PointToScreen(position);

            dia.Left = point.X;
            dia.Top = point.Y;

            if (dia.ShowDialog() == true)
            {
                if (index is not null && data is not null)
                {
                    content.Content[(int)index] = new PageBlock()
                    {
                        Type = ContentType.Heading,
                        Text = dia.Header.Text
                    };
                }
                else
                {
                    content.Content.Add(new PageBlock()
                    {
                        Type = ContentType.Heading,
                        Text = dia.Header.Text
                    });
                }
            }
            Update();
        }

        private void ParagraphC()
        {
            Paragraph();
        }
        private void Paragraph(PageBlock? data = null, int? index = null)
        {
            var dia = new AddParagraphView(data);

            var position = Mouse.GetPosition(Manager.GetInstance().MainWindow);
            var point = Manager.GetInstance().MainWindow.PointToScreen(position);

            dia.Left = point.X;
            dia.Top = point.Y;

            if (dia.ShowDialog() == true)
            {
                if (index is not null && data is not null)
                {
                    content.Content[(int)index] = new PageBlock()
                    {
                        Type = ContentType.Paragraph,
                        Text = dia.Paragraph.Text
                    };
                }
                else
                {
                    content.Content.Add(new PageBlock()
                    {
                        Type = ContentType.Paragraph,
                        Text = dia.Paragraph.Text
                    });
                }
            }
            Update();
        }

        private void ImageC()
        {
            Image();
        }
        private void Image(PageBlock? data = null, int? index = null)
        {
            var dia = new AddImageView(data);

            var position = Mouse.GetPosition(Manager.GetInstance().MainWindow);
            var point = Manager.GetInstance().MainWindow.PointToScreen(position);

            dia.Left = point.X;
            dia.Top = point.Y;

            if (dia.ShowDialog() == true)
            {
                if (index is not null && data is not null)
                {
                    content.Content[(int)index] = new PageBlock()
                    {
                        Type = ContentType.Image,
                        Text = dia.Name.Text,
                        Path = dia.Name.Text,
                    };
                }
                else
                {
                    content.Content.Add(new PageBlock()
                    {
                        Type = ContentType.Image,
                        Text = dia.Name.Text,
                        Path = dia.Name.Text,
                    });
                }
            }
            Update();
        }

        private void LinkC()
        {
            Link();
        }
        private void Link(PageBlock? data = null, int? index = null)
        {
            var dia = new AddLinkView(data);

            var position = Mouse.GetPosition(Manager.GetInstance().MainWindow);
            var point = Manager.GetInstance().MainWindow.PointToScreen(position);

            dia.Left = point.X;
            dia.Top = point.Y;

            if (dia.ShowDialog() == true)
            {
                if (index is not null && data is not null)
                {
                    content.Content[(int)index] = new PageBlock()
                    {
                        Type = ContentType.Link,
                        Text = dia.Name.Text,
                        Url = dia.ReadableId.Text
                    };
                }
                else
                {
                    content.Content.Add(new PageBlock()
                    {
                        Type = ContentType.Link,
                        Text = dia.Name.Text,
                        Url = dia.ReadableId.Text
                    });
                }
            }
            Update();
        }
    }
}
