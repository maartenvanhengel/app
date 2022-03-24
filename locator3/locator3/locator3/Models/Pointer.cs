using System;
using System.Collections.Generic;
using System.Text;

namespace locator3
{
    public class Pointer
    {
        
        public string Name { get; set; }
        public double Latitude { get; set; } 
        public double Longitude { get; set; }
        public string type { get; set; }
        public bool isEanabled { get; set; }
        public string text { get; set; }

        public Pointer()
        {
            isEanabled = true;
        }
    }
}
