using System;
using System.Collections.Generic;
using SDORP.Intilization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SDORP.Dataplane.PacketRouter
{
    /// <summary>
    /// TABLE 2: NEIGHBORING NODES INFORMATION TABLE (NEIGHBORS-TABLE)
    /// </summary>
    public class NeighborsTableEntry
    {
        public int ID { get { return NeiNode.ID; } } // id of candidate.
        public int NCount { get { return NeiNode.NeighborsTable.Count; } }
        // Elementry values:

        public double EnergyLP { get; set; } // battry level.
        public double TransmissionEP { get; set; } // Transmission Distance
        public double ExpectedHopsHP { get; set; } // Expected Hops.



        // Hops:
        public int H { get { return NeiNode.HopsToSink; } }

        //Normalized Energy
        public double  NormalizedEnergy { get { return (NeiNode.ResidualEnergyPercentage / 100); }}


        //Normalized Transmission Distance
        public double NormalizedTransDistance
        {
            get
            {
                double nj = (DistanceFromSenderToForwarder / (2 * r));
                return nj;
            }
        }


        public double ExpectedNumberofHops
        {
            get
            {
                double h = (2 * Operations.DistanceBetweenTwoSensors(NeiNode, PublicParamerters.SinkNode)) / Math.Pow(r, 2);
                double max_h1 = 2 * NCount;
                double max_h2 = 2 * NCount + 1;
                double max_h3 = max_h1 / max_h2;
                double max = max_h3 * r;
                double ehp = h * max;
                return ehp;
            }
        }


        public double NormalizedExNHP
        {
            get
            {
                double NormalizedHops = ExpectedNumberofHops / (2 * r);
                return NormalizedHops;
            }
        }

        //double S2D;
        //public double Sender2DesitnationDist
        //{
        //    get
        //    {
        //        S2D = Operations.DistanceBetweenTwoSensors(NeiNode, PublicParamerters.SinkNode); // i and b;
        //        return S2D;
        //    }
        //}

        //public double MaxDistForwarderinside_CR
        //{
        //    get
        //    {
        //        double max_h1 = 2 * NCount;
        //        double max_h2 = 2 * NCount + 1;
        //        double max_h3 = max_h1 / max_h2;
        //        double max = max_h3 * r;
        //        return max;
        //    }
        //}

        //public double Sender2Neighbor
        //{
        //    get
        //    {
        //        S2D = Operations.DistanceBetweenTwoSensors(SourceNode, NeiNode); // i and b;
        //        return S2D;
        //    }
        //}


        public System.Windows.Point CenterLocation { get { return NeiNode.CenterLocation; } }
        //: The neighbor Node
        public Sensor NeiNode { get; set; }


        /// <summary>
        /// Distance from sender to potential forwarder
        /// </summary>
        public double DistanceFromSenderToForwarder { get; set; } // 
        /// <summary>
        /// from sender to sink
        /// </summary>
        public double DistanceFromSenderToSink { get; set; } // 
        /// <summary>
        /// from forwarder to the sink
        /// </summary>
        public double DistanceFromFrowarderToSink { get; set; } // 




        public double[] RoutingProbRange = new double[2];

        public double r { get; set; }
        public Sensor SourceNode { get; set; } // s // the source node of this packet 
        public Sensor SenderNode { get; set; } //i // the current senser. ( current relay).

        public List<Sensor> ForwarderList = new List<Sensor>();

    }

}
