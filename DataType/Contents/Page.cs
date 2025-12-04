using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WpfApp1.DataType.Entity
{
    public class Page
    {
        public int Id { init; get; }

        public Entity Owner { init; get; }

        public List<Block> Content { get; set; } = new();


        //public //subcontent
    
        /*
        -list of contents (paragraphs, images)
        -update
        -delete


         */
    }
}
