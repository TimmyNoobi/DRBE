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

using System.Diagnostics;
using Windows.Networking.Sockets;
using Windows.Media.Core;
using Windows.ApplicationModel.Background;

namespace DRBE
{
    public class Simulator_V1
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
        private SolidColorBrush sandy_brown_brush = new SolidColorBrush(Color.FromArgb((byte)255, (byte)244, (byte)164, (byte)96));
        private Brush textwhite = new SolidColorBrush(Color.FromArgb((byte)255, (byte)250, (byte)250, (byte)250));
        private List<SolidColorBrush> All_Color = new List<SolidColorBrush>();
        #endregion
        public Grid ParentGrid;
        public MainPage ParentPage;
        private Save_Screen DRBE_SS;
        //public DRBE_Link_Viewer_s SC_Dlv;
        //public DRBE_LinkViewer Link_Viewer;
        private Random rand = new Random();

        public Simulator_V1(Grid parent, MainPage parentpage)
        {
            ParentGrid = parent;
            ParentPage = parentpage;
            //hide();
            //Link_Viewer = new DRBE_LinkViewer(parent, ParentPage);
            Setup();
            hide();
            
        }
        private async Task<string> Dummy_signal_gen(int prf, double duty)
        {
            string result = "";
            double tap_len = 3300000000;
            double repeatlength = (int) (tap_len / prf);
            double dutylength =  (int) (repeatlength * duty);


            int i = 0;
            i = 0;
            while(i< prf)
            {

                result += "{";
                result += (i * repeatlength).ToString();
                result += ",";
                result += dutylength.ToString();
                result += "}";
                i++;
            }

            return result;
        }
        private async Task<string> Dummy_coefficient_gen(int interactn, int objn, double mean, double stdDev)
        {
            int i = 0;
            int ii = 0;
            double tap_len = 3300000000;
            
            double u1 = 0;
            double u2 = 0;
            double randStdNormal = 0;
            double randNormal = 0;

            string result = "";
            ii = 0;
            while (ii < interactn)
            {
                result += "{";
                List<double> timeline = new List<double>();
                List<double> coef = new List<double>();
                i = 0;
                while (i < objn)
                {
                    timeline.Add(Math.Ceiling(tap_len * rand.NextDouble()));

                    i++;
                }
                timeline.Add(tap_len);
                timeline.Sort();
                i = 0;
                while (i < objn)
                {

                    u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
                    u2 = 1.0 - rand.NextDouble();
                    randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                 Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
                    randNormal = Math.Ceiling(Math.Abs(mean + stdDev * randStdNormal)); //random normal(mean,stdDev^2)

                    coef.Add(randNormal);

                    i++;
                }

                i = 0;
                while (i < objn)
                {
                    result += "{";
                    if ((timeline[i + 1] - timeline[i]) < coef[i])
                    {
                        coef[i] = timeline[i + 1] - timeline[i];
                    }
                    result += timeline[i].ToString();
                    result += ",";
                    result += coef[i].ToString();
                    result += "}";
                    i++;
                }
                result += "}";
                ii++;
            }
           

            await ShowDialog("message", result);


            return result;
        }





        private async void Test_bt_Click(object sender, RoutedEventArgs e)
        {
            DRBE_SS = new Save_Screen(ParentGrid);
            await DRBE_SS.Start("Save Scenario", new List<string>() { "Simulator File", "Coefficient" }, "cco", await Dummy_coefficient_gen(136, 8, 200, 50));
            await Task.Delay(500);
            await DRBE_SS.Start("Save Scenario", new List<string>() { "Simulator File", "Signal" }, "cso", await Dummy_signal_gen(4000, 0.2));
            await Task.Delay(500);
        }
        private Button Test_bt = new Button();

        #region GE blocks bt
        private Button Geodetic_bt = new Button();
        private Button Location_update_bt = new Button();
        private Button Location_memory_bt = new Button();
        private Button NR_bt = new Button();
        private Button NR_memory_bt = new Button();
        private Button Relative_transform_bt = new Button();
        private Button Path_tu_bt = new Button();
        private Button Ant_memory_bt = new Button();
        private Button Ant_process_bt = new Button();
        private Button Ant_gain_bt = new Button();
        private Button Path_cal_bt = new Button();
        private Button Path_gain_bt = new Button();
        private Button RCS_cal_bt = new Button();
        private Button RCS_memory_bt = new Button();
        private Button Fractional_bt = new Button();
        private Button Combine_bt = new Button();
        private Button FIR_location_bt = new Button();
        private Button FIR_tap_bt = new Button();
        private Button SUT_input_bt = new Button();
        private Button SUT_preproc_bt = new Button();
        private Button PPU_bt = new Button();

        #endregion
        private Canvas GE_drawboard = new Canvas();
        private Polyline Geodetic_gon = new Polyline();

        private int C_offset = 0;
        private int R_offset = 0;

        private void Setup()
        {
            Test_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Background = Default_back_black_color_brush,
                Content = "Generate",
                Foreground = white_button_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5),
                BorderBrush = dark_grey_brush,
                FontSize = 20
            };
            ParentGrid.Children.Add(Test_bt);
            Test_bt.SetValue(Grid.ColumnProperty, 110);
            Test_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Test_bt.SetValue(Grid.RowProperty, 10);
            Test_bt.SetValue(Grid.RowSpanProperty, 7);
            Test_bt.Click += Test_bt_Click;

            GE_drawboard = new Canvas() { 
                Background = Default_back_black_color_brush,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            
            };
            ParentGrid.Children.Add(GE_drawboard);
            GE_drawboard.SetValue(Grid.ColumnProperty, 0);
            GE_drawboard.SetValue(Grid.ColumnSpanProperty, 200);
            GE_drawboard.SetValue(Grid.RowProperty, 0);
            GE_drawboard.SetValue(Grid.RowSpanProperty, 150);
            Canvas.SetZIndex(GE_drawboard,-5);

            Geodetic_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Background = sandy_brown_brush,
                Content = "Geodetic",
                Foreground = Default_back_black_color_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5),
                CornerRadius = new CornerRadius(5,5,5,5),
                BorderBrush = dark_grey_brush,
                FontSize = 14,
                FontWeight = FontWeights.ExtraBold
            };
            ParentGrid.Children.Add(Geodetic_bt);
            Geodetic_bt.SetValue(Grid.ColumnProperty, C_offset + 20);
            Geodetic_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Geodetic_bt.SetValue(Grid.RowProperty, R_offset + 20);
            Geodetic_bt.SetValue(Grid.RowSpanProperty, 10);

            Geodetic_gon = new Polyline() { 
                Fill = red_bright_button_brush,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            Geodetic_gon.Points.Add(new Point(10, 10));
            Geodetic_gon.Points.Add(new Point(100, 10));
            Geodetic_gon.Points.Add(new Point(100, 100));
            Geodetic_gon.Points.Add(new Point(10, 100));
            //GE_drawboard.Children.Add(Geodetic_gon);
        }
        private void hide()
        {
            Test_bt.Visibility = Visibility.Collapsed;
            Geodetic_bt.Visibility = Visibility.Collapsed;
            GE_drawboard.Visibility = Visibility.Collapsed;
        }

        public void show()
        {
            Test_bt.Visibility = Visibility.Visible;
            Geodetic_bt.Visibility = Visibility.Visible;
            GE_drawboard.Visibility = Visibility.Visible;
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
