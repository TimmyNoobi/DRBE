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
    public class Sim_sweep
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
        private Save_Screen DRBE_SS;
        DRBE_Link_Viewer_s SC_Dlv;

        private Simple_DRBE_Plotter sdp;
        private MainPage ParentPage;
        public Sim_sweep(Grid parent, MainPage parentpage)
        {
            ParentGrid = parent;
            ParentPage = parentpage;
            //hide();
            DRBE_SS = new Save_Screen(parent);
            SC_Dlv = new DRBE_Link_Viewer_s(parent, ParentPage);
            Setup();
            sdp = new Simple_DRBE_Plotter(ParentGrid, 80, 120, 30, 120, 0);

            Calculate_position(0, 0, 0);
            Calculate_position(10, -16, 5);
            Calculate_position(10, 14, 5);
            Calculate_position(-17, 17, 5);
            Calculate_position(10, -21, 5);
        }

        private int ID_index = 0;


        private int Number_of_Transmitter = 1;
        private int Number_of_Reflector = 1;
        private int Number_of_Receiver = 1;

        public List<DRBE_Transmitter> Sc_transmitter_list = new List<DRBE_Transmitter>();
        public List<DRBE_Receiver> Sc_receiver_list = new List<DRBE_Receiver>();
        public List<DRBE_Reflector> Sc_reflector_list = new List<DRBE_Reflector>();
        public List<List<List<bool>>> Ss_enable_list = new List<List<List<bool>>>();

        public List<bool> Ss_tena_list = new List<bool>();
        public List<bool> Ss_pena_list = new List<bool>();
        public List<bool> Ss_rena_list = new List<bool>();

        private void Construct_metadata_enable_list()
        {
            int i = 0;
            int ii = 0;
            int iii = 0;

            i = 0;
            while(i< Ss_enable_list.Count)
            {
                Ss_tena_list.Add(false);
                i++;
            }
            i = 0;
            while (i < Ss_enable_list[0].Count)
            {
                Ss_pena_list.Add(false);
                i++;
            }
            i = 0;
            while (i < Ss_enable_list[0][0].Count)
            {
                Ss_rena_list.Add(false);
                i++;
            }
            i = 0;
            while(i< Ss_enable_list.Count)
            {
                ii = 0;
                while(ii < Ss_enable_list[i].Count)
                {
                    iii = 0;
                    while(iii < Ss_enable_list[i][ii].Count)
                    {
                        if(Ss_enable_list[i][ii][iii])
                        {
                            Ss_tena_list[i] = true;
                            Ss_pena_list[ii] = true;
                            Ss_rena_list[iii] = true;
                        }
                        iii++;
                    }
                    ii++;
                }
                i++;
            }
        }

        private TextBlock Object_ID_tb = new TextBlock();

        private TextBlock PositionX_ttb = new TextBlock();
        private TextBox PositionX_tb = new TextBox();

        private TextBlock PositionY_ttb = new TextBlock();
        private TextBox PositionY_tb = new TextBox();

        private TextBlock PositionZ_ttb = new TextBlock();
        private TextBox PositionZ_tb = new TextBox();

        private TextBlock VelocityX_ttb = new TextBlock();
        private TextBox VelocityX_tb = new TextBox();

        private TextBlock VelocityY_ttb = new TextBlock();
        private TextBox VelocityY_tb = new TextBox();

        private TextBlock VelocityZ_ttb = new TextBlock();
        private TextBox VelocityZ_tb = new TextBox();

        private TextBlock AccelerationX_ttb = new TextBlock();
        private TextBox AccelerationX_tb = new TextBox();

        private TextBlock AccelerationY_ttb = new TextBlock();
        private TextBox AccelerationY_tb = new TextBox();

        private TextBlock AccelerationZ_ttb = new TextBlock();
        private TextBox AccelerationZ_tb = new TextBox();

        private TextBlock OrientationX_ttb = new TextBlock();
        private TextBox OrientationX_tb = new TextBox();

        private TextBlock OrientationY_ttb = new TextBlock();
        private TextBox OrientationY_tb = new TextBox();

        private TextBlock OrientationZ_ttb = new TextBlock();
        private TextBox OrientationZ_tb = new TextBox();

        private Slider Time_line_sd = new Slider();

        private Button Time_left_bt = new Button();
        private Image Time_left_i = new Image();

        private Button Time_right_bt = new Button();
        private Image Time_right_i = new Image();

        private TextBlock Time_info_tb = new TextBlock();
        private TextBlock Time_info_ttb = new TextBlock();

        private TextBlock Initial_info_tb = new TextBlock();

        private TextBlock Transmitter_title_tb = new TextBlock();
        private TextBlock Reflector_title_tb = new TextBlock();
        private TextBlock Receiver_title_tb = new TextBlock();

        private TextBlock Report_tb = new TextBlock();
        private Button Report_bt = new Button();
        private Image Report_i = new Image();

        private TextBlock Save_tb = new TextBlock();
        private Button Save_bt = new Button();
        private Image Save_i = new Image();


        private TextBlock Position_sum_tb = new TextBlock();
        private TextBlock Velocity_sum_tb = new TextBlock();
        private TextBlock Orientation_sum_tb = new TextBlock();
        private TextBlock ESteering_sum_tb = new TextBlock();
        private TextBlock MSteering_sum_tb = new TextBlock();

        public void Setup()
        {


            Initial_info_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Info",
                Foreground = white_button_brush,
                FontSize = 18,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Initial_info_tb);
            Initial_info_tb.SetValue(Grid.ColumnProperty, 35);
            Initial_info_tb.SetValue(Grid.ColumnSpanProperty, 60);
            Initial_info_tb.SetValue(Grid.RowProperty, 25);
            Initial_info_tb.SetValue(Grid.RowSpanProperty, 125);

            #region Save scenario
            Save_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Save Update",
                Foreground = white_button_brush,
                FontSize = 18,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Save_tb);
            Save_tb.SetValue(Grid.ColumnProperty, 0);
            Save_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Save_tb.SetValue(Grid.RowProperty, 0);
            Save_tb.SetValue(Grid.RowSpanProperty, 5);

            Save_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Save_icon.png", UriKind.RelativeOrAbsolute));
            Save_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Save_i,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Save_bt);
            Save_bt.SetValue(Grid.ColumnProperty, 0);
            Save_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Save_bt.SetValue(Grid.RowProperty, 5);
            Save_bt.SetValue(Grid.RowSpanProperty, 10);
            Save_bt.Click += Save_bt_Click;
            #endregion

            #region Report scenario
            Report_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Report Update",
                Foreground = white_button_brush,
                FontSize = 18,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Report_tb);
            Report_tb.SetValue(Grid.ColumnProperty, 20);
            Report_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Report_tb.SetValue(Grid.RowProperty, 0);
            Report_tb.SetValue(Grid.RowSpanProperty, 5);

            Report_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Report_icon.png", UriKind.RelativeOrAbsolute));
            Report_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Report_i,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Report_bt);
            Report_bt.SetValue(Grid.ColumnProperty, 20);
            Report_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Report_bt.SetValue(Grid.RowProperty, 5);
            Report_bt.SetValue(Grid.RowSpanProperty, 10);
            Report_bt.Click += Report_bt_Click;
            #endregion

            Transmitter_title_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Transmitter",
                Foreground = white_button_brush,
                FontSize = 18,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Transmitter_title_tb);
            Transmitter_title_tb.SetValue(Grid.ColumnProperty, 0);
            Transmitter_title_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Transmitter_title_tb.SetValue(Grid.RowProperty, 20);
            Transmitter_title_tb.SetValue(Grid.RowSpanProperty, 5);

            Reflector_title_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Platform",
                Foreground = white_button_brush,
                FontSize = 18,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Reflector_title_tb);
            Reflector_title_tb.SetValue(Grid.ColumnProperty, 15);
            Reflector_title_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Reflector_title_tb.SetValue(Grid.RowProperty, 20);
            Reflector_title_tb.SetValue(Grid.RowSpanProperty, 5);

            Receiver_title_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Receiver",
                Foreground = white_button_brush,
                FontSize = 18,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Receiver_title_tb);
            Receiver_title_tb.SetValue(Grid.ColumnProperty, 30);
            Receiver_title_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Receiver_title_tb.SetValue(Grid.RowProperty, 20);
            Receiver_title_tb.SetValue(Grid.RowSpanProperty, 5);

            Initial_Property_show();


        }

        private async void Report_bt_Click(object sender, RoutedEventArgs e)
        {
            await ShowDialog("Meta-data Update Report", Generate_metadata());

        }

        private async void Save_bt_Click(object sender, RoutedEventArgs e)
        {
            await DRBE_SS.Start("Save Object: Meta-data Update", new List<string>() { "Simulator File", "Meta-data File" }, "dmd", Generate_metadata());

        }

        public void hide()
        {

            #region Save scenario
            Report_tb.Visibility = Visibility.Collapsed;
            Report_bt.Visibility = Visibility.Collapsed;
            #endregion



        }
        public void show()
        {

            #region Save scenario
            Report_tb.Visibility = Visibility.Visible;
            Report_bt.Visibility = Visibility.Visible;
            #endregion



        }


        private int Meta_Time_scale = 1000;
        private int Initial_time = 0;
        private int Final_time = 1;
        private string Generate_metadata()
        {
            string result = "";
            int total_stamp = (Final_time - Initial_time) * Meta_Time_scale;
            int i = 0;
            int ii = 0;
            i = 0;
            while(i< total_stamp)
            {
                result += "{T} \r\n Timestamp: {" + (i*1000000).ToString() + "}";
                ii = 0;
                while(ii< Sc_transmitter_list.Count)
                {

                    if(Ss_tena_list[ii])
                    {
                        result += " , Object ID: {" + Sc_transmitter_list[ii].ID.ToString() + "} , ";
                        result += "Position X: {" + Sc_transmitter_list[ii].Initial_Position_X.ToString() + "} , ";
                        result += "Position Y: {" + Sc_transmitter_list[ii].Initial_Position_Y.ToString() + "} , ";
                        result += "Position Z: {" + Sc_transmitter_list[ii].Initial_Position_Z.ToString() + "} , ";

                        result += "Velocity X: {" + Sc_transmitter_list[ii].Initial_Velocity_X.ToString() + "} , ";
                        result += "Velocity Y: {" + Sc_transmitter_list[ii].Initial_Velocity_Y.ToString() + "} , ";
                        result += "Velocity Z: {" + Sc_transmitter_list[ii].Initial_Velocity_Z.ToString() + "} , ";

                        result += "Acceleration X: {" + Sc_transmitter_list[ii].Initial_Acceleration_X.ToString() + "} , ";
                        result += "Acceleration Y: {" + Sc_transmitter_list[ii].Initial_Acceleration_Y.ToString() + "} , ";
                        result += "Acceleration Z: {" + Sc_transmitter_list[ii].Initial_Acceleration_Z.ToString() + "} , ";

                        result += "Pitch: {" + Sc_transmitter_list[ii].Initial_Orientation_X.ToString() + "} , ";
                        result += "Roll Y: {" + Sc_transmitter_list[ii].Initial_Orientation_Y.ToString() + "} , ";
                        result += "Yaw Z: {" + Sc_transmitter_list[ii].Initial_Orientation_Z.ToString() + "} , ";

                        result += "Mechanical Steering Angle Azimuth: {" + Sc_transmitter_list[ii].Initial_Mangle_AZ.ToString() + "} , ";
                        result += "Mechanical Steering Angle Elevation: {" + Sc_transmitter_list[ii].Initial_Mangle_EL.ToString() + "} , ";

                        result += "Eechanical Steering Angle Azimuth: {" + Sc_transmitter_list[ii].Initial_Eangle_AZ.ToString() + "} , ";
                        result += "Eechanical Steering Angle Elevation: {" + Sc_transmitter_list[ii].Initial_Eangle_EL.ToString() + "}";
                    }
                    ii++;
                }
                result += " | \r\n";

                i++;
            }


            return result;
        }

        public async void Property_setup( List<DRBE_Transmitter> x, List<DRBE_Reflector> y, List<DRBE_Receiver> z, List<List<List<bool>>> ena)
        {
            Sc_transmitter_list = x;
            Sc_reflector_list = y;
            Sc_receiver_list = z;
            Ss_enable_list = ena;

            Transmitter_setup();
            Reflector_setup();
            Receiver_setup();

            Time_info_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Time Stamp: ",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Time_info_ttb);
            Time_info_ttb.SetValue(Grid.ColumnProperty, 110);
            Time_info_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            Time_info_ttb.SetValue(Grid.RowProperty, 25);
            Time_info_ttb.SetValue(Grid.RowSpanProperty, 10);

            Time_info_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "0",
                Foreground = white_button_brush,
                FontSize = 18,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Time_info_tb);
            Time_info_tb.SetValue(Grid.ColumnProperty, 130);
            Time_info_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Time_info_tb.SetValue(Grid.RowProperty, 25);
            Time_info_tb.SetValue(Grid.RowSpanProperty, 10);

            Time_line_sd = new Slider() { 
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Maximum = 10000,
                Minimum = 0,
                Foreground = Light_blue_brush,
                Background = Light_blue_brush,
                ManipulationMode = ManipulationModes.All
                
            
            };
            Time_line_sd.ManipulationCompleted += Time_line_sd_ManipulationCompleted;
            //Time_line_sd.m
            Time_line_sd.ValueChanged += Time_line_sd_ValueChanged;
            ParentGrid.Children.Add(Time_line_sd);
            Time_line_sd.SetValue(Grid.ColumnProperty, 75);
            Time_line_sd.SetValue(Grid.ColumnSpanProperty, 125);
            Time_line_sd.SetValue(Grid.RowProperty, 15);
            Time_line_sd.SetValue(Grid.RowSpanProperty, 5);

            Position_sum_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Position: \r\n [100, 200, 400]",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Position_sum_tb);
            Position_sum_tb.SetValue(Grid.ColumnProperty, 80);
            Position_sum_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Position_sum_tb.SetValue(Grid.RowProperty, 5);
            Position_sum_tb.SetValue(Grid.RowSpanProperty, 10);


            Velocity_sum_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Velocity: \r\n [100, 200, 400]",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Velocity_sum_tb);
            Velocity_sum_tb.SetValue(Grid.ColumnProperty, 100);
            Velocity_sum_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Velocity_sum_tb.SetValue(Grid.RowProperty, 5);
            Velocity_sum_tb.SetValue(Grid.RowSpanProperty, 10);

            Orientation_sum_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Orientation: \r\n [10, 20, 40]",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Orientation_sum_tb);
            Orientation_sum_tb.SetValue(Grid.ColumnProperty, 120);
            Orientation_sum_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Orientation_sum_tb.SetValue(Grid.RowProperty, 5);
            Orientation_sum_tb.SetValue(Grid.RowSpanProperty, 10);

            ESteering_sum_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Radar Pointing Angle: \r\n [50, 30]",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(ESteering_sum_tb);
            ESteering_sum_tb.SetValue(Grid.ColumnProperty, 140);
            ESteering_sum_tb.SetValue(Grid.ColumnSpanProperty, 20);
            ESteering_sum_tb.SetValue(Grid.RowProperty, 5);
            ESteering_sum_tb.SetValue(Grid.RowSpanProperty, 10);
            #region Position X
            PositionX_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(PositionX_tb);
            PositionX_tb.SetValue(Grid.ColumnProperty, 100);
            PositionX_tb.SetValue(Grid.ColumnSpanProperty, 15);
            PositionX_tb.SetValue(Grid.RowProperty, 55);
            PositionX_tb.SetValue(Grid.RowSpanProperty, 5);

            PositionX_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Position X",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(PositionX_ttb);
            PositionX_ttb.SetValue(Grid.ColumnProperty, 100);
            PositionX_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            PositionX_ttb.SetValue(Grid.RowProperty, 50);
            PositionX_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion
            #region Position Y
            PositionY_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(PositionY_tb);
            PositionY_tb.SetValue(Grid.ColumnProperty, 115);
            PositionY_tb.SetValue(Grid.ColumnSpanProperty, 15);
            PositionY_tb.SetValue(Grid.RowProperty, 55);
            PositionY_tb.SetValue(Grid.RowSpanProperty, 5);

            PositionY_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Position Y",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(PositionY_ttb);
            PositionY_ttb.SetValue(Grid.ColumnProperty, 115);
            PositionY_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            PositionY_ttb.SetValue(Grid.RowProperty, 50);
            PositionY_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion
            #region Position Z
            PositionZ_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(PositionZ_tb);
            PositionZ_tb.SetValue(Grid.ColumnProperty, 130);
            PositionZ_tb.SetValue(Grid.ColumnSpanProperty, 15);
            PositionZ_tb.SetValue(Grid.RowProperty, 55);
            PositionZ_tb.SetValue(Grid.RowSpanProperty, 5);

            PositionZ_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Position Z",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(PositionZ_ttb);
            PositionZ_ttb.SetValue(Grid.ColumnProperty, 130);
            PositionZ_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            PositionZ_ttb.SetValue(Grid.RowProperty, 50);
            PositionZ_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

            #region Velocity X
            VelocityX_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(VelocityX_tb);
            VelocityX_tb.SetValue(Grid.ColumnProperty, 100);
            VelocityX_tb.SetValue(Grid.ColumnSpanProperty, 15);
            VelocityX_tb.SetValue(Grid.RowProperty, 75);
            VelocityX_tb.SetValue(Grid.RowSpanProperty, 5);

            VelocityX_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Velocity X",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(VelocityX_ttb);
            VelocityX_ttb.SetValue(Grid.ColumnProperty, 100);
            VelocityX_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            VelocityX_ttb.SetValue(Grid.RowProperty, 70);
            VelocityX_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion
            #region Velocity Y
            VelocityY_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(VelocityY_tb);
            VelocityY_tb.SetValue(Grid.ColumnProperty, 115);
            VelocityY_tb.SetValue(Grid.ColumnSpanProperty, 15);
            VelocityY_tb.SetValue(Grid.RowProperty, 75);
            VelocityY_tb.SetValue(Grid.RowSpanProperty, 5);

            VelocityY_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Velocity Y",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(VelocityY_ttb);
            VelocityY_ttb.SetValue(Grid.ColumnProperty, 115);
            VelocityY_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            VelocityY_ttb.SetValue(Grid.RowProperty, 70);
            VelocityY_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion
            #region Velocity Z
            VelocityZ_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(VelocityZ_tb);
            VelocityZ_tb.SetValue(Grid.ColumnProperty, 130);
            VelocityZ_tb.SetValue(Grid.ColumnSpanProperty, 15);
            VelocityZ_tb.SetValue(Grid.RowProperty, 75);
            VelocityZ_tb.SetValue(Grid.RowSpanProperty, 5);

            VelocityZ_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Velocity Z",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(VelocityZ_ttb);
            VelocityZ_ttb.SetValue(Grid.ColumnProperty, 130);
            VelocityZ_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            VelocityZ_ttb.SetValue(Grid.RowProperty, 70);
            VelocityZ_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

            #region Acceleration X
            AccelerationX_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(AccelerationX_tb);
            AccelerationX_tb.SetValue(Grid.ColumnProperty, 60);
            AccelerationX_tb.SetValue(Grid.ColumnSpanProperty, 15);
            AccelerationX_tb.SetValue(Grid.RowProperty, 75);
            AccelerationX_tb.SetValue(Grid.RowSpanProperty, 5);

            AccelerationX_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Acceleration X",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(AccelerationX_ttb);
            AccelerationX_ttb.SetValue(Grid.ColumnProperty, 60);
            AccelerationX_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            AccelerationX_ttb.SetValue(Grid.RowProperty, 70);
            AccelerationX_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion
            #region Acceleration Y
            AccelerationY_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(AccelerationY_tb);
            AccelerationY_tb.SetValue(Grid.ColumnProperty, 60);
            AccelerationY_tb.SetValue(Grid.ColumnSpanProperty, 15);
            AccelerationY_tb.SetValue(Grid.RowProperty, 85);
            AccelerationY_tb.SetValue(Grid.RowSpanProperty, 5);

            AccelerationY_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Acceleration Y",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(AccelerationY_ttb);
            AccelerationY_ttb.SetValue(Grid.ColumnProperty, 60);
            AccelerationY_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            AccelerationY_ttb.SetValue(Grid.RowProperty, 80);
            AccelerationY_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion
            #region Acceleration Z
            AccelerationZ_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(AccelerationZ_tb);
            AccelerationZ_tb.SetValue(Grid.ColumnProperty, 60);
            AccelerationZ_tb.SetValue(Grid.ColumnSpanProperty, 15);
            AccelerationZ_tb.SetValue(Grid.RowProperty, 95);
            AccelerationZ_tb.SetValue(Grid.RowSpanProperty, 5);

            AccelerationZ_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Acceleration Z",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(AccelerationZ_ttb);
            AccelerationZ_ttb.SetValue(Grid.ColumnProperty, 60);
            AccelerationZ_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            AccelerationZ_ttb.SetValue(Grid.RowProperty, 90);
            AccelerationZ_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

            #region Orientation X
            OrientationX_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(OrientationX_tb);
            OrientationX_tb.SetValue(Grid.ColumnProperty, 60);
            OrientationX_tb.SetValue(Grid.ColumnSpanProperty, 15);
            OrientationX_tb.SetValue(Grid.RowProperty, 35);
            OrientationX_tb.SetValue(Grid.RowSpanProperty, 5);

            OrientationX_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Pitch",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(OrientationX_ttb);
            OrientationX_ttb.SetValue(Grid.ColumnProperty, 60);
            OrientationX_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            OrientationX_ttb.SetValue(Grid.RowProperty, 30);
            OrientationX_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion
            #region Orientation Y
            OrientationY_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(OrientationY_tb);
            OrientationY_tb.SetValue(Grid.ColumnProperty, 60);
            OrientationY_tb.SetValue(Grid.ColumnSpanProperty, 15);
            OrientationY_tb.SetValue(Grid.RowProperty, 45);
            OrientationY_tb.SetValue(Grid.RowSpanProperty, 5);

            OrientationY_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Roll",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(OrientationY_ttb);
            OrientationY_ttb.SetValue(Grid.ColumnProperty, 60);
            OrientationY_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            OrientationY_ttb.SetValue(Grid.RowProperty, 40);
            OrientationY_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion
            #region Orientation Z
            OrientationZ_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(OrientationZ_tb);
            OrientationZ_tb.SetValue(Grid.ColumnProperty, 60);
            OrientationZ_tb.SetValue(Grid.ColumnSpanProperty, 15);
            OrientationZ_tb.SetValue(Grid.RowProperty, 55);
            OrientationZ_tb.SetValue(Grid.RowSpanProperty, 5);

            OrientationZ_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Yaw",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(OrientationZ_ttb);
            OrientationZ_ttb.SetValue(Grid.ColumnProperty, 60);
            OrientationZ_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            OrientationZ_ttb.SetValue(Grid.RowProperty, 50);
            OrientationZ_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion
            Construct_metadata_enable_list();


        }

        private void Time_line_sd_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Time_info_tb.Text = Time_line_sd.Value.ToString();
        }

        private async void Time_line_sd_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
           // await ShowDialog(Time_line_sd.Value.ToString(), "ok");
        }
        private List<double> Posx_L = new List<double>();
        private List<double> Posy_L = new List<double>();

        private double o1x = 40;
        private double o2x = 20;

        private double o1y = -50;
        private double o2y = 50;


        private void Calculate_position(double accx, double accy, int t)
        {
            int i = 0;
            i = 0;
            Posx_L = new List<double>();
            Posy_L = new List<double>();

            o1x = o1x + t * accx;
            o1y = o1y + t * accy;

            o2x = o2x + t * accx;
            o2y = o2y + t * accy;


            Posx_L.Add(o1x);
            Posy_L.Add(o1y);

            Posx_L.Add(o2x);
            Posy_L.Add(o2y);


            sdp.Plot_object(Posx_L,Posy_L,t);

        }

        public void Property_hide()
        {

            #region Position X
            PositionX_tb.Visibility = Visibility.Collapsed;

            PositionX_ttb.Visibility = Visibility.Collapsed;
            #endregion
            #region Position Y
            PositionY_tb.Visibility = Visibility.Collapsed;

            PositionY_ttb.Visibility = Visibility.Collapsed;
            #endregion
            #region Position Z
            PositionZ_tb.Visibility = Visibility.Collapsed;

            PositionZ_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region Velocity X
            VelocityX_tb.Visibility = Visibility.Collapsed;

            VelocityX_ttb.Visibility = Visibility.Collapsed;
            #endregion
            #region Velocity Y
            VelocityY_tb.Visibility = Visibility.Collapsed;

            VelocityY_ttb.Visibility = Visibility.Collapsed;
            #endregion
            #region Velocity Z
            VelocityZ_tb.Visibility = Visibility.Collapsed;

            VelocityZ_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region Acceleration X
            AccelerationX_tb.Visibility = Visibility.Collapsed;

            AccelerationX_ttb.Visibility = Visibility.Collapsed;
            #endregion
            #region Acceleration Y
            AccelerationY_tb.Visibility = Visibility.Collapsed;

            AccelerationY_ttb.Visibility = Visibility.Collapsed;
            #endregion
            #region Acceleration Z
            AccelerationZ_tb.Visibility = Visibility.Collapsed;

            AccelerationZ_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region Orientation X
            OrientationX_tb.Visibility = Visibility.Collapsed;

            OrientationX_ttb.Visibility = Visibility.Collapsed;
            #endregion
            #region Orientation Y
            OrientationY_tb.Visibility = Visibility.Collapsed;

            OrientationY_ttb.Visibility = Visibility.Collapsed;
            #endregion
            #region Orientation Z
            OrientationZ_tb.Visibility = Visibility.Collapsed;

            OrientationZ_ttb.Visibility = Visibility.Collapsed;
            #endregion



        }
        public void Initial_Property_show()
        {



            #region Position X
            PositionX_tb.Visibility = Visibility.Visible;

            PositionX_ttb.Visibility = Visibility.Visible;
            #endregion
            #region Position Y
            PositionY_tb.Visibility = Visibility.Visible;

            PositionY_ttb.Visibility = Visibility.Visible;
            #endregion
            #region Position Z
            PositionZ_tb.Visibility = Visibility.Visible;

            PositionZ_ttb.Visibility = Visibility.Visible;
            #endregion

            #region Velocity X
            VelocityX_tb.Visibility = Visibility.Visible;

            VelocityX_ttb.Visibility = Visibility.Visible;
            #endregion
            #region Velocity Y
            VelocityY_tb.Visibility = Visibility.Visible;

            VelocityY_ttb.Visibility = Visibility.Visible;
            #endregion
            #region Velocity Z
            VelocityZ_tb.Visibility = Visibility.Visible;

            VelocityZ_ttb.Visibility = Visibility.Visible;
            #endregion

            #region Acceleration X
            AccelerationX_tb.Visibility = Visibility.Visible;

            AccelerationX_ttb.Visibility = Visibility.Visible;
            #endregion
            #region Acceleration Y
            AccelerationY_tb.Visibility = Visibility.Visible;

            AccelerationY_ttb.Visibility = Visibility.Visible;
            #endregion
            #region Acceleration Z
            AccelerationZ_tb.Visibility = Visibility.Visible;

            AccelerationZ_ttb.Visibility = Visibility.Visible;
            #endregion

            #region Orientation X
            OrientationX_tb.Visibility = Visibility.Visible;

            OrientationX_ttb.Visibility = Visibility.Visible;
            #endregion
            #region Orientation Y
            OrientationY_tb.Visibility = Visibility.Visible;

            OrientationY_ttb.Visibility = Visibility.Visible;
            #endregion
            #region Orientation Z
            OrientationZ_tb.Visibility = Visibility.Visible;

            OrientationZ_ttb.Visibility = Visibility.Visible;
            #endregion

        }


        private Button Global_temp_bt;

        private List<Button> Transmitter_btl = new List<Button>();
        public Dictionary<Button, DRBE_Transmitter> Dic_bt_drbet = new Dictionary<Button, DRBE_Transmitter>();

        private List<Button> Receiver_btl = new List<Button>();
        public Dictionary<Button, DRBE_Receiver> Dic_bt_drber = new Dictionary<Button, DRBE_Receiver>();

        private List<Button> Reflector_btl = new List<Button>();
        public Dictionary<Button, DRBE_Reflector> Dic_bt_drbef = new Dictionary<Button, DRBE_Reflector>();

        public void Transmitter_setup()
        {
            TRF_bt_decolor();
            Dic_bt_drbet = new Dictionary<Button, DRBE_Transmitter>();

            int i = 0;
            i = 0;
            while (i < Transmitter_btl.Count)
            {
                if (ParentGrid.Children.Contains(Transmitter_btl[i]))
                {
                    ParentGrid.Children.Remove(Transmitter_btl[i]);
                }
                i++;
            }
            Transmitter_btl = new List<Button>();
            i = 0;
            while (i < Sc_transmitter_list.Count)
            {
                Transmitter_btl.Add(new Button()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    Background = Default_back_black_color_brush,
                    Content = "Tran:  " + Sc_transmitter_list[i].ID.ToString(),
                    Foreground = white_button_brush,
                    FontWeight = FontWeights.Bold,
                    BorderBrush = white_button_brush,
                    BorderThickness = new Thickness(2, 2, 2, 2),
                    FontSize = 18
                });
                ParentGrid.Children.Add(Transmitter_btl[Transmitter_btl.Count - 1]);
                Transmitter_btl[Transmitter_btl.Count - 1].SetValue(Grid.ColumnProperty, 0);
                Transmitter_btl[Transmitter_btl.Count - 1].SetValue(Grid.ColumnSpanProperty, 15);
                Transmitter_btl[Transmitter_btl.Count - 1].SetValue(Grid.RowProperty, 25 + i * 5);
                Transmitter_btl[Transmitter_btl.Count - 1].SetValue(Grid.RowSpanProperty, 5);
                Dic_bt_drbet[Transmitter_btl[Transmitter_btl.Count - 1]] = Sc_transmitter_list[i];
                Transmitter_btl[Transmitter_btl.Count - 1].Click += Transmitter_bt_Click;
                i++;
            }

            Transmitter_btl[Transmitter_btl.Count - 1].BorderBrush = green_bright_button_brush;

            Object_ID_tb.Text = "Object ID:  " + Dic_bt_drbet[Transmitter_btl[Transmitter_btl.Count - 1]].ID.ToString();
            Global_temp_bt = Transmitter_btl[Transmitter_btl.Count - 1];
        }

        private void TRF_bt_decolor()
        {
            int i = 0;
            i = 0;
            while (i < Transmitter_btl.Count)
            {
                Transmitter_btl[i].BorderBrush = white_button_brush;
                i++;
            }

            i = 0;
            while (i < Reflector_btl.Count)
            {
                Reflector_btl[i].BorderBrush = white_button_brush;
                i++;
            }

            i = 0;
            while (i < Receiver_btl.Count)
            {
                Receiver_btl[i].BorderBrush = white_button_brush;
                i++;
            }
        }

        private async void Transmitter_bt_Click(object sender, RoutedEventArgs e)
        {
            Button foo = sender as Button;
            TRF_bt_decolor();
            foo.BorderBrush = green_bright_button_brush;
            Object_ID_tb.Text = "Object ID:  " + Dic_bt_drbet[foo].ID.ToString();
            Global_temp_bt = foo;
            Initial_info_tb.Text = Dic_bt_drbet[foo].Summary();
        }
        public void Reflector_setup()
        {
            TRF_bt_decolor();
            Dic_bt_drbef = new Dictionary<Button, DRBE_Reflector>();

            int i = 0;
            i = 0;
            while (i < Reflector_btl.Count)
            {
                if (ParentGrid.Children.Contains(Reflector_btl[i]))
                {
                    ParentGrid.Children.Remove(Reflector_btl[i]);
                }
                i++;
            }






            Reflector_btl = new List<Button>();
            i = 0;
            while (i < Sc_reflector_list.Count)
            {
                Reflector_btl.Add(new Button()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    Background = Default_back_black_color_brush,
                    Content = "Plat:  " + Sc_reflector_list[i].ID.ToString(),
                    Foreground = white_button_brush,
                    FontWeight = FontWeights.Bold,
                    BorderBrush = white_button_brush,
                    BorderThickness = new Thickness(2, 2, 2, 2),
                    FontSize = 18
                });
                ParentGrid.Children.Add(Reflector_btl[Reflector_btl.Count - 1]);
                Reflector_btl[Reflector_btl.Count - 1].SetValue(Grid.ColumnProperty, 15);
                Reflector_btl[Reflector_btl.Count - 1].SetValue(Grid.ColumnSpanProperty, 15);
                Reflector_btl[Reflector_btl.Count - 1].SetValue(Grid.RowProperty, 25 + i * 5);
                Reflector_btl[Reflector_btl.Count - 1].SetValue(Grid.RowSpanProperty, 5);
                Dic_bt_drbef[Reflector_btl[Reflector_btl.Count - 1]] = Sc_reflector_list[i];
                Reflector_btl[Reflector_btl.Count - 1].Click += Reflector_bt_Click;
                i++;
            }
            Reflector_btl[Reflector_btl.Count - 1].BorderBrush = green_bright_button_brush;
            Object_ID_tb.Text = "Object ID:  " + Dic_bt_drbef[Reflector_btl[Reflector_btl.Count - 1]].ID.ToString();
            Global_temp_bt = Reflector_btl[Reflector_btl.Count - 1];
        }
        private async void Reflector_bt_Click(object sender, RoutedEventArgs e)
        {
            Button foo = sender as Button;
            TRF_bt_decolor();
            foo.BorderBrush = green_bright_button_brush;
            Object_ID_tb.Text = "Object ID:  " + Dic_bt_drbef[foo].ID.ToString();
            Global_temp_bt = foo;
            Initial_info_tb.Text = Dic_bt_drbef[foo].Summary();
        }

        public void Receiver_setup()
        {
            TRF_bt_decolor();
            Dic_bt_drber = new Dictionary<Button, DRBE_Receiver>();

            int i = 0;
            i = 0;
            while (i < Receiver_btl.Count)
            {
                if (ParentGrid.Children.Contains(Receiver_btl[i]))
                {
                    ParentGrid.Children.Remove(Receiver_btl[i]);
                }
                i++;
            }

            Receiver_btl = new List<Button>();
            i = 0;
            while (i < Sc_receiver_list.Count)
            {
                Receiver_btl.Add(new Button()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    Background = Default_back_black_color_brush,
                    Content = "Rvr:  " + Sc_receiver_list[i].ID.ToString(),
                    Foreground = white_button_brush,
                    FontWeight = FontWeights.Bold,
                    BorderBrush = white_button_brush,
                    BorderThickness = new Thickness(2, 2, 2, 2),
                    FontSize = 18
                });
                ParentGrid.Children.Add(Receiver_btl[Receiver_btl.Count - 1]);
                Receiver_btl[Receiver_btl.Count - 1].SetValue(Grid.ColumnProperty, 30);
                Receiver_btl[Receiver_btl.Count - 1].SetValue(Grid.ColumnSpanProperty, 15);
                Receiver_btl[Receiver_btl.Count - 1].SetValue(Grid.RowProperty, 25 + i * 5);
                Receiver_btl[Receiver_btl.Count - 1].SetValue(Grid.RowSpanProperty, 5);
                Dic_bt_drber[Receiver_btl[Receiver_btl.Count - 1]] = Sc_receiver_list[i];
                Receiver_btl[Receiver_btl.Count - 1].Click += Receiver_bt_Click;
                i++;
            }
            Receiver_btl[Receiver_btl.Count - 1].BorderBrush = green_bright_button_brush;
            Object_ID_tb.Text = "Object ID:  " + Dic_bt_drber[Receiver_btl[Receiver_btl.Count - 1]].ID.ToString();
            Global_temp_bt = Receiver_btl[Receiver_btl.Count - 1];
        }

        private async void Receiver_bt_Click(object sender, RoutedEventArgs e)
        {
            Button foo = sender as Button;
            TRF_bt_decolor();
            foo.BorderBrush = green_bright_button_brush;
            Object_ID_tb.Text = "Object ID:  " + Dic_bt_drber[foo].ID.ToString();
            Global_temp_bt = foo;
            Initial_info_tb.Text = Dic_bt_drber[foo].Summary();
        }





        #region others


        private async Task<int> ConfirmDialog(string title, string content, string confirmtext, string canceltext)
        {
            ContentDialog ConfirmDialog = new ContentDialog
            {
                Title = title,
                Content = content,
                PrimaryButtonText = confirmtext,
                CloseButtonText = canceltext
            };

            ContentDialogResult result = await ConfirmDialog.ShowAsync();

            // Delete the file if the user clicked the primary button.
            /// Otherwise, do nothing.
            if (result == ContentDialogResult.Primary)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        private async Task ShowDialog(string x, string y)
        {
            TextBlock svt = new TextBlock()
            {
                FontSize = 12,
                Text = y,
                Width = 1000,
                TextWrapping = TextWrapping.WrapWholeWords
            };
            ScrollViewer sv = new ScrollViewer()
            {
                Width = 1000
            };
            sv.Content = svt;
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = x,
                Content = sv,
                //Content = y,
                CloseButtonText = "Ok"
            };



            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }
        private double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        private List<double> DRBE_ATON_XYZ(double lat, double lon, double h)
        {
            List<double> result = new List<double>();

            double Req = 6378.137;
            double f = 1 / 298.257223563;
            double ee = f * (2 - f);
            double N = Req / Math.Sqrt(1 - ee * Math.Sin(lat / 180 * Math.PI) * Math.Sin(lat / 180 * Math.PI));
            double x = (N + h) * Math.Cos(lat / 180 * Math.PI) * Math.Cos(lon / 180 * Math.PI);
            double y = (N + h) * Math.Cos(lat / 180 * Math.PI) * Math.Sin(lon / 180 * Math.PI);
            double z = ((1 - ee) * N + h) * Math.Sin(lat / 180 * Math.PI);

            result.Add(x);
            result.Add(y);
            result.Add(z);

            return result;
        }

        private double DRBE_ATON_Distance(double lat, double lon, double h, double lat1, double lon1, double h1)
        {
            double result = 0;
            List<double> temp1 = new List<double>(DRBE_ATON_XYZ(lat, lon, h));
            List<double> temp2 = new List<double>(DRBE_ATON_XYZ(lat1, lon1, h1));

            result = Math.Sqrt((temp1[0] - temp2[0]) * (temp1[0] - temp2[0]) + (temp1[1] - temp2[1]) * (temp1[1] - temp2[1]) + (temp1[2] - temp2[2]) * (temp1[2] - temp2[2]));

            return result;
        }
        private List<double> Get_XY_lat_km(double lat1, double lon1, double lat2, double lon2)
        {
            List<double> result = new List<double>();
            result.Add(getDistanceFromLatLonInKm(lat1, lon1, lat2, lon1));
            result.Add(getDistanceFromLatLonInKm(lat1, lon1, lat1, lon2));

            return result;
        }
        private double getDistanceFromLatLonInKm(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371000; // Radius of the earth in km
            double dLat = (lat2 - lat1) / 180 * Math.PI;  // deg2rad below
            double dLon = (lon2 - lon1) / 180 * Math.PI;
            var a =
              Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(lat1 / 180 * Math.PI) * Math.Cos(lat2 / 180 * Math.PI) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
              ;
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // Distance in km
            return d;
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
    }
}
