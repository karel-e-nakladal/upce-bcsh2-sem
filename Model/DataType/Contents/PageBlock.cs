using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model.DataType.Entities;

namespace WpfApp1.Model.DataType.Contents
{
    public enum ContentType
    {
        Heading,
        Paragraph,
        Image,
        Link,
    }
    public class PageBlock
    {
        public ContentType Type { get; set; }
        public string? Text { get; set; }
        public string? Path { get; set; }

        public int? EntityId { get; set; }
        public EntityType? EntityType { get; set; }

    }
}
