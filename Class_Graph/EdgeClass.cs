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

        public int flow;          // поток через дугу
        public int flow_capacity; // пропускная способность дуги

        public EdgeClass(NodeClass valFrom, NodeClass valTo, int weight)
        {
            ValFrom = valFrom;
            ValTo = valTo;
            Weight = weight;
        }

    }
}
