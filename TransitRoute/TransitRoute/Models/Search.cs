using System;
using System.Collections.Generic;
using System.Text;

namespace TransitRoute.Models
{
    public class Search : BaseDataObject
    {
        string from = string.Empty;
        public string From
        {
            get { return from; }
            set { SetProperty(ref from, value); }
        }

        string to = string.Empty;
        public string To
        {
            get { return to; }
            set { SetProperty(ref to, value); }
        }
    }
}
