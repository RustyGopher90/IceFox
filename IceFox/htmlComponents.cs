using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceFox
{
    internal class htmlComponents
    {
        public string Head { get; set; }
        public string Body { get; set; }

        public string WhatsLeft { get; set; }

        static internal string[] GetAllHeadLinks(string htmlHead)
        {
            //var links = new List<string>();
            string[] headLinks = htmlHead.Split(">");
            return headLinks;
        }
    }
}
