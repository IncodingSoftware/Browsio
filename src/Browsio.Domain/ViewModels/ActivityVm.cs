using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.Domain
{
    public class ActivityVm
    {
        public string Title { get; set; }

        public string TypePicture { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

        public string ObjectActivity { get; set; }

        public string ActivityFromUserId { get; set; }
    }
}
