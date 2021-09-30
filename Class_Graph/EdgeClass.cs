using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public class EdgeClass
    {
        public NodeClass ValFrom
        {
            get;
        }

        public NodeClass ValTo
        {
            get;
        }

        public int Weight
        {
            get; set;
        }

        public EdgeClass(NodeClass valFrom, NodeClass valTo, int weight)
        {
            ValFrom = valFrom;
            ValTo = valTo;
            Weight = weight;
        }
    }
}
