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
    public class DRBE_AP
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
        public BinaryWriter UWbinarywriter;
        public bool UI_Stream_ready_flag = false;
        public DRBE_AP(Grid parent, BinaryWriter writer)
        {
            ParentGrid = parent;
            UWbinarywriter = writer;
            Setup();
            hide();
        }

        private Border AP_O4_bd = new Border();
        private TextBlock AP_O4_ttb = new TextBlock();

        private TextBlock AP_N_ttb = new TextBlock();
        private TextBox AP_N_tb = new TextBox();

        private TextBlock AP_phi_point_ttb = new TextBlock();
        private TextBox AP_phi_point_tb = new TextBox();

        private TextBlock AP_beamwidth_az_ttb = new TextBlock();
        private TextBox AP_beamwidth_az_tb = new TextBox();

        private TextBlock AP_az_res_ttb = new TextBlock();
        private TextBox AP_az_res_tb = new TextBox();

        private TextBlock AP_M_ttb = new TextBlock();
        private TextBox AP_M_tb = new TextBox();

        private TextBlock AP_psi_point_ttb = new TextBlock();
        private TextBox AP_psi_point_tb = new TextBox();

        private TextBlock AP_beamwidth_el_ttb = new TextBlock();
        private TextBox AP_beamwidth_el_tb = new TextBox();

        private TextBlock AP_el_res_ttb = new TextBlock();
        private TextBox AP_el_res_tb = new TextBox();

        private TextBlock AP_d_ttb = new TextBlock();
        private TextBox AP_d_tb = new TextBox();

        private TextBlock AP_backlobe_scaling_ttb = new TextBlock();
        private TextBox AP_backlobe_scaling_tb = new TextBox();


        private TextBlock AP_plotcutsurf_tb = new TextBlock();
        public Button AP_plotcutsurf_bt = new Button();
        private Image AP_plotcutsurf_im = new Image();


        private TextBlock AP_plotfftsurf_tb = new TextBlock();
        private Button AP_plotfftsurf_bt = new Button();
        private Image AP_plotfftsurf_im = new Image();

        private TextBlock AP_plotcutamp_tb = new TextBlock();
        private Button AP_plotcutamp_bt = new Button();
        private Image AP_plotcutamp_im = new Image();

        private TextBlock AP_plotfftamp_tb = new TextBlock();
        private Button AP_plotfftamp_bt = new Button();
        private Image AP_plotfftamp_im = new Image();

        private TextBlock AP_wind_mode_ttb = new TextBlock();
        private ComboBox AP_wind_mode_cb = new ComboBox();


        private void Setup()
        {
            #region AP BD Label
            AP_O4_bd = new Border()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Canvas.SetZIndex(AP_O4_bd, -1);
            AP_O4_bd.SetValue(Grid.ColumnProperty, 0);
            AP_O4_bd.SetValue(Grid.ColumnSpanProperty, 200);
            AP_O4_bd.SetValue(Grid.RowProperty, 10);
            AP_O4_bd.SetValue(Grid.RowSpanProperty, 70);
            ParentGrid.Children.Add(AP_O4_bd);

            AP_O4_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Antena Pattern",
                Foreground = white_button_brush,
                FontSize = 24

            };
            ParentGrid.Children.Add(AP_O4_ttb);
            AP_O4_ttb.SetValue(Grid.ColumnProperty, 85);
            AP_O4_ttb.SetValue(Grid.ColumnSpanProperty, 30);
            AP_O4_ttb.SetValue(Grid.RowProperty, 12);
            AP_O4_ttb.SetValue(Grid.RowSpanProperty, 8);
            #endregion

            #region AP Number of atten AZ

            AP_N_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Number of Antenna Az",
                Foreground = white_button_brush,
                FontSize = 16

            };
            ParentGrid.Children.Add(AP_N_ttb);
            AP_N_ttb.SetValue(Grid.ColumnProperty, 5);
            AP_N_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            AP_N_ttb.SetValue(Grid.RowProperty, 25);
            AP_N_ttb.SetValue(Grid.RowSpanProperty, 5);

            AP_N_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "20",
                Foreground = white_button_brush,
                FontSize = 16

            };
            ParentGrid.Children.Add(AP_N_tb);
            AP_N_tb.SetValue(Grid.ColumnProperty, 5);
            AP_N_tb.SetValue(Grid.ColumnSpanProperty, 20);
            AP_N_tb.SetValue(Grid.RowProperty, 30);
            AP_N_tb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

            #region AP Pointing Angle AZ

            AP_phi_point_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Pointing Angle Az",
                Foreground = white_button_brush,
                FontSize = 16

            };
            ParentGrid.Children.Add(AP_phi_point_ttb);
            AP_phi_point_ttb.SetValue(Grid.ColumnProperty, 30);
            AP_phi_point_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            AP_phi_point_ttb.SetValue(Grid.RowProperty, 25);
            AP_phi_point_ttb.SetValue(Grid.RowSpanProperty, 5);

            AP_phi_point_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "60",
                Foreground = white_button_brush,
                FontSize = 16

            };
            ParentGrid.Children.Add(AP_phi_point_tb);
            AP_phi_point_tb.SetValue(Grid.ColumnProperty, 30);
            AP_phi_point_tb.SetValue(Grid.ColumnSpanProperty, 20);
            AP_phi_point_tb.SetValue(Grid.RowProperty, 30);
            AP_phi_point_tb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

            #region AP Beam Width AZ

            AP_beamwidth_az_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Beam Width Az",
                Foreground = white_button_brush,
                FontSize = 16

            };
            ParentGrid.Children.Add(AP_beamwidth_az_ttb);
            AP_beamwidth_az_ttb.SetValue(Grid.ColumnProperty, 55);
            AP_beamwidth_az_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            AP_beamwidth_az_ttb.SetValue(Grid.RowProperty, 25);
            AP_beamwidth_az_ttb.SetValue(Grid.RowSpanProperty, 5);

            AP_beamwidth_az_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "10",
                Foreground = white_button_brush,
                FontSize = 16

            };
            ParentGrid.Children.Add(AP_beamwidth_az_tb);
            AP_beamwidth_az_tb.SetValue(Grid.ColumnProperty, 55);
            AP_beamwidth_az_tb.SetValue(Grid.ColumnSpanProperty, 20);
            AP_beamwidth_az_tb.SetValue(Grid.RowProperty, 30);
            AP_beamwidth_az_tb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

            #region AP resolution az

            AP_az_res_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Resolution Az",
                Foreground = white_button_brush,
                FontSize = 16

            };
            ParentGrid.Children.Add(AP_az_res_ttb);
            AP_az_res_ttb.SetValue(Grid.ColumnProperty, 80);
            AP_az_res_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            AP_az_res_ttb.SetValue(Grid.RowProperty, 25);
            AP_az_res_ttb.SetValue(Grid.RowSpanProperty, 5);

            AP_az_res_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "100",
                Foreground = white_button_brush,
                FontSize = 16

            };
            ParentGrid.Children.Add(AP_az_res_tb);
            AP_az_res_tb.SetValue(Grid.ColumnProperty, 80);
            AP_az_res_tb.SetValue(Grid.ColumnSpanProperty, 20);
            AP_az_res_tb.SetValue(Grid.RowProperty, 30);
            AP_az_res_tb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

            #region AP Antenna spacing

            AP_d_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Antenna Spacing",
                Foreground = white_button_brush,
                FontSize = 16

            };
            ParentGrid.Children.Add(AP_d_ttb);
            AP_d_ttb.SetValue(Grid.ColumnProperty, 105);
            AP_d_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            AP_d_ttb.SetValue(Grid.RowProperty, 25);
            AP_d_ttb.SetValue(Grid.RowSpanProperty, 5);

            AP_d_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "0.2",
                Foreground = white_button_brush,
                FontSize = 16

            };
            ParentGrid.Children.Add(AP_d_tb);
            AP_d_tb.SetValue(Grid.ColumnProperty, 105);
            AP_d_tb.SetValue(Grid.ColumnSpanProperty, 20);
            AP_d_tb.SetValue(Grid.RowProperty, 30);
            AP_d_tb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

            #region AP Number of atten EL

            AP_M_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Number of Antenna El",
                Foreground = white_button_brush,
                FontSize = 16

            };
            ParentGrid.Children.Add(AP_M_ttb);
            AP_M_ttb.SetValue(Grid.ColumnProperty, 5);
            AP_M_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            AP_M_ttb.SetValue(Grid.RowProperty, 40);
            AP_M_ttb.SetValue(Grid.RowSpanProperty, 5);

            AP_M_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "10",
                Foreground = white_button_brush,
                FontSize = 16

            };
            ParentGrid.Children.Add(AP_M_tb);
            AP_M_tb.SetValue(Grid.ColumnProperty, 5);
            AP_M_tb.SetValue(Grid.ColumnSpanProperty, 20);
            AP_M_tb.SetValue(Grid.RowProperty, 45);
            AP_M_tb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

            #region AP Pointing Angle El

            AP_psi_point_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Pointing Angle El",
                Foreground = white_button_brush,
                FontSize = 16

            };
            ParentGrid.Children.Add(AP_psi_point_ttb);
            AP_psi_point_ttb.SetValue(Grid.ColumnProperty, 30);
            AP_psi_point_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            AP_psi_point_ttb.SetValue(Grid.RowProperty, 40);
            AP_psi_point_ttb.SetValue(Grid.RowSpanProperty, 5);

            AP_psi_point_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "60",
                Foreground = white_button_brush,
                FontSize = 16

            };
            ParentGrid.Children.Add(AP_psi_point_tb);
            AP_psi_point_tb.SetValue(Grid.ColumnProperty, 30);
            AP_psi_point_tb.SetValue(Grid.ColumnSpanProperty, 20);
            AP_psi_point_tb.SetValue(Grid.RowProperty, 45);
            AP_psi_point_tb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

            #region AP Beam Width El

            AP_beamwidth_el_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Beam Width El",
                Foreground = white_button_brush,
                FontSize = 16

            };
            ParentGrid.Children.Add(AP_beamwidth_el_ttb);
            AP_beamwidth_el_ttb.SetValue(Grid.ColumnProperty, 55);
            AP_beamwidth_el_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            AP_beamwidth_el_ttb.SetValue(Grid.RowProperty, 40);
            AP_beamwidth_el_ttb.SetValue(Grid.RowSpanProperty, 5);

            AP_beamwidth_el_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "10",
                Foreground = white_button_brush,
                FontSize = 16

            };
            ParentGrid.Children.Add(AP_beamwidth_el_tb);
            AP_beamwidth_el_tb.SetValue(Grid.ColumnProperty, 55);
            AP_beamwidth_el_tb.SetValue(Grid.ColumnSpanProperty, 20);
            AP_beamwidth_el_tb.SetValue(Grid.RowProperty, 45);
            AP_beamwidth_el_tb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

            #region AP resolution el

            AP_el_res_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Resolution El",
                Foreground = white_button_brush,
                FontSize = 16

            };
            ParentGrid.Children.Add(AP_el_res_ttb);
            AP_el_res_ttb.SetValue(Grid.ColumnProperty, 80);
            AP_el_res_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            AP_el_res_ttb.SetValue(Grid.RowProperty, 40);
            AP_el_res_ttb.SetValue(Grid.RowSpanProperty, 5);

            AP_el_res_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "100",
                Foreground = white_button_brush,
                FontSize = 16

            };
            ParentGrid.Children.Add(AP_el_res_tb);
            AP_el_res_tb.SetValue(Grid.ColumnProperty, 80);
            AP_el_res_tb.SetValue(Grid.ColumnSpanProperty, 20);
            AP_el_res_tb.SetValue(Grid.RowProperty, 45);
            AP_el_res_tb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

            #region AP backlobe scling

            AP_backlobe_scaling_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Backlobe Scaling",
                Foreground = white_button_brush,
                FontSize = 16

            };
            ParentGrid.Children.Add(AP_backlobe_scaling_ttb);
            AP_backlobe_scaling_ttb.SetValue(Grid.ColumnProperty, 105);
            AP_backlobe_scaling_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            AP_backlobe_scaling_ttb.SetValue(Grid.RowProperty, 40);
            AP_backlobe_scaling_ttb.SetValue(Grid.RowSpanProperty, 5);

            AP_backlobe_scaling_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "50",
                Foreground = white_button_brush,
                FontSize = 16

            };
            ParentGrid.Children.Add(AP_backlobe_scaling_tb);
            AP_backlobe_scaling_tb.SetValue(Grid.ColumnProperty, 105);
            AP_backlobe_scaling_tb.SetValue(Grid.ColumnSpanProperty, 20);
            AP_backlobe_scaling_tb.SetValue(Grid.RowProperty, 45);
            AP_backlobe_scaling_tb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

            #region plotcutsurface
            AP_plotcutsurf_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/surfaceplot_CUTS.png", UriKind.RelativeOrAbsolute));
            AP_plotcutsurf_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = AP_plotcutsurf_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(AP_plotcutsurf_bt);
            AP_plotcutsurf_bt.SetValue(Grid.ColumnProperty, 130);
            AP_plotcutsurf_bt.SetValue(Grid.ColumnSpanProperty, 20);
            AP_plotcutsurf_bt.SetValue(Grid.RowProperty, 20);
            AP_plotcutsurf_bt.SetValue(Grid.RowSpanProperty, 20);
            AP_plotcutsurf_bt.Click += AP_plotcutsurf_bt_Click;

            AP_plotcutsurf_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Surface Plot (Cuts)",
                Foreground = white_button_brush,
                FontSize = 20

            };
            //ParentGrid.Children.Add(AP_plotcutsurf_tb);
            AP_plotcutsurf_tb.SetValue(Grid.ColumnProperty, 130);
            AP_plotcutsurf_tb.SetValue(Grid.ColumnSpanProperty, 20);
            AP_plotcutsurf_tb.SetValue(Grid.RowProperty, 15);
            AP_plotcutsurf_tb.SetValue(Grid.RowSpanProperty, 5);
            #endregion
        }

        private MatlabPacket mpacket = new MatlabPacket();
        private List<byte> testpacket = new List<byte>();
        private void AP_plotcutsurf_bt_Click(object sender, RoutedEventArgs e)
        {
            if (UI_Stream_ready_flag)
            {
                testpacket = new List<byte>(mpacket.HC_APO4(new List<byte>()));
                UWbinarywriter.Write(testpacket.ToArray(), 0, 255);
                UWbinarywriter.Flush();
            }
        }

        public void hide()
        {
            #region AP BD Label
            AP_O4_bd.Visibility = Visibility.Collapsed;

            AP_O4_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region AP Number of atten AZ

            AP_N_ttb.Visibility = Visibility.Collapsed;

            AP_N_tb.Visibility = Visibility.Collapsed;
            #endregion

            #region AP Pointing Angle AZ

            AP_phi_point_ttb.Visibility = Visibility.Collapsed;

            AP_phi_point_tb.Visibility = Visibility.Collapsed;
            #endregion

            #region AP Beam Width AZ

            AP_beamwidth_az_ttb.Visibility = Visibility.Collapsed;

            AP_beamwidth_az_tb.Visibility = Visibility.Collapsed;
            #endregion

            #region AP resolution az

            AP_az_res_ttb.Visibility = Visibility.Collapsed;

            AP_az_res_tb.Visibility = Visibility.Collapsed;
            #endregion

            #region AP Antenna spacing

            AP_d_ttb.Visibility = Visibility.Collapsed;

            AP_d_tb.Visibility = Visibility.Collapsed;
            #endregion

            #region AP Number of atten EL

            AP_M_ttb.Visibility = Visibility.Collapsed;

            AP_M_tb.Visibility = Visibility.Collapsed;
            #endregion

            #region AP Pointing Angle El

            AP_psi_point_ttb.Visibility = Visibility.Collapsed;

            AP_psi_point_tb.Visibility = Visibility.Collapsed;
            #endregion

            #region AP Beam Width El

            AP_beamwidth_el_ttb.Visibility = Visibility.Collapsed;

            AP_beamwidth_el_tb.Visibility = Visibility.Collapsed;
            #endregion

            #region AP resolution el

            AP_el_res_ttb.Visibility = Visibility.Collapsed;

            AP_el_res_tb.Visibility = Visibility.Collapsed;
            #endregion

            #region AP backlobe scling

            AP_backlobe_scaling_ttb.Visibility = Visibility.Collapsed;

            AP_backlobe_scaling_tb.Visibility = Visibility.Collapsed;
            #endregion

            #region plotcutsurface
            AP_plotcutsurf_bt.Visibility = Visibility.Collapsed;

            AP_plotcutsurf_tb.Visibility = Visibility.Collapsed;
            #endregion
        }
        public void Show()
        {
            #region AP BD Label
            AP_O4_bd.Visibility = Visibility.Visible;

            AP_O4_ttb.Visibility = Visibility.Visible;
            #endregion

            #region AP Number of atten AZ

            AP_N_ttb.Visibility = Visibility.Visible;

            AP_N_tb.Visibility = Visibility.Visible;
            #endregion

            #region AP Pointing Angle AZ

            AP_phi_point_ttb.Visibility = Visibility.Visible;

            AP_phi_point_tb.Visibility = Visibility.Visible;
            #endregion

            #region AP Beam Width AZ

            AP_beamwidth_az_ttb.Visibility = Visibility.Visible;

            AP_beamwidth_az_tb.Visibility = Visibility.Visible;
            #endregion

            #region AP resolution az

            AP_az_res_ttb.Visibility = Visibility.Visible;

            AP_az_res_tb.Visibility = Visibility.Visible;
            #endregion

            #region AP Antenna spacing

            AP_d_ttb.Visibility = Visibility.Visible;

            AP_d_tb.Visibility = Visibility.Visible;
            #endregion

            #region AP Number of atten EL

            AP_M_ttb.Visibility = Visibility.Visible;

            AP_M_tb.Visibility = Visibility.Visible;
            #endregion

            #region AP Pointing Angle El

            AP_psi_point_ttb.Visibility = Visibility.Visible;

            AP_psi_point_tb.Visibility = Visibility.Visible;
            #endregion

            #region AP Beam Width El

            AP_beamwidth_el_ttb.Visibility = Visibility.Visible;

            AP_beamwidth_el_tb.Visibility = Visibility.Visible;
            #endregion

            #region AP resolution el

            AP_el_res_ttb.Visibility = Visibility.Visible;

            AP_el_res_tb.Visibility = Visibility.Visible;
            #endregion

            #region AP backlobe scling

            AP_backlobe_scaling_ttb.Visibility = Visibility.Visible;

            AP_backlobe_scaling_tb.Visibility = Visibility.Visible;
            #endregion

            #region plotcutsurface
            AP_plotcutsurf_bt.Visibility = Visibility.Visible;

            AP_plotcutsurf_tb.Visibility = Visibility.Visible;
            #endregion
        }
    }
}
