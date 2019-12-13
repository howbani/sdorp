using SDORP.Dataplane.NOS;
using System;
using System.Collections.Generic;

namespace SDORP.Dataplane.PacketRouter
{
    public enum FlowAction { Forward, Drop }

    public class MiniFlowTableEntry
    {
        public int ID { get { return NeighborEntry.NeiNode.ID; } }
        public double MINIEDC { get { return NeighborEntry.NeiNode.EDC; } }
        public double UpLinkPriority { get; set; }
        public FlowAction UpLinkAction /*{ get { return NeighborEntry.NeiNode.UpLinkActionn; } }*/{ get; set; }
        public double UpLinkStatistics { get { return NeighborEntry.NeiNode.ControllerUpLinkStatistics; } }  


        public SensorState SensorState { get { return NeighborEntry.NeiNode.CurrentSensorState; } }
        //public double Statistics { get { return UpLinkStatistics; } }
        public  NeighborsTableEntry NeighborEntry { get; set; }

        public Sensor NodeEntry { get; set; }

    }
}
