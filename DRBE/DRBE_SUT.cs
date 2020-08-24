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
    public class DRBE_SUT
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

        private Button Add_SUT_bt = new Button();
        private Image Add_SUT_bti = new Image();
        private TextBlock Add_SUT_tb = new TextBlock();

        private Button Demo_SUT_bt1 = new Button();
        private Image Demo_SUT_bti1 = new Image();
        private TextBlock Demo_SUT_tb1 = new TextBlock();

        private Button Demo_SUT_bt2 = new Button();
        private Image Demo_SUT_bti2 = new Image();
        private TextBlock Demo_SUT_tb2 = new TextBlock();

        private Button Demo_SUT_bt3 = new Button();
        private Image Demo_SUT_bti3 = new Image();
        private TextBlock Demo_SUT_tb3 = new TextBlock();

        private Button Demo_SUT_bt4 = new Button();
        private Image Demo_SUT_bti4 = new Image();
        private TextBlock Demo_SUT_tb4 = new TextBlock();

        private Button Demo_SUT_bt5 = new Button();
        private Image Demo_SUT_bti5 = new Image();
        private TextBlock Demo_SUT_tb5 = new TextBlock();

        private Button Demo_SUT_bt6 = new Button();
        private Image Demo_SUT_bti6 = new Image();
        private TextBlock Demo_SUT_tb6 = new TextBlock();

        private Button Demo_SUT_bt7 = new Button();
        private Image Demo_SUT_bti7 = new Image();
        private TextBlock Demo_SUT_tb7 = new TextBlock();

        private Button Demo_SUT_bt8 = new Button();
        private Image Demo_SUT_bti8 = new Image();


        private TextBlock SUT_status_tb_Interface_header = new TextBlock();
        private TextBlock SUT_status_tb_Interface = new TextBlock();

        private TextBlock SUT_status_tb_Property_header = new TextBlock();
        private TextBlock SUT_status_tb_Property = new TextBlock();

        private Button SUT_status_bt = new Button();
        private Image SUT_status_bti = new Image();

        private Button SUT_Connect_bt = new Button();
        private Image SUT_Connect_bti = new Image();
        private Button SUT_Sync_bt = new Button();
        private Image SUT_Sync_bti = new Image();
        private Button SUT_Refresh_bt = new Button();
        private Image SUT_Refresh_bti = new Image();
        private Button SUT_Check_bt = new Button();
        private Image SUT_Check_bti = new Image();


        public Grid ParentGrid;
        public DRBE_SUT(Grid parent)
        {
            ParentGrid = parent;
            Setup();
            hide();
        }
        private void Setup()
        {
            Add_SUT_bti.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Add_Page.png", UriKind.RelativeOrAbsolute));
            Add_SUT_bt = new Button() {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Add_SUT_bti,
                Foreground = white_button_brush,
                Padding = new Thickness(15,15,15,15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Add_SUT_bt);
            Add_SUT_bt.SetValue(Grid.ColumnProperty, 5);
            Add_SUT_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Add_SUT_bt.SetValue(Grid.RowProperty, 15);
            Add_SUT_bt.SetValue(Grid.RowSpanProperty, 20);

            Add_SUT_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                Text = "Import SUT",
                Foreground = white_button_brush,
                FontSize = 12
            };
            ParentGrid.Children.Add(Add_SUT_tb);
            Add_SUT_tb.SetValue(Grid.ColumnProperty, 5);
            Add_SUT_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Add_SUT_tb.SetValue(Grid.RowProperty, 36);
            Add_SUT_tb.SetValue(Grid.RowSpanProperty, 7);

            Demo_SUT_tb1 = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                Text = "Weather Radar WR1",
                Foreground = white_button_brush,
                FontSize = 12
            };
            ParentGrid.Children.Add(Demo_SUT_tb1);
            Demo_SUT_tb1.SetValue(Grid.ColumnProperty, 25);
            Demo_SUT_tb1.SetValue(Grid.ColumnSpanProperty, 20);
            Demo_SUT_tb1.SetValue(Grid.RowProperty, 36);
            Demo_SUT_tb1.SetValue(Grid.RowSpanProperty, 7);

            Demo_SUT_bti1.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/radar_icon1.png", UriKind.RelativeOrAbsolute));
            Demo_SUT_bt1 = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Demo_SUT_bti1,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Demo_SUT_bt1);
            Demo_SUT_bt1.SetValue(Grid.ColumnProperty, 25);
            Demo_SUT_bt1.SetValue(Grid.ColumnSpanProperty, 20);
            Demo_SUT_bt1.SetValue(Grid.RowProperty, 15);
            Demo_SUT_bt1.SetValue(Grid.RowSpanProperty, 20);

            Demo_SUT_tb2 = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                Text = "Weather Radar WR2",
                Foreground = white_button_brush,
                FontSize = 12
            };
            ParentGrid.Children.Add(Demo_SUT_tb2);
            Demo_SUT_tb2.SetValue(Grid.ColumnProperty, 45);
            Demo_SUT_tb2.SetValue(Grid.ColumnSpanProperty, 20);
            Demo_SUT_tb2.SetValue(Grid.RowProperty, 36);
            Demo_SUT_tb2.SetValue(Grid.RowSpanProperty, 7);


            Demo_SUT_bti2.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/radar_icon1.png", UriKind.RelativeOrAbsolute));
            Demo_SUT_bt2 = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Demo_SUT_bti2,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Demo_SUT_bt2);
            Demo_SUT_bt2.SetValue(Grid.ColumnProperty, 45);
            Demo_SUT_bt2.SetValue(Grid.ColumnSpanProperty, 20);
            Demo_SUT_bt2.SetValue(Grid.RowProperty, 15);
            Demo_SUT_bt2.SetValue(Grid.RowSpanProperty, 20);

            Demo_SUT_tb3 = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                Text = "Ground Radar GR1",
                Foreground = white_button_brush,
                FontSize = 12
            };
            ParentGrid.Children.Add(Demo_SUT_tb3);
            Demo_SUT_tb3.SetValue(Grid.ColumnProperty, 5);
            Demo_SUT_tb3.SetValue(Grid.ColumnSpanProperty, 20);
            Demo_SUT_tb3.SetValue(Grid.RowProperty, 66);
            Demo_SUT_tb3.SetValue(Grid.RowSpanProperty, 7);

            Demo_SUT_bti3.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/radar_icon2.png", UriKind.RelativeOrAbsolute));
            Demo_SUT_bt3 = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Demo_SUT_bti3,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Demo_SUT_bt3);
            Demo_SUT_bt3.SetValue(Grid.ColumnProperty, 5);
            Demo_SUT_bt3.SetValue(Grid.ColumnSpanProperty, 20);
            Demo_SUT_bt3.SetValue(Grid.RowProperty, 45);
            Demo_SUT_bt3.SetValue(Grid.RowSpanProperty, 20);

            Demo_SUT_tb4 = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                Text = "Ground Radar GR2",
                Foreground = white_button_brush,
                FontSize = 12
            };
            ParentGrid.Children.Add(Demo_SUT_tb4);
            Demo_SUT_tb4.SetValue(Grid.ColumnProperty, 25);
            Demo_SUT_tb4.SetValue(Grid.ColumnSpanProperty, 20);
            Demo_SUT_tb4.SetValue(Grid.RowProperty, 66);
            Demo_SUT_tb4.SetValue(Grid.RowSpanProperty, 7);

            Demo_SUT_bti4.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/radar_icon2.png", UriKind.RelativeOrAbsolute));
            Demo_SUT_bt4 = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Demo_SUT_bti4,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                BorderBrush = Teal_color,
                BorderThickness = new Thickness(3,3,3,3),
                FontSize = 18
            };
            ParentGrid.Children.Add(Demo_SUT_bt4);
            Demo_SUT_bt4.SetValue(Grid.ColumnProperty, 25);
            Demo_SUT_bt4.SetValue(Grid.ColumnSpanProperty, 20);
            Demo_SUT_bt4.SetValue(Grid.RowProperty, 45);
            Demo_SUT_bt4.SetValue(Grid.RowSpanProperty, 20);

            Demo_SUT_tb5 = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                Text = "Weather Radar WR3",
                Foreground = white_button_brush,
                FontSize = 12
            };
            ParentGrid.Children.Add(Demo_SUT_tb5);
            Demo_SUT_tb5.SetValue(Grid.ColumnProperty, 45);
            Demo_SUT_tb5.SetValue(Grid.ColumnSpanProperty, 20);
            Demo_SUT_tb5.SetValue(Grid.RowProperty, 66);
            Demo_SUT_tb5.SetValue(Grid.RowSpanProperty, 7);

            Demo_SUT_bti5.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/radar_icon1.png", UriKind.RelativeOrAbsolute));
            Demo_SUT_bt5 = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Demo_SUT_bti5,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Demo_SUT_bt5);
            Demo_SUT_bt5.SetValue(Grid.ColumnProperty, 45);
            Demo_SUT_bt5.SetValue(Grid.ColumnSpanProperty, 20);
            Demo_SUT_bt5.SetValue(Grid.RowProperty, 45);
            Demo_SUT_bt5.SetValue(Grid.RowSpanProperty, 20);

            Demo_SUT_tb6 = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                Text = "Phase Array PA1",
                Foreground = white_button_brush,
                FontSize = 12
            };
            ParentGrid.Children.Add(Demo_SUT_tb6);
            Demo_SUT_tb6.SetValue(Grid.ColumnProperty, 5);
            Demo_SUT_tb6.SetValue(Grid.ColumnSpanProperty, 20);
            Demo_SUT_tb6.SetValue(Grid.RowProperty, 96);
            Demo_SUT_tb6.SetValue(Grid.RowSpanProperty, 7);

            Demo_SUT_bti6.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/radar_scan1.jpg", UriKind.RelativeOrAbsolute));
            Demo_SUT_bt6 = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Demo_SUT_bti6,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Demo_SUT_bt6);
            Demo_SUT_bt6.SetValue(Grid.ColumnProperty, 5);
            Demo_SUT_bt6.SetValue(Grid.ColumnSpanProperty, 20);
            Demo_SUT_bt6.SetValue(Grid.RowProperty, 75);
            Demo_SUT_bt6.SetValue(Grid.RowSpanProperty, 20);

            Demo_SUT_tb7 = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                Text = "Ground Radar GR3",
                Foreground = white_button_brush,
                FontSize = 12
            };
            ParentGrid.Children.Add(Demo_SUT_tb7);
            Demo_SUT_tb7.SetValue(Grid.ColumnProperty, 25);
            Demo_SUT_tb7.SetValue(Grid.ColumnSpanProperty, 20);
            Demo_SUT_tb7.SetValue(Grid.RowProperty, 96);
            Demo_SUT_tb7.SetValue(Grid.RowSpanProperty, 7);


            Demo_SUT_bti7.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/radar_icon2.png", UriKind.RelativeOrAbsolute));
            Demo_SUT_bt7 = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Demo_SUT_bti7,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Demo_SUT_bt7);
            Demo_SUT_bt7.SetValue(Grid.ColumnProperty, 25);
            Demo_SUT_bt7.SetValue(Grid.ColumnSpanProperty, 20);
            Demo_SUT_bt7.SetValue(Grid.RowProperty, 75);
            Demo_SUT_bt7.SetValue(Grid.RowSpanProperty, 20);

            SUT_Connect_bti.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/FPGAs-Icon_4x.jpg", UriKind.RelativeOrAbsolute));
            SUT_Connect_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = SUT_Connect_bti,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(SUT_Connect_bt);
            SUT_Connect_bt.SetValue(Grid.ColumnProperty, 90);
            SUT_Connect_bt.SetValue(Grid.ColumnSpanProperty, 15);
            SUT_Connect_bt.SetValue(Grid.RowProperty, 50);
            SUT_Connect_bt.SetValue(Grid.RowSpanProperty, 15);

            SUT_Sync_bti.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Link.png", UriKind.RelativeOrAbsolute));
            SUT_Sync_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = SUT_Sync_bti,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(SUT_Sync_bt);
            SUT_Sync_bt.SetValue(Grid.ColumnProperty, 105);
            SUT_Sync_bt.SetValue(Grid.ColumnSpanProperty, 15);
            SUT_Sync_bt.SetValue(Grid.RowProperty, 50);
            SUT_Sync_bt.SetValue(Grid.RowSpanProperty, 15);

            SUT_Refresh_bti.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Refresh.png", UriKind.RelativeOrAbsolute));
            SUT_Refresh_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = SUT_Refresh_bti,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(SUT_Refresh_bt);
            SUT_Refresh_bt.SetValue(Grid.ColumnProperty, 90);
            SUT_Refresh_bt.SetValue(Grid.ColumnSpanProperty, 15);
            SUT_Refresh_bt.SetValue(Grid.RowProperty, 70);
            SUT_Refresh_bt.SetValue(Grid.RowSpanProperty, 15);

            SUT_Check_bti.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Check.png", UriKind.RelativeOrAbsolute));
            SUT_Check_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = SUT_Check_bti,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(SUT_Check_bt);
            SUT_Check_bt.SetValue(Grid.ColumnProperty, 105);
            SUT_Check_bt.SetValue(Grid.ColumnSpanProperty, 15);
            SUT_Check_bt.SetValue(Grid.RowProperty, 70);
            SUT_Check_bt.SetValue(Grid.RowSpanProperty, 15);


            SUT_status_bti.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/radar_icon2.png", UriKind.RelativeOrAbsolute));
            SUT_status_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = SUT_status_bti,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(SUT_status_bt);
            SUT_status_bt.SetValue(Grid.ColumnProperty, 100);
            SUT_status_bt.SetValue(Grid.ColumnSpanProperty, 20);
            SUT_status_bt.SetValue(Grid.RowProperty, 10);
            SUT_status_bt.SetValue(Grid.RowSpanProperty, 30);



            SUT_status_tb_Interface_header = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                Text = "Interface Properties",
                
                Foreground = white_button_brush,
                FontSize = 22,
                FontWeight = FontWeights.ExtraBold
            };
            ParentGrid.Children.Add(SUT_status_tb_Interface_header);
            SUT_status_tb_Interface_header.SetValue(Grid.ColumnProperty, 130);
            SUT_status_tb_Interface_header.SetValue(Grid.ColumnSpanProperty, 60);
            SUT_status_tb_Interface_header.SetValue(Grid.RowProperty, 10);
            SUT_status_tb_Interface_header.SetValue(Grid.RowSpanProperty, 10);



            SUT_status_tb_Interface = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Text = "",
                Foreground = white_button_brush,
                FontSize = 12
                
            };
            ParentGrid.Children.Add(SUT_status_tb_Interface);
            SUT_status_tb_Interface.SetValue(Grid.ColumnProperty, 130);
            SUT_status_tb_Interface.SetValue(Grid.ColumnSpanProperty, 60);
            SUT_status_tb_Interface.SetValue(Grid.RowProperty, 20);
            SUT_status_tb_Interface.SetValue(Grid.RowSpanProperty, 30);

            //tb.Inlines.Add(new Run("bold") { FontWeight = FontWeights.Bold });
            //tb.Inlines.Add(new Run("italic ") { FontStyle = FontStyles.Italic });
            //tb.Inlines.Add(new Run("underlined") { TextDecorations = TextDecorations.Underline });

            SUT_status_tb_Interface.Inlines.Add(new Run() { Text = "Transmitter ID:          ", Foreground = white_button_brush, FontSize = 18, FontWeight = FontWeights.ExtraBold });
            SUT_status_tb_Interface.Inlines.Add(new Run() { Text = "Ground Radar GR2\r\n", Foreground = white_button_brush, FontSize = 18, FontWeight = FontWeights.Light });
            SUT_status_tb_Interface.Inlines.Add(new Run() { Text = "Interface Type:          ", Foreground = white_button_brush, FontSize = 18, FontWeight = FontWeights.ExtraBold });
            SUT_status_tb_Interface.Inlines.Add(new Run() { Text = "PGR2-UART-115200\r\n", Foreground = white_button_brush, FontSize = 18, FontWeight = FontWeights.Light });
            SUT_status_tb_Interface.Inlines.Add(new Run() { Text = "Involved Links:          ", Foreground = white_button_brush, FontSize = 18, FontWeight = FontWeights.ExtraBold });
            SUT_status_tb_Interface.Inlines.Add(new Run() { Text = "17\r\n", Foreground = white_button_brush, FontSize = 18, FontWeight = FontWeights.Light });
            SUT_status_tb_Interface.Inlines.Add(new Run() { Text = "Interface Connection:    ", Foreground = white_button_brush, FontSize = 18, FontWeight = FontWeights.ExtraBold });
            SUT_status_tb_Interface.Inlines.Add(new Run() { Text = "Connected\r\n", Foreground = green_bright_button_brush, FontSize = 18, FontWeight = FontWeights.Bold });
            SUT_status_tb_Interface.Inlines.Add(new Run() { Text = "Update Rate:                   ", Foreground = white_button_brush, FontSize = 18, FontWeight = FontWeights.ExtraBold });
            SUT_status_tb_Interface.Inlines.Add(new Run() { Text = "<2000/s , 500us>\r\n", Foreground = orange_brush, FontSize = 18, FontWeight = FontWeights.Bold });


            SUT_status_tb_Property_header = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center,
                Text = "SUT Properties",

                Foreground = white_button_brush,
                FontSize = 22,
                FontWeight = FontWeights.ExtraBold
            };
            ParentGrid.Children.Add(SUT_status_tb_Property_header);
            SUT_status_tb_Property_header.SetValue(Grid.ColumnProperty, 130);
            SUT_status_tb_Property_header.SetValue(Grid.ColumnSpanProperty, 60);
            SUT_status_tb_Property_header.SetValue(Grid.RowProperty, 60);
            SUT_status_tb_Property_header.SetValue(Grid.RowSpanProperty, 10);

            SUT_status_tb_Property = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Text = "",
                Foreground = white_button_brush,
                FontSize = 12

            };
            ParentGrid.Children.Add(SUT_status_tb_Property);
            SUT_status_tb_Property.SetValue(Grid.ColumnProperty, 130);
            SUT_status_tb_Property.SetValue(Grid.ColumnSpanProperty, 60);
            SUT_status_tb_Property.SetValue(Grid.RowProperty, 70);
            SUT_status_tb_Property.SetValue(Grid.RowSpanProperty, 30);

            SUT_status_tb_Property.Inlines.Add(new Run() { Text = "Slowest Link ID:          ", Foreground = white_button_brush, FontSize = 18, FontWeight = FontWeights.ExtraBold });
            SUT_status_tb_Property.Inlines.Add(new Run() { Text = "GR2-FA112-GR1\r\n", Foreground = white_button_brush, FontSize = 18, FontWeight = FontWeights.Light });
            SUT_status_tb_Property.Inlines.Add(new Run() { Text = "Slowest Link Delay:          ", Foreground = white_button_brush, FontSize = 18, FontWeight = FontWeights.ExtraBold });
            SUT_status_tb_Property.Inlines.Add(new Run() { Text = "300 us\r\n", Foreground = orange_brush, FontSize = 18, FontWeight = FontWeights.Bold });
            SUT_status_tb_Property.Inlines.Add(new Run() { Text = "Slowest Link Range:          ", Foreground = white_button_brush, FontSize = 18, FontWeight = FontWeights.ExtraBold });
            SUT_status_tb_Property.Inlines.Add(new Run() { Text = "90Km\r\n", Foreground = orange_brush, FontSize = 18, FontWeight = FontWeights.Bold });
            SUT_status_tb_Property.Inlines.Add(new Run() { Text = "Fastest Link ID:    ", Foreground = white_button_brush, FontSize = 18, FontWeight = FontWeights.ExtraBold });
            SUT_status_tb_Property.Inlines.Add(new Run() { Text = "GR2-FA28-WR3\r\n", Foreground = white_button_brush, FontSize = 18, FontWeight = FontWeights.Light });
            SUT_status_tb_Property.Inlines.Add(new Run() { Text = "Fastest Link Delay:          ", Foreground = white_button_brush, FontSize = 18, FontWeight = FontWeights.ExtraBold });
            SUT_status_tb_Property.Inlines.Add(new Run() { Text = "30 us\r\n", Foreground = orange_brush, FontSize = 18, FontWeight = FontWeights.Bold });
            SUT_status_tb_Property.Inlines.Add(new Run() { Text = "Fastest Link Range:          ", Foreground = white_button_brush, FontSize = 18, FontWeight = FontWeights.ExtraBold });
            SUT_status_tb_Property.Inlines.Add(new Run() { Text = "9Km\r\n", Foreground = orange_brush, FontSize = 18, FontWeight = FontWeights.Bold });


        }
        private void hide()
        {
            Add_SUT_bt.Visibility = Visibility.Collapsed;

            Add_SUT_tb.Visibility = Visibility.Collapsed;

            Demo_SUT_tb1.Visibility = Visibility.Collapsed;
            Demo_SUT_bt1.Visibility = Visibility.Collapsed;

            Demo_SUT_tb2.Visibility = Visibility.Collapsed;
            Demo_SUT_bt2.Visibility = Visibility.Collapsed;

            Demo_SUT_tb3.Visibility = Visibility.Collapsed;
            Demo_SUT_bt3.Visibility = Visibility.Collapsed;

            Demo_SUT_tb4.Visibility = Visibility.Collapsed;
            Demo_SUT_bt4.Visibility = Visibility.Collapsed;

            Demo_SUT_tb5.Visibility = Visibility.Collapsed;
            Demo_SUT_bt5.Visibility = Visibility.Collapsed;

            Demo_SUT_tb6.Visibility = Visibility.Collapsed;
            Demo_SUT_bt6.Visibility = Visibility.Collapsed;

            Demo_SUT_tb7.Visibility = Visibility.Collapsed;
            Demo_SUT_bt7.Visibility = Visibility.Collapsed;
            SUT_Connect_bt.Visibility = Visibility.Collapsed;
            SUT_Sync_bt.Visibility = Visibility.Collapsed;
            SUT_Refresh_bt.Visibility = Visibility.Collapsed;
            SUT_Check_bt.Visibility = Visibility.Collapsed;
            SUT_status_bt.Visibility = Visibility.Collapsed;



            SUT_status_tb_Interface_header.Visibility = Visibility.Collapsed;



            SUT_status_tb_Interface.Visibility = Visibility.Collapsed;


            SUT_status_tb_Property_header.Visibility = Visibility.Collapsed;

            SUT_status_tb_Property.Visibility = Visibility.Collapsed; 
  
        }
    }
}
