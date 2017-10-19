using System;
using System.Collections.Generic;
using System.Text;

namespace TransitRoute.Models
{
    public class Operator
    {
        public string Name { get; set; }
        public IList<string> FeedIds { get; set; }
    }
}
