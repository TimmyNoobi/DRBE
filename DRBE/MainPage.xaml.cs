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

using System.Diagnostics;
using Windows.Networking.Sockets;

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
        public Stream UWinputstream;
        public Stream UWoutputstream;

        public BinaryReader UWbinaryreader;
        public BinaryWriter UWbinarywriter;


        public bool Data_ready_flag = false;
        private async void ClientReading()
        {
            byte[] data = new byte[1];
            //await ShowDialog("sError", "sError");
            while (true)
            {
                if (UWconnectedflag)
                {
                    try 
                    {
                        await UWinputstream.ReadAsync(data, 0, 1);
                        //DRBE_Debug_tb.Text += BitConverter.ToString(data);
                        await Packet_receiver(data[0]);
                    }
                    catch(Exception ex)
                    {
                        DRBE_frontPage.DRBE_controlpanel.Server_ui_tb.Text = "Not Found";
                        DRBE_frontPage.DRBE_controlpanel.Server_ui_tb.Foreground = red_bright_button_brush;


                        DRBE_frontPage.DRBE_controlpanel.Message_tb.Text += "\r\n" + DateTime.Now.ToString("HH: mm: ss~~") + "Server Disconnected";
                        UWconnectedflag = false;
                        UWstreamsocket = new Windows.Networking.Sockets.StreamSocket();

                        //MainPageTestTb.Text = ex.ToString();
                        //await ShowDialog("Error", ex.ToString());

                        //StartClient();
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private MatlabPacket mpacket = new MatlabPacket();
        private List<byte> testpacket = new List<byte>();

        private bool UI_Stream_ready_flag = false;
        private async void StartClient()
        {
            int i = 0;
            while (true)
            {
                if (!UWconnectedflag)
                {
                    UI_Stream_ready_flag = false;
                    DRBE_softwarePage.UI_Stream_ready_flag = false;
                    DRBE_ap.UI_Stream_ready_flag = false;
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

                        DRBE_softwarePage.UWbinarywriter = UWbinarywriter;
                        DRBE_softwarePage.UI_Stream_ready_flag = true;

                        DRBE_ap.UWbinarywriter = UWbinarywriter;
                        DRBE_ap.UI_Stream_ready_flag = true;

                        UI_Stream_ready_flag = true;
                        testpacket = new List<byte>(mpacket.Pass(new List<byte>()));

                        await Task.Delay(500);
                        //UWbinarywriter.Write(testpacket.ToArray(), 0, 255);
                        //UWbinarywriter.Flush();

                        

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
        private List<byte> D_Fixed(double x, int ger, int fra)
        {
            List<byte> result = new List<byte>();

            int i = 0;
            int dger = (int)x;
            double dfra = x - (int)x;
            UInt32 temp = 0;
            int refger = (int)Math.Pow(2,ger);
            double reffra = 1/2;

            while (i<ger)
            {
                if(dger>=refger)
                {
                    temp += 1;
                    dger = dger - refger;
                }
                temp = temp << 1;
                refger = refger / 2;
                i++;
            }
            i = 0;
            while (i < fra)
            {
                if(dfra>reffra)
                {
                    temp += 1;
                    dfra -= reffra;
                    reffra /= 2;
                }
                
                temp = temp << 1;
                i++;
            }

            result = new List<byte>(BitConverter.GetBytes(temp));
            

            return result;
        }

        private double D_Fixed_d(double x, int ger, int fra)
        {
            double result = 0;

            int i = 0;
            int dger = (int)x;
            double dfra = x - (int)x;
            UInt32 temp = 0;
            int refger = (int)Math.Pow(2, ger);
            double reffra = 1 / 2;

            while (i < ger)
            {
                if (dger >= refger)
                {
                    temp += 1;
                    dger = dger - refger;
                }
                temp = temp << 1;
                refger = refger / 2;
                i++;
            }
            i = 0;
            while (i < fra)
            {
                if (dfra > reffra)
                {
                    temp += 1;
                    dfra -= reffra;
                    reffra /= 2;
                }

                temp = temp << 1;
                i++;
            }

            result = temp;


            return result;
        }
        private byte S_B(string x)
        {
            byte result = 0;
            int temp = S_H(x);
            result = BitConverter.GetBytes(temp)[0];
            //ShowDialog(BitConverter.ToString(BitConverter.GetBytes(temp)), result.ToString());
            return result;
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
            result = S_I_vd(before) + (S_I_vd(after)) / tenpower;
            result = result * sign;
            return result;
        }
        private double S_I_vd(string x)
        {
            double result = 0;
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
        public List<double> Packet_data_buffer = new List<double>();
        private byte Packet_device = 0;
        private byte Packet_command = 0;
        private int Packet_len = 0;
        private string Packet_message = "";


        //private void Packet_receiver(byte x)
        //{


        //    DRBE_frontPage.Statues_tb.Text += x.ToString() + "-";
        //    Packet_receiver_result.Add(x);
        //    Packet_receiver_index++;
        //    if (Packet_receiver_index == 1) 
        //    {
        //        Packet_device = x;
        //        Packet_len = 0;

        //    } //device ID
        //    else if (Packet_receiver_index == 2)
        //    {
        //        Packet_len = x;
        //    } //length MS
        //    else if (Packet_receiver_index == 3)
        //    {
        //        Packet_len = Packet_len * 255 + x;
        //        DRBE_frontPage.Statues_tb.Text += "\r\n Packet Length: " + Packet_len.ToString() + "\r\n";
        //    } //length LS
        //    else if (Packet_receiver_index == 4)
        //    {
        //        Packet_command = x;
        //    } //command
        //    else if (Packet_receiver_index == Packet_len + 7)
        //    {
        //        if (Packet_device != x)
        //        {
        //            DRBE_frontPage.Statues_tb.Text += "\r\nError: " + BitConverter.ToString(Packet_receiver_result.ToArray()) + "\r\n";
        //        }
        //        else
        //        {
        //            DRBE_frontPage.Statues_tb.Text += "\r\nReceived: " + BitConverter.ToString(Packet_receiver_result.ToArray()) + "\r\n";
        //            Packet_message += "\r\n" + DateTime.Now.ToString("HH: mm: ss~~");
        //            if (Packet_device == 0x02)
        //            {
        //                Packet_message += "Matlab ";
        //            }
        //            if(Packet_command == 0x01)
        //            {
        //                Packet_message += "is Synced ";
        //                DRBE_frontPage.DRBE_controlpanel.Server_matlab_tb.Text = "Connected";
        //                DRBE_frontPage.DRBE_controlpanel.Server_matlab_tb.Foreground = green_bright_button_brush;

        //            }else if(Packet_command == 0x10)
        //            {
        //                DRBE_softwarePage.Message_calibration_tb.Text += "\r\n" + "Performance: " + (((double)1 / (double)(Packet_receiver_result[5]*256 + Packet_receiver_result[6]))).ToString();
        //            } 
        //            DRBE_frontPage.DRBE_controlpanel.Message_tb.Text += Packet_message;
        //            Packet_receiver_result = new List<byte>();
        //        }
        //        Packet_receiver_index = 0;
        //    }
        //    else
        //    {

        //    }

        //}


        public List<List<double>> Data_buffer_2D = new List<List<double>>();
        public int Data_buffer_2D_index = 0;

        private async Task Packet_receiver(byte x)
        {
            //await ShowDialog("start",Packet_receiver_index.ToString());
            //MainPageTestTb.Text += x.ToString() + "-";
            Packet_receiver_result.Add(x);
            Packet_receiver_index++;
            if (Packet_receiver_index == 1)
            {
                Packet_data_buffer.Clear();
                Packet_device = x;
                Packet_len = 0;
            } //device ID
            else if (Packet_receiver_index == 2)
            {
                Packet_len = x;
            } //length MS
            else if (Packet_receiver_index == 3)
            {
                //await ShowDialog(Packet_receiver_result[Packet_receiver_index - 1].ToString(), Packet_receiver_result.Count.ToString());
                Packet_len = Packet_len * 255 + x;
                //MainPageTestTb.Text += "\r\n Packet Length: " + Packet_len.ToString() + "\r\n";
            } //length LS
            else if (Packet_receiver_index == 4)
            {
                Packet_command = x;
            } //command
            else if (Packet_receiver_index == Packet_len +3)
            //else if (Packet_receiver_index == 20)
            {
                //await ShowDialog(Packet_receiver_result[Packet_receiver_index-1].ToString(), Packet_receiver_result.Count.ToString());
                Packet_receiver_index = 0;
                int i = 0;
                i = 3;
                while(i<Packet_receiver_result.Count)
                {
                    Packet_data_buffer.Add(BitConverter.ToDouble(Packet_receiver_result.GetRange(i, 8).ToArray(), 0));
                    //temp += BitConverter.ToDouble(Packet_receiver_result.GetRange(i, 8).ToArray(), 0).ToString() + " ";
                    i +=8;
                }
                Packet_receiver_result.Clear();
                Data_buffer_2D.Add(new List<double>(Packet_data_buffer));
                Data_buffer_2D_index++;
                //MainPageTestTb.Text += temp;
                Data_ready_flag = true;

            }
            else
            {

            }

        }

        private TextBlock DRBE_Debug_tb = new TextBlock();


        

        private Button DRBE_Sync = new Button();

        private FrontPage DRBE_frontPage;
        private SoftwarePanel DRBE_softwarePage;
        private DRBEScenario DRBE_Scenario;
        private DRBE_SUT DRBE_SUT_Page;
        private Communication_Protocol_Page Communication_Protocol_Page;
        private DRBE_AP DRBE_ap;
        public DRBE_MainPage1 DRBE_mainpage1;
        //private DRBE_Link_Viewer_s DRBE_lv;
        private Template_Make TM_Test;

        public DRBE_Scenario_Generator DRBE_SG;


        public TextBlock MainPageTestTb = new TextBlock();
        public Border MainPageTestBd = new Border();
        public ScrollViewer MainPageTestSv = new ScrollViewer();

        private byte[] Communication_test_byte = new byte[] { 0x22 };
        private List<byte> C_T_B_N = new List<byte>();

        public DRBE_LinkViewer Link_Viewer;
        private async void MainPage_loaded(object sender, RoutedEventArgs e)
        {
            MainPageTestSv = new ScrollViewer() { 
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush
            
            };
            //MainGrid.Children.Add(MainPageTestSv);
            MainPageTestSv.SetValue(Grid.ColumnProperty, 0);
            MainPageTestSv.SetValue(Grid.ColumnSpanProperty, 50);
            MainPageTestSv.SetValue(Grid.RowProperty, 0);
            MainPageTestSv.SetValue(Grid.RowSpanProperty, 100);
            Canvas.SetZIndex(MainPageTestSv, 20);

            MainPageTestTb = new TextBlock() { 
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Foreground = white_button_brush,
                FontSize = 12
            };

            //MainGrid.Children.Add(MainPageTestTb);
            //MainPageTestTb.SetValue(Grid.ColumnProperty, 0);
            //MainPageTestTb.SetValue(Grid.ColumnSpanProperty, 50);
            //MainPageTestTb.SetValue(Grid.RowProperty, 0);
            //MainPageTestTb.SetValue(Grid.RowSpanProperty, 100);
            //Canvas.SetZIndex(MainPageTestTb,20);

            MainPageTestSv.Content = MainPageTestTb;

            MainPageTestBd = new Border() {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush
            
            };
            //MainGrid.Children.Add(MainPageTestBd);
            MainPageTestBd.SetValue(Grid.ColumnProperty, 0);
            MainPageTestBd.SetValue(Grid.ColumnSpanProperty, 50);
            MainPageTestBd.SetValue(Grid.RowProperty, 0);
            MainPageTestBd.SetValue(Grid.RowSpanProperty, 100);
            Canvas.SetZIndex(MainPageTestBd, 19);


            await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
            await Task.Delay(1000);

            DRBE_frontPage = new FrontPage(MainGrid);
            DRBE_softwarePage = new SoftwarePanel(MainGrid, UWbinarywriter);
            Communication_Protocol_Page = new Communication_Protocol_Page(MainGrid);
            DRBE_ap = new DRBE_AP(MainGrid, UWbinarywriter);
            DRBE_mainpage1 = new DRBE_MainPage1(MainGrid, this);
            

            //DRBE_SUT_Page = new DRBE_SUT(MainGrid);
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




            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            DRBE_frontPage.Statues_tb.Text += storageFolder.Path;

            DRBE_Scenario = new DRBEScenario(MainGrid);
            await DRBE_Scenario.Parse_Scenario("DS1.txt");
            //DRBE_lv = new DRBE_Link_Viewer_s(MainGrid, this);
            //DRBE_SG = new DRBE_Scenario_Generator(MainGrid);

            DRBE_mainpage1.Show();

            //TM_Test = new Template_Make(MainGrid, this);


            //DRBE_ap.Show();
            //DRBE_lv.Setup(DRBE_Scenario.D_Trans, DRBE_Scenario.D_Rec, DRBE_Scenario.D_Ref);
            StartClient();
            
            //DRBE_softwarePage.Show();
            //DRBE_frontPage.Show();
            //DRBE_ap.Show();
            //DRBE_lv.hide();
            //ConnectToSerialPort();
            //AdvReadByte(ReadCancellationTokenSource.Token);

            DRBE_frontPage.Send_Seq_1.Click += Send_Seq_1_Click;

            S_B("A1");


            ScrollViewer svtest = new ScrollViewer() { 
            
            };
            StackPanel sptest = new StackPanel() { 
                
            };




            #region Slides
            Button ssbt = new Button() { 
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Content = "1",
                Foreground = white_button_brush,
                FontSize = 18

            };
            MainGrid.Children.Add(ssbt);
            ssbt.SetValue(Grid.ColumnProperty, 185);
            ssbt.SetValue(Grid.ColumnSpanProperty, 5);
            ssbt.SetValue(Grid.RowProperty, 145);
            ssbt.SetValue(Grid.RowSpanProperty, 5);
            ssbt.Click += Ssbt1_Click;

            Canvas.SetZIndex(ssbt,20);

            //Button ssbt2 = new Button()
            //{
            //    VerticalAlignment = VerticalAlignment.Stretch,
            //    HorizontalAlignment = HorizontalAlignment.Stretch,
            //    Content = "2",
            //    Foreground = white_button_brush,
            //    FontSize = 18

            //};
            //MainGrid.Children.Add(ssbt2);
            //ssbt2.SetValue(Grid.ColumnProperty, 190);
            //ssbt2.SetValue(Grid.ColumnSpanProperty, 5);
            //ssbt2.SetValue(Grid.RowProperty, 145);
            //ssbt2.SetValue(Grid.RowSpanProperty, 5);
            //ssbt2.Click += Ssbt2_Click;

            //Button ssbt3 = new Button()
            //{
            //    VerticalAlignment = VerticalAlignment.Stretch,
            //    HorizontalAlignment = HorizontalAlignment.Stretch,
            //    Content = "3",
            //    Foreground = white_button_brush,
            //    FontSize = 18

            //};
            //MainGrid.Children.Add(ssbt3);
            //ssbt3.SetValue(Grid.ColumnProperty, 195);
            //ssbt3.SetValue(Grid.ColumnSpanProperty, 5);
            //ssbt3.SetValue(Grid.RowProperty, 145);
            //ssbt3.SetValue(Grid.RowSpanProperty, 5);
            //ssbt3.Click += Ssbt3_Click;
            #endregion
            pp = new Pic_Player(MainGrid);


            All_Received_byte_list = new List<byte>();

            //executeCommand();

            DRBE_SG = new DRBE_Scenario_Generator(MainGrid, this);
            //DRBE_SG.show();
            Link_Viewer = new DRBE_LinkViewer(MainGrid, this);

        }

        public void Write_byte_list(List<byte> x)
        {
            UWbinarywriter.Write(x.ToArray(), 0, x.Count);
            UWbinarywriter.Flush();
        }

        private async void executeCommand()
        {
            Process myProcess = new Process();
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            string path = storageFolder.Path;
            myProcess.StartInfo = new ProcessStartInfo(path + "\\Simulator File\\DRBE_CPP1.exe");
            //myProcess.StartInfo.WorkingDirectory = workingDirectory;
            //myProcess.StartInfo.FileName = programFilePath;
            //myProcess.StartInfo.Arguments = commandLineArgs;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.CreateNoWindow = false;
            myProcess.StartInfo.RedirectStandardOutput = true;
            myProcess.StartInfo.RedirectStandardError = true;
            myProcess.Start();

            //StreamReader sOut = myProcess.StandardOutput;
            //StreamReader sErr = myProcess.StandardError;

            try
            {

                string output = myProcess.StandardOutput.ReadToEnd();
                string outerr = myProcess.StandardError.ReadToEnd();
                //string str = "";

                // reading errors and output async...
                await Task.Delay(1000);

                myProcess.WaitForExit();
                await ShowDialog(output, outerr);
            }
            catch(Exception ex)
            {
                await ShowDialog("Error", ex.ToString());
            }
            finally
            {
            }
        }



        public Pic_Player pp;
        private void Ssbt1_Click(object sender, RoutedEventArgs e)
        {
            pp.path = "New_demo.PNG";
            pp.Show();
        }
        private void Ssbt2_Click(object sender, RoutedEventArgs e)
        {
            pp.path = "Slides4.PNG";
            pp.Show();
        }
        private void Ssbt3_Click(object sender, RoutedEventArgs e)
        {
            pp.path = "Slides5.PNG";
            pp.Show();
        }
        private int Com_Print_LineC = 0;
        private async void Send_Seq_1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                C_T_B_N = new List<byte>(D_Fixed(32.776, 7, 10));
                DRBE_frontPage.Statues_tb.Text += "\r\nDouble: " + (D_Fixed_d(32.776, 7, 10)).ToString();
                //DRBE_frontPage.Statues_tb.Text += "\r\nPrepare: " + BitConverter.ToString(C_T_B_N.ToArray());
                C_T_B_N.Insert(0, 0x01);
                C_T_B_N.Insert(0, 0x08);  
                C_T_B_N.Insert(0, 0x00);
                C_T_B_N.Insert(0, 0x04);
                C_T_B_N.Add(0x09);
                d_writer.WriteBytes((C_T_B_N.ToArray()));
                await d_writer.StoreAsync();
                await Task.Delay(60);
                //await d_writer.FlushAsync();
                DRBE_frontPage.Statues_tb.Text += "         Sent: " + BitConverter.ToString(C_T_B_N.ToArray());
                Com_Print_LineC++;
                if (Com_Print_LineC>30)
                {
                    Com_Print_LineC = 0;
                    DRBE_frontPage.Statues_tb.Text = "";
                }
            }
            catch(Exception ex)
            {
                DRBE_frontPage.Statues_tb.Text += "\r\n Seq1 Send: " + ex.ToString();
            }
        }


        private List<byte> Build_Packet_V1(List<byte> x)
        {
            List<byte> result = new List<byte>();
            //UInt16 
            //result.Add(byte)
            return result;
        }

        private void MainPage_sizechange(object sender, SizeChangedEventArgs e)
        {

        }

        #region communication
        SerialDevice serialDevice = null;
        DataReader d_reader = null;
        DataWriter d_writer = null;
        CancellationTokenSource ReadCancellationTokenSource = new CancellationTokenSource();
        bool USB_Connected_flag = false;
        private async void ConnectToSerialPort()
        {
            while (true)
            {
                if (!USB_Connected_flag)
                {
                    string selector = SerialDevice.GetDeviceSelector();
                    DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(selector);
                    if (devices.Count > 0)
                    {
                        DRBE_frontPage.Statues_tb.Text += selector;
                        DRBE_frontPage.Statues_tb.Text += "\r\n " + devices.Count.ToString();
                        DRBE_frontPage.Statues_tb.Text += "\r\n " + devices[0].Name.ToString();

                        DeviceInformation deviceInfo = devices[0];

                        DRBE_frontPage.Statues_tb.Text += "\r\n " + deviceInfo.Id.ToString();
                        try
                        {
                            serialDevice = await SerialDevice.FromIdAsync(deviceInfo.Id);
                            DRBE_frontPage.Statues_tb.Text += "\r\n Connected" + serialDevice.ToString();
                            serialDevice.BaudRate = 115200;
                            serialDevice.DataBits = 8;
                            serialDevice.StopBits = SerialStopBitCount.Two;
                            serialDevice.Parity = SerialParity.None;
                            d_reader = new DataReader(serialDevice.InputStream);
                            d_writer = new DataWriter(serialDevice.OutputStream);
                            USB_Connected_flag = true;

                        }
                        catch (Exception es)
                        {
                            DRBE_frontPage.Statues_tb.Text += "\r\n " + es.ToString();
                            await Task.Delay(3000);
                        }




                    }
                    else
                    {

                    }
                }
                else
                {
                    await Task.Delay(2000);
                }
            }
        }
        private List<byte> All_Received_byte_list = new List<byte>();
        private async void AdvReadByte(CancellationToken cancellationToken)
        {
            Task<UInt32> loadAsyncTask;
            int bytesRead = 0;
            while (true)
            {
                if (USB_Connected_flag)
                {
                    try
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        d_reader.InputStreamOptions = InputStreamOptions.Partial;
                        loadAsyncTask = d_reader.LoadAsync(1).AsTask(cancellationToken);
                        bytesRead = Convert.ToInt32(await loadAsyncTask);
                    }
                    catch
                    {
                        throw;
                    }


                    int i = 0;
                    byte[] resultb = new byte[bytesRead];
                    while (i < bytesRead)
                    {

                        resultb[i] = d_reader.ReadByte();

                        DRBE_frontPage.Statues_tb.Text += "received" + BitConverter.ToString(resultb) + " - " + ((char)resultb[i]).ToString();
                        i++;
                    }
                }
                else
                {
                    await Task.Delay(2000);
                }
            }
        }



        #endregion
    }
}
