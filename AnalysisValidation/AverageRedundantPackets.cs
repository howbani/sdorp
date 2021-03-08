using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDORP.Dataplane;
using SDORP.Intilization;
using SDORP.Forwarding;
using SDORP.ui;

namespace SDORP.AnalysisValidation
{
    class AverageRedundantPackets
    {
        public class TheoraticalRedundantPacketsProbability
        {
            public static double ConvertSecondsToMilliseconds(double seconds)
            {
                return TimeSpan.FromSeconds(seconds).TotalMilliseconds;
            }
            double timelength = ConvertSecondsToMilliseconds(PublicParamerters.Periods.ActivePeriod);
            double WaitingTimeProbability = 0;
            double WaitingTimeProbability1 = 0;
            double RedudntantPacketProbability = 0;
            double TRedudntantPacketProbability = 0;
            double CommulativeProbability = 0;
            double denomina = 1;
            double P4;
            double P3;
            double P2;
            int sub;
            double B1;
            int ac = 0;
            double P5;
            double P6;
            double P7;
            int sub2;
            double B2;
            int fdcount = 0;
            double c = 0;
            int acc = 1;
            double www2 = 0;
            double w2 = 0;
            double ww2 = 0;
            double www3 = 0;
            double sum = 0;
            double TRsum = 0;
            double x = 0;
            double RpAverage = 0;
            double TRpAverage = 0;
            double loopcount = 0;
            public void RedundantPacketsProbabilityTheoratical()
            {
                 MainWindow _mianWind = new MainWindow();
                for (int i = 1; i <= _mianWind.stopSimlationWhen; i++)
                {
                    Random randomName = new Random(); //only required once
                   // int forwarders = randomName.Next(2, 4); // No Of Nodes: 100
                    int forwarders = randomName.Next(3, 4); // No Of Nodes: 120, 140
                    // int forwarders = randomName.Next(3, 5); // No Of Nodes: 160, 180
                    // int forwarders = randomName.Next(4, 6); // No Of Nodes: 200
                    int active = randomName.Next(0, forwarders);
                    for (int j = 1; j <= forwarders; j++) // no of active candidates among forwarders
                    {
                        if (active == 0)
                        {
                            ac += 0;
                            B1 = Operations.Combination(forwarders, ac);
                            sub = forwarders - ac;
                            P2 = Math.Pow((1 / timelength), ac);
                            P3 = Math.Pow(1 - (1 / timelength), sub);
                            P4 = B1 * P2 * P3;
                            WaitingTimeProbability = P4 / denomina;
                            x += WaitingTimeProbability;
                            c += 1;
                            www2 = x / c;
                        }
                    }

                   fdcount += 1;
                    for (int k = 1; k <= forwarders; k++) // no of active candidates among forwarders
                    {
                        if (fdcount == 1)
                        {
                            acc *= 1;
                            B2 = Operations.Combination(forwarders, acc);
                            sub2 = forwarders - acc;
                            P5 = Math.Pow((1 / timelength), acc);
                            P6 = Math.Pow(1 - (1 / timelength), sub2);
                            P7 = B2 * P5 * P6;
                            WaitingTimeProbability1 = P7 / denomina;
                            ww2 += WaitingTimeProbability1;
                            w2 += 1;
                            www3 = ww2 / w2;
                        }
                        fdcount = 0;
                    }

                    RedudntantPacketProbability = 1 - (WaitingTimeProbability + WaitingTimeProbability1);
                    sum += RedudntantPacketProbability;
                    CommulativeProbability = WaitingTimeProbability + WaitingTimeProbability1 + RedudntantPacketProbability;
                    loopcount += 1;
                    RpAverage = sum / loopcount;
                    TRedudntantPacketProbability = 1 - (www2 + www3);
                    TRsum += TRedudntantPacketProbability;
                    TRpAverage = TRsum / loopcount;
                }
                    Console.WriteLine("Theoratical Redundant Packet Probability:" + TRedudntantPacketProbability);
                    Console.WriteLine("Theoratical Average Redundant Packet Probability:" + TRpAverage);
                
            }

        }
    }
}
