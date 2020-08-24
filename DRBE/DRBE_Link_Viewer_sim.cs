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

namespace DRBE.Assets
{
    public class DRBE_Link_Viewer_sim
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
        public DRBE_Link_Viewer_sim(Grid parent)
        {
            ParentGrid = parent;

            //Setup();
            //hide();
        }

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

        public void Setup(List<DRBE_Transmitter> DT, List<DRBE_Receiver> DR, List<DRBE_Reflector> DRF)
        {
            int i = 0;

            Simulation_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/surfaceplot_CUTS.png", UriKind.RelativeOrAbsolute));
            Simulation_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Simulation_i,
                Foreground = white_button_brush,
                Padding = new Thickness(15, 15, 15, 15),
                FontSize = 18
            };
            ParentGrid.Children.Add(Simulation_bt);
            Simulation_bt.SetValue(Grid.ColumnProperty, 130);
            Simulation_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Simulation_bt.SetValue(Grid.RowProperty, 125);
            Simulation_bt.SetValue(Grid.RowSpanProperty, 15);

            Text_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = DT[0].Summary(),
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Text_tb);
            Text_tb.SetValue(Grid.ColumnProperty, 130);
            Text_tb.SetValue(Grid.ColumnSpanProperty, 50);
            Text_tb.SetValue(Grid.RowProperty, 20);
            Text_tb.SetValue(Grid.RowSpanProperty, 100);


            #region Order

            Ref_AO1_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,

                Background = Default_back_black_color_brush,
                Content = "Order 1:  Not Selected",
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                FontSize = 18
            };
            ParentGrid.Children.Add(Ref_AO1_bt);
            Ref_AO1_bt.SetValue(Grid.ColumnProperty, 95);
            Ref_AO1_bt.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO1_bt.SetValue(Grid.RowProperty, 30);
            Ref_AO1_bt.SetValue(Grid.RowSpanProperty, 5);

            Ref_AO1_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Requirement",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Ref_AO1_tb);
            Ref_AO1_tb.SetValue(Grid.ColumnProperty, 95);
            Ref_AO1_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO1_tb.SetValue(Grid.RowProperty, 35);
            Ref_AO1_tb.SetValue(Grid.RowSpanProperty, 25);

            Ref_AO2_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,

                Background = Default_back_black_color_brush,
                Content = "Order 2:  Not Selected",
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                FontSize = 18
            };
            ParentGrid.Children.Add(Ref_AO2_bt);
            Ref_AO2_bt.SetValue(Grid.ColumnProperty, 95);
            Ref_AO2_bt.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO2_bt.SetValue(Grid.RowProperty, 60);
            Ref_AO2_bt.SetValue(Grid.RowSpanProperty, 5);

            Ref_AO2_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Requirement",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Ref_AO2_tb);
            Ref_AO2_tb.SetValue(Grid.ColumnProperty, 95);
            Ref_AO2_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO2_tb.SetValue(Grid.RowProperty, 65);
            Ref_AO2_tb.SetValue(Grid.RowSpanProperty, 25);

            Ref_AO3_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,

                Background = Default_back_black_color_brush,
                Content = "Order 3:  Not Selected",
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                FontSize = 18
            };
            ParentGrid.Children.Add(Ref_AO3_bt);
            Ref_AO3_bt.SetValue(Grid.ColumnProperty, 95);
            Ref_AO3_bt.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO3_bt.SetValue(Grid.RowProperty, 90);
            Ref_AO3_bt.SetValue(Grid.RowSpanProperty, 5);

            Ref_AO3_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Requirement",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Ref_AO3_tb);
            Ref_AO3_tb.SetValue(Grid.ColumnProperty, 95);
            Ref_AO3_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO3_tb.SetValue(Grid.RowProperty, 95);
            Ref_AO3_tb.SetValue(Grid.RowSpanProperty, 25);

            Ref_AO4_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,

                Background = Default_back_black_color_brush,
                Content = "Order 4:  Selected",
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                BorderBrush = green_bright_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                FontSize = 18
            };
            ParentGrid.Children.Add(Ref_AO4_bt);
            Ref_AO4_bt.SetValue(Grid.ColumnProperty, 95);
            Ref_AO4_bt.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO4_bt.SetValue(Grid.RowProperty, 120);
            Ref_AO4_bt.SetValue(Grid.RowSpanProperty, 5);

            Ref_AO4_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Requirement: ",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Ref_AO4_tb);
            Ref_AO4_tb.SetValue(Grid.ColumnProperty, 95);
            Ref_AO4_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO4_tb.SetValue(Grid.RowProperty, 125);
            Ref_AO4_tb.SetValue(Grid.RowSpanProperty, 25);

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
                    //Padding = new Thickness(15, 15, 15, 15),
                    FontSize = 18
                });
                ParentGrid.Children.Add(DT_bt[DT_i.Count - 1]);
                DT_bt[DT_i.Count - 1].SetValue(Grid.ColumnProperty, 10);
                DT_bt[DT_i.Count - 1].SetValue(Grid.ColumnSpanProperty, 10);
                DT_bt[DT_i.Count - 1].SetValue(Grid.RowProperty, 30 + bt_height * i + tb_height * i);
                DT_bt[DT_i.Count - 1].SetValue(Grid.RowSpanProperty, 10);
                D_BT_DT[DT_bt[DT_i.Count - 1]] = DT[i];

                DT_tb.Add(new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = 14,
                    FontWeight = FontWeights.Bold,
                    Foreground = white_button_brush

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
                    //Padding = new Thickness(15, 15, 15, 15),
                    FontSize = 18
                });
                ParentGrid.Children.Add(DF_bt[DF_i.Count - 1]);
                DF_bt[DF_i.Count - 1].SetValue(Grid.ColumnProperty, 30);
                DF_bt[DF_i.Count - 1].SetValue(Grid.ColumnSpanProperty, 10);
                DF_bt[DF_i.Count - 1].SetValue(Grid.RowProperty, 30 + bt_height * i + tb_height * i);
                DF_bt[DF_i.Count - 1].SetValue(Grid.RowSpanProperty, 10);
                D_BT_DRF[DF_bt[DF_i.Count - 1]] = DRF[i];

                DF_tb.Add(new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = 14,
                    FontWeight = FontWeights.Bold,
                    Foreground = white_button_brush

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
                    //Padding = new Thickness(15, 15, 15, 15),
                    FontSize = 18
                });
                ParentGrid.Children.Add(DR_bt[DR_i.Count - 1]);
                DR_bt[DR_i.Count - 1].SetValue(Grid.ColumnProperty, 50);
                DR_bt[DR_i.Count - 1].SetValue(Grid.ColumnSpanProperty, 10);
                DR_bt[DR_i.Count - 1].SetValue(Grid.RowProperty, 30 + bt_height * i + tb_height * i);
                DR_bt[DR_i.Count - 1].SetValue(Grid.RowSpanProperty, 10);
                D_BT_DR[DT_bt[DR_i.Count - 1]] = DR[i];

                DR_tb.Add(new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = 14,
                    FontWeight = FontWeights.Bold,
                    Foreground = white_button_brush

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
            Ref_Antenna_bt.SetValue(Grid.ColumnProperty, 70);
            Ref_Antenna_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Ref_Antenna_bt.SetValue(Grid.RowProperty, 30);
            Ref_Antenna_bt.SetValue(Grid.RowSpanProperty, 15);

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
            Ref_Doppler_bt.SetValue(Grid.ColumnProperty, 70);
            Ref_Doppler_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Ref_Doppler_bt.SetValue(Grid.RowProperty, 50);
            Ref_Doppler_bt.SetValue(Grid.RowSpanProperty, 15);

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
            Ref_Polarization_bt.SetValue(Grid.ColumnProperty, 70);
            Ref_Polarization_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Ref_Polarization_bt.SetValue(Grid.RowProperty, 70);
            Ref_Polarization_bt.SetValue(Grid.RowSpanProperty, 15);

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
            Ref_RCS_bt.SetValue(Grid.ColumnProperty, 70);
            Ref_RCS_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Ref_RCS_bt.SetValue(Grid.RowProperty, 90);
            Ref_RCS_bt.SetValue(Grid.RowSpanProperty, 15);

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
            Ref_RFim_bt.SetValue(Grid.ColumnProperty, 70);
            Ref_RFim_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Ref_RFim_bt.SetValue(Grid.RowProperty, 110);
            Ref_RFim_bt.SetValue(Grid.RowSpanProperty, 15);

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
            Ref_Clutter_bt.SetValue(Grid.ColumnProperty, 70);
            Ref_Clutter_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Ref_Clutter_bt.SetValue(Grid.RowProperty, 130);
            Ref_Clutter_bt.SetValue(Grid.RowSpanProperty, 15);
            #endregion

            //DT_bt[2].BorderBrush = green_bright_button_brush;
            //DF_bt[5].BorderBrush = green_bright_button_brush;
            //DR_bt[1].BorderBrush = green_bright_button_brush;
            Ref_Antenna_bt.BorderBrush = green_bright_button_brush;
        }
        public void hide()
        {
            int i = 0;

            Simulation_bt.Visibility = Visibility.Collapsed;

            Text_tb.Visibility = Visibility.Collapsed;
            Ref_AO1_bt.Visibility = Visibility.Collapsed;

            Ref_AO1_tb.Visibility = Visibility.Collapsed;

            Ref_AO2_bt.Visibility = Visibility.Collapsed;

            Ref_AO2_tb.Visibility = Visibility.Collapsed;

            Ref_AO3_bt.Visibility = Visibility.Collapsed;

            Ref_AO3_tb.Visibility = Visibility.Collapsed;

            Ref_AO4_bt.Visibility = Visibility.Collapsed;

            Ref_AO4_tb.Visibility = Visibility.Collapsed;


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

    }
}
