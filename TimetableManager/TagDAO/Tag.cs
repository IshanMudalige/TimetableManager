using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableManager.TagDAO
{
    class Tag
    {
        private string tagname;

        public Tag()
        {

        }

        public Tag( string tagname)
        {
            this.tagname = tagname;
        }

        public string Tags { get => tagname; set => tagname = value; }
    }
}
