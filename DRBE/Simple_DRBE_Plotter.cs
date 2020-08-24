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
    public class Simple_DRBE_Plotter
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
        private double C_height = 0;
        private double C_width = 0;
        public Grid ParentGrid;
        private int zdex = 10;




        private List<Button> O_bt = new List<Button>();
        public Polyline pl = new Polyline();
        public Canvas SDP_ca = new Canvas();
        private Border SDP_bd = new Border();

        private double c = 0;

        private double cs = 0;

        private double r = 0;

        private double rs = 0;

        public Simple_DRBE_Plotter(Grid g, double ic, double ics, double ir, double irs, int z)
        {

            c = ic;
            cs = ics;
            r = ir;
            rs = irs;

            ParentGrid = g;
            
            SDP_ca = new Canvas() { 
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                CanDrag = true
            
            };
            ParentGrid.Children.Add(SDP_ca);
            SDP_ca.SetValue(Grid.ColumnProperty, c);
            SDP_ca.SetValue(Grid.ColumnSpanProperty, cs);
            SDP_ca.SetValue(Grid.RowProperty, r);
            SDP_ca.SetValue(Grid.RowSpanProperty, rs);
            Canvas.SetZIndex(SDP_ca, z);

            SDP_bd = new Border()
            {
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(1,1,1,1),
                Background = Default_back_black_color_brush,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };
            ParentGrid.Children.Add(SDP_bd);
            SDP_bd.SetValue(Grid.ColumnProperty, c-1);
            SDP_bd.SetValue(Grid.ColumnSpanProperty, cs+2);
            SDP_bd.SetValue(Grid.RowProperty, r-1);
            SDP_bd.SetValue(Grid.RowSpanProperty, rs+2);
            Canvas.SetZIndex(SDP_bd, z-1);


            C_height = ParentGrid.ActualHeight / 150;
            C_width = ParentGrid.ActualWidth / 200;

        }

        private List<Button> Object_bt = new List<Button>();
        private List<Polyline> PLL = new List<Polyline>();
        public void Plot_object(List<double> x, List<double> y, double T) //List<double> ang,  List<int> ID)
        {
            int i = 0;
            i = 0;
            if(T==0)
            {
                i = 0;
                while(i<x.Count)
                {
                    Object_bt.Add(new Button()
                    {
                        BorderBrush = green_bright_button_brush,
                        BorderThickness = new Thickness(2, 2, 2, 2),
                        Background = green_bright_button_brush,
                        Content = i,
                        Foreground = Default_back_black_color_brush,
                        FontSize = 14,
                        Width = 30,
                        Height = 30,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        CornerRadius = new CornerRadius(10, 10, 10, 10)
                    });
                    Canvas.SetLeft(Object_bt[i], C_width * cs /500 * (250 - x[i]));
                    Canvas.SetTop(Object_bt[i], C_height * rs / 500 * (250 - y[i]));
                    SDP_ca.Children.Add(Object_bt[i]);


                    PLL.Add(new Polyline() { 
                        Stroke = red_bright_button_brush,
                        StrokeThickness = 2
                    });
                    SDP_ca.Children.Add(PLL[i]);
                    PLL[i].Points.Add(new Point(C_width * cs / 500 * (250 - x[i]), C_height * rs / 500 * (250 - y[i])));
                    i++;
                }
            }else
            {
                i = 0;
                while (i < x.Count)
                {
                    Object_bt.Add(new Button()
                    {
                        BorderBrush = green_bright_button_brush,
                        BorderThickness = new Thickness(2, 2, 2, 2),
                        Background = green_bright_button_brush,
                        Content = i,
                        Foreground = Default_back_black_color_brush,
                        FontSize = 14,
                        Width = 30,
                        Height = 30,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        CornerRadius = new CornerRadius(10, 10, 10, 10)
                    });
                    Canvas.SetLeft(Object_bt[Object_bt.Count-1], C_width * cs / 500 * (250 - x[i]));
                    Canvas.SetTop(Object_bt[Object_bt.Count-1], C_height * rs / 500 * (250 - y[i]));
                    SDP_ca.Children.Add(Object_bt[Object_bt.Count-1]);

                    PLL[i].Points.Add(new Point(C_width * cs / 500 * (250 - x[i]), C_height * rs / 500 * (250 - y[i])));
                    i++;
                }
            }
        }
        public async Task Setup()
        {

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
