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
    public class ControlPanel
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
        #region Server
        public Border Server_bd = new Border();

        public TextBlock Server_title_tb = new TextBlock();
        public TextBlock Server_matlab_title_tb = new TextBlock();
        public TextBlock Server_ui_title_tb = new TextBlock();
        public TextBlock Server_fpga_title_tb = new TextBlock();
        public TextBlock Server_python_title_tb = new TextBlock();

        public TextBlock Server_matlab_tb = new TextBlock();
        public TextBlock Server_ui_tb = new TextBlock();
        public TextBlock Server_fpga_tb = new TextBlock();
        public TextBlock Server_python_tb = new TextBlock();
        #endregion
        #region Hardware
        public Border Hardware_bd = new Border();

        public TextBlock Hardware_title_tb = new TextBlock();

        public TextBlock Hardware_riskv_title_tb = new TextBlock();
        public TextBlock Hardware_filter1_title_tb = new TextBlock();
        public TextBlock Hardware_filter2_title_tb = new TextBlock();
        public TextBlock Hardware_filter3_title_tb = new TextBlock();
        public TextBlock Hardware_controlmodule_title_tb = new TextBlock();
        public TextBlock Hardware_module1_title_tb = new TextBlock();
        public TextBlock Hardware_module2_title_tb = new TextBlock();
        public TextBlock Hardware_module3_title_tb = new TextBlock();

        public TextBlock Hardware_riskv_tb = new TextBlock();
        public TextBlock Hardware_filter1_tb = new TextBlock();
        public TextBlock Hardware_filter2_tb = new TextBlock();
        public TextBlock Hardware_filter3_tb = new TextBlock();
        public TextBlock Hardware_controlmodule_tb = new TextBlock();
        public TextBlock Hardware_module1_tb = new TextBlock();
        public TextBlock Hardware_module2_tb = new TextBlock();
        public TextBlock Hardware_module3_tb = new TextBlock();
        #endregion
        #region IO
        public Border IO_bd = new Border();

        public TextBlock IO_title_tb = new TextBlock();

        public TextBlock IO_rf_title_tb = new TextBlock();
        public TextBlock IO_scenario_title_tb = new TextBlock();
        public TextBlock IO_compiler_title_tb = new TextBlock();
        public TextBlock IO_firmware_title_tb = new TextBlock();

        public TextBlock IO_rf_tb = new TextBlock();
        public TextBlock IO_scenario_tb = new TextBlock();
        public TextBlock IO_compiler_tb = new TextBlock();
        public TextBlock IO_firmware_tb = new TextBlock();
        #endregion

        #region DRBE Command
        public TextBlock DRBE_Command_title_tb = new TextBlock();

        public Border DRBE_Command_bd = new Border();

        public Button DRBE_Initialization_bt = new Button();
        public Button DRBE_Import_bt = new Button();
        public Button DRBE_Start_bt = new Button();
        public Button DRBE_Pause_bt = new Button();
        public Button DRBE_Stop_bt = new Button();
        public Button DRBE_Refresh_bt = new Button();
        #endregion

        #region Message
        public Border Message_bd = new Border();

        public TextBlock Message_tb = new TextBlock();
        #endregion

        public ControlPanel(Grid parent)
        {
            ParentGrid = parent;
            Setup();
        }

        private void Setup()
        {
            #region server
            Server_title_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Server Connections"
            };
            Server_title_tb.SetValue(Grid.ColumnProperty, 75);
            Server_title_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Server_title_tb.SetValue(Grid.RowProperty, 20);
            Server_title_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Server_title_tb);

            Server_bd = new Border()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Canvas.SetZIndex(Server_bd, -1);
            Server_bd.SetValue(Grid.ColumnProperty, 60);
            Server_bd.SetValue(Grid.ColumnSpanProperty, 50);
            Server_bd.SetValue(Grid.RowProperty, 20);
            Server_bd.SetValue(Grid.RowSpanProperty, 60);
            ParentGrid.Children.Add(Server_bd);

            Server_matlab_title_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Matlab: "
            };
            Server_matlab_title_tb.SetValue(Grid.ColumnProperty, 61);
            Server_matlab_title_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Server_matlab_title_tb.SetValue(Grid.RowProperty, 30);
            Server_matlab_title_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Server_matlab_title_tb);

            Server_matlab_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 16,
                Foreground = red_bright_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Not Found"
            };
            Server_matlab_tb.SetValue(Grid.ColumnProperty, 81);
            Server_matlab_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Server_matlab_tb.SetValue(Grid.RowProperty, 30);
            Server_matlab_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Server_matlab_tb);

            Server_ui_title_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "UI: "
            };
            Server_ui_title_tb.SetValue(Grid.ColumnProperty, 61);
            Server_ui_title_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Server_ui_title_tb.SetValue(Grid.RowProperty, 40);
            Server_ui_title_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Server_ui_title_tb);

            Server_ui_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 16,
                Foreground = red_bright_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Not Found"
            };
            Server_ui_tb.SetValue(Grid.ColumnProperty, 81);
            Server_ui_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Server_ui_tb.SetValue(Grid.RowProperty, 40);
            Server_ui_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Server_ui_tb);

            Server_python_title_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Python: "
            };
            Server_python_title_tb.SetValue(Grid.ColumnProperty, 61);
            Server_python_title_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Server_python_title_tb.SetValue(Grid.RowProperty, 50);
            Server_python_title_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Server_python_title_tb);

            Server_python_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 16,
                Foreground = red_bright_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Not Found"
            };
            Server_python_tb.SetValue(Grid.ColumnProperty, 81);
            Server_python_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Server_python_tb.SetValue(Grid.RowProperty, 50);
            Server_python_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Server_python_tb);


            Server_fpga_title_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "FPGA: "
            };
            Server_fpga_title_tb.SetValue(Grid.ColumnProperty, 61);
            Server_fpga_title_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Server_fpga_title_tb.SetValue(Grid.RowProperty, 60);
            Server_fpga_title_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Server_fpga_title_tb);

            Server_fpga_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 16,
                Foreground = red_bright_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Not Found"
            };
            Server_fpga_tb.SetValue(Grid.ColumnProperty, 81);
            Server_fpga_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Server_fpga_tb.SetValue(Grid.RowProperty, 60);
            Server_fpga_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Server_fpga_tb);
            #endregion
            #region Hardware
            Hardware_title_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Hardware Statues"
            };
            Hardware_title_tb.SetValue(Grid.ColumnProperty, 75);
            Hardware_title_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Hardware_title_tb.SetValue(Grid.RowProperty, 80);
            Hardware_title_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Hardware_title_tb);

            Hardware_bd = new Border()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Canvas.SetZIndex(Hardware_bd, -1);
            Hardware_bd.SetValue(Grid.ColumnProperty, 60);
            Hardware_bd.SetValue(Grid.ColumnSpanProperty, 50);
            Hardware_bd.SetValue(Grid.RowProperty, 80);
            Hardware_bd.SetValue(Grid.RowSpanProperty, 70);
            ParentGrid.Children.Add(Hardware_bd);

            Hardware_riskv_title_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "RISC-Core: "
            };
            Hardware_riskv_title_tb.SetValue(Grid.ColumnProperty, 61);
            Hardware_riskv_title_tb.SetValue(Grid.ColumnSpanProperty, 12);
            Hardware_riskv_title_tb.SetValue(Grid.RowProperty, 90);
            Hardware_riskv_title_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Hardware_riskv_title_tb);

            Hardware_riskv_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 16,
                Foreground = red_bright_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Not Found"
            };
            Hardware_riskv_tb.SetValue(Grid.ColumnProperty, 73);
            Hardware_riskv_tb.SetValue(Grid.ColumnSpanProperty, 12);
            Hardware_riskv_tb.SetValue(Grid.RowProperty, 90);
            Hardware_riskv_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Hardware_riskv_tb);

            Hardware_controlmodule_title_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Controler: "
            };
            Hardware_controlmodule_title_tb.SetValue(Grid.ColumnProperty, 61);
            Hardware_controlmodule_title_tb.SetValue(Grid.ColumnSpanProperty, 12);
            Hardware_controlmodule_title_tb.SetValue(Grid.RowProperty, 100);
            Hardware_controlmodule_title_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Hardware_controlmodule_title_tb);

            Hardware_controlmodule_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 16,
                Foreground = red_bright_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Not Found"
            };
            Hardware_controlmodule_tb.SetValue(Grid.ColumnProperty, 73);
            Hardware_controlmodule_tb.SetValue(Grid.ColumnSpanProperty, 12);
            Hardware_controlmodule_tb.SetValue(Grid.RowProperty, 100);
            Hardware_controlmodule_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Hardware_controlmodule_tb);

            Hardware_filter1_title_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Filter 1: "
            };
            Hardware_filter1_title_tb.SetValue(Grid.ColumnProperty, 61);
            Hardware_filter1_title_tb.SetValue(Grid.ColumnSpanProperty, 12);
            Hardware_filter1_title_tb.SetValue(Grid.RowProperty, 110);
            Hardware_filter1_title_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Hardware_filter1_title_tb);

            Hardware_filter1_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 16,
                Foreground = red_bright_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Not Found"
            };
            Hardware_filter1_tb.SetValue(Grid.ColumnProperty, 73);
            Hardware_filter1_tb.SetValue(Grid.ColumnSpanProperty, 12);
            Hardware_filter1_tb.SetValue(Grid.RowProperty, 110);
            Hardware_filter1_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Hardware_filter1_tb);

            Hardware_filter2_title_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Filter 2: "
            };
            Hardware_filter2_title_tb.SetValue(Grid.ColumnProperty, 61);
            Hardware_filter2_title_tb.SetValue(Grid.ColumnSpanProperty, 12);
            Hardware_filter2_title_tb.SetValue(Grid.RowProperty, 120);
            Hardware_filter2_title_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Hardware_filter2_title_tb);

            Hardware_filter2_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 16,
                Foreground = red_bright_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Not Found"
            };
            Hardware_filter2_tb.SetValue(Grid.ColumnProperty, 73);
            Hardware_filter2_tb.SetValue(Grid.ColumnSpanProperty, 12);
            Hardware_filter2_tb.SetValue(Grid.RowProperty, 120);
            Hardware_filter2_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Hardware_filter2_tb);

            Hardware_filter3_title_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Filter 3: "
            };
            Hardware_filter3_title_tb.SetValue(Grid.ColumnProperty, 61);
            Hardware_filter3_title_tb.SetValue(Grid.ColumnSpanProperty, 12);
            Hardware_filter3_title_tb.SetValue(Grid.RowProperty, 130);
            Hardware_filter3_title_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Hardware_filter3_title_tb);

            Hardware_filter3_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 16,
                Foreground = red_bright_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Not Found"
            };
            Hardware_filter3_tb.SetValue(Grid.ColumnProperty, 73);
            Hardware_filter3_tb.SetValue(Grid.ColumnSpanProperty, 12);
            Hardware_filter3_tb.SetValue(Grid.RowProperty, 130);
            Hardware_filter3_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Hardware_filter3_tb);

            Hardware_module1_title_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Module 1: "
            };
            Hardware_module1_title_tb.SetValue(Grid.ColumnProperty, 85);
            Hardware_module1_title_tb.SetValue(Grid.ColumnSpanProperty, 12);
            Hardware_module1_title_tb.SetValue(Grid.RowProperty, 90);
            Hardware_module1_title_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Hardware_module1_title_tb);

            Hardware_module1_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 16,
                Foreground = red_bright_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Not Found"
            };
            Hardware_module1_tb.SetValue(Grid.ColumnProperty, 97);
            Hardware_module1_tb.SetValue(Grid.ColumnSpanProperty, 12);
            Hardware_module1_tb.SetValue(Grid.RowProperty, 90);
            Hardware_module1_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Hardware_module1_tb);

            Hardware_module2_title_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Module 2: "
            };
            Hardware_module2_title_tb.SetValue(Grid.ColumnProperty, 85);
            Hardware_module2_title_tb.SetValue(Grid.ColumnSpanProperty, 12);
            Hardware_module2_title_tb.SetValue(Grid.RowProperty, 100);
            Hardware_module2_title_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Hardware_module2_title_tb);

            Hardware_module2_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 16,
                Foreground = red_bright_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Not Found"
            };
            Hardware_module2_tb.SetValue(Grid.ColumnProperty, 97);
            Hardware_module2_tb.SetValue(Grid.ColumnSpanProperty, 12);
            Hardware_module2_tb.SetValue(Grid.RowProperty, 100);
            Hardware_module2_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Hardware_module2_tb);

            Hardware_module3_title_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Module 3: "
            };
            Hardware_module3_title_tb.SetValue(Grid.ColumnProperty, 85);
            Hardware_module3_title_tb.SetValue(Grid.ColumnSpanProperty, 12);
            Hardware_module3_title_tb.SetValue(Grid.RowProperty, 110);
            Hardware_module3_title_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Hardware_module3_title_tb);

            Hardware_module3_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 16,
                Foreground = red_bright_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Not Found"
            };
            Hardware_module3_tb.SetValue(Grid.ColumnProperty, 97);
            Hardware_module3_tb.SetValue(Grid.ColumnSpanProperty, 12);
            Hardware_module3_tb.SetValue(Grid.RowProperty, 110);
            Hardware_module3_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Hardware_module3_tb);
            #endregion
            #region IO
            IO_title_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "IO Connection"
            };
            IO_title_tb.SetValue(Grid.ColumnProperty, 125);
            IO_title_tb.SetValue(Grid.ColumnSpanProperty, 20);
            IO_title_tb.SetValue(Grid.RowProperty, 80);
            IO_title_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(IO_title_tb);

            IO_bd = new Border()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Canvas.SetZIndex(IO_bd, -1);
            IO_bd.SetValue(Grid.ColumnProperty, 110);
            IO_bd.SetValue(Grid.ColumnSpanProperty, 50);
            IO_bd.SetValue(Grid.RowProperty, 80);
            IO_bd.SetValue(Grid.RowSpanProperty, 70);
            ParentGrid.Children.Add(IO_bd);

            IO_rf_title_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "RF Interface: "
            };
            IO_rf_title_tb.SetValue(Grid.ColumnProperty, 111);
            IO_rf_title_tb.SetValue(Grid.ColumnSpanProperty, 15);
            IO_rf_title_tb.SetValue(Grid.RowProperty, 90);
            IO_rf_title_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(IO_rf_title_tb);

            IO_rf_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 16,
                Foreground = red_bright_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Not Found"
            };
            IO_rf_tb.SetValue(Grid.ColumnProperty, 126);
            IO_rf_tb.SetValue(Grid.ColumnSpanProperty, 15);
            IO_rf_tb.SetValue(Grid.RowProperty, 90);
            IO_rf_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(IO_rf_tb);

            IO_scenario_title_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Scenario Eng: "
            };
            IO_scenario_title_tb.SetValue(Grid.ColumnProperty, 111);
            IO_scenario_title_tb.SetValue(Grid.ColumnSpanProperty, 15);
            IO_scenario_title_tb.SetValue(Grid.RowProperty, 100);
            IO_scenario_title_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(IO_scenario_title_tb);

            IO_scenario_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 16,
                Foreground = red_bright_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Not Found"
            };
            IO_scenario_tb.SetValue(Grid.ColumnProperty, 126);
            IO_scenario_tb.SetValue(Grid.ColumnSpanProperty, 15);
            IO_scenario_tb.SetValue(Grid.RowProperty, 100);
            IO_scenario_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(IO_scenario_tb);

            IO_compiler_title_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Compiler: "
            };
            IO_compiler_title_tb.SetValue(Grid.ColumnProperty, 111);
            IO_compiler_title_tb.SetValue(Grid.ColumnSpanProperty, 15);
            IO_compiler_title_tb.SetValue(Grid.RowProperty, 110);
            IO_compiler_title_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(IO_compiler_title_tb);

            IO_compiler_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 16,
                Foreground = red_bright_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Not Found"
            };
            IO_compiler_tb.SetValue(Grid.ColumnProperty, 126);
            IO_compiler_tb.SetValue(Grid.ColumnSpanProperty, 15);
            IO_compiler_tb.SetValue(Grid.RowProperty, 110);
            IO_compiler_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(IO_compiler_tb);

            IO_firmware_title_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Firmware: "
            };
            IO_firmware_title_tb.SetValue(Grid.ColumnProperty, 111);
            IO_firmware_title_tb.SetValue(Grid.ColumnSpanProperty, 15);
            IO_firmware_title_tb.SetValue(Grid.RowProperty, 120);
            IO_firmware_title_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(IO_firmware_title_tb);

            IO_firmware_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 16,
                Foreground = red_bright_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Not Found"
            };
            IO_firmware_tb.SetValue(Grid.ColumnProperty, 126);
            IO_firmware_tb.SetValue(Grid.ColumnSpanProperty, 15);
            IO_firmware_tb.SetValue(Grid.RowProperty, 120);
            IO_firmware_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(IO_firmware_tb);
            #endregion
            #region Command
            DRBE_Command_title_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Basic Command"
            };
            DRBE_Command_title_tb.SetValue(Grid.ColumnProperty, 125);
            DRBE_Command_title_tb.SetValue(Grid.ColumnSpanProperty, 20);
            DRBE_Command_title_tb.SetValue(Grid.RowProperty, 20);
            DRBE_Command_title_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(DRBE_Command_title_tb);

            DRBE_Command_bd = new Border()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Canvas.SetZIndex(DRBE_Command_bd, -1);
            DRBE_Command_bd.SetValue(Grid.ColumnProperty, 110);
            DRBE_Command_bd.SetValue(Grid.ColumnSpanProperty, 50);
            DRBE_Command_bd.SetValue(Grid.RowProperty, 20);
            DRBE_Command_bd.SetValue(Grid.RowSpanProperty, 60);
            ParentGrid.Children.Add(DRBE_Command_bd);

            DRBE_Initialization_bt = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Content = "Initialization",
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            DRBE_Initialization_bt.SetValue(Grid.ColumnProperty, 111);
            DRBE_Initialization_bt.SetValue(Grid.ColumnSpanProperty, 15);
            DRBE_Initialization_bt.SetValue(Grid.RowProperty, 30);
            DRBE_Initialization_bt.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(DRBE_Initialization_bt);

            DRBE_Import_bt = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Content = "Import",
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            DRBE_Import_bt.SetValue(Grid.ColumnProperty, 111);
            DRBE_Import_bt.SetValue(Grid.ColumnSpanProperty, 15);
            DRBE_Import_bt.SetValue(Grid.RowProperty, 40);
            DRBE_Import_bt.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(DRBE_Import_bt);

            DRBE_Start_bt = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Content = "Start",
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            DRBE_Start_bt.SetValue(Grid.ColumnProperty, 111);
            DRBE_Start_bt.SetValue(Grid.ColumnSpanProperty, 15);
            DRBE_Start_bt.SetValue(Grid.RowProperty, 50);
            DRBE_Start_bt.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(DRBE_Start_bt);

            DRBE_Pause_bt = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Content = "Pause",
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            DRBE_Pause_bt.SetValue(Grid.ColumnProperty, 111);
            DRBE_Pause_bt.SetValue(Grid.ColumnSpanProperty, 15);
            DRBE_Pause_bt.SetValue(Grid.RowProperty, 60);
            DRBE_Pause_bt.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(DRBE_Pause_bt);

            DRBE_Stop_bt = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Content = "Stop",
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            DRBE_Stop_bt.SetValue(Grid.ColumnProperty, 127);
            DRBE_Stop_bt.SetValue(Grid.ColumnSpanProperty, 15);
            DRBE_Stop_bt.SetValue(Grid.RowProperty, 30);
            DRBE_Stop_bt.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(DRBE_Stop_bt);

            DRBE_Refresh_bt = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Content = "Refresh",
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            DRBE_Refresh_bt.SetValue(Grid.ColumnProperty, 127);
            DRBE_Refresh_bt.SetValue(Grid.ColumnSpanProperty, 15);
            DRBE_Refresh_bt.SetValue(Grid.RowProperty, 40);
            DRBE_Refresh_bt.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(DRBE_Refresh_bt);
            #endregion
            #region Message
            Message_tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                Text = "Messages: \r\n"
            };
            Message_tb.SetValue(Grid.ColumnProperty, 161);
            Message_tb.SetValue(Grid.ColumnSpanProperty, 39);
            Message_tb.SetValue(Grid.RowProperty, 20);
            Message_tb.SetValue(Grid.RowSpanProperty, 130);
            ParentGrid.Children.Add(Message_tb);

            Message_bd = new Border()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Canvas.SetZIndex(Message_bd, -1);
            Message_bd.SetValue(Grid.ColumnProperty, 160);
            Message_bd.SetValue(Grid.ColumnSpanProperty, 400);
            Message_bd.SetValue(Grid.RowProperty, 20);
            Message_bd.SetValue(Grid.RowSpanProperty, 130);
            ParentGrid.Children.Add(Message_bd);
            #endregion 
        }

        public void Show()
        {
            Server_title_tb.Visibility = Visibility.Visible;
            Server_bd.Visibility = Visibility.Visible;
            Server_matlab_title_tb.Visibility = Visibility.Visible;
            Server_matlab_tb.Visibility = Visibility.Visible;
            Server_ui_title_tb.Visibility = Visibility.Visible;
            Server_ui_tb.Visibility = Visibility.Visible;
            Server_python_title_tb.Visibility = Visibility.Visible;
            Server_python_tb.Visibility = Visibility.Visible;
            Server_fpga_title_tb.Visibility = Visibility.Visible;
            Server_fpga_tb.Visibility = Visibility.Visible;
            Hardware_title_tb.Visibility = Visibility.Visible;
            Hardware_bd.Visibility = Visibility.Visible;
            Hardware_riskv_title_tb.Visibility = Visibility.Visible;
            Hardware_riskv_tb.Visibility = Visibility.Visible;
            Hardware_controlmodule_title_tb.Visibility = Visibility.Visible;
            Hardware_controlmodule_tb.Visibility = Visibility.Visible;
            Hardware_filter1_title_tb.Visibility = Visibility.Visible;
            Hardware_filter1_tb.Visibility = Visibility.Visible;
            Hardware_filter2_title_tb.Visibility = Visibility.Visible;
            Hardware_filter2_tb.Visibility = Visibility.Visible;
            Hardware_filter3_title_tb.Visibility = Visibility.Visible;
            Hardware_filter3_tb.Visibility = Visibility.Visible;
            Hardware_module1_title_tb.Visibility = Visibility.Visible;
            Hardware_module1_tb.Visibility = Visibility.Visible;
            Hardware_module2_title_tb.Visibility = Visibility.Visible;
            Hardware_module2_tb.Visibility = Visibility.Visible;
            Hardware_module3_title_tb.Visibility = Visibility.Visible;
            Hardware_module3_tb.Visibility = Visibility.Visible;
            IO_title_tb.Visibility = Visibility.Visible;
            IO_bd.Visibility = Visibility.Visible;
            IO_rf_title_tb.Visibility = Visibility.Visible;
            IO_rf_tb.Visibility = Visibility.Visible;
            IO_scenario_title_tb.Visibility = Visibility.Visible;
            IO_scenario_tb.Visibility = Visibility.Visible;
            IO_compiler_title_tb.Visibility = Visibility.Visible;
            IO_compiler_tb.Visibility = Visibility.Visible;
            IO_firmware_title_tb.Visibility = Visibility.Visible;
            IO_firmware_tb.Visibility = Visibility.Visible;
            DRBE_Command_title_tb.Visibility = Visibility.Visible;
            DRBE_Command_bd.Visibility = Visibility.Visible;
            DRBE_Initialization_bt.Visibility = Visibility.Visible;
            DRBE_Import_bt.Visibility = Visibility.Visible;
            DRBE_Start_bt.Visibility = Visibility.Visible;
            DRBE_Pause_bt.Visibility = Visibility.Visible;
            DRBE_Stop_bt.Visibility = Visibility.Visible;
            DRBE_Refresh_bt.Visibility = Visibility.Visible;
            Message_tb.Visibility = Visibility.Visible;
            Message_bd.Visibility = Visibility.Visible;

        }

        public void Hide()
        {
            Server_title_tb.Visibility = Visibility.Collapsed;
            Server_bd.Visibility = Visibility.Collapsed;
            Server_matlab_title_tb.Visibility = Visibility.Collapsed;
            Server_matlab_tb.Visibility = Visibility.Collapsed;
            Server_ui_title_tb.Visibility = Visibility.Collapsed;
            Server_ui_tb.Visibility = Visibility.Collapsed;
            Server_python_title_tb.Visibility = Visibility.Collapsed;
            Server_python_tb.Visibility = Visibility.Collapsed;
            Server_fpga_title_tb.Visibility = Visibility.Collapsed;
            Server_fpga_tb.Visibility = Visibility.Collapsed;
            Hardware_title_tb.Visibility = Visibility.Collapsed;
            Hardware_bd.Visibility = Visibility.Collapsed;
            Hardware_riskv_title_tb.Visibility = Visibility.Collapsed;
            Hardware_riskv_tb.Visibility = Visibility.Collapsed;
            Hardware_controlmodule_title_tb.Visibility = Visibility.Collapsed;
            Hardware_controlmodule_tb.Visibility = Visibility.Collapsed;
            Hardware_filter1_title_tb.Visibility = Visibility.Collapsed;
            Hardware_filter1_tb.Visibility = Visibility.Collapsed;
            Hardware_filter2_title_tb.Visibility = Visibility.Collapsed;
            Hardware_filter2_tb.Visibility = Visibility.Collapsed;
            Hardware_filter3_title_tb.Visibility = Visibility.Collapsed;
            Hardware_filter3_tb.Visibility = Visibility.Collapsed;
            Hardware_module1_title_tb.Visibility = Visibility.Collapsed;
            Hardware_module1_tb.Visibility = Visibility.Collapsed;
            Hardware_module2_title_tb.Visibility = Visibility.Collapsed;
            Hardware_module2_tb.Visibility = Visibility.Collapsed;
            Hardware_module3_title_tb.Visibility = Visibility.Collapsed;
            Hardware_module3_tb.Visibility = Visibility.Collapsed;
            IO_title_tb.Visibility = Visibility.Collapsed;
            IO_bd.Visibility = Visibility.Collapsed;
            IO_rf_title_tb.Visibility = Visibility.Collapsed;
            IO_rf_tb.Visibility = Visibility.Collapsed;
            IO_scenario_title_tb.Visibility = Visibility.Collapsed;
            IO_scenario_tb.Visibility = Visibility.Collapsed;
            IO_compiler_title_tb.Visibility = Visibility.Collapsed;
            IO_compiler_tb.Visibility = Visibility.Collapsed;
            IO_firmware_title_tb.Visibility = Visibility.Collapsed;
            IO_firmware_tb.Visibility = Visibility.Collapsed;
            DRBE_Command_title_tb.Visibility = Visibility.Collapsed;
            DRBE_Command_bd.Visibility = Visibility.Collapsed;
            DRBE_Initialization_bt.Visibility = Visibility.Collapsed;
            DRBE_Import_bt.Visibility = Visibility.Collapsed;
            DRBE_Start_bt.Visibility = Visibility.Collapsed;
            DRBE_Pause_bt.Visibility = Visibility.Collapsed;
            DRBE_Stop_bt.Visibility = Visibility.Collapsed;
            DRBE_Refresh_bt.Visibility = Visibility.Collapsed;
            Message_tb.Visibility = Visibility.Collapsed;
            Message_bd.Visibility = Visibility.Collapsed;
        }

        public void Dispose()
        {

        }
    }

}
