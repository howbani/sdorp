using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDORP.Dataplane;
using SDORP.Intilization;
using SDORP.Forwarding;
using SDORP.ui;
using System.Diagnostics;


namespace SDORP.AnalysisValidation
{
    class AverageWaitingTime
    {
        public class TheoraticalWaitingTimeProbability
        {
            public static double ConvertSecondsToMilliseconds(double seconds)
            {
                return TimeSpan.FromSeconds(seconds).TotalMilliseconds;
            }
            double timelength = ConvertSecondsToMilliseconds(PublicParamerters.Periods.ActivePeriod);
            double WaitingTimeProbability = 0;
            double denomina = 1;
            double P4;
            double P3;
            double P2;
            int sub;
            double B1;
            int ac = 0;
            double x = 0;
            double c = 0;
            double count = 0;
            double totalwaitingtime = 0;
            double averagewt;
            public void WaitingTimeProbabilityTheoratical()
            {
                MainWindow _mianWind = new MainWindow();
                for (int i = 1; i <=_mianWind.stopSimlationWhen; i++)
                { 
                    Random randomName = new Random(); //only required once
                   int forwarders = randomName.Next(2, 4); // No Of Nodes: 100
                 //   int forwarders = randomName.Next(2, 5); // No Of Nodes: 120, 140
               // int forwarders = randomName.Next(3, 5); // No Of Nodes: 160, 180
                //  int forwarders = randomName.Next(4, 6); // No Of Nodes: 200
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
                        }
                    }
                    count += c;
                    totalwaitingtime += x;
                }
                averagewt = totalwaitingtime / count;
                Trace.WriteLine("Theoratical Probability of Average Waiting Time:" + averagewt);
                Console.WriteLine("Theoratical Probability of Average Waiting Time:" + averagewt);

            }



        }
    }
}
