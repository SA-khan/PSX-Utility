using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PSXDataFetchingApp
{
    /// <summary>
    /// Interaction logic for PreviewWindow.xaml
    /// </summary>
    public partial class PreviewWindow : Window
    {
        public class MarketSummary
        {
            public string Name { get; set; }
            public string Symbol { get; set; }
            public double CURRENT { get; set; }

            public double LDCP { get; set; }
            public double OPEN { get; set; }
            public double HIGH { get; set; }
            public double LOW { get; set; }
            public double Change { get; set; }
            public double Volume { get; set; }
        }
        public PreviewWindow(DateTime date, string status, Double Volume, Double Value, Double Trades, List<string> companyName, List<string> companySymbol, List<double> LDCP, List<double> OPEN, List<double> HIGH, List<double> LOW, List<double> CURRENT, List<double> CHANGE, List<double> VOLUME)
        {
            InitializeComponent();
            lblDate.Content += date.ToString();
            lblStatus.Content += status.ToString();
            lblVolume.Content += Volume.ToString();
            lblValue.Content += Value.ToString();
            lblTrades.Content += Trades.ToString();

            MarketSummary[] data = new MarketSummary[companyName.Count];

            GridViewColumn col1 = new GridViewColumn();
            GridViewColumn col2 = new GridViewColumn();
            GridViewColumn col3 = new GridViewColumn();
            GridViewColumn col4 = new GridViewColumn();
            GridViewColumn col5 = new GridViewColumn();
            GridViewColumn col6 = new GridViewColumn();
            GridViewColumn col7 = new GridViewColumn();
            GridViewColumn col8 = new GridViewColumn();
            GridViewColumn col9 = new GridViewColumn();

            gridView.Columns.Add(col1);
            gridView.Columns.Add(col2);
            gridView.Columns.Add(col3);
            gridView.Columns.Add(col4);
            gridView.Columns.Add(col5);
            gridView.Columns.Add(col6);
            gridView.Columns.Add(col7);
            gridView.Columns.Add(col8);
            gridView.Columns.Add(col9);
            col1.DisplayMemberBinding = new Binding("Name");
            col2.DisplayMemberBinding = new Binding("Symbol");
            col3.DisplayMemberBinding = new Binding("CURRENT");
            col4.DisplayMemberBinding = new Binding("LDCP");
            col5.DisplayMemberBinding = new Binding("OPEN");
            col6.DisplayMemberBinding = new Binding("HIGH");
            col7.DisplayMemberBinding = new Binding("LOW");
            col8.DisplayMemberBinding = new Binding("Change");
            col9.DisplayMemberBinding = new Binding("Volume");
            col1.Header = "NAME";
            col2.Header = "SYMBOL";
            col3.Header = "CURRENT";
            col4.Header = "LDCP";
            col5.Header = "OPEN";
            col6.Header = "HIGH";
            col7.Header = "LOW";
            col8.Header = "CHANGE";
            col9.Header = "VOLUME";

            //lblMessage.Content = "COMPANY NAME - SYMBOL - CURRENT - LDCP - OPEN - HIGH - LOW - CURRENT - CHANGE - VOLUME \n" ;
            for (int i = 0; i < companyName.Count; i++)
            {
                if (companyName[i] == null) { }
                else
                {

                    list1.Items.Add(new MarketSummary { Name = companyName[i], Symbol = companySymbol[i], CURRENT = CURRENT[i],LDCP = LDCP[i], OPEN = OPEN[i], HIGH = HIGH[i], LOW = LOW[i], Change = CHANGE[i], Volume = VOLUME[i] });
                }
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            mainWindow.Button_Click(sender, e);
            this.Close();
        }
    }
}
