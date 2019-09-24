using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeezupAPI
{
    public class ModelView
    {
        public string csvUri { get; set; }
        public int? take { get; set; }
        public int? skip { get; set; }
    }
}