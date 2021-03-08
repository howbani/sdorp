using SDORP.Dataplane;
using System;
using SDORP.Dataplane.NOS;
using SDORP.ui;

namespace SDORP.AnalysisValidation
{

    class ExpectedEnergyConsumption
    {
        public class TheoraticalExpectedEnergyCost
        {
            double E_elec = PublicParamerters.E_elec;
            double Efs = PublicParamerters.Efs;
            double Emp = PublicParamerters.Emp;
            double ERx = 0;
            double energysum = 0;
            double distance;
            double trans;
            double recv;
            double packetlength;
            double averagesum = 0;
            double count = 0;
            double incount = 0;
            double totalenergy = 0;
            double add = 0;

            private double ConvertToJoule(double UsedEnergy_Nanojoule) //the energy used for current operation
            {
                double _e9 = 1000000000; // 1*e^-9
                double _ONE = 1;
                double oNE_DIVIDE_e9 = _ONE / _e9;
                double re = UsedEnergy_Nanojoule * oNE_DIVIDE_e9;
                return re;
            }
            public double d0  //Distance threshold ( unit m)
            {
                get { return Math.Sqrt(Efs / Emp); }
            }

            public double DistanceFromSenderToForwarder { get; set; }

            public double Transmit(double k, double d)
            {
                Packet packet = new Packet();
                double E_tx = 0;
                if (packet.PacketType == PacketType.Beacon)
                {
                    if (d <= d0)
                    {
                       E_tx = (k * E_elec) + (k * Efs * d * d);
                    }
                    else if (d > d0)
                    {
                        E_tx = (k * E_elec) + (k * Emp * d * d * d * d);
                    }
                }
                else if (packet.PacketType == PacketType.ACK )
                {
                    if (d <= d0)
                    {
                        E_tx = (k * E_elec) + (k * Efs * d * d);
                    }
                    else if (d > d0)
                    {
                        E_tx = (k * E_elec) + (k * Emp * d * d * d * d);
                    }
                }
                else if (packet.PacketType == PacketType.Data)
                {
                    if (d <= d0)
                    {
                        E_tx = (k * E_elec) + (k * Efs * d * d);
                    }
                    else if (d > d0)
                    {
                        E_tx = (k * E_elec) + (k * Emp * d * d * d * d);
                    }
                }
                return E_tx;
            }
            public double Receive(double k)
            {
                Packet packet1 = new Packet();
                if (packet1.PacketType == PacketType.Beacon)
                {
                    ERx = k * E_elec;
                }
                if (packet1.PacketType == PacketType.ACK)
                {
                    ERx = k * E_elec;
                }
                if (packet1.PacketType == PacketType.Data)
                {
                    ERx = k * E_elec;
                }
                return ERx;
            }

            public void TheoraticalEnergyConsumption(double k) 
            {
                // Packet packet = new Packet();
                Random randomName = new Random(); //only required once
                MainWindow _mianWind = new MainWindow();
                for (int i =1; i<=_mianWind.stopSimlationWhen; i++)
                //foreach (Packet packet in PublicParamerters.FinishedRoutedPackets)
                 {
                
               // int forwarders = randomName.Next(2, 4); // No Of Nodes: 100
                //  int forwarders = randomName.Next(2, 5); // No Of Nodes: 120, 140
                int forwarders = randomName.Next(3, 5); // No Of Nodes: 160, 180
                //  int forwarders = randomName.Next(4, 6); // No Of Nodes: 200
                int active = randomName.Next(0, forwarders);
                    // randomly selected the active forwarders from a given range for each node i 
                    for (int j = 1; j<= forwarders; j++) // no of active candidates among forwarders
                    {
                        if (active > 0)
                        {
                            distance = randomName.Next(1, 81); //Communication Range: 80m //the generated number is *equal or above* the min(1) and *bellow* the max(101)
                            packetlength = k;
                            trans = Transmit(packetlength, distance);
                            recv = Receive(packetlength);
                            add += trans + recv;
                            double convertadd = ConvertToJoule(add);
                            energysum = convertadd;
                            incount += 1;
                        }
                    }
                  count += 1;

                }
                totalenergy = energysum;
                averagesum = totalenergy / count;
                //averagesum = totalenergy / count;
                Console.WriteLine("Expected Total Energy Theoratical:" + energysum);
                Console.WriteLine("Average Total Energy Theoratical:" + averagesum);
            }


        }


        

    }
}
