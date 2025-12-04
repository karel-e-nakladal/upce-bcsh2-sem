using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.DataType.Contents
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
        public string? Url { get; set; }
        public string? Path { get; set; }

    }
}
