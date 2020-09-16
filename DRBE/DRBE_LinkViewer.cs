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
using Windows.ApplicationModel.Background;
using Windows.Services.TargetedContent;
using Windows.Networking.Sockets;

namespace DRBE
{
    public class DRBE_LinkViewer
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
        private Save_Screen DRBE_LV_SS;
        private Sim_sweep DRBE_sweep;
        public DRBE_LinkViewer(Grid parent, MainPage parentpage)
        {
            ParentGrid = parent;
            ParentPage = parentpage;
            DRBE_LV_SS = new Save_Screen(parent);
            Setup();
            hide();
        }

        private TextBlock Sweep_tb = new TextBlock();
        private Button Sweep_bt = new Button();
        private Image Sweep_i = new Image();

        private TextBlock Obj_save_tb = new TextBlock();
        private Button Obj_save_bt = new Button();
        private Image Obj_save_i = new Image();

        private TextBlock Obj_summary_tb = new TextBlock();
        private Button Obj_summary_bt = new Button();
        private Image Obj_summary_i = new Image();

        private Button Scenario_gen_bt = new Button();
        private Image Scenario_gen_i = new Image();

        private List<Button> DT_bt = new List<Button>();
        private List<Image> DT_i = new List<Image>();
        private List<TextBlock> DT_tb = new List<TextBlock>();
        private List<StackPanel> DT_sp = new List<StackPanel>();
        private List<Grid> DT_gd = new List<Grid>();

        private ScrollViewer DT_sv = new ScrollViewer();
        private StackPanel DTp_sp = new StackPanel();
        private Grid DTp_gd = new Grid();

        private List<Button> DR_bt = new List<Button>();
        private List<Image> DR_i = new List<Image>();
        private List<TextBlock> DR_tb = new List<TextBlock>();
        private List<StackPanel> DR_sp = new List<StackPanel>();
        private List<Grid> DR_gd = new List<Grid>();

        private ScrollViewer DR_sv = new ScrollViewer();
        private StackPanel DRp_sp = new StackPanel();
        private Grid DRp_gd = new Grid();

        private List<Button> DF_bt = new List<Button>();
        private List<Image> DF_i = new List<Image>();
        private List<TextBlock> DF_tb = new List<TextBlock>();
        private List<StackPanel> DF_sp = new List<StackPanel>();
        private List<Grid> DF_gd = new List<Grid>();

        private ScrollViewer DF_sv = new ScrollViewer();
        private StackPanel DFp_sp = new StackPanel();
        private Grid DFp_gd = new Grid();

        private List<Button> SingleO_bt = new List<Button>();
        private List<Image> SingleO_i = new List<Image>();
        private List<TextBlock> SingleO_tb = new List<TextBlock>();
        private List<StackPanel> SingleO_sp = new List<StackPanel>();
        private List<Grid> SingleO_gd = new List<Grid>();

        private ScrollViewer SingleOP_sv = new ScrollViewer();
        private StackPanel SingleOP_sp = new StackPanel();
        private Grid SingleOP_gd = new Grid();

        private Button Temp_singleo_bt;

        private Dictionary<Button, int> Dic_tbt_i = new Dictionary<Button, int>();
        private Dictionary<Button, int> Dic_pbt_i = new Dictionary<Button, int>();
        private Dictionary<Button, int> Dic_rbt_i = new Dictionary<Button, int>();
        private Dictionary<Button, int> Dic_SObt_i = new Dictionary<Button, int>();

        private int bt_height = 10;
        private int bt_width = 20;
        private int tb_height = 5;

        private int EndInd_T = 8;

        //private Dictionary<Button, DRBE_Transmitter> D_BT_DT = new Dictionary<Button, DRBE_Transmitter>();
        //private Dictionary<Button, DRBE_Receiver> D_BT_DR = new Dictionary<Button, DRBE_Receiver>();
        //private Dictionary<Button, DRBE_Reflector> D_BT_DRF = new Dictionary<Button, DRBE_Reflector>();

        private TextBlock Transmitter_title_tb = new TextBlock();
        private TextBlock Reflector_title_tb = new TextBlock();
        private TextBlock Receiver_title_tb = new TextBlock();
        private TextBlock SingleO_title_tb = new TextBlock();

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

        private TextBlock Link_de_tb = new TextBlock();
        private Button Link_de_bt = new Button();
        private Image Link_de_bt_i = new Image();

        private TextBlock Link_en_tb = new TextBlock();
        private Button Link_en_bt = new Button();
        private Image Link_en_bt_i = new Image();

        private TextBlock Link_uns_tb = new TextBlock();
        private Button Link_uns_bt = new Button();
        private Image Link_uns_bt_i = new Image();

        //public List<DRBE_Transmitter> Lv_dtl = new List<DRBE_Transmitter>();
        //public List<DRBE_Reflector> Lv_dfl = new List<DRBE_Reflector>();
        //public List<DRBE_Receiver> Lv_drl = new List<DRBE_Receiver>();

        private List<DRBE_Objs> DRBE_obj_list;

        private Dictionary<int, DRBE_Objs> Dic_t_i_obj;
        private Dictionary<int, DRBE_Objs> Dic_o_i_obj;
        private Dictionary<int, DRBE_Objs> Dic_r_i_obj;

        private Dictionary<DRBE_Objs, int> Dic_i_t_obj;
        private Dictionary<DRBE_Objs, int> Dic_i_o_obj;
        private Dictionary<DRBE_Objs, int> Dic_i_r_obj;
        private List<List<List<bool>>> Link_list;

        private Button AMT_bt = new Button();
        private Image AMT_bti = new Image();
        private TextBlock AMT_tb = new TextBlock();

        private Button CMP_bt = new Button();
        private Image CMP_bti = new Image();
        private TextBlock CMP_tb = new TextBlock();



        #region presentation choice
        private int Presentation_choice_mode = 0;
        private List<double> Obj_resource_record = new List<double>();

        private Button DRBEP_rescan_bt = new Button();
        private Grid DRBEP_rescan_gd = new Grid();
        private TextBlock DRBEP_rescan_tb = new TextBlock();
        private Image DRBEP_rescan_i = new Image();
        private StackPanel DRBEP_rescan_sp = new StackPanel();

        private Button DRBEP_Global_resource_bt = new Button();
        private Grid DRBEP_Global_resource_gd = new Grid();
        private TextBlock DRBEP_Global_resource_tb = new TextBlock();
        private Image DRBEP_Global_resource_i = new Image();
        private StackPanel DRBEP_Global_resource_sp = new StackPanel();

        private Button DRBEP_Global_memory_bt = new Button();
        private Grid DRBEP_Global_memory_gd = new Grid();
        private TextBlock DRBEP_Global_memory_tb = new TextBlock();
        private Image DRBEP_Global_memory_i = new Image();
        private StackPanel DRBEP_Global_memory_sp = new StackPanel();

        private Button DRBEP_Global_bandwidth_bt = new Button();
        private Grid DRBEP_Global_bandwidth_gd = new Grid();
        private TextBlock DRBEP_Global_bandwidth_tb = new TextBlock();
        private Image DRBEP_Global_bandwidth_i = new Image();
        private StackPanel DRBEP_Global_bandwidth_sp = new StackPanel();

        private Button DRBEP_Global_latency_bt = new Button();
        private Grid DRBEP_Global_latency_gd = new Grid();
        private TextBlock DRBEP_Global_latency_tb = new TextBlock();
        private Image DRBEP_Global_latency_i = new Image();
        private StackPanel DRBEP_Global_latency_sp = new StackPanel();

        private Button DRBEP_Global_computation_bt = new Button();
        private Grid DRBEP_Global_computation_gd = new Grid();
        private TextBlock DRBEP_Global_computation_tb = new TextBlock();
        private Image DRBEP_Global_computation_i = new Image();
        private StackPanel DRBEP_Global_computation_sp = new StackPanel();

        private Button DRBEOP_rescan_bt = new Button();
        private Grid DRBEOP_rescan_gd = new Grid();
        private TextBlock DRBEOP_rescan_tb = new TextBlock();
        private Image DRBEOP_rescan_i = new Image();
        private StackPanel DRBEOP_rescan_sp = new StackPanel();

        private Button DRBEP_obj_property_c_bt = new Button();
        private Grid DRBEP_obj_property_c_gd = new Grid();
        private TextBlock DRBEP_obj_property_c_tb = new TextBlock();
        private Image DRBEP_obj_property_c_i = new Image();
        private StackPanel DRBEP_obj_property_c_sp = new StackPanel();

        private Button DRBEP_obj_fidelity_c_bt = new Button();
        private Grid DRBEP_obj_fidelity_c_gd = new Grid();
        private TextBlock DRBEP_obj_fidelity_c_tb = new TextBlock();
        private Image DRBEP_obj_fidelity_c_i = new Image();
        private StackPanel DRBEP_obj_fidelity_c_sp = new StackPanel();

        private Button DRBEP_obj_property_m_bt = new Button();
        private Grid DRBEP_obj_property_m_gd = new Grid();
        private TextBlock DRBEP_obj_property_m_tb = new TextBlock();
        private Image DRBEP_obj_property_m_i = new Image();
        private StackPanel DRBEP_obj_property_m_sp = new StackPanel();

        private Button DRBEP_obj_fidelity_m_bt = new Button();
        private Grid DRBEP_obj_fidelity_m_gd = new Grid();
        private TextBlock DRBEP_obj_fidelity_m_tb = new TextBlock();
        private Image DRBEP_obj_fidelity_m_i = new Image();
        private StackPanel DRBEP_obj_fidelity_m_sp = new StackPanel();

        private Button DRBEP_obj_property_b_bt = new Button();
        private Grid DRBEP_obj_property_b_gd = new Grid();
        private TextBlock DRBEP_obj_property_b_tb = new TextBlock();
        private Image DRBEP_obj_property_b_i = new Image();
        private StackPanel DRBEP_obj_property_b_sp = new StackPanel();

        private Button DRBEP_obj_fidelity_b_bt = new Button();
        private Grid DRBEP_obj_fidelity_b_gd = new Grid();
        private TextBlock DRBEP_obj_fidelity_b_tb = new TextBlock();
        private Image DRBEP_obj_fidelity_b_i = new Image();
        private StackPanel DRBEP_obj_fidelity_b_sp = new StackPanel();

        private Button DRBEP_obj_property_l_bt = new Button();
        private Grid DRBEP_obj_property_l_gd = new Grid();
        private TextBlock DRBEP_obj_property_l_tb = new TextBlock();
        private Image DRBEP_obj_property_l_i = new Image();
        private StackPanel DRBEP_obj_property_l_sp = new StackPanel();

        private Button DRBEP_obj_fidelity_l_bt = new Button();
        private Grid DRBEP_obj_fidelity_l_gd = new Grid();
        private TextBlock DRBEP_obj_fidelity_l_tb = new TextBlock();
        private Image DRBEP_obj_fidelity_l_i = new Image();
        private StackPanel DRBEP_obj_fidelity_l_sp = new StackPanel();
        #endregion

        #region obj rank setup
        private Border OR_up_bd = new Border();
        private TextBlock OR_label_tb = new TextBlock();
        private Border OR_down_bd = new Border();

        private List<Grid> OR_gd = new List<Grid>();
        private List<TextBlock> OR_l_tb = new List<TextBlock>();
        private List<TextBlock> OR_i_tb = new List<TextBlock>();
        private List<ProgressBar> OR_pb = new List<ProgressBar>();

        private double OR_max = 0;

        #endregion
        #region presentation setup
        private ScrollViewer RS_sv = new ScrollViewer();
        private StackPanel RS_sp = new StackPanel();
        private Grid RS_gd = new Grid();
        private StackPanel RS_label_sp = new StackPanel();
        private StackPanel RS_pb_sp = new StackPanel();
        private StackPanel RS_info_sp = new StackPanel();
        #region Class Resource

        #region Scan label
        private Border S_up_bd = new Border();
        private TextBlock S_label_tb = new TextBlock();
        private Border S_down_bd = new Border();
        #endregion

        #region Scan
        private Grid Scan_gd = new Grid();
        private TextBlock Scan_l_tb = new TextBlock();
        private TextBlock Scan_i_tb = new TextBlock();
        private ProgressBar Scan_pb = new ProgressBar();
        private bool Global_scanned_flag = false;
        #endregion

        #region Fidelity

        #region RCS
        #region label
        private Border FRCS_up_bd = new Border();
        private TextBlock FRCS_label_tb = new TextBlock();
        private Border FRCS_down_bd = new Border();

        #endregion

        #region order
        private Grid FRCS0_gd = new Grid();
        private TextBlock FRCS0_l_tb = new TextBlock();
        private TextBlock FRCS0_i_tb = new TextBlock();
        private ProgressBar FRCS0_pb = new ProgressBar();

        private Grid FRCS1_gd = new Grid();
        private TextBlock FRCS1_l_tb = new TextBlock();
        private TextBlock FRCS1_i_tb = new TextBlock();
        private ProgressBar FRCS1_pb = new ProgressBar();

        private Grid FRCS2_gd = new Grid();
        private TextBlock FRCS2_l_tb = new TextBlock();
        private TextBlock FRCS2_i_tb = new TextBlock();
        private ProgressBar FRCS2_pb = new ProgressBar();

        private Grid FRCS3_gd = new Grid();
        private TextBlock FRCS3_l_tb = new TextBlock();
        private TextBlock FRCS3_i_tb = new TextBlock();
        private ProgressBar FRCS3_pb = new ProgressBar();

        private Grid FRCS4_gd = new Grid();
        private TextBlock FRCS4_l_tb = new TextBlock();
        private TextBlock FRCS4_i_tb = new TextBlock();
        private ProgressBar FRCS4_pb = new ProgressBar();

        private Grid FRCS5_gd = new Grid();
        private TextBlock FRCS5_l_tb = new TextBlock();
        private TextBlock FRCS5_i_tb = new TextBlock();
        private ProgressBar FRCS5_pb = new ProgressBar();

        private Grid FRCS6_gd = new Grid();
        private TextBlock FRCS6_l_tb = new TextBlock();
        private TextBlock FRCS6_i_tb = new TextBlock();
        private ProgressBar FRCS6_pb = new ProgressBar();
        #endregion


        #endregion
        #region ANT

        #region label
        private Border FANT_up_bd = new Border();
        private TextBlock FANT_label_tb = new TextBlock();
        private Border FANT_down_bd = new Border();

        #endregion

        #region order
        private Grid FANT0_gd = new Grid();
        private TextBlock FANT0_l_tb = new TextBlock();
        private TextBlock FANT0_i_tb = new TextBlock();
        private ProgressBar FANT0_pb = new ProgressBar();

        private Grid FANT1_gd = new Grid();
        private TextBlock FANT1_l_tb = new TextBlock();
        private TextBlock FANT1_i_tb = new TextBlock();
        private ProgressBar FANT1_pb = new ProgressBar();

        private Grid FANT2_gd = new Grid();
        private TextBlock FANT2_l_tb = new TextBlock();
        private TextBlock FANT2_i_tb = new TextBlock();
        private ProgressBar FANT2_pb = new ProgressBar();

        private Grid FANT3_gd = new Grid();
        private TextBlock FANT3_l_tb = new TextBlock();
        private TextBlock FANT3_i_tb = new TextBlock();
        private ProgressBar FANT3_pb = new ProgressBar();

        private Grid FANT4_gd = new Grid();
        private TextBlock FANT4_l_tb = new TextBlock();
        private TextBlock FANT4_i_tb = new TextBlock();
        private ProgressBar FANT4_pb = new ProgressBar();

        private Grid FANT5_gd = new Grid();
        private TextBlock FANT5_l_tb = new TextBlock();
        private TextBlock FANT5_i_tb = new TextBlock();
        private ProgressBar FANT5_pb = new ProgressBar();
        #endregion

        #endregion

        #endregion

        #region Property
        #region RCS

        #region label
        private Border PRCS_up_bd = new Border();
        private TextBlock PRCS_label_tb = new TextBlock();
        private Border PRCS_down_bd = new Border();

        #endregion

        #region Prop
        //scatter point
        private Grid PRCS_SP_gd = new Grid();
        private TextBlock PRCS_SP_l_tb = new TextBlock();
        private TextBlock PRCS_SP_pi_tb = new TextBlock();
        private TextBlock PRCS_SP_ni_tb = new TextBlock();
        private TextBlock PRCS_SP_TL_tb = new TextBlock();
        private TextBlock PRCS_SP_TR_tb = new TextBlock();
        private ProgressBar PRCS_SPL_pb = new ProgressBar();
        private ProgressBar PRCS_SPR_pb = new ProgressBar();
        //Angle resolution
        private Grid PRCS_AR_gd = new Grid();
        private TextBlock PRCS_AR_l_tb = new TextBlock();
        private TextBlock PRCS_AR_pi_tb = new TextBlock();
        private TextBlock PRCS_AR_ni_tb = new TextBlock();
        private TextBlock PRCS_AR_TL_tb = new TextBlock();
        private TextBlock PRCS_AR_TR_tb = new TextBlock();
        private ProgressBar PRCS_ARL_pb = new ProgressBar();
        private ProgressBar PRCS_ARR_pb = new ProgressBar();
        //Frequency Bins
        private Grid PRCS_FB_gd = new Grid();
        private TextBlock PRCS_FB_l_tb = new TextBlock();
        private TextBlock PRCS_FB_pi_tb = new TextBlock();
        private TextBlock PRCS_FB_ni_tb = new TextBlock();
        private TextBlock PRCS_FB_TL_tb = new TextBlock();
        private TextBlock PRCS_FB_TR_tb = new TextBlock();
        private ProgressBar PRCS_FBL_pb = new ProgressBar();
        private ProgressBar PRCS_FBR_pb = new ProgressBar();
        //Sample Size
        private Grid PRCS_SS_gd = new Grid();
        private TextBlock PRCS_SS_l_tb = new TextBlock();
        private TextBlock PRCS_SS_pi_tb = new TextBlock();
        private TextBlock PRCS_SS_ni_tb = new TextBlock();
        private TextBlock PRCS_SS_TL_tb = new TextBlock();
        private TextBlock PRCS_SS_TR_tb = new TextBlock();
        private ProgressBar PRCS_SSL_pb = new ProgressBar();
        private ProgressBar PRCS_SSR_pb = new ProgressBar();
        //Polarization Number
        private Grid PRCS_PN_gd = new Grid();
        private TextBlock PRCS_PN_l_tb = new TextBlock();
        private TextBlock PRCS_PN_pi_tb = new TextBlock();
        private TextBlock PRCS_PN_ni_tb = new TextBlock();
        private TextBlock PRCS_PN_TL_tb = new TextBlock();
        private TextBlock PRCS_PN_TR_tb = new TextBlock();
        private ProgressBar PRCS_PNL_pb = new ProgressBar();
        private ProgressBar PRCS_PNR_pb = new ProgressBar();
        #endregion
        #endregion
        #region ANT
        #region label
        private Border PANT_up_bd = new Border();
        private TextBlock PANT_label_tb = new TextBlock();
        private Border PANT_down_bd = new Border();

        #endregion
        #region Prop
        //Number of antenna
        private Grid PANT_NA_gd = new Grid();
        private TextBlock PANT_NA_l_tb = new TextBlock();
        private TextBlock PANT_NA_pi_tb = new TextBlock();
        private TextBlock PANT_NA_ni_tb = new TextBlock();
        private TextBlock PANT_NA_TL_tb = new TextBlock();
        private TextBlock PANT_NA_TR_tb = new TextBlock();
        private ProgressBar PANT_NAL_pb = new ProgressBar();
        private ProgressBar PANT_NAR_pb = new ProgressBar();

        //Angle Resolution
        private Grid PANT_AR_gd = new Grid();
        private TextBlock PANT_AR_l_tb = new TextBlock();
        private TextBlock PANT_AR_pi_tb = new TextBlock();
        private TextBlock PANT_AR_ni_tb = new TextBlock();
        private TextBlock PANT_AR_TL_tb = new TextBlock();
        private TextBlock PANT_AR_TR_tb = new TextBlock();
        private ProgressBar PANT_ARL_pb = new ProgressBar();
        private ProgressBar PANT_ARR_pb = new ProgressBar();

        //Dictionary Size
        private Grid PANT_DS_gd = new Grid();
        private TextBlock PANT_DS_l_tb = new TextBlock();
        private TextBlock PANT_DS_pi_tb = new TextBlock();
        private TextBlock PANT_DS_ni_tb = new TextBlock();
        private TextBlock PANT_DS_TL_tb = new TextBlock();
        private TextBlock PANT_DS_TR_tb = new TextBlock();
        private ProgressBar PANT_DSL_pb = new ProgressBar();
        private ProgressBar PANT_DSR_pb = new ProgressBar();
        #endregion
        #endregion
        #endregion

        #region computation Memory Bandwidth Lantency

        #region label
        private Border C_up_bd = new Border();
        private TextBlock C_label_tb = new TextBlock();
        private Border C_down_bd = new Border();

        private Border M_up_bd = new Border();
        private TextBlock M_label_tb = new TextBlock();
        private Border M_down_bd = new Border();

        private Border B_up_bd = new Border();
        private TextBlock B_label_tb = new TextBlock();
        private Border B_down_bd = new Border();

        private Border L_up_bd = new Border();
        private TextBlock L_label_tb = new TextBlock();
        private Border L_down_bd = new Border();
        #endregion

        #region  Global Object
        private List<Grid> C_obj_gd_l = new List<Grid>();
        private List<TextBlock> C_obj_l_tb_l = new List<TextBlock>();
        private List<TextBlock> C_obj_i_tb_l = new List<TextBlock>();
        private List<ProgressBar> C_obj_pb_l = new List<ProgressBar>();
        #endregion 


        
        #region RCS
        private Grid C_RCS_gd = new Grid();
        private TextBlock C_RCS_l_tb = new TextBlock();
        private TextBlock C_RCS_i_tb = new TextBlock();
        private ProgressBar C_RCS_pb = new ProgressBar();

        private Grid M_RCS_gd = new Grid();
        private TextBlock M_RCS_l_tb = new TextBlock();
        private TextBlock M_RCS_i_tb = new TextBlock();
        private ProgressBar M_RCS_pb = new ProgressBar();

        private Grid B_RCS_gd = new Grid();
        private TextBlock B_RCS_l_tb = new TextBlock();
        private TextBlock B_RCS_i_tb = new TextBlock();
        private ProgressBar B_RCS_pb = new ProgressBar();

        private Grid L_RCS_gd = new Grid();
        private TextBlock L_RCS_l_tb = new TextBlock();
        private TextBlock L_RCS_i_tb = new TextBlock();
        private ProgressBar L_RCS_pb = new ProgressBar();
        #endregion

        #region Antenna
        private Grid C_ANT_gd = new Grid();
        private TextBlock C_ANT_l_tb = new TextBlock();
        private TextBlock C_ANT_i_tb = new TextBlock();
        private ProgressBar C_ANT_pb = new ProgressBar();

        private Grid M_ANT_gd = new Grid();
        private TextBlock M_ANT_l_tb = new TextBlock();
        private TextBlock M_ANT_i_tb = new TextBlock();
        private ProgressBar M_ANT_pb = new ProgressBar();

        private Grid B_ANT_gd = new Grid();
        private TextBlock B_ANT_l_tb = new TextBlock();
        private TextBlock B_ANT_i_tb = new TextBlock();
        private ProgressBar B_ANT_pb = new ProgressBar();

        private Grid L_ANT_gd = new Grid();
        private TextBlock L_ANT_l_tb = new TextBlock();
        private TextBlock L_ANT_i_tb = new TextBlock();
        private ProgressBar L_ANT_pb = new ProgressBar();
        #endregion

        #region Coordination 
        private Grid C_COR_gd = new Grid();
        private TextBlock C_COR_l_tb = new TextBlock();
        private TextBlock C_COR_i_tb = new TextBlock();
        private ProgressBar C_COR_pb = new ProgressBar();

        private Grid M_COR_gd = new Grid();
        private TextBlock M_COR_l_tb = new TextBlock();
        private TextBlock M_COR_i_tb = new TextBlock();
        private ProgressBar M_COR_pb = new ProgressBar();

        private Grid B_COR_gd = new Grid();
        private TextBlock B_COR_l_tb = new TextBlock();
        private TextBlock B_COR_i_tb = new TextBlock();
        private ProgressBar B_COR_pb = new ProgressBar();

        private Grid L_COR_gd = new Grid();
        private TextBlock L_COR_l_tb = new TextBlock();
        private TextBlock L_COR_i_tb = new TextBlock();
        private ProgressBar L_COR_pb = new ProgressBar();
        #endregion

        #region NR engine
        private Grid C_NRE_gd = new Grid();
        private TextBlock C_NRE_l_tb = new TextBlock();
        private TextBlock C_NRE_i_tb = new TextBlock();
        private ProgressBar C_NRE_pb = new ProgressBar();

        private Grid M_NRE_gd = new Grid();
        private TextBlock M_NRE_l_tb = new TextBlock();
        private TextBlock M_NRE_i_tb = new TextBlock();
        private ProgressBar M_NRE_pb = new ProgressBar();

        private Grid B_NRE_gd = new Grid();
        private TextBlock B_NRE_l_tb = new TextBlock();
        private TextBlock B_NRE_i_tb = new TextBlock();
        private ProgressBar B_NRE_pb = new ProgressBar();

        private Grid L_NRE_gd = new Grid();
        private TextBlock L_NRE_l_tb = new TextBlock();
        private TextBlock L_NRE_i_tb = new TextBlock();
        private ProgressBar L_NRE_pb = new ProgressBar();
        #endregion

        #region Orientation
        private Grid C_ORI_gd = new Grid();
        private TextBlock C_ORI_l_tb = new TextBlock();
        private TextBlock C_ORI_i_tb = new TextBlock();
        private ProgressBar C_ORI_pb = new ProgressBar();

        private Grid M_ORI_gd = new Grid();
        private TextBlock M_ORI_l_tb = new TextBlock();
        private TextBlock M_ORI_i_tb = new TextBlock();
        private ProgressBar M_ORI_pb = new ProgressBar();

        private Grid B_ORI_gd = new Grid();
        private TextBlock B_ORI_l_tb = new TextBlock();
        private TextBlock B_ORI_i_tb = new TextBlock();
        private ProgressBar B_ORI_pb = new ProgressBar();

        private Grid L_ORI_gd = new Grid();
        private TextBlock L_ORI_l_tb = new TextBlock();
        private TextBlock L_ORI_i_tb = new TextBlock();
        private ProgressBar L_ORI_pb = new ProgressBar();
        #endregion

        #region TU
        private Grid C_TU_gd = new Grid();
        private TextBlock C_TU_l_tb = new TextBlock();
        private TextBlock C_TU_i_tb = new TextBlock();
        private ProgressBar C_TU_pb = new ProgressBar();

        private Grid M_TU_gd = new Grid();
        private TextBlock M_TU_l_tb = new TextBlock();
        private TextBlock M_TU_i_tb = new TextBlock();
        private ProgressBar M_TU_pb = new ProgressBar();

        private Grid B_TU_gd = new Grid();
        private TextBlock B_TU_l_tb = new TextBlock();
        private TextBlock B_TU_i_tb = new TextBlock();
        private ProgressBar B_TU_pb = new ProgressBar();

        private Grid L_TU_gd = new Grid();
        private TextBlock L_TU_l_tb = new TextBlock();
        private TextBlock L_TU_i_tb = new TextBlock();
        private ProgressBar L_TU_pb = new ProgressBar();
        #endregion

        #region PG
        private Grid C_PG_gd = new Grid();
        private TextBlock C_PG_l_tb = new TextBlock();
        private TextBlock C_PG_i_tb = new TextBlock();
        private ProgressBar C_PG_pb = new ProgressBar();

        private Grid M_PG_gd = new Grid();
        private TextBlock M_PG_l_tb = new TextBlock();
        private TextBlock M_PG_i_tb = new TextBlock();
        private ProgressBar M_PG_pb = new ProgressBar();

        private Grid B_PG_gd = new Grid();
        private TextBlock B_PG_l_tb = new TextBlock();
        private TextBlock B_PG_i_tb = new TextBlock();
        private ProgressBar B_PG_pb = new ProgressBar();

        private Grid L_PG_gd = new Grid();
        private TextBlock L_PG_l_tb = new TextBlock();
        private TextBlock L_PG_i_tb = new TextBlock();
        private ProgressBar L_PG_pb = new ProgressBar();
        #endregion

        private double RCS_Compute = 0;
        private double ANT_Compute = 0;
        private double COR_Compute = 0;
        private double NRE_Compute = 0;
        private double ORI_Compute = 0;
        private double TU_Compute = 0;
        private double PG_Compute = 0;
        private double Computation_max = 500;

        private double RCS_Memory = 0;
        private double ANT_Memory = 0;
        private double COR_Memory = 0;
        private double NRE_Memory = 0;
        private double ORI_Memory = 0;
        private double TU_Memory = 0;
        private double PG_Memory = 0;
        private double Memory_max = 500;

        private double RCS_Bandwidth = 0;
        private double ANT_Bandwidth = 0;
        private double COR_Bandwidth = 0;
        private double NRE_Bandwidth = 0;
        private double ORI_Bandwidth = 0;
        private double TU_Bandwidth = 0;
        private double PG_Bandwidth = 0;
        private double Bandwidth_max = 500;

        private double RCS_Latency = 0;
        private double ANT_Latency = 0;
        private double COR_Latency = 0;
        private double NRE_Latency = 0;
        private double ORI_Latency = 0;
        private double TU_Latency = 0;
        private double PG_Latency = 0;
        private double Latency_max = 500;

        #endregion


        #endregion
        #endregion
        private int Scan_max = 0;
        private int Scan_max_obj = 0;
        private int Scan_cur_obj = 0;
        private void Presentation_choice_setup()
        {
            #region Rescan
            DRBEP_rescan_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush
            };

            DRBEP_rescan_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            DRBEP_rescan_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


            DRBEP_rescan_i = new Image()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Refresh.png", UriKind.RelativeOrAbsolute)),
                Height = 20
            };
            DRBEP_rescan_i.SetValue(Grid.ColumnProperty, 0);
            DRBEP_rescan_i.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_rescan_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Text = "Scenario Scan",
                Foreground = white_button_brush
            };
            DRBEP_rescan_tb.SetValue(Grid.ColumnProperty, 1);
            DRBEP_rescan_tb.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_rescan_sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                //Height = 40,
                Background = Default_back_black_color_brush
            };
            DRBEP_rescan_sp.Children.Add(DRBEP_rescan_gd);
            DRBEP_rescan_gd.Children.Add(DRBEP_rescan_i);
            DRBEP_rescan_gd.Children.Add(DRBEP_rescan_tb);


            DRBEP_rescan_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = DRBEP_rescan_sp,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            ParentGrid.Children.Add(DRBEP_rescan_bt);
            DRBEP_rescan_bt.SetValue(Grid.ColumnProperty, 120);
            DRBEP_rescan_bt.SetValue(Grid.ColumnSpanProperty, 17);
            DRBEP_rescan_bt.SetValue(Grid.RowProperty, 10);
            DRBEP_rescan_bt.SetValue(Grid.RowSpanProperty, 5);
            DRBEP_rescan_bt.Click += DRBEP_rescan_bt_Click;
            #endregion
            #region Global resource
            DRBEP_Global_resource_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush
            };

            DRBEP_Global_resource_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            DRBEP_Global_resource_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


            DRBEP_Global_resource_i = new Image()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Histogram_icon.png", UriKind.RelativeOrAbsolute)),
                Height = 20
            };
            DRBEP_Global_resource_i.SetValue(Grid.ColumnProperty, 0);
            DRBEP_Global_resource_i.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_Global_resource_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Text = "Overall view",
                Foreground = white_button_brush
            };
            DRBEP_Global_resource_tb.SetValue(Grid.ColumnProperty, 1);
            DRBEP_Global_resource_tb.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_Global_resource_sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                //Height = 40,
                Background = Default_back_black_color_brush
            };
            DRBEP_Global_resource_sp.Children.Add(DRBEP_Global_resource_gd);
            DRBEP_Global_resource_gd.Children.Add(DRBEP_Global_resource_i);
            DRBEP_Global_resource_gd.Children.Add(DRBEP_Global_resource_tb);


            DRBEP_Global_resource_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = DRBEP_Global_resource_sp,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            ParentGrid.Children.Add(DRBEP_Global_resource_bt);
            DRBEP_Global_resource_bt.SetValue(Grid.ColumnProperty, 120);
            DRBEP_Global_resource_bt.SetValue(Grid.ColumnSpanProperty, 17);
            DRBEP_Global_resource_bt.SetValue(Grid.RowProperty, 17);
            DRBEP_Global_resource_bt.SetValue(Grid.RowSpanProperty, 5);
            DRBEP_Global_resource_bt.Click += DRBEP_Global_resource_bt_Click;
            #endregion
            #region Computation
            DRBEP_Global_computation_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush
            };

            DRBEP_Global_computation_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            DRBEP_Global_computation_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


            DRBEP_Global_computation_i = new Image()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Histogram_icon.png", UriKind.RelativeOrAbsolute)),
                Height = 20
            };
            DRBEP_Global_computation_i.SetValue(Grid.ColumnProperty, 0);
            DRBEP_Global_computation_i.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_Global_computation_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Text = "Computation Rank",
                Foreground = white_button_brush
            };
            DRBEP_Global_computation_tb.SetValue(Grid.ColumnProperty, 1);
            DRBEP_Global_computation_tb.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_Global_computation_sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                //Height = 40,
                Background = Default_back_black_color_brush
            };
            DRBEP_Global_computation_sp.Children.Add(DRBEP_Global_computation_gd);
            DRBEP_Global_computation_gd.Children.Add(DRBEP_Global_computation_i);
            DRBEP_Global_computation_gd.Children.Add(DRBEP_Global_computation_tb);


            DRBEP_Global_computation_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = DRBEP_Global_computation_sp,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            ParentGrid.Children.Add(DRBEP_Global_computation_bt);
            DRBEP_Global_computation_bt.SetValue(Grid.ColumnProperty, 120);
            DRBEP_Global_computation_bt.SetValue(Grid.ColumnSpanProperty, 17);
            DRBEP_Global_computation_bt.SetValue(Grid.RowProperty, 24);
            DRBEP_Global_computation_bt.SetValue(Grid.RowSpanProperty, 5);
            DRBEP_Global_computation_bt.Click += DRBEP_Global_computation_bt_Click; ;
            #endregion
            #region Memory
            DRBEP_Global_memory_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush
            };

            DRBEP_Global_memory_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            DRBEP_Global_memory_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


            DRBEP_Global_memory_i = new Image()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Histogram_icon.png", UriKind.RelativeOrAbsolute)),
                Height = 20
            };
            DRBEP_Global_memory_i.SetValue(Grid.ColumnProperty, 0);
            DRBEP_Global_memory_i.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_Global_memory_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Text = "Memory Rank",
                Foreground = white_button_brush
            };
            DRBEP_Global_memory_tb.SetValue(Grid.ColumnProperty, 1);
            DRBEP_Global_memory_tb.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_Global_memory_sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                //Height = 40,
                Background = Default_back_black_color_brush
            };
            DRBEP_Global_memory_sp.Children.Add(DRBEP_Global_memory_gd);
            DRBEP_Global_memory_gd.Children.Add(DRBEP_Global_memory_i);
            DRBEP_Global_memory_gd.Children.Add(DRBEP_Global_memory_tb);


            DRBEP_Global_memory_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = DRBEP_Global_memory_sp,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            ParentGrid.Children.Add(DRBEP_Global_memory_bt);
            DRBEP_Global_memory_bt.SetValue(Grid.ColumnProperty, 120);
            DRBEP_Global_memory_bt.SetValue(Grid.ColumnSpanProperty, 17);
            DRBEP_Global_memory_bt.SetValue(Grid.RowProperty, 31);
            DRBEP_Global_memory_bt.SetValue(Grid.RowSpanProperty, 5);
            DRBEP_Global_memory_bt.Click += DRBEP_Global_memory_bt_Click; ;
            #endregion
            #region Bandwidth
            DRBEP_Global_bandwidth_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush
            };

            DRBEP_Global_bandwidth_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            DRBEP_Global_bandwidth_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


            DRBEP_Global_bandwidth_i = new Image()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Histogram_icon.png", UriKind.RelativeOrAbsolute)),
                Height = 20
            };
            DRBEP_Global_bandwidth_i.SetValue(Grid.ColumnProperty, 0);
            DRBEP_Global_bandwidth_i.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_Global_bandwidth_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Text = "Bandwidth Rank",
                Foreground = white_button_brush
            };
            DRBEP_Global_bandwidth_tb.SetValue(Grid.ColumnProperty, 1);
            DRBEP_Global_bandwidth_tb.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_Global_bandwidth_sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                //Height = 40,
                Background = Default_back_black_color_brush
            };
            DRBEP_Global_bandwidth_sp.Children.Add(DRBEP_Global_bandwidth_gd);
            DRBEP_Global_bandwidth_gd.Children.Add(DRBEP_Global_bandwidth_i);
            DRBEP_Global_bandwidth_gd.Children.Add(DRBEP_Global_bandwidth_tb);


            DRBEP_Global_bandwidth_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = DRBEP_Global_bandwidth_sp,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            ParentGrid.Children.Add(DRBEP_Global_bandwidth_bt);
            DRBEP_Global_bandwidth_bt.SetValue(Grid.ColumnProperty, 120);
            DRBEP_Global_bandwidth_bt.SetValue(Grid.ColumnSpanProperty, 17);
            DRBEP_Global_bandwidth_bt.SetValue(Grid.RowProperty, 38);
            DRBEP_Global_bandwidth_bt.SetValue(Grid.RowSpanProperty, 5);
            DRBEP_Global_bandwidth_bt.Click += DRBEP_Global_bandwidth_bt_Click; ;
            #endregion
            #region Latency
            DRBEP_Global_latency_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush
            };

            DRBEP_Global_latency_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            DRBEP_Global_latency_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


            DRBEP_Global_latency_i = new Image()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Histogram_icon.png", UriKind.RelativeOrAbsolute)),
                Height = 20
            };
            DRBEP_Global_latency_i.SetValue(Grid.ColumnProperty, 0);
            DRBEP_Global_latency_i.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_Global_latency_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Text = "Latency Rank",
                Foreground = white_button_brush
            };
            DRBEP_Global_latency_tb.SetValue(Grid.ColumnProperty, 1);
            DRBEP_Global_latency_tb.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_Global_latency_sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                //Height = 40,
                Background = Default_back_black_color_brush
            };
            DRBEP_Global_latency_sp.Children.Add(DRBEP_Global_latency_gd);
            DRBEP_Global_latency_gd.Children.Add(DRBEP_Global_latency_i);
            DRBEP_Global_latency_gd.Children.Add(DRBEP_Global_latency_tb);


            DRBEP_Global_latency_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = DRBEP_Global_latency_sp,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            ParentGrid.Children.Add(DRBEP_Global_latency_bt);
            DRBEP_Global_latency_bt.SetValue(Grid.ColumnProperty, 120);
            DRBEP_Global_latency_bt.SetValue(Grid.ColumnSpanProperty, 17);
            DRBEP_Global_latency_bt.SetValue(Grid.RowProperty, 45);
            DRBEP_Global_latency_bt.SetValue(Grid.RowSpanProperty, 5);
            DRBEP_Global_latency_bt.Click += DRBEP_Global_latency_bt_Click; ;
            #endregion


            #region OP Rescan
            DRBEOP_rescan_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush
            };

            DRBEOP_rescan_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            DRBEOP_rescan_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


            DRBEOP_rescan_i = new Image()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Refresh.png", UriKind.RelativeOrAbsolute)),
                Height = 20
            };
            DRBEOP_rescan_i.SetValue(Grid.ColumnProperty, 0);
            DRBEOP_rescan_i.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEOP_rescan_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Text = "Object scan",
                Foreground = white_button_brush
            };
            DRBEOP_rescan_tb.SetValue(Grid.ColumnProperty, 1);
            DRBEOP_rescan_tb.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEOP_rescan_sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                //Height = 40,
                Background = Default_back_black_color_brush
            };
            DRBEOP_rescan_sp.Children.Add(DRBEOP_rescan_gd);
            DRBEOP_rescan_gd.Children.Add(DRBEOP_rescan_i);
            DRBEOP_rescan_gd.Children.Add(DRBEOP_rescan_tb);


            DRBEOP_rescan_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = DRBEOP_rescan_sp,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            ParentGrid.Children.Add(DRBEOP_rescan_bt);
            DRBEOP_rescan_bt.SetValue(Grid.ColumnProperty, 120);
            DRBEOP_rescan_bt.SetValue(Grid.ColumnSpanProperty, 17);
            DRBEOP_rescan_bt.SetValue(Grid.RowProperty, 60);
            DRBEOP_rescan_bt.SetValue(Grid.RowSpanProperty, 5);
            DRBEOP_rescan_bt.Click += DRBEOP_rescan_bt_Click;
            #endregion

            #region Fidelity Computation
            DRBEP_obj_fidelity_c_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush
            };

            DRBEP_obj_fidelity_c_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            DRBEP_obj_fidelity_c_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


            DRBEP_obj_fidelity_c_i = new Image()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Histogram_icon.png", UriKind.RelativeOrAbsolute)),
                Height = 20
            };
            DRBEP_obj_fidelity_c_i.SetValue(Grid.ColumnProperty, 0);
            DRBEP_obj_fidelity_c_i.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_obj_fidelity_c_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Text = "Fidelity Computation",
                Foreground = white_button_brush
            };
            DRBEP_obj_fidelity_c_tb.SetValue(Grid.ColumnProperty, 1);
            DRBEP_obj_fidelity_c_tb.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_obj_fidelity_c_sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                //Height = 40,
                Background = Default_back_black_color_brush
            };
            DRBEP_obj_fidelity_c_sp.Children.Add(DRBEP_obj_fidelity_c_gd);
            DRBEP_obj_fidelity_c_gd.Children.Add(DRBEP_obj_fidelity_c_i);
            DRBEP_obj_fidelity_c_gd.Children.Add(DRBEP_obj_fidelity_c_tb);


            DRBEP_obj_fidelity_c_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = DRBEP_obj_fidelity_c_sp,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            ParentGrid.Children.Add(DRBEP_obj_fidelity_c_bt);
            DRBEP_obj_fidelity_c_bt.SetValue(Grid.ColumnProperty, 120);
            DRBEP_obj_fidelity_c_bt.SetValue(Grid.ColumnSpanProperty, 17);
            DRBEP_obj_fidelity_c_bt.SetValue(Grid.RowProperty, 67);
            DRBEP_obj_fidelity_c_bt.SetValue(Grid.RowSpanProperty, 5);
            DRBEP_obj_fidelity_c_bt.Click += DRBEP_obj_fidelity_c_bt_Click;
            #endregion

            #region Fidelity Memory
            DRBEP_obj_fidelity_m_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush
            };

            DRBEP_obj_fidelity_m_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            DRBEP_obj_fidelity_m_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


            DRBEP_obj_fidelity_m_i = new Image()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Histogram_icon.png", UriKind.RelativeOrAbsolute)),
                Height = 20
            };
            DRBEP_obj_fidelity_m_i.SetValue(Grid.ColumnProperty, 0);
            DRBEP_obj_fidelity_m_i.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_obj_fidelity_m_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Text = "Fidelity Memory",
                Foreground = white_button_brush
            };
            DRBEP_obj_fidelity_m_tb.SetValue(Grid.ColumnProperty, 1);
            DRBEP_obj_fidelity_m_tb.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_obj_fidelity_m_sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                //Height = 40,
                Background = Default_back_black_color_brush
            };
            DRBEP_obj_fidelity_m_sp.Children.Add(DRBEP_obj_fidelity_m_gd);
            DRBEP_obj_fidelity_m_gd.Children.Add(DRBEP_obj_fidelity_m_i);
            DRBEP_obj_fidelity_m_gd.Children.Add(DRBEP_obj_fidelity_m_tb);


            DRBEP_obj_fidelity_m_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = DRBEP_obj_fidelity_m_sp,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            ParentGrid.Children.Add(DRBEP_obj_fidelity_m_bt);
            DRBEP_obj_fidelity_m_bt.SetValue(Grid.ColumnProperty, 120);
            DRBEP_obj_fidelity_m_bt.SetValue(Grid.ColumnSpanProperty, 17);
            DRBEP_obj_fidelity_m_bt.SetValue(Grid.RowProperty, 74);
            DRBEP_obj_fidelity_m_bt.SetValue(Grid.RowSpanProperty, 5);
            DRBEP_obj_fidelity_m_bt.Click += DRBEP_obj_fidelity_m_bt_Click;
            #endregion

            #region Fidelity Bandwidth
            DRBEP_obj_fidelity_b_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush
            };

            DRBEP_obj_fidelity_b_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            DRBEP_obj_fidelity_b_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


            DRBEP_obj_fidelity_b_i = new Image()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Histogram_icon.png", UriKind.RelativeOrAbsolute)),
                Height = 20
            };
            DRBEP_obj_fidelity_b_i.SetValue(Grid.ColumnProperty, 0);
            DRBEP_obj_fidelity_b_i.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_obj_fidelity_b_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Text = "Fidelity Bandwidth",
                Foreground = white_button_brush
            };
            DRBEP_obj_fidelity_b_tb.SetValue(Grid.ColumnProperty, 1);
            DRBEP_obj_fidelity_b_tb.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_obj_fidelity_b_sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                //Height = 40,
                Background = Default_back_black_color_brush
            };
            DRBEP_obj_fidelity_b_sp.Children.Add(DRBEP_obj_fidelity_b_gd);
            DRBEP_obj_fidelity_b_gd.Children.Add(DRBEP_obj_fidelity_b_i);
            DRBEP_obj_fidelity_b_gd.Children.Add(DRBEP_obj_fidelity_b_tb);


            DRBEP_obj_fidelity_b_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = DRBEP_obj_fidelity_b_sp,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            ParentGrid.Children.Add(DRBEP_obj_fidelity_b_bt);
            DRBEP_obj_fidelity_b_bt.SetValue(Grid.ColumnProperty, 120);
            DRBEP_obj_fidelity_b_bt.SetValue(Grid.ColumnSpanProperty, 17);
            DRBEP_obj_fidelity_b_bt.SetValue(Grid.RowProperty, 81);
            DRBEP_obj_fidelity_b_bt.SetValue(Grid.RowSpanProperty, 5);
            DRBEP_obj_fidelity_b_bt.Click += DRBEP_obj_fidelity_b_bt_Click;
            #endregion

            #region Fidelity Latency
            DRBEP_obj_fidelity_l_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush
            };

            DRBEP_obj_fidelity_l_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            DRBEP_obj_fidelity_l_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


            DRBEP_obj_fidelity_l_i = new Image()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Histogram_icon.png", UriKind.RelativeOrAbsolute)),
                Height = 20
            };
            DRBEP_obj_fidelity_l_i.SetValue(Grid.ColumnProperty, 0);
            DRBEP_obj_fidelity_l_i.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_obj_fidelity_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Text = "Fidelity Latency",
                Foreground = white_button_brush
            };
            DRBEP_obj_fidelity_l_tb.SetValue(Grid.ColumnProperty, 1);
            DRBEP_obj_fidelity_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_obj_fidelity_l_sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                //Height = 40,
                Background = Default_back_black_color_brush
            };
            DRBEP_obj_fidelity_l_sp.Children.Add(DRBEP_obj_fidelity_l_gd);
            DRBEP_obj_fidelity_l_gd.Children.Add(DRBEP_obj_fidelity_l_i);
            DRBEP_obj_fidelity_l_gd.Children.Add(DRBEP_obj_fidelity_l_tb);


            DRBEP_obj_fidelity_l_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = DRBEP_obj_fidelity_l_sp,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            ParentGrid.Children.Add(DRBEP_obj_fidelity_l_bt);
            DRBEP_obj_fidelity_l_bt.SetValue(Grid.ColumnProperty, 120);
            DRBEP_obj_fidelity_l_bt.SetValue(Grid.ColumnSpanProperty, 17);
            DRBEP_obj_fidelity_l_bt.SetValue(Grid.RowProperty, 88);
            DRBEP_obj_fidelity_l_bt.SetValue(Grid.RowSpanProperty, 5);
            DRBEP_obj_fidelity_l_bt.Click += DRBEP_obj_fidelity_l_bt_Click;
            #endregion

            #region Property Computation
            DRBEP_obj_property_c_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush
            };

            DRBEP_obj_property_c_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            DRBEP_obj_property_c_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


            DRBEP_obj_property_c_i = new Image()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Histogram_icon.png", UriKind.RelativeOrAbsolute)),
                Height = 20
            };
            DRBEP_obj_property_c_i.SetValue(Grid.ColumnProperty, 0);
            DRBEP_obj_property_c_i.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_obj_property_c_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Text = "Property Computation",
                Foreground = white_button_brush
            };
            DRBEP_obj_property_c_tb.SetValue(Grid.ColumnProperty, 1);
            DRBEP_obj_property_c_tb.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_obj_property_c_sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                //Height = 40,
                Background = Default_back_black_color_brush
            };
            DRBEP_obj_property_c_sp.Children.Add(DRBEP_obj_property_c_gd);
            DRBEP_obj_property_c_gd.Children.Add(DRBEP_obj_property_c_i);
            DRBEP_obj_property_c_gd.Children.Add(DRBEP_obj_property_c_tb);


            DRBEP_obj_property_c_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = DRBEP_obj_property_c_sp,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            ParentGrid.Children.Add(DRBEP_obj_property_c_bt);
            DRBEP_obj_property_c_bt.SetValue(Grid.ColumnProperty, 120);
            DRBEP_obj_property_c_bt.SetValue(Grid.ColumnSpanProperty, 17);
            DRBEP_obj_property_c_bt.SetValue(Grid.RowProperty, 100);
            DRBEP_obj_property_c_bt.SetValue(Grid.RowSpanProperty, 5);
            DRBEP_obj_property_c_bt.Click += DRBEP_obj_property_c_bt_Click;
            #endregion

            #region Property Memory
            DRBEP_obj_property_m_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush
            };

            DRBEP_obj_property_m_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            DRBEP_obj_property_m_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


            DRBEP_obj_property_m_i = new Image()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Histogram_icon.png", UriKind.RelativeOrAbsolute)),
                Height = 20
            };
            DRBEP_obj_property_m_i.SetValue(Grid.ColumnProperty, 0);
            DRBEP_obj_property_m_i.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_obj_property_m_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Text = "Property Memory",
                Foreground = white_button_brush
            };
            DRBEP_obj_property_m_tb.SetValue(Grid.ColumnProperty, 1);
            DRBEP_obj_property_m_tb.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_obj_property_m_sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                //Height = 40,
                Background = Default_back_black_color_brush
            };
            DRBEP_obj_property_m_sp.Children.Add(DRBEP_obj_property_m_gd);
            DRBEP_obj_property_m_gd.Children.Add(DRBEP_obj_property_m_i);
            DRBEP_obj_property_m_gd.Children.Add(DRBEP_obj_property_m_tb);


            DRBEP_obj_property_m_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = DRBEP_obj_property_m_sp,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            ParentGrid.Children.Add(DRBEP_obj_property_m_bt);
            DRBEP_obj_property_m_bt.SetValue(Grid.ColumnProperty, 120);
            DRBEP_obj_property_m_bt.SetValue(Grid.ColumnSpanProperty, 17);
            DRBEP_obj_property_m_bt.SetValue(Grid.RowProperty, 107);
            DRBEP_obj_property_m_bt.SetValue(Grid.RowSpanProperty, 5);
            DRBEP_obj_property_m_bt.Click += DRBEP_obj_property_m_bt_Click;
            #endregion

            #region Property Bandwidth
            DRBEP_obj_property_b_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush
            };

            DRBEP_obj_property_b_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            DRBEP_obj_property_b_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


            DRBEP_obj_property_b_i = new Image()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Histogram_icon.png", UriKind.RelativeOrAbsolute)),
                Height = 20
            };
            DRBEP_obj_property_b_i.SetValue(Grid.ColumnProperty, 0);
            DRBEP_obj_property_b_i.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_obj_property_b_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Text = "Property Bandwidth",
                Foreground = white_button_brush
            };
            DRBEP_obj_property_b_tb.SetValue(Grid.ColumnProperty, 1);
            DRBEP_obj_property_b_tb.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_obj_property_b_sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                //Height = 40,
                Background = Default_back_black_color_brush
            };
            DRBEP_obj_property_b_sp.Children.Add(DRBEP_obj_property_b_gd);
            DRBEP_obj_property_b_gd.Children.Add(DRBEP_obj_property_b_i);
            DRBEP_obj_property_b_gd.Children.Add(DRBEP_obj_property_b_tb);


            DRBEP_obj_property_b_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = DRBEP_obj_property_b_sp,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            ParentGrid.Children.Add(DRBEP_obj_property_b_bt);
            DRBEP_obj_property_b_bt.SetValue(Grid.ColumnProperty, 120);
            DRBEP_obj_property_b_bt.SetValue(Grid.ColumnSpanProperty, 17);
            DRBEP_obj_property_b_bt.SetValue(Grid.RowProperty, 114);
            DRBEP_obj_property_b_bt.SetValue(Grid.RowSpanProperty, 5);
            DRBEP_obj_property_b_bt.Click += DRBEP_obj_property_b_bt_Click;
            #endregion

            #region Property Latency
            DRBEP_obj_property_l_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush
            };

            DRBEP_obj_property_l_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            DRBEP_obj_property_l_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


            DRBEP_obj_property_l_i = new Image()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Histogram_icon.png", UriKind.RelativeOrAbsolute)),
                Height = 20
            };
            DRBEP_obj_property_l_i.SetValue(Grid.ColumnProperty, 0);
            DRBEP_obj_property_l_i.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_obj_property_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 12,
                Text = "Property Latency",
                Foreground = white_button_brush
            };
            DRBEP_obj_property_l_tb.SetValue(Grid.ColumnProperty, 1);
            DRBEP_obj_property_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            DRBEP_obj_property_l_sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                //Height = 40,
                Background = Default_back_black_color_brush
            };
            DRBEP_obj_property_l_sp.Children.Add(DRBEP_obj_property_l_gd);
            DRBEP_obj_property_l_gd.Children.Add(DRBEP_obj_property_l_i);
            DRBEP_obj_property_l_gd.Children.Add(DRBEP_obj_property_l_tb);


            DRBEP_obj_property_l_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = DRBEP_obj_property_l_sp,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(1, 1, 1, 1)
            };
            ParentGrid.Children.Add(DRBEP_obj_property_l_bt);
            DRBEP_obj_property_l_bt.SetValue(Grid.ColumnProperty, 120);
            DRBEP_obj_property_l_bt.SetValue(Grid.ColumnSpanProperty, 17);
            DRBEP_obj_property_l_bt.SetValue(Grid.RowProperty, 121);
            DRBEP_obj_property_l_bt.SetValue(Grid.RowSpanProperty, 5);
            DRBEP_obj_property_l_bt.Click += DRBEP_obj_property_l_bt_Click;
            #endregion
        }
        private void Presentation_choice_hide()
        {
            #region Rescan
            DRBEP_rescan_bt.Visibility = Visibility.Collapsed;
            #endregion
            #region Global resource
            DRBEP_Global_resource_bt.Visibility = Visibility.Collapsed;
            #endregion
            #region Computation
            DRBEP_Global_computation_bt.Visibility = Visibility.Collapsed;
            #endregion
            #region Memory
            DRBEP_Global_memory_bt.Visibility = Visibility.Collapsed;
            #endregion
            #region Bandwidth
            DRBEP_Global_bandwidth_bt.Visibility = Visibility.Collapsed;
            #endregion
            #region Latency
            DRBEP_Global_latency_bt.Visibility = Visibility.Collapsed;
            #endregion


            #region OP Rescan
            DRBEOP_rescan_bt.Visibility = Visibility.Collapsed;
            #endregion

            #region Fidelity Computation
            DRBEP_obj_fidelity_c_bt.Visibility = Visibility.Collapsed;
            #endregion

            #region Fidelity Memory
            DRBEP_obj_fidelity_m_bt.Visibility = Visibility.Collapsed;
            #endregion

            #region Fidelity Bandwidth
            DRBEP_obj_fidelity_b_bt.Visibility = Visibility.Collapsed;
            #endregion

            #region Fidelity Latency
            DRBEP_obj_fidelity_l_bt.Visibility = Visibility.Collapsed;
            #endregion

            #region Property Computation
            DRBEP_obj_property_c_bt.Visibility = Visibility.Collapsed;
            #endregion

            #region Property Memory
            DRBEP_obj_property_m_bt.Visibility = Visibility.Collapsed;
            #endregion

            #region Property Bandwidth
            DRBEP_obj_property_b_bt.Visibility = Visibility.Collapsed;
            #endregion

            #region Property Latency
            DRBEP_obj_property_l_bt.Visibility = Visibility.Collapsed;
            #endregion
        }
        private void Presentation_choice_show()
        {
            #region Rescan
            DRBEP_rescan_bt.Visibility = Visibility.Visible;
            #endregion
            #region Global resource
            DRBEP_Global_resource_bt.Visibility = Visibility.Visible;
            #endregion
            #region Computation
            DRBEP_Global_computation_bt.Visibility = Visibility.Visible;
            #endregion
            #region Memory
            DRBEP_Global_memory_bt.Visibility = Visibility.Visible;
            #endregion
            #region Bandwidth
            DRBEP_Global_bandwidth_bt.Visibility = Visibility.Visible;
            #endregion
            #region Latency
            DRBEP_Global_latency_bt.Visibility = Visibility.Visible;
            #endregion


            #region OP Rescan
            DRBEOP_rescan_bt.Visibility = Visibility.Visible;
            #endregion

            #region Fidelity Computation
            DRBEP_obj_fidelity_c_bt.Visibility = Visibility.Visible;
            #endregion

            #region Fidelity Memory
            DRBEP_obj_fidelity_m_bt.Visibility = Visibility.Visible;
            #endregion

            #region Fidelity Bandwidth
            DRBEP_obj_fidelity_b_bt.Visibility = Visibility.Visible;
            #endregion

            #region Fidelity Latency
            DRBEP_obj_fidelity_l_bt.Visibility = Visibility.Visible;
            #endregion

            #region Property Computation
            DRBEP_obj_property_c_bt.Visibility = Visibility.Visible;
            #endregion

            #region Property Memory
            DRBEP_obj_property_m_bt.Visibility = Visibility.Visible;
            #endregion

            #region Property Bandwidth
            DRBEP_obj_property_b_bt.Visibility = Visibility.Visible;
            #endregion

            #region Property Latency
            DRBEP_obj_property_l_bt.Visibility = Visibility.Visible;
            #endregion
        }
        private void Presentation_choice_decolor()
        {
            DRBEP_Global_computation_bt.BorderBrush = white_button_brush;
            DRBEP_Global_memory_bt.BorderBrush = white_button_brush;
            DRBEP_Global_bandwidth_bt.BorderBrush = white_button_brush;
            DRBEP_Global_latency_bt.BorderBrush = white_button_brush;
            DRBEP_Global_resource_bt.BorderBrush = white_button_brush;

            DRBEP_obj_property_c_bt.BorderBrush = white_button_brush;
            DRBEP_obj_property_m_bt.BorderBrush = white_button_brush;
            DRBEP_obj_property_b_bt.BorderBrush = white_button_brush;
            DRBEP_obj_property_l_bt.BorderBrush = white_button_brush;

            DRBEP_obj_fidelity_c_bt.BorderBrush = white_button_brush;
            DRBEP_obj_fidelity_m_bt.BorderBrush = white_button_brush;
            DRBEP_obj_fidelity_b_bt.BorderBrush = white_button_brush;
            DRBEP_obj_fidelity_l_bt.BorderBrush = white_button_brush;
        }
        private async void DRBEP_obj_fidelity_c_bt_Click(object sender, RoutedEventArgs e)
        {
            Presentation_choice_decolor();
            Button foo = sender as Button;
            foo.BorderBrush = green_bright_button_brush;


            Presentation_choice_mode = 5;
            Presentation_hide();
            Presentation_fidelity_show();
            Last_result_update_show();
            await Scan_obj_presentation_update(Dic_SObt_i[Temp_singleo_bt]);
        }
        private async void DRBEP_obj_fidelity_m_bt_Click(object sender, RoutedEventArgs e)
        {
            Presentation_choice_decolor();
            Button foo = sender as Button;
            foo.BorderBrush = green_bright_button_brush;
            Presentation_choice_mode = 6;
            Presentation_hide();
            Presentation_fidelity_show();
            Last_result_update_show();
            await Scan_obj_presentation_update(Dic_SObt_i[Temp_singleo_bt]);
        }
        private async void DRBEP_obj_fidelity_b_bt_Click(object sender, RoutedEventArgs e)
        {
            Presentation_choice_decolor();
            Button foo = sender as Button;
            foo.BorderBrush = green_bright_button_brush;
            Presentation_choice_mode = 7;
            Presentation_hide();
            Presentation_fidelity_show();
            Last_result_update_show();
            await Scan_obj_presentation_update(Dic_SObt_i[Temp_singleo_bt]);
        }
        private async void DRBEP_obj_fidelity_l_bt_Click(object sender, RoutedEventArgs e)
        {
            Presentation_choice_decolor();
            Button foo = sender as Button;
            foo.BorderBrush = green_bright_button_brush;
            Presentation_choice_mode = 8;
            Presentation_hide();
            Presentation_fidelity_show();
            Last_result_update_show();
            await Scan_obj_presentation_update(Dic_SObt_i[Temp_singleo_bt]);
        }
        private async void DRBEP_obj_property_c_bt_Click(object sender, RoutedEventArgs e)
        {
            Presentation_choice_decolor();
            Button foo = sender as Button;
            foo.BorderBrush = green_bright_button_brush;
            Presentation_choice_mode = 9;
            Presentation_hide();
            Presentation_property_show();
            Last_result_update_show();
            await Scan_obj_presentation_update(Dic_SObt_i[Temp_singleo_bt]);
        }
        private async void DRBEP_obj_property_m_bt_Click(object sender, RoutedEventArgs e)
        {
            Presentation_choice_decolor();
            Button foo = sender as Button;
            foo.BorderBrush = green_bright_button_brush;
            Presentation_choice_mode = 10;
            Presentation_hide();
            Presentation_property_show();
            Last_result_update_show();
            await Scan_obj_presentation_update(Dic_SObt_i[Temp_singleo_bt]);
        }
        private async void DRBEP_obj_property_b_bt_Click(object sender, RoutedEventArgs e)
        {
            Presentation_choice_decolor();
            Button foo = sender as Button;
            foo.BorderBrush = green_bright_button_brush;
            Presentation_choice_mode = 11;
            Presentation_hide();
            Presentation_property_show();
            Last_result_update_show();
            await Scan_obj_presentation_update(Dic_SObt_i[Temp_singleo_bt]);
        }
        private async void DRBEP_obj_property_l_bt_Click(object sender, RoutedEventArgs e)
        {
            Presentation_choice_decolor();
            Button foo = sender as Button;
            foo.BorderBrush = green_bright_button_brush;
            Presentation_choice_mode = 12;
            Presentation_hide();
            Presentation_property_show();
            Last_result_update_show();
            await Scan_obj_presentation_update(Dic_SObt_i[Temp_singleo_bt]);
        }
        private void DRBEP_Global_computation_bt_Click(object sender, RoutedEventArgs e)
        {
            Presentation_choice_decolor();
            Button foo = sender as Button;
            foo.BorderBrush = green_bright_button_brush;
            Presentation_choice_mode = 1;
            Presentation_hide();
            Presentation_Global_obj_show();
            Last_result_update_show();
        }
        private void DRBEP_Global_memory_bt_Click(object sender, RoutedEventArgs e)
        {
            Presentation_choice_decolor();
            Button foo = sender as Button;
            foo.BorderBrush = green_bright_button_brush;
            Presentation_choice_mode = 2;
            Presentation_hide();
            Presentation_Global_obj_show();
            Last_result_update_show();
        }
        private void DRBEP_Global_bandwidth_bt_Click(object sender, RoutedEventArgs e)
        {
            Presentation_choice_decolor();
            Button foo = sender as Button;
            foo.BorderBrush = green_bright_button_brush;
            Presentation_choice_mode = 3;
            Presentation_hide();
            Presentation_Global_obj_show();
            Last_result_update_show();
        }
        private void DRBEP_Global_latency_bt_Click(object sender, RoutedEventArgs e)
        {
            Presentation_choice_decolor();
            Button foo = sender as Button;
            foo.BorderBrush = green_bright_button_brush;
            Presentation_choice_mode = 4;
            Presentation_hide();
            Presentation_Global_obj_show();
            Last_result_update_show();
        }
        private void DRBEP_Global_resource_bt_Click(object sender, RoutedEventArgs e)
        {
            Presentation_choice_decolor();
            Button foo = sender as Button;
            foo.BorderBrush = green_bright_button_brush;
            Presentation_choice_mode = 0;
            Presentation_hide();
            Presentation_Global_resource_show();
            Last_result_update_show();

        }
        private List<List<double>> DRBE_obj_scan_latency_list = new List<List<double>>();
        private async void DRBEP_rescan_bt_Click(object sender, RoutedEventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Get_class_result_refresh();
            int i = 0;
            int ii = 0;
            int iii = 0;
            int count = 0;
            i = 0;

            Global_scanned_flag = true;
            Scan_max = Get_total_scan_number(0);
            Scan_pb.Maximum = Scan_max;

            while (i < Link_list.Count)
            {
                ii = 0;
                while (ii < Link_list[i].Count)
                {
                    iii = 0;
                    while (iii < Link_list[i][ii].Count)
                    {
                        if (Link_list[i][ii][iii])
                        {
                            //Get_link_class_result_update(await Transceive(Fetch_link_info(i, ii, iii)), i, ii, iii);
                            Get_link_class_tr_result_update(await Transceive(Fetch_link_info(iii, ii, i)), iii, ii, i);
                            Get_link_class_tr_result_update(await Transceive(Fetch_link_info(i, ii, iii)), i, ii, iii);
                            Get_link_class_o_result_update(await Transceive(Fetch_link_info(ii, iii, i)), iii, ii, i);

                            Dic_t_i_obj[i].Is_transmitting = true;
                            Dic_o_i_obj[ii].Is_reflecting = true;
                            Dic_r_i_obj[iii].Is_receiving = true;
                            count++;
                            if (count % 10 == 0)
                            {
                                Get_class_result_show(count);
                            }
                        }
                        iii++;
                    }
                    ii++;
                }

                i++;
            }


            

            i = 0;
            while (i < DRBE_obj_list.Count)
            {
                if (DRBE_obj_list[i].Is_transmitting)
                {
                    Get_obj_class_result_update(await Transceive(Fetch_obj_info(DRBE_obj_list[i])), i, 0);
                }
                if (DRBE_obj_list[i].Is_reflecting)
                {
                    Get_obj_class_result_update(await Transceive(Fetch_obj_info(DRBE_obj_list[i])), i, 1);
                }
                if (DRBE_obj_list[i].Is_receiving)
                {
                    Get_obj_class_result_update(await Transceive(Fetch_obj_info(DRBE_obj_list[i])), i, 2);
                }
                count++;
                if (count % 3 == 0)
                {
                    Get_class_result_show(count);
                }
                i++;
            }
            i = 0;
            while (i < DRBE_obj_list.Count)
            {
                //COR_Compute += DRBE_obj_list[i].COR_Compute;
                //COR_Memory += DRBE_obj_list[i].COR_Memory;
                i++;
            }
            Get_class_result_show(count);
            Last_result_update_show();
            watch.Stop();
            ParentPage.MainPageTestTb.Text += "Overview Scane: " + watch.ElapsedMilliseconds.ToString() + "\r\n";


        }

        private async Task Scan_obj_presentation_update(int ind)
        {
            double maxa = 0;
            double maxr = 0;

            double total_m = DRBE_obj_list[ind].ANT_Memory + DRBE_obj_list[ind].RCS_Memory + DRBE_obj_list[ind].COR_Memory + DRBE_obj_list[ind].ORI_Memory + DRBE_obj_list[ind].NRE_Memory + DRBE_obj_list[ind].PG_Memory + DRBE_obj_list[ind].TU_Memory;
            double total_c = DRBE_obj_list[ind].ANT_Compute + DRBE_obj_list[ind].RCS_Compute + DRBE_obj_list[ind].COR_Compute + DRBE_obj_list[ind].ORI_Compute + DRBE_obj_list[ind].NRE_Compute + DRBE_obj_list[ind].PG_Compute + DRBE_obj_list[ind].TU_Compute;
            double total_b = DRBE_obj_list[ind].ANT_Bandwidth + DRBE_obj_list[ind].RCS_Bandwidth + DRBE_obj_list[ind].COR_Bandwidth + DRBE_obj_list[ind].ORI_Bandwidth + DRBE_obj_list[ind].NRE_Bandwidth + DRBE_obj_list[ind].PG_Bandwidth + DRBE_obj_list[ind].TU_Bandwidth;
            double total_l = DRBE_obj_list[ind].ANT_Latency + DRBE_obj_list[ind].RCS_Latency + DRBE_obj_list[ind].COR_Latency + DRBE_obj_list[ind].ORI_Latency + DRBE_obj_list[ind].NRE_Latency + DRBE_obj_list[ind].PG_Latency + DRBE_obj_list[ind].TU_Latency;



            //if(Scan_cur_obj%3!=0)
            //{
            //    return;
            //}
            Scan_pb.Value = Scan_cur_obj;
            Scan_i_tb.Text = Scan_cur_obj.ToString() + "/" + Scan_max_obj.ToString();
            switch (Presentation_choice_mode)
            {
                case 5:
                    FANT0_pb.Value = DRBE_obj_list[ind].cf_ant0;
                    FANT1_pb.Value = DRBE_obj_list[ind].cf_ant1;
                    FANT2_pb.Value = DRBE_obj_list[ind].cf_ant2;
                    FANT3_pb.Value = DRBE_obj_list[ind].cf_ant3;
                    FANT4_pb.Value = DRBE_obj_list[ind].cf_ant4;
                    FANT5_pb.Value = DRBE_obj_list[ind].cf_ant5;

                    FRCS0_pb.Value = DRBE_obj_list[ind].cf_rcs0;
                    FRCS1_pb.Value = DRBE_obj_list[ind].cf_rcs1;
                    FRCS2_pb.Value = DRBE_obj_list[ind].cf_rcs2;
                    FRCS3_pb.Value = DRBE_obj_list[ind].cf_rcs3;
                    FRCS4_pb.Value = DRBE_obj_list[ind].cf_rcs4;
                    FRCS5_pb.Value = DRBE_obj_list[ind].cf_rcs5;
                    FRCS6_pb.Value = DRBE_obj_list[ind].cf_rcs6;

                    maxa = DRBE_obj_list[ind].cf_ant0 + DRBE_obj_list[ind].cf_ant1 + DRBE_obj_list[ind].cf_ant2 + DRBE_obj_list[ind].cf_ant3 + DRBE_obj_list[ind].cf_ant4 + DRBE_obj_list[ind].cf_ant5;
                    maxr = DRBE_obj_list[ind].cf_rcs0 + DRBE_obj_list[ind].cf_rcs1 + DRBE_obj_list[ind].cf_rcs2 + DRBE_obj_list[ind].cf_rcs3 + DRBE_obj_list[ind].cf_rcs4 + DRBE_obj_list[ind].cf_rcs5 + DRBE_obj_list[ind].cf_rcs6;

                    FANT0_pb.Maximum = maxa;
                    FANT1_pb.Maximum = maxa;
                    FANT2_pb.Maximum = maxa;
                    FANT3_pb.Maximum = maxa;
                    FANT4_pb.Maximum = maxa;
                    FANT5_pb.Maximum = maxa;

                    FRCS0_pb.Maximum = maxr;
                    FRCS1_pb.Maximum = maxr;
                    FRCS2_pb.Maximum = maxr;
                    FRCS3_pb.Maximum = maxr;
                    FRCS4_pb.Maximum = maxr;
                    FRCS5_pb.Maximum = maxr;
                    FRCS6_pb.Maximum = maxr;

                    FANT0_i_tb.Text = DRBE_obj_list[ind].cf_ant0.ToString("E5");
                    FANT1_i_tb.Text = DRBE_obj_list[ind].cf_ant1.ToString("E5");
                    FANT2_i_tb.Text = DRBE_obj_list[ind].cf_ant2.ToString("E5");
                    FANT3_i_tb.Text = DRBE_obj_list[ind].cf_ant3.ToString("E5");
                    FANT4_i_tb.Text = DRBE_obj_list[ind].cf_ant4.ToString("E5");
                    FANT5_i_tb.Text = DRBE_obj_list[ind].cf_ant5.ToString("E5");

                    FRCS0_i_tb.Text = DRBE_obj_list[ind].cf_rcs0.ToString("E5");
                    FRCS1_i_tb.Text = DRBE_obj_list[ind].cf_rcs1.ToString("E5");
                    FRCS2_i_tb.Text = DRBE_obj_list[ind].cf_rcs2.ToString("E5");
                    FRCS3_i_tb.Text = DRBE_obj_list[ind].cf_rcs3.ToString("E5");
                    FRCS4_i_tb.Text = DRBE_obj_list[ind].cf_rcs4.ToString("E5");
                    FRCS5_i_tb.Text = DRBE_obj_list[ind].cf_rcs5.ToString("E5");
                    FRCS6_i_tb.Text = DRBE_obj_list[ind].cf_rcs6.ToString("E5");
                    break;

                case 6:

                    FANT0_pb.Value = DRBE_obj_list[ind].mf_ant0;
                    FANT1_pb.Value = DRBE_obj_list[ind].mf_ant1;
                    FANT2_pb.Value = DRBE_obj_list[ind].mf_ant2;
                    FANT3_pb.Value = DRBE_obj_list[ind].mf_ant3;
                    FANT4_pb.Value = DRBE_obj_list[ind].mf_ant4;
                    FANT5_pb.Value = DRBE_obj_list[ind].mf_ant5;

                    FRCS0_pb.Value = DRBE_obj_list[ind].mf_rcs0;
                    FRCS1_pb.Value = DRBE_obj_list[ind].mf_rcs1;
                    FRCS2_pb.Value = DRBE_obj_list[ind].mf_rcs2;
                    FRCS3_pb.Value = DRBE_obj_list[ind].mf_rcs3;
                    FRCS4_pb.Value = DRBE_obj_list[ind].mf_rcs4;
                    FRCS5_pb.Value = DRBE_obj_list[ind].mf_rcs5;
                    FRCS6_pb.Value = DRBE_obj_list[ind].mf_rcs6;

                    maxa = DRBE_obj_list[ind].mf_ant0 + DRBE_obj_list[ind].mf_ant1 + DRBE_obj_list[ind].mf_ant2 + DRBE_obj_list[ind].mf_ant3 + DRBE_obj_list[ind].mf_ant4 + DRBE_obj_list[ind].mf_ant5;
                    maxr = DRBE_obj_list[ind].mf_rcs0 + DRBE_obj_list[ind].mf_rcs1 + DRBE_obj_list[ind].mf_rcs2 + DRBE_obj_list[ind].mf_rcs3 + DRBE_obj_list[ind].mf_rcs4 + DRBE_obj_list[ind].mf_rcs5 + DRBE_obj_list[ind].mf_rcs6;

                    FANT0_pb.Maximum = maxa;
                    FANT1_pb.Maximum = maxa;
                    FANT2_pb.Maximum = maxa;
                    FANT3_pb.Maximum = maxa;
                    FANT4_pb.Maximum = maxa;
                    FANT5_pb.Maximum = maxa;

                    FRCS0_pb.Maximum = maxr;
                    FRCS1_pb.Maximum = maxr;
                    FRCS2_pb.Maximum = maxr;
                    FRCS3_pb.Maximum = maxr;
                    FRCS4_pb.Maximum = maxr;
                    FRCS5_pb.Maximum = maxr;
                    FRCS6_pb.Maximum = maxr;

                    FANT0_i_tb.Text = DRBE_obj_list[ind].mf_ant0.ToString("E5");
                    FANT1_i_tb.Text = DRBE_obj_list[ind].mf_ant1.ToString("E5");
                    FANT2_i_tb.Text = DRBE_obj_list[ind].mf_ant2.ToString("E5");
                    FANT3_i_tb.Text = DRBE_obj_list[ind].mf_ant3.ToString("E5");
                    FANT4_i_tb.Text = DRBE_obj_list[ind].mf_ant4.ToString("E5");
                    FANT5_i_tb.Text = DRBE_obj_list[ind].mf_ant5.ToString("E5");

                    FRCS0_i_tb.Text = DRBE_obj_list[ind].mf_rcs0.ToString("E5");
                    FRCS1_i_tb.Text = DRBE_obj_list[ind].mf_rcs1.ToString("E5");
                    FRCS2_i_tb.Text = DRBE_obj_list[ind].mf_rcs2.ToString("E5");
                    FRCS3_i_tb.Text = DRBE_obj_list[ind].mf_rcs3.ToString("E5");
                    FRCS4_i_tb.Text = DRBE_obj_list[ind].mf_rcs4.ToString("E5");
                    FRCS5_i_tb.Text = DRBE_obj_list[ind].mf_rcs5.ToString("E5");
                    FRCS6_i_tb.Text = DRBE_obj_list[ind].mf_rcs6.ToString("E5");
                    break;

                case 7:

                    FANT0_pb.Value = DRBE_obj_list[ind].bf_ant0;
                    FANT1_pb.Value = DRBE_obj_list[ind].bf_ant1;
                    FANT2_pb.Value = DRBE_obj_list[ind].bf_ant2;
                    FANT3_pb.Value = DRBE_obj_list[ind].bf_ant3;
                    FANT4_pb.Value = DRBE_obj_list[ind].bf_ant4;
                    FANT5_pb.Value = DRBE_obj_list[ind].bf_ant5;

                    FRCS0_pb.Value = DRBE_obj_list[ind].bf_rcs0;
                    FRCS1_pb.Value = DRBE_obj_list[ind].bf_rcs1;
                    FRCS2_pb.Value = DRBE_obj_list[ind].bf_rcs2;
                    FRCS3_pb.Value = DRBE_obj_list[ind].bf_rcs3;
                    FRCS4_pb.Value = DRBE_obj_list[ind].bf_rcs4;
                    FRCS5_pb.Value = DRBE_obj_list[ind].bf_rcs5;
                    FRCS6_pb.Value = DRBE_obj_list[ind].bf_rcs6;

                    maxa = DRBE_obj_list[ind].bf_ant0 + DRBE_obj_list[ind].bf_ant1 + DRBE_obj_list[ind].bf_ant2 + DRBE_obj_list[ind].bf_ant3 + DRBE_obj_list[ind].bf_ant4 + DRBE_obj_list[ind].bf_ant5;
                    maxr = DRBE_obj_list[ind].bf_rcs0 + DRBE_obj_list[ind].bf_rcs1 + DRBE_obj_list[ind].bf_rcs2 + DRBE_obj_list[ind].bf_rcs3 + DRBE_obj_list[ind].bf_rcs4 + DRBE_obj_list[ind].bf_rcs5 + DRBE_obj_list[ind].bf_rcs6;

                    FANT0_pb.Maximum = maxa;
                    FANT1_pb.Maximum = maxa;
                    FANT2_pb.Maximum = maxa;
                    FANT3_pb.Maximum = maxa;
                    FANT4_pb.Maximum = maxa;
                    FANT5_pb.Maximum = maxa;

                    FRCS0_pb.Maximum = maxr;
                    FRCS1_pb.Maximum = maxr;
                    FRCS2_pb.Maximum = maxr;
                    FRCS3_pb.Maximum = maxr;
                    FRCS4_pb.Maximum = maxr;
                    FRCS5_pb.Maximum = maxr;
                    FRCS6_pb.Maximum = maxr;

                    FANT0_i_tb.Text = DRBE_obj_list[ind].bf_ant0.ToString("E5");
                    FANT1_i_tb.Text = DRBE_obj_list[ind].bf_ant1.ToString("E5");
                    FANT2_i_tb.Text = DRBE_obj_list[ind].bf_ant2.ToString("E5");
                    FANT3_i_tb.Text = DRBE_obj_list[ind].bf_ant3.ToString("E5");
                    FANT4_i_tb.Text = DRBE_obj_list[ind].bf_ant4.ToString("E5");
                    FANT5_i_tb.Text = DRBE_obj_list[ind].bf_ant5.ToString("E5");

                    FRCS0_i_tb.Text = DRBE_obj_list[ind].bf_rcs0.ToString("E5");
                    FRCS1_i_tb.Text = DRBE_obj_list[ind].bf_rcs1.ToString("E5");
                    FRCS2_i_tb.Text = DRBE_obj_list[ind].bf_rcs2.ToString("E5");
                    FRCS3_i_tb.Text = DRBE_obj_list[ind].bf_rcs3.ToString("E5");
                    FRCS4_i_tb.Text = DRBE_obj_list[ind].bf_rcs4.ToString("E5");
                    FRCS5_i_tb.Text = DRBE_obj_list[ind].bf_rcs5.ToString("E5");
                    FRCS6_i_tb.Text = DRBE_obj_list[ind].bf_rcs6.ToString("E5");
                    break;

                case 8:

                    FANT0_pb.Value = DRBE_obj_list[ind].lf_ant0;
                    FANT1_pb.Value = DRBE_obj_list[ind].lf_ant1;
                    FANT2_pb.Value = DRBE_obj_list[ind].lf_ant2;
                    FANT3_pb.Value = DRBE_obj_list[ind].lf_ant3;
                    FANT4_pb.Value = DRBE_obj_list[ind].lf_ant4;
                    FANT5_pb.Value = DRBE_obj_list[ind].lf_ant5;

                    FRCS0_pb.Value = DRBE_obj_list[ind].lf_rcs0;
                    FRCS1_pb.Value = DRBE_obj_list[ind].lf_rcs1;
                    FRCS2_pb.Value = DRBE_obj_list[ind].lf_rcs2;
                    FRCS3_pb.Value = DRBE_obj_list[ind].lf_rcs3;
                    FRCS4_pb.Value = DRBE_obj_list[ind].lf_rcs4;
                    FRCS5_pb.Value = DRBE_obj_list[ind].lf_rcs5;
                    FRCS6_pb.Value = DRBE_obj_list[ind].lf_rcs6;

                    maxa = DRBE_obj_list[ind].lf_ant0 + DRBE_obj_list[ind].lf_ant1 + DRBE_obj_list[ind].lf_ant2 + DRBE_obj_list[ind].lf_ant3 + DRBE_obj_list[ind].lf_ant4 + DRBE_obj_list[ind].lf_ant5;
                    maxr = DRBE_obj_list[ind].lf_rcs0 + DRBE_obj_list[ind].lf_rcs1 + DRBE_obj_list[ind].lf_rcs2 + DRBE_obj_list[ind].lf_rcs3 + DRBE_obj_list[ind].lf_rcs4 + DRBE_obj_list[ind].lf_rcs5 + DRBE_obj_list[ind].lf_rcs6;

                    FANT0_pb.Maximum = maxa;
                    FANT1_pb.Maximum = maxa;
                    FANT2_pb.Maximum = maxa;
                    FANT3_pb.Maximum = maxa;
                    FANT4_pb.Maximum = maxa;
                    FANT5_pb.Maximum = maxa;

                    FRCS0_pb.Maximum = maxr;
                    FRCS1_pb.Maximum = maxr;
                    FRCS2_pb.Maximum = maxr;
                    FRCS3_pb.Maximum = maxr;
                    FRCS4_pb.Maximum = maxr;
                    FRCS5_pb.Maximum = maxr;
                    FRCS6_pb.Maximum = maxr;

                    FANT0_i_tb.Text = DRBE_obj_list[ind].lf_ant0.ToString("E5");
                    FANT1_i_tb.Text = DRBE_obj_list[ind].lf_ant1.ToString("E5");
                    FANT2_i_tb.Text = DRBE_obj_list[ind].lf_ant2.ToString("E5");
                    FANT3_i_tb.Text = DRBE_obj_list[ind].lf_ant3.ToString("E5");
                    FANT4_i_tb.Text = DRBE_obj_list[ind].lf_ant4.ToString("E5");
                    FANT5_i_tb.Text = DRBE_obj_list[ind].lf_ant5.ToString("E5");

                    FRCS0_i_tb.Text = DRBE_obj_list[ind].lf_rcs0.ToString("E5");
                    FRCS1_i_tb.Text = DRBE_obj_list[ind].lf_rcs1.ToString("E5");
                    FRCS2_i_tb.Text = DRBE_obj_list[ind].lf_rcs2.ToString("E5");
                    FRCS3_i_tb.Text = DRBE_obj_list[ind].lf_rcs3.ToString("E5");
                    FRCS4_i_tb.Text = DRBE_obj_list[ind].lf_rcs4.ToString("E5");
                    FRCS5_i_tb.Text = DRBE_obj_list[ind].lf_rcs5.ToString("E5");
                    FRCS6_i_tb.Text = DRBE_obj_list[ind].lf_rcs6.ToString("E5");
                    break;


                case 9:

                    PRCS_ARL_pb.Minimum = 0;
                    PRCS_ARL_pb.Maximum = total_c;
                    PRCS_ARR_pb.Minimum = total_c;
                    PRCS_ARR_pb.Maximum = 2 * total_c;
                    PRCS_ARL_pb.Value = DRBE_obj_list[ind].cp_rcs_ar_n;
                    PRCS_ARR_pb.Value = DRBE_obj_list[ind].cp_rcs_ar_p;
                    PRCS_AR_ni_tb.Text = "- " + (total_c - DRBE_obj_list[ind].cp_rcs_ar_n).ToString("E5") + " FP-64b";
                    PRCS_AR_pi_tb.Text = "+ " + (DRBE_obj_list[ind].cp_rcs_ar_p - total_c).ToString("E5") + " FP-64b";

                    PRCS_FBL_pb.Minimum = 0;
                    PRCS_FBL_pb.Maximum = total_c;
                    PRCS_FBR_pb.Minimum = total_c;
                    PRCS_FBR_pb.Maximum = 2 * total_c;
                    PRCS_FBL_pb.Value = DRBE_obj_list[ind].cp_rcs_fb_n;
                    PRCS_FBR_pb.Value = DRBE_obj_list[ind].cp_rcs_fb_p;
                    PRCS_FB_ni_tb.Text = "- " + (total_c - DRBE_obj_list[ind].cp_rcs_fb_n).ToString("E5") + " FP-64b";
                    PRCS_FB_pi_tb.Text = "+ " + (DRBE_obj_list[ind].cp_rcs_fb_p - total_c).ToString("E5") + " FP-64b";

                    PRCS_SPL_pb.Minimum = 0;
                    PRCS_SPL_pb.Maximum = total_c;
                    PRCS_SPR_pb.Minimum = total_c;
                    PRCS_SPR_pb.Maximum = 2 * total_c;
                    PRCS_SPL_pb.Value = DRBE_obj_list[ind].cp_rcs_sp_n;
                    PRCS_SPR_pb.Value = DRBE_obj_list[ind].cp_rcs_sp_p;
                    PRCS_SP_ni_tb.Text = "- " + (total_c - DRBE_obj_list[ind].cp_rcs_sp_n).ToString("E5") + " FP-64b";
                    PRCS_SP_pi_tb.Text = "+ " + (DRBE_obj_list[ind].cp_rcs_sp_p - total_c).ToString("E5") + " FP-64b";

                    PRCS_SSL_pb.Minimum = 0;
                    PRCS_SSL_pb.Maximum = total_c;
                    PRCS_SSR_pb.Minimum = total_c;
                    PRCS_SSR_pb.Maximum = 2 * total_c;
                    PRCS_SSL_pb.Value = DRBE_obj_list[ind].cp_rcs_ss_n;
                    PRCS_SSR_pb.Value = DRBE_obj_list[ind].cp_rcs_ss_p;
                    PRCS_SS_ni_tb.Text = "- " + (total_c - DRBE_obj_list[ind].cp_rcs_ss_n).ToString("E5") + " FP-64b";
                    PRCS_SS_pi_tb.Text = "+ " + (DRBE_obj_list[ind].cp_rcs_ss_p - total_c).ToString("E5") + " FP-64b";

                    PRCS_PNL_pb.Minimum = 0;
                    PRCS_PNL_pb.Maximum = total_c;
                    PRCS_PNR_pb.Minimum = total_c;
                    PRCS_PNR_pb.Maximum = 2 * total_c;
                    PRCS_PNL_pb.Value = DRBE_obj_list[ind].cp_rcs_pn_n;
                    PRCS_PNR_pb.Value = DRBE_obj_list[ind].cp_rcs_pn_p;
                    PRCS_PN_ni_tb.Text = "- " + (total_c - DRBE_obj_list[ind].cp_rcs_pn_n).ToString("E5") + " FP-64b";
                    PRCS_PN_pi_tb.Text = "+ " + (DRBE_obj_list[ind].cp_rcs_pn_p - total_c).ToString("E5") + " FP-64b";

                    PANT_ARL_pb.Minimum = 0;
                    PANT_ARL_pb.Maximum = total_c;
                    PANT_ARR_pb.Minimum = total_c;
                    PANT_ARR_pb.Maximum = 2 * total_c;
                    PANT_ARL_pb.Value = DRBE_obj_list[ind].cp_ant_ar_n;
                    PANT_ARR_pb.Value = DRBE_obj_list[ind].cp_ant_ar_p;
                    PANT_AR_ni_tb.Text = "- " + (total_c - DRBE_obj_list[ind].cp_ant_ar_n).ToString("E5") + " FP-64b";
                    PANT_AR_pi_tb.Text = "+ " + (DRBE_obj_list[ind].cp_ant_ar_p - total_c).ToString("E5") + " FP-64b";

                    PANT_NAL_pb.Minimum = 0;
                    PANT_NAL_pb.Maximum = total_c;
                    PANT_NAR_pb.Minimum = total_c;
                    PANT_NAR_pb.Maximum = 2 * total_c;
                    PANT_NAL_pb.Value = DRBE_obj_list[ind].cp_ant_na_n;
                    PANT_NAR_pb.Value = DRBE_obj_list[ind].cp_ant_na_p;
                    PANT_NA_ni_tb.Text = "- " + (total_c - DRBE_obj_list[ind].cp_ant_na_n).ToString("E5") + " FP-64b";
                    PANT_NA_pi_tb.Text = "+ " + (DRBE_obj_list[ind].cp_ant_na_p - total_c).ToString("E5") + " FP-64b";

                    PANT_DSL_pb.Minimum = 0;
                    PANT_DSL_pb.Maximum = total_c;
                    PANT_DSR_pb.Minimum = total_c;
                    PANT_DSR_pb.Maximum = 2 * total_c;
                    PANT_DSL_pb.Value = DRBE_obj_list[ind].cp_ant_ds_n;
                    PANT_DSR_pb.Value = DRBE_obj_list[ind].cp_ant_ds_p;
                    PANT_DS_ni_tb.Text = "- " + (total_c - DRBE_obj_list[ind].cp_ant_ds_n).ToString("E5") + " FP-64b";
                    PANT_DS_pi_tb.Text = "+ " + (DRBE_obj_list[ind].cp_ant_ds_p - total_c).ToString("E5") + " FP-64b";
                    break;


                case 10:

                    PRCS_ARL_pb.Minimum = 0;
                    PRCS_ARL_pb.Maximum = total_m;
                    PRCS_ARR_pb.Minimum = total_m;
                    PRCS_ARR_pb.Maximum = 2 * total_m;
                    PRCS_ARL_pb.Value = DRBE_obj_list[ind].mp_rcs_ar_n;
                    PRCS_ARR_pb.Value = DRBE_obj_list[ind].mp_rcs_ar_p;
                    PRCS_AR_ni_tb.Text = "- " + (total_m - DRBE_obj_list[ind].mp_rcs_ar_n).ToString("E5") + " FP-64b";
                    PRCS_AR_pi_tb.Text = "+ " + (DRBE_obj_list[ind].mp_rcs_ar_p - total_m).ToString("E5") + " FP-64b";

                    PRCS_FBL_pb.Minimum = 0;
                    PRCS_FBL_pb.Maximum = total_m;
                    PRCS_FBR_pb.Minimum = total_m;
                    PRCS_FBR_pb.Maximum = 2 * total_m;
                    PRCS_FBL_pb.Value = DRBE_obj_list[ind].mp_rcs_fb_n;
                    PRCS_FBR_pb.Value = DRBE_obj_list[ind].mp_rcs_fb_p;
                    PRCS_FB_ni_tb.Text = "- " + (total_m - DRBE_obj_list[ind].mp_rcs_fb_n).ToString("E5") + " FP-64b";
                    PRCS_FB_pi_tb.Text = "+ " + (DRBE_obj_list[ind].mp_rcs_fb_p - total_m).ToString("E5") + " FP-64b";

                    PRCS_SPL_pb.Minimum = 0;
                    PRCS_SPL_pb.Maximum = total_m;
                    PRCS_SPR_pb.Minimum = total_m;
                    PRCS_SPR_pb.Maximum = 2 * total_m;
                    PRCS_SPL_pb.Value = DRBE_obj_list[ind].mp_rcs_sp_n;
                    PRCS_SPR_pb.Value = DRBE_obj_list[ind].mp_rcs_sp_p;
                    PRCS_SP_ni_tb.Text = "- " + (total_m - DRBE_obj_list[ind].mp_rcs_sp_n).ToString("E5") + " FP-64b";
                    PRCS_SP_pi_tb.Text = "+ " + (DRBE_obj_list[ind].mp_rcs_sp_p - total_m).ToString("E5") + " FP-64b";

                    PRCS_SSL_pb.Minimum = 0;
                    PRCS_SSL_pb.Maximum = total_m;
                    PRCS_SSR_pb.Minimum = total_m;
                    PRCS_SSR_pb.Maximum = 2 * total_m;
                    PRCS_SSL_pb.Value = DRBE_obj_list[ind].mp_rcs_ss_n;
                    PRCS_SSR_pb.Value = DRBE_obj_list[ind].mp_rcs_ss_p;
                    PRCS_SS_ni_tb.Text = "- " + (total_m - DRBE_obj_list[ind].mp_rcs_ss_n).ToString("E5") + " FP-64b";
                    PRCS_SS_pi_tb.Text = "+ " + (DRBE_obj_list[ind].mp_rcs_ss_p - total_m).ToString("E5") + " FP-64b";

                    PRCS_PNL_pb.Minimum = 0;
                    PRCS_PNL_pb.Maximum = total_m;
                    PRCS_PNR_pb.Minimum = total_m;
                    PRCS_PNR_pb.Maximum = 2 * total_m;
                    PRCS_PNL_pb.Value = DRBE_obj_list[ind].mp_rcs_pn_n;
                    PRCS_PNR_pb.Value = DRBE_obj_list[ind].mp_rcs_pn_p;
                    PRCS_PN_ni_tb.Text = "- " + (total_m - DRBE_obj_list[ind].mp_rcs_pn_n).ToString("E5") + " FP-64b";
                    PRCS_PN_pi_tb.Text = "+ " + (DRBE_obj_list[ind].mp_rcs_pn_p - total_m).ToString("E5") + " FP-64b";

                    PANT_ARL_pb.Minimum = 0;
                    PANT_ARL_pb.Maximum = total_m;
                    PANT_ARR_pb.Minimum = total_m;
                    PANT_ARR_pb.Maximum = 2 * total_m;
                    PANT_ARL_pb.Value = DRBE_obj_list[ind].mp_ant_ar_n;
                    PANT_ARR_pb.Value = DRBE_obj_list[ind].mp_ant_ar_p;
                    PANT_AR_ni_tb.Text = "- " + (total_m - DRBE_obj_list[ind].mp_ant_ar_n).ToString("E5") + " FP-64b";
                    PANT_AR_pi_tb.Text = "+ " + (DRBE_obj_list[ind].mp_ant_ar_p - total_m).ToString("E5") + " FP-64b";

                    PANT_NAL_pb.Minimum = 0;
                    PANT_NAL_pb.Maximum = total_m;
                    PANT_NAR_pb.Minimum = total_m;
                    PANT_NAR_pb.Maximum = 2 * total_m;
                    PANT_NAL_pb.Value = DRBE_obj_list[ind].mp_ant_na_n;
                    PANT_NAR_pb.Value = DRBE_obj_list[ind].mp_ant_na_p;
                    PANT_NA_ni_tb.Text = "- " + (total_m - DRBE_obj_list[ind].mp_ant_na_n).ToString("E5") + " FP-64b";
                    PANT_NA_pi_tb.Text = "+ " + (DRBE_obj_list[ind].mp_ant_na_p - total_m).ToString("E5") + " FP-64b";

                    PANT_DSL_pb.Minimum = 0;
                    PANT_DSL_pb.Maximum = total_m;
                    PANT_DSR_pb.Minimum = total_m;
                    PANT_DSR_pb.Maximum = 2 * total_m;
                    PANT_DSL_pb.Value = DRBE_obj_list[ind].mp_ant_ds_n;
                    PANT_DSR_pb.Value = DRBE_obj_list[ind].mp_ant_ds_p;
                    PANT_DS_ni_tb.Text = "- " + (total_m - DRBE_obj_list[ind].mp_ant_ds_n).ToString("E5") + " FP-64b";
                    PANT_DS_pi_tb.Text = "+ " + (DRBE_obj_list[ind].mp_ant_ds_p - total_m).ToString("E5") + " FP-64b";
                    break;



                case 11:
                    PRCS_ARL_pb.Minimum = 0;
                    PRCS_ARL_pb.Maximum = total_b;
                    PRCS_ARR_pb.Minimum = total_b;
                    PRCS_ARR_pb.Maximum = 2 * total_b;
                    PRCS_ARL_pb.Value = DRBE_obj_list[ind].bp_rcs_ar_n;
                    PRCS_ARR_pb.Value = DRBE_obj_list[ind].bp_rcs_ar_p;
                    PRCS_AR_ni_tb.Text = "- " + (total_b - DRBE_obj_list[ind].bp_rcs_ar_n).ToString("E5") + " FP-64b";
                    PRCS_AR_pi_tb.Text = "+ " + (DRBE_obj_list[ind].bp_rcs_ar_p - total_b).ToString("E5") + " FP-64b";

                    PRCS_FBL_pb.Minimum = 0;
                    PRCS_FBL_pb.Maximum = total_b;
                    PRCS_FBR_pb.Minimum = total_b;
                    PRCS_FBR_pb.Maximum = 2 * total_b;
                    PRCS_FBL_pb.Value = DRBE_obj_list[ind].bp_rcs_fb_n;
                    PRCS_FBR_pb.Value = DRBE_obj_list[ind].bp_rcs_fb_p;
                    PRCS_FB_ni_tb.Text = "- " + (total_b - DRBE_obj_list[ind].bp_rcs_fb_n).ToString("E5") + " FP-64b";
                    PRCS_FB_pi_tb.Text = "+ " + (DRBE_obj_list[ind].bp_rcs_fb_p - total_b).ToString("E5") + " FP-64b";

                    PRCS_SPL_pb.Minimum = 0;
                    PRCS_SPL_pb.Maximum = total_b;
                    PRCS_SPR_pb.Minimum = total_b;
                    PRCS_SPR_pb.Maximum = 2 * total_b;
                    PRCS_SPL_pb.Value = DRBE_obj_list[ind].bp_rcs_sp_n;
                    PRCS_SPR_pb.Value = DRBE_obj_list[ind].bp_rcs_sp_p;
                    PRCS_SP_ni_tb.Text = "- " + (total_b - DRBE_obj_list[ind].bp_rcs_sp_n).ToString("E5") + " FP-64b";
                    PRCS_SP_pi_tb.Text = "+ " + (DRBE_obj_list[ind].bp_rcs_sp_p - total_b).ToString("E5") + " FP-64b";

                    PRCS_SSL_pb.Minimum = 0;
                    PRCS_SSL_pb.Maximum = total_b;
                    PRCS_SSR_pb.Minimum = total_b;
                    PRCS_SSR_pb.Maximum = 2 * total_b;
                    PRCS_SSL_pb.Value = DRBE_obj_list[ind].bp_rcs_ss_n;
                    PRCS_SSR_pb.Value = DRBE_obj_list[ind].bp_rcs_ss_p;
                    PRCS_SS_ni_tb.Text = "- " + (total_b - DRBE_obj_list[ind].bp_rcs_ss_n).ToString("E5") + " FP-64b";
                    PRCS_SS_pi_tb.Text = "+ " + (DRBE_obj_list[ind].bp_rcs_ss_p - total_b).ToString("E5") + " FP-64b";

                    PRCS_PNL_pb.Minimum = 0;
                    PRCS_PNL_pb.Maximum = total_b;
                    PRCS_PNR_pb.Minimum = total_b;
                    PRCS_PNR_pb.Maximum = 2 * total_b;
                    PRCS_PNL_pb.Value = DRBE_obj_list[ind].bp_rcs_pn_n;
                    PRCS_PNR_pb.Value = DRBE_obj_list[ind].bp_rcs_pn_p;
                    PRCS_PN_ni_tb.Text = "- " + (total_b - DRBE_obj_list[ind].bp_rcs_pn_n).ToString("E5") + " FP-64b";
                    PRCS_PN_pi_tb.Text = "+ " + (DRBE_obj_list[ind].bp_rcs_pn_p - total_b).ToString("E5") + " FP-64b";

                    PANT_ARL_pb.Minimum = 0;
                    PANT_ARL_pb.Maximum = total_b;
                    PANT_ARR_pb.Minimum = total_b;
                    PANT_ARR_pb.Maximum = 2 * total_b;
                    PANT_ARL_pb.Value = DRBE_obj_list[ind].bp_ant_ar_n;
                    PANT_ARR_pb.Value = DRBE_obj_list[ind].bp_ant_ar_p;
                    PANT_AR_ni_tb.Text = "- " + (total_b - DRBE_obj_list[ind].bp_ant_ar_n).ToString("E5") + " FP-64b";
                    PANT_AR_pi_tb.Text = "+ " + (DRBE_obj_list[ind].bp_ant_ar_p - total_b).ToString("E5") + " FP-64b";

                    PANT_NAL_pb.Minimum = 0;
                    PANT_NAL_pb.Maximum = total_b;
                    PANT_NAR_pb.Minimum = total_b;
                    PANT_NAR_pb.Maximum = 2 * total_b;
                    PANT_NAL_pb.Value = DRBE_obj_list[ind].bp_ant_na_n;
                    PANT_NAR_pb.Value = DRBE_obj_list[ind].bp_ant_na_p;
                    PANT_NA_ni_tb.Text = "- " + (total_b - DRBE_obj_list[ind].bp_ant_na_n).ToString("E5") + " FP-64b";
                    PANT_NA_pi_tb.Text = "+ " + (DRBE_obj_list[ind].bp_ant_na_p - total_b).ToString("E5") + " FP-64b";

                    PANT_DSL_pb.Minimum = 0;
                    PANT_DSL_pb.Maximum = total_b;
                    PANT_DSR_pb.Minimum = total_b;
                    PANT_DSR_pb.Maximum = 2 * total_b;
                    PANT_DSL_pb.Value = DRBE_obj_list[ind].bp_ant_ds_n;
                    PANT_DSR_pb.Value = DRBE_obj_list[ind].bp_ant_ds_p;
                    PANT_DS_ni_tb.Text = "- " + (total_b - DRBE_obj_list[ind].bp_ant_ds_n).ToString("E5") + " FP-64b";
                    PANT_DS_pi_tb.Text = "+ " + (DRBE_obj_list[ind].bp_ant_ds_p - total_b).ToString("E5") + " FP-64b";
                    break;


                case 12:
                    PRCS_ARL_pb.Minimum = 0;
                    PRCS_ARL_pb.Maximum = total_l;
                    PRCS_ARR_pb.Minimum = total_l;
                    PRCS_ARR_pb.Maximum = 2 * total_l;
                    PRCS_ARL_pb.Value = DRBE_obj_list[ind].lp_rcs_ar_n;
                    PRCS_ARR_pb.Value = DRBE_obj_list[ind].lp_rcs_ar_p;
                    PRCS_AR_ni_tb.Text = "- " + (total_l - DRBE_obj_list[ind].lp_rcs_ar_n).ToString("E5") + " FP-64b";
                    PRCS_AR_pi_tb.Text = "+ " + (DRBE_obj_list[ind].lp_rcs_ar_p - total_l).ToString("E5") + " FP-64b";

                    PRCS_FBL_pb.Minimum = 0;
                    PRCS_FBL_pb.Maximum = total_l;
                    PRCS_FBR_pb.Minimum = total_l;
                    PRCS_FBR_pb.Maximum = 2 * total_l;
                    PRCS_FBL_pb.Value = DRBE_obj_list[ind].lp_rcs_fb_n;
                    PRCS_FBR_pb.Value = DRBE_obj_list[ind].lp_rcs_fb_p;
                    PRCS_FB_ni_tb.Text = "- " + (total_l - DRBE_obj_list[ind].lp_rcs_fb_n).ToString("E5") + " FP-64b";
                    PRCS_FB_pi_tb.Text = "+ " + (DRBE_obj_list[ind].lp_rcs_fb_p - total_l).ToString("E5") + " FP-64b";

                    PRCS_SPL_pb.Minimum = 0;
                    PRCS_SPL_pb.Maximum = total_l;
                    PRCS_SPR_pb.Minimum = total_l;
                    PRCS_SPR_pb.Maximum = 2 * total_l;
                    PRCS_SPL_pb.Value = DRBE_obj_list[ind].lp_rcs_sp_n;
                    PRCS_SPR_pb.Value = DRBE_obj_list[ind].lp_rcs_sp_p;
                    PRCS_SP_ni_tb.Text = "- " + (total_l - DRBE_obj_list[ind].lp_rcs_sp_n).ToString("E5") + " FP-64b";
                    PRCS_SP_pi_tb.Text = "+ " + (DRBE_obj_list[ind].lp_rcs_sp_p - total_l).ToString("E5") + " FP-64b";

                    PRCS_SSL_pb.Minimum = 0;
                    PRCS_SSL_pb.Maximum = total_l;
                    PRCS_SSR_pb.Minimum = total_l;
                    PRCS_SSR_pb.Maximum = 2 * total_l;
                    PRCS_SSL_pb.Value = DRBE_obj_list[ind].lp_rcs_ss_n;
                    PRCS_SSR_pb.Value = DRBE_obj_list[ind].lp_rcs_ss_p;
                    PRCS_SS_ni_tb.Text = "- " + (total_l - DRBE_obj_list[ind].lp_rcs_ss_n).ToString("E5") + " FP-64b";
                    PRCS_SS_pi_tb.Text = "+ " + (DRBE_obj_list[ind].lp_rcs_ss_p - total_l).ToString("E5") + " FP-64b";

                    PRCS_PNL_pb.Minimum = 0;
                    PRCS_PNL_pb.Maximum = total_l;
                    PRCS_PNR_pb.Minimum = total_l;
                    PRCS_PNR_pb.Maximum = 2 * total_l;
                    PRCS_PNL_pb.Value = DRBE_obj_list[ind].lp_rcs_pn_n;
                    PRCS_PNR_pb.Value = DRBE_obj_list[ind].lp_rcs_pn_p;
                    PRCS_PN_ni_tb.Text = "- " + (total_l - DRBE_obj_list[ind].lp_rcs_pn_n).ToString("E5") + " FP-64b";
                    PRCS_PN_pi_tb.Text = "+ " + (DRBE_obj_list[ind].lp_rcs_pn_p - total_l).ToString("E5") + " FP-64b";

                    PANT_ARL_pb.Minimum = 0;
                    PANT_ARL_pb.Maximum = total_l;
                    PANT_ARR_pb.Minimum = total_l;
                    PANT_ARR_pb.Maximum = 2 * total_l;
                    PANT_ARL_pb.Value = DRBE_obj_list[ind].lp_ant_ar_n;
                    PANT_ARR_pb.Value = DRBE_obj_list[ind].lp_ant_ar_p;
                    PANT_AR_ni_tb.Text = "- " + (total_l - DRBE_obj_list[ind].lp_ant_ar_n).ToString("E5") + " FP-64b";
                    PANT_AR_pi_tb.Text = "+ " + (DRBE_obj_list[ind].lp_ant_ar_p - total_l).ToString("E5") + " FP-64b";

                    PANT_NAL_pb.Minimum = 0;
                    PANT_NAL_pb.Maximum = total_l;
                    PANT_NAR_pb.Minimum = total_l;
                    PANT_NAR_pb.Maximum = 2 * total_l;
                    PANT_NAL_pb.Value = DRBE_obj_list[ind].lp_ant_na_n;
                    PANT_NAR_pb.Value = DRBE_obj_list[ind].lp_ant_na_p;
                    PANT_NA_ni_tb.Text = "- " + (total_l - DRBE_obj_list[ind].lp_ant_na_n).ToString("E5") + " FP-64b";
                    PANT_NA_pi_tb.Text = "+ " + (DRBE_obj_list[ind].lp_ant_na_p - total_l).ToString("E5") + " FP-64b";

                    PANT_DSL_pb.Minimum = 0;
                    PANT_DSL_pb.Maximum = total_l;
                    PANT_DSR_pb.Minimum = total_l;
                    PANT_DSR_pb.Maximum = 2 * total_l;
                    PANT_DSL_pb.Value = DRBE_obj_list[ind].lp_ant_ds_n;
                    PANT_DSR_pb.Value = DRBE_obj_list[ind].lp_ant_ds_p;
                    PANT_DS_ni_tb.Text = "- " + (total_l - DRBE_obj_list[ind].lp_ant_ds_n).ToString("E5") + " FP-64b";
                    PANT_DS_pi_tb.Text = "+ " + (DRBE_obj_list[ind].lp_ant_ds_p - total_l).ToString("E5") + " FP-64b";
                    break;

                default:
                    await ShowDialog("Panel Update Error", "Unexpected Mode");
                    break;
            }

               
        }
        private void Scan_obj_refresh(int ind)
        {
            DRBE_obj_list[ind].number_of_path = 0;

            FRCS0_l_tb.Foreground = white_button_brush;
            FRCS1_l_tb.Foreground = white_button_brush;
            FRCS2_l_tb.Foreground = white_button_brush;
            FRCS3_l_tb.Foreground = white_button_brush;
            FRCS4_l_tb.Foreground = white_button_brush;
            FRCS5_l_tb.Foreground = white_button_brush;
            FRCS6_l_tb.Foreground = white_button_brush;

            FANT0_l_tb.Foreground = white_button_brush;
            FANT1_l_tb.Foreground = white_button_brush;
            FANT2_l_tb.Foreground = white_button_brush;
            FANT3_l_tb.Foreground = white_button_brush;
            FANT4_l_tb.Foreground = white_button_brush;
            FANT5_l_tb.Foreground = white_button_brush;

            DRBE_obj_list[ind].cp_rcs_pn_n = 0;
            DRBE_obj_list[ind].cp_rcs_pn_p = 0;

            DRBE_obj_list[ind].cp_rcs_ss_n = 0;
            DRBE_obj_list[ind].cp_rcs_ss_p = 0;

            DRBE_obj_list[ind].cp_rcs_sp_n = 0;
            DRBE_obj_list[ind].cp_rcs_sp_p = 0;

            DRBE_obj_list[ind].cp_rcs_ar_n = 0;
            DRBE_obj_list[ind].cp_rcs_ar_p = 0;

            DRBE_obj_list[ind].cp_rcs_fb_n = 0;
            DRBE_obj_list[ind].cp_rcs_fb_p = 0;

            DRBE_obj_list[ind].cp_ant_ar_n = 0;
            DRBE_obj_list[ind].cp_ant_ar_p = 0;

            DRBE_obj_list[ind].cp_ant_na_n = 0;
            DRBE_obj_list[ind].cp_ant_na_p = 0;

            DRBE_obj_list[ind].cp_ant_ds_n = 0;
            DRBE_obj_list[ind].cp_ant_ds_p = 0;



            DRBE_obj_list[ind].mp_rcs_pn_n = 0;
            DRBE_obj_list[ind].mp_rcs_pn_p = 0;

            DRBE_obj_list[ind].mp_rcs_ss_n = 0;
            DRBE_obj_list[ind].mp_rcs_ss_p = 0;

            DRBE_obj_list[ind].mp_rcs_sp_n = 0;
            DRBE_obj_list[ind].mp_rcs_sp_p = 0;

            DRBE_obj_list[ind].mp_rcs_ar_n = 0;
            DRBE_obj_list[ind].mp_rcs_ar_p = 0;

            DRBE_obj_list[ind].mp_rcs_fb_n = 0;
            DRBE_obj_list[ind].mp_rcs_fb_p = 0;

            DRBE_obj_list[ind].mp_ant_ar_n = 0;
            DRBE_obj_list[ind].mp_ant_ar_p = 0;

            DRBE_obj_list[ind].mp_ant_na_n = 0;
            DRBE_obj_list[ind].mp_ant_na_p = 0;

            DRBE_obj_list[ind].mp_ant_ds_n = 0;
            DRBE_obj_list[ind].mp_ant_ds_p = 0;



            DRBE_obj_list[ind].bp_rcs_pn_n = 0;
            DRBE_obj_list[ind].bp_rcs_pn_p = 0;

            DRBE_obj_list[ind].bp_rcs_ss_n = 0;
            DRBE_obj_list[ind].bp_rcs_ss_p = 0;

            DRBE_obj_list[ind].bp_rcs_sp_n = 0;
            DRBE_obj_list[ind].bp_rcs_sp_p = 0;

            DRBE_obj_list[ind].bp_rcs_ar_n = 0;
            DRBE_obj_list[ind].bp_rcs_ar_p = 0;

            DRBE_obj_list[ind].bp_rcs_fb_n = 0;
            DRBE_obj_list[ind].bp_rcs_fb_p = 0;

            DRBE_obj_list[ind].bp_ant_ar_n = 0;
            DRBE_obj_list[ind].bp_ant_ar_p = 0;

            DRBE_obj_list[ind].bp_ant_na_n = 0;
            DRBE_obj_list[ind].bp_ant_na_p = 0;

            DRBE_obj_list[ind].bp_ant_ds_n = 0;
            DRBE_obj_list[ind].bp_ant_ds_p = 0;





            DRBE_obj_list[ind].lp_rcs_pn_n = 0;
            DRBE_obj_list[ind].lp_rcs_pn_p = 0;

            DRBE_obj_list[ind].lp_rcs_ss_n = 0;
            DRBE_obj_list[ind].lp_rcs_ss_p = 0;

            DRBE_obj_list[ind].lp_rcs_sp_n = 0;
            DRBE_obj_list[ind].lp_rcs_sp_p = 0;

            DRBE_obj_list[ind].lp_rcs_ar_n = 0;
            DRBE_obj_list[ind].lp_rcs_ar_p = 0;

            DRBE_obj_list[ind].lp_rcs_fb_n = 0;
            DRBE_obj_list[ind].lp_rcs_fb_p = 0;
        
            DRBE_obj_list[ind].lp_ant_ar_n = 0;
            DRBE_obj_list[ind].lp_ant_ar_p = 0;

            DRBE_obj_list[ind].lp_ant_na_n = 0;
            DRBE_obj_list[ind].lp_ant_na_p = 0;

            DRBE_obj_list[ind].lp_ant_ds_n = 0;
            DRBE_obj_list[ind].lp_ant_ds_p = 0;

            DRBE_obj_list[ind].cf_ant0 = 0;
            DRBE_obj_list[ind].cf_ant1 = 0;
            DRBE_obj_list[ind].cf_ant2 = 0;
            DRBE_obj_list[ind].cf_ant3 = 0;
            DRBE_obj_list[ind].cf_ant4 = 0;
            DRBE_obj_list[ind].cf_ant5 = 0;

            DRBE_obj_list[ind].cf_rcs0 = 0;
            DRBE_obj_list[ind].cf_rcs1 = 0;
            DRBE_obj_list[ind].cf_rcs2 = 0;
            DRBE_obj_list[ind].cf_rcs3 = 0;
            DRBE_obj_list[ind].cf_rcs4 = 0;
            DRBE_obj_list[ind].cf_rcs5 = 0;
            DRBE_obj_list[ind].cf_rcs6 = 0;


            DRBE_obj_list[ind].mf_ant0 = 0;
            DRBE_obj_list[ind].mf_ant1 = 0;
            DRBE_obj_list[ind].mf_ant2 = 0;
            DRBE_obj_list[ind].mf_ant3 = 0;
            DRBE_obj_list[ind].mf_ant4 = 0;
            DRBE_obj_list[ind].mf_ant5 = 0;

            DRBE_obj_list[ind].mf_rcs0 = 0;
            DRBE_obj_list[ind].mf_rcs1 = 0;
            DRBE_obj_list[ind].mf_rcs2 = 0;
            DRBE_obj_list[ind].mf_rcs3 = 0;
            DRBE_obj_list[ind].mf_rcs4 = 0;
            DRBE_obj_list[ind].mf_rcs5 = 0;
            DRBE_obj_list[ind].mf_rcs6 = 0;



            DRBE_obj_list[ind].bf_ant0 = 0;
            DRBE_obj_list[ind].bf_ant1 = 0;
            DRBE_obj_list[ind].bf_ant2 = 0;
            DRBE_obj_list[ind].bf_ant3 = 0;
            DRBE_obj_list[ind].bf_ant4 = 0;
            DRBE_obj_list[ind].bf_ant5 = 0;

            DRBE_obj_list[ind].bf_rcs0 = 0;
            DRBE_obj_list[ind].bf_rcs1 = 0;
            DRBE_obj_list[ind].bf_rcs2 = 0;
            DRBE_obj_list[ind].bf_rcs3 = 0;
            DRBE_obj_list[ind].bf_rcs4 = 0;
            DRBE_obj_list[ind].bf_rcs5 = 0;
            DRBE_obj_list[ind].bf_rcs6 = 0;



            DRBE_obj_list[ind].lf_ant0 = 0;
            DRBE_obj_list[ind].lf_ant1 = 0;
            DRBE_obj_list[ind].lf_ant2 = 0;
            DRBE_obj_list[ind].lf_ant3 = 0;
            DRBE_obj_list[ind].lf_ant4 = 0;
            DRBE_obj_list[ind].lf_ant5 = 0;

            DRBE_obj_list[ind].lf_rcs0 = 0;
            DRBE_obj_list[ind].lf_rcs1 = 0;
            DRBE_obj_list[ind].lf_rcs2 = 0;
            DRBE_obj_list[ind].lf_rcs3 = 0;
            DRBE_obj_list[ind].lf_rcs4 = 0;
            DRBE_obj_list[ind].lf_rcs5 = 0;
            DRBE_obj_list[ind].lf_rcs6 = 0;

            DRBE_obj_scan_latency_list = new List<List<double>>();
            int i = 0;
            int ii = 0;
            i = 0;
            while(i<7)
            {
                DRBE_obj_scan_latency_list.Add(new List<double>());
                ii = 0;
                while(ii<29)
                {
                    DRBE_obj_scan_latency_list[i].Add(0);
                    ii++;
                }
                i++;
            }
        }
        private async Task Scan_obj_update(List<double> x, int ind, int pind, int mode)
        {
            //ind object number
            //pind property number
            //mode t,o,r
            //x information
            //bound lower, original, uppder

            if (mode == 0 || mode == 2)//transmitter
            {
                switch (pind)
                {


                    case 0://ant 0

                        //DRBE_obj_list[ind].cf_ant0 += x[3];  //cor
                        DRBE_obj_list[ind].cf_ant0 += x[7];  //nr
                        DRBE_obj_list[ind].cf_ant0 += x[11]; //ori
                        DRBE_obj_list[ind].cf_ant0 += x[15]; //ant
                        DRBE_obj_list[ind].cf_ant0 += x[19]; //pg
                                                             //DRBE_obj_list[ind].cf_ant0 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_ant0 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_ant0 += x[4];  //cor
                        DRBE_obj_list[ind].mf_ant0 += x[8];  //nr
                        DRBE_obj_list[ind].mf_ant0 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_ant0 += x[16]; //ant
                        DRBE_obj_list[ind].mf_ant0 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_ant0 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_ant0 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_ant0 += x[5];  //cor
                        DRBE_obj_list[ind].bf_ant0 += x[9];  //nr
                        DRBE_obj_list[ind].bf_ant0 += x[13]; //ori
                        DRBE_obj_list[ind].bf_ant0 += x[17]; //ant
                        DRBE_obj_list[ind].bf_ant0 += x[21]; //pg
                                                             //DRBE_obj_list[ind].bf_ant0 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_ant0 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break;

                    case 1:

                        //DRBE_obj_list[ind].cf_ant1 += x[3];  //cor
                        DRBE_obj_list[ind].cf_ant1 += x[7];  //nr
                        DRBE_obj_list[ind].cf_ant1 += x[11]; //ori
                        DRBE_obj_list[ind].cf_ant1 += x[15]; //ant
                        DRBE_obj_list[ind].cf_ant1 += x[19]; //pg
                                                             //DRBE_obj_list[ind].cf_ant1 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_ant1 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_ant1 += x[4];  //cor
                        DRBE_obj_list[ind].mf_ant1 += x[8];  //nr
                        DRBE_obj_list[ind].mf_ant1 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_ant1 += x[16]; //ant
                        DRBE_obj_list[ind].mf_ant1 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_ant1 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_ant1 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_ant1 += x[5];  //cor
                        DRBE_obj_list[ind].bf_ant1 += x[9];  //nr
                        DRBE_obj_list[ind].bf_ant1 += x[13]; //ori
                        DRBE_obj_list[ind].bf_ant1 += x[17]; //ant
                        DRBE_obj_list[ind].bf_ant1 += x[21]; //pg
                                                             //DRBE_obj_list[ind].bf_ant1 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_ant1 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                                                                                                                    //ant 1
                        break;
                    case 2:

                        //DRBE_obj_list[ind].cf_ant2 += x[3];  //cor
                        DRBE_obj_list[ind].cf_ant2 += x[7];  //nr
                        DRBE_obj_list[ind].cf_ant2 += x[11]; //ori
                        DRBE_obj_list[ind].cf_ant2 += x[15]; //ant
                        DRBE_obj_list[ind].cf_ant2 += x[19]; //pg
                                                             //DRBE_obj_list[ind].cf_ant2 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_ant2 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_ant2 += x[4];  //cor
                        DRBE_obj_list[ind].mf_ant2 += x[8];  //nr
                        DRBE_obj_list[ind].mf_ant2 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_ant2 += x[16]; //ant
                        DRBE_obj_list[ind].mf_ant2 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_ant2 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_ant2 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_ant2 += x[5];  //cor
                        DRBE_obj_list[ind].bf_ant2 += x[9];  //nr
                        DRBE_obj_list[ind].bf_ant2 += x[13]; //ori
                        DRBE_obj_list[ind].bf_ant2 += x[17]; //ant
                        DRBE_obj_list[ind].bf_ant2 += x[21]; //pg
                                                             //DRBE_obj_list[ind].bf_ant2 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_ant2 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                                                                                                                    //ant 2
                        break;
                    case 3:

                        //DRBE_obj_list[ind].cf_ant3 += x[3];  //cor
                        DRBE_obj_list[ind].cf_ant3 += x[7];  //nr
                        DRBE_obj_list[ind].cf_ant3 += x[11]; //ori
                        DRBE_obj_list[ind].cf_ant3 += x[15]; //ant
                        DRBE_obj_list[ind].cf_ant3 += x[19]; //pg
                                                             //DRBE_obj_list[ind].cf_ant3 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_ant3 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_ant3 += x[4];  //cor
                        DRBE_obj_list[ind].mf_ant3 += x[8];  //nr
                        DRBE_obj_list[ind].mf_ant3 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_ant3 += x[16]; //ant
                        DRBE_obj_list[ind].mf_ant3 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_ant3 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_ant3 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_ant3 += x[5];  //cor
                        DRBE_obj_list[ind].bf_ant3 += x[9];  //nr
                        DRBE_obj_list[ind].bf_ant3 += x[13]; //ori
                        DRBE_obj_list[ind].bf_ant3 += x[17]; //ant
                        DRBE_obj_list[ind].bf_ant3 += x[21]; //pg
                                                             //DRBE_obj_list[ind].bf_ant3 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_ant3 += x[29]; //tu
                                                             //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ant 3
                    case 4:

                        //DRBE_obj_list[ind].cf_ant4 += x[3];  //cor
                        DRBE_obj_list[ind].cf_ant4 += x[7];  //nr
                        DRBE_obj_list[ind].cf_ant4 += x[11]; //ori
                        DRBE_obj_list[ind].cf_ant4 += x[15]; //ant
                        DRBE_obj_list[ind].cf_ant4 += x[19]; //pg
                                                             //DRBE_obj_list[ind].cf_ant4 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_ant4 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_ant4 += x[4];  //cor
                        DRBE_obj_list[ind].mf_ant4 += x[8];  //nr
                        DRBE_obj_list[ind].mf_ant4 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_ant4 += x[16]; //ant
                        DRBE_obj_list[ind].mf_ant4 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_ant4 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_ant4 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_ant4 += x[5];  //cor
                        DRBE_obj_list[ind].bf_ant4 += x[9];  //nr
                        DRBE_obj_list[ind].bf_ant4 += x[13]; //ori
                        DRBE_obj_list[ind].bf_ant4 += x[17]; //ant
                        DRBE_obj_list[ind].bf_ant4 += x[21]; //pg
                                                             //DRBE_obj_list[ind].bf_ant4 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_ant4 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ant 4
                    case 5:

                        //DRBE_obj_list[ind].cf_ant5 += x[3];  //cor
                        DRBE_obj_list[ind].cf_ant5 += x[7];  //nr
                        DRBE_obj_list[ind].cf_ant5 += x[11]; //ori
                        DRBE_obj_list[ind].cf_ant5 += x[15]; //ant
                        DRBE_obj_list[ind].cf_ant5 += x[19]; //pg
                                                             //DRBE_obj_list[ind].cf_ant5 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_ant5 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_ant5 += x[4];  //cor
                        DRBE_obj_list[ind].mf_ant5 += x[8];  //nr
                        DRBE_obj_list[ind].mf_ant5 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_ant5 += x[16]; //ant
                        DRBE_obj_list[ind].mf_ant5 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_ant5 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_ant5 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_ant5 += x[5];  //cor
                        DRBE_obj_list[ind].bf_ant5 += x[9];  //nr
                        DRBE_obj_list[ind].bf_ant5 += x[13]; //ori
                        DRBE_obj_list[ind].bf_ant5 += x[17]; //ant
                        DRBE_obj_list[ind].bf_ant5 += x[21]; //pg
                                                             //DRBE_obj_list[ind].bf_ant5 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_ant5 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ant 5
                    case 6:

                        //DRBE_obj_list[ind].cp_ant_na_n += x[3];  //cor
                        DRBE_obj_list[ind].cp_ant_na_n += x[7];  //nr
                        DRBE_obj_list[ind].cp_ant_na_n += x[11]; //ori
                        DRBE_obj_list[ind].cp_ant_na_n += x[15]; //ant
                        DRBE_obj_list[ind].cp_ant_na_n += x[19]; //pg
                                                                 //DRBE_obj_list[ind].cp_ant_na_n += x[23]; //rcs
                        DRBE_obj_list[ind].cp_ant_na_n += x[27]; //tu

                        //DRBE_obj_list[ind].mp_ant_na_n += x[4];  //cor
                        DRBE_obj_list[ind].mp_ant_na_n += x[8];  //nr
                        DRBE_obj_list[ind].mp_ant_na_n += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_ant_na_n += x[16]; //ant
                        DRBE_obj_list[ind].mp_ant_na_n += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_ant_na_n += x[24]; //rcs
                        DRBE_obj_list[ind].mp_ant_na_n += x[28]; //tu

                        //DRBE_obj_list[ind].bp_ant_na_n += x[5];  //cor
                        DRBE_obj_list[ind].bp_ant_na_n += x[9];  //nr
                        DRBE_obj_list[ind].bp_ant_na_n += x[13]; //ori
                        DRBE_obj_list[ind].bp_ant_na_n += x[17]; //ant
                        DRBE_obj_list[ind].bp_ant_na_n += x[21]; //pg
                                                                 //DRBE_obj_list[ind].bp_ant_na_n += x[25]; //rcs
                        DRBE_obj_list[ind].bp_ant_na_n += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //na l
                    case 7:

                        //DRBE_obj_list[ind].cp_ant_na_p += x[3];  //cor
                        DRBE_obj_list[ind].cp_ant_na_p += x[7];  //nr
                        DRBE_obj_list[ind].cp_ant_na_p += x[11]; //ori
                        DRBE_obj_list[ind].cp_ant_na_p += x[15]; //ant
                        DRBE_obj_list[ind].cp_ant_na_p += x[19]; //pg
                                                                 //DRBE_obj_list[ind].cp_ant_na_p += x[23]; //rcs
                        DRBE_obj_list[ind].cp_ant_na_p += x[27]; //tu

                        //DRBE_obj_list[ind].mp_ant_na_p += x[4];  //cor
                        DRBE_obj_list[ind].mp_ant_na_p += x[8];  //nr
                        DRBE_obj_list[ind].mp_ant_na_p += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_ant_na_p += x[16]; //ant
                        DRBE_obj_list[ind].mp_ant_na_p += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_ant_na_p += x[24]; //rcs
                        DRBE_obj_list[ind].mp_ant_na_p += x[28]; //tu

                        //DRBE_obj_list[ind].bp_ant_na_p += x[5];  //cor
                        DRBE_obj_list[ind].bp_ant_na_p += x[9];  //nr
                        DRBE_obj_list[ind].bp_ant_na_p += x[13]; //ori
                        DRBE_obj_list[ind].bp_ant_na_p += x[17]; //ant
                        DRBE_obj_list[ind].bp_ant_na_p += x[21]; //pg
                                                                 //DRBE_obj_list[ind].bp_ant_na_p += x[25]; //rcs
                        DRBE_obj_list[ind].bp_ant_na_p += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //na u
                    case 8:

                        //DRBE_obj_list[ind].cp_ant_ar_n += x[3];  //cor
                        DRBE_obj_list[ind].cp_ant_ar_n += x[7];  //nr
                        DRBE_obj_list[ind].cp_ant_ar_n += x[11]; //ori
                        DRBE_obj_list[ind].cp_ant_ar_n += x[15]; //ant
                        DRBE_obj_list[ind].cp_ant_ar_n += x[19]; //pg
                                                                 //DRBE_obj_list[ind].cp_ant_ar_n += x[23]; //rcs
                        DRBE_obj_list[ind].cp_ant_ar_n += x[27]; //tu

                        //DRBE_obj_list[ind].mp_ant_ar_n += x[4];  //cor
                        DRBE_obj_list[ind].mp_ant_ar_n += x[8];  //nr
                        DRBE_obj_list[ind].mp_ant_ar_n += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_ant_ar_n += x[16]; //ant
                        DRBE_obj_list[ind].mp_ant_ar_n += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_ant_ar_n += x[24]; //rcs
                        DRBE_obj_list[ind].mp_ant_ar_n += x[28]; //tu

                        //DRBE_obj_list[ind].bp_ant_ar_n += x[5];  //cor
                        DRBE_obj_list[ind].bp_ant_ar_n += x[9];  //nr
                        DRBE_obj_list[ind].bp_ant_ar_n += x[13]; //ori
                        DRBE_obj_list[ind].bp_ant_ar_n += x[17]; //ant
                        DRBE_obj_list[ind].bp_ant_ar_n += x[21]; //pg
                                                                 //DRBE_obj_list[ind].bp_ant_ar_n += x[25]; //rcs
                        DRBE_obj_list[ind].bp_ant_ar_n += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ar l
                    case 9:

                        //DRBE_obj_list[ind].cp_ant_ar_p += x[3];  //cor
                        DRBE_obj_list[ind].cp_ant_ar_p += x[7];  //nr
                        DRBE_obj_list[ind].cp_ant_ar_p += x[11]; //ori
                        DRBE_obj_list[ind].cp_ant_ar_p += x[15]; //ant
                        DRBE_obj_list[ind].cp_ant_ar_p += x[19]; //pg
                                                                 //DRBE_obj_list[ind].cp_ant_ar_p += x[23]; //rcs
                        DRBE_obj_list[ind].cp_ant_ar_p += x[27]; //tu

                        //DRBE_obj_list[ind].mp_ant_ar_p += x[4];  //cor
                        DRBE_obj_list[ind].mp_ant_ar_p += x[8];  //nr
                        DRBE_obj_list[ind].mp_ant_ar_p += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_ant_ar_p += x[16]; //ant
                        DRBE_obj_list[ind].mp_ant_ar_p += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_ant_ar_p += x[24]; //rcs
                        DRBE_obj_list[ind].mp_ant_ar_p += x[28]; //tu

                        //DRBE_obj_list[ind].bp_ant_ar_p += x[5];  //cor
                        DRBE_obj_list[ind].bp_ant_ar_p += x[9];  //nr
                        DRBE_obj_list[ind].bp_ant_ar_p += x[13]; //ori
                        DRBE_obj_list[ind].bp_ant_ar_p += x[17]; //ant
                        DRBE_obj_list[ind].bp_ant_ar_p += x[21]; //pg
                                                                 //DRBE_obj_list[ind].bp_ant_ar_p += x[25]; //rcs
                        DRBE_obj_list[ind].bp_ant_ar_p += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ar u
                    case 10:

                        //DRBE_obj_list[ind].cp_ant_ds_n += x[3];  //cor
                        DRBE_obj_list[ind].cp_ant_ds_n += x[7];  //nr
                        DRBE_obj_list[ind].cp_ant_ds_n += x[11]; //ori
                        DRBE_obj_list[ind].cp_ant_ds_n += x[15]; //ant
                        DRBE_obj_list[ind].cp_ant_ds_n += x[19]; //pg
                                                                 //DRBE_obj_list[ind].cp_ant_ds_n += x[23]; //rcs
                        DRBE_obj_list[ind].cp_ant_ds_n += x[27]; //tu

                        //DRBE_obj_list[ind].mp_ant_ds_n += x[4];  //cor
                        DRBE_obj_list[ind].mp_ant_ds_n += x[8];  //nr
                        DRBE_obj_list[ind].mp_ant_ds_n += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_ant_ds_n += x[16]; //ant
                        DRBE_obj_list[ind].mp_ant_ds_n += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_ant_ds_n += x[24]; //rcs
                        DRBE_obj_list[ind].mp_ant_ds_n += x[28]; //tu

                        //DRBE_obj_list[ind].bp_ant_ds_n += x[5];  //cor
                        DRBE_obj_list[ind].bp_ant_ds_n += x[9];  //nr
                        DRBE_obj_list[ind].bp_ant_ds_n += x[13]; //ori
                        DRBE_obj_list[ind].bp_ant_ds_n += x[17]; //ant
                        DRBE_obj_list[ind].bp_ant_ds_n += x[21]; //pg
                                                                 //DRBE_obj_list[ind].bp_ant_ds_n += x[25]; //rcs
                        DRBE_obj_list[ind].bp_ant_ds_n += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ds l
                    case 11:

                        //DRBE_obj_list[ind].cp_ant_ds_p += x[3];  //cor
                        DRBE_obj_list[ind].cp_ant_ds_p += x[7];  //nr
                        DRBE_obj_list[ind].cp_ant_ds_p += x[11]; //ori
                        DRBE_obj_list[ind].cp_ant_ds_p += x[15]; //ant
                        DRBE_obj_list[ind].cp_ant_ds_p += x[19]; //pg
                                                                 //DRBE_obj_list[ind].cp_ant_ds_p += x[23]; //rcs
                        DRBE_obj_list[ind].cp_ant_ds_p += x[27]; //tu

                        //DRBE_obj_list[ind].mp_ant_ds_p += x[4];  //cor
                        DRBE_obj_list[ind].mp_ant_ds_p += x[8];  //nr
                        DRBE_obj_list[ind].mp_ant_ds_p += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_ant_ds_p += x[16]; //ant
                        DRBE_obj_list[ind].mp_ant_ds_p += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_ant_ds_p += x[24]; //rcs
                        DRBE_obj_list[ind].mp_ant_ds_p += x[28]; //tu

                        //DRBE_obj_list[ind].bp_ant_ds_p += x[5];  //cor
                        DRBE_obj_list[ind].bp_ant_ds_p += x[9];  //nr
                        DRBE_obj_list[ind].bp_ant_ds_p += x[13]; //ori
                        DRBE_obj_list[ind].bp_ant_ds_p += x[17]; //ant
                        DRBE_obj_list[ind].bp_ant_ds_p += x[21]; //pg
                                                                 //DRBE_obj_list[ind].bp_ant_ds_p += x[25]; //rcs
                        DRBE_obj_list[ind].bp_ant_ds_p += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ds u
                    case 12:

                        //DRBE_obj_list[ind].cf_rcs0 += x[3];  //cor
                        DRBE_obj_list[ind].cf_rcs0 += x[7];  //nr
                        DRBE_obj_list[ind].cf_rcs0 += x[11]; //ori
                        DRBE_obj_list[ind].cf_rcs0 += x[15]; //ant
                        DRBE_obj_list[ind].cf_rcs0 += x[19]; //pg
                                                             //DRBE_obj_list[ind].cf_rcs0 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_rcs0 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_rcs0 += x[4];  //cor
                        DRBE_obj_list[ind].mf_rcs0 += x[8];  //nr
                        DRBE_obj_list[ind].mf_rcs0 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_rcs0 += x[16]; //ant
                        DRBE_obj_list[ind].mf_rcs0 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_rcs0 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_rcs0 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_rcs0 += x[5];  //cor
                        DRBE_obj_list[ind].bf_rcs0 += x[9];  //nr
                        DRBE_obj_list[ind].bf_rcs0 += x[13]; //ori
                        DRBE_obj_list[ind].bf_rcs0 += x[17]; //ant
                        DRBE_obj_list[ind].bf_rcs0 += x[21]; //pg
                                                             //DRBE_obj_list[ind].bf_rcs0 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_rcs0 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //rcs 0
                    case 13:

                        //DRBE_obj_list[ind].cf_rcs1 += x[3];  //cor
                        DRBE_obj_list[ind].cf_rcs1 += x[7];  //nr
                        DRBE_obj_list[ind].cf_rcs1 += x[11]; //ori
                        DRBE_obj_list[ind].cf_rcs1 += x[15]; //ant
                        DRBE_obj_list[ind].cf_rcs1 += x[19]; //pg
                                                             //DRBE_obj_list[ind].cf_rcs1 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_rcs1 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_rcs1 += x[4];  //cor
                        DRBE_obj_list[ind].mf_rcs1 += x[8];  //nr
                        DRBE_obj_list[ind].mf_rcs1 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_rcs1 += x[16]; //ant
                        DRBE_obj_list[ind].mf_rcs1 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_rcs1 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_rcs1 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_rcs1 += x[5];  //cor
                        DRBE_obj_list[ind].bf_rcs1 += x[9];  //nr
                        DRBE_obj_list[ind].bf_rcs1 += x[13]; //ori
                        DRBE_obj_list[ind].bf_rcs1 += x[17]; //ant
                        DRBE_obj_list[ind].bf_rcs1 += x[21]; //pg
                                                             //DRBE_obj_list[ind].bf_rcs1 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_rcs1 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //rcs 1
                    case 14:

                        //DRBE_obj_list[ind].cf_rcs2 += x[3];  //cor
                        DRBE_obj_list[ind].cf_rcs2 += x[7];  //nr
                        DRBE_obj_list[ind].cf_rcs2 += x[11]; //ori
                        DRBE_obj_list[ind].cf_rcs2 += x[15]; //ant
                        DRBE_obj_list[ind].cf_rcs2 += x[19]; //pg
                                                             //DRBE_obj_list[ind].cf_rcs2 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_rcs2 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_rcs2 += x[4];  //cor
                        DRBE_obj_list[ind].mf_rcs2 += x[8];  //nr
                        DRBE_obj_list[ind].mf_rcs2 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_rcs2 += x[16]; //ant
                        DRBE_obj_list[ind].mf_rcs2 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_rcs2 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_rcs2 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_rcs2 += x[5];  //cor
                        DRBE_obj_list[ind].bf_rcs2 += x[9];  //nr
                        DRBE_obj_list[ind].bf_rcs2 += x[13]; //ori
                        DRBE_obj_list[ind].bf_rcs2 += x[17]; //ant
                        DRBE_obj_list[ind].bf_rcs2 += x[21]; //pg
                                                             //DRBE_obj_list[ind].bf_rcs2 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_rcs2 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //rcs 2
                    case 15:

                        //DRBE_obj_list[ind].cf_rcs3 += x[3];  //cor
                        DRBE_obj_list[ind].cf_rcs3 += x[7];  //nr
                        DRBE_obj_list[ind].cf_rcs3 += x[11]; //ori
                        DRBE_obj_list[ind].cf_rcs3 += x[15]; //ant
                        DRBE_obj_list[ind].cf_rcs3 += x[19]; //pg
                                                             //DRBE_obj_list[ind].cf_rcs3 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_rcs3 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_rcs3 += x[4];  //cor
                        DRBE_obj_list[ind].mf_rcs3 += x[8];  //nr
                        DRBE_obj_list[ind].mf_rcs3 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_rcs3 += x[16]; //ant
                        DRBE_obj_list[ind].mf_rcs3 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_rcs3 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_rcs3 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_rcs3 += x[5];  //cor
                        DRBE_obj_list[ind].bf_rcs3 += x[9];  //nr
                        DRBE_obj_list[ind].bf_rcs3 += x[13]; //ori
                        DRBE_obj_list[ind].bf_rcs3 += x[17]; //ant
                        DRBE_obj_list[ind].bf_rcs3 += x[21]; //pg
                                                             //DRBE_obj_list[ind].bf_rcs3 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_rcs3 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //rcs 3
                    case 16:

                        //DRBE_obj_list[ind].cf_rcs4 += x[3];  //cor
                        DRBE_obj_list[ind].cf_rcs4 += x[7];  //nr
                        DRBE_obj_list[ind].cf_rcs4 += x[11]; //ori
                        DRBE_obj_list[ind].cf_rcs4 += x[15]; //ant
                        DRBE_obj_list[ind].cf_rcs4 += x[19]; //pg
                                                             //DRBE_obj_list[ind].cf_rcs4 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_rcs4 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_rcs4 += x[4];  //cor
                        DRBE_obj_list[ind].mf_rcs4 += x[8];  //nr
                        DRBE_obj_list[ind].mf_rcs4 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_rcs4 += x[16]; //ant
                        DRBE_obj_list[ind].mf_rcs4 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_rcs4 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_rcs4 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_rcs4 += x[5];  //cor
                        DRBE_obj_list[ind].bf_rcs4 += x[9];  //nr
                        DRBE_obj_list[ind].bf_rcs4 += x[13]; //ori
                        DRBE_obj_list[ind].bf_rcs4 += x[17]; //ant
                        DRBE_obj_list[ind].bf_rcs4 += x[21]; //pg
                                                             //DRBE_obj_list[ind].bf_rcs4 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_rcs4 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //rcs 4
                    case 17:

                        //DRBE_obj_list[ind].cf_rcs5 += x[3];  //cor
                        DRBE_obj_list[ind].cf_rcs5 += x[7];  //nr
                        DRBE_obj_list[ind].cf_rcs5 += x[11]; //ori
                        DRBE_obj_list[ind].cf_rcs5 += x[15]; //ant
                        DRBE_obj_list[ind].cf_rcs5 += x[19]; //pg
                                                             //DRBE_obj_list[ind].cf_rcs5 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_rcs5 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_rcs5 += x[4];  //cor
                        DRBE_obj_list[ind].mf_rcs5 += x[8];  //nr
                        DRBE_obj_list[ind].mf_rcs5 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_rcs5 += x[16]; //ant
                        DRBE_obj_list[ind].mf_rcs5 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_rcs5 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_rcs5 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_rcs5 += x[5];  //cor
                        DRBE_obj_list[ind].bf_rcs5 += x[9];  //nr
                        DRBE_obj_list[ind].bf_rcs5 += x[13]; //ori
                        DRBE_obj_list[ind].bf_rcs5 += x[17]; //ant
                        DRBE_obj_list[ind].bf_rcs5 += x[21]; //pg
                                                             //DRBE_obj_list[ind].bf_rcs5 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_rcs5 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //rcs 5
                    case 18:

                        //DRBE_obj_list[ind].cf_rcs6 += x[3];  //cor
                        DRBE_obj_list[ind].cf_rcs6 += x[7];  //nr
                        DRBE_obj_list[ind].cf_rcs6 += x[11]; //ori
                        DRBE_obj_list[ind].cf_rcs6 += x[15]; //ant
                        DRBE_obj_list[ind].cf_rcs6 += x[19]; //pg
                                                             //DRBE_obj_list[ind].cf_rcs6 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_rcs6 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_rcs6 += x[4];  //cor
                        DRBE_obj_list[ind].mf_rcs6 += x[8];  //nr
                        DRBE_obj_list[ind].mf_rcs6 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_rcs6 += x[16]; //ant
                        DRBE_obj_list[ind].mf_rcs6 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_rcs6 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_rcs6 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_rcs6 += x[5];  //cor
                        DRBE_obj_list[ind].bf_rcs6 += x[9];  //nr
                        DRBE_obj_list[ind].bf_rcs6 += x[13]; //ori
                        DRBE_obj_list[ind].bf_rcs6 += x[17]; //ant
                        DRBE_obj_list[ind].bf_rcs6 += x[21]; //pg
                                                             //DRBE_obj_list[ind].bf_rcs6 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_rcs6 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //rcs 6
                    case 19:

                        //DRBE_obj_list[ind].cp_rcs_pn_n += x[3];  //cor
                        DRBE_obj_list[ind].cp_rcs_pn_n += x[7];  //nr
                        DRBE_obj_list[ind].cp_rcs_pn_n += x[11]; //ori
                        DRBE_obj_list[ind].cp_rcs_pn_n += x[15]; //ant
                        DRBE_obj_list[ind].cp_rcs_pn_n += x[19]; //pg
                                                                 //DRBE_obj_list[ind].cp_rcs_pn_n += x[23]; //rcs
                        DRBE_obj_list[ind].cp_rcs_pn_n += x[27]; //tu

                        //DRBE_obj_list[ind].mp_rcs_pn_n += x[4];  //cor
                        DRBE_obj_list[ind].mp_rcs_pn_n += x[8];  //nr
                        DRBE_obj_list[ind].mp_rcs_pn_n += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_rcs_pn_n += x[16]; //ant
                        DRBE_obj_list[ind].mp_rcs_pn_n += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_rcs_pn_n += x[24]; //rcs
                        DRBE_obj_list[ind].mp_rcs_pn_n += x[28]; //tu

                        //DRBE_obj_list[ind].bp_rcs_pn_n += x[5];  //cor
                        DRBE_obj_list[ind].bp_rcs_pn_n += x[9];  //nr
                        DRBE_obj_list[ind].bp_rcs_pn_n += x[13]; //ori
                        DRBE_obj_list[ind].bp_rcs_pn_n += x[17]; //ant
                        DRBE_obj_list[ind].bp_rcs_pn_n += x[21]; //pg
                                                                 //DRBE_obj_list[ind].bp_rcs_pn_n += x[25]; //rcs
                        DRBE_obj_list[ind].bp_rcs_pn_n += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //pn l
                    case 20:

                        //DRBE_obj_list[ind].cp_rcs_pn_p += x[3];  //cor
                        DRBE_obj_list[ind].cp_rcs_pn_p += x[7];  //nr
                        DRBE_obj_list[ind].cp_rcs_pn_p += x[11]; //ori
                        DRBE_obj_list[ind].cp_rcs_pn_p += x[15]; //ant
                        DRBE_obj_list[ind].cp_rcs_pn_p += x[19]; //pg
                                                                 //DRBE_obj_list[ind].cp_rcs_pn_p += x[23]; //rcs
                        DRBE_obj_list[ind].cp_rcs_pn_p += x[27]; //tu

                        //DRBE_obj_list[ind].mp_rcs_pn_p += x[4];  //cor
                        DRBE_obj_list[ind].mp_rcs_pn_p += x[8];  //nr
                        DRBE_obj_list[ind].mp_rcs_pn_p += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_rcs_pn_p += x[16]; //ant
                        DRBE_obj_list[ind].mp_rcs_pn_p += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_rcs_pn_p += x[24]; //rcs
                        DRBE_obj_list[ind].mp_rcs_pn_p += x[28]; //tu

                        //DRBE_obj_list[ind].bp_rcs_pn_p += x[5];  //cor
                        DRBE_obj_list[ind].bp_rcs_pn_p += x[9];  //nr
                        DRBE_obj_list[ind].bp_rcs_pn_p += x[13]; //ori
                        DRBE_obj_list[ind].bp_rcs_pn_p += x[17]; //ant
                        DRBE_obj_list[ind].bp_rcs_pn_p += x[21]; //pg
                                                                 //DRBE_obj_list[ind].bp_rcs_pn_p += x[25]; //rcs
                        DRBE_obj_list[ind].bp_rcs_pn_p += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //pn u
                    case 21:

                        //DRBE_obj_list[ind].cp_rcs_ss_n += x[3];  //cor
                        DRBE_obj_list[ind].cp_rcs_ss_n += x[7];  //nr
                        DRBE_obj_list[ind].cp_rcs_ss_n += x[11]; //ori
                        DRBE_obj_list[ind].cp_rcs_ss_n += x[15]; //ant
                        DRBE_obj_list[ind].cp_rcs_ss_n += x[19]; //pg
                                                                 //DRBE_obj_list[ind].cp_rcs_ss_n += x[23]; //rcs
                        DRBE_obj_list[ind].cp_rcs_ss_n += x[27]; //tu

                        //DRBE_obj_list[ind].mp_rcs_ss_n += x[4];  //cor
                        DRBE_obj_list[ind].mp_rcs_ss_n += x[8];  //nr
                        DRBE_obj_list[ind].mp_rcs_ss_n += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_rcs_ss_n += x[16]; //ant
                        DRBE_obj_list[ind].mp_rcs_ss_n += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_rcs_ss_n += x[24]; //rcs
                        DRBE_obj_list[ind].mp_rcs_ss_n += x[28]; //tu

                        //DRBE_obj_list[ind].bp_rcs_ss_n += x[5];  //cor
                        DRBE_obj_list[ind].bp_rcs_ss_n += x[9];  //nr
                        DRBE_obj_list[ind].bp_rcs_ss_n += x[13]; //ori
                        DRBE_obj_list[ind].bp_rcs_ss_n += x[17]; //ant
                        DRBE_obj_list[ind].bp_rcs_ss_n += x[21]; //pg
                                                                 //DRBE_obj_list[ind].bp_rcs_ss_n += x[25]; //rcs
                        DRBE_obj_list[ind].bp_rcs_ss_n += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ss l
                    case 22:

                        //DRBE_obj_list[ind].cp_rcs_ss_p += x[3];  //cor
                        DRBE_obj_list[ind].cp_rcs_ss_p += x[7];  //nr
                        DRBE_obj_list[ind].cp_rcs_ss_p += x[11]; //ori
                        DRBE_obj_list[ind].cp_rcs_ss_p += x[15]; //ant
                        DRBE_obj_list[ind].cp_rcs_ss_p += x[19]; //pg
                                                                 //DRBE_obj_list[ind].cp_rcs_ss_p += x[23]; //rcs
                        DRBE_obj_list[ind].cp_rcs_ss_p += x[27]; //tu

                        //DRBE_obj_list[ind].mp_rcs_ss_p += x[4];  //cor
                        DRBE_obj_list[ind].mp_rcs_ss_p += x[8];  //nr
                        DRBE_obj_list[ind].mp_rcs_ss_p += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_rcs_ss_p += x[16]; //ant
                        DRBE_obj_list[ind].mp_rcs_ss_p += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_rcs_ss_p += x[24]; //rcs
                        DRBE_obj_list[ind].mp_rcs_ss_p += x[28]; //tu

                        //DRBE_obj_list[ind].bp_rcs_ss_p += x[5];  //cor
                        DRBE_obj_list[ind].bp_rcs_ss_p += x[9];  //nr
                        DRBE_obj_list[ind].bp_rcs_ss_p += x[13]; //ori
                        DRBE_obj_list[ind].bp_rcs_ss_p += x[17]; //ant
                        DRBE_obj_list[ind].bp_rcs_ss_p += x[21]; //pg
                                                                 //DRBE_obj_list[ind].bp_rcs_ss_p += x[25]; //rcs
                        DRBE_obj_list[ind].bp_rcs_ss_p += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ss u
                    case 23:

                        //DRBE_obj_list[ind].cp_rcs_sp_n += x[3];  //cor
                        DRBE_obj_list[ind].cp_rcs_sp_n += x[7];  //nr
                        DRBE_obj_list[ind].cp_rcs_sp_n += x[11]; //ori
                        DRBE_obj_list[ind].cp_rcs_sp_n += x[15]; //ant
                        DRBE_obj_list[ind].cp_rcs_sp_n += x[19]; //pg
                                                                 //DRBE_obj_list[ind].cp_rcs_sp_n += x[23]; //rcs
                        DRBE_obj_list[ind].cp_rcs_sp_n += x[27]; //tu

                        //DRBE_obj_list[ind].mp_rcs_sp_n += x[4];  //cor
                        DRBE_obj_list[ind].mp_rcs_sp_n += x[8];  //nr
                        DRBE_obj_list[ind].mp_rcs_sp_n += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_rcs_sp_n += x[16]; //ant
                        DRBE_obj_list[ind].mp_rcs_sp_n += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_rcs_sp_n += x[24]; //rcs
                        DRBE_obj_list[ind].mp_rcs_sp_n += x[28]; //tu

                        //DRBE_obj_list[ind].bp_rcs_sp_n += x[5];  //cor
                        DRBE_obj_list[ind].bp_rcs_sp_n += x[9];  //nr
                        DRBE_obj_list[ind].bp_rcs_sp_n += x[13]; //ori
                        DRBE_obj_list[ind].bp_rcs_sp_n += x[17]; //ant
                        DRBE_obj_list[ind].bp_rcs_sp_n += x[21]; //pg
                                                                 //DRBE_obj_list[ind].bp_rcs_sp_n += x[25]; //rcs
                        DRBE_obj_list[ind].bp_rcs_sp_n += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //sp l
                    case 24:

                        //DRBE_obj_list[ind].cp_rcs_sp_p += x[3];  //cor
                        DRBE_obj_list[ind].cp_rcs_sp_p += x[7];  //nr
                        DRBE_obj_list[ind].cp_rcs_sp_p += x[11]; //ori
                        DRBE_obj_list[ind].cp_rcs_sp_p += x[15]; //ant
                        DRBE_obj_list[ind].cp_rcs_sp_p += x[19]; //pg
                                                                 //DRBE_obj_list[ind].cp_rcs_sp_p += x[23]; //rcs
                        DRBE_obj_list[ind].cp_rcs_sp_p += x[27]; //tu

                        //DRBE_obj_list[ind].mp_rcs_sp_p += x[4];  //cor
                        DRBE_obj_list[ind].mp_rcs_sp_p += x[8];  //nr
                        DRBE_obj_list[ind].mp_rcs_sp_p += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_rcs_sp_p += x[16]; //ant
                        DRBE_obj_list[ind].mp_rcs_sp_p += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_rcs_sp_p += x[24]; //rcs
                        DRBE_obj_list[ind].mp_rcs_sp_p += x[28]; //tu

                        //DRBE_obj_list[ind].bp_rcs_sp_p += x[5];  //cor
                        DRBE_obj_list[ind].bp_rcs_sp_p += x[9];  //nr
                        DRBE_obj_list[ind].bp_rcs_sp_p += x[13]; //ori
                        DRBE_obj_list[ind].bp_rcs_sp_p += x[17]; //ant
                        DRBE_obj_list[ind].bp_rcs_sp_p += x[21]; //pg
                                                                 //DRBE_obj_list[ind].bp_rcs_sp_p += x[25]; //rcs
                        DRBE_obj_list[ind].bp_rcs_sp_p += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //sp u
                    case 25:

                        //DRBE_obj_list[ind].cp_rcs_ar_n += x[3];  //cor
                        DRBE_obj_list[ind].cp_rcs_ar_n += x[7];  //nr
                        DRBE_obj_list[ind].cp_rcs_ar_n += x[11]; //ori
                        DRBE_obj_list[ind].cp_rcs_ar_n += x[15]; //ant
                        DRBE_obj_list[ind].cp_rcs_ar_n += x[19]; //pg
                                                                 //DRBE_obj_list[ind].cp_rcs_ar_n += x[23]; //rcs
                        DRBE_obj_list[ind].cp_rcs_ar_n += x[27]; //tu

                        //DRBE_obj_list[ind].mp_rcs_ar_n += x[4];  //cor
                        DRBE_obj_list[ind].mp_rcs_ar_n += x[8];  //nr
                        DRBE_obj_list[ind].mp_rcs_ar_n += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_rcs_ar_n += x[16]; //ant
                        DRBE_obj_list[ind].mp_rcs_ar_n += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_rcs_ar_n += x[24]; //rcs
                        DRBE_obj_list[ind].mp_rcs_ar_n += x[28]; //tu

                        //DRBE_obj_list[ind].bp_rcs_ar_n += x[5];  //cor
                        DRBE_obj_list[ind].bp_rcs_ar_n += x[9];  //nr
                        DRBE_obj_list[ind].bp_rcs_ar_n += x[13]; //ori
                        DRBE_obj_list[ind].bp_rcs_ar_n += x[17]; //ant
                        DRBE_obj_list[ind].bp_rcs_ar_n += x[21]; //pg
                                                                 //DRBE_obj_list[ind].bp_rcs_ar_n += x[25]; //rcs
                        DRBE_obj_list[ind].bp_rcs_ar_n += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ar l
                    case 26:

                        //DRBE_obj_list[ind].cp_rcs_ar_p += x[3];  //cor
                        DRBE_obj_list[ind].cp_rcs_ar_p += x[7];  //nr
                        DRBE_obj_list[ind].cp_rcs_ar_p += x[11]; //ori
                        DRBE_obj_list[ind].cp_rcs_ar_p += x[15]; //ant
                        DRBE_obj_list[ind].cp_rcs_ar_p += x[19]; //pg
                                                                 //DRBE_obj_list[ind].cp_rcs_ar_p += x[23]; //rcs
                        DRBE_obj_list[ind].cp_rcs_ar_p += x[27]; //tu

                        //DRBE_obj_list[ind].mp_rcs_ar_p += x[4];  //cor
                        DRBE_obj_list[ind].mp_rcs_ar_p += x[8];  //nr
                        DRBE_obj_list[ind].mp_rcs_ar_p += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_rcs_ar_p += x[16]; //ant
                        DRBE_obj_list[ind].mp_rcs_ar_p += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_rcs_ar_p += x[24]; //rcs
                        DRBE_obj_list[ind].mp_rcs_ar_p += x[28]; //tu

                        //DRBE_obj_list[ind].bp_rcs_ar_p += x[5];  //cor
                        DRBE_obj_list[ind].bp_rcs_ar_p += x[9];  //nr
                        DRBE_obj_list[ind].bp_rcs_ar_p += x[13]; //ori
                        DRBE_obj_list[ind].bp_rcs_ar_p += x[17]; //ant
                        DRBE_obj_list[ind].bp_rcs_ar_p += x[21]; //pg
                                                                 //DRBE_obj_list[ind].bp_rcs_ar_p += x[25]; //rcs
                        DRBE_obj_list[ind].bp_rcs_ar_p += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ar u
                    case 27:

                        //DRBE_obj_list[ind].cp_rcs_fb_n += x[3];  //cor
                        DRBE_obj_list[ind].cp_rcs_fb_n += x[7];  //nr
                        DRBE_obj_list[ind].cp_rcs_fb_n += x[11]; //ori
                        DRBE_obj_list[ind].cp_rcs_fb_n += x[15]; //ant
                        DRBE_obj_list[ind].cp_rcs_fb_n += x[19]; //pg
                                                                 //DRBE_obj_list[ind].cp_rcs_fb_n += x[23]; //rcs
                        DRBE_obj_list[ind].cp_rcs_fb_n += x[27]; //tu

                        //DRBE_obj_list[ind].mp_rcs_fb_n += x[4];  //cor
                        DRBE_obj_list[ind].mp_rcs_fb_n += x[8];  //nr
                        DRBE_obj_list[ind].mp_rcs_fb_n += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_rcs_fb_n += x[16]; //ant
                        DRBE_obj_list[ind].mp_rcs_fb_n += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_rcs_fb_n += x[24]; //rcs
                        DRBE_obj_list[ind].mp_rcs_fb_n += x[28]; //tu

                        //DRBE_obj_list[ind].bp_rcs_fb_n += x[5];  //cor
                        DRBE_obj_list[ind].bp_rcs_fb_n += x[9];  //nr
                        DRBE_obj_list[ind].bp_rcs_fb_n += x[13]; //ori
                        DRBE_obj_list[ind].bp_rcs_fb_n += x[17]; //ant
                        DRBE_obj_list[ind].bp_rcs_fb_n += x[21]; //pg
                                                                 //DRBE_obj_list[ind].bp_rcs_fb_n += x[25]; //rcs
                        DRBE_obj_list[ind].bp_rcs_fb_n += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break;
                    //fb l
                    case 28:

                        //DRBE_obj_list[ind].cp_rcs_fb_p += x[3];  //cor
                        DRBE_obj_list[ind].cp_rcs_fb_p += x[7];  //nr
                        DRBE_obj_list[ind].cp_rcs_fb_p += x[11]; //ori
                        DRBE_obj_list[ind].cp_rcs_fb_p += x[15]; //ant
                        DRBE_obj_list[ind].cp_rcs_fb_p += x[19]; //pg
                                                                 //DRBE_obj_list[ind].cp_rcs_fb_p += x[23]; //rcs
                        DRBE_obj_list[ind].cp_rcs_fb_p += x[27]; //tu

                        //DRBE_obj_list[ind].mp_rcs_fb_p += x[4];  //cor
                        DRBE_obj_list[ind].mp_rcs_fb_p += x[8];  //nr
                        DRBE_obj_list[ind].mp_rcs_fb_p += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_rcs_fb_p += x[16]; //ant
                        DRBE_obj_list[ind].mp_rcs_fb_p += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_rcs_fb_p += x[24]; //rcs
                        DRBE_obj_list[ind].mp_rcs_fb_p += x[28]; //tu

                        //DRBE_obj_list[ind].bp_rcs_fb_p += x[5];  //cor
                        DRBE_obj_list[ind].bp_rcs_fb_p += x[9];  //nr
                        DRBE_obj_list[ind].bp_rcs_fb_p += x[13]; //ori
                        DRBE_obj_list[ind].bp_rcs_fb_p += x[17]; //ant
                        DRBE_obj_list[ind].bp_rcs_fb_p += x[21]; //pg
                                                                 //DRBE_obj_list[ind].bp_rcs_fb_p += x[25]; //rcs
                        DRBE_obj_list[ind].bp_rcs_fb_p += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                        DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                                                                                                                    // DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break;
                    default:
                        await ShowDialog("Invalid Scan Update", "Invalide Variable number");
                        break;

                        //fb u
                }
            }
            else if (mode == 1)
            {
                switch (pind)
                {
                    case 0://ant 0

                        //DRBE_obj_list[ind].cf_ant0 += x[3];  //cor
                        DRBE_obj_list[ind].cf_ant0 += x[7];  //nr
                        DRBE_obj_list[ind].cf_ant0 += x[11]; //ori
                                                             //DRBE_obj_list[ind].cf_ant0 += x[15]; //ant
                        DRBE_obj_list[ind].cf_ant0 += x[19]; //pg
                        DRBE_obj_list[ind].cf_ant0 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_ant0 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_ant0 += x[4];  //cor
                        DRBE_obj_list[ind].mf_ant0 += x[8];  //nr
                        DRBE_obj_list[ind].mf_ant0 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_ant0 += x[16]; //ant
                        DRBE_obj_list[ind].mf_ant0 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_ant0 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_ant0 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_ant0 += x[5];  //cor
                        DRBE_obj_list[ind].bf_ant0 += x[9];  //nr
                        DRBE_obj_list[ind].bf_ant0 += x[13]; //ori
                                                             //DRBE_obj_list[ind].bf_ant0 += x[17]; //ant
                        DRBE_obj_list[ind].bf_ant0 += x[21]; //pg
                        DRBE_obj_list[ind].bf_ant0 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_ant0 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu

                        break; //ant 0

                    case 1:

                        //DRBE_obj_list[ind].cf_ant1 += x[3];  //cor
                        DRBE_obj_list[ind].cf_ant1 += x[7];  //nr
                        DRBE_obj_list[ind].cf_ant1 += x[11]; //ori
                                                             //DRBE_obj_list[ind].cf_ant1 += x[15]; //ant
                        DRBE_obj_list[ind].cf_ant1 += x[19]; //pg
                        DRBE_obj_list[ind].cf_ant1 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_ant1 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_ant1 += x[4];  //cor
                        DRBE_obj_list[ind].mf_ant1 += x[8];  //nr
                        DRBE_obj_list[ind].mf_ant1 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_ant1 += x[16]; //ant
                        DRBE_obj_list[ind].mf_ant1 += x[20]; //pg
                                                             //DRBE_obj_list[ind].mf_ant1 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_ant1 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_ant1 += x[5];  //cor
                        DRBE_obj_list[ind].bf_ant1 += x[9];  //nr
                        DRBE_obj_list[ind].bf_ant1 += x[13]; //ori
                                                             //DRBE_obj_list[ind].bf_ant1 += x[17]; //ant
                        DRBE_obj_list[ind].bf_ant1 += x[21]; //pg
                        DRBE_obj_list[ind].bf_ant1 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_ant1 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ant 1

                    case 2:

                        //DRBE_obj_list[ind].cf_ant2 += x[3];  //cor
                        DRBE_obj_list[ind].cf_ant2 += x[7];  //nr
                        DRBE_obj_list[ind].cf_ant2 += x[11]; //ori
                                                             //DRBE_obj_list[ind].cf_ant2 += x[15]; //ant
                        DRBE_obj_list[ind].cf_ant2 += x[19]; //pg
                        DRBE_obj_list[ind].cf_ant2 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_ant2 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_ant2 += x[4];  //cor
                        DRBE_obj_list[ind].mf_ant2 += x[8];  //nr
                        DRBE_obj_list[ind].mf_ant2 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_ant2 += x[16]; //ant
                        DRBE_obj_list[ind].mf_ant2 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_ant2 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_ant2 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_ant2 += x[5];  //cor
                        DRBE_obj_list[ind].bf_ant2 += x[9];  //nr
                        DRBE_obj_list[ind].bf_ant2 += x[13]; //ori
                                                             //DRBE_obj_list[ind].bf_ant2 += x[17]; //ant
                        DRBE_obj_list[ind].bf_ant2 += x[21]; //pg
                        DRBE_obj_list[ind].bf_ant2 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_ant2 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ant 2
                    case 3:

                        //DRBE_obj_list[ind].cf_ant3 += x[3];  //cor
                        DRBE_obj_list[ind].cf_ant3 += x[7];  //nr
                        DRBE_obj_list[ind].cf_ant3 += x[11]; //ori
                                                             //DRBE_obj_list[ind].cf_ant3 += x[15]; //ant
                        DRBE_obj_list[ind].cf_ant3 += x[19]; //pg
                        DRBE_obj_list[ind].cf_ant3 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_ant3 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_ant3 += x[4];  //cor
                        DRBE_obj_list[ind].mf_ant3 += x[8];  //nr
                        DRBE_obj_list[ind].mf_ant3 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_ant3 += x[16]; //ant
                        DRBE_obj_list[ind].mf_ant3 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_ant3 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_ant3 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_ant3 += x[5];  //cor
                        DRBE_obj_list[ind].bf_ant3 += x[9];  //nr
                        DRBE_obj_list[ind].bf_ant3 += x[13]; //ori
                                                             //DRBE_obj_list[ind].bf_ant3 += x[17]; //ant
                        DRBE_obj_list[ind].bf_ant3 += x[21]; //pg
                        DRBE_obj_list[ind].bf_ant3 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_ant3 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ant 3
                    case 4:

                        //DRBE_obj_list[ind].cf_ant4 += x[3];  //cor
                        DRBE_obj_list[ind].cf_ant4 += x[7];  //nr
                        DRBE_obj_list[ind].cf_ant4 += x[11]; //ori
                                                             //DRBE_obj_list[ind].cf_ant4 += x[15]; //ant
                        DRBE_obj_list[ind].cf_ant4 += x[19]; //pg
                        DRBE_obj_list[ind].cf_ant4 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_ant4 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_ant4 += x[4];  //cor
                        DRBE_obj_list[ind].mf_ant4 += x[8];  //nr
                        DRBE_obj_list[ind].mf_ant4 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_ant4 += x[16]; //ant
                        DRBE_obj_list[ind].mf_ant4 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_ant4 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_ant4 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_ant4 += x[5];  //cor
                        DRBE_obj_list[ind].bf_ant4 += x[9];  //nr
                        DRBE_obj_list[ind].bf_ant4 += x[13]; //ori
                                                             //DRBE_obj_list[ind].bf_ant4 += x[17]; //ant
                        DRBE_obj_list[ind].bf_ant4 += x[21]; //pg
                        DRBE_obj_list[ind].bf_ant4 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_ant4 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ant 4
                    case 5:

                        //DRBE_obj_list[ind].cf_ant5 += x[3];  //cor
                        DRBE_obj_list[ind].cf_ant5 += x[7];  //nr
                        DRBE_obj_list[ind].cf_ant5 += x[11]; //ori
                                                             //DRBE_obj_list[ind].cf_ant5 += x[15]; //ant
                        DRBE_obj_list[ind].cf_ant5 += x[19]; //pg
                        DRBE_obj_list[ind].cf_ant5 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_ant5 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_ant5 += x[4];  //cor
                        DRBE_obj_list[ind].mf_ant5 += x[8];  //nr
                        DRBE_obj_list[ind].mf_ant5 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_ant5 += x[16]; //ant
                        DRBE_obj_list[ind].mf_ant5 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_ant5 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_ant5 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_ant5 += x[5];  //cor
                        DRBE_obj_list[ind].bf_ant5 += x[9];  //nr
                        DRBE_obj_list[ind].bf_ant5 += x[13]; //ori
                                                             //DRBE_obj_list[ind].bf_ant5 += x[17]; //ant
                        DRBE_obj_list[ind].bf_ant5 += x[21]; //pg
                        DRBE_obj_list[ind].bf_ant5 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_ant5 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ant 5
                    case 6:

                        //DRBE_obj_list[ind].cp_ant_na_n += x[3];  //cor
                        DRBE_obj_list[ind].cp_ant_na_n += x[7];  //nr
                        DRBE_obj_list[ind].cp_ant_na_n += x[11]; //ori
                                                                 //DRBE_obj_list[ind].cp_ant_na_n += x[15]; //ant
                        DRBE_obj_list[ind].cp_ant_na_n += x[19]; //pg
                        DRBE_obj_list[ind].cp_ant_na_n += x[23]; //rcs
                        DRBE_obj_list[ind].cp_ant_na_n += x[27]; //tu

                        //DRBE_obj_list[ind].mp_ant_na_n += x[4];  //cor
                        DRBE_obj_list[ind].mp_ant_na_n += x[8];  //nr
                        DRBE_obj_list[ind].mp_ant_na_n += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_ant_na_n += x[16]; //ant
                        DRBE_obj_list[ind].mp_ant_na_n += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_ant_na_n += x[24]; //rcs
                        DRBE_obj_list[ind].mp_ant_na_n += x[28]; //tu

                        //DRBE_obj_list[ind].bp_ant_na_n += x[5];  //cor
                        DRBE_obj_list[ind].bp_ant_na_n += x[9];  //nr
                        DRBE_obj_list[ind].bp_ant_na_n += x[13]; //ori
                                                                 //DRBE_obj_list[ind].bp_ant_na_n += x[17]; //ant
                        DRBE_obj_list[ind].bp_ant_na_n += x[21]; //pg
                        DRBE_obj_list[ind].bp_ant_na_n += x[25]; //rcs
                        DRBE_obj_list[ind].bp_ant_na_n += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //na l
                    case 7:

                        //DRBE_obj_list[ind].cp_ant_na_p += x[3];  //cor
                        DRBE_obj_list[ind].cp_ant_na_p += x[7];  //nr
                        DRBE_obj_list[ind].cp_ant_na_p += x[11]; //ori
                                                                 //DRBE_obj_list[ind].cp_ant_na_p += x[15]; //ant
                        DRBE_obj_list[ind].cp_ant_na_p += x[19]; //pg
                        DRBE_obj_list[ind].cp_ant_na_p += x[23]; //rcs
                        DRBE_obj_list[ind].cp_ant_na_p += x[27]; //tu

                        //DRBE_obj_list[ind].mp_ant_na_p += x[4];  //cor
                        DRBE_obj_list[ind].mp_ant_na_p += x[8];  //nr
                        DRBE_obj_list[ind].mp_ant_na_p += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_ant_na_p += x[16]; //ant
                        DRBE_obj_list[ind].mp_ant_na_p += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_ant_na_p += x[24]; //rcs
                        DRBE_obj_list[ind].mp_ant_na_p += x[28]; //tu

                        //DRBE_obj_list[ind].bp_ant_na_p += x[5];  //cor
                        DRBE_obj_list[ind].bp_ant_na_p += x[9];  //nr
                        DRBE_obj_list[ind].bp_ant_na_p += x[13]; //ori
                                                                 //DRBE_obj_list[ind].bp_ant_na_p += x[17]; //ant
                        DRBE_obj_list[ind].bp_ant_na_p += x[21]; //pg
                        DRBE_obj_list[ind].bp_ant_na_p += x[25]; //rcs
                        DRBE_obj_list[ind].bp_ant_na_p += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //na u
                    case 8:

                        //DRBE_obj_list[ind].cp_ant_ar_n += x[3];  //cor
                        DRBE_obj_list[ind].cp_ant_ar_n += x[7];  //nr
                        DRBE_obj_list[ind].cp_ant_ar_n += x[11]; //ori
                                                                 //DRBE_obj_list[ind].cp_ant_ar_n += x[15]; //ant
                        DRBE_obj_list[ind].cp_ant_ar_n += x[19]; //pg
                        DRBE_obj_list[ind].cp_ant_ar_n += x[23]; //rcs
                        DRBE_obj_list[ind].cp_ant_ar_n += x[27]; //tu

                        //DRBE_obj_list[ind].mp_ant_ar_n += x[4];  //cor
                        DRBE_obj_list[ind].mp_ant_ar_n += x[8];  //nr
                        DRBE_obj_list[ind].mp_ant_ar_n += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_ant_ar_n += x[16]; //ant
                        DRBE_obj_list[ind].mp_ant_ar_n += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_ant_ar_n += x[24]; //rcs
                        DRBE_obj_list[ind].mp_ant_ar_n += x[28]; //tu

                        //DRBE_obj_list[ind].bp_ant_ar_n += x[5];  //cor
                        DRBE_obj_list[ind].bp_ant_ar_n += x[9];  //nr
                        DRBE_obj_list[ind].bp_ant_ar_n += x[13]; //ori
                                                                 //DRBE_obj_list[ind].bp_ant_ar_n += x[17]; //ant
                        DRBE_obj_list[ind].bp_ant_ar_n += x[21]; //pg
                        DRBE_obj_list[ind].bp_ant_ar_n += x[25]; //rcs
                        DRBE_obj_list[ind].bp_ant_ar_n += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ar l
                    case 9:

                        //DRBE_obj_list[ind].cp_ant_ar_p += x[3];  //cor
                        DRBE_obj_list[ind].cp_ant_ar_p += x[7];  //nr
                        DRBE_obj_list[ind].cp_ant_ar_p += x[11]; //ori
                                                                 //DRBE_obj_list[ind].cp_ant_ar_p += x[15]; //ant
                        DRBE_obj_list[ind].cp_ant_ar_p += x[19]; //pg
                        DRBE_obj_list[ind].cp_ant_ar_p += x[23]; //rcs
                        DRBE_obj_list[ind].cp_ant_ar_p += x[27]; //tu

                        //DRBE_obj_list[ind].mp_ant_ar_p += x[4];  //cor
                        DRBE_obj_list[ind].mp_ant_ar_p += x[8];  //nr
                        DRBE_obj_list[ind].mp_ant_ar_p += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_ant_ar_p += x[16]; //ant
                        DRBE_obj_list[ind].mp_ant_ar_p += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_ant_ar_p += x[24]; //rcs
                        DRBE_obj_list[ind].mp_ant_ar_p += x[28]; //tu

                        //DRBE_obj_list[ind].bp_ant_ar_p += x[5];  //cor
                        DRBE_obj_list[ind].bp_ant_ar_p += x[9];  //nr
                        DRBE_obj_list[ind].bp_ant_ar_p += x[13]; //ori
                                                                 //DRBE_obj_list[ind].bp_ant_ar_p += x[17]; //ant
                        DRBE_obj_list[ind].bp_ant_ar_p += x[21]; //pg
                        DRBE_obj_list[ind].bp_ant_ar_p += x[25]; //rcs
                        DRBE_obj_list[ind].bp_ant_ar_p += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ar u
                    case 10:

                        //DRBE_obj_list[ind].cp_ant_ds_n += x[3];  //cor
                        DRBE_obj_list[ind].cp_ant_ds_n += x[7];  //nr
                        DRBE_obj_list[ind].cp_ant_ds_n += x[11]; //ori
                                                                 //DRBE_obj_list[ind].cp_ant_ds_n += x[15]; //ant
                        DRBE_obj_list[ind].cp_ant_ds_n += x[19]; //pg
                        DRBE_obj_list[ind].cp_ant_ds_n += x[23]; //rcs
                        DRBE_obj_list[ind].cp_ant_ds_n += x[27]; //tu

                        //DRBE_obj_list[ind].mp_ant_ds_n += x[4];  //cor
                        DRBE_obj_list[ind].mp_ant_ds_n += x[8];  //nr
                        DRBE_obj_list[ind].mp_ant_ds_n += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_ant_ds_n += x[16]; //ant
                        DRBE_obj_list[ind].mp_ant_ds_n += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_ant_ds_n += x[24]; //rcs
                        DRBE_obj_list[ind].mp_ant_ds_n += x[28]; //tu

                        //DRBE_obj_list[ind].bp_ant_ds_n += x[5];  //cor
                        DRBE_obj_list[ind].bp_ant_ds_n += x[9];  //nr
                        DRBE_obj_list[ind].bp_ant_ds_n += x[13]; //ori
                                                                 //DRBE_obj_list[ind].bp_ant_ds_n += x[17]; //ant
                        DRBE_obj_list[ind].bp_ant_ds_n += x[21]; //pg
                        DRBE_obj_list[ind].bp_ant_ds_n += x[25]; //rcs
                        DRBE_obj_list[ind].bp_ant_ds_n += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ds l
                    case 11:

                        //DRBE_obj_list[ind].cp_ant_ds_p += x[3];  //cor
                        DRBE_obj_list[ind].cp_ant_ds_p += x[7];  //nr
                        DRBE_obj_list[ind].cp_ant_ds_p += x[11]; //ori
                                                                 //DRBE_obj_list[ind].cp_ant_ds_p += x[15]; //ant
                        DRBE_obj_list[ind].cp_ant_ds_p += x[19]; //pg
                        DRBE_obj_list[ind].cp_ant_ds_p += x[23]; //rcs
                        DRBE_obj_list[ind].cp_ant_ds_p += x[27]; //tu

                        //DRBE_obj_list[ind].mp_ant_ds_p += x[4];  //cor
                        DRBE_obj_list[ind].mp_ant_ds_p += x[8];  //nr
                        DRBE_obj_list[ind].mp_ant_ds_p += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_ant_ds_p += x[16]; //ant
                        DRBE_obj_list[ind].mp_ant_ds_p += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_ant_ds_p += x[24]; //rcs
                        DRBE_obj_list[ind].mp_ant_ds_p += x[28]; //tu

                        //DRBE_obj_list[ind].bp_ant_ds_p += x[5];  //cor
                        DRBE_obj_list[ind].bp_ant_ds_p += x[9];  //nr
                        DRBE_obj_list[ind].bp_ant_ds_p += x[13]; //ori
                                                                 //DRBE_obj_list[ind].bp_ant_ds_p += x[17]; //ant
                        DRBE_obj_list[ind].bp_ant_ds_p += x[21]; //pg
                        DRBE_obj_list[ind].bp_ant_ds_p += x[25]; //rcs
                        DRBE_obj_list[ind].bp_ant_ds_p += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ds u
                    case 12:

                        //DRBE_obj_list[ind].cf_rcs0 += x[3];  //cor
                        DRBE_obj_list[ind].cf_rcs0 += x[7];  //nr
                        DRBE_obj_list[ind].cf_rcs0 += x[11]; //ori
                                                             //DRBE_obj_list[ind].cf_rcs0 += x[15]; //ant
                        DRBE_obj_list[ind].cf_rcs0 += x[19]; //pg
                        DRBE_obj_list[ind].cf_rcs0 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_rcs0 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_rcs0 += x[4];  //cor
                        DRBE_obj_list[ind].mf_rcs0 += x[8];  //nr
                        DRBE_obj_list[ind].mf_rcs0 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_rcs0 += x[16]; //ant
                        DRBE_obj_list[ind].mf_rcs0 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_rcs0 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_rcs0 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_rcs0 += x[5];  //cor
                        DRBE_obj_list[ind].bf_rcs0 += x[9];  //nr
                        DRBE_obj_list[ind].bf_rcs0 += x[13]; //ori
                                                             //DRBE_obj_list[ind].bf_rcs0 += x[17]; //ant
                        DRBE_obj_list[ind].bf_rcs0 += x[21]; //pg
                        DRBE_obj_list[ind].bf_rcs0 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_rcs0 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //rcs 0
                    case 13:

                        //DRBE_obj_list[ind].cf_rcs1 += x[3];  //cor
                        DRBE_obj_list[ind].cf_rcs1 += x[7];  //nr
                        DRBE_obj_list[ind].cf_rcs1 += x[11]; //ori
                                                             //DRBE_obj_list[ind].cf_rcs1 += x[15]; //ant
                        DRBE_obj_list[ind].cf_rcs1 += x[19]; //pg
                        DRBE_obj_list[ind].cf_rcs1 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_rcs1 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_rcs1 += x[4];  //cor
                        DRBE_obj_list[ind].mf_rcs1 += x[8];  //nr
                        DRBE_obj_list[ind].mf_rcs1 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_rcs1 += x[16]; //ant
                        DRBE_obj_list[ind].mf_rcs1 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_rcs1 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_rcs1 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_rcs1 += x[5];  //cor
                        DRBE_obj_list[ind].bf_rcs1 += x[9];  //nr
                        DRBE_obj_list[ind].bf_rcs1 += x[13]; //ori
                                                             //DRBE_obj_list[ind].bf_rcs1 += x[17]; //ant
                        DRBE_obj_list[ind].bf_rcs1 += x[21]; //pg
                        DRBE_obj_list[ind].bf_rcs1 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_rcs1 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //rcs 1
                    case 14:

                        //DRBE_obj_list[ind].cf_rcs2 += x[3];  //cor
                        DRBE_obj_list[ind].cf_rcs2 += x[7];  //nr
                        DRBE_obj_list[ind].cf_rcs2 += x[11]; //ori
                                                             //DRBE_obj_list[ind].cf_rcs2 += x[15]; //ant
                        DRBE_obj_list[ind].cf_rcs2 += x[19]; //pg
                        DRBE_obj_list[ind].cf_rcs2 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_rcs2 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_rcs2 += x[4];  //cor
                        DRBE_obj_list[ind].mf_rcs2 += x[8];  //nr
                        DRBE_obj_list[ind].mf_rcs2 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_rcs2 += x[16]; //ant
                        DRBE_obj_list[ind].mf_rcs2 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_rcs2 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_rcs2 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_rcs2 += x[5];  //cor
                        DRBE_obj_list[ind].bf_rcs2 += x[9];  //nr
                        DRBE_obj_list[ind].bf_rcs2 += x[13]; //ori
                                                             //DRBE_obj_list[ind].bf_rcs2 += x[17]; //ant
                        DRBE_obj_list[ind].bf_rcs2 += x[21]; //pg
                        DRBE_obj_list[ind].bf_rcs2 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_rcs2 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //rcs 2
                    case 15:

                        //DRBE_obj_list[ind].cf_rcs3 += x[3];  //cor
                        DRBE_obj_list[ind].cf_rcs3 += x[7];  //nr
                        DRBE_obj_list[ind].cf_rcs3 += x[11]; //ori
                                                             //DRBE_obj_list[ind].cf_rcs3 += x[15]; //ant
                        DRBE_obj_list[ind].cf_rcs3 += x[19]; //pg
                        DRBE_obj_list[ind].cf_rcs3 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_rcs3 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_rcs3 += x[4];  //cor
                        DRBE_obj_list[ind].mf_rcs3 += x[8];  //nr
                        DRBE_obj_list[ind].mf_rcs3 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_rcs3 += x[16]; //ant
                        DRBE_obj_list[ind].mf_rcs3 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_rcs3 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_rcs3 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_rcs3 += x[5];  //cor
                        DRBE_obj_list[ind].bf_rcs3 += x[9];  //nr
                        DRBE_obj_list[ind].bf_rcs3 += x[13]; //ori
                                                             //DRBE_obj_list[ind].bf_rcs3 += x[17]; //ant
                        DRBE_obj_list[ind].bf_rcs3 += x[21]; //pg
                        DRBE_obj_list[ind].bf_rcs3 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_rcs3 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //rcs 3
                    case 16:

                        //DRBE_obj_list[ind].cf_rcs4 += x[3];  //cor
                        DRBE_obj_list[ind].cf_rcs4 += x[7];  //nr
                        DRBE_obj_list[ind].cf_rcs4 += x[11]; //ori
                                                             //DRBE_obj_list[ind].cf_rcs4 += x[15]; //ant
                        DRBE_obj_list[ind].cf_rcs4 += x[19]; //pg
                        DRBE_obj_list[ind].cf_rcs4 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_rcs4 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_rcs4 += x[4];  //cor
                        DRBE_obj_list[ind].mf_rcs4 += x[8];  //nr
                        DRBE_obj_list[ind].mf_rcs4 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_rcs4 += x[16]; //ant
                        DRBE_obj_list[ind].mf_rcs4 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_rcs4 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_rcs4 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_rcs4 += x[5];  //cor
                        DRBE_obj_list[ind].bf_rcs4 += x[9];  //nr
                        DRBE_obj_list[ind].bf_rcs4 += x[13]; //ori
                                                             //DRBE_obj_list[ind].bf_rcs4 += x[17]; //ant
                        DRBE_obj_list[ind].bf_rcs4 += x[21]; //pg
                        DRBE_obj_list[ind].bf_rcs4 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_rcs4 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //rcs 4
                    case 17:

                        //DRBE_obj_list[ind].cf_rcs5 += x[3];  //cor
                        DRBE_obj_list[ind].cf_rcs5 += x[7];  //nr
                        DRBE_obj_list[ind].cf_rcs5 += x[11]; //ori
                                                             //DRBE_obj_list[ind].cf_rcs5 += x[15]; //ant
                        DRBE_obj_list[ind].cf_rcs5 += x[19]; //pg
                        DRBE_obj_list[ind].cf_rcs5 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_rcs5 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_rcs5 += x[4];  //cor
                        DRBE_obj_list[ind].mf_rcs5 += x[8];  //nr
                        DRBE_obj_list[ind].mf_rcs5 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_rcs5 += x[16]; //ant
                        DRBE_obj_list[ind].mf_rcs5 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_rcs5 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_rcs5 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_rcs5 += x[5];  //cor
                        DRBE_obj_list[ind].bf_rcs5 += x[9];  //nr
                        DRBE_obj_list[ind].bf_rcs5 += x[13]; //ori
                                                             //DRBE_obj_list[ind].bf_rcs5 += x[17]; //ant
                        DRBE_obj_list[ind].bf_rcs5 += x[21]; //pg
                        DRBE_obj_list[ind].bf_rcs5 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_rcs5 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //rcs 5
                    case 18:

                        //DRBE_obj_list[ind].cf_rcs6 += x[3];  //cor
                        DRBE_obj_list[ind].cf_rcs6 += x[7];  //nr
                        DRBE_obj_list[ind].cf_rcs6 += x[11]; //ori
                                                             //DRBE_obj_list[ind].cf_rcs6 += x[15]; //ant
                        DRBE_obj_list[ind].cf_rcs6 += x[19]; //pg
                        DRBE_obj_list[ind].cf_rcs6 += x[23]; //rcs
                        DRBE_obj_list[ind].cf_rcs6 += x[27]; //tu

                        //DRBE_obj_list[ind].mf_rcs6 += x[4];  //cor
                        DRBE_obj_list[ind].mf_rcs6 += x[8];  //nr
                        DRBE_obj_list[ind].mf_rcs6 += x[12]; //ori
                                                             //DRBE_obj_list[ind].mf_rcs6 += x[16]; //ant
                        DRBE_obj_list[ind].mf_rcs6 += x[20]; //pg
                                                             //DRBE_obj_list[ind].cf_rcs6 += x[24]; //rcs
                        DRBE_obj_list[ind].mf_rcs6 += x[28]; //tu

                        //DRBE_obj_list[ind].bf_rcs6 += x[5];  //cor
                        DRBE_obj_list[ind].bf_rcs6 += x[9];  //nr
                        DRBE_obj_list[ind].bf_rcs6 += x[13]; //ori
                                                             //DRBE_obj_list[ind].bf_rcs6 += x[17]; //ant
                        DRBE_obj_list[ind].bf_rcs6 += x[21]; //pg
                        DRBE_obj_list[ind].bf_rcs6 += x[25]; //rcs
                        DRBE_obj_list[ind].bf_rcs6 += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //rcs 6
                    case 19:

                        //DRBE_obj_list[ind].cp_rcs_pn_n += x[3];  //cor
                        DRBE_obj_list[ind].cp_rcs_pn_n += x[7];  //nr
                        DRBE_obj_list[ind].cp_rcs_pn_n += x[11]; //ori
                                                                 //DRBE_obj_list[ind].cp_rcs_pn_n += x[15]; //ant
                        DRBE_obj_list[ind].cp_rcs_pn_n += x[19]; //pg
                        DRBE_obj_list[ind].cp_rcs_pn_n += x[23]; //rcs
                        DRBE_obj_list[ind].cp_rcs_pn_n += x[27]; //tu

                        //DRBE_obj_list[ind].mp_rcs_pn_n += x[4];  //cor
                        DRBE_obj_list[ind].mp_rcs_pn_n += x[8];  //nr
                        DRBE_obj_list[ind].mp_rcs_pn_n += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_rcs_pn_n += x[16]; //ant
                        DRBE_obj_list[ind].mp_rcs_pn_n += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_rcs_pn_n += x[24]; //rcs
                        DRBE_obj_list[ind].mp_rcs_pn_n += x[28]; //tu

                        //DRBE_obj_list[ind].bp_rcs_pn_n += x[5];  //cor
                        DRBE_obj_list[ind].bp_rcs_pn_n += x[9];  //nr
                        DRBE_obj_list[ind].bp_rcs_pn_n += x[13]; //ori
                                                                 //DRBE_obj_list[ind].bp_rcs_pn_n += x[17]; //ant
                        DRBE_obj_list[ind].bp_rcs_pn_n += x[21]; //pg
                        DRBE_obj_list[ind].bp_rcs_pn_n += x[25]; //rcs
                        DRBE_obj_list[ind].bp_rcs_pn_n += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //pn l
                    case 20:

                        //DRBE_obj_list[ind].cp_rcs_pn_p += x[3];  //cor
                        DRBE_obj_list[ind].cp_rcs_pn_p += x[7];  //nr
                        DRBE_obj_list[ind].cp_rcs_pn_p += x[11]; //ori
                                                                 //DRBE_obj_list[ind].cp_rcs_pn_p += x[15]; //ant
                        DRBE_obj_list[ind].cp_rcs_pn_p += x[19]; //pg
                        DRBE_obj_list[ind].cp_rcs_pn_p += x[23]; //rcs
                        DRBE_obj_list[ind].cp_rcs_pn_p += x[27]; //tu

                        //DRBE_obj_list[ind].mp_rcs_pn_p += x[4];  //cor
                        DRBE_obj_list[ind].mp_rcs_pn_p += x[8];  //nr
                        DRBE_obj_list[ind].mp_rcs_pn_p += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_rcs_pn_p += x[16]; //ant
                        DRBE_obj_list[ind].mp_rcs_pn_p += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_rcs_pn_p += x[24]; //rcs
                        DRBE_obj_list[ind].mp_rcs_pn_p += x[28]; //tu

                        //DRBE_obj_list[ind].bp_rcs_pn_p += x[5];  //cor
                        DRBE_obj_list[ind].bp_rcs_pn_p += x[9];  //nr
                        DRBE_obj_list[ind].bp_rcs_pn_p += x[13]; //ori
                                                                 //DRBE_obj_list[ind].bp_rcs_pn_p += x[17]; //ant
                        DRBE_obj_list[ind].bp_rcs_pn_p += x[21]; //pg
                        DRBE_obj_list[ind].bp_rcs_pn_p += x[25]; //rcs
                        DRBE_obj_list[ind].bp_rcs_pn_p += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //pn u
                    case 21:

                        //DRBE_obj_list[ind].cp_rcs_ss_n += x[3];  //cor
                        DRBE_obj_list[ind].cp_rcs_ss_n += x[7];  //nr
                        DRBE_obj_list[ind].cp_rcs_ss_n += x[11]; //ori
                                                                 //DRBE_obj_list[ind].cp_rcs_ss_n += x[15]; //ant
                        DRBE_obj_list[ind].cp_rcs_ss_n += x[19]; //pg
                        DRBE_obj_list[ind].cp_rcs_ss_n += x[23]; //rcs
                        DRBE_obj_list[ind].cp_rcs_ss_n += x[27]; //tu

                        //DRBE_obj_list[ind].mp_rcs_ss_n += x[4];  //cor
                        DRBE_obj_list[ind].mp_rcs_ss_n += x[8];  //nr
                        DRBE_obj_list[ind].mp_rcs_ss_n += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_rcs_ss_n += x[16]; //ant
                        DRBE_obj_list[ind].mp_rcs_ss_n += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_rcs_ss_n += x[24]; //rcs
                        DRBE_obj_list[ind].mp_rcs_ss_n += x[28]; //tu

                        //DRBE_obj_list[ind].bp_rcs_ss_n += x[5];  //cor
                        DRBE_obj_list[ind].bp_rcs_ss_n += x[9];  //nr
                        DRBE_obj_list[ind].bp_rcs_ss_n += x[13]; //ori
                                                                 //DRBE_obj_list[ind].bp_rcs_ss_n += x[17]; //ant
                        DRBE_obj_list[ind].bp_rcs_ss_n += x[21]; //pg
                        DRBE_obj_list[ind].bp_rcs_ss_n += x[25]; //rcs
                        DRBE_obj_list[ind].bp_rcs_ss_n += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ss l
                    case 22:

                        //DRBE_obj_list[ind].cp_rcs_ss_p += x[3];  //cor
                        DRBE_obj_list[ind].cp_rcs_ss_p += x[7];  //nr
                        DRBE_obj_list[ind].cp_rcs_ss_p += x[11]; //ori
                                                                 //DRBE_obj_list[ind].cp_rcs_ss_p += x[15]; //ant
                        DRBE_obj_list[ind].cp_rcs_ss_p += x[19]; //pg
                        DRBE_obj_list[ind].cp_rcs_ss_p += x[23]; //rcs
                        DRBE_obj_list[ind].cp_rcs_ss_p += x[27]; //tu

                        //DRBE_obj_list[ind].mp_rcs_ss_p += x[4];  //cor
                        DRBE_obj_list[ind].mp_rcs_ss_p += x[8];  //nr
                        DRBE_obj_list[ind].mp_rcs_ss_p += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_rcs_ss_p += x[16]; //ant
                        DRBE_obj_list[ind].mp_rcs_ss_p += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_rcs_ss_p += x[24]; //rcs
                        DRBE_obj_list[ind].mp_rcs_ss_p += x[28]; //tu

                        //DRBE_obj_list[ind].bp_rcs_ss_p += x[5];  //cor
                        DRBE_obj_list[ind].bp_rcs_ss_p += x[9];  //nr
                        DRBE_obj_list[ind].bp_rcs_ss_p += x[13]; //ori
                                                                 //DRBE_obj_list[ind].bp_rcs_ss_p += x[17]; //ant
                        DRBE_obj_list[ind].bp_rcs_ss_p += x[21]; //pg
                        DRBE_obj_list[ind].bp_rcs_ss_p += x[25]; //rcs
                        DRBE_obj_list[ind].bp_rcs_ss_p += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ss u
                    case 23:

                        //DRBE_obj_list[ind].cp_rcs_sp_n += x[3];  //cor
                        DRBE_obj_list[ind].cp_rcs_sp_n += x[7];  //nr
                        DRBE_obj_list[ind].cp_rcs_sp_n += x[11]; //ori
                                                                 //DRBE_obj_list[ind].cp_rcs_sp_n += x[15]; //ant
                        DRBE_obj_list[ind].cp_rcs_sp_n += x[19]; //pg
                        DRBE_obj_list[ind].cp_rcs_sp_n += x[23]; //rcs
                        DRBE_obj_list[ind].cp_rcs_sp_n += x[27]; //tu

                        //DRBE_obj_list[ind].mp_rcs_sp_n += x[4];  //cor
                        DRBE_obj_list[ind].mp_rcs_sp_n += x[8];  //nr
                        DRBE_obj_list[ind].mp_rcs_sp_n += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_rcs_sp_n += x[16]; //ant
                        DRBE_obj_list[ind].mp_rcs_sp_n += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_rcs_sp_n += x[24]; //rcs
                        DRBE_obj_list[ind].mp_rcs_sp_n += x[28]; //tu

                        //DRBE_obj_list[ind].bp_rcs_sp_n += x[5];  //cor
                        DRBE_obj_list[ind].bp_rcs_sp_n += x[9];  //nr
                        DRBE_obj_list[ind].bp_rcs_sp_n += x[13]; //ori
                                                                 //DRBE_obj_list[ind].bp_rcs_sp_n += x[17]; //ant
                        DRBE_obj_list[ind].bp_rcs_sp_n += x[21]; //pg
                        DRBE_obj_list[ind].bp_rcs_sp_n += x[25]; //rcs
                        DRBE_obj_list[ind].bp_rcs_sp_n += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //sp l
                    case 24:

                        //DRBE_obj_list[ind].cp_rcs_sp_p += x[3];  //cor
                        DRBE_obj_list[ind].cp_rcs_sp_p += x[7];  //nr
                        DRBE_obj_list[ind].cp_rcs_sp_p += x[11]; //ori
                                                                 //DRBE_obj_list[ind].cp_rcs_sp_p += x[15]; //ant
                        DRBE_obj_list[ind].cp_rcs_sp_p += x[19]; //pg
                        DRBE_obj_list[ind].cp_rcs_sp_p += x[23]; //rcs
                        DRBE_obj_list[ind].cp_rcs_sp_p += x[27]; //tu

                        //DRBE_obj_list[ind].mp_rcs_sp_p += x[4];  //cor
                        DRBE_obj_list[ind].mp_rcs_sp_p += x[8];  //nr
                        DRBE_obj_list[ind].mp_rcs_sp_p += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_rcs_sp_p += x[16]; //ant
                        DRBE_obj_list[ind].mp_rcs_sp_p += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_rcs_sp_p += x[24]; //rcs
                        DRBE_obj_list[ind].mp_rcs_sp_p += x[28]; //tu

                        //DRBE_obj_list[ind].bp_rcs_sp_p += x[5];  //cor
                        DRBE_obj_list[ind].bp_rcs_sp_p += x[9];  //nr
                        DRBE_obj_list[ind].bp_rcs_sp_p += x[13]; //ori
                                                                 //DRBE_obj_list[ind].bp_rcs_sp_p += x[17]; //ant
                        DRBE_obj_list[ind].bp_rcs_sp_p += x[21]; //pg
                        DRBE_obj_list[ind].bp_rcs_sp_p += x[25]; //rcs
                        DRBE_obj_list[ind].bp_rcs_sp_p += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //sp u
                    case 25:

                        //DRBE_obj_list[ind].cp_rcs_ar_n += x[3];  //cor
                        DRBE_obj_list[ind].cp_rcs_ar_n += x[7];  //nr
                        DRBE_obj_list[ind].cp_rcs_ar_n += x[11]; //ori
                                                                 //DRBE_obj_list[ind].cp_rcs_ar_n += x[15]; //ant
                        DRBE_obj_list[ind].cp_rcs_ar_n += x[19]; //pg
                        DRBE_obj_list[ind].cp_rcs_ar_n += x[23]; //rcs
                        DRBE_obj_list[ind].cp_rcs_ar_n += x[27]; //tu

                        //DRBE_obj_list[ind].mp_rcs_ar_n += x[4];  //cor
                        DRBE_obj_list[ind].mp_rcs_ar_n += x[8];  //nr
                        DRBE_obj_list[ind].mp_rcs_ar_n += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_rcs_ar_n += x[16]; //ant
                        DRBE_obj_list[ind].mp_rcs_ar_n += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_rcs_ar_n += x[24]; //rcs
                        DRBE_obj_list[ind].mp_rcs_ar_n += x[28]; //tu

                        //DRBE_obj_list[ind].bp_rcs_ar_n += x[5];  //cor
                        DRBE_obj_list[ind].bp_rcs_ar_n += x[9];  //nr
                        DRBE_obj_list[ind].bp_rcs_ar_n += x[13]; //ori
                                                                 //DRBE_obj_list[ind].bp_rcs_ar_n += x[17]; //ant
                        DRBE_obj_list[ind].bp_rcs_ar_n += x[21]; //pg
                        DRBE_obj_list[ind].bp_rcs_ar_n += x[25]; //rcs
                        DRBE_obj_list[ind].bp_rcs_ar_n += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ar l
                    case 26:

                        //DRBE_obj_list[ind].cp_rcs_ar_p += x[3];  //cor
                        DRBE_obj_list[ind].cp_rcs_ar_p += x[7];  //nr
                        DRBE_obj_list[ind].cp_rcs_ar_p += x[11]; //ori
                                                                 //DRBE_obj_list[ind].cp_rcs_ar_p += x[15]; //ant
                        DRBE_obj_list[ind].cp_rcs_ar_p += x[19]; //pg
                        DRBE_obj_list[ind].cp_rcs_ar_p += x[23]; //rcs
                        DRBE_obj_list[ind].cp_rcs_ar_p += x[27]; //tu

                        //DRBE_obj_list[ind].mp_rcs_ar_p += x[4];  //cor
                        DRBE_obj_list[ind].mp_rcs_ar_p += x[8];  //nr
                        DRBE_obj_list[ind].mp_rcs_ar_p += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_rcs_ar_p += x[16]; //ant
                        DRBE_obj_list[ind].mp_rcs_ar_p += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_rcs_ar_p += x[24]; //rcs
                        DRBE_obj_list[ind].mp_rcs_ar_p += x[28]; //tu

                        //DRBE_obj_list[ind].bp_rcs_ar_p += x[5];  //cor
                        DRBE_obj_list[ind].bp_rcs_ar_p += x[9];  //nr
                        DRBE_obj_list[ind].bp_rcs_ar_p += x[13]; //ori
                                                                 //DRBE_obj_list[ind].bp_rcs_ar_p += x[17]; //ant
                        DRBE_obj_list[ind].bp_rcs_ar_p += x[21]; //pg
                        DRBE_obj_list[ind].bp_rcs_ar_p += x[25]; //rcs
                        DRBE_obj_list[ind].bp_rcs_ar_p += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //ar u
                    case 27:

                        //DRBE_obj_list[ind].cp_rcs_fb_n += x[3];  //cor
                        DRBE_obj_list[ind].cp_rcs_fb_n += x[7];  //nr
                        DRBE_obj_list[ind].cp_rcs_fb_n += x[11]; //ori
                                                                 //DRBE_obj_list[ind].cp_rcs_fb_n += x[15]; //ant
                        DRBE_obj_list[ind].cp_rcs_fb_n += x[19]; //pg
                        DRBE_obj_list[ind].cp_rcs_fb_n += x[23]; //rcs
                        DRBE_obj_list[ind].cp_rcs_fb_n += x[27]; //tu

                        //DRBE_obj_list[ind].mp_rcs_fb_n += x[4];  //cor
                        DRBE_obj_list[ind].mp_rcs_fb_n += x[8];  //nr
                        DRBE_obj_list[ind].mp_rcs_fb_n += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_rcs_fb_n += x[16]; //ant
                        DRBE_obj_list[ind].mp_rcs_fb_n += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_rcs_fb_n += x[24]; //rcs
                        DRBE_obj_list[ind].mp_rcs_fb_n += x[28]; //tu

                        //DRBE_obj_list[ind].bp_rcs_fb_n += x[5];  //cor
                        DRBE_obj_list[ind].bp_rcs_fb_n += x[9];  //nr
                        DRBE_obj_list[ind].bp_rcs_fb_n += x[13]; //ori
                                                                 //DRBE_obj_list[ind].bp_rcs_fb_n += x[17]; //ant
                        DRBE_obj_list[ind].bp_rcs_fb_n += x[21]; //pg
                        DRBE_obj_list[ind].bp_rcs_fb_n += x[25]; //rcs
                        DRBE_obj_list[ind].bp_rcs_fb_n += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //fb l
                    case 28:

                        //DRBE_obj_list[ind].cp_rcs_fb_p += x[3];  //cor
                        DRBE_obj_list[ind].cp_rcs_fb_p += x[7];  //nr
                        DRBE_obj_list[ind].cp_rcs_fb_p += x[11]; //ori
                                                                 //DRBE_obj_list[ind].cp_rcs_fb_p += x[15]; //ant
                        DRBE_obj_list[ind].cp_rcs_fb_p += x[19]; //pg
                        DRBE_obj_list[ind].cp_rcs_fb_p += x[23]; //rcs
                        DRBE_obj_list[ind].cp_rcs_fb_p += x[27]; //tu

                        //DRBE_obj_list[ind].mp_rcs_fb_p += x[4];  //cor
                        DRBE_obj_list[ind].mp_rcs_fb_p += x[8];  //nr
                        DRBE_obj_list[ind].mp_rcs_fb_p += x[12]; //ori
                                                                 //DRBE_obj_list[ind].mp_rcs_fb_p += x[16]; //ant
                        DRBE_obj_list[ind].mp_rcs_fb_p += x[20]; //pg
                                                                 //DRBE_obj_list[ind].mp_rcs_fb_p += x[24]; //rcs
                        DRBE_obj_list[ind].mp_rcs_fb_p += x[28]; //tu

                        //DRBE_obj_list[ind].bp_rcs_fb_p += x[5];  //cor
                        DRBE_obj_list[ind].bp_rcs_fb_p += x[9];  //nr
                        DRBE_obj_list[ind].bp_rcs_fb_p += x[13]; //ori
                                                                 //DRBE_obj_list[ind].bp_rcs_fb_p += x[17]; //ant
                        DRBE_obj_list[ind].bp_rcs_fb_p += x[21]; //pg
                        DRBE_obj_list[ind].bp_rcs_fb_p += x[25]; //rcs
                        DRBE_obj_list[ind].bp_rcs_fb_p += x[29]; //tu

                        //DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[6]);  //cor
                        DRBE_obj_scan_latency_list[1][pind] = Math.Max(DRBE_obj_scan_latency_list[1][pind], x[10]);  //nr
                        DRBE_obj_scan_latency_list[2][pind] = Math.Max(DRBE_obj_scan_latency_list[2][pind], x[14]); //ori
                                                                                                                    //DRBE_obj_scan_latency_list[3][pind] = Math.Max(DRBE_obj_scan_latency_list[3][pind], x[18]); //ant
                        DRBE_obj_scan_latency_list[4][pind] = Math.Max(DRBE_obj_scan_latency_list[4][pind], x[22]); //pg
                        DRBE_obj_scan_latency_list[5][pind] = Math.Max(DRBE_obj_scan_latency_list[5][pind], x[26]); //rcs
                        DRBE_obj_scan_latency_list[6][pind] = Math.Max(DRBE_obj_scan_latency_list[6][pind], x[30]); //tu
                        break; //fb u
                    default:
                        await ShowDialog("Invalid Scan Update", "Invalide Variable number");
                        break;
                }
            }
        }
        private void Scan_obj_special_update(List<double> x, int ind, int pind, int mode)
        {
            //ind object number
            //pind property number
            //mode t,o,r
            //x information
            //bound lower, original, uppder

            if (mode == 0)//transmitter
            {
                if (pind == 0)//ant 0
                {
                    DRBE_obj_list[ind].cf_ant0 += x[3];  //cor

                    DRBE_obj_list[ind].mf_ant0 += x[4];  //cor

                    DRBE_obj_list[ind].mf_ant0 += x[16]; //ant

                    DRBE_obj_list[ind].bf_ant0 += x[5];  //cor
                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor




                } //ant 0
                else if (pind == 1)
                {
                    DRBE_obj_list[ind].cf_ant1 += x[3];  //cor

                    DRBE_obj_list[ind].mf_ant1 += x[4];  //cor

                    DRBE_obj_list[ind].mf_ant1 += x[16]; //ant

                    DRBE_obj_list[ind].bf_ant1 += x[5];  //cor
                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //ant 1
                else if (pind == 2)
                {
                    DRBE_obj_list[ind].cf_ant2 += x[3];  //cor

                    DRBE_obj_list[ind].mf_ant2 += x[4];  //cor

                    DRBE_obj_list[ind].mf_ant2 += x[16]; //ant

                    DRBE_obj_list[ind].bf_ant2 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //ant 2
                else if (pind == 3)
                {
                    DRBE_obj_list[ind].cf_ant3 += x[3];  //cor

                    DRBE_obj_list[ind].mf_ant3 += x[4];  //cor

                    DRBE_obj_list[ind].mf_ant3 += x[16]; //ant

                    DRBE_obj_list[ind].bf_ant3 += x[5];  //cor
                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //ant 3
                else if (pind == 4)
                {
                    DRBE_obj_list[ind].cf_ant4 += x[3];  //cor

                    DRBE_obj_list[ind].mf_ant4 += x[4];  //cor

                    DRBE_obj_list[ind].mf_ant4 += x[16]; //ant

                    DRBE_obj_list[ind].bf_ant4 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //ant 4
                else if (pind == 5)
                {
                    DRBE_obj_list[ind].cf_ant5 += x[3];  //cor

                    DRBE_obj_list[ind].mf_ant5 += x[4];  //cor

                    DRBE_obj_list[ind].mf_ant5 += x[16]; //ant

                    DRBE_obj_list[ind].bf_ant5 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //ant 5
                else if (pind == 6)
                {
                    DRBE_obj_list[ind].cp_ant_na_n += x[3];  //cor

                    DRBE_obj_list[ind].mp_ant_na_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_ant_na_n += x[16]; //ant


                    DRBE_obj_list[ind].bp_ant_na_n += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //na l
                else if (pind == 7)
                {
                    DRBE_obj_list[ind].cp_ant_na_p += x[3];  //cor


                    DRBE_obj_list[ind].mp_ant_na_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_ant_na_p += x[16]; //ant


                    DRBE_obj_list[ind].bp_ant_na_p += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //na u
                else if (pind == 8)
                {
                    DRBE_obj_list[ind].cp_ant_ar_n += x[3];  //cor


                    DRBE_obj_list[ind].mp_ant_ar_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_ant_ar_n += x[16]; //ant


                    DRBE_obj_list[ind].bp_ant_ar_n += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ar l
                else if (pind == 9)
                {
                    DRBE_obj_list[ind].cp_ant_ar_p += x[3];  //cor


                    DRBE_obj_list[ind].mp_ant_ar_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_ant_ar_p += x[16]; //ant


                    DRBE_obj_list[ind].bp_ant_ar_p += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ar u
                else if (pind == 10)
                {
                    DRBE_obj_list[ind].cp_ant_ds_n += x[3];  //cor


                    DRBE_obj_list[ind].mp_ant_ds_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_ant_ds_n += x[16]; //ant


                    DRBE_obj_list[ind].bp_ant_ds_n += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ds l
                else if (pind == 11)
                {
                    DRBE_obj_list[ind].cp_ant_ds_p += x[3];  //cor


                    DRBE_obj_list[ind].mp_ant_ds_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_ant_ds_p += x[16]; //ant


                    DRBE_obj_list[ind].bp_ant_ds_p += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ds u
                else if (pind == 12)
                {
                    DRBE_obj_list[ind].cf_rcs0 += x[3];  //cor


                    DRBE_obj_list[ind].mf_rcs0 += x[4];  //cor

                    DRBE_obj_list[ind].mf_rcs0 += x[16]; //ant


                    DRBE_obj_list[ind].bf_rcs0 += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //rcs 0
                else if (pind == 13)
                {
                    DRBE_obj_list[ind].cf_rcs1 += x[3];  //cor


                    DRBE_obj_list[ind].mf_rcs1 += x[4];  //cor

                    DRBE_obj_list[ind].mf_rcs1 += x[16]; //ant


                    DRBE_obj_list[ind].bf_rcs1 += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //rcs 1
                else if (pind == 14)
                {
                    DRBE_obj_list[ind].cf_rcs2 += x[3];  //cor


                    DRBE_obj_list[ind].mf_rcs2 += x[4];  //cor

                    DRBE_obj_list[ind].mf_rcs2 += x[16]; //ant

                    DRBE_obj_list[ind].bf_rcs2 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //rcs 2
                else if (pind == 15)
                {
                    DRBE_obj_list[ind].cf_rcs3 += x[3];  //cor

                    DRBE_obj_list[ind].mf_rcs3 += x[4];  //cor

                    DRBE_obj_list[ind].mf_rcs3 += x[16]; //ant

                    DRBE_obj_list[ind].bf_rcs3 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //rcs 3
                else if (pind == 16)
                {
                    DRBE_obj_list[ind].cf_rcs4 += x[3];  //cor


                    DRBE_obj_list[ind].mf_rcs4 += x[4];  //cor

                    DRBE_obj_list[ind].mf_rcs4 += x[16]; //ant


                    DRBE_obj_list[ind].bf_rcs4 += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //rcs 4
                else if (pind == 17)
                {
                    DRBE_obj_list[ind].cf_rcs5 += x[3];  //cor


                    DRBE_obj_list[ind].mf_rcs5 += x[4];  //cor

                    DRBE_obj_list[ind].mf_rcs5 += x[16]; //ant


                    DRBE_obj_list[ind].bf_rcs5 += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //rcs 5
                else if (pind == 18)
                {
                    DRBE_obj_list[ind].cf_rcs6 += x[3];  //cor


                    DRBE_obj_list[ind].mf_rcs6 += x[4];  //cor

                    DRBE_obj_list[ind].mf_rcs6 += x[16]; //ant


                    DRBE_obj_list[ind].bf_rcs6 += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //rcs 6
                else if (pind == 19)
                {
                    DRBE_obj_list[ind].cp_rcs_pn_n += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_pn_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_pn_n += x[16]; //ant


                    DRBE_obj_list[ind].bp_rcs_pn_n += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //pn l
                else if (pind == 20)
                {
                    DRBE_obj_list[ind].cp_rcs_pn_p += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_pn_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_pn_p += x[16]; //ant


                    DRBE_obj_list[ind].bp_rcs_pn_p += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //pn u
                else if (pind == 21)
                {
                    DRBE_obj_list[ind].cp_rcs_ss_n += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_ss_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_ss_n += x[16]; //ant


                    DRBE_obj_list[ind].bp_rcs_ss_n += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ss l
                else if (pind == 22)
                {
                    DRBE_obj_list[ind].cp_rcs_ss_p += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_ss_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_ss_p += x[16]; //ant


                    DRBE_obj_list[ind].bp_rcs_ss_p += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ss u
                else if (pind == 23)
                {
                    DRBE_obj_list[ind].cp_rcs_sp_n += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_sp_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_sp_n += x[16]; //ant


                    DRBE_obj_list[ind].bp_rcs_sp_n += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //sp l
                else if (pind == 24)
                {
                    DRBE_obj_list[ind].cp_rcs_sp_p += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_sp_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_sp_p += x[16]; //ant


                    DRBE_obj_list[ind].bp_rcs_sp_p += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //sp u
                else if (pind == 25)
                {
                    DRBE_obj_list[ind].cp_rcs_ar_n += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_ar_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_ar_n += x[16]; //ant


                    DRBE_obj_list[ind].bp_rcs_ar_n += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ar l
                else if (pind == 26)
                {
                    DRBE_obj_list[ind].cp_rcs_ar_p += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_ar_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_ar_p += x[16]; //ant


                    DRBE_obj_list[ind].bp_rcs_ar_p += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ar u
                else if (pind == 27)
                {
                    DRBE_obj_list[ind].cp_rcs_fb_n += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_fb_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_fb_n += x[16]; //ant

                    DRBE_obj_list[ind].bp_rcs_fb_n += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //fb l
                else if (pind == 28)
                {
                    DRBE_obj_list[ind].cp_rcs_fb_p += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_fb_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_fb_p += x[16]; //ant


                    DRBE_obj_list[ind].bp_rcs_fb_p += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //fb u
            }
            else if (mode == 1)
            {
                if (pind == 0)//ant 0
                {
                    DRBE_obj_list[ind].cf_ant0 += x[3];  //cor

                    DRBE_obj_list[ind].mf_ant0 += x[4];  //cor

                    DRBE_obj_list[ind].mf_ant0 += x[24]; //rcs

                    DRBE_obj_list[ind].bf_ant0 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor


                } //ant 0
                else if (pind == 1)
                {
                    DRBE_obj_list[ind].cf_ant1 += x[3];  //cor

                    DRBE_obj_list[ind].mf_ant1 += x[4];  //cor

                    DRBE_obj_list[ind].mf_ant1 += x[24]; //rcs

                    DRBE_obj_list[ind].bf_ant1 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //ant 1
                else if (pind == 2)
                {
                    DRBE_obj_list[ind].cf_ant2 += x[3];  //cor

                    DRBE_obj_list[ind].mf_ant2 += x[4];  //cor

                    DRBE_obj_list[ind].mf_ant2 += x[24]; //rcs

                    DRBE_obj_list[ind].bf_ant2 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //ant 2
                else if (pind == 3)
                {
                    DRBE_obj_list[ind].cf_ant3 += x[3];  //cor

                    DRBE_obj_list[ind].mf_ant3 += x[4];  //cor

                    DRBE_obj_list[ind].mf_ant3 += x[24]; //rcs

                    DRBE_obj_list[ind].bf_ant3 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //ant 3
                else if (pind == 4)
                {
                    DRBE_obj_list[ind].cf_ant4 += x[3];  //cor

                    DRBE_obj_list[ind].mf_ant4 += x[4];  //cor

                    DRBE_obj_list[ind].mf_ant4 += x[24]; //rcs

                    DRBE_obj_list[ind].bf_ant4 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //ant 4
                else if (pind == 5)
                {
                    DRBE_obj_list[ind].cf_ant5 += x[3];  //cor

                    DRBE_obj_list[ind].mf_ant5 += x[4];  //cor

                    DRBE_obj_list[ind].mf_ant5 += x[24]; //rcs

                    DRBE_obj_list[ind].bf_ant5 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //ant 5
                else if (pind == 6)
                {
                    DRBE_obj_list[ind].cp_ant_na_n += x[3];  //cor


                    DRBE_obj_list[ind].mp_ant_na_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_ant_na_n += x[24]; //rcs


                    DRBE_obj_list[ind].bp_ant_na_n += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //na l
                else if (pind == 7)
                {
                    DRBE_obj_list[ind].cp_ant_na_p += x[3];  //cor


                    DRBE_obj_list[ind].mp_ant_na_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_ant_na_p += x[24]; //rcs


                    DRBE_obj_list[ind].bp_ant_na_p += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //na u
                else if (pind == 8)
                {
                    DRBE_obj_list[ind].cp_ant_ar_n += x[3];  //cor

                    DRBE_obj_list[ind].mp_ant_ar_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_ant_ar_n += x[24]; //rcs


                    DRBE_obj_list[ind].bp_ant_ar_n += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ar l
                else if (pind == 9)
                {
                    DRBE_obj_list[ind].cp_ant_ar_p += x[3];  //cor


                    DRBE_obj_list[ind].mp_ant_ar_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_ant_ar_p += x[24]; //rcs


                    DRBE_obj_list[ind].bp_ant_ar_p += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ar u
                else if (pind == 10)
                {
                    DRBE_obj_list[ind].cp_ant_ds_n += x[3];  //cor


                    DRBE_obj_list[ind].mp_ant_ds_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_ant_ds_n += x[24]; //rcs


                    DRBE_obj_list[ind].bp_ant_ds_n += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ds l
                else if (pind == 11)
                {
                    DRBE_obj_list[ind].cp_ant_ds_p += x[3];  //cor


                    DRBE_obj_list[ind].mp_ant_ds_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_ant_ds_p += x[24]; //rcs


                    DRBE_obj_list[ind].bp_ant_ds_p += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ds u
                else if (pind == 12)
                {
                    DRBE_obj_list[ind].cf_rcs0 += x[3];  //cor


                    DRBE_obj_list[ind].mf_rcs0 += x[4];  //cor

                    DRBE_obj_list[ind].mf_rcs0 += x[24]; //rcs


                    DRBE_obj_list[ind].bf_rcs0 += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //rcs 0
                else if (pind == 13)
                {
                    DRBE_obj_list[ind].cf_rcs1 += x[3];  //cor


                    DRBE_obj_list[ind].mf_rcs1 += x[4];  //cor

                    DRBE_obj_list[ind].mf_rcs1 += x[24]; //rcs


                    DRBE_obj_list[ind].bf_rcs1 += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //rcs 1
                else if (pind == 14)
                {
                    DRBE_obj_list[ind].cf_rcs2 += x[3];  //cor


                    DRBE_obj_list[ind].mf_rcs2 += x[4];  //cor

                    DRBE_obj_list[ind].mf_rcs2 += x[24]; //rcs


                    DRBE_obj_list[ind].bf_rcs2 += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //rcs 2
                else if (pind == 15)
                {
                    DRBE_obj_list[ind].cf_rcs3 += x[3];  //cor


                    DRBE_obj_list[ind].mf_rcs3 += x[4];  //cor

                    DRBE_obj_list[ind].mf_rcs3 += x[24]; //rcs


                    DRBE_obj_list[ind].bf_rcs3 += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //rcs 3
                else if (pind == 16)
                {
                    DRBE_obj_list[ind].cf_rcs4 += x[3];  //cor


                    DRBE_obj_list[ind].mf_rcs4 += x[4];  //cor

                    DRBE_obj_list[ind].mf_rcs4 += x[24]; //rcs


                    DRBE_obj_list[ind].bf_rcs4 += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //rcs 4
                else if (pind == 17)
                {
                    DRBE_obj_list[ind].cf_rcs5 += x[3];  //cor


                    DRBE_obj_list[ind].mf_rcs5 += x[4];  //cor

                    DRBE_obj_list[ind].mf_rcs5 += x[24]; //rcs


                    DRBE_obj_list[ind].bf_rcs5 += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //rcs 5
                else if (pind == 18)
                {
                    DRBE_obj_list[ind].cf_rcs6 += x[3];  //cor


                    DRBE_obj_list[ind].mf_rcs6 += x[4];  //cor

                    DRBE_obj_list[ind].mf_rcs6 += x[24]; //rcs


                    DRBE_obj_list[ind].bf_rcs6 += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //rcs 6
                else if (pind == 19)
                {
                    DRBE_obj_list[ind].cp_rcs_pn_n += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_pn_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_pn_n += x[24]; //rcs


                    DRBE_obj_list[ind].bp_rcs_pn_n += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //pn l
                else if (pind == 20)
                {
                    DRBE_obj_list[ind].cp_rcs_pn_p += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_pn_p += x[4];  //cor
  
                    DRBE_obj_list[ind].mp_rcs_pn_p += x[24]; //rcs


                    DRBE_obj_list[ind].bp_rcs_pn_p += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //pn u
                else if (pind == 21)
                {
                    DRBE_obj_list[ind].cp_rcs_ss_n += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_ss_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_ss_n += x[24]; //rcs


                    DRBE_obj_list[ind].bp_rcs_ss_n += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ss l
                else if (pind == 22)
                {
                    DRBE_obj_list[ind].cp_rcs_ss_p += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_ss_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_ss_p += x[24]; //rcs

                    DRBE_obj_list[ind].bp_rcs_ss_p += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ss u
                else if (pind == 23)
                {
                    DRBE_obj_list[ind].cp_rcs_sp_n += x[3];  //cor

                    DRBE_obj_list[ind].mp_rcs_sp_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_sp_n += x[24]; //rcs

                    DRBE_obj_list[ind].bp_rcs_sp_n += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //sp l
                else if (pind == 24)
                {
                    DRBE_obj_list[ind].cp_rcs_sp_p += x[3];  //cor

                    DRBE_obj_list[ind].mp_rcs_sp_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_sp_p += x[24]; //rcs

                    DRBE_obj_list[ind].bp_rcs_sp_p += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //sp u
                else if (pind == 25)
                {
                    DRBE_obj_list[ind].cp_rcs_ar_n += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_ar_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_ar_n += x[24]; //rcs

                    DRBE_obj_list[ind].bp_rcs_ar_n += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //ar l
                else if (pind == 26)
                {
                    DRBE_obj_list[ind].cp_rcs_ar_p += x[3];  //cor

                    DRBE_obj_list[ind].mp_rcs_ar_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_ar_p += x[24]; //rcs

                    DRBE_obj_list[ind].bp_rcs_ar_p += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ar u
                else if (pind == 27)
                {
                    DRBE_obj_list[ind].cp_rcs_fb_n += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_fb_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_fb_n += x[24]; //rcs


                    DRBE_obj_list[ind].bp_rcs_fb_n += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //fb l
                else if (pind == 28)
                {
                    DRBE_obj_list[ind].cp_rcs_fb_p += x[3];  //cor

                    DRBE_obj_list[ind].mp_rcs_fb_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_fb_p += x[24]; //rcs


                    DRBE_obj_list[ind].bp_rcs_fb_p += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //fb u
            }
            else if (mode == 2)
            {
                if (pind == 0)//ant 0
                {
                    DRBE_obj_list[ind].cf_ant0 += x[3];  //cor

                    DRBE_obj_list[ind].mf_ant0 += x[4];  //cor

                    DRBE_obj_list[ind].mf_ant0 += x[16]; //ant

                    DRBE_obj_list[ind].bf_ant0 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ant 0
                else if (pind == 1)
                {
                    DRBE_obj_list[ind].cf_ant1 += x[3];  //cor

                    DRBE_obj_list[ind].mf_ant1 += x[4];  //cor

                    DRBE_obj_list[ind].mf_ant1 += x[16]; //ant

                    DRBE_obj_list[ind].bf_ant1 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //ant 1
                else if (pind == 2)
                {
                    DRBE_obj_list[ind].cf_ant2 += x[3];  //cor

                    DRBE_obj_list[ind].mf_ant2 += x[4];  //cor

                    DRBE_obj_list[ind].mf_ant2 += x[16]; //ant

                    DRBE_obj_list[ind].bf_ant2 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //ant 2
                else if (pind == 3)
                {
                    DRBE_obj_list[ind].cf_ant3 += x[3];  //cor

                    DRBE_obj_list[ind].mf_ant3 += x[4];  //cor

                    DRBE_obj_list[ind].mf_ant3 += x[16]; //ant

                    DRBE_obj_list[ind].bf_ant3 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //ant 3
                else if (pind == 4)
                {
                    DRBE_obj_list[ind].cf_ant4 += x[3];  //cor

                    DRBE_obj_list[ind].mf_ant4 += x[4];  //cor

                    DRBE_obj_list[ind].mf_ant4 += x[16]; //ant

                    DRBE_obj_list[ind].bf_ant4 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //ant 4
                else if (pind == 5)
                {
                    DRBE_obj_list[ind].cf_ant5 += x[3];  //cor

                    DRBE_obj_list[ind].mf_ant5 += x[4];  //cor

                    DRBE_obj_list[ind].mf_ant5 += x[16]; //ant

                    DRBE_obj_list[ind].bf_ant5 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //ant 5
                else if (pind == 6)
                {
                    DRBE_obj_list[ind].cp_ant_na_n += x[3];  //cor

                    DRBE_obj_list[ind].mp_ant_na_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_ant_na_n += x[16]; //ant


                    DRBE_obj_list[ind].bp_ant_na_n += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //na l
                else if (pind == 7)
                {
                    DRBE_obj_list[ind].cp_ant_na_p += x[3];  //cor


                    DRBE_obj_list[ind].mp_ant_na_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_ant_na_p += x[16]; //ant


                    DRBE_obj_list[ind].bp_ant_na_p += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor
                } //na u
                else if (pind == 8)
                {
                    DRBE_obj_list[ind].cp_ant_ar_n += x[3];  //cor


                    DRBE_obj_list[ind].mp_ant_ar_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_ant_ar_n += x[16]; //ant


                    DRBE_obj_list[ind].bp_ant_ar_n += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ar l
                else if (pind == 9)
                {
                    DRBE_obj_list[ind].cp_ant_ar_p += x[3];  //cor


                    DRBE_obj_list[ind].mp_ant_ar_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_ant_ar_p += x[16]; //ant


                    DRBE_obj_list[ind].bp_ant_ar_p += x[5];  //cor


                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ar u
                else if (pind == 10)
                {
                    DRBE_obj_list[ind].cp_ant_ds_n += x[3];  //cor


                    DRBE_obj_list[ind].mp_ant_ds_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_ant_ds_n += x[16]; //ant


                    DRBE_obj_list[ind].bp_ant_ds_n += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ds l
                else if (pind == 11)
                {
                    DRBE_obj_list[ind].cp_ant_ds_p += x[3];  //cor


                    DRBE_obj_list[ind].mp_ant_ds_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_ant_ds_p += x[16]; //ant


                    DRBE_obj_list[ind].bp_ant_ds_p += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ds u
                else if (pind == 12)
                {
                    DRBE_obj_list[ind].cf_rcs0 += x[3];  //cor


                    DRBE_obj_list[ind].mf_rcs0 += x[4];  //cor

                    DRBE_obj_list[ind].mf_rcs0 += x[16]; //ant


                    DRBE_obj_list[ind].bf_rcs0 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //rcs 0
                else if (pind == 13)
                {
                    DRBE_obj_list[ind].cf_rcs1 += x[3];  //cor


                    DRBE_obj_list[ind].mf_rcs1 += x[4];  //cor

                    DRBE_obj_list[ind].mf_rcs1 += x[16]; //ant


                    DRBE_obj_list[ind].bf_rcs1 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //rcs 1
                else if (pind == 14)
                {
                    DRBE_obj_list[ind].cf_rcs2 += x[3];  //cor


                    DRBE_obj_list[ind].mf_rcs2 += x[4];  //cor

                    DRBE_obj_list[ind].mf_rcs2 += x[16]; //ant

                    DRBE_obj_list[ind].bf_rcs2 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //rcs 2
                else if (pind == 15)
                {
                    DRBE_obj_list[ind].cf_rcs3 += x[3];  //cor

                    DRBE_obj_list[ind].mf_rcs3 += x[4];  //cor

                    DRBE_obj_list[ind].mf_rcs3 += x[16]; //ant

                    DRBE_obj_list[ind].bf_rcs3 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //rcs 3
                else if (pind == 16)
                {
                    DRBE_obj_list[ind].cf_rcs4 += x[3];  //cor


                    DRBE_obj_list[ind].mf_rcs4 += x[4];  //cor

                    DRBE_obj_list[ind].mf_rcs4 += x[16]; //ant


                    DRBE_obj_list[ind].bf_rcs4 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //rcs 4
                else if (pind == 17)
                {
                    DRBE_obj_list[ind].cf_rcs5 += x[3];  //cor


                    DRBE_obj_list[ind].mf_rcs5 += x[4];  //cor

                    DRBE_obj_list[ind].mf_rcs5 += x[16]; //ant


                    DRBE_obj_list[ind].bf_rcs5 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //rcs 5
                else if (pind == 18)
                {
                    DRBE_obj_list[ind].cf_rcs6 += x[3];  //cor


                    DRBE_obj_list[ind].mf_rcs6 += x[4];  //cor

                    DRBE_obj_list[ind].mf_rcs6 += x[16]; //ant


                    DRBE_obj_list[ind].bf_rcs6 += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //rcs 6
                else if (pind == 19)
                {
                    DRBE_obj_list[ind].cp_rcs_pn_n += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_pn_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_pn_n += x[16]; //ant


                    DRBE_obj_list[ind].bp_rcs_pn_n += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //pn l
                else if (pind == 20)
                {
                    DRBE_obj_list[ind].cp_rcs_pn_p += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_pn_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_pn_p += x[16]; //ant


                    DRBE_obj_list[ind].bp_rcs_pn_p += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //pn u
                else if (pind == 21)
                {
                    DRBE_obj_list[ind].cp_rcs_ss_n += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_ss_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_ss_n += x[16]; //ant


                    DRBE_obj_list[ind].bp_rcs_ss_n += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ss l
                else if (pind == 22)
                {
                    DRBE_obj_list[ind].cp_rcs_ss_p += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_ss_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_ss_p += x[16]; //ant


                    DRBE_obj_list[ind].bp_rcs_ss_p += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ss u
                else if (pind == 23)
                {
                    DRBE_obj_list[ind].cp_rcs_sp_n += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_sp_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_sp_n += x[16]; //ant


                    DRBE_obj_list[ind].bp_rcs_sp_n += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //sp l
                else if (pind == 24)
                {
                    DRBE_obj_list[ind].cp_rcs_sp_p += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_sp_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_sp_p += x[16]; //ant


                    DRBE_obj_list[ind].bp_rcs_sp_p += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //sp u
                else if (pind == 25)
                {
                    DRBE_obj_list[ind].cp_rcs_ar_n += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_ar_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_ar_n += x[16]; //ant


                    DRBE_obj_list[ind].bp_rcs_ar_n += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ar l
                else if (pind == 26)
                {
                    DRBE_obj_list[ind].cp_rcs_ar_p += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_ar_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_ar_p += x[16]; //ant


                    DRBE_obj_list[ind].bp_rcs_ar_p += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //ar u
                else if (pind == 27)
                {
                    DRBE_obj_list[ind].cp_rcs_fb_n += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_fb_n += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_fb_n += x[16]; //ant

                    DRBE_obj_list[ind].bp_rcs_fb_n += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //fb l
                else if (pind == 28)
                {
                    DRBE_obj_list[ind].cp_rcs_fb_p += x[3];  //cor


                    DRBE_obj_list[ind].mp_rcs_fb_p += x[4];  //cor

                    DRBE_obj_list[ind].mp_rcs_fb_p += x[16]; //ant


                    DRBE_obj_list[ind].bp_rcs_fb_p += x[5];  //cor

                    DRBE_obj_scan_latency_list[0][pind] = Math.Max(DRBE_obj_scan_latency_list[0][pind], x[6]);  //cor

                } //fb u
            }
        }
        private void Obj_scan_latency_special_update(int ind)
        {
            DRBE_obj_list[ind].lf_ant0 = DRBE_obj_scan_latency_list[0][0] + DRBE_obj_scan_latency_list[1][0] + DRBE_obj_scan_latency_list[2][0] + DRBE_obj_scan_latency_list[3][0] + DRBE_obj_scan_latency_list[4][0] + DRBE_obj_scan_latency_list[5][0] + DRBE_obj_scan_latency_list[6][0];
            DRBE_obj_list[ind].lf_ant1 = DRBE_obj_scan_latency_list[0][1] + DRBE_obj_scan_latency_list[1][1] + DRBE_obj_scan_latency_list[2][1] + DRBE_obj_scan_latency_list[3][1] + DRBE_obj_scan_latency_list[4][1] + DRBE_obj_scan_latency_list[5][1] + DRBE_obj_scan_latency_list[6][1];
            DRBE_obj_list[ind].lf_ant2 = DRBE_obj_scan_latency_list[0][2] + DRBE_obj_scan_latency_list[1][2] + DRBE_obj_scan_latency_list[2][2] + DRBE_obj_scan_latency_list[3][2] + DRBE_obj_scan_latency_list[4][2] + DRBE_obj_scan_latency_list[5][2] + DRBE_obj_scan_latency_list[6][2];
            DRBE_obj_list[ind].lf_ant3 = DRBE_obj_scan_latency_list[0][3] + DRBE_obj_scan_latency_list[1][3] + DRBE_obj_scan_latency_list[2][3] + DRBE_obj_scan_latency_list[3][3] + DRBE_obj_scan_latency_list[4][3] + DRBE_obj_scan_latency_list[5][3] + DRBE_obj_scan_latency_list[6][3];
            DRBE_obj_list[ind].lf_ant4 = DRBE_obj_scan_latency_list[0][4] + DRBE_obj_scan_latency_list[1][4] + DRBE_obj_scan_latency_list[2][4] + DRBE_obj_scan_latency_list[3][4] + DRBE_obj_scan_latency_list[4][4] + DRBE_obj_scan_latency_list[5][4] + DRBE_obj_scan_latency_list[6][4];
            DRBE_obj_list[ind].lf_ant5 = DRBE_obj_scan_latency_list[0][5] + DRBE_obj_scan_latency_list[1][5] + DRBE_obj_scan_latency_list[2][5] + DRBE_obj_scan_latency_list[3][5] + DRBE_obj_scan_latency_list[4][5] + DRBE_obj_scan_latency_list[5][5] + DRBE_obj_scan_latency_list[6][5];
            DRBE_obj_list[ind].lp_ant_na_n = DRBE_obj_scan_latency_list[0][6] + DRBE_obj_scan_latency_list[1][6] + DRBE_obj_scan_latency_list[2][6] + DRBE_obj_scan_latency_list[3][6] + DRBE_obj_scan_latency_list[4][6] + DRBE_obj_scan_latency_list[5][6] + DRBE_obj_scan_latency_list[6][6];
            DRBE_obj_list[ind].lp_ant_na_p = DRBE_obj_scan_latency_list[0][7] + DRBE_obj_scan_latency_list[1][7] + DRBE_obj_scan_latency_list[2][7] + DRBE_obj_scan_latency_list[3][7] + DRBE_obj_scan_latency_list[4][7] + DRBE_obj_scan_latency_list[5][7] + DRBE_obj_scan_latency_list[6][7];
            DRBE_obj_list[ind].lp_ant_ar_n = DRBE_obj_scan_latency_list[0][8] + DRBE_obj_scan_latency_list[1][8] + DRBE_obj_scan_latency_list[2][8] + DRBE_obj_scan_latency_list[3][8] + DRBE_obj_scan_latency_list[4][8] + DRBE_obj_scan_latency_list[5][8] + DRBE_obj_scan_latency_list[6][8];
            DRBE_obj_list[ind].lp_ant_ar_p = DRBE_obj_scan_latency_list[0][9] + DRBE_obj_scan_latency_list[1][9] + DRBE_obj_scan_latency_list[2][9] + DRBE_obj_scan_latency_list[3][9] + DRBE_obj_scan_latency_list[4][9] + DRBE_obj_scan_latency_list[5][9] + DRBE_obj_scan_latency_list[6][9];
            DRBE_obj_list[ind].lp_ant_ds_n = DRBE_obj_scan_latency_list[0][10] + DRBE_obj_scan_latency_list[1][10] + DRBE_obj_scan_latency_list[2][10] + DRBE_obj_scan_latency_list[3][10] + DRBE_obj_scan_latency_list[4][10] + DRBE_obj_scan_latency_list[5][10] + DRBE_obj_scan_latency_list[6][10];
            DRBE_obj_list[ind].lp_ant_ds_p = DRBE_obj_scan_latency_list[0][11] + DRBE_obj_scan_latency_list[1][11] + DRBE_obj_scan_latency_list[2][11] + DRBE_obj_scan_latency_list[3][11] + DRBE_obj_scan_latency_list[4][11] + DRBE_obj_scan_latency_list[5][11] + DRBE_obj_scan_latency_list[6][11];
            DRBE_obj_list[ind].lf_rcs0 = DRBE_obj_scan_latency_list[0][12] + DRBE_obj_scan_latency_list[1][12] + DRBE_obj_scan_latency_list[2][12] + DRBE_obj_scan_latency_list[3][12] + DRBE_obj_scan_latency_list[4][12] + DRBE_obj_scan_latency_list[5][12] + DRBE_obj_scan_latency_list[6][12];
            DRBE_obj_list[ind].lf_rcs1 = DRBE_obj_scan_latency_list[0][13] + DRBE_obj_scan_latency_list[1][13] + DRBE_obj_scan_latency_list[2][13] + DRBE_obj_scan_latency_list[3][13] + DRBE_obj_scan_latency_list[4][13] + DRBE_obj_scan_latency_list[5][13] + DRBE_obj_scan_latency_list[6][13];
            DRBE_obj_list[ind].lf_rcs2 = DRBE_obj_scan_latency_list[0][14] + DRBE_obj_scan_latency_list[1][14] + DRBE_obj_scan_latency_list[2][14] + DRBE_obj_scan_latency_list[3][14] + DRBE_obj_scan_latency_list[4][14] + DRBE_obj_scan_latency_list[5][14] + DRBE_obj_scan_latency_list[6][14];
            DRBE_obj_list[ind].lf_rcs3 = DRBE_obj_scan_latency_list[0][15] + DRBE_obj_scan_latency_list[1][15] + DRBE_obj_scan_latency_list[2][15] + DRBE_obj_scan_latency_list[3][15] + DRBE_obj_scan_latency_list[4][15] + DRBE_obj_scan_latency_list[5][15] + DRBE_obj_scan_latency_list[6][15];
            DRBE_obj_list[ind].lf_rcs4 = DRBE_obj_scan_latency_list[0][16] + DRBE_obj_scan_latency_list[1][16] + DRBE_obj_scan_latency_list[2][16] + DRBE_obj_scan_latency_list[3][16] + DRBE_obj_scan_latency_list[4][16] + DRBE_obj_scan_latency_list[5][16] + DRBE_obj_scan_latency_list[6][16];
            DRBE_obj_list[ind].lf_rcs5 = DRBE_obj_scan_latency_list[0][17] + DRBE_obj_scan_latency_list[1][17] + DRBE_obj_scan_latency_list[2][17] + DRBE_obj_scan_latency_list[3][17] + DRBE_obj_scan_latency_list[4][17] + DRBE_obj_scan_latency_list[5][17] + DRBE_obj_scan_latency_list[6][17];
            DRBE_obj_list[ind].lf_rcs6 = DRBE_obj_scan_latency_list[0][18] + DRBE_obj_scan_latency_list[1][18] + DRBE_obj_scan_latency_list[2][18] + DRBE_obj_scan_latency_list[3][18] + DRBE_obj_scan_latency_list[4][18] + DRBE_obj_scan_latency_list[5][18] + DRBE_obj_scan_latency_list[6][18];
            DRBE_obj_list[ind].lp_rcs_pn_n = DRBE_obj_scan_latency_list[0][19] + DRBE_obj_scan_latency_list[1][19] + DRBE_obj_scan_latency_list[2][19] + DRBE_obj_scan_latency_list[3][19] + DRBE_obj_scan_latency_list[4][19] + DRBE_obj_scan_latency_list[5][19] + DRBE_obj_scan_latency_list[6][19];
            DRBE_obj_list[ind].lp_rcs_pn_p = DRBE_obj_scan_latency_list[0][20] + DRBE_obj_scan_latency_list[1][20] + DRBE_obj_scan_latency_list[2][20] + DRBE_obj_scan_latency_list[3][20] + DRBE_obj_scan_latency_list[4][20] + DRBE_obj_scan_latency_list[5][20] + DRBE_obj_scan_latency_list[6][20];
            DRBE_obj_list[ind].lp_rcs_ss_n = DRBE_obj_scan_latency_list[0][21] + DRBE_obj_scan_latency_list[1][21] + DRBE_obj_scan_latency_list[2][21] + DRBE_obj_scan_latency_list[3][21] + DRBE_obj_scan_latency_list[4][21] + DRBE_obj_scan_latency_list[5][21] + DRBE_obj_scan_latency_list[6][21];
            DRBE_obj_list[ind].lp_rcs_ss_p = DRBE_obj_scan_latency_list[0][22] + DRBE_obj_scan_latency_list[1][22] + DRBE_obj_scan_latency_list[2][22] + DRBE_obj_scan_latency_list[3][22] + DRBE_obj_scan_latency_list[4][22] + DRBE_obj_scan_latency_list[5][22] + DRBE_obj_scan_latency_list[6][22];
            DRBE_obj_list[ind].lp_rcs_sp_n = DRBE_obj_scan_latency_list[0][23] + DRBE_obj_scan_latency_list[1][23] + DRBE_obj_scan_latency_list[2][23] + DRBE_obj_scan_latency_list[3][23] + DRBE_obj_scan_latency_list[4][23] + DRBE_obj_scan_latency_list[5][23] + DRBE_obj_scan_latency_list[6][23];
            DRBE_obj_list[ind].lp_rcs_sp_p = DRBE_obj_scan_latency_list[0][24] + DRBE_obj_scan_latency_list[1][24] + DRBE_obj_scan_latency_list[2][24] + DRBE_obj_scan_latency_list[3][24] + DRBE_obj_scan_latency_list[4][24] + DRBE_obj_scan_latency_list[5][24] + DRBE_obj_scan_latency_list[6][24];
            DRBE_obj_list[ind].lp_rcs_ar_n = DRBE_obj_scan_latency_list[0][25] + DRBE_obj_scan_latency_list[1][25] + DRBE_obj_scan_latency_list[2][25] + DRBE_obj_scan_latency_list[3][25] + DRBE_obj_scan_latency_list[4][25] + DRBE_obj_scan_latency_list[5][25] + DRBE_obj_scan_latency_list[6][25];
            DRBE_obj_list[ind].lp_rcs_ar_p = DRBE_obj_scan_latency_list[0][26] + DRBE_obj_scan_latency_list[1][26] + DRBE_obj_scan_latency_list[2][26] + DRBE_obj_scan_latency_list[3][26] + DRBE_obj_scan_latency_list[4][26] + DRBE_obj_scan_latency_list[5][26] + DRBE_obj_scan_latency_list[6][26];
            DRBE_obj_list[ind].lp_rcs_fb_n = DRBE_obj_scan_latency_list[0][27] + DRBE_obj_scan_latency_list[1][27] + DRBE_obj_scan_latency_list[2][27] + DRBE_obj_scan_latency_list[3][27] + DRBE_obj_scan_latency_list[4][27] + DRBE_obj_scan_latency_list[5][27] + DRBE_obj_scan_latency_list[6][27];
            DRBE_obj_list[ind].lp_rcs_fb_p = DRBE_obj_scan_latency_list[0][28] + DRBE_obj_scan_latency_list[1][28] + DRBE_obj_scan_latency_list[2][28] + DRBE_obj_scan_latency_list[3][28] + DRBE_obj_scan_latency_list[4][28] + DRBE_obj_scan_latency_list[5][28] + DRBE_obj_scan_latency_list[6][28];
        }
        private async Task Scan_obj_info_property(int ind)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            bool ist = false;
            bool iso = false;
            bool isr = false;
            Scan_obj_refresh(ind);
            Get_total_scan_number(0);
            Scan_max_obj = DRBE_obj_list[ind].number_of_path;
            Scan_pb.Maximum = Scan_max_obj;
            Scan_cur_obj = 0;
            int objit = -1;
            int objio = -1;
            int objir = -1;
            int ti = 0;
            #region create scan list
            List<double> original = new List<double>();
            List<double> upperbound = new List<double>();
            List<double> lowerbound = new List<double>();


            List<double> result = new List<double>();

            original.Add((double)5 / 1000000);
            original.Add(1);
            original.Add(1);
            original.Add((double)1 / 1000);

            original.Add(DRBE_obj_list[ind].Interpolation_order);
            original.Add(DRBE_obj_list[ind].Convergence);

            original.Add(DRBE_obj_list[ind].Antenna_order);
            original.Add(DRBE_obj_list[ind].Number_Antenna_AZ * DRBE_obj_list[ind].Number_Antenna_EL);
            original.Add(DRBE_obj_list[ind].Resolution_AZ);
            original.Add(DRBE_obj_list[ind].Dictionary_dimension);

            original.Add(DRBE_obj_list[ind].RCS_order);
            original.Add(DRBE_obj_list[ind].RCS_point);
            original.Add(DRBE_obj_list[ind].RCS_angle_resolution);
            original.Add(DRBE_obj_list[ind].RCS_frequency_point);
            original.Add(DRBE_obj_list[ind].RCS_number_of_polarization);
            original.Add(DRBE_obj_list[ind].RCS_output_time_sampe);

            if(DRBE_obj_list[ind].Antenna_order ==0)
            {
                FANT0_l_tb.Foreground = green_bright_button_brush;
            }else if(DRBE_obj_list[ind].Antenna_order == 1)
            {
                FANT1_l_tb.Foreground = green_bright_button_brush;
            }
            else if (DRBE_obj_list[ind].Antenna_order == 2)
            {
                FANT2_l_tb.Foreground = green_bright_button_brush;
            }
            else if (DRBE_obj_list[ind].Antenna_order == 3)
            {
                FANT3_l_tb.Foreground = green_bright_button_brush;
            }
            else if (DRBE_obj_list[ind].Antenna_order == 4)
            {
                FANT4_l_tb.Foreground = green_bright_button_brush;
            }
            else if (DRBE_obj_list[ind].Antenna_order == 5)
            {
                FANT5_l_tb.Foreground = green_bright_button_brush;
            }

            if (DRBE_obj_list[ind].RCS_order == 0)
            {
                FRCS0_l_tb.Foreground = green_bright_button_brush;
            }
            else if (DRBE_obj_list[ind].RCS_order == 1)
            {
                FRCS1_l_tb.Foreground = green_bright_button_brush;
            }
            else if (DRBE_obj_list[ind].RCS_order == 2)
            {
                FRCS2_l_tb.Foreground = green_bright_button_brush;
            }
            else if (DRBE_obj_list[ind].RCS_order == 3)
            {
                FRCS3_l_tb.Foreground = green_bright_button_brush;
            }
            else if (DRBE_obj_list[ind].RCS_order == 4)
            {
                FRCS4_l_tb.Foreground = green_bright_button_brush;
            }
            else if (DRBE_obj_list[ind].RCS_order == 5)
            {
                FRCS5_l_tb.Foreground = green_bright_button_brush;
            }
            else if (DRBE_obj_list[ind].RCS_order == 6)
            {
                FRCS6_l_tb.Foreground = green_bright_button_brush;
            }


            lowerbound.Add((double)((int)(original[7] * 0.8)));
            upperbound.Add((double)((int)(original[7] * 1.2)));
            lowerbound.Add(original[8] * 1.2);
            upperbound.Add(original[8] * 0.8);
            lowerbound.Add((double)((int)(original[9] * 0.8)));
            upperbound.Add((double)((int)(original[9] * 1.2)));


            lowerbound.Add((double)((int)(original[11] * 0.8)));
            upperbound.Add((double)((int)(original[11] * 1.2)));
            lowerbound.Add(original[12] * 1.2);
            upperbound.Add(original[12] * 0.8);

            lowerbound.Add(original[13] * 0.8);
            upperbound.Add(original[13] * 1.2);

            if (original[14] > 1)
            {
                lowerbound.Add(original[14] - 1);
            }
            else
            {
                lowerbound.Add(original[14]);
            }

            if (original[14] < 4)
            {
                upperbound.Add(original[14] + 1);
            }
            else
            {
                upperbound.Add(original[14]);
            }

            lowerbound.Add(original[15] * 0.8);
            upperbound.Add(original[15] * 1.2);

            PANT_NA_TL_tb.Text = lowerbound[0].ToString("F2") + " -- " + original[7].ToString("F2");
            PANT_NA_TR_tb.Text = original[7].ToString("F2") + " -- " + upperbound[0].ToString("F2");

            PANT_AR_TL_tb.Text = lowerbound[1].ToString("F2") + " -- " + original[8].ToString("F2");
            PANT_AR_TR_tb.Text = original[8].ToString("F2") + " -- " + upperbound[1].ToString("F2");

            PANT_DS_TL_tb.Text = lowerbound[2].ToString("F2") + " -- " + original[9].ToString("F2");
            PANT_DS_TR_tb.Text = original[9].ToString("F2") + " -- " + upperbound[2].ToString("F2");

            PRCS_SP_TL_tb.Text = lowerbound[3].ToString("F2") + " -- " + original[11].ToString("F2");
            PRCS_SP_TR_tb.Text = original[11].ToString("F2") + " -- " + upperbound[3].ToString("F2");

            PRCS_AR_TL_tb.Text = lowerbound[4].ToString("F2") + " -- " + original[12].ToString("F2");
            PRCS_AR_TR_tb.Text = original[12].ToString("F2") + " -- " + upperbound[4].ToString("F2");

            PRCS_FB_TL_tb.Text = lowerbound[5].ToString("F2") + " -- " + original[13].ToString("F2");
            PRCS_FB_TR_tb.Text = original[13].ToString("F2") + " -- " + upperbound[5].ToString("F2");

            PRCS_PN_TL_tb.Text = lowerbound[6].ToString("F2") + " -- " + original[14].ToString("F2");
            PRCS_PN_TR_tb.Text = original[14].ToString("F2") + " -- " + upperbound[6].ToString("F2");

            PRCS_SS_TL_tb.Text = lowerbound[7].ToString("F2") + " -- " + original[15].ToString("F2");
            PRCS_SS_TR_tb.Text = original[15].ToString("F2") + " -- " + upperbound[7].ToString("F2");

            #endregion

            if (Dic_i_t_obj.ContainsKey(DRBE_obj_list[ind]))
            {
                objit = Dic_i_t_obj[DRBE_obj_list[ind]];
            }

            if (Dic_i_o_obj.ContainsKey(DRBE_obj_list[ind]))
            {
                objio = Dic_i_o_obj[DRBE_obj_list[ind]];
            }

            if (Dic_i_r_obj.ContainsKey(DRBE_obj_list[ind]))
            {
                objir = Dic_i_r_obj[DRBE_obj_list[ind]];
            }

            int i = 0;
            int ii = 0;
            int iii = 0;
            int mode = 0;

            List<double> Tosend = new List<double>();
            i = 0;
            while (i < Link_list.Count)
            {
                ii = 0;
                while (ii < Link_list[i].Count)
                {
                    iii = 0;
                    while (iii < Link_list[i][ii].Count)
                    {
                        if (Link_list[i][ii][iii])
                        {
                            if(i == objit)
                            {
                                ist = true;
                                mode = 0;
                                Tosend = new List<double>(original);
                                Tosend[6] = 0;
                                await Scan_obj_update(await Transceive(Tosend), ind, 0, mode);  //ant 0
                                Tosend[6] = 1;
                                await Scan_obj_update(await Transceive(Tosend), ind, 1, mode);  //ant 1
                                Tosend[6] = 2;
                                await Scan_obj_update(await Transceive(Tosend), ind, 2, mode);  //ant 2
                                Tosend[6] = 3;
                                await Scan_obj_update(await Transceive(Tosend), ind, 3, mode);  //ant 3
                                Tosend[6] = 4;
                                await Scan_obj_update(await Transceive(Tosend), ind, 4, mode);  //ant 4
                                Tosend[6] = 5;
                                await Scan_obj_update(await Transceive(Tosend), ind, 5, mode);  //ant 5
                                Tosend = new List<double>(original);
                                Tosend[7] = lowerbound[0];
                                await Scan_obj_update(await Transceive(Tosend), ind, 6, mode);  //na l
                                Tosend[7] = upperbound[0];
                                await Scan_obj_update(await Transceive(Tosend), ind, 7, mode);  //na u
                                Tosend = new List<double>(original);
                                Tosend[8] = lowerbound[1];
                                await Scan_obj_update(await Transceive(Tosend), ind, 8, mode);  //ar l
                                Tosend[8] = upperbound[1];
                                await Scan_obj_update(await Transceive(Tosend), ind, 9, mode);  //ar u
                                Tosend = new List<double>(original);
                                Tosend[9] = lowerbound[2];
                                await Scan_obj_update(await Transceive(Tosend), ind, 10, mode);  //ds l
                                Tosend[9] = upperbound[2];
                                await Scan_obj_update(await Transceive(Tosend), ind, 11, mode);  //ds u
                                Tosend = new List<double>(original);
                                Tosend[10] = 0;
                                await Scan_obj_update(await Transceive(Tosend), ind, 12, mode);  //rcs 0
                                Tosend[10] = 1;
                                await Scan_obj_update(await Transceive(Tosend), ind, 13, mode);  //rcs 1
                                Tosend[10] = 2;
                                await Scan_obj_update(await Transceive(Tosend), ind, 14, mode);  //rcs 2
                                Tosend[10] = 3;
                                await Scan_obj_update(await Transceive(Tosend), ind, 15, mode);  //rcs 3
                                Tosend[10] = 4;
                                await Scan_obj_update(await Transceive(Tosend), ind, 16, mode);  //rcs 4
                                Tosend[10] = 5;
                                await Scan_obj_update(await Transceive(Tosend), ind, 17, mode);  //rcs 5
                                Tosend[10] = 6;
                                await Scan_obj_update(await Transceive(Tosend), ind, 18, mode);  //rcs 6


                                Tosend = new List<double>(original);
                                Tosend[14] = lowerbound[6];
                                await Scan_obj_update(await Transceive(Tosend), ind, 19, mode);  //pn l
                                Tosend[14] = upperbound[6];
                                await Scan_obj_update(await Transceive(Tosend), ind, 20, mode);  //pn u
                                Tosend = new List<double>(original);
                                Tosend[15] = lowerbound[7];
                                await Scan_obj_update(await Transceive(Tosend), ind, 21, mode);  //ss l
                                Tosend[15] = upperbound[7];
                                await Scan_obj_update(await Transceive(Tosend), ind, 22, mode);  //ss u
                                Tosend = new List<double>(original);
                                Tosend[11] = lowerbound[3];
                                await Scan_obj_update(await Transceive(Tosend), ind, 23, mode);  //sp l
                                Tosend[11] = upperbound[3];
                                await Scan_obj_update(await Transceive(Tosend), ind, 24, mode);  //sp u
                                Tosend = new List<double>(original);
                                Tosend[12] = lowerbound[4];
                                await Scan_obj_update(await Transceive(Tosend), ind, 25, mode);  //ar l
                                Tosend[12] = upperbound[4];
                                await Scan_obj_update(await Transceive(Tosend), ind, 26, mode);  //ar u
                                Tosend = new List<double>(original);
                                Tosend[13] = lowerbound[5];
                                await Scan_obj_update(await Transceive(Tosend), ind, 27, mode);  //fb l
                                Tosend[13] = upperbound[5];
                                await Scan_obj_update(await Transceive(Tosend), ind, 28, mode);  //fb u
                                Scan_cur_obj++;
                                await Scan_obj_presentation_update(ind);
                            }

                            if (ii == objio)
                            {
                                iso = true;
                                mode = 1;
                                Tosend = new List<double>(original);
                                Tosend[6] = 0;
                                await Scan_obj_update(await Transceive(Tosend), ind, 0, mode);  //ant 0
                                Tosend[6] = 1;
                                await Scan_obj_update(await Transceive(Tosend), ind, 1, mode);  //ant 1
                                Tosend[6] = 2;
                                await Scan_obj_update(await Transceive(Tosend), ind, 2, mode);  //ant 2
                                Tosend[6] = 3;
                                await Scan_obj_update(await Transceive(Tosend), ind, 3, mode);  //ant 3
                                Tosend[6] = 4;
                                await Scan_obj_update(await Transceive(Tosend), ind, 4, mode);  //ant 4
                                Tosend[6] = 5;
                                await Scan_obj_update(await Transceive(Tosend), ind, 5, mode);  //ant 5
                                Tosend = new List<double>(original);
                                Tosend[7] = lowerbound[0];
                                await Scan_obj_update(await Transceive(Tosend), ind, 6, mode);  //na l
                                Tosend[7] = upperbound[0];
                                await Scan_obj_update(await Transceive(Tosend), ind, 7, mode);  //na u
                                Tosend = new List<double>(original);
                                Tosend[8] = lowerbound[1];
                                await Scan_obj_update(await Transceive(Tosend), ind, 8, mode);  //ar l
                                Tosend[8] = upperbound[1];
                                await Scan_obj_update(await Transceive(Tosend), ind, 9, mode);  //ar u
                                Tosend = new List<double>(original);
                                Tosend[9] = lowerbound[2];
                                await Scan_obj_update(await Transceive(Tosend), ind, 10, mode);  //ds l
                                Tosend[9] = upperbound[2];
                                await Scan_obj_update(await Transceive(Tosend), ind, 11, mode);  //ds u
                                Tosend = new List<double>(original);
                                Tosend[10] = 0;
                                await Scan_obj_update(await Transceive(Tosend), ind, 12, mode);  //rcs 0
                                Tosend[10] = 1;
                                await Scan_obj_update(await Transceive(Tosend), ind, 13, mode);  //rcs 1
                                Tosend[10] = 2;
                                await Scan_obj_update(await Transceive(Tosend), ind, 14, mode);  //rcs 2
                                Tosend[10] = 3;
                                await Scan_obj_update(await Transceive(Tosend), ind, 15, mode);  //rcs 3
                                Tosend[10] = 4;
                                await Scan_obj_update(await Transceive(Tosend), ind, 16, mode);  //rcs 4
                                Tosend[10] = 5;
                                await Scan_obj_update(await Transceive(Tosend), ind, 17, mode);  //rcs 5
                                Tosend[10] = 6;
                                await Scan_obj_update(await Transceive(Tosend), ind, 18, mode);  //rcs 5


                                Tosend = new List<double>(original);
                                Tosend[14] = lowerbound[6];
                                await Scan_obj_update(await Transceive(Tosend), ind, 19, mode);  //pn l
                                Tosend[14] = upperbound[6];
                                await Scan_obj_update(await Transceive(Tosend), ind, 20, mode);  //pn u
                                Tosend = new List<double>(original);
                                Tosend[15] = lowerbound[7];
                                await Scan_obj_update(await Transceive(Tosend), ind, 21, mode);  //ss l
                                Tosend[15] = upperbound[7];
                                await Scan_obj_update(await Transceive(Tosend), ind, 22, mode);  //ss u
                                Tosend = new List<double>(original);
                                Tosend[11] = lowerbound[3];
                                await Scan_obj_update(await Transceive(Tosend), ind, 23, mode);  //sp l
                                Tosend[11] = upperbound[3];
                                await Scan_obj_update(await Transceive(Tosend), ind, 24, mode);  //sp u
                                Tosend = new List<double>(original);
                                Tosend[12] = lowerbound[4];
                                await Scan_obj_update(await Transceive(Tosend), ind, 25, mode);  //ar l
                                Tosend[12] = upperbound[4];
                                await Scan_obj_update(await Transceive(Tosend), ind, 26, mode);  //ar u
                                Tosend = new List<double>(original);
                                Tosend[13] = lowerbound[5];
                                await Scan_obj_update(await Transceive(Tosend), ind, 27, mode);  //fb l
                                Tosend[13] = upperbound[5];
                                await Scan_obj_update(await Transceive(Tosend), ind, 28, mode);  //fb u
                                Scan_cur_obj++;
                                await Scan_obj_presentation_update(ind);
                            }

                            if (iii == objir)
                            {
                                isr = true;
                                mode = 2;
                                Tosend = new List<double>(original);
                                Tosend[6] = 0;
                                await Scan_obj_update(await Transceive(Tosend), ind, 0, mode);  //ant 0
                                Tosend[6] = 1;
                                await Scan_obj_update(await Transceive(Tosend), ind, 1, mode);  //ant 1
                                Tosend[6] = 2;
                                await Scan_obj_update(await Transceive(Tosend), ind, 2, mode);  //ant 2
                                Tosend[6] = 3;
                                await Scan_obj_update(await Transceive(Tosend), ind, 3, mode);  //ant 3
                                Tosend[6] = 4;
                                await Scan_obj_update(await Transceive(Tosend), ind, 4, mode);  //ant 4
                                Tosend[6] = 5;
                                await Scan_obj_update(await Transceive(Tosend), ind, 5, mode);  //ant 5
                                Tosend = new List<double>(original);
                                Tosend[7] = lowerbound[0];
                                await Scan_obj_update(await Transceive(Tosend), ind, 6, mode);  //na l
                                Tosend[7] = upperbound[0];
                                await Scan_obj_update(await Transceive(Tosend), ind, 7, mode);  //na u
                                Tosend = new List<double>(original);
                                Tosend[8] = lowerbound[1];
                                await Scan_obj_update(await Transceive(Tosend), ind, 8, mode);  //ar l
                                Tosend[8] = upperbound[1];
                                await Scan_obj_update(await Transceive(Tosend), ind, 9, mode);  //ar u
                                Tosend = new List<double>(original);
                                Tosend[9] = lowerbound[2];
                                await Scan_obj_update(await Transceive(Tosend), ind, 10, mode);  //ds l
                                Tosend[9] = upperbound[2];
                                await Scan_obj_update(await Transceive(Tosend), ind, 11, mode);  //ds u
                                Tosend = new List<double>(original);
                                Tosend[10] = 0;
                                await Scan_obj_update(await Transceive(Tosend), ind, 12, mode);  //rcs 0
                                Tosend[10] = 1;
                                await Scan_obj_update(await Transceive(Tosend), ind, 13, mode);  //rcs 1
                                Tosend[10] = 2;
                                await Scan_obj_update(await Transceive(Tosend), ind, 14, mode);  //rcs 2
                                Tosend[10] = 3;
                                await Scan_obj_update(await Transceive(Tosend), ind, 15, mode);  //rcs 3
                                Tosend[10] = 4;
                                await Scan_obj_update(await Transceive(Tosend), ind, 16, mode);  //rcs 4
                                Tosend[10] = 5;
                                await Scan_obj_update(await Transceive(Tosend), ind, 17, mode);  //rcs 5
                                Tosend[10] = 6;
                                await Scan_obj_update(await Transceive(Tosend), ind, 18, mode);  //rcs 5


                                Tosend = new List<double>(original);
                                Tosend[14] = lowerbound[6];
                                await Scan_obj_update(await Transceive(Tosend), ind, 19, mode);  //pn l
                                Tosend[14] = upperbound[6];
                                await Scan_obj_update(await Transceive(Tosend), ind, 20, mode);  //pn u
                                Tosend = new List<double>(original);
                                Tosend[15] = lowerbound[7];
                                await Scan_obj_update(await Transceive(Tosend), ind, 21, mode);  //ss l
                                Tosend[15] = upperbound[7];
                                await Scan_obj_update(await Transceive(Tosend), ind, 22, mode);  //ss u
                                Tosend = new List<double>(original);
                                Tosend[11] = lowerbound[3];
                                await Scan_obj_update(await Transceive(Tosend), ind, 23, mode);  //sp l
                                Tosend[11] = upperbound[3];
                                await Scan_obj_update(await Transceive(Tosend), ind, 24, mode);  //sp u
                                Tosend = new List<double>(original);
                                Tosend[12] = lowerbound[4];
                                await Scan_obj_update(await Transceive(Tosend), ind, 25, mode);  //ar l
                                Tosend[12] = upperbound[4];
                                await Scan_obj_update(await Transceive(Tosend), ind, 26, mode);  //ar u
                                Tosend = new List<double>(original);
                                Tosend[13] = lowerbound[5];
                                await Scan_obj_update(await Transceive(Tosend), ind, 27, mode);  //fb l
                                Tosend[13] = upperbound[5];
                                await Scan_obj_update(await Transceive(Tosend), ind, 28, mode);  //fb u
                                Scan_cur_obj++;

                                await Scan_obj_presentation_update(ind);
                            }

                        }
                        iii++;
                    }
                    ii++;
                }

                i++;
            }


            if (ist)
            {
                mode = 0;
                Tosend = new List<double>(original);
                Tosend[6] = 0;
                Scan_obj_special_update(await Transceive(Tosend), ind, 0, mode);  //ant 0
                Tosend[6] = 1;
                Scan_obj_special_update(await Transceive(Tosend), ind, 1, mode);  //ant 1
                Tosend[6] = 2;
                Scan_obj_special_update(await Transceive(Tosend), ind, 2, mode);  //ant 2
                Tosend[6] = 3;
                Scan_obj_special_update(await Transceive(Tosend), ind, 3, mode);  //ant 3
                Tosend[6] = 4;
                Scan_obj_special_update(await Transceive(Tosend), ind, 4, mode);  //ant 4
                Tosend[6] = 5;
                Scan_obj_special_update(await Transceive(Tosend), ind, 5, mode);  //ant 5
                Tosend = new List<double>(original);
                Tosend[7] = lowerbound[0];
                Scan_obj_special_update(await Transceive(Tosend), ind, 6, mode);  //na l
                Tosend[7] = upperbound[0];
                Scan_obj_special_update(await Transceive(Tosend), ind, 7, mode);  //na u
                Tosend = new List<double>(original);
                Tosend[8] = lowerbound[1];
                Scan_obj_special_update(await Transceive(Tosend), ind, 8, mode);  //ar l
                Tosend[8] = upperbound[1];
                Scan_obj_special_update(await Transceive(Tosend), ind, 9, mode);  //ar u
                Tosend = new List<double>(original);
                Tosend[9] = lowerbound[2];
                Scan_obj_special_update(await Transceive(Tosend), ind, 10, mode);  //ds l
                Tosend[9] = upperbound[2];
                Scan_obj_special_update(await Transceive(Tosend), ind, 11, mode);  //ds u
                Tosend = new List<double>(original);
                Tosend[10] = 0;
                Scan_obj_special_update(await Transceive(Tosend), ind, 12, mode);  //rcs 0
                Tosend[10] = 1;
                Scan_obj_special_update(await Transceive(Tosend), ind, 13, mode);  //rcs 1
                Tosend[10] = 2;
                Scan_obj_special_update(await Transceive(Tosend), ind, 14, mode);  //rcs 2
                Tosend[10] = 3;
                Scan_obj_special_update(await Transceive(Tosend), ind, 15, mode);  //rcs 3
                Tosend[10] = 4;
                Scan_obj_special_update(await Transceive(Tosend), ind, 16, mode);  //rcs 4
                Tosend[10] = 5;
                Scan_obj_special_update(await Transceive(Tosend), ind, 17, mode);  //rcs 5
                Tosend[10] = 6;
                Scan_obj_special_update(await Transceive(Tosend), ind, 18, mode);  //rcs 5

                Tosend = new List<double>(original);
                Tosend[14] = lowerbound[6];
                Scan_obj_special_update(await Transceive(Tosend), ind, 19, mode);  //pn l
                Tosend[14] = upperbound[6];
                Scan_obj_special_update(await Transceive(Tosend), ind, 20, mode);  //pn u
                Tosend = new List<double>(original);
                Tosend[15] = lowerbound[7];
                Scan_obj_special_update(await Transceive(Tosend), ind, 21, mode);  //ss l
                Tosend[15] = upperbound[7];
                Scan_obj_special_update(await Transceive(Tosend), ind, 22, mode);  //ss u
                Tosend = new List<double>(original);
                Tosend[11] = lowerbound[3];
                Scan_obj_special_update(await Transceive(Tosend), ind, 23, mode);  //sp l
                Tosend[11] = upperbound[3];
                Scan_obj_special_update(await Transceive(Tosend), ind, 24, mode);  //sp u
                Tosend = new List<double>(original);
                Tosend[12] = lowerbound[4];
                Scan_obj_special_update(await Transceive(Tosend), ind, 25, mode);  //ar l
                Tosend[12] = upperbound[4];
                Scan_obj_special_update(await Transceive(Tosend), ind, 26, mode);  //ar u
                Tosend = new List<double>(original);
                Tosend[13] = lowerbound[5];
                Scan_obj_special_update(await Transceive(Tosend), ind, 27, mode);  //fb l
                Tosend[13] = upperbound[5];
                Scan_obj_special_update(await Transceive(Tosend), ind, 28, mode);  //fb u

                Scan_pb.Maximum += 1;
                Scan_max_obj++;
                Scan_cur_obj++;
                await Scan_obj_presentation_update(ind);
            }

            if (iso)
            {

                mode = 1;
                Tosend = new List<double>(original);
                Tosend[6] = 0;
                Scan_obj_special_update(await Transceive(Tosend), ind, 0, mode);  //ant 0
                Tosend[6] = 1;
                Scan_obj_special_update(await Transceive(Tosend), ind, 1, mode);  //ant 1
                Tosend[6] = 2;
                Scan_obj_special_update(await Transceive(Tosend), ind, 2, mode);  //ant 2
                Tosend[6] = 3;
                Scan_obj_special_update(await Transceive(Tosend), ind, 3, mode);  //ant 3
                Tosend[6] = 4;
                Scan_obj_special_update(await Transceive(Tosend), ind, 4, mode);  //ant 4
                Tosend[6] = 5;
                Scan_obj_special_update(await Transceive(Tosend), ind, 5, mode);  //ant 5
                Tosend = new List<double>(original);
                Tosend[7] = lowerbound[0];
                Scan_obj_special_update(await Transceive(Tosend), ind, 6, mode);  //na l
                Tosend[7] = upperbound[0];
                Scan_obj_special_update(await Transceive(Tosend), ind, 7, mode);  //na u
                Tosend = new List<double>(original);
                Tosend[8] = lowerbound[1];
                Scan_obj_special_update(await Transceive(Tosend), ind, 8, mode);  //ar l
                Tosend[8] = upperbound[1];
                Scan_obj_special_update(await Transceive(Tosend), ind, 9, mode);  //ar u
                Tosend = new List<double>(original);
                Tosend[9] = lowerbound[2];
                Scan_obj_special_update(await Transceive(Tosend), ind, 10, mode);  //ds l
                Tosend[9] = upperbound[2];
                Scan_obj_special_update(await Transceive(Tosend), ind, 11, mode);  //ds u
                Tosend = new List<double>(original);
                Tosend[10] = 0;
                Scan_obj_special_update(await Transceive(Tosend), ind, 12, mode);  //rcs 0
                Tosend[10] = 1;
                Scan_obj_special_update(await Transceive(Tosend), ind, 13, mode);  //rcs 1
                Tosend[10] = 2;
                Scan_obj_special_update(await Transceive(Tosend), ind, 14, mode);  //rcs 2
                Tosend[10] = 3;
                Scan_obj_special_update(await Transceive(Tosend), ind, 15, mode);  //rcs 3
                Tosend[10] = 4;
                Scan_obj_special_update(await Transceive(Tosend), ind, 16, mode);  //rcs 4
                Tosend[10] = 5;
                Scan_obj_special_update(await Transceive(Tosend), ind, 17, mode);  //rcs 5
                Tosend[10] = 6;
                Scan_obj_special_update(await Transceive(Tosend), ind, 18, mode);  //rcs 5


                Tosend = new List<double>(original);
                Tosend[14] = lowerbound[6];
                Scan_obj_special_update(await Transceive(Tosend), ind, 19, mode);  //pn l
                Tosend[14] = upperbound[6];
                Scan_obj_special_update(await Transceive(Tosend), ind, 20, mode);  //pn u
                Tosend = new List<double>(original);
                Tosend[15] = lowerbound[7];
                Scan_obj_special_update(await Transceive(Tosend), ind, 21, mode);  //ss l
                Tosend[15] = upperbound[7];
                Scan_obj_special_update(await Transceive(Tosend), ind, 22, mode);  //ss u
                Tosend = new List<double>(original);
                Tosend[11] = lowerbound[3];
                Scan_obj_special_update(await Transceive(Tosend), ind, 23, mode);  //sp l
                Tosend[11] = upperbound[3];
                Scan_obj_special_update(await Transceive(Tosend), ind, 24, mode);  //sp u
                Tosend = new List<double>(original);
                Tosend[12] = lowerbound[4];
                Scan_obj_special_update(await Transceive(Tosend), ind, 25, mode);  //ar l
                Tosend[12] = upperbound[4];
                Scan_obj_special_update(await Transceive(Tosend), ind, 26, mode);  //ar u
                Tosend = new List<double>(original);
                Tosend[13] = lowerbound[5];
                Scan_obj_special_update(await Transceive(Tosend), ind, 27, mode);  //fb l
                Tosend[13] = upperbound[5];
                Scan_obj_special_update(await Transceive(Tosend), ind, 28, mode);  //fb u
                Scan_pb.Maximum += 1;
                Scan_max_obj++;
                Scan_cur_obj++;
                await Scan_obj_presentation_update(ind);
            }

            if (isr)
            {
                mode = 2;
                Tosend = new List<double>(original);
                Tosend[6] = 0;
                Scan_obj_special_update(await Transceive(Tosend), ind, 0, mode);  //ant 0
                Tosend[6] = 1;
                Scan_obj_special_update(await Transceive(Tosend), ind, 1, mode);  //ant 1
                Tosend[6] = 2;
                Scan_obj_special_update(await Transceive(Tosend), ind, 2, mode);  //ant 2
                Tosend[6] = 3;
                Scan_obj_special_update(await Transceive(Tosend), ind, 3, mode);  //ant 3
                Tosend[6] = 4;
                Scan_obj_special_update(await Transceive(Tosend), ind, 4, mode);  //ant 4
                Tosend[6] = 5;
                Scan_obj_special_update(await Transceive(Tosend), ind, 5, mode);  //ant 5
                Tosend = new List<double>(original);
                Tosend[7] = lowerbound[0];
                Scan_obj_special_update(await Transceive(Tosend), ind, 6, mode);  //na l
                Tosend[7] = upperbound[0];
                Scan_obj_special_update(await Transceive(Tosend), ind, 7, mode);  //na u
                Tosend = new List<double>(original);
                Tosend[8] = lowerbound[1];
                Scan_obj_special_update(await Transceive(Tosend), ind, 8, mode);  //ar l
                Tosend[8] = upperbound[1];
                Scan_obj_special_update(await Transceive(Tosend), ind, 9, mode);  //ar u
                Tosend = new List<double>(original);
                Tosend[9] = lowerbound[2];
                Scan_obj_special_update(await Transceive(Tosend), ind, 10, mode);  //ds l
                Tosend[9] = upperbound[2];
                Scan_obj_special_update(await Transceive(Tosend), ind, 11, mode);  //ds u
                Tosend = new List<double>(original);
                Tosend[10] = 0;
                Scan_obj_special_update(await Transceive(Tosend), ind, 12, mode);  //rcs 0
                Tosend[10] = 1;
                Scan_obj_special_update(await Transceive(Tosend), ind, 13, mode);  //rcs 1
                Tosend[10] = 2;
                Scan_obj_special_update(await Transceive(Tosend), ind, 14, mode);  //rcs 2
                Tosend[10] = 3;
                Scan_obj_special_update(await Transceive(Tosend), ind, 15, mode);  //rcs 3
                Tosend[10] = 4;
                Scan_obj_special_update(await Transceive(Tosend), ind, 16, mode);  //rcs 4
                Tosend[10] = 5;
                Scan_obj_special_update(await Transceive(Tosend), ind, 17, mode);  //rcs 5
                Tosend[10] = 6;
                Scan_obj_special_update(await Transceive(Tosend), ind, 18, mode);  //rcs 5

                Tosend = new List<double>(original);
                Tosend[14] = lowerbound[6];
                Scan_obj_special_update(await Transceive(Tosend), ind, 19, mode);  //pn l
                Tosend[14] = upperbound[6];
                Scan_obj_special_update(await Transceive(Tosend), ind, 20, mode);  //pn u
                Tosend = new List<double>(original);
                Tosend[15] = lowerbound[7];
                Scan_obj_special_update(await Transceive(Tosend), ind, 21, mode);  //ss l
                Tosend[15] = upperbound[7];
                Scan_obj_special_update(await Transceive(Tosend), ind, 22, mode);  //ss u
                Tosend = new List<double>(original);
                Tosend[11] = lowerbound[3];
                Scan_obj_special_update(await Transceive(Tosend), ind, 23, mode);  //sp l
                Tosend[11] = upperbound[3];
                Scan_obj_special_update(await Transceive(Tosend), ind, 24, mode);  //sp u
                Tosend = new List<double>(original);
                Tosend[12] = lowerbound[4];
                Scan_obj_special_update(await Transceive(Tosend), ind, 25, mode);  //ar l
                Tosend[12] = upperbound[4];
                Scan_obj_special_update(await Transceive(Tosend), ind, 26, mode);  //ar u
                Tosend = new List<double>(original);
                Tosend[13] = lowerbound[5];
                Scan_obj_special_update(await Transceive(Tosend), ind, 27, mode);  //fb l
                Tosend[13] = upperbound[5];
                Scan_obj_special_update(await Transceive(Tosend), ind, 28, mode);  //fb u
                Scan_pb.Maximum += 1;
                Scan_max_obj++;
                Scan_cur_obj++;

                await Scan_obj_presentation_update(ind);
            }
            Obj_scan_latency_special_update(ind);
            await Scan_obj_presentation_update(ind);
            watch.Stop();
            ParentPage.MainPageTestTb.Text += "Prop Scane: " + watch.ElapsedMilliseconds.ToString() + "\r\n";
            //Get_obj_class_result_update(await Transceive(Fetch_obj_info(DRBE_obj_list[i])), i, 0);
        }

        

        private async void DRBEOP_rescan_bt_Click(object sender, RoutedEventArgs e)
        {

            await Scan_obj_info_property(Dic_SObt_i[Temp_singleo_bt]);
        }
        private void Presentation_fidelity_setup()
        {
            #region RCS label
            FRCS_up_bd = new Border()
            {
                Height = 10,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0, 0, 0.5)
            };
            RS_sp.Children.Add(FRCS_up_bd);

            FRCS_label_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "RCS Fidelity",
                Foreground = Light_blue_brush,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Height = 30
            };
            RS_sp.Children.Add(FRCS_label_tb);

            FRCS_down_bd = new Border()
            {
                Height = 10,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            RS_sp.Children.Add(FRCS_down_bd);
            #endregion

            #region Order 0
            FRCS0_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            FRCS0_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            FRCS0_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FRCS0_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FRCS0_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            FRCS0_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Order 0",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FRCS0_gd.Children.Add(FRCS0_l_tb);
            FRCS0_l_tb.SetValue(Grid.ColumnProperty, 0);
            FRCS0_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FRCS0_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FRCS0_gd.Children.Add(FRCS0_i_tb);
            FRCS0_i_tb.SetValue(Grid.ColumnProperty, 3);
            FRCS0_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FRCS0_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0
            };
            FRCS0_gd.Children.Add(FRCS0_pb);
            FRCS0_pb.SetValue(Grid.ColumnProperty, 1);
            FRCS0_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(FRCS0_gd);
            #endregion
            #region Order 1
            FRCS1_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            FRCS1_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            FRCS1_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FRCS1_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FRCS1_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            FRCS1_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Order 1",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FRCS1_gd.Children.Add(FRCS1_l_tb);
            FRCS1_l_tb.SetValue(Grid.ColumnProperty, 0);
            FRCS1_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FRCS1_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FRCS1_gd.Children.Add(FRCS1_i_tb);
            FRCS1_i_tb.SetValue(Grid.ColumnProperty, 3);
            FRCS1_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FRCS1_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0
            };
            FRCS1_gd.Children.Add(FRCS1_pb);
            FRCS1_pb.SetValue(Grid.ColumnProperty, 1);
            FRCS1_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(FRCS1_gd);
            #endregion
            #region Order 2
            FRCS2_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            FRCS2_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            FRCS2_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FRCS2_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FRCS2_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            FRCS2_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Order 2",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FRCS2_gd.Children.Add(FRCS2_l_tb);
            FRCS2_l_tb.SetValue(Grid.ColumnProperty, 0);
            FRCS2_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FRCS2_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FRCS2_gd.Children.Add(FRCS2_i_tb);
            FRCS2_i_tb.SetValue(Grid.ColumnProperty, 3);
            FRCS2_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FRCS2_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0
            };
            FRCS2_gd.Children.Add(FRCS2_pb);
            FRCS2_pb.SetValue(Grid.ColumnProperty, 1);
            FRCS2_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(FRCS2_gd);
            #endregion
            #region Order 3
            FRCS3_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            FRCS3_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            FRCS3_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FRCS3_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FRCS3_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            FRCS3_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Order 3",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FRCS3_gd.Children.Add(FRCS3_l_tb);
            FRCS3_l_tb.SetValue(Grid.ColumnProperty, 0);
            FRCS3_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FRCS3_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FRCS3_gd.Children.Add(FRCS3_i_tb);
            FRCS3_i_tb.SetValue(Grid.ColumnProperty, 3);
            FRCS3_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FRCS3_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0
            };
            FRCS3_gd.Children.Add(FRCS3_pb);
            FRCS3_pb.SetValue(Grid.ColumnProperty, 1);
            FRCS3_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(FRCS3_gd);
            #endregion
            #region Order 4
            FRCS4_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            FRCS4_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            FRCS4_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FRCS4_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FRCS4_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            FRCS4_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Order 4",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FRCS4_gd.Children.Add(FRCS4_l_tb);
            FRCS4_l_tb.SetValue(Grid.ColumnProperty, 0);
            FRCS4_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FRCS4_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FRCS4_gd.Children.Add(FRCS4_i_tb);
            FRCS4_i_tb.SetValue(Grid.ColumnProperty, 3);
            FRCS4_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FRCS4_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0
            };
            FRCS4_gd.Children.Add(FRCS4_pb);
            FRCS4_pb.SetValue(Grid.ColumnProperty, 1);
            FRCS4_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(FRCS4_gd);
            #endregion
            #region Order 5
            FRCS5_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            FRCS5_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            FRCS5_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FRCS5_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FRCS5_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            FRCS5_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Order 5",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FRCS5_gd.Children.Add(FRCS5_l_tb);
            FRCS5_l_tb.SetValue(Grid.ColumnProperty, 0);
            FRCS5_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FRCS5_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FRCS5_gd.Children.Add(FRCS5_i_tb);
            FRCS5_i_tb.SetValue(Grid.ColumnProperty, 3);
            FRCS5_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FRCS5_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0
            };
            FRCS5_gd.Children.Add(FRCS5_pb);
            FRCS5_pb.SetValue(Grid.ColumnProperty, 1);
            FRCS5_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(FRCS5_gd);
            #endregion
            #region Order 6
            FRCS6_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            FRCS6_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            FRCS6_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FRCS6_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FRCS6_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            FRCS6_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Order 6",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FRCS6_gd.Children.Add(FRCS6_l_tb);
            FRCS6_l_tb.SetValue(Grid.ColumnProperty, 0);
            FRCS6_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FRCS6_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FRCS6_gd.Children.Add(FRCS6_i_tb);
            FRCS6_i_tb.SetValue(Grid.ColumnProperty, 3);
            FRCS6_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FRCS6_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0
            };
            FRCS6_gd.Children.Add(FRCS6_pb);
            FRCS6_pb.SetValue(Grid.ColumnProperty, 1);
            FRCS6_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(FRCS6_gd);
            #endregion

            #region ANT label
            FANT_up_bd = new Border()
            {
                Height = 10,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0, 0, 0.5)
            };
            RS_sp.Children.Add(FANT_up_bd);

            FANT_label_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Antenna Fidelity",
                Foreground = Light_blue_brush,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Height = 30
            };
            RS_sp.Children.Add(FANT_label_tb);

            FANT_down_bd = new Border()
            {
                Height = 10,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            RS_sp.Children.Add(FANT_down_bd);
            #endregion

            #region Order 0
            FANT0_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            FANT0_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            FANT0_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FANT0_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FANT0_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            FANT0_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Order 0",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FANT0_gd.Children.Add(FANT0_l_tb);
            FANT0_l_tb.SetValue(Grid.ColumnProperty, 0);
            FANT0_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FANT0_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FANT0_gd.Children.Add(FANT0_i_tb);
            FANT0_i_tb.SetValue(Grid.ColumnProperty, 3);
            FANT0_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FANT0_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0
            };
            FANT0_gd.Children.Add(FANT0_pb);
            FANT0_pb.SetValue(Grid.ColumnProperty, 1);
            FANT0_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(FANT0_gd);
            #endregion
            #region Order 1
            FANT1_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            FANT1_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            FANT1_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FANT1_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FANT1_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            FANT1_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Order 1",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FANT1_gd.Children.Add(FANT1_l_tb);
            FANT1_l_tb.SetValue(Grid.ColumnProperty, 0);
            FANT1_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FANT1_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FANT1_gd.Children.Add(FANT1_i_tb);
            FANT1_i_tb.SetValue(Grid.ColumnProperty, 3);
            FANT1_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FANT1_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0
            };
            FANT1_gd.Children.Add(FANT1_pb);
            FANT1_pb.SetValue(Grid.ColumnProperty, 1);
            FANT1_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(FANT1_gd);
            #endregion
            #region Order 2
            FANT2_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            FANT2_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            FANT2_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FANT2_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FANT2_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            FANT2_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Order 2",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FANT2_gd.Children.Add(FANT2_l_tb);
            FANT2_l_tb.SetValue(Grid.ColumnProperty, 0);
            FANT2_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FANT2_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FANT2_gd.Children.Add(FANT2_i_tb);
            FANT2_i_tb.SetValue(Grid.ColumnProperty, 3);
            FANT2_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FANT2_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0
            };
            FANT2_gd.Children.Add(FANT2_pb);
            FANT2_pb.SetValue(Grid.ColumnProperty, 1);
            FANT2_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(FANT2_gd);
            #endregion
            #region Order 3
            FANT3_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            FANT3_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            FANT3_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FANT3_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FANT3_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            FANT3_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Order 3",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FANT3_gd.Children.Add(FANT3_l_tb);
            FANT3_l_tb.SetValue(Grid.ColumnProperty, 0);
            FANT3_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FANT3_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FANT3_gd.Children.Add(FANT3_i_tb);
            FANT3_i_tb.SetValue(Grid.ColumnProperty, 3);
            FANT3_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FANT3_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0
            };
            FANT3_gd.Children.Add(FANT3_pb);
            FANT3_pb.SetValue(Grid.ColumnProperty, 1);
            FANT3_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(FANT3_gd);
            #endregion
            #region Order 4
            FANT4_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            FANT4_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            FANT4_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FANT4_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FANT4_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            FANT4_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Order 4",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FANT4_gd.Children.Add(FANT4_l_tb);
            FANT4_l_tb.SetValue(Grid.ColumnProperty, 0);
            FANT4_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FANT4_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FANT4_gd.Children.Add(FANT4_i_tb);
            FANT4_i_tb.SetValue(Grid.ColumnProperty, 3);
            FANT4_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FANT4_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0
            };
            FANT4_gd.Children.Add(FANT4_pb);
            FANT4_pb.SetValue(Grid.ColumnProperty, 1);
            FANT4_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(FANT4_gd);
            #endregion
            #region Order 5
            FANT5_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            FANT5_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            FANT5_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FANT5_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            FANT5_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            FANT5_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Order 5",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FANT5_gd.Children.Add(FANT5_l_tb);
            FANT5_l_tb.SetValue(Grid.ColumnProperty, 0);
            FANT5_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FANT5_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            FANT5_gd.Children.Add(FANT5_i_tb);
            FANT5_i_tb.SetValue(Grid.ColumnProperty, 3);
            FANT5_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            FANT5_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0
            };
            FANT5_gd.Children.Add(FANT5_pb);
            FANT5_pb.SetValue(Grid.ColumnProperty, 1);
            FANT5_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(FANT5_gd);
            #endregion
        }
        private void Presentation_fidelity_show()
        {
            #region RCS label
            RS_sp.Children.Add(FRCS_up_bd);
            RS_sp.Children.Add(FRCS_label_tb);
            RS_sp.Children.Add(FRCS_down_bd);
            #endregion

            #region Order 0
            RS_sp.Children.Add(FRCS0_gd);
            #endregion
            #region Order 1
            RS_sp.Children.Add(FRCS1_gd);
            #endregion
            #region Order 2
            RS_sp.Children.Add(FRCS2_gd);
            #endregion
            #region Order 3
            RS_sp.Children.Add(FRCS3_gd);
            #endregion
            #region Order 4
            RS_sp.Children.Add(FRCS4_gd);
            #endregion
            #region Order 5
            RS_sp.Children.Add(FRCS5_gd);
            #endregion
            #region Order 6
            RS_sp.Children.Add(FRCS6_gd);
            #endregion

            #region ANT label
            RS_sp.Children.Add(FANT_up_bd);
            RS_sp.Children.Add(FANT_label_tb);
            RS_sp.Children.Add(FANT_down_bd);
            #endregion

            #region Order 0
            RS_sp.Children.Add(FANT0_gd);
            #endregion
            #region Order 1
            RS_sp.Children.Add(FANT1_gd);
            #endregion
            #region Order 2
            RS_sp.Children.Add(FANT2_gd);
            #endregion
            #region Order 3
            RS_sp.Children.Add(FANT3_gd);
            #endregion
            #region Order 4
            RS_sp.Children.Add(FANT4_gd);
            #endregion
            #region Order 5
            RS_sp.Children.Add(FANT5_gd);
            #endregion
        }

        private void Presentation_property_setup()
        {
            #region RCS label
            PRCS_up_bd = new Border()
            {
                Height = 10,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0, 0, 0.5)
            };
            RS_sp.Children.Add(PRCS_up_bd);

            PRCS_label_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "RCS Property",
                Foreground = Light_blue_brush,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Height = 30
            };
            RS_sp.Children.Add(PRCS_label_tb);

            PRCS_down_bd = new Border()
            {
                Height = 10,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            RS_sp.Children.Add(PRCS_down_bd);
            #endregion

            #region Scatter Point
            PRCS_SP_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            PRCS_SP_gd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            PRCS_SP_gd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            PRCS_SP_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            PRCS_SP_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            PRCS_SP_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            PRCS_SP_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            PRCS_SP_TL_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "0-20",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_SP_gd.Children.Add(PRCS_SP_TL_tb);
            PRCS_SP_TL_tb.SetValue(Grid.ColumnProperty, 1);
            PRCS_SP_TL_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_SP_TL_tb.SetValue(Grid.RowProperty, 0);
            PRCS_SP_TL_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_SP_TR_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "0-20",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_SP_gd.Children.Add(PRCS_SP_TR_tb);
            PRCS_SP_TR_tb.SetValue(Grid.ColumnProperty, 2);
            PRCS_SP_TR_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_SP_TR_tb.SetValue(Grid.RowProperty, 0);
            PRCS_SP_TR_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_SP_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Scatter Point",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_SP_gd.Children.Add(PRCS_SP_l_tb);
            PRCS_SP_l_tb.SetValue(Grid.ColumnProperty, 0);
            PRCS_SP_l_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_SP_l_tb.SetValue(Grid.RowProperty, 1);
            PRCS_SP_l_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_SP_pi_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "+ 0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_SP_gd.Children.Add(PRCS_SP_pi_tb);
            PRCS_SP_pi_tb.SetValue(Grid.ColumnProperty, 3);
            PRCS_SP_pi_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_SP_pi_tb.SetValue(Grid.RowProperty, 0);
            PRCS_SP_pi_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_SP_ni_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "- 0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_SP_gd.Children.Add(PRCS_SP_ni_tb);
            PRCS_SP_ni_tb.SetValue(Grid.ColumnProperty, 3);
            PRCS_SP_ni_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_SP_ni_tb.SetValue(Grid.RowProperty, 1);
            PRCS_SP_ni_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_SPL_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0,
                Foreground = Default_back_black_color_brush,
                Background = orange_brush
            };
            PRCS_SP_gd.Children.Add(PRCS_SPL_pb);
            PRCS_SPL_pb.SetValue(Grid.ColumnProperty, 1);
            PRCS_SPL_pb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_SPL_pb.SetValue(Grid.RowProperty, 1);
            PRCS_SPL_pb.SetValue(Grid.RowSpanProperty, 1);


            PRCS_SPR_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0
            };
            PRCS_SP_gd.Children.Add(PRCS_SPR_pb);
            PRCS_SPR_pb.SetValue(Grid.ColumnProperty, 2);
            PRCS_SPR_pb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_SPR_pb.SetValue(Grid.RowProperty, 1);
            PRCS_SPR_pb.SetValue(Grid.RowSpanProperty, 1);
            RS_sp.Children.Add(PRCS_SP_gd);
            #endregion

            #region Angle Resolution
            PRCS_AR_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            PRCS_AR_gd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            PRCS_AR_gd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            PRCS_AR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            PRCS_AR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            PRCS_AR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            PRCS_AR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            PRCS_AR_TL_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "0-20",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_AR_gd.Children.Add(PRCS_AR_TL_tb);
            PRCS_AR_TL_tb.SetValue(Grid.ColumnProperty, 1);
            PRCS_AR_TL_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_AR_TL_tb.SetValue(Grid.RowProperty, 0);
            PRCS_AR_TL_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_AR_TR_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "0-20",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_AR_gd.Children.Add(PRCS_AR_TR_tb);
            PRCS_AR_TR_tb.SetValue(Grid.ColumnProperty, 2);
            PRCS_AR_TR_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_AR_TR_tb.SetValue(Grid.RowProperty, 0);
            PRCS_AR_TR_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_AR_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Angle Resolution",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_AR_gd.Children.Add(PRCS_AR_l_tb);
            PRCS_AR_l_tb.SetValue(Grid.ColumnProperty, 0);
            PRCS_AR_l_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_AR_l_tb.SetValue(Grid.RowProperty, 1);
            PRCS_AR_l_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_AR_pi_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "+ 0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_AR_gd.Children.Add(PRCS_AR_pi_tb);
            PRCS_AR_pi_tb.SetValue(Grid.ColumnProperty, 3);
            PRCS_AR_pi_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_AR_pi_tb.SetValue(Grid.RowProperty, 0);
            PRCS_AR_pi_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_AR_ni_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "- 0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_AR_gd.Children.Add(PRCS_AR_ni_tb);
            PRCS_AR_ni_tb.SetValue(Grid.ColumnProperty, 3);
            PRCS_AR_ni_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_AR_ni_tb.SetValue(Grid.RowProperty, 1);
            PRCS_AR_ni_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_ARL_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0,
                Foreground = Default_back_black_color_brush,
                Background = orange_brush
            };
            PRCS_AR_gd.Children.Add(PRCS_ARL_pb);
            PRCS_ARL_pb.SetValue(Grid.ColumnProperty, 1);
            PRCS_ARL_pb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_ARL_pb.SetValue(Grid.RowProperty, 1);
            PRCS_ARL_pb.SetValue(Grid.RowSpanProperty, 1);


            PRCS_ARR_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0
            };
            PRCS_AR_gd.Children.Add(PRCS_ARR_pb);
            PRCS_ARR_pb.SetValue(Grid.ColumnProperty, 2);
            PRCS_ARR_pb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_ARR_pb.SetValue(Grid.RowProperty, 1);
            PRCS_ARR_pb.SetValue(Grid.RowSpanProperty, 1);
            RS_sp.Children.Add(PRCS_AR_gd);
            #endregion

            #region Frequency Bin
            PRCS_FB_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            PRCS_FB_gd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            PRCS_FB_gd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            PRCS_FB_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            PRCS_FB_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            PRCS_FB_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            PRCS_FB_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            PRCS_FB_TL_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "0-20",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_FB_gd.Children.Add(PRCS_FB_TL_tb);
            PRCS_FB_TL_tb.SetValue(Grid.ColumnProperty, 1);
            PRCS_FB_TL_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_FB_TL_tb.SetValue(Grid.RowProperty, 0);
            PRCS_FB_TL_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_FB_TR_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "0-20",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_FB_gd.Children.Add(PRCS_FB_TR_tb);
            PRCS_FB_TR_tb.SetValue(Grid.ColumnProperty, 2);
            PRCS_FB_TR_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_FB_TR_tb.SetValue(Grid.RowProperty, 0);
            PRCS_FB_TR_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_FB_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Frequnecy Bin no.",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_FB_gd.Children.Add(PRCS_FB_l_tb);
            PRCS_FB_l_tb.SetValue(Grid.ColumnProperty, 0);
            PRCS_FB_l_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_FB_l_tb.SetValue(Grid.RowProperty, 1);
            PRCS_FB_l_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_FB_pi_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "+ 0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_FB_gd.Children.Add(PRCS_FB_pi_tb);
            PRCS_FB_pi_tb.SetValue(Grid.ColumnProperty, 3);
            PRCS_FB_pi_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_FB_pi_tb.SetValue(Grid.RowProperty, 0);
            PRCS_FB_pi_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_FB_ni_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "- 0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_FB_gd.Children.Add(PRCS_FB_ni_tb);
            PRCS_FB_ni_tb.SetValue(Grid.ColumnProperty, 3);
            PRCS_FB_ni_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_FB_ni_tb.SetValue(Grid.RowProperty, 1);
            PRCS_FB_ni_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_FBL_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0,
                Foreground = Default_back_black_color_brush,
                Background = orange_brush
            };
            PRCS_FB_gd.Children.Add(PRCS_FBL_pb);
            PRCS_FBL_pb.SetValue(Grid.ColumnProperty, 1);
            PRCS_FBL_pb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_FBL_pb.SetValue(Grid.RowProperty, 1);
            PRCS_FBL_pb.SetValue(Grid.RowSpanProperty, 1);


            PRCS_FBR_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0
            };
            PRCS_FB_gd.Children.Add(PRCS_FBR_pb);
            PRCS_FBR_pb.SetValue(Grid.ColumnProperty, 2);
            PRCS_FBR_pb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_FBR_pb.SetValue(Grid.RowProperty, 1);
            PRCS_FBR_pb.SetValue(Grid.RowSpanProperty, 1);
            RS_sp.Children.Add(PRCS_FB_gd);
            #endregion

            #region Sample Size
            PRCS_SS_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            PRCS_SS_gd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            PRCS_SS_gd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            PRCS_SS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            PRCS_SS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            PRCS_SS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            PRCS_SS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            PRCS_SS_TL_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "0-20",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_SS_gd.Children.Add(PRCS_SS_TL_tb);
            PRCS_SS_TL_tb.SetValue(Grid.ColumnProperty, 1);
            PRCS_SS_TL_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_SS_TL_tb.SetValue(Grid.RowProperty, 0);
            PRCS_SS_TL_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_SS_TR_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "0-20",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_SS_gd.Children.Add(PRCS_SS_TR_tb);
            PRCS_SS_TR_tb.SetValue(Grid.ColumnProperty, 2);
            PRCS_SS_TR_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_SS_TR_tb.SetValue(Grid.RowProperty, 0);
            PRCS_SS_TR_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_SS_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Sample Size",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_SS_gd.Children.Add(PRCS_SS_l_tb);
            PRCS_SS_l_tb.SetValue(Grid.ColumnProperty, 0);
            PRCS_SS_l_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_SS_l_tb.SetValue(Grid.RowProperty, 1);
            PRCS_SS_l_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_SS_pi_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "+ 0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_SS_gd.Children.Add(PRCS_SS_pi_tb);
            PRCS_SS_pi_tb.SetValue(Grid.ColumnProperty, 3);
            PRCS_SS_pi_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_SS_pi_tb.SetValue(Grid.RowProperty, 0);
            PRCS_SS_pi_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_SS_ni_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "- 0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_SS_gd.Children.Add(PRCS_SS_ni_tb);
            PRCS_SS_ni_tb.SetValue(Grid.ColumnProperty, 3);
            PRCS_SS_ni_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_SS_ni_tb.SetValue(Grid.RowProperty, 1);
            PRCS_SS_ni_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_SSL_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0,
                Foreground = Default_back_black_color_brush,
                Background = orange_brush
            };
            PRCS_SS_gd.Children.Add(PRCS_SSL_pb);
            PRCS_SSL_pb.SetValue(Grid.ColumnProperty, 1);
            PRCS_SSL_pb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_SSL_pb.SetValue(Grid.RowProperty, 1);
            PRCS_SSL_pb.SetValue(Grid.RowSpanProperty, 1);


            PRCS_SSR_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0
            };
            PRCS_SS_gd.Children.Add(PRCS_SSR_pb);
            PRCS_SSR_pb.SetValue(Grid.ColumnProperty, 2);
            PRCS_SSR_pb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_SSR_pb.SetValue(Grid.RowProperty, 1);
            PRCS_SSR_pb.SetValue(Grid.RowSpanProperty, 1);
            RS_sp.Children.Add(PRCS_SS_gd);
            #endregion

            #region Polarization Number
            PRCS_PN_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            PRCS_PN_gd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            PRCS_PN_gd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            PRCS_PN_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            PRCS_PN_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            PRCS_PN_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            PRCS_PN_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            PRCS_PN_TL_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "0-20",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_PN_gd.Children.Add(PRCS_PN_TL_tb);
            PRCS_PN_TL_tb.SetValue(Grid.ColumnProperty, 1);
            PRCS_PN_TL_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_PN_TL_tb.SetValue(Grid.RowProperty, 0);
            PRCS_PN_TL_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_PN_TR_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "0-20",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_PN_gd.Children.Add(PRCS_PN_TR_tb);
            PRCS_PN_TR_tb.SetValue(Grid.ColumnProperty, 2);
            PRCS_PN_TR_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_PN_TR_tb.SetValue(Grid.RowProperty, 0);
            PRCS_PN_TR_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_PN_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "no. of Polarization",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_PN_gd.Children.Add(PRCS_PN_l_tb);
            PRCS_PN_l_tb.SetValue(Grid.ColumnProperty, 0);
            PRCS_PN_l_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_PN_l_tb.SetValue(Grid.RowProperty, 1);
            PRCS_PN_l_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_PN_pi_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "+ 0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_PN_gd.Children.Add(PRCS_PN_pi_tb);
            PRCS_PN_pi_tb.SetValue(Grid.ColumnProperty, 3);
            PRCS_PN_pi_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_PN_pi_tb.SetValue(Grid.RowProperty, 0);
            PRCS_PN_pi_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_PN_ni_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "- 0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PRCS_PN_gd.Children.Add(PRCS_PN_ni_tb);
            PRCS_PN_ni_tb.SetValue(Grid.ColumnProperty, 3);
            PRCS_PN_ni_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_PN_ni_tb.SetValue(Grid.RowProperty, 1);
            PRCS_PN_ni_tb.SetValue(Grid.RowSpanProperty, 1);

            PRCS_PNL_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0,
                Foreground = Default_back_black_color_brush,
                Background = orange_brush
            };
            PRCS_PN_gd.Children.Add(PRCS_PNL_pb);
            PRCS_PNL_pb.SetValue(Grid.ColumnProperty, 1);
            PRCS_PNL_pb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_PNL_pb.SetValue(Grid.RowProperty, 1);
            PRCS_PNL_pb.SetValue(Grid.RowSpanProperty, 1);


            PRCS_PNR_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0
            };
            PRCS_PN_gd.Children.Add(PRCS_PNR_pb);
            PRCS_PNR_pb.SetValue(Grid.ColumnProperty, 2);
            PRCS_PNR_pb.SetValue(Grid.ColumnSpanProperty, 1);
            PRCS_PNR_pb.SetValue(Grid.RowProperty, 1);
            PRCS_PNR_pb.SetValue(Grid.RowSpanProperty, 1);
            RS_sp.Children.Add(PRCS_PN_gd);
            #endregion

            #region Antenna label
            PANT_up_bd = new Border()
            {
                Height = 10,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0, 0, 0.5)
            };
            RS_sp.Children.Add(PANT_up_bd);

            PANT_label_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Antenna Property",
                Foreground = Light_blue_brush,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Height = 30
            };
            RS_sp.Children.Add(PANT_label_tb);

            PANT_down_bd = new Border()
            {
                Height = 10,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            RS_sp.Children.Add(PANT_down_bd);
            #endregion

            #region Number of Antenna
            PANT_NA_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            PANT_NA_gd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            PANT_NA_gd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            PANT_NA_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            PANT_NA_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            PANT_NA_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            PANT_NA_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            PANT_NA_TL_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "0-20",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PANT_NA_gd.Children.Add(PANT_NA_TL_tb);
            PANT_NA_TL_tb.SetValue(Grid.ColumnProperty, 1);
            PANT_NA_TL_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PANT_NA_TL_tb.SetValue(Grid.RowProperty, 0);
            PANT_NA_TL_tb.SetValue(Grid.RowSpanProperty, 1);

            PANT_NA_TR_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "0-20",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PANT_NA_gd.Children.Add(PANT_NA_TR_tb);
            PANT_NA_TR_tb.SetValue(Grid.ColumnProperty, 2);
            PANT_NA_TR_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PANT_NA_TR_tb.SetValue(Grid.RowProperty, 0);
            PANT_NA_TR_tb.SetValue(Grid.RowSpanProperty, 1);

            PANT_NA_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "No. of Antenna",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PANT_NA_gd.Children.Add(PANT_NA_l_tb);
            PANT_NA_l_tb.SetValue(Grid.ColumnProperty, 0);
            PANT_NA_l_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PANT_NA_l_tb.SetValue(Grid.RowProperty, 1);
            PANT_NA_l_tb.SetValue(Grid.RowSpanProperty, 1);

            PANT_NA_pi_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "+ 0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PANT_NA_gd.Children.Add(PANT_NA_pi_tb);
            PANT_NA_pi_tb.SetValue(Grid.ColumnProperty, 3);
            PANT_NA_pi_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PANT_NA_pi_tb.SetValue(Grid.RowProperty, 0);
            PANT_NA_pi_tb.SetValue(Grid.RowSpanProperty, 1);

            PANT_NA_ni_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "- 0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PANT_NA_gd.Children.Add(PANT_NA_ni_tb);
            PANT_NA_ni_tb.SetValue(Grid.ColumnProperty, 3);
            PANT_NA_ni_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PANT_NA_ni_tb.SetValue(Grid.RowProperty, 1);
            PANT_NA_ni_tb.SetValue(Grid.RowSpanProperty, 1);

            PANT_NAL_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0,
                Foreground = Default_back_black_color_brush,
                Background = orange_brush
            };
            PANT_NA_gd.Children.Add(PANT_NAL_pb);
            PANT_NAL_pb.SetValue(Grid.ColumnProperty, 1);
            PANT_NAL_pb.SetValue(Grid.ColumnSpanProperty, 1);
            PANT_NAL_pb.SetValue(Grid.RowProperty, 1);
            PANT_NAL_pb.SetValue(Grid.RowSpanProperty, 1);


            PANT_NAR_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0
            };
            PANT_NA_gd.Children.Add(PANT_NAR_pb);
            PANT_NAR_pb.SetValue(Grid.ColumnProperty, 2);
            PANT_NAR_pb.SetValue(Grid.ColumnSpanProperty, 1);
            PANT_NAR_pb.SetValue(Grid.RowProperty, 1);
            PANT_NAR_pb.SetValue(Grid.RowSpanProperty, 1);
            RS_sp.Children.Add(PANT_NA_gd);
            #endregion

            #region Angle Resolution
            PANT_AR_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            PANT_AR_gd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            PANT_AR_gd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            PANT_AR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            PANT_AR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            PANT_AR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            PANT_AR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            PANT_AR_TL_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "0-20",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PANT_AR_gd.Children.Add(PANT_AR_TL_tb);
            PANT_AR_TL_tb.SetValue(Grid.ColumnProperty, 1);
            PANT_AR_TL_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PANT_AR_TL_tb.SetValue(Grid.RowProperty, 0);
            PANT_AR_TL_tb.SetValue(Grid.RowSpanProperty, 1);

            PANT_AR_TR_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "0-20",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PANT_AR_gd.Children.Add(PANT_AR_TR_tb);
            PANT_AR_TR_tb.SetValue(Grid.ColumnProperty, 2);
            PANT_AR_TR_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PANT_AR_TR_tb.SetValue(Grid.RowProperty, 0);
            PANT_AR_TR_tb.SetValue(Grid.RowSpanProperty, 1);

            PANT_AR_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Angle Resolution",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PANT_AR_gd.Children.Add(PANT_AR_l_tb);
            PANT_AR_l_tb.SetValue(Grid.ColumnProperty, 0);
            PANT_AR_l_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PANT_AR_l_tb.SetValue(Grid.RowProperty, 1);
            PANT_AR_l_tb.SetValue(Grid.RowSpanProperty, 1);

            PANT_AR_pi_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "+ 0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PANT_AR_gd.Children.Add(PANT_AR_pi_tb);
            PANT_AR_pi_tb.SetValue(Grid.ColumnProperty, 3);
            PANT_AR_pi_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PANT_AR_pi_tb.SetValue(Grid.RowProperty, 0);
            PANT_AR_pi_tb.SetValue(Grid.RowSpanProperty, 1);

            PANT_AR_ni_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "- 0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PANT_AR_gd.Children.Add(PANT_AR_ni_tb);
            PANT_AR_ni_tb.SetValue(Grid.ColumnProperty, 3);
            PANT_AR_ni_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PANT_AR_ni_tb.SetValue(Grid.RowProperty, 1);
            PANT_AR_ni_tb.SetValue(Grid.RowSpanProperty, 1);

            PANT_ARL_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0,
                Foreground = Default_back_black_color_brush,
                Background = orange_brush
            };
            PANT_AR_gd.Children.Add(PANT_ARL_pb);
            PANT_ARL_pb.SetValue(Grid.ColumnProperty, 1);
            PANT_ARL_pb.SetValue(Grid.ColumnSpanProperty, 1);
            PANT_ARL_pb.SetValue(Grid.RowProperty, 1);
            PANT_ARL_pb.SetValue(Grid.RowSpanProperty, 1);


            PANT_ARR_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0
            };
            PANT_AR_gd.Children.Add(PANT_ARR_pb);
            PANT_ARR_pb.SetValue(Grid.ColumnProperty, 2);
            PANT_ARR_pb.SetValue(Grid.ColumnSpanProperty, 1);
            PANT_ARR_pb.SetValue(Grid.RowProperty, 1);
            PANT_ARR_pb.SetValue(Grid.RowSpanProperty, 1);
            RS_sp.Children.Add(PANT_AR_gd);
            #endregion

            #region Dictionary Size
            PANT_DS_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            PANT_DS_gd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            PANT_DS_gd.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            PANT_DS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            PANT_DS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            PANT_DS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            PANT_DS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            PANT_DS_TL_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "0-20",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PANT_DS_gd.Children.Add(PANT_DS_TL_tb);
            PANT_DS_TL_tb.SetValue(Grid.ColumnProperty, 1);
            PANT_DS_TL_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PANT_DS_TL_tb.SetValue(Grid.RowProperty, 0);
            PANT_DS_TL_tb.SetValue(Grid.RowSpanProperty, 1);

            PANT_DS_TR_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "0-20",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PANT_DS_gd.Children.Add(PANT_DS_TR_tb);
            PANT_DS_TR_tb.SetValue(Grid.ColumnProperty, 2);
            PANT_DS_TR_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PANT_DS_TR_tb.SetValue(Grid.RowProperty, 0);
            PANT_DS_TR_tb.SetValue(Grid.RowSpanProperty, 1);

            PANT_DS_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Dictionary Size",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PANT_DS_gd.Children.Add(PANT_DS_l_tb);
            PANT_DS_l_tb.SetValue(Grid.ColumnProperty, 0);
            PANT_DS_l_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PANT_DS_l_tb.SetValue(Grid.RowProperty, 1);
            PANT_DS_l_tb.SetValue(Grid.RowSpanProperty, 1);

            PANT_DS_pi_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "+ 0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PANT_DS_gd.Children.Add(PANT_DS_pi_tb);
            PANT_DS_pi_tb.SetValue(Grid.ColumnProperty, 3);
            PANT_DS_pi_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PANT_DS_pi_tb.SetValue(Grid.RowProperty, 0);
            PANT_DS_pi_tb.SetValue(Grid.RowSpanProperty, 1);

            PANT_DS_ni_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "- 0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            PANT_DS_gd.Children.Add(PANT_DS_ni_tb);
            PANT_DS_ni_tb.SetValue(Grid.ColumnProperty, 3);
            PANT_DS_ni_tb.SetValue(Grid.ColumnSpanProperty, 1);
            PANT_DS_ni_tb.SetValue(Grid.RowProperty, 1);
            PANT_DS_ni_tb.SetValue(Grid.RowSpanProperty, 1);

            PANT_DSL_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0,
                Foreground = Default_back_black_color_brush,
                Background = orange_brush
            };
            PANT_DS_gd.Children.Add(PANT_DSL_pb);
            PANT_DSL_pb.SetValue(Grid.ColumnProperty, 1);
            PANT_DSL_pb.SetValue(Grid.ColumnSpanProperty, 1);
            PANT_DSL_pb.SetValue(Grid.RowProperty, 1);
            PANT_DSL_pb.SetValue(Grid.RowSpanProperty, 1);


            PANT_DSR_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0
            };
            PANT_DS_gd.Children.Add(PANT_DSR_pb);
            PANT_DSR_pb.SetValue(Grid.ColumnProperty, 2);
            PANT_DSR_pb.SetValue(Grid.ColumnSpanProperty, 1);
            PANT_DSR_pb.SetValue(Grid.RowProperty, 1);
            PANT_DSR_pb.SetValue(Grid.RowSpanProperty, 1);
            RS_sp.Children.Add(PANT_DS_gd);
            #endregion
        }

        private void Presentation_property_show()
        {
            #region RCS label
            RS_sp.Children.Add(PRCS_up_bd);
            RS_sp.Children.Add(PRCS_label_tb);
            RS_sp.Children.Add(PRCS_down_bd);
            #endregion

            #region Scatter Point
            RS_sp.Children.Add(PRCS_SP_gd);
            #endregion

            #region Angle Resolution
            RS_sp.Children.Add(PRCS_AR_gd);
            #endregion

            #region Frequency Bin
            RS_sp.Children.Add(PRCS_FB_gd);
            #endregion

            #region Sample Size
            RS_sp.Children.Add(PRCS_SS_gd);
            #endregion

            #region Polarization Number
            RS_sp.Children.Add(PRCS_PN_gd);
            #endregion

            #region Antenna label
            RS_sp.Children.Add(PANT_up_bd);
            RS_sp.Children.Add(PANT_label_tb);
            RS_sp.Children.Add(PANT_down_bd);
            #endregion

            #region Number of Antenna
            RS_sp.Children.Add(PANT_NA_gd);
            #endregion

            #region Angle Resolution
            RS_sp.Children.Add(PANT_AR_gd);
            #endregion

            #region Dictionary Size
            RS_sp.Children.Add(PANT_DS_gd);
            #endregion
        }
        private void Presentation_setup()
        {
            RS_sv = new ScrollViewer()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                VerticalScrollMode = ScrollMode.Enabled
            };
            ParentGrid.Children.Add(RS_sv);
            RS_sv.SetValue(Grid.ColumnProperty, 140);
            RS_sv.SetValue(Grid.ColumnSpanProperty, 78);
            RS_sv.SetValue(Grid.RowProperty, 7);
            RS_sv.SetValue(Grid.RowSpanProperty, 135);

            RS_sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush
            };
            RS_sv.Content = RS_sp;
        }
        private void Presentation_Global_obj_refresh()
        {
            int i = 0;
            RS_sp.Children.Clear();
            OR_gd = new List<Grid>();
            OR_i_tb = new List<TextBlock>();
            OR_l_tb = new List<TextBlock>();
            OR_pb = new List<ProgressBar>();
            i = 0;
            while (i < DRBE_obj_list.Count)
            {
                OR_gd.Add(new Grid()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Background = Default_back_black_color_brush,
                    Height = 30

                });
                OR_gd[OR_gd.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
                OR_gd[OR_gd.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
                OR_gd[OR_gd.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
                OR_gd[OR_gd.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

                OR_l_tb.Add(new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    HorizontalTextAlignment = TextAlignment.Center,
                    Text = "Object ID: " + DRBE_obj_list[i].ID.ToString(),
                    Foreground = white_button_brush,
                    FontSize = 12,
                    FontWeight = FontWeights.Bold
                });
                OR_gd[OR_gd.Count - 1].Children.Add(OR_l_tb[OR_l_tb.Count - 1]);
                OR_l_tb[OR_l_tb.Count - 1].SetValue(Grid.ColumnProperty, 0);
                OR_l_tb[OR_l_tb.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

                OR_i_tb.Add(new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    HorizontalTextAlignment = TextAlignment.Right,
                    Text = "",
                    Foreground = white_button_brush,
                    FontSize = 12,
                    FontWeight = FontWeights.Bold
                });
                OR_gd[OR_gd.Count - 1].Children.Add(OR_i_tb[OR_i_tb.Count - 1]);
                OR_i_tb[OR_i_tb.Count - 1].SetValue(Grid.ColumnProperty, 3);
                OR_i_tb[OR_i_tb.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

                OR_pb.Add(new ProgressBar()
                {
                    Minimum = 0,
                    Maximum = 1,
                    Value = 0
                });
                OR_gd[OR_gd.Count - 1].Children.Add(OR_pb[OR_pb.Count - 1]);
                OR_pb[OR_pb.Count - 1].SetValue(Grid.ColumnProperty, 1);
                OR_pb[OR_pb.Count - 1].SetValue(Grid.ColumnSpanProperty, 2);
                RS_sp.Children.Add(OR_gd[OR_gd.Count - 1]);
                i++;
            }
        }
        private void Presentation_Global_obj_setup()
        {
            int i = 0;
            
            #region Computation label
            OR_up_bd = new Border()
            {
                Height = 10,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0, 0, 0.5)
            };
            RS_sp.Children.Add(OR_up_bd);

            OR_label_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Object Resource Ranking",
                Foreground = Light_blue_brush,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Height = 30
            };
            RS_sp.Children.Add(OR_label_tb);

            OR_down_bd = new Border()
            {
                Height = 10,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            RS_sp.Children.Add(OR_down_bd);
            #endregion
            //Presentation_Global_obj_refresh();

        }
        private void Presentation_Global_obj_show()
        {
            int i = 0;

            #region Computation label

            RS_sp.Children.Add(OR_up_bd);


            RS_sp.Children.Add(OR_label_tb);


            RS_sp.Children.Add(OR_down_bd);

            if(Presentation_choice_mode == 1)
            {
                OR_label_tb.Text = "Object Computation Ranking";
            }
            else if (Presentation_choice_mode == 2)
            {
                OR_label_tb.Text = "Object Memory Ranking";
            }
            else if (Presentation_choice_mode == 3)
            {
                OR_label_tb.Text = "Object Bandwidth Ranking";
            }
            else if (Presentation_choice_mode == 4)
            {
                OR_label_tb.Text = "Object Latency Ranking";
            }
            #endregion

            i = 0;
            while(i<OR_gd.Count)
            {
                RS_sp.Children.Add(OR_gd[i]);
                i++;
            }
        }
        private void Presentation_Global_resource_setup()
        {

            #region Scan label
            S_up_bd = new Border()
            {
                Height = 10,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0, 0, 0.5)
            };
            RS_sp.Children.Add(S_up_bd);

            S_label_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Scan Progress",
                Foreground = Light_blue_brush,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Height = 30
            };
            RS_sp.Children.Add(S_label_tb);

            S_down_bd = new Border()
            {
                Height = 10,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            RS_sp.Children.Add(S_down_bd);
            #endregion

            #region Scan
            Scan_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            Scan_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            Scan_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            Scan_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            Scan_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            Scan_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Scan Progress",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            Scan_gd.Children.Add(Scan_l_tb);
            Scan_l_tb.SetValue(Grid.ColumnProperty, 0);
            Scan_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            Scan_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0/0 link",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            Scan_gd.Children.Add(Scan_i_tb);
            Scan_i_tb.SetValue(Grid.ColumnProperty, 3);
            Scan_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            Scan_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0,
                Background = white_button_brush
            };
            Scan_gd.Children.Add(Scan_pb);
            Scan_pb.SetValue(Grid.ColumnProperty, 1);
            Scan_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(Scan_gd);
            #endregion

            #region computation

            #region Computation label
            C_up_bd = new Border()
            {
                Height = 10,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0, 0, 0.5)
            };
            RS_sp.Children.Add(C_up_bd);

            C_label_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Computation",
                Foreground = Light_blue_brush,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Height = 30
            };
            RS_sp.Children.Add(C_label_tb);

            C_down_bd = new Border()
            {
                Height = 10,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            RS_sp.Children.Add(C_down_bd);
            #endregion

            #region Computation RCS
            C_RCS_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            C_RCS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            C_RCS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            C_RCS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            C_RCS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            C_RCS_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "RCS",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            C_RCS_gd.Children.Add(C_RCS_l_tb);
            C_RCS_l_tb.SetValue(Grid.ColumnProperty, 0);
            C_RCS_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            C_RCS_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            C_RCS_gd.Children.Add(C_RCS_i_tb);
            C_RCS_i_tb.SetValue(Grid.ColumnProperty, 3);
            C_RCS_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            C_RCS_pb = new ProgressBar() {
                Minimum = 0,
                Maximum = Computation_max,
                Value = 0
            };
            C_RCS_gd.Children.Add(C_RCS_pb);
            C_RCS_pb.SetValue(Grid.ColumnProperty, 1);
            C_RCS_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(C_RCS_gd);
            #endregion
            #region Computation Antenna
            C_ANT_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            C_ANT_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            C_ANT_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            C_ANT_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            C_ANT_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            C_ANT_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Antenna",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            C_ANT_gd.Children.Add(C_ANT_l_tb);
            C_ANT_l_tb.SetValue(Grid.ColumnProperty, 0);
            C_ANT_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            C_ANT_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            C_ANT_gd.Children.Add(C_ANT_i_tb);
            C_ANT_i_tb.SetValue(Grid.ColumnProperty, 3);
            C_ANT_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            C_ANT_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Computation_max,
                Value = 0
            };
            C_ANT_gd.Children.Add(C_ANT_pb);
            C_ANT_pb.SetValue(Grid.ColumnProperty, 1);
            C_ANT_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(C_ANT_gd);
            #endregion
            #region Computation Cordination
            C_COR_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            C_COR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            C_COR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            C_COR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            C_COR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            C_COR_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Coordination",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            C_COR_gd.Children.Add(C_COR_l_tb);
            C_COR_l_tb.SetValue(Grid.ColumnProperty, 0);
            C_COR_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            C_COR_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            C_COR_gd.Children.Add(C_COR_i_tb);
            C_COR_i_tb.SetValue(Grid.ColumnProperty, 3);
            C_COR_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            C_COR_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Computation_max,
                Value = 0
            };
            C_COR_gd.Children.Add(C_COR_pb);
            C_COR_pb.SetValue(Grid.ColumnProperty, 1);
            C_COR_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(C_COR_gd);
            #endregion
            #region Orientation
            C_ORI_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            C_ORI_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            C_ORI_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            C_ORI_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            C_ORI_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            C_ORI_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Orientation",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            C_ORI_gd.Children.Add(C_ORI_l_tb);
            C_ORI_l_tb.SetValue(Grid.ColumnProperty, 0);
            C_ORI_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            C_ORI_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            C_ORI_gd.Children.Add(C_ORI_i_tb);
            C_ORI_i_tb.SetValue(Grid.ColumnProperty, 3);
            C_ORI_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            C_ORI_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Computation_max,
                Value = 0
            };
            C_ORI_gd.Children.Add(C_ORI_pb);
            C_ORI_pb.SetValue(Grid.ColumnProperty, 1);
            C_ORI_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(C_ORI_gd);
            #endregion
            #region TU
            C_TU_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            C_TU_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            C_TU_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            C_TU_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            C_TU_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            C_TU_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Updates Time",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            C_TU_gd.Children.Add(C_TU_l_tb);
            C_TU_l_tb.SetValue(Grid.ColumnProperty, 0);
            C_TU_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            C_TU_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            C_TU_gd.Children.Add(C_TU_i_tb);
            C_TU_i_tb.SetValue(Grid.ColumnProperty, 3);
            C_TU_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            C_TU_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Computation_max,
                Value = 0
            };
            C_TU_gd.Children.Add(C_TU_pb);
            C_TU_pb.SetValue(Grid.ColumnProperty, 1);
            C_TU_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(C_TU_gd);
            #endregion
            #region NRE
            C_NRE_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            C_NRE_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            C_NRE_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            C_NRE_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            C_NRE_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            C_NRE_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "NR Engine",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            C_NRE_gd.Children.Add(C_NRE_l_tb);
            C_NRE_l_tb.SetValue(Grid.ColumnProperty, 0);
            C_NRE_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            C_NRE_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            C_NRE_gd.Children.Add(C_NRE_i_tb);
            C_NRE_i_tb.SetValue(Grid.ColumnProperty, 3);
            C_NRE_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            C_NRE_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Computation_max,
                Value = 0
            };
            C_NRE_gd.Children.Add(C_NRE_pb);
            C_NRE_pb.SetValue(Grid.ColumnProperty, 1);
            C_NRE_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(C_NRE_gd);
            #endregion
            #region PG
            C_PG_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            C_PG_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            C_PG_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            C_PG_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            C_PG_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            C_PG_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Path Gain",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            C_PG_gd.Children.Add(C_PG_l_tb);
            C_PG_l_tb.SetValue(Grid.ColumnProperty, 0);
            C_PG_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            C_PG_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP MAC",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            C_PG_gd.Children.Add(C_PG_i_tb);
            C_PG_i_tb.SetValue(Grid.ColumnProperty, 3);
            C_PG_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            C_PG_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Computation_max,
                Value = 0
            };
            C_PG_gd.Children.Add(C_PG_pb);
            C_PG_pb.SetValue(Grid.ColumnProperty, 1);
            C_PG_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(C_PG_gd);
            #endregion
            #endregion

            #region Memory
            #region Memory label
            M_up_bd = new Border()
            {
                Height = 10,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0, 0, 0.5)
            };
            RS_sp.Children.Add(M_up_bd);

            M_label_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Memory",
                Foreground = Light_blue_brush,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Height = 30
            };
            RS_sp.Children.Add(M_label_tb);

            M_down_bd = new Border()
            {
                Height = 10,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            RS_sp.Children.Add(M_down_bd);
            #endregion

            #region Memory RCS
            M_RCS_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            M_RCS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            M_RCS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            M_RCS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            M_RCS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            M_RCS_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "RCS",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            M_RCS_gd.Children.Add(M_RCS_l_tb);
            M_RCS_l_tb.SetValue(Grid.ColumnProperty, 0);
            M_RCS_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            M_RCS_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP-64b",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            M_RCS_gd.Children.Add(M_RCS_i_tb);
            M_RCS_i_tb.SetValue(Grid.ColumnProperty, 3);
            M_RCS_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            M_RCS_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Memory_max,
                Value = 0
            };
            M_RCS_gd.Children.Add(M_RCS_pb);
            M_RCS_pb.SetValue(Grid.ColumnProperty, 1);
            M_RCS_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(M_RCS_gd);
            #endregion
            #region Memory Antenna
            M_ANT_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            M_ANT_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            M_ANT_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            M_ANT_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            M_ANT_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            M_ANT_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Antenna",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            M_ANT_gd.Children.Add(M_ANT_l_tb);
            M_ANT_l_tb.SetValue(Grid.ColumnProperty, 0);
            M_ANT_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            M_ANT_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP-64b",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            M_ANT_gd.Children.Add(M_ANT_i_tb);
            M_ANT_i_tb.SetValue(Grid.ColumnProperty, 3);
            M_ANT_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            M_ANT_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Memory_max,
                Value = 0
            };
            M_ANT_gd.Children.Add(M_ANT_pb);
            M_ANT_pb.SetValue(Grid.ColumnProperty, 1);
            M_ANT_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(M_ANT_gd);
            #endregion
            #region Memory Cordination
            M_COR_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            M_COR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            M_COR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            M_COR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            M_COR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            M_COR_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Coordination",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            M_COR_gd.Children.Add(M_COR_l_tb);
            M_COR_l_tb.SetValue(Grid.ColumnProperty, 0);
            M_COR_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            M_COR_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP-64b",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            M_COR_gd.Children.Add(M_COR_i_tb);
            M_COR_i_tb.SetValue(Grid.ColumnProperty, 3);
            M_COR_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            M_COR_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Memory_max,
                Value = 0
            };
            M_COR_gd.Children.Add(M_COR_pb);
            M_COR_pb.SetValue(Grid.ColumnProperty, 1);
            M_COR_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(M_COR_gd);
            #endregion
            #region Orientation
            M_ORI_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            M_ORI_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            M_ORI_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            M_ORI_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            M_ORI_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            M_ORI_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Orientation",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            M_ORI_gd.Children.Add(M_ORI_l_tb);
            M_ORI_l_tb.SetValue(Grid.ColumnProperty, 0);
            M_ORI_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            M_ORI_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP-64b",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            M_ORI_gd.Children.Add(M_ORI_i_tb);
            M_ORI_i_tb.SetValue(Grid.ColumnProperty, 3);
            M_ORI_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            M_ORI_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Memory_max,
                Value = 0
            };
            M_ORI_gd.Children.Add(M_ORI_pb);
            M_ORI_pb.SetValue(Grid.ColumnProperty, 1);
            M_ORI_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(M_ORI_gd);
            #endregion
            #region TU
            M_TU_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            M_TU_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            M_TU_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            M_TU_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            M_TU_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            M_TU_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Updates Time",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            M_TU_gd.Children.Add(M_TU_l_tb);
            M_TU_l_tb.SetValue(Grid.ColumnProperty, 0);
            M_TU_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            M_TU_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP-64b",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            M_TU_gd.Children.Add(M_TU_i_tb);
            M_TU_i_tb.SetValue(Grid.ColumnProperty, 3);
            M_TU_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            M_TU_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Memory_max,
                Value = 0
            };
            M_TU_gd.Children.Add(M_TU_pb);
            M_TU_pb.SetValue(Grid.ColumnProperty, 1);
            M_TU_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(M_TU_gd);
            #endregion
            #region NRE
            M_NRE_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            M_NRE_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            M_NRE_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            M_NRE_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            M_NRE_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            M_NRE_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "NR Engine",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            M_NRE_gd.Children.Add(M_NRE_l_tb);
            M_NRE_l_tb.SetValue(Grid.ColumnProperty, 0);
            M_NRE_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            M_NRE_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP-64b",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            M_NRE_gd.Children.Add(M_NRE_i_tb);
            M_NRE_i_tb.SetValue(Grid.ColumnProperty, 3);
            M_NRE_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            M_NRE_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Memory_max,
                Value = 0
            };
            M_NRE_gd.Children.Add(M_NRE_pb);
            M_NRE_pb.SetValue(Grid.ColumnProperty, 1);
            M_NRE_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(M_NRE_gd);
            #endregion
            #region PG
            M_PG_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            M_PG_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            M_PG_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            M_PG_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            M_PG_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            M_PG_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Path Gain",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            M_PG_gd.Children.Add(M_PG_l_tb);
            M_PG_l_tb.SetValue(Grid.ColumnProperty, 0);
            M_PG_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            M_PG_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP-64b",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            M_PG_gd.Children.Add(M_PG_i_tb);
            M_PG_i_tb.SetValue(Grid.ColumnProperty, 3);
            M_PG_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            M_PG_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Memory_max,
                Value = 0
            };
            M_PG_gd.Children.Add(M_PG_pb);
            M_PG_pb.SetValue(Grid.ColumnProperty, 1);
            M_PG_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(M_PG_gd);
            #endregion
            #endregion

            #region Bandwidth
            #region label
            B_up_bd = new Border()
            {
                Height = 10,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0, 0, 0.5)
            };
            RS_sp.Children.Add(B_up_bd);

            B_label_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Bandwidth",
                Foreground = Light_blue_brush,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Height = 30
            };
            RS_sp.Children.Add(B_label_tb);

            B_down_bd = new Border()
            {
                Height = 10,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            RS_sp.Children.Add(B_down_bd);
            #endregion

            #region RCS
            B_RCS_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            B_RCS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            B_RCS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            B_RCS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            B_RCS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            B_RCS_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "RCS",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            B_RCS_gd.Children.Add(B_RCS_l_tb);
            B_RCS_l_tb.SetValue(Grid.ColumnProperty, 0);
            B_RCS_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            B_RCS_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP-64b",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            B_RCS_gd.Children.Add(B_RCS_i_tb);
            B_RCS_i_tb.SetValue(Grid.ColumnProperty, 3);
            B_RCS_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            B_RCS_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Bandwidth_max,
                Value = 0
            };
            B_RCS_gd.Children.Add(B_RCS_pb);
            B_RCS_pb.SetValue(Grid.ColumnProperty, 1);
            B_RCS_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(B_RCS_gd);
            #endregion
            #region Antenna
            B_ANT_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            B_ANT_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            B_ANT_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            B_ANT_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            B_ANT_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            B_ANT_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Antenna",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            B_ANT_gd.Children.Add(B_ANT_l_tb);
            B_ANT_l_tb.SetValue(Grid.ColumnProperty, 0);
            B_ANT_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            B_ANT_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP-64b",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            B_ANT_gd.Children.Add(B_ANT_i_tb);
            B_ANT_i_tb.SetValue(Grid.ColumnProperty, 3);
            B_ANT_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            B_ANT_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Bandwidth_max,
                Value = 0
            };
            B_ANT_gd.Children.Add(B_ANT_pb);
            B_ANT_pb.SetValue(Grid.ColumnProperty, 1);
            B_ANT_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(B_ANT_gd);
            #endregion
            #region Cordination
            B_COR_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            B_COR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            B_COR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            B_COR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            B_COR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            B_COR_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Coordination",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            B_COR_gd.Children.Add(B_COR_l_tb);
            B_COR_l_tb.SetValue(Grid.ColumnProperty, 0);
            B_COR_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            B_COR_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP-64b",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            B_COR_gd.Children.Add(B_COR_i_tb);
            B_COR_i_tb.SetValue(Grid.ColumnProperty, 3);
            B_COR_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            B_COR_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Bandwidth_max,
                Value = 0
            };
            B_COR_gd.Children.Add(B_COR_pb);
            B_COR_pb.SetValue(Grid.ColumnProperty, 1);
            B_COR_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(B_COR_gd);
            #endregion
            #region Orientation
            B_ORI_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            B_ORI_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            B_ORI_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            B_ORI_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            B_ORI_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            B_ORI_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Orientation",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            B_ORI_gd.Children.Add(B_ORI_l_tb);
            B_ORI_l_tb.SetValue(Grid.ColumnProperty, 0);
            B_ORI_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            B_ORI_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP-64b",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            B_ORI_gd.Children.Add(B_ORI_i_tb);
            B_ORI_i_tb.SetValue(Grid.ColumnProperty, 3);
            B_ORI_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            B_ORI_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Bandwidth_max,
                Value = 0
            };
            B_ORI_gd.Children.Add(B_ORI_pb);
            B_ORI_pb.SetValue(Grid.ColumnProperty, 1);
            B_ORI_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(B_ORI_gd);
            #endregion
            #region TU
            B_TU_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            B_TU_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            B_TU_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            B_TU_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            B_TU_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            B_TU_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Updates Time",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            B_TU_gd.Children.Add(B_TU_l_tb);
            B_TU_l_tb.SetValue(Grid.ColumnProperty, 0);
            B_TU_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            B_TU_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP-64b",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            B_TU_gd.Children.Add(B_TU_i_tb);
            B_TU_i_tb.SetValue(Grid.ColumnProperty, 3);
            B_TU_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            B_TU_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Bandwidth_max,
                Value = 0
            };
            B_TU_gd.Children.Add(B_TU_pb);
            B_TU_pb.SetValue(Grid.ColumnProperty, 1);
            B_TU_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(B_TU_gd);
            #endregion
            #region NRE
            B_NRE_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            B_NRE_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            B_NRE_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            B_NRE_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            B_NRE_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            B_NRE_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "NR Engine",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            B_NRE_gd.Children.Add(B_NRE_l_tb);
            B_NRE_l_tb.SetValue(Grid.ColumnProperty, 0);
            B_NRE_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            B_NRE_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP-64b",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            B_NRE_gd.Children.Add(B_NRE_i_tb);
            B_NRE_i_tb.SetValue(Grid.ColumnProperty, 3);
            B_NRE_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            B_NRE_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Bandwidth_max,
                Value = 0
            };
            B_NRE_gd.Children.Add(B_NRE_pb);
            B_NRE_pb.SetValue(Grid.ColumnProperty, 1);
            B_NRE_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(B_NRE_gd);
            #endregion
            #region PG
            B_PG_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            B_PG_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            B_PG_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            B_PG_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            B_PG_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            B_PG_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Path Gain",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            B_PG_gd.Children.Add(B_PG_l_tb);
            B_PG_l_tb.SetValue(Grid.ColumnProperty, 0);
            B_PG_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            B_PG_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 FP-64b",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            B_PG_gd.Children.Add(B_PG_i_tb);
            B_PG_i_tb.SetValue(Grid.ColumnProperty, 3);
            B_PG_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            B_PG_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Bandwidth_max,
                Value = 0
            };
            B_PG_gd.Children.Add(B_PG_pb);
            B_PG_pb.SetValue(Grid.ColumnProperty, 1);
            B_PG_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(B_PG_gd);
            #endregion
            #endregion

            #region Latency
            #region label
            L_up_bd = new Border()
            {
                Height = 10,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0, 0, 0.5)
            };
            RS_sp.Children.Add(L_up_bd);

            L_label_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Latency",
                Foreground = Light_blue_brush,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Height = 30
            };
            RS_sp.Children.Add(L_label_tb);

            L_down_bd = new Border()
            {
                Height = 10,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0, 0.5, 0, 0)
            };
            RS_sp.Children.Add(L_down_bd);
            #endregion

            #region RCS
            L_RCS_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            L_RCS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            L_RCS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            L_RCS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            L_RCS_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            L_RCS_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "RCS",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            L_RCS_gd.Children.Add(L_RCS_l_tb);
            L_RCS_l_tb.SetValue(Grid.ColumnProperty, 0);
            L_RCS_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            L_RCS_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 IO-64b",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            L_RCS_gd.Children.Add(L_RCS_i_tb);
            L_RCS_i_tb.SetValue(Grid.ColumnProperty, 3);
            L_RCS_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            L_RCS_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Latency_max,
                Value = 0
            };
            L_RCS_gd.Children.Add(L_RCS_pb);
            L_RCS_pb.SetValue(Grid.ColumnProperty, 1);
            L_RCS_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(L_RCS_gd);
            #endregion
            #region Antenna
            L_ANT_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            L_ANT_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            L_ANT_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            L_ANT_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            L_ANT_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            L_ANT_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Antenna",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            L_ANT_gd.Children.Add(L_ANT_l_tb);
            L_ANT_l_tb.SetValue(Grid.ColumnProperty, 0);
            L_ANT_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            L_ANT_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 IO-64b",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            L_ANT_gd.Children.Add(L_ANT_i_tb);
            L_ANT_i_tb.SetValue(Grid.ColumnProperty, 3);
            L_ANT_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            L_ANT_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Latency_max,
                Value = 0
            };
            L_ANT_gd.Children.Add(L_ANT_pb);
            L_ANT_pb.SetValue(Grid.ColumnProperty, 1);
            L_ANT_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(L_ANT_gd);
            #endregion
            #region Cordination
            L_COR_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            L_COR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            L_COR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            L_COR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            L_COR_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            L_COR_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Coordination",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            L_COR_gd.Children.Add(L_COR_l_tb);
            L_COR_l_tb.SetValue(Grid.ColumnProperty, 0);
            L_COR_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            L_COR_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 IO-64b",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            L_COR_gd.Children.Add(L_COR_i_tb);
            L_COR_i_tb.SetValue(Grid.ColumnProperty, 3);
            L_COR_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            L_COR_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Latency_max,
                Value = 0
            };
            L_COR_gd.Children.Add(L_COR_pb);
            L_COR_pb.SetValue(Grid.ColumnProperty, 1);
            L_COR_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(L_COR_gd);
            #endregion
            #region Orientation
            L_ORI_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            L_ORI_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            L_ORI_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            L_ORI_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            L_ORI_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            L_ORI_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Orientation",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            L_ORI_gd.Children.Add(L_ORI_l_tb);
            L_ORI_l_tb.SetValue(Grid.ColumnProperty, 0);
            L_ORI_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            L_ORI_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 IO-64b",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            L_ORI_gd.Children.Add(L_ORI_i_tb);
            L_ORI_i_tb.SetValue(Grid.ColumnProperty, 3);
            L_ORI_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            L_ORI_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Latency_max,
                Value = 0
            };
            L_ORI_gd.Children.Add(L_ORI_pb);
            L_ORI_pb.SetValue(Grid.ColumnProperty, 1);
            L_ORI_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(L_ORI_gd);
            #endregion
            #region TU
            L_TU_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            L_TU_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            L_TU_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            L_TU_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            L_TU_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            L_TU_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Updates Time",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            L_TU_gd.Children.Add(L_TU_l_tb);
            L_TU_l_tb.SetValue(Grid.ColumnProperty, 0);
            L_TU_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            L_TU_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 IO-64b",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            L_TU_gd.Children.Add(L_TU_i_tb);
            L_TU_i_tb.SetValue(Grid.ColumnProperty, 3);
            L_TU_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            L_TU_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Latency_max,
                Value = 0
            };
            L_TU_gd.Children.Add(L_TU_pb);
            L_TU_pb.SetValue(Grid.ColumnProperty, 1);
            L_TU_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(L_TU_gd);
            #endregion
            #region NRE
            L_NRE_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            L_NRE_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            L_NRE_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            L_NRE_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            L_NRE_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            L_NRE_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "NR Engine",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            L_NRE_gd.Children.Add(L_NRE_l_tb);
            L_NRE_l_tb.SetValue(Grid.ColumnProperty, 0);
            L_NRE_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            L_NRE_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 IO-64b",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            L_NRE_gd.Children.Add(L_NRE_i_tb);
            L_NRE_i_tb.SetValue(Grid.ColumnProperty, 3);
            L_NRE_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            L_NRE_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Latency_max,
                Value = 0
            };
            L_NRE_gd.Children.Add(L_NRE_pb);
            L_NRE_pb.SetValue(Grid.ColumnProperty, 1);
            L_NRE_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(L_NRE_gd);
            #endregion
            #region PG
            L_PG_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Height = 30

            };
            L_PG_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            L_PG_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            L_PG_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            L_PG_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            L_PG_l_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Path Gain",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            L_PG_gd.Children.Add(L_PG_l_tb);
            L_PG_l_tb.SetValue(Grid.ColumnProperty, 0);
            L_PG_l_tb.SetValue(Grid.ColumnSpanProperty, 1);

            L_PG_i_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Right,
                Text = "0 IO-64b",
                Foreground = white_button_brush,
                FontSize = 12,
                FontWeight = FontWeights.Bold
            };
            L_PG_gd.Children.Add(L_PG_i_tb);
            L_PG_i_tb.SetValue(Grid.ColumnProperty, 3);
            L_PG_i_tb.SetValue(Grid.ColumnSpanProperty, 1);

            L_PG_pb = new ProgressBar()
            {
                Minimum = 0,
                Maximum = Latency_max,
                Value = 0
            };
            L_PG_gd.Children.Add(L_PG_pb);
            L_PG_pb.SetValue(Grid.ColumnProperty, 1);
            L_PG_pb.SetValue(Grid.ColumnSpanProperty, 2);
            RS_sp.Children.Add(L_PG_gd);
            #endregion
            #endregion
        }
        private void Presentation_hide()
        {
            RS_sp.Children.Clear();
            RS_sp.Children.Add(S_up_bd);
            RS_sp.Children.Add(S_label_tb);
            RS_sp.Children.Add(S_down_bd);
            RS_sp.Children.Add(Scan_gd);
        }
        private void Presentation_Global_resource_show()
        {

            #region computation

            #region Computation label
            RS_sp.Children.Add(C_up_bd);

            RS_sp.Children.Add(C_label_tb);

            RS_sp.Children.Add(C_down_bd);
            #endregion

            #region Computation RCS
            RS_sp.Children.Add(C_RCS_gd);
            #endregion
            #region Computation Antenna
            RS_sp.Children.Add(C_ANT_gd);
            #endregion
            #region Computation Cordination
            RS_sp.Children.Add(C_COR_gd);
            #endregion
            #region Orientation
            RS_sp.Children.Add(C_ORI_gd);
            #endregion
            #region TU
            RS_sp.Children.Add(C_TU_gd);
            #endregion
            #region NRE
            RS_sp.Children.Add(C_NRE_gd);
            #endregion
            #region PG
            RS_sp.Children.Add(C_PG_gd);
            #endregion
            #endregion

            #region Memory
            #region label
            RS_sp.Children.Add(M_up_bd);

            RS_sp.Children.Add(M_label_tb);

            RS_sp.Children.Add(M_down_bd);
            #endregion

            #region Memory RCS
            RS_sp.Children.Add(M_RCS_gd);
            #endregion
            #region Memory Antenna
            RS_sp.Children.Add(M_ANT_gd);
            #endregion
            #region Memory Cordination
            RS_sp.Children.Add(M_COR_gd);
            #endregion
            #region Orientation
            RS_sp.Children.Add(M_ORI_gd);
            #endregion
            #region TU
            RS_sp.Children.Add(M_TU_gd);
            #endregion
            #region NRE
            RS_sp.Children.Add(M_NRE_gd);
            #endregion
            #region PG
            RS_sp.Children.Add(M_PG_gd);
            #endregion
            #endregion

            #region Bandwidth
            #region label
            RS_sp.Children.Add(B_up_bd);

            RS_sp.Children.Add(B_label_tb);

            RS_sp.Children.Add(B_down_bd);
            #endregion

            #region RCS
            RS_sp.Children.Add(B_RCS_gd);
            #endregion
            #region Antenna
            RS_sp.Children.Add(B_ANT_gd);
            #endregion
            #region Cordination
            RS_sp.Children.Add(B_COR_gd);
            #endregion
            #region Orientation
            RS_sp.Children.Add(B_ORI_gd);
            #endregion
            #region TU
            RS_sp.Children.Add(B_TU_gd);
            #endregion
            #region NRE
            RS_sp.Children.Add(B_NRE_gd);
            #endregion
            #region PG
            RS_sp.Children.Add(B_PG_gd);
            #endregion
            #endregion

            #region Latency
            #region label

            RS_sp.Children.Add(L_up_bd);


            RS_sp.Children.Add(L_label_tb);


            RS_sp.Children.Add(L_down_bd);
            #endregion

            #region RCS

            RS_sp.Children.Add(L_RCS_gd);
            #endregion
            #region Antenna

            RS_sp.Children.Add(L_ANT_gd);
            #endregion
            #region Cordination

            RS_sp.Children.Add(L_COR_gd);
            #endregion
            #region Orientation

            RS_sp.Children.Add(L_ORI_gd);
            #endregion
            #region TU

            RS_sp.Children.Add(L_TU_gd);
            #endregion
            #region NRE

            RS_sp.Children.Add(L_NRE_gd);
            #endregion
            #region PG

            RS_sp.Children.Add(L_PG_gd);
            #endregion
            #endregion
        }
        private void Base_setup()
        {
            CMP_bti.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Compiler.png", UriKind.RelativeOrAbsolute));
            CMP_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = CMP_bti,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(CMP_bt);
            CMP_bt.SetValue(Grid.ColumnProperty, 55);
            CMP_bt.SetValue(Grid.ColumnSpanProperty, 10);
            CMP_bt.SetValue(Grid.RowProperty, 0);
            CMP_bt.SetValue(Grid.RowSpanProperty, 10);
            CMP_bt.Click += CMP_bt_Click;

            AMT_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Run AMT",
                Foreground = white_button_brush,
                FontSize = 18,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(AMT_tb);
            AMT_tb.SetValue(Grid.ColumnProperty, 55);
            AMT_tb.SetValue(Grid.ColumnSpanProperty, 15);
            AMT_tb.SetValue(Grid.RowProperty, 15);
            AMT_tb.SetValue(Grid.RowSpanProperty, 5);

            AMT_bti.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Calculator_icon.png", UriKind.RelativeOrAbsolute));
            AMT_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = AMT_bti,
                Foreground = white_button_brush,
                FontSize = 18
            };
            //ParentGrid.Children.Add(AMT_bt);
            AMT_bt.SetValue(Grid.ColumnProperty, 55);
            AMT_bt.SetValue(Grid.ColumnSpanProperty, 15);
            AMT_bt.SetValue(Grid.RowProperty, 20);
            AMT_bt.SetValue(Grid.RowSpanProperty, 10);
            AMT_bt.Click += AMT_bt_Click;

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
                FontSize = 18
            };
            //ParentGrid.Children.Add(Obj_save_bt);
            Obj_save_bt.SetValue(Grid.ColumnProperty, 180);
            Obj_save_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Obj_save_bt.SetValue(Grid.RowProperty, 0);
            Obj_save_bt.SetValue(Grid.RowSpanProperty, 10);
            Obj_save_bt.Click += Obj_save_bt_Click;

            Obj_summary_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Report_icon.png", UriKind.RelativeOrAbsolute));
            Obj_summary_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Obj_summary_i,
                Foreground = white_button_brush,
                FontSize = 18
            };
            //ParentGrid.Children.Add(Obj_summary_bt);
            Obj_summary_bt.SetValue(Grid.ColumnProperty, 160);
            Obj_summary_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Obj_summary_bt.SetValue(Grid.RowProperty, 0);
            Obj_summary_bt.SetValue(Grid.RowSpanProperty, 10);
            Obj_summary_bt.Click += Obj_summary_bt_Click;

            Sweep_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Simulation_icon.png", UriKind.RelativeOrAbsolute));
            Sweep_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Sweep_i,
                Foreground = white_button_brush,
                FontSize = 18
            };
            //ParentGrid.Children.Add(Sweep_bt);
            Sweep_bt.SetValue(Grid.ColumnProperty, 140);
            Sweep_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Sweep_bt.SetValue(Grid.RowProperty, 0);
            Sweep_bt.SetValue(Grid.RowSpanProperty, 10);
            Sweep_bt.Click += Sweep_bt_Click;


            Text_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Total Resource Utilization",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Text_tb);
            Text_tb.SetValue(Grid.ColumnProperty, 140);
            Text_tb.SetValue(Grid.ColumnSpanProperty, 50);
            Text_tb.SetValue(Grid.RowProperty, 2);
            Text_tb.SetValue(Grid.RowSpanProperty, 80);


            #region Order

            Ref_AO0_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,

                Background = Default_back_black_color_brush,
                Content = "Order 0:  Available",
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                FontSize = 18
            };
            //ParentGrid.Children.Add(Ref_AO0_bt);
            Ref_AO0_bt.SetValue(Grid.ColumnProperty, 125);
            Ref_AO0_bt.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO0_bt.SetValue(Grid.RowProperty, 10);
            Ref_AO0_bt.SetValue(Grid.RowSpanProperty, 5);
            Ref_AO0_bt.Click += Ref_AO_bt_Click;

            Ref_AO0_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Resource Requirement: \r\n Hardware: 0 KB \r\n Computational: 0 FLOP",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Ref_AO0_tb);
            Ref_AO0_tb.SetValue(Grid.ColumnProperty, 125);
            Ref_AO0_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO0_tb.SetValue(Grid.RowProperty, 15);
            Ref_AO0_tb.SetValue(Grid.RowSpanProperty, 15);

            Ref_AO1_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,

                Background = Default_back_black_color_brush,
                Content = "Order 1:  Available",
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                FontSize = 18
            };
            //ParentGrid.Children.Add(Ref_AO1_bt);
            Ref_AO1_bt.SetValue(Grid.ColumnProperty, 125);
            Ref_AO1_bt.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO1_bt.SetValue(Grid.RowProperty, 30);
            Ref_AO1_bt.SetValue(Grid.RowSpanProperty, 5);
            Ref_AO1_bt.Click += Ref_AO_bt_Click;

            Ref_AO1_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Resource Requirement: \r\n Hardware: 0 KB \r\n Computational: 0 FLOP",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Ref_AO1_tb);
            Ref_AO1_tb.SetValue(Grid.ColumnProperty, 125);
            Ref_AO1_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO1_tb.SetValue(Grid.RowProperty, 35);
            Ref_AO1_tb.SetValue(Grid.RowSpanProperty, 15);

            Ref_AO2_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,

                Background = Default_back_black_color_brush,
                Content = "Order 2:  Available",
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                FontSize = 18
            };
            //ParentGrid.Children.Add(Ref_AO2_bt);
            Ref_AO2_bt.SetValue(Grid.ColumnProperty, 125);
            Ref_AO2_bt.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO2_bt.SetValue(Grid.RowProperty, 50);
            Ref_AO2_bt.SetValue(Grid.RowSpanProperty, 5);
            Ref_AO2_bt.Click += Ref_AO_bt_Click;

            Ref_AO2_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Resource Requirement: \r\n Hardware: 0 KB \r\n Computational: 0 FLOP",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Ref_AO2_tb);
            Ref_AO2_tb.SetValue(Grid.ColumnProperty, 125);
            Ref_AO2_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO2_tb.SetValue(Grid.RowProperty, 55);
            Ref_AO2_tb.SetValue(Grid.RowSpanProperty, 15);

            Ref_AO3_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,

                Background = Default_back_black_color_brush,
                Content = "Order 3:  Available",
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                FontSize = 18
            };
            //ParentGrid.Children.Add(Ref_AO3_bt);
            Ref_AO3_bt.SetValue(Grid.ColumnProperty, 125);
            Ref_AO3_bt.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO3_bt.SetValue(Grid.RowProperty, 70);
            Ref_AO3_bt.SetValue(Grid.RowSpanProperty, 5);
            Ref_AO3_bt.Click += Ref_AO_bt_Click;

            Ref_AO3_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Resource Requirement: \r\n Hardware: 0 KB \r\n Computational: 0 FLOP",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Ref_AO3_tb);
            Ref_AO3_tb.SetValue(Grid.ColumnProperty, 125);
            Ref_AO3_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO3_tb.SetValue(Grid.RowProperty, 75);
            Ref_AO3_tb.SetValue(Grid.RowSpanProperty, 15);

            Ref_AO4_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,

                Background = Default_back_black_color_brush,
                Content = "Order 4:  Available",
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                FontSize = 18
            };
            //ParentGrid.Children.Add(Ref_AO4_bt);
            Ref_AO4_bt.SetValue(Grid.ColumnProperty, 125);
            Ref_AO4_bt.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO4_bt.SetValue(Grid.RowProperty, 90);
            Ref_AO4_bt.SetValue(Grid.RowSpanProperty, 5);
            Ref_AO4_bt.Click += Ref_AO_bt_Click;

            Ref_AO4_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Resource Requirement: \r\n Hardware: 0 KB \r\n Computational: 0 FLOP",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Ref_AO4_tb);
            Ref_AO4_tb.SetValue(Grid.ColumnProperty, 125);
            Ref_AO4_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO4_tb.SetValue(Grid.RowProperty, 95);
            Ref_AO4_tb.SetValue(Grid.RowSpanProperty, 15);

            Ref_AO5_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,

                Background = Default_back_black_color_brush,
                Content = "Order 5:  Available",
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                FontSize = 18
            };
            //ParentGrid.Children.Add(Ref_AO5_bt);
            Ref_AO5_bt.SetValue(Grid.ColumnProperty, 125);
            Ref_AO5_bt.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO5_bt.SetValue(Grid.RowProperty, 110);
            Ref_AO5_bt.SetValue(Grid.RowSpanProperty, 5);
            Ref_AO5_bt.Click += Ref_AO_bt_Click;

            Ref_AO5_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Resource Requirement: \r\n Hardware: 0 KB \r\n Computational: 0 FLOP",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Ref_AO5_tb);
            Ref_AO5_tb.SetValue(Grid.ColumnProperty, 125);
            Ref_AO5_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO5_tb.SetValue(Grid.RowProperty, 115);
            Ref_AO5_tb.SetValue(Grid.RowSpanProperty, 15);

            Ref_AO6_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,

                Background = Default_back_black_color_brush,
                Content = "Order 6:  Available",
                Foreground = white_button_brush,
                FontWeight = FontWeights.Bold,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(2, 2, 2, 2),
                FontSize = 18
            };
            //ParentGrid.Children.Add(Ref_AO6_bt);
            Ref_AO6_bt.SetValue(Grid.ColumnProperty, 125);
            Ref_AO6_bt.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO6_bt.SetValue(Grid.RowProperty, 130);
            Ref_AO6_bt.SetValue(Grid.RowSpanProperty, 5);
            Ref_AO6_bt.Click += Ref_AO_bt_Click;

            Ref_AO6_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Left,
                Text = "Resource Requirement: \r\n Hardware: 0 KB \r\n Computational: 0 FLOP",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            //ParentGrid.Children.Add(Ref_AO6_tb);
            Ref_AO6_tb.SetValue(Grid.ColumnProperty, 125);
            Ref_AO6_tb.SetValue(Grid.ColumnSpanProperty, 30);
            Ref_AO6_tb.SetValue(Grid.RowProperty, 135);
            Ref_AO6_tb.SetValue(Grid.RowSpanProperty, 15);
            #endregion

            #region Link unsure
            Link_uns_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = Sky_green_color,
                Text = "Low Priority"

            };
            //ParentGrid.Children.Add(Link_uns_tb);
            Link_uns_tb.SetValue(Grid.ColumnProperty, 70);
            Link_uns_tb.SetValue(Grid.ColumnSpanProperty, 25);
            Link_uns_tb.SetValue(Grid.RowProperty, 110);
            Link_uns_tb.SetValue(Grid.RowSpanProperty, 10);

            Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Uns_all_icon.png", UriKind.RelativeOrAbsolute));
            Link_uns_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Link_uns_bt_i,
                Foreground = white_button_brush,
                FontSize = 18
            };
            //ParentGrid.Children.Add(Link_uns_bt);
            Link_uns_bt.SetValue(Grid.ColumnProperty, 70);
            Link_uns_bt.SetValue(Grid.ColumnSpanProperty, 25);
            Link_uns_bt.SetValue(Grid.RowProperty, 120);
            Link_uns_bt.SetValue(Grid.RowSpanProperty, 25);
            Link_uns_bt.Click += Link_uns_bt_Click;
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
            Ref_Antenna_bt.SetValue(Grid.ColumnProperty, 100);
            Ref_Antenna_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Ref_Antenna_bt.SetValue(Grid.RowProperty, 30);
            Ref_Antenna_bt.SetValue(Grid.RowSpanProperty, 15);
            Ref_Antenna_bt.Click += Ref_Antenna_bt_Click;

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
            Ref_Doppler_bt.SetValue(Grid.ColumnProperty, 100);
            Ref_Doppler_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Ref_Doppler_bt.SetValue(Grid.RowProperty, 50);
            Ref_Doppler_bt.SetValue(Grid.RowSpanProperty, 15);
            Ref_Doppler_bt.Click += Ref_Doppler_bt_Click;

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
            Ref_Polarization_bt.SetValue(Grid.ColumnProperty, 100);
            Ref_Polarization_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Ref_Polarization_bt.SetValue(Grid.RowProperty, 70);
            Ref_Polarization_bt.SetValue(Grid.RowSpanProperty, 15);
            Ref_Polarization_bt.Click += Ref_Polarization_bt_Click;

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
            Ref_RCS_bt.SetValue(Grid.ColumnProperty, 100);
            Ref_RCS_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Ref_RCS_bt.SetValue(Grid.RowProperty, 90);
            Ref_RCS_bt.SetValue(Grid.RowSpanProperty, 15);
            Ref_RCS_bt.Click += Ref_RCS_bt_Click;

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
            Ref_RFim_bt.SetValue(Grid.ColumnProperty, 100);
            Ref_RFim_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Ref_RFim_bt.SetValue(Grid.RowProperty, 110);
            Ref_RFim_bt.SetValue(Grid.RowSpanProperty, 15);
            Ref_RFim_bt.Click += Ref_RFim_bt_Click;

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
            Ref_Clutter_bt.SetValue(Grid.ColumnProperty, 100);
            Ref_Clutter_bt.SetValue(Grid.ColumnSpanProperty, 15);
            Ref_Clutter_bt.SetValue(Grid.RowProperty, 130);
            Ref_Clutter_bt.SetValue(Grid.RowSpanProperty, 15);
            Ref_Clutter_bt.Click += Ref_Clutter_bt_Click;
            #endregion

            Link_uns_tb.Text = "Less Priority Scenario";
            Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Uns_all_icon.png", UriKind.RelativeOrAbsolute));
        }
        public void Setup_refresh(List<DRBE_Objs> drbeol, Dictionary<int, DRBE_Objs> dit, Dictionary<int, DRBE_Objs> dio, Dictionary<int, DRBE_Objs> dir, Dictionary<DRBE_Objs, int> dti, Dictionary<DRBE_Objs, int> doi, Dictionary<DRBE_Objs, int> dri, List<List<List<bool>>> x)
        {
            int i = 0;
            int ii = 0;
            int iii = 0;
            DRBE_obj_list = drbeol;
            Dic_t_i_obj = dit;
            Dic_o_i_obj = dio;
            Dic_r_i_obj = dir;

            Dic_i_t_obj = dti;
            Dic_i_o_obj = doi;
            Dic_i_r_obj = dri;
            Link_list = x;
            DRp_sp.Children.Clear();
            DFp_sp.Children.Clear();
            DTp_sp.Children.Clear();
            SingleOP_sp.Children.Clear();

            DF_gd = new List<Grid>();
            DF_i = new List<Image>();
            DF_sp = new List<StackPanel>();
            DF_tb = new List<TextBlock>();
            DF_bt = new List<Button>();
            Dic_pbt_i = new Dictionary<Button, int>();

            DT_gd = new List<Grid>();
            DT_i = new List<Image>();
            DT_sp = new List<StackPanel>();
            DT_tb = new List<TextBlock>();
            DT_bt = new List<Button>();
            Dic_tbt_i = new Dictionary<Button, int>();

            DR_gd = new List<Grid>();
            DR_i = new List<Image>();
            DR_sp = new List<StackPanel>();
            DR_tb = new List<TextBlock>();
            DR_bt = new List<Button>();
            Dic_rbt_i = new Dictionary<Button, int>();

            SingleO_gd = new List<Grid>();
            SingleO_i = new List<Image>();
            SingleO_sp = new List<StackPanel>();
            SingleO_tb = new List<TextBlock>();
            SingleO_bt = new List<Button>();
            Dic_SObt_i = new Dictionary<Button, int>();
            #region DT
            i = 0;
            while (i < Link_list.Count)
            {
                DT_gd.Add(new Grid()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Background = Default_back_black_color_brush
                });

                DT_gd[DT_gd.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                DT_gd[DT_gd.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


                DT_i.Add(new Image()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Phase_Array_icon.png", UriKind.RelativeOrAbsolute)),
                    Height = 20
                });
                DT_i[DT_i.Count - 1].SetValue(Grid.ColumnProperty, 0);
                DT_i[DT_i.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

                DT_tb.Add(new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontSize = 12,
                    Text = "ID: " + Dic_t_i_obj[i].ID.ToString(),
                    Foreground = red_bright_button_brush
                });
                DT_tb[DT_tb.Count - 1].SetValue(Grid.ColumnProperty, 1);
                DT_tb[DT_tb.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

                DT_sp.Add(new StackPanel()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Height = 40,
                    Background = Default_back_black_color_brush
                });
                DT_sp[DT_sp.Count - 1].Children.Add(DT_gd[DT_gd.Count - 1]);
                DT_gd[DT_gd.Count - 1].Children.Add(DT_i[DT_i.Count - 1]);
                DT_gd[DT_gd.Count - 1].Children.Add(DT_tb[DT_tb.Count - 1]);


                DT_bt.Add(new Button()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Stretch,
                    Background = Default_back_black_color_brush,
                    Height = 40,
                    Content = DT_sp[DT_sp.Count - 1],
                    BorderBrush = dark_grey_brush,
                    BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
                });
                Dic_tbt_i[DT_bt[DT_i.Count - 1]] = i;
                DT_bt[DT_bt.Count - 1].Click += DT_bt_Click;

                DTp_sp.Children.Add(DT_bt[DT_bt.Count - 1]);

                Dic_tbt_i[DT_bt[DT_i.Count - 1]] = i;
                i++;
            }
            #endregion

            #region DRF
            i = 0;
            while (i < Link_list[0].Count)
            {
                DF_gd.Add(new Grid()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Background = Default_back_black_color_brush
                });

                DF_gd[DF_gd.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                DF_gd[DF_gd.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


                DF_i.Add(new Image()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Object_icon.png", UriKind.RelativeOrAbsolute)),
                    Height = 20
                });
                DF_i[DF_i.Count - 1].SetValue(Grid.ColumnProperty, 0);
                DF_i[DF_i.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

                DF_tb.Add(new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontSize = 12,
                    Text = "ID: " + Dic_o_i_obj[i].ID.ToString(),
                    Foreground = red_bright_button_brush
                });
                DF_tb[DF_tb.Count - 1].SetValue(Grid.ColumnProperty, 1);
                DF_tb[DF_tb.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

                DF_sp.Add(new StackPanel()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Height = 40,
                    Background = Default_back_black_color_brush
                });
                DF_sp[DF_sp.Count - 1].Children.Add(DF_gd[DF_gd.Count - 1]);
                DF_gd[DF_gd.Count - 1].Children.Add(DF_i[DF_i.Count - 1]);
                DF_gd[DF_gd.Count - 1].Children.Add(DF_tb[DF_tb.Count - 1]);


                DF_bt.Add(new Button()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Stretch,
                    Background = Default_back_black_color_brush,
                    Height = 40,
                    Content = DF_sp[DF_sp.Count - 1],
                    BorderBrush = dark_grey_brush,
                    BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
                });
                Dic_pbt_i[DF_bt[DF_i.Count - 1]] = i;
                DF_bt[DF_bt.Count - 1].Click += DF_bt_Click;

                DFp_sp.Children.Add(DF_bt[DF_bt.Count - 1]);


                i++;
            }
            #endregion

            #region DR
            i = 0;
            while (i < Link_list[0][0].Count)
            {
                DR_gd.Add(new Grid()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Background = Default_back_black_color_brush
                });

                DR_gd[DR_gd.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                DR_gd[DR_gd.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


                DR_i.Add(new Image()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Phase_Array_icon_t.png", UriKind.RelativeOrAbsolute)),
                    Height = 20
                });
                DR_i[DR_i.Count - 1].SetValue(Grid.ColumnProperty, 0);
                DR_i[DR_i.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

                DR_tb.Add(new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontSize = 12,
                    Text = "ID: " + Dic_r_i_obj[i].ID.ToString(),
                    Foreground = red_bright_button_brush
                });
                DR_tb[DR_tb.Count - 1].SetValue(Grid.ColumnProperty, 1);
                DR_tb[DR_tb.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

                DR_sp.Add(new StackPanel()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Height = 40,
                    Background = Default_back_black_color_brush
                });
                DR_sp[DR_sp.Count - 1].Children.Add(DR_gd[DR_gd.Count - 1]);
                DR_gd[DR_gd.Count - 1].Children.Add(DR_i[DR_i.Count - 1]);
                DR_gd[DR_gd.Count - 1].Children.Add(DR_tb[DR_tb.Count - 1]);


                DR_bt.Add(new Button()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Stretch,
                    Background = Default_back_black_color_brush,
                    Height = 40,
                    Content = DR_sp[DR_sp.Count - 1],
                    BorderBrush = dark_grey_brush,
                    BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
                });
                Dic_rbt_i[DR_bt[DR_i.Count - 1]] = i;
                DR_bt[DR_bt.Count - 1].Click += DR_bt_Click;

                DRp_sp.Children.Add(DR_bt[DR_bt.Count - 1]);

                i++;
            }
            #endregion

            #region Single O
            SingleO_title_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Object",
                Foreground = white_button_brush,
                FontSize = 14,
                FontWeight = FontWeights.Bold
            };
            ParentGrid.Children.Add(SingleO_title_tb);
            SingleO_title_tb.SetValue(Grid.ColumnProperty, 65);
            SingleO_title_tb.SetValue(Grid.ColumnSpanProperty, 16);
            SingleO_title_tb.SetValue(Grid.RowProperty, 2);
            SingleO_title_tb.SetValue(Grid.RowSpanProperty, 5);

            SingleOP_sv = new ScrollViewer()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                VerticalScrollMode = ScrollMode.Enabled,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            };
            ParentGrid.Children.Add(SingleOP_sv);
            SingleOP_sv.SetValue(Grid.ColumnProperty, 65);
            SingleOP_sv.SetValue(Grid.ColumnSpanProperty, 16);
            SingleOP_sv.SetValue(Grid.RowProperty, 7);
            SingleOP_sv.SetValue(Grid.RowSpanProperty, 135);

            SingleOP_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush

            };
            SingleOP_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            SingleOP_sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            };
            SingleOP_sv.Content = SingleOP_gd;
            SingleOP_gd.Children.Add(SingleOP_sp);
            //ParentGrid.Children.Add(DRBE_SPL);
            SingleOP_sp.SetValue(Grid.ColumnProperty, 0);
            SingleOP_sp.SetValue(Grid.ColumnSpanProperty, 1);
            i = 0;
            while (i < DRBE_obj_list.Count)
            {
                SingleO_gd.Add(new Grid()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Background = Default_back_black_color_brush
                });

                SingleO_gd[SingleO_gd.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                SingleO_gd[SingleO_gd.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


                SingleO_i.Add(new Image()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Phase_Array_icon.png", UriKind.RelativeOrAbsolute)),
                    Height = 20
                });
                SingleO_i[SingleO_i.Count - 1].SetValue(Grid.ColumnProperty, 0);
                SingleO_i[SingleO_i.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

                SingleO_tb.Add(new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontSize = 12,
                    Text = "ID: " + DRBE_obj_list[i].ID.ToString(),
                    Foreground = white_button_brush
                });
                SingleO_tb[SingleO_tb.Count - 1].SetValue(Grid.ColumnProperty, 1);
                SingleO_tb[SingleO_tb.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

                SingleO_sp.Add(new StackPanel()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Height = 40,
                    Background = Default_back_black_color_brush
                });
                SingleO_sp[SingleO_sp.Count - 1].Children.Add(SingleO_gd[SingleO_gd.Count - 1]);
                SingleO_gd[SingleO_gd.Count - 1].Children.Add(SingleO_i[SingleO_i.Count - 1]);
                SingleO_gd[SingleO_gd.Count - 1].Children.Add(SingleO_tb[SingleO_tb.Count - 1]);


                SingleO_bt.Add(new Button()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Stretch,
                    Background = Default_back_black_color_brush,
                    Height = 40,
                    Content = SingleO_sp[SingleO_sp.Count - 1],
                    BorderBrush = dark_grey_brush,
                    BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
                });
                Dic_SObt_i[SingleO_bt[SingleO_bt.Count - 1]] = i;
                SingleO_bt[SingleO_bt.Count - 1].Click += DRBE_LinkViewer_Click; ;

                SingleOP_sp.Children.Add(SingleO_bt[SingleO_bt.Count - 1]);

                Dic_SObt_i[SingleO_bt[SingleO_i.Count - 1]] = i;

                Temp_singleo_bt = SingleO_bt[0];
                Temp_singleo_bt.BorderBrush = green_bright_button_brush;
                i++;
            }
            #endregion

            #region read enable
            i = 0;
            while (i < Link_list.Count)
            {
                ii = 0;
                while (ii < Link_list[i].Count)
                {
                    iii = 0;
                    while (iii < Link_list[i][ii].Count)
                    {
                        if (Link_list[i][ii][iii])
                        {
                            DR_tb[iii].Foreground = green_bright_button_brush;
                            DF_tb[ii].Foreground = green_bright_button_brush;
                            DT_tb[i].Foreground = green_bright_button_brush;
                        }
                        iii++;
                    }
                    ii++;
                }
                i++;
            }

            Presentation_Global_obj_refresh();
            Scan_max = Get_total_scan_number(0);
            Presentation_hide();
            Presentation_Global_resource_show();
            DRBEP_Global_resource_bt.BorderBrush = green_bright_button_brush;
            Presentation_choice_mode = 0;
            #endregion
        }
        public void Setup()
        {

            int i = 0;
            int ii = 0;
            int iii = 0;


            Presentation_setup();
            Presentation_Global_resource_setup();
            Presentation_choice_setup();

            Presentation_Global_obj_setup();
            Presentation_property_setup();
            Presentation_fidelity_setup();
            //Presentation_fidelity_setup();
            Presentation_hide();
            Presentation_Global_resource_show();
            DRBEP_Global_resource_bt.BorderBrush = green_bright_button_brush;
            Presentation_choice_mode = 0;
            //Presentation_fidelity_show();

            //Presentation_property_setup();





            Scenario_gen_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Calculator_icon.png", UriKind.RelativeOrAbsolute));
            Scenario_gen_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Scenario_gen_i,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Scenario_gen_bt);
            Scenario_gen_bt.SetValue(Grid.ColumnProperty, 190);
            Scenario_gen_bt.SetValue(Grid.ColumnSpanProperty, 5);
            Scenario_gen_bt.SetValue(Grid.RowProperty, 0);
            Scenario_gen_bt.SetValue(Grid.RowSpanProperty, 5);
            Scenario_gen_bt.Click += Scenario_gen_bt_Click;

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
            Transmitter_title_tb.SetValue(Grid.ColumnProperty, 3);
            Transmitter_title_tb.SetValue(Grid.ColumnSpanProperty, 16);
            Transmitter_title_tb.SetValue(Grid.RowProperty, 2);
            Transmitter_title_tb.SetValue(Grid.RowSpanProperty, 5);

            DT_sv = new ScrollViewer()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                VerticalScrollMode = ScrollMode.Enabled,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            };
            ParentGrid.Children.Add(DT_sv);
            DT_sv.SetValue(Grid.ColumnProperty, 3);
            DT_sv.SetValue(Grid.ColumnSpanProperty, 16);
            DT_sv.SetValue(Grid.RowProperty, 7);
            DT_sv.SetValue(Grid.RowSpanProperty, 135);

            DTp_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush

            };
            DTp_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            DTp_sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            };
            DT_sv.Content = DTp_gd;
            DTp_gd.Children.Add(DTp_sp);
            //ParentGrid.Children.Add(DRBE_SPL);
            DTp_sp.SetValue(Grid.ColumnProperty, 0);
            DTp_sp.SetValue(Grid.ColumnSpanProperty, 1);

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
            Reflector_title_tb.SetValue(Grid.ColumnProperty, 20);
            Reflector_title_tb.SetValue(Grid.ColumnSpanProperty, 16);
            Reflector_title_tb.SetValue(Grid.RowProperty, 2);
            Reflector_title_tb.SetValue(Grid.RowSpanProperty, 5);

            DF_sv = new ScrollViewer()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                VerticalScrollMode = ScrollMode.Enabled,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            };
            ParentGrid.Children.Add(DF_sv);
            DF_sv.SetValue(Grid.ColumnProperty, 20);
            DF_sv.SetValue(Grid.ColumnSpanProperty, 16);
            DF_sv.SetValue(Grid.RowProperty, 7);
            DF_sv.SetValue(Grid.RowSpanProperty, 135);

            DFp_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush

            };
            DFp_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            DFp_sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            };
            DF_sv.Content = DFp_gd;
            DFp_gd.Children.Add(DFp_sp);
            //ParentGrid.Children.Add(DRBE_SPL);
            DFp_sp.SetValue(Grid.ColumnProperty, 0);
            DFp_sp.SetValue(Grid.ColumnSpanProperty, 1);

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
            Receiver_title_tb.SetValue(Grid.ColumnProperty, 37);
            Receiver_title_tb.SetValue(Grid.ColumnSpanProperty, 16);
            Receiver_title_tb.SetValue(Grid.RowProperty, 2);
            Receiver_title_tb.SetValue(Grid.RowSpanProperty, 5);

            DR_sv = new ScrollViewer()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                VerticalScrollMode = ScrollMode.Enabled,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            };
            ParentGrid.Children.Add(DR_sv);
            DR_sv.SetValue(Grid.ColumnProperty, 37);
            DR_sv.SetValue(Grid.ColumnSpanProperty, 16);
            DR_sv.SetValue(Grid.RowProperty, 7);
            DR_sv.SetValue(Grid.RowSpanProperty, 135);

            DRp_gd = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush

            };
            DRp_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            DRp_sp = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                BorderBrush = dark_grey_brush,
                BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            };
            DR_sv.Content = DRp_gd;
            DRp_gd.Children.Add(DRp_sp);
            //ParentGrid.Children.Add(DRBE_SPL);
            DRp_sp.SetValue(Grid.ColumnProperty, 0);
            DRp_sp.SetValue(Grid.ColumnSpanProperty, 1);


            #region DT
            //i = 0;
            //while (i < Link_list.Count)
            //{
            //    DT_gd.Add(new Grid()
            //    {
            //        VerticalAlignment = VerticalAlignment.Stretch,
            //        HorizontalAlignment = HorizontalAlignment.Stretch,
            //        Background = Default_back_black_color_brush
            //    });

            //    DT_gd[DT_gd.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            //    DT_gd[DT_gd.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


            //    DT_i.Add(new Image()
            //    {
            //        VerticalAlignment = VerticalAlignment.Center,
            //        HorizontalAlignment = HorizontalAlignment.Center,
            //        Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Phase_Array_icon.png", UriKind.RelativeOrAbsolute)),
            //        Height = 20
            //    });
            //    DT_i[DT_i.Count - 1].SetValue(Grid.ColumnProperty, 0);
            //    DT_i[DT_i.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

            //    DT_tb.Add(new TextBlock()
            //    {
            //        VerticalAlignment = VerticalAlignment.Center,
            //        HorizontalAlignment = HorizontalAlignment.Center,
            //        FontSize = 12,
            //        Text = "ID: " + Dic_t_i_obj[i].ID.ToString(),
            //        Foreground = red_bright_button_brush
            //    });
            //    DT_tb[DT_tb.Count - 1].SetValue(Grid.ColumnProperty, 1);
            //    DT_tb[DT_tb.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

            //    DT_sp.Add(new StackPanel()
            //    {
            //        VerticalAlignment = VerticalAlignment.Stretch,
            //        HorizontalAlignment = HorizontalAlignment.Stretch,
            //        Height = 40,
            //        Background = Default_back_black_color_brush
            //    });
            //    DT_sp[DT_sp.Count-1].Children.Add(DT_gd[DT_gd.Count-1]);
            //    DT_gd[DT_gd.Count - 1].Children.Add(DT_i[DT_i.Count - 1]);
            //    DT_gd[DT_gd.Count - 1].Children.Add(DT_tb[DT_tb.Count - 1]);


            //    DT_bt.Add(new Button()
            //    {
            //        VerticalAlignment = VerticalAlignment.Stretch,
            //        HorizontalAlignment = HorizontalAlignment.Stretch,
            //        HorizontalContentAlignment = HorizontalAlignment.Stretch,
            //        VerticalContentAlignment = VerticalAlignment.Stretch,
            //        Background = Default_back_black_color_brush,
            //        Height = 40,
            //        Content = DT_sp[DT_sp.Count - 1],
            //        BorderBrush = dark_grey_brush,
            //        BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            //    });
            //    Dic_tbt_i[DT_bt[DT_i.Count - 1]] = i;
            //    DT_bt[DT_bt.Count - 1].Click += DT_bt_Click;

            //    DTp_sp.Children.Add(DT_bt[DT_bt.Count - 1]);

            //    Dic_tbt_i[DT_bt[DT_i.Count - 1]] = i;
            //    i++;
            //}
            //#endregion

            //#region DRF
            //i = 0;
            //while (i < Link_list[0].Count)
            //{
            //    DF_gd.Add(new Grid()
            //    {
            //        VerticalAlignment = VerticalAlignment.Stretch,
            //        HorizontalAlignment = HorizontalAlignment.Stretch,
            //        Background = Default_back_black_color_brush
            //    });

            //    DF_gd[DF_gd.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            //    DF_gd[DF_gd.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


            //    DF_i.Add(new Image()
            //    {
            //        VerticalAlignment = VerticalAlignment.Center,
            //        HorizontalAlignment = HorizontalAlignment.Center,
            //        Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Object_icon.png", UriKind.RelativeOrAbsolute)),
            //        Height = 20
            //    });
            //    DF_i[DF_i.Count - 1].SetValue(Grid.ColumnProperty, 0);
            //    DF_i[DF_i.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

            //    DF_tb.Add(new TextBlock()
            //    {
            //        VerticalAlignment = VerticalAlignment.Center,
            //        HorizontalAlignment = HorizontalAlignment.Center,
            //        FontSize = 12,
            //        Text = "ID: " + Dic_o_i_obj[i].ID.ToString(),
            //        Foreground = red_bright_button_brush
            //    });
            //    DF_tb[DF_tb.Count - 1].SetValue(Grid.ColumnProperty, 1);
            //    DF_tb[DF_tb.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

            //    DF_sp.Add(new StackPanel()
            //    {
            //        VerticalAlignment = VerticalAlignment.Stretch,
            //        HorizontalAlignment = HorizontalAlignment.Stretch,
            //        Height = 40,
            //        Background = Default_back_black_color_brush
            //    });
            //    DF_sp[DF_sp.Count - 1].Children.Add(DF_gd[DF_gd.Count - 1]);
            //    DF_gd[DF_gd.Count - 1].Children.Add(DF_i[DF_i.Count - 1]);
            //    DF_gd[DF_gd.Count - 1].Children.Add(DF_tb[DF_tb.Count - 1]);


            //    DF_bt.Add(new Button()
            //    {
            //        VerticalAlignment = VerticalAlignment.Stretch,
            //        HorizontalAlignment = HorizontalAlignment.Stretch,
            //        HorizontalContentAlignment = HorizontalAlignment.Stretch,
            //        VerticalContentAlignment = VerticalAlignment.Stretch,
            //        Background = Default_back_black_color_brush,
            //        Height = 40,
            //        Content = DF_sp[DF_sp.Count - 1],
            //        BorderBrush = dark_grey_brush,
            //        BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            //    });
            //    Dic_pbt_i[DF_bt[DF_i.Count - 1]] = i;
            //    DF_bt[DF_bt.Count - 1].Click += DF_bt_Click;

            //    DFp_sp.Children.Add(DF_bt[DF_bt.Count - 1]);


            //    i++;
            //}
            //#endregion

            //#region DR
            //i = 0;
            //while (i < Link_list[0][0].Count)
            //{
            //    DR_gd.Add(new Grid()
            //    {
            //        VerticalAlignment = VerticalAlignment.Stretch,
            //        HorizontalAlignment = HorizontalAlignment.Stretch,
            //        Background = Default_back_black_color_brush
            //    });

            //    DR_gd[DR_gd.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            //    DR_gd[DR_gd.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


            //    DR_i.Add(new Image()
            //    {
            //        VerticalAlignment = VerticalAlignment.Center,
            //        HorizontalAlignment = HorizontalAlignment.Center,
            //        Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Phase_Array_icon_t.png", UriKind.RelativeOrAbsolute)),
            //        Height = 20
            //    });
            //    DR_i[DR_i.Count - 1].SetValue(Grid.ColumnProperty, 0);
            //    DR_i[DR_i.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

            //    DR_tb.Add(new TextBlock()
            //    {
            //        VerticalAlignment = VerticalAlignment.Center,
            //        HorizontalAlignment = HorizontalAlignment.Center,
            //        FontSize = 12,
            //        Text = "ID: " + Dic_r_i_obj[i].ID.ToString(),
            //        Foreground = red_bright_button_brush
            //    });
            //    DR_tb[DR_tb.Count - 1].SetValue(Grid.ColumnProperty, 1);
            //    DR_tb[DR_tb.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

            //    DR_sp.Add(new StackPanel()
            //    {
            //        VerticalAlignment = VerticalAlignment.Stretch,
            //        HorizontalAlignment = HorizontalAlignment.Stretch,
            //        Height = 40,
            //        Background = Default_back_black_color_brush
            //    });
            //    DR_sp[DR_sp.Count - 1].Children.Add(DR_gd[DR_gd.Count - 1]);
            //    DR_gd[DR_gd.Count - 1].Children.Add(DR_i[DR_i.Count - 1]);
            //    DR_gd[DR_gd.Count - 1].Children.Add(DR_tb[DR_tb.Count - 1]);


            //    DR_bt.Add(new Button()
            //    {
            //        VerticalAlignment = VerticalAlignment.Stretch,
            //        HorizontalAlignment = HorizontalAlignment.Stretch,
            //        HorizontalContentAlignment = HorizontalAlignment.Stretch,
            //        VerticalContentAlignment = VerticalAlignment.Stretch,
            //        Background = Default_back_black_color_brush,
            //        Height = 40,
            //        Content = DR_sp[DR_sp.Count - 1],
            //        BorderBrush = dark_grey_brush,
            //        BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            //    });
            //    Dic_rbt_i[DR_bt[DR_i.Count - 1]] = i;
            //    DR_bt[DR_bt.Count - 1].Click += DR_bt_Click;

            //    DRp_sp.Children.Add(DR_bt[DR_bt.Count - 1]);

            //    i++;
            //}
            //#endregion

            //#region Single O
            //SingleO_title_tb = new TextBlock()
            //{
            //    VerticalAlignment = VerticalAlignment.Stretch,
            //    HorizontalAlignment = HorizontalAlignment.Stretch,
            //    HorizontalTextAlignment = TextAlignment.Center,
            //    Text = "Object",
            //    Foreground = white_button_brush,
            //    FontSize = 14,
            //    FontWeight = FontWeights.Bold
            //};
            //ParentGrid.Children.Add(SingleO_title_tb);
            //SingleO_title_tb.SetValue(Grid.ColumnProperty, 65);
            //SingleO_title_tb.SetValue(Grid.ColumnSpanProperty, 16);
            //SingleO_title_tb.SetValue(Grid.RowProperty, 2);
            //SingleO_title_tb.SetValue(Grid.RowSpanProperty, 5);

            //SingleOP_sv = new ScrollViewer()
            //{
            //    VerticalAlignment = VerticalAlignment.Stretch,
            //    HorizontalAlignment = HorizontalAlignment.Stretch,
            //    Background = Default_back_black_color_brush,
            //    VerticalScrollMode = ScrollMode.Enabled,
            //    BorderBrush = dark_grey_brush,
            //    BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            //};
            //ParentGrid.Children.Add(SingleOP_sv);
            //SingleOP_sv.SetValue(Grid.ColumnProperty, 65);
            //SingleOP_sv.SetValue(Grid.ColumnSpanProperty, 16);
            //SingleOP_sv.SetValue(Grid.RowProperty, 7);
            //SingleOP_sv.SetValue(Grid.RowSpanProperty, 135);

            //SingleOP_gd = new Grid()
            //{
            //    VerticalAlignment = VerticalAlignment.Stretch,
            //    HorizontalAlignment = HorizontalAlignment.Stretch,
            //    Background = Default_back_black_color_brush

            //};
            //SingleOP_gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            //SingleOP_sp = new StackPanel()
            //{
            //    VerticalAlignment = VerticalAlignment.Stretch,
            //    HorizontalAlignment = HorizontalAlignment.Stretch,
            //    Background = Default_back_black_color_brush,
            //    BorderBrush = dark_grey_brush,
            //    BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            //};
            //SingleOP_sv.Content = SingleOP_gd;
            //SingleOP_gd.Children.Add(SingleOP_sp);
            ////ParentGrid.Children.Add(DRBE_SPL);
            //SingleOP_sp.SetValue(Grid.ColumnProperty, 0);
            //SingleOP_sp.SetValue(Grid.ColumnSpanProperty, 1);
            //i = 0;
            //while (i < DRBE_obj_list.Count)
            //{
            //    SingleO_gd.Add(new Grid()
            //    {
            //        VerticalAlignment = VerticalAlignment.Stretch,
            //        HorizontalAlignment = HorizontalAlignment.Stretch,
            //        Background = Default_back_black_color_brush
            //    });

            //    SingleO_gd[SingleO_gd.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            //    SingleO_gd[SingleO_gd.Count - 1].ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });


            //    SingleO_i.Add(new Image()
            //    {
            //        VerticalAlignment = VerticalAlignment.Center,
            //        HorizontalAlignment = HorizontalAlignment.Center,
            //        Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Phase_Array_icon.png", UriKind.RelativeOrAbsolute)),
            //        Height = 20
            //    });
            //    SingleO_i[SingleO_i.Count - 1].SetValue(Grid.ColumnProperty, 0);
            //    SingleO_i[SingleO_i.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

            //    SingleO_tb.Add(new TextBlock()
            //    {
            //        VerticalAlignment = VerticalAlignment.Center,
            //        HorizontalAlignment = HorizontalAlignment.Center,
            //        FontSize = 12,
            //        Text = "ID: " + DRBE_obj_list[i].ID.ToString(),
            //        Foreground = white_button_brush
            //    });
            //    SingleO_tb[SingleO_tb.Count - 1].SetValue(Grid.ColumnProperty, 1);
            //    SingleO_tb[SingleO_tb.Count - 1].SetValue(Grid.ColumnSpanProperty, 1);

            //    SingleO_sp.Add(new StackPanel()
            //    {
            //        VerticalAlignment = VerticalAlignment.Stretch,
            //        HorizontalAlignment = HorizontalAlignment.Stretch,
            //        Height = 40,
            //        Background = Default_back_black_color_brush
            //    });
            //    SingleO_sp[SingleO_sp.Count - 1].Children.Add(SingleO_gd[SingleO_gd.Count - 1]);
            //    SingleO_gd[SingleO_gd.Count - 1].Children.Add(SingleO_i[SingleO_i.Count - 1]);
            //    SingleO_gd[SingleO_gd.Count - 1].Children.Add(SingleO_tb[SingleO_tb.Count - 1]);


            //    SingleO_bt.Add(new Button()
            //    {
            //        VerticalAlignment = VerticalAlignment.Stretch,
            //        HorizontalAlignment = HorizontalAlignment.Stretch,
            //        HorizontalContentAlignment = HorizontalAlignment.Stretch,
            //        VerticalContentAlignment = VerticalAlignment.Stretch,
            //        Background = Default_back_black_color_brush,
            //        Height = 40,
            //        Content = SingleO_sp[SingleO_sp.Count - 1],
            //        BorderBrush = dark_grey_brush,
            //        BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5)
            //    });
            //    Dic_SObt_i[SingleO_bt[SingleO_bt.Count - 1]] = i;
            //    SingleO_bt[SingleO_bt.Count - 1].Click += DRBE_LinkViewer_Click; ;

            //    SingleOP_sp.Children.Add(SingleO_bt[SingleO_bt.Count - 1]);

            //    Dic_SObt_i[SingleO_bt[SingleO_i.Count - 1]] = i;

            //    Temp_singleo_bt = SingleO_bt[0];
            //    Temp_singleo_bt.BorderBrush = green_bright_button_brush;
            //    i++;
            //}
            #endregion

            #region Link deactive
            Link_de_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 12,
                FontWeight = FontWeights.Bold,
                Foreground = Sky_green_color,
                Text = "Disable All"

            };
            ParentGrid.Children.Add(Link_de_tb);
            Link_de_tb.SetValue(Grid.ColumnProperty, 55);
            Link_de_tb.SetValue(Grid.ColumnSpanProperty, 10);
            Link_de_tb.SetValue(Grid.RowProperty, 30);
            Link_de_tb.SetValue(Grid.RowSpanProperty, 5);

            Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/All_de_icon.png", UriKind.RelativeOrAbsolute));
            Link_de_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Link_de_bt_i,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Link_de_bt);
            Link_de_bt.SetValue(Grid.ColumnProperty, 55);
            Link_de_bt.SetValue(Grid.ColumnSpanProperty, 10);
            Link_de_bt.SetValue(Grid.RowProperty, 35);
            Link_de_bt.SetValue(Grid.RowSpanProperty, 10);
            Link_de_bt.Click += Link_de_bt_Click;
            #endregion

            #region Link enactive
            Link_en_tb = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 12,
                FontWeight = FontWeights.Bold,
                Foreground = Sky_green_color,
                Text = "Enable All"

            };
            ParentGrid.Children.Add(Link_en_tb);
            Link_en_tb.SetValue(Grid.ColumnProperty, 55);
            Link_en_tb.SetValue(Grid.ColumnSpanProperty, 10);
            Link_en_tb.SetValue(Grid.RowProperty, 60);
            Link_en_tb.SetValue(Grid.RowSpanProperty, 5);

            Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/En_all_icon.png", UriKind.RelativeOrAbsolute));
            Link_en_bt = new Button()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                Background = Default_back_black_color_brush,
                Content = Link_en_bt_i,
                Foreground = white_button_brush,
                FontSize = 18
            };
            ParentGrid.Children.Add(Link_en_bt);
            Link_en_bt.SetValue(Grid.ColumnProperty, 55);
            Link_en_bt.SetValue(Grid.ColumnSpanProperty, 10);
            Link_en_bt.SetValue(Grid.RowProperty, 65);
            Link_en_bt.SetValue(Grid.RowSpanProperty, 10);
            Link_en_bt.Click += Link_en_bt_Click;
            #endregion


            //DT_bt[2].BorderBrush = green_bright_button_brush;
            //DF_bt[5].BorderBrush = green_bright_button_brush;
            //DR_bt[1].BorderBrush = green_bright_button_brush;
            Ref_Antenna_bt.BorderBrush = green_bright_button_brush;
            Link_de_tb.Text = "Disable Scenario";
            Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/All_de_icon.png", UriKind.RelativeOrAbsolute));
            Link_en_tb.Text = "Enable Scenario";
            Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/En_all_icon.png", UriKind.RelativeOrAbsolute));
            //testchain();


            //hide();
        }

        private void Scenario_gen_bt_Click(object sender, RoutedEventArgs e)
        {
            ParentPage.DRBE_SG.show();
            hide();
        }
        public void show()
        {
            Simulation_bt.Visibility = Visibility.Visible;

            Text_tb.Visibility = Visibility.Visible;

            CMP_bt.Visibility = Visibility.Visible;

            AMT_bt.Visibility = Visibility.Visible;

            AMT_tb.Visibility = Visibility.Visible;

            Obj_save_tb.Visibility = Visibility.Visible;

            Obj_save_bt.Visibility = Visibility.Visible;

            Obj_summary_bt.Visibility = Visibility.Visible;

            Obj_summary_tb.Visibility = Visibility.Visible;

            Sweep_bt.Visibility = Visibility.Visible;

            Sweep_tb.Visibility = Visibility.Visible;

            Link_uns_bt.Visibility = Visibility.Visible;
            Link_uns_tb.Visibility = Visibility.Visible;

            Link_en_bt.Visibility = Visibility.Visible;
            Link_en_tb.Visibility = Visibility.Visible;

            Link_de_bt.Visibility = Visibility.Visible;
            Link_de_tb.Visibility = Visibility.Visible;

            Transmitter_title_tb.Visibility = Visibility.Visible;

            Reflector_title_tb.Visibility = Visibility.Visible;
            Receiver_title_tb.Visibility = Visibility.Visible;

            DF_sv.Visibility = Visibility.Visible;
            DT_sv.Visibility = Visibility.Visible;
            DR_sv.Visibility = Visibility.Visible;
            RS_sv.Visibility = Visibility.Visible;
            SingleOP_sv.Visibility = Visibility.Visible;
            SingleO_title_tb.Visibility = Visibility.Visible;


            Ref_Clutter_bt.Visibility = Visibility.Visible;

            Scenario_gen_bt.Visibility = Visibility.Visible;
            Presentation_choice_show();
        }
        public void hide()
        {
            int i = 0;

            Scenario_gen_bt.Visibility = Visibility.Collapsed;

            Simulation_bt.Visibility = Visibility.Collapsed;

            Text_tb.Visibility = Visibility.Collapsed;
            Ref_AO0_bt.Visibility = Visibility.Collapsed;

            Ref_AO0_tb.Visibility = Visibility.Collapsed;

            Ref_AO1_bt.Visibility = Visibility.Collapsed;

            Ref_AO1_tb.Visibility = Visibility.Collapsed;

            Ref_AO2_bt.Visibility = Visibility.Collapsed;

            Ref_AO2_tb.Visibility = Visibility.Collapsed;

            Ref_AO3_bt.Visibility = Visibility.Collapsed;

            Ref_AO3_tb.Visibility = Visibility.Collapsed;

            Ref_AO4_bt.Visibility = Visibility.Collapsed;

            Ref_AO4_tb.Visibility = Visibility.Collapsed;

            Ref_AO5_bt.Visibility = Visibility.Collapsed;

            Ref_AO5_tb.Visibility = Visibility.Collapsed;

            Ref_AO6_bt.Visibility = Visibility.Collapsed;

            Ref_AO6_tb.Visibility = Visibility.Collapsed;

            CMP_bt.Visibility = Visibility.Collapsed;

            AMT_bt.Visibility = Visibility.Collapsed;

            AMT_tb.Visibility = Visibility.Collapsed;

            Obj_save_tb.Visibility = Visibility.Collapsed;

            Obj_save_bt.Visibility = Visibility.Collapsed;

            Obj_summary_bt.Visibility = Visibility.Collapsed;

            Obj_summary_tb.Visibility = Visibility.Collapsed;

            Sweep_bt.Visibility = Visibility.Collapsed;

            Sweep_tb.Visibility = Visibility.Collapsed;

            Link_uns_bt.Visibility = Visibility.Collapsed;
            Link_uns_tb.Visibility = Visibility.Collapsed;

            Link_en_bt.Visibility = Visibility.Collapsed;
            Link_en_tb.Visibility = Visibility.Collapsed;

            Link_de_bt.Visibility = Visibility.Collapsed;
            Link_de_tb.Visibility = Visibility.Collapsed;

            Transmitter_title_tb.Visibility = Visibility.Collapsed;

            Reflector_title_tb.Visibility = Visibility.Collapsed;
            Receiver_title_tb.Visibility = Visibility.Collapsed;

            //i = 0;
            //while (i < DR_bt.Count)
            //{
            //    DR_bt[i].Visibility = Visibility.Collapsed;
            //    i++;
            //}
            //i = 0;
            //while (i < DR_tb.Count)
            //{
            //    DR_tb[i].Visibility = Visibility.Collapsed;
            //    i++;
            //}
            //i = 0;
            //while (i < DF_bt.Count)
            //{
            //    DF_bt[i].Visibility = Visibility.Collapsed;
            //    i++;
            //}
            //i = 0;
            //while (i < DF_tb.Count)
            //{
            //    DF_tb[i].Visibility = Visibility.Collapsed;
            //    i++;
            //}
            //i = 0;
            //while (i < DT_bt.Count)
            //{
            //    DT_bt[i].Visibility = Visibility.Collapsed;
            //    i++;
            //}
            //i = 0;
            //while (i < DT_tb.Count)
            //{
            //    DT_tb[i].Visibility = Visibility.Collapsed;
            //    i++;
            //}

            DF_sv.Visibility = Visibility.Collapsed;
            DT_sv.Visibility = Visibility.Collapsed;
            DR_sv.Visibility = Visibility.Collapsed;
            RS_sv.Visibility = Visibility.Collapsed;
            SingleOP_sv.Visibility = Visibility.Collapsed;
            SingleO_title_tb.Visibility = Visibility.Collapsed;

            Ref_Antenna_bt.Visibility = Visibility.Collapsed;
            Ref_Doppler_bt.Visibility = Visibility.Collapsed;
            Ref_Polarization_bt.Visibility = Visibility.Collapsed;
            Ref_RCS_bt.Visibility = Visibility.Collapsed;
            Ref_RFim_bt.Visibility = Visibility.Collapsed;

            Ref_Clutter_bt.Visibility = Visibility.Collapsed;
            Presentation_choice_hide();

        }
        private void DRBE_LinkViewer_Click(object sender, RoutedEventArgs e)
        {
            Button yoo = sender as Button;
            Temp_singleo_bt.BorderBrush = white_button_brush;
            yoo.BorderBrush = green_bright_button_brush;
            Temp_singleo_bt = yoo;
        }

        private int Get_total_scan_number(int mode)
        {
            int i = 0;
            int ii = 0;
            int iii = 0;
            int result = 0;
            if(mode == 0) //total links
            {
                i = 0;
                while(i < Link_list.Count)
                {
                    ii = 0;
                    while(ii < Link_list[i].Count)
                    {
                        iii = 0;
                        while(iii<Link_list[i][ii].Count)
                        {
                            if(Link_list[i][ii][iii])
                            {
                                Dic_t_i_obj[i].number_of_path++;
                                Dic_o_i_obj[ii].number_of_path++;
                                Dic_r_i_obj[iii].number_of_path++;
                                result++;
                            }
                            iii++;
                        }
                        ii++;
                    }
                    i++;
                }

            }
            result += DRBE_obj_list.Count;
            return result;
        }

        private void Get_class_result_refresh()
        {
            int i = 0;
            i = 0;
            while(i<DRBE_obj_list.Count)
            {
                DRBE_obj_list[i].RCS_Compute = 0;
                DRBE_obj_list[i].ANT_Compute = 0;
                DRBE_obj_list[i].TU_Compute = 0;
                DRBE_obj_list[i].NRE_Compute = 0;
                DRBE_obj_list[i].ORI_Compute = 0;
                DRBE_obj_list[i].PG_Compute = 0;
                DRBE_obj_list[i].COR_Compute = 0;

                DRBE_obj_list[i].RCS_Bandwidth = 0;
                DRBE_obj_list[i].ANT_Bandwidth = 0;
                DRBE_obj_list[i].TU_Bandwidth = 0;
                DRBE_obj_list[i].NRE_Bandwidth = 0;
                DRBE_obj_list[i].ORI_Bandwidth = 0;
                DRBE_obj_list[i].PG_Bandwidth = 0;
                DRBE_obj_list[i].COR_Bandwidth = 0;

                DRBE_obj_list[i].RCS_Latency = 0;
                DRBE_obj_list[i].ANT_Latency = 0;
                DRBE_obj_list[i].TU_Latency = 0;
                DRBE_obj_list[i].NRE_Latency = 0;
                DRBE_obj_list[i].ORI_Latency = 0;
                DRBE_obj_list[i].PG_Latency = 0;
                DRBE_obj_list[i].COR_Latency = 0;

                DRBE_obj_list[i].RCS_Memory = 0;
                DRBE_obj_list[i].ANT_Memory = 0;
                DRBE_obj_list[i].TU_Memory = 0;
                DRBE_obj_list[i].NRE_Memory = 0;
                DRBE_obj_list[i].ORI_Memory = 0;
                DRBE_obj_list[i].PG_Memory = 0;
                DRBE_obj_list[i].COR_Memory = 0;
                i++;
            }

            RCS_Compute = 0;
            ANT_Compute = 0;
            TU_Compute = 0;
            NRE_Compute = 0;
            ORI_Compute = 0;
            PG_Compute = 0;
            COR_Compute = 0;

            RCS_Bandwidth = 0;
            ANT_Bandwidth = 0;
            TU_Bandwidth = 0;
            NRE_Bandwidth = 0;
            ORI_Bandwidth = 0;
            PG_Bandwidth = 0;
            COR_Bandwidth = 0;

            RCS_Latency = 0;
            ANT_Latency = 0;
            TU_Latency = 0;
            NRE_Latency = 0;
            ORI_Latency = 0;
            PG_Latency = 0;
            COR_Latency = 0;

            RCS_Memory = 0;
            ANT_Memory = 0;
            TU_Memory = 0;
            NRE_Memory = 0;
            ORI_Memory = 0;
            PG_Memory = 0;
            COR_Memory = 0;

            Computation_max = 0;
            Memory_max = 0;
            Latency_max = 0;
            Bandwidth_max = 0;
        }
        private void Get_obj_class_result_update(List<double> x, int i, int mode)
        {
            if(mode == 0) //Antenna
            {
                
                //DRBE_obj_list[i].ANT_Bandwidth += x[17];
                DRBE_obj_list[i].ANT_Memory += x[16];
                DRBE_obj_list[i].COR_Memory += x[4];
                DRBE_obj_list[i].COR_Compute += x[3];
                DRBE_obj_list[i].COR_Bandwidth += x[5];
                ANT_Memory += x[16];

                DRBE_obj_list[i].ANT_Latency = Math.Max(DRBE_obj_list[i].ANT_Latency, x[18]);
                DRBE_obj_list[i].COR_Latency = x[6];
                ANT_Latency = Math.Max(ANT_Latency, x[18]);

                COR_Compute += x[3];
                COR_Memory += x[4];

            }
            else if(mode == 1)
            {
                DRBE_obj_list[i].RCS_Memory += x[24];
                DRBE_obj_list[i].COR_Memory += x[4];
                DRBE_obj_list[i].COR_Compute += x[3];
                DRBE_obj_list[i].COR_Bandwidth += x[5];
                //DRBE_obj_list[i].RCS_Bandwidth += x[25];
                RCS_Memory += x[24];

                DRBE_obj_list[i].RCS_Latency = Math.Max(DRBE_obj_list[i].RCS_Latency, x[26]);
                DRBE_obj_list[i].COR_Latency = x[6];
                RCS_Latency = Math.Max(DRBE_obj_list[i].RCS_Latency, x[26]);

                COR_Compute += x[3];
                COR_Memory += x[4];

            }
            else if(mode == 2)
            {
                //DRBE_obj_list[i].ANT_Bandwidth += x[17];
                DRBE_obj_list[i].ANT_Memory += x[16];
                DRBE_obj_list[i].COR_Memory += x[4];
                DRBE_obj_list[i].COR_Compute += x[3];
                DRBE_obj_list[i].COR_Bandwidth += x[5];
                ANT_Memory += x[16];

                DRBE_obj_list[i].ANT_Latency = Math.Max(DRBE_obj_list[i].ANT_Latency, x[18]);
                DRBE_obj_list[i].COR_Latency = x[6];
                ANT_Latency = Math.Max(ANT_Latency, x[18]);

                COR_Compute += x[3];
                COR_Memory += x[4];
            }
            else
            {

            }

            Computation_max = RCS_Compute + ANT_Compute + COR_Compute + TU_Compute + ORI_Compute + PG_Compute + NRE_Compute;
            Memory_max = RCS_Memory + ANT_Memory + COR_Memory + TU_Memory + ORI_Memory + PG_Memory + NRE_Memory;
            Latency_max = RCS_Latency + ANT_Latency + COR_Latency + TU_Latency + ORI_Latency + PG_Latency + NRE_Latency;

        }
        private void Get_link_class_tr_result_update(List<double> x, int i, int ii, int iii)
        {


            //Dic_t_i_obj[i].RCS_Compute += x[23];
            Dic_t_i_obj[i].ANT_Compute += x[15];
            Dic_t_i_obj[i].TU_Compute += x[27];
            Dic_t_i_obj[i].NRE_Compute += x[7];
            Dic_t_i_obj[i].ORI_Compute += x[11];
            Dic_t_i_obj[i].PG_Compute += x[19];

            //Dic_t_i_obj[i].RCS_Bandwidth += x[25];
            Dic_t_i_obj[i].ANT_Bandwidth += x[17];
            Dic_t_i_obj[i].TU_Bandwidth += x[29];
            Dic_t_i_obj[i].NRE_Bandwidth += x[9];
            Dic_t_i_obj[i].ORI_Bandwidth += x[13];
            Dic_t_i_obj[i].PG_Bandwidth += x[21];

            Dic_t_i_obj[i].TU_Memory += x[8];
            Dic_t_i_obj[i].NRE_Memory += x[28];
            Dic_t_i_obj[i].ORI_Memory += x[12];
            Dic_t_i_obj[i].PG_Memory += x[20];

            Dic_t_i_obj[i].TU_Latency = Math.Max(Dic_t_i_obj[i].TU_Latency, x[30]);
            Dic_t_i_obj[i].NRE_Latency = Math.Max(Dic_t_i_obj[i].NRE_Latency, x[10]);
            Dic_t_i_obj[i].ORI_Latency = Math.Max(Dic_t_i_obj[i].ORI_Latency, x[14]);
            Dic_t_i_obj[i].PG_Latency = Math.Max(Dic_t_i_obj[i].PG_Latency, x[22]);

            //RCS_Compute += x[23];
            ANT_Compute += x[15];
            TU_Compute += x[27];
            NRE_Compute += x[7];
            ORI_Compute += x[11];
            PG_Compute += x[19];

            //RCS_Bandwidth += x[25];
            ANT_Bandwidth += x[17];
            TU_Bandwidth += x[29];
            NRE_Bandwidth += x[9];
            ORI_Bandwidth += x[13];
            PG_Bandwidth += x[21];


            TU_Latency = Math.Max(TU_Latency, x[30]);
            NRE_Latency = Math.Max(NRE_Latency, x[10]);
            ORI_Latency = Math.Max(ORI_Latency, x[14]);
            PG_Latency = Math.Max(PG_Latency, x[22]);

            TU_Memory += x[8];
            NRE_Memory += x[28];
            ORI_Memory += x[12];
            PG_Memory += x[20];

            Computation_max = RCS_Compute + ANT_Compute + COR_Compute + TU_Compute + ORI_Compute + PG_Compute + NRE_Compute;
            Memory_max = RCS_Memory + ANT_Memory + COR_Memory + TU_Memory + ORI_Memory + PG_Memory + NRE_Memory;
            Latency_max = RCS_Latency + ANT_Latency + COR_Latency + TU_Latency + ORI_Latency + PG_Latency + NRE_Latency;
            Bandwidth_max = RCS_Bandwidth + ANT_Bandwidth + COR_Bandwidth + TU_Bandwidth + ORI_Bandwidth + PG_Bandwidth + NRE_Bandwidth;
        }
        private void Get_link_class_o_result_update(List<double> x, int i, int ii, int iii)
        {


            Dic_o_i_obj[ii].RCS_Compute += x[23];
            //Dic_o_i_obj[ii].ANT_Compute += x[15];
            Dic_o_i_obj[ii].TU_Compute += x[27];
            Dic_o_i_obj[ii].NRE_Compute += x[7];
            Dic_o_i_obj[ii].ORI_Compute += x[11];
            Dic_o_i_obj[ii].PG_Compute += x[19];

            Dic_o_i_obj[ii].RCS_Bandwidth += x[25];
            //Dic_o_i_obj[ii].ANT_Bandwidth += x[17];
            Dic_o_i_obj[ii].TU_Bandwidth += x[29];
            Dic_o_i_obj[ii].NRE_Bandwidth += x[9];
            Dic_o_i_obj[ii].ORI_Bandwidth += x[13];
            Dic_o_i_obj[ii].PG_Bandwidth += x[21];

            Dic_o_i_obj[ii].TU_Memory += x[8];
            Dic_o_i_obj[ii].NRE_Memory += x[28];
            Dic_o_i_obj[ii].ORI_Memory += x[12];
            Dic_o_i_obj[ii].PG_Memory += x[20];

            Dic_o_i_obj[ii].TU_Latency = Math.Max(Dic_o_i_obj[ii].TU_Latency, x[30]);
            Dic_o_i_obj[ii].NRE_Latency = Math.Max(Dic_o_i_obj[ii].NRE_Latency, x[10]);
            Dic_o_i_obj[ii].ORI_Latency = Math.Max(Dic_o_i_obj[ii].ORI_Latency, x[14]);
            Dic_o_i_obj[ii].PG_Latency = Math.Max(Dic_o_i_obj[ii].PG_Latency, x[22]);

            RCS_Compute += x[23];
            //ANT_Compute += x[15];
            TU_Compute += x[27];
            NRE_Compute += x[7];
            ORI_Compute += x[11];
            PG_Compute += x[19];

            RCS_Bandwidth += x[25];
            //ANT_Bandwidth += x[17];
            TU_Bandwidth += x[29];
            NRE_Bandwidth += x[9];
            ORI_Bandwidth += x[13];
            PG_Bandwidth += x[21];


            TU_Latency = Math.Max(TU_Latency, x[30]);
            NRE_Latency = Math.Max(NRE_Latency, x[10]);
            ORI_Latency = Math.Max(ORI_Latency, x[14]);
            PG_Latency = Math.Max(PG_Latency, x[22]);

            TU_Memory += x[8];
            NRE_Memory += x[28];
            ORI_Memory += x[12];
            PG_Memory += x[20];

            Computation_max = RCS_Compute + ANT_Compute + COR_Compute + TU_Compute + ORI_Compute + PG_Compute + NRE_Compute;
            Memory_max = RCS_Memory + ANT_Memory + COR_Memory + TU_Memory + ORI_Memory + PG_Memory + NRE_Memory;
            Latency_max = RCS_Latency + ANT_Latency + COR_Latency + TU_Latency + ORI_Latency + PG_Latency + NRE_Latency;
            Bandwidth_max = RCS_Bandwidth + ANT_Bandwidth + COR_Bandwidth + TU_Bandwidth + ORI_Bandwidth + PG_Bandwidth + NRE_Bandwidth;
        }
        private void Get_link_class_result_update(List<double> x, int i, int ii, int iii)
        {


            //Dic_t_i_obj[i].RCS_Compute += x[23];
            Dic_t_i_obj[i].ANT_Compute += x[15];
            Dic_t_i_obj[i].TU_Compute += x[27];
            Dic_t_i_obj[i].NRE_Compute += x[7];
            Dic_t_i_obj[i].ORI_Compute += x[11];
            Dic_t_i_obj[i].PG_Compute += x[19];

            //Dic_t_i_obj[i].RCS_Bandwidth += x[25];
            Dic_t_i_obj[i].ANT_Bandwidth += x[17];
            Dic_t_i_obj[i].TU_Bandwidth += x[29];
            Dic_t_i_obj[i].NRE_Bandwidth += x[9];
            Dic_t_i_obj[i].ORI_Bandwidth += x[13];
            Dic_t_i_obj[i].PG_Bandwidth += x[21];

            Dic_t_i_obj[i].TU_Memory += x[8];
            Dic_t_i_obj[i].NRE_Memory += x[28];
            Dic_t_i_obj[i].ORI_Memory += x[12];
            Dic_t_i_obj[i].PG_Memory += x[20];

            Dic_t_i_obj[i].TU_Latency = Math.Max(Dic_t_i_obj[i].TU_Latency, x[30]);
            Dic_t_i_obj[i].NRE_Latency = Math.Max(Dic_t_i_obj[i].NRE_Latency, x[10]);
            Dic_t_i_obj[i].ORI_Latency = Math.Max(Dic_t_i_obj[i].ORI_Latency, x[14]);
            Dic_t_i_obj[i].PG_Latency = Math.Max(Dic_t_i_obj[i].PG_Latency, x[22]);

            Dic_o_i_obj[ii].RCS_Compute += x[23];
            //Dic_o_i_obj[ii].ANT_Compute += x[15];
            Dic_o_i_obj[ii].TU_Compute += x[27];
            Dic_o_i_obj[ii].NRE_Compute += x[7];
            Dic_o_i_obj[ii].ORI_Compute += x[11];
            Dic_o_i_obj[ii].PG_Compute += x[19];

            Dic_o_i_obj[ii].RCS_Bandwidth += x[25];
            //Dic_o_i_obj[ii].ANT_Bandwidth += x[17];
            Dic_o_i_obj[ii].TU_Bandwidth += x[29];
            Dic_o_i_obj[ii].NRE_Bandwidth += x[9];
            Dic_o_i_obj[ii].ORI_Bandwidth += x[13];
            Dic_o_i_obj[ii].PG_Bandwidth += x[21];

            Dic_o_i_obj[ii].TU_Memory += x[8];
            Dic_o_i_obj[ii].NRE_Memory += x[28];
            Dic_o_i_obj[ii].ORI_Memory += x[12];
            Dic_o_i_obj[ii].PG_Memory += x[20];

            Dic_o_i_obj[ii].TU_Latency = Math.Max(Dic_o_i_obj[ii].TU_Latency, x[30]);
            Dic_o_i_obj[ii].NRE_Latency = Math.Max(Dic_o_i_obj[ii].NRE_Latency, x[10]);
            Dic_o_i_obj[ii].ORI_Latency = Math.Max(Dic_o_i_obj[ii].ORI_Latency, x[14]);
            Dic_o_i_obj[ii].PG_Latency = Math.Max(Dic_o_i_obj[ii].PG_Latency, x[22]);




            ////Dic_r_i_obj[iii].RCS_Compute += x[23];
            //Dic_r_i_obj[iii].ANT_Compute += x[15];
            //Dic_r_i_obj[iii].TU_Compute += x[27];
            //Dic_r_i_obj[iii].NRE_Compute += x[7];
            //Dic_r_i_obj[iii].ORI_Compute += x[11];
            //Dic_r_i_obj[iii].PG_Compute += x[19];

            ////Dic_r_i_obj[iii].RCS_Bandwidth += x[25];
            //Dic_r_i_obj[iii].ANT_Bandwidth += x[17];
            //Dic_r_i_obj[iii].TU_Bandwidth += x[29];
            //Dic_r_i_obj[iii].NRE_Bandwidth += x[9];
            //Dic_r_i_obj[iii].ORI_Bandwidth += x[13];
            //Dic_r_i_obj[iii].PG_Bandwidth += x[21];

            //Dic_r_i_obj[iii].TU_Memory += x[8];
            //Dic_r_i_obj[iii].NRE_Memory += x[28];
            //Dic_r_i_obj[iii].ORI_Memory += x[12];
            //Dic_r_i_obj[iii].PG_Memory += x[20];

            //Dic_r_i_obj[iii].TU_Latency = Math.Max(Dic_r_i_obj[iii].TU_Latency, x[30]);
            //Dic_r_i_obj[iii].NRE_Latency = Math.Max(Dic_r_i_obj[iii].NRE_Latency, x[10]);
            //Dic_r_i_obj[iii].ORI_Latency = Math.Max(Dic_r_i_obj[iii].ORI_Latency, x[14]);
            //Dic_r_i_obj[iii].PG_Latency = Math.Max(Dic_r_i_obj[iii].PG_Latency, x[22]);





            RCS_Compute += x[23];
            ANT_Compute += x[15];
            TU_Compute += x[27];
            NRE_Compute += x[7];
            ORI_Compute += x[11];
            PG_Compute += x[19];

            RCS_Bandwidth += x[25];
            ANT_Bandwidth += x[17];
            TU_Bandwidth += x[29];
            NRE_Bandwidth += x[9];
            ORI_Bandwidth += x[13];
            PG_Bandwidth += x[21];


            TU_Latency = Math.Max(TU_Latency, x[30]);
            NRE_Latency = Math.Max(NRE_Latency, x[10]);
            ORI_Latency = Math.Max(ORI_Latency, x[14]);
            PG_Latency = Math.Max(PG_Latency, x[22]);

            TU_Memory += x[8];
            NRE_Memory += x[28];
            ORI_Memory += x[12];
            PG_Memory += x[20];

            Computation_max = RCS_Compute + ANT_Compute + COR_Compute + TU_Compute + ORI_Compute + PG_Compute + NRE_Compute;
            Memory_max = RCS_Memory + ANT_Memory + COR_Memory + TU_Memory + ORI_Memory + PG_Memory + NRE_Memory;
            Latency_max = RCS_Latency + ANT_Latency + COR_Latency + TU_Latency + ORI_Latency + PG_Latency + NRE_Latency;
            Bandwidth_max = RCS_Bandwidth + ANT_Bandwidth + COR_Bandwidth + TU_Bandwidth + ORI_Bandwidth + PG_Bandwidth + NRE_Bandwidth;
        }
        private void Get_class_result_show(int x)
        {
            int i = 0;
            double value = 0;
            Scan_pb.Value = x;
            Scan_i_tb.Text = x.ToString() + "/" + Scan_max.ToString();

            if (Presentation_choice_mode == 0)
            {
                C_RCS_pb.Value = RCS_Compute;
                C_RCS_i_tb.Text = RCS_Compute.ToString("E5") + " FP MAC";

                C_ANT_pb.Value = ANT_Compute;
                C_ANT_i_tb.Text = ANT_Compute.ToString("E5") + " FP MAC";

                C_COR_pb.Value = COR_Compute;
                C_COR_i_tb.Text = COR_Compute.ToString("E5") + " FP MAC";

                C_TU_pb.Value = TU_Compute;
                C_TU_i_tb.Text = TU_Compute.ToString("E5") + " FP MAC";

                C_PG_pb.Value = PG_Compute;
                C_PG_i_tb.Text = PG_Compute.ToString("E5") + " FP MAC";

                C_ORI_pb.Value = ORI_Compute;
                C_ORI_i_tb.Text = ORI_Compute.ToString("E5") + " FP MAC";

                C_NRE_pb.Value = NRE_Compute;
                C_NRE_i_tb.Text = NRE_Compute.ToString("E5") + " FP MAC";


                M_RCS_pb.Value = RCS_Memory;
                M_RCS_i_tb.Text = RCS_Memory.ToString("E5") + " FP-64b";

                M_ANT_pb.Value = ANT_Memory;
                M_ANT_i_tb.Text = ANT_Memory.ToString("E5") + " FP-64b";

                M_COR_pb.Value = COR_Memory;
                M_COR_i_tb.Text = COR_Memory.ToString("E5") + " FP-64b";

                M_TU_pb.Value = TU_Memory;
                M_TU_i_tb.Text = TU_Memory.ToString("E5") + " FP-64b";

                M_PG_pb.Value = PG_Memory;
                M_PG_i_tb.Text = PG_Memory.ToString("E5") + " FP-64b";

                M_ORI_pb.Value = ORI_Memory;
                M_ORI_i_tb.Text = ORI_Memory.ToString("E5") + " FP-64b";

                M_NRE_pb.Value = NRE_Memory;
                M_NRE_i_tb.Text = NRE_Memory.ToString("E5") + " FP-64b";


                L_RCS_pb.Value = RCS_Latency;
                L_RCS_i_tb.Text = RCS_Latency.ToString("E5") + " FP-64b";

                L_ANT_pb.Value = ANT_Latency;
                L_ANT_i_tb.Text = ANT_Latency.ToString("E5") + " FP-64b";

                L_COR_pb.Value = COR_Latency;
                L_COR_i_tb.Text = COR_Latency.ToString("E5") + " FP-64b";

                L_TU_pb.Value = TU_Latency;
                L_TU_i_tb.Text = TU_Latency.ToString("E5") + " FP-64b";

                L_PG_pb.Value = PG_Latency;
                L_PG_i_tb.Text = PG_Latency.ToString("E5") + " FP-64b";

                L_ORI_pb.Value = ORI_Latency;
                L_ORI_i_tb.Text = ORI_Latency.ToString("E5") + " FP-64b";

                L_NRE_pb.Value = NRE_Latency;
                L_NRE_i_tb.Text = NRE_Latency.ToString("E5") + " FP-64b";




                B_RCS_pb.Value = RCS_Bandwidth;
                B_RCS_i_tb.Text = RCS_Bandwidth.ToString("E5") + " FP-64b";

                B_ANT_pb.Value = ANT_Bandwidth;
                B_ANT_i_tb.Text = ANT_Bandwidth.ToString("E5") + " FP-64b";

                B_COR_pb.Value = COR_Bandwidth;
                B_COR_i_tb.Text = COR_Bandwidth.ToString("E5") + " FP-64b";

                B_TU_pb.Value = TU_Bandwidth;
                B_TU_i_tb.Text = TU_Bandwidth.ToString("E5") + " FP-64b";

                B_PG_pb.Value = PG_Bandwidth;
                B_PG_i_tb.Text = PG_Bandwidth.ToString("E5") + " FP-64b";

                B_ORI_pb.Value = ORI_Bandwidth;
                B_ORI_i_tb.Text = ORI_Bandwidth.ToString("E5") + " FP-64b";

                B_NRE_pb.Value = NRE_Bandwidth;
                B_NRE_i_tb.Text = NRE_Bandwidth.ToString("E5") + " FP-64b";


                C_RCS_pb.Maximum = Computation_max;
                C_ANT_pb.Maximum = Computation_max;
                C_COR_pb.Maximum = Computation_max;
                C_TU_pb.Maximum = Computation_max;
                C_ORI_pb.Maximum = Computation_max;
                C_PG_pb.Maximum = Computation_max;
                C_NRE_pb.Maximum = Computation_max;

                M_RCS_pb.Maximum = Memory_max;
                M_ANT_pb.Maximum = Memory_max;
                M_COR_pb.Maximum = Memory_max;
                M_TU_pb.Maximum = Memory_max;
                M_ORI_pb.Maximum = Memory_max;
                M_PG_pb.Maximum = Memory_max;
                M_NRE_pb.Maximum = Memory_max;

                L_RCS_pb.Maximum = Latency_max;
                L_ANT_pb.Maximum = Latency_max;
                L_COR_pb.Maximum = Latency_max;
                L_TU_pb.Maximum = Latency_max;
                L_ORI_pb.Maximum = Latency_max;
                L_PG_pb.Maximum = Latency_max;
                L_NRE_pb.Maximum = Latency_max;

                B_RCS_pb.Maximum = Bandwidth_max;
                B_ANT_pb.Maximum = Bandwidth_max;
                B_COR_pb.Maximum = Bandwidth_max;
                B_TU_pb.Maximum = Bandwidth_max;
                B_ORI_pb.Maximum = Bandwidth_max;
                B_PG_pb.Maximum = Bandwidth_max;
                B_NRE_pb.Maximum = Bandwidth_max;
            }
            else if(Presentation_choice_mode == 1)
            {
                i = 0;
                while(i<OR_pb.Count)
                {
                    value = DRBE_obj_list[i].RCS_Compute + DRBE_obj_list[i].ANT_Compute + DRBE_obj_list[i].COR_Compute + DRBE_obj_list[i].TU_Compute + DRBE_obj_list[i].ORI_Compute + DRBE_obj_list[i].PG_Compute + DRBE_obj_list[i].NRE_Compute;
                    OR_pb[i].Value = value;
                    OR_i_tb[i].Text = OR_pb[i].Value.ToString("E5") + "FP-64b";
                    OR_max = Math.Max(OR_max, value);
                    i++;
                }
                i = 0;
                while(i<OR_pb.Count)
                {
                    OR_pb[i].Maximum = OR_max;
                    i++;
                }
            }
            else if (Presentation_choice_mode == 2)
            {
                i = 0;
                OR_max = 0;
                while (i < OR_pb.Count)
                {
                    value = DRBE_obj_list[i].RCS_Memory + DRBE_obj_list[i].ANT_Memory + DRBE_obj_list[i].COR_Memory + DRBE_obj_list[i].TU_Memory + DRBE_obj_list[i].ORI_Memory + DRBE_obj_list[i].PG_Memory + DRBE_obj_list[i].NRE_Memory;
                    OR_max = Math.Max(OR_max, value);
                    OR_pb[i].Value = value;
                    OR_i_tb[i].Text = OR_pb[i].Value.ToString("E5") + "FP-64b";
                    i++;
                }
                i = 0;
                while (i < OR_pb.Count)
                {
                    OR_pb[i].Maximum = OR_max;
                    i++;
                }
            }
            else if (Presentation_choice_mode == 3)
            {
                i = 0;
                OR_max = 0;
                while (i < OR_pb.Count)
                {
                    value = DRBE_obj_list[i].RCS_Bandwidth + DRBE_obj_list[i].ANT_Bandwidth + DRBE_obj_list[i].COR_Bandwidth + DRBE_obj_list[i].TU_Bandwidth + DRBE_obj_list[i].ORI_Bandwidth + DRBE_obj_list[i].PG_Bandwidth + DRBE_obj_list[i].NRE_Bandwidth;
                    OR_max = Math.Max(OR_max, value);
                    OR_pb[i].Value = value;
                    OR_i_tb[i].Text = OR_pb[i].Value.ToString("E5") + "FP-64b";
                    i++;
                }
                i = 0;
                while (i < OR_pb.Count)
                {
                    OR_pb[i].Maximum = OR_max;
                    i++;
                }
            }
            else if (Presentation_choice_mode == 4)
            {
                i = 0;
                OR_max = 0;
                while (i < OR_pb.Count)
                {
                    //value = new double[] { DRBE_obj_list[i].RCS_Latency, DRBE_obj_list[i].ANT_Latency, DRBE_obj_list[i].COR_Latency, DRBE_obj_list[i].TU_Latency, DRBE_obj_list[i].ORI_Latency, DRBE_obj_list[i].PG_Latency, DRBE_obj_list[i].NRE_Latency }.Max();
                    value = DRBE_obj_list[i].RCS_Latency + DRBE_obj_list[i].ANT_Latency + DRBE_obj_list[i].COR_Latency + DRBE_obj_list[i].TU_Latency + DRBE_obj_list[i].ORI_Latency + DRBE_obj_list[i].PG_Latency + DRBE_obj_list[i].NRE_Latency;
                    OR_max = Math.Max(OR_max, value);
                    OR_pb[i].Value = value;
                    OR_i_tb[i].Text = OR_pb[i].Value.ToString("E5") + "FP-64b";
                    i++;
                }
                i = 0;
                while (i < OR_pb.Count)
                {
                    OR_pb[i].Maximum = OR_max;
                    i++;
                }
            }

        }
        private void Last_result_update_show()
        {
            Obj_resource_record = new List<double>();
            int i = 0;
            double value = 0;
            if (Presentation_choice_mode == 0)
            {
                C_RCS_pb.Value = RCS_Compute;
                C_RCS_i_tb.Text = RCS_Compute.ToString("E5") + " FP MAC";

                C_ANT_pb.Value = ANT_Compute;
                C_ANT_i_tb.Text = ANT_Compute.ToString("E5") + " FP MAC";

                C_COR_pb.Value = COR_Compute;
                C_COR_i_tb.Text = COR_Compute.ToString("E5") + " FP MAC";

                C_TU_pb.Value = TU_Compute;
                C_TU_i_tb.Text = TU_Compute.ToString("E5") + " FP MAC";

                C_PG_pb.Value = PG_Compute;
                C_PG_i_tb.Text = PG_Compute.ToString("E5") + " FP MAC";

                C_ORI_pb.Value = ORI_Compute;
                C_ORI_i_tb.Text = ORI_Compute.ToString("E5") + " FP MAC";

                C_NRE_pb.Value = NRE_Compute;
                C_NRE_i_tb.Text = NRE_Compute.ToString("E5") + " FP MAC";


                M_RCS_pb.Value = RCS_Memory;
                M_RCS_i_tb.Text = RCS_Memory.ToString("E5") + " FP-64b";

                M_ANT_pb.Value = ANT_Memory;
                M_ANT_i_tb.Text = ANT_Memory.ToString("E5") + " FP-64b";

                M_COR_pb.Value = COR_Memory;
                M_COR_i_tb.Text = COR_Memory.ToString("E5") + " FP-64b";

                M_TU_pb.Value = TU_Memory;
                M_TU_i_tb.Text = TU_Memory.ToString("E5") + " FP-64b";

                M_PG_pb.Value = PG_Memory;
                M_PG_i_tb.Text = PG_Memory.ToString("E5") + " FP-64b";

                M_ORI_pb.Value = ORI_Memory;
                M_ORI_i_tb.Text = ORI_Memory.ToString("E5") + " FP-64b";

                M_NRE_pb.Value = NRE_Memory;
                M_NRE_i_tb.Text = NRE_Memory.ToString("E5") + " FP-64b";


                L_RCS_pb.Value = RCS_Latency;
                L_RCS_i_tb.Text = RCS_Latency.ToString("E5") + " FP-64b";

                L_ANT_pb.Value = ANT_Latency;
                L_ANT_i_tb.Text = ANT_Latency.ToString("E5") + " FP-64b";

                L_COR_pb.Value = COR_Latency;
                L_COR_i_tb.Text = COR_Latency.ToString("E5") + " FP-64b";

                L_TU_pb.Value = TU_Latency;
                L_TU_i_tb.Text = TU_Latency.ToString("E5") + " FP-64b";

                L_PG_pb.Value = PG_Latency;
                L_PG_i_tb.Text = PG_Latency.ToString("E5") + " FP-64b";

                L_ORI_pb.Value = ORI_Latency;
                L_ORI_i_tb.Text = ORI_Latency.ToString("E5") + " FP-64b";

                L_NRE_pb.Value = NRE_Latency;
                L_NRE_i_tb.Text = NRE_Latency.ToString("E5") + " FP-64b";




                B_RCS_pb.Value = RCS_Bandwidth;
                B_RCS_i_tb.Text = RCS_Bandwidth.ToString("E5") + " FP-64b";

                B_ANT_pb.Value = ANT_Bandwidth;
                B_ANT_i_tb.Text = ANT_Bandwidth.ToString("E5") + " FP-64b";

                B_COR_pb.Value = COR_Bandwidth;
                B_COR_i_tb.Text = COR_Bandwidth.ToString("E5") + " FP-64b";

                B_TU_pb.Value = TU_Bandwidth;
                B_TU_i_tb.Text = TU_Bandwidth.ToString("E5") + " FP-64b";

                B_PG_pb.Value = PG_Bandwidth;
                B_PG_i_tb.Text = PG_Bandwidth.ToString("E5") + " FP-64b";

                B_ORI_pb.Value = ORI_Bandwidth;
                B_ORI_i_tb.Text = ORI_Bandwidth.ToString("E5") + " FP-64b";

                B_NRE_pb.Value = NRE_Bandwidth;
                B_NRE_i_tb.Text = NRE_Bandwidth.ToString("E5") + " FP-64b";


                C_RCS_pb.Maximum = Computation_max;
                C_ANT_pb.Maximum = Computation_max;
                C_COR_pb.Maximum = Computation_max;
                C_TU_pb.Maximum = Computation_max;
                C_ORI_pb.Maximum = Computation_max;
                C_PG_pb.Maximum = Computation_max;
                C_NRE_pb.Maximum = Computation_max;

                M_RCS_pb.Maximum = Memory_max;
                M_ANT_pb.Maximum = Memory_max;
                M_COR_pb.Maximum = Memory_max;
                M_TU_pb.Maximum = Memory_max;
                M_ORI_pb.Maximum = Memory_max;
                M_PG_pb.Maximum = Memory_max;
                M_NRE_pb.Maximum = Memory_max;

                L_RCS_pb.Maximum = Latency_max;
                L_ANT_pb.Maximum = Latency_max;
                L_COR_pb.Maximum = Latency_max;
                L_TU_pb.Maximum = Latency_max;
                L_ORI_pb.Maximum = Latency_max;
                L_PG_pb.Maximum = Latency_max;
                L_NRE_pb.Maximum = Latency_max;

                B_RCS_pb.Maximum = Bandwidth_max;
                B_ANT_pb.Maximum = Bandwidth_max;
                B_COR_pb.Maximum = Bandwidth_max;
                B_TU_pb.Maximum = Bandwidth_max;
                B_ORI_pb.Maximum = Bandwidth_max;
                B_PG_pb.Maximum = Bandwidth_max;
                B_NRE_pb.Maximum = Bandwidth_max;
            }
            else if (Presentation_choice_mode == 1)
            {
                i = 0;
                OR_max = 0;
                while (i < DRBE_obj_list.Count)
                {
                    value = DRBE_obj_list[i].RCS_Compute + DRBE_obj_list[i].ANT_Compute + DRBE_obj_list[i].COR_Compute + DRBE_obj_list[i].TU_Compute + DRBE_obj_list[i].ORI_Compute + DRBE_obj_list[i].PG_Compute + DRBE_obj_list[i].NRE_Compute;
                    OR_max = Math.Max(OR_max, value);
                    Obj_resource_record.Add(value);
                    i++;
                }
                var sorted = Obj_resource_record
                .Select((x, ind) => new KeyValuePair<double, int>(x, ind))
                .OrderByDescending(x => x.Key)
                .ToList();

                List<double> B = sorted.Select(x => x.Key).ToList();
                List<int> idx = sorted.Select(x => x.Value).ToList();

                i = 0;
                while (i < DRBE_obj_list.Count)
                {
                    
                    OR_pb[i].Maximum = OR_max;
                    OR_pb[i].Value = B[i];
                    OR_l_tb[i].Text = "Object ID: " + DRBE_obj_list[idx[i]].ID.ToString();
                    OR_i_tb[i].Text = B[i].ToString("E5") + "FP-64b";

                    i++;
                }
            }
            else if (Presentation_choice_mode == 2)
            {
                i = 0;
                OR_max = 0;
                while (i < DRBE_obj_list.Count)
                {
                    value = DRBE_obj_list[i].RCS_Memory + DRBE_obj_list[i].ANT_Memory + DRBE_obj_list[i].COR_Memory + DRBE_obj_list[i].TU_Memory + DRBE_obj_list[i].ORI_Memory + DRBE_obj_list[i].PG_Memory + DRBE_obj_list[i].NRE_Memory;
                    OR_max = Math.Max(OR_max, value);
                    Obj_resource_record.Add(value);
                    i++;
                }
                var sorted = Obj_resource_record
                .Select((x, ind) => new KeyValuePair<double, int>(x, ind))
                .OrderByDescending(x => x.Key)
                .ToList();

                List<double> B = sorted.Select(x => x.Key).ToList();
                List<int> idx = sorted.Select(x => x.Value).ToList();

                i = 0;
                while (i < DRBE_obj_list.Count)
                {
                    OR_pb[i].Maximum = OR_max;
                    OR_pb[i].Value = B[i];
                    OR_l_tb[i].Text = "Object ID: " + DRBE_obj_list[idx[i]].ID.ToString();
                    OR_i_tb[i].Text = B[i].ToString("E5") + "FP-64b";

                    i++;
                }
            }
            else if (Presentation_choice_mode == 3)
            {
                i = 0;
                OR_max = 0;
                while (i < DRBE_obj_list.Count)
                {
                    value = DRBE_obj_list[i].RCS_Bandwidth + DRBE_obj_list[i].ANT_Bandwidth + DRBE_obj_list[i].COR_Bandwidth + DRBE_obj_list[i].TU_Bandwidth + DRBE_obj_list[i].ORI_Bandwidth + DRBE_obj_list[i].PG_Bandwidth + DRBE_obj_list[i].NRE_Bandwidth;
                    OR_max = Math.Max(OR_max, value);
                    Obj_resource_record.Add(value);
                    i++;
                }
                var sorted = Obj_resource_record
                .Select((x, ind) => new KeyValuePair<double, int>(x, ind))
                .OrderByDescending(x => x.Key)
                .ToList();

                List<double> B = sorted.Select(x => x.Key).ToList();
                List<int> idx = sorted.Select(x => x.Value).ToList();

                i = 0;
                while (i < DRBE_obj_list.Count)
                {
                    OR_pb[i].Maximum = OR_max;
                    OR_pb[i].Value = B[i];
                    OR_l_tb[i].Text = "Object ID: " + DRBE_obj_list[idx[i]].ID.ToString();
                    OR_i_tb[i].Text = B[i].ToString("E5") + "FP-64b";

                    i++;
                }
            }
            else if (Presentation_choice_mode == 4)
            {
                i = 0;
                OR_max = 0;
                while (i < DRBE_obj_list.Count)
                {
                    //value = new double[] { DRBE_obj_list[i].RCS_Latency, DRBE_obj_list[i].ANT_Latency, DRBE_obj_list[i].COR_Latency, DRBE_obj_list[i].TU_Latency, DRBE_obj_list[i].ORI_Latency, DRBE_obj_list[i].PG_Latency, DRBE_obj_list[i].NRE_Latency }.Max(); 
                    value = DRBE_obj_list[i].RCS_Latency + DRBE_obj_list[i].ANT_Latency + DRBE_obj_list[i].COR_Latency + DRBE_obj_list[i].TU_Latency + DRBE_obj_list[i].ORI_Latency + DRBE_obj_list[i].PG_Latency + DRBE_obj_list[i].NRE_Latency;
                    OR_max = Math.Max(OR_max, value);
                    Obj_resource_record.Add(value);
                    i++;
                }
                var sorted = Obj_resource_record
                .Select((x, ind) => new KeyValuePair<double, int>(x, ind))
                .OrderByDescending(x => x.Key)
                .ToList();

                List<double> B = sorted.Select(x => x.Key).ToList();
                List<int> idx = sorted.Select(x => x.Value).ToList();

                i = 0;
                while (i < DRBE_obj_list.Count)
                {
                    OR_pb[i].Maximum = OR_max;
                    OR_pb[i].Value = B[i];
                    OR_l_tb[i].Text = "Object ID: " + DRBE_obj_list[idx[i]].ID.ToString();
                    OR_i_tb[i].Text = B[i].ToString("E5") + "FP-64b";

                    i++;
                }
            }

        }
        private async Task<List<double>> Transceive(List<double> x)
        {
            List<byte> tosend = new List<byte>();
            int i = 0;
            i = 0;
            tosend.Add(0x02);
            tosend.Add(0x00);
            tosend.Add(0x8C);
            tosend.Add(0x00);
            tosend.AddRange(BitConverter.GetBytes((double)777));
            while (i<x.Count)
            {
                tosend.AddRange(BitConverter.GetBytes(x[i]));
                i++;
            }
            //await ShowDialog("hi",tosend.Count.ToString());
            ParentPage.Data_ready_flag = false;
            ParentPage.UWbinarywriter.Write(tosend.ToArray(), 0, tosend.Count);
            ParentPage.UWbinarywriter.Flush();
            while(!ParentPage.Data_ready_flag)
            {
                await Task.Delay(1);
            }
            List<double> result = new List<double>(ParentPage.Packet_data_buffer);
            ParentPage.Packet_data_buffer.Clear();
            //await ShowDialog("hi",ParentPage.Packet_data_buffer.Count.ToString());
            return result;
        }

        private List<double> Fetch_link_info(int i, int ii, int iii)
        {
            List<double> result = new List<double>();

            result.Add((double)5 / 1000000);
            result.Add(1);
            result.Add(1);
            result.Add((double)1 / 1000);

            result.Add(Dic_t_i_obj[i].Interpolation_order);
            result.Add(Dic_t_i_obj[i].Convergence);

            result.Add(Dic_t_i_obj[i].Antenna_order);
            result.Add(Dic_t_i_obj[i].Number_Antenna_AZ * Dic_t_i_obj[i].Number_Antenna_EL);
            result.Add(Dic_t_i_obj[i].Resolution_AZ);
            result.Add(Dic_t_i_obj[i].Dictionary_dimension);

            result.Add(Dic_o_i_obj[i].RCS_order);
            result.Add(Dic_o_i_obj[i].RCS_point);
            result.Add(Dic_o_i_obj[i].RCS_angle_resolution);
            result.Add(Dic_o_i_obj[i].RCS_frequency_point);
            result.Add(Dic_o_i_obj[i].RCS_number_of_polarization);
            result.Add(Dic_o_i_obj[i].RCS_output_time_sampe);


            return result;
        }

        private List<double> Fetch_obj_info(DRBE_Objs x)
        {
            List<double> result = new List<double>();

            result.Add((double)5/1000000);
            result.Add(1);
            result.Add(1);
            result.Add((double)1 /1000);

            result.Add(x.Interpolation_order);
            result.Add(x.Convergence);

            result.Add(x.Antenna_order);
            result.Add(x.Number_Antenna_AZ * x.Number_Antenna_EL);
            result.Add(x.Resolution_AZ);
            result.Add(x.Dictionary_dimension);

            result.Add(x.RCS_order);
            result.Add(x.RCS_point);
            result.Add(x.RCS_angle_resolution);
            result.Add(x.RCS_frequency_point);
            result.Add(x.RCS_number_of_polarization);
            result.Add(x.RCS_output_time_sampe);


            return result;
        }

        private async void CMP_bt_Click(object sender, RoutedEventArgs e)
        {
            //await ShowDialog("Compilation result", "Compilation Succeed/ Failed \r\n \r\n--------------------------------------------------------\r\n \r\n  If design exceed DRBE capability: Suggestions (........) \r\n \r\n------------------------------------------\r\n \r\nIf DRBE is not fully utilized: Suggestions(.................): \r\n \r\n" + Lv_dtl[0].Summary());
        }

        private async void AMT_bt_Click(object sender, RoutedEventArgs e)
        {
            Get_class_result_refresh();
            int i = 0;
            int ii = 0;
            int iii = 0;
            int count = 0;
            i = 0;

            Global_scanned_flag = true;
            Scan_max = Get_total_scan_number(0);
            Scan_pb.Maximum = Scan_max;
            
            while (i<Link_list.Count)
            {
                ii = 0;
                while(ii<Link_list[i].Count)
                {
                    iii = 0;
                    while(iii<Link_list[i][ii].Count)
                    {
                        if(Link_list[i][ii][iii])
                        {
                            Get_link_class_result_update(await Transceive(Fetch_link_info(i,ii,iii)), i, ii, iii);

                            Dic_t_i_obj[i].Is_transmitting = true;
                            Dic_o_i_obj[ii].Is_reflecting = true;
                            Dic_r_i_obj[iii].Is_receiving = true;
                            count++;
                            if(count%10==0)
                            {
                                Get_class_result_show(count);
                            }
                        }
                        iii++;
                    }
                    ii++;
                }
                
                i++;
            }
            i = 0;
            while(i< DRBE_obj_list.Count)
            {
                if(DRBE_obj_list[i].Is_transmitting)
                {
                    Get_obj_class_result_update(await Transceive(Fetch_obj_info(DRBE_obj_list[i])), i, 0);
                }
                if (DRBE_obj_list[i].Is_reflecting)
                {
                    Get_obj_class_result_update(await Transceive(Fetch_obj_info(DRBE_obj_list[i])), i, 1);
                }
                if (DRBE_obj_list[i].Is_receiving)
                {
                    Get_obj_class_result_update(await Transceive(Fetch_obj_info(DRBE_obj_list[i])), i, 2);
                }
                count++;
                if (count % 3 == 0)
                {
                    Get_class_result_show(count);
                }
                i++;
            }
            i = 0;
            while(i<DRBE_obj_list.Count)
            {
                COR_Compute += DRBE_obj_list[i].COR_Compute;
                COR_Memory += DRBE_obj_list[i].COR_Memory;
                i++;
            }
            Get_class_result_show(count);


            //Ref_AO0_tb.Text = "Resource Requirement: \r\n Memory: 20 KB \r\n Computational: 10 FLOP \r\n IO Bandwidth:";
            //Ref_AO1_tb.Text = "Resource Requirement: \r\n Memory: 122 KB \r\n Computational: 37 FLOP \r\n IO Bandwidth:";
            //Ref_AO2_tb.Text = "Resource Requirement: \r\n Memory: 154 KB \r\n Computational: 55 FLOP \r\n IO Bandwidth:";
            //Ref_AO3_tb.Text = "Resource Requirement: \r\n Memory: 263 KB \r\n Computational: 70 FLOP \r\n IO Bandwidth:";
            //Ref_AO4_tb.Text = "Resource Requirement: \r\n Memory: 399 KB \r\n Computational: 730 FLOP \r\n IO Bandwidth:";
            //Ref_AO4_tb.Foreground = orange_brush;
            //Ref_AO5_tb.Text = "Resource Requirement: \r\n Memory: 22390 KB \r\n Computational: 120 FLOP \r\n IO Bandwidth:";
            //Ref_AO5_tb.Foreground = orange_brush;
            //Ref_AO6_tb.Text = "Resource Requirement: \r\n Memory: 00 KB \r\n Computational: 00 FLOP \r\n IO Bandwidth:";
            //Text_tb.Text += "Total Resource Utilization: \r\n \r\n Memory 8944.53 MB / 800000 MB. -----   1.12 % \r\n \r\n";
            //Text_tb.Text += "PPU:  34 units / 4500 units. -----   0.76 % \r\n \r\n";
            //Text_tb.Text += "Computation:  00 / 00. -----   0.00 % \r\n \r\n";
            //Text_tb.Text += "IO Bandwidth:  00 / 00. -----   0.00 % \r\n \r\n";
        }

        private void Ref_AO_bt_Click(object sender, RoutedEventArgs e)
        {
            Button foo = sender as Button;
            if (foo.BorderBrush == dark_grey_brush)
            {
                foo.BorderBrush = green_bright_button_brush;
            }
            else if (foo.BorderBrush == green_bright_button_brush)
            {
                foo.BorderBrush = orange_brush;
            }
            else if (foo.BorderBrush == orange_brush)
            {
                foo.BorderBrush = dark_grey_brush;
            }
        }

        private async void testchain()
        {
            hide();
            DRBE_sweep = new Sim_sweep(ParentGrid, ParentPage);
            //DRBE_sweep.Property_setup(Lv_dtl, Lv_dfl, Lv_drl, Link_enable_list);
        }

        private void Ref_Clutter_bt_Click(object sender, RoutedEventArgs e)
        {
            Ref_bt_decolor();
            Button yoo = sender as Button;
            yoo.BorderBrush = green_bright_button_brush;
            if (T_bt_flag == false && R_bt_flag == false && P_bt_flag == false)
            {

            }
        }

        private void Ref_RFim_bt_Click(object sender, RoutedEventArgs e)
        {
            Ref_bt_decolor();
            Button yoo = sender as Button;
            yoo.BorderBrush = green_bright_button_brush;
        }

        private void Ref_RCS_bt_Click(object sender, RoutedEventArgs e)
        {
            Ref_bt_decolor();
            Button yoo = sender as Button;
            yoo.BorderBrush = green_bright_button_brush;
        }

        private void Ref_Polarization_bt_Click(object sender, RoutedEventArgs e)
        {
            Ref_bt_decolor();
            Button yoo = sender as Button;
            yoo.BorderBrush = green_bright_button_brush;
        }

        private void Ref_Doppler_bt_Click(object sender, RoutedEventArgs e)
        {
            Ref_bt_decolor();
            Button yoo = sender as Button;
            yoo.BorderBrush = green_bright_button_brush;
        }

        private void Ref_Antenna_bt_Click(object sender, RoutedEventArgs e)
        {
            Ref_bt_decolor();
            Button yoo = sender as Button;
            yoo.BorderBrush = green_bright_button_brush;
        }

        private void Ref_bt_decolor()
        {
            Ref_Antenna_bt.BorderBrush = Default_back_black_color_brush;
            Ref_Doppler_bt.BorderBrush = Default_back_black_color_brush;
            Ref_Polarization_bt.BorderBrush = Default_back_black_color_brush;
            Ref_RCS_bt.BorderBrush = Default_back_black_color_brush;
            Ref_RFim_bt.BorderBrush = Default_back_black_color_brush;
            Ref_Clutter_bt.BorderBrush = Default_back_black_color_brush;

            Ref_AO0_bt.BorderBrush = white_button_brush;
            Ref_AO1_bt.BorderBrush = white_button_brush;
            Ref_AO2_bt.BorderBrush = white_button_brush;
            Ref_AO3_bt.BorderBrush = white_button_brush;
            Ref_AO4_bt.BorderBrush = white_button_brush;
            Ref_AO5_bt.BorderBrush = white_button_brush;
            Ref_AO6_bt.BorderBrush = white_button_brush;
        }
        private async void Link_de_bt_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            int ii = 0;
            int iii = 0;

            if (T_bt_flag == false && R_bt_flag == false && P_bt_flag == false)
            {
                i = 0;
                while (i < Link_list.Count)
                {
                    ii = 0;
                    while (ii < Link_list[i].Count)
                    {
                        iii = 0;
                        while (iii < Link_list[i][ii].Count)
                        {
                            Link_list[i][ii][iii] = false;
                            iii++;
                        }
                        ii++;
                    }
                    i++;
                }

                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }

            }
            else if (T_bt_flag == false && R_bt_flag == false && P_bt_flag == true)
            {
                i = 0;
                while (i < Link_list.Count)
                {
                    ii = BTP_Gindex;
                    iii = 0;
                    while (iii < Link_list[i][ii].Count)
                    {
                        Link_list[i][ii][iii] = false;
                        iii++;
                    }
                    i++;
                }

                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }
                DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
            }
            else if (T_bt_flag == false && R_bt_flag == true && P_bt_flag == false)
            {
                i = 0;
                while (i < Link_list.Count)
                {
                    ii = 0;
                    while (ii < Link_list[i].Count)
                    {
                        iii = BTR_Gindex;
                        Link_list[i][ii][iii] = false;
                        ii++;
                    }
                    i++;
                }

                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }
                DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
            }
            else if (T_bt_flag == true && R_bt_flag == false && P_bt_flag == false)
            {
                i = BTT_Gindex;
                ii = 0;
                while (ii < Link_list[i].Count)
                {
                    iii = 0;
                    while (iii < Link_list[i][ii].Count)
                    {
                        Link_list[i][ii][iii] = false;
                        iii++;
                    }
                    ii++;
                }
                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }
                DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;


            }
            else if (T_bt_flag == true && R_bt_flag == true && P_bt_flag == false)
            {
                i = BTT_Gindex;
                ii = 0;
                iii = BTR_Gindex;
                while (ii < Link_list[i].Count)
                {
                    Link_list[i][ii][iii] = false;
                    ii++;
                }

                DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }
                DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
            }
            else if (T_bt_flag == true && R_bt_flag == false && P_bt_flag == true)
            {
                i = BTT_Gindex;
                ii = BTP_Gindex;
                iii = 0;
                while (iii < Link_list[i][ii].Count)
                {
                    Link_list[i][ii][iii] = false;
                    iii++;
                }

                DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }
                DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
            }
            else if (T_bt_flag == false && R_bt_flag == true && P_bt_flag == true)
            {
                i = 0;
                ii = BTP_Gindex;
                iii = BTR_Gindex;
                while (i < Link_list.Count)
                {
                    Link_list[i][ii][iii] = false;
                    i++;
                }


                DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = red_bright_button_brush;
                    i++;
                }
                DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;

            }
            else if (T_bt_flag == true && R_bt_flag == true && P_bt_flag == true)
            {
                i = BTT_Gindex;
                ii = BTP_Gindex;
                iii = BTR_Gindex;
                Link_list[i][ii][iii] = false;
                DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
            }
            else
            {
                await ShowDialog("Error", "En Error");
            }
        }

        private async void Link_en_bt_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            int ii = 0;
            int iii = 0;

            if (T_bt_flag == false && R_bt_flag == false && P_bt_flag == false)
            {
                i = 0;
                while (i < Link_list.Count)
                {
                    ii = 0;
                    while (ii < Link_list[i].Count)
                    {
                        iii = 0;
                        while (iii < Link_list[i][ii].Count)
                        {
                            Link_list[i][ii][iii] = true;
                            iii++;
                        }
                        ii++;
                    }
                    i++;
                }
                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
            }
            else if (T_bt_flag == false && R_bt_flag == false && P_bt_flag == true)
            {
                i = 0;
                while (i < Link_list.Count)
                {
                    ii = BTP_Gindex;
                    iii = 0;
                    while (iii < Link_list[i][ii].Count)
                    {
                        Link_list[i][ii][iii] = true;
                        iii++;
                    }
                    i++;
                }
                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DF_tb[BTP_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == false && R_bt_flag == true && P_bt_flag == false)
            {
                i = 0;
                while (i < Link_list.Count)
                {
                    ii = 0;
                    while (ii < Link_list[i].Count)
                    {
                        iii = BTR_Gindex;
                        Link_list[i][ii][iii] = true;
                        ii++;
                    }
                    i++;
                }
                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DR_tb[BTR_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == true && R_bt_flag == false && P_bt_flag == false)
            {
                i = BTT_Gindex;
                ii = 0;
                while (ii < Link_list[i].Count)
                {
                    iii = 0;
                    while (iii < Link_list[i][ii].Count)
                    {
                        Link_list[i][ii][iii] = true;
                        iii++;
                    }
                    ii++;
                }
                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DT_tb[BTT_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == true && R_bt_flag == true && P_bt_flag == false)
            {
                i = BTT_Gindex;
                ii = 0;
                iii = BTR_Gindex;
                while (ii < Link_list[i].Count)
                {
                    Link_list[i][ii][iii] = true;
                    ii++;
                }
                DR_tb[BTR_Gindex].Foreground = green_bright_button_brush;
                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DT_tb[BTT_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == true && R_bt_flag == false && P_bt_flag == true)
            {
                i = BTT_Gindex;
                ii = BTP_Gindex;
                iii = 0;
                while (iii < Link_list[i][ii].Count)
                {
                    Link_list[i][ii][iii] = true;
                    iii++;
                }
                DF_tb[BTP_Gindex].Foreground = green_bright_button_brush;

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DT_tb[BTT_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == false && R_bt_flag == true && P_bt_flag == true)
            {
                i = 0;
                ii = BTP_Gindex;
                iii = BTR_Gindex;
                while (i < Link_list.Count)
                {
                    Link_list[i][ii][iii] = true;
                    i++;
                }
                DR_tb[BTR_Gindex].Foreground = green_bright_button_brush;
                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DF_tb[BTP_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == true && R_bt_flag == true && P_bt_flag == true)
            {
                i = BTT_Gindex;
                ii = BTP_Gindex;
                iii = BTR_Gindex;
                Link_list[i][ii][iii] = true;
                DR_tb[BTR_Gindex].Foreground = green_bright_button_brush;
                DF_tb[BTP_Gindex].Foreground = green_bright_button_brush;
                DT_tb[BTT_Gindex].Foreground = green_bright_button_brush;
            }
            else
            {
                await ShowDialog("Error", "DP Error");
            }
        }

        private async void Link_uns_bt_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            int ii = 0;
            int iii = 0;

            if (T_bt_flag == false && R_bt_flag == false && P_bt_flag == false)
            {
                i = 0;
                while (i < Link_list.Count)
                {
                    ii = 0;
                    while (ii < Link_list[i].Count)
                    {
                        iii = 0;
                        while (iii < Link_list[i][ii].Count)
                        {
                            Link_list[i][ii][iii] = true;
                            iii++;
                        }
                        ii++;
                    }
                    i++;
                }
                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
            }
            else if (T_bt_flag == false && R_bt_flag == false && P_bt_flag == true)
            {
                i = 0;
                while (i < Link_list.Count)
                {
                    ii = BTP_Gindex;
                    iii = 0;
                    while (iii < Link_list[i][ii].Count)
                    {
                        Link_list[i][ii][iii] = true;
                        iii++;
                    }
                    i++;
                }
                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DF_tb[BTP_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == false && R_bt_flag == true && P_bt_flag == false)
            {
                i = 0;
                while (i < Link_list.Count)
                {
                    ii = 0;
                    while (ii < Link_list[i].Count)
                    {
                        iii = BTR_Gindex;
                        Link_list[i][ii][iii] = true;
                        ii++;
                    }
                    i++;
                }
                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DR_tb[BTR_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == true && R_bt_flag == false && P_bt_flag == false)
            {
                i = BTT_Gindex;
                ii = 0;
                while (ii < Link_list[i].Count)
                {
                    iii = 0;
                    while (iii < Link_list[i][ii].Count)
                    {
                        Link_list[i][ii][iii] = true;
                        iii++;
                    }
                    ii++;
                }
                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DT_tb[BTT_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == true && R_bt_flag == true && P_bt_flag == false)
            {
                i = BTT_Gindex;
                ii = 0;
                iii = BTR_Gindex;
                while (ii < Link_list[i].Count)
                {
                    Link_list[i][ii][iii] = true;
                    ii++;
                }
                DR_tb[BTR_Gindex].Foreground = green_bright_button_brush;
                i = 0;
                while (i < DF_tb.Count)
                {
                    DF_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DT_tb[BTT_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == true && R_bt_flag == false && P_bt_flag == true)
            {
                i = BTT_Gindex;
                ii = BTP_Gindex;
                iii = 0;
                while (iii < Link_list[i][ii].Count)
                {
                    Link_list[i][ii][iii] = true;
                    iii++;
                }
                DF_tb[BTP_Gindex].Foreground = green_bright_button_brush;

                i = 0;
                while (i < DR_tb.Count)
                {
                    DR_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DT_tb[BTT_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == false && R_bt_flag == true && P_bt_flag == true)
            {
                i = 0;
                ii = BTP_Gindex;
                iii = BTR_Gindex;
                while (i < Link_list.Count)
                {
                    Link_list[i][ii][iii] = true;
                    i++;
                }
                DR_tb[BTR_Gindex].Foreground = green_bright_button_brush;
                i = 0;
                while (i < DT_tb.Count)
                {
                    DT_tb[i].Foreground = green_bright_button_brush;
                    i++;
                }
                DF_tb[BTP_Gindex].Foreground = green_bright_button_brush;
            }
            else if (T_bt_flag == true && R_bt_flag == true && P_bt_flag == true)
            {
                i = BTT_Gindex;
                ii = BTP_Gindex;
                iii = BTR_Gindex;
                Link_list[i][ii][iii] = true;
                DR_tb[BTR_Gindex].Foreground = green_bright_button_brush;
                DF_tb[BTP_Gindex].Foreground = green_bright_button_brush;
                DT_tb[BTT_Gindex].Foreground = green_bright_button_brush;
            }
            else
            {
                await ShowDialog("Error", "DP Error");
            }
        }

        private bool T_bt_flag = false;
        private bool P_bt_flag = false;
        private bool R_bt_flag = false;




        //public List<List<List<bool>>> Link_enable_list = new List<List<List<bool>>>();

        public List<List<List<ushort>>> Link_Antenna_list = new List<List<List<ushort>>>();
        public List<List<List<ushort>>> Link_RCS_list = new List<List<List<ushort>>>();
        public List<List<List<ushort>>> Link_RFimp_list = new List<List<List<ushort>>>();
        public List<List<List<ushort>>> Link_Polar_list = new List<List<List<ushort>>>();
        public List<List<List<ushort>>> Link_Clutter_list = new List<List<List<ushort>>>();
        public List<List<List<ushort>>> Link_Doppler_list = new List<List<List<ushort>>>();



        //List T  P  R;


        private void Lbt_decolor()
        {
            int i = 0;
            i = 0;
            while (i < DR_bt.Count)
            {
                DR_bt[i].BorderBrush = dark_grey_brush;
                i++;
            }

            i = 0;
            while (i < DT_bt.Count)
            {
                DT_bt[i].BorderBrush = dark_grey_brush;
                i++;
            }

            i = 0;
            while (i < DF_bt.Count)
            {
                DF_bt[i].BorderBrush = dark_grey_brush;
                i++;
            }
        }
        private int BTT_Gindex = 0;
        private int BTP_Gindex = 0;
        private int BTR_Gindex = 0;

        private int Gindex = 0;
        private async void DF_bt_Click(object sender, RoutedEventArgs e)
        {
            Button yoo = sender as Button;
            int i = 0;
            int ii = 0;
            int iii = 0;
            bool difflag = false;
            bool selfflag = false;
            bool All_de_flag = true;
            if (Gindex == BTP_Gindex)
            {
                difflag = false;
            }
            else
            {
                difflag = true;
            }
            BTP_Gindex = Dic_pbt_i[yoo];
            Gindex = BTP_Gindex;
            if (yoo.BorderBrush == dark_grey_brush)
            {
                if (T_bt_flag == false && R_bt_flag == false) //T W, R W, F W, change F G
                {
                    #region bt decolor
                    i = 0;
                    while (i < DF_bt.Count)
                    {
                        DF_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = 0;
                    while (i < Link_list.Count)
                    {
                        ii = BTP_Gindex;
                        iii = 0;
                        while (iii < Link_list[i][ii].Count)
                        {
                            if (Link_list[i][ii][iii])
                            {
                                DR_tb[iii].Foreground = green_bright_button_brush;
                                DF_tb[ii].Foreground = green_bright_button_brush;
                                DT_tb[i].Foreground = green_bright_button_brush;
                            }
                            iii++;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Object";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Object";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == true && R_bt_flag == false) //T G, R W, F W, change F G
                {
                    #region bt decolor
                    i = 0;
                    while (i < DF_bt.Count)
                    {
                        DF_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = BTP_Gindex;
                    iii = 0;
                    while (iii < Link_list[i][ii].Count)
                    {
                        if (Link_list[i][ii][iii])
                        {
                            DR_tb[iii].Foreground = green_bright_button_brush;
                            DF_tb[ii].Foreground = green_bright_button_brush;
                            DT_tb[i].Foreground = green_bright_button_brush;
                        }
                        iii++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Path";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Path";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Path";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == false && R_bt_flag == true) //T G, R W, F G, change F G
                {

                    #region bt decolor
                    i = 0;
                    while (i < DF_bt.Count)
                    {
                        DF_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = 0;
                    ii = BTP_Gindex;
                    iii = BTR_Gindex;
                    while (i < Link_list.Count)
                    {
                        if (Link_list[i][ii][iii])
                        {
                            DR_tb[iii].Foreground = green_bright_button_brush;
                            DF_tb[ii].Foreground = green_bright_button_brush;
                            DT_tb[i].Foreground = green_bright_button_brush;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Path";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Path";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Path";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == true && R_bt_flag == true) //T G, R W, F W, change F G
                {
                    #region bt decolor
                    i = 0;
                    while (i < DF_bt.Count)
                    {
                        DF_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = BTP_Gindex;
                    iii = BTR_Gindex;
                    if (Link_list[i][ii][iii])
                    {
                        DR_tb[iii].Foreground = green_bright_button_brush;
                        DF_tb[ii].Foreground = green_bright_button_brush;
                        DT_tb[i].Foreground = green_bright_button_brush;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Link";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Link_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Link";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Link_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Link";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Link_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    await ShowDialog("Error", "DP Error");
                }

                yoo.BorderBrush = green_bright_button_brush;
                P_bt_flag = true;
            }
            else
            {
                if (T_bt_flag == false && R_bt_flag == false) //T W, R W, F W, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    #region read enable
                    i = 0;
                    while (i < Link_list.Count)
                    {
                        ii = 0;
                        while (ii < Link_list[i].Count)
                        {
                            iii = 0;
                            while (iii < Link_list[i][ii].Count)
                            {
                                if (Link_list[i][ii][iii])
                                {
                                    DR_tb[iii].Foreground = green_bright_button_brush;
                                    DF_tb[ii].Foreground = green_bright_button_brush;
                                    DT_tb[i].Foreground = green_bright_button_brush;
                                }
                                iii++;
                            }
                            ii++;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Scenario";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/All_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Scenario";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/En_all_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Scenario";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Uns_all_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == true && R_bt_flag == false) //T G, R W, F W, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = 0;
                    while (ii < Link_list[i].Count)
                    {
                        iii = 0;
                        while (iii < Link_list[i][ii].Count)
                        {
                            if (Link_list[i][ii][iii])
                            {
                                DR_tb[iii].Foreground = green_bright_button_brush;
                                DF_tb[ii].Foreground = green_bright_button_brush;
                                DT_tb[i].Foreground = green_bright_button_brush;
                            }
                            iii++;
                        }
                        ii++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Object";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Object";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == false && R_bt_flag == true) //T G, R W, F G, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = 0;
                    while (i < Link_list.Count)
                    {
                        iii = BTR_Gindex;
                        ii = 0;
                        while (ii < Link_list[i].Count)
                        {
                            if (Link_list[i][ii][iii])
                            {
                                DR_tb[iii].Foreground = green_bright_button_brush;
                                DF_tb[ii].Foreground = green_bright_button_brush;
                                DT_tb[i].Foreground = green_bright_button_brush;
                            }
                            ii++;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Object";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Object";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == true && R_bt_flag == true) //T G, R W, F W, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = 0;
                    iii = BTR_Gindex;
                    while (ii < Link_list[i].Count)
                    {
                        if (Link_list[i][ii][iii])
                        {
                            DR_tb[iii].Foreground = green_bright_button_brush;
                            DF_tb[ii].Foreground = green_bright_button_brush;
                            DT_tb[i].Foreground = green_bright_button_brush;
                        }
                        ii++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Path";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Path";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Path";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    await ShowDialog("Error", "DP Error");
                }
                yoo.BorderBrush = dark_grey_brush;
                P_bt_flag = false;
            }
        }
        private async void DR_bt_Click(object sender, RoutedEventArgs e)
        {
            Button yoo = sender as Button;
            int i = 0;
            int ii = 0;
            int iii = 0;
            bool difflag = false;
            bool selfflag = false;

            BTR_Gindex = Dic_rbt_i[yoo];
            if (yoo.BorderBrush == dark_grey_brush)
            {
                if (P_bt_flag == false && T_bt_flag == false) //T W, R W, F W, change F G
                {
                    #region bt decolor
                    i = 0;
                    while (i < DR_bt.Count)
                    {
                        DR_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = 0;
                    while (i < Link_list.Count)
                    {
                        iii = BTR_Gindex;
                        ii = 0;
                        while (ii < Link_list[i].Count)
                        {
                            if (Link_list[i][ii][iii])
                            {
                                DR_tb[iii].Foreground = green_bright_button_brush;
                                DF_tb[ii].Foreground = green_bright_button_brush;
                                DT_tb[i].Foreground = green_bright_button_brush;
                            }
                            ii++;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Object";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Object";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == true && P_bt_flag == false) //T G, R W, F W, change F G
                {
                    #region bt decolor
                    i = 0;
                    while (i < DR_bt.Count)
                    {
                        DR_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = 0;
                    iii = BTR_Gindex;
                    while (ii < Link_list[i].Count)
                    {
                        if (Link_list[i][ii][iii])
                        {
                            DR_tb[iii].Foreground = green_bright_button_brush;
                            DF_tb[ii].Foreground = green_bright_button_brush;
                            DT_tb[i].Foreground = green_bright_button_brush;
                        }
                        ii++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Path";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Path";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Path";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == false && P_bt_flag == true) //T G, R W, F G, change F G
                {

                    #region bt decolor
                    i = 0;
                    while (i < DR_bt.Count)
                    {
                        DR_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = 0;
                    ii = BTP_Gindex;
                    iii = BTR_Gindex;
                    while (i < Link_list.Count)
                    {
                        if (Link_list[i][ii][iii])
                        {
                            DR_tb[iii].Foreground = green_bright_button_brush;
                            DF_tb[ii].Foreground = green_bright_button_brush;
                            DT_tb[i].Foreground = green_bright_button_brush;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Path";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Path";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Path";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == true && P_bt_flag == true) //T G, R W, F W, change F G
                {
                    #region bt decolor
                    i = 0;
                    while (i < DR_bt.Count)
                    {
                        DR_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = BTP_Gindex;
                    iii = BTR_Gindex;
                    if (Link_list[i][ii][iii])
                    {
                        DR_tb[iii].Foreground = green_bright_button_brush;
                        DF_tb[ii].Foreground = green_bright_button_brush;
                        DT_tb[i].Foreground = green_bright_button_brush;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Link";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Link_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Link";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Link_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Link";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Link_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    await ShowDialog("Error", "DP Error");
                }

                yoo.BorderBrush = green_bright_button_brush;
                R_bt_flag = true;
            }
            else
            {
                if (T_bt_flag == false && P_bt_flag == false) //T W, R W, F W, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    #region read enable
                    i = 0;
                    while (i < Link_list.Count)
                    {
                        ii = 0;
                        while (ii < Link_list[i].Count)
                        {
                            iii = 0;
                            while (iii < Link_list[i][ii].Count)
                            {
                                if (Link_list[i][ii][iii])
                                {
                                    DR_tb[iii].Foreground = green_bright_button_brush;
                                    DF_tb[ii].Foreground = green_bright_button_brush;
                                    DT_tb[i].Foreground = green_bright_button_brush;
                                }
                                iii++;
                            }
                            ii++;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Scenario";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/All_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Scenario";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/En_all_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Scenario";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Uns_all_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == true && P_bt_flag == false) //T G, R W, F W, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = 0;
                    while (ii < Link_list[i].Count)
                    {
                        iii = 0;
                        while (iii < Link_list[i][ii].Count)
                        {
                            if (Link_list[i][ii][iii])
                            {
                                DR_tb[iii].Foreground = green_bright_button_brush;
                                DF_tb[ii].Foreground = green_bright_button_brush;
                                DT_tb[i].Foreground = green_bright_button_brush;
                            }
                            iii++;
                        }
                        ii++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Object";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Object";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == false && P_bt_flag == true) //T G, R W, F G, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = 0;
                    while (i < Link_list.Count)
                    {
                        ii = BTP_Gindex;
                        iii = 0;
                        while (iii < Link_list[i][ii].Count)
                        {
                            if (Link_list[i][ii][iii])
                            {
                                DR_tb[iii].Foreground = green_bright_button_brush;
                                DF_tb[ii].Foreground = green_bright_button_brush;
                                DT_tb[i].Foreground = green_bright_button_brush;
                            }
                            iii++;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Object";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Object";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (T_bt_flag == true && P_bt_flag == true) //T G, R W, F W, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = BTP_Gindex;
                    iii = 0;
                    while (iii < Link_list[i][ii].Count)
                    {
                        if (Link_list[i][ii][iii])
                        {
                            DR_tb[iii].Foreground = green_bright_button_brush;
                            DF_tb[ii].Foreground = green_bright_button_brush;
                            DT_tb[i].Foreground = green_bright_button_brush;
                        }
                        iii++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Path";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Path";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Path";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    await ShowDialog("Error", "DP Error");
                }
                yoo.BorderBrush = dark_grey_brush;
                R_bt_flag = false;
            }
        }

        private async void DT_bt_Click(object sender, RoutedEventArgs e)
        {
            Button yoo = sender as Button;
            int i = 0;
            int ii = 0;
            int iii = 0;
            bool difflag = false;
            bool selfflag = false;

            BTT_Gindex = Dic_tbt_i[yoo];
            if (yoo.BorderBrush == dark_grey_brush)
            {
                if (P_bt_flag == false && R_bt_flag == false) //T W, R W, F W, change F G
                {
                    #region bt decolor
                    i = 0;
                    while (i < DT_bt.Count)
                    {
                        DT_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = 0;
                    while (ii < Link_list[i].Count)
                    {
                        iii = 0;
                        while (iii < Link_list[i][ii].Count)
                        {
                            if (Link_list[i][ii][iii])
                            {
                                DR_tb[iii].Foreground = green_bright_button_brush;
                                DF_tb[ii].Foreground = green_bright_button_brush;
                                DT_tb[i].Foreground = green_bright_button_brush;
                            }
                            iii++;
                        }
                        ii++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Object";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Object";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (R_bt_flag == true && P_bt_flag == false) //T G, R W, F W, change F G
                {
                    #region bt decolor
                    i = 0;
                    while (i < DT_bt.Count)
                    {
                        DT_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = 0;
                    iii = BTR_Gindex;
                    while (ii < Link_list[i].Count)
                    {
                        if (Link_list[i][ii][iii])
                        {
                            DR_tb[iii].Foreground = green_bright_button_brush;
                            DF_tb[ii].Foreground = green_bright_button_brush;
                            DT_tb[i].Foreground = green_bright_button_brush;
                        }
                        ii++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Path";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Path";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object Path";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (R_bt_flag == false && P_bt_flag == true) //T G, R W, F G, change F G
                {

                    #region bt decolor
                    i = 0;
                    while (i < DT_bt.Count)
                    {
                        DT_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = BTP_Gindex;
                    iii = 0;
                    while (iii < Link_list[i][ii].Count)
                    {
                        if (Link_list[i][ii][iii])
                        {
                            DR_tb[iii].Foreground = green_bright_button_brush;
                            DF_tb[ii].Foreground = green_bright_button_brush;
                            DT_tb[i].Foreground = green_bright_button_brush;
                        }
                        iii++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Path";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Path";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object Path";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (P_bt_flag == true && R_bt_flag == true) //T G, R W, F W, change F G
                {
                    #region bt decolor
                    i = 0;
                    while (i < DT_bt.Count)
                    {
                        DT_bt[i].BorderBrush = dark_grey_brush;
                        i++;
                    }
                    #endregion
                    #region tb decolor
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    DT_tb[BTT_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = BTT_Gindex;
                    ii = BTP_Gindex;
                    iii = BTR_Gindex;
                    if (Link_list[i][ii][iii])
                    {
                        DR_tb[iii].Foreground = green_bright_button_brush;
                        DF_tb[ii].Foreground = green_bright_button_brush;
                        DT_tb[i].Foreground = green_bright_button_brush;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Link";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Link_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Link";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Link_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Link";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Link_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    await ShowDialog("Error", "DP Error");
                }

                yoo.BorderBrush = green_bright_button_brush;
                T_bt_flag = true;
            }
            else
            {
                if (P_bt_flag == false && R_bt_flag == false) //T W, R W, F W, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    #region read enable
                    i = 0;
                    while (i < Link_list.Count)
                    {
                        ii = 0;
                        while (ii < Link_list[i].Count)
                        {
                            iii = 0;
                            while (iii < Link_list[i][ii].Count)
                            {
                                if (Link_list[i][ii][iii])
                                {
                                    DR_tb[iii].Foreground = green_bright_button_brush;
                                    DF_tb[ii].Foreground = green_bright_button_brush;
                                    DT_tb[i].Foreground = green_bright_button_brush;
                                }
                                iii++;
                            }
                            ii++;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Scenario";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/All_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Scenario";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/En_all_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Scenario";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Uns_all_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (P_bt_flag == true && R_bt_flag == false) //T G, R W, F W, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = 0;
                    while (i < Link_list.Count)
                    {
                        ii = BTP_Gindex;
                        iii = 0;
                        while (iii < Link_list[i][ii].Count)
                        {
                            if (Link_list[i][ii][iii])
                            {
                                DR_tb[iii].Foreground = green_bright_button_brush;
                                DF_tb[ii].Foreground = green_bright_button_brush;
                                DT_tb[i].Foreground = green_bright_button_brush;
                            }
                            iii++;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Object";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Object";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (P_bt_flag == false && R_bt_flag == true) //T G, R W, F G, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    #endregion
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = 0;
                    while (i < Link_list.Count)
                    {
                        iii = BTR_Gindex;
                        ii = 0;
                        while (ii < Link_list[i].Count)
                        {
                            if (Link_list[i][ii][iii])
                            {
                                DR_tb[iii].Foreground = green_bright_button_brush;
                                DF_tb[ii].Foreground = green_bright_button_brush;
                                DT_tb[i].Foreground = green_bright_button_brush;
                            }
                            ii++;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Object";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Object";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Node_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else if (P_bt_flag == true && R_bt_flag == true) //T G, R W, F W, change F G
                {
                    #region tb decolor
                    i = 0;
                    while (i < DR_tb.Count)
                    {
                        DR_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DF_tb.Count)
                    {
                        DF_tb[i].Foreground = white_button_brush;
                        i++;
                    }
                    i = 0;
                    while (i < DT_tb.Count)
                    {
                        DT_tb[i].Foreground = red_bright_button_brush;
                        i++;
                    }
                    #endregion
                    DF_tb[BTP_Gindex].Foreground = red_bright_button_brush;
                    DR_tb[BTR_Gindex].Foreground = red_bright_button_brush;
                    #region read enable
                    i = 0;
                    ii = BTP_Gindex;
                    iii = BTR_Gindex;
                    while (i < Link_list.Count)
                    {
                        if (Link_list[i][ii][iii])
                        {
                            DR_tb[iii].Foreground = green_bright_button_brush;
                            DF_tb[ii].Foreground = green_bright_button_brush;
                            DT_tb[i].Foreground = green_bright_button_brush;
                        }
                        i++;
                    }
                    #endregion
                    Link_de_tb.Text = "Disable Path";
                    Link_de_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_de_icon.png", UriKind.RelativeOrAbsolute));
                    Link_en_tb.Text = "Enable Path";
                    Link_en_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_en_icon.png", UriKind.RelativeOrAbsolute));
                    Link_uns_tb.Text = "Less Priority Object Path";
                    Link_uns_bt_i.Source = new BitmapImage(new Uri("ms-appx://DRBE/Assets/Path_uns_icon.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    await ShowDialog("Error", "DP Error");
                }
                yoo.BorderBrush = dark_grey_brush;
                T_bt_flag = false;
            }
        }







        private async void Sweep_bt_Click(object sender, RoutedEventArgs e)
        {
            hide();
            DRBE_sweep = new Sim_sweep(ParentGrid, ParentPage);
            //DRBE_sweep.Property_setup(Lv_dtl, Lv_dfl, Lv_drl, Link_enable_list);
        }

        private async void Obj_save_bt_Click(object sender, RoutedEventArgs e)
        {
            await DRBE_LV_SS.Start("Save Object: Transmitter", new List<string>() { "Simulator File", "Link File" }, "dlv", Write_LV_file());
        }

        private async void Obj_summary_bt_Click(object sender, RoutedEventArgs e)
        {
            await ShowDialog("Model File Report", Write_LV_file());
        }

        private string Write_LV_file()
        {
            string result = "";
            int i = 0;
            int ii = 0;
            int iii = 0;
            i = 0;
            while (i < Link_list.Count)
            {
                ii = 0;
                while (ii < Link_list[i].Count)
                {
                    iii = 0;
                    while (iii < Link_list[i][ii].Count)
                    {
                        if (Link_list[i][ii][iii])
                        {
                            result += "Transmitter ID: {" + Dic_t_i_obj[i].ID.ToString() + "} , Platform ID: {" + Dic_o_i_obj[ii].ID.ToString() + "} , Receiver ID: {" + Dic_r_i_obj[iii].ID.ToString() + "} , ";
                            result += "Transmitter Antenna Pattern Fidelity: {" + Link_Antenna_list[i][ii][iii].ToString() + "} , Receiver Antenna Pattern Fidelity: {" + Link_Antenna_list[i][ii][iii].ToString() + "} , ";
                            result += "Transmitter RF Impairment Fidelity: {" + Link_RFimp_list[i][ii][iii].ToString() + "} , Receiver RF Impairment Fidelity: {" + Link_RFimp_list[i][ii][iii].ToString() + "} , ";
                            result += "Clutter Fidelity: {" + Link_Clutter_list[i][ii][iii].ToString() + "} , ";
                            result += "Doppler Fidelity: {" + Link_Doppler_list[i][ii][iii].ToString() + "} , ";
                            result += "Polarization Fidelity: {" + Link_Polar_list[i][ii][iii].ToString() + "} , ";
                            result += "RCS Fidelity: {" + Link_RCS_list[i][ii][iii].ToString() + "} \r\n";
                            result += "\r\n ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\r\n";
                        }
                        iii++;
                    }
                    ii++;
                }
                i++;
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
