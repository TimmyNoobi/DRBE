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
    public class DRBEScenario
    {
        public Grid ParentGrid;

        private StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

        public List<Transmitter> transmitter = new List<Transmitter>();
        public List<Receiver> receiver = new List<Receiver>();
        public List<Platform> platform = new List<Platform>();
        private SoftwarePanel scenario_software_panel;





        public DRBEScenario(Grid parent)
        {
            //ParentGrid = parent;
            //scenario_software_panel = new SoftwarePanel(ParentGrid);
        }

        DRBE_Transmitter D_T;
        public async Task Parse_Scenario(string x)
        {
            StorageFile file = await storageFolder.CreateFileAsync(x, CreationCollisionOption.OpenIfExists);
            List<string> FromFile = new List<string>( await FileIO.ReadLinesAsync(file));
            await Task.Delay(1000);
            string filestring = await FileIO.ReadTextAsync(file);
            //await ShowDialog("Test", BitConverter.ToString(String_ByteList(filestring).ToArray()));
            await Bytelist_Object(String_ByteList(filestring));
            transmitter = new List<Transmitter>();
            receiver = new List<Receiver>();
            platform = new List<Platform>();
           

            //await ShowDialog(D_Trans.Count.ToString(), D_Trans[0].Summary());

            int i = 0;
            i = 0;
            //while(i<FromFile.Count)
            //{
            //    Organizer(FromFile[i]);
            //    i++;
            //}
            //await ShowDialog(transmitter.Count.ToString(), FromFile.Count.ToString());
            //scenario_software_panel.Create_Transmitter(transmitter);
            //scenario_software_panel.Create_Receiver(receiver);
            //scenario_software_panel.Create_Platform(platform);
        }


        public List<byte> String_ByteList(string x)
        {
            List<byte> result = new List<byte>();
            string temp = "";
            int i = 0;
            i = 0;
            while (i < x.Length)
            {
                if (x[i] == '-')
                {
                    result.Add(S_B(temp));
                    temp = "";
                }
                else if(x[i] == '\n')
                {

                }
                else
                {
                    
                    temp += x[i].ToString();
                }
                i++;
            }
            return result;
        }
        private List<byte> Raw_data = new List<byte>();

        public List<DRBE_Transmitter> D_Trans = new List<DRBE_Transmitter>();
        public List<DRBE_Reflector> D_Ref = new List<DRBE_Reflector>();
        public List<DRBE_Receiver> D_Rec = new List<DRBE_Receiver>();
        public async Task Bytelist_Object(List<byte> x)
        {
            int mode = 0;
            Raw_data = new List<byte>();
            List<byte> temp = new List<byte>();
            int i = 0;
            i = 0;
            int ii = 0;
            bool outflag = false;
            while (i < x.Count)
            {

                try
                {
                    if (x[i] == 0x01 && mode == 0)
                    {
                        i++;
                        if (x[i] == 0x10)
                        {
                            D_Trans.Add(new DRBE_Transmitter());
                            D_Trans[D_Trans.Count - 1].Type = "Phase Array Radar";
                            D_Trans[D_Trans.Count - 1].Raw_data = new List<byte>();
                            //await ShowDialog("Error", "Added");
                            i++;
                            while(i<x.Count)
                            {
                                if (x[i] == 0x30 && mode == 0) //ID
                                {
                                    //await ShowDialog("Error", "Added");
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    //await ShowDialog("Error", "Added");
                                    i++;
                                    ii = i + 2;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].ID = BitConverter.ToUInt16(temp.ToArray(), 0);
                                    //await ShowDialog("Error", D_Trans[D_Trans.Count - 1].ID.ToString());
                                }
                                else if (x[i] == 0x20 && mode == 0) //Initial_Position_X
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Position_X = BitConverter.ToDouble(temp.ToArray(), 0);
                                    //await ShowDialog("Error1", D_Trans[D_Trans.Count - 1].Initial_Position_X.ToString());
                                }
                                else if (x[i] == 0x21 && mode == 0) //Initial_Position_Y
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Position_Y = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x22 && mode == 0)//Initial_Position_Z
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Position_Z = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x23 && mode == 0) //Initial_Velocity_X
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Velocity_X = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x24 && mode == 0) //Initial_Velocity_Y
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Velocity_Y = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x25 && mode == 0) //Initial_Velocity_Z
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Velocity_Z = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x26 && mode == 0) //Initial_Orientation_X
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Orientation_X = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x27 && mode == 0) //Initial_Orientation_Y
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Orientation_Y = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x28 && mode == 0) //Initial_Orientation_Z
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Orientation_Z = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x29 && mode == 0) //Initial_Acceleration_X
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Acceleration_X = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x2A && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Acceleration_Y = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x2B && mode == 0) //Initial_Acceleration_Z
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Acceleration_Z = BitConverter.ToDouble(temp.ToArray(), 0);

                                    //await ShowDialog("Acccle", D_Trans[D_Trans.Count - 1].Initial_Acceleration_Z.ToString());
                                }
                                else if (x[i] == 0x2C && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Eangle_AZ = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x2D && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Eangle_EL = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x2E && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Mangle_AZ = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x2F && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Mangle_EL = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x50 && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Number_Antenna_AZ = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x51 && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Number_Antenna_EL = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else
                                {
                                    break;
                                }
                            }
                            
                        }
                        else if (x[i] == 0x11)
                        {
                            D_Trans.Add(new DRBE_Transmitter());
                            D_Trans[D_Trans.Count - 1].Type = "Mechanical Radar";
                            D_Trans[D_Trans.Count - 1].Raw_data = new List<byte>();
                            //await ShowDialog("Error", "Added");
                            i++;
                            while (i < x.Count)
                            {
                                if (x[i] == 0x30 && mode == 0) //ID
                                {
                                    //await ShowDialog("Error", "Added");
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    //await ShowDialog("Error", "Added");
                                    i++;
                                    ii = i + 2;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].ID = BitConverter.ToUInt16(temp.ToArray(), 0);
                                    //await ShowDialog("Error", D_Trans[D_Trans.Count - 1].ID.ToString());
                                }
                                else if (x[i] == 0x20 && mode == 0) //Initial_Position_X
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Position_X = BitConverter.ToDouble(temp.ToArray(), 0);
                                    //await ShowDialog("Error1", D_Trans[D_Trans.Count - 1].Initial_Position_X.ToString());
                                }
                                else if (x[i] == 0x21 && mode == 0) //Initial_Position_Y
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Position_Y = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x22 && mode == 0)//Initial_Position_Z
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Position_Z = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x23 && mode == 0) //Initial_Velocity_X
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Velocity_X = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x24 && mode == 0) //Initial_Velocity_Y
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Velocity_Y = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x25 && mode == 0) //Initial_Velocity_Z
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Velocity_Z = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x26 && mode == 0) //Initial_Orientation_X
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Orientation_X = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x27 && mode == 0) //Initial_Orientation_Y
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Orientation_Y = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x28 && mode == 0) //Initial_Orientation_Z
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Orientation_Z = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x29 && mode == 0) //Initial_Acceleration_X
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Acceleration_X = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x2A && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Acceleration_Y = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x2B && mode == 0) //Initial_Acceleration_Z
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Acceleration_Z = BitConverter.ToDouble(temp.ToArray(), 0);

                                    //await ShowDialog("Acccle", D_Trans[D_Trans.Count - 1].Initial_Acceleration_Z.ToString());
                                }
                                else if (x[i] == 0x2C && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Eangle_AZ = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x2D && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Eangle_EL = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x2E && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Mangle_AZ = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x2F && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Initial_Mangle_EL = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x50 && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Number_Antenna_AZ = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x51 && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Trans[D_Trans.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Trans[D_Trans.Count - 1].Number_Antenna_EL = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                        else if (x[i] == 0x12)
                        {
                            D_Rec.Add(new DRBE_Receiver());
                            D_Rec[D_Rec.Count - 1].Type = "Phase Array Radar";
                            D_Rec[D_Rec.Count - 1].Raw_data = new List<byte>();
                            //await ShowDialog("Error", "Added");
                            i++;
                            while (i < x.Count)
                            {
                                if (x[i] == 0x30 && mode == 0) //ID
                                {
                                    //await ShowDialog("Error", "Added");
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    //await ShowDialog("Error", "Added");
                                    i++;
                                    ii = i + 2;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].ID = BitConverter.ToUInt16(temp.ToArray(), 0);
                                    //await ShowDialog("Error", D_Trans[D_Trans.Count - 1].ID.ToString());
                                }
                                else if (x[i] == 0x20 && mode == 0) //Initial_Position_X
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Position_X = BitConverter.ToDouble(temp.ToArray(), 0);
                                    //await ShowDialog("Error1", D_Trans[D_Trans.Count - 1].Initial_Position_X.ToString());
                                }
                                else if (x[i] == 0x21 && mode == 0) //Initial_Position_Y
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Position_Y = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x22 && mode == 0)//Initial_Position_Z
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Position_Z = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x23 && mode == 0) //Initial_Velocity_X
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Velocity_X = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x24 && mode == 0) //Initial_Velocity_Y
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Velocity_Y = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x25 && mode == 0) //Initial_Velocity_Z
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Velocity_Z = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x26 && mode == 0) //Initial_Orientation_X
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Orientation_X = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x27 && mode == 0) //Initial_Orientation_Y
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Orientation_Y = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x28 && mode == 0) //Initial_Orientation_Z
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Orientation_Z = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x29 && mode == 0) //Initial_Acceleration_X
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Acceleration_X = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x2A && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Acceleration_Y = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x2B && mode == 0) //Initial_Acceleration_Z
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Acceleration_Z = BitConverter.ToDouble(temp.ToArray(), 0);

                                    //await ShowDialog("Acccle", D_Trans[D_Trans.Count - 1].Initial_Acceleration_Z.ToString());
                                }
                                else if (x[i] == 0x2C && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Eangle_AZ = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x2D && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Eangle_EL = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x2E && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Mangle_AZ = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x2F && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Mangle_EL = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x50 && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Number_Antenna_AZ = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x51 && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Number_Antenna_EL = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                        else if (x[i] == 0x13)
                        {
                            D_Rec.Add(new DRBE_Receiver());
                            D_Rec[D_Rec.Count - 1].Type = "Mechanical Radar";
                            D_Rec[D_Rec.Count - 1].Raw_data = new List<byte>();
                            //await ShowDialog("Error", "Added");
                            i++;
                            while (i < x.Count)
                            {
                                if (x[i] == 0x30 && mode == 0) //ID
                                {
                                    //await ShowDialog("Error", "Added");
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    //await ShowDialog("Error", "Added");
                                    i++;
                                    ii = i + 2;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].ID = BitConverter.ToUInt16(temp.ToArray(), 0);
                                    //await ShowDialog("Error", D_Trans[D_Trans.Count - 1].ID.ToString());
                                }
                                else if (x[i] == 0x20 && mode == 0) //Initial_Position_X
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Position_X = BitConverter.ToDouble(temp.ToArray(), 0);
                                    //await ShowDialog("Error1", D_Trans[D_Trans.Count - 1].Initial_Position_X.ToString());
                                }
                                else if (x[i] == 0x21 && mode == 0) //Initial_Position_Y
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Position_Y = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x22 && mode == 0)//Initial_Position_Z
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Position_Z = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x23 && mode == 0) //Initial_Velocity_X
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Velocity_X = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x24 && mode == 0) //Initial_Velocity_Y
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Velocity_Y = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x25 && mode == 0) //Initial_Velocity_Z
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Velocity_Z = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x26 && mode == 0) //Initial_Orientation_X
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Orientation_X = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x27 && mode == 0) //Initial_Orientation_Y
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Orientation_Y = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x28 && mode == 0) //Initial_Orientation_Z
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Orientation_Z = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x29 && mode == 0) //Initial_Acceleration_X
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Acceleration_X = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x2A && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Acceleration_Y = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x2B && mode == 0) //Initial_Acceleration_Z
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Acceleration_Z = BitConverter.ToDouble(temp.ToArray(), 0);

                                    //await ShowDialog("Acccle", D_Trans[D_Trans.Count - 1].Initial_Acceleration_Z.ToString());
                                }
                                else if (x[i] == 0x2C && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Eangle_AZ = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x2D && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Eangle_EL = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x2E && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Mangle_AZ = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x2F && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Initial_Mangle_EL = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x50 && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Number_Antenna_AZ = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x51 && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Rec[D_Rec.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Rec[D_Rec.Count - 1].Number_Antenna_EL = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                        else if (x[i] == 0x14)
                        {
                            D_Ref.Add(new DRBE_Reflector());
                            D_Ref[D_Ref.Count - 1].Type = "Object";
                            D_Ref[D_Ref.Count - 1].Raw_data = new List<byte>();
                            //await ShowDialog("Error", "Added");
                            i++;
                            while (i < x.Count)
                            {
                                if (x[i] == 0x30 && mode == 0) //ID
                                {
                                    //await ShowDialog("Error", "Added");
                                    temp = new List<byte>();
                                    D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                    //await ShowDialog("Error", "Added");
                                    i++;
                                    ii = i + 2;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Ref[D_Ref.Count - 1].ID = BitConverter.ToUInt16(temp.ToArray(), 0);
                                    //await ShowDialog("Error", D_Trans[D_Trans.Count - 1].ID.ToString());
                                }
                                else if (x[i] == 0x20 && mode == 0) //Initial_Position_X
                                {
                                    temp = new List<byte>();
                                    D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Ref[D_Ref.Count - 1].Initial_Position_X = BitConverter.ToDouble(temp.ToArray(), 0);
                                    //await ShowDialog("Error1", D_Trans[D_Trans.Count - 1].Initial_Position_X.ToString());
                                }
                                else if (x[i] == 0x21 && mode == 0) //Initial_Position_Y
                                {
                                    temp = new List<byte>();
                                    D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Ref[D_Ref.Count - 1].Initial_Position_Y = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x22 && mode == 0)//Initial_Position_Z
                                {
                                    temp = new List<byte>();
                                    D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Ref[D_Ref.Count - 1].Initial_Position_Z = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x23 && mode == 0) //Initial_Velocity_X
                                {
                                    temp = new List<byte>();
                                    D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Ref[D_Ref.Count - 1].Initial_Velocity_X = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x24 && mode == 0) //Initial_Velocity_Y
                                {
                                    temp = new List<byte>();
                                    D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Ref[D_Ref.Count - 1].Initial_Velocity_Y = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x25 && mode == 0) //Initial_Velocity_Z
                                {
                                    temp = new List<byte>();
                                    D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Ref[D_Ref.Count - 1].Initial_Velocity_Z = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x26 && mode == 0) //Initial_Orientation_X
                                {
                                    temp = new List<byte>();
                                    D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Ref[D_Ref.Count - 1].Initial_Orientation_X = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x27 && mode == 0) //Initial_Orientation_Y
                                {
                                    temp = new List<byte>();
                                    D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Ref[D_Ref.Count - 1].Initial_Orientation_Y = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x28 && mode == 0) //Initial_Orientation_Z
                                {
                                    temp = new List<byte>();
                                    D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Ref[D_Ref.Count - 1].Initial_Orientation_Z = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x29 && mode == 0) //Initial_Acceleration_X
                                {
                                    temp = new List<byte>();
                                    D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Ref[D_Ref.Count - 1].Initial_Acceleration_X = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x2A && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Ref[D_Ref.Count - 1].Initial_Acceleration_Y = BitConverter.ToDouble(temp.ToArray(), 0);
                                }
                                else if (x[i] == 0x2B && mode == 0) //Initial_Acceleration_Z
                                {
                                    temp = new List<byte>();
                                    D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Ref[D_Ref.Count - 1].Initial_Acceleration_Z = BitConverter.ToDouble(temp.ToArray(), 0);

                                    //await ShowDialog("Acccle", D_Trans[D_Trans.Count - 1].Initial_Acceleration_Z.ToString());
                                }
                                else if (x[i] == 0x2C && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Ref[D_Ref.Count - 1].Initial_Eangle_AZ = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x2D && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Ref[D_Ref.Count - 1].Initial_Eangle_EL = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x2E && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Ref[D_Ref.Count - 1].Initial_Mangle_AZ = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x2F && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Ref[D_Ref.Count - 1].Initial_Mangle_EL = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x50 && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Ref[D_Ref.Count - 1].Number_Antenna_AZ = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else if (x[i] == 0x51 && mode == 0)
                                {
                                    temp = new List<byte>();
                                    D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                    i++;
                                    ii = i + 8;
                                    while (i < ii)
                                    {
                                        temp.Insert(0, x[i]);
                                        D_Ref[D_Ref.Count - 1].Raw_data.Add(x[i]);
                                        i++;
                                    }
                                    D_Ref[D_Ref.Count - 1].Number_Antenna_EL = BitConverter.ToDouble(temp.ToArray(), 0);

                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        await ShowDialog("Error" + x[i].ToString(), "Format Error");
                        return;
                    }
                }
                catch(Exception ex)
                {
                    await ShowDialog("Error", ex.ToString());
                    return;
                }

            }
                

            

        }


        private void Organizer(string x)
        {
            List<double> result = new List<double>();
            string sti = "";
            int i = 0;
            i = 1;
            while(i<x.Length)
            {
                if(x[i]==',')
                {
                    result.Add(S_D(sti));
                    sti = "";
                }
                else
                {
                    sti += x[i];
                }

                i++;
            }
            if(x[0]=='r')
            {
                receiver.Add(new Receiver(result));
            }else if(x[0]=='p')
            {
                platform.Add(new Platform(result));
            }
            else if (x[0] == 't')
            {
                transmitter.Add(new Transmitter(result));
            }
        }

        #region others
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
        private byte S_B(string x)
        {
            byte result = 0;
            int temp = S_H(x);
            result = BitConverter.GetBytes(temp)[0];
            //ShowDialog(result.ToString(),result.ToString());
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
        private string Only_int_string(string x)
        {
            int len = x.Length;
            int i = 0;
            string result = "";
            while (i < len)
            {
                if (x[i] <= '9' && x[i] >= '0')
                {
                    result += x[i].ToString();
                }
                i++;
            }
            return result;
        }
        private string Only_double_string(string x)
        {
            int len = x.Length;
            int i = 0;
            string result = "";
            if (len >= 1)
            {
                if (x[0] == '-')
                {
                    result = "-";
                }
            }
            while (i < len)
            {
                if ((x[i] <= '9' && x[i] >= '0') || x[i] == '.')
                {
                    result += x[i].ToString();
                }
                i++;
            }
            return result;
        }
        private List<int> Create_ordered_list(int x1, int x2)
        {
            List<int> result = new List<int>();
            int i = x1;
            while (i <= x2)
            {
                result.Add(i);
                i++;
            }
            return result;
        }

        #endregion

    }
}
