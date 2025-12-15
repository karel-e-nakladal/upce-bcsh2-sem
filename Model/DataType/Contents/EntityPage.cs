using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApp1.DataType;
using WpfApp1.View;


namespace WpfApp1.Model.DataType.Contents
{
    public class EntityPage
    {
        public int Id { init; get; }

        public List<PageBlock> Content { get; set; } = new();


        private void CheckEmpty()
        {
            for(int i = 0; i < Content.Count; i++)
            {
                if(Content[i] == null || Content[i].Text == null || Content[i].Text == "")
                {
                    Content.RemoveAt(i);
                }
            }

        }
        public List<Block> Build()
        {
            var result = new List<Block>();
            CheckEmpty();
            foreach (var item in Content)
            {
                if (item is null)
                    continue;
                switch (item.Type)
                {
                    case ContentType.Heading:
                        result.Add(new Paragraph(new Run(item.Text)
                        {
                            FontSize = 24,
                            FontWeight = FontWeights.Bold
                        }));
                        break;
                    case ContentType.Paragraph:
                        result.Add(new Paragraph(new Run(item.Text)));
                        break;
                    case ContentType.Link:
                        var link = new Hyperlink(new Run(item.Text));

                        link.Click += (sender, e) =>
                        {
                            var targetEntity = item.Path;

                            var entityPage = new EntityView(targetEntity);

                            var nav = Manager.GetInstance().MainWindow.MainFrame.Navigate(entityPage);
                        };

                        result.Add(new Paragraph(link));

                        break;
                    case ContentType.Image:
                        var image = new Image()
                        {
                            Source = new BitmapImage(new Uri(item.Url)),
                            Width = 200
                        };
                        result.Add(new Paragraph(new InlineUIContainer(image)));
                        break;

                }
            }
            return result;
        }

        public List<Block> BuildEditable(Action<PageBlock, int> onEdit)
        {
            var result = new List<Block>();
            CheckEmpty();
            for (int i = 0; i < Content.Count; i++)
            {
                var item = Content[i];
                if (item is null)
                    continue;

                UIElement element =  item.Type switch
                {
                    ContentType.Heading => new TextBlock
                    {
                        Text = item.Text,
                        FontSize = 24,
                        FontWeight = FontWeights.Bold
                    },
                    ContentType.Paragraph => new TextBlock
                    {
                        Text = item.Text,
                        TextWrapping = TextWrapping.Wrap
                    },
                    ContentType.Link => new TextBlock
                    {
                        Text = item.Text,
                        TextDecorations = TextDecorations.Underline,
                        Foreground = Brushes.Blue,
                        Cursor = Cursors.Hand
                    },
                    ContentType.Image => new Image
                    {
                        Source = new BitmapImage(new Uri(item.Url)),
                        Width = 200
                    },
                    _ => null
                };

                if (element == null)
                    continue;

                var button = CreateInvisibleButton(element, item, i, onEdit);
                result.Add(new Paragraph(new InlineUIContainer(button)));
            }

            return result;
        }


        private Button CreateInvisibleButton(
            UIElement content,
            PageBlock item,
            int index,
            Action<PageBlock, int> onEdit)
        {
            var button = new Button
            {
                Content = content,
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(0),
                Padding = new Thickness(0),
                FocusVisualStyle = null,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            button.Click += (s, e) => onEdit?.Invoke(item, index);

            return button;
        }

        public void Update()
        {
            Manager.GetInstance().Database.Page.Update(this);
        }

        public string JsonSerialize()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            return JsonSerializer.Serialize(Content, options);
        }

        public static List<PageBlock> JsonDeSerialize(string? json)
        {
            if (json is null)
                return new List<PageBlock>();
            return JsonSerializer.Deserialize<List<PageBlock>>(json) ?? new List<PageBlock>();
        }
    }
}
