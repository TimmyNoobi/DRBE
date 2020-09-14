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
    public class DRBE_Link_Viewer_s
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
        public MainPage ParentPage;
        private Save_Screen DRBE_LV_SS;
        private Sim_sweep DRBE_sweep;
        public DRBE_Link_Viewer_s(Grid parent, MainPage parentpage)
        {
            ParentGrid = parent;
            ParentPage = parentpage;
            DRBE_LV_SS = new Save_Screen(parent);
            //Setup();
            hide();
        }

        private TextBlock Sweep_tb = new TextBlock();
        private Button Sweep_bt = new Button();
        private Image Sweep_i = new Image();

        private TextBlock Obj_save_tb = new TextBlock();
        private Button Obj_save_bt = new Button();
        private Image Obj_save_i = new Image();

        private TextBlock Obj_summary_tb = new TextBlock();
        private Button Obj_summary_bt = new Button();
        private Image Obj_summary_i = new Image();

        private List<Button> DT_bt = new List<Button>();
        private List<Image> DT_i = new List<Image>();
        private List<TextBlock> DT_tb = new List<TextBlock>();

        private List<Button> DR_bt = new List<Button>();
        private List<Image> DR_i = new List<Image>();
        private List<TextBlock> DR_tb = new List<TextBlock>();

        private List<Button> DF_bt = new List<Button>();
        private List<Image> DF_i = new List<Image>();
        private List<TextBlock> DF_tb = new List<TextBlock>();

        private int bt_height = 10;
        private int bt_width = 20;
        private int tb_height = 5;

        private int EndInd_T = 8;

        private Dictionary<Button, DRBE_Transmitter> D_BT_DT = new Dictionary<Button, DRBE_Transmitter>();
        private Dictionary<Button, DRBE_Receiver> D_BT_DR = new Dictionary<Button, DRBE_Receiver>();
        private Dictionary<Button, DRBE_Reflector> D_BT_DRF = new Dictionary<Button, DRBE_Reflector>();

        private TextBlock Transmitter_title_tb = new TextBlock();
        private TextBlock Reflector_title_tb = new TextBlock();
        private TextBlock Receiver_title_tb = new TextBlock();

        private Image Ref_Overview_i = new Image();
        private Button Ref_Overview_bt = new Button();

        private Image Ref_Antenna_i = new Image();
        private Button Ref_Antenna_bt = new Button();

        private Image Ref_Doppler_i = new Image();
        private Button Ref_Doppler_bt = new Button();

        private Image Ref_Polarization_i = new Image();
        private Button Ref_Polarization_bt = new Button();

        private Image Ref_RCS_i = new Image();
        private Button Ref_RCS_bt = new Button();

        private Image Ref_RFim_i = new Image();
        private Button Ref_RFim_bt = new Button();

        private Image Ref_Clutter_i = new Image();
        private Button Ref_Clutter_bt = new Button();

        private Button Ref_AO0_bt = new Button();
        private TextBlock Ref_AO0_tb = new TextBlock();

        private Button Ref_AO1_bt = new Button();
        private TextBlock Ref_AO1_tb = new TextBlock();

        private Button Ref_AO2_bt = new Button();
        private TextBlock Ref_AO2_tb = new TextBlock();

        private Button Ref_AO3_bt = new Button();
        private TextBlock Ref_AO3_tb = new TextBlock();

        private Button Ref_AO4_bt = new Button();
        private TextBlock Ref_AO4_tb = new TextBlock();

        private Button Ref_AO5_bt = new Button();
        private TextBlock Ref_AO5_tb = new TextBlock();

        private Button Ref_AO6_bt = new Button();
        private TextBlock Ref_AO6_tb = new TextBlock();

        private Border Tool_bd = new Border();
        private TextBlock Text_tb = new TextBlock();

        private Button Report_bt = new Button();

        private Button Simulation_bt = new Button();
        private Image Simulation_i = new Image();

        private Button Plot_bt = new Button();

        private TextBlock Link_de_tb = new TextBlock();
        private Button Link_de_bt = new Button();
        private Image Link_de_bt_i = new Image();

        private TextBlock Link_en_tb = new TextBlock();
        private Button Link_en_bt = new Button();
        private Image Link_en_bt_i = new Image();

        private TextBlock Link_uns_tb = new TextBlock();
        private Button Link_uns_bt = new Button();
        private Image Link_uns_bt_i = new Image();

        public List<DRBE_Transmitter> Lv_dtl = new List<DRBE_Transmitter>();
        public List<DRBE_Reflector> Lv_dfl = new List<DRBE_Reflector>();
        public List<DRBE_Receiver> Lv_drl = new List<DRBE_Receiver>();

        private Button AMT_bt = new Button();
        private Image AMT_bti = new Image();
        private TextBlock AMT_tb = new TextBlock();

        private Button CMP_bt = new Button();
        private Image CMP_bti = new Image();
        private TextBlock CMP_tb = new TextBlock();
        public void Setup(List<DRBE_Transmitter> DT, List<DRBE_Receiver> DR, List<DRBE_Reflector> DRF)
        {

            Lv_dtl = DT;
            Lv_dfl = DRF;
            Lv_drl = DR;


            Lv_dtl.Add(new DRBE_Transmitter());
            Lv_dfl.Add(new DRBE_Reflector());
            Lv_drl.Add(new DRBE_Receiver());
            int i = 0;
            int ii = 0;
            int iii = 0;

            CMP_bti.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Compiler.png", UriKind.RelativeOrAbsolute));
            CMP_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = CMP_bti,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(CMP_bt);
            CMP_bt.SetValue(Grid.ColumnProperty, 160);
            CMP_bt.SetValue(Grid.ColumnSpanProperty, 20);
            CMP_bt.SetValue(Grid.RowProperty, 100);
            CMP_bt.SetValue(Grid.RowSpanProperty, 20);
            CMP_bt.Click += CMP_bt_Click;


            AMT_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Run AMT",
                Foreground = white_button_brush,
                FontSize = 18,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(AMT_tb);
            AMT_tb.SetValue(Grid.ColumnProperty, 70);
            AMT_tb.SetValue(Grid.ColumnSpanProperty, 30);
            AMT_tb.SetValue(Grid.RowProperty, 15);
            AMT_tb.SetValue(Grid.RowSpanProperty, 5);

            AMT_bti.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Calculator_icon.png", UriKind.RelativeOrAbsolute));
            AMT_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = AMT_bti,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(AMT_bt);
            AMT_bt.SetValue(Grid.ColumnProperty, 70);
            AMT_bt.SetValue(Grid.ColumnSpanProperty, 30);
            AMT_bt.SetValue(Grid.RowProperty, 20);
            AMT_bt.SetValue(Grid.RowSpanProperty, 10);
            AMT_bt.Click += AMT_bt_Click;

            Obj_save_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Save_icon.png", UriKind.RelativeOrAbsolute));
            Obj_save_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Obj_save_i,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Obj_save_bt);
            Obj_save_bt.SetValue(Grid.ColumnProperty, 180);
            Obj_save_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Obj_save_bt.SetValue(Grid.RowProperty, 0);
            Obj_save_bt.SetValue(Grid.RowSpanProperty, 10);
            Obj_save_bt.Click += Obj_save_bt_Click;

            Obj_summary_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Report_icon.png", UriKind.RelativeOrAbsolute));
            Obj_summary_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Obj_summary_i,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Obj_summary_bt);
            Obj_summary_bt.SetValue(Grid.ColumnProperty, 160);
            Obj_summary_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Obj_summary_bt.SetValue(Grid.RowProperty, 0);
            Obj_summary_bt.SetValue(Grid.RowSpanProperty, 10);
            Obj_summary_bt.Click += Obj_summary_bt_Click;

            Sweep_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Simulation_icon.png", UriKind.RelativeOrAbsolute));
            Sweep_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Sweep_i,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Sweep_bt);
            Sweep_bt.SetValue(Grid.ColumnProperty, 140);
            Sweep_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Sweep_bt.SetValue(Grid.RowProperty, 0);
            Sweep_bt.SetValue(Grid.RowSpanProperty, 10);
            Sweep_bt.Click += Sweep_bt_Click;

            //Simulation_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/surfaceplot_CUTS.png", UriKind.RelativeOrAbsolute));
            //Simulation_bt = new Button()
            //{
            //    VerticalAlignment = VerticalAlignment.Stretch,
            //    HorizontalAlignment = HorizontalAlignment.Stretch,
            //    VerticalContentAlignment = VerticalAlignment.Stretch,
            //    HorizontalContentAlignment = HorizontalAlignment.Stretch,
            //    Background = Default_back_black_color_brush,
            //    Content = Simulation_i,
            //    Foreground = white_button_brush,
            //    Padding = new Thickness(15, 15, 15, 15),
            //    FontSize = 18
            //};
            //ParentGrid.Children.Add(Simulation_bt);
            //Simulation_bt.SetValue(Grid.ColumnProperty, 130);
            //Simulation_bt.SetValue(Grid.ColumnSpanProperty, 15);
            //Simulation_bt.SetValue(Grid.RowProperty, 125);
            //Simulation_bt.SetValue(Grid.RowSpanProperty, 15);

            Text_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Total Resource Utilization",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Text_tb);
            Text_tb.SetValue(Grid.ColumnProperty, 160);
            Text_tb.SetValue(Grid.ColumnSpanProperty, 50);
            Text_tb.SetValue(Grid.RowProperty, 20);
            Text_tb.SetValue(Grid.RowSpanProperty, 80);


            #region Order

            Ref_AO0_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,

                Background = Default_back_black_color_brush,
                Content = "Order 0:  Available",
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                FontSize = 18
            };
            ParentGrid.Children.Add(Ref_AO0_bt);
            Ref_AO0_bt.SetValue(Grid.ColumnProperty, 125);
            Ref_AO0_bt.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO0_bt.SetValue(Grid.RowProperty, 10);
            Ref_AO0_bt.SetValue(Grid.RowSpanProperty, 5);
            Ref_AO0_bt.Click += Ref_AO_bt_Click;

            Ref_AO0_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Resource Requirement: \r\n Hardware: 0 KB \r\n Computational: 0 FLOP",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Ref_AO0_tb);
            Ref_AO0_tb.SetValue(Grid.ColumnProperty, 125);
            Ref_AO0_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO0_tb.SetValue(Grid.RowProperty, 15);
            Ref_AO0_tb.SetValue(Grid.RowSpanProperty, 15);

            Ref_AO1_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,

                Background = Default_back_black_color_brush,
                Content = "Order 1:  Available",
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                FontSize = 18
            };
            ParentGrid.Children.Add(Ref_AO1_bt);
            Ref_AO1_bt.SetValue(Grid.ColumnProperty, 125);
            Ref_AO1_bt.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO1_bt.SetValue(Grid.RowProperty, 30);
            Ref_AO1_bt.SetValue(Grid.RowSpanProperty, 5);
            Ref_AO1_bt.Click += Ref_AO_bt_Click;

            Ref_AO1_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Resource Requirement: \r\n Hardware: 0 KB \r\n Computational: 0 FLOP",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Ref_AO1_tb);
            Ref_AO1_tb.SetValue(Grid.ColumnProperty, 125);
            Ref_AO1_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO1_tb.SetValue(Grid.RowProperty, 35);
            Ref_AO1_tb.SetValue(Grid.RowSpanProperty, 15);

            Ref_AO2_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,

                Background = Default_back_black_color_brush,
                Content = "Order 2:  Available",
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                FontSize = 18
            };
            ParentGrid.Children.Add(Ref_AO2_bt);
            Ref_AO2_bt.SetValue(Grid.ColumnProperty, 125);
            Ref_AO2_bt.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO2_bt.SetValue(Grid.RowProperty, 50);
            Ref_AO2_bt.SetValue(Grid.RowSpanProperty, 5);
            Ref_AO2_bt.Click += Ref_AO_bt_Click;

            Ref_AO2_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Resource Requirement: \r\n Hardware: 0 KB \r\n Computational: 0 FLOP",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Ref_AO2_tb);
            Ref_AO2_tb.SetValue(Grid.ColumnProperty, 125);
            Ref_AO2_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO2_tb.SetValue(Grid.RowProperty, 55);
            Ref_AO2_tb.SetValue(Grid.RowSpanProperty, 15);

            Ref_AO3_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,

                Background = Default_back_black_color_brush,
                Content = "Order 3:  Available",
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                FontSize = 18
            };
            ParentGrid.Children.Add(Ref_AO3_bt);
            Ref_AO3_bt.SetValue(Grid.ColumnProperty, 125);
            Ref_AO3_bt.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO3_bt.SetValue(Grid.RowProperty, 70);
            Ref_AO3_bt.SetValue(Grid.RowSpanProperty, 5);
            Ref_AO3_bt.Click += Ref_AO_bt_Click;

            Ref_AO3_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Resource Requirement: \r\n Hardware: 0 KB \r\n Computational: 0 FLOP",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Ref_AO3_tb);
            Ref_AO3_tb.SetValue(Grid.ColumnProperty, 125);
            Ref_AO3_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO3_tb.SetValue(Grid.RowProperty, 75);
            Ref_AO3_tb.SetValue(Grid.RowSpanProperty, 15);

            Ref_AO4_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,

                Background = Default_back_black_color_brush,
                Content = "Order 4:  Available",
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                FontSize = 18
            };
            ParentGrid.Children.Add(Ref_AO4_bt);
            Ref_AO4_bt.SetValue(Grid.ColumnProperty, 125);
            Ref_AO4_bt.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO4_bt.SetValue(Grid.RowProperty, 90);
            Ref_AO4_bt.SetValue(Grid.RowSpanProperty, 5);
            Ref_AO4_bt.Click += Ref_AO_bt_Click;

            Ref_AO4_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Resource Requirement: \r\n Hardware: 0 KB \r\n Computational: 0 FLOP",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Ref_AO4_tb);
            Ref_AO4_tb.SetValue(Grid.ColumnProperty, 125);
            Ref_AO4_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO4_tb.SetValue(Grid.RowProperty, 95);
            Ref_AO4_tb.SetValue(Grid.RowSpanProperty, 15);

            Ref_AO5_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,

                Background = Default_back_black_color_brush,
                Content = "Order 5:  Available",
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                FontSize = 18
            };
            ParentGrid.Children.Add(Ref_AO5_bt);
            Ref_AO5_bt.SetValue(Grid.ColumnProperty, 125);
            Ref_AO5_bt.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO5_bt.SetValue(Grid.RowProperty, 110);
            Ref_AO5_bt.SetValue(Grid.RowSpanProperty, 5);
            Ref_AO5_bt.Click += Ref_AO_bt_Click;

            Ref_AO5_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Resource Requirement: \r\n Hardware: 0 KB \r\n Computational: 0 FLOP",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Ref_AO5_tb);
            Ref_AO5_tb.SetValue(Grid.ColumnProperty, 125);
            Ref_AO5_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO5_tb.SetValue(Grid.RowProperty, 115);
            Ref_AO5_tb.SetValue(Grid.RowSpanProperty, 15);

            Ref_AO6_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,

                Background = Default_back_black_color_brush,
                Content = "Order 6:  Available",
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                FontSize = 18
            };
            ParentGrid.Children.Add(Ref_AO6_bt);
            Ref_AO6_bt.SetValue(Grid.ColumnProperty, 125);
            Ref_AO6_bt.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO6_bt.SetValue(Grid.RowProperty, 130);
            Ref_AO6_bt.SetValue(Grid.RowSpanProperty, 5);
            Ref_AO6_bt.Click += Ref_AO_bt_Click;

            Ref_AO6_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Resource Requirement: \r\n Hardware: 0 KB \r\n Computational: 0 FLOP",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Ref_AO6_tb);
            Ref_AO6_tb.SetValue(Grid.ColumnProperty, 125);
            Ref_AO6_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO6_tb.SetValue(Grid.RowProperty, 135);
            Ref_AO6_tb.SetValue(Grid.RowSpanProperty, 15);

            Transmitter_title_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Transmitter",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Transmitter_title_tb);
            Transmitter_title_tb.SetValue(Grid.ColumnProperty, 5);
            Transmitter_title_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Transmitter_title_tb.SetValue(Grid.RowProperty, 5);
            Transmitter_title_tb.SetValue(Grid.RowSpanProperty, 5);

            Reflector_title_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Reflector",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Reflector_title_tb);
            Reflector_title_tb.SetValue(Grid.ColumnProperty, 25);
            Reflector_title_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Reflector_title_tb.SetValue(Grid.RowProperty, 5);
            Reflector_title_tb.SetValue(Grid.RowSpanProperty, 5);

            Receiver_title_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Receiver",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Receiver_title_tb);
            Receiver_title_tb.SetValue(Grid.ColumnProperty, 45);
            Receiver_title_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Receiver_title_tb.SetValue(Grid.RowProperty, 5);
            Receiver_title_tb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

            #region DT
            i = 0;
            while (i < DT.Count && i < EndInd_T)
            {
                DT_i.Add(new Image());
                if (DT[i].Type == "Phase Array Radar")
                {
                    DT_i[DT_i.Count - 1].Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Phase_Array_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (DT[i].Type == "Mechanical Radar")
                {
                    DT_i[DT_i.Count - 1].Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/radar_icon_t.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    DT_i[DT_i.Count - 1].Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/radar_icon_t.png", UriKind.RelativeOrAbsolute));
                }



                DT_bt.Add(new Button()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    Background = Default_back_black_color_brush,
                    Content = DT_i[DT_i.Count - 1],
                    Foreground = white_button_brush,
                    BorderBrush = dark_grey_brush,
                    BorderThickness = new Thickness(2,2,2,2),
                    //Padding = new Thickness(15, 15, 15, 15),
                    FontSize = 18
                });
                ParentGrid.Children.Add(DT_bt[DT_i.Count - 1]);
                DT_bt[DT_i.Count - 1].SetValue(Grid.ColumnProperty, 10);
                DT_bt[DT_i.Count - 1].SetValue(Grid.ColumnSpanProperty, 10);
                DT_bt[DT_i.Count - 1].SetValue(Grid.RowProperty, 30 + bt_height * i + tb_height * i);
                DT_bt[DT_i.Count - 1].SetValue(Grid.RowSpanProperty, 10);
                D_BT_DT[DT_bt[DT_i.Count - 1]] = DT[i];
                DT_bt[DT_i.Count - 1].Click += DT_bt_Click;
                Dic_tbt_i[DT_bt[DT_i.Count - 1]] = i;


                DT_tb.Add(new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = 14,
                    FontWeight = FontWeights.Bold,
                    Foreground = Sky_green_color

                });
                ParentGrid.Children.Add(DT_tb[DT_tb.Count - 1]);
                DT_tb[DT_tb.Count - 1].SetValue(Grid.ColumnProperty, 10);
                DT_tb[DT_tb.Count - 1].SetValue(Grid.ColumnSpanProperty, 10);
                DT_tb[DT_tb.Count - 1].SetValue(Grid.RowProperty, 40 + bt_height * i + tb_height * i);
                DT_tb[DT_tb.Count - 1].SetValue(Grid.RowSpanProperty, 10);
                DT_tb[DT_tb.Count - 1].Text = "ID: " + DT[i].ID.ToString();
                i++;
            }
            #endregion

            #region DRF
            i = 0;
            while (i < DRF.Count && i < EndInd_T)
            {
                DF_i.Add(new Image());
                DF_i[DF_i.Count - 1].Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Object_icon.png", UriKind.RelativeOrAbsolute));
                DF_bt.Add(new Button()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    Background = Default_back_black_color_brush,
                    Content = DF_i[DF_i.Count - 1],
                    Foreground = white_button_brush,
                    BorderBrush = dark_grey_brush,
                    BorderThickness = new Thickness(2, 2, 2, 2),
                    //Padding = new Thickness(15, 15, 15, 15),
                    FontSize = 18
                });
                ParentGrid.Children.Add(DF_bt[DF_i.Count - 1]);
                DF_bt[DF_i.Count - 1].SetValue(Grid.ColumnProperty, 30);
                DF_bt[DF_i.Count - 1].SetValue(Grid.ColumnSpanProperty, 10);
                DF_bt[DF_i.Count - 1].SetValue(Grid.RowProperty, 30 + bt_height * i + tb_height * i);
                DF_bt[DF_i.Count - 1].SetValue(Grid.RowSpanProperty, 10);
                D_BT_DRF[DF_bt[DF_i.Count - 1]] = DRF[i];
                DF_bt[DF_i.Count - 1].Click += DF_bt_Click;

                Dic_pbt_i[DF_bt[DF_i.Count - 1]] = i;

                DF_tb.Add(new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = 14,
                    FontWeight = FontWeights.Bold,
                    Foreground = Sky_green_color

                });
                ParentGrid.Children.Add(DF_tb[DF_tb.Count - 1]);
                DF_tb[DF_tb.Count - 1].SetValue(Grid.ColumnProperty, 30);
                DF_tb[DF_tb.Count - 1].SetValue(Grid.ColumnSpanProperty, 10);
                DF_tb[DF_tb.Count - 1].SetValue(Grid.RowProperty, 40 + bt_height * i + tb_height * i);
                DF_tb[DF_tb.Count - 1].SetValue(Grid.RowSpanProperty, 10);
                DF_tb[DF_tb.Count - 1].Text = "ID: " + DRF[i].ID.ToString();
                i++;
            }
            #endregion

            #region DR
            i = 0;
            while (i < DR.Count && i < EndInd_T)
            {
                DR_i.Add(new Image());
                if (DR[i].Type == "Phase Array Radar")
                {
                    DR_i[DR_i.Count - 1].Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Phase_Array_icon_t.png", UriKind.RelativeOrAbsolute));
                }
                else if (DR[i].Type == "Mechanical Radar")
                {
                    DR_i[DR_i.Count - 1].Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/radar_icon_m.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    DR_i[DR_i.Count - 1].Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/radar_icon_m.png", UriKind.RelativeOrAbsolute));
                }

                DR_bt.Add(new Button()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    Background = Default_back_black_color_brush,
                    Content = DR_i[DR_i.Count - 1],
                    Foreground = white_button_brush,
                    BorderBrush = dark_grey_brush,
                    BorderThickness = new Thickness(2, 2, 2, 2),
                    //Padding = new Thickness(15, 15, 15, 15),
                    FontSize = 18
                });
                ParentGrid.Children.Add(DR_bt[DR_i.Count - 1]);
                DR_bt[DR_i.Count - 1].SetValue(Grid.ColumnProperty, 50);
                DR_bt[DR_i.Count - 1].SetValue(Grid.ColumnSpanProperty, 10);
                DR_bt[DR_i.Count - 1].SetValue(Grid.RowProperty, 30 + bt_height * i + tb_height * i);
                DR_bt[DR_i.Count - 1].SetValue(Grid.RowSpanProperty, 10);
                D_BT_DR[DR_bt[DR_i.Count - 1]] = DR[i];
                DR_bt[DR_i.Count - 1].Click += DR_bt_Click;
                Dic_rbt_i[DR_bt[DR_i.Count - 1]] = i;

                DR_tb.Add(new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = 14,
                    FontWeight = FontWeights.Bold,
                    Foreground = Sky_green_color

                });
                ParentGrid.Children.Add(DR_tb[DR_tb.Count - 1]);
                DR_tb[DR_tb.Count - 1].SetValue(Grid.ColumnProperty, 50);
                DR_tb[DR_tb.Count - 1].SetValue(Grid.ColumnSpanProperty, 10);
                DR_tb[DR_tb.Count - 1].SetValue(Grid.RowProperty, 40 + bt_height * i + tb_height * i);
                DR_tb[DR_tb.Count - 1].SetValue(Grid.RowSpanProperty, 10);
                DR_tb[DR_tb.Count - 1].Text = "ID: " + DR[i].ID.ToString();
                i++;
            }
            #endregion

            #region Link deactive
            Link_de_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = Sky_green_color,
                Text = "Disable All"

            };
            ParentGrid.Children.Add(Link_de_tb);
            Link_de_tb.SetValue(Grid.ColumnProperty, 70);
            Link_de_tb.SetValue(Grid.ColumnSpanProperty, 25);
            Link_de_tb.SetValue(Grid.RowProperty, 30);
            Link_de_tb.SetValue(Grid.RowSpanProperty, 10);

            Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/All_de_icon.png", UriKind.RelativeOrAbsolute));
            Link_de_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Link_de_bt_i,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Link_de_bt);
            Link_de_bt.SetValue(Grid.ColumnProperty, 70);
            Link_de_bt.SetValue(Grid.ColumnSpanProperty, 25);
            Link_de_bt.SetValue(Grid.RowProperty, 40);
            Link_de_bt.SetValue(Grid.RowSpanProperty, 25);
            Link_de_bt.Click += Link_de_bt_Click;
            #endregion

            #region Link enactive
            Link_en_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = Sky_green_color,
                Text = "Enable All"

            };
            ParentGrid.Children.Add(Link_en_tb);
            Link_en_tb.SetValue(Grid.ColumnProperty, 70);
            Link_en_tb.SetValue(Grid.ColumnSpanProperty, 25);
            Link_en_tb.SetValue(Grid.RowProperty, 70);
            Link_en_tb.SetValue(Grid.RowSpanProperty, 10);

            Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/En_all_icon.png", UriKind.RelativeOrAbsolute));
            Link_en_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Link_en_bt_i,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Link_en_bt);
            Link_en_bt.SetValue(Grid.ColumnProperty, 70);
            Link_en_bt.SetValue(Grid.ColumnSpanProperty, 25);
            Link_en_bt.SetValue(Grid.RowProperty, 80);
            Link_en_bt.SetValue(Grid.RowSpanProperty, 25);
            Link_en_bt.Click += Link_en_bt_Click;
            #endregion

            #region Link unsure
            Link_uns_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = Sky_green_color,
                Text = "Low Priority"

            };
            ParentGrid.Children.Add(Link_uns_tb);
            Link_uns_tb.SetValue(Grid.ColumnProperty, 70);
            Link_uns_tb.SetValue(Grid.ColumnSpanProperty, 25);
            Link_uns_tb.SetValue(Grid.RowProperty, 110);
            Link_uns_tb.SetValue(Grid.RowSpanProperty, 10);

            Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Uns_all_icon.png", UriKind.RelativeOrAbsolute));
            Link_uns_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Link_uns_bt_i,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Link_uns_bt);
            Link_uns_bt.SetValue(Grid.ColumnProperty, 70);
            Link_uns_bt.SetValue(Grid.ColumnSpanProperty, 25);
            Link_uns_bt.SetValue(Grid.RowProperty, 120);
            Link_uns_bt.SetValue(Grid.RowSpanProperty, 25);
            Link_uns_bt.Click += Link_uns_bt_Click;
            #endregion 

            #region Reference model
            Ref_Antenna_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Antenna_Pattern_icon.png", UriKind.RelativeOrAbsolute));
            Ref_Antenna_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Ref_Antenna_i,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Ref_Antenna_bt);
            Ref_Antenna_bt.SetValue(Grid.ColumnProperty, 100);
            Ref_Antenna_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Ref_Antenna_bt.SetValue(Grid.RowProperty, 30);
            Ref_Antenna_bt.SetValue(Grid.RowSpanProperty, 15);
            Ref_Antenna_bt.Click += Ref_Antenna_bt_Click;

            Ref_Doppler_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Doppler_icon.png", UriKind.RelativeOrAbsolute));
            Ref_Doppler_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Ref_Doppler_i,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Ref_Doppler_bt);
            Ref_Doppler_bt.SetValue(Grid.ColumnProperty, 100);
            Ref_Doppler_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Ref_Doppler_bt.SetValue(Grid.RowProperty, 50);
            Ref_Doppler_bt.SetValue(Grid.RowSpanProperty, 15);
            Ref_Doppler_bt.Click += Ref_Doppler_bt_Click;

            Ref_Polarization_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Polarization_icon.png", UriKind.RelativeOrAbsolute));
            Ref_Polarization_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Ref_Polarization_i,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Ref_Polarization_bt);
            Ref_Polarization_bt.SetValue(Grid.ColumnProperty, 100);
            Ref_Polarization_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Ref_Polarization_bt.SetValue(Grid.RowProperty, 70);
            Ref_Polarization_bt.SetValue(Grid.RowSpanProperty, 15);
            Ref_Polarization_bt.Click += Ref_Polarization_bt_Click;

            Ref_RCS_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/RCS_icon.png", UriKind.RelativeOrAbsolute));
            Ref_RCS_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Ref_RCS_i,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Ref_RCS_bt);
            Ref_RCS_bt.SetValue(Grid.ColumnProperty, 100);
            Ref_RCS_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Ref_RCS_bt.SetValue(Grid.RowProperty, 90);
            Ref_RCS_bt.SetValue(Grid.RowSpanProperty, 15);
            Ref_RCS_bt.Click += Ref_RCS_bt_Click;

            Ref_RFim_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/RF_impairment_icon.jpg", UriKind.RelativeOrAbsolute));
            Ref_RFim_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Ref_RFim_i,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Ref_RFim_bt);
            Ref_RFim_bt.SetValue(Grid.ColumnProperty, 100);
            Ref_RFim_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Ref_RFim_bt.SetValue(Grid.RowProperty, 110);
            Ref_RFim_bt.SetValue(Grid.RowSpanProperty, 15);
            Ref_RFim_bt.Click += Ref_RFim_bt_Click;

            Ref_Clutter_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Clutter_icon.PNG", UriKind.RelativeOrAbsolute));
            Ref_Clutter_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Ref_Clutter_i,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Ref_Clutter_bt);
            Ref_Clutter_bt.SetValue(Grid.ColumnProperty, 100);
            Ref_Clutter_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Ref_Clutter_bt.SetValue(Grid.RowProperty, 130);
            Ref_Clutter_bt.SetValue(Grid.RowSpanProperty, 15);
            Ref_Clutter_bt.Click += Ref_Clutter_bt_Click;
            #endregion

            //DT_bt[2].BorderBrush = green_bright_button_brush;
            //DF_bt[5].BorderBrush = green_bright_button_brush;
            //DR_bt[1].BorderBrush = green_bright_button_brush;
            Ref_Antenna_bt.BorderBrush = green_bright_button_brush;
            Generate_Lists();
            #region read enable
            i = 0;
            while (i < Link_enable_list.Count)
            {
                ii = 0;
                while (ii < Link_enable_list[i].Count)
                {
                    iii = 0;
                    while (iii < Link_enable_list[i][ii].Count)
                    {
                        if (Link_enable_list[i][ii][iii])
                        {
                            DR_tb[iii].Foreground = green_bright_button_brush;
                            DF_tb[ii].Foreground = green_bright_button_brush;
                            DT_tb[i].Foreground = green_bright_button_brush;
                        }
                        iii++;
                    }
                    ii++;
                }
                i++;
            }
            #endregion
            Link_de_tb.Text = "Disable Scenario";
            Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/All_de_icon.png", UriKind.RelativeOrAbsolute));
            Link_en_tb.Text = "Enable Scenario";
            Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/En_all_icon.png", UriKind.RelativeOrAbsolute));
            Link_uns_tb.Text = "Less Priority Scenario";
            Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Uns_all_icon.png", UriKind.RelativeOrAbsolute));
            //testchain();

        }

        private async void CMP_bt_Click(object sender, RoutedEventArgs e)
        {
            await ShowDialog("Compilation result", "Compilation Succeed/ Failed \r\n \r\n--------------------------------------------------------\r\n \r\n  If design exceed DRBE capability: Suggestions (........) \r\n \r\n------------------------------------------\r\n \r\nIf DRBE is not fully utilized: Suggestions(.................): \r\n \r\n"  + Lv_dtl[0].Summary());
        }

        private void AMT_bt_Click(object sender, RoutedEventArgs e)
        {
            Ref_AO0_tb.Text = "Resource Requirement: \r\n Memory: 20 KB \r\n Computational: 10 FLOP \r\n IO Bandwidth:";
            Ref_AO1_tb.Text = "Resource Requirement: \r\n Memory: 122 KB \r\n Computational: 37 FLOP \r\n IO Bandwidth:";
            Ref_AO2_tb.Text = "Resource Requirement: \r\n Memory: 154 KB \r\n Computational: 55 FLOP \r\n IO Bandwidth:";
            Ref_AO3_tb.Text = "Resource Requirement: \r\n Memory: 263 KB \r\n Computational: 70 FLOP \r\n IO Bandwidth:";
            Ref_AO4_tb.Text = "Resource Requirement: \r\n Memory: 399 KB \r\n Computational: 730 FLOP \r\n IO Bandwidth:";
            Ref_AO4_tb.Foreground = orange_brush;
            Ref_AO5_tb.Text = "Resource Requirement: \r\n Memory: 22390 KB \r\n Computational: 120 FLOP \r\n IO Bandwidth:";
            Ref_AO5_tb.Foreground = orange_brush;
            Ref_AO6_tb.Text = "Resource Requirement: \r\n Memory: 00 KB \r\n Computational: 00 FLOP \r\n IO Bandwidth:";
            Text_tb.Text += "Total Resource Utilization: \r\n \r\n Memory 8944.53 MB / 800000 MB. -----   1.12 % \r\n \r\n";
            Text_tb.Text += "PPU:  34 units / 4500 units. -----   0.76 % \r\n \r\n";
            Text_tb.Text += "Computation:  00 / 00. -----   0.00 % \r\n \r\n";
            Text_tb.Text += "IO Bandwidth:  00 / 00. -----   0.00 % \r\n \r\n";
        }

        private void Ref_AO_bt_Click(object sender, RoutedEventArgs e)
        {
            Button foo = sender as Button;
            if(foo.BorderBrush == dark_grey_brush)
            {
                foo.BorderBrush = green_bright_button_brush;
            }
            else if (foo.BorderBrush == green_bright_button_brush)
            {
                foo.BorderBrush = orange_brush;
            }
            else if (foo.BorderBrush == orange_brush)
            {
                foo.BorderBrush = dark_grey_brush;
            }
        }

        private async void testchain()
        {
            hide();
            DRBE_sweep = new Sim_sweep(ParentGrid, ParentPage);
            DRBE_sweep.Property_setup(Lv_dtl, Lv_dfl, Lv_drl, Link_enable_list);
        }

        private void Ref_Clutter_bt_Click(object sender, RoutedEventArgs e)
        {
            Ref_bt_decolor();
            Button yoo = sender as Button;
            yoo.BorderBrush = green_bright_button_brush;
            if(T_bt_flag == false && R_bt_flag == false && P_bt_flag == false)
            {

            }
        }

        private void Ref_RFim_bt_Click(object sender, RoutedEventArgs e)
        {
            Ref_bt_decolor();
            Button yoo = sender as Button;
            yoo.BorderBrush = green_bright_button_brush;
        }

        private void Ref_RCS_bt_Click(object sender, RoutedEventArgs e)
        {
            Ref_bt_decolor();
            Button yoo = sender as Button;
            yoo.BorderBrush = green_bright_button_brush;
        }

        private void Ref_Polarization_bt_Click(object sender, RoutedEventArgs e)
        {
            Ref_bt_decolor();
            Button yoo = sender as Button;
            yoo.BorderBrush = green_bright_button_brush;
        }

        private void Ref_Doppler_bt_Click(object sender, RoutedEventArgs e)
        {
            Ref_bt_decolor();
            Button yoo = sender as Button;
            yoo.BorderBrush = green_bright_button_brush;
        }

        private void Ref_Antenna_bt_Click(object sender, RoutedEventArgs e)
        {
            Ref_bt_decolor();
            Button yoo = sender as Button;
            yoo.BorderBrush = green_bright_button_brush;
        }

        private void Ref_bt_decolor()
        {
            Ref_Antenna_bt.BorderBrush = Default_back_black_color_brush;
            Ref_Doppler_bt.BorderBrush = Default_back_black_color_brush;
            Ref_Polarization_bt.BorderBrush = Default_back_black_color_brush;
            Ref_RCS_bt.BorderBrush = Default_back_black_color_brush;
            Ref_RFim_bt.BorderBrush = Default_back_black_color_brush;
            Ref_Clutter_bt.BorderBrush = Default_back_black_color_brush;

            Ref_AO0_bt.BorderBrush = white_button_brush;
            Ref_AO1_bt.BorderBrush = white_button_brush;
            Ref_AO2_bt.BorderBrush = white_button_brush;
            Ref_AO3_bt.BorderBrush = white_button_brush;
            Ref_AO4_bt.BorderBrush = white_button_brush;
            Ref_AO5_bt.BorderBrush = white_button_brush;
            Ref_AO6_bt.BorderBrush = white_button_brush;
        }
        private async void Link_de_bt_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            int ii = 0;
            int iii = 0;

            if (T_bt_flag == false && R_bt_flag == false && P_bt_flag == false)
            {
                i = 0;
                while (i < Link_enable_list.Count)
                {
                    ii = 0;
                    while (ii < Link_enable_list[i].Count)
                    {
                        iii = 0;
                        while (iii < Link_enable_list[i][ii].Count)
                        {
                            Link_enable_list[i][ii][iii] = false;
                            iii++;
                        }
                        ii++;
                    }
                    i++;
                }

                i = 0;
                while(i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }

            }
            else if (T_bt_flag == false && R_bt_flag == false && P_bt_flag == true)
            {
                i = 0;
                while (i < Link_enable_list.Count)
                {
                    ii = BTP_Gindex;
                    iii = 0;
                    while (iii < Link_enable_list[i][ii].Count)
                    {
                        Link_enable_list[i][ii][iii] = false;
                        iii++;
                    }
                    i++;
                }

                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }
                DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
            }
            else if (T_bt_flag == false && R_bt_flag == true && P_bt_flag == false)
            {
                i = 0;
                while (i < Link_enable_list.Count)
                {
                    ii = 0;
                    while (ii < Link_enable_list[i].Count)
                    {
                        iii = BTR_Gindex;
                        Link_enable_list[i][ii][iii] = false;
                        ii++;
                    }
                    i++;
                }

                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }
                DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
            }
            else if (T_bt_flag == true && R_bt_flag == false && P_bt_flag == false)
            {
                i = BTT_Gindex;
                ii = 0;
                while (ii < Link_enable_list[i].Count)
                {
                    iii = 0;
                    while (iii < Link_enable_list[i][ii].Count)
                    {
                        Link_enable_list[i][ii][iii] = false;
                        iii++;
                    }
                    ii++;
                }
                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }
                DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;


            }
            else if (T_bt_flag == true && R_bt_flag == true && P_bt_flag == false)
            {
                i = BTT_Gindex;
                ii = 0;
                iii = BTR_Gindex;
                while (ii < Link_enable_list[i].Count)
                {
                    Link_enable_list[i][ii][iii] = false;
                    ii++;
                }

                DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }
                DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
            }
            else if (T_bt_flag == true && R_bt_flag == false && P_bt_flag == true)
            {
                i = BTT_Gindex;
                ii = BTP_Gindex;
                iii = 0;
                while (iii < Link_enable_list[i][ii].Count)
                {
                    Link_enable_list[i][ii][iii] = false;
                    iii++;
                }

                DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }
                DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
            }
            else if (T_bt_flag == false && R_bt_flag == true && P_bt_flag == true)
            {
                i = 0;
                ii = BTP_Gindex;
                iii = BTR_Gindex;
                while (i < Link_enable_list.Count)
                {
                    Link_enable_list[i][ii][iii] = false;
                    i++;
                }


                DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }
                DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;

            }
            else if (T_bt_flag == true && R_bt_flag == true && P_bt_flag == true)
            {
                i = BTT_Gindex;
                ii = BTP_Gindex;
                iii = BTR_Gindex;
                Link_enable_list[i][ii][iii] = false;
                DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
            }
            else
            {
                await ShowDialog("Error", "En Error");
            }
        }

        private async void Link_en_bt_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            int ii = 0;
            int iii = 0;

            if (T_bt_flag == false && R_bt_flag == false && P_bt_flag == false)
            {
                i = 0;
                while (i < Link_enable_list.Count)
                {
                    ii = 0;
                    while (ii < Link_enable_list[i].Count)
                    {
                        iii = 0;
                        while (iii < Link_enable_list[i][ii].Count)
                        {
                            Link_enable_list[i][ii][iii] = true;
                            iii++;
                        }
                        ii++;
                    }
                    i++;
                }
                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
            }
            else if (T_bt_flag == false && R_bt_flag == false && P_bt_flag == true)
            {
                i = 0;
                while (i < Link_enable_list.Count)
                {
                    ii = BTP_Gindex;
                    iii = 0;
                    while (iii < Link_enable_list[i][ii].Count)
                    {
                        Link_enable_list[i][ii][iii] = true;
                        iii++;
                    }
                    i++;
                }
                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DF_tb[BTP_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == false && R_bt_flag == true && P_bt_flag == false)
            {
                i = 0;
                while (i < Link_enable_list.Count)
                {
                    ii = 0;
                    while (ii < Link_enable_list[i].Count)
                    {
                        iii = BTR_Gindex;
                        Link_enable_list[i][ii][iii] = true;
                        ii++;
                    }
                    i++;
                }
                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DR_tb[BTR_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == true && R_bt_flag == false && P_bt_flag == false)
            {
                i = BTT_Gindex;
                ii = 0;
                while (ii < Link_enable_list[i].Count)
                {
                    iii = 0;
                    while (iii < Link_enable_list[i][ii].Count)
                    {
                        Link_enable_list[i][ii][iii] = true;
                        iii++;
                    }
                    ii++;
                }
                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DT_tb[BTT_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == true && R_bt_flag == true && P_bt_flag == false)
            {
                i = BTT_Gindex;
                ii = 0;
                iii = BTR_Gindex;
                while (ii < Link_enable_list[i].Count)
                {
                    Link_enable_list[i][ii][iii] = true;
                    ii++;
                }
                DR_tb[BTR_Gindex].Foreground = green_bright_button_brush;
                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DT_tb[BTT_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == true && R_bt_flag == false && P_bt_flag == true)
            {
                i = BTT_Gindex;
                ii = BTP_Gindex;
                iii = 0;
                while (iii < Link_enable_list[i][ii].Count)
                {
                    Link_enable_list[i][ii][iii] = true;
                    iii++;
                }
                DF_tb[BTP_Gindex].Foreground = green_bright_button_brush;

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DT_tb[BTT_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == false && R_bt_flag == true && P_bt_flag == true)
            {
                i = 0;
                ii = BTP_Gindex;
                iii = BTR_Gindex;
                while (i < Link_enable_list.Count)
                {
                    Link_enable_list[i][ii][iii] = true;
                    i++;
                }
                DR_tb[BTR_Gindex].Foreground = green_bright_button_brush;
                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DF_tb[BTP_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == true && R_bt_flag == true && P_bt_flag == true)
            {
                i = BTT_Gindex;
                ii = BTP_Gindex;
                iii = BTR_Gindex;
                Link_enable_list[i][ii][iii] = true;
                DR_tb[BTR_Gindex].Foreground = green_bright_button_brush;
                DF_tb[BTP_Gindex].Foreground = green_bright_button_brush;
                DT_tb[BTT_Gindex].Foreground = green_bright_button_brush;
            }
            else
            {
                await ShowDialog("Error", "DP Error");
            }
        }

        private async void Link_uns_bt_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            int ii = 0;
            int iii = 0;

            if (T_bt_flag == false && R_bt_flag == false && P_bt_flag == false)
            {
                i = 0;
                while (i < Link_enable_list.Count)
                {
                    ii = 0;
                    while (ii < Link_enable_list[i].Count)
                    {
                        iii = 0;
                        while (iii < Link_enable_list[i][ii].Count)
                        {
                            Link_enable_list[i][ii][iii] = true;
                            iii++;
                        }
                        ii++;
                    }
                    i++;
                }
                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
            }
            else if (T_bt_flag == false && R_bt_flag == false && P_bt_flag == true)
            {
                i = 0;
                while (i < Link_enable_list.Count)
                {
                    ii = BTP_Gindex;
                    iii = 0;
                    while (iii < Link_enable_list[i][ii].Count)
                    {
                        Link_enable_list[i][ii][iii] = true;
                        iii++;
                    }
                    i++;
                }
                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DF_tb[BTP_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == false && R_bt_flag == true && P_bt_flag == false)
            {
                i = 0;
                while (i < Link_enable_list.Count)
                {
                    ii = 0;
                    while (ii < Link_enable_list[i].Count)
                    {
                        iii = BTR_Gindex;
                        Link_enable_list[i][ii][iii] = true;
                        ii++;
                    }
                    i++;
                }
                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DR_tb[BTR_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == true && R_bt_flag == false && P_bt_flag == false)
            {
                i = BTT_Gindex;
                ii = 0;
                while (ii < Link_enable_list[i].Count)
                {
                    iii = 0;
                    while (iii < Link_enable_list[i][ii].Count)
                    {
                        Link_enable_list[i][ii][iii] = true;
                        iii++;
                    }
                    ii++;
                }
                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DT_tb[BTT_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == true && R_bt_flag == true && P_bt_flag == false)
            {
                i = BTT_Gindex;
                ii = 0;
                iii = BTR_Gindex;
                while (ii < Link_enable_list[i].Count)
                {
                    Link_enable_list[i][ii][iii] = true;
                    ii++;
                }
                DR_tb[BTR_Gindex].Foreground = green_bright_button_brush;
                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DT_tb[BTT_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == true && R_bt_flag == false && P_bt_flag == true)
            {
                i = BTT_Gindex;
                ii = BTP_Gindex;
                iii = 0;
                while (iii < Link_enable_list[i][ii].Count)
                {
                    Link_enable_list[i][ii][iii] = true;
                    iii++;
                }
                DF_tb[BTP_Gindex].Foreground = green_bright_button_brush;

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DT_tb[BTT_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == false && R_bt_flag == true && P_bt_flag == true)
            {
                i = 0;
                ii = BTP_Gindex;
                iii = BTR_Gindex;
                while (i < Link_enable_list.Count)
                {
                    Link_enable_list[i][ii][iii] = true;
                    i++;
                }
                DR_tb[BTR_Gindex].Foreground = green_bright_button_brush;
                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DF_tb[BTP_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == true && R_bt_flag == true && P_bt_flag == true)
            {
                i = BTT_Gindex;
                ii = BTP_Gindex;
                iii = BTR_Gindex;
                Link_enable_list[i][ii][iii] = true;
                DR_tb[BTR_Gindex].Foreground = green_bright_button_brush;
                DF_tb[BTP_Gindex].Foreground = green_bright_button_brush;
                DT_tb[BTT_Gindex].Foreground = green_bright_button_brush;
            }
            else
            {
                await ShowDialog("Error", "DP Error");
            }
        }

        private bool T_bt_flag = false;
        private bool P_bt_flag = false;
        private bool R_bt_flag = false;


        private Dictionary<Button, int> Dic_tbt_i = new Dictionary<Button, int>();
        private Dictionary<Button, int> Dic_pbt_i = new Dictionary<Button, int>();
        private Dictionary<Button, int> Dic_rbt_i = new Dictionary<Button, int>();

        public List<List<List<bool>>> Link_enable_list = new List<List<List<bool>>>();

        public List<List<List<ushort>>> Link_Antenna_list = new List<List<List<ushort>>>();
        public List<List<List<ushort>>> Link_RCS_list = new List<List<List<ushort>>>();
        public List<List<List<ushort>>> Link_RFimp_list = new List<List<List<ushort>>>();
        public List<List<List<ushort>>> Link_Polar_list = new List<List<List<ushort>>>();
        public List<List<List<ushort>>> Link_Clutter_list = new List<List<List<ushort>>>();
        public List<List<List<ushort>>> Link_Doppler_list = new List<List<List<ushort>>>();



        private void Generate_Lists()
        {
            int i = 0;
            int ii = 0;
            int iii = 0;
            i = 0;
            while(i<Lv_dtl.Count)
            {
                Link_enable_list.Add(new List<List<bool>>());
                Link_Antenna_list.Add(new List<List<ushort>>());
                Link_RCS_list.Add(new List<List<ushort>>());
                Link_RFimp_list.Add(new List<List<ushort>>());
                Link_Polar_list.Add(new List<List<ushort>>());
                Link_Clutter_list.Add(new List<List<ushort>>());
                Link_Doppler_list.Add(new List<List<ushort>>());
                ii = 0;
                while(ii<Lv_dfl.Count)
                {
                    Link_enable_list[i].Add(new List<bool>());
                    Link_Antenna_list[i].Add(new List<ushort>());
                    Link_RCS_list[i].Add(new List<ushort>());
                    Link_RFimp_list[i].Add(new List<ushort>());
                    Link_Polar_list[i].Add(new List<ushort>());
                    Link_Clutter_list[i].Add(new List<ushort>());
                    Link_Doppler_list[i].Add(new List<ushort>());
                    iii = 0;
                    while(iii<Lv_drl.Count)
                    {
                        Link_enable_list[i][ii].Add(true);
                        Link_Antenna_list[i][ii].Add(4);
                        Link_RCS_list[i][ii].Add(4);
                        Link_RFimp_list[i][ii].Add(4);
                        Link_Polar_list[i][ii].Add(4);
                        Link_Clutter_list[i][ii].Add(4);
                        Link_Doppler_list[i][ii].Add(4);
                        iii++;
                    }
                    ii++;
                }
                i++;
            }
            #region tb decolor
            i = 0;
            while (i < DT_tb.Count)
            {
                DT_tb[i].Foreground = red_bright_button_brush;
                i++;
            }
            i = 0;
            while (i < DF_tb.Count)
            {
                DF_tb[i].Foreground = red_bright_button_brush;
                i++;
            }
            i = 0;
            while (i < DR_tb.Count)
            {
                DR_tb[i].Foreground = red_bright_button_brush;
                i++;
            }
            #endregion
            #region read enable
            i = 0;
            while (i < Link_enable_list.Count)
            {
                ii = 0;
                while (ii < Link_enable_list[i].Count)
                {
                    iii = 0;
                    while (iii < Link_enable_list[i][ii].Count)
                    {
                        if (Link_enable_list[i][ii][iii])
                        {
                            DR_tb[iii].Foreground = Sky_green_color;
                            DF_tb[ii].Foreground = Sky_green_color;
                            DT_tb[i].Foreground = Sky_green_color;
                        }
                        iii++;
                    }
                    ii++;
                }
                i++;
            }
            #endregion

        }
        //List T  P  R;


        private void Lbt_decolor()
        {
            int i = 0;
            i = 0;
            while(i < DR_bt.Count)
            {
                DR_bt[i].BorderBrush = dark_grey_brush;
                i++;
            }

            i = 0;
            while (i < DT_bt.Count)
            {
                DT_bt[i].BorderBrush = dark_grey_brush;
                i++;
            }

            i = 0;
            while (i < DF_bt.Count)
            {
                DF_bt[i].BorderBrush = dark_grey_brush;
                i++;
            }
        }
        private int BTT_Gindex = 0;
        private int BTP_Gindex = 0;
        private int BTR_Gindex = 0;

        private int Gindex = 0;
        private async void DF_bt_Click(object sender, RoutedEventArgs e)
        {
            Button yoo = sender as Button;
            int i = 0;
            int ii = 0;
            int iii = 0;
            bool difflag = false;
            bool selfflag = false;
            bool All_de_flag = true;
            if(Gindex== BTP_Gindex)
            {
                difflag = false;
            }
            else
            {
                difflag = true;
            }
            BTP_Gindex = Dic_pbt_i[yoo];
            Gindex = BTP_Gindex;
            if (yoo.BorderBrush == dark_grey_brush)
            {
                if (T_bt_flag == false && R_bt_flag == false) //T W, R W, F W, change F G
                {
                    #region bt decolor
                    i = 0;
                    while (i < DF_bt.Count)
                    {
                        DF_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = 0;
                    while (i < Link_enable_list.Count)
                    {
                        ii = BTP_Gindex;
                        iii = 0;
                        while (iii < Link_enable_list[i][ii].Count)
                        {
                            if (Link_enable_list[i][ii][iii])
                            {
                                DR_tb[iii].Foreground = green_bright_button_brush;
                                DF_tb[ii].Foreground = green_bright_button_brush;
                                DT_tb[i].Foreground = green_bright_button_brush;
                            }
                            iii++;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Object";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Object";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == true && R_bt_flag == false) //T G, R W, F W, change F G
                {
                    #region bt decolor
                    i = 0;
                    while (i < DF_bt.Count)
                    {
                        DF_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = BTP_Gindex;
                    iii = 0;
                    while (iii < Link_enable_list[i][ii].Count)
                    {
                        if (Link_enable_list[i][ii][iii])
                        {
                            DR_tb[iii].Foreground = green_bright_button_brush;
                            DF_tb[ii].Foreground = green_bright_button_brush;
                            DT_tb[i].Foreground = green_bright_button_brush;
                        }
                        iii++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Path";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Path";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Path";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == false && R_bt_flag == true) //T G, R W, F G, change F G
                {

                    #region bt decolor
                    i = 0;
                    while (i < DF_bt.Count)
                    {
                        DF_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = 0;
                    ii = BTP_Gindex;
                    iii = BTR_Gindex;
                    while (i < Link_enable_list.Count)
                    {
                        if (Link_enable_list[i][ii][iii])
                        {
                            DR_tb[iii].Foreground = green_bright_button_brush;
                            DF_tb[ii].Foreground = green_bright_button_brush;
                            DT_tb[i].Foreground = green_bright_button_brush;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Path";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Path";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Path";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == true && R_bt_flag == true) //T G, R W, F W, change F G
                {
                    #region bt decolor
                    i = 0;
                    while (i < DF_bt.Count)
                    {
                        DF_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = BTP_Gindex;
                    iii = BTR_Gindex;
                    if (Link_enable_list[i][ii][iii])
                    {
                        DR_tb[iii].Foreground = green_bright_button_brush;
                        DF_tb[ii].Foreground = green_bright_button_brush;
                        DT_tb[i].Foreground = green_bright_button_brush;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Link";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Link_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Link";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Link_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Link";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Link_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    await ShowDialog("Error", "DP Error");
                }

                yoo.BorderBrush = green_bright_button_brush;
                P_bt_flag = true;
            }
            else
            {
                if (T_bt_flag == false && R_bt_flag == false) //T W, R W, F W, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    #region read enable
                    i = 0;
                    while (i < Link_enable_list.Count)
                    {
                        ii = 0;
                        while(ii< Link_enable_list[i].Count)
                        {
                            iii = 0;
                            while (iii < Link_enable_list[i][ii].Count)
                            {
                                if (Link_enable_list[i][ii][iii])
                                {
                                    DR_tb[iii].Foreground = green_bright_button_brush;
                                    DF_tb[ii].Foreground = green_bright_button_brush;
                                    DT_tb[i].Foreground = green_bright_button_brush;
                                }
                                iii++;
                            }
                            ii++;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Scenario";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/All_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Scenario";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/En_all_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Scenario";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Uns_all_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == true && R_bt_flag == false) //T G, R W, F W, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = 0;
                    while (ii < Link_enable_list[i].Count)
                    {
                        iii = 0;
                        while (iii < Link_enable_list[i][ii].Count)
                        {
                            if (Link_enable_list[i][ii][iii])
                            {
                                DR_tb[iii].Foreground = green_bright_button_brush;
                                DF_tb[ii].Foreground = green_bright_button_brush;
                                DT_tb[i].Foreground = green_bright_button_brush;
                            }
                            iii++;
                        }
                        ii++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Object";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Object";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == false && R_bt_flag == true) //T G, R W, F G, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = 0;
                    while (i < Link_enable_list.Count)
                    {
                        iii = BTR_Gindex;
                        ii = 0;
                        while (ii < Link_enable_list[i].Count)
                        {
                            if (Link_enable_list[i][ii][iii])
                            {
                                DR_tb[iii].Foreground = green_bright_button_brush;
                                DF_tb[ii].Foreground = green_bright_button_brush;
                                DT_tb[i].Foreground = green_bright_button_brush;
                            }
                            ii++;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Object";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Object";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == true && R_bt_flag == true) //T G, R W, F W, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = 0;
                    iii = BTR_Gindex;
                    while (ii < Link_enable_list[i].Count)
                    {
                        if (Link_enable_list[i][ii][iii])
                        {
                            DR_tb[iii].Foreground = green_bright_button_brush;
                            DF_tb[ii].Foreground = green_bright_button_brush;
                            DT_tb[i].Foreground = green_bright_button_brush;
                        }
                        ii++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Path";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Path";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Path";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    await ShowDialog("Error", "DP Error");
                }
                yoo.BorderBrush = dark_grey_brush;
                P_bt_flag = false;
            }
        }
        private async void DR_bt_Click(object sender, RoutedEventArgs e)
        {
            Button yoo = sender as Button;
            int i = 0;
            int ii = 0;
            int iii = 0;
            bool difflag = false;
            bool selfflag = false;

            BTR_Gindex = Dic_rbt_i[yoo];
            if (yoo.BorderBrush == dark_grey_brush)
            {
                if (P_bt_flag == false && T_bt_flag == false) //T W, R W, F W, change F G
                {
                    #region bt decolor
                    i = 0;
                    while (i < DR_bt.Count)
                    {
                        DR_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = 0;
                    while (i < Link_enable_list.Count)
                    {
                        iii = BTR_Gindex;
                        ii = 0;
                        while (ii < Link_enable_list[i].Count)
                        {
                            if (Link_enable_list[i][ii][iii])
                            {
                                DR_tb[iii].Foreground = green_bright_button_brush;
                                DF_tb[ii].Foreground = green_bright_button_brush;
                                DT_tb[i].Foreground = green_bright_button_brush;
                            }
                            ii++;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Object";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Object";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == true && P_bt_flag == false) //T G, R W, F W, change F G
                {
                    #region bt decolor
                    i = 0;
                    while (i < DR_bt.Count)
                    {
                        DR_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = 0;
                    iii = BTR_Gindex;
                    while (ii < Link_enable_list[i].Count)
                    {
                        if (Link_enable_list[i][ii][iii])
                        {
                            DR_tb[iii].Foreground = green_bright_button_brush;
                            DF_tb[ii].Foreground = green_bright_button_brush;
                            DT_tb[i].Foreground = green_bright_button_brush;
                        }
                        ii++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Path";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Path";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Path";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == false && P_bt_flag == true) //T G, R W, F G, change F G
                {

                    #region bt decolor
                    i = 0;
                    while (i < DR_bt.Count)
                    {
                        DR_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = 0;
                    ii = BTP_Gindex;
                    iii = BTR_Gindex;
                    while (i < Link_enable_list.Count)
                    {
                        if (Link_enable_list[i][ii][iii])
                        {
                            DR_tb[iii].Foreground = green_bright_button_brush;
                            DF_tb[ii].Foreground = green_bright_button_brush;
                            DT_tb[i].Foreground = green_bright_button_brush;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Path";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Path";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Path";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == true && P_bt_flag == true) //T G, R W, F W, change F G
                {
                    #region bt decolor
                    i = 0;
                    while (i < DR_bt.Count)
                    {
                        DR_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = BTP_Gindex;
                    iii = BTR_Gindex;
                    if (Link_enable_list[i][ii][iii])
                    {
                        DR_tb[iii].Foreground = green_bright_button_brush;
                        DF_tb[ii].Foreground = green_bright_button_brush;
                        DT_tb[i].Foreground = green_bright_button_brush;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Link";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Link_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Link";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Link_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Link";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Link_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    await ShowDialog("Error", "DP Error");
                }

                yoo.BorderBrush = green_bright_button_brush;
                R_bt_flag = true;
            }
            else
            {
                if (T_bt_flag == false && P_bt_flag == false) //T W, R W, F W, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    #region read enable
                    i = 0;
                    while (i < Link_enable_list.Count)
                    {
                        ii = 0;
                        while (ii < Link_enable_list[i].Count)
                        {
                            iii = 0;
                            while (iii < Link_enable_list[i][ii].Count)
                            {
                                if (Link_enable_list[i][ii][iii])
                                {
                                    DR_tb[iii].Foreground = green_bright_button_brush;
                                    DF_tb[ii].Foreground = green_bright_button_brush;
                                    DT_tb[i].Foreground = green_bright_button_brush;
                                }
                                iii++;
                            }
                            ii++;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Scenario";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/All_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Scenario";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/En_all_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Scenario";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Uns_all_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == true && P_bt_flag == false) //T G, R W, F W, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = 0;
                    while (ii < Link_enable_list[i].Count)
                    {
                        iii = 0;
                        while (iii < Link_enable_list[i][ii].Count)
                        {
                            if (Link_enable_list[i][ii][iii])
                            {
                                DR_tb[iii].Foreground = green_bright_button_brush;
                                DF_tb[ii].Foreground = green_bright_button_brush;
                                DT_tb[i].Foreground = green_bright_button_brush;
                            }
                            iii++;
                        }
                        ii++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Object";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Object";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == false && P_bt_flag == true) //T G, R W, F G, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = 0;
                    while (i < Link_enable_list.Count)
                    {
                        ii = BTP_Gindex;
                        iii = 0;
                        while (iii < Link_enable_list[i][ii].Count)
                        {
                            if (Link_enable_list[i][ii][iii])
                            {
                                DR_tb[iii].Foreground = green_bright_button_brush;
                                DF_tb[ii].Foreground = green_bright_button_brush;
                                DT_tb[i].Foreground = green_bright_button_brush;
                            }
                            iii++;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Object";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Object";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == true && P_bt_flag == true) //T G, R W, F W, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = BTP_Gindex;
                    iii = 0;
                    while (iii < Link_enable_list[i][ii].Count)
                    {
                        if (Link_enable_list[i][ii][iii])
                        {
                            DR_tb[iii].Foreground = green_bright_button_brush;
                            DF_tb[ii].Foreground = green_bright_button_brush;
                            DT_tb[i].Foreground = green_bright_button_brush;
                        }
                        iii++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Path";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Path";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Path";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    await ShowDialog("Error", "DP Error");
                }
                yoo.BorderBrush = dark_grey_brush;
                R_bt_flag = false;
            }
        }

        private async void DT_bt_Click(object sender, RoutedEventArgs e)
        {
            Button yoo = sender as Button;
            int i = 0;
            int ii = 0;
            int iii = 0;
            bool difflag = false;
            bool selfflag = false;

            BTT_Gindex = Dic_tbt_i[yoo];
            if (yoo.BorderBrush == dark_grey_brush)
            {
                if (P_bt_flag == false && R_bt_flag == false) //T W, R W, F W, change F G
                {
                    #region bt decolor
                    i = 0;
                    while (i < DT_bt.Count)
                    {
                        DT_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = 0;
                    while (ii < Link_enable_list[i].Count)
                    {
                        iii = 0;
                        while (iii < Link_enable_list[i][ii].Count)
                        {
                            if (Link_enable_list[i][ii][iii])
                            {
                                DR_tb[iii].Foreground = green_bright_button_brush;
                                DF_tb[ii].Foreground = green_bright_button_brush;
                                DT_tb[i].Foreground = green_bright_button_brush;
                            }
                            iii++;
                        }
                        ii++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Object";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Object";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (R_bt_flag == true && P_bt_flag == false) //T G, R W, F W, change F G
                {
                    #region bt decolor
                    i = 0;
                    while (i < DT_bt.Count)
                    {
                        DT_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = 0;
                    iii = BTR_Gindex;
                    while (ii < Link_enable_list[i].Count)
                    {
                        if (Link_enable_list[i][ii][iii])
                        {
                            DR_tb[iii].Foreground = green_bright_button_brush;
                            DF_tb[ii].Foreground = green_bright_button_brush;
                            DT_tb[i].Foreground = green_bright_button_brush;
                        }
                        ii++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Path";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Path";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object Path";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (R_bt_flag == false && P_bt_flag == true) //T G, R W, F G, change F G
                {

                    #region bt decolor
                    i = 0;
                    while (i < DT_bt.Count)
                    {
                        DT_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = BTP_Gindex;
                    iii = 0;
                    while (iii < Link_enable_list[i][ii].Count)
                    {
                        if (Link_enable_list[i][ii][iii])
                        {
                            DR_tb[iii].Foreground = green_bright_button_brush;
                            DF_tb[ii].Foreground = green_bright_button_brush;
                            DT_tb[i].Foreground = green_bright_button_brush;
                        }
                        iii++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Path";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Path";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object Path";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (P_bt_flag == true && R_bt_flag == true) //T G, R W, F W, change F G
                {
                    #region bt decolor
                    i = 0;
                    while (i < DT_bt.Count)
                    {
                        DT_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = BTP_Gindex;
                    iii = BTR_Gindex;
                    if (Link_enable_list[i][ii][iii])
                    {
                        DR_tb[iii].Foreground = green_bright_button_brush;
                        DF_tb[ii].Foreground = green_bright_button_brush;
                        DT_tb[i].Foreground = green_bright_button_brush;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Link";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Link_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Link";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Link_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Link";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Link_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    await ShowDialog("Error", "DP Error");
                }

                yoo.BorderBrush = green_bright_button_brush;
                T_bt_flag = true;
            }
            else
            {
                if (P_bt_flag == false && R_bt_flag == false) //T W, R W, F W, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    #region read enable
                    i = 0;
                    while (i < Link_enable_list.Count)
                    {
                        ii = 0;
                        while (ii < Link_enable_list[i].Count)
                        {
                            iii = 0;
                            while (iii < Link_enable_list[i][ii].Count)
                            {
                                if (Link_enable_list[i][ii][iii])
                                {
                                    DR_tb[iii].Foreground = green_bright_button_brush;
                                    DF_tb[ii].Foreground = green_bright_button_brush;
                                    DT_tb[i].Foreground = green_bright_button_brush;
                                }
                                iii++;
                            }
                            ii++;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Scenario";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/All_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Scenario";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/En_all_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Scenario";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Uns_all_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (P_bt_flag == true && R_bt_flag == false) //T G, R W, F W, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = 0;
                    while (i < Link_enable_list.Count)
                    {
                        ii = BTP_Gindex;
                        iii = 0;
                        while (iii < Link_enable_list[i][ii].Count)
                        {
                            if (Link_enable_list[i][ii][iii])
                            {
                                DR_tb[iii].Foreground = green_bright_button_brush;
                                DF_tb[ii].Foreground = green_bright_button_brush;
                                DT_tb[i].Foreground = green_bright_button_brush;
                            }
                            iii++;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Object";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Object";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (P_bt_flag == false && R_bt_flag == true) //T G, R W, F G, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = 0;
                    while (i < Link_enable_list.Count)
                    {
                        iii = BTR_Gindex;
                        ii = 0;
                        while (ii < Link_enable_list[i].Count)
                        {
                            if (Link_enable_list[i][ii][iii])
                            {
                                DR_tb[iii].Foreground = green_bright_button_brush;
                                DF_tb[ii].Foreground = green_bright_button_brush;
                                DT_tb[i].Foreground = green_bright_button_brush;
                            }
                            ii++;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Object";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Object";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (P_bt_flag == true && R_bt_flag == true) //T G, R W, F W, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = 0;
                    ii = BTP_Gindex;
                    iii = BTR_Gindex;
                    while (i < Link_enable_list.Count)
                    {
                        if (Link_enable_list[i][ii][iii])
                        {
                            DR_tb[iii].Foreground = green_bright_button_brush;
                            DF_tb[ii].Foreground = green_bright_button_brush;
                            DT_tb[i].Foreground = green_bright_button_brush;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Path";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Path";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object Path";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    await ShowDialog("Error", "DP Error");
                }
                yoo.BorderBrush = dark_grey_brush;
                T_bt_flag = false;
            }
        }





        public void hide()
        {
            int i = 0;

            Simulation_bt.Visibility = Visibility.Collapsed;

            Text_tb.Visibility = Visibility.Collapsed;
            Ref_AO0_bt.Visibility = Visibility.Collapsed;

            Ref_AO0_tb.Visibility = Visibility.Collapsed;

            Ref_AO1_bt.Visibility = Visibility.Collapsed;

            Ref_AO1_tb.Visibility = Visibility.Collapsed;

            Ref_AO2_bt.Visibility = Visibility.Collapsed;

            Ref_AO2_tb.Visibility = Visibility.Collapsed;

            Ref_AO3_bt.Visibility = Visibility.Collapsed;

            Ref_AO3_tb.Visibility = Visibility.Collapsed;

            Ref_AO4_bt.Visibility = Visibility.Collapsed;

            Ref_AO4_tb.Visibility = Visibility.Collapsed;

            Ref_AO5_bt.Visibility = Visibility.Collapsed;

            Ref_AO5_tb.Visibility = Visibility.Collapsed;

            Ref_AO6_bt.Visibility = Visibility.Collapsed;

            Ref_AO6_tb.Visibility = Visibility.Collapsed;

            CMP_bt.Visibility = Visibility.Collapsed;

            AMT_bt.Visibility = Visibility.Collapsed;

            AMT_tb.Visibility = Visibility.Collapsed;

            Obj_save_tb.Visibility = Visibility.Collapsed;

            Obj_save_bt.Visibility = Visibility.Collapsed;

            Obj_summary_bt.Visibility = Visibility.Collapsed;

            Obj_summary_tb.Visibility = Visibility.Collapsed;

            Sweep_bt.Visibility = Visibility.Collapsed;

            Sweep_tb.Visibility = Visibility.Collapsed;

            Link_uns_bt.Visibility = Visibility.Collapsed;
            Link_uns_tb.Visibility = Visibility.Collapsed;

            Link_en_bt.Visibility = Visibility.Collapsed;
            Link_en_tb.Visibility = Visibility.Collapsed;

            Link_de_bt.Visibility = Visibility.Collapsed;
            Link_de_tb.Visibility = Visibility.Collapsed;

            Transmitter_title_tb.Visibility = Visibility.Collapsed;

            Reflector_title_tb.Visibility = Visibility.Collapsed;
            Receiver_title_tb.Visibility = Visibility.Collapsed;

            i = 0;
            while (i < DR_bt.Count)
            {
                DR_bt[i].Visibility = Visibility.Collapsed;
                i++;
            }
            i = 0;
            while (i < DR_tb.Count)
            {
                DR_tb[i].Visibility = Visibility.Collapsed;
                i++;
            }
            i = 0;
            while (i < DF_bt.Count)
            {
                DF_bt[i].Visibility = Visibility.Collapsed;
                i++;
            }
            i = 0;
            while (i < DF_tb.Count)
            {
                DF_tb[i].Visibility = Visibility.Collapsed;
                i++;
            }
            i = 0;
            while (i < DT_bt.Count)
            {
                DT_bt[i].Visibility = Visibility.Collapsed;
                i++;
            }
            i = 0;
            while (i < DT_tb.Count)
            {
                DT_tb[i].Visibility = Visibility.Collapsed;
                i++;
            }

            Ref_Antenna_bt.Visibility = Visibility.Collapsed;
            Ref_Doppler_bt.Visibility = Visibility.Collapsed;
            Ref_Polarization_bt.Visibility = Visibility.Collapsed;
            Ref_RCS_bt.Visibility = Visibility.Collapsed;
            Ref_RFim_bt.Visibility = Visibility.Collapsed;

            Ref_Clutter_bt.Visibility = Visibility.Collapsed;
        }

        private async void Sweep_bt_Click(object sender, RoutedEventArgs e)
        {
            hide();
            DRBE_sweep = new Sim_sweep(ParentGrid, ParentPage);
            DRBE_sweep.Property_setup(Lv_dtl, Lv_dfl, Lv_drl, Link_enable_list);
        }

        private async void Obj_save_bt_Click(object sender, RoutedEventArgs e)
        {
            await DRBE_LV_SS.Start("Save Object: Transmitter", new List<string>() { "Simulator File", "Link File" }, "dlv", Write_LV_file());
        }

        private async void Obj_summary_bt_Click(object sender, RoutedEventArgs e)
        {
            await ShowDialog("Model File Report", Write_LV_file());
        }

        private string Write_LV_file()
        {
            string result = "";
            int i = 0;
            int ii = 0;
            int iii = 0;
            i = 0;
            while(i<Link_enable_list.Count)
            {
                ii = 0;
                while(ii<Link_enable_list[i].Count)
                {
                    iii = 0;
                    while(iii<Link_enable_list[i][ii].Count)
                    {
                        if(Link_enable_list[i][ii][iii])
                        {
                            result += "Transmitter ID: {" + Lv_dtl[i].ID.ToString() + "} , Platform ID: {" + Lv_dfl[ii].ID.ToString() + "} , Receiver ID: {" + Lv_drl[iii].ID.ToString() + "} , ";
                            result += "Transmitter Antenna Pattern Fidelity: {" + Link_Antenna_list[i][ii][iii].ToString() + "} , Receiver Antenna Pattern Fidelity: {" + Link_Antenna_list[i][ii][iii].ToString() + "} , ";
                            result += "Transmitter RF Impairment Fidelity: {" + Link_RFimp_list[i][ii][iii].ToString() + "} , Receiver RF Impairment Fidelity: {" + Link_RFimp_list[i][ii][iii].ToString() + "} , ";
                            result += "Clutter Fidelity: {" + Link_Clutter_list[i][ii][iii].ToString() + "} , ";
                            result += "Doppler Fidelity: {" + Link_Doppler_list[i][ii][iii].ToString() + "} , ";
                            result += "Polarization Fidelity: {" + Link_Polar_list[i][ii][iii].ToString() + "} , ";
                            result += "RCS Fidelity: {" + Link_RCS_list[i][ii][iii].ToString() + "} \r\n";
                            result += "\r\n ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\r\n";
                        }
                        iii++;
                    }
                    ii++;
                }
                i++;
            }
            return result;
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
            TextBlock svt = new TextBlock() { 
                FontSize = 12,
                Text = y,
                Width = 1000,
                TextWrapping = TextWrapping.WrapWholeWords
            };
            ScrollViewer sv = new ScrollViewer() {
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
