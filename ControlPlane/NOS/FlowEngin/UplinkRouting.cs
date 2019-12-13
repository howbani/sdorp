using SDORP.Dataplane;
using SDORP.Dataplane.PacketRouter;
using SDORP.Intilization;
using SDORP.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SDORP.ControlPlane.NOS.FlowEngin
{

    public class MiniFlowTableSorterUpLinkPriority : IComparer<MiniFlowTableEntry>
    {

        public int Compare(MiniFlowTableEntry y, MiniFlowTableEntry x)
        {
            return y.MINIEDC.CompareTo(x.MINIEDC);
        }
    }

    public class UplinkRouting
    {
        public UplinkRouting()
        {

        }
        public List<Sensor> myNetWork;
        public Queue<Sensor> U = new Queue<Sensor>();
        public double FS0 = 10000;
        public UplinkRouting(List<Sensor> myNetWork)
        {
            this.myNetWork = myNetWork;
        }

        public void compute()
        {
            initail();
            FindAllNodeForwarders();
        }

        public void initail()
        {
            PublicParamerters.SinkNode.EDC = 0;
            PublicParamerters.SinkNode.MyForwarders = null;
            List<Sensor> temp = new List<Sensor>();
            //foreach (Sensor i in myNetWork)
            //{
            //    if (i.ID != PublicParamerters.SinkNode.ID && isNeighbour(PublicParamerters.SinkNode, i) == true)
            //    {
            //        List<Sensor> forwarders = new List<Sensor>();
            //        i.EDC = 1;
            //        forwarders.Add(PublicParamerters.SinkNode);
            //        i.MyForwarders = forwarders;
            //        temp.Add(i);
            //    }
            //}

            foreach (Sensor i in myNetWork)
            {
                if (i.ID != PublicParamerters.SinkNode.ID/* && temp.Contains(i) == false*/)
                {
                    i.MyForwarders = new List<Sensor>();
                    i.EDC = FS0;
                    U.Enqueue(i);
                }
            }

        }

        public void FindAllNodeForwarders()
        {
            while (U.Count > 0)
            {
                Sensor node = U.Dequeue();
                double FsOld = node.EDC;
                ComputeUplinkFlowEnery(node);
                calculateMetric(node);
                //UplinkRouting up = new UplinkRouting();
                if (node.EDC < FsOld)
                {
                    foreach (Sensor nei in node.NeighboreNodes)
                    {
                        if (node.EDC < nei.EDC)
                        {
                            U.Enqueue(nei);
                        }
                    }
                }

            }
        }

        public void calculateMetric (Sensor sender)
        {
            double n = Convert.ToDouble(sender.NeighborsTable.Count) + 1;
            foreach (MiniFlowTableEntry MiniEntry in sender.MiniFlowTable)
            {
                List<Sensor> V = new List<Sensor>();
                if (sender.NeighboreNodes == null)
                {
                    sender.NeighboreNodes = new List<Sensor>();
                    foreach (Sensor i in myNetWork)
                        if (i.ID != sender.ID && isNeighbour(i, sender) == true)
                            sender.NeighboreNodes.Add(i);
                }

                sender.EDC = FS0;
                sender.MyForwarders.Clear();
                foreach (Sensor j in sender.NeighboreNodes)
                {
                    if (j.EDC < sender.EDC)
                    {
                        V.Add(j);
                    }
                }

                while (V.Count > 0)
                {
                    MiniEntry.NodeEntry = FindMinimumFsNode(V);
                    V.Remove(MiniEntry.NodeEntry);
                    if (MiniEntry.NodeEntry.EDC < sender.EDC)
                    {
                        sender.MyForwarders.Add(MiniEntry.NodeEntry);
                    }
                    else
                        break;
                    int number = sender.MyForwarders.Count;
                    double sum = 0;
                    foreach (Sensor i in sender.MyForwarders)
                        sum += i.EDC;
                    sender.EDC = 1 / (number * MiniEntry.UpLinkPriority) + (sum / number);


                }

            }

            sender.MiniFlowTable.Sort(new MiniFlowTableSorterUpLinkPriority());
            //action
            double MiniSensorEDC = 0;
            double averageEDC = 0;
            foreach (MiniFlowTableEntry Mini in sender.MiniFlowTable)
            {
                MiniSensorEDC += Mini.NeighborEntry.NeiNode.EDC;
                averageEDC = MiniSensorEDC / Convert.ToDouble(sender.MiniFlowTable.Count);
            }

            int Ftheashoeld1 = Convert.ToInt16(Math.Ceiling(Math.Sqrt(1 + n/Math.PI))); // theshold.
            int forwardersCount1 = 0;
            sender.MyForwarders.Clear();
            foreach (MiniFlowTableEntry MiniNode in sender.MiniFlowTable)
            {
                if (MiniNode.NeighborEntry.NeiNode.EDC <= averageEDC && forwardersCount1 < Ftheashoeld1)
                {
                    MiniNode.UpLinkAction = FlowAction.Forward;
                    sender.MyForwarders.Add(MiniNode.NeighborEntry.NeiNode);
                    forwardersCount1++;
                    sender.forwardersCount = forwardersCount1;
                }
                else MiniNode.UpLinkAction = FlowAction.Drop;
            }
        }
        public void UpdateUplinkFlowEnery(Sensor sender)
        {
            sender.GenerateDataPacket(); // send packet to controller.

            sender.MiniFlowTable.Clear();
            ComputeUplinkFlowEnery(sender);
            calculateMetric(sender);

        }

        public void ComputeUplinkFlowEnery(Sensor sender)
        {
            sender.MiniFlowTable.Clear();
            double EnergyLsum = 0;
            double ExpectedHopSum = 0;
            double TransmissionEpSum = 0;

            double n = Convert.ToDouble(sender.NeighborsTable.Count) + 1;

            double LControl = Settings.Default.ExpoLCnt;
            double EControl = Settings.Default.ExpoECnt;
            double HControl = Settings.Default.ExpoHCnt;



            double HSum = 0; // sum of h value.
            double RSum = 0;
            foreach (NeighborsTableEntry can in sender.NeighborsTable)
            {
                HSum += can.H;
                //RSum += can.R;
            }

            // normalized values.
            foreach (NeighborsTableEntry neiEntry in sender.NeighborsTable)
            {
                if (neiEntry.NeiNode.ResidualEnergyPercentage >= 0) // the node is a live.
                {

                        double Dij = Operations.DistanceBetweenTwoSensors(sender, neiEntry.NeiNode);// i and j
                        double Djb = Operations.DistanceBetweenTwoSensors(neiEntry.NeiNode, PublicParamerters.SinkNode); // j and b
                        double Dib = Operations.DistanceBetweenTwoSensors(sender, PublicParamerters.SinkNode); // i and b

                        MiniFlowTableEntry MiniEntry = new MiniFlowTableEntry();
                        MiniEntry.NeighborEntry = neiEntry;
                        MiniEntry.NeighborEntry.SenderNode = sender;
                        MiniEntry.NeighborEntry.SourceNode = sender;
                        MiniEntry.NeighborEntry.NeiNode = neiEntry.NeiNode;
                        MiniEntry.NeighborEntry.DistanceFromSenderToForwarder = Dij;
                        MiniEntry.NeighborEntry.DistanceFromSenderToSink = Dib;
                        MiniEntry.NeighborEntry.DistanceFromFrowarderToSink = Djb;
                        MiniEntry.NeighborEntry.r = sender.ComunicationRangeRadius;



                        EnergyLsum += Math.Exp(1 - (1 / Math.Pow(MiniEntry.NeighborEntry.NormalizedEnergy, LControl)));
                        TransmissionEpSum += 1 - Math.Exp(1 - (1 / Math.Pow(MiniEntry.NeighborEntry.NormalizedTransDistance, EControl)));
                        ExpectedHopSum += 1 - Math.Exp(1 - (1 / Math.Pow(MiniEntry.NeighborEntry.NormalizedExNHP, HControl)));

                    sender.MiniFlowTable.Add(MiniEntry);
                }
            }


            double sumAll = 0;
            foreach (MiniFlowTableEntry MiniEntry in sender.MiniFlowTable)
            {

                MiniEntry.NeighborEntry.EnergyLP = Math.Exp(1 - (1 / Math.Pow(MiniEntry.NeighborEntry.NormalizedEnergy, LControl))) / EnergyLsum;
                MiniEntry.NeighborEntry.ExpectedHopsHP = 1 - Math.Exp(1 - (1 / Math.Pow(MiniEntry.NeighborEntry.NormalizedExNHP, HControl))) / ExpectedHopSum;
                MiniEntry.NeighborEntry.TransmissionEP = 1 - Math.Exp(1 - (1 / Math.Pow(MiniEntry.NeighborEntry.NormalizedTransDistance, EControl))) / TransmissionEpSum;
                MiniEntry.UpLinkPriority = (MiniEntry.NeighborEntry.TransmissionEP + MiniEntry.NeighborEntry.EnergyLP + MiniEntry.NeighborEntry.ExpectedHopsHP) / 3;

                sumAll += MiniEntry.UpLinkPriority;
            }         

        }

        public Sensor FindMinimumFsNode(List<Sensor> V)
        {
            Sensor res = null;
            double min = 100000;
            foreach (Sensor i in V)
            {
                if (min > i.EDC)
                {
                    res = i;
                    min = i.EDC;
                }
            }
            return res;
        }

        public bool isNeighbour(Sensor n1, Sensor n2)
        {
            if (Operations.DistanceBetweenTwoSensors(n1, n2) < PublicParamerters.CommunicationRangeRadius)
                return true;
            else
                return false;
        }

    }
}
