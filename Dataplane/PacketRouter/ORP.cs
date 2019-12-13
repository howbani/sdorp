//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using MiniSDN.Intilization;
//using MiniSDN.ControlPlane.NOS.FlowEngin;
//using MiniSDN.Dataplane.PacketRouter;

//namespace MiniSDN.Dataplane.PacketRouter
//{
//    public class ORP
//    {
//        public ORP()
//        {

//        }        
//        public List<Sensor> myNetWork;
//        public Queue<Sensor> U = new Queue<Sensor>();
//        public double FS0 = 10000;
//        public ORP(List<Sensor> myNetWork)
//        {
//            this.myNetWork = myNetWork;
//        }

//        public void compute()
//        {
//            initail();
//            FindAllNodeForwarders();
//        }

//        public void initail()
//        {
//            PublicParamerters.SinkNode.EDC = 0;
//            PublicParamerters.SinkNode.MyForwarders = null;
//            List<Sensor> temp = new List<Sensor>();
//            foreach (Sensor i in myNetWork)
//            {
//                if (i.ID != PublicParamerters.SinkNode.ID && isNeighbour(PublicParamerters.SinkNode, i) == true)
//                {
//                    List<Sensor> forwarders = new List<Sensor>();
//                    i.EDC = 1;
//                    forwarders.Add(PublicParamerters.SinkNode);
//                    i.MyForwarders = forwarders;
//                    temp.Add(i);
//                }
//            }

//            foreach (Sensor i in myNetWork)
//            {
//                if (i.ID != PublicParamerters.SinkNode.ID && temp.Contains(i) == false)
//                {
//                    i.MyForwarders = new List<Sensor>();
//                    i.EDC = FS0;
//                    U.Enqueue(i);
//                }
//            }

//        }


//        public void FindAllNodeForwarders()
//        {
//            while (U.Count > 0)
//            {
//                Sensor node = U.Dequeue();
//                double FsOld = node.EDC;
//                //CalculateMetric(node);
//                UplinkRouting.ComputeUplinkFlowEnery(node);
//                if (node.EDC < FsOld)
//                {
//                    foreach (Sensor nei in node.NeighboreNodes)
//                    {
//                        if (node.EDC < nei.EDC)
//                        {
//                            U.Enqueue(nei);
//                        }
//                    }
//                }

//            }
//        }
//        //public void CalculateMetric(Sensor node)
//        //{

//            //    node.MiniFlowTable.Clear();
//            //    UplinkRouting.ComputeUplinkFlowEnery(node);
//            //    foreach (MiniFlowTableEntry MiniEntry in node.MiniFlowTable)
//            //    {
//            //        List<Sensor> V = new List<Sensor>();
//            //        Sensor t;
//            //        if (node.NeighboreNodes == null)
//            //        {
//            //            node.NeighboreNodes = new List<Sensor>();
//            //            foreach (Sensor i in PublicParamerters.MyNetwork)
//            //                if (i.ID != node.ID && isNeighbour(i, node) == true)
//            //                    node.NeighboreNodes.Add(i);
//            //        }

//            //        node.EDC = FS0;
//            //        node.MyForwarders.Clear();
//            //        foreach (Sensor j in node.NeighboreNodes)
//            //        {
//            //            if (j.EDC < node.EDC)
//            //            {
//            //                V.Add(j);
//            //            }
//            //        }

//            //        while (V.Count > 0)
//            //        {
//            //            t = FindMinimumFsNode(V);
//            //            V.Remove(t);
//            //            if (t.EDC < node.EDC)
//            //            {
//            //                node.MyForwarders.Add(t);
//            //            }
//            //            else
//            //                break;
//            //            int number = node.MyForwarders.Count;
//            //            double sum = 0;
//            //            foreach (Sensor i in node.MyForwarders)
//            //                sum += i.EDC;
//            //            //node.EDC = (Math.Exp(1 / Math.Pow(number, MiniEntry.UpLinkPriority))) + (sum / number);
//            //            //node.EDC = 1 / number + (sum / number);
//            //            node.EDC = 1 / Math.Ceiling(1 / Math.Exp(Math.Pow(number, MiniEntry.UpLinkPriority))) + (sum / number);
//            //        }
//            //    }

//        //}
//        public Sensor FindMinimumFsNode(List<Sensor> V)
//        {
//            Sensor res = null;
//            double min = 100000;
//            foreach (Sensor i in V)
//            {
//                if (min > i.EDC)
//                {
//                    res = i;
//                    min = i.EDC;
//                }
//            }
//            return res;
//        }

//        public bool isNeighbour(Sensor n1, Sensor n2)
//        {
//            if (Operations.DistanceBetweenTwoSensors(n1, n2) < PublicParamerters.CommunicationRangeRadius)
//                return true;
//            else
//                return false;
//        }
//    }


//}
