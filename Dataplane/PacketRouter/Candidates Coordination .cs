//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MiniSDN.Dataplane.PacketRouter
//{
//    class Candidates_Coordination
//    {
//        /// <summary>
//        /// select the first active one.
//        /// </summary>
//        /// <param name="probabilities"></param>
//        /// <returns></returns>
//        public Sensor SelectNextHop(Sensor SenderNode)
//        {
//            Sensor max = null;
//            bool isMaxSelected = false;


//            foreach (Sensor s in SenderNode.MyForwarders)
//            {
//                if (s.ID == PublicParamerters.SinkNode.ID) { return s; }
//            }

//            foreach (Sensor pf in SenderNode.MyForwarders)
//            {

//                if (pf.CurrentSensorState == SensorState.Active)
//                {
//                    if (!isMaxSelected)
//                    {
//                        max = pf;
//                        isMaxSelected = true;
//                    }
//                    else
//                    {
//                        // these nodes loss the energy of reciving.
//                        // Logs.Logs.RedundantPackets(SenderNode, pf);
//                        Logs.RedundantPackets(SenderNode, pf);
//                        PublicParamerters.TotalReduntantTransmission += 1;
//                    }
//                }
//            }


//            return max;
//        }
//    }
//}
