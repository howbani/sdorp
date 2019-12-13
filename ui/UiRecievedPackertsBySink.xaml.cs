using SDORP.Dataplane;
using System.Windows;

namespace SDORP.ui
{
    /// <summary>
    /// Interaction logic for UiRecievedPackertsBySink.xaml
    /// </summary>
    public partial class UiRecievedPackertsBySink : Window
    {
       
        public UiRecievedPackertsBySink()
        {
            InitializeComponent();
            dg_packets.ItemsSource = PublicParamerters.FinishedRoutedPackets;
        }
    }
    
}
