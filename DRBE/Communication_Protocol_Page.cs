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
    public class Communication_Protocol_Page
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
        public Communication_Protocol_Page(Grid parent)
        {
            ParentGrid = parent;
            Setup();
            hide();
        }


        private Border RF_interface_bd = new Border();
        private Border Packet_building_bd = new Border();
        private Border Coefficient_interface_bd = new Border();
        private Border Control_interface_bd = new Border();

        private TextBlock RF_label_tb = new TextBlock();

        private TextBlock RF_enable_label_tb = new TextBlock();
        private TextBlock RF_enable_status_tb = new TextBlock();

        private TextBlock RF_sync_label_tb = new TextBlock();
        private TextBlock RF_sync_status_tb = new TextBlock();

        private TextBlock RF_error_label_tb = new TextBlock();
        private TextBlock RF_error_status_tb = new TextBlock();

        private TextBlock RF_rate_label_tb = new TextBlock();
        private TextBlock RF_rate_status_tb = new TextBlock();


        private TextBlock Coe_label_tb = new TextBlock();

        private TextBlock Coe_connector_tb = new TextBlock();
        private TextBlock Coe_connector_info_tb = new TextBlock();
        private Button Coe_connector_bt = new Button();
        private Image Coe_connector_im = new Image();

        private TextBlock Coe_protocol_tb = new TextBlock();
        private TextBlock Coe_protocol_info_tb = new TextBlock();
        private Button Coe_protocol_bt = new Button();
        private Image Coe_protocol_im = new Image();

        private TextBlock Coe_structure_tb = new TextBlock();
        private TextBlock Coe_structure_info_tb = new TextBlock();
        private Button Coe_structure_bt = new Button();
        private Image Coe_structure_im = new Image();

        private TextBlock Coe_rate_tb = new TextBlock();
        private TextBlock Coe_rate_info_tb = new TextBlock();
        private Button Coe_rate_bt = new Button();
        private Image Coe_rate_im = new Image();


        private TextBlock Con_label_tb = new TextBlock();

        private TextBlock Con_connector_tb = new TextBlock();
        private TextBlock Con_connector_info_tb = new TextBlock();
        private Button Con_connector_bt = new Button();
        private Image Con_connector_im = new Image();

        private TextBlock Con_protocol_tb = new TextBlock();
        private TextBlock Con_protocol_info_tb = new TextBlock();
        private Button Con_protocol_bt = new Button();
        private Image Con_protocol_im = new Image();

        private TextBlock Con_structure_tb = new TextBlock();
        private TextBlock Con_structure_info_tb = new TextBlock();
        private Button Con_structure_bt = new Button();
        private Image Con_structure_im = new Image();

        private TextBlock Con_rate_tb = new TextBlock();
        private TextBlock Con_rate_info_tb = new TextBlock();
        private Button Con_rate_bt = new Button();
        private Image Con_rate_im = new Image();

        private TextBlock Con_channel_tb = new TextBlock();
        private TextBlock Con_channel_info_tb = new TextBlock();
        private Button Con_channel_bt = new Button();
        private Image Con_channel_im = new Image();

        private TextBlock Con_domain_tb = new TextBlock();
        private TextBlock Con_domain_info_tb = new TextBlock();
        private Button Con_domain_bt = new Button();
        private Image Con_domain_im = new Image();

        private TextBlock Con_template_tb = new TextBlock();
        private TextBlock Con_template_info_tb = new TextBlock();
        private Button Con_template_bt = new Button();
        private Image Con_template_im = new Image();

        private TextBlock Pk_label1_tb = new TextBlock();
        private TextBlock Pk_label2_tb = new TextBlock();

        private Button Pk_USBB_bt = new Button();
        private Image Pk_USBB_im = new Image();
        private Button Pk_SMA_bt = new Button();
        private Image Pk_SMA_im = new Image();
        private Button Pk_FMC_bt = new Button();
        private Image Pk_FMC_im = new Image();
        private Button Pk_PCIe_bt = new Button();
        private Image Pk_PCIe_im = new Image();
        private Button Pk_Ethernet_bt = new Button();
        private Image Pk_Ethernet_im = new Image();
        private Button Pk_Bulleye_bt = new Button();
        private Image Pk_Bulleye_im = new Image();

        private void Setup()
        {
            #region RF BD Label
            RF_interface_bd = new Border()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Canvas.SetZIndex(RF_interface_bd, -1);
            RF_interface_bd.SetValue(Grid.ColumnProperty, 0);
            RF_interface_bd.SetValue(Grid.ColumnSpanProperty, 150);
            RF_interface_bd.SetValue(Grid.RowProperty, 10);
            RF_interface_bd.SetValue(Grid.RowSpanProperty, 30);
            ParentGrid.Children.Add(RF_interface_bd);

            RF_label_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "RF Interface",
                Foreground = white_button_brush,
                FontSize = 30

            };
            ParentGrid.Children.Add(RF_label_tb);
            RF_label_tb.SetValue(Grid.ColumnProperty, 60);
            RF_label_tb.SetValue(Grid.ColumnSpanProperty, 30);
            RF_label_tb.SetValue(Grid.RowProperty, 12);
            RF_label_tb.SetValue(Grid.RowSpanProperty, 8);
            #endregion
            #region RF Enable
            RF_enable_label_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Enabled Channel",
                Foreground = white_button_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(RF_enable_label_tb);
            RF_enable_label_tb.SetValue(Grid.ColumnProperty, 5);
            RF_enable_label_tb.SetValue(Grid.ColumnSpanProperty, 20);
            RF_enable_label_tb.SetValue(Grid.RowProperty, 25);
            RF_enable_label_tb.SetValue(Grid.RowSpanProperty, 5);

            RF_enable_status_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Foreground = white_button_brush,
                FontSize = 12

            };
            ParentGrid.Children.Add(RF_enable_status_tb);
            RF_enable_status_tb.SetValue(Grid.ColumnProperty, 5);
            RF_enable_status_tb.SetValue(Grid.ColumnSpanProperty, 20);
            RF_enable_status_tb.SetValue(Grid.RowProperty, 30);
            RF_enable_status_tb.SetValue(Grid.RowSpanProperty, 5);
            RF_enable_status_tb.Inlines.Add(new Run() { Text = "73  ", Foreground = green_bright_button_brush, FontSize = 24, FontWeight = FontWeights.ExtraBold });
            RF_enable_status_tb.Inlines.Add(new Run() { Text = "/", Foreground = white_button_brush, FontSize = 24, FontWeight = FontWeights.ExtraBold });
            RF_enable_status_tb.Inlines.Add(new Run() { Text = "  200", Foreground = red_bright_button_brush, FontSize = 24, FontWeight = FontWeights.ExtraBold });
            #endregion
            #region RF sync
            RF_sync_label_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Channel Sync",
                Foreground = white_button_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(RF_sync_label_tb);
            RF_sync_label_tb.SetValue(Grid.ColumnProperty, 40);
            RF_sync_label_tb.SetValue(Grid.ColumnSpanProperty, 20);
            RF_sync_label_tb.SetValue(Grid.RowProperty, 25);
            RF_sync_label_tb.SetValue(Grid.RowSpanProperty, 5);


            RF_sync_status_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "",
                Foreground = white_button_brush,
                FontSize = 12

            };
            ParentGrid.Children.Add(RF_sync_status_tb);
            RF_sync_status_tb.SetValue(Grid.ColumnProperty, 40);
            RF_sync_status_tb.SetValue(Grid.ColumnSpanProperty, 20);
            RF_sync_status_tb.SetValue(Grid.RowProperty, 30);
            RF_sync_status_tb.SetValue(Grid.RowSpanProperty, 5);
            RF_sync_status_tb.Inlines.Add(new Run() { Text = "73  ", Foreground = green_bright_button_brush, FontSize = 24, FontWeight = FontWeights.ExtraBold });
            RF_sync_status_tb.Inlines.Add(new Run() { Text = "/", Foreground = white_button_brush, FontSize = 24, FontWeight = FontWeights.ExtraBold });
            RF_sync_status_tb.Inlines.Add(new Run() { Text = "  73", Foreground = red_bright_button_brush, FontSize = 24, FontWeight = FontWeights.ExtraBold });
            #endregion
            #region RF error
            RF_error_label_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Error",
                Foreground = white_button_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(RF_error_label_tb);
            RF_error_label_tb.SetValue(Grid.ColumnProperty, 75);
            RF_error_label_tb.SetValue(Grid.ColumnSpanProperty, 20);
            RF_error_label_tb.SetValue(Grid.RowProperty, 25);
            RF_error_label_tb.SetValue(Grid.RowSpanProperty, 5);

            RF_error_status_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "",
                Foreground = white_button_brush,
                FontSize = 12

            };
            ParentGrid.Children.Add(RF_error_status_tb);
            RF_error_status_tb.SetValue(Grid.ColumnProperty, 75);
            RF_error_status_tb.SetValue(Grid.ColumnSpanProperty, 20);
            RF_error_status_tb.SetValue(Grid.RowProperty, 30);
            RF_error_status_tb.SetValue(Grid.RowSpanProperty, 5);
            RF_error_status_tb.Inlines.Add(new Run() { Text = "0  ", Foreground = green_bright_button_brush, FontSize = 24, FontWeight = FontWeights.ExtraBold });
            #endregion
            #region RF rate
            RF_rate_label_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Rate",
                Foreground = white_button_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(RF_rate_label_tb);
            RF_rate_label_tb.SetValue(Grid.ColumnProperty, 110);
            RF_rate_label_tb.SetValue(Grid.ColumnSpanProperty, 20);
            RF_rate_label_tb.SetValue(Grid.RowProperty, 25);
            RF_rate_label_tb.SetValue(Grid.RowSpanProperty, 5);

            RF_rate_status_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "",
                Foreground = white_button_brush,
                FontSize = 12

            };
            ParentGrid.Children.Add(RF_rate_status_tb);
            RF_rate_status_tb.SetValue(Grid.ColumnProperty, 110);
            RF_rate_status_tb.SetValue(Grid.ColumnSpanProperty, 20);
            RF_rate_status_tb.SetValue(Grid.RowProperty, 30);
            RF_rate_status_tb.SetValue(Grid.RowSpanProperty, 10);
            RF_rate_status_tb.Inlines.Add(new Run() { Text = "High: ", Foreground = white_button_brush, FontSize = 20, FontWeight = FontWeights.ExtraBold });
            RF_rate_status_tb.Inlines.Add(new Run() { Text = "2.014 Gbs \r\n  ", Foreground = green_bright_button_brush, FontSize = 20, FontWeight = FontWeights.ExtraBold });

            RF_rate_status_tb.Inlines.Add(new Run() { Text = "Low: ", Foreground = white_button_brush, FontSize = 20, FontWeight = FontWeights.ExtraBold });
            RF_rate_status_tb.Inlines.Add(new Run() { Text = "2.012 Gbs ", Foreground = orange_brush, FontSize = 20, FontWeight = FontWeights.ExtraBold });
            #endregion

            #region Coe BD Label
            Coefficient_interface_bd = new Border()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch, 
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Canvas.SetZIndex(Coefficient_interface_bd, -1);
            Coefficient_interface_bd.SetValue(Grid.ColumnProperty, 0);
            Coefficient_interface_bd.SetValue(Grid.ColumnSpanProperty, 150);
            Coefficient_interface_bd.SetValue(Grid.RowProperty, 40);
            Coefficient_interface_bd.SetValue(Grid.RowSpanProperty, 40);
            ParentGrid.Children.Add(Coefficient_interface_bd);

            Coe_label_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Data Plane",
                Foreground = white_button_brush,
                FontSize = 30
            };
            ParentGrid.Children.Add(Coe_label_tb);
            Coe_label_tb.SetValue(Grid.ColumnProperty, 60);
            Coe_label_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Coe_label_tb.SetValue(Grid.RowProperty, 43);
            Coe_label_tb.SetValue(Grid.RowSpanProperty, 8);
            #endregion
            #region Coe Connector
            Coe_connector_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/connector_icon.png", UriKind.RelativeOrAbsolute));
            Coe_connector_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Coe_connector_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Coe_connector_bt);
            Coe_connector_bt.SetValue(Grid.ColumnProperty, 5);
            Coe_connector_bt.SetValue(Grid.ColumnSpanProperty, 10);
            Coe_connector_bt.SetValue(Grid.RowProperty, 50);
            Coe_connector_bt.SetValue(Grid.RowSpanProperty, 20);

            Coe_connector_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Connector",
                Foreground = white_button_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(Coe_connector_tb);
            Coe_connector_tb.SetValue(Grid.ColumnProperty, 15);
            Coe_connector_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Coe_connector_tb.SetValue(Grid.RowProperty, 53);
            Coe_connector_tb.SetValue(Grid.RowSpanProperty, 7);

            Coe_connector_info_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "SFP+ ",
                Foreground = ardu_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(Coe_connector_info_tb);
            Coe_connector_info_tb.SetValue(Grid.ColumnProperty, 15);
            Coe_connector_info_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Coe_connector_info_tb.SetValue(Grid.RowProperty, 63);
            Coe_connector_info_tb.SetValue(Grid.RowSpanProperty, 7);
            #endregion
            #region Coe Protocol
            Coe_protocol_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/protocol_icon.png", UriKind.RelativeOrAbsolute));
            Coe_protocol_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Coe_protocol_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Coe_protocol_bt);
            Coe_protocol_bt.SetValue(Grid.ColumnProperty, 40);
            Coe_protocol_bt.SetValue(Grid.ColumnSpanProperty, 10);
            Coe_protocol_bt.SetValue(Grid.RowProperty, 50);
            Coe_protocol_bt.SetValue(Grid.RowSpanProperty, 20);

            Coe_protocol_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Protocol",
                Foreground = white_button_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(Coe_protocol_tb);
            Coe_protocol_tb.SetValue(Grid.ColumnProperty, 50);
            Coe_protocol_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Coe_protocol_tb.SetValue(Grid.RowProperty, 53);
            Coe_protocol_tb.SetValue(Grid.RowSpanProperty, 7);

            Coe_protocol_info_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Aurora 8b/10b",
                Foreground = ardu_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(Coe_protocol_info_tb);
            Coe_protocol_info_tb.SetValue(Grid.ColumnProperty, 50);
            Coe_protocol_info_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Coe_protocol_info_tb.SetValue(Grid.RowProperty, 63);
            Coe_protocol_info_tb.SetValue(Grid.RowSpanProperty, 7);
            #endregion
            #region Coe Structure
            Coe_structure_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/structure_icon.png", UriKind.RelativeOrAbsolute));
            Coe_structure_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Coe_structure_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Coe_structure_bt);
            Coe_structure_bt.SetValue(Grid.ColumnProperty, 75);
            Coe_structure_bt.SetValue(Grid.ColumnSpanProperty, 10);
            Coe_structure_bt.SetValue(Grid.RowProperty, 50);
            Coe_structure_bt.SetValue(Grid.RowSpanProperty, 20);

            Coe_structure_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Structure",
                Foreground = white_button_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(Coe_structure_tb);
            Coe_structure_tb.SetValue(Grid.ColumnProperty, 85);
            Coe_structure_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Coe_structure_tb.SetValue(Grid.RowProperty, 53);
            Coe_structure_tb.SetValue(Grid.RowSpanProperty, 7);

            Coe_structure_info_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "3 X 10 \r\nFramed Stream",
                Foreground = ardu_brush,
                FontSize = 18

            };
            ParentGrid.Children.Add(Coe_structure_info_tb);
            Coe_structure_info_tb.SetValue(Grid.ColumnProperty, 85);
            Coe_structure_info_tb.SetValue(Grid.ColumnSpanProperty, 25);
            Coe_structure_info_tb.SetValue(Grid.RowProperty, 60);
            Coe_structure_info_tb.SetValue(Grid.RowSpanProperty, 10);
            #endregion
            #region Coe Rate
            Coe_rate_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/rate_icon.png", UriKind.RelativeOrAbsolute));
            Coe_rate_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Coe_rate_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Coe_rate_bt);
            Coe_rate_bt.SetValue(Grid.ColumnProperty, 110);
            Coe_rate_bt.SetValue(Grid.ColumnSpanProperty, 10);
            Coe_rate_bt.SetValue(Grid.RowProperty, 50);
            Coe_rate_bt.SetValue(Grid.RowSpanProperty, 20);

            Coe_rate_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "RX Bandwidth",
                Foreground = white_button_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(Coe_rate_tb);
            Coe_rate_tb.SetValue(Grid.ColumnProperty, 120);
            Coe_rate_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Coe_rate_tb.SetValue(Grid.RowProperty, 53);
            Coe_rate_tb.SetValue(Grid.RowSpanProperty, 7);

            Coe_rate_info_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "2 X 5.120 Gbs",
                Foreground = ardu_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(Coe_rate_info_tb);
            Coe_rate_info_tb.SetValue(Grid.ColumnProperty, 120);
            Coe_rate_info_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Coe_rate_info_tb.SetValue(Grid.RowProperty, 63);
            Coe_rate_info_tb.SetValue(Grid.RowSpanProperty, 7);
            #endregion

            #region Con BD Label
            Control_interface_bd = new Border()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Canvas.SetZIndex(Control_interface_bd, -1);
            Control_interface_bd.SetValue(Grid.ColumnProperty, 0);
            Control_interface_bd.SetValue(Grid.ColumnSpanProperty, 150);
            Control_interface_bd.SetValue(Grid.RowProperty, 80);
            Control_interface_bd.SetValue(Grid.RowSpanProperty, 70);
            ParentGrid.Children.Add(Control_interface_bd);

            

            Con_label_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Control Plane",
                Foreground = white_button_brush,
                FontSize = 30

            };
            ParentGrid.Children.Add(Con_label_tb);
            Con_label_tb.SetValue(Grid.ColumnProperty, 60);
            Con_label_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Con_label_tb.SetValue(Grid.RowProperty, 83);
            Con_label_tb.SetValue(Grid.RowSpanProperty, 38);
            #endregion
            #region Con Connector
            Con_connector_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/connector_icon.png", UriKind.RelativeOrAbsolute));
            Con_connector_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Con_connector_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                BorderBrush = green_bright_button_brush,
                BorderThickness = new Thickness(1, 1, 1, 1),
                FontSize = 18
            };
            ParentGrid.Children.Add(Con_connector_bt);
            Con_connector_bt.SetValue(Grid.ColumnProperty, 5);
            Con_connector_bt.SetValue(Grid.ColumnSpanProperty, 10);
            Con_connector_bt.SetValue(Grid.RowProperty, 90);
            Con_connector_bt.SetValue(Grid.RowSpanProperty, 20);

            Con_connector_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Connector",
                Foreground = white_button_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(Con_connector_tb);
            Con_connector_tb.SetValue(Grid.ColumnProperty, 15);
            Con_connector_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Con_connector_tb.SetValue(Grid.RowProperty, 93);
            Con_connector_tb.SetValue(Grid.RowSpanProperty, 5);

            Con_connector_info_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "SMA",
                Foreground = ardu_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(Con_connector_info_tb);
            Con_connector_info_tb.SetValue(Grid.ColumnProperty, 15);
            Con_connector_info_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Con_connector_info_tb.SetValue(Grid.RowProperty, 103);
            Con_connector_info_tb.SetValue(Grid.RowSpanProperty, 5);
            #endregion
            #region Con Protocol
            Con_protocol_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/protocol_icon.png", UriKind.RelativeOrAbsolute));
            Con_protocol_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Con_protocol_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Con_protocol_bt);
            Con_protocol_bt.SetValue(Grid.ColumnProperty, 40);
            Con_protocol_bt.SetValue(Grid.ColumnSpanProperty, 10);
            Con_protocol_bt.SetValue(Grid.RowProperty, 90);
            Con_protocol_bt.SetValue(Grid.RowSpanProperty, 20);

            Con_protocol_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Protocol",
                Foreground = white_button_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(Con_protocol_tb);
            Con_protocol_tb.SetValue(Grid.ColumnProperty, 50);
            Con_protocol_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Con_protocol_tb.SetValue(Grid.RowProperty, 93);
            Con_protocol_tb.SetValue(Grid.RowSpanProperty, 7);

            Con_protocol_info_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "UART-Bridge",
                Foreground = ardu_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(Con_protocol_info_tb);
            Con_protocol_info_tb.SetValue(Grid.ColumnProperty, 50);
            Con_protocol_info_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Con_protocol_info_tb.SetValue(Grid.RowProperty, 103);
            Con_protocol_info_tb.SetValue(Grid.RowSpanProperty, 7);
            #endregion
            #region Con Structure
            Con_structure_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/structure_icon.png", UriKind.RelativeOrAbsolute));
            Con_structure_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Con_structure_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Con_structure_bt);
            Con_structure_bt.SetValue(Grid.ColumnProperty, 75);
            Con_structure_bt.SetValue(Grid.ColumnSpanProperty, 10);
            Con_structure_bt.SetValue(Grid.RowProperty, 90);
            Con_structure_bt.SetValue(Grid.RowSpanProperty, 20);

            Con_structure_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Structure",
                Foreground = white_button_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(Con_structure_tb);
            Con_structure_tb.SetValue(Grid.ColumnProperty, 85);
            Con_structure_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Con_structure_tb.SetValue(Grid.RowProperty, 93);
            Con_structure_tb.SetValue(Grid.RowSpanProperty, 7);

            Con_structure_info_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "1 X 5 \r\nFramed Non-Stream",
                Foreground = ardu_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Con_structure_info_tb);
            Con_structure_info_tb.SetValue(Grid.ColumnProperty, 85);
            Con_structure_info_tb.SetValue(Grid.ColumnSpanProperty, 25);
            Con_structure_info_tb.SetValue(Grid.RowProperty, 100);
            Con_structure_info_tb.SetValue(Grid.RowSpanProperty, 10);
            #endregion
            #region Con Rate
            Con_rate_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/rate_icon.png", UriKind.RelativeOrAbsolute));
            Con_rate_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Con_rate_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Con_rate_bt);
            Con_rate_bt.SetValue(Grid.ColumnProperty, 110);
            Con_rate_bt.SetValue(Grid.ColumnSpanProperty, 10);
            Con_rate_bt.SetValue(Grid.RowProperty, 90);
            Con_rate_bt.SetValue(Grid.RowSpanProperty, 20);

            Con_rate_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "TX Bandwidth",
                Foreground = white_button_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(Con_rate_tb);
            Con_rate_tb.SetValue(Grid.ColumnProperty, 120);
            Con_rate_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Con_rate_tb.SetValue(Grid.RowProperty, 93);
            Con_rate_tb.SetValue(Grid.RowSpanProperty, 7);

            Con_rate_info_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "1 X 1.024 Gbs",
                Foreground = ardu_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(Con_rate_info_tb);
            Con_rate_info_tb.SetValue(Grid.ColumnProperty, 120);
            Con_rate_info_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Con_rate_info_tb.SetValue(Grid.RowProperty, 103);
            Con_rate_info_tb.SetValue(Grid.RowSpanProperty, 7);
            #endregion
            #region Con Channel
            Con_channel_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/channel_icon.png", UriKind.RelativeOrAbsolute));
            Con_channel_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Con_channel_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Con_channel_bt);
            Con_channel_bt.SetValue(Grid.ColumnProperty, 5);
            Con_channel_bt.SetValue(Grid.ColumnSpanProperty, 10);
            Con_channel_bt.SetValue(Grid.RowProperty, 120);
            Con_channel_bt.SetValue(Grid.RowSpanProperty, 20);

            Con_channel_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Channels",
                Foreground = white_button_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(Con_channel_tb);
            Con_channel_tb.SetValue(Grid.ColumnProperty, 15);
            Con_channel_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Con_channel_tb.SetValue(Grid.RowProperty, 123);
            Con_channel_tb.SetValue(Grid.RowSpanProperty, 7);

            Con_channel_info_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "4 X 4 Interleave",
                Foreground = ardu_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(Con_channel_info_tb);
            Con_channel_info_tb.SetValue(Grid.ColumnProperty, 15);
            Con_channel_info_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Con_channel_info_tb.SetValue(Grid.RowProperty, 133);
            Con_channel_info_tb.SetValue(Grid.RowSpanProperty, 7);
            #endregion
            #region Con Domain
            Con_domain_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/domain_icon.png", UriKind.RelativeOrAbsolute));
            Con_domain_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Con_domain_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Con_domain_bt);
            Con_domain_bt.SetValue(Grid.ColumnProperty, 40);
            Con_domain_bt.SetValue(Grid.ColumnSpanProperty, 10);
            Con_domain_bt.SetValue(Grid.RowProperty, 120);
            Con_domain_bt.SetValue(Grid.RowSpanProperty, 20);

            Con_domain_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Domain",
                Foreground = white_button_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(Con_domain_tb);
            Con_domain_tb.SetValue(Grid.ColumnProperty, 50);
            Con_domain_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Con_domain_tb.SetValue(Grid.RowProperty, 123);
            Con_domain_tb.SetValue(Grid.RowSpanProperty, 7);

            Con_domain_info_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Debug Signal",
                Foreground = ardu_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(Con_domain_info_tb);
            Con_domain_info_tb.SetValue(Grid.ColumnProperty, 50);
            Con_domain_info_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Con_domain_info_tb.SetValue(Grid.RowProperty, 133);
            Con_domain_info_tb.SetValue(Grid.RowSpanProperty, 7);
            #endregion
            #region Con Template
            Con_template_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/template_icon.png", UriKind.RelativeOrAbsolute));
            Con_template_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Con_template_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Con_template_bt);
            Con_template_bt.SetValue(Grid.ColumnProperty, 75);
            Con_template_bt.SetValue(Grid.ColumnSpanProperty, 10);
            Con_template_bt.SetValue(Grid.RowProperty, 120);
            Con_template_bt.SetValue(Grid.RowSpanProperty, 20);

            Con_template_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Template",
                Foreground = white_button_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(Con_template_tb);
            Con_template_tb.SetValue(Grid.ColumnProperty, 85);
            Con_template_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Con_template_tb.SetValue(Grid.RowProperty, 123);
            Con_template_tb.SetValue(Grid.RowSpanProperty, 7);

            Con_template_info_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "User Defined 1",
                Foreground = ardu_brush,
                FontSize = 20

            };
            ParentGrid.Children.Add(Con_template_info_tb);
            Con_template_info_tb.SetValue(Grid.ColumnProperty, 85);
            Con_template_info_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Con_template_info_tb.SetValue(Grid.RowProperty, 133);
            Con_template_info_tb.SetValue(Grid.RowSpanProperty, 7);
            #endregion

            #region Pack BD Label
            Packet_building_bd = new Border()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Canvas.SetZIndex(Packet_building_bd, -1);
            Packet_building_bd.SetValue(Grid.ColumnProperty, 150);
            Packet_building_bd.SetValue(Grid.ColumnSpanProperty, 50);
            Packet_building_bd.SetValue(Grid.RowProperty, 10);
            Packet_building_bd.SetValue(Grid.RowSpanProperty, 140);
            ParentGrid.Children.Add(Packet_building_bd);

            Pk_label1_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Control Plane",
                Foreground = white_button_brush,
                FontSize = 24

            };
            ParentGrid.Children.Add(Pk_label1_tb);
            Pk_label1_tb.SetValue(Grid.ColumnProperty, 160);
            Pk_label1_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Pk_label1_tb.SetValue(Grid.RowProperty, 15);
            Pk_label1_tb.SetValue(Grid.RowSpanProperty, 10);

            Pk_label2_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.ExtraBold,
                Text = "Connector",
                Foreground = white_button_brush,
                FontSize = 24

            };
            ParentGrid.Children.Add(Pk_label2_tb);
            Pk_label2_tb.SetValue(Grid.ColumnProperty, 160);
            Pk_label2_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Pk_label2_tb.SetValue(Grid.RowProperty, 25);
            Pk_label2_tb.SetValue(Grid.RowSpanProperty, 10);


            Pk_USBB_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/USB-B_icon.png", UriKind.RelativeOrAbsolute));
            Pk_USBB_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Pk_USBB_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Pk_USBB_bt);
            Pk_USBB_bt.SetValue(Grid.ColumnProperty, 152);
            Pk_USBB_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Pk_USBB_bt.SetValue(Grid.RowProperty, 40);
            Pk_USBB_bt.SetValue(Grid.RowSpanProperty, 20);

            Pk_SMA_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/SMA_icon.png", UriKind.RelativeOrAbsolute));
            Pk_SMA_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Pk_SMA_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                BorderBrush = green_bright_button_brush,
                BorderThickness = new Thickness(1, 1, 1, 1),
                FontSize = 18
            };
            ParentGrid.Children.Add(Pk_SMA_bt);
            Pk_SMA_bt.SetValue(Grid.ColumnProperty, 177);
            Pk_SMA_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Pk_SMA_bt.SetValue(Grid.RowProperty, 40);
            Pk_SMA_bt.SetValue(Grid.RowSpanProperty, 20);

            Pk_FMC_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/FMC_icon.png", UriKind.RelativeOrAbsolute));
            Pk_FMC_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Pk_FMC_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Pk_FMC_bt);
            Pk_FMC_bt.SetValue(Grid.ColumnProperty, 152);
            Pk_FMC_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Pk_FMC_bt.SetValue(Grid.RowProperty, 70);
            Pk_FMC_bt.SetValue(Grid.RowSpanProperty, 20);

            Pk_PCIe_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/PCIe_icon.png", UriKind.RelativeOrAbsolute));
            Pk_PCIe_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Pk_PCIe_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Pk_PCIe_bt);
            Pk_PCIe_bt.SetValue(Grid.ColumnProperty, 177);
            Pk_PCIe_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Pk_PCIe_bt.SetValue(Grid.RowProperty, 70);
            Pk_PCIe_bt.SetValue(Grid.RowSpanProperty, 20);

            Pk_Ethernet_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Ethernet_icon.png", UriKind.RelativeOrAbsolute));
            Pk_Ethernet_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Pk_Ethernet_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Pk_Ethernet_bt);
            Pk_Ethernet_bt.SetValue(Grid.ColumnProperty, 152);
            Pk_Ethernet_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Pk_Ethernet_bt.SetValue(Grid.RowProperty, 100);
            Pk_Ethernet_bt.SetValue(Grid.RowSpanProperty, 20);

            Pk_Bulleye_im.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Bullseye_icon.png", UriKind.RelativeOrAbsolute));
            Pk_Bulleye_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Pk_Bulleye_im,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Pk_Bulleye_bt);
            Pk_Bulleye_bt.SetValue(Grid.ColumnProperty, 177);
            Pk_Bulleye_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Pk_Bulleye_bt.SetValue(Grid.RowProperty, 100);
            Pk_Bulleye_bt.SetValue(Grid.RowSpanProperty, 20);
            #endregion
        }

        private void show()
        {
            #region RF BD Label
            RF_interface_bd.Visibility = Visibility.Visible;

            RF_label_tb.Visibility = Visibility.Visible;
            #endregion
            #region RF Enable
            RF_enable_label_tb.Visibility = Visibility.Visible;

            RF_enable_status_tb.Visibility = Visibility.Visible;
            #endregion
            #region RF sync
            RF_sync_label_tb.Visibility = Visibility.Visible;


            RF_sync_status_tb.Visibility = Visibility.Visible;
            #endregion
            #region RF error
            RF_error_label_tb.Visibility = Visibility.Visible;

            RF_error_status_tb.Visibility = Visibility.Visible;
            #endregion
            #region RF rate
            RF_rate_label_tb.Visibility = Visibility.Visible;

            RF_rate_status_tb.Visibility = Visibility.Visible;
            #endregion

            #region Coe BD Label
            Coefficient_interface_bd.Visibility = Visibility.Visible;

            Coe_label_tb.Visibility = Visibility.Visible;
            #endregion
            #region Coe Connector

            Coe_connector_bt.Visibility = Visibility.Visible;

            Coe_connector_tb.Visibility = Visibility.Visible;

            Coe_connector_info_tb.Visibility = Visibility.Visible;
            #endregion
            #region Coe Protocol
            Coe_protocol_bt.Visibility = Visibility.Visible;

            Coe_protocol_tb.Visibility = Visibility.Visible;

            Coe_protocol_info_tb.Visibility = Visibility.Visible;
            #endregion
            #region Coe Structure

            Coe_structure_bt.Visibility = Visibility.Visible;

            Coe_structure_tb.Visibility = Visibility.Visible;

            Coe_structure_info_tb.Visibility = Visibility.Visible;
            #endregion
            #region Coe Rate
            Coe_rate_bt.Visibility = Visibility.Visible;

            Coe_rate_tb.Visibility = Visibility.Visible;

            Coe_rate_info_tb.Visibility = Visibility.Visible;
            #endregion

            #region Con BD Label
            Control_interface_bd.Visibility = Visibility.Visible;



            Con_label_tb.Visibility = Visibility.Visible;
            #endregion
            #region Con Connector
            Con_connector_bt.Visibility = Visibility.Visible;

            Con_connector_tb.Visibility = Visibility.Visible;

            Con_connector_info_tb.Visibility = Visibility.Visible;
            #endregion
            #region Con Protocol
            Con_protocol_bt.Visibility = Visibility.Visible;

            Con_protocol_tb.Visibility = Visibility.Visible;

            Con_protocol_info_tb.Visibility = Visibility.Visible;
            #endregion
            #region Con Structure

            Con_structure_bt.Visibility = Visibility.Visible;

            Con_structure_tb.Visibility = Visibility.Visible;

            Con_structure_info_tb.Visibility = Visibility.Visible;
            #endregion
            #region Con Rate
            Con_rate_bt.Visibility = Visibility.Visible;

            Con_rate_tb.Visibility = Visibility.Visible;

            Con_rate_info_tb.Visibility = Visibility.Visible;
            #endregion
            #region Con Channel

            Con_channel_bt.Visibility = Visibility.Visible;

            Con_channel_tb.Visibility = Visibility.Visible;

            Con_channel_info_tb.Visibility = Visibility.Visible;
            #endregion
            #region Con Domain

            Con_domain_bt.Visibility = Visibility.Visible;

            Con_domain_tb.Visibility = Visibility.Visible;

            Con_domain_info_tb.Visibility = Visibility.Visible;
            #endregion
            #region Con Template
            Con_template_bt.Visibility = Visibility.Visible;

            Con_template_tb.Visibility = Visibility.Visible;

            Con_template_info_tb.Visibility = Visibility.Visible;
            #endregion

            #region Pack BD Label
            Packet_building_bd.Visibility = Visibility.Visible;

            Pk_label1_tb.Visibility = Visibility.Visible;

            Pk_label2_tb.Visibility = Visibility.Visible;
            Pk_USBB_bt.Visibility = Visibility.Visible;
            Pk_SMA_bt.Visibility = Visibility.Visible;
            Pk_FMC_bt.Visibility = Visibility.Visible;
            Pk_PCIe_bt.Visibility = Visibility.Visible;
            Pk_Ethernet_bt.Visibility = Visibility.Visible;
            Pk_Bulleye_bt.Visibility = Visibility.Visible;
            #endregion
        }

        private void hide()
        {
            #region RF BD Label
            RF_interface_bd.Visibility = Visibility.Collapsed;

            RF_label_tb.Visibility = Visibility.Collapsed;
            #endregion
            #region RF Enable
            RF_enable_label_tb.Visibility = Visibility.Collapsed;

            RF_enable_status_tb.Visibility = Visibility.Collapsed;
            #endregion
            #region RF sync
            RF_sync_label_tb.Visibility = Visibility.Collapsed;


            RF_sync_status_tb.Visibility = Visibility.Collapsed;
            #endregion
            #region RF error
            RF_error_label_tb.Visibility = Visibility.Collapsed;

            RF_error_status_tb.Visibility = Visibility.Collapsed;
            #endregion
            #region RF rate
            RF_rate_label_tb.Visibility = Visibility.Collapsed;

            RF_rate_status_tb.Visibility = Visibility.Collapsed;
            #endregion

            #region Coe BD Label
            Coefficient_interface_bd.Visibility = Visibility.Collapsed;

            Coe_label_tb.Visibility = Visibility.Collapsed;
            #endregion
            #region Coe Connector

            Coe_connector_bt.Visibility = Visibility.Collapsed;

            Coe_connector_tb.Visibility = Visibility.Collapsed;

            Coe_connector_info_tb.Visibility = Visibility.Collapsed;
            #endregion
            #region Coe Protocol
            Coe_protocol_bt.Visibility = Visibility.Collapsed;

            Coe_protocol_tb.Visibility = Visibility.Collapsed;

            Coe_protocol_info_tb.Visibility = Visibility.Collapsed;
            #endregion
            #region Coe Structure

            Coe_structure_bt.Visibility = Visibility.Collapsed;

            Coe_structure_tb.Visibility = Visibility.Collapsed;

            Coe_structure_info_tb.Visibility = Visibility.Collapsed;
            #endregion
            #region Coe Rate
            Coe_rate_bt.Visibility = Visibility.Collapsed;

            Coe_rate_tb.Visibility = Visibility.Collapsed;

            Coe_rate_info_tb.Visibility = Visibility.Collapsed;
            #endregion

            #region Con BD Label
            Control_interface_bd.Visibility = Visibility.Collapsed;



            Con_label_tb.Visibility = Visibility.Collapsed;
            #endregion
            #region Con Connector
            Con_connector_bt.Visibility = Visibility.Collapsed;

            Con_connector_tb.Visibility = Visibility.Collapsed;

            Con_connector_info_tb.Visibility = Visibility.Collapsed;
            #endregion
            #region Con Protocol
            Con_protocol_bt.Visibility = Visibility.Collapsed;

            Con_protocol_tb.Visibility = Visibility.Collapsed;

            Con_protocol_info_tb.Visibility = Visibility.Collapsed;
            #endregion
            #region Con Structure

            Con_structure_bt.Visibility = Visibility.Collapsed;

            Con_structure_tb.Visibility = Visibility.Collapsed;

            Con_structure_info_tb.Visibility = Visibility.Collapsed;
            #endregion
            #region Con Rate
            Con_rate_bt.Visibility = Visibility.Collapsed;

            Con_rate_tb.Visibility = Visibility.Collapsed;

            Con_rate_info_tb.Visibility = Visibility.Collapsed;
            #endregion
            #region Con Channel

            Con_channel_bt.Visibility = Visibility.Collapsed;

            Con_channel_tb.Visibility = Visibility.Collapsed;

            Con_channel_info_tb.Visibility = Visibility.Collapsed;
            #endregion
            #region Con Domain

            Con_domain_bt.Visibility = Visibility.Collapsed;

            Con_domain_tb.Visibility = Visibility.Collapsed;

            Con_domain_info_tb.Visibility = Visibility.Collapsed;
            #endregion
            #region Con Template
            Con_template_bt.Visibility = Visibility.Collapsed;

            Con_template_tb.Visibility = Visibility.Collapsed;

            Con_template_info_tb.Visibility = Visibility.Collapsed;
            #endregion

            #region Pack BD Label
            Packet_building_bd.Visibility = Visibility.Collapsed;

            Pk_label1_tb.Visibility = Visibility.Collapsed;

            Pk_label2_tb.Visibility = Visibility.Collapsed;
            Pk_USBB_bt.Visibility = Visibility.Collapsed;
            Pk_SMA_bt.Visibility = Visibility.Collapsed;
            Pk_FMC_bt.Visibility = Visibility.Collapsed;
            Pk_PCIe_bt.Visibility = Visibility.Collapsed;
            Pk_Ethernet_bt.Visibility = Visibility.Collapsed;
            Pk_Bulleye_bt.Visibility = Visibility.Collapsed;
            #endregion
        }
    }
    
}

