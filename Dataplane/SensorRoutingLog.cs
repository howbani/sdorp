using System;
using SDORP.Dataplane.NOS;
using SDORP.Intilization;
using SDORP.Properties;

namespace SDORP.Dataplane
{

    public class RoutingLog
    {
        public long PID { get; set; }
        public int RelaySequence { get; set; } // the sequnces of forwards the packet of the nodes.
        public int NodeID { get; set; }
        public double ForwardingRandomNumber { get; set; }
        public bool iSRedundant { get; set; }
        public double EnergyDistCnt { set; get; }
        public double TransDistanceDistCnt { set; get; }
        public double DirectionDistCnt { set; get; }
        public double PrepDistanceDistCnt { set; get; }
        public double RoutingZoneWidthCnt { get; set; }
        public PacketType PacketType { get; set; }
        public string Operation { get; set; } // sent to/ recive form .. ID
        public double UsedEnergy_Nanojoule { get; set; } // the energy used for current operation
        public double UsedEnergy_Joule //the energy used for current operation
        {
            get
            {
                double _e9 = 1000000000; // 1*e^-9
                double _ONE = 1;
                double oNE_DIVIDE_e9 = _ONE / _e9;
                double re = UsedEnergy_Nanojoule * oNE_DIVIDE_e9;
                return re;
            }
        }
        public double RemaimBatteryEnergy_Joule { get; set; } // the remain energy of battery
        public double Distance_M { get; set; }
        public bool IsSend { get; set; } // is sending operation
        public bool IsReceive { get; set; }
        public DateTime Time { get; set; }
       

    }

    //public class Logs
    //{
    //    public static void RedundantPackets(Sensor sender, Sensor reciver)
    //    {
    //        RoutingLog log = new RoutingLog();
    //        log.IsReceive = true;
    //        log.iSRedundant = true;
    //        log.NodeID = reciver.ID;
    //        log.Operation = "From:" + sender.ID;
    //        log.Time = DateTime.Now;
    //        log.Distance_M = Operations.DistanceBetweenTwoSensors(reciver, sender);
    //        log.UsedEnergy_Nanojoule = sender.EnergyModel.Receive(PublicParamerters.PreamblePacketLength);
    //        // set the remain battery Energy:
    //        if (reciver.ID != PublicParamerters.SinkNode.ID)
    //        {
    //            double remainEnergy = reciver.ResidualEnergy - log.UsedEnergy_Joule;
    //            reciver.ResidualEnergy = remainEnergy;
    //            log.RemaimBatteryEnergy_Joule = reciver.ResidualEnergy;


    //            PublicParamerters.TotalWastedEnergyJoule += log.UsedEnergy_Joule;
    //            PublicParamerters.TotalEnergyConsumptionJoule += log.UsedEnergy_Joule;
    //        }

    //        if (Settings.Default.KeepLogs)
    //        {
    //            reciver.Logs.Add(log); // keep logs for each node.
    //        }
    //        else
    //        {
    //            reciver.Dispose();
    //        }
    //    }
    //}





}
