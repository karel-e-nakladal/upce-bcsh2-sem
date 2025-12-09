using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.View;
using System.Reflection.Metadata;
using System.Windows.Documents;
using System.Windows.Input;
using WpfApp1.DataType;
using WpfApp1.Model.DataType.Contents;

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

        public EntityPage GetContnet()
        {
            return content;
        }

        private void Update()
        {
            Document.Blocks.Clear();
            Document.Blocks.AddRange(content.Build());
        }

        private void Header()
        {
            var dia = new AddHeaderView();

            var position = Mouse.GetPosition(Manager.GetInstance().MainWindow);
            var point = Manager.GetInstance().MainWindow.PointToScreen(position);

            dia.Left = point.X;
            dia.Top = point.Y;

            if (dia.ShowDialog() == true)
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

            var position = Mouse.GetPosition(Manager.GetInstance().MainWindow);
            var point = Manager.GetInstance().MainWindow.PointToScreen(position);

            dia.Left = point.X;
            dia.Top = point.Y;

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

            var position = Mouse.GetPosition(Manager.GetInstance().MainWindow);
            var point = Manager.GetInstance().MainWindow.PointToScreen(position);

            dia.Left = point.X;
            dia.Top = point.Y;

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

            var position = Mouse.GetPosition(Manager.GetInstance().MainWindow);
            var point = Manager.GetInstance().MainWindow.PointToScreen(position);

            dia.Left = point.X;
            dia.Top = point.Y;

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
