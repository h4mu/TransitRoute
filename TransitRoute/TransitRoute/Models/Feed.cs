using System;
using System.Collections.Generic;
using System.Text;

namespace TransitRoute.Models
{
    public class Feed
    {
        public string Name { get; set; }
        public Uri Uri { get; set; }
        public Uri LicenseUri { get; set; }
        public bool LicenseAttributionRequired { get; set; }
        public string LicenseText { get; set; }
    }
}
