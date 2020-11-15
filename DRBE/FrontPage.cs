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

namespace DRBE
{

    public class FrontPage
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

        public TextBlock Statues_tb = new TextBlock();

        public Button Tab_control_tb = new Button();
        public Button Tab_software_tb = new Button();
        public Button Tab_core_tb = new Button();
        public Button Tab_graph_tb = new Button();
        public Button Tab_workspace_tb = new Button();

        public Button Send_Seq_1 = new Button();
        public Button Send_Seq_2 = new Button();
        public Button Send_Seq_3 = new Button();
        public Button Send_Seq_4 = new Button();

        public TextBox Send_info_box = new TextBox();

        public Button Text_Clear_bt = new Button();
        private Image Text_Clear_bti = new Image();


        private Button Com_Message_bt = new Button();
        private Image Com_Message_bti = new Image();
        private ToolTip Com_Message_tt = new ToolTip();


        public Border Statues_bd = new Border();



        public Grid ParentGrid;
        public MainPage ParentPage;
        public ControlPanel DRBE_controlpanel;
        public FrontPage(Grid parent, MainPage parentpage)
        {
            ParentGrid = parent;
            ParentPage = parentpage;
            DRBE_controlpanel = new ControlPanel(ParentGrid);
            DRBE_controlpanel.Hide();
            Setup();
            Hide();


        }

        private void Setup()
        {

            Send_info_box = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 12,
                Text = "0"

            };
            Send_info_box.SetValue(Grid.ColumnProperty, 136);
            Send_info_box.SetValue(Grid.ColumnSpanProperty, 20);
            Send_info_box.SetValue(Grid.RowProperty, 30);
            Send_info_box.SetValue(Grid.RowSpanProperty, 5);
            ParentGrid.Children.Add(Send_info_box);


            Send_Seq_1 = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 12,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Content = "Send Through JTag",
                BorderBrush = white_button_brush,
                Background = Default_back_black_color_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Send_Seq_1.SetValue(Grid.ColumnProperty, 136);
            Send_Seq_1.SetValue(Grid.ColumnSpanProperty, 20);
            Send_Seq_1.SetValue(Grid.RowProperty, 70);
            Send_Seq_1.SetValue(Grid.RowSpanProperty, 5);
            Send_Seq_1.Click += Send_Seq_1_Click;
            ParentGrid.Children.Add(Send_Seq_1);

            Send_Seq_2 = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 12,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Content = "Send Ethernet",
                BorderBrush = white_button_brush,
                Background = Default_back_black_color_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Send_Seq_2.SetValue(Grid.ColumnProperty, 136);
            Send_Seq_2.SetValue(Grid.ColumnSpanProperty, 20);
            Send_Seq_2.SetValue(Grid.RowProperty, 80);
            Send_Seq_2.SetValue(Grid.RowSpanProperty, 5);
            Send_Seq_2.Click += Send_Seq_2_Click;
            ParentGrid.Children.Add(Send_Seq_2);
            //Canvas.SetZIndex(Send_Seq_1,5);

            Tab_workspace_tb = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Content = "Workspace",
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Tab_workspace_tb.SetValue(Grid.ColumnProperty, 140);
            Tab_workspace_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Tab_workspace_tb.SetValue(Grid.RowProperty, 10);
            Tab_workspace_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Tab_workspace_tb);


            Tab_graph_tb = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Content = "Graphics",
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Tab_graph_tb.SetValue(Grid.ColumnProperty, 120);
            Tab_graph_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Tab_graph_tb.SetValue(Grid.RowProperty, 10);
            Tab_graph_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Tab_graph_tb);

            Tab_core_tb = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Content = "Cores",
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Tab_core_tb.SetValue(Grid.ColumnProperty, 100);
            Tab_core_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Tab_core_tb.SetValue(Grid.RowProperty, 10);
            Tab_core_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Tab_core_tb);

            Tab_software_tb = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Content = "Software",
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Tab_software_tb.SetValue(Grid.ColumnProperty, 80);
            Tab_software_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Tab_software_tb.SetValue(Grid.RowProperty, 10);
            Tab_software_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Tab_software_tb);

            Tab_control_tb = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Content = "Control Panel",
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Tab_control_tb.SetValue(Grid.ColumnProperty, 60);
            Tab_control_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Tab_control_tb.SetValue(Grid.RowProperty, 10);
            Tab_control_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Tab_control_tb);

            Statues_bd = new Border()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.1, 0.1, 0.1, 0.1),
            };
            Canvas.SetZIndex(Statues_bd, 19);
            Statues_bd.SetValue(Grid.ColumnProperty, 0);
            Statues_bd.SetValue(Grid.ColumnSpanProperty, 60);
            Statues_bd.SetValue(Grid.RowProperty, 0);
            Statues_bd.SetValue(Grid.RowSpanProperty, 150);
            ParentGrid.Children.Add(Statues_bd);

            //Com_Message_bti.Source = new BitmapImage(new Uri("ms-appx://Assets/cnull.PNG", UriKind.RelativeOrAbsolute));
            Com_Message_bti.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Com_Icon.jpg", UriKind.RelativeOrAbsolute));
            Com_Message_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Content = Com_Message_bti,
                Background = Default_back_black_color_brush
            };
            Com_Message_bt.SetValue(Grid.ColumnProperty, 1);
            Com_Message_bt.SetValue(Grid.ColumnSpanProperty, 8);
            Com_Message_bt.SetValue(Grid.RowProperty, 1);
            Com_Message_bt.SetValue(Grid.RowSpanProperty, 10);
            ParentGrid.Children.Add(Com_Message_bt);

            Text_Clear_bti.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Refresh.png", UriKind.RelativeOrAbsolute));
            Text_Clear_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Content = Text_Clear_bti,
                Background = Default_back_black_color_brush
            };
            Text_Clear_bt.SetValue(Grid.ColumnProperty, 10);
            Text_Clear_bt.SetValue(Grid.ColumnSpanProperty, 8);
            Text_Clear_bt.SetValue(Grid.RowProperty, 1);
            Text_Clear_bt.SetValue(Grid.RowSpanProperty, 10);
            ParentGrid.Children.Add(Text_Clear_bt);
            Text_Clear_bt.Click += Text_Clear_bt_Click;
            Canvas.SetZIndex(Text_Clear_bt, 20);

            Com_Message_tt.Content = "Show COM Message";
            ToolTipService.SetToolTip(Com_Message_bt, Com_Message_tt);

            Canvas.SetZIndex(Com_Message_bt, 20);
            Com_Message_bt.Click += Com_Message_bt_Click;
            Statues_tb = new TextBlock() {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 12,
                Foreground = white_button_brush,
                Text = "DRBE_Debug_tb\r\n"
            };
            Statues_tb.SetValue(Grid.ColumnProperty, 1);
            Statues_tb.SetValue(Grid.ColumnSpanProperty, 55);
            Statues_tb.SetValue(Grid.RowProperty, 11);
            Statues_tb.SetValue(Grid.RowSpanProperty, 139);
            ParentGrid.Children.Add(Statues_tb);
            Canvas.SetZIndex(Statues_tb, 20);
        }

        private void Send_Seq_2_Click(object sender, RoutedEventArgs e)
        {
            if(Send_info_box.Text.Length>0)
            {
                List<byte> tosend = new List<byte>(Debug_Create_Packet(Send_info_box.Text));
                Statues_tb.Text += "\r\n" + BitConverter.ToString(tosend.ToArray());
                ParentPage.UWbinarywriter.Write(tosend.ToArray(), 0, tosend.Count);
                ParentPage.UWbinarywriter.Flush();
            }
        }

        private List<byte> C_T_B_N = new List<byte>();

        private List<byte> Debug_Create_Packet(string x)
        {
            List<byte> result = new List<byte>();

            int i = 0;
            string temp = "";
            i = 0;
            while(i<x.Length)
            {
                if(x[i]!=',' && x[i]!='_' && x[i]!=' ')
                {
                    temp += x[i].ToString();
                }
                else
                {
                    result.AddRange(BitConverter.GetBytes(S_D(temp)));
                    temp = "";
                }
                i++;
            }
            if(temp.Length>0)
            {
                result.AddRange(BitConverter.GetBytes(S_D(temp)));
            }
            UInt16 len = (UInt16)(result.Count + 4);

            result.Insert(0, 0x05);
            result.Insert(0, BitConverter.GetBytes(len)[0]);
            result.Insert(0, BitConverter.GetBytes(len)[1]);
            result.Insert(0, 0x08);


            return result;
        }
        private int Com_Print_LineC = 0;
        private async void Send_Seq_1_Click(object sender, RoutedEventArgs e)
        {
            List<byte> doubleby = new List<byte>();
            try
            {
                C_T_B_N = new List<byte>(D_Fixed(32.776, 7, 10));
                Statues_tb.Text += "\r\nDouble: " + (D_Fixed_d(32.776, 7, 10)).ToString();
                //DRBE_frontPage.Statues_tb.Text += "\r\nPrepare: " + BitConverter.ToString(C_T_B_N.ToArray());
                C_T_B_N.Insert(0, 0x01);
                C_T_B_N.Insert(0, 0x08);
                C_T_B_N.Insert(0, 0x00);
                C_T_B_N.Insert(0, 0x04);
                C_T_B_N.Add(0x09);



                doubleby = new List<byte>(BitConverter.GetBytes(S_D(Send_info_box.Text)));

                UInt16 thelen = 75;
                doubleby.Insert(0, BitConverter.GetBytes(thelen)[0]);
                doubleby.Insert(0, BitConverter.GetBytes(thelen)[1]);
                doubleby.Insert(0, 0x51);

                ParentPage.d_writer.WriteBytes((doubleby.ToArray()));
                await ParentPage.d_writer.StoreAsync();
                await Task.Delay(60);
                //await d_writer.FlushAsync();
                Statues_tb.Text += "         Sent: " + BitConverter.ToString(doubleby.ToArray());
                Com_Print_LineC++;
                if (Com_Print_LineC > 30)
                {
                    Com_Print_LineC = 0;
                    Statues_tb.Text = "";
                }
            }
            catch (Exception ex)
            {
                Statues_tb.Text += "\r\n Seq1 Send: " + ex.ToString();
            }
        }






        private void Text_Clear_bt_Click(object sender, RoutedEventArgs e)
        {
            Statues_tb.Text = "";
        }

        private void Com_Message_bt_Click(object sender, RoutedEventArgs e)
        {
            if (Statues_tb.Visibility == Visibility.Visible)
            {
                Statues_tb.Visibility = Visibility.Collapsed;
                Statues_bd.Visibility = Visibility.Collapsed;
            } else
            {
                Statues_tb.Visibility = Visibility.Visible;
                Statues_bd.Visibility = Visibility.Visible;
            }
        }
        public void Debugger_show()
        {
            Statues_bd.Visibility = Visibility.Visible;
            Com_Message_bt.Visibility = Visibility.Visible;
            Statues_tb.Visibility = Visibility.Visible;
            Text_Clear_bt.Visibility = Visibility.Visible;
        }
        public void Debugger_hide()
        {
            Statues_bd.Visibility = Visibility.Collapsed;
            Com_Message_bt.Visibility = Visibility.Collapsed;
            Statues_tb.Visibility = Visibility.Collapsed;
            Text_Clear_bt.Visibility = Visibility.Collapsed;
        }
        public void Show()
        {
            Send_Seq_1.Visibility = Visibility.Visible;

            Text_Clear_bt.Visibility = Visibility.Visible;

            Tab_workspace_tb.Visibility = Visibility.Visible;


            Tab_graph_tb.Visibility = Visibility.Visible;

            Tab_core_tb.Visibility = Visibility.Visible;

            Tab_software_tb.Visibility = Visibility.Visible;

            Tab_control_tb.Visibility = Visibility.Visible;

            Statues_bd.Visibility = Visibility.Visible;
            Com_Message_bt.Visibility = Visibility.Visible;
            Statues_tb.Visibility = Visibility.Visible;

            Send_info_box.Visibility = Visibility.Visible;

        }

        public void Hide()
        {
            Send_Seq_1.Visibility = Visibility.Collapsed;


            Text_Clear_bt.Visibility = Visibility.Collapsed;
            Tab_workspace_tb.Visibility = Visibility.Collapsed;


            Tab_graph_tb.Visibility = Visibility.Collapsed;

            Tab_core_tb.Visibility = Visibility.Collapsed;

            Tab_software_tb.Visibility = Visibility.Collapsed;

            Tab_control_tb.Visibility = Visibility.Collapsed;

            Statues_bd.Visibility = Visibility.Collapsed;
            Com_Message_bt.Visibility = Visibility.Collapsed;
            Statues_tb.Visibility = Visibility.Collapsed;

            Send_info_box.Visibility = Visibility.Collapsed;
        }

        public void Dispose()
        {

        }

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
    }
}
