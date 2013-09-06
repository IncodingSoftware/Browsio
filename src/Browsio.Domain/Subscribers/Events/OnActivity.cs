using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.Domain
{
    using Incoding.EventBroker;

    public class OnActivity : IEvent
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public ActivityOfType Type { get; set; }

        public virtual string ObjectActivity { get; set; }

        public User ActivityToUser { get; set; }

        public User ActivityFromUser { get; set; }
    }
}
