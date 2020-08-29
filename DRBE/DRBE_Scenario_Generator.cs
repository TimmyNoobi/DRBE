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
    public class DRBE_Scenario_Generator
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
        private Save_Screen DRBE_SS;
        public DRBE_Link_Viewer_s SC_Dlv;



        public DRBE_Scenario_Generator(Grid parent, MainPage parentpage)
        {
            ParentGrid = parent;
            ParentPage = parentpage;
            //hide();
            DRBE_SS = new Save_Screen(parent);
            SC_Dlv = new DRBE_Link_Viewer_s(parent, ParentPage);
            Setup();
            hide();
        }

        private int ID_index = 0;

        private Button Generate_Random_bt = new Button();
        private Button Add_Transmitter_bt = new Button();
        private Button Add_Reflector_bt = new Button();
        private Button Add_Receiver_bt = new Button();

        
        private int Number_of_Transmitter = 1;
        private int Number_of_Reflector = 1;
        private int Number_of_Receiver = 1;

        private TextBox Number_of_transmitter_tb = new TextBox();
        private TextBlock Number_of_transmitter_ttb = new TextBlock();

        private TextBox Number_of_reflector_tb = new TextBox();
        private TextBlock Number_of_reflector_ttb = new TextBlock();

        private TextBox Number_of_receiver_tb = new TextBox();
        private TextBlock Number_of_receiver_ttb = new TextBlock();

        private Button Global_Property_bt = new Button();
        private Button Initial_Property_bt = new Button();
        private Button All_Property_bt = new Button();
        private Button Antenna_Pol_Property_bt = new Button();
        private Button Clut_RFim_Property_bt = new Button();
        private Button RCS_Property_bt = new Button();
        private Button Constraint_Property_bt = new Button();
        private Button Lookup_table_bt = new Button();









        private TextBlock Sc_save_tb = new TextBlock();
        private Button Sc_save_bt = new Button();
        private Image Sc_save_i = new Image();

        private TextBlock Sc_load_tb = new TextBlock();
        private ComboBox Sc_load_cb = new ComboBox();
        private Button Sc_load_bt = new Button();
        private Image Sc_load_i = new Image();

        private TextBlock Obj_del_tb = new TextBlock();
        private Button Obj_del_bt = new Button(); 
        private Image Obj_del_i = new Image();

        private TextBlock Obj_save_tb = new TextBlock();
        private Button Obj_save_bt = new Button();
        private Image Obj_save_i = new Image();

        private TextBlock Obj_load_tb = new TextBlock();
        private ComboBox Obj_load_cb = new ComboBox();
        private Button Obj_load_bt = new Button();
        private Image Obj_load_i = new Image();


        private Button Gen_Lv_view = new Button();





        private TextBlock Object_ID_tb = new TextBlock();



        private Border Info_bd = new Border();



        private List<Button> Transmitter_btl = new List<Button>();
        private List<Button> Reflector_btl = new List<Button>();
        private List<Button> Receiver_btl = new List<Button>();

        private Dictionary<Button, DRBE_Transmitter> Dic_bt_drbet = new Dictionary<Button, DRBE_Transmitter>();
        private Dictionary<Button, DRBE_Reflector> Dic_bt_drbef = new Dictionary<Button, DRBE_Reflector>();
        private Dictionary<Button, DRBE_Receiver> Dic_bt_drber = new Dictionary<Button, DRBE_Receiver>();

        private TextBlock Coordinate_system_tb = new TextBlock();
        private ComboBox Coordinate_system_cb = new ComboBox();
        private Border Coordinate_system_bd = new Border();
        private Border Coordinate_system_bd1 = new Border();

        private TextBlock Generate_obj_tb = new TextBlock();
        private Button Generate_obj_bt = new Button();
        private Image Generate_obj_i = new Image();



        private TextBlock Lv_view_tb = new TextBlock();
        private Button Lv_view_bt = new Button();
        private Image Lv_view_i = new Image();

        private ScrollViewer DRBE_SV = new ScrollViewer();
        private StackPanel DRBE_SPL = new StackPanel();
        private StackPanel DRBE_SPR = new StackPanel();
        private Grid DRBE_GD = new Grid();
        
        public void Setup()
        {
            DRBE_GD = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush
                
            };
            DRBE_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            DRBE_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            //DRBE_GD.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            DRBE_SV = new ScrollViewer() { 
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                VerticalScrollMode = ScrollMode.Enabled,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.5,0.5,0.5,0.5)
            };
            ParentGrid.Children.Add(DRBE_SV);
            DRBE_SV.SetValue(Grid.ColumnProperty, 160);
            DRBE_SV.SetValue(Grid.ColumnSpanProperty, 40);
            DRBE_SV.SetValue(Grid.RowProperty, 40);
            DRBE_SV.SetValue(Grid.RowSpanProperty, 110);


            DRBE_SPL = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            };
            DRBE_SV.Content = DRBE_GD;
            DRBE_GD.Children.Add(DRBE_SPL);
            //ParentGrid.Children.Add(DRBE_SPL);
            DRBE_SPL.SetValue(Grid.ColumnProperty, 0);
            DRBE_SPL.SetValue(Grid.ColumnSpanProperty, 1);
            //DRBE_SPL.SetValue(Grid.RowProperty, 40);
            //DRBE_SPL.SetValue(Grid.RowSpanProperty, 110);

            DRBE_SPR = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            };
            //DRBE_SV.Content = DRBE_SPL;
            DRBE_GD.Children.Add(DRBE_SPR);
            //ParentGrid.Children.Add(DRBE_SPL);
            DRBE_SPR.SetValue(Grid.ColumnProperty, 1);
            DRBE_SPR.SetValue(Grid.ColumnSpanProperty, 1);
            //DRBE_SPL.SetValue(Grid.RowProperty, 40);
            //DRBE_SPL.SetValue(Grid.RowSpanProperty, 110);


            #region generate
            Generate_obj_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Generate",
                Foreground = green_bright_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Generate_obj_tb);
            Generate_obj_tb.SetValue(Grid.ColumnProperty, 20);
            Generate_obj_tb.SetValue(Grid.ColumnSpanProperty, 10);
            Generate_obj_tb.SetValue(Grid.RowProperty, 5);
            Generate_obj_tb.SetValue(Grid.RowSpanProperty, 5);

            Generate_obj_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Input.png", UriKind.RelativeOrAbsolute));
            Generate_obj_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Generate_obj_i,
                Foreground = white_button_brush,
                BorderBrush = green_bright_button_brush,
                BorderThickness = new Thickness(2,2,2,2),
                FontSize = 18
            };
            ParentGrid.Children.Add(Generate_obj_bt);
            Generate_obj_bt.SetValue(Grid.ColumnProperty, 20);
            Generate_obj_bt.SetValue(Grid.ColumnSpanProperty, 10);
            Generate_obj_bt.SetValue(Grid.RowProperty, 10);
            Generate_obj_bt.SetValue(Grid.RowSpanProperty, 10);
            Generate_obj_bt.Click += Generate_obj_bt_Click;
            #endregion

            #region LV page
            Lv_view_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Link Model",
                Foreground = white_button_brush,
                FontSize = 18,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Lv_view_tb);
            Lv_view_tb.SetValue(Grid.ColumnProperty, 130);
            Lv_view_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Lv_view_tb.SetValue(Grid.RowProperty, 0);
            Lv_view_tb.SetValue(Grid.RowSpanProperty, 5);

            Lv_view_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Constraint.jpg", UriKind.RelativeOrAbsolute));
            Lv_view_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Lv_view_i,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Lv_view_bt);
            Lv_view_bt.SetValue(Grid.ColumnProperty, 130);
            Lv_view_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Lv_view_bt.SetValue(Grid.RowProperty, 5);
            Lv_view_bt.SetValue(Grid.RowSpanProperty, 10);
            Lv_view_bt.Click += Lv_view_bt_Click;
            #endregion

            #region Save scenario
            Sc_save_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Save Scenario",
                Foreground = white_button_brush,
                FontSize = 18,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Sc_save_tb);
            Sc_save_tb.SetValue(Grid.ColumnProperty, 147);
            Sc_save_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Sc_save_tb.SetValue(Grid.RowProperty, 0);
            Sc_save_tb.SetValue(Grid.RowSpanProperty, 5);

            Sc_save_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Save_icon.png", UriKind.RelativeOrAbsolute));
            Sc_save_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Sc_save_i,
                Foreground = white_button_brush,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5),
                FontSize = 18
            };
            ParentGrid.Children.Add(Sc_save_bt);
            Sc_save_bt.SetValue(Grid.ColumnProperty, 175);
            Sc_save_bt.SetValue(Grid.ColumnSpanProperty, 4);
            Sc_save_bt.SetValue(Grid.RowProperty, 5);
            Sc_save_bt.SetValue(Grid.RowSpanProperty, 4);
            Sc_save_bt.Click += Sc_save_bt_Click;
            #endregion

            #region Load scenario
            Sc_load_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Load Scenario",
                Foreground = white_button_brush,
                FontSize = 18,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Sc_load_tb);
            Sc_load_tb.SetValue(Grid.ColumnProperty, 190);
            Sc_load_tb.SetValue(Grid.ColumnSpanProperty, 6);
            Sc_load_tb.SetValue(Grid.RowProperty, 0);
            Sc_load_tb.SetValue(Grid.RowSpanProperty, 4);

            Sc_load_cb = new ComboBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                SelectedIndex = 0,
                Background = white_button_brush,
                FontSize = 10
            };
            ParentGrid.Children.Add(Sc_load_cb);
            Sc_load_cb.SetValue(Grid.ColumnProperty, 180);
            Sc_load_cb.SetValue(Grid.ColumnSpanProperty, 16);
            Sc_load_cb.SetValue(Grid.RowProperty, 5);
            Sc_load_cb.SetValue(Grid.RowSpanProperty, 4);

            Sc_load_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Load_icon.jpg", UriKind.RelativeOrAbsolute));
            Sc_load_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Sc_load_i,
                Foreground = white_button_brush,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5),
                FontSize = 18
            };
            ParentGrid.Children.Add(Sc_load_bt);
            Sc_load_bt.SetValue(Grid.ColumnProperty, 196);
            Sc_load_bt.SetValue(Grid.ColumnSpanProperty, 4);
            Sc_load_bt.SetValue(Grid.RowProperty, 5);
            Sc_load_bt.SetValue(Grid.RowSpanProperty, 4);
            Sc_load_bt.Click += Sc_load_bt_Click;
            #endregion

            #region Obj Save
            Obj_save_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Save Object",
                Foreground = white_button_brush,
                FontSize = 18,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Obj_save_tb);
            Obj_save_tb.SetValue(Grid.ColumnProperty, 150);
            Obj_save_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Obj_save_tb.SetValue(Grid.RowProperty, 25);
            Obj_save_tb.SetValue(Grid.RowSpanProperty, 5);

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
                BorderThickness = new Thickness(0.5,0.5,0.5,0.5),
                BorderBrush = dark_grey_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Obj_save_bt);
            Obj_save_bt.SetValue(Grid.ColumnProperty, 175);
            Obj_save_bt.SetValue(Grid.ColumnSpanProperty, 4);
            Obj_save_bt.SetValue(Grid.RowProperty, 31);
            Obj_save_bt.SetValue(Grid.RowSpanProperty, 4);
            Obj_save_bt.Click += Obj_save_bt_Click;
            #endregion

            #region Object delete
            Obj_del_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Delete Object",
                Foreground = white_button_brush,
                FontSize = 18,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Obj_del_tb);
            Obj_del_tb.SetValue(Grid.ColumnProperty, 135);
            Obj_del_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Obj_del_tb.SetValue(Grid.RowProperty, 25);
            Obj_del_tb.SetValue(Grid.RowSpanProperty, 5);

            Obj_del_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Trashcan_icon.png", UriKind.RelativeOrAbsolute));
            Obj_del_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Obj_del_i,
                Foreground = white_button_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5),
                BorderBrush = dark_grey_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Obj_del_bt);
            Obj_del_bt.SetValue(Grid.ColumnProperty, 170);
            Obj_del_bt.SetValue(Grid.ColumnSpanProperty, 4);
            Obj_del_bt.SetValue(Grid.RowProperty, 31);
            Obj_del_bt.SetValue(Grid.RowSpanProperty, 4);
            #endregion

            #region Load Object
            Obj_load_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Load Object",
                Foreground = white_button_brush,
                FontSize = 18,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Obj_load_tb);
            Obj_load_tb.SetValue(Grid.ColumnProperty, 165);
            Obj_load_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_load_tb.SetValue(Grid.RowProperty, 25);
            Obj_load_tb.SetValue(Grid.RowSpanProperty, 5);

            Obj_load_cb = new ComboBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                SelectedIndex = 0,
                Background = white_button_brush
            };
            ParentGrid.Children.Add(Obj_load_cb);
            Obj_load_cb.SetValue(Grid.ColumnProperty, 180);
            Obj_load_cb.SetValue(Grid.ColumnSpanProperty, 15);
            Obj_load_cb.SetValue(Grid.RowProperty, 30);
            Obj_load_cb.SetValue(Grid.RowSpanProperty, 4);

            Obj_load_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Load_icon.jpg", UriKind.RelativeOrAbsolute));
            Obj_load_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Obj_load_i,
                Foreground = white_button_brush,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.5,0.5,0.5,0.5),
                FontSize = 18
            };
            ParentGrid.Children.Add(Obj_load_bt);
            Obj_load_bt.SetValue(Grid.ColumnProperty, 195);
            Obj_load_bt.SetValue(Grid.ColumnSpanProperty, 4);
            Obj_load_bt.SetValue(Grid.RowProperty, 31);
            Obj_load_bt.SetValue(Grid.RowSpanProperty, 4);
            Obj_load_bt.Click += Obj_load_bt_Click;
            #endregion


            #region unmarked
            Object_ID_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Object ID: ",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold,
                Width = 200,
                Height = 50
            };
            ParentGrid.Children.Add(Object_ID_tb);
            Object_ID_tb.SetValue(Grid.ColumnProperty, 161);
            Object_ID_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Object_ID_tb.SetValue(Grid.RowProperty, 36);
            Object_ID_tb.SetValue(Grid.RowSpanProperty, 4);
            //DRBE_SP.Children.Add(Object_ID_tb);

            Number_of_receiver_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Number of Receiver",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Number_of_receiver_ttb);
            Number_of_receiver_ttb.SetValue(Grid.ColumnProperty, 0);
            Number_of_receiver_ttb.SetValue(Grid.ColumnSpanProperty, 17);
            Number_of_receiver_ttb.SetValue(Grid.RowProperty, 15);
            Number_of_receiver_ttb.SetValue(Grid.RowSpanProperty, 5);

            Number_of_receiver_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Number_of_receiver_tb);
            Number_of_receiver_tb.SetValue(Grid.ColumnProperty, 15);
            Number_of_receiver_tb.SetValue(Grid.ColumnSpanProperty, 5);
            Number_of_receiver_tb.SetValue(Grid.RowProperty, 15);
            Number_of_receiver_tb.SetValue(Grid.RowSpanProperty, 5);

            Number_of_reflector_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Number of Reflector",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Number_of_reflector_ttb);
            Number_of_reflector_ttb.SetValue(Grid.ColumnProperty, 0);
            Number_of_reflector_ttb.SetValue(Grid.ColumnSpanProperty, 17);
            Number_of_reflector_ttb.SetValue(Grid.RowProperty, 10);
            Number_of_reflector_ttb.SetValue(Grid.RowSpanProperty, 5);

            Number_of_reflector_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Reflector.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Number_of_reflector_tb);
            Number_of_reflector_tb.SetValue(Grid.ColumnProperty, 15);
            Number_of_reflector_tb.SetValue(Grid.ColumnSpanProperty, 5);
            Number_of_reflector_tb.SetValue(Grid.RowProperty, 10);
            Number_of_reflector_tb.SetValue(Grid.RowSpanProperty, 5);

            Number_of_transmitter_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Number of Transmitter",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Number_of_transmitter_ttb);
            Number_of_transmitter_ttb.SetValue(Grid.ColumnProperty, 0);
            Number_of_transmitter_ttb.SetValue(Grid.ColumnSpanProperty, 17);
            Number_of_transmitter_ttb.SetValue(Grid.RowProperty, 5);
            Number_of_transmitter_ttb.SetValue(Grid.RowSpanProperty, 5);

            Number_of_transmitter_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Transmitter.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Number_of_transmitter_tb);
            Number_of_transmitter_tb.SetValue(Grid.ColumnProperty, 15);
            Number_of_transmitter_tb.SetValue(Grid.ColumnSpanProperty, 5);
            Number_of_transmitter_tb.SetValue(Grid.RowProperty, 5);
            Number_of_transmitter_tb.SetValue(Grid.RowSpanProperty, 5);

            Global_Property_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "Basic: \r\n Global Properties",
                Foreground = white_button_brush,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 10
            };
            ParentGrid.Children.Add(Global_Property_bt);
            Global_Property_bt.SetValue(Grid.ColumnProperty, 160);
            Global_Property_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Global_Property_bt.SetValue(Grid.RowProperty, 10);
            Global_Property_bt.SetValue(Grid.RowSpanProperty, 5);
            Global_Property_bt.Click += Global_Property_bt_Click;

            Initial_Property_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "Basic: \r\n Initial Properties",
                Foreground = white_button_brush,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(0.5,0.5,0.5,0),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 10
            };
            ParentGrid.Children.Add(Initial_Property_bt);
            Initial_Property_bt.SetValue(Grid.ColumnProperty, 180);
            Initial_Property_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Initial_Property_bt.SetValue(Grid.RowProperty, 10);
            Initial_Property_bt.SetValue(Grid.RowSpanProperty, 5);
            Initial_Property_bt.Click += Initial_Property_bt_Click;

            All_Property_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "All Properties",
                Foreground = white_button_brush,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 10
            };
            ParentGrid.Children.Add(All_Property_bt);
            All_Property_bt.SetValue(Grid.ColumnProperty, 180);
            All_Property_bt.SetValue(Grid.ColumnSpanProperty, 20);
            All_Property_bt.SetValue(Grid.RowProperty, 25);
            All_Property_bt.SetValue(Grid.RowSpanProperty, 5);
            All_Property_bt.Click += All_Property_bt_Click;

            Antenna_Pol_Property_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "Adv:   Antenna/\r\nPolarization",
                Foreground = white_button_brush,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 10
            };
            ParentGrid.Children.Add(Antenna_Pol_Property_bt);
            Antenna_Pol_Property_bt.SetValue(Grid.ColumnProperty, 160);
            Antenna_Pol_Property_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Antenna_Pol_Property_bt.SetValue(Grid.RowProperty, 15);
            Antenna_Pol_Property_bt.SetValue(Grid.RowSpanProperty, 5);
            Antenna_Pol_Property_bt.Click += Antenna_Pol_Property_bt_Click;

            Clut_RFim_Property_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "Adv:   Clutter/\r\nRF impairment",
                Foreground = white_button_brush,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 10
            };
            ParentGrid.Children.Add(Clut_RFim_Property_bt);
            Clut_RFim_Property_bt.SetValue(Grid.ColumnProperty, 180);
            Clut_RFim_Property_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Clut_RFim_Property_bt.SetValue(Grid.RowProperty, 15);
            Clut_RFim_Property_bt.SetValue(Grid.RowSpanProperty, 5);
            Clut_RFim_Property_bt.Click += Clut_RFim_Property_bt_Click;

            RCS_Property_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "Adv: \r\n RCS/Doppler",
                Foreground = white_button_brush,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 10
            };
            ParentGrid.Children.Add(RCS_Property_bt);
            RCS_Property_bt.SetValue(Grid.ColumnProperty, 160);
            RCS_Property_bt.SetValue(Grid.ColumnSpanProperty, 20);
            RCS_Property_bt.SetValue(Grid.RowProperty, 20);
            RCS_Property_bt.SetValue(Grid.RowSpanProperty, 5);
            RCS_Property_bt.Click += RCS_Property_bt_Click;

            Constraint_Property_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "Adv: \r\n Constraint",
                Foreground = white_button_brush,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 10
            };
            ParentGrid.Children.Add(Constraint_Property_bt);
            Constraint_Property_bt.SetValue(Grid.ColumnProperty, 180);
            Constraint_Property_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Constraint_Property_bt.SetValue(Grid.RowProperty, 20);
            Constraint_Property_bt.SetValue(Grid.RowSpanProperty, 5);
            Constraint_Property_bt.Click += Constraint_Property_bt_Click;

            Lookup_table_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "Adv: \r\n Lookup Tables \r\n Fidelity Data",
                Foreground = white_button_brush,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 10
            };
            ParentGrid.Children.Add(Lookup_table_bt);
            Lookup_table_bt.SetValue(Grid.ColumnProperty, 160);
            Lookup_table_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Lookup_table_bt.SetValue(Grid.RowProperty, 25);
            Lookup_table_bt.SetValue(Grid.RowSpanProperty, 5);
            Lookup_table_bt.Click += Lookup_table_bt_Click;

            Add_Transmitter_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "Add Transmitter +",
                Foreground = white_button_brush,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(3, 3, 3, 3),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 14
            };
            ParentGrid.Children.Add(Add_Transmitter_bt);
            Add_Transmitter_bt.SetValue(Grid.ColumnProperty, 30);
            Add_Transmitter_bt.SetValue(Grid.ColumnSpanProperty, 17);
            Add_Transmitter_bt.SetValue(Grid.RowProperty, 5);
            Add_Transmitter_bt.SetValue(Grid.RowSpanProperty, 5);
            Add_Transmitter_bt.Click += Add_Transmitter_bt_Click;


            Add_Reflector_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "Add Reflector +",
                Foreground = white_button_brush,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(3, 3, 3, 3),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 14
            };
            ParentGrid.Children.Add(Add_Reflector_bt);
            Add_Reflector_bt.SetValue(Grid.ColumnProperty, 30);
            Add_Reflector_bt.SetValue(Grid.ColumnSpanProperty, 17);
            Add_Reflector_bt.SetValue(Grid.RowProperty, 10);
            Add_Reflector_bt.SetValue(Grid.RowSpanProperty, 5);
            Add_Reflector_bt.Click += Add_Reflector_bt_Click;

            Add_Receiver_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "Add Receiver +",
                Foreground = white_button_brush,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(3, 3, 3, 3),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 14
            };
            ParentGrid.Children.Add(Add_Receiver_bt);
            Add_Receiver_bt.SetValue(Grid.ColumnProperty, 30);
            Add_Receiver_bt.SetValue(Grid.ColumnSpanProperty, 17);
            Add_Receiver_bt.SetValue(Grid.RowProperty, 15);
            Add_Receiver_bt.SetValue(Grid.RowSpanProperty, 5);
            Add_Receiver_bt.Click += Add_Receiver_bt_Click;

            Info_bd = new Border() { 
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5),
                BorderBrush = dark_grey_brush
            };
            ParentGrid.Children.Add(Info_bd);
            Info_bd.SetValue(Grid.ColumnProperty, 160);
            Info_bd.SetValue(Grid.ColumnSpanProperty, 40);
            Info_bd.SetValue(Grid.RowProperty, 30);
            Info_bd.SetValue(Grid.RowSpanProperty, 120);
            Canvas.SetZIndex(Info_bd, -10);
            #endregion

            //Transmitter_setup();
            //Reflector_setup();
            //Receiver_setup();
            //Global_Property_setup();
            //Global_Property_hide();
            Global_Property_setup_s();
            Global_Property_hide_s();
            //Initial_Property_setup();
            //Initial_Property_hide();
            Initial_Property_setup_s();
            Initial_Property_hide_s();
            Antenna_Pol_Property_setup();
            Antenna_Pol_Property_hide();
            RCS_Property_setup();
            RCS_Property_hide();
            RF_Clut_Property_setup();
            RF_Clut_Property_hide();
            Constraint_Property_setup();
            Constraint_Property_hide();
            Lookup_table_setup();
            Lookup_table_hide();
            Property_tab_decolor();
            Initial_Property_bt.BorderBrush = green_bright_button_brush;
            Hide_all_Property();
            Initial_Property_show_s();

            Detect_object_file();
            Detect_scenario_file();

            //Add_transmitter();
            //Add_reflector();
            //Add_receiver();

            //Add_transmitter();

            //Add_receiver();
            //Add_reflector();
            //Add_transmitter();

            //Add_reflector();
            //Add_receiver();
            //Add_transmitter();

            //Add_transmitter();
            //Add_transmitter();
            //Add_receiver();

            //hide();
            //Sim_sweep dsweep = new Sim_sweep(ParentGrid);
            //dsweep.Property_setup(Sc_transmitter_list, Sc_reflector_list, Sc_receiver_list);

            //SC_Dlv.Setup(Sc_transmitter_list, Sc_receiver_list, Sc_reflector_list);
            //testchain();
            Sc_centerX_ttb.Visibility = Visibility.Visible;
        }



        private async void testchain()
        {
            Add_transmitter();
            Add_reflector();
            Add_receiver();

            Add_transmitter();

            Add_transmitter();
            Add_reflector();
            Add_receiver();

            Add_receiver();
            Add_receiver();
            if (Sc_transmitter_list.Count < 1 || Sc_reflector_list.Count < 1 || Sc_receiver_list.Count < 1)
            {
                await ShowDialog("Error", "Require at least one Transmitter, Platform, Receiver Triplet");
            }
            else
            {
                hide();
                SC_Dlv.Setup(Sc_transmitter_list, Sc_receiver_list, Sc_reflector_list);
            }
        }

        private string Generate_scenario_file()
        {
            string result = "";
            int i = 0;
            i = 0;
            while (i < Sc_transmitter_list.Count)
            {
                result += "{OT} \r\n";
                result += Sc_transmitter_list[i].Generate_file_report() + "\r\n";
                i++;
            }
            i = 0;
            while (i < Sc_reflector_list.Count)
            {
                result += "{OP} \r\n";
                result += Sc_reflector_list[i].Generate_file_report() + "\r\n";
                i++;
            }
            i = 0;
            while (i < Sc_receiver_list.Count)
            {
                result += "{OR} \r\n";
                result += Sc_receiver_list[i].Generate_file_report() + "\r\n";
                i++;
            }
            return result;
        }

        private async void Sc_save_bt_Click(object sender, RoutedEventArgs e)
        {
            await DRBE_SS.Start("Save Scenario", new List<string>() { "Simulator File", "Scenario File" }, "dsc", Generate_scenario_file());

            await Task.Delay(500);
            await Detect_scenario_file();
        }

        private async void Lv_view_bt_Click(object sender, RoutedEventArgs e)
        {
            
            if(Sc_transmitter_list.Count<1 || Sc_reflector_list.Count<1 || Sc_receiver_list.Count<1)
            {
                await ShowDialog("Error","Require at least one Transmitter, Platform, Receiver Triplet");
            }else
            {
                hide();
                SC_Dlv.Setup(Sc_transmitter_list, Sc_receiver_list, Sc_reflector_list);
            }
            
        }

        public void hide()
        {
            Hide_all_Property();
            #region generate
            Generate_obj_tb.Visibility = Visibility.Collapsed;
            Generate_obj_bt.Visibility = Visibility.Collapsed;
            #endregion

            #region Save scenario
            Lv_view_tb.Visibility = Visibility.Collapsed;
            Lv_view_bt.Visibility = Visibility.Collapsed;
            #endregion

            #region Save scenario
            Sc_save_tb.Visibility = Visibility.Collapsed;
            Sc_save_bt.Visibility = Visibility.Collapsed;
            #endregion

            #region Load scenario
            Sc_load_tb.Visibility = Visibility.Collapsed;

            Sc_load_cb.Visibility = Visibility.Collapsed;
            Sc_load_bt.Visibility = Visibility.Collapsed;
            #endregion

            #region Obj Save
            Obj_save_tb.Visibility = Visibility.Collapsed;
            Obj_save_bt.Visibility = Visibility.Collapsed;
            #endregion

            #region Object delete
            Obj_del_tb.Visibility = Visibility.Collapsed;
            Obj_del_bt.Visibility = Visibility.Collapsed;
            #endregion

            #region Load Object
            Obj_load_tb.Visibility = Visibility.Collapsed;

            Obj_load_cb.Visibility = Visibility.Collapsed;
            Obj_load_bt.Visibility = Visibility.Collapsed;
            #endregion



            Object_ID_tb.Visibility = Visibility.Collapsed;

            Number_of_receiver_ttb.Visibility = Visibility.Collapsed;

            Number_of_receiver_tb.Visibility = Visibility.Collapsed;

            Number_of_reflector_ttb.Visibility = Visibility.Collapsed;

            Number_of_reflector_tb.Visibility = Visibility.Collapsed;

            Number_of_transmitter_ttb.Visibility = Visibility.Collapsed;

            Number_of_transmitter_tb.Visibility = Visibility.Collapsed;

            Global_Property_bt.Visibility = Visibility.Collapsed;

            Initial_Property_bt.Visibility = Visibility.Collapsed;

            All_Property_bt.Visibility = Visibility.Collapsed;

            Antenna_Pol_Property_bt.Visibility = Visibility.Collapsed;

            Clut_RFim_Property_bt.Visibility = Visibility.Collapsed;    

            RCS_Property_bt.Visibility = Visibility.Collapsed;

            Constraint_Property_bt.Visibility = Visibility.Collapsed;

            Lookup_table_bt.Visibility = Visibility.Collapsed;

            Add_Transmitter_bt.Visibility = Visibility.Collapsed;   


            Add_Reflector_bt.Visibility = Visibility.Collapsed;

            Add_Receiver_bt.Visibility = Visibility.Collapsed;

            Info_bd.Visibility = Visibility.Collapsed;

            int i = 0;
            i = 0;
            while(i< Transmitter_btl.Count)
            {
                Transmitter_btl[i].Visibility = Visibility.Collapsed;
                i++;
            }

            i = 0;
            while (i < Reflector_btl.Count)
            {
                Reflector_btl[i].Visibility = Visibility.Collapsed;
                i++;
            }

            i = 0;
            while (i < Receiver_btl.Count)
            {
                Receiver_btl[i].Visibility = Visibility.Collapsed;
                i++;
            }
        }
        public void show()
        {
            #region generate
            Generate_obj_tb.Visibility = Visibility.Visible;
            Generate_obj_bt.Visibility = Visibility.Visible;
            #endregion

            #region Save scenario
            Lv_view_tb.Visibility = Visibility.Visible;
            Lv_view_bt.Visibility = Visibility.Visible;
            #endregion

            #region Save scenario
            Sc_save_tb.Visibility = Visibility.Visible;
            Sc_save_bt.Visibility = Visibility.Visible;
            #endregion

            #region Load scenario
            Sc_load_tb.Visibility = Visibility.Visible;

            Sc_load_cb.Visibility = Visibility.Visible;
            Sc_load_bt.Visibility = Visibility.Visible;
            #endregion

            #region Obj Save
            Obj_save_tb.Visibility = Visibility.Visible;
            Obj_save_bt.Visibility = Visibility.Visible;
            #endregion

            #region Object delete
            Obj_del_tb.Visibility = Visibility.Visible;
            Obj_del_bt.Visibility = Visibility.Visible;
            #endregion

            #region Load Object
            Obj_load_tb.Visibility = Visibility.Visible;

            Obj_load_cb.Visibility = Visibility.Visible;
            Obj_load_bt.Visibility = Visibility.Visible;
            #endregion



            Object_ID_tb.Visibility = Visibility.Visible;

            Number_of_receiver_ttb.Visibility = Visibility.Visible;

            Number_of_receiver_tb.Visibility = Visibility.Visible;

            Number_of_reflector_ttb.Visibility = Visibility.Visible;

            Number_of_reflector_tb.Visibility = Visibility.Visible;

            Number_of_transmitter_ttb.Visibility = Visibility.Visible;

            Number_of_transmitter_tb.Visibility = Visibility.Visible;

            Global_Property_bt.Visibility = Visibility.Visible;

            Initial_Property_bt.Visibility = Visibility.Visible;

            All_Property_bt.Visibility = Visibility.Visible;

            Antenna_Pol_Property_bt.Visibility = Visibility.Visible;

            Clut_RFim_Property_bt.Visibility = Visibility.Visible;

            RCS_Property_bt.Visibility = Visibility.Visible;

            Constraint_Property_bt.Visibility = Visibility.Visible;

            Lookup_table_bt.Visibility = Visibility.Visible;

            Add_Transmitter_bt.Visibility = Visibility.Visible;


            Add_Reflector_bt.Visibility = Visibility.Visible;

            Add_Receiver_bt.Visibility = Visibility.Visible;

            Info_bd.Visibility = Visibility.Visible;

            int i = 0;
            i = 0;
            while (i < Transmitter_btl.Count)
            {
                Transmitter_btl[i].Visibility = Visibility.Visible;
                i++;
            }

            i = 0;
            while (i < Reflector_btl.Count)
            {
                Reflector_btl[i].Visibility = Visibility.Visible;
                i++;
            }

            i = 0;
            while (i < Receiver_btl.Count)
            {
                Receiver_btl[i].Visibility = Visibility.Visible;
                i++;
            }
        }
        private async void Sc_load_bt_Click(object sender, RoutedEventArgs e)
        {
            string content = "";
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            try
            {
                content = await Read_file(storageFolder.Path.ToString() + "\\Simulator File\\Scenario File\\" + (Sc_load_cb.Items[Sc_load_cb.SelectedIndex]).ToString());
                await Read_sc_file_ui(content);
            }
            catch
            {
                await ShowDialog("Unavailable", "Unavailable Object");
            }
            
        }
        private async void Obj_load_bt_Click(object sender, RoutedEventArgs e)
        {
            string content = "";
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

            try
            {
                //await ShowDialog("here", storageFolder.Path.ToString() + "\\Simulator File\\Object File\\" + (Obj_load_cb.Items[Obj_load_cb.SelectedIndex]).ToString());
                if (Dic_bt_drbet.ContainsKey(Global_temp_bt))
                {
                    content = await Read_file(storageFolder.Path.ToString() + "\\Simulator File\\Object File\\" + (Obj_load_cb.Items[Obj_load_cb.SelectedIndex]).ToString());
                    //await ShowDialog("here", content);
                }
                else if (Dic_bt_drbef.ContainsKey(Global_temp_bt))
                {
                    content = await Read_file(storageFolder.Path.ToString() + "\\Simulator File\\Object File\\" + (Obj_load_cb.Items[Obj_load_cb.SelectedIndex]).ToString());
                }
                else if (Dic_bt_drber.ContainsKey(Global_temp_bt))
                {
                    content = await Read_file(storageFolder.Path.ToString() + "\\Simulator File\\Object File\\" + (Obj_load_cb.Items[Obj_load_cb.SelectedIndex]).ToString());
                }
                else
                {
                    return;
                }
                Read_obj_file_ui(content);
            }
            catch
            {
                await ShowDialog("Unavailable", "Unavailable Object");
            }

            
        }
        private async Task Read_sc_file_ui(string x)
        {
            List<string> Content = new List<string>();

            int i = 0;
            i = 0;
            string temp = "";
            int Casei = 0;
            while (i < x.Length)
            {
                if (x[i] == '{')
                {
                    temp = "";
                }
                else if (x[i] == '}')
                {
                    Content.Add(temp);
                    Casei++;
                }
                else
                {
                    temp = temp + x[i].ToString();
                }

                i++;
            }
            List<string> templ = new List<string>();
            i = 0;
            while(i<Content.Count)
            {
                if(Content[i] == "OT")
                {
                    Sc_add_transmitter(templ);
                    templ = new List<string>();
                }else if(Content[i] == "OP")
                {
                    Sc_add_reflector(templ);
                    templ = new List<string>();
                }
                else if (Content[i] == "OR")
                {
                    Sc_add_receiver(templ);
                    templ = new List<string>();
                }
                else
                {
                    templ.Add(Content[i]);
                }
                i++;
            }
            Transmitter_setup();
            Reflector_setup();
            Receiver_setup();
        }
        private async Task<string> Read_file(string x)
        {
            string content = "";
            int i = 0;

            StorageFile file = await StorageFile.GetFileFromPathAsync(x);
            try
            {
                content = await FileIO.ReadTextAsync(file);
            }
            catch
            {
                await ShowDialog("Error","Corrupted File");
            }
            return content;
        }
        private async void Read_obj_file_ui(string x)
        {
            List<string> Content = new List<string>();
            int i = 0;
            i = 0;
            string temp = "";
            int Casei = 0;
            while(i<x.Length)
            {
                if(x[i]=='{')
                {
                    temp = "";
                }else if(x[i]=='}')
                {
                    Content.Add(temp);
                    Casei++;
                }
                else
                {
                    temp = temp + x[i].ToString();
                }

                i++;
            }

            await ShowDialog("Loaded", "Object Loaded");

            Initial_positionX_tb.Text = Content[2];
            Initial_positionY_tb.Text = Content[3];
            Initial_positionZ_tb.Text = Content[4];

            Initial_velocityX_tb.Text = Content[11];
            Initial_velocityY_tb.Text = Content[12];
            Initial_velocityZ_tb.Text = Content[13];

            Initial_accelerationX_tb.Text = Content[14];
            Initial_accelerationY_tb.Text = Content[15];
            Initial_accelerationZ_tb.Text = Content[16];

            Initial_orientationX_tb.Text = Content[17];
            Initial_orientationY_tb.Text = Content[18];
            Initial_orientationZ_tb.Text = Content[19];

            Initial_mpointan_az_tb.Text = Content[22];
            Initial_mpointan_el_tb.Text = Content[23];

            Initial_epointan_az_tb.Text = Content[20];
            Initial_epointan_el_tb.Text = Content[21];

            Initial_epointan_az_tb.Text = Content[24];
            Initial_epointan_el_tb.Text = Content[25];

            Beamwidth_AZ_tb.Text = Content[27];
            Beamwidth_EL_tb.Text = Content[28];

            //Resolution_AZ_tb.Text = x.Resolution_AZ.ToString();

            Ele_space_tb.Text = Content[26];

            Backlobe_scaling_tb.Text = Content[29];

            //Antenna_window_tb.Text = x.Window_type;
        }
        private async Task Detect_object_file()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            IReadOnlyList<StorageFile> sfl;
            int i = 0;
            if(ParentGrid.Children.Contains(Obj_load_cb))
            {
                ParentGrid.Children.Remove(Obj_load_cb);
            }
           
            Obj_load_cb = new ComboBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                SelectedIndex = 0,
                Background = white_button_brush
            };
            ParentGrid.Children.Add(Obj_load_cb);
            Obj_load_cb.SetValue(Grid.ColumnProperty, 180);
            Obj_load_cb.SetValue(Grid.ColumnSpanProperty, 15);
            Obj_load_cb.SetValue(Grid.RowProperty, 31);
            Obj_load_cb.SetValue(Grid.RowSpanProperty, 4);
            Obj_load_cb.Items.Add("Selection");
            string subs = "";
            try 
            {
                storageFolder = await storageFolder.GetFolderAsync("Simulator File");
                storageFolder = await storageFolder.GetFolderAsync("Object File");
                if (Dic_bt_drbet.ContainsKey(Global_temp_bt))
                {
                    subs = "dtobj";
                }
                else if (Dic_bt_drbef.ContainsKey(Global_temp_bt))
                {
                    subs = "dpobj";
                }
                else if (Dic_bt_drber.ContainsKey(Global_temp_bt))
                {
                    subs = "drobj";
                }else
                {
                    return;
                }
                sfl = await storageFolder.GetFilesAsync();
                i = 0;
                while(i<sfl.Count)
                {
                    if((sfl[i].Name).Contains(subs))
                    {
                        Obj_load_cb.Items.Add(sfl[i].Name);
                    }
                    
                    i++;
                }
            }
            catch 
            {
            }

        }
        private async Task Detect_scenario_file()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            IReadOnlyList<StorageFile> sfl;
            int i = 0;
            if (ParentGrid.Children.Contains(Sc_load_cb))
            {
                ParentGrid.Children.Remove(Sc_load_cb);
            }

            Sc_load_cb = new ComboBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                SelectedIndex = 0,
                Background = white_button_brush,
                FontSize = 10
            };
            ParentGrid.Children.Add(Sc_load_cb);
            Sc_load_cb.SetValue(Grid.ColumnProperty, 180);
            Sc_load_cb.SetValue(Grid.ColumnSpanProperty, 16);
            Sc_load_cb.SetValue(Grid.RowProperty, 5);
            Sc_load_cb.SetValue(Grid.RowSpanProperty, 4);
            Sc_load_cb.Items.Add("Scenario");
            string subs = "";
            try
            {
                storageFolder = await storageFolder.GetFolderAsync("Simulator File");
                storageFolder = await storageFolder.GetFolderAsync("Scenario File");
                subs = "dsc";
                sfl = await storageFolder.GetFilesAsync();
                i = 0;
                while (i < sfl.Count)
                {
                    if ((sfl[i].Name).Contains(subs))
                    {
                        Sc_load_cb.Items.Add(sfl[i].Name);
                        
                    }

                    i++;
                }
            }
            catch
            {
            }

        }
        private async void Obj_save_bt_Click(object sender, RoutedEventArgs e)
        {
            if(Dic_bt_drbet.ContainsKey(Global_temp_bt))
            {
                Write_to_DRBEt(Dic_bt_drbet[Global_temp_bt]);
                await DRBE_SS.Start("Save Object: Transmitter", new List<string>() { "Simulator File", "Object File" }, "dtobj", Dic_bt_drbet[Global_temp_bt].Generate_file_report());
            }else if(Dic_bt_drbef.ContainsKey(Global_temp_bt))
            {
                Write_to_DRBEf(Dic_bt_drbef[Global_temp_bt]);
                await DRBE_SS.Start("Save Object: Platform", new List<string>() { "Simulator File", "Object File" }, "dpobj", Dic_bt_drbef[Global_temp_bt].Generate_file_report());
            }
            else if (Dic_bt_drber.ContainsKey(Global_temp_bt))
            {
                Write_to_DRBEr(Dic_bt_drber[Global_temp_bt]);
                await DRBE_SS.Start("Save Object: Receiver", new List<string>() { "Simulator File", "Object File" }, "drobj", Dic_bt_drber[Global_temp_bt].Generate_file_report());
            }
            await Task.Delay(500);
            await Detect_object_file();
            
        }
        private void Lookup_table_bt_Click(object sender, RoutedEventArgs e)
        {
            Property_tab_decolor();
            Lookup_table_bt.BorderBrush = green_bright_button_brush;
            Hide_all_Property();
            Lookup_table_show();
            
        }
        private void Constraint_Property_bt_Click(object sender, RoutedEventArgs e)
        {
            Property_tab_decolor();
            Constraint_Property_bt.BorderBrush = green_bright_button_brush;
            Hide_all_Property();
            Constraint_Property_show();
        }
        private void RCS_Property_bt_Click(object sender, RoutedEventArgs e)
        {
            Property_tab_decolor();
            RCS_Property_bt.BorderBrush = green_bright_button_brush;
            Hide_all_Property();
            RCS_Property_show();
        }

        private void Clut_RFim_Property_bt_Click(object sender, RoutedEventArgs e)
        {
            Property_tab_decolor();
            Clut_RFim_Property_bt.BorderBrush = green_bright_button_brush;
            Hide_all_Property();
            RF_Clut_Property_show();
        }

        private void Antenna_Pol_Property_bt_Click(object sender, RoutedEventArgs e)
        {
            Property_tab_decolor();
            Antenna_Pol_Property_bt.BorderBrush = green_bright_button_brush;
            Hide_all_Property();
            Antenna_Pol_Property_show();
        }

        private void Global_Property_bt_Click(object sender, RoutedEventArgs e)
        {
            Property_tab_decolor();
            Global_Property_bt.BorderBrush = green_bright_button_brush;
            Hide_all_Property();
            //Global_Property_show();
            Global_Property_show_s();
        }

        private void Initial_Property_bt_Click(object sender, RoutedEventArgs e)
        {
            Property_tab_decolor();
            Initial_Property_bt.BorderBrush = green_bright_button_brush;
            Hide_all_Property();
            //Initial_Property_show();
            Initial_Property_show_s();
        }
        private void All_Property_bt_Click(object sender, RoutedEventArgs e)
        {
            Property_tab_decolor();
            All_Property_bt.BorderBrush = green_bright_button_brush;
            Hide_all_Property();
            //Initial_Property_show();
            All_Property_show_s();
        }
        private void Property_tab_decolor()
        {
            Global_Property_bt.BorderBrush = white_button_brush;

            Initial_Property_bt.BorderBrush = white_button_brush;

            All_Property_bt.BorderBrush = white_button_brush;

            Antenna_Pol_Property_bt.BorderBrush = white_button_brush;

            Clut_RFim_Property_bt.BorderBrush = white_button_brush;

            RCS_Property_bt.BorderBrush = white_button_brush;

            Constraint_Property_bt.BorderBrush = white_button_brush;

            Lookup_table_bt.BorderBrush = white_button_brush;
        }

        private Button Global_temp_bt = new Button();


        private async void Add_Transmitter_bt_Click(object sender, RoutedEventArgs e)
        {
            Add_transmitter();
            await Detect_object_file();
            //SC_DRBEtl.Add();
        }

        private async void Add_Reflector_bt_Click(object sender, RoutedEventArgs e)
        {
            Add_reflector();
            await Detect_object_file();
        }

        private async void Add_Receiver_bt_Click(object sender, RoutedEventArgs e)
        {
            Add_receiver();
            await Detect_object_file();
        }

        private async void Generate_obj_bt_Click(object sender, RoutedEventArgs e)
        {
            await Generate_all_obj();
        }

        private void Hide_all_Property()
        {
            //Global_Property_hide();
            Global_Property_hide_s();
            //Initial_Property_hide();
            Initial_Property_hide_s();
            Antenna_Pol_Property_hide();
            RCS_Property_hide();
            RF_Clut_Property_hide();
            Constraint_Property_hide();
            Lookup_table_hide();
        }
        private void Show_all_Property()
        {
            Global_Property_show_s();
            Initial_Property_show_s();
        }
        private double Sc_icenter_lat = 30;
        private double Sc_icenter_lon = -30;

        private void Write_to_DRBEt(DRBE_Transmitter temp)
        {

            temp.Initial_Position_X = S_D(Initial_positionX_tb.Text);
            temp.Initial_Position_Y = S_D(Initial_positionY_tb.Text);
            temp.Initial_Position_Z = S_D(Initial_positionZ_tb.Text);

            temp.Initial_Velocity_X = S_D(Initial_velocityX_tb.Text);
            temp.Initial_Velocity_Y = S_D(Initial_velocityY_tb.Text);
            temp.Initial_Velocity_Z = S_D(Initial_velocityZ_tb.Text);

            temp.Initial_Acceleration_X = S_D(Initial_accelerationX_tb.Text);
            temp.Initial_Acceleration_Y = S_D(Initial_accelerationY_tb.Text);
            temp.Initial_Acceleration_Z = S_D(Initial_accelerationZ_tb.Text);

            temp.Initial_Orientation_X = S_D(Initial_orientationX_tb.Text);
            temp.Initial_Orientation_Y = S_D(Initial_orientationY_tb.Text);
            temp.Initial_Orientation_Z = S_D(Initial_orientationZ_tb.Text);

            temp.Initial_Mangle_AZ = S_D(Initial_mpointan_az_tb.Text);
            temp.Initial_Mangle_EL = S_D(Initial_mpointan_el_tb.Text);

            temp.Initial_Eangle_AZ = S_D(Initial_epointan_az_tb.Text);
            temp.Initial_Eangle_AZ = S_D(Initial_epointan_el_tb.Text);

            temp.Number_Antenna_AZ = S_D(Initial_epointan_az_tb.Text);
            temp.Number_Antenna_EL = S_D(Initial_epointan_el_tb.Text);

            temp.Beamwidth_AZ = S_D(Beamwidth_AZ_tb.Text);
            temp.Beamwidth_EL = S_D(Beamwidth_EL_tb.Text);

            temp.Resolution_AZ = S_D(Resolution_AZ_tb.Text);
            temp.Resolution_EL = S_D(Resolution_EL_tb.Text);

            temp.Element_Spacing = S_D(Ele_space_tb.Text);

            temp.Backlobe_Scaling = S_D(Backlobe_scaling_tb.Text);

            temp.Window_type = Antenna_window_tb.Text;
        }

        private void Read_from_DRBEt(DRBE_Transmitter x)
        {
            Initial_positionX_tb.Text = x.Initial_Position_X.ToString();
            Initial_positionY_tb.Text = x.Initial_Position_Y.ToString();
            Initial_positionZ_tb.Text = x.Initial_Position_Z.ToString();

            Initial_velocityX_tb.Text = x.Initial_Velocity_X.ToString();
            Initial_velocityY_tb.Text = x.Initial_Velocity_Y.ToString();
            Initial_velocityZ_tb.Text = x.Initial_Velocity_Z.ToString();

            Initial_accelerationX_tb.Text = x.Initial_Acceleration_X.ToString();
            Initial_accelerationY_tb.Text = x.Initial_Acceleration_Y.ToString();
            Initial_accelerationZ_tb.Text = x.Initial_Acceleration_Z.ToString();

            Initial_orientationX_tb.Text = x.Initial_Orientation_X.ToString();
            Initial_orientationY_tb.Text = x.Initial_Orientation_Y.ToString();
            Initial_orientationZ_tb.Text = x.Initial_Orientation_Z.ToString();

            Initial_mpointan_az_tb.Text = x.Initial_Mangle_AZ.ToString();
            Initial_mpointan_el_tb.Text = x.Initial_Mangle_EL.ToString();

            Initial_epointan_az_tb.Text = x.Initial_Eangle_AZ.ToString();
            Initial_epointan_el_tb.Text = x.Initial_Eangle_EL.ToString();

            Initial_epointan_az_tb.Text = x.Number_Antenna_AZ.ToString();
            Initial_epointan_el_tb.Text = x.Number_Antenna_EL.ToString();

            Beamwidth_AZ_tb.Text = x.Beamwidth_AZ.ToString();
            Beamwidth_EL_tb.Text = x.Beamwidth_EL.ToString();

            Resolution_AZ_tb.Text = x.Resolution_AZ.ToString();

            Ele_space_tb.Text = x.Element_Spacing.ToString();

            Backlobe_scaling_tb.Text = x.Backlobe_Scaling.ToString();

            Antenna_window_tb.Text = x.Window_type;

        }

        private void Write_to_DRBEf(DRBE_Reflector temp)
        {

            temp.Initial_Position_X = S_D(Initial_positionX_tb.Text);
            temp.Initial_Position_Y = S_D(Initial_positionY_tb.Text);
            temp.Initial_Position_Z = S_D(Initial_positionZ_tb.Text);

            temp.Initial_Velocity_X = S_D(Initial_velocityX_tb.Text);
            temp.Initial_Velocity_Y = S_D(Initial_velocityY_tb.Text);
            temp.Initial_Velocity_Z = S_D(Initial_velocityZ_tb.Text);

            temp.Initial_Acceleration_X = S_D(Initial_accelerationX_tb.Text);
            temp.Initial_Acceleration_Y = S_D(Initial_accelerationY_tb.Text);
            temp.Initial_Acceleration_Z = S_D(Initial_accelerationZ_tb.Text);

            temp.Initial_Orientation_X = S_D(Initial_orientationX_tb.Text);
            temp.Initial_Orientation_Y = S_D(Initial_orientationY_tb.Text);
            temp.Initial_Orientation_Z = S_D(Initial_orientationZ_tb.Text);

            temp.Initial_Mangle_AZ = S_D(Initial_mpointan_az_tb.Text);
            temp.Initial_Mangle_EL = S_D(Initial_mpointan_el_tb.Text);

            temp.Initial_Eangle_AZ = S_D(Initial_epointan_az_tb.Text);
            temp.Initial_Eangle_AZ = S_D(Initial_epointan_el_tb.Text);

            temp.Number_Antenna_AZ = S_D(Initial_epointan_az_tb.Text);
            temp.Number_Antenna_EL = S_D(Initial_epointan_el_tb.Text);

            temp.Beamwidth_AZ = S_D(Beamwidth_AZ_tb.Text);
            temp.Beamwidth_EL = S_D(Beamwidth_EL_tb.Text);

            temp.Resolution_AZ = S_D(Resolution_AZ_tb.Text);
            temp.Resolution_EL = S_D(Resolution_EL_tb.Text);

            temp.Element_Spacing = S_D(Ele_space_tb.Text);

            temp.Backlobe_Scaling = S_D(Backlobe_scaling_tb.Text);

            temp.Window_type = Antenna_window_tb.Text;
        }

        private void Read_from_DRBEf(DRBE_Reflector x)
        {
            Initial_positionX_tb.Text = x.Initial_Position_X.ToString();
            Initial_positionY_tb.Text = x.Initial_Position_Y.ToString();
            Initial_positionZ_tb.Text = x.Initial_Position_Z.ToString();

            Initial_velocityX_tb.Text = x.Initial_Velocity_X.ToString();
            Initial_velocityY_tb.Text = x.Initial_Velocity_Y.ToString();
            Initial_velocityZ_tb.Text = x.Initial_Velocity_Z.ToString();

            Initial_accelerationX_tb.Text = x.Initial_Acceleration_X.ToString();
            Initial_accelerationY_tb.Text = x.Initial_Acceleration_Y.ToString();
            Initial_accelerationZ_tb.Text = x.Initial_Acceleration_Z.ToString();

            Initial_orientationX_tb.Text = x.Initial_Orientation_X.ToString();
            Initial_orientationY_tb.Text = x.Initial_Orientation_Y.ToString();
            Initial_orientationZ_tb.Text = x.Initial_Orientation_Z.ToString();

            Initial_mpointan_az_tb.Text = x.Initial_Mangle_AZ.ToString();
            Initial_mpointan_el_tb.Text = x.Initial_Mangle_EL.ToString();

            Initial_epointan_az_tb.Text = x.Initial_Eangle_AZ.ToString();
            Initial_epointan_el_tb.Text = x.Initial_Eangle_EL.ToString();

            Initial_epointan_az_tb.Text = x.Number_Antenna_AZ.ToString();
            Initial_epointan_el_tb.Text = x.Number_Antenna_EL.ToString();

            Beamwidth_AZ_tb.Text = x.Beamwidth_AZ.ToString();
            Beamwidth_EL_tb.Text = x.Beamwidth_EL.ToString();

            Resolution_AZ_tb.Text = x.Resolution_AZ.ToString();

            Ele_space_tb.Text = x.Element_Spacing.ToString();

            Backlobe_scaling_tb.Text = x.Backlobe_Scaling.ToString();

            Antenna_window_tb.Text = x.Window_type;

        }

        private void Write_to_DRBEr(DRBE_Receiver temp)
        {

            temp.Initial_Position_X = S_D(Initial_positionX_tb.Text);
            temp.Initial_Position_Y = S_D(Initial_positionY_tb.Text);
            temp.Initial_Position_Z = S_D(Initial_positionZ_tb.Text);

            temp.Initial_Velocity_X = S_D(Initial_velocityX_tb.Text);
            temp.Initial_Velocity_Y = S_D(Initial_velocityY_tb.Text);
            temp.Initial_Velocity_Z = S_D(Initial_velocityZ_tb.Text);

            temp.Initial_Acceleration_X = S_D(Initial_accelerationX_tb.Text);
            temp.Initial_Acceleration_Y = S_D(Initial_accelerationY_tb.Text);
            temp.Initial_Acceleration_Z = S_D(Initial_accelerationZ_tb.Text);

            temp.Initial_Orientation_X = S_D(Initial_orientationX_tb.Text);
            temp.Initial_Orientation_Y = S_D(Initial_orientationY_tb.Text);
            temp.Initial_Orientation_Z = S_D(Initial_orientationZ_tb.Text);

            temp.Initial_Mangle_AZ = S_D(Initial_mpointan_az_tb.Text);
            temp.Initial_Mangle_EL = S_D(Initial_mpointan_el_tb.Text);

            temp.Initial_Eangle_AZ = S_D(Initial_epointan_az_tb.Text);
            temp.Initial_Eangle_AZ = S_D(Initial_epointan_el_tb.Text);

            temp.Number_Antenna_AZ = S_D(Initial_epointan_az_tb.Text);
            temp.Number_Antenna_EL = S_D(Initial_epointan_el_tb.Text);

            temp.Beamwidth_AZ = S_D(Beamwidth_AZ_tb.Text);
            temp.Beamwidth_EL = S_D(Beamwidth_EL_tb.Text);

            temp.Resolution_AZ = S_D(Resolution_AZ_tb.Text);
            temp.Resolution_EL = S_D(Resolution_EL_tb.Text);

            temp.Element_Spacing = S_D(Ele_space_tb.Text);

            temp.Backlobe_Scaling = S_D(Backlobe_scaling_tb.Text);

            temp.Window_type = Antenna_window_tb.Text;
        }

        private void Read_from_DRBEr(DRBE_Receiver x)
        {
            Initial_positionX_tb.Text = x.Initial_Position_X.ToString();
            Initial_positionY_tb.Text = x.Initial_Position_Y.ToString();
            Initial_positionZ_tb.Text = x.Initial_Position_Z.ToString();

            Initial_velocityX_tb.Text = x.Initial_Velocity_X.ToString();
            Initial_velocityY_tb.Text = x.Initial_Velocity_Y.ToString();
            Initial_velocityZ_tb.Text = x.Initial_Velocity_Z.ToString();

            Initial_accelerationX_tb.Text = x.Initial_Acceleration_X.ToString();
            Initial_accelerationY_tb.Text = x.Initial_Acceleration_Y.ToString();
            Initial_accelerationZ_tb.Text = x.Initial_Acceleration_Z.ToString();

            Initial_orientationX_tb.Text = x.Initial_Orientation_X.ToString();
            Initial_orientationY_tb.Text = x.Initial_Orientation_Y.ToString();
            Initial_orientationZ_tb.Text = x.Initial_Orientation_Z.ToString();

            Initial_mpointan_az_tb.Text = x.Initial_Mangle_AZ.ToString();
            Initial_mpointan_el_tb.Text = x.Initial_Mangle_EL.ToString();

            Initial_epointan_az_tb.Text = x.Initial_Eangle_AZ.ToString();
            Initial_epointan_el_tb.Text = x.Initial_Eangle_EL.ToString();

            Initial_epointan_az_tb.Text = x.Number_Antenna_AZ.ToString();
            Initial_epointan_el_tb.Text = x.Number_Antenna_EL.ToString();

            Beamwidth_AZ_tb.Text = x.Beamwidth_AZ.ToString();
            Beamwidth_EL_tb.Text = x.Beamwidth_EL.ToString();

            Resolution_AZ_tb.Text = x.Resolution_AZ.ToString();

            Ele_space_tb.Text = x.Element_Spacing.ToString();

            Backlobe_scaling_tb.Text = x.Backlobe_Scaling.ToString();

            Antenna_window_tb.Text = x.Window_type;

        }


        #region Initial Property declare
        private ToolTip Initial_position_tp = new ToolTip();

        private TextBlock Initial_positionX_ttb = new TextBlock();
        private TextBox Initial_positionX_tb = new TextBox();
        private Border Initial_positionX_bd = new Border();
        private Border Initial_positionX_bd1 = new Border();

        private TextBlock Initial_positionY_ttb = new TextBlock();
        private TextBox Initial_positionY_tb = new TextBox();
        private Border Initial_positionY_bd = new Border();
        private Border Initial_positionY_bd1 = new Border();

        private TextBlock Initial_positionZ_ttb = new TextBlock();
        private TextBox Initial_positionZ_tb = new TextBox();
        private Border Initial_positionZ_bd = new Border();
        private Border Initial_positionZ_bd1 = new Border();

        private TextBlock Initial_velocityX_ttb = new TextBlock();
        private TextBox Initial_velocityX_tb = new TextBox();
        private Border Initial_velocityX_bd = new Border();
        private Border Initial_velocityX_bd1 = new Border();

        private TextBlock Initial_velocityY_ttb = new TextBlock();
        private TextBox Initial_velocityY_tb = new TextBox();
        private Border Initial_velocityY_bd = new Border();
        private Border Initial_velocityY_bd1 = new Border();

        private TextBlock Initial_velocityZ_ttb = new TextBlock();
        private TextBox Initial_velocityZ_tb = new TextBox();
        private Border Initial_velocityZ_bd = new Border();
        private Border Initial_velocityZ_bd1 = new Border();

        private TextBlock Initial_accelerationX_ttb = new TextBlock();
        private TextBox Initial_accelerationX_tb = new TextBox();
        private Border Initial_accelerationX_bd = new Border();
        private Border Initial_accelerationX_bd1 = new Border();

        private TextBlock Initial_accelerationY_ttb = new TextBlock();
        private TextBox Initial_accelerationY_tb = new TextBox();
        private Border Initial_accelerationY_bd = new Border();
        private Border Initial_accelerationY_bd1 = new Border();

        private TextBlock Initial_accelerationZ_ttb = new TextBlock();
        private TextBox Initial_accelerationZ_tb = new TextBox();
        private Border Initial_accelerationZ_bd = new Border();
        private Border Initial_accelerationZ_bd1 = new Border();

        private TextBlock Initial_orientationX_ttb = new TextBlock();
        private TextBox Initial_orientationX_tb = new TextBox();
        private Border Initial_orientationX_bd = new Border();
        private Border Initial_orientationX_bd1 = new Border();

        private TextBlock Initial_orientationY_ttb = new TextBlock();
        private TextBox Initial_orientationY_tb = new TextBox();
        private Border Initial_orientationY_bd = new Border();
        private Border Initial_orientationY_bd1 = new Border();

        private TextBlock Initial_orientationZ_ttb = new TextBlock();
        private TextBox Initial_orientationZ_tb = new TextBox();
        private Border Initial_orientationZ_bd = new Border();
        private Border Initial_orientationZ_bd1 = new Border();

        private TextBlock Initial_mpointan_az_ttb = new TextBlock();
        private TextBox Initial_mpointan_az_tb = new TextBox();
        private Border Initial_mpointan_az_bd = new Border();
        private Border Initial_mpointan_az_bd1 = new Border();

        private TextBlock Initial_mpointan_el_ttb = new TextBlock();
        private TextBox Initial_mpointan_el_tb = new TextBox();
        private Border Initial_mpointan_el_bd = new Border();
        private Border Initial_mpointan_el_bd1 = new Border();

        private TextBlock Initial_epointan_az_ttb = new TextBlock();
        private TextBox Initial_epointan_az_tb = new TextBox();
        private Border Initial_epointan_az_bd = new Border();
        private Border Initial_epointan_az_bd1 = new Border();

        private TextBlock Initial_epointan_el_ttb = new TextBlock();
        private TextBox Initial_epointan_el_tb = new TextBox();
        private Border Initial_epointan_el_bd = new Border();
        private Border Initial_epointan_el_bd1 = new Border();

        private ToolTip Sc_center_tp = new ToolTip();

        private TextBlock Sc_centerX_ttb = new TextBlock();
        private TextBox Sc_centerX_tb = new TextBox();
        private Border Sc_centerX_bd = new Border();
        private Border Sc_centerX_bd1 = new Border();

        private TextBlock Sc_centerY_ttb = new TextBlock();
        private TextBox Sc_centerY_tb = new TextBox();
        private Border Sc_centerY_bd = new Border();
        private Border Sc_centerY_bd1 = new Border();

        private TextBlock Sc_centerZ_ttb = new TextBlock();
        private TextBox Sc_centerZ_tb = new TextBox();
        private Border Sc_centerZ_bd = new Border();
        private Border Sc_centerZ_bd1 = new Border();

        private TextBlock Reference_frame_id_ttb = new TextBlock();
        private ComboBox Reference_frame_id_cb = new ComboBox();
        private Border Reference_frame_id_bd = new Border();
        private Border Reference_frame_id_bd1 = new Border();

        private Button Sc_rnd_gen_bt = new Button();

        #endregion
        public async void Global_Property_setup_s()
        {
            #region initial lat

            Sc_centerX_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Sc_icenter_lat.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Sc_centerX_tb);

            Sc_centerX_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Scenario Center: \r\n Latitude (degree)",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Sc_centerX_ttb);

            Sc_centerX_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Sc_centerX_bd);
            Sc_centerX_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Sc_centerX_bd1);
            #endregion

            #region initial lon
            Sc_centerY_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Sc_icenter_lon.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Sc_centerY_tb);

            Sc_centerY_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Scenario Center: \r\n Longitude (degree)",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Sc_centerY_ttb);

            Sc_centerY_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Sc_centerY_bd);
            Sc_centerY_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Sc_centerY_bd1);
            #endregion

            #region Height
            Sc_centerZ_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Sc_icenter_lon.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Sc_centerZ_tb);

            Sc_centerZ_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Scenario Center: \r\n Height",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Sc_centerZ_ttb);

            Sc_centerZ_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Sc_centerZ_bd);
            Sc_centerZ_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Sc_centerZ_bd1);
            #endregion

            Sc_center_tp.Content = "Center of Scenario in Longitutde , Latitude and Height to earth surface";
            ToolTipService.SetToolTip(Sc_centerX_ttb, Sc_center_tp);
            ToolTipService.SetToolTip(Sc_centerY_ttb, Sc_center_tp);
            ToolTipService.SetToolTip(Sc_centerZ_ttb, Sc_center_tp);
        }
        public async void Global_Property_setup()
        {

            #region initial lat

            Sc_centerX_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Sc_icenter_lat.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 40
            };
            //ParentGrid.Children.Add(Sc_centerX_tb);
            //Sc_centerX_tb.SetValue(Grid.ColumnProperty, 0);
            //Sc_centerX_tb.SetValue(Grid.ColumnSpanProperty, 1);
            //Sc_centerX_tb.SetValue(Grid.RowProperty, 41);
            //Sc_centerX_tb.SetValue(Grid.RowSpanProperty, 4);
            DRBE_SPR.Children.Add(Sc_centerX_tb);

            Sc_centerX_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Scenario Center: \r\n Latitude (degree)",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            //ParentGrid.Children.Add(Sc_centerX_ttb);
            //Sc_centerX_ttb.SetValue(Grid.ColumnProperty, 1);
            //Sc_centerX_ttb.SetValue(Grid.ColumnSpanProperty, 1);
            //Sc_centerX_ttb.SetValue(Grid.RowProperty, 41);
            //Sc_centerX_ttb.SetValue(Grid.RowSpanProperty, 4);
            DRBE_SPL.Children.Add(Sc_centerX_ttb);

            Border bdt = new Border() { 
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0,0.5,0,0)
            };
            DRBE_SPL.Children.Add(bdt);
            #endregion

            #region initial lon
            Sc_centerY_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Sc_icenter_lon.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            //ParentGrid.Children.Add(Sc_centerY_tb);
            //Sc_centerY_tb.SetValue(Grid.ColumnProperty, 73);
            //Sc_centerY_tb.SetValue(Grid.ColumnSpanProperty, 20);
            //Sc_centerY_tb.SetValue(Grid.RowProperty, 45);
            //Sc_centerY_tb.SetValue(Grid.RowSpanProperty, 5);
            DRBE_SPR.Children.Add(Sc_centerY_tb);

            Sc_centerY_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Scenario Center: \r\n Longitude (degree)",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            //ParentGrid.Children.Add(Sc_centerY_ttb);
            //Sc_centerY_ttb.SetValue(Grid.ColumnProperty, 73);
            //Sc_centerY_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            //Sc_centerY_ttb.SetValue(Grid.RowProperty, 37);
            //Sc_centerY_ttb.SetValue(Grid.RowSpanProperty, 8);
            DRBE_SPL.Children.Add(Sc_centerY_ttb);

            Border bdt1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(bdt1);
            #endregion

            #region Height
            Sc_centerZ_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Sc_icenter_lon.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            //ParentGrid.Children.Add(Sc_centerZ_tb);
            //Sc_centerZ_tb.SetValue(Grid.ColumnProperty, 93);
            //Sc_centerZ_tb.SetValue(Grid.ColumnSpanProperty, 20);
            //Sc_centerZ_tb.SetValue(Grid.RowProperty, 45);
            //Sc_centerZ_tb.SetValue(Grid.RowSpanProperty, 5);
            DRBE_SPR.Children.Add(Sc_centerZ_tb);

            Sc_centerZ_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Scenario Center: \r\n Height",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            //ParentGrid.Children.Add(Sc_centerZ_ttb);
            //Sc_centerZ_ttb.SetValue(Grid.ColumnProperty, 93);
            //Sc_centerZ_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            //Sc_centerZ_ttb.SetValue(Grid.RowProperty, 37);
            //Sc_centerZ_ttb.SetValue(Grid.RowSpanProperty, 8);
            DRBE_SPL.Children.Add(Sc_centerZ_ttb);

            Border bdt2 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(bdt2);
            #endregion

            Sc_center_tp.Content = "Center of Scenario in Longitutde , Latitude and Height to earth surface";
            ToolTipService.SetToolTip(Sc_centerX_ttb, Sc_center_tp);
            ToolTipService.SetToolTip(Sc_centerY_ttb, Sc_center_tp);
            ToolTipService.SetToolTip(Sc_centerZ_ttb, Sc_center_tp);
        }
        public async void Initial_Property_setup_s()
        {



            #region coordination
            Coordinate_system_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Coordinate System",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Coordinate_system_tb);

            Coordinate_system_cb = new ComboBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Text = "Global Frame",
                SelectedIndex = 0,
                Background = white_button_brush,
                Height = 35
            };
            DRBE_SPR.Children.Add(Coordinate_system_cb);
            Coordinate_system_cb.Items.Add("Global Frame \\ WGS84");
            Coordinate_system_cb.Items.Add("Cartesian \\ Earth Cent");
            Coordinate_system_cb.Items.Add("Scenario Center Frame");
            Coordinate_system_cb.Items.Add("Object Reference Frame");

            Coordinate_system_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Coordinate_system_bd);
            Coordinate_system_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Coordinate_system_bd1);

            #endregion

            #region object reference
            Reference_frame_id_cb = new ComboBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Text = "Global Frame",
                SelectedIndex = 0,
                Background = white_button_brush,
                Height = 35
            };
            DRBE_SPR.Children.Add(Reference_frame_id_cb);

            Reference_frame_id_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Object Frame Selection: \r\n Reference Obj ID",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Reference_frame_id_ttb);

            Reference_frame_id_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Reference_frame_id_bd);
            Reference_frame_id_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Reference_frame_id_bd1);
            #endregion



            #region Position X
            Initial_positionX_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Initial_positionX_tb);

            Initial_positionX_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Position X",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Initial_positionX_ttb);

            Initial_positionX_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Initial_positionX_bd);
            Initial_positionX_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Initial_positionX_bd1);
            #endregion
            #region Position Y
            Initial_positionY_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Initial_positionY_tb);

            Initial_positionY_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Position Y",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Initial_positionY_ttb);

            Initial_positionY_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Initial_positionY_bd);
            Initial_positionY_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Initial_positionY_bd1);
            #endregion
            #region Position Z
            Initial_positionZ_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Initial_positionZ_tb);

            Initial_positionZ_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Position Z",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Initial_positionZ_ttb);

            Initial_positionZ_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0),
            };
            DRBE_SPL.Children.Add(Initial_positionZ_bd);

            Initial_positionZ_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Initial_positionZ_bd1);
            #endregion

            #region Velocity X
            Initial_velocityX_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Initial_velocityX_tb);

            Initial_velocityX_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Velocity X",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Initial_velocityX_ttb);

            Initial_velocityX_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0),
            };
            DRBE_SPL.Children.Add(Initial_velocityX_bd);

            Initial_velocityX_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Initial_velocityX_bd1);
            #endregion
            #region Velocity Y
            Initial_velocityY_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Initial_velocityY_tb);

            Initial_velocityY_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Velocity Y",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Initial_velocityY_ttb);

            Initial_velocityY_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0),
            };
            DRBE_SPL.Children.Add(Initial_velocityY_bd);

            Initial_velocityY_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Initial_velocityY_bd1);
            #endregion
            #region Velocity Z
            Initial_velocityZ_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Initial_velocityZ_tb);

            Initial_velocityZ_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Velocity Z",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Initial_velocityZ_ttb);

            Initial_velocityZ_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0),
            };
            DRBE_SPL.Children.Add(Initial_velocityZ_bd);

            Initial_velocityZ_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Initial_velocityZ_bd1);
            #endregion

            #region Acceleration X
            Initial_accelerationX_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Initial_accelerationX_tb);

            Initial_accelerationX_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Acceleration X",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Initial_accelerationX_ttb);

            Initial_accelerationX_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0),
            };
            DRBE_SPL.Children.Add(Initial_accelerationX_bd);

            Initial_accelerationX_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Initial_accelerationX_bd1);
            #endregion
            #region Acceleration Y
            Initial_accelerationY_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Initial_accelerationY_tb);

            Initial_accelerationY_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Acceleration Y",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Initial_accelerationY_ttb);

            Initial_accelerationY_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0),
            };
            DRBE_SPL.Children.Add(Initial_accelerationY_bd);

            Initial_accelerationY_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Initial_accelerationY_bd1);
            #endregion
            #region Acceleration Z
            Initial_accelerationZ_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Initial_accelerationZ_tb);

            Initial_accelerationZ_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Acceleration Z",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Initial_accelerationZ_ttb);

            Initial_accelerationZ_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0),
            };
            DRBE_SPL.Children.Add(Initial_accelerationZ_bd);

            Initial_accelerationZ_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Initial_accelerationZ_bd1);
            #endregion

            #region Orientation X

            Initial_orientationX_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Initial_orientationX_tb);

            Initial_orientationX_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Pitch",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Initial_orientationX_ttb);

            Initial_orientationX_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0),
            };
            DRBE_SPL.Children.Add(Initial_orientationX_bd);

            Initial_orientationX_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Initial_orientationX_bd1);
            #endregion
            #region Orientation Y
            Initial_orientationY_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Initial_orientationY_tb);

            Initial_orientationY_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Roll",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Initial_orientationY_ttb);

            Initial_orientationY_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0),
            };
            DRBE_SPL.Children.Add(Initial_orientationY_bd);

            Initial_orientationY_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Initial_orientationY_bd1);
            #endregion
            #region Orientation Z
            Initial_orientationZ_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Initial_orientationZ_tb);

            Initial_orientationZ_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Yaw",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Initial_orientationZ_ttb);

            Initial_orientationZ_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0),
            };
            DRBE_SPL.Children.Add(Initial_orientationZ_bd);

            Initial_orientationZ_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Initial_orientationZ_bd1);
            #endregion



            #region M Pointing Angle Az
            Initial_mpointan_az_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Initial_mpointan_az_tb);

            Initial_mpointan_az_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Mech Ster Ang AZ",
                Foreground = Sea_Green,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Initial_mpointan_az_ttb);

            Initial_mpointan_az_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0),
            };
            DRBE_SPL.Children.Add(Initial_mpointan_az_bd);

            Initial_mpointan_az_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Initial_mpointan_az_bd1);
            #endregion

            #region M Pointing Angle EL
            Initial_mpointan_el_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Initial_mpointan_el_tb);

            Initial_mpointan_el_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Mech Ster Ang EL",
                Foreground = Sea_Green,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Initial_mpointan_el_ttb);

            Initial_mpointan_el_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0),
            };
            DRBE_SPL.Children.Add(Initial_mpointan_el_bd);

            Initial_mpointan_el_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Initial_mpointan_el_bd1);
            #endregion

            #region M Pointing Angle Az
            Initial_epointan_az_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Initial_epointan_az_tb);

            Initial_epointan_az_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Elec Ster Ang AZ",
                Foreground = Sea_Green,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Initial_epointan_az_ttb);

            Initial_epointan_az_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0),
            };
            DRBE_SPL.Children.Add(Initial_epointan_az_bd);

            Initial_epointan_az_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Initial_epointan_az_bd1);
            #endregion

            #region E Pointing Angle EL
            Initial_epointan_el_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Initial_epointan_el_tb);

            Initial_epointan_el_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Elec Ster Ang EL",
                Foreground = Sea_Green,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Initial_epointan_el_ttb);

            Initial_epointan_el_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0),
            };
            DRBE_SPL.Children.Add(Initial_epointan_el_bd);

            Initial_epointan_el_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Initial_epointan_el_bd1);
            #endregion

        }
        public async void Initial_Property_setup()
        {



            #region coordination
            Coordinate_system_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Coordinate System",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Coordinate_system_tb);
            Coordinate_system_tb.SetValue(Grid.ColumnProperty, 53);
            Coordinate_system_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Coordinate_system_tb.SetValue(Grid.RowProperty, 57);
            Coordinate_system_tb.SetValue(Grid.RowSpanProperty, 5);

            Coordinate_system_cb = new ComboBox() { 
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Text = "Global Frame",
                SelectedIndex = 0,
                Background = white_button_brush
            };
            ParentGrid.Children.Add(Coordinate_system_cb);
            Coordinate_system_cb.SetValue(Grid.ColumnProperty, 53);
            Coordinate_system_cb.SetValue(Grid.ColumnSpanProperty, 20);
            Coordinate_system_cb.SetValue(Grid.RowProperty, 62);
            Coordinate_system_cb.SetValue(Grid.RowSpanProperty, 5);
            Coordinate_system_cb.Items.Add("Global Frame \\ WGS84");
            Coordinate_system_cb.Items.Add("Cartesian \\ Earth Cent");
            Coordinate_system_cb.Items.Add("Scenario Center Frame");
            Coordinate_system_cb.Items.Add("Object Reference Frame");


            #endregion

            #region object reference
            Reference_frame_id_cb = new ComboBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Text = "Global Frame",
                SelectedIndex = 0,
                Background = white_button_brush
            };
            ParentGrid.Children.Add(Reference_frame_id_cb);
            Reference_frame_id_cb.SetValue(Grid.ColumnProperty, 75);
            Reference_frame_id_cb.SetValue(Grid.ColumnSpanProperty, 20);
            Reference_frame_id_cb.SetValue(Grid.RowProperty, 62);
            Reference_frame_id_cb.SetValue(Grid.RowSpanProperty, 5);

            Reference_frame_id_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Object Frame Selection: \r\n Reference Obj ID",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Reference_frame_id_ttb);
            Reference_frame_id_ttb.SetValue(Grid.ColumnProperty, 75);
            Reference_frame_id_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            Reference_frame_id_ttb.SetValue(Grid.RowProperty, 55);
            Reference_frame_id_ttb.SetValue(Grid.RowSpanProperty, 7);
            #endregion



            #region Position X
            Initial_positionX_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_positionX_tb);
            Initial_positionX_tb.SetValue(Grid.ColumnProperty, 53);
            Initial_positionX_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_positionX_tb.SetValue(Grid.RowProperty, 75);
            Initial_positionX_tb.SetValue(Grid.RowSpanProperty, 5);

            Initial_positionX_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Position X",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_positionX_ttb);
            Initial_positionX_ttb.SetValue(Grid.ColumnProperty, 53);
            Initial_positionX_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_positionX_ttb.SetValue(Grid.RowProperty, 70);
            Initial_positionX_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion
            #region Position Y
            Initial_positionY_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_positionY_tb);
            Initial_positionY_tb.SetValue(Grid.ColumnProperty, 68);
            Initial_positionY_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_positionY_tb.SetValue(Grid.RowProperty, 75);
            Initial_positionY_tb.SetValue(Grid.RowSpanProperty, 5);

            Initial_positionY_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Position Y",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_positionY_ttb);
            Initial_positionY_ttb.SetValue(Grid.ColumnProperty, 68);
            Initial_positionY_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_positionY_ttb.SetValue(Grid.RowProperty, 70);
            Initial_positionY_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion
            #region Position Z
            Initial_positionZ_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_positionZ_tb);
            Initial_positionZ_tb.SetValue(Grid.ColumnProperty, 83);
            Initial_positionZ_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_positionZ_tb.SetValue(Grid.RowProperty, 75);
            Initial_positionZ_tb.SetValue(Grid.RowSpanProperty, 5);

            Initial_positionZ_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Position Z",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_positionZ_ttb);
            Initial_positionZ_ttb.SetValue(Grid.ColumnProperty, 83);
            Initial_positionZ_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_positionZ_ttb.SetValue(Grid.RowProperty, 70);
            Initial_positionZ_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

            #region Velocity X
            Initial_velocityX_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_velocityX_tb);
            Initial_velocityX_tb.SetValue(Grid.ColumnProperty, 103);
            Initial_velocityX_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_velocityX_tb.SetValue(Grid.RowProperty, 75);
            Initial_velocityX_tb.SetValue(Grid.RowSpanProperty, 5);

            Initial_velocityX_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Velocity X",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_velocityX_ttb);
            Initial_velocityX_ttb.SetValue(Grid.ColumnProperty, 103);
            Initial_velocityX_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_velocityX_ttb.SetValue(Grid.RowProperty, 70);
            Initial_velocityX_ttb.SetValue(Grid.RowSpanProperty, 5);

            
            #endregion
            #region Velocity Y
            Initial_velocityY_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_velocityY_tb);
            Initial_velocityY_tb.SetValue(Grid.ColumnProperty, 118);
            Initial_velocityY_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_velocityY_tb.SetValue(Grid.RowProperty, 75);
            Initial_velocityY_tb.SetValue(Grid.RowSpanProperty, 5);

            Initial_velocityY_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Velocity Y",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_velocityY_ttb);
            Initial_velocityY_ttb.SetValue(Grid.ColumnProperty, 118);
            Initial_velocityY_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_velocityY_ttb.SetValue(Grid.RowProperty, 70);
            Initial_velocityY_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion
            #region Velocity Z
            Initial_velocityZ_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_velocityZ_tb);
            Initial_velocityZ_tb.SetValue(Grid.ColumnProperty, 133);
            Initial_velocityZ_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_velocityZ_tb.SetValue(Grid.RowProperty, 75);
            Initial_velocityZ_tb.SetValue(Grid.RowSpanProperty, 5);

            Initial_velocityZ_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Velocity Z",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_velocityZ_ttb);
            Initial_velocityZ_ttb.SetValue(Grid.ColumnProperty, 133);
            Initial_velocityZ_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_velocityZ_ttb.SetValue(Grid.RowProperty, 70);
            Initial_velocityZ_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

            #region Acceleration X
            Initial_accelerationX_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_accelerationX_tb);
            Initial_accelerationX_tb.SetValue(Grid.ColumnProperty, 153);
            Initial_accelerationX_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_accelerationX_tb.SetValue(Grid.RowProperty, 75);
            Initial_accelerationX_tb.SetValue(Grid.RowSpanProperty, 5);

            Initial_accelerationX_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Acceleration X",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_accelerationX_ttb);
            Initial_accelerationX_ttb.SetValue(Grid.ColumnProperty, 153);
            Initial_accelerationX_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_accelerationX_ttb.SetValue(Grid.RowProperty, 70);
            Initial_accelerationX_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion
            #region Acceleration Y
            Initial_accelerationY_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_accelerationY_tb);
            Initial_accelerationY_tb.SetValue(Grid.ColumnProperty, 168);
            Initial_accelerationY_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_accelerationY_tb.SetValue(Grid.RowProperty, 75);
            Initial_accelerationY_tb.SetValue(Grid.RowSpanProperty, 5);

            Initial_accelerationY_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Acceleration Y",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_accelerationY_ttb);
            Initial_accelerationY_ttb.SetValue(Grid.ColumnProperty, 168);
            Initial_accelerationY_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_accelerationY_ttb.SetValue(Grid.RowProperty, 70);
            Initial_accelerationY_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion
            #region Acceleration Z
            Initial_accelerationZ_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_accelerationZ_tb);
            Initial_accelerationZ_tb.SetValue(Grid.ColumnProperty, 183);
            Initial_accelerationZ_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_accelerationZ_tb.SetValue(Grid.RowProperty, 75);
            Initial_accelerationZ_tb.SetValue(Grid.RowSpanProperty, 5);

            Initial_accelerationZ_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Acceleration Z",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_accelerationZ_ttb);
            Initial_accelerationZ_ttb.SetValue(Grid.ColumnProperty, 183);
            Initial_accelerationZ_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_accelerationZ_ttb.SetValue(Grid.RowProperty, 70);
            Initial_accelerationZ_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

            #region Orientation X
            Initial_orientationX_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_orientationX_tb);
            Initial_orientationX_tb.SetValue(Grid.ColumnProperty, 53);
            Initial_orientationX_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_orientationX_tb.SetValue(Grid.RowProperty, 90);
            Initial_orientationX_tb.SetValue(Grid.RowSpanProperty, 5);

            Initial_orientationX_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Pitch",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_orientationX_ttb);
            Initial_orientationX_ttb.SetValue(Grid.ColumnProperty, 53);
            Initial_orientationX_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_orientationX_ttb.SetValue(Grid.RowProperty, 85);
            Initial_orientationX_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion
            #region Orientation Y
            Initial_orientationY_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_orientationY_tb);
            Initial_orientationY_tb.SetValue(Grid.ColumnProperty, 68);
            Initial_orientationY_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_orientationY_tb.SetValue(Grid.RowProperty, 90);
            Initial_orientationY_tb.SetValue(Grid.RowSpanProperty, 5);

            Initial_orientationY_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Roll",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_orientationY_ttb);
            Initial_orientationY_ttb.SetValue(Grid.ColumnProperty, 68);
            Initial_orientationY_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_orientationY_ttb.SetValue(Grid.RowProperty, 85);
            Initial_orientationY_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion
            #region Orientation Z
            Initial_orientationZ_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_orientationZ_tb);
            Initial_orientationZ_tb.SetValue(Grid.ColumnProperty, 83);
            Initial_orientationZ_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_orientationZ_tb.SetValue(Grid.RowProperty, 90);
            Initial_orientationZ_tb.SetValue(Grid.RowSpanProperty, 5);

            Initial_orientationZ_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Yaw",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_orientationZ_ttb);
            Initial_orientationZ_ttb.SetValue(Grid.ColumnProperty, 83);
            Initial_orientationZ_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_orientationZ_ttb.SetValue(Grid.RowProperty, 85);
            Initial_orientationZ_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion



            #region M Pointing Angle Az
            Initial_mpointan_az_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_mpointan_az_tb);
            Initial_mpointan_az_tb.SetValue(Grid.ColumnProperty, 103);
            Initial_mpointan_az_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_mpointan_az_tb.SetValue(Grid.RowProperty, 90);
            Initial_mpointan_az_tb.SetValue(Grid.RowSpanProperty, 5);

            Initial_mpointan_az_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Mech Ster Ang AZ",
                Foreground = Sea_Green,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_mpointan_az_ttb);
            Initial_mpointan_az_ttb.SetValue(Grid.ColumnProperty, 103);
            Initial_mpointan_az_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_mpointan_az_ttb.SetValue(Grid.RowProperty, 85);
            Initial_mpointan_az_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

            #region M Pointing Angle EL
            Initial_mpointan_el_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_mpointan_el_tb);
            Initial_mpointan_el_tb.SetValue(Grid.ColumnProperty, 118);
            Initial_mpointan_el_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_mpointan_el_tb.SetValue(Grid.RowProperty, 90);
            Initial_mpointan_el_tb.SetValue(Grid.RowSpanProperty, 5);

            Initial_mpointan_el_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Mech Ster Ang EL",
                Foreground = Sea_Green,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_mpointan_el_ttb);
            Initial_mpointan_el_ttb.SetValue(Grid.ColumnProperty, 118);
            Initial_mpointan_el_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_mpointan_el_ttb.SetValue(Grid.RowProperty, 85);
            Initial_mpointan_el_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

            #region M Pointing Angle Az
            Initial_epointan_az_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_epointan_az_tb);
            Initial_epointan_az_tb.SetValue(Grid.ColumnProperty, 138);
            Initial_epointan_az_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_epointan_az_tb.SetValue(Grid.RowProperty, 90);
            Initial_epointan_az_tb.SetValue(Grid.RowSpanProperty, 5);

            Initial_epointan_az_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Elec Ster Ang AZ",
                Foreground = Sea_Green,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_epointan_az_ttb);
            Initial_epointan_az_ttb.SetValue(Grid.ColumnProperty, 138);
            Initial_epointan_az_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_epointan_az_ttb.SetValue(Grid.RowProperty, 85);
            Initial_epointan_az_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

            #region E Pointing Angle EL
            Initial_epointan_el_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_epointan_el_tb);
            Initial_epointan_el_tb.SetValue(Grid.ColumnProperty, 153);
            Initial_epointan_el_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_epointan_el_tb.SetValue(Grid.RowProperty, 90);
            Initial_epointan_el_tb.SetValue(Grid.RowSpanProperty, 5);

            Initial_epointan_el_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Elec Ster Ang EL",
                Foreground = Sea_Green,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Initial_epointan_el_ttb);
            Initial_epointan_el_ttb.SetValue(Grid.ColumnProperty, 153);
            Initial_epointan_el_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Initial_epointan_el_ttb.SetValue(Grid.RowProperty, 85);
            Initial_epointan_el_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

        }

        public void Global_Property_hide()
        {
            #region initial lat
            Sc_centerX_tb.Visibility = Visibility.Collapsed;

            Sc_centerX_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region initial lon
            Sc_centerY_tb.Visibility = Visibility.Collapsed;

            Sc_centerY_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region Height
            Sc_centerZ_tb.Visibility = Visibility.Collapsed;

            Sc_centerZ_ttb.Visibility = Visibility.Collapsed;
            #endregion
        }
        public void Global_Property_hide_s()
        {
            DRBE_SPL.Children.Clear();
            DRBE_SPR.Children.Clear();
        }
        public void Global_Property_show()
        {
            #region initial lat
            Sc_centerX_tb.Visibility = Visibility.Visible;

            Sc_centerX_ttb.Visibility = Visibility.Visible;
            #endregion

            #region initial lon
            Sc_centerY_tb.Visibility = Visibility.Visible;

            Sc_centerY_ttb.Visibility = Visibility.Visible;
            #endregion

            #region Height
            Sc_centerZ_tb.Visibility = Visibility.Visible;

            Sc_centerZ_ttb.Visibility = Visibility.Visible;
            #endregion
        }
        public void All_Property_show_s()
        {
            Global_Property_show_s();
            Initial_Property_show_s();
        }
        public void Global_Property_show_s()
        {
            #region initial lat
            DRBE_SPR.Children.Add(Sc_centerX_tb);
            DRBE_SPL.Children.Add(Sc_centerX_ttb);
            DRBE_SPL.Children.Add(Sc_centerX_bd);
            DRBE_SPR.Children.Add(Sc_centerX_bd1);
            #endregion

            #region initial lon

            DRBE_SPR.Children.Add(Sc_centerY_tb);
            DRBE_SPL.Children.Add(Sc_centerY_ttb);
            DRBE_SPL.Children.Add(Sc_centerY_bd);
            DRBE_SPR.Children.Add(Sc_centerY_bd1);
            #endregion

            #region Height
            DRBE_SPR.Children.Add(Sc_centerZ_tb);
            DRBE_SPL.Children.Add(Sc_centerZ_ttb);
            DRBE_SPL.Children.Add(Sc_centerZ_bd);
            DRBE_SPR.Children.Add(Sc_centerZ_bd1);
            #endregion
        }
        public void Initial_Property_hide()
        {




            #region coordination

            Coordinate_system_cb.Visibility = Visibility.Collapsed;

            Coordinate_system_tb.Visibility = Visibility.Collapsed;

            #endregion


            #region object reference
            Reference_frame_id_cb.Visibility = Visibility.Collapsed;

            Reference_frame_id_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region Position X
            Initial_positionX_tb.Visibility = Visibility.Collapsed;

            Initial_positionX_ttb.Visibility = Visibility.Collapsed;
            #endregion
            #region Position Y
            Initial_positionY_tb.Visibility = Visibility.Collapsed;

            Initial_positionY_ttb.Visibility = Visibility.Collapsed;
            #endregion
            #region Position Z
            Initial_positionZ_tb.Visibility = Visibility.Collapsed;

            Initial_positionZ_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region Velocity X
            Initial_velocityX_tb.Visibility = Visibility.Collapsed;

            Initial_velocityX_ttb.Visibility = Visibility.Collapsed;
            #endregion
            #region Velocity Y
            Initial_velocityY_tb.Visibility = Visibility.Collapsed;

            Initial_velocityY_ttb.Visibility = Visibility.Collapsed;
            #endregion
            #region Velocity Z
            Initial_velocityZ_tb.Visibility = Visibility.Collapsed;

            Initial_velocityZ_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region Acceleration X
            Initial_accelerationX_tb.Visibility = Visibility.Collapsed;

            Initial_accelerationX_ttb.Visibility = Visibility.Collapsed;
            #endregion
            #region Acceleration Y
            Initial_accelerationY_tb.Visibility = Visibility.Collapsed;

            Initial_accelerationY_ttb.Visibility = Visibility.Collapsed;
            #endregion
            #region Acceleration Z
            Initial_accelerationZ_tb.Visibility = Visibility.Collapsed;

            Initial_accelerationZ_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region Orientation X
            Initial_orientationX_tb.Visibility = Visibility.Collapsed;

            Initial_orientationX_ttb.Visibility = Visibility.Collapsed;
            #endregion
            #region Orientation Y
            Initial_orientationY_tb.Visibility = Visibility.Collapsed;

            Initial_orientationY_ttb.Visibility = Visibility.Collapsed;
            #endregion
            #region Orientation Z
            Initial_orientationZ_tb.Visibility = Visibility.Collapsed;

            Initial_orientationZ_ttb.Visibility = Visibility.Collapsed;
            #endregion



            #region M Pointing Angle Az
            Initial_mpointan_az_tb.Visibility = Visibility.Collapsed;

            Initial_mpointan_az_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region M Pointing Angle EL
            Initial_mpointan_el_tb.Visibility = Visibility.Collapsed;

            Initial_mpointan_el_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region M Pointing Angle Az
            Initial_epointan_az_tb.Visibility = Visibility.Collapsed;

            Initial_epointan_az_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region E Pointing Angle EL
            Initial_epointan_el_tb.Visibility = Visibility.Collapsed;

            Initial_epointan_el_ttb.Visibility = Visibility.Collapsed;
            #endregion
        }
        public void Initial_Property_hide_s()
        {
            DRBE_SPL.Children.Clear();
            DRBE_SPR.Children.Clear();
        }
        public void Initial_Property_show()
        {



            #region coordination

            Coordinate_system_cb.Visibility = Visibility.Visible;

            Coordinate_system_tb.Visibility = Visibility.Visible;

            #endregion

            #region object reference
            Reference_frame_id_cb.Visibility = Visibility.Visible;

            Reference_frame_id_ttb.Visibility = Visibility.Visible;
            #endregion


            #region Position X
            Initial_positionX_tb.Visibility = Visibility.Visible;

            Initial_positionX_ttb.Visibility = Visibility.Visible;
            #endregion
            #region Position Y
            Initial_positionY_tb.Visibility = Visibility.Visible;

            Initial_positionY_ttb.Visibility = Visibility.Visible;
            #endregion
            #region Position Z
            Initial_positionZ_tb.Visibility = Visibility.Visible;

            Initial_positionZ_ttb.Visibility = Visibility.Visible;
            #endregion

            #region Velocity X
            Initial_velocityX_tb.Visibility = Visibility.Visible;

            Initial_velocityX_ttb.Visibility = Visibility.Visible;
            #endregion
            #region Velocity Y
            Initial_velocityY_tb.Visibility = Visibility.Visible;

            Initial_velocityY_ttb.Visibility = Visibility.Visible;
            #endregion
            #region Velocity Z
            Initial_velocityZ_tb.Visibility = Visibility.Visible;

            Initial_velocityZ_ttb.Visibility = Visibility.Visible;
            #endregion

            #region Acceleration X
            Initial_accelerationX_tb.Visibility = Visibility.Visible;

            Initial_accelerationX_ttb.Visibility = Visibility.Visible;
            #endregion
            #region Acceleration Y
            Initial_accelerationY_tb.Visibility = Visibility.Visible;

            Initial_accelerationY_ttb.Visibility = Visibility.Visible;
            #endregion
            #region Acceleration Z
            Initial_accelerationZ_tb.Visibility = Visibility.Visible;

            Initial_accelerationZ_ttb.Visibility = Visibility.Visible;
            #endregion

            #region Orientation X
            Initial_orientationX_tb.Visibility = Visibility.Visible;

            Initial_orientationX_ttb.Visibility = Visibility.Visible;
            #endregion
            #region Orientation Y
            Initial_orientationY_tb.Visibility = Visibility.Visible;

            Initial_orientationY_ttb.Visibility = Visibility.Visible;
            #endregion
            #region Orientation Z
            Initial_orientationZ_tb.Visibility = Visibility.Visible;

            Initial_orientationZ_ttb.Visibility = Visibility.Visible;
            #endregion



            #region M Pointing Angle Az
            Initial_mpointan_az_tb.Visibility = Visibility.Visible;

            Initial_mpointan_az_ttb.Visibility = Visibility.Visible;
            #endregion

            #region M Pointing Angle EL
            Initial_mpointan_el_tb.Visibility = Visibility.Visible;

            Initial_mpointan_el_ttb.Visibility = Visibility.Visible;
            #endregion

            #region M Pointing Angle Az
            Initial_epointan_az_tb.Visibility = Visibility.Visible;

            Initial_epointan_az_ttb.Visibility = Visibility.Visible;
            #endregion

            #region E Pointing Angle EL
            Initial_epointan_el_tb.Visibility = Visibility.Visible;

            Initial_epointan_el_ttb.Visibility = Visibility.Visible;
            #endregion
        }
        public void Initial_Property_show_s()
        {



            #region coordination
            DRBE_SPL.Children.Add(Coordinate_system_tb);
            DRBE_SPR.Children.Add(Coordinate_system_cb);
            DRBE_SPL.Children.Add(Coordinate_system_bd);
            DRBE_SPR.Children.Add(Coordinate_system_bd1);

            #endregion

            #region object reference
            DRBE_SPR.Children.Add(Reference_frame_id_cb);
            DRBE_SPL.Children.Add(Reference_frame_id_ttb);
            DRBE_SPL.Children.Add(Reference_frame_id_bd);
            DRBE_SPR.Children.Add(Reference_frame_id_bd1);
            #endregion



            #region Position X
            DRBE_SPR.Children.Add(Initial_positionX_tb);
            DRBE_SPL.Children.Add(Initial_positionX_ttb);
            DRBE_SPL.Children.Add(Initial_positionX_bd);
            DRBE_SPR.Children.Add(Initial_positionX_bd1);
            #endregion
            #region Position Y
            DRBE_SPR.Children.Add(Initial_positionY_tb);
            DRBE_SPL.Children.Add(Initial_positionY_ttb);
            DRBE_SPL.Children.Add(Initial_positionY_bd);
            DRBE_SPR.Children.Add(Initial_positionY_bd1);
            #endregion
            #region Position Z
            DRBE_SPR.Children.Add(Initial_positionZ_tb);
            DRBE_SPL.Children.Add(Initial_positionZ_ttb);
            DRBE_SPL.Children.Add(Initial_positionZ_bd);
            DRBE_SPR.Children.Add(Initial_positionZ_bd1);
            #endregion

            #region Velocity X
            DRBE_SPR.Children.Add(Initial_velocityX_tb);
            DRBE_SPL.Children.Add(Initial_velocityX_ttb);
            DRBE_SPL.Children.Add(Initial_velocityX_bd);
            DRBE_SPR.Children.Add(Initial_velocityX_bd1);
            #endregion
            #region Velocity Y
            DRBE_SPR.Children.Add(Initial_velocityY_tb);
            DRBE_SPL.Children.Add(Initial_velocityY_ttb);
            DRBE_SPL.Children.Add(Initial_velocityY_bd);
            DRBE_SPR.Children.Add(Initial_velocityY_bd1);
            #endregion
            #region Velocity Z
            DRBE_SPR.Children.Add(Initial_velocityZ_tb);
            DRBE_SPL.Children.Add(Initial_velocityZ_ttb);
            DRBE_SPL.Children.Add(Initial_velocityZ_bd);
            DRBE_SPR.Children.Add(Initial_velocityZ_bd1);
            #endregion

            #region Acceleration X
            DRBE_SPR.Children.Add(Initial_accelerationX_tb);
            DRBE_SPL.Children.Add(Initial_accelerationX_ttb);
            DRBE_SPL.Children.Add(Initial_accelerationX_bd);
            DRBE_SPR.Children.Add(Initial_accelerationX_bd1);
            #endregion
            #region Acceleration Y
            DRBE_SPR.Children.Add(Initial_accelerationY_tb);
            DRBE_SPL.Children.Add(Initial_accelerationY_ttb);
            DRBE_SPL.Children.Add(Initial_accelerationY_bd);
            DRBE_SPR.Children.Add(Initial_accelerationY_bd1);
            #endregion
            #region Acceleration Z
            DRBE_SPR.Children.Add(Initial_accelerationZ_tb);
            DRBE_SPL.Children.Add(Initial_accelerationZ_ttb);
            DRBE_SPL.Children.Add(Initial_accelerationZ_bd);
            DRBE_SPR.Children.Add(Initial_accelerationZ_bd1);
            #endregion

            #region Orientation X
            DRBE_SPR.Children.Add(Initial_orientationX_tb);
            DRBE_SPL.Children.Add(Initial_orientationX_ttb);
            DRBE_SPL.Children.Add(Initial_orientationX_bd);
            DRBE_SPR.Children.Add(Initial_orientationX_bd1);
            #endregion
            #region Orientation Y
            DRBE_SPR.Children.Add(Initial_orientationY_tb);
            DRBE_SPL.Children.Add(Initial_orientationY_ttb);
            DRBE_SPL.Children.Add(Initial_orientationY_bd);
            DRBE_SPR.Children.Add(Initial_orientationY_bd1);
            #endregion
            #region Orientation Z
            DRBE_SPR.Children.Add(Initial_orientationZ_tb);
            DRBE_SPL.Children.Add(Initial_orientationZ_ttb);
            DRBE_SPL.Children.Add(Initial_orientationZ_bd);
            DRBE_SPR.Children.Add(Initial_orientationZ_bd1);
            #endregion



            #region M Pointing Angle Az
            DRBE_SPR.Children.Add(Initial_mpointan_az_tb);
            DRBE_SPL.Children.Add(Initial_mpointan_az_ttb);
            DRBE_SPL.Children.Add(Initial_mpointan_az_bd);
            DRBE_SPR.Children.Add(Initial_mpointan_az_bd1);
            #endregion

            #region M Pointing Angle EL
            DRBE_SPR.Children.Add(Initial_mpointan_el_tb);
            DRBE_SPL.Children.Add(Initial_mpointan_el_ttb);
            DRBE_SPL.Children.Add(Initial_mpointan_el_bd);
            DRBE_SPR.Children.Add(Initial_mpointan_el_bd1);
            #endregion

            #region M Pointing Angle Az
            DRBE_SPR.Children.Add(Initial_epointan_az_tb);
            DRBE_SPL.Children.Add(Initial_epointan_az_ttb);
            DRBE_SPL.Children.Add(Initial_epointan_az_bd);
            DRBE_SPR.Children.Add(Initial_epointan_az_bd1);
            #endregion

            #region E Pointing Angle EL
            DRBE_SPR.Children.Add(Initial_epointan_el_tb);
            DRBE_SPL.Children.Add(Initial_epointan_el_ttb);
            DRBE_SPL.Children.Add(Initial_epointan_el_bd);
            DRBE_SPR.Children.Add(Initial_epointan_el_bd1);
            #endregion
        }


        private TextBlock Beamwidth_EL_ttb = new TextBlock();
        private TextBox Beamwidth_EL_tb = new TextBox();
        private Button Beamwidth_EL_bt = new Button();

        private TextBlock Beamwidth_AZ_ttb = new TextBlock();
        private TextBox Beamwidth_AZ_tb = new TextBox();
        private Button Beamwidth_AZ_bt = new Button();

        private TextBlock Number_of_ant_az_ttb = new TextBlock();
        private TextBox Number_of_ant_az_tb = new TextBox();
        private Button Number_of_ant_az_bt = new Button();

        private TextBlock Number_of_ant_el_ttb = new TextBlock();
        private TextBox Number_of_ant_el_tb = new TextBox();
        private Button Number_of_ant_el_bt = new Button();

        private TextBlock Ele_space_ttb = new TextBlock();
        private TextBox Ele_space_tb = new TextBox();
        private Button Ele_space_bt = new Button();

        private TextBlock Antenna_window_tb = new TextBlock();
        private ComboBox Antenna_window_cb = new ComboBox();
        private Button Antenna_window_bt = new Button();

        private TextBlock Resolution_EL_ttb = new TextBlock();
        private TextBox Resolution_EL_tb = new TextBox();
        private Button Resolution_EL_bt = new Button();

        private TextBlock Resolution_AZ_ttb = new TextBlock();
        private TextBox Resolution_AZ_tb = new TextBox();
        private Button Resolution_AZ_bt = new Button();

        private TextBlock Backlobe_scaling_ttb = new TextBlock();
        private TextBox Backlobe_scaling_tb = new TextBox();
        private Button Backlobe_scaling_bt = new Button();

        private TextBlock Constant_factor_ttb = new TextBlock();
        private TextBox Constant_factor_tb = new TextBox();
        private Button Constant_factor_bt = new Button();

        private TextBlock Polar_coe1_ttb = new TextBlock();
        private TextBox Polar_coe1_tb = new TextBox();
        private Button Polar_coe1_bt = new Button();

        private TextBlock Polar_coe2_ttb = new TextBlock();
        private TextBox Polar_coe2_tb = new TextBox();
        private Button Polar_coe2_bt = new Button();


        public void Antenna_Pol_Property_hide()
        {
            #region Beamwith EL
            Beamwidth_EL_tb.Visibility = Visibility.Collapsed;

            Beamwidth_EL_ttb.Visibility = Visibility.Collapsed;

            Beamwidth_EL_bt.Visibility = Visibility.Collapsed;
            #endregion
            #region Beamwidth AZ
            Beamwidth_AZ_tb.Visibility = Visibility.Collapsed;

            Beamwidth_AZ_ttb.Visibility = Visibility.Collapsed;

            Beamwidth_AZ_bt.Visibility = Visibility.Collapsed;
            #endregion

            #region No of Antenna AZ
            Number_of_ant_az_tb.Visibility = Visibility.Collapsed;

            Number_of_ant_az_ttb.Visibility = Visibility.Collapsed;

            Number_of_ant_az_bt.Visibility = Visibility.Collapsed;
            #endregion
            #region No of Antenna EL
            Number_of_ant_el_tb.Visibility = Visibility.Collapsed;

            Number_of_ant_el_ttb.Visibility = Visibility.Collapsed;

            Number_of_ant_el_bt.Visibility = Visibility.Collapsed;
            #endregion
            #region Element Spacing
            Ele_space_tb.Visibility = Visibility.Collapsed;

            Ele_space_ttb.Visibility = Visibility.Collapsed;

            Ele_space_bt.Visibility = Visibility.Collapsed;
            #endregion

            #region Window Function
            Antenna_window_cb.Visibility = Visibility.Collapsed;

            Antenna_window_tb.Visibility = Visibility.Collapsed;

            Antenna_window_bt.Visibility = Visibility.Collapsed;
            #endregion

            #region Resolution EL
            Resolution_EL_tb.Visibility = Visibility.Collapsed;

            Resolution_EL_ttb.Visibility = Visibility.Collapsed;

            Resolution_EL_bt.Visibility = Visibility.Collapsed;
            #endregion
            #region Resolution AZ
            Resolution_AZ_tb.Visibility = Visibility.Collapsed;

            Resolution_AZ_ttb.Visibility = Visibility.Collapsed;

            Resolution_AZ_bt.Visibility = Visibility.Collapsed;
            #endregion
            #region Backlobe scale
            Backlobe_scaling_tb.Visibility = Visibility.Collapsed;

            Backlobe_scaling_ttb.Visibility = Visibility.Collapsed;

            Backlobe_scaling_bt.Visibility = Visibility.Collapsed;
            #endregion

            #region Resolution AZ
            Constant_factor_tb.Visibility = Visibility.Collapsed;

            Constant_factor_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region Polarization Coe1
            Polar_coe1_tb.Visibility = Visibility.Collapsed;

            Polar_coe1_ttb.Visibility = Visibility.Collapsed;
            #endregion
            #region Polarization Coe2
            Polar_coe2_tb.Visibility = Visibility.Collapsed;

            Polar_coe2_ttb.Visibility = Visibility.Collapsed;
            #endregion
        }
        public void Antenna_Pol_Property_setup()
        {

            #region Beamwith EL
            Beamwidth_EL_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Beamwidth_EL_tb);
            Beamwidth_EL_tb.SetValue(Grid.ColumnProperty, 53);
            Beamwidth_EL_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Beamwidth_EL_tb.SetValue(Grid.RowProperty, 55);
            Beamwidth_EL_tb.SetValue(Grid.RowSpanProperty, 5);

            Beamwidth_EL_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Beamwith EL",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Beamwidth_EL_ttb);
            Beamwidth_EL_ttb.SetValue(Grid.ColumnProperty, 53);
            Beamwidth_EL_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Beamwidth_EL_ttb.SetValue(Grid.RowProperty, 50);
            Beamwidth_EL_ttb.SetValue(Grid.RowSpanProperty, 5);

            Beamwidth_EL_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "Beamwith EL",
                Foreground = white_button_brush,
                BorderBrush = orange_brush,
                BorderThickness = new Thickness(3, 3, 3, 3),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 12
            };
            ParentGrid.Children.Add(Beamwidth_EL_bt);
            Beamwidth_EL_bt.SetValue(Grid.ColumnProperty, 53);
            Beamwidth_EL_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Beamwidth_EL_bt.SetValue(Grid.RowProperty, 50);
            Beamwidth_EL_bt.SetValue(Grid.RowSpanProperty, 5);
            Beamwidth_EL_bt.Click += Demo_bt_Click;

            #endregion
            #region Beamwidth AZ
            Beamwidth_AZ_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Beamwidth_AZ_tb);
            Beamwidth_AZ_tb.SetValue(Grid.ColumnProperty, 68);
            Beamwidth_AZ_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Beamwidth_AZ_tb.SetValue(Grid.RowProperty, 55);
            Beamwidth_AZ_tb.SetValue(Grid.RowSpanProperty, 5);

            Beamwidth_AZ_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Beamwidth AZ",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Beamwidth_AZ_ttb);
            Beamwidth_AZ_ttb.SetValue(Grid.ColumnProperty, 68);
            Beamwidth_AZ_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Beamwidth_AZ_ttb.SetValue(Grid.RowProperty, 50);
            Beamwidth_AZ_ttb.SetValue(Grid.RowSpanProperty, 5);

            Beamwidth_AZ_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "Beamwith AZ",
                Foreground = white_button_brush,
                BorderBrush = orange_brush,
                BorderThickness = new Thickness(3, 3, 3, 3),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 12
            };
            ParentGrid.Children.Add(Beamwidth_AZ_bt);
            Beamwidth_AZ_bt.SetValue(Grid.ColumnProperty, 68);
            Beamwidth_AZ_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Beamwidth_AZ_bt.SetValue(Grid.RowProperty, 50);
            Beamwidth_AZ_bt.SetValue(Grid.RowSpanProperty, 5);
            Beamwidth_EL_bt.Click += Demo_bt_Click;
            #endregion

            #region No of Antenna AZ
            Number_of_ant_az_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Number_of_ant_az_tb);
            Number_of_ant_az_tb.SetValue(Grid.ColumnProperty, 88);
            Number_of_ant_az_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Number_of_ant_az_tb.SetValue(Grid.RowProperty, 55);
            Number_of_ant_az_tb.SetValue(Grid.RowSpanProperty, 5);

            Number_of_ant_az_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "No of Antenna AZ",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Number_of_ant_az_ttb);
            Number_of_ant_az_ttb.SetValue(Grid.ColumnProperty, 88);
            Number_of_ant_az_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Number_of_ant_az_ttb.SetValue(Grid.RowProperty, 50);
            Number_of_ant_az_ttb.SetValue(Grid.RowSpanProperty, 5);

            Number_of_ant_az_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "No of Antenna AZ",
                Foreground = white_button_brush,
                BorderBrush = orange_brush,
                BorderThickness = new Thickness(3, 3, 3, 3),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 12
            };
            ParentGrid.Children.Add(Number_of_ant_az_bt);
            Number_of_ant_az_bt.SetValue(Grid.ColumnProperty, 88);
            Number_of_ant_az_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Number_of_ant_az_bt.SetValue(Grid.RowProperty, 50);
            Number_of_ant_az_bt.SetValue(Grid.RowSpanProperty, 5);
            Number_of_ant_az_bt.Click += Demo_bt_Click;
            #endregion
            #region No of Antenna EL
            Number_of_ant_el_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Number_of_ant_el_tb);
            Number_of_ant_el_tb.SetValue(Grid.ColumnProperty, 103);
            Number_of_ant_el_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Number_of_ant_el_tb.SetValue(Grid.RowProperty, 55);
            Number_of_ant_el_tb.SetValue(Grid.RowSpanProperty, 5);

            Number_of_ant_el_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "No of Antenna EL",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Number_of_ant_el_ttb);
            Number_of_ant_el_ttb.SetValue(Grid.ColumnProperty, 103);
            Number_of_ant_el_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Number_of_ant_el_ttb.SetValue(Grid.RowProperty, 50);
            Number_of_ant_el_ttb.SetValue(Grid.RowSpanProperty, 5);

            Number_of_ant_el_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "No of Antenna EL",
                Foreground = white_button_brush,
                BorderBrush = orange_brush,
                BorderThickness = new Thickness(3, 3, 3, 3),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 12
            };
            ParentGrid.Children.Add(Number_of_ant_el_bt);
            Number_of_ant_el_bt.SetValue(Grid.ColumnProperty, 103);
            Number_of_ant_el_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Number_of_ant_el_bt.SetValue(Grid.RowProperty, 50);
            Number_of_ant_el_bt.SetValue(Grid.RowSpanProperty, 5);
            Number_of_ant_el_bt.Click += Demo_bt_Click;
            #endregion
            #region Element Spacing
            Ele_space_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Ele_space_tb);
            Ele_space_tb.SetValue(Grid.ColumnProperty, 118);
            Ele_space_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Ele_space_tb.SetValue(Grid.RowProperty, 55);
            Ele_space_tb.SetValue(Grid.RowSpanProperty, 5);

            Ele_space_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Element Spacing",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Ele_space_ttb);
            Ele_space_ttb.SetValue(Grid.ColumnProperty, 118);
            Ele_space_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Ele_space_ttb.SetValue(Grid.RowProperty, 50);
            Ele_space_ttb.SetValue(Grid.RowSpanProperty, 5);

            Ele_space_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "Element Spacing",
                Foreground = white_button_brush,
                BorderBrush = orange_brush,
                BorderThickness = new Thickness(3, 3, 3, 3),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 12
            };
            ParentGrid.Children.Add(Ele_space_bt);
            Ele_space_bt.SetValue(Grid.ColumnProperty, 118);
            Ele_space_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Ele_space_bt.SetValue(Grid.RowProperty, 50);
            Ele_space_bt.SetValue(Grid.RowSpanProperty, 5);
            Ele_space_bt.Click += Demo_bt_Click;
            #endregion

            #region Window Function
            Antenna_window_cb = new ComboBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Text = "Window Function",
                SelectedIndex = 0,
                Background = white_button_brush
            };
            ParentGrid.Children.Add(Antenna_window_cb);
            Antenna_window_cb.SetValue(Grid.ColumnProperty, 180);
            Antenna_window_cb.SetValue(Grid.ColumnSpanProperty, 20);
            Antenna_window_cb.SetValue(Grid.RowProperty, 55);
            Antenna_window_cb.SetValue(Grid.RowSpanProperty, 5);
            Antenna_window_cb.Items.Add("Uniform/Rectangular");
            Antenna_window_cb.Items.Add("Hann Window");
            Antenna_window_cb.Items.Add("Hamming Window");
            Antenna_window_cb.Items.Add("Blackman Window");


            Antenna_window_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Window Function",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Antenna_window_tb);
            Antenna_window_tb.SetValue(Grid.ColumnProperty, 135);
            Antenna_window_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Antenna_window_tb.SetValue(Grid.RowProperty, 50);
            Antenna_window_tb.SetValue(Grid.RowSpanProperty, 5);

            Antenna_window_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "Window Function",
                Foreground = white_button_brush,
                BorderBrush = orange_brush,
                BorderThickness = new Thickness(3, 3, 3, 3),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 14
            };
            ParentGrid.Children.Add(Antenna_window_bt);
            Antenna_window_bt.SetValue(Grid.ColumnProperty, 135);
            Antenna_window_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Antenna_window_bt.SetValue(Grid.RowProperty, 50);
            Antenna_window_bt.SetValue(Grid.RowSpanProperty, 5);
            Antenna_window_bt.Click += Demo_bt_Click;
            #endregion

            #region Resolution EL
            Resolution_EL_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Resolution_EL_tb);
            Resolution_EL_tb.SetValue(Grid.ColumnProperty, 53);
            Resolution_EL_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Resolution_EL_tb.SetValue(Grid.RowProperty, 70);
            Resolution_EL_tb.SetValue(Grid.RowSpanProperty, 5);

            Resolution_EL_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Resolution EL",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Resolution_EL_ttb);
            Resolution_EL_ttb.SetValue(Grid.ColumnProperty, 53);
            Resolution_EL_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Resolution_EL_ttb.SetValue(Grid.RowProperty, 65);
            Resolution_EL_ttb.SetValue(Grid.RowSpanProperty, 5);

            Resolution_EL_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "Resolution EL",
                Foreground = white_button_brush,
                BorderBrush = orange_brush,
                BorderThickness = new Thickness(3, 3, 3, 3),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 12
            };
            ParentGrid.Children.Add(Resolution_EL_bt);
            Resolution_EL_bt.SetValue(Grid.ColumnProperty, 53);
            Resolution_EL_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Resolution_EL_bt.SetValue(Grid.RowProperty, 65);
            Resolution_EL_bt.SetValue(Grid.RowSpanProperty, 5);
            Resolution_EL_bt.Click += Demo_bt_Click;
            #endregion
            #region Resolution AZ
            Resolution_AZ_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Resolution_AZ_tb);
            Resolution_AZ_tb.SetValue(Grid.ColumnProperty, 68);
            Resolution_AZ_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Resolution_AZ_tb.SetValue(Grid.RowProperty, 70);
            Resolution_AZ_tb.SetValue(Grid.RowSpanProperty, 5);

            Resolution_AZ_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Resolution AZ",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Resolution_AZ_ttb);
            Resolution_AZ_ttb.SetValue(Grid.ColumnProperty, 68);
            Resolution_AZ_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Resolution_AZ_ttb.SetValue(Grid.RowProperty, 65);
            Resolution_AZ_ttb.SetValue(Grid.RowSpanProperty, 5);

            Resolution_AZ_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "Resolution AZ",
                Foreground = white_button_brush,
                BorderBrush = orange_brush,
                BorderThickness = new Thickness(3, 3, 3, 3),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 12
            };
            ParentGrid.Children.Add(Resolution_AZ_bt);
            Resolution_AZ_bt.SetValue(Grid.ColumnProperty, 68);
            Resolution_AZ_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Resolution_AZ_bt.SetValue(Grid.RowProperty, 65);
            Resolution_AZ_bt.SetValue(Grid.RowSpanProperty, 5);
            Resolution_AZ_bt.Click += Demo_bt_Click;
            #endregion
            #region Backlobe scale
            Backlobe_scaling_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Backlobe_scaling_tb);
            Backlobe_scaling_tb.SetValue(Grid.ColumnProperty, 85);
            Backlobe_scaling_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Backlobe_scaling_tb.SetValue(Grid.RowProperty, 70);
            Backlobe_scaling_tb.SetValue(Grid.RowSpanProperty, 5);

            Backlobe_scaling_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Backlobe scaling",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Backlobe_scaling_ttb);
            Backlobe_scaling_ttb.SetValue(Grid.ColumnProperty, 85);
            Backlobe_scaling_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Backlobe_scaling_ttb.SetValue(Grid.RowProperty, 65);
            Backlobe_scaling_ttb.SetValue(Grid.RowSpanProperty, 5);

            Backlobe_scaling_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "Backlobe scaling",
                Foreground = white_button_brush,
                BorderBrush = orange_brush,
                BorderThickness = new Thickness(3, 3, 3, 3),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 12
            };
            ParentGrid.Children.Add(Backlobe_scaling_bt);
            Backlobe_scaling_bt.SetValue(Grid.ColumnProperty, 85);
            Backlobe_scaling_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Backlobe_scaling_bt.SetValue(Grid.RowProperty, 65);
            Backlobe_scaling_bt.SetValue(Grid.RowSpanProperty, 5);
            Backlobe_scaling_bt.Click += Demo_bt_Click;
            #endregion

            #region Constant_factor
            Constant_factor_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Constant_factor_tb);
            Constant_factor_tb.SetValue(Grid.ColumnProperty, 105);
            Constant_factor_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Constant_factor_tb.SetValue(Grid.RowProperty, 70);
            Constant_factor_tb.SetValue(Grid.RowSpanProperty, 5);

            Constant_factor_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Constant Factor\r\n Low Fidelity",
                Foreground = Violet_Red,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Constant_factor_ttb);
            Constant_factor_ttb.SetValue(Grid.ColumnProperty, 105);
            Constant_factor_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            Constant_factor_ttb.SetValue(Grid.RowProperty, 65);
            Constant_factor_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

            #region Polarization Coe1
            Polar_coe1_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Polar_coe1_tb);
            Polar_coe1_tb.SetValue(Grid.ColumnProperty, 53);
            Polar_coe1_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Polar_coe1_tb.SetValue(Grid.RowProperty, 95);
            Polar_coe1_tb.SetValue(Grid.RowSpanProperty, 5);

            Polar_coe1_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Polarization Coefficient 1",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Polar_coe1_ttb);
            Polar_coe1_ttb.SetValue(Grid.ColumnProperty, 53);
            Polar_coe1_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            Polar_coe1_ttb.SetValue(Grid.RowProperty, 90);
            Polar_coe1_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion
            #region Polarization Coe2
            Polar_coe2_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Polar_coe2_tb);
            Polar_coe2_tb.SetValue(Grid.ColumnProperty, 73);
            Polar_coe2_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Polar_coe2_tb.SetValue(Grid.RowProperty, 95);
            Polar_coe2_tb.SetValue(Grid.RowSpanProperty, 5);

            Polar_coe2_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Polarization Coefficient 2",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Polar_coe2_ttb);
            Polar_coe2_ttb.SetValue(Grid.ColumnProperty, 73);
            Polar_coe2_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            Polar_coe2_ttb.SetValue(Grid.RowProperty, 90);
            Polar_coe2_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

        }

        private void Demo_bt_Click(object sender, RoutedEventArgs e)
        {
            Button foo = sender as Button;
            if(foo.BorderBrush == green_bright_button_brush)
            {
                foo.BorderBrush = orange_brush;
            }
            else if (foo.BorderBrush == orange_brush)
            {
                foo.BorderBrush = red_bright_button_brush;
            }
            else if (foo.BorderBrush == red_bright_button_brush)
            {
                foo.BorderBrush = green_bright_button_brush;
            }
        }

        public void Antenna_Pol_Property_show()
        {
            #region Beamwith EL
            Beamwidth_EL_tb.Visibility = Visibility.Visible;

            Beamwidth_EL_ttb.Visibility = Visibility.Visible;

            Beamwidth_EL_bt.Visibility = Visibility.Visible;
            #endregion
            #region Beamwidth AZ
            Beamwidth_AZ_tb.Visibility = Visibility.Visible;

            Beamwidth_AZ_ttb.Visibility = Visibility.Visible;

            Beamwidth_AZ_bt.Visibility = Visibility.Visible;
            #endregion

            #region No of Antenna AZ
            Number_of_ant_az_tb.Visibility = Visibility.Visible;

            Number_of_ant_az_ttb.Visibility = Visibility.Visible;

            Number_of_ant_az_bt.Visibility = Visibility.Visible;
            #endregion
            #region No of Antenna EL
            Number_of_ant_el_tb.Visibility = Visibility.Visible;

            Number_of_ant_el_ttb.Visibility = Visibility.Visible;

            Number_of_ant_el_bt.Visibility = Visibility.Visible;
            #endregion
            #region Element Spacing
            Ele_space_tb.Visibility = Visibility.Visible;

            Ele_space_ttb.Visibility = Visibility.Visible;

            Ele_space_bt.Visibility = Visibility.Visible;
            #endregion

            #region Window Function
            Antenna_window_cb.Visibility = Visibility.Visible;

            Antenna_window_tb.Visibility = Visibility.Visible;

            Antenna_window_bt.Visibility = Visibility.Visible;
            #endregion

            #region Resolution EL
            Resolution_EL_tb.Visibility = Visibility.Visible;

            Resolution_EL_ttb.Visibility = Visibility.Visible;

            Resolution_EL_bt.Visibility = Visibility.Visible;
            #endregion
            #region Resolution AZ
            Resolution_AZ_tb.Visibility = Visibility.Visible;

            Resolution_AZ_ttb.Visibility = Visibility.Visible;

            Resolution_AZ_bt.Visibility = Visibility.Visible;
            #endregion
            #region Backlobe scale
            Backlobe_scaling_tb.Visibility = Visibility.Visible;

            Backlobe_scaling_ttb.Visibility = Visibility.Visible;

            Backlobe_scaling_bt.Visibility = Visibility.Visible;
            #endregion

            #region Resolution AZ
            Constant_factor_tb.Visibility = Visibility.Visible;

            Constant_factor_ttb.Visibility = Visibility.Visible;

            Constant_factor_bt.Visibility = Visibility.Visible;
            #endregion

            #region Polarization Coe1
            Polar_coe1_tb.Visibility = Visibility.Visible;

            Polar_coe1_ttb.Visibility = Visibility.Visible;
            #endregion
            #region Polarization Coe2
            Polar_coe2_tb.Visibility = Visibility.Visible;

            Polar_coe2_ttb.Visibility = Visibility.Visible;
            #endregion
        }

        private TextBlock RCS_holder1_ttb = new TextBlock();
        private TextBox RCS_holder1_tb = new TextBox();

        public void RCS_Property_setup()
        {

            #region RCS holder
            RCS_holder1_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(RCS_holder1_tb);
            RCS_holder1_tb.SetValue(Grid.ColumnProperty, 53);
            RCS_holder1_tb.SetValue(Grid.ColumnSpanProperty, 15);
            RCS_holder1_tb.SetValue(Grid.RowProperty, 55);
            RCS_holder1_tb.SetValue(Grid.RowSpanProperty, 5);

            RCS_holder1_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "RCS Place holder",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(RCS_holder1_ttb);
            RCS_holder1_ttb.SetValue(Grid.ColumnProperty, 53);
            RCS_holder1_ttb.SetValue(Grid.ColumnSpanProperty, 15);
            RCS_holder1_ttb.SetValue(Grid.RowProperty, 50);
            RCS_holder1_ttb.SetValue(Grid.RowSpanProperty, 5);
            #endregion

        }
        public void RCS_Property_hide()
        {

            #region RCS holder
            RCS_holder1_tb.Visibility = Visibility.Collapsed;

            RCS_holder1_ttb.Visibility = Visibility.Collapsed;
            #endregion
        }
        public void RCS_Property_show()
        {

            #region RCS holder
            RCS_holder1_tb.Visibility = Visibility.Visible;

            RCS_holder1_ttb.Visibility = Visibility.Visible;
            #endregion
        }

        private TextBlock Clut_Gamma_k_ttb = new TextBlock();
        private TextBox Clut_Gamma_k_tb = new TextBox();

        private TextBlock Clut_Gamma_theta_ttb = new TextBlock();
        private TextBox Clut_Gamma_theta_tb = new TextBox();

        private TextBlock Clut_Gausian_m_ttb = new TextBlock();
        private TextBox Clut_Gausian_m_tb = new TextBox();

        private TextBlock Clut_Gausian_v_ttb = new TextBlock();
        private TextBox Clut_Gausian_v_tb = new TextBox();

        private TextBlock CPI_ttb = new TextBlock();
        private TextBox CPI_tb = new TextBox();

        private TextBlock PRI_ttb = new TextBlock();
        private TextBox PRI_tb = new TextBox();

        public void RF_Clut_Property_setup()
        {

            #region Clutter Gamma K
            Clut_Gamma_k_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Clut_Gamma_k_tb);
            Clut_Gamma_k_tb.SetValue(Grid.ColumnProperty, 53);
            Clut_Gamma_k_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Clut_Gamma_k_tb.SetValue(Grid.RowProperty, 57);
            Clut_Gamma_k_tb.SetValue(Grid.RowSpanProperty, 5);
            

            Clut_Gamma_k_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Clutter: Gamma \r\n Shape Parameter: k",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Clut_Gamma_k_ttb);
            Clut_Gamma_k_ttb.SetValue(Grid.ColumnProperty, 53);
            Clut_Gamma_k_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            Clut_Gamma_k_ttb.SetValue(Grid.RowProperty, 50);
            Clut_Gamma_k_ttb.SetValue(Grid.RowSpanProperty, 7);
            #endregion
            #region Clutter Gamma theta
            Clut_Gamma_theta_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Clut_Gamma_theta_tb);
            Clut_Gamma_theta_tb.SetValue(Grid.ColumnProperty, 73);
            Clut_Gamma_theta_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Clut_Gamma_theta_tb.SetValue(Grid.RowProperty, 57);
            Clut_Gamma_theta_tb.SetValue(Grid.RowSpanProperty, 5);


            Clut_Gamma_theta_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Clutter: Gamma \r\n Shape Parameter: " + "\u0398",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Clut_Gamma_theta_ttb);
            Clut_Gamma_theta_ttb.SetValue(Grid.ColumnProperty, 73);
            Clut_Gamma_theta_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            Clut_Gamma_theta_ttb.SetValue(Grid.RowProperty, 50);
            Clut_Gamma_theta_ttb.SetValue(Grid.RowSpanProperty, 7);
            #endregion

            #region Clutter Gausian m
            Clut_Gausian_m_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Clut_Gausian_m_tb);
            Clut_Gausian_m_tb.SetValue(Grid.ColumnProperty, 98);
            Clut_Gausian_m_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Clut_Gausian_m_tb.SetValue(Grid.RowProperty, 57);
            Clut_Gausian_m_tb.SetValue(Grid.RowSpanProperty, 5);


            Clut_Gausian_m_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Clutter Gausian: \r\n mean",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Clut_Gausian_m_ttb);
            Clut_Gausian_m_ttb.SetValue(Grid.ColumnProperty, 98);
            Clut_Gausian_m_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            Clut_Gausian_m_ttb.SetValue(Grid.RowProperty, 50);
            Clut_Gausian_m_ttb.SetValue(Grid.RowSpanProperty, 7);
            #endregion
            #region Clutter Gausian Variant
            Clut_Gausian_v_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Clut_Gausian_v_tb);
            Clut_Gausian_v_tb.SetValue(Grid.ColumnProperty, 118);
            Clut_Gausian_v_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Clut_Gausian_v_tb.SetValue(Grid.RowProperty, 57);
            Clut_Gausian_v_tb.SetValue(Grid.RowSpanProperty, 5);


            Clut_Gausian_v_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                //Text = "Clutter Gausian: \r\n variant",
                Text = "Clutter ring size: ",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Clut_Gausian_v_ttb);
            Clut_Gausian_v_ttb.SetValue(Grid.ColumnProperty, 118);
            Clut_Gausian_v_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            Clut_Gausian_v_ttb.SetValue(Grid.RowProperty, 50);
            Clut_Gausian_v_ttb.SetValue(Grid.RowSpanProperty, 7);
            #endregion
        }
        public void RF_Clut_Property_hide()
        {

            #region Clutter Gamma K
            Clut_Gamma_k_tb.Visibility = Visibility.Collapsed;


            Clut_Gamma_k_ttb.Visibility = Visibility.Collapsed;
            #endregion
            #region Clutter Gamma theta
            Clut_Gamma_theta_tb.Visibility = Visibility.Collapsed;


            Clut_Gamma_theta_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region Clutter Gausian m
            Clut_Gausian_m_tb.Visibility = Visibility.Collapsed;


            Clut_Gausian_m_ttb.Visibility = Visibility.Collapsed;
            #endregion
            #region Clutter Gausian Variant
            Clut_Gausian_v_tb.Visibility = Visibility.Collapsed;


            Clut_Gausian_v_ttb.Visibility = Visibility.Collapsed;
            #endregion
        }
        public void RF_Clut_Property_show()
        {

            #region Clutter Gamma K
            Clut_Gamma_k_tb.Visibility = Visibility.Visible;


            Clut_Gamma_k_ttb.Visibility = Visibility.Visible;
            #endregion
            #region Clutter Gamma theta
            Clut_Gamma_theta_tb.Visibility = Visibility.Visible;


            Clut_Gamma_theta_ttb.Visibility = Visibility.Visible;
            #endregion

            #region Clutter Gausian m
            Clut_Gausian_m_tb.Visibility = Visibility.Visible;


            Clut_Gausian_m_ttb.Visibility = Visibility.Visible;
            #endregion
            #region Clutter Gausian Variant
            Clut_Gausian_v_tb.Visibility = Visibility.Visible;


            Clut_Gausian_v_ttb.Visibility = Visibility.Visible;
            #endregion
        }

        private TextBlock Obj_centerX_ttb = new TextBlock();
        private TextBox Obj_centerX_tb = new TextBox();

        private TextBlock Obj_centerY_ttb = new TextBlock();
        private TextBox Obj_centerY_tb = new TextBox();

        private TextBlock Obj_centerZ_ttb = new TextBlock();
        private TextBox Obj_centerZ_tb = new TextBox();

        private TextBlock Obj_max_height_ttb = new TextBlock();
        private TextBox Obj_max_height_tb = new TextBox();

        private TextBlock Obj_radius_ttb = new TextBlock();
        private TextBox Obj_radius_tb = new TextBox();

        private TextBlock Obj_max_speed_ttb = new TextBlock();
        private TextBox Obj_max_speed_tb = new TextBox();

        private TextBlock Obj_max_acc_ttb = new TextBlock();
        private TextBox Obj_max_acc_tb = new TextBox();

        private TextBlock Obj_max_ori_ttb = new TextBlock();
        private TextBox Obj_max_ori_tb = new TextBox();

        private TextBlock Obj_min_band_ttb = new TextBlock();
        private TextBox Obj_min_band_tb = new TextBox();

        private TextBlock Obj_max_band_ttb = new TextBlock();
        private TextBox Obj_max_band_tb = new TextBox();

        private TextBlock Obj_max_bandchg_ttb = new TextBlock();
        private TextBox Obj_max_bandchg_tb = new TextBox();

        private TextBlock Obj_min_centerfreq_ttb = new TextBlock();
        private TextBox Obj_min_centerfreq_tb = new TextBox();

        private TextBlock Obj_max_centerfreq_ttb = new TextBlock();
        private TextBox Obj_max_centerfreq_tb = new TextBox();

        private TextBlock Obj_min_CPI_ttb = new TextBlock();
        private TextBox Obj_min_CPI_tb = new TextBox();

        private TextBlock Obj_max_CPI_ttb = new TextBlock();
        private TextBox Obj_max_CPI_tb = new TextBox();

        private TextBlock Obj_min_PRI_ttb = new TextBlock();
        private TextBox Obj_min_PRI_tb = new TextBox();

        private TextBlock Obj_max_PRI_ttb = new TextBlock();
        private TextBox Obj_max_PRI_tb = new TextBox();


        public void Constraint_Property_setup()
        {
            #region Object Center X
            Obj_centerX_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_centerX_tb);
            Obj_centerX_tb.SetValue(Grid.ColumnProperty, 53);
            Obj_centerX_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_centerX_tb.SetValue(Grid.RowProperty, 57);
            Obj_centerX_tb.SetValue(Grid.RowSpanProperty, 5);


            Obj_centerX_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Center of movement X",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_centerX_ttb);
            Obj_centerX_ttb.SetValue(Grid.ColumnProperty, 53);
            Obj_centerX_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_centerX_ttb.SetValue(Grid.RowProperty, 50);
            Obj_centerX_ttb.SetValue(Grid.RowSpanProperty, 7);
            #endregion

            #region Object Center Y
            Obj_centerY_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_centerY_tb);
            Obj_centerY_tb.SetValue(Grid.ColumnProperty, 73);
            Obj_centerY_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_centerY_tb.SetValue(Grid.RowProperty, 57);
            Obj_centerY_tb.SetValue(Grid.RowSpanProperty, 5);


            Obj_centerY_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Center of movement Y",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_centerY_ttb);
            Obj_centerY_ttb.SetValue(Grid.ColumnProperty, 73);
            Obj_centerY_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_centerY_ttb.SetValue(Grid.RowProperty, 50);
            Obj_centerY_ttb.SetValue(Grid.RowSpanProperty, 7);
            #endregion

            #region Object Center Z
            Obj_centerZ_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_centerZ_tb);
            Obj_centerZ_tb.SetValue(Grid.ColumnProperty, 93);
            Obj_centerZ_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_centerZ_tb.SetValue(Grid.RowProperty, 57);
            Obj_centerZ_tb.SetValue(Grid.RowSpanProperty, 5);


            Obj_centerZ_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Center of movement Z",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_centerZ_ttb);
            Obj_centerZ_ttb.SetValue(Grid.ColumnProperty, 93);
            Obj_centerZ_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_centerZ_ttb.SetValue(Grid.RowProperty, 50);
            Obj_centerZ_ttb.SetValue(Grid.RowSpanProperty, 7);
            #endregion

            #region Object max height 
            Obj_max_height_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_max_height_tb);
            Obj_max_height_tb.SetValue(Grid.ColumnProperty, 118);
            Obj_max_height_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_max_height_tb.SetValue(Grid.RowProperty, 57);
            Obj_max_height_tb.SetValue(Grid.RowSpanProperty, 5);


            Obj_max_height_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Max height",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_max_height_ttb);
            Obj_max_height_ttb.SetValue(Grid.ColumnProperty, 118);
            Obj_max_height_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_max_height_ttb.SetValue(Grid.RowProperty, 50);
            Obj_max_height_ttb.SetValue(Grid.RowSpanProperty, 7);
            #endregion

            #region Object Radius 
            Obj_radius_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_radius_tb);
            Obj_radius_tb.SetValue(Grid.ColumnProperty, 143);
            Obj_radius_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_radius_tb.SetValue(Grid.RowProperty, 57);
            Obj_radius_tb.SetValue(Grid.RowSpanProperty, 5);


            Obj_radius_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Radius of movement",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_radius_ttb);
            Obj_radius_ttb.SetValue(Grid.ColumnProperty, 143);
            Obj_radius_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_radius_ttb.SetValue(Grid.RowProperty, 50);
            Obj_radius_ttb.SetValue(Grid.RowSpanProperty, 7);
            #endregion

            #region Object max speed
            Obj_max_speed_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_max_speed_tb);
            Obj_max_speed_tb.SetValue(Grid.ColumnProperty, 53);
            Obj_max_speed_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_max_speed_tb.SetValue(Grid.RowProperty, 77);
            Obj_max_speed_tb.SetValue(Grid.RowSpanProperty, 5);


            Obj_max_speed_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                //Text = "Center of movement X",
                Text = "Maximum Length of Obj",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_max_speed_ttb);
            Obj_max_speed_ttb.SetValue(Grid.ColumnProperty, 53);
            Obj_max_speed_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_max_speed_ttb.SetValue(Grid.RowProperty, 70);
            Obj_max_speed_ttb.SetValue(Grid.RowSpanProperty, 7);
            #endregion

            #region Object max acceleration
            Obj_max_acc_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_max_acc_tb);
            Obj_max_acc_tb.SetValue(Grid.ColumnProperty, 73);
            Obj_max_acc_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_max_acc_tb.SetValue(Grid.RowProperty, 77);
            Obj_max_acc_tb.SetValue(Grid.RowSpanProperty, 5);


            Obj_max_acc_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Maximum acceleration",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_max_acc_ttb);
            Obj_max_acc_ttb.SetValue(Grid.ColumnProperty, 73);
            Obj_max_acc_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_max_acc_ttb.SetValue(Grid.RowProperty, 70);
            Obj_max_acc_ttb.SetValue(Grid.RowSpanProperty, 7);
            #endregion

            #region Object max turn Angle
            Obj_max_ori_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_max_ori_tb);
            Obj_max_ori_tb.SetValue(Grid.ColumnProperty, 93);
            Obj_max_ori_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_max_ori_tb.SetValue(Grid.RowProperty, 77);
            Obj_max_ori_tb.SetValue(Grid.RowSpanProperty, 5);


            Obj_max_ori_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Maximum turn Angle: \r\n Degree\\Second",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_max_ori_ttb);
            Obj_max_ori_ttb.SetValue(Grid.ColumnProperty, 93);
            Obj_max_ori_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_max_ori_ttb.SetValue(Grid.RowProperty, 70);
            Obj_max_ori_ttb.SetValue(Grid.RowSpanProperty, 7);
            #endregion

            #region Object min bandwidth
            Obj_min_band_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_min_band_tb);
            Obj_min_band_tb.SetValue(Grid.ColumnProperty, 118);
            Obj_min_band_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_min_band_tb.SetValue(Grid.RowProperty, 77);
            Obj_min_band_tb.SetValue(Grid.RowSpanProperty, 5);


            Obj_min_band_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Minimum bandwidth",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_min_band_ttb);
            Obj_min_band_ttb.SetValue(Grid.ColumnProperty, 118);
            Obj_min_band_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_min_band_ttb.SetValue(Grid.RowProperty, 70);
            Obj_min_band_ttb.SetValue(Grid.RowSpanProperty, 7);
            #endregion

            #region Object max bandwidth
            Obj_max_band_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_max_band_tb);
            Obj_max_band_tb.SetValue(Grid.ColumnProperty, 138);
            Obj_max_band_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_max_band_tb.SetValue(Grid.RowProperty, 77);
            Obj_max_band_tb.SetValue(Grid.RowSpanProperty, 5);


            Obj_max_band_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Maximum bandwidth",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_max_band_ttb);
            Obj_max_band_ttb.SetValue(Grid.ColumnProperty, 138);
            Obj_max_band_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_max_band_ttb.SetValue(Grid.RowProperty, 70);
            Obj_max_band_ttb.SetValue(Grid.RowSpanProperty, 7);
            #endregion

            #region Object max bandwidth change
            Obj_max_bandchg_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_max_bandchg_tb);
            Obj_max_bandchg_tb.SetValue(Grid.ColumnProperty, 158);
            Obj_max_bandchg_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_max_bandchg_tb.SetValue(Grid.RowProperty, 77);
            Obj_max_bandchg_tb.SetValue(Grid.RowSpanProperty, 5);


            Obj_max_bandchg_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Max bandwidth chg: \r\n Hz\\ms",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Obj_max_bandchg_ttb);
            Obj_max_bandchg_ttb.SetValue(Grid.ColumnProperty, 158);
            Obj_max_bandchg_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            Obj_max_bandchg_ttb.SetValue(Grid.RowProperty, 70);
            Obj_max_bandchg_ttb.SetValue(Grid.RowSpanProperty, 7);
            #endregion
        }

        public void Constraint_Property_hide()
        {
            #region Object Center X
            Obj_centerX_tb.Visibility = Visibility.Collapsed;


            Obj_centerX_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region Object Center Y
            Obj_centerY_tb.Visibility = Visibility.Collapsed;


            Obj_centerY_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region Object Center Z
            Obj_centerZ_tb.Visibility = Visibility.Collapsed;


            Obj_centerZ_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region Object max height 
            Obj_max_height_tb.Visibility = Visibility.Collapsed;


            Obj_max_height_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region Object Radius 
            Obj_radius_tb.Visibility = Visibility.Collapsed;


            Obj_radius_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region Object max speed
            Obj_max_speed_tb.Visibility = Visibility.Collapsed;


            Obj_max_speed_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region Object max acceleration
            Obj_max_acc_tb.Visibility = Visibility.Collapsed;

            Obj_max_acc_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region Object max turn Angle
            Obj_max_ori_tb.Visibility = Visibility.Collapsed;


            Obj_max_ori_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region Object min bandwidth
            Obj_min_band_tb.Visibility = Visibility.Collapsed;


            Obj_min_band_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region Object max bandwidth
            Obj_max_band_tb.Visibility = Visibility.Collapsed;


            Obj_max_band_ttb.Visibility = Visibility.Collapsed;
            #endregion

            #region Object max bandwidth change
            Obj_max_bandchg_tb.Visibility = Visibility.Collapsed;


            Obj_max_bandchg_ttb.Visibility = Visibility.Collapsed;
            #endregion
        }

        public void Constraint_Property_show()
        {
            #region Object Center X
            Obj_centerX_tb.Visibility = Visibility.Visible;


            Obj_centerX_ttb.Visibility = Visibility.Visible;
            #endregion

            #region Object Center Y
            Obj_centerY_tb.Visibility = Visibility.Visible;


            Obj_centerY_ttb.Visibility = Visibility.Visible;
            #endregion

            #region Object Center Z
            Obj_centerZ_tb.Visibility = Visibility.Visible;


            Obj_centerZ_ttb.Visibility = Visibility.Visible;
            #endregion

            #region Object max height 
            Obj_max_height_tb.Visibility = Visibility.Visible;


            Obj_max_height_ttb.Visibility = Visibility.Visible;
            #endregion

            #region Object Radius 
            Obj_radius_tb.Visibility = Visibility.Visible;


            Obj_radius_ttb.Visibility = Visibility.Visible;
            #endregion

            #region Object max speed
            Obj_max_speed_tb.Visibility = Visibility.Visible;


            Obj_max_speed_ttb.Visibility = Visibility.Visible;
            #endregion

            #region Object max acceleration
            Obj_max_acc_tb.Visibility = Visibility.Visible;

            Obj_max_acc_ttb.Visibility = Visibility.Visible;
            #endregion

            #region Object max turn Angle
            Obj_max_ori_tb.Visibility = Visibility.Visible;


            Obj_max_ori_ttb.Visibility = Visibility.Visible;
            #endregion

            #region Object min bandwidth
            Obj_min_band_tb.Visibility = Visibility.Visible;


            Obj_min_band_ttb.Visibility = Visibility.Visible;
            #endregion

            #region Object max bandwidth
            Obj_max_band_tb.Visibility = Visibility.Visible;


            Obj_max_band_ttb.Visibility = Visibility.Visible;
            #endregion

            #region Object max bandwidth change
            Obj_max_bandchg_tb.Visibility = Visibility.Visible;


            Obj_max_bandchg_ttb.Visibility = Visibility.Visible;
            #endregion
        }

        private Button Attach_lut_bt = new Button();

        public void Lookup_table_setup()
        {
            Attach_lut_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "Attach Lookup Table Place Holder",
                Foreground = white_button_brush,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(1, 1, 1, 1),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 14
            };
            ParentGrid.Children.Add(Attach_lut_bt);
            Attach_lut_bt.SetValue(Grid.ColumnProperty, 100);
            Attach_lut_bt.SetValue(Grid.ColumnSpanProperty, 40);
            Attach_lut_bt.SetValue(Grid.RowProperty, 70);
            Attach_lut_bt.SetValue(Grid.RowSpanProperty, 10);
            //Attach_lut_bt.Click += Antenna_Pol_Property_bt_Click;
        }
        public void Lookup_table_hide()
        {
            Attach_lut_bt.Visibility = Visibility.Collapsed;
        }
        public void Lookup_table_show()
        {
            Attach_lut_bt.Visibility = Visibility.Visible;
        }
        public void Transmitter_setup()
        {
            TRF_bt_decolor();
            Dic_bt_drbet = new Dictionary<Button, DRBE_Transmitter>();

            int i = 0;
            i = 0;
            while (i < Transmitter_btl.Count)
            {
                if(ParentGrid.Children.Contains(Transmitter_btl[i]))
                {
                    ParentGrid.Children.Remove(Transmitter_btl[i]);
                }
                i++;
            }
            Transmitter_btl = new List<Button>();
            i = 0;
            while(i< Sc_transmitter_list.Count)
            {
                Transmitter_btl.Add(new Button() {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    Background = Default_back_black_color_brush,
                    Content = "Tran:  " + Sc_transmitter_list[i].ID.ToString(),
                    Foreground = white_button_brush,
                    FontWeight = FontWeights.Bold,
                    BorderBrush = white_button_brush,
                    BorderThickness = new Thickness(2,2,2,2),
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
            Read_from_DRBEt(Dic_bt_drbet[Transmitter_btl[Transmitter_btl.Count - 1]]);
            if (Dic_bt_drbet.ContainsKey(Global_temp_bt))
            {
                Write_to_DRBEt(Dic_bt_drbet[Global_temp_bt]);
            }
            else if (Dic_bt_drbef.ContainsKey(Global_temp_bt))
            {
                Write_to_DRBEf(Dic_bt_drbef[Global_temp_bt]);
            }
            else if (Dic_bt_drber.ContainsKey(Global_temp_bt))
            {
                Write_to_DRBEr(Dic_bt_drber[Global_temp_bt]);
            }
            else
            {

            }
            Object_ID_tb.Text = "Object ID:  " + Dic_bt_drbet[Transmitter_btl[Transmitter_btl.Count - 1]].ID.ToString();
            Global_temp_bt = Transmitter_btl[Transmitter_btl.Count - 1];
        }

        private void TRF_bt_decolor()
        {
            int i = 0;
            i = 0;
            while(i < Transmitter_btl.Count)
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
            if (Dic_bt_drbet.ContainsKey(Global_temp_bt))
            {
                Write_to_DRBEt(Dic_bt_drbet[Global_temp_bt]);
            }else if(Dic_bt_drbef.ContainsKey(Global_temp_bt))
            {
                Write_to_DRBEf(Dic_bt_drbef[Global_temp_bt]);
            }
            else if (Dic_bt_drber.ContainsKey(Global_temp_bt))
            {
                Write_to_DRBEr(Dic_bt_drber[Global_temp_bt]);
            }
            else
            {

            }
            Read_from_DRBEt(Dic_bt_drbet[foo]);
            Object_ID_tb.Text = "Object ID:  " + Dic_bt_drbet[foo].ID.ToString();
            Global_temp_bt = foo;
            await Detect_object_file();
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
            Read_from_DRBEf(Dic_bt_drbef[Reflector_btl[Reflector_btl.Count - 1]]);
            if (Dic_bt_drbet.ContainsKey(Global_temp_bt))
            {
                Write_to_DRBEt(Dic_bt_drbet[Global_temp_bt]);
            }
            else if (Dic_bt_drbef.ContainsKey(Global_temp_bt))
            {
                Write_to_DRBEf(Dic_bt_drbef[Global_temp_bt]);
            }
            else if (Dic_bt_drber.ContainsKey(Global_temp_bt))
            {
                Write_to_DRBEr(Dic_bt_drber[Global_temp_bt]);
            }
            else
            {

            }
            Object_ID_tb.Text = "Object ID:  " + Dic_bt_drbef[Reflector_btl[Reflector_btl.Count - 1]].ID.ToString();
            Global_temp_bt = Reflector_btl[Reflector_btl.Count - 1];
        }



        private async void Reflector_bt_Click(object sender, RoutedEventArgs e)
        {
            Button foo = sender as Button;
            TRF_bt_decolor();
            foo.BorderBrush = green_bright_button_brush;
            if (Dic_bt_drbet.ContainsKey(Global_temp_bt))
            {
                Write_to_DRBEt(Dic_bt_drbet[Global_temp_bt]);
            }
            else if (Dic_bt_drbef.ContainsKey(Global_temp_bt))
            {
                Write_to_DRBEf(Dic_bt_drbef[Global_temp_bt]);
            }
            else if (Dic_bt_drber.ContainsKey(Global_temp_bt))
            {
                Write_to_DRBEr(Dic_bt_drber[Global_temp_bt]);
            }
            else
            {

            }
            Read_from_DRBEf(Dic_bt_drbef[foo]);
            Object_ID_tb.Text = "Object ID:  " + Dic_bt_drbef[foo].ID.ToString();
            Global_temp_bt = foo;
            await Detect_object_file();
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
            Read_from_DRBEr(Dic_bt_drber[Receiver_btl[Receiver_btl.Count - 1]]);
            if (Dic_bt_drbet.ContainsKey(Global_temp_bt))
            {
                Write_to_DRBEt(Dic_bt_drbet[Global_temp_bt]);
            }
            else if (Dic_bt_drbef.ContainsKey(Global_temp_bt))
            {
                Write_to_DRBEf(Dic_bt_drbef[Global_temp_bt]);
            }
            else if (Dic_bt_drber.ContainsKey(Global_temp_bt))
            {
                Write_to_DRBEr(Dic_bt_drber[Global_temp_bt]);
            }
            else
            {

            }
            Object_ID_tb.Text = "Object ID:  " + Dic_bt_drber[Receiver_btl[Receiver_btl.Count - 1]].ID.ToString();
            Global_temp_bt = Receiver_btl[Receiver_btl.Count - 1];
        }

        private async void Receiver_bt_Click(object sender, RoutedEventArgs e)
        {
            Button foo = sender as Button;
            TRF_bt_decolor();
            foo.BorderBrush = green_bright_button_brush;
            if (Dic_bt_drbet.ContainsKey(Global_temp_bt))
            {
                Write_to_DRBEt(Dic_bt_drbet[Global_temp_bt]);
            }
            else if (Dic_bt_drbef.ContainsKey(Global_temp_bt))
            {
                Write_to_DRBEf(Dic_bt_drbef[Global_temp_bt]);
            }
            else if (Dic_bt_drber.ContainsKey(Global_temp_bt))
            {
                Write_to_DRBEr(Dic_bt_drber[Global_temp_bt]);
            }
            else
            {

            }
            Read_from_DRBEr(Dic_bt_drber[foo]);
            Object_ID_tb.Text = "Object ID:  " + Dic_bt_drber[foo].ID.ToString();
            Global_temp_bt = foo;
            await Detect_object_file();
        }







        public List<DRBE_Transmitter> Sc_transmitter_list = new List<DRBE_Transmitter>();
        public List<DRBE_Receiver> Sc_receiver_list = new List<DRBE_Receiver>();
        public List<DRBE_Reflector> Sc_reflector_list = new List<DRBE_Reflector>();
        #region Tool
        private async Task Generate_all_obj()
        {
            int flag = 0;
            int i = 0;
            flag = await ConfirmDialog("Generate new random scenario", "All of previous defined object will be overwritten by new object with random properties", "Confirm", "Cancel");
            if(flag==1)
            {
                Sc_transmitter_list = new List<DRBE_Transmitter>();
                Sc_receiver_list = new List<DRBE_Receiver>();
                Sc_reflector_list = new List<DRBE_Reflector>();

                Number_of_Transmitter = S_I(Number_of_transmitter_tb.Text);
                Number_of_Reflector = S_I(Number_of_reflector_tb.Text);
                Number_of_Receiver = S_I(Number_of_receiver_tb.Text);

                i = 0;
                while(i< Number_of_Transmitter)
                {
                    Random_drbet();
                    i++;
                }
                i = 0;
                while (i < Number_of_Reflector)
                {
                    Random_drbef();
                    i++;
                }
                i = 0;
                while (i < Number_of_Receiver)
                {
                    Random_drber();
                    i++;
                }




                Transmitter_setup();
                Reflector_setup();
                Receiver_setup();
                return;
            }
            else
            {
                return;
            }
        }
        private void Sc_add_transmitter(List<string> x)
        {
            int i = 0;

            Sc_transmitter_list.Add(new DRBE_Transmitter());
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Set_default_1();

            Sc_transmitter_list[Sc_transmitter_list.Count - 1].ID = (ushort)S_D(x[0]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Initial_Position_X = S_D(x[2]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Initial_Position_Y = S_D(x[3]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Initial_Position_Z = S_D(x[4]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Initial_Latitude = S_D(x[5]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Initial_Longtitude = S_D(x[6]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Initial_Sc_posX = S_D(x[7]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Initial_Sc_posY = S_D(x[8]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Initial_Sc_posZ = S_D(x[9]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Reference_Obj_ID = (ushort)S_D(x[10]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Initial_Velocity_X = S_D(x[11]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Initial_Velocity_Y = S_D(x[12]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Initial_Velocity_Z = S_D(x[13]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Initial_Acceleration_X = S_D(x[14]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Initial_Acceleration_Y = S_D(x[15]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Initial_Acceleration_Z = S_D(x[16]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Initial_Orientation_X = S_D(x[17]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Initial_Orientation_Y = S_D(x[18]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Initial_Orientation_Z = S_D(x[19]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Initial_Eangle_AZ = S_D(x[20]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Initial_Eangle_EL = S_D(x[21]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Initial_Mangle_AZ = S_D(x[22]);
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].Initial_Mangle_EL = S_D(x[23]);

            if (ID_index<=S_I(x[0]))
            {
                ID_index = S_I(x[0]) + 1;
            }
            //Transmitter_setup();
        }

        private void Sc_add_reflector(List<string> x)
        {
            int i = 0;

            Sc_reflector_list.Add(new DRBE_Reflector());
            Sc_reflector_list[Sc_reflector_list.Count - 1].Set_default_1();

            Sc_reflector_list[Sc_reflector_list.Count - 1].ID = (ushort)S_D(x[0]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Initial_Position_X = S_D(x[2]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Initial_Position_X = S_D(x[3]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Initial_Position_X = S_D(x[4]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Initial_Latitude = S_D(x[5]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Initial_Longtitude = S_D(x[6]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Initial_Sc_posX = S_D(x[7]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Initial_Sc_posY = S_D(x[8]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Initial_Sc_posZ = S_D(x[9]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Reference_Obj_ID = (ushort)S_D(x[10]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Initial_Velocity_X = S_D(x[11]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Initial_Velocity_Y = S_D(x[12]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Initial_Velocity_Z = S_D(x[13]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Initial_Acceleration_X = S_D(x[14]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Initial_Acceleration_Y = S_D(x[15]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Initial_Acceleration_Z = S_D(x[16]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Initial_Orientation_X = S_D(x[17]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Initial_Orientation_Y = S_D(x[18]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Initial_Orientation_Z = S_D(x[19]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Initial_Eangle_AZ = S_D(x[20]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Initial_Eangle_EL = S_D(x[21]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Initial_Mangle_AZ = S_D(x[22]);
            Sc_reflector_list[Sc_reflector_list.Count - 1].Initial_Mangle_EL = S_D(x[23]);

            if (ID_index < S_I(x[0]))
            {
                ID_index = S_I(x[0]) + 1;
            }
            //Transmitter_setup();
        }

        private void Sc_add_receiver(List<string> x)
        {
            int i = 0;

            Sc_receiver_list.Add(new DRBE_Receiver());
            Sc_receiver_list[Sc_receiver_list.Count - 1].Set_default_1();

            Sc_receiver_list[Sc_receiver_list.Count - 1].ID = (ushort)S_D(x[0]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Initial_Position_X = S_D(x[2]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Initial_Position_X = S_D(x[3]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Initial_Position_X = S_D(x[4]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Initial_Latitude = S_D(x[5]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Initial_Longtitude = S_D(x[6]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Initial_Sc_posX = S_D(x[7]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Initial_Sc_posY = S_D(x[8]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Initial_Sc_posZ = S_D(x[9]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Reference_Obj_ID = (ushort)S_D(x[10]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Initial_Velocity_X = S_D(x[11]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Initial_Velocity_Y = S_D(x[12]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Initial_Velocity_Z = S_D(x[13]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Initial_Acceleration_X = S_D(x[14]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Initial_Acceleration_Y = S_D(x[15]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Initial_Acceleration_Z = S_D(x[16]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Initial_Orientation_X = S_D(x[17]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Initial_Orientation_Y = S_D(x[18]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Initial_Orientation_Z = S_D(x[19]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Initial_Eangle_AZ = S_D(x[20]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Initial_Eangle_EL = S_D(x[21]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Initial_Mangle_AZ = S_D(x[22]);
            Sc_receiver_list[Sc_receiver_list.Count - 1].Initial_Mangle_EL = S_D(x[23]);

            if (ID_index < S_I(x[0]))
            {
                ID_index = S_I(x[0]) + 1;
            }
            //Transmitter_setup();
        }
        private void Generate_all_transmitter()
        {

        }
        private void Add_transmitter()
        {
            Sc_transmitter_list.Add(new DRBE_Transmitter());
            Sc_transmitter_list[Sc_transmitter_list.Count-1].Set_default_1();
            Sc_transmitter_list[Sc_transmitter_list.Count - 1].ID = (ushort)ID_index;
            ID_index++;
            Transmitter_setup();
        }

        private void Add_reflector()
        {
            //Random_drbef();
            Sc_reflector_list.Add(new DRBE_Reflector());
            Sc_reflector_list[Sc_reflector_list.Count - 1].Set_default_1();
            Sc_reflector_list[Sc_reflector_list.Count - 1].ID = (ushort)ID_index;
            ID_index++;
            Reflector_setup();
        }

        private void Add_receiver()
        {
            //Random_drber();
            Sc_receiver_list.Add(new DRBE_Receiver());
            Sc_receiver_list[Sc_receiver_list.Count - 1].Set_default_1();
            Sc_receiver_list[Sc_receiver_list.Count - 1].ID = (ushort)ID_index;
            ID_index++;

            Receiver_setup();
        }
        private void Delete_all_bt()
        {

        }
        private void Attach_all_bt()
        {

        }

        private DRBE_Transmitter Random_drbet()
        {
            DRBE_Transmitter result = new DRBE_Transmitter();

            //result.Initial_Position_X = GetRandomNumber();




            Sc_transmitter_list.Add(result);
            return result;
        }

        private DRBE_Receiver Random_drber()
        {
            DRBE_Receiver result = new DRBE_Receiver();
            Sc_receiver_list.Add(result);
            return result;
        }

        private DRBE_Reflector Random_drbef()
        {
            DRBE_Reflector result = new DRBE_Reflector();
            



            Sc_reflector_list.Add(result);
            return result;
        }

        private void Delete_obj()
        {

        }


        #endregion


        private async Task<List<double>> DRBE_Generate_random_distance(double distance, double lat, double lon, double height, double eheight)
        {
            List<double> result = new List<double>();

            if (distance <= 0)
            {
                await ShowDialog("Error", "Distance <= 0");
                return result;
            }

            double tempon = GetRandomNumber(-180, 180);
            double tempat = GetRandomNumber(-180, 180);
            tempon = 180;
            tempat = 180;
            int i = 0;
            i = 0;
            double temp = 0;
            while (true)
            {
                temp = DRBE_ATON_Distance(tempat, tempon, eheight, lat, lon, height);
                if (temp > distance)
                {
                    if ((lat - tempat) != 0)
                    {
                        tempat = tempat + (lat - tempat) / Math.Abs(lat - tempat) / 20;
                    }
                    if ((lon - tempon)!= 0)
                    {
                        tempon = tempon + (lon - tempon) / Math.Abs(lon - tempon) / 20;
                    }
                    if((eheight - height)!= 0)
                    {
                        eheight = eheight + (height - eheight) / Math.Abs(height - eheight) / 20;
                    }

                    if( (i % 300) == 0)
                    {
                        await ShowDialog("Result", tempat.ToString() + "  ,  " + tempon.ToString() + "  ,  " + eheight.ToString() + " , " + temp.ToString());
                    }

                }
                else
                {
                    result.Add(tempat);
                    result.Add(tempon);
                    result.Add(eheight);
                    await ShowDialog("Result", tempat.ToString() + "  ,  " + tempon.ToString() + "  ,  " + eheight.ToString());
                    return result;
                }
                i++;
            }

            return result;
        }
        private async Task<List<double>> Generate_random_distance(double distance, double lat, double lon, double height)
        {
            List<double> result = new List<double>();

            if(distance <= 0)
            {
                await ShowDialog("Error", "Distance <= 0");
                return result;
            }

            double tempon = GetRandomNumber(-90, 90);
            double tempat = GetRandomNumber(-180, 180);
            double temph = height * height;

            double temp = 0;
            while(true)
            {
                temp = getDistanceFromLatLonInKm(lat,lon,tempat,tempon);
                temp = Math.Sqrt(temph + temp * temp); 
                if (temp > distance)
                {
                    tempat = tempat + (lat - tempat) / Math.Abs(lat - tempat)/20;
                    tempon = tempon + (lon - tempon) / Math.Abs(lon - tempon)/20;
                }
                else
                {
                    result.Add(tempat);
                    result.Add(tempon);
                    await ShowDialog("Result", tempat.ToString() + "  ,  " + tempon.ToString());
                    return result;
                }
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
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = x,
                Content = y,
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
            double N = Req / Math.Sqrt(1 - ee * Math.Sin(lat/180 * Math.PI) * Math.Sin(lat / 180 * Math.PI));
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
            double dLat = (lat2 - lat1)/180 * Math.PI;  // deg2rad below
            double dLon = (lon2 - lon1)/180 * Math.PI;
            var a =
              Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(lat1/180 * Math.PI) * Math.Cos(lat2/180 * Math.PI) *
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
