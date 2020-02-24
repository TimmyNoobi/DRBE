using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
using Windows.Devices.Power;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Windows.UI;
using Windows.UI.Popups;
using Windows.Devices.SerialCommunication;
using Windows.Devices.Enumeration;
using Windows.Storage.Streams;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using System.Collections.Concurrent;
using Windows.UI.Xaml.Documents;
using Windows.Storage.Search;
using Windows.Devices.Usb;
using System.Text;
using Windows.UI.Text;
using Windows.ApplicationModel;

using System.Net;
using System.Net.Sockets;

using System.Net.Http;
using Windows.Data.Pdf;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DRBE
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region color & brush
        private Color PLS_Config_canvas_color = Color.FromArgb((byte)255, (byte)64, (byte)80, (byte)80);
        private Color Back_color = Color.FromArgb((byte)255, (byte)32, (byte)32, (byte)32);
        private SolidColorBrush Transparent_brush = new SolidColorBrush(Color.FromArgb((byte)0, (byte)0, (byte)0, (byte)0));
        private SolidColorBrush Dodge_blue_brush = new SolidColorBrush(Color.FromArgb((byte)255, (byte)30, (byte)144, (byte)255));
        private SolidColorBrush Default_back_black_color_brush = new SolidColorBrush(Color.FromArgb((byte)255, (byte)32, (byte)32, (byte)32));
        private SolidColorBrush Light_back_black_color_brush = new SolidColorBrush(Color.FromArgb((byte)255, (byte)42, (byte)42, (byte)42));
        private SolidColorBrush white_button_brush = new SolidColorBrush(Color.FromArgb((byte)255, (byte)250, (byte)250, (byte)250));
        private SolidColorBrush green_text_brush = new SolidColorBrush(Color.FromArgb((byte)255, (byte)140, (byte)255, (byte)140));
        private SolidColorBrush green_button_brush = new SolidColorBrush(Color.FromArgb((byte)40, (byte)20, (byte)230, (byte)20));
        private SolidColorBrush Teal_color = new SolidColorBrush(Color.FromArgb((byte)255, (byte)84, (byte)100, (byte)130));
        private SolidColorBrush Blue_color = new SolidColorBrush(Color.FromArgb((byte)255, (byte)34, (byte)50, (byte)70));
        private SolidColorBrush Sky_blue_color = new SolidColorBrush(Color.FromArgb((byte)255, (byte)170, (byte)170, (byte)255));
        private SolidColorBrush Sky_green_color = new SolidColorBrush(Color.FromArgb((byte)255, (byte)170, (byte)255, (byte)170));
        private SolidColorBrush Sky_red_color = new SolidColorBrush(Color.FromArgb((byte)255, (byte)255, (byte)170, (byte)170));
        private SolidColorBrush Blue_Violet = new SolidColorBrush(Color.FromArgb((byte)255, (byte)138, (byte)43, (byte)226));
        private SolidColorBrush Deep_Pink = new SolidColorBrush(Color.FromArgb((byte)255, (byte)255, (byte)20, (byte)147));
        private SolidColorBrush Sea_Green = new SolidColorBrush(Color.FromArgb((byte)255, (byte)46, (byte)139, (byte)87));
        private SolidColorBrush Violet_Red = new SolidColorBrush(Color.FromArgb((byte)255, (byte)199, (byte)21, (byte)112));
        private SolidColorBrush Navy = new SolidColorBrush(Color.FromArgb((byte)255, (byte)0, (byte)0, (byte)128));
        private SolidColorBrush Light_blue_color = new SolidColorBrush(Color.FromArgb((byte)255, (byte)210, (byte)230, (byte)250));
        private SolidColorBrush red_button_brush = new SolidColorBrush(Color.FromArgb((byte)40, (byte)230, (byte)20, (byte)20));
        private SolidColorBrush yellow_brush = new SolidColorBrush(Color.FromArgb((byte)200, (byte)250, (byte)250, (byte)20));
        private SolidColorBrush Light_blue_brush = new SolidColorBrush(Color.FromArgb((byte)250, (byte)173, (byte)216, (byte)230));
        private SolidColorBrush orange_brush = new SolidColorBrush(Color.FromArgb((byte)255, (byte)255, (byte)180, (byte)50));
        private SolidColorBrush ardu_brush = new SolidColorBrush(Color.FromArgb((byte)250, (byte)0, (byte)139, (byte)139));
        private SolidColorBrush dark_grey_brush = new SolidColorBrush(Color.FromArgb((byte)250, (byte)169, (byte)169, (byte)169));
        private SolidColorBrush red_bright_button_brush = new SolidColorBrush(Color.FromArgb((byte)255, (byte)230, (byte)10, (byte)10));
        private SolidColorBrush green_bright_button_brush = new SolidColorBrush(Color.FromArgb((byte)255, (byte)10, (byte)250, (byte)10));
        private SolidColorBrush green_ready_brush = new SolidColorBrush(Color.FromArgb((byte)155, (byte)20, (byte)230, (byte)20));
        private Brush textwhite = new SolidColorBrush(Color.FromArgb((byte)255, (byte)250, (byte)250, (byte)250));
        private List<SolidColorBrush> All_Color = new List<SolidColorBrush>();
        #endregion

        public MainPage()
        {
            this.InitializeComponent();
        }


        #region communication
        private string UWPportnumber = "8200";
        private Windows.Networking.Sockets.StreamSocket UWstreamsocket = new Windows.Networking.Sockets.StreamSocket();
        private Windows.Networking.HostName UWhostname = new Windows.Networking.HostName("localhost");

        private bool UWconnectedflag = false;
        private Stream UWinputstream;
        private Stream UWoutputstream;

        private BinaryReader UWbinaryreader;
        private BinaryWriter UWbinarywriter;

        private async void ClientReading()
        {
            byte[] data = new byte[1];
            while (true)
            {
                if (UWconnectedflag)
                {
                    try 
                    {
                        await UWinputstream.ReadAsync(data, 0, 1);
                        DRBE_Debug_tb.Text += BitConverter.ToString(data);
                        Packet_receiver(data[0]);
                    }
                    catch
                    {
                        DRBE_frontPage.DRBE_controlpanel.Server_ui_tb.Text = "Not Found";
                        DRBE_frontPage.DRBE_controlpanel.Server_ui_tb.Foreground = red_bright_button_brush;

                        DRBE_frontPage.DRBE_controlpanel.Message_tb.Text += "\r\n" + DateTime.Now.ToString("HH: mm: ss~~") + "Server Disconnected";
                        UWconnectedflag = false;
                        UWstreamsocket = new Windows.Networking.Sockets.StreamSocket();
                        StartClient();
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private async void StartClient()
        {
            int i = 0;
            while (true)
            {
                if (!UWconnectedflag)
                {
                    try
                    {
                        // Create the StreamSocket and establish a connection to the echo server.
                        DRBE_frontPage.Statues_tb.Text += "\r\n Client Try to Connect" ;
                        await Task.Delay(300);
                        await UWstreamsocket.ConnectAsync(UWhostname, UWPportnumber);
                        DRBE_frontPage.Statues_tb.Text += "\r\n Client Connected: " + UWstreamsocket.Information.LocalAddress.ToString();

                        DRBE_frontPage.DRBE_controlpanel.Message_tb.Text += "\r\n" + DateTime.Now.ToString("HH: mm: ss~~") +  "Server Connected";
                        DRBE_frontPage.DRBE_controlpanel.Server_ui_tb.Text = "Connected";
                        DRBE_frontPage.DRBE_controlpanel.Server_ui_tb.Foreground = green_bright_button_brush;


                        UWconnectedflag = true;
                        UWoutputstream = UWstreamsocket.OutputStream.AsStreamForWrite();
                        UWinputstream = UWstreamsocket.InputStream.AsStreamForRead();

                        UWbinarywriter = new BinaryWriter(UWoutputstream);
                        UWbinaryreader = new BinaryReader(UWinputstream);


                        UWbinarywriter.Write(new byte[] { 0x02, 0x00, 0x05, 0x0B, 0x01, 0x02, 0x03, 0x04, 0x05, 0x00, 0x00, 0x02 }, 0, 12);
                        UWbinarywriter.Flush();

                        

                        ClientReading();

                        break;

                    }
                    catch (Exception ex)
                    {

                        await Task.Delay(1000);
                        i++;

                    }
                }
            }
            //}
        }


        // State object for reading client data asynchronously  



        #endregion
        #region others
        private async Task ShowDialog(string x, string y)
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = x,
                Content = y,
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }
        private double S_D(string x)
        {
            double result = 0;
            double sign = 1;
            string before = "";
            string after = "";
            int i = 0;
            int tenpower = 1;
            int len = x.Length;
            int beforeflag = 0;
            if (len >= 1)
            {
                if (x[0] == '-')
                {
                    sign = -1;
                }
            }
            while (i < len)
            {
                if (beforeflag == 0 && x[i] != '.')
                {
                    before += x[i].ToString();
                }
                else if (beforeflag == 1 && x[i] != '.')
                {
                    after += x[i].ToString();
                    tenpower = tenpower * 10;
                }
                else if (x[i] == '.')
                {
                    beforeflag = 1;
                }
                else if (x[i] == '-')
                {
                    //sign = -1;
                }
                else
                {
                    //sign = -1;
                }
                i++;
            }
            result = (double)S_I(before) + ((double)S_I(after)) / tenpower;
            result = result * sign;
            return result;
        }
        private int S_I(string x)
        {
            int result = 0;
            int index = 0;
            int rindex = 0;
            index = x.Length;
            while (index > 0)
            {
                if (C_I(x[rindex]) != -1)
                {
                    result = result * 10 + C_I(x[rindex]);
                }
                else
                {

                }

                rindex++;
                index--;
            }
            return result;
        }
        private int C_I(char x)
        {
            int reint = 0;
            if (x == '0')
            {
                reint = 0;
            }
            else if (x == '1')
            {
                reint = 1;
            }
            else if (x == '2')
            {
                reint = 2;

            }
            else if (x == '3')
            {
                reint = 3;
            }
            else if (x == '4')
            {
                reint = 4;
            }
            else if (x == '5')
            {
                reint = 5;
            }
            else if (x == '6')
            {
                reint = 6;
            }
            else if (x == '7')
            {
                reint = 7;
            }
            else if (x == '8')
            {
                reint = 8;
            }
            else if (x == '9')
            {
                reint = 9;
            }
            else
            {
                reint = -1;
            }
            return reint;
        }
        private int S_H(string x)
        {
            int result = 0;
            int index = 0;
            int rindex = 0;
            index = x.Length;
            while (index > 0)
            {
                if (C_H(x[rindex]) != -1)
                {
                    result = result * 16 + C_H(x[rindex]);
                }
                else
                {

                }

                rindex++;
                index--;
            }
            return result;
        }
        private int C_H(char x)
        {
            int reint = 0;
            if (x == '0')
            {
                reint = 0;
            }
            else if (x == '1')
            {
                reint = 1;
            }
            else if (x == '2')
            {
                reint = 2;

            }
            else if (x == '3')
            {
                reint = 3;
            }
            else if (x == '4')
            {
                reint = 4;
            }
            else if (x == '5')
            {
                reint = 5;
            }
            else if (x == '6')
            {
                reint = 6;
            }
            else if (x == '7')
            {
                reint = 7;
            }
            else if (x == '8')
            {
                reint = 8;
            }
            else if (x == '9')
            {
                reint = 9;
            }
            else if (x == 'a' || x == 'A')
            {
                reint = 10;
            }
            else if (x == 'b' || x == 'B')
            {
                reint = 11;
            }
            else if (x == 'c' || x == 'C')
            {
                reint = 12;
            }
            else if (x == 'd' || x == 'D')
            {
                reint = 13;
            }
            else if (x == 'e' || x == 'E')
            {
                reint = 14;
            }
            else if (x == 'f' || x == 'F')
            {
                reint = 15;
            }
            else
            {
                reint = -1;
            }
            return reint;
        }
        #endregion
        private int Packet_receiver_index = 0;
        private List<byte> Packet_receiver_result = new List<byte>();
        private byte Packet_device = 0;
        private byte Packet_command = 0;
        private int Packet_len = 0;
        private string Packet_message = "";


        private void Packet_receiver(byte x)
        {


            DRBE_frontPage.Statues_tb.Text += x.ToString() + "-";
            Packet_receiver_result.Add(x);
            Packet_receiver_index++;
            if (Packet_receiver_index == 1)
            {
                Packet_device = x;
                Packet_len = 0;
                
            }
            else if (Packet_receiver_index == 2)
            {
                Packet_len = x;
            }
            else if (Packet_receiver_index == 3)
            {
                Packet_len = Packet_len * 255 + x;
                DRBE_frontPage.Statues_tb.Text += "\r\n Packet Length: " + Packet_len.ToString() + "\r\n";
            }
            else if (Packet_receiver_index == 5)
            {
                Packet_command = x;
            }
            else if (Packet_receiver_index == Packet_len + 7)
            {
                if (Packet_device != x)
                {
                    DRBE_frontPage.Statues_tb.Text += "\r\nError: " + BitConverter.ToString(Packet_receiver_result.ToArray()) + "\r\n";
                }
                else
                {
                    DRBE_frontPage.Statues_tb.Text += "\r\nReceived: " + BitConverter.ToString(Packet_receiver_result.ToArray()) + "\r\n";
                    Packet_receiver_result = new List<byte>();
                    Packet_message += "\r\n" + DateTime.Now.ToString("HH: mm: ss~~");
                    if (Packet_device == 0x02)
                    {
                        Packet_message += "Matlab ";
                    }
                    if(Packet_command == 0x01)
                    {
                        Packet_message += "is Synced ";
                        DRBE_frontPage.DRBE_controlpanel.Server_matlab_tb.Text = "Connected";
                        DRBE_frontPage.DRBE_controlpanel.Server_matlab_tb.Foreground = green_bright_button_brush;

                    }
                    DRBE_frontPage.DRBE_controlpanel.Message_tb.Text += Packet_message;
                }
                Packet_receiver_index = 0;
            }
            else
            {

            }

        }

        private TextBlock DRBE_Debug_tb = new TextBlock();


        

        private Button DRBE_Sync = new Button();

        private FrontPage DRBE_frontPage;
        
        private async void MainPage_loaded(object sender, RoutedEventArgs e)
        {
            DRBE_frontPage = new FrontPage(MainGrid);
            //DRBE_Debug_tb = new TextBlock() { 
            //    HorizontalAlignment = HorizontalAlignment.Stretch,
            //    VerticalAlignment = VerticalAlignment.Stretch,
            //    FontSize = 12,
            //    Foreground = white_button_brush,
            //    Text = "DRBE_Debug_tb\r\n"
            //};
            //DRBE_Debug_tb.SetValue(Grid.ColumnProperty, 5);
            //DRBE_Debug_tb.SetValue(Grid.ColumnSpanProperty, 100);
            //DRBE_Debug_tb.SetValue(Grid.RowProperty, 5);
            //DRBE_Debug_tb.SetValue(Grid.RowSpanProperty, 100);
            //MainGrid.Children.Add(DRBE_Debug_tb);



            StartClient();

            DRBE_frontPage.Show();

        }

        private void MainPage_sizechange(object sender, SizeChangedEventArgs e)
        {

        }
    }
}
