using System;
using System.Collections.Generic;
using System.Text;

namespace CustomSerializer
{
    public class Product
    {
        public string Name { get; set; }
        public List<Category> Categories { get; set; }
        public string Type { get; set; }
    }
}
