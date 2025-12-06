using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using WpfApp1.DataType.Entities;
using WpfApp1.View;


namespace WpfApp1.DataType.Contents
{
    public class EntityPage
    {
        public int Id { init; get; }

        public List<PageBlock> Content { get; set; } = new();

        public List<Block> Build()
        {
            var result = new List<Block>();
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
