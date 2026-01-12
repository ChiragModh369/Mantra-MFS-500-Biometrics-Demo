using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MorFin_Auth;
using System.Threading;
using System.IO;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace MorFin_Auth_CSharp
{
    public partial class frmSDK : Form
    {
        #region Variable Declaration
        string GblProductName = "";

        MorFinAuth morFinAuth = null;
        int quality = 60;
        int timeout = 10000;
        FINGER_DEVICE_INFO ObjDeviceInfo;
        Int16 IsMatching = 0;
        byte[] bytesTemplateOld = null;
        int int_compressionRatio = 5;

        #endregion

        #region Form Event
        public frmSDK()
        {
            InitializeComponent();
        }
        private void form1_Load(object sender, EventArgs e)
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;

                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                string version = fvi.FileVersion;
                lblHeader.Text = "MORFIN AUTH : Mantra Softech India Pvt Ltd © 2023";
                ClearDeviceInfo();
                lblStatus.Text = "";
                lblQltyAndNfiq.Text = "Quality: 0, NFIQ: 0";
                cmbTemplateType.SelectedIndex = 0;
                cmbImageType.SelectedIndex = 0;
                GblProductName = "";
                morFinAuth = new MorFinAuth();
                morFinAuth.OnDeviceDetection += OnDeviceDetection;
                morFinAuth.OnPreview += OnPreview;
                morFinAuth.OnCaptureCompleted += OnCaptureCompleted;
                morFinAuth.OnFingerPosition += OnFingerPosition;

                btnVersion_Click(null, null);
                btn_SupportDvc_Click(null, null);
                morFinAuth.EnableLogs(MorFin_Auth_LOG_LEVEL.MorFin_Auth_LOG_LEVEL_OFF, null);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex.Message, true);
            }
        }
        private void form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                morFinAuth.Uninit();
                Application.Exit();
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex.Message, true);
            }
        }
        #endregion

        #region Device Event
        void OnDeviceDetection(string DeviceName, DEVICE_DETECTION_EVENT dvcStatus)
        {
            string EventName = "";

            if (dvcStatus == DEVICE_DETECTION_EVENT.EVENT_CONNECTED)
            {
                EventName = "ATTACHED";
            }
            if (dvcStatus == DEVICE_DETECTION_EVENT.EVENT_DISCONNECTED)
            {
                EventName = "DETACHED";
            }

            if (EventName == "DETACHED")
            {
                try
                {
                    ClearDeviceInfo();
                    resetControl();
                    txtQuality.Text = "60";
                    txtTimeout.Text = "10000";
                    txt_Client_Key.Text = null;
                    pbIsDeviceConnected.Image = MorFin_Auth_CSharp.Properties.Resources.RedFinger;
                }
                catch
                {
                }
            }
            AutoConnect();
            InfoMessage(DeviceName + " " + EventName, false);
        }

        #endregion

        #region Button Event
        private void btnVersion_Click(object sender, EventArgs e)
        {
            try
            {
                int ret = -1;
                string ver = "";
                ret = morFinAuth.GetSDKVersion(out ver);
                if (ret == 0)
                {
                    InfoMessage("Public SDK Version : " + ver, false);
                    lblSdkVersion.Text = ver;
                }
                else
                {
                    InfoMessage(morFinAuth.GetErrDescription(ret), true);
                    lblSdkVersion.Text = "";
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex.ToString(), true);
            }
        }
        private void btnCheckDevice_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckProductName())
                {
                    InfoMessage("Device not connected", true);
                    return;
                }
                int ret = -1;
                ret = morFinAuth.IsConnected(GblProductName);
                if (ret == 0)
                {
                    InfoMessage("Device Connected", false);
                    try
                    {
                        pbIsDeviceConnected.Image = MorFin_Auth_CSharp.Properties.Resources.GreenFinger;
                    }
                    catch
                    {
                    }
                }
                else
                {
                    InfoMessage(morFinAuth.GetErrDescription(ret), true);
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex.ToString(), true);
            }
        }

        string clientkey = null;
        private void btnInit_Click(object sender, EventArgs e)
        {
            try
            {
                resetControl();
                if (!CheckProductName())
                {
                    InfoMessage("Device not connected", true);
                    return;
                }
                ObjDeviceInfo = new FINGER_DEVICE_INFO();

                if (string.IsNullOrEmpty(txt_Client_Key.Text))
                {
                    clientkey = "";
                }
                else
                {
                    clientkey = txt_Client_Key.Text.Trim();
                }
                
                int ret = morFinAuth.Init(GblProductName, ref ObjDeviceInfo , clientkey);
                if (ret != 0)
                {
                    InfoMessage(morFinAuth.GetErrDescription(ret), true);
                }
                else
                {
                    ClearDeviceInfo();
                    lblInitInfo.Text = "SrNo:" + ObjDeviceInfo.SerialNo + ", Make:" + ObjDeviceInfo.Make + ", Model :" + ObjDeviceInfo.Model + ", Fw :" + ObjDeviceInfo.Firmware + ", W:" + ObjDeviceInfo.Width + ", H:" + ObjDeviceInfo.Height;
                    InfoMessage("Device initialized", false);
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex.ToString(), true);
            }
            finally
            {
                GC.Collect();
            }
        }
        private void btnUninit_Click(object sender, EventArgs e)
        {
            ClearDeviceInfo();
            try
            {
                resetControl();
                txt_Client_Key.Text = null;
                txtQuality.Text = "60";
                txtTimeout.Text = "10000";
                Application.DoEvents(); Application.DoEvents(); Application.DoEvents();
                int ret = morFinAuth.Uninit();
                if (ret != 0)
                {
                    InfoMessage(morFinAuth.GetErrDescription(ret), true);
                }
                else
                {
                    InfoMessage("Device Uninitialized", false);
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex.ToString(), true);
            }
            finally
            {
                GC.Collect();
            }
        }
        private void btnStartCapture_Click(object sender, EventArgs e)
        {
            try
            {
                IsMatching = 0;
                bytesTemplateOld = null;
                resetControl();
                if (setQuality() == false)
                {
                    return;
                }
                if (setTimeout() == false)
                {
                    return;
                }
                int ret = morFinAuth.StartCapture(timeout, quality);
                if (ret != 0)
                {
                    InfoMessage(morFinAuth.GetErrDescription(ret), true);
                    return;
                }
                InfoMessage("Start Capture In Progress...", false);
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex.ToString(), true);
            }
            finally
            {
                GC.Collect();
            }
        }
        private void AutoCapture()
        {
            try
            {
                try
                {
                    IsMatching = 0;
                    resetControl();
                    if (setQuality() == false)
                    {
                        return;
                    }
                    if (setTimeout() == false)
                    {
                        return;
                    }
                    int nfiq;
                    InfoMessage("Auto Capture In Progress...", false);
                    int ret = morFinAuth.AutoCapture(out quality, out nfiq, timeout, quality);
                    if (ret != 0)
                    {
                        InfoMessage(morFinAuth.GetErrDescription(ret), true);
                    }
                    else
                    {
                        if (quality > 0)
                        {
                            GetTemplate();
                            lblQltyAndNfiq.Text = "Quality: " + quality.ToString() + ", NFIQ: " + nfiq.ToString();
                            InfoMessage("Auto Capture Completed", false);
                        }
                    }
                    lblQltyAndNfiq.Refresh();
                }
                catch (Exception ex)
                {
                    ExceptionMessage(ex.ToString(), true);
                }
                finally
                {
                    GC.Collect();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnAutoCapture_Click(object sender, EventArgs e)
        {
            Thread trd = new Thread(new ThreadStart(() =>
            {
                AutoCapture();
            }));
            trd.IsBackground = true;
            trd.Start();
        }

        private void btnStopCapture_Click(object sender, EventArgs e)
        {
            try
            {
                resetControl();
                int ret = morFinAuth.StopCapture();
                if (ret != 0)
                {
                    InfoMessage(morFinAuth.GetErrDescription(ret), true);
                }
                else
                {
                    InfoMessage("Stop : " + morFinAuth.GetErrDescription(ret), false);
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex.ToString(), true);
            }
            finally
            {
                GC.Collect();
            }
        }

        #endregion

        #region Function
        bool setQuality()
        {
            try
            {
                quality = Convert.ToInt32(txtQuality.Text.Trim());
                return true;
            }
            catch (Exception ex)
            {
                ExceptionMessage("Invalid Quality Value", true);
            }
            finally
            {
                GC.Collect();
            }
            return false;

        }
        bool setTimeout()
        {
            try
            {
                timeout = Convert.ToInt32(txtTimeout.Text.Trim());
                return true;
            }
            catch (Exception ex)
            {
                ExceptionMessage("Invalid Timeout Value", true);
            }
            finally
            {
                GC.Collect();
            }
            return false;

        }
        void ExceptionMessage(string msg, bool iserror)
        {
            MessageBox.Show(msg, "MorFin_Auth_Core", MessageBoxButtons.OK, (iserror ? MessageBoxIcon.Error : MessageBoxIcon.Information), MessageBoxDefaultButton.Button1);
        }
        void InfoMessage(string msg, bool iserror)
        {

            lblStatus.ForeColor = Color.White;

            lblStatus.Text = msg;
            lblStatus.Refresh(); lblStatus.Refresh();
        }

        private void StartCaptureForMatching(object sender, EventArgs e)
        {
            try
            {
                resetControlForMatching();
                if (setQuality() == false)
                {
                    return;
                }
                if (setTimeout() == false)
                {
                    return;
                }
                int nfiq;
                InfoMessage("Auto Capture In Progress...", false);
                int ret = morFinAuth.AutoCapture(out quality, out nfiq, timeout, quality);
                if (ret != 0)
                {
                    InfoMessage(morFinAuth.GetErrDescription(ret), true);
                    return;
                }
                lblQltyAndNfiq.Text = "Quality: " + quality.ToString() + ", NFIQ: " + nfiq.ToString();
                MatchFinger();
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex.ToString(), true);
            }
            finally
            {
                GC.Collect();
            }
        }

        void OnCaptureCompleted(CaptureData ObjCaptureData)
        {
            try
            {
                if (ObjCaptureData.ErrorCode == 0)
                {
                    InfoMessage("Captured Success", false);
                    lblQltyAndNfiq.Text = "Quality: " + ObjCaptureData.Quality.ToString() + ", NFIQ: " + ObjCaptureData.Nfiq.ToString();
                }
                else
                {
                    InfoMessage("Failed : " + morFinAuth.GetErrDescription(ObjCaptureData.ErrorCode), true);
                    lblQltyAndNfiq.Text = "Quality: " + ObjCaptureData.Quality.ToString() + ", NFIQ: " + ObjCaptureData.Nfiq.ToString();
                }
                lblStatus.Refresh();
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex.ToString(), true);
            }
            finally
            {
                GC.Collect();
            }
        }

        public void GetTemplate()
        {
            int ret = morFinAuth.GetTemplate(out bytesTemplateOld, (TEMPLATE_FORMAT)cmbTemplateType.SelectedIndex, int_compressionRatio);
            if (ret != 0)
            {
                InfoMessage(morFinAuth.GetErrDescription(ret), true);
                return;
            }
        }
        public void MatchFinger()
        {
            try
            {
                byte[] bytesTemplateNew = null;
                int MatchScore = 0;

                int ret = morFinAuth.GetTemplate(out bytesTemplateNew, (TEMPLATE_FORMAT)cmbTemplateType.SelectedIndex, int_compressionRatio);
                if (ret != 0)
                {
                    InfoMessage(morFinAuth.GetErrDescription(ret), true);
                    return;
                }
                if (bytesTemplateOld != null && bytesTemplateOld.Length > 0)
                {
                    ret = morFinAuth.MatchTemplate(bytesTemplateNew, bytesTemplateNew.Length, bytesTemplateOld, bytesTemplateOld.Length, out  MatchScore, (TEMPLATE_FORMAT)cmbTemplateType.SelectedIndex);
                    if (ret != 0)
                    {
                        InfoMessage(morFinAuth.GetErrDescription(ret), true);
                        return;
                    }
                    InfoMessage("Template Match With Score : " + MatchScore.ToString(), false);
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex.ToString(), true);
            }
            finally
            {
                GC.Collect();
            }
        }

        void OnPreview(CaptureData ObjCaptureData)
        {
            try
            {
                Bitmap BitImage = new Bitmap(ObjCaptureData.AutoCaptureBitmap);
                picFinger.Image = BitImage;
                picFinger.Refresh();
                lblQltyAndNfiq.Text = "Quality: " + ObjCaptureData.Quality.ToString();
            }
            catch
            {

            }
            finally
            {
                GC.Collect();
            }
        }

        void OnFingerPosition(CaptureData ObjCaptureData)
        {
            try
            {
                lblStatus.Text = null;
                lblStatus.Text = ObjCaptureData.FingerPosition.ToString();
                lblStatus.Refresh();

            }
            catch
            {

            }
            finally
            {
                GC.Collect();
            }
        }
        void resetControlForMatching()
        {
            lblStatus.Text = "";
            lblQltyAndNfiq.Text = "Quality: 0, NFIQ: 0";
            picFinger.Image = null;
        }
        void resetControl()
        {
            bytesTemplateOld = null;
            lblStatus.Text = "";
            lblQltyAndNfiq.Text = "Quality: 0, NFIQ: 0";
            picFinger.Image = null;
        }
        public bool CheckProductName()
        {
            if (string.IsNullOrEmpty(GblProductName))
            {
                lblStatus.ForeColor = Color.Red;
                return false;
            }
            return true;
        }

        public void ClearDeviceInfo()
        {
            bytesTemplateOld = null;
            lblInitInfo.Text = "";
        }

        #endregion

        #region Button Event
        private void btnGetTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytesTemplate = null;
                int ret = morFinAuth.GetTemplate(out bytesTemplate, (TEMPLATE_FORMAT)cmbTemplateType.SelectedIndex, 15);
                if (ret != 0)
                {
                    InfoMessage(morFinAuth.GetErrDescription(ret), true);
                }
                else
                {
                    System.IO.File.WriteAllBytes(cmbTemplateType.Text + ".iso", bytesTemplate);
                    InfoMessage(cmbTemplateType.Text + " Template saved successfully on application path.", false);
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex.ToString(), true);
            }
            finally
            {
                GC.Collect();
            }
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytesTemplate = null;
                int ret = morFinAuth.GetImage(out bytesTemplate, (IMAGE_FORMAT)cmbImageType.SelectedIndex, int_compressionRatio);
                if (ret != 0)
                {
                    InfoMessage(morFinAuth.GetErrDescription(ret), true);
                }
                else
                {
                    if (cmbImageType.Text == "BMP")
                    {
                        using (Image image = Image.FromStream(new MemoryStream(bytesTemplate)))
                        {
                            if (cmbImageType.Text == "BMP")
                                image.Save("BMP.bmp", ImageFormat.Bmp);
                        }
                    }
                    else if (cmbImageType.Text == "JPEG2000" || cmbImageType.Text == "WSQ" || cmbImageType.Text == "RAW")
                    {
                        if (cmbImageType.Text == "JPEG2000")
                            System.IO.File.WriteAllBytes("JPEG2000.jp2", bytesTemplate);
                        else if (cmbImageType.Text == "WSQ")
                            System.IO.File.WriteAllBytes("WSQ.wsq", bytesTemplate);
                        else if (cmbImageType.Text == "RAW")
                            System.IO.File.WriteAllBytes("RAW.raw", bytesTemplate);
                    }
                    else if (cmbImageType.Text == "FIR_V2005" || cmbImageType.Text == "FIR_V2011" || cmbImageType.Text == "FIR_WSQ_V2005"
                       || cmbImageType.Text == "FIR_WSQ_V2011" || cmbImageType.Text == "FIR_JPEG2000_V2005" || cmbImageType.Text == "FIR_JPEG2000_V2011")
                    {
                        System.IO.File.WriteAllBytes(cmbImageType.Text + ".iso", bytesTemplate);
                    }
                    InfoMessage("Image saved successfully on application path.", false);
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex.ToString(), true);
            }
            finally
            {
                GC.Collect();
            }

            try
            {
                byte[] bytesTemplate = null;
                int ret = morFinAuth.GetTemplate(out bytesTemplate, (TEMPLATE_FORMAT)cmbTemplateType.SelectedIndex, int_compressionRatio);
                if (ret != 0)
                {
                    InfoMessage(morFinAuth.GetErrDescription(ret), true);
                }
                else
                {
                    System.IO.File.WriteAllBytes(cmbTemplateType.Text + ".iso", bytesTemplate);
                    InfoMessage("Image and template saved successfully on application path.", false);
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex.ToString(), true);
            }
            finally
            {
                GC.Collect();
            }
        }

        private void btnMatchISO_Click(object sender, EventArgs e)
        {
            byte[] bytesTemplate = null;
            int MatchScore = 0;
            int ret = morFinAuth.GetTemplate(out bytesTemplate, TEMPLATE_FORMAT.FMR_V2005, int_compressionRatio);
            if (ret != 0)
            {
                InfoMessage(morFinAuth.GetErrDescription(ret), true);
                return;
            }
            ret = morFinAuth.MatchTemplate(bytesTemplate, bytesTemplate.Length, bytesTemplate, bytesTemplate.Length, out  MatchScore, TEMPLATE_FORMAT.FMR_V2005);
            if (ret != 0)
            {
                InfoMessage(morFinAuth.GetErrDescription(ret), true);
                return;
            }

            InfoMessage("ISO Template Match With Score : " + MatchScore.ToString(), false);

        }

        private void btnMatchAnsi_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn_SupportDvc_Click(object sender, EventArgs e)
        {
            List<string> DeviceList = new List<string>();
            int ret = morFinAuth.GetSupportedDevices(DeviceList);
            if (ret != 0)
            {
                InfoMessage(morFinAuth.GetErrDescription(ret), true);
                return;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("Model", typeof(String));
            for (int i = 0; i < DeviceList.Count; i++)
            {
                dt.Rows.Add(DeviceList[i].ToString());
            }
            cmbSupportedDvc.DataSource = dt;
            cmbSupportedDvc.DisplayMember = "Model";
        }

        private void btn_ConnectDvc_Click(object sender, EventArgs e)
        {
            List<string> DeviceList = new List<string>();
            cmbConnectedDvc.DataSource = null;
            int ret = morFinAuth.GetConnectedDevices(DeviceList);
            if (ret != 0)
            {
                InfoMessage(morFinAuth.GetErrDescription(ret), true);
                return;
            }
            if (DeviceList.Count > 0)
            {
                try
                {
                    pbIsDeviceConnected.Image = MorFin_Auth_CSharp.Properties.Resources.GreenFinger;
                }
                catch
                {

                }
                DataTable dt = new DataTable();
                dt.Columns.Add("Model", typeof(String));
                for (int i = 0; i < DeviceList.Count; i++)
                {
                    dt.Rows.Add(DeviceList[i].ToString());
                }
                cmbConnectedDvc.DataSource = dt;
                cmbConnectedDvc.DisplayMember = "Model";
                if (dt != null && dt.Rows.Count > 0)
                    GblProductName = cmbConnectedDvc.Text;
                else
                    GblProductName = "";
            }
        }
        bool IsProcess = false;
        private void AutoConnect()
        {
            if (IsProcess)
            {
                return;
            }
            try
            {
                IsProcess = true;
                List<string> DeviceList = new List<string>();
                cmbConnectedDvc.DataSource = null;
                int ret = morFinAuth.GetConnectedDevices(DeviceList);
                if (ret != 0)
                {
                    InfoMessage(morFinAuth.GetErrDescription(ret), true);
                    return;
                }
                if (DeviceList.Count > 0)
                {
                    try
                    {
                        pbIsDeviceConnected.Image = MorFin_Auth_CSharp.Properties.Resources.GreenFinger;
                    }
                    catch
                    {

                    }
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Model", typeof(String));
                    for (int i = 0; i < DeviceList.Count; i++)
                    {
                        dt.Rows.Add(DeviceList[i].ToString());
                    }
                    cmbConnectedDvc.DataSource = dt;
                    cmbConnectedDvc.DisplayMember = "Model";
                    if (dt != null && dt.Rows.Count > 0)
                        GblProductName = cmbConnectedDvc.Text;
                    else
                        GblProductName = "";
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex.ToString(), true);
            }
            finally
            {
                IsProcess = false;
                GC.Collect();
            }
        }

        #endregion

        #region Other Event
        private void cmbConnectedDvc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GblProductName = cmbConnectedDvc.Text;
            }
            catch (Exception)
            {
                GblProductName = "";
            }
        }

        private void btnMatchFinger_Click(object sender, EventArgs e)
        {
            if (bytesTemplateOld == null)
            {
                InfoMessage("First Follow Auto Capture Request!", true);
                return;
            }

            Thread trd = new Thread(new ThreadStart(() =>
            {
                StartCaptureForMatching(null, null);
            }));
            trd.IsBackground = true;
            trd.Start();

        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex.Message, true);
            }
        }

        private void pnlBack_Paint(object sender, PaintEventArgs e)
        {
            Color ColorNormal = Color.FromArgb(0, 112, 192);
            ControlPaint.DrawBorder(e.Graphics, pnlBack.ClientRectangle,
                       ColorNormal, 3, ButtonBorderStyle.Solid, // left
                       ColorNormal, 3, ButtonBorderStyle.Solid, // top
                       ColorNormal, 3, ButtonBorderStyle.Solid, // right
                       ColorNormal, 3, ButtonBorderStyle.Solid);// bottom
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {
            Color ColorNormal = Color.FromArgb(0, 112, 192);
            ControlPaint.DrawBorder(e.Graphics, pnlHeader.ClientRectangle,
                       ColorNormal, 3, ButtonBorderStyle.Solid, // left
                       ColorNormal, 3, ButtonBorderStyle.Solid, // top
                       ColorNormal, 3, ButtonBorderStyle.Solid, // right
                       ColorNormal, 1, ButtonBorderStyle.Solid);// bottom
        }
        #endregion
    }
}
