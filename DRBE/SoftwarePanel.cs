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
    public class SoftwarePanel
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
        public Grid ParentGrid;

        private Border Transmitter_bd = new Border();
        private Border Receiver_bd = new Border();
        private Border Platform_bd = new Border();
        private Border Tool_bd = new Border();
        private Border Message_bd = new Border();

        private TextBlock Transmitter_tb = new TextBlock();
        private TextBlock Receiver_tb = new TextBlock();
        private TextBlock Platform_tb = new TextBlock();
        private TextBlock Tool_tb = new TextBlock();
        private TextBlock Message_tb = new TextBlock();
        public TextBlock Message_calibration_tb = new TextBlock();

        private List<Button> Transmitter_bt = new List<Button>();
        private List<Button> Platform_bt = new List<Button>();
        private List<Button> Receiver_bt = new List<Button>();

        private Button Read_scenario_bt = new Button();
        private Image Read_scenario_bti = new Image();

        private Button Fidelity_simulation_bt = new Button();
        private Image Fidelity_simulation_bti = new Image();

        private Button Show_link_bt = new Button();
        private Image Show_link_bti = new Image();


        private Dictionary<Button, Transmitter> D_bt_transmitter = new Dictionary<Button, Transmitter>();
        private Dictionary<Button, Platform> D_bt_platform = new Dictionary<Button, Platform>();
        private Dictionary<Button, Receiver> D_bt_receiver = new Dictionary<Button, Receiver>();
        public BinaryWriter UWbinarywriter;
        public bool UI_Stream_ready_flag = false;
        public SoftwarePanel(Grid parent, BinaryWriter writer)
        {
            ParentGrid = parent;
            UWbinarywriter = writer;
            Setup();
            Hide();
        }
        public void Create_Transmitter(List<Transmitter> x)
        {
            int row = 32;

            int i = 0;
            i = 0;
            while (i < Transmitter_bt.Count())
            {
                if (ParentGrid.Children.Contains(Transmitter_bt[i]))
                {
                    ParentGrid.Children.Remove(Transmitter_bt[i]);
                }
                i++;
            }
            Transmitter_bt = new List<Button>();
            D_bt_transmitter = new Dictionary<Button, Transmitter>();
            i = 0;
            while (i < x.Count)
            {
                Transmitter_bt.Add(new Button()
                {
                    BorderBrush = white_button_brush,
                    BorderThickness = new Thickness(3, 3, 3, 3),
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Content = "Transmitter" + x[i].ID.ToString(),
                    Foreground = white_button_brush,
                    FontSize = 18
                });
                ParentGrid.Children.Add(Transmitter_bt[Transmitter_bt.Count - 1]);
                Transmitter_bt[Transmitter_bt.Count - 1].SetValue(Grid.ColumnProperty, 62);
                Transmitter_bt[Transmitter_bt.Count - 1].SetValue(Grid.ColumnSpanProperty, 20);
                Transmitter_bt[Transmitter_bt.Count - 1].SetValue(Grid.RowProperty, row);
                Transmitter_bt[Transmitter_bt.Count - 1].SetValue(Grid.RowSpanProperty, 8);
                D_bt_transmitter[Transmitter_bt[Transmitter_bt.Count - 1]] = x[i];
                Transmitter_bt[Transmitter_bt.Count - 1].Click += Transmitter_bt_Click;
                row += 10;
                i++;
            }

        }

        private void Transmitter_bt_Click(object sender, RoutedEventArgs e)
        {
            Button yoo = sender as Button;
            Message_calibration_tb.Text = "";
            int i = 0;
            i = 0;
            while (i < D_bt_transmitter[yoo].Property_string.Count)
            {

                Message_calibration_tb.Text += "\r\n" + D_bt_transmitter[yoo].Property_string[i] + D_bt_transmitter[yoo].Property_value[i].ToString();
                i++;
            }
        }

        public void Create_Receiver(List<Receiver> x)
        {
            int row = 32;

            int i = 0;
            i = 0;
            while (i < Receiver_bt.Count())
            {
                if (ParentGrid.Children.Contains(Receiver_bt[i]))
                {
                    ParentGrid.Children.Remove(Receiver_bt[i]);
                }
                i++;
            }
            Receiver_bt = new List<Button>();
            D_bt_receiver = new Dictionary<Button, Receiver>();
            i = 0;
            while (i < x.Count)
            {
                Receiver_bt.Add(new Button()
                {
                    BorderBrush = white_button_brush,
                    BorderThickness = new Thickness(3, 3, 3, 3),
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Content = "Receiver" + x[i].ID.ToString(),
                    Foreground = white_button_brush,
                    FontSize = 18
                });
                ParentGrid.Children.Add(Receiver_bt[Receiver_bt.Count - 1]);
                Receiver_bt[Receiver_bt.Count - 1].SetValue(Grid.ColumnProperty, 87);
                Receiver_bt[Receiver_bt.Count - 1].SetValue(Grid.ColumnSpanProperty, 20);
                Receiver_bt[Receiver_bt.Count - 1].SetValue(Grid.RowProperty, row);
                Receiver_bt[Receiver_bt.Count - 1].SetValue(Grid.RowSpanProperty, 8);
                D_bt_receiver[Receiver_bt[Receiver_bt.Count - 1]] = x[i];
                Receiver_bt[Receiver_bt.Count - 1].Click += Receiver_bt_Click;
                row += 10;
                i++;
            }
        }

        private void Receiver_bt_Click(object sender, RoutedEventArgs e)
        {
            Button yoo = sender as Button;
            Message_calibration_tb.Text = "";
            int i = 0;
            i = 0;
            while (i < D_bt_receiver[yoo].Property_string.Count)
            {

                Message_calibration_tb.Text += "\r\n" + D_bt_receiver[yoo].Property_string[i] + D_bt_receiver[yoo].Property_value[i].ToString();
                i++;
            }
        }

        public void Create_Platform(List<Platform> x)
        {
            int row = 32;

            int i = 0;
            i = 0;
            while (i < Platform_bt.Count())
            {
                if (ParentGrid.Children.Contains(Platform_bt[i]))
                {
                    ParentGrid.Children.Remove(Platform_bt[i]);
                }
                i++;
            }
            Platform_bt = new List<Button>();
            D_bt_platform = new Dictionary<Button, Platform>();
            i = 0;
            while (i < x.Count)
            {
                Platform_bt.Add(new Button()
                {
                    BorderBrush = white_button_brush,
                    BorderThickness = new Thickness(3, 3, 3, 3),
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Content = "Platform" + x[i].ID.ToString(),
                    Foreground = white_button_brush,
                    FontSize = 18
                });
                ParentGrid.Children.Add(Platform_bt[Platform_bt.Count - 1]);
                Platform_bt[Platform_bt.Count - 1].SetValue(Grid.ColumnProperty, 112);
                Platform_bt[Platform_bt.Count - 1].SetValue(Grid.ColumnSpanProperty, 20);
                Platform_bt[Platform_bt.Count - 1].SetValue(Grid.RowProperty, row);
                Platform_bt[Platform_bt.Count - 1].SetValue(Grid.RowSpanProperty, 8);
                D_bt_platform[Platform_bt[Platform_bt.Count - 1]] = x[i];
                Platform_bt[Platform_bt.Count - 1].Click += Platform_bt_Click;
                row += 10;
                i++;
            }
        }

        private void Platform_bt_Click(object sender, RoutedEventArgs e)
        {
            Button yoo = sender as Button;
            Message_calibration_tb.Text = "";
            int i = 0;
            i = 0;
            while (i < D_bt_platform[yoo].Property_string.Count)
            {

                Message_calibration_tb.Text += "\r\n" + D_bt_platform[yoo].Property_string[i] + D_bt_platform[yoo].Property_value[i].ToString();
                i++;
            }
            if (yoo.BorderBrush == green_bright_button_brush)
            {
                yoo.BorderBrush = white_button_brush;
            }
            else
            {
                yoo.BorderBrush = green_bright_button_brush;
            }
        }

        ScrollViewer DRBE_SP_SV = new ScrollViewer();
        StackPanel DRBE_SP_SP = new StackPanel();
        TextBlock DRBE_tb_tt = new TextBlock();




        private void Setup()
        {
            DRBE_tb_tt = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Height = 100,
                Width = 100,
                Text = "Text"
            };

            DRBE_SP_SP = new StackPanel()
            {


            };
            DRBE_SP_SP.Children.Add(DRBE_tb_tt);

            DRBE_SP_SV = new ScrollViewer()
            {
                ZoomMode = ZoomMode.Enabled,
                MaxZoomFactor = 10,
                VerticalScrollBarVisibility = ScrollBarVisibility.Visible,
                Background = white_button_brush

            };

            DRBE_SP_SV.Content = DRBE_SP_SP;

            DRBE_SP_SV.SetValue(Grid.ColumnProperty, 15);
            DRBE_SP_SV.SetValue(Grid.ColumnSpanProperty, 25);
            DRBE_SP_SV.SetValue(Grid.RowProperty, 18);
            DRBE_SP_SV.SetValue(Grid.RowSpanProperty, 132);
            Canvas.SetZIndex(DRBE_SP_SV, 20);

            //ParentGrid.Children.Add(DRBE_SP_SV);

            #region transmitter
            Transmitter_bd = new Border()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.1, 0.1, 0.1, 0.1),
                Background = Transparent_brush
            };
            ParentGrid.Children.Add(Transmitter_bd);
            Transmitter_bd.SetValue(Grid.ColumnProperty, 60);
            Transmitter_bd.SetValue(Grid.ColumnSpanProperty, 25);
            Transmitter_bd.SetValue(Grid.RowProperty, 18);
            Transmitter_bd.SetValue(Grid.RowSpanProperty, 132);
            Canvas.SetZIndex(Transmitter_bd, -5);

            Transmitter_tb = new TextBlock()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 18,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Transmitter"
            };
            Transmitter_tb.SetValue(Grid.ColumnProperty, 60);
            Transmitter_tb.SetValue(Grid.ColumnSpanProperty, 25);
            Transmitter_tb.SetValue(Grid.RowProperty, 22);
            Transmitter_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Transmitter_tb);
            #endregion
            #region platform
            Platform_bd = new Border()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.1, 0.1, 0.1, 0.1),
                Background = Transparent_brush
            };
            ParentGrid.Children.Add(Platform_bd);
            Platform_bd.SetValue(Grid.ColumnProperty, 85);
            Platform_bd.SetValue(Grid.ColumnSpanProperty, 25);
            Platform_bd.SetValue(Grid.RowProperty, 18);
            Platform_bd.SetValue(Grid.RowSpanProperty, 132);
            Canvas.SetZIndex(Platform_bd, -5);


            Platform_tb = new TextBlock()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 18,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Platform"
            };
            Platform_tb.SetValue(Grid.ColumnProperty, 85);
            Platform_tb.SetValue(Grid.ColumnSpanProperty, 25);
            Platform_tb.SetValue(Grid.RowProperty, 22);
            Platform_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Platform_tb);
            #endregion
            #region receiver

            Receiver_bd = new Border()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.1, 0.1, 0.1, 0.1),
                Background = Transparent_brush
            };
            ParentGrid.Children.Add(Receiver_bd);
            Receiver_bd.SetValue(Grid.ColumnProperty, 110);
            Receiver_bd.SetValue(Grid.ColumnSpanProperty, 25);
            Receiver_bd.SetValue(Grid.RowProperty, 18);
            Receiver_bd.SetValue(Grid.RowSpanProperty, 132);
            Canvas.SetZIndex(Receiver_bd, -5);

            Receiver_tb = new TextBlock()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 18,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Receiver"
            };
            Receiver_tb.SetValue(Grid.ColumnProperty, 110);
            Receiver_tb.SetValue(Grid.ColumnSpanProperty, 25);
            Receiver_tb.SetValue(Grid.RowProperty, 22);
            Receiver_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Receiver_tb);
            #endregion
            #region tool
            Tool_bd = new Border()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.1, 0.1, 0.1, 0.1),
                Background = Transparent_brush
            };
            ParentGrid.Children.Add(Tool_bd);
            Tool_bd.SetValue(Grid.ColumnProperty, 135);
            Tool_bd.SetValue(Grid.ColumnSpanProperty, 25);
            Tool_bd.SetValue(Grid.RowProperty, 18);
            Tool_bd.SetValue(Grid.RowSpanProperty, 132);
            Canvas.SetZIndex(Tool_bd, -5);

            Tool_tb = new TextBlock()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 18,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Tool"
            };
            Tool_tb.SetValue(Grid.ColumnProperty, 135);
            Tool_tb.SetValue(Grid.ColumnSpanProperty, 25);
            Tool_tb.SetValue(Grid.RowProperty, 22);
            Tool_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Tool_tb);


            Read_scenario_bti.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Input.png", UriKind.RelativeOrAbsolute));
            Read_scenario_bt = new Button()
            {
                //BorderBrush = white_button_brush,
                //BorderThickness = new Thickness(3, 3, 3, 3),
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Read_scenario_bti,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Read_scenario_bt);
            Read_scenario_bt.SetValue(Grid.ColumnProperty, 137);
            Read_scenario_bt.SetValue(Grid.ColumnSpanProperty, 8);
            Read_scenario_bt.SetValue(Grid.RowProperty, 30);
            Read_scenario_bt.SetValue(Grid.RowSpanProperty, 10);
            Read_scenario_bt.Click += Read_scenario_bt_Click;

            Fidelity_simulation_bti.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Fidelity_Check.png", UriKind.RelativeOrAbsolute));
            Fidelity_simulation_bt = new Button()
            {
                //BorderBrush = white_button_brush,
                //BorderThickness = new Thickness(3, 3, 3, 3),
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Fidelity_simulation_bti,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Fidelity_simulation_bt);
            Fidelity_simulation_bt.SetValue(Grid.ColumnProperty, 137);
            Fidelity_simulation_bt.SetValue(Grid.ColumnSpanProperty, 8);
            Fidelity_simulation_bt.SetValue(Grid.RowProperty, 45);
            Fidelity_simulation_bt.SetValue(Grid.RowSpanProperty, 10);
            Fidelity_simulation_bt.Click += Fidelity_simulation_bt_Click;

            Show_link_bti.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Link.png", UriKind.RelativeOrAbsolute));
            Show_link_bt = new Button()
            {
                //BorderBrush = white_button_brush,
                //BorderThickness = new Thickness(3, 3, 3, 3),
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Show_link_bti,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Show_link_bt);
            Show_link_bt.SetValue(Grid.ColumnProperty, 147);
            Show_link_bt.SetValue(Grid.ColumnSpanProperty, 8);
            Show_link_bt.SetValue(Grid.RowProperty, 45);
            Show_link_bt.SetValue(Grid.RowSpanProperty, 10);
            Show_link_bt.Click += Fidelity_simulation_bt_Click;
            #endregion
            Message_bd = new Border()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.1, 0.1, 0.1, 0.1),
                Background = Transparent_brush
            };
            ParentGrid.Children.Add(Message_bd);
            Message_bd.SetValue(Grid.ColumnProperty, 160);
            Message_bd.SetValue(Grid.ColumnSpanProperty, 40);
            Message_bd.SetValue(Grid.RowProperty, 18);
            Message_bd.SetValue(Grid.RowSpanProperty, 132);
            Canvas.SetZIndex(Message_bd, -5);


            Message_tb = new TextBlock()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 18,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Message"
            };
            Message_tb.SetValue(Grid.ColumnProperty, 160);
            Message_tb.SetValue(Grid.ColumnSpanProperty, 40);
            Message_tb.SetValue(Grid.RowProperty, 22);
            Message_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Message_tb);

            Message_calibration_tb = new TextBlock()
            {
                HorizontalTextAlignment = TextAlignment.Left,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 18,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Result"
            };
            Message_calibration_tb.SetValue(Grid.ColumnProperty, 160);
            Message_calibration_tb.SetValue(Grid.ColumnSpanProperty, 40);
            Message_calibration_tb.SetValue(Grid.RowProperty, 30);
            Message_calibration_tb.SetValue(Grid.RowSpanProperty, 120);
            ParentGrid.Children.Add(Message_calibration_tb);
        }
        private MatlabPacket mpacket = new MatlabPacket();
        private List<byte> testpacket = new List<byte>();
        private void Fidelity_simulation_bt_Click(object sender, RoutedEventArgs e)
        {
            if (UI_Stream_ready_flag)
            {
                testpacket = new List<byte>(mpacket.HC_APO4 (new List<byte>()));
                UWbinarywriter.Write(testpacket.ToArray(), 0, 255);
                UWbinarywriter.Flush();
            }
        }

        private DRBEScenario DRBE_Scenario;
        public async void Read_scenario_bt_Click(object sender, RoutedEventArgs e)
        {
            DRBE_Scenario = new DRBEScenario(ParentGrid);
            await DRBE_Scenario.Parse_Scenario("Scenario1.txt");
            Create_Platform(DRBE_Scenario.platform);
            Create_Transmitter(DRBE_Scenario.transmitter);
            Create_Receiver(DRBE_Scenario.receiver);
        }

        public void Show()
        {
            DRBE_tb_tt.Visibility = Visibility.Visible;

            DRBE_SP_SP.Visibility = Visibility.Visible;

            DRBE_SP_SV.Visibility = Visibility.Visible;

            //ParentGrid.Children.Add(DRBE_SP_SV);

            #region transmitter
            Transmitter_bd.Visibility = Visibility.Visible;

            Transmitter_tb.Visibility = Visibility.Visible;
            #endregion
            #region platform
            Platform_bd.Visibility = Visibility.Visible;


            Platform_tb.Visibility = Visibility.Visible;
            #endregion
            #region receiver

            Receiver_bd.Visibility = Visibility.Visible;

            Receiver_tb.Visibility = Visibility.Visible;
            #endregion
            #region tool
            Tool_bd.Visibility = Visibility.Visible;

            Tool_tb.Visibility = Visibility.Visible;
            Read_scenario_bt.Visibility = Visibility.Visible;
            Fidelity_simulation_bt.Visibility = Visibility.Visible;
            Show_link_bt.Visibility = Visibility.Visible;
            #endregion
            Message_bd.Visibility = Visibility.Visible;


            Message_tb.Visibility = Visibility.Visible;

            Message_calibration_tb.Visibility = Visibility.Visible;
        }
        public void Hide()
        {
            DRBE_tb_tt.Visibility = Visibility.Collapsed;

            DRBE_SP_SP.Visibility = Visibility.Collapsed;

            DRBE_SP_SV.Visibility = Visibility.Collapsed;

            //ParentGrid.Children.Add(DRBE_SP_SV);

            #region transmitter
            Transmitter_bd.Visibility = Visibility.Collapsed;

            Transmitter_tb.Visibility = Visibility.Collapsed;
            #endregion
            #region platform
            Platform_bd.Visibility = Visibility.Collapsed;


            Platform_tb.Visibility = Visibility.Collapsed;
            #endregion
            #region receiver

            Receiver_bd.Visibility = Visibility.Collapsed;

            Receiver_tb.Visibility = Visibility.Collapsed;
            #endregion
            #region tool
            Tool_bd.Visibility = Visibility.Collapsed;

            Tool_tb.Visibility = Visibility.Collapsed;
            Read_scenario_bt.Visibility = Visibility.Collapsed;
            Fidelity_simulation_bt.Visibility = Visibility.Collapsed;
            Show_link_bt.Visibility = Visibility.Collapsed;
            #endregion
            Message_bd.Visibility = Visibility.Collapsed;


            Message_tb.Visibility = Visibility.Collapsed;

            Message_calibration_tb.Visibility = Visibility.Collapsed;
        }
    }
}
