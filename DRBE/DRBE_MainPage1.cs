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
    public class DRBE_MainPage1
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

        private Button DRBE_bt = new Button();
        private Image DRBE_im = new Image();
        private Button Home_bt = new Button();
        private Image Home_im = new Image();
        private Border Side_bd = new Border();
        private Button Refresh_bt = new Button();
        private Image Refresh_im = new Image();


        private Button Hardware_view_bt = new Button();
        private Image Hardware_view_im = new Image();

        private Button FPGA_bt = new Button();
        private Image FPGA_im = new Image();
        private Button GE_bt = new Button();
        private Image GE_im = new Image();
        private Button PPU_bt = new Button();
        private Image PPU_im = new Image();
        private Button Compiler_bt = new Button();
        private Image Compiler_im = new Image();

        private Button Extern_bt = new Button();
        private Image Extern_im = new Image();
        private Button RF_int_bt = new Button();
        private Image RF_int_im = new Image();
        private Button Coe_int_bt = new Button();
        private Image Coe_int_im = new Image();

        private Button Simulation_view_bt = new Button();
        private Image Simulation_view_im = new Image();
        private Button Load_sim_bt = new Button();
        private Image Load_sim_im = new Image();
        private Button Load_constraint_bt = new Button();
        private Image Load_constraint_im = new Image();
        private Button Reference_bt = new Button();
        private Image Reference_im = new Image();
        private Button Load_fidel_bt = new Button();
        private Image Load_fidel_im = new Image();
        private Button Start_simulator_bt = new Button();
        private Image Start_simulator_im = new Image();

        private Button Tool_view_bt = new Button();
        private Image Tool_view_im = new Image();
        private Button Monitor_bt = new Button();
        private Image Monitor_im = new Image();
        private Button Start_bt = new Button();
        private Image Start_im = new Image();
        private Button Config_bt = new Button();
        private Image Config_im = new Image();
        private Button Report_bt = new Button();
        private Image Report_im = new Image();

        private TextBlock DRBE_Timer_tb = new TextBlock();

        private Image DRBE_Battery_bt_i = new Image();
        private TextBlock DRBE_Battery_tb = new TextBlock();
        private Button DRBE_Battery_bt = new Button();
        private ProgressBar DRBE_Battery_Progressbar = new ProgressBar();
        private Battery DRBE_Battery;
        private DRBE_Scenario_Generator DRBE_SG; // = new DRBE_Scenario_Generator(MainGrid);
        public DRBE_MainPage1(Grid parent)
        {
            ParentGrid = parent;
            Setup();
            Hide();
            Clock_setup();
            Battery_setup();

            
        }
        #region Timer
        private DispatcherTimer DRBE_Timer = new DispatcherTimer();
        private void Clock_setup()
        {
            DRBE_Timer.Tick += DRBE_Timer_Tick;
            DRBE_Timer.Interval = new TimeSpan(0, 0, 1);
            DRBE_Timer.Start();
        }
        private async void DRBE_Timer_Tick(object sender, object e)
        {
            DRBE_Timer_tb.Text = DateTime.Now.ToString("h:mm:ss tt");
        }
        #endregion
        #region Battery

        private void Battery_setup()
        {
            RequestAggregateBatteryReport();
            Battery.AggregateBattery.ReportUpdated += AggregateBattery_ReportUpdated;
        }
        private void RequestAggregateBatteryReport()
        {
            DRBE_Battery = Battery.AggregateBattery;

            // Get report
            var report = DRBE_Battery.GetReport();
            if ((report.FullChargeCapacityInMilliwattHours == null) ||
            (report.RemainingCapacityInMilliwattHours == null))
            {
                DRBE_Battery_Progressbar.IsEnabled = false;
                DRBE_Battery_tb.Text = "N/A";
            }
            else
            {
                DRBE_Battery_Progressbar.IsEnabled = true;
                DRBE_Battery_Progressbar.Maximum = Convert.ToDouble(report.FullChargeCapacityInMilliwattHours);
                DRBE_Battery_Progressbar.Value = Convert.ToDouble(report.RemainingCapacityInMilliwattHours);
                DRBE_Battery_Progressbar.Background = white_button_brush;
                DRBE_Battery_tb.Text = ((DRBE_Battery_Progressbar.Value / DRBE_Battery_Progressbar.Maximum) * 100).ToString("F2") + "%";
            }
        }
        async private void AggregateBattery_ReportUpdated(Battery sender, object args)
        {
            RequestAggregateBatteryReport();
        }
        #endregion
        public void Setup()
        {
            #region Header

            #region Battery
            DRBE_Battery_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Foreground = white_button_brush,
                TextAlignment = TextAlignment.Center,
                Text = "Battery",
                FontFamily = new FontFamily("Comic Sans MS"),
                FontSize = 14
            };
            DRBE_Battery_tb.SetValue(TextBlock.FontWeightProperty, FontWeights.ExtraBold);
            ParentGrid.Children.Add(DRBE_Battery_tb);
            DRBE_Battery_tb.SetValue(Grid.ColumnProperty, 10);
            DRBE_Battery_tb.SetValue(Grid.ColumnSpanProperty, 10);
            DRBE_Battery_tb.SetValue(Grid.RowProperty, 53);
            DRBE_Battery_tb.SetValue(Grid.RowSpanProperty, 5);

            DRBE_Battery_Progressbar = new ProgressBar()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Visibility = Visibility.Collapsed
            };
            ParentGrid.Children.Add(DRBE_Battery_Progressbar);
            DRBE_Battery_Progressbar.SetValue(Grid.ColumnProperty, 0);
            DRBE_Battery_Progressbar.SetValue(Grid.ColumnSpanProperty, 20);
            DRBE_Battery_Progressbar.SetValue(Grid.RowProperty, 60);
            DRBE_Battery_Progressbar.SetValue(Grid.RowSpanProperty, 5);

            DRBE_Battery_bt_i.Source = new BitmapImage(new Uri("ms-appx:///Assets/Battery_icon.png", UriKind.RelativeOrAbsolute));
            DRBE_Battery_bt = new Button()
            {
                Background = Default_back_black_color_brush,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Foreground = white_button_brush,
                Content = DRBE_Battery_bt_i
            };
            ParentGrid.Children.Add(DRBE_Battery_bt);
            DRBE_Battery_bt.SetValue(Grid.ColumnProperty, 0);
            DRBE_Battery_bt.SetValue(Grid.ColumnSpanProperty, 10);
            DRBE_Battery_bt.SetValue(Grid.RowProperty, 50);
            DRBE_Battery_bt.SetValue(Grid.RowSpanProperty, 10);
            #endregion



            DRBE_Timer_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "-- -- --",
                Foreground = white_button_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(DRBE_Timer_tb);
            DRBE_Timer_tb.SetValue(Grid.ColumnProperty, 0);
            DRBE_Timer_tb.SetValue(Grid.ColumnSpanProperty, 20);
            DRBE_Timer_tb.SetValue(Grid.RowProperty, 45);
            DRBE_Timer_tb.SetValue(Grid.RowSpanProperty, 5);

            Side_bd = new Border()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                BorderThickness = new Thickness(0,0,1,0),
                BorderBrush = dark_grey_brush
            };
            ParentGrid.Children.Add(Side_bd);
            Side_bd.SetValue(Grid.ColumnProperty, 0);
            Side_bd.SetValue(Grid.ColumnSpanProperty, 20);
            Side_bd.SetValue(Grid.RowProperty, 0);
            Side_bd.SetValue(Grid.RowSpanProperty, 150);
            Canvas.SetZIndex(Side_bd,-10);

            DRBE_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/DRBE_icon.png", UriKind.RelativeOrAbsolute));
            DRBE_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = DRBE_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(DRBE_bt);
            DRBE_bt.SetValue(Grid.ColumnProperty, 0);
            DRBE_bt.SetValue(Grid.ColumnSpanProperty, 15);
            DRBE_bt.SetValue(Grid.RowProperty, 2);
            DRBE_bt.SetValue(Grid.RowSpanProperty, 15);


            Home_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Home_icon.png", UriKind.RelativeOrAbsolute));
            Home_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Home_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Home_bt);
            Home_bt.SetValue(Grid.ColumnProperty, 0);
            Home_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Home_bt.SetValue(Grid.RowProperty, 20);
            Home_bt.SetValue(Grid.RowSpanProperty, 15);



            #endregion

            #region Hardware
            Hardware_view_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Hardware_icon.png", UriKind.RelativeOrAbsolute));
            Hardware_view_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Hardware_view_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0,0,0,1),
                FontSize = 18
            };
            ParentGrid.Children.Add(Hardware_view_bt);
            Hardware_view_bt.SetValue(Grid.ColumnProperty, 30);
            Hardware_view_bt.SetValue(Grid.ColumnSpanProperty, 40);
            Hardware_view_bt.SetValue(Grid.RowProperty, 10);
            Hardware_view_bt.SetValue(Grid.RowSpanProperty, 35);


            FPGA_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/FPGAs-Icon_4x.jpg", UriKind.RelativeOrAbsolute));
            FPGA_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = FPGA_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(FPGA_bt);
            FPGA_bt.SetValue(Grid.ColumnProperty, 50);
            FPGA_bt.SetValue(Grid.ColumnSpanProperty, 20);
            FPGA_bt.SetValue(Grid.RowProperty, 70);
            FPGA_bt.SetValue(Grid.RowSpanProperty, 20);


            GE_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/GE.png", UriKind.RelativeOrAbsolute));
            GE_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = GE_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(GE_bt);
            GE_bt.SetValue(Grid.ColumnProperty, 50);
            GE_bt.SetValue(Grid.ColumnSpanProperty, 20);
            GE_bt.SetValue(Grid.RowProperty, 50);
            GE_bt.SetValue(Grid.RowSpanProperty, 20);

            PPU_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/PPU.jpg", UriKind.RelativeOrAbsolute));
            PPU_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = PPU_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(PPU_bt);
            PPU_bt.SetValue(Grid.ColumnProperty, 30);
            PPU_bt.SetValue(Grid.ColumnSpanProperty, 20);
            PPU_bt.SetValue(Grid.RowProperty, 50);
            PPU_bt.SetValue(Grid.RowSpanProperty, 20);

            Compiler_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Compiler.png", UriKind.RelativeOrAbsolute));
            Compiler_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Compiler_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Compiler_bt);
            Compiler_bt.SetValue(Grid.ColumnProperty, 30);
            Compiler_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Compiler_bt.SetValue(Grid.RowProperty, 70);
            Compiler_bt.SetValue(Grid.RowSpanProperty, 20);

            Extern_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Device.png", UriKind.RelativeOrAbsolute));
            Extern_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Extern_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Extern_bt);
            Extern_bt.SetValue(Grid.ColumnProperty, 30);
            Extern_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Extern_bt.SetValue(Grid.RowProperty, 90);
            Extern_bt.SetValue(Grid.RowSpanProperty, 20);


            RF_int_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/RF_interface.png", UriKind.RelativeOrAbsolute));
            RF_int_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = RF_int_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(RF_int_bt);
            RF_int_bt.SetValue(Grid.ColumnProperty, 50);
            RF_int_bt.SetValue(Grid.ColumnSpanProperty, 20);
            RF_int_bt.SetValue(Grid.RowProperty, 90);
            RF_int_bt.SetValue(Grid.RowSpanProperty, 20);

            Coe_int_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Data_interface.png", UriKind.RelativeOrAbsolute));
            Coe_int_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Coe_int_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Coe_int_bt);
            Coe_int_bt.SetValue(Grid.ColumnProperty, 30);
            Coe_int_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Coe_int_bt.SetValue(Grid.RowProperty, 110);
            Coe_int_bt.SetValue(Grid.RowSpanProperty, 20);

            #endregion

            #region Scenario

            Simulation_view_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Scenario_icon.jpg", UriKind.RelativeOrAbsolute));
            Simulation_view_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Simulation_view_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0, 0, 1),
                FontSize = 18
            };
            ParentGrid.Children.Add(Simulation_view_bt);
            Simulation_view_bt.SetValue(Grid.ColumnProperty, 90);
            Simulation_view_bt.SetValue(Grid.ColumnSpanProperty, 40);
            Simulation_view_bt.SetValue(Grid.RowProperty, 10);
            Simulation_view_bt.SetValue(Grid.RowSpanProperty, 35);

            Load_sim_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Simulation_icon.png", UriKind.RelativeOrAbsolute));
            Load_sim_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Load_sim_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Load_sim_bt);
            Load_sim_bt.SetValue(Grid.ColumnProperty, 90);
            Load_sim_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Load_sim_bt.SetValue(Grid.RowProperty, 50);
            Load_sim_bt.SetValue(Grid.RowSpanProperty, 20);

            Load_constraint_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Constraint.jpg", UriKind.RelativeOrAbsolute));
            Load_constraint_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Load_constraint_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Load_constraint_bt);
            Load_constraint_bt.SetValue(Grid.ColumnProperty, 110);
            Load_constraint_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Load_constraint_bt.SetValue(Grid.RowProperty, 50);
            Load_constraint_bt.SetValue(Grid.RowSpanProperty, 20);

            Reference_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Reference_icon.png", UriKind.RelativeOrAbsolute));
            Reference_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Reference_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Reference_bt);
            Reference_bt.SetValue(Grid.ColumnProperty, 90);
            Reference_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Reference_bt.SetValue(Grid.RowProperty, 70);
            Reference_bt.SetValue(Grid.RowSpanProperty, 20);

            Load_fidel_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Fidelity_data_icon.png", UriKind.RelativeOrAbsolute));
            Load_fidel_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Load_fidel_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Load_fidel_bt);
            Load_fidel_bt.SetValue(Grid.ColumnProperty, 110);
            Load_fidel_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Load_fidel_bt.SetValue(Grid.RowProperty, 70);
            Load_fidel_bt.SetValue(Grid.RowSpanProperty, 20);

            Start_simulator_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Simulator_icon.png", UriKind.RelativeOrAbsolute));
            Start_simulator_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Start_simulator_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Start_simulator_bt);
            Start_simulator_bt.SetValue(Grid.ColumnProperty, 90);
            Start_simulator_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Start_simulator_bt.SetValue(Grid.RowProperty, 90);
            Start_simulator_bt.SetValue(Grid.RowSpanProperty, 20);

            Start_simulator_bt.Click += Start_simulator_bt_Click;

            #endregion

            #region Tool

            Tool_view_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Test_icon.png", UriKind.RelativeOrAbsolute));
            Tool_view_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Tool_view_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0, 0, 1),
                FontSize = 18
            };
            ParentGrid.Children.Add(Tool_view_bt);
            Tool_view_bt.SetValue(Grid.ColumnProperty, 150);
            Tool_view_bt.SetValue(Grid.ColumnSpanProperty, 40);
            Tool_view_bt.SetValue(Grid.RowProperty, 10);
            Tool_view_bt.SetValue(Grid.RowSpanProperty, 35);

            Monitor_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Monitor_icon.png", UriKind.RelativeOrAbsolute));
            Monitor_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Monitor_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Monitor_bt);
            Monitor_bt.SetValue(Grid.ColumnProperty, 150);
            Monitor_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Monitor_bt.SetValue(Grid.RowProperty, 50);
            Monitor_bt.SetValue(Grid.RowSpanProperty, 20);

            Start_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Play_icon.png", UriKind.RelativeOrAbsolute));
            Start_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Start_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Start_bt);
            Start_bt.SetValue(Grid.ColumnProperty, 170);
            Start_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Start_bt.SetValue(Grid.RowProperty, 50);
            Start_bt.SetValue(Grid.RowSpanProperty, 20);

            Config_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Configuration_icon.png", UriKind.RelativeOrAbsolute));
            Config_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Config_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Config_bt);
            Config_bt.SetValue(Grid.ColumnProperty, 150);
            Config_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Config_bt.SetValue(Grid.RowProperty, 70);
            Config_bt.SetValue(Grid.RowSpanProperty, 20);

            Report_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Report_icon.png", UriKind.RelativeOrAbsolute));
            Report_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Report_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Report_bt);
            Report_bt.SetValue(Grid.ColumnProperty, 170);
            Report_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Report_bt.SetValue(Grid.RowProperty, 70);
            Report_bt.SetValue(Grid.RowSpanProperty, 20);

            #endregion 

        }

        private void Start_simulator_bt_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            DRBE_SG = new DRBE_Scenario_Generator(ParentGrid);
        }

        public void Hide()
        {
            Start_simulator_bt.Visibility = Visibility.Collapsed;

            #region Header

            #region Battery
            DRBE_Battery_tb.Visibility = Visibility.Collapsed;

            DRBE_Battery_Progressbar.Visibility = Visibility.Collapsed;

            DRBE_Battery_bt.Visibility = Visibility.Collapsed;
            #endregion



            DRBE_Timer_tb.Visibility = Visibility.Collapsed;

            Side_bd.Visibility = Visibility.Collapsed;

            DRBE_bt.Visibility = Visibility.Collapsed;

            Home_bt.Visibility = Visibility.Collapsed;



            #endregion

            #region Hardware

            Hardware_view_bt.Visibility = Visibility.Collapsed;

            FPGA_bt.Visibility = Visibility.Collapsed;


            GE_bt.Visibility = Visibility.Collapsed;

            PPU_bt.Visibility = Visibility.Collapsed;

            Compiler_bt.Visibility = Visibility.Collapsed;

            Extern_bt.Visibility = Visibility.Collapsed;

            RF_int_bt.Visibility = Visibility.Collapsed;

            Coe_int_bt.Visibility = Visibility.Collapsed;

            #endregion

            #region Scenario

            Simulation_view_bt.Visibility = Visibility.Collapsed;

            Load_sim_bt.Visibility = Visibility.Collapsed;

            Load_constraint_bt.Visibility = Visibility.Collapsed;

            Reference_bt.Visibility = Visibility.Collapsed;

            Load_fidel_bt.Visibility = Visibility.Collapsed;

            #endregion

            #region Tool

            Tool_view_bt.Visibility = Visibility.Collapsed; 

            Monitor_bt.Visibility = Visibility.Collapsed;

            Start_bt.Visibility = Visibility.Collapsed;

            Config_bt.Visibility = Visibility.Collapsed;

            Report_bt.Visibility = Visibility.Collapsed;

            #endregion 
        }
        public void Show()
        {
            Start_simulator_bt.Visibility = Visibility.Visible;

            #region Header

            #region Battery
            DRBE_Battery_tb.Visibility = Visibility.Visible;

            DRBE_Battery_Progressbar.Visibility = Visibility.Visible;

            DRBE_Battery_bt.Visibility = Visibility.Visible;
            #endregion



            DRBE_Timer_tb.Visibility = Visibility.Visible;

            Side_bd.Visibility = Visibility.Visible;

            DRBE_bt.Visibility = Visibility.Visible;

            Home_bt.Visibility = Visibility.Visible;



            #endregion

            #region Hardware

            Hardware_view_bt.Visibility = Visibility.Visible;

            FPGA_bt.Visibility = Visibility.Visible;


            GE_bt.Visibility = Visibility.Visible;

            PPU_bt.Visibility = Visibility.Visible;

            Compiler_bt.Visibility = Visibility.Visible;

            Extern_bt.Visibility = Visibility.Visible;

            RF_int_bt.Visibility = Visibility.Visible;

            Coe_int_bt.Visibility = Visibility.Visible;

            #endregion

            #region Scenario

            Simulation_view_bt.Visibility = Visibility.Visible;

            Load_sim_bt.Visibility = Visibility.Visible;

            Load_constraint_bt.Visibility = Visibility.Visible;

            Reference_bt.Visibility = Visibility.Visible;

            Load_fidel_bt.Visibility = Visibility.Visible;

            #endregion

            #region Tool

            Tool_view_bt.Visibility = Visibility.Visible;

            Monitor_bt.Visibility = Visibility.Visible;

            Start_bt.Visibility = Visibility.Visible;

            Config_bt.Visibility = Visibility.Visible;

            Report_bt.Visibility = Visibility.Visible;

            #endregion 
        }
    }
}