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

    public class FrontPage
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

        public TextBlock Statues_tb = new TextBlock();

        public Button Tab_control_tb = new Button();
        public Button Tab_software_tb = new Button();
        public Button Tab_core_tb = new Button();
        public Button Tab_graph_tb = new Button();
        public Button Tab_workspace_tb = new Button();

        public Button Send_Seq_1 = new Button();
        public Button Send_Seq_2 = new Button();
        public Button Send_Seq_3 = new Button();
        public Button Send_Seq_4 = new Button();




        private Button Com_Message_bt = new Button();
        private Image Com_Message_bti = new Image();
        private ToolTip Com_Message_tt = new ToolTip();


        public Border Statues_bd = new Border();



        public Grid ParentGrid;
        public ControlPanel DRBE_controlpanel;
        public FrontPage(Grid parent)
        {
            ParentGrid = parent;
            DRBE_controlpanel = new ControlPanel(ParentGrid);
            DRBE_controlpanel.Hide();
            Setup();
            Hide();


        }

        private void Setup()
        {
            Send_Seq_1 = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 12,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Content = "Seq 1",
                BorderBrush = white_button_brush,
                Background = Default_back_black_color_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Send_Seq_1.SetValue(Grid.ColumnProperty, 136);
            Send_Seq_1.SetValue(Grid.ColumnSpanProperty, 20);
            Send_Seq_1.SetValue(Grid.RowProperty, 70);
            Send_Seq_1.SetValue(Grid.RowSpanProperty, 5);
            ParentGrid.Children.Add(Send_Seq_1);
            //Canvas.SetZIndex(Send_Seq_1,5);

            Tab_workspace_tb = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Content = "Workspace",
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Tab_workspace_tb.SetValue(Grid.ColumnProperty, 140);
            Tab_workspace_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Tab_workspace_tb.SetValue(Grid.RowProperty, 10);
            Tab_workspace_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Tab_workspace_tb);


            Tab_graph_tb = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Content = "Graphics",
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Tab_graph_tb.SetValue(Grid.ColumnProperty, 120);
            Tab_graph_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Tab_graph_tb.SetValue(Grid.RowProperty, 10);
            Tab_graph_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Tab_graph_tb);

            Tab_core_tb = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Content = "Cores",
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Tab_core_tb.SetValue(Grid.ColumnProperty, 100);
            Tab_core_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Tab_core_tb.SetValue(Grid.RowProperty, 10);
            Tab_core_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Tab_core_tb);

            Tab_software_tb = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Content = "Software",
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2)
            };
            Tab_software_tb.SetValue(Grid.ColumnProperty, 80);
            Tab_software_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Tab_software_tb.SetValue(Grid.RowProperty, 10);
            Tab_software_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Tab_software_tb);

            Tab_control_tb = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                FontSize = 20,
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Content = "Control Panel",
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2,2,2,2)
            };
            Tab_control_tb.SetValue(Grid.ColumnProperty, 60);
            Tab_control_tb.SetValue(Grid.ColumnSpanProperty, 20);
            Tab_control_tb.SetValue(Grid.RowProperty, 10);
            Tab_control_tb.SetValue(Grid.RowSpanProperty, 8);
            ParentGrid.Children.Add(Tab_control_tb);

            Statues_bd = new Border()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.1, 0.1, 0.1, 0.1),
            };
            Canvas.SetZIndex(Statues_bd,-1);
            Statues_bd.SetValue(Grid.ColumnProperty, 0);
            Statues_bd.SetValue(Grid.ColumnSpanProperty, 60);
            Statues_bd.SetValue(Grid.RowProperty, 0);
            Statues_bd.SetValue(Grid.RowSpanProperty, 150);
            ParentGrid.Children.Add(Statues_bd);

            //Com_Message_bti.Source = new BitmapImage(new Uri("ms-appx://Assets/cnull.PNG", UriKind.RelativeOrAbsolute));
            Com_Message_bti.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Com_Icon.jpg", UriKind.RelativeOrAbsolute));
            Com_Message_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Content = Com_Message_bti,
                Background = Default_back_black_color_brush
            };
            Com_Message_bt.SetValue(Grid.ColumnProperty, 1);
            Com_Message_bt.SetValue(Grid.ColumnSpanProperty, 8);
            Com_Message_bt.SetValue(Grid.RowProperty, 1);
            Com_Message_bt.SetValue(Grid.RowSpanProperty, 10);
            ParentGrid.Children.Add(Com_Message_bt);

            Com_Message_tt.Content = "Show COM Message";
            ToolTipService.SetToolTip(Com_Message_bt, Com_Message_tt);

            Canvas.SetZIndex(Com_Message_bt,20);
            Com_Message_bt.Click += Com_Message_bt_Click;
            Statues_tb = new TextBlock() { 
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                TextWrapping = TextWrapping.WrapWholeWords,
                FontSize = 12,
                Foreground = white_button_brush,
                Text = "DRBE_Debug_tb\r\n"
            };
            Statues_tb.SetValue(Grid.ColumnProperty, 1);
            Statues_tb.SetValue(Grid.ColumnSpanProperty, 59);
            Statues_tb.SetValue(Grid.RowProperty, 11);
            Statues_tb.SetValue(Grid.RowSpanProperty, 139);
            ParentGrid.Children.Add(Statues_tb);
        }

        private void Com_Message_bt_Click(object sender, RoutedEventArgs e)
        {
            if(Statues_tb.Visibility == Visibility.Visible)
            {
                Statues_tb.Visibility = Visibility.Collapsed;
                Statues_bd.Visibility = Visibility.Collapsed;
            }else
            {
                Statues_tb.Visibility = Visibility.Visible;
                Statues_bd.Visibility = Visibility.Visible;
            }
        }

        public void Show()
        {
            Send_Seq_1.Visibility = Visibility.Visible;

            Tab_workspace_tb.Visibility = Visibility.Visible;


            Tab_graph_tb.Visibility = Visibility.Visible;

            Tab_core_tb.Visibility = Visibility.Visible;

            Tab_software_tb.Visibility = Visibility.Visible;

            Tab_control_tb.Visibility = Visibility.Visible;

            Statues_bd.Visibility = Visibility.Visible;
            Com_Message_bt.Visibility = Visibility.Visible;
            Statues_tb.Visibility = Visibility.Visible;

        }

        public void Hide()
        {
            Send_Seq_1.Visibility = Visibility.Collapsed;

            Tab_workspace_tb.Visibility = Visibility.Collapsed;


            Tab_graph_tb.Visibility = Visibility.Collapsed;

            Tab_core_tb.Visibility = Visibility.Collapsed;

            Tab_software_tb.Visibility = Visibility.Collapsed;

            Tab_control_tb.Visibility = Visibility.Collapsed;

            Statues_bd.Visibility = Visibility.Collapsed;
            Com_Message_bt.Visibility = Visibility.Collapsed;
            Statues_tb.Visibility = Visibility.Collapsed;
        }

        public void Dispose()
        {

        }
    }
}
