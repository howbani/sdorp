using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDORP.Dataplane.PacketRouter
{
    class ForwardersSelections
    {
        /// <summary>
        /// get my forwarders.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<Sensor> ComputeDistributions(Sensor node)
        {
            return node.MyForwarders;
        }
    }
}
