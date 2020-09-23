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
using Windows.Devices.Bluetooth.Background;

namespace DRBE
{
    public class DRBE_Objs
    {
        public List<byte> Raw_data = new List<byte>();

        public UInt16 ID = 0;

        public string Coordinate_system_s = "Global Frame \\ WGS84"; //
        public int Coordinate_system = 1;

        public int Reference_Obj_ID = 0;

        public double Initial_Position_X = 4792000;
        public double Initial_Position_Y = 2711200;
        public double Initial_Position_Z = 3221500;

        public double Initial_Ref_Position_X = 4792000;
        public double Initial_Ref_Position_Y = 2711200;
        public double Initial_Ref_Position_Z = 3221500;

        public double Initial_Latitude = 30.5;
        public double Initial_Longtitude = 29.5;

        public bool Is_Transmitter = false;
        public bool Is_Reflector = false;
        public bool Is_Receiver = false;
        public bool Is_Stationary = false;

        public double Initial_Velocity_X = 20;
        public double Initial_Velocity_Y = 20;
        public double Initial_Velocity_Z = 20;

        public double Initial_Acceleration_X = 0;
        public double Initial_Acceleration_Y = 0;
        public double Initial_Acceleration_Z = 0;

        public double Initial_Orientation_X = 20;
        public double Initial_Orientation_Y = 40;
        public double Initial_Orientation_Z = 50;

        public double Initial_Eangle_EL = 10;
        public double Initial_Eangle_AZ = 20;

        public double Initial_Mangle_EL = 30;
        public double Initial_Mangle_AZ = -10;

        public double Number_Antenna_AZ = 4;
        public double Number_Antenna_EL = 4;

        public double Beamwidth_AZ = 10;
        public double Beamwidth_EL = 10;

        public double Resolution_AZ = 1;
        public double Resolution_EL = 1;

        public double Element_Spacing = 0.5;
        public double Backlobe_Scaling = 50;

        public double Antenna_constant = 20;

        public double Polar_coefficient1 = 10;
        public double Polar_coefficient2 = 10;

        public double Clutter_Gamma_k = 20;
        public double Clutter_Gamma_theta = 20;

        public double Clutter_Gausian_m = 20;
        public double Clutter_Gausian_v = 20;

        public double Constraint_centerX = 300;
        public double Constraint_centerY = 300;
        public double Constraint_centerZ = 300;

        public double Constraint_max_height = 300;
        public double Constraint_radius = 300;
        public double Constraint_max_speed = 300;
        public double Constraint_max_acceleration = 300;
        public double Constraint_max_orientation = 300;
        public double Constraint_max_band = 300;
        public double Constraint_min_band = 300;
        public double Constraint_max_band_change = 300;

        public string Window_type_s = "";
        public int Window_type = 0;

        public string Cut_type_s = "";
        public int Cut_type = 0;

        public double Ta1_update_rate = 1000;
        public double Interpolation_order = 4;
        public double Convergence = 2;

        public double Antenna_order = 5;

        public double Dictionary_dimension = 800;
        public double RCS_order = 6;
        public double RCS_point = 20;
        public double RCS_angle_resolution = 2;
        public double RCS_frequency_point = 1;
        public double RCS_number_of_polarization = 3;
        public double RCS_output_time_sampe = 10;

        public double RCS_Compute = 0;
        public double ANT_Compute = 0;
        public double COR_Compute = 0;
        public double NRE_Compute = 0;
        public double ORI_Compute = 0;
        public double TU_Compute = 0;
        public double PG_Compute = 0;


        public double RCS_Memory = 0;
        public double ANT_Memory = 0;
        public double COR_Memory = 0;
        public double NRE_Memory = 0;
        public double ORI_Memory = 0;
        public double TU_Memory = 0;
        public double PG_Memory = 0;


        public double RCS_Bandwidth = 0;
        public double ANT_Bandwidth = 0;
        public double COR_Bandwidth = 0;
        public double NRE_Bandwidth = 0;
        public double ORI_Bandwidth = 0;
        public double TU_Bandwidth = 0;
        public double PG_Bandwidth = 0;


        public double RCS_Latency = 0;
        public double ANT_Latency = 0;
        public double COR_Latency = 0;
        public double NRE_Latency = 0;
        public double ORI_Latency = 0;
        public double TU_Latency = 0;
        public double PG_Latency = 0;

        public bool Is_transmitting = false;
        public bool Is_reflecting = false;
        public bool Is_receiving = false;

        public double cp_rcs_pn_n = 0;
        public double cp_rcs_pn_p = 0;

        public double cp_rcs_ar_n = 0;
        public double cp_rcs_ar_p = 0;

        public double cp_rcs_fb_n = 0;
        public double cp_rcs_fb_p = 0;

        public double cp_rcs_sp_n = 0;
        public double cp_rcs_sp_p = 0;

        public double cp_rcs_ss_n = 0;
        public double cp_rcs_ss_p = 0;

        public double cp_ant_ar_n = 0;
        public double cp_ant_ar_p = 0;

        public double cp_ant_na_n = 0;
        public double cp_ant_na_p = 0;

        public double cp_ant_ds_n = 0;
        public double cp_ant_ds_p = 0;

        public double cf_ant0 = 0;
        public double cf_ant1 = 0;
        public double cf_ant2 = 0;
        public double cf_ant3 = 0;
        public double cf_ant4 = 0;
        public double cf_ant5 = 0;

        public double cf_rcs0 = 0;
        public double cf_rcs1 = 0;
        public double cf_rcs2 = 0;
        public double cf_rcs3 = 0;
        public double cf_rcs4 = 0;
        public double cf_rcs5 = 0;
        public double cf_rcs6 = 0;


        public double mp_rcs_pn_n = 0;
        public double mp_rcs_pn_p = 0;

        public double mp_rcs_ar_n = 0;
        public double mp_rcs_ar_p = 0;

        public double mp_rcs_fb_n = 0;
        public double mp_rcs_fb_p = 0;

        public double mp_rcs_sp_n = 0;
        public double mp_rcs_sp_p = 0;

        public double mp_rcs_ss_n = 0;
        public double mp_rcs_ss_p = 0;

        public double mp_ant_ar_n = 0;
        public double mp_ant_ar_p = 0;

        public double mp_ant_na_n = 0;
        public double mp_ant_na_p = 0;

        public double mp_ant_ds_n = 0;
        public double mp_ant_ds_p = 0;

        public double mf_ant0 = 0;
        public double mf_ant1 = 0;
        public double mf_ant2 = 0;
        public double mf_ant3 = 0;
        public double mf_ant4 = 0;
        public double mf_ant5 = 0;

        public double mf_rcs0 = 0;
        public double mf_rcs1 = 0;
        public double mf_rcs2 = 0;
        public double mf_rcs3 = 0;
        public double mf_rcs4 = 0;
        public double mf_rcs5 = 0;
        public double mf_rcs6 = 0;

        public double lp_rcs_pn_n = 0;
        public double lp_rcs_pn_p = 0;

        public double lp_rcs_ar_n = 0;
        public double lp_rcs_ar_p = 0;

        public double lp_rcs_fb_n = 0;
        public double lp_rcs_fb_p = 0;
   
        public double lp_rcs_sp_n = 0;
        public double lp_rcs_sp_p = 0;

        public double lp_rcs_ss_n = 0;
        public double lp_rcs_ss_p = 0;

        public double lp_ant_ar_n = 0;
        public double lp_ant_ar_p = 0;

        public double lp_ant_na_n = 0;
        public double lp_ant_na_p = 0;

        public double lp_ant_ds_n = 0;
        public double lp_ant_ds_p = 0;

        public double lf_ant0 = 0;
        public double lf_ant1 = 0;
        public double lf_ant2 = 0;
        public double lf_ant3 = 0;
        public double lf_ant4 = 0;
        public double lf_ant5 = 0;

        public double lf_rcs0 = 0;
        public double lf_rcs1 = 0;
        public double lf_rcs2 = 0;
        public double lf_rcs3 = 0;
        public double lf_rcs4 = 0;
        public double lf_rcs5 = 0;
        public double lf_rcs6 = 0;

        public double bp_rcs_pn_n = 0;
        public double bp_rcs_pn_p = 0;

        public double bp_rcs_ar_n = 0;
        public double bp_rcs_ar_p = 0;

        public double bp_rcs_fb_n = 0;
        public double bp_rcs_fb_p = 0;

        public double bp_rcs_sp_n = 0;
        public double bp_rcs_sp_p = 0;

        public double bp_rcs_ss_n = 0;
        public double bp_rcs_ss_p = 0;

        public double bp_ant_ar_n = 0;
        public double bp_ant_ar_p = 0;

        public double bp_ant_na_n = 0;
        public double bp_ant_na_p = 0;

        public double bp_ant_ds_n = 0;
        public double bp_ant_ds_p = 0;

        public double bf_ant0 = 0;
        public double bf_ant1 = 0;
        public double bf_ant2 = 0;
        public double bf_ant3 = 0;
        public double bf_ant4 = 0;
        public double bf_ant5 = 0;

        public double bf_rcs0 = 0;
        public double bf_rcs1 = 0;
        public double bf_rcs2 = 0;
        public double bf_rcs3 = 0;
        public double bf_rcs4 = 0;
        public double bf_rcs5 = 0;
        public double bf_rcs6 = 0;
        public int number_of_path = 0;
        public DRBE_Objs(UInt16 x)
        {
            Random r = new Random();

            ID = x;

            Coordinate_system = 0;

            Reference_Obj_ID = 0;

            Initial_Position_X = r.Next(-400, 400);
            Initial_Position_Y = r.Next(-400, 400);
            Initial_Position_Z = r.Next(0, 200);

            Initial_Ref_Position_X = 10;
            Initial_Ref_Position_Y = 20;
            Initial_Ref_Position_Z = 30;

            Initial_Latitude = 30.5;
            Initial_Longtitude = 29.5;

            Initial_Orientation_X = r.Next(-45, 45);
            Initial_Orientation_Y = r.Next(-45, 45);
            Initial_Orientation_Z = r.Next(-45, 45);

            if (r.Next(0,1)==1)
            {
                Is_Transmitter = true;
                Is_Receiver = true;
                Is_Reflector = true;
            }
            else
            {
                Is_Transmitter = true;
                Is_Receiver = true;
                Is_Reflector = true;
            }
            if (r.Next(0, 3) == 1)
            {
                Initial_Orientation_X = 0;
                Initial_Orientation_Y = 0;
                Is_Stationary = true;
                Initial_Position_Z = 0;
            }


            Initial_Velocity_X = 20;
            Initial_Velocity_Y = 20;
            Initial_Velocity_Z = 20;

            Initial_Acceleration_X = 0;
            Initial_Acceleration_Y = 0;
            Initial_Acceleration_Z = 0;


            Initial_Eangle_EL = 10;
            Initial_Eangle_AZ = 20;

            Initial_Mangle_EL = 30;
            Initial_Mangle_AZ = -10;

            Number_Antenna_AZ = 4;
            Number_Antenna_EL = 4;

            Beamwidth_AZ = 10;
            Beamwidth_EL = 10;

            Resolution_AZ = 1;
            Resolution_EL = 1;

            Element_Spacing = 0.5;
            Backlobe_Scaling = 50;

            Antenna_constant = 20;

            Polar_coefficient1 = 10;
            Polar_coefficient2 = 10;

            Clutter_Gamma_k = 20;
            Clutter_Gamma_theta = 20;

            Clutter_Gausian_m = 20;
            Clutter_Gausian_v = 20;

            Constraint_centerX = 300;
            Constraint_centerY = 300;
            Constraint_centerZ = 300;

            Constraint_max_height = 300;
            Constraint_radius = 300;
            Constraint_max_speed = 300;
            Constraint_max_acceleration = 300;
            Constraint_max_orientation = 300;
            Constraint_max_band = 300;
            Constraint_min_band = 300;
            Constraint_max_band_change = 300;

            Cut_type = 0;

            Window_type = 0;

        }
        public void Set_default_2()
        {

        }
        public void Set_default_1()
        {
            Coordinate_system = 0;
            Coordinate_system_s = "Global Frame \\ WGS84"; //

            Reference_Obj_ID = 0;

            Initial_Position_X = 4792000;
            Initial_Position_Y = 2711200;
            Initial_Position_Z = 3221500;

            Initial_Latitude = 30.5;
            Initial_Longtitude = 29.5;

            Initial_Velocity_X = 20;
            Initial_Velocity_Y = 20;
            Initial_Velocity_Z = 20;

            Initial_Acceleration_X = 0;
            Initial_Acceleration_Y = 0;
            Initial_Acceleration_Z = 0;

            Initial_Orientation_X = 20;
            Initial_Orientation_Y = 40;
            Initial_Orientation_Z = 50;

            Initial_Eangle_EL = 10;
            Initial_Eangle_AZ = 20;

            Initial_Mangle_EL = 30;
            Initial_Mangle_AZ = -10;

            Number_Antenna_AZ = 20;
            Number_Antenna_EL = 10;

            Beamwidth_AZ = 10;
            Beamwidth_EL = 10;

            Resolution_AZ = 100;
            Resolution_EL = 100;

            Element_Spacing = 0.5;
            Backlobe_Scaling = 50;

            Window_type_s = "Hann Window";
            Cut_type_s = "";

        }
        public string Generate_file_report_s()
        {
            string result = "";
            result += "DRBE Receiver\r\n";
            result += "Object ID: " + "{" + ID.ToString() + "}" + "\r\n";
            result += "--------------------Initial Property-------------------------\r\n";
            result += "Object is Transmitter: " + "{" + Is_Transmitter.ToString() + "}" + "\r\n";
            result += "Object is Reflector: " + "{" + Is_Reflector.ToString() + "}" + "\r\n";
            result += "Object is Receiver: " + "{" + Is_Receiver.ToString() + "}" + "\r\n";
            result += "Object is Stationary: " + "{" + Is_Stationary.ToString() + "}" + "\r\n";
            result += "Coordination System: " + "{" + Coordinate_system.ToString() + "}" + "\r\n";
            result += "If using Cartesian \\ Earth Cent: ------------------------------------------------------\r\n";
            result += "Initial Position X: " + "{" + Initial_Position_X.ToString() + "}" + "\r\n";
            result += "Initial Position Y: " + "{" + Initial_Position_Y.ToString() + "}" + "\r\n";
            result += "Initial Position Z: " + "{" + Initial_Position_Z.ToString() + "}" + "\r\n";
            result += "If using Global Frame \\ WGS84: --------------------------------------------------------\r\n";
            result += "Latitude: " + "{" + Initial_Latitude.ToString() + "}" + "\r\n";
            result += "longtitude: " + "{" + Initial_Longtitude.ToString() + "}" + "\r\n";
            result += "If using Scenario Center Frame: --------------------------------------------------------\r\n";
            result += "Initial Relation to Reference Position X: " + "{" + Initial_Ref_Position_X.ToString() + "}" + "\r\n";
            result += "Initial Relation to Reference Position Y: " + "{" + Initial_Ref_Position_Y.ToString() + "}" + "\r\n";
            result += "Initial Relation to Reference Position Z: " + "{" + Initial_Ref_Position_Z.ToString() + "}" + "\r\n";
            result += "If using Object Reference Frame: -------------------------------------------------------\r\n";
            result += "Referenced Object ID: " + "{" + Reference_Obj_ID.ToString() + "}" + "\r\n";
            result += "Velocity: ------------------------------------------------------------------------------\r\n";
            result += "Initial Velocity X: " + "{" + Initial_Velocity_X.ToString() + "}" + "\r\n";
            result += "Initial Velocity Y: " + "{" + Initial_Velocity_Y.ToString() + "}" + "\r\n";
            result += "Initial Velocity Z: " + "{" + Initial_Velocity_Z.ToString() + "}" + "\r\n";
            result += "Acceleration: --------------------------------------------------------------------------\r\n";
            result += "Initial Acceleration X: " + "{" + Initial_Acceleration_X.ToString() + "}" + "\r\n";
            result += "Initial Acceleration Y: " + "{" + Initial_Acceleration_Y.ToString() + "}" + "\r\n";
            result += "Initial Acceleration Z: " + "{" + Initial_Acceleration_Z.ToString() + "}" + "\r\n";
            result += "Orientation: ---------------------------------------------------------------------------\r\n";
            result += "Initial Pitch: " + "{" + Initial_Orientation_X.ToString() + "}" + "\r\n";
            result += "Initial Roll : " + "{" + Initial_Orientation_Y.ToString() + "}" + "\r\n";
            result += "Initial Yaw  : " + "{" + Initial_Orientation_Z.ToString() + "}" + "\r\n";
            result += "Steering Angle: ------------------------------------------------------------------------\r\n";
            result += "Electronic Steering Angle Azimuth: " + "{" + Initial_Eangle_AZ.ToString() + "}" + "\r\n";
            result += "Electronic Steering Angle Elevation: " + "{" + Initial_Eangle_EL.ToString() + "}" + "\r\n";
            result += "Mechanical Steering Angle Azimuth: " + "{" + Initial_Mangle_AZ.ToString() + "}" + "\r\n";
            result += "Mechanical Steering Angle Elevation: " + "{" + Initial_Mangle_EL.ToString() + "}" + "\r\n";
            result += "Antenna Fidelity Order 4: --------------------------------------------------------------\r\n";
            result += "Number of Antenna on Azimuth: " + "{" + Number_Antenna_AZ.ToString() + "}" + "\r\n";
            result += "Number of Antenna on Elevation: " + "{" + Number_Antenna_EL.ToString() + "}" + "\r\n";
            result += "Element Spacing: " + "{" + Element_Spacing.ToString() + "}" + "\r\n";
            result += "Beam width on Azimuth: " + "{" + Beamwidth_AZ.ToString() + "}" + "\r\n";
            result += "Beam width on Elevation: " + "{" + Beamwidth_EL.ToString() + "}" + "\r\n";
            result += "Angle Resolution on Azimuth: " + "{" + Resolution_AZ.ToString() + "}" + "\r\n";
            result += "Angle Resolution on Elevation: " + "{" + Resolution_EL.ToString() + "}" + "\r\n";
            result += "Backlobe Scaling: " + "{" + Backlobe_Scaling.ToString() + "}" + "\r\n";
            result += "Window Type: " + "{" + Window_type.ToString() + "}" + "\r\n";
            result += "Cut Type: " + "{" + Cut_type.ToString() + "}" + "\r\n";
            result += "Antenna Constant: " + "{" + Antenna_constant.ToString() + "}" + "\r\n";
            result += "Polarization: --------------------------------------------------------------------------\r\n";
            result += "Polarization Coefficient 1: " + "{" + Polar_coefficient1.ToString() + "}" + "\r\n";
            result += "Polarization Coefficient 2: " + "{" + Polar_coefficient2.ToString() + "}" + "\r\n";
            result += "Clutter     : --------------------------------------------------------------------------\r\n";
            result += "Gamma Distribution K: " + "{" + Clutter_Gamma_k.ToString() + "}" + "\r\n";
            result += "Gamma Distribution theta: " + "{" + Clutter_Gamma_theta.ToString() + "}" + "\r\n";
            result += "Gausian Distribution M: " + "{" + Clutter_Gausian_m.ToString() + "}" + "\r\n";
            result += "Gausian Distribution V: " + "{" + Clutter_Gausian_v.ToString() + "}" + "\r\n";
            result += "Constraint  : --------------------------------------------------------------------------\r\n";
            result += "Center of Movement X: " + "{" + Constraint_centerX.ToString() + "}" + "\r\n";
            result += "Center of Movement Y: " + "{" + Constraint_centerY.ToString() + "}" + "\r\n";
            result += "Center of Movement Z: " + "{" + Constraint_centerZ.ToString() + "}" + "\r\n";
            result += "Maximum Height: " + "{" + Constraint_max_height.ToString() + "}" + "\r\n";
            result += "Radius: " + "{" + Constraint_radius.ToString() + "}" + "\r\n";
            result += "Maximum Speed: " + "{" + Constraint_max_speed.ToString() + "}" + "\r\n";
            result += "Maximum Acceleration: " + "{" + Constraint_max_acceleration.ToString() + "}" + "\r\n";
            result += "Maximum Angular Velocity: " + "{" + Constraint_max_orientation.ToString() + "}" + "\r\n";
            result += "Maximum band: " + "{" + Constraint_max_band.ToString() + "}" + "\r\n";
            result += "Minimum band: " + "{" + Constraint_min_band.ToString() + "}" + "\r\n";
            result += "Maximum band change interval: " + "{" + Constraint_max_band_change.ToString() + "}" + "\r\n";
            result += "Just added: ---------------------------------------------------------------------------\r\n";
            result += "Antenna Dictionary Size: " + "{" + Dictionary_dimension.ToString() + "}" + "\r\n";
            result += "Antenna Fidelity Order: " + "{" + Antenna_order.ToString() + "}" + "\r\n";
            result += "RCS number of polarization: " + "{" + RCS_number_of_polarization.ToString() + "}" + "\r\n";
            result += "RCS output time sample size: " + "{" + RCS_output_time_sampe.ToString() + "}" + "\r\n";
            result += "RCS point: " + "{" + RCS_point.ToString() + "}" + "\r\n";
            result += "RCS Angle resolution: " + "{" + RCS_angle_resolution.ToString() + "}" + "\r\n";
            result += "RCS Frequency point: " + "{" + RCS_frequency_point.ToString() + "}" + "\r\n";
            result += "RCS Fidelity Order: " + "{" + RCS_order.ToString() + "}" + "\r\n";
            result += "End of Object: " + "{EO}" + "\r\n";
            return result;
        }
        public string Generate_file_report()
        {
            string result = "";
            result += "DRBE Receiver\r\n";
            result += "Object ID: " + "{" + ID.ToString() + "}" + "\r\n";
            result += "--------------------Initial Property-------------------------\r\n";
            result += "Coordination System: " + "{" + Coordinate_system + "}" + "\r\n";
            result += "If using Cartesian \\ Earth Cent: ------------------------------------------------------\r\n";
            result += "Initial Position X: " + "{" + Initial_Position_X.ToString() + "}" + "\r\n";
            result += "Initial Position Y: " + "{" + Initial_Position_Y.ToString() + "}" + "\r\n";
            result += "Initial Position Z: " + "{" + Initial_Position_Z.ToString() + "}" + "\r\n";
            result += "If using Global Frame \\ WGS84: --------------------------------------------------------\r\n";
            result += "Latitude: " + "{" + Initial_Latitude.ToString() + "}" + "\r\n";
            result += "longtitude: " + "{" + Initial_Longtitude.ToString() + "}" + "\r\n";
            result += "If using Scenario Center Frame: --------------------------------------------------------\r\n";
            result += "Initial Relation to Reference Position X: " + "{" + Initial_Ref_Position_X.ToString() + "}" + "\r\n";
            result += "Initial Relation to Reference Position Y: " + "{" + Initial_Ref_Position_Y.ToString() + "}" + "\r\n";
            result += "Initial Relation to Reference Position Z: " + "{" + Initial_Ref_Position_Z.ToString() + "}" + "\r\n";
            result += "If using Object Reference Frame: -------------------------------------------------------\r\n";
            result += "Referenced Object ID: " + "{" + Reference_Obj_ID.ToString() + "}" + "\r\n";
            result += "Velocity: ------------------------------------------------------------------------------\r\n";
            result += "Initial Velocity X: " + "{" + Initial_Velocity_X.ToString() + "}" + "\r\n";
            result += "Initial Velocity Y: " + "{" + Initial_Velocity_Y.ToString() + "}" + "\r\n";
            result += "Initial Velocity Z: " + "{" + Initial_Velocity_Z.ToString() + "}" + "\r\n";
            result += "Acceleration: --------------------------------------------------------------------------\r\n";
            result += "Initial Acceleration X: " + "{" + Initial_Acceleration_X.ToString() + "}" + "\r\n";
            result += "Initial Acceleration Y: " + "{" + Initial_Acceleration_Y.ToString() + "}" + "\r\n";
            result += "Initial Acceleration Z: " + "{" + Initial_Acceleration_Z.ToString() + "}" + "\r\n";
            result += "Orientation: ---------------------------------------------------------------------------\r\n";
            result += "Initial Pitch: " + "{" + Initial_Orientation_X.ToString() + "}" + "\r\n";
            result += "Initial Roll : " + "{" + Initial_Orientation_Y.ToString() + "}" + "\r\n";
            result += "Initial Yaw  : " + "{" + Initial_Orientation_Z.ToString() + "}" + "\r\n";
            result += "Steering Angle: ------------------------------------------------------------------------\r\n";
            result += "Electronic Steering Angle Azimuth: " + "{" + Initial_Eangle_AZ.ToString() + "}" + "\r\n";
            result += "Electronic Steering Angle Elevation: " + "{" + Initial_Eangle_EL.ToString() + "}" + "\r\n";
            result += "Mechanical Steering Angle Azimuth: " + "{" + Initial_Mangle_AZ.ToString() + "}" + "\r\n";
            result += "Mechanical Steering Angle Elevation: " + "{" + Initial_Mangle_EL.ToString() + "}" + "\r\n";
            result += "Antenna Fidelity Order 4: ------ -------------------------------------------------------\r\n";
            result += "Number of Antenna on Azimuth: " + "{" + Number_Antenna_AZ.ToString() + "}" + "\r\n";
            result += "Number of Antenna on Elevation: " + "{" + Number_Antenna_EL.ToString() + "}" + "\r\n";
            result += "Element Spacing: " + "{" + Element_Spacing.ToString() + "}" + "\r\n";
            result += "Beam width on Azimuth: " + "{" + Beamwidth_AZ.ToString() + "}" + "\r\n";
            result += "Beam width on Elevation: " + "{" + Beamwidth_EL.ToString() + "}" + "\r\n";
            result += "Backlobe Scaling: " + "{" + Backlobe_Scaling.ToString() + "}" + "\r\n";
            return result;
        }

        public string Summary()
        {
            string result = "";

            result += "Object ID: " + ID.ToString() + "\r\n";
            result += "Initial Position X: " + Initial_Position_X.ToString() + "\r\n";
            result += "Initial Position Y: " + Initial_Position_Y.ToString() + "\r\n";
            result += "Initial Position Z: " + Initial_Position_Z.ToString() + "\r\n";
            result += "Initial Velocity X: " + Initial_Velocity_X.ToString() + "\r\n";
            result += "Initial Velocity Y: " + Initial_Velocity_Y.ToString() + "\r\n";
            result += "Initial Velocity Z: " + Initial_Velocity_Z.ToString() + "\r\n";
            result += "Initial Acceleration X: " + Initial_Acceleration_X.ToString() + "\r\n";
            result += "Initial Acceleration Y: " + Initial_Acceleration_Y.ToString() + "\r\n";
            result += "Initial Acceleration Z: " + Initial_Acceleration_Z.ToString() + "\r\n";
            result += "Initial Orientation Pitch: " + Initial_Orientation_X.ToString() + "\r\n";
            result += "Initial Orientation Roll: " + Initial_Orientation_Y.ToString() + "\r\n";
            result += "Initial Orientation Yaw: " + Initial_Orientation_Z.ToString() + "\r\n";
            result += "Initial Electronic Angle Azimuth: " + Initial_Eangle_EL.ToString() + "\r\n";
            result += "Initial Electronic Angle Elevation: " + Initial_Eangle_AZ.ToString() + "\r\n";
            result += "Initial Mechanical Angle Azimuth: " + Initial_Mangle_EL.ToString() + "\r\n";
            result += "Initial Mechanical Angle Elevation: " + Initial_Mangle_AZ.ToString() + "\r\n";
            result += "Initial Number of Antenna Azimuth: " + Number_Antenna_AZ.ToString() + "\r\n";
            result += "Initial Number of Antenna Elevation: " + Number_Antenna_EL.ToString() + "\r\n";
            result += "Initial Beamwidth Azimuth: " + Beamwidth_AZ.ToString() + "\r\n";
            result += "Initial Beamwidth Elevation: " + Beamwidth_EL.ToString() + "\r\n";

            result += "Initial Antenna Spacing: " + Element_Spacing.ToString() + "\r\n";
            result += "Initial Backlobe Scaling: " + Backlobe_Scaling.ToString() + "\r\n";
            result += "Initial Pattern Window Type: " + Window_type.ToString() + "\r\n";
            result += "Initial Pattern Cut Type: " + Cut_type + "\r\n";

            return result;
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
        private byte S_B(string x)
        {
            byte result = 0;
            int temp = S_H(x);
            result = BitConverter.GetBytes(temp)[0];
            //ShowDialog(result.ToString(),result.ToString());
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
