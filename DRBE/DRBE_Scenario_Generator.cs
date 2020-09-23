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
using System.Data;
using Windows.UI.Xaml.Automation.Peers;
using System.Runtime.CompilerServices;
using Windows.AI.MachineLearning;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Gaming.Input.Custom;

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
        //public DRBE_Link_Viewer_s SC_Dlv;
        //public DRBE_LinkViewer Link_Viewer;


        public DRBE_Scenario_Generator(Grid parent, MainPage parentpage)
        {
            ParentGrid = parent;
            ParentPage = parentpage;
            //hide();
            DRBE_SS = new Save_Screen(parent);
            //Link_Viewer = new DRBE_LinkViewer(parent, ParentPage);
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

        private ScrollViewer DRBE_Obj_SV = new ScrollViewer();
        private List<StackPanel> DRBE_Obj_SPL = new List<StackPanel>();
        private Grid DRBE_Obj_GD = new Grid();

        private List<Button> Obj_bt_list = new List<Button>();
        private List<StackPanel> Obj_sp_list = new List<StackPanel>();
        private List<Image> Obj_img_list = new List<Image>();
        private List<TextBlock> Obj_tb_list = new List<TextBlock>();
        private List<Grid> Obj_gd_list = new List<Grid>();

        private ScrollViewer DRBE_grp_SV = new ScrollViewer();
        private List<StackPanel> DRBE_grp_SP = new List<StackPanel>();
        private Grid DRBE_grp_GD = new Grid();
        

        private Button Add_grp_bt = new Button();
        private StackPanel Add_grp_sp = new StackPanel();
        private Image Add_grp_img = new Image();
        private TextBlock Add_grp_tb = new TextBlock();
        private Grid Add_grp_gd = new Grid();

        private TextBlock Home_tb = new TextBlock();
        private Button Home_bt = new Button();
        private Image Home_bti = new Image();

        private TextBlock Mode_obj_tb = new TextBlock();
        private Button Mode_obj_bt = new Button();
        private Image Mode_obj_bti = new Image();

        private TextBlock Mode_grp_tb = new TextBlock();
        private Button Mode_grp_bt = new Button();
        private Image Mode_grp_bti = new Image();

        private TextBlock List_view_tb = new TextBlock();
        private Button List_view_bt = new Button();
        private Image List_view_bti = new Image();

        private TextBlock Unity_view_tb = new TextBlock();
        private Button Unity_view_bt = new Button();
        private Image Unity_view_bti = new Image();

        private int Mode_flag = 0;
        public void Setup()
        {
            int i = 0;

            #region Home


            Home_bti.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Home_icon.png", UriKind.RelativeOrAbsolute));
            Home_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Home_bti,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Home_bt);
            Home_bt.SetValue(Grid.ColumnProperty, 70);
            Home_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Home_bt.SetValue(Grid.RowProperty, 5);
            Home_bt.SetValue(Grid.RowSpanProperty, 10);
            Home_bt.Click += Home_bt_Click; 
            #endregion

            #region Mode obj


            Mode_obj_bti.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Object_icon.png", UriKind.RelativeOrAbsolute));
            Mode_obj_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = green_button_brush,
                Content = Mode_obj_bti,
                FontSize = 18
            };
            ParentGrid.Children.Add(Mode_obj_bt);
            Mode_obj_bt.SetValue(Grid.ColumnProperty, 85);
            Mode_obj_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Mode_obj_bt.SetValue(Grid.RowProperty, 5);
            Mode_obj_bt.SetValue(Grid.RowSpanProperty, 10);
            Mode_obj_bt.Click += Mode_obj_bt_Click; ;

            Mode_flag = 0;
            #endregion

            #region Mode grp


            Mode_grp_bti.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Aircraft_group_icon.png", UriKind.RelativeOrAbsolute));
            Mode_grp_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Mode_grp_bti,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Mode_grp_bt);
            Mode_grp_bt.SetValue(Grid.ColumnProperty, 100);
            Mode_grp_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Mode_grp_bt.SetValue(Grid.RowProperty, 5);
            Mode_grp_bt.SetValue(Grid.RowSpanProperty, 10);
            Mode_grp_bt.Click += Mode_grp_bt_Click; ;
            #endregion

            #region List view page
            List_view_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Object List",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(List_view_tb);
            List_view_tb.SetValue(Grid.ColumnProperty, 130);
            List_view_tb.SetValue(Grid.ColumnSpanProperty, 15);
            List_view_tb.SetValue(Grid.RowProperty, 0);
            List_view_tb.SetValue(Grid.RowSpanProperty, 5);

            List_view_bti.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/List_icon.png", UriKind.RelativeOrAbsolute));
            List_view_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = green_button_brush,
                Content = List_view_bti,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(List_view_bt);
            List_view_bt.SetValue(Grid.ColumnProperty, 130);
            List_view_bt.SetValue(Grid.ColumnSpanProperty, 15);
            List_view_bt.SetValue(Grid.RowProperty, 5);
            List_view_bt.SetValue(Grid.RowSpanProperty, 10);
            List_view_bt.Click += List_view_bt_Click;
            #endregion
            #region List view page
            Unity_view_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "3D Virtual",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Unity_view_tb);
            Unity_view_tb.SetValue(Grid.ColumnProperty, 115);
            Unity_view_tb.SetValue(Grid.ColumnSpanProperty, 15);
            Unity_view_tb.SetValue(Grid.RowProperty, 0);
            Unity_view_tb.SetValue(Grid.RowSpanProperty, 5);

            Unity_view_bti.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Scenario_icon.jpg", UriKind.RelativeOrAbsolute));
            Unity_view_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Unity_view_bti,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Unity_view_bt);
            Unity_view_bt.SetValue(Grid.ColumnProperty, 115);
            Unity_view_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Unity_view_bt.SetValue(Grid.RowProperty, 5);
            Unity_view_bt.SetValue(Grid.RowSpanProperty, 10);
            Unity_view_bt.Click += Unity_view_bt_Click;
            #endregion

            #region LV page
            Lv_view_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Link Model",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Lv_view_tb);
            Lv_view_tb.SetValue(Grid.ColumnProperty, 145);
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
            Lv_view_bt.SetValue(Grid.ColumnProperty, 145);
            Lv_view_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Lv_view_bt.SetValue(Grid.RowProperty, 5);
            Lv_view_bt.SetValue(Grid.RowSpanProperty, 10);
            Lv_view_bt.Click += Lv_view_bt_Click;
            #endregion

            #region group panel
            DRBE_grp_GD = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush

            };
            DRBE_grp_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            DRBE_grp_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            DRBE_grp_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            DRBE_grp_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            DRBE_grp_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            DRBE_grp_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            DRBE_grp_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            DRBE_grp_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            DRBE_grp_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            DRBE_grp_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            DRBE_grp_SV = new ScrollViewer()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                VerticalScrollMode = ScrollMode.Enabled,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            };
            ParentGrid.Children.Add(DRBE_grp_SV);
            DRBE_grp_SV.SetValue(Grid.ColumnProperty, 1);
            DRBE_grp_SV.SetValue(Grid.ColumnSpanProperty, 154);
            DRBE_grp_SV.SetValue(Grid.RowProperty, 110);
            DRBE_grp_SV.SetValue(Grid.RowSpanProperty, 40);

            i = 0;
            while(i<10)
            {
                DRBE_grp_SP.Add(new StackPanel()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Background = Default_back_black_color_brush
                });
                DRBE_grp_GD.Children.Add(DRBE_grp_SP[DRBE_grp_SP.Count - 1]);
                DRBE_grp_SP[DRBE_grp_SP.Count - 1].SetValue(Grid.ColumnProperty, i);
                DRBE_grp_SP[DRBE_grp_SP.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);
                i++;
            }
            DRBE_grp_SV.Content = DRBE_grp_GD;


            #region grp bt
            Add_grp_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush
            };
            Add_grp_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            Add_grp_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });
            Add_grp_img = new Image()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Add_Page.png", UriKind.RelativeOrAbsolute)),
                Height = 20
            };
            Add_grp_img.SetValue(Grid.ColumnProperty, 0);
            Add_grp_img.SetValue(Grid.ColumnSpanProperty, 1);
            //Add_grp_img.SetValue(Grid.RowSpanProperty, 1);

            Add_grp_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Text = "Add New Group",
                Foreground = white_button_brush
            };
            Add_grp_tb.SetValue(Grid.ColumnProperty, 1);
            Add_grp_tb.SetValue(Grid.ColumnSpanProperty, 1);
            

            Add_grp_sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Height = 40,
                Background = Default_back_black_color_brush
            };
            Add_grp_sp.Children.Add(Add_grp_gd);
            Add_grp_gd.Children.Add(Add_grp_img);
            Add_grp_gd.Children.Add(Add_grp_tb);
            Add_grp_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height =40,
                Content = Add_grp_sp,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(0.5,0.5,0.5,0.5)
            };
            DRBE_grp_SP[0].Children.Add(Add_grp_bt);
            Add_grp_bt.Click += Add_grp_bt_Click;

            #endregion
            #endregion

            #region object panel
            DRBE_Obj_GD = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush

            };
            DRBE_Obj_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            DRBE_Obj_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            DRBE_Obj_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            DRBE_Obj_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            DRBE_Obj_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            DRBE_Obj_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            DRBE_Obj_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            DRBE_Obj_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            DRBE_Obj_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            DRBE_Obj_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            DRBE_Obj_SV = new ScrollViewer()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                VerticalScrollMode = ScrollMode.Enabled,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            };
            ParentGrid.Children.Add(DRBE_Obj_SV);
            DRBE_Obj_SV.SetValue(Grid.ColumnProperty, 1);
            DRBE_Obj_SV.SetValue(Grid.ColumnSpanProperty, 154);
            DRBE_Obj_SV.SetValue(Grid.RowProperty, 25);
            DRBE_Obj_SV.SetValue(Grid.RowSpanProperty, 83);

            DRBE_Obj_SV.Content = DRBE_Obj_GD;
            i = 0;
            while(i<10)
            {
                DRBE_Obj_SPL.Add(new StackPanel()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Background = Default_back_black_color_brush,
                    BorderBrush = dark_grey_brush,
                    BorderThickness = new Thickness(0, 0, 0, 0.5)
                });
                DRBE_Obj_GD.Children.Add(DRBE_Obj_SPL[DRBE_Obj_SPL.Count - 1]);
                DRBE_Obj_SPL[DRBE_Obj_SPL.Count - 1].SetValue(Grid.ColumnProperty, i);
                DRBE_Obj_SPL[DRBE_Obj_SPL.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);
                i++;
            }

            #endregion

            #region property panel
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
            #endregion


            #region generate
            Generate_obj_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Add Object",
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
                FontSize = 10,
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
                FontSize = 15,
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
                FontSize = 10,
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
                FontSize = 10,
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
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 10
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
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 10
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
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 10
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
            //Antenna_Pol_Property_setup();
            //Antenna_Pol_Property_hide();
            Antenna_Pol_Property_setup_s();
            Antenna_Pol_Property_hide_s();
            //RCS_Property_setup();
            //RCS_Property_hide();
            RCS_Property_setup_s();
            RCS_Property_hide_s();
            //RF_Clut_Property_setup();
            //RF_Clut_Property_hide();
            RF_Clut_Property_setup_s();
            RF_Clut_Property_hide_s();
            Constraint_Property_setup_s();
            Constraint_Property_hide_s();
            Lookup_table_setup_s();
            Lookup_table_hide_s();
            Property_tab_decolor();
            Initial_Property_bt.BorderBrush = green_bright_button_brush;
            Hide_all_Property();
            Initial_Property_show_s();

            Detect_object_file();
            Detect_scenario_file();

            Create_group_panel_setup();
            Create_group_panel_hide();

            Group_panel_setup();
            Group_panel_hide();

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


            #region hardcode
            //Sc_centerX_ttb.Visibility = Visibility.Visible;

            //Generate_default_objs(10);
            //Generate_object_bt();
            //DRBE_grp_list.Add(new DRBE_Group());
            //DRBE_grp_list.Add(new DRBE_Group());
            //DRBE_grp_list.Add(new DRBE_Group());
            //DRBE_grp_list.Add(new DRBE_Group());
            //DRBE_grp_list.Add(new DRBE_Group());
            //DRBE_grp_list.Add(new DRBE_Group());
            //DRBE_grp_list.Add(new DRBE_Group());
            //DRBE_grp_list.Add(new DRBE_Group());
            //DRBE_grp_list.Add(new DRBE_Group());
            //DRBE_grp_list.Add(new DRBE_Group());
            //DRBE_grp_list.Add(new DRBE_Group());
            //Create_grp_button();
            //Temp_grp_bt = Group_bt_list[0];

            ////Grp_obj_bt_fetch(DRBE_obj_list[Dic_bt_obj_int[Temp_obj_bt]]);
            //Group_panel_refresh();

            //Group_generate_link_list();

            //Group_trans_link_list[0][1] = 3;
            //Group_receive_link_list[0][1] = 3;

            //Group_trans_link_list[1][0] = 3;
            //Group_receive_link_list[1][0] = 3;

            //Group_trans_link_list[2][1] = 3;
            //Group_receive_link_list[2][1] = 3;

            //Group_trans_link_list[2][0] = 3;
            //Group_receive_link_list[2][0] = 3;


            //Dic_objs_grp[DRBE_obj_list[0]] = new List<DRBE_Group>();
            //Dic_objs_grp[DRBE_obj_list[1]] = new List<DRBE_Group>();
            //Dic_objs_grp[DRBE_obj_list[2]] = new List<DRBE_Group>();
            //Dic_objs_grp[DRBE_obj_list[3]] = new List<DRBE_Group>();
            //Dic_objs_grp[DRBE_obj_list[4]] = new List<DRBE_Group>();
            //Dic_objs_grp[DRBE_obj_list[5]] = new List<DRBE_Group>();
            //Dic_objs_grp[DRBE_obj_list[6]] = new List<DRBE_Group>();
            //Dic_objs_grp[DRBE_obj_list[7]] = new List<DRBE_Group>();


            //Dic_grp_objs[DRBE_grp_list[0]] = new List<DRBE_Objs>();
            //Dic_grp_objs[DRBE_grp_list[1]] = new List<DRBE_Objs>();
            //Dic_grp_objs[DRBE_grp_list[2]] = new List<DRBE_Objs>();

            //Dic_grp_objs[DRBE_grp_list[0]].Add(DRBE_obj_list[0]);
            //Dic_grp_objs[DRBE_grp_list[0]].Add(DRBE_obj_list[1]);

            //Dic_grp_objs[DRBE_grp_list[1]].Add(DRBE_obj_list[2]);
            //Dic_grp_objs[DRBE_grp_list[1]].Add(DRBE_obj_list[3]);

            //Dic_grp_objs[DRBE_grp_list[2]].Add(DRBE_obj_list[4]);
            //Dic_grp_objs[DRBE_grp_list[2]].Add(DRBE_obj_list[5]);

            //Dic_grp_objs[DRBE_grp_list[0]].Add(DRBE_obj_list[6]);
            //Dic_grp_objs[DRBE_grp_list[0]].Add(DRBE_obj_list[7]);

            //Dic_objs_grp[DRBE_obj_list[0]].Add(DRBE_grp_list[0]);
            //Dic_objs_grp[DRBE_obj_list[1]].Add(DRBE_grp_list[0]);

            //Dic_objs_grp[DRBE_obj_list[2]].Add(DRBE_grp_list[1]);
            //Dic_objs_grp[DRBE_obj_list[3]].Add(DRBE_grp_list[1]);

            //Dic_objs_grp[DRBE_obj_list[4]].Add(DRBE_grp_list[2]);
            //Dic_objs_grp[DRBE_obj_list[5]].Add(DRBE_grp_list[2]);

            //Dic_objs_grp[DRBE_obj_list[6]].Add(DRBE_grp_list[0]);
            //Dic_objs_grp[DRBE_obj_list[7]].Add(DRBE_grp_list[0]);

            //DRBE_obj_list[2].RCS_point = 50;
            //DRBE_obj_list[2].RCS_output_time_sampe = 20;
            //DRBE_obj_list[2].RCS_angle_resolution = 0.5;
            //Update_DRBE_objs(Dic_bt_obj_int[Temp_obj_bt]);
            //Grp_obj_bt_fetch(DRBE_obj_list[Dic_bt_obj_int[Temp_obj_bt]]);
            //Process_link_information_create();
            ////hide();
            ////Link_Viewer.Setup(DRBE_obj_list, Dic_LPI_it, Dic_LPI_io, Dic_LPI_ir, Dic_LPI_ti, Dic_LPI_oi, Dic_LPI_ri, Link_table);
            ////Process_link_information_create();
            #endregion
        }

        private async void Unity_view_bt_Click(object sender, RoutedEventArgs e)
        {
            await DRBE_SS.Quiet_Start("Save Scenario", new List<string>() { "Simulator File", "Scenario File" }, "dsc", Generate_scenario_file_s());
            await Task.Delay(50);
            Button foo = sender as Button;

            if(foo.Background == Default_back_black_color_brush)
            {
                List<byte> tosend = new List<byte>();
                int i = 0;
                i = 0;
                tosend.Add(0x06);
                tosend.Add(0x00);
                tosend.Add(0x05);
                tosend.Add(0x01);
                tosend.Add(0x00);

                ParentPage.UWbinarywriter.Write(tosend.ToArray(), 0, tosend.Count);
                ParentPage.UWbinarywriter.Flush();
                foo.Background = green_button_brush;
                List_view_bt.Background = Default_back_black_color_brush;
            }
            else
            {

            }
        }

        private void Home_bt_Click(object sender, RoutedEventArgs e)
        {
            hide();
            ParentPage.DRBE_mainpage1.Show();
        }
        
        private void List_view_bt_Click(object sender, RoutedEventArgs e)
        {
            Button foo = sender as Button;

            if (foo.Background == Default_back_black_color_brush)
            {
                List<byte> tosend = new List<byte>();
                int i = 0;
                i = 0;
                tosend.Add(0x06);
                tosend.Add(0x00);
                tosend.Add(0x05);
                tosend.Add(0x02);
                tosend.Add(0x00);

                ParentPage.UWbinarywriter.Write(tosend.ToArray(), 0, tosend.Count);
                ParentPage.UWbinarywriter.Flush();
                foo.Background = green_button_brush;
                Unity_view_bt.Background = Default_back_black_color_brush;
            }
            else
            {
            }
        }

        private List<List<List<bool>>> Link_table = new List<List<List<bool>>>();

        private Dictionary<DRBE_Objs, int> Dic_LPI_ti = new Dictionary<DRBE_Objs, int>();
        private Dictionary<DRBE_Objs, int> Dic_LPI_oi = new Dictionary<DRBE_Objs, int>();
        private Dictionary<DRBE_Objs, int> Dic_LPI_ri = new Dictionary<DRBE_Objs, int>();

        private Dictionary<int, DRBE_Objs> Dic_LPI_it = new Dictionary<int, DRBE_Objs>();
        private Dictionary<int, DRBE_Objs> Dic_LPI_io = new Dictionary<int, DRBE_Objs>();
        private Dictionary<int, DRBE_Objs> Dic_LPI_ir = new Dictionary<int, DRBE_Objs>();

        private int DRBE_t_count = 0;
        private int DRBE_o_count = 0;
        private int DRBE_r_count = 0;
        private async Task Process_link_information_create()
        {
            string temp = "";
            Dic_LPI_ti = new Dictionary<DRBE_Objs, int>();
            Dic_LPI_oi = new Dictionary<DRBE_Objs, int>();
            Dic_LPI_ri = new Dictionary<DRBE_Objs, int>();

            Dic_LPI_it = new Dictionary<int, DRBE_Objs>();
            Dic_LPI_io = new Dictionary<int, DRBE_Objs>();
            Dic_LPI_ir = new Dictionary<int, DRBE_Objs>();
            int i = 0;
            int ii = 0;
            int iii = 0;
            i = 0;
            DRBE_t_count = 0;
            DRBE_o_count = 0;
            DRBE_r_count = 0;
            while (i<DRBE_obj_list.Count)
            {
                if(DRBE_obj_list[i].Is_Receiver==true)
                {
                    Dic_LPI_ri[DRBE_obj_list[i]] = DRBE_r_count;
                    Dic_LPI_ir[DRBE_r_count] = DRBE_obj_list[i];
                    DRBE_r_count++;
                }
                if (DRBE_obj_list[i].Is_Transmitter == true)
                {
                    Dic_LPI_ti[DRBE_obj_list[i]] = DRBE_t_count;
                    Dic_LPI_it[DRBE_t_count] = DRBE_obj_list[i];
                    DRBE_t_count++;
                }

                if (DRBE_obj_list[i].Is_Reflector == true)
                {
                    Dic_LPI_oi[DRBE_obj_list[i]] = DRBE_o_count;
                    Dic_LPI_io[DRBE_o_count] = DRBE_obj_list[i];
                    DRBE_o_count++;
                }
                i++;
            }
            Link_table = new List<List<List<bool>>>();
            i = 0;
            while(i<DRBE_t_count)
            {
                Link_table.Add(new List<List<bool>>());
                ii = 0;
                while(ii<DRBE_o_count)
                {
                    Link_table[i].Add(new List<bool>());
                    iii = 0;
                    while(iii<DRBE_r_count)
                    {
                        Link_table[i][ii].Add(false);
                        iii++;
                    }
                    ii++;
                }
                i++;
            }
            int lent = Group_trans_link_list.Count;
            int lenr = Group_receive_link_list.Count;
            int oi = 0;
            int oii = 0;
            int oiii = 0;
            i = 0;
            while(i<lent)
            {
                ii = 0;
                while(ii<lent)
                {
                    if(Group_trans_link_list[i][ii]>0)
                    {
                        iii = 0;
                        while(iii<lenr)
                        {
                            if(Group_receive_link_list[iii][ii]>0)
                            {
                                oi = 0;
                                while(oi< Dic_grp_objs[DRBE_grp_list[i]].Count)
                                {
                                    oii = 0;
                                    while(oii< Dic_grp_objs[DRBE_grp_list[ii]].Count)
                                    {
                                        oiii = 0;
                                        while(oiii< Dic_grp_objs[DRBE_grp_list[iii]].Count)
                                        {
                                            if(Dic_LPI_ti.ContainsKey(Dic_grp_objs[DRBE_grp_list[i]][oi]) && Dic_LPI_oi.ContainsKey(Dic_grp_objs[DRBE_grp_list[ii]][oii]) && Dic_LPI_ri.ContainsKey(Dic_grp_objs[DRBE_grp_list[iii]][oiii]))
                                            {
                                                if((Dic_grp_objs[DRBE_grp_list[i]][oi]!= Dic_grp_objs[DRBE_grp_list[ii]][oii]) &&(Dic_grp_objs[DRBE_grp_list[iii]][oiii]!= Dic_grp_objs[DRBE_grp_list[ii]][oii]))
                                                {
                                                    Link_table[Dic_LPI_ti[Dic_grp_objs[DRBE_grp_list[i]][oi]]][Dic_LPI_oi[Dic_grp_objs[DRBE_grp_list[ii]][oii]]][Dic_LPI_ri[Dic_grp_objs[DRBE_grp_list[iii]][oiii]]] = true;
                                                }
                                                //temp += Dic_LPI_ti[Dic_grp_objs[DRBE_grp_list[i]][oi]].ToString() + " + " + Dic_LPI_oi[Dic_grp_objs[DRBE_grp_list[ii]][oii]].ToString() + " + " + Dic_LPI_ri[Dic_grp_objs[DRBE_grp_list[iii]][oiii]].ToString() + "\r\n";
                                            }
                                           
                                            oiii++;
                                        }

                                        oii++;
                                    }
                                    oi++;
                                }


                            }
                            iii++;
                        }
                    }
                    ii++;
                }
                i++;
            }
            //await ShowDialog("Here", temp);
        }


        private async Task Process_link_information()
        {
            int i = 0;
            int ii = 0;
            int iii = 0;

            int ni = 0;
            int nii = 0;

            string text = "";
            i = 0;
            while(i<DRBE_grp_list.Count)
            {
                Link_table.Add(new List<List<bool>>());
                ii = 0;
                while(ii< DRBE_grp_list.Count)
                {
                    Link_table[i].Add(new List<bool>());
                    iii = 0;
                    while(iii< DRBE_grp_list.Count)
                    {
                        if(Group_trans_link_list[i][ii]>0 && Group_receive_link_list[ii][iii]>0)
                        {
                            Link_table[i][ii].Add(true);
                            text += i.ToString() + " + " + ii.ToString() + " + " + iii.ToString() + "\r\n";
                        }else
                        {
                            Link_table[i][ii].Add(false);
                        }
                        iii++;
                    }
                    ii++;
                }
                i++;
            }

            await ShowDialog("Here", text);



        }


        private async void Mode_grp_bt_Click(object sender, RoutedEventArgs e)
        {
            if(DRBE_grp_list.Count<1)
            {
                await ShowDialog("Error", "No Group Defined, Please Add Group");
                return;
            }

            Mode_grp_bt.Background = green_button_brush;
            Mode_obj_bt.Background = Default_back_black_color_brush;
            Mode_flag = 1;
            Object_panel_hide();
            Group_panel_show();



            Grp_obj_write_dic();
            Temp_obj_bt.Background = Default_back_black_color_brush;
            Update_DRBE_objs(Dic_bt_obj_int[Temp_obj_bt]);

            Group_button_decolor();

            Temp_grp_bt.Background = green_ready_brush;
            await Grp_obj_show();
            DRBE_group_config_show();
        }

        private void Mode_obj_bt_Click(object sender, RoutedEventArgs e)
        {
            Mode_grp_bt.Background = Default_back_black_color_brush;
            Mode_obj_bt.Background = green_button_brush;
            Mode_flag = 0;
            Object_panel_show();
            Group_panel_hide();
            DRBE_group_config_update();



            DRBE_obj_bt_decolor();
            Group_button_decolor();

            Grp_obj_bt_fetch(DRBE_obj_list[Dic_bt_obj_int[Temp_obj_bt]]);
            Temp_obj_bt.Background = green_ready_brush;
        }

        private void Add_grp_bt_Click(object sender, RoutedEventArgs e)
        {
            Create_group_panel_show();
        }
        #region Generate Group

        private List<Button> Group_bt_list = new List<Button>();
        private List<StackPanel> Group_sp_list = new List<StackPanel>();
        private List<Image> Group_img_list = new List<Image>();
        private List<TextBlock> Group_tb_list = new List<TextBlock>();
        private List<Grid> Group_gd_list = new List<Grid>();

        private Border Create_grp_bd = new Border();
        private TextBox Create_grp_name_tb = new TextBox();
        private TextBlock Create_grp_name_title_tb = new TextBlock();
        private TextBlock Create_grp_title_tb = new TextBlock();
        private Button Create_grp_confirm_bt = new Button();
        private Button Create_grp_cancel_bt = new Button();

        private Dictionary<DRBE_Group, List<DRBE_Objs>> Dic_grp_objs = new Dictionary<DRBE_Group, List<DRBE_Objs>>();
        private Dictionary<DRBE_Objs, List<DRBE_Group>> Dic_objs_grp = new Dictionary<DRBE_Objs, List<DRBE_Group>>();
        private Dictionary<Button, int> Dic_group_bt_int = new Dictionary<Button, int>();

        private List<DRBE_Group> DRBE_grp_list = new List<DRBE_Group>();

        private Button Temp_grp_bt;

        private void Group_button_decolor()
        {
            int i = 0;
            i = 0;
            while(i< Group_bt_list.Count)
            {
                Group_bt_list[i].Background = Default_back_black_color_brush;
                i++;
            }
        }
        private void Create_group_panel_setup()
        {
            Create_grp_bd = new Border() { 
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.5,0.5,0.5,0.5)
            };
            ParentGrid.Children.Add(Create_grp_bd);
            Create_grp_bd.SetValue(Grid.ColumnProperty, 1);
            Create_grp_bd.SetValue(Grid.ColumnSpanProperty, 154);
            Create_grp_bd.SetValue(Grid.RowProperty, 110);
            Create_grp_bd.SetValue(Grid.RowSpanProperty, 40);
            Canvas.SetZIndex(Create_grp_bd, 9);

            Create_grp_title_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Add New Group",
                Foreground = white_button_brush,
                FontSize = 18,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Create_grp_title_tb);
            Create_grp_title_tb.SetValue(Grid.ColumnProperty, 60);
            Create_grp_title_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Create_grp_title_tb.SetValue(Grid.RowProperty, 112);
            Create_grp_title_tb.SetValue(Grid.RowSpanProperty, 8);
            Canvas.SetZIndex(Create_grp_title_tb, 10);


            Create_grp_name_title_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Group Name",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Create_grp_name_title_tb);
            Create_grp_name_title_tb.SetValue(Grid.ColumnProperty, 5);
            Create_grp_name_title_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Create_grp_name_title_tb.SetValue(Grid.RowProperty, 125);
            Create_grp_name_title_tb.SetValue(Grid.RowSpanProperty, 5);
            Canvas.SetZIndex(Create_grp_name_title_tb, 10);

            Create_grp_name_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Create_grp_name_tb);
            Create_grp_name_tb.SetValue(Grid.ColumnProperty, 25);
            Create_grp_name_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Create_grp_name_tb.SetValue(Grid.RowProperty, 125);
            Create_grp_name_tb.SetValue(Grid.RowSpanProperty, 5);
            Canvas.SetZIndex(Create_grp_name_tb, 10);

            Create_grp_confirm_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Background = Default_back_black_color_brush,
                Content = "Add",
                Foreground = white_button_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5),
                BorderBrush = dark_grey_brush,
                FontSize = 20
            };
            ParentGrid.Children.Add(Create_grp_confirm_bt);
            Create_grp_confirm_bt.SetValue(Grid.ColumnProperty, 20);
            Create_grp_confirm_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Create_grp_confirm_bt.SetValue(Grid.RowProperty, 140);
            Create_grp_confirm_bt.SetValue(Grid.RowSpanProperty, 7);
            Create_grp_confirm_bt.Click += Create_grp_confirm_bt_Click;
            Canvas.SetZIndex(Create_grp_confirm_bt, 10);

            Create_grp_cancel_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Background = Default_back_black_color_brush,
                Content = "Cancel",
                Foreground = white_button_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5),
                BorderBrush = dark_grey_brush,
                FontSize = 20
            };
            ParentGrid.Children.Add(Create_grp_cancel_bt);
            Create_grp_cancel_bt.SetValue(Grid.ColumnProperty, 110);
            Create_grp_cancel_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Create_grp_cancel_bt.SetValue(Grid.RowProperty, 140);
            Create_grp_cancel_bt.SetValue(Grid.RowSpanProperty, 7);
            Create_grp_cancel_bt.Click += Create_grp_cancel_bt_Click;
            Canvas.SetZIndex(Create_grp_cancel_bt, 10);
        }

        private void Create_grp_cancel_bt_Click(object sender, RoutedEventArgs e)
        {
            Create_group_panel_hide();
        }

        private async void Create_grp_confirm_bt_Click(object sender, RoutedEventArgs e)
        {
            DRBE_grp_list.Add(new DRBE_Group());
            if(Create_grp_name_tb.Text.Length<1)
            {
                await ShowDialog("Error", "Please Enter name");
            }else
            {
                DRBE_grp_list[DRBE_grp_list.Count - 1].Name = Create_grp_name_tb.Text;
            }

            int i = 0;
            int ii = 0;
            Group_trans_link_list.Add(new List<int>());
            Group_receive_link_list.Add(new List<int>());
            while(i<Group_trans_link_list.Count-1)
            {
                Group_trans_link_list[i].Add(0);
                Group_trans_link_list[Group_trans_link_list.Count - 1].Add(0);

                Group_receive_link_list[i].Add(0);
                Group_receive_link_list[Group_trans_link_list.Count - 1].Add(0);
                i++;
            }
            Group_receive_link_list[Group_trans_link_list.Count - 1].Add(0);
            Group_trans_link_list[Group_trans_link_list.Count - 1].Add(0);

            Group_panel_refresh();
            Create_grp_button();
            Create_group_panel_hide();
        }

        private void Create_grp_button()
        {
            Group_gd_list = new List<Grid>();
            Group_img_list = new List<Image>();
            Group_tb_list = new List<TextBlock>();
            Group_sp_list = new List<StackPanel>();
            Group_bt_list = new List<Button>();
            int i = 0;
            i = 0;
            while(i< DRBE_grp_SP.Count)
            {
                DRBE_grp_SP[i].Children.Clear();
                i++;
            }
            DRBE_grp_SP[0].Children.Add(Add_grp_bt);
            i = 0;
            while (i < DRBE_grp_list.Count)
            {
                #region grp bt
                Group_gd_list.Add(new Grid()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Background = Default_back_black_color_brush
                });

                Group_gd_list[Group_gd_list.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                Group_gd_list[Group_gd_list.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });

                Group_img_list.Add(new Image()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Aircraft_group_icon.png", UriKind.RelativeOrAbsolute)),
                    Height = 20
                });
                Group_img_list[Group_img_list.Count - 1].SetValue(Grid.ColumnProperty, 0);
                Group_img_list[Group_img_list.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

                Group_tb_list.Add(new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontSize = 12,
                    Text = DRBE_grp_list[i].Name,
                    Foreground = white_button_brush
                });

                Group_tb_list[Group_tb_list.Count - 1].SetValue(Grid.ColumnProperty, 1);
                Group_tb_list[Group_tb_list.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);
                //Add_grp_tb.SetValue(Grid.RowSpanProperty, 1);

                Group_sp_list.Add(new StackPanel()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Height = 40,
                    Background = Default_back_black_color_brush
                });
                Group_sp_list[Group_sp_list.Count - 1].Children.Add(Group_gd_list[Group_gd_list.Count - 1]);
                Group_gd_list[Group_gd_list.Count - 1].Children.Add(Group_img_list[Group_img_list.Count - 1]);
                Group_gd_list[Group_gd_list.Count - 1].Children.Add(Group_tb_list[Group_tb_list.Count - 1]);

                Group_bt_list.Add(new Button()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Stretch,
                    Background = Default_back_black_color_brush,
                    Height = 40,
                    Content = Group_sp_list[Group_sp_list.Count - 1],
                    BorderBrush = white_button_brush,
                    BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
                });
                Group_bt_list[Group_bt_list.Count - 1].Click += DRBE_grp_button_Click;
                Dic_group_bt_int[Group_bt_list[Group_bt_list.Count - 1]] = i;

                DRBE_grp_SP[(i + 1) % 10].Children.Add(Group_bt_list[Group_bt_list.Count - 1]);


                
                #endregion
                i++;
            }
            if(Group_bt_list.Count>0)
            {
                Temp_grp_bt = Group_bt_list[Group_bt_list.Count - 1];
                Group_bt_list[Group_bt_list.Count - 1].Background = green_button_brush;
            }
        }

        private void Grp_bt_decolor()
        {
            int i = 0;
            i = 0;
            while(i< Group_bt_list.Count)
            {
                Group_bt_list[i].Background = Default_back_black_color_brush;
                i++;
            }
        }
        private void Grp_obj_hide()
        {
            int i = 0;
            i = 0;
            int gindex = Group_bt_list.IndexOf(Temp_grp_bt);
            int oindex = 0;
            if (Dic_grp_objs.ContainsKey(DRBE_grp_list[gindex]))
            {
                i = 0;
                while (i < Dic_grp_objs[DRBE_grp_list[gindex]].Count)
                {
                    oindex = DRBE_obj_list.IndexOf(Dic_grp_objs[DRBE_grp_list[gindex]][i]);
                    if (oindex >= 0)
                    {
                        Obj_bt_list[oindex].Background = Default_back_black_color_brush;
                    }
                    i++;
                }
            }

        }
        private async Task Grp_obj_show()
        {
            int i = 0;
            i = 0;
            int gindex = Group_bt_list.IndexOf(Temp_grp_bt);
            int oindex = 0;
            if(DRBE_grp_list.Count<1)
            {
                await ShowDialog("Error", "No Group Defined, Please Add Group");
                return;
            }
            if(Dic_grp_objs.ContainsKey(DRBE_grp_list[gindex]))
            {
                i = 0;
                while(i< Dic_grp_objs[DRBE_grp_list[gindex]].Count)
                {
                    oindex = DRBE_obj_list.IndexOf(Dic_grp_objs[DRBE_grp_list[gindex]][i]);
                    if(oindex>=0)
                    {
                        Obj_bt_list[oindex].Background = green_bright_button_brush;
                    }
                    i++;
                }
            }

        }
        private void Grp_obj_write_dic()
        {
            int i = 0;
            i = 0;
            Dic_objs_grp[DRBE_obj_list[Dic_bt_obj_int[Temp_obj_bt]]] = new List<DRBE_Group>();
            while (i<Group_bt_list.Count)
            {
                if(Group_bt_list[i].Background == green_bright_button_brush)
                {
                    Dic_objs_grp[DRBE_obj_list[Dic_bt_obj_int[Temp_obj_bt]]].Add(DRBE_grp_list[i]);
                    if(Dic_grp_objs.ContainsKey(DRBE_grp_list[i]))
                    {
                        if(Dic_grp_objs[DRBE_grp_list[i]].IndexOf(DRBE_obj_list[Dic_bt_obj_int[Temp_obj_bt]])<0)
                        {
                            Dic_grp_objs[DRBE_grp_list[i]].Add(DRBE_obj_list[Dic_bt_obj_int[Temp_obj_bt]]);
                        }
                    }else
                    {
                        Dic_grp_objs[DRBE_grp_list[i]] = new List<DRBE_Objs>();
                    }
                }else
                {
                    if (Dic_grp_objs.ContainsKey(DRBE_grp_list[i]))
                    {
                        int ri = Dic_grp_objs[DRBE_grp_list[i]].IndexOf(DRBE_obj_list[Dic_bt_obj_int[Temp_obj_bt]]);
                        if (ri > 0)
                        {
                            Dic_grp_objs[DRBE_grp_list[i]].RemoveAt(ri);
                        }
                    }
                    else
                    {
                        Dic_grp_objs[DRBE_grp_list[i]] = new List<DRBE_Objs>();
                    }
                }
                i++;
            }
        }
        private void Grp_obj_bt_fetch(DRBE_Objs x)
        {
            int index = -1;
            int i = 0;
            Grp_bt_decolor();
            if (Dic_objs_grp.ContainsKey(x))
            {
                i = 0;
                while(i< Dic_objs_grp[x].Count)
                {
                    index = DRBE_grp_list.IndexOf(Dic_objs_grp[x][i]);
                    if(index>=0)
                    {
                        Group_bt_list[index].Background = green_bright_button_brush;
                    }
                    i++;
                }
                
            }
        }
        private async void DRBE_grp_button_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            int index = 0;
            i = 0;
            Button foo = sender as Button;
            if(Mode_flag == 0)
            {
                if(foo.Background == green_bright_button_brush)
                {
                    foo.Background = Default_back_black_color_brush;
                }else
                {
                    foo.Background = green_bright_button_brush;
                }
                Grp_obj_write_dic();
            }else if(Mode_flag == 1)
            {
                Grp_obj_hide();
                Temp_grp_bt.Background = Default_back_black_color_brush;
                DRBE_group_config_update();
                Temp_grp_bt = foo;
                DRBE_group_config_show();
                Temp_grp_bt.Background = green_ready_brush;
                await Grp_obj_show();
            }
            else
            {

            }
        }
        private void DRBE_group_config_update()
        {
            int index = Group_bt_list.IndexOf(Temp_grp_bt);
            int i = 0;
            if(Group_rt_mode_flag==0)
            {
                i = 0;
                while (i < Groupp_bt_list.Count)
                {
                    if(Groupp_bt_list[i].Background == green_bright_button_brush)
                    {
                        Group_receive_link_list[index][i] = DRBE_grp_cb[i].SelectedIndex;
                    }else
                    {
                        Group_receive_link_list[index][i] = 0;
                    }
                    i++;
                }
            }
            else if (Group_rt_mode_flag == 1)
            {
                i = 0;
                while (i < Groupp_bt_list.Count)
                {
                    if (Groupp_bt_list[i].Background == green_bright_button_brush)
                    {
                        Group_trans_link_list[index][i] = DRBE_grp_cb[i].SelectedIndex;
                    }else
                    {
                        Group_trans_link_list[index][i] = 0;
                    }
                    i++;
                }
            }

        }
        private void DRBE_group_config_show()
        {
            DRBE_group_panel_bt_decolor();
            int index = 0;
            int i = 0;
            index = Group_bt_list.IndexOf(Temp_grp_bt);
            i = 0;
            if(Group_rt_mode_flag==0)
            {
                i = 0;
                while (i < Group_receive_link_list[index].Count)
                {
                    if (Group_receive_link_list[index][i] > 0)
                    {
                        Groupp_bt_list[i].Background = green_bright_button_brush;
                    }
                    i++;
                }
            }else if(Group_rt_mode_flag == 1)
            {

                i = 0;
                while (i < Group_trans_link_list[index].Count)
                {
                    if (Group_trans_link_list[index][i] > 0)
                    {
                        Groupp_bt_list[i].Background = green_bright_button_brush;
                    }
                    i++;
                }
            }
        }
        private void DRBE_group_panel_bt_decolor()
        {
            int i = 0;
            i = 0;
            while(i< Groupp_bt_list.Count)
            {
                Groupp_bt_list[i].Background = Default_back_black_color_brush;
                i++;
            }
        }
        private void Create_group_panel_show()
        {
            Create_grp_bd.Visibility = Visibility.Visible;

            Create_grp_title_tb.Visibility = Visibility.Visible;

            Create_grp_name_title_tb.Visibility = Visibility.Visible;

            Create_grp_name_tb.Visibility = Visibility.Visible;

            Create_grp_confirm_bt.Visibility = Visibility.Visible;

            Create_grp_cancel_bt.Visibility = Visibility.Visible;
        }

        private void Create_group_panel_hide()
        {
            Create_grp_bd.Visibility = Visibility.Collapsed;

            Create_grp_title_tb.Visibility = Visibility.Collapsed;

            Create_grp_name_title_tb.Visibility = Visibility.Collapsed;

            Create_grp_name_tb.Visibility = Visibility.Collapsed;

            Create_grp_confirm_bt.Visibility = Visibility.Collapsed;

            Create_grp_cancel_bt.Visibility = Visibility.Collapsed;
        }

        private ScrollViewer DRBE_grpp_SV = new ScrollViewer();
        private StackPanel DRBE_grpp_SPL = new StackPanel();
        private StackPanel DRBE_grpp_SPR = new StackPanel();
        private Grid DRBE_grpp_GD = new Grid();

        private TextBlock DRBE_grp_id_tb = new TextBlock();

        private Button DRBE_grpp_receive_bt = new Button();
        private Button DRBE_grpp_transmit_bt = new Button();
        private Button DRBE_grpp_property_bt = new Button();

        private List<ComboBox> DRBE_grp_cb = new List<ComboBox>();

        private List<Border> DRBE_grp_bdr = new List<Border>();
        private List<Border> DRBE_grp_bdl = new List<Border>();

        private List<Button> Groupp_bt_list = new List<Button>();
        private List<StackPanel> Groupp_sp_list = new List<StackPanel>();
        private List<Image> Groupp_img_list = new List<Image>();
        private List<TextBlock> Groupp_tb_list = new List<TextBlock>();
        private List<Grid> Groupp_gd_list = new List<Grid>();

        private Border Groupp_block_bd = new Border();

        private List<List<int>> Group_trans_link_list = new List<List<int>>();
        private List<List<int>> Group_receive_link_list = new List<List<int>>();

        private int Group_rt_mode_flag = 0;

        private void Group_generate_link_list()
        {
            Group_receive_link_list = new List<List<int>>();
            Group_trans_link_list = new List<List<int>>();
            int i = 0;
            int ii = 0;
            i = 0;
            while(i<DRBE_grp_list.Count)
            {
                Group_receive_link_list.Add(new List<int>());
                Group_trans_link_list.Add(new List<int>());
                ii = 0;
                while(ii<DRBE_grp_list.Count)
                {
                    Group_receive_link_list[i].Add(0);
                    Group_trans_link_list[i].Add(0);
                    ii++;
                }
                i++;
            }
        }
        private void Group_panel_setup()
        {

            Groupp_block_bd = new Border() { 
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Transparent_brush
            };
            ParentGrid.Children.Add(Groupp_block_bd);
            Groupp_block_bd.SetValue(Grid.ColumnProperty, 1);
            Groupp_block_bd.SetValue(Grid.ColumnSpanProperty, 154);
            Groupp_block_bd.SetValue(Grid.RowProperty, 25);
            Groupp_block_bd.SetValue(Grid.RowSpanProperty, 83);
            Canvas.SetZIndex(Groupp_block_bd,5);

            DRBE_grp_id_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Group ID ",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold,
                Width = 200,
                Height = 50
            };
            ParentGrid.Children.Add(DRBE_grp_id_tb);
            DRBE_grp_id_tb.SetValue(Grid.ColumnProperty, 161);
            DRBE_grp_id_tb.SetValue(Grid.ColumnSpanProperty, 15);
            DRBE_grp_id_tb.SetValue(Grid.RowProperty, 31);
            DRBE_grp_id_tb.SetValue(Grid.RowSpanProperty, 4);


            DRBE_grpp_receive_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "Receive Interaction",
                Foreground = white_button_brush,
                BorderBrush = green_bright_button_brush,
                BorderThickness = new Thickness(1, 1, 1, 0),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 10
            };
            ParentGrid.Children.Add(DRBE_grpp_receive_bt);
            DRBE_grpp_receive_bt.SetValue(Grid.ColumnProperty, 160);
            DRBE_grpp_receive_bt.SetValue(Grid.ColumnSpanProperty, 20);
            DRBE_grpp_receive_bt.SetValue(Grid.RowProperty, 25);
            DRBE_grpp_receive_bt.SetValue(Grid.RowSpanProperty, 5);
            DRBE_grpp_receive_bt.Click += DRBE_grpp_receive_bt_Click;
            Group_rt_mode_flag = 0;

            DRBE_grpp_transmit_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = "Transmit Interaction",
                Foreground = white_button_brush,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(1, 1, 1, 0),
                FontWeight = FontWeights.ExtraBold,
                FontSize = 10
            };
            ParentGrid.Children.Add(DRBE_grpp_transmit_bt);
            DRBE_grpp_transmit_bt.SetValue(Grid.ColumnProperty, 180);
            DRBE_grpp_transmit_bt.SetValue(Grid.ColumnSpanProperty, 20);
            DRBE_grpp_transmit_bt.SetValue(Grid.RowProperty, 25);
            DRBE_grpp_transmit_bt.SetValue(Grid.RowSpanProperty, 5);
            DRBE_grpp_transmit_bt.Click += DRBE_grpp_transmit_bt_Click;
            #region property panel
            DRBE_grpp_GD = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush

            };
            DRBE_grpp_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            DRBE_grpp_GD.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            //DRBE_GD.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            DRBE_grpp_SV = new ScrollViewer()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                VerticalScrollMode = ScrollMode.Enabled,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            };
            ParentGrid.Children.Add(DRBE_grpp_SV);
            DRBE_grpp_SV.SetValue(Grid.ColumnProperty, 160);
            DRBE_grpp_SV.SetValue(Grid.ColumnSpanProperty, 40);
            DRBE_grpp_SV.SetValue(Grid.RowProperty, 35);
            DRBE_grpp_SV.SetValue(Grid.RowSpanProperty, 115);


            DRBE_grpp_SPL = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            };
            DRBE_grpp_SV.Content = DRBE_grpp_GD;
            DRBE_grpp_GD.Children.Add(DRBE_grpp_SPL);
            //ParentGrid.Children.Add(DRBE_SPL);
            DRBE_grpp_SPL.SetValue(Grid.ColumnProperty, 0);
            DRBE_grpp_SPL.SetValue(Grid.ColumnSpanProperty, 1);
            //DRBE_SPL.SetValue(Grid.RowProperty, 40);
            //DRBE_SPL.SetValue(Grid.RowSpanProperty, 110);

            DRBE_grpp_SPR = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            };
            //DRBE_SV.Content = DRBE_SPL;
            DRBE_grpp_GD.Children.Add(DRBE_grpp_SPR);
            //ParentGrid.Children.Add(DRBE_SPL);
            DRBE_grpp_SPR.SetValue(Grid.ColumnProperty, 1);
            DRBE_grpp_SPR.SetValue(Grid.ColumnSpanProperty, 1);
            //DRBE_SPL.SetValue(Grid.RowProperty, 40);
            //DRBE_SPL.SetValue(Grid.RowSpanProperty, 110);
            #endregion
        }

        private void DRBE_grpp_transmit_bt_Click(object sender, RoutedEventArgs e)
        {
            DRBE_grpp_receive_bt.BorderBrush = white_button_brush;
            DRBE_grpp_transmit_bt.BorderBrush = green_bright_button_brush;
            DRBE_group_config_update();
            Group_rt_mode_flag = 1;
            DRBE_group_panel_bt_decolor();
            DRBE_group_config_show();
        }

        private void DRBE_grpp_receive_bt_Click(object sender, RoutedEventArgs e)
        {
            DRBE_grpp_receive_bt.BorderBrush = green_bright_button_brush;
            DRBE_grpp_transmit_bt.BorderBrush = white_button_brush;
            DRBE_group_config_update();
            Group_rt_mode_flag = 0;
            DRBE_group_panel_bt_decolor();
            DRBE_group_config_show();
        }

        private void Group_panel_refresh()
        {
            int i = 0;
            i = 0;
            DRBE_grpp_SPR.Children.Clear();
            DRBE_grpp_SPL.Children.Clear();
            Groupp_bt_list = new List<Button>();
            Groupp_sp_list = new List<StackPanel>();
            Groupp_img_list = new List<Image>();
            Groupp_tb_list = new List<TextBlock>();
            Groupp_gd_list = new List<Grid>();
            while (i<DRBE_grp_list.Count)
            {
                #region grp bt
                Groupp_gd_list.Add(new Grid()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Background = Default_back_black_color_brush
                });

                Groupp_gd_list[Groupp_gd_list.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                Groupp_gd_list[Groupp_gd_list.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });

                Groupp_img_list.Add(new Image()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Aircraft_group_icon.png", UriKind.RelativeOrAbsolute)),
                    Height = 20
                });
                Groupp_img_list[Groupp_img_list.Count - 1].SetValue(Grid.ColumnProperty, 0);
                Groupp_img_list[Groupp_img_list.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

                Groupp_tb_list.Add(new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontSize = 12,
                    Text = DRBE_grp_list[i].Name,
                    Foreground = white_button_brush
                });

                Groupp_tb_list[Groupp_tb_list.Count - 1].SetValue(Grid.ColumnProperty, 1);
                Groupp_tb_list[Groupp_tb_list.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);
                //Add_grp_tb.SetValue(Grid.RowSpanProperty, 1);

                Groupp_sp_list.Add(new StackPanel()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Height = 35,
                    Background = Default_back_black_color_brush
                });
                Groupp_sp_list[Groupp_sp_list.Count - 1].Children.Add(Groupp_gd_list[Groupp_gd_list.Count - 1]);
                Groupp_gd_list[Groupp_gd_list.Count - 1].Children.Add(Groupp_img_list[Groupp_img_list.Count - 1]);
                Groupp_gd_list[Groupp_gd_list.Count - 1].Children.Add(Groupp_tb_list[Groupp_tb_list.Count - 1]);

                Groupp_bt_list.Add(new Button()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Stretch,
                    Background = Default_back_black_color_brush,
                    Height = 35,
                    Content = Groupp_sp_list[Groupp_sp_list.Count - 1],
                    BorderBrush = white_button_brush,
                    BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
                });

                DRBE_grpp_SPL.Children.Add(Groupp_bt_list[Groupp_bt_list.Count - 1]);

                Groupp_bt_list[Groupp_bt_list.Count - 1].Click += Groupp_bt_list_Click;

                DRBE_grp_bdl.Add(new Border()
                {
                    Height = 5,
                    BorderBrush = dark_grey_brush,
                    BorderThickness = new Thickness(0, 0.5, 0, 0)
                });
                DRBE_grpp_SPL.Children.Add(DRBE_grp_bdl[DRBE_grp_bdl.Count - 1]);

                DRBE_grp_cb.Add( new ComboBox()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Text = "Priority",
                    SelectedIndex = 2,
                    Background = white_button_brush,
                    Height = 35
                });
                DRBE_grpp_SPR.Children.Add(DRBE_grp_cb[DRBE_grp_cb.Count-1]);
                DRBE_grp_cb[DRBE_grp_cb.Count - 1].Items.Add("Low Priority");
                DRBE_grp_cb[DRBE_grp_cb.Count - 1].Items.Add("Medium Priority");
                DRBE_grp_cb[DRBE_grp_cb.Count - 1].Items.Add("High Priority");

                DRBE_grp_bdr.Add(new Border()
                {
                    Height = 5,
                    BorderBrush = dark_grey_brush,
                    BorderThickness = new Thickness(0, 0.5, 0, 0)
                });
                DRBE_grpp_SPR.Children.Add(DRBE_grp_bdr[DRBE_grp_bdr.Count - 1]);

                #endregion
                i++;
            }
        }

        private void Groupp_bt_list_Click(object sender, RoutedEventArgs e)
        {
            Button foo = sender as Button;
            if(foo.Background == Default_back_black_color_brush)
            {
                foo.Background = green_bright_button_brush;
            }else
            {
                foo.Background = Default_back_black_color_brush;
            }
        }

        private void Group_panel_hide()
        {
            DRBE_grp_id_tb.Visibility = Visibility.Collapsed;


            DRBE_grpp_receive_bt.Visibility = Visibility.Collapsed;

            DRBE_grpp_transmit_bt.Visibility = Visibility.Collapsed;
            #region property panel


            DRBE_grpp_SV.Visibility = Visibility.Collapsed;
            #endregion

            Groupp_block_bd.Visibility = Visibility.Collapsed;
        }

        private void Group_panel_show()
        {
            DRBE_grp_id_tb.Visibility = Visibility.Visible;


            DRBE_grpp_receive_bt.Visibility = Visibility.Visible;

            DRBE_grpp_transmit_bt.Visibility = Visibility.Visible;
            #region property panel


            DRBE_grpp_SV.Visibility = Visibility.Visible;
            #endregion

            Groupp_block_bd.Visibility = Visibility.Visible;
        }
        #endregion
        #region Generate Object
        private Dictionary<Button, int> Dic_bt_obj_int = new Dictionary<Button, int>();
        private List<DRBE_Objs> DRBE_obj_list = new List<DRBE_Objs>();
        private void Object_panel_hide()
        {
            Global_Property_bt.Visibility = Visibility.Collapsed;
            Initial_Property_bt.Visibility = Visibility.Collapsed;
            All_Property_bt.Visibility = Visibility.Collapsed;
            Antenna_Pol_Property_bt.Visibility = Visibility.Collapsed;
            Clut_RFim_Property_bt.Visibility = Visibility.Collapsed;
            RCS_Property_bt.Visibility = Visibility.Collapsed;
            Constraint_Property_bt.Visibility = Visibility.Collapsed;
            Lookup_table_bt.Visibility = Visibility.Collapsed;
            #region property panel

            DRBE_SV.Visibility = Visibility.Collapsed;

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
            #region Obj Save
            Obj_save_tb.Visibility = Visibility.Collapsed;

            Obj_save_bt.Visibility = Visibility.Collapsed;
            #endregion
            Object_ID_tb.Visibility = Visibility.Collapsed;

            //Info_bd.Visibility = Visibility.Collapsed;
        }

        private void Object_panel_show()
        {
            Global_Property_bt.Visibility = Visibility.Visible;
            Initial_Property_bt.Visibility = Visibility.Visible;
            All_Property_bt.Visibility = Visibility.Visible;
            Antenna_Pol_Property_bt.Visibility = Visibility.Visible;
            Clut_RFim_Property_bt.Visibility = Visibility.Visible;
            RCS_Property_bt.Visibility = Visibility.Visible;
            Constraint_Property_bt.Visibility = Visibility.Visible;
            Lookup_table_bt.Visibility = Visibility.Visible;
            #region property panel

            DRBE_SV.Visibility = Visibility.Visible;

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
            #region Obj Save
            Obj_save_tb.Visibility = Visibility.Visible;

            Obj_save_bt.Visibility = Visibility.Visible;
            #endregion
            Object_ID_tb.Visibility = Visibility.Visible;

            //Info_bd.Visibility = Visibility.Visible;
        }
        private void Generate_object_bt()
        {
            int i = 0;
            i = 0;
            while(i< DRBE_Obj_SPL.Count)
            {
                DRBE_Obj_SPL[i].Children.Clear();
                i++;
            }
            Obj_gd_list = new List<Grid>();
            Obj_img_list = new List<Image>();
            Obj_tb_list = new List<TextBlock>();
            Obj_sp_list = new List<StackPanel>();
            Obj_bt_list = new List<Button>();
            i = 0;
            while (i<DRBE_obj_list.Count)
            {
                #region grp bt
                Obj_gd_list.Add(new Grid()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Background = Default_back_black_color_brush
                });

                Obj_gd_list[Obj_gd_list.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                Obj_gd_list[Obj_gd_list.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });

                Obj_img_list.Add(new Image()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Object_icon.png", UriKind.RelativeOrAbsolute)),
                    Height = 20
                });
                Obj_img_list[Obj_img_list.Count - 1].SetValue(Grid.ColumnProperty, 0);
                Obj_img_list[Obj_img_list.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

                Obj_tb_list.Add(new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontSize = 12,
                    Text = "ID:" + DRBE_obj_list[i].ID.ToString(),
                    Foreground = white_button_brush
                });

                Obj_tb_list[Obj_tb_list.Count - 1].SetValue(Grid.ColumnProperty, 1);
                Obj_tb_list[Obj_tb_list.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);
                //Add_grp_tb.SetValue(Grid.RowSpanProperty, 1);

                Obj_sp_list.Add(new StackPanel()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Height = 40,
                    Background = Default_back_black_color_brush
                });
                Obj_sp_list[Obj_sp_list.Count - 1].Children.Add(Obj_gd_list[Obj_gd_list.Count - 1]);
                Obj_gd_list[Obj_gd_list.Count - 1].Children.Add(Obj_img_list[Obj_img_list.Count - 1]);
                Obj_gd_list[Obj_gd_list.Count - 1].Children.Add(Obj_tb_list[Obj_tb_list.Count - 1]);

                Obj_bt_list.Add(new Button()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Stretch,
                    Background = Default_back_black_color_brush,
                    Height = 40,
                    Content = Obj_sp_list[Obj_sp_list.Count - 1],
                    BorderBrush = white_button_brush,
                    BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
                });
                Dic_bt_obj_int[Obj_bt_list[Obj_bt_list.Count - 1]] = i;
                Obj_bt_list[Obj_bt_list.Count - 1].Click += DRBE_obj_bt_Click;

                DRBE_Obj_SPL[i % 10].Children.Add(Obj_bt_list[Obj_bt_list.Count - 1]);

                #endregion
                i++;
            }
            
            if(Obj_bt_list.Count>0)
            {
                Obj_bt_list[0].Background = green_ready_brush;
                Update_DRBE_objs(0);
                Temp_obj_bt = Obj_bt_list[0];

            }
        }
        private void DRBE_obj_bt_decolor()
        {
            int i = 0;
            i = 0;
            while(i< Obj_bt_list.Count)
            {
                Obj_bt_list[i].Background = Default_back_black_color_brush;
                i++;
            }    
        }
        private Button Temp_obj_bt = new Button();
        private void DRBE_obj_bt_Click(object sender, RoutedEventArgs e)
        {
            Button foo = sender as Button;
            if(Mode_flag==0)
            {
                Grp_obj_write_dic();
                Temp_obj_bt.Background = Default_back_black_color_brush;
                foo.Background = green_ready_brush;
                Load_DRBE_objs(Dic_bt_obj_int[Temp_obj_bt]);
                Temp_obj_bt = foo;
                Grp_obj_bt_fetch(DRBE_obj_list[Dic_bt_obj_int[Temp_obj_bt]]);
                Update_DRBE_objs(Dic_bt_obj_int[foo]);
            }
        }

        private void Generate_default_objs(int x)
        {
            int i = 0;
            i = 0;
            while(i<x)
            {
                DRBE_obj_list.Add(new DRBE_Objs((UInt16)i));
                i++;
            }
        }
        private void Load_DRBE_objs(int x)
        {

            DRBE_obj_list[x].Coordinate_system = Coordinate_system_cb.SelectedIndex;
            DRBE_obj_list[x].Reference_Obj_ID = Reference_frame_id_cb.SelectedIndex;

            if(Is_transmitter_chb.IsChecked == true)
            {
                DRBE_obj_list[x].Is_Transmitter = true;
            }else
            {
                DRBE_obj_list[x].Is_Transmitter = false;
            }

            if (Is_reflector_chb.IsChecked == true)
            {
                DRBE_obj_list[x].Is_Reflector = true;
            }
            else
            {
                DRBE_obj_list[x].Is_Reflector = false;
            }

            if (Is_receiver_chb.IsChecked == true)
            {
                DRBE_obj_list[x].Is_Receiver = true;
            }
            else
            {
                DRBE_obj_list[x].Is_Receiver = false;
            }

            if (Is_stationary_chb.IsChecked == true)
            {
                DRBE_obj_list[x].Is_Stationary = true;
            }
            else
            {
                DRBE_obj_list[x].Is_Stationary = false;
            }

            DRBE_obj_list[x].Initial_Position_X = S_D(Initial_positionX_tb.Text);
            DRBE_obj_list[x].Initial_Position_Y = S_D(Initial_positionY_tb.Text);
            DRBE_obj_list[x].Initial_Position_Z = S_D(Initial_positionZ_tb.Text);

            DRBE_obj_list[x].Initial_Velocity_X = S_D(Initial_velocityX_tb.Text);
            DRBE_obj_list[x].Initial_Velocity_Y = S_D(Initial_velocityY_tb.Text);
            DRBE_obj_list[x].Initial_Velocity_Z = S_D(Initial_velocityZ_tb.Text);

            DRBE_obj_list[x].Initial_Acceleration_X = S_D(Initial_accelerationX_tb.Text);
            DRBE_obj_list[x].Initial_Acceleration_Y = S_D(Initial_accelerationY_tb.Text);
            DRBE_obj_list[x].Initial_Acceleration_Z = S_D(Initial_accelerationZ_tb.Text);

            DRBE_obj_list[x].Initial_Orientation_X = S_D(Initial_orientationX_tb.Text);
            DRBE_obj_list[x].Initial_Orientation_Y = S_D(Initial_orientationY_tb.Text);
            DRBE_obj_list[x].Initial_Orientation_Z = S_D(Initial_orientationZ_tb.Text);

            DRBE_obj_list[x].Initial_Mangle_AZ = S_D(Initial_mpointan_az_tb.Text);
            DRBE_obj_list[x].Initial_Mangle_EL = S_D(Initial_mpointan_el_tb.Text);

            DRBE_obj_list[x].Initial_Eangle_AZ = S_D(Initial_epointan_az_tb.Text);
            DRBE_obj_list[x].Initial_Eangle_AZ = S_D(Initial_epointan_el_tb.Text);

            DRBE_obj_list[x].Number_Antenna_AZ = S_D(Initial_epointan_az_tb.Text);
            DRBE_obj_list[x].Number_Antenna_EL = S_D(Initial_epointan_el_tb.Text);

            DRBE_obj_list[x].Beamwidth_AZ = S_D(Beamwidth_AZ_tb.Text);
            DRBE_obj_list[x].Beamwidth_EL = S_D(Beamwidth_EL_tb.Text);

            DRBE_obj_list[x].Resolution_AZ = S_D(Resolution_AZ_tb.Text);
            DRBE_obj_list[x].Resolution_EL = S_D(Resolution_EL_tb.Text);

            DRBE_obj_list[x].Element_Spacing = S_D(Ele_space_tb.Text);

            DRBE_obj_list[x].Backlobe_Scaling = S_D(Backlobe_scaling_tb.Text);

            DRBE_obj_list[x].Window_type = Antenna_window_cb.SelectedIndex;

            DRBE_obj_list[x].Antenna_order = ANT_F_cb.SelectedIndex;
            DRBE_obj_list[x].Dictionary_dimension = S_D(ANT_DS_tb.Text);

            DRBE_obj_list[x].RCS_order = RCS_F_cb.SelectedIndex;
            DRBE_obj_list[x].RCS_point = S_D(RCS_SP_tb.Text);
            DRBE_obj_list[x].RCS_angle_resolution = S_D(RCS_AR_tb.Text);
            DRBE_obj_list[x].RCS_frequency_point = S_D(RCS_FB_tb.Text);
            DRBE_obj_list[x].RCS_number_of_polarization = S_D(RCS_PN_tb.Text);
            DRBE_obj_list[x].RCS_output_time_sampe = S_D(RCS_SS_tb.Text);

        }

        private void Update_DRBE_objs(int x)
        {
            Object_ID_tb.Text = "Object ID: " + DRBE_obj_list[x].ID.ToString();

            Coordinate_system_cb.SelectedIndex = DRBE_obj_list[x].Coordinate_system;
            Reference_frame_id_cb.SelectedIndex = DRBE_obj_list[x].Reference_Obj_ID;

            //Initial_Ref_Position_X = 10;
            //Initial_Ref_Position_Y = 20;
            //Initial_Ref_Position_Z = 30;
            //Initial_Latitude = 30.5;
            //Initial_Longtitude = 29.5;

            //Antenna_constant = 20;

            //Polar_coefficient1 = 10;
            //Polar_coefficient2 = 10;

            //Clutter_Gamma_k = 20;
            //Clutter_Gamma_theta = 20;

            //Clutter_Gausian_m = 20;
            //Clutter_Gausian_v = 20;

            //Constraint_centerX = 300;
            //Constraint_centerY = 300;
            //Constraint_centerZ = 300;

            //Constraint_max_height = 300;
            //Constraint_radius = 300;
            //Constraint_max_speed = 300;
            //Constraint_max_acceleration = 300;
            //Constraint_max_orientation = 300;
            //Constraint_max_band = 300;
            //Constraint_min_band = 300;
            //Constraint_max_band_change = 300;

            //Cut_type = 0;

            Is_transmitter_chb.IsChecked = DRBE_obj_list[x].Is_Transmitter;
            Is_reflector_chb.IsChecked = DRBE_obj_list[x].Is_Reflector;
            Is_receiver_chb.IsChecked = DRBE_obj_list[x].Is_Receiver;
            Is_stationary_chb.IsChecked = DRBE_obj_list[x].Is_Stationary;

            Initial_positionX_tb.Text = DRBE_obj_list[x].Initial_Position_X.ToString();
            Initial_positionY_tb.Text = DRBE_obj_list[x].Initial_Position_Y.ToString();
            Initial_positionZ_tb.Text = DRBE_obj_list[x].Initial_Position_Z.ToString();

            Initial_velocityX_tb.Text = DRBE_obj_list[x].Initial_Velocity_X.ToString();
            Initial_velocityY_tb.Text = DRBE_obj_list[x].Initial_Velocity_Y.ToString();
            Initial_velocityZ_tb.Text = DRBE_obj_list[x].Initial_Velocity_Z.ToString();

            Initial_accelerationX_tb.Text = DRBE_obj_list[x].Initial_Acceleration_X.ToString();
            Initial_accelerationY_tb.Text = DRBE_obj_list[x].Initial_Acceleration_Y.ToString();
            Initial_accelerationZ_tb.Text = DRBE_obj_list[x].Initial_Acceleration_Z.ToString();

            Initial_orientationX_tb.Text = DRBE_obj_list[x].Initial_Orientation_X.ToString();
            Initial_orientationY_tb.Text = DRBE_obj_list[x].Initial_Orientation_Y.ToString();
            Initial_orientationZ_tb.Text = DRBE_obj_list[x].Initial_Orientation_Z.ToString();

            Initial_mpointan_az_tb.Text = DRBE_obj_list[x].Initial_Mangle_AZ.ToString();
            Initial_mpointan_el_tb.Text = DRBE_obj_list[x].Initial_Mangle_EL.ToString();

            Initial_epointan_az_tb.Text = DRBE_obj_list[x].Initial_Eangle_AZ.ToString();
            Initial_epointan_el_tb.Text = DRBE_obj_list[x].Initial_Eangle_EL.ToString();

            Initial_epointan_az_tb.Text = DRBE_obj_list[x].Number_Antenna_AZ.ToString();
            Initial_epointan_el_tb.Text = DRBE_obj_list[x].Number_Antenna_EL.ToString();

            Beamwidth_AZ_tb.Text = DRBE_obj_list[x].Beamwidth_AZ.ToString();
            Beamwidth_EL_tb.Text = DRBE_obj_list[x].Beamwidth_EL.ToString();

            Resolution_AZ_tb.Text = DRBE_obj_list[x].Resolution_AZ.ToString();
            Resolution_EL_tb.Text = DRBE_obj_list[x].Resolution_EL.ToString();

            Ele_space_tb.Text = DRBE_obj_list[x].Element_Spacing.ToString();
            Backlobe_scaling_tb.Text = DRBE_obj_list[x].Backlobe_Scaling.ToString();

            Antenna_window_cb.SelectedIndex = DRBE_obj_list[x].Window_type;

            ANT_F_cb.SelectedIndex = (int)DRBE_obj_list[x].Antenna_order;
            ANT_DS_tb.Text = DRBE_obj_list[x].Dictionary_dimension.ToString();

            RCS_F_cb.SelectedIndex = (int)DRBE_obj_list[x].RCS_order;
            RCS_SP_tb.Text = DRBE_obj_list[x].RCS_point.ToString();
            RCS_AR_tb.Text = DRBE_obj_list[x].RCS_angle_resolution.ToString();
            RCS_FB_tb.Text = DRBE_obj_list[x].RCS_frequency_point.ToString();
            RCS_PN_tb.Text = DRBE_obj_list[x].RCS_number_of_polarization.ToString();
            RCS_SS_tb.Text= DRBE_obj_list[x].RCS_output_time_sampe.ToString();
        }
        #endregion

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
                //Link_Viewer.Setup(Sc_transmitter_list, Sc_receiver_list, Sc_reflector_list);
            }
        }
        private async Task Sc_loading()
        {
            string content = "";

            string pname = (Sc_load_cb.Items[Sc_load_cb.SelectedIndex]).ToString();
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            string lpath = storageFolder.Path.ToString() + "\\Simulator File\\Link File\\";
            string gpath = storageFolder.Path.ToString() + "\\Simulator File\\Group File\\";
            string spath = storageFolder.Path.ToString() + "\\Simulator File\\Scenario File\\";
            //string path = storageFolder.Path.ToString() + "\\Simulator File\\Scenario File\\" + (Sc_load_cb.Items[Sc_load_cb.SelectedIndex]).ToString();
            int i = 0;
            i = 0;
            while (i < pname.Length - 3)
            {
                lpath += pname[i].ToString();
                gpath += pname[i].ToString();
                spath += pname[i].ToString();
                i++;
            }
            lpath += "dlv";
            spath += "dsc";
            gpath += "dgp";
            try
            {
                content = await Read_file(spath);

                await Parse_scenario_file(content);

                Generate_object_bt();


            }
            catch
            {
                await ShowDialog("Load Error", "Unable to Load Object Files");
            }

            try
            {

                content = await Read_file(lpath);
                //await Read_sc_file_ui(content);
                await Parse_link_file(content);

            }
            catch
            {
                Link_table = new List<List<List<bool>>>();
                await ShowDialog("Load Error", "Unable to Load Link Files");
            }

            try
            {


                content = await Read_file(gpath);
                //await Read_sc_file_ui(content);
                await Parse_group_file(content);

                Create_grp_button();
                Temp_grp_bt = Group_bt_list[0];
                
                Grp_obj_bt_fetch(DRBE_obj_list[Dic_bt_obj_int[Temp_obj_bt]]);
                Group_panel_refresh();

                Update_DRBE_objs(Dic_bt_obj_int[Temp_obj_bt]);

                //Process_link_information_create();

            }
            catch
            {
                DRBE_grp_list = new List<DRBE_Group>();
                Group_trans_link_list = new List<List<int>>();
                Group_receive_link_list = new List<List<int>>();
                Dic_grp_objs = new Dictionary<DRBE_Group, List<DRBE_Objs>>();
                Dic_objs_grp = new Dictionary<DRBE_Objs, List<DRBE_Group>>();

                Create_grp_button();
                Group_panel_refresh();


                await ShowDialog("Load Error", "Unable to Load Group Files");
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
        private string Generate_scenario_file_s()
        {
            string result = "";
            int i = 0;
            i = 0;
            while (i < DRBE_obj_list.Count())
            {
                result += DRBE_obj_list[i].Generate_file_report_s() + "\r\n";
                i++;
            }
            return result;
        }
        private string Generate_link_file_s()
        {
            int i = 0;
            int ii = 0;
            int iii = 0;
            string temp = "";
            i = 0;
            temp += "{";
            while(i<DRBE_obj_list.Count)
            {
                if(Dic_LPI_ti.ContainsKey(DRBE_obj_list[i]))
                {
                    temp += i.ToString() + ",";
                }
                i++;
            }
            temp += "}";
            i = 0;
            temp += "{";
            while (i < DRBE_obj_list.Count)
            {
                if (Dic_LPI_oi.ContainsKey(DRBE_obj_list[i]))
                {
                    temp += i.ToString() + ",";
                }
                i++;
            }
            temp += "}";
            i = 0;
            temp += "{";
            while (i < DRBE_obj_list.Count)
            {
                if (Dic_LPI_ri.ContainsKey(DRBE_obj_list[i]))
                {
                    temp += i.ToString() + ",";
                }
                i++;
            }
            temp += "}";
            i = 0;
            while(i<Link_table.Count)
            {
                ii = 0;
                while(ii<Link_table[i].Count)
                {
                    iii = 0;
                    while(iii<Link_table[i][ii].Count)
                    {
                        if(Link_table[i][ii][iii])
                        {
                            temp += "{" + i.ToString() + "," + ii.ToString() + "," + iii.ToString() + ",}"; 
                        }
                        iii++;
                    }
                    ii++;
                }
                i++;
            }


            return temp;
            
        }
        private async Task Parse_link_file(string x)
        {
            string temp = "";
            int i = 0;
            int dici = 0;
            int mode = 0;
            int ind1 = 0;
            int ind2 = 0;
            int ind3 = 0;

            int inx1 = 0;
            int inx2 = 0;
            int inx3 = 0;

            int tcount = 0;
            int ocount = 0;
            int rcount = 0;
            i = 0;
            Dic_LPI_ti = new Dictionary<DRBE_Objs, int>();
            Dic_LPI_it = new Dictionary<int, DRBE_Objs>();
            Dic_LPI_oi = new Dictionary<DRBE_Objs, int>();
            Dic_LPI_io = new Dictionary<int, DRBE_Objs>();
            Dic_LPI_ri = new Dictionary<DRBE_Objs, int>();
            Dic_LPI_ir = new Dictionary<int, DRBE_Objs>();
            while (i<x.Length)
            {
                if(x[i] == '{')
                {

                }else if(x[i] ==',')
                {
                    if(mode == 0)
                    {
                        Dic_LPI_ti[DRBE_obj_list[S_I(temp)]] = dici;
                        Dic_LPI_it[dici] = DRBE_obj_list[S_I(temp)];
                        //await ShowDialog("Read L", "Trans: " + temp + "-" + dici.ToString());
                        tcount++;
                        dici++;
                    }
                    else if(mode == 1)
                    {
                        Dic_LPI_oi[DRBE_obj_list[S_I(temp)]] = dici;
                        Dic_LPI_io[dici] = DRBE_obj_list[S_I(temp)];
                        //await ShowDialog("Read L", "Obj: " + temp + "-" + dici.ToString());
                        ocount++;
                        dici++;
                    }
                    else if (mode == 2)
                    {
                        Dic_LPI_ri[DRBE_obj_list[S_I(temp)]] = dici;
                        Dic_LPI_ir[dici] = DRBE_obj_list[S_I(temp)];
                        //await ShowDialog("Read L", "Rec: " + temp + "-" + dici.ToString());
                        rcount++;
                        dici++;
                    }else if(mode == 4)
                    {
                        ind1 = S_I(temp);
                        mode++;
                    }
                    else if (mode == 5)
                    {
                        ind2 = S_I(temp);
                        mode++;
                    }
                    else if (mode == 6)
                    {
                        ind3 = S_I(temp);
                        mode = 3;
                        Link_table[ind1][ind2][ind3] = true;
                        //await ShowDialog("Read L", ind1.ToString() + "-" + ind2.ToString()+ "-" + ind3.ToString());
                    }
                    temp = "";
                }else if(x[i] == '}')
                {
                    if(mode==2)
                    {
                        DRBE_t_count = tcount;
                        DRBE_o_count = ocount;
                        DRBE_r_count = rcount;
                        Link_table = new List<List<List<bool>>>();
                        inx1 = 0;
                        while(inx1<tcount)
                        {
                            Link_table.Add(new List<List<bool>>());
                            inx2 = 0;
                            while(inx2<ocount)
                            {
                                Link_table[inx1].Add(new List<bool>());
                                inx3 = 0;
                                while(inx3<rcount)
                                {
                                    Link_table[inx1][inx2].Add(false);
                                    inx3++;
                                }
                                inx2++;
                            }
                            inx1++;
                        }
                        mode++;
                    }
                    dici = 0;
                    mode++;
                }else
                {
                    temp += x[i].ToString();
                }
                i++;
            }
        }
        private async Task Parse_scenario_file(string x)
        {
            int i = 0;
            int obji = 0;
            string temp = "";
            DRBE_obj_list = new List<DRBE_Objs>();
            int mode = 0;
            while(i<x.Length)
            {
                if(x[i] == '{')
                {
                    i++;
                    while(x[i]!='}')
                    {
                        temp += x[i].ToString();
                        i++;
                    }
                    mode++;
                    if (mode == 1)
                    {
                        DRBE_obj_list.Add(new DRBE_Objs((ushort)S_I(temp)));
                    }
                    else if(mode == 2)
                    {
                        if(temp=="True")
                        {
                            DRBE_obj_list[DRBE_obj_list.Count-1].Is_Transmitter = true;
                        }else
                        {
                            DRBE_obj_list[DRBE_obj_list.Count-1].Is_Transmitter = false;
                        }
                    } //is transmitter
                    else if (mode == 3)
                    {
                        if (temp == "True")
                        {
                            DRBE_obj_list[DRBE_obj_list.Count-1].Is_Reflector = true;
                        }
                        else
                        {
                            DRBE_obj_list[DRBE_obj_list.Count-1].Is_Reflector = false;
                        }
   
                    } //is reflector
                    else if (mode == 4)
                    {
                        if (temp == "True")
                        {
                            DRBE_obj_list[DRBE_obj_list.Count-1].Is_Receiver = true;
                        }
                        else
                        {
                            DRBE_obj_list[DRBE_obj_list.Count-1].Is_Receiver = false;
                        }

                    } //is receiver
                    else if (mode == 5)
                    {
                        if (temp == "True")
                        {
                            DRBE_obj_list[DRBE_obj_list.Count-1].Is_Stationary = true;
                        }
                        else
                        {
                            DRBE_obj_list[DRBE_obj_list.Count-1].Is_Stationary = false;
                        }

                    } //is transmitter
                    else if (mode == 6)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Coordinate_system = S_I(temp);
                    } //cor
                    else if (mode == 7)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Initial_Position_X = S_D(temp);
                    } //ini pos x
                    else if (mode == 8)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Initial_Position_Y = S_D(temp);
                    } //ini pos y
                    else if (mode == 9)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Initial_Position_Z = S_D(temp);
                    } //ini pos z
                    else if (mode == 10)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Initial_Longtitude = S_D(temp);
                    } //ini long
                    else if (mode == 11)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Initial_Latitude = S_D(temp);
                    } //ini la
                    else if (mode == 12)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Initial_Ref_Position_X = S_D(temp);
                    } //ini ref pos x
                    else if (mode == 13)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Initial_Ref_Position_Y = S_D(temp);
                    } //ini ref pos y
                    else if (mode == 14)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Initial_Ref_Position_Z = S_D(temp);
                    } //ini ref pos z
                    else if (mode == 15)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Reference_Obj_ID = S_I(temp);
                    } //ref obj ID
                    else if (mode == 16)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Initial_Velocity_X = S_D(temp);
                    } //ini velocity x
                    else if (mode == 17)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Initial_Velocity_Y = S_D(temp);
                    } //ini velocity y
                    else if (mode == 18)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Initial_Velocity_Z = S_D(temp);
                    } //ini velocity z
                    else if (mode == 19)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Initial_Acceleration_X = S_D(temp);
                    } //ini acceleration x
                    else if (mode == 20)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Initial_Acceleration_Y = S_D(temp);
                    } //ini acceleration y
                    else if (mode == 21)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Initial_Acceleration_Z = S_D(temp);
                    } //ini acceleration z
                    else if (mode == 22)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Initial_Orientation_X= S_D(temp);
                    } //ini orientation x
                    else if (mode == 23)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Initial_Orientation_Y = S_D(temp);
                    } //ini orientation y
                    else if (mode == 24)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Initial_Orientation_Z = S_D(temp);
                    } //ini orientation z
                    else if (mode == 25)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Initial_Eangle_AZ = S_D(temp);
                    } //E Az angle
                    else if (mode == 26)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Initial_Eangle_EL = S_D(temp);
                    } //E El angle
                    else if (mode == 27)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Initial_Mangle_AZ = S_D(temp);
                    } //M AZ angle
                    else if (mode == 28)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Initial_Mangle_EL = S_D(temp);
                    } //M EL angle
                    else if (mode == 29)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count - 1].Number_Antenna_AZ = S_D(temp);
                    } //no antenna az
                    else if (mode == 30)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count - 1].Number_Antenna_EL = S_D(temp);
                    } //no antenna el
                    else if (mode == 31)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count - 1].Element_Spacing = S_D(temp);
                    } //element spacing
                    else if (mode == 32)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count - 1].Beamwidth_AZ = S_D(temp);
                    } //beamwidth az
                    else if (mode == 33)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count - 1].Beamwidth_EL = S_D(temp);
                    } //beamwidth el
                    else if (mode == 34)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count - 1].Resolution_AZ = S_D(temp);
                    } //angle reso az
                    else if (mode == 35)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count - 1].Resolution_EL = S_D(temp);
                    } //angle reso az
                    else if (mode == 36)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Backlobe_Scaling = S_D(temp);
                    } //backlobe scale
                    else if (mode == 37)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Window_type = S_I(temp);
                    } //window type
                    else if (mode == 38)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Cut_type = S_I(temp);
                    } //cut type
                    else if (mode == 39)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Antenna_constant = S_D(temp);
                    } //ant const
                    else if (mode == 40)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Polar_coefficient1 = S_D(temp);
                    } //polar coe 1
                    else if (mode == 41)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Polar_coefficient2 = S_D(temp);
                    } //polar coe 2
                    else if (mode == 42)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Clutter_Gamma_k = S_D(temp);
                    } //gamma k
                    else if (mode == 43)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Clutter_Gamma_theta = S_D(temp);
                    } //gamma theta
                    else if (mode == 44)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Clutter_Gausian_m = S_D(temp);
                    } //gausian m
                    else if (mode == 45)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Clutter_Gausian_v = S_D(temp);
                    } //gausian v
                    else if (mode == 46)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Constraint_centerX = S_D(temp);
                    } //mov center x
                    else if (mode == 47)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Constraint_centerY = S_D(temp);
                    } //mov center y
                    else if (mode == 48)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Constraint_centerZ = S_D(temp);
                    } //mov center z
                    else if (mode == 49)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Constraint_max_height = S_D(temp);
                    } //max height
                    else if (mode == 50)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Constraint_radius = S_D(temp);
                    } //radius
                    else if (mode == 51)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Constraint_max_speed = S_D(temp);
                    } //max speed
                    else if (mode == 52)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Constraint_max_acceleration = S_D(temp);
                    } //max acceleration
                    else if (mode == 53)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Constraint_max_orientation = S_D(temp);
                    } //max angular vel
                    else if (mode == 54)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Constraint_max_band = S_D(temp);
                    } //max band
                    else if (mode == 55)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Constraint_min_band = S_D(temp);
                    } //min band
                    else if (mode == 56)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Constraint_max_band_change = S_D(temp);
                    } //max band change
                    else if (mode == 57)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Dictionary_dimension = S_D(temp);
                    } //dictionary size
                    else if (mode == 58)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].Antenna_order = S_D(temp);
                    } //Antenna order
                    else if (mode == 59)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].RCS_number_of_polarization = S_D(temp);
                    } //number of polar
                    else if (mode == 60)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].RCS_output_time_sampe = S_D(temp);
                    } //RCS sample size
                    else if (mode == 61)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].RCS_point = S_D(temp);
                    } //RCS points
                    else if (mode == 62)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].RCS_angle_resolution = S_D(temp);
                    } //angle resolution
                    else if (mode == 63)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].RCS_frequency_point = S_D(temp);
                    } //frequency point
                    else if (mode == 64)
                    {
                        DRBE_obj_list[DRBE_obj_list.Count-1].RCS_order = S_D(temp);
                    } //number of polar
                    else if (mode == 65)
                    {
                        //await ShowDialog(DRBE_obj_list.Count.ToString(), temp);
                        mode = 0;
                        //DRBE_obj_list[DRBE_obj_list.Count].RCS_number_of_polarization = S_D(temp);
                    } //number of polar
                    temp = "";

                }

                i++;
            }
        }
        private string Generate_group_file_s()
        {
            string temp = "";
            int i = 0;
            int ii = 0;
            temp += "{";
            temp += DRBE_grp_list.Count.ToString();
            temp += ",}";


            temp += "{";
            i = 0;
            while (i < DRBE_grp_list.Count)
            {
                temp += DRBE_grp_list[i].Name + ",";
                i++;
            }
            temp += "}";


            temp += "{";
            i = 0;
            while(i<Group_trans_link_list.Count)
            {
                ii = 0;
                while(ii<Group_trans_link_list[i].Count)
                {
                    if(Group_trans_link_list[i][ii]>0)
                    {
                        temp += i.ToString() + "," + ii.ToString() + "," + Group_trans_link_list[i][ii].ToString() + ",";
                    }
                    ii++;
                }
                i++;
            }
            temp += "}";


            temp += "{";
            i = 0;
            while (i < Group_receive_link_list.Count)
            {
                ii = 0;
                while (ii < Group_receive_link_list[i].Count)
                {
                    if (Group_receive_link_list[i][ii] > 0)
                    {
                        temp += i.ToString() + "," + ii.ToString() + "," + Group_receive_link_list[i][ii].ToString() + ",";
                    }
                    ii++;
                }
                i++;
            }
            temp += "}";
            
            i = 0;
            while(i<DRBE_grp_list.Count)
            {
                temp += "{";
                if(Dic_grp_objs.ContainsKey(DRBE_grp_list[i]))
                {
                    ii = 0;
                    while(ii< Dic_grp_objs[DRBE_grp_list[i]].Count)
                    {
                        temp += DRBE_obj_list.IndexOf(Dic_grp_objs[DRBE_grp_list[i]][ii]) + ",";
                        ii++;
                    }
                }else
                {
                    temp += ",";
                }
                temp += "}";
                i++;
            }

            return temp;
        }
        private async Task Parse_group_file(string x)
        {
            int i = 0;
            string temp = "";
            i = 0;
            int mode = 0;
            int ginxi = 0;
            int ginxii = 0;
            int cnt = 0;
            int glisti = 0;

            int dici = 0;
            int gind1 = 0;
            int gind2 = 0;
            int gind3 = 0;

            int grpid = 0;
            DRBE_grp_list = new List<DRBE_Group>();
            Group_trans_link_list = new List<List<int>>();
            Group_receive_link_list = new List<List<int>>();
            Dic_grp_objs = new Dictionary<DRBE_Group, List<DRBE_Objs>>();
            while(i<x.Length)
            {
                if(x[i]=='{')
                {
                    i++;
                    while(x[i]!='}')
                    {
                        if(x[i]==',')
                        {
                            if(mode==0)
                            {
                                cnt = S_I(temp);
                                ginxi = 0;
                                while (ginxi < cnt)
                                {
                                    DRBE_grp_list.Add(new DRBE_Group());
                                    ginxi++;
                                }

                                ginxi = 0;
                                while(ginxi<cnt)
                                {
                                    Group_trans_link_list.Add(new List<int>());
                                    Group_receive_link_list.Add(new List<int>());
                                    ginxii = 0;
                                    while(ginxii<cnt)
                                    {
                                        Group_trans_link_list[ginxi].Add(0);
                                        Group_receive_link_list[ginxi].Add(0);
                                        ginxii++;
                                    }
                                    ginxi++;
                                }
                            }
                            else if(mode == 1)
                            {
                                DRBE_grp_list[grpid].Name = temp;
                                grpid++;
                            }
                            else if(mode == 2)
                            {
                                if(glisti==0)
                                {
                                    gind1 = S_I(temp);
                                    glisti++;
                                }else if(glisti == 1)
                                {
                                    gind2 = S_I(temp);
                                    glisti++;
                                }
                                else if (glisti == 2)
                                {
                                    gind3 = S_I(temp);
                                    Group_trans_link_list[gind1][gind2] = gind3;
                                    glisti = 0;
                                }
                            }
                            else if (mode == 3)
                            {
                                if (glisti == 0)
                                {
                                    gind1 = S_I(temp);
                                    glisti++;
                                }
                                else if (glisti == 1)
                                {
                                    gind2 = S_I(temp);
                                    glisti++;
                                }
                                else if (glisti == 2)
                                {
                                    gind3 = S_I(temp);
                                    Group_receive_link_list[gind1][gind2] = gind3;
                                    glisti = 0;
                                }
                            }else if(mode == 4)
                            {
                                //await ShowDialog(dici.ToString(), temp);
                                if(temp.Length>0)
                                {
                                    if(!Dic_grp_objs.ContainsKey(DRBE_grp_list[dici]))
                                    {
                                        Dic_grp_objs[DRBE_grp_list[dici]] = new List<DRBE_Objs>();
                                        Dic_grp_objs[DRBE_grp_list[dici]].Add(DRBE_obj_list[S_I(temp)]);
                                    }
                                    else
                                    {
                                        Dic_grp_objs[DRBE_grp_list[dici]].Add(DRBE_obj_list[S_I(temp)]);
                                    }

                                    if(!Dic_objs_grp.ContainsKey(DRBE_obj_list[S_I(temp)]))
                                    {
                                        Dic_objs_grp[DRBE_obj_list[S_I(temp)]] = new List<DRBE_Group>();
                                        Dic_objs_grp[DRBE_obj_list[S_I(temp)]].Add(DRBE_grp_list[dici]);
                                    }else
                                    {
                                        Dic_objs_grp[DRBE_obj_list[S_I(temp)]].Add(DRBE_grp_list[dici]);
                                    }
                                }
                                
                            }
                            temp = "";
                        }else
                        {
                            temp += x[i].ToString();
                        }
                        i++;
                    }
                    mode++;
                    if(mode==5)
                    {
                        mode = 4;
                        dici++;
                    }
                }
                else
                {
                    temp += x[i].ToString();
                }
                i++;
            }
        }
        
        private async void Sc_save_bt_Click(object sender, RoutedEventArgs e)
        {
            await Save_All_Files();
        }
        private async Task Save_All_Files()
        {
            await Process_link_information_create();
            await DRBE_SS.Start("Save Scenario", new List<string>() { "Simulator File", "Scenario File" }, "dsc", Generate_scenario_file_s());
            await Task.Delay(500);
            await DRBE_SS.Start("Save Links", new List<string>() { "Simulator File", "Link File" }, "dlv", Generate_link_file_s());
            await Task.Delay(500);
            await DRBE_SS.Start("Save Scenario", new List<string>() { "Simulator File", "Group File" }, "dgp", Generate_group_file_s());
            await Task.Delay(500);

            await Task.Delay(500);
            await Detect_scenario_file();
        }
        private async void Lv_view_bt_Click(object sender, RoutedEventArgs e)
        {
            await Process_link_information_create();
            if (Link_table.Count>0)
            {
                if(Link_table[0].Count > 0)
                {
                    if(Link_table[0][0].Count > 0)
                    {
                        Load_DRBE_objs(Dic_bt_obj_int[Temp_obj_bt]);
                        hide();
                        ParentPage.Link_Viewer.Setup_refresh(DRBE_obj_list, Dic_LPI_it, Dic_LPI_io, Dic_LPI_ir, Dic_LPI_ti, Dic_LPI_oi, Dic_LPI_ri, Link_table);
                        ParentPage.Link_Viewer.show();
                    }
                    else
                    {
                        await ShowDialog("Error", "Missing Receiver");
                    }
                }
                else
                {
                    await ShowDialog("Error", "Missing Reflector");
                }
            }else
            {
                await ShowDialog("Error", "Missing Transmitter");
            }
            
        }

        public void hide()
        {
            Hide_all_Property();
            Create_group_panel_hide();
            Object_panel_hide();
            Group_panel_hide();
            List_view_bt.Visibility = Visibility.Collapsed;
            List_view_tb.Visibility = Visibility.Collapsed;
            DRBE_Obj_SV.Visibility = Visibility.Collapsed;
            DRBE_grp_SV.Visibility = Visibility.Collapsed;
            Home_bt.Visibility = Visibility.Collapsed;
            Mode_grp_bt.Visibility = Visibility.Collapsed;
            Mode_obj_bt.Visibility = Visibility.Collapsed;
            Unity_view_bt.Visibility = Visibility.Collapsed;
            Unity_view_tb.Visibility = Visibility.Collapsed;

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


            DRBE_SV.Visibility = Visibility.Collapsed;
            

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
            Object_panel_show();
            //Group_panel_show();
            List_view_bt.Visibility = Visibility.Visible;
            List_view_tb.Visibility = Visibility.Visible;
            Home_bt.Visibility = Visibility.Visible;
            Mode_grp_bt.Visibility = Visibility.Visible;
            Mode_obj_bt.Visibility = Visibility.Visible;
            DRBE_Obj_SV.Visibility = Visibility.Visible;
            DRBE_grp_SV.Visibility = Visibility.Visible;
            Unity_view_bt.Visibility = Visibility.Visible;
            Unity_view_tb.Visibility = Visibility.Visible;
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
            DRBE_SV.Visibility = Visibility.Visible;


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
            await Sc_loading();
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
            Lookup_table_show_s();
            
        }
        private void Constraint_Property_bt_Click(object sender, RoutedEventArgs e)
        {
            Property_tab_decolor();
            Constraint_Property_bt.BorderBrush = green_bright_button_brush;
            Hide_all_Property();
            Constraint_Property_show_s();
        }
        private void RCS_Property_bt_Click(object sender, RoutedEventArgs e)
        {
            Property_tab_decolor();
            RCS_Property_bt.BorderBrush = green_bright_button_brush;
            Hide_all_Property();
            RCS_Property_show_s();
        }

        private void Clut_RFim_Property_bt_Click(object sender, RoutedEventArgs e)
        {
            Property_tab_decolor();
            Clut_RFim_Property_bt.BorderBrush = green_bright_button_brush;
            Hide_all_Property();
            RF_Clut_Property_show_s();
        }

        private void Antenna_Pol_Property_bt_Click(object sender, RoutedEventArgs e)
        {
            Property_tab_decolor();
            Antenna_Pol_Property_bt.BorderBrush = green_bright_button_brush;
            Hide_all_Property();
            Antenna_Pol_Property_show_s();
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

            //myProcess.StandardInput.WriteLine("1900000000,60,500000,0.001,6,2,5,16,1,800,6,20,2,1,4,10");

            //await Generate_all_obj();

            DRBE_obj_list.Add(new DRBE_Objs((ushort)(DRBE_obj_list.Count)));
            #region grp bt
            Obj_gd_list.Add(new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush
            });

            Obj_gd_list[Obj_gd_list.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            Obj_gd_list[Obj_gd_list.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });

            Obj_img_list.Add(new Image()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Object_icon.png", UriKind.RelativeOrAbsolute)),
                Height = 20
            });
            Obj_img_list[Obj_img_list.Count - 1].SetValue(Grid.ColumnProperty, 0);
            Obj_img_list[Obj_img_list.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

            Obj_tb_list.Add(new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Text = "ID:" + DRBE_obj_list[DRBE_obj_list.Count - 1].ID.ToString(),
                Foreground = white_button_brush
            });

            Obj_tb_list[Obj_tb_list.Count - 1].SetValue(Grid.ColumnProperty, 1);
            Obj_tb_list[Obj_tb_list.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);
            //Add_grp_tb.SetValue(Grid.RowSpanProperty, 1);

            Obj_sp_list.Add(new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Height = 40,
                Background = Default_back_black_color_brush
            });
            Obj_sp_list[Obj_sp_list.Count - 1].Children.Add(Obj_gd_list[Obj_gd_list.Count - 1]);
            Obj_gd_list[Obj_gd_list.Count - 1].Children.Add(Obj_img_list[Obj_img_list.Count - 1]);
            Obj_gd_list[Obj_gd_list.Count - 1].Children.Add(Obj_tb_list[Obj_tb_list.Count - 1]);

            Obj_bt_list.Add(new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 40,
                Content = Obj_sp_list[Obj_sp_list.Count - 1],
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            });
            Dic_bt_obj_int[Obj_bt_list[Obj_bt_list.Count - 1]] = DRBE_obj_list.Count - 1;
            Obj_bt_list[Obj_bt_list.Count - 1].Click += DRBE_obj_bt_Click;

            DRBE_Obj_SPL[(DRBE_obj_list.Count - 1) % 10].Children.Add(Obj_bt_list[Obj_bt_list.Count - 1]);

            if(Obj_bt_list.Count==1)
            {
                Temp_obj_bt = Obj_bt_list[0];
                Temp_obj_bt.Background = green_ready_brush;
                Update_DRBE_objs(Dic_bt_obj_int[Temp_obj_bt]);
            }

            #endregion


        }

        private void Hide_all_Property()
        {
            //Global_Property_hide();
            Global_Property_hide_s();
            //Initial_Property_hide();
            Initial_Property_hide_s();
            Antenna_Pol_Property_hide_s();
            RCS_Property_hide_s();
            RF_Clut_Property_hide_s();
            Constraint_Property_hide_s();
            Lookup_table_hide_s();
        }
        private void Show_all_Property()
        {
            Global_Property_show_s();
            Initial_Property_show_s();
            Antenna_Pol_Property_show_s();
            RCS_Property_show_s();
            RF_Clut_Property_show_s();
            Constraint_Property_show_s();
            Lookup_table_show_s();
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

        private CheckBox Is_transmitter_chb = new CheckBox();
        private TextBlock Is_transmitter_tb = new TextBlock();
        private Border Is_transmitter_bd = new Border();

        private CheckBox Is_reflector_chb = new CheckBox();
        private TextBlock Is_reflector_tb = new TextBlock();
        private Border Is_reflector_bd = new Border();

        private CheckBox Is_receiver_chb = new CheckBox();
        private TextBlock Is_receiver_tb = new TextBlock();
        private Border Is_receiver_bd = new Border();

        private CheckBox Is_stationary_chb = new CheckBox();
        private TextBlock Is_stationary_tb = new TextBlock();
        private Border Is_stationary_bd = new Border();

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

            #region Is transmitter
            Is_transmitter_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Is Transmitter",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold
            };
            Is_transmitter_chb = new CheckBox() { 
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35,
                Content = Is_transmitter_tb,
                IsChecked = null
            };
            DRBE_SPL.Children.Add(Is_transmitter_chb);

            Is_transmitter_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Is_transmitter_bd);
            #endregion

            #region Is reflector
            Is_reflector_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Is Reflector",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold
            };

            Is_reflector_chb = new CheckBox()
            {
                IsChecked = null,
                Background = Default_back_black_color_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35,
                Content = Is_reflector_tb
            };
            DRBE_SPR.Children.Add(Is_reflector_chb);

            Is_reflector_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Is_reflector_bd);
            #endregion

            #region Is reflector
            Is_receiver_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Is Receiver",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold
            };

            Is_receiver_chb = new CheckBox()
            {
                IsChecked = null,
                Background = Default_back_black_color_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35,
                Content = Is_receiver_tb
            };
            DRBE_SPL.Children.Add(Is_receiver_chb);

            Is_receiver_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Is_receiver_bd);
            #endregion

            #region Is Stationary
            Is_stationary_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Is Stationary",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold
            };

            Is_stationary_chb = new CheckBox()
            {
                IsChecked = null,
                Background = Default_back_black_color_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35,
                Content = Is_stationary_tb
            };
            DRBE_SPR.Children.Add(Is_stationary_chb);

            Is_stationary_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Is_stationary_bd);
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
            Antenna_Pol_Property_show_s();
            RCS_Property_show_s();
            RF_Clut_Property_show_s();
            Constraint_Property_show_s();
            Lookup_table_show_s();
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

            #region Is transmitter

            DRBE_SPL.Children.Add(Is_transmitter_chb);
            DRBE_SPL.Children.Add(Is_transmitter_bd);
            #endregion

            #region Is reflector

            DRBE_SPR.Children.Add(Is_reflector_chb);
            DRBE_SPR.Children.Add(Is_reflector_bd);
            #endregion

            #region Is reflector
            DRBE_SPL.Children.Add(Is_receiver_chb);
            DRBE_SPL.Children.Add(Is_receiver_bd);
            #endregion

            #region Is Stationary
            DRBE_SPR.Children.Add(Is_stationary_chb);
            DRBE_SPR.Children.Add(Is_stationary_bd);
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

        #region antenna property
        private TextBlock Beamwidth_EL_ttb = new TextBlock();
        private TextBox Beamwidth_EL_tb = new TextBox();
        private Button Beamwidth_EL_bt = new Button();
        private Border Beamwidth_EL_bd = new Border();
        private Border Beamwidth_EL_bd1 = new Border();

        private TextBlock Beamwidth_AZ_ttb = new TextBlock();
        private TextBox Beamwidth_AZ_tb = new TextBox();
        private Button Beamwidth_AZ_bt = new Button();
        private Border Beamwidth_AZ_bd = new Border();
        private Border Beamwidth_AZ_bd1 = new Border();

        private TextBlock Number_of_ant_az_ttb = new TextBlock();
        private TextBox Number_of_ant_az_tb = new TextBox();
        private Button Number_of_ant_az_bt = new Button();
        private Border Number_of_ant_az_bd = new Border();
        private Border Number_of_ant_az_bd1 = new Border();

        private TextBlock Number_of_ant_el_ttb = new TextBlock();
        private TextBox Number_of_ant_el_tb = new TextBox();
        private Button Number_of_ant_el_bt = new Button();
        private Border Number_of_ant_el_bd = new Border();
        private Border Number_of_ant_el_bd1 = new Border();

        private TextBlock Ele_space_ttb = new TextBlock();
        private TextBox Ele_space_tb = new TextBox();
        private Button Ele_space_bt = new Button();
        private Border Ele_space_bd = new Border();
        private Border Ele_space_bd1 = new Border();

        private TextBlock Antenna_window_tb = new TextBlock();
        private ComboBox Antenna_window_cb = new ComboBox();
        private Button Antenna_window_bt = new Button();
        private Border Antenna_window_bd = new Border();
        private Border Antenna_window_bd1 = new Border();

        private TextBlock Resolution_EL_ttb = new TextBlock();
        private TextBox Resolution_EL_tb = new TextBox();
        private Button Resolution_EL_bt = new Button();
        private Border Resolution_EL_bd = new Border();
        private Border Resolution_EL_bd1 = new Border();

        private TextBlock Resolution_AZ_ttb = new TextBlock();
        private TextBox Resolution_AZ_tb = new TextBox();
        private Button Resolution_AZ_bt = new Button();
        private Border Resolution_AZ_bd = new Border();
        private Border Resolution_AZ_bd1 = new Border();

        private TextBlock Backlobe_scaling_ttb = new TextBlock();
        private TextBox Backlobe_scaling_tb = new TextBox();
        private Button Backlobe_scaling_bt = new Button();
        private Border Backlobe_scaling_bd = new Border();
        private Border Backlobe_scaling_bd1 = new Border();

        private TextBlock Constant_factor_ttb = new TextBlock();
        private TextBox Constant_factor_tb = new TextBox();
        private Button Constant_factor_bt = new Button();
        private Border Constant_factor_bd = new Border();
        private Border Constant_factor_bd1 = new Border();

        private TextBlock Polar_coe1_ttb = new TextBlock();
        private TextBox Polar_coe1_tb = new TextBox();
        private Button Polar_coe1_bt = new Button();
        private Border Polar_coe1_bd = new Border();
        private Border Polar_coe1_bd1 = new Border();

        private TextBlock Polar_coe2_ttb = new TextBlock();
        private TextBox Polar_coe2_tb = new TextBox();
        private Button Polar_coe2_bt = new Button();
        private Border Polar_coe2_bd = new Border();
        private Border Polar_coe2_bd1 = new Border();

        private TextBlock ANT_DS_ttb = new TextBlock();
        private TextBox ANT_DS_tb = new TextBox();
        private Border ANT_DS_bd = new Border();
        private Border ANT_DS_bd1 = new Border();

        private TextBlock ANT_F_ttb = new TextBlock();
        private ComboBox ANT_F_cb = new ComboBox();
        private Border ANT_F_bd = new Border();
        private Border ANT_F_bd1 = new Border();
        #endregion
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

        public void Antenna_Pol_Property_hide_s()
        {
            DRBE_SPL.Children.Clear();
            DRBE_SPR.Children.Clear(); 
        }
        public void Antenna_Pol_Property_setup_s()
        {

            #region Beamwith EL
            Beamwidth_EL_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Beamwidth_EL_tb);

            Beamwidth_EL_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Beamwith EL",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Beamwidth_EL_ttb);

            Beamwidth_EL_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Beamwidth_EL_bd);
            Beamwidth_EL_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Beamwidth_EL_bd1);

            #endregion
            #region Beamwidth AZ
            Beamwidth_AZ_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Beamwidth_AZ_tb);

            Beamwidth_AZ_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Beamwidth AZ",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Beamwidth_AZ_ttb);

            Beamwidth_AZ_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Beamwidth_AZ_bd);
            Beamwidth_AZ_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Beamwidth_AZ_bd1);
            #endregion

            #region No of Antenna AZ
            Number_of_ant_az_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Number_of_ant_az_tb);

            Number_of_ant_az_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "No of Antenna AZ",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Number_of_ant_az_ttb);

            Number_of_ant_az_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Number_of_ant_az_bd);
            Number_of_ant_az_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Number_of_ant_az_bd1);
            #endregion
            #region No of Antenna EL
            Number_of_ant_el_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Number_of_ant_el_tb);

            Number_of_ant_el_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "No of Antenna EL",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Number_of_ant_el_ttb);

            Number_of_ant_el_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Number_of_ant_el_bd);
            Number_of_ant_el_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Number_of_ant_el_bd1);
            #endregion
            #region Element Spacing
            Ele_space_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Ele_space_tb);

            Ele_space_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Element Spacing",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Ele_space_ttb);

            Ele_space_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Ele_space_bd);
            Ele_space_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Ele_space_bd1);
            #endregion

            #region Window Function
            Antenna_window_cb = new ComboBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Text = "Window Function",
                SelectedIndex = 0,
                Background = white_button_brush,
                Height = 35
            };
            DRBE_SPR.Children.Add(Antenna_window_cb);
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
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Antenna_window_tb);

            Antenna_window_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Antenna_window_bd);
            Antenna_window_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Antenna_window_bd1);
            #endregion

            #region Resolution EL
            Resolution_EL_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Resolution_EL_tb);

            Resolution_EL_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Resolution EL",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Resolution_EL_ttb);

            Resolution_EL_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Resolution_EL_bd);
            Resolution_EL_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Resolution_EL_bd1);
            #endregion
            #region Resolution AZ
            Resolution_AZ_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Resolution_AZ_tb);

            Resolution_AZ_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Resolution AZ",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Resolution_AZ_ttb);

            Resolution_AZ_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Resolution_AZ_bd);
            Resolution_AZ_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Resolution_AZ_bd1);
            #endregion
            #region Backlobe scale
            Backlobe_scaling_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Backlobe_scaling_tb);

            Backlobe_scaling_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Backlobe scaling",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Backlobe_scaling_ttb);

            Backlobe_scaling_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Backlobe_scaling_bd);
            Backlobe_scaling_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Backlobe_scaling_bd1);
            #endregion

            #region Constant_factor
            Constant_factor_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Constant_factor_tb);

            Constant_factor_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Constant Factor\r\n Low Fidelity",
                Foreground = Violet_Red,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Constant_factor_ttb);

            Constant_factor_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Constant_factor_bd);
            Constant_factor_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Constant_factor_bd1);
            #endregion

            #region Polarization Coe1
            Polar_coe1_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Polar_coe1_tb);

            Polar_coe1_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Polarization Coefficient 1",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Polar_coe1_ttb);

            Polar_coe1_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Polar_coe1_bd);
            Polar_coe1_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Polar_coe1_bd1);
            #endregion
            #region Polarization Coe2
            Polar_coe2_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Polar_coe2_tb);

            Polar_coe2_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Polarization Coefficient 2",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Polar_coe2_ttb);

            Polar_coe2_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Polar_coe2_bd);
            Polar_coe2_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Polar_coe2_bd1);
            #endregion

            #region ANT DS
            ANT_DS_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(ANT_DS_tb);

            ANT_DS_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Antenna Dictionary Size",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(ANT_DS_ttb);

            ANT_DS_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(ANT_DS_bd);
            ANT_DS_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(ANT_DS_bd1);
            #endregion

            #region ANT F
            ANT_F_cb = new ComboBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Text = "ANT Fidelity",
                SelectedIndex = 0,
                Background = white_button_brush,
                Height = 35
            };
            ANT_F_cb.Items.Add("Order 0");
            ANT_F_cb.Items.Add("Order 1");
            ANT_F_cb.Items.Add("Order 2");
            ANT_F_cb.Items.Add("Order 3");
            ANT_F_cb.Items.Add("Order 4");
            ANT_F_cb.Items.Add("Order 5");

            DRBE_SPR.Children.Add(ANT_F_cb);

            ANT_F_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "ANT Fidelity",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(ANT_F_ttb);

            ANT_F_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(ANT_F_bd);
            ANT_F_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(ANT_F_bd1);
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
        public void Antenna_Pol_Property_show_s()
        {
            #region Beamwith EL
            DRBE_SPR.Children.Add(Beamwidth_EL_tb);
            DRBE_SPL.Children.Add(Beamwidth_EL_ttb);
            DRBE_SPL.Children.Add(Beamwidth_EL_bd);
            DRBE_SPR.Children.Add(Beamwidth_EL_bd1);

            #endregion
            #region Beamwidth AZ
            DRBE_SPR.Children.Add(Beamwidth_AZ_tb);
            DRBE_SPL.Children.Add(Beamwidth_AZ_ttb);
            DRBE_SPL.Children.Add(Beamwidth_AZ_bd);
            DRBE_SPR.Children.Add(Beamwidth_AZ_bd1);
            #endregion

            #region No of Antenna AZ
            DRBE_SPR.Children.Add(Number_of_ant_az_tb);
            DRBE_SPL.Children.Add(Number_of_ant_az_ttb);
            DRBE_SPL.Children.Add(Number_of_ant_az_bd);
            DRBE_SPR.Children.Add(Number_of_ant_az_bd1);
            #endregion
            #region No of Antenna EL
            DRBE_SPR.Children.Add(Number_of_ant_el_tb);
            DRBE_SPL.Children.Add(Number_of_ant_el_ttb);
            DRBE_SPL.Children.Add(Number_of_ant_el_bd);
            DRBE_SPR.Children.Add(Number_of_ant_el_bd1);
            #endregion
            #region Element Spacing
            DRBE_SPR.Children.Add(Ele_space_tb);
            DRBE_SPL.Children.Add(Ele_space_ttb);
            DRBE_SPL.Children.Add(Ele_space_bd);
            DRBE_SPR.Children.Add(Ele_space_bd1);
            #endregion

            #region Window Function
            DRBE_SPR.Children.Add(Antenna_window_cb);
            DRBE_SPL.Children.Add(Antenna_window_tb);
            DRBE_SPL.Children.Add(Antenna_window_bd);
            DRBE_SPR.Children.Add(Antenna_window_bd1);
            #endregion

            #region Resolution EL
            DRBE_SPR.Children.Add(Resolution_EL_tb);
            DRBE_SPL.Children.Add(Resolution_EL_ttb);
            DRBE_SPL.Children.Add(Resolution_EL_bd);
            DRBE_SPR.Children.Add(Resolution_EL_bd1);
            #endregion
            #region Resolution AZ
            DRBE_SPR.Children.Add(Resolution_AZ_tb);
            DRBE_SPL.Children.Add(Resolution_AZ_ttb);
            DRBE_SPL.Children.Add(Resolution_AZ_bd);
            DRBE_SPR.Children.Add(Resolution_AZ_bd1);
            #endregion
            #region Backlobe scale
            DRBE_SPR.Children.Add(Backlobe_scaling_tb);
            DRBE_SPL.Children.Add(Backlobe_scaling_ttb);
            DRBE_SPL.Children.Add(Backlobe_scaling_bd);
            DRBE_SPR.Children.Add(Backlobe_scaling_bd1);
            #endregion

            #region Constant_factor
            DRBE_SPR.Children.Add(Constant_factor_tb);
            DRBE_SPL.Children.Add(Constant_factor_ttb);
            DRBE_SPL.Children.Add(Constant_factor_bd);
            DRBE_SPR.Children.Add(Constant_factor_bd1);
            #endregion

            #region Polarization Coe1
            DRBE_SPR.Children.Add(Polar_coe1_tb);
            DRBE_SPL.Children.Add(Polar_coe1_ttb);
            DRBE_SPL.Children.Add(Polar_coe1_bd);
            DRBE_SPR.Children.Add(Polar_coe1_bd1);
            #endregion
            #region Polarization Coe2
            DRBE_SPR.Children.Add(Polar_coe2_tb);
            DRBE_SPL.Children.Add(Polar_coe2_ttb);
            DRBE_SPL.Children.Add(Polar_coe2_bd);
            DRBE_SPR.Children.Add(Polar_coe2_bd1);
            #endregion

            #region ANT DS

            DRBE_SPR.Children.Add(ANT_DS_tb);

            DRBE_SPL.Children.Add(ANT_DS_ttb);

            DRBE_SPL.Children.Add(ANT_DS_bd);

            DRBE_SPR.Children.Add(ANT_DS_bd1);
            #endregion

            #region ANT F


            DRBE_SPR.Children.Add(ANT_F_cb);

            DRBE_SPL.Children.Add(ANT_F_ttb);

            DRBE_SPL.Children.Add(ANT_F_bd);

            DRBE_SPR.Children.Add(ANT_F_bd1);
            #endregion
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

        private TextBlock RCS_PN_ttb = new TextBlock();
        private TextBox RCS_PN_tb = new TextBox();
        private Border RCS_PN_bd = new Border();
        private Border RCS_PN_bd1 = new Border();

        private TextBlock RCS_SS_ttb = new TextBlock();
        private TextBox RCS_SS_tb = new TextBox();
        private Border RCS_SS_bd = new Border();
        private Border RCS_SS_bd1 = new Border();

        private TextBlock RCS_SP_ttb = new TextBlock();
        private TextBox RCS_SP_tb = new TextBox();
        private Border RCS_SP_bd = new Border();
        private Border RCS_SP_bd1 = new Border();

        private TextBlock RCS_AR_ttb = new TextBlock();
        private TextBox RCS_AR_tb = new TextBox();
        private Border RCS_AR_bd = new Border();
        private Border RCS_AR_bd1 = new Border();

        private TextBlock RCS_FB_ttb = new TextBlock();
        private TextBox RCS_FB_tb = new TextBox();
        private Border RCS_FB_bd = new Border();
        private Border RCS_FB_bd1 = new Border();

        private TextBlock RCS_F_ttb = new TextBlock();
        private ComboBox RCS_F_cb = new ComboBox();
        private Border RCS_F_bd = new Border();
        private Border RCS_F_bd1 = new Border();

        public void RCS_Property_setup_s()
        {

            #region RCS PN
            RCS_PN_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(RCS_PN_tb);

            RCS_PN_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "RCS Polarization Number",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(RCS_PN_ttb);

            RCS_PN_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(RCS_PN_bd);
            RCS_PN_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(RCS_PN_bd1);
            #endregion

            #region RCS SS
            RCS_SS_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(RCS_SS_tb);

            RCS_SS_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "RCS Output Sample Size",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(RCS_SS_ttb);

            RCS_SS_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(RCS_SS_bd);
            RCS_SS_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(RCS_SS_bd1);
            #endregion

            #region RCS SP
            RCS_SP_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(RCS_SP_tb);

            RCS_SP_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "RCS Point",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(RCS_SP_ttb);

            RCS_SP_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(RCS_SP_bd);
            RCS_SP_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(RCS_SP_bd1);
            #endregion

            #region RCS AR
            RCS_AR_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(RCS_AR_tb);

            RCS_AR_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "RCS Angle Resolution",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(RCS_AR_ttb);

            RCS_AR_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(RCS_AR_bd);
            RCS_AR_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(RCS_AR_bd1);
            #endregion

            #region RCS FB
            RCS_FB_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(RCS_FB_tb);

            RCS_FB_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "RCS Frequency Bin Size",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(RCS_FB_ttb);

            RCS_FB_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(RCS_FB_bd);
            RCS_FB_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(RCS_FB_bd1);
            #endregion

            #region RCS F
            RCS_F_cb = new ComboBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Text = "RCS Fidelity",
                SelectedIndex = 0,
                Background = white_button_brush,
                Height = 35
            };
            RCS_F_cb.Items.Add("Order 0");
            RCS_F_cb.Items.Add("Order 1");
            RCS_F_cb.Items.Add("Order 2");
            RCS_F_cb.Items.Add("Order 3");
            RCS_F_cb.Items.Add("Order 4");
            RCS_F_cb.Items.Add("Order 5");
            RCS_F_cb.Items.Add("Order 6");

            DRBE_SPR.Children.Add(RCS_F_cb);

            RCS_F_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "RCS Fidelity",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(RCS_F_ttb);

            RCS_F_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(RCS_F_bd);
            RCS_F_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(RCS_F_bd1);
            #endregion

        }
        public void RCS_Property_setup()
        {

        }
        public void RCS_Property_hide_s()
        {
            DRBE_SPL.Children.Clear();
            DRBE_SPR.Children.Clear();
        }
        public void RCS_Property_show_s()
        {

            #region RCS PN

            DRBE_SPR.Children.Add(RCS_PN_tb);

            DRBE_SPL.Children.Add(RCS_PN_ttb);

            DRBE_SPL.Children.Add(RCS_PN_bd);

            DRBE_SPR.Children.Add(RCS_PN_bd1);
            #endregion

            #region RCS SS

            DRBE_SPR.Children.Add(RCS_SS_tb);

            DRBE_SPL.Children.Add(RCS_SS_ttb);

            DRBE_SPL.Children.Add(RCS_SS_bd);

            DRBE_SPR.Children.Add(RCS_SS_bd1);
            #endregion

            #region RCS SP

            DRBE_SPR.Children.Add(RCS_SP_tb);

            DRBE_SPL.Children.Add(RCS_SP_ttb);

            DRBE_SPL.Children.Add(RCS_SP_bd);

            DRBE_SPR.Children.Add(RCS_SP_bd1);
            #endregion

            #region RCS AR

            DRBE_SPR.Children.Add(RCS_AR_tb);

            DRBE_SPL.Children.Add(RCS_AR_ttb);
            DRBE_SPL.Children.Add(RCS_AR_bd);

            DRBE_SPR.Children.Add(RCS_AR_bd1);
            #endregion

            #region RCS FB

            DRBE_SPR.Children.Add(RCS_FB_tb);

            DRBE_SPL.Children.Add(RCS_FB_ttb);

            DRBE_SPL.Children.Add(RCS_FB_bd);

            DRBE_SPR.Children.Add(RCS_FB_bd1);
            #endregion

            #region RCS F

            DRBE_SPR.Children.Add(RCS_F_cb);


            DRBE_SPL.Children.Add(RCS_F_ttb);


            DRBE_SPL.Children.Add(RCS_F_bd);

            DRBE_SPR.Children.Add(RCS_F_bd1);
            #endregion
        }
        public void RCS_Property_hide()
        {

            #region RCS holder
            //RCS_holder1_tb.Visibility = Visibility.Collapsed;

            //RCS_holder1_ttb.Visibility = Visibility.Collapsed;
            #endregion
        }
        public void RCS_Property_show()
        {

            #region RCS holder
            //RCS_holder1_tb.Visibility = Visibility.Visible;

            //RCS_holder1_ttb.Visibility = Visibility.Visible;
            #endregion
        }

        #region clutter property
        private TextBlock Clut_Gamma_k_ttb = new TextBlock();
        private TextBox Clut_Gamma_k_tb = new TextBox();
        private Border Clut_Gamma_k_bd = new Border();
        private Border Clut_Gamma_k_bd1 = new Border();

        private TextBlock Clut_Gamma_theta_ttb = new TextBlock();
        private TextBox Clut_Gamma_theta_tb = new TextBox();
        private Border Clut_Gamma_theta_bd = new Border();
        private Border Clut_Gamma_theta_bd1 = new Border();

        private TextBlock Clut_Gausian_m_ttb = new TextBlock();
        private TextBox Clut_Gausian_m_tb = new TextBox();
        private Border Clut_Gausian_m_bd = new Border();
        private Border Clut_Gausian_m_bd1 = new Border();

        private TextBlock Clut_Gausian_v_ttb = new TextBlock();
        private TextBox Clut_Gausian_v_tb = new TextBox();
        private Border Clut_Gausian_v_bd = new Border();
        private Border Clut_Gausian_v_bd1 = new Border();

        private TextBlock CPI_ttb = new TextBlock();
        private TextBox CPI_tb = new TextBox();
        private Border CPI_bd = new Border();
        private Border CPI_bd1 = new Border();

        private TextBlock PRI_ttb = new TextBlock();
        private TextBox PRI_tb = new TextBox();
        private Border PRI_bd = new Border();
        private Border PRI_bd1 = new Border();
        #endregion
        public void RF_Clut_Property_setup_s()
        {

            #region Clutter Gamma K
            Clut_Gamma_k_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Clut_Gamma_k_tb);


            Clut_Gamma_k_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Clutter: Gamma \r\n Shape Parameter: k",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Clut_Gamma_k_ttb);

            Clut_Gamma_k_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Clut_Gamma_k_bd);
            Clut_Gamma_k_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Clut_Gamma_k_bd1);
            #endregion
            #region Clutter Gamma theta
            Clut_Gamma_theta_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Clut_Gamma_theta_tb);


            Clut_Gamma_theta_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Clutter: Gamma \r\n Shape Parameter: " + "\u0398",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Clut_Gamma_theta_ttb);

            Clut_Gamma_theta_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Clut_Gamma_theta_bd);
            Clut_Gamma_theta_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Clut_Gamma_theta_bd1);
            #endregion

            #region Clutter Gausian m
            Clut_Gausian_m_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Clut_Gausian_m_tb);


            Clut_Gausian_m_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Clutter Gausian: \r\n mean",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Clut_Gausian_m_ttb);

            Clut_Gausian_m_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Clut_Gausian_m_bd);
            Clut_Gausian_m_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Clut_Gausian_m_bd1);
            #endregion
            #region Clutter Gausian Variant
            Clut_Gausian_v_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Clut_Gausian_v_tb);


            Clut_Gausian_v_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                //Text = "Clutter Gausian: \r\n variant",
                Text = "Clutter ring size: ",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Clut_Gausian_v_ttb);

            Clut_Gausian_v_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Clut_Gausian_v_bd);
            Clut_Gausian_v_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Clut_Gausian_v_bd1);
            #endregion
        }
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
        public void RF_Clut_Property_hide_s()
        {
            DRBE_SPL.Children.Clear();
            DRBE_SPR.Children.Clear();
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
        public void RF_Clut_Property_show_s()
        {

            #region Clutter Gamma K
            DRBE_SPR.Children.Add(Clut_Gamma_k_tb);
            DRBE_SPL.Children.Add(Clut_Gamma_k_ttb);
            DRBE_SPL.Children.Add(Clut_Gamma_k_bd);
            DRBE_SPR.Children.Add(Clut_Gamma_k_bd1);
            #endregion
            #region Clutter Gamma theta
            DRBE_SPR.Children.Add(Clut_Gamma_theta_tb);
            DRBE_SPL.Children.Add(Clut_Gamma_theta_ttb);
            DRBE_SPL.Children.Add(Clut_Gamma_theta_bd);
            DRBE_SPR.Children.Add(Clut_Gamma_theta_bd1);
            #endregion

            #region Clutter Gausian m
            DRBE_SPR.Children.Add(Clut_Gausian_m_tb);
            DRBE_SPL.Children.Add(Clut_Gausian_m_ttb);
            DRBE_SPL.Children.Add(Clut_Gausian_m_bd);
            DRBE_SPR.Children.Add(Clut_Gausian_m_bd1);
            #endregion
            #region Clutter Gausian Variant
            DRBE_SPR.Children.Add(Clut_Gausian_v_tb);
            DRBE_SPL.Children.Add(Clut_Gausian_v_ttb);
            DRBE_SPL.Children.Add(Clut_Gausian_v_bd);
            DRBE_SPR.Children.Add(Clut_Gausian_v_bd1);
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

        #region constraint property
        private TextBlock Obj_centerX_ttb = new TextBlock();
        private TextBox Obj_centerX_tb = new TextBox();
        private Border Obj_centerX_bd = new Border();
        private Border Obj_centerX_bd1 = new Border();

        private TextBlock Obj_centerY_ttb = new TextBlock();
        private TextBox Obj_centerY_tb = new TextBox();
        private Border Obj_centerY_bd = new Border();
        private Border Obj_centerY_bd1 = new Border();

        private TextBlock Obj_centerZ_ttb = new TextBlock();
        private TextBox Obj_centerZ_tb = new TextBox();
        private Border Obj_centerZ_bd = new Border();
        private Border Obj_centerZ_bd1 = new Border();

        private TextBlock Obj_max_height_ttb = new TextBlock();
        private TextBox Obj_max_height_tb = new TextBox();
        private Border Obj_max_height_bd = new Border();
        private Border Obj_max_height_bd1 = new Border();

        private TextBlock Obj_radius_ttb = new TextBlock();
        private TextBox Obj_radius_tb = new TextBox();
        private Border Obj_radius_bd = new Border();
        private Border Obj_radius_bd1 = new Border();

        private TextBlock Obj_max_speed_ttb = new TextBlock();
        private TextBox Obj_max_speed_tb = new TextBox();
        private Border Obj_max_speed_bd = new Border();
        private Border Obj_max_speed_bd1 = new Border();

        private TextBlock Obj_max_acc_ttb = new TextBlock();
        private TextBox Obj_max_acc_tb = new TextBox();
        private Border Obj_max_acc_bd = new Border();
        private Border Obj_max_acc_bd1 = new Border();

        private TextBlock Obj_max_ori_ttb = new TextBlock();
        private TextBox Obj_max_ori_tb = new TextBox();
        private Border Obj_max_ori_bd = new Border();
        private Border Obj_max_ori_bd1 = new Border();

        private TextBlock Obj_min_band_ttb = new TextBlock();
        private TextBox Obj_min_band_tb = new TextBox();
        private Border Obj_min_band_bd = new Border();
        private Border Obj_min_band_bd1 = new Border();

        private TextBlock Obj_max_band_ttb = new TextBlock();
        private TextBox Obj_max_band_tb = new TextBox();
        private Border Obj_max_band_bd = new Border();
        private Border Obj_max_band_bd1 = new Border();

        private TextBlock Obj_max_bandchg_ttb = new TextBlock();
        private TextBox Obj_max_bandchg_tb = new TextBox();
        private Border Obj_max_bandchg_bd = new Border();
        private Border Obj_max_bandchg_bd1 = new Border();

        private TextBlock Obj_min_centerfreq_ttb = new TextBlock();
        private TextBox Obj_min_centerfreq_tb = new TextBox();
        private Border Obj_min_centerfreq_bd = new Border();
        private Border Obj_min_centerfreq_bd1 = new Border();

        private TextBlock Obj_max_centerfreq_ttb = new TextBlock();
        private TextBox Obj_max_centerfreq_tb = new TextBox();
        private Border Obj_max_centerfreq_bd = new Border();
        private Border Obj_max_centerfreq_bd1 = new Border();

        private TextBlock Obj_min_CPI_ttb = new TextBlock();
        private TextBox Obj_min_CPI_tb = new TextBox();
        private Border Obj_min_CPI_bd = new Border();
        private Border Obj_min_CPI_bd1 = new Border();

        private TextBlock Obj_max_CPI_ttb = new TextBlock();
        private TextBox Obj_max_CPI_tb = new TextBox();
        private Border Obj_max_CPI_bd = new Border();
        private Border Obj_max_CPI_bd1 = new Border();

        private TextBlock Obj_min_PRI_ttb = new TextBlock();
        private TextBox Obj_min_PRI_tb = new TextBox();
        private Border Obj_min_PRI_bd = new Border();
        private Border Obj_min_PRI_bd1 = new Border();

        private TextBlock Obj_max_PRI_ttb = new TextBlock();
        private TextBox Obj_max_PRI_tb = new TextBox();
        private Border Obj_max_PRI_bd = new Border();
        private Border Obj_max_PRI_bd1 = new Border();
        #endregion
        public void Constraint_Property_setup_s()
        {
            #region Object Center X
            Obj_centerX_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Obj_centerX_tb);


            Obj_centerX_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Center of movement X",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Obj_centerX_ttb);

            Obj_centerX_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Obj_centerX_bd);
            Obj_centerX_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Obj_centerX_bd1);
            #endregion

            #region Object Center Y
            Obj_centerY_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Obj_centerY_tb);


            Obj_centerY_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Center of movement Y",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Obj_centerY_ttb);

            Obj_centerY_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Obj_centerY_bd);
            Obj_centerY_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Obj_centerY_bd1);
            #endregion

            #region Object Center Z
            Obj_centerZ_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Obj_centerZ_tb);


            Obj_centerZ_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Center of movement Z",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Obj_centerZ_ttb);

            Obj_centerZ_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Obj_centerZ_bd);
            Obj_centerZ_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Obj_centerZ_bd1);
            #endregion

            #region Object max height 
            Obj_max_height_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Obj_max_height_tb);


            Obj_max_height_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Max height",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Obj_max_height_ttb);

            Obj_max_height_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Obj_max_height_bd);
            Obj_max_height_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Obj_max_height_bd1);
            #endregion

            #region Object Radius 
            Obj_radius_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Obj_radius_tb);


            Obj_radius_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Radius of movement",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Obj_radius_ttb);

            Obj_radius_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Obj_radius_bd);
            Obj_radius_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Obj_radius_bd1);
            #endregion

            #region Object max speed
            Obj_max_speed_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Obj_max_speed_tb);


            Obj_max_speed_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                //Text = "Center of movement X",
                Text = "Maximum Length of Obj",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Obj_max_speed_ttb);

            Obj_max_speed_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Obj_max_speed_bd);
            Obj_max_speed_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Obj_max_speed_bd1);
            #endregion

            #region Object max acceleration
            Obj_max_acc_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Obj_max_acc_tb);


            Obj_max_acc_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Maximum acceleration",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Obj_max_acc_ttb);

            Obj_max_acc_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Obj_max_acc_bd);
            Obj_max_acc_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Obj_max_acc_bd1);
            #endregion

            #region Object max turn Angle
            Obj_max_ori_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Obj_max_ori_tb);


            Obj_max_ori_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Maximum turn Angle: \r\n Degree\\Second",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Obj_max_ori_ttb);

            Obj_max_ori_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Obj_max_ori_bd);
            Obj_max_ori_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Obj_max_ori_bd1);
            #endregion

            #region Object min bandwidth
            Obj_min_band_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Obj_min_band_tb);


            Obj_min_band_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Minimum bandwidth",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Obj_min_band_ttb);

            Obj_min_band_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Obj_min_band_bd);
            Obj_min_band_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Obj_min_band_bd1);
            #endregion

            #region Object max bandwidth
            Obj_max_band_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Obj_max_band_tb);


            Obj_max_band_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Maximum bandwidth",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Obj_max_band_ttb);

            Obj_max_band_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Obj_max_band_bd);
            Obj_max_band_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Obj_max_band_bd1);
            #endregion

            #region Object max bandwidth change
            Obj_max_bandchg_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = Number_of_Receiver.ToString(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPR.Children.Add(Obj_max_bandchg_tb);


            Obj_max_bandchg_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Max bandwidth chg: \r\n Hz\\ms",
                Foreground = white_button_brush,
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Height = 35
            };
            DRBE_SPL.Children.Add(Obj_max_bandchg_ttb);

            Obj_max_bandchg_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Obj_max_bandchg_bd);
            Obj_max_bandchg_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Obj_max_bandchg_bd1);
            #endregion
        }
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
        public void Constraint_Property_hide_s()
        {
            DRBE_SPL.Children.Clear();
            DRBE_SPR.Children.Clear();
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
        public void Constraint_Property_show_s()
        {
            #region Object Center X
            DRBE_SPR.Children.Add(Obj_centerX_tb);
            DRBE_SPL.Children.Add(Obj_centerX_ttb);
            DRBE_SPL.Children.Add(Obj_centerX_bd);
            DRBE_SPR.Children.Add(Obj_centerX_bd1);
            #endregion

            #region Object Center Y
            DRBE_SPR.Children.Add(Obj_centerY_tb);
            DRBE_SPL.Children.Add(Obj_centerY_ttb);
            DRBE_SPL.Children.Add(Obj_centerY_bd);
            DRBE_SPR.Children.Add(Obj_centerY_bd1);
            #endregion

            #region Object Center Z
            DRBE_SPR.Children.Add(Obj_centerZ_tb);
            DRBE_SPL.Children.Add(Obj_centerZ_ttb);
            DRBE_SPL.Children.Add(Obj_centerZ_bd);
            DRBE_SPR.Children.Add(Obj_centerZ_bd1);
            #endregion

            #region Object max height 
            DRBE_SPR.Children.Add(Obj_max_height_tb);
            DRBE_SPL.Children.Add(Obj_max_height_ttb);
            DRBE_SPL.Children.Add(Obj_max_height_bd);
            DRBE_SPR.Children.Add(Obj_max_height_bd1);
            #endregion

            #region Object Radius 
            DRBE_SPR.Children.Add(Obj_radius_tb);
            DRBE_SPL.Children.Add(Obj_radius_ttb);
            DRBE_SPL.Children.Add(Obj_radius_bd);
            DRBE_SPR.Children.Add(Obj_radius_bd1);
            #endregion

            #region Object max speed
            DRBE_SPR.Children.Add(Obj_max_speed_tb);
            DRBE_SPL.Children.Add(Obj_max_speed_ttb);
            DRBE_SPL.Children.Add(Obj_max_speed_bd);
            DRBE_SPR.Children.Add(Obj_max_speed_bd1);
            #endregion

            #region Object max acceleration
            DRBE_SPR.Children.Add(Obj_max_acc_tb);
            DRBE_SPL.Children.Add(Obj_max_acc_ttb);
            DRBE_SPL.Children.Add(Obj_max_acc_bd);
            DRBE_SPR.Children.Add(Obj_max_acc_bd1);
            #endregion

            #region Object max turn Angle
            DRBE_SPR.Children.Add(Obj_max_ori_tb);
            DRBE_SPL.Children.Add(Obj_max_ori_ttb);
            DRBE_SPL.Children.Add(Obj_max_ori_bd);
            DRBE_SPR.Children.Add(Obj_max_ori_bd1);
            #endregion

            #region Object min bandwidth
            DRBE_SPR.Children.Add(Obj_min_band_tb);
            DRBE_SPL.Children.Add(Obj_min_band_ttb);
            DRBE_SPL.Children.Add(Obj_min_band_bd);
            DRBE_SPR.Children.Add(Obj_min_band_bd1);
            #endregion

            #region Object max bandwidth
            DRBE_SPR.Children.Add(Obj_max_band_tb);
            DRBE_SPL.Children.Add(Obj_max_band_ttb);
            DRBE_SPL.Children.Add(Obj_max_band_bd);
            DRBE_SPR.Children.Add(Obj_max_band_bd1);
            #endregion

            #region Object max bandwidth change
            DRBE_SPR.Children.Add(Obj_max_bandchg_tb);
            DRBE_SPL.Children.Add(Obj_max_bandchg_ttb);
            DRBE_SPL.Children.Add(Obj_max_bandchg_bd);
            DRBE_SPR.Children.Add(Obj_max_bandchg_bd1);
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
        private ComboBox Attach_lut_cb = new ComboBox();
        private Border Attach_lut_bd = new Border();
        private Border Attach_lut_bd1 = new Border();
        public void Lookup_table_setup_s()
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
                FontSize = 10,
                Height = 35
            };
            DRBE_SPL.Children.Add(Attach_lut_bt);
            //Attach_lut_bt.Click += Antenna_Pol_Property_bt_Click;

            Attach_lut_cb = new ComboBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                SelectedIndex = 0,
                Background = white_button_brush,
                Height = 35
            };
            DRBE_SPR.Children.Add(Attach_lut_cb);
            Attach_lut_cb.Items.Add("Selection");

            Attach_lut_bd = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPL.Children.Add(Attach_lut_bd);
            Attach_lut_bd1 = new Border()
            {
                Height = 5,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            DRBE_SPR.Children.Add(Attach_lut_bd1);
        }
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

            Attach_lut_cb = new ComboBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                SelectedIndex = 0,
                Background = white_button_brush
            };
            //ParentGrid.Children.Add(Attach_lut_cb);
            Attach_lut_cb.SetValue(Grid.ColumnProperty, 180);
            Attach_lut_cb.SetValue(Grid.ColumnSpanProperty, 15);
            Attach_lut_cb.SetValue(Grid.RowProperty, 31);
            Attach_lut_cb.SetValue(Grid.RowSpanProperty, 4);
            Attach_lut_cb.Items.Add("Selection");
        }
        public void Lookup_table_hide_s()
        {
            DRBE_SPL.Children.Clear();
            DRBE_SPR.Children.Clear();
        }
        public void Lookup_table_hide()
        {
            Attach_lut_bt.Visibility = Visibility.Collapsed;
        }
        public void Lookup_table_show_s()
        {
            DRBE_SPL.Children.Add(Attach_lut_bt);
            DRBE_SPR.Children.Add(Attach_lut_cb);
            Attach_lut_cb.Items.Add("Selection");
            DRBE_SPL.Children.Add(Attach_lut_bd);
            DRBE_SPR.Children.Add(Attach_lut_bd1);
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
