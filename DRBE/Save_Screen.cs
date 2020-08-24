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
    public class Save_Screen
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
        public Save_Screen(Grid parent)
        {
            ParentGrid = parent;
            Setup();
            hide();

        }

        private Border Save_bd = new Border();

        private TextBlock Title_ttb = new TextBlock();
        private TextBlock Address_ttb = new TextBlock();
        private TextBlock Enter_name_ttb = new TextBlock();
        private TextBlock Name_sub_ttb = new TextBlock();
        private TextBox Enter_name_tb = new TextBox();
        private Button Save_bt = new Button();
        private Button Cancel_bt = new Button();
        public StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        public string SS_content = "";
        public async Task Start(string title, List<string> folder, string file_type, string content)
        {
            SS_content = content;
            storageFolder = ApplicationData.Current.LocalFolder;
            Name_sub_ttb.Text = "." + file_type; 
            Title_ttb.Text = title;
            int i = 0;
            i = 0;
            while(i < folder.Count)
            {
                try
                {
                    storageFolder = await storageFolder.GetFolderAsync(folder[i]);
                }
                catch
                {
                    await storageFolder.CreateFolderAsync(folder[i]);
                    storageFolder = await storageFolder.GetFolderAsync(folder[i]);
                }
                i++;
            }
            Address_ttb.Text = "File saved at path:\r\n " + storageFolder.Path.ToString();
            #region show
            Save_bd.Visibility = Visibility.Visible;

            Title_ttb.Visibility = Visibility.Visible;

            Address_ttb.Visibility = Visibility.Visible;

            Enter_name_ttb.Visibility = Visibility.Visible;

            Enter_name_tb.Visibility = Visibility.Visible;

            Name_sub_ttb.Visibility = Visibility.Visible;

            Save_bt.Visibility = Visibility.Visible;

            Cancel_bt.Visibility = Visibility.Visible;
            #endregion
        }

        public void Setup()
        {

            Save_bd = new Border() {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2,2,2,2),
                CornerRadius = new CornerRadius(2,2,2,2)
            
            };
            ParentGrid.Children.Add(Save_bd);
            Save_bd.SetValue(Grid.ColumnProperty, 60);
            Save_bd.SetValue(Grid.ColumnSpanProperty, 100);
            Save_bd.SetValue(Grid.RowProperty, 30);
            Save_bd.SetValue(Grid.RowSpanProperty, 70);
            Canvas.SetZIndex(Save_bd,10);

            Title_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Title",
                TextWrapping = TextWrapping.Wrap,
                Foreground = white_button_brush,
                FontSize = 24,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Title_ttb);
            Title_ttb.SetValue(Grid.ColumnProperty, 65);
            Title_ttb.SetValue(Grid.ColumnSpanProperty, 90);
            Title_ttb.SetValue(Grid.RowProperty, 35);
            Title_ttb.SetValue(Grid.RowSpanProperty, 10);
            Canvas.SetZIndex(Title_ttb, 11);

            Address_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "File saved at path:\r\n " + storageFolder.Path.ToString(),
                TextWrapping = TextWrapping.Wrap,
                Foreground = green_text_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Address_ttb);
            Address_ttb.SetValue(Grid.ColumnProperty, 65);
            Address_ttb.SetValue(Grid.ColumnSpanProperty, 80);
            Address_ttb.SetValue(Grid.RowProperty, 50);
            Address_ttb.SetValue(Grid.RowSpanProperty, 10);
            Canvas.SetZIndex(Address_ttb, 11);

            Enter_name_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Enter File Name",
                TextWrapping = TextWrapping.Wrap,
                Foreground = white_button_brush,
                FontSize = 18,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Enter_name_ttb);
            Enter_name_ttb.SetValue(Grid.ColumnProperty, 65);
            Enter_name_ttb.SetValue(Grid.ColumnSpanProperty, 20);
            Enter_name_ttb.SetValue(Grid.RowProperty, 65);
            Enter_name_ttb.SetValue(Grid.RowSpanProperty, 5);
            Canvas.SetZIndex(Enter_name_ttb, 11);

            Enter_name_tb = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                HorizontalContentAlignment = HorizontalAlignment.Right,
                Text = "",
                FontSize = 16,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Enter_name_tb);
            Enter_name_tb.SetValue(Grid.ColumnProperty, 65);
            Enter_name_tb.SetValue(Grid.ColumnSpanProperty, 35);
            Enter_name_tb.SetValue(Grid.RowProperty, 70);
            Enter_name_tb.SetValue(Grid.RowSpanProperty, 5);
            Canvas.SetZIndex(Enter_name_tb, 11);

            Name_sub_ttb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = ".Misc",
                TextWrapping = TextWrapping.Wrap,
                Foreground = white_button_brush,
                FontSize = 22,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(Name_sub_ttb);
            Name_sub_ttb.SetValue(Grid.ColumnProperty, 100);
            Name_sub_ttb.SetValue(Grid.ColumnSpanProperty, 10);
            Name_sub_ttb.SetValue(Grid.RowProperty, 70);
            Name_sub_ttb.SetValue(Grid.RowSpanProperty, 5);
            Canvas.SetZIndex(Name_sub_ttb, 11);

            Save_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Background = Default_back_black_color_brush,
                Content = "Save",
                Foreground = white_button_brush,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                FontSize = 22,
                FontWeight = FontWeights.ExtraBold
            };
            ParentGrid.Children.Add(Save_bt);
            Save_bt.SetValue(Grid.ColumnProperty, 70);
            Save_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Save_bt.SetValue(Grid.RowProperty, 90);
            Save_bt.SetValue(Grid.RowSpanProperty, 7);
            Save_bt.Click += Save_bt_Click;
            Canvas.SetZIndex(Save_bt, 11);

            Cancel_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Background = Default_back_black_color_brush,
                Content = "Cancel",
                Foreground = white_button_brush,
                BorderBrush = white_button_brush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                FontSize = 22,
                FontWeight = FontWeights.ExtraBold
            };
            ParentGrid.Children.Add(Cancel_bt);
            Cancel_bt.SetValue(Grid.ColumnProperty, 130);
            Cancel_bt.SetValue(Grid.ColumnSpanProperty, 20);
            Cancel_bt.SetValue(Grid.RowProperty, 90);
            Cancel_bt.SetValue(Grid.RowSpanProperty, 7);
            Cancel_bt.Click += Cancel_bt_Click;
            Canvas.SetZIndex(Cancel_bt, 11);
        }

        private void Cancel_bt_Click(object sender, RoutedEventArgs e)
        {
            hide();
        }

        private async void Save_bt_Click(object sender, RoutedEventArgs e)
        {

            string filename = Enter_name_tb.Text + Name_sub_ttb.Text;
            StorageFile file;
            int selection = 0;
            try
            {
                await storageFolder.GetFileAsync(filename);
                selection = await ConfirmDialog("Object Already Exist", "Do you want to over-write?", "Over-write", "Cancel");
                if(selection == 0)
                {
                    return;
                }
                else
                {
                    file = await storageFolder.CreateFileAsync(filename,CreationCollisionOption.OpenIfExists);
                    await FileIO.WriteTextAsync(file, SS_content);
                    await ShowDialog("Over-write succeed",file.Path.ToLower());
                    hide();
                }
            }
            catch
            {
                file = await storageFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
                await FileIO.WriteTextAsync(file, SS_content);
                await ShowDialog("Write succeed", file.Path.ToLower());
                hide();
            }
        }

        public void hide()
        {
            Save_bd.Visibility = Visibility.Collapsed;

            Title_ttb.Visibility = Visibility.Collapsed;

            Address_ttb.Visibility = Visibility.Collapsed;

            Enter_name_ttb.Visibility = Visibility.Collapsed;

            Enter_name_tb.Visibility = Visibility.Collapsed;

            Name_sub_ttb.Visibility = Visibility.Collapsed;

            Save_bt.Visibility = Visibility.Collapsed;

            Cancel_bt.Visibility = Visibility.Collapsed;
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
