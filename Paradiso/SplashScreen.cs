using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using DAnTE.Tools;
using Microsoft.Win32;

namespace DAnTE.Paradiso
{
    /// <summary>
    /// Summary description for SplashScreen.
    /// </summary>
    public class SplashScreen : Form
    {
        public const string VALIDATING_R_PACKAGES = "Validating R Packages ...";

        private enum PackageValidationState
        {
            Initializing = 0,
            Started = 1,
            OverThreeSeconds = 2,
            OverSixSeconds = 3
        }

        // Threading
        static SplashScreen ms_frmSplash;
        static Thread ms_oThread;

        // Fade in and out.
        private double m_dblOpacityIncrement = .05;
        private readonly double m_dblOpacityDecrement = .08;
        private const int TIMER_INTERVAL = 50;

        // Status and progress bar
        static string ms_sStatus;
        private double m_dblCompletionFraction;
        private Rectangle m_rProgress;

        // Progress smoothing
        private double m_dblLastCompletionFraction;
        private double m_dblPBIncrementPerTimerInterval = .015;

        // Self-calibration support
        private bool m_bFirstLaunch;
        private DateTime m_dtStart;
        private bool m_bDTSet;
        private int m_iIndex = 1;
        private int m_iActualTicks;
        private List<double> m_alPreviousCompletionFraction;
        private readonly List<double> m_ActualTimes = new List<double>();

        private const string REG_VALUE_PB_MILLISECOND_INCREMENT = "Increment";
        private const string REG_VALUE_PB_PERCENTS = "Percents";

        //
        private PackageValidationState m_ValidatingPackagesState = PackageValidationState.Initializing;
        private DateTime m_ValidatingPackagesStartTime;
        private DateTime m_LastSecondsRemainingUpdate;

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblTimeRemaining;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pnlStatus;
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// Constructor
        /// </summary>
        public SplashScreen()
        {
            InitializeComponent();
            this.Opacity = .00;
            timer1.Interval = TIMER_INTERVAL;
            timer1.Start();

            // Do not allow the form to be too large
            // Otherwise, it can cover up Dialog Boxes shown by R
            this.Width = 525;
            this.Height = 450;

            Control.CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblTimeRemaining = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            //
            // lblStatus
            //
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold,
                                                          System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.SystemColors.Control;
            this.lblStatus.Location = new System.Drawing.Point(5, 327);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(514, 54);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "xx";
            this.lblStatus.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
            //
            // pnlStatus
            //
            this.pnlStatus.BackColor = System.Drawing.Color.Transparent;
            this.pnlStatus.Location = new System.Drawing.Point(18, 387);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(500, 4);
            this.pnlStatus.TabIndex = 1;
            this.pnlStatus.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlStatus_Paint);
            this.pnlStatus.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
            //
            // lblTimeRemaining
            //
            this.lblTimeRemaining.BackColor = System.Drawing.Color.Transparent;
            this.lblTimeRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F,
                                                                 System.Drawing.FontStyle.Bold,
                                                                 System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeRemaining.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTimeRemaining.Location = new System.Drawing.Point(16, 411);
            this.lblTimeRemaining.Name = "lblTimeRemaining";
            this.lblTimeRemaining.Size = new System.Drawing.Size(208, 30);
            this.lblTimeRemaining.TabIndex = 2;
            this.lblTimeRemaining.Text = "Time remaining";
            this.lblTimeRemaining.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
            //
            // timer1
            //
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            //
            // SplashScreen
            //
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.BackColor = System.Drawing.Color.LightGray;
            this.BackgroundImage = global::DAnTE.Properties.Resources.InfernoSplash21;
            this.ClientSize = new System.Drawing.Size(525, 450);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblTimeRemaining);
            this.Controls.Add(this.pnlStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SplashScreen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SplashScreen";
            this.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
            this.ResumeLayout(false);
        }

        #endregion

        // ************* Static Methods *************** //

        // A static method to create the thread and
        // launch the SplashScreen.
        public static void ShowSplashScreen()
        {
            // Make sure it's only launched once.
            if (ms_frmSplash != null)
                return;
            ms_oThread = new Thread(ShowForm)
            {
                IsBackground = true
            };
            //ms_oThread.ApartmentState = ApartmentState.STA;
            ms_oThread.Start();
        }

        // A property returning the splash screen instance
        public static SplashScreen SplashForm => ms_frmSplash;

        // A private entry point for the thread.
        private static void ShowForm()
        {
            ms_frmSplash = new SplashScreen();
            Application.Run(ms_frmSplash);
        }

        // A static method to close the SplashScreen
        public static void CloseForm()
        {
            if (ms_frmSplash != null && !ms_frmSplash.IsDisposed)
            {
                // Make it start going away.
                ms_frmSplash.m_dblOpacityIncrement = -Math.Abs(ms_frmSplash.m_dblOpacityDecrement);
            }
            ms_oThread = null; // we don't need these any more.
            ms_frmSplash = null;
        }

        // A static method to set the status and update the reference.
        public static void SetStatus(string newStatus)
        {
            SetStatus(newStatus, true);
        }

        // A static method to set the status and optionally update the reference.
        // This is useful if you are in a section of code that has a variable
        // set of status string updates.  In that case, don't set the reference.
        public static void SetStatus(string newStatus, bool setReference)
        {
            ms_sStatus = newStatus;
            if (ms_frmSplash == null)
                return;
            if (setReference)
                ms_frmSplash.SetReferenceInternal();
        }

        // Static method called from the initializing application to
        // give the splash screen reference points.  Not needed if
        // you are using a lot of status strings.
        public static void SetReferencePoint()
        {
            ms_frmSplash?.SetReferenceInternal();
        }

        // ************ Private methods ************

        // Internal method for setting reference points.
        private void SetReferenceInternal()
        {
            if (!m_bDTSet)
            {
                m_bDTSet = true;
                m_dtStart = DateTime.Now;
                ReadIncrements();
            }
            var dblMilliseconds = ElapsedMilliSeconds();
            m_ActualTimes.Add(dblMilliseconds);
            m_dblLastCompletionFraction = m_dblCompletionFraction;
            if (m_alPreviousCompletionFraction != null && m_iIndex < m_alPreviousCompletionFraction.Count)
                m_dblCompletionFraction = m_alPreviousCompletionFraction[m_iIndex++];
            else
                m_dblCompletionFraction = (m_iIndex > 0) ? 1 : 0;
        }

        // Utility function to return elapsed Milliseconds since the
        // SplashScreen was launched.
        private double ElapsedMilliSeconds()
        {
            var ts = DateTime.Now - m_dtStart;
            return ts.TotalMilliseconds;
        }

        // Function to read the checkpoint intervals from the previous invocation of the
        // splashscreen from the registry.
        private void ReadIncrements()
        {
            var sPBIncrementPerTimerInterval = RegistryAccess.GetStringRegistryValue(REG_VALUE_PB_MILLISECOND_INCREMENT,
                                                                                     "0.0015");
            if (clsUtilities.ParseDouble(sPBIncrementPerTimerInterval, out var dblResult))
                m_dblPBIncrementPerTimerInterval = dblResult;
            else
                m_dblPBIncrementPerTimerInterval = .0015;

            var sPBPreviousPctComplete = RegistryAccess.GetStringRegistryValue(REG_VALUE_PB_PERCENTS, "");

            if (sPBPreviousPctComplete != "")
            {
                var aTimes = sPBPreviousPctComplete.Split(null);
                m_alPreviousCompletionFraction = new List<double>();

                foreach (var timeVal in aTimes)
                {
                    if (clsUtilities.ParseDouble(timeVal, out var dblVal))
                        m_alPreviousCompletionFraction.Add(dblVal);
                    else
                        m_alPreviousCompletionFraction.Add(1.0);
                }
            }
            else
            {
                m_bFirstLaunch = true;
                lblTimeRemaining.Text = "";
            }
        }

        // Method to store the intervals (in percent complete) from the current invocation of
        // the splash screen to the registry.
        private void StoreIncrements()
        {
            var sPercent = "";
            var dblElapsedMilliseconds = ElapsedMilliSeconds();
            foreach (var timeVal in m_ActualTimes)
                sPercent +=
                    (timeVal / dblElapsedMilliseconds).ToString("0.####",
                                                          System.Globalization.NumberFormatInfo
                                                              .InvariantInfo) + " ";

            RegistryAccess.SetStringRegistryValue(REG_VALUE_PB_PERCENTS, sPercent);

            if (m_iActualTicks < 1)
                m_iActualTicks = 1;

            m_dblPBIncrementPerTimerInterval = 1.0 / m_iActualTicks;
            RegistryAccess.SetStringRegistryValue(REG_VALUE_PB_MILLISECOND_INCREMENT,
                                                  m_dblPBIncrementPerTimerInterval.ToString("#.000000",
                                                                                            System.Globalization
                                                                                                .NumberFormatInfo
                                                                                                .InvariantInfo));
        }

        //********* Event Handlers ************

        // Tick Event handler for the Timer control.  Handle fade in and fade out.  Also
        // handle the smoothed progress bar.
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblStatus.Text = ms_sStatus;

            if (m_dblOpacityIncrement > 0)
            {
                // Form is fading in
                m_iActualTicks++;
                if (this.Opacity < 1)
                    this.Opacity += m_dblOpacityIncrement;
            }
            else
            {
                // Form is fading out
                if (this.Opacity > 0)
                    this.Opacity += m_dblOpacityIncrement;
                else
                {
                    StoreIncrements();
                    this.Close();
                }
            }

            var showPeriods = true;
            if (!m_bFirstLaunch && m_dblLastCompletionFraction <= m_dblCompletionFraction)
            {
                m_dblLastCompletionFraction += m_dblPBIncrementPerTimerInterval;
                var width = (int)Math.Floor(pnlStatus.ClientRectangle.Width * m_dblLastCompletionFraction);
                var height = pnlStatus.ClientRectangle.Height;
                var x = pnlStatus.ClientRectangle.X;
                var y = pnlStatus.ClientRectangle.Y;

                if (width > 0 && height > 0)
                {
                    m_rProgress = new Rectangle(x, y, width, height);
                    pnlStatus.Invalidate(m_rProgress);
                    var iSecondsLeft = 1 +
                                       (int)
                                       (TIMER_INTERVAL *
                                        ((1.0 - m_dblLastCompletionFraction) / m_dblPBIncrementPerTimerInterval)) / 1000;

                    if (iSecondsLeft > 1)
                    {
                        lblTimeRemaining.Text = string.Format("{0} seconds remaining", iSecondsLeft);
                        showPeriods = false;
                    }
                    else
                    {
                        if (iSecondsLeft == 1)
                        {
                            lblTimeRemaining.Text = "1 second remaining";
                            showPeriods = false;
                        }
                    }
                }
            }

            if (showPeriods)
            {
                if (m_ValidatingPackagesStartTime > DateTime.UtcNow.AddDays(-1) &&
                    DateTime.UtcNow.AddSeconds(-2) > m_LastSecondsRemainingUpdate)
                {
                    var periodCount =
                        (int)(Math.Round(DateTime.UtcNow.Subtract(m_ValidatingPackagesStartTime).TotalSeconds)) % 8 + 1;
                    lblTimeRemaining.Text = new string('.', periodCount);
                }
            }
            else
            {
                m_LastSecondsRemainingUpdate = DateTime.UtcNow;
            }

            // Check for status "Validating R Packages" being visible for more than 3 seconds

            if (ms_sStatus == VALIDATING_R_PACKAGES)
            {
                if (m_ValidatingPackagesState == PackageValidationState.Initializing)
                {
                    m_ValidatingPackagesState = PackageValidationState.Started;
                    m_ValidatingPackagesStartTime = DateTime.UtcNow;
                }
                else if (m_ValidatingPackagesState == PackageValidationState.Started)
                {
                    if (DateTime.UtcNow.Subtract(m_ValidatingPackagesStartTime).TotalSeconds >= 3)
                    {
                        m_ValidatingPackagesState = PackageValidationState.OverThreeSeconds;
                        ms_sStatus =
                            "Validating R Packages ... If prompted 'Would you like to use a personal library?' Answer 'Yes' to each of the questions.";
                    }
                }
            }
            else
            {
                if (m_ValidatingPackagesState == PackageValidationState.OverThreeSeconds &&
                    DateTime.UtcNow.Subtract(m_ValidatingPackagesStartTime).TotalSeconds >= 6)
                {
                    lblStatus.BackColor = Color.Chocolate;
                    m_ValidatingPackagesState = PackageValidationState.OverSixSeconds;
                }
            }
        }

        // Paint the portion of the panel invalidated during the tick event.
        private void pnlStatus_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (!m_bFirstLaunch && e.ClipRectangle.Width > 0 && m_iActualTicks > 1)
            {
                try
                {
                    var brBackground = new LinearGradientBrush(m_rProgress, Color.FromArgb(213, 39, 27),
                                                               Color.FromArgb(245, 155, 27),
                                                               LinearGradientMode.Horizontal);
                    e.Graphics.FillRectangle(brBackground, m_rProgress);
                }
                catch
                {
                    // Ignore errors here
                }
            }
        }

        // Close the form if they double click on it.
        private void SplashScreen_DoubleClick(object sender, System.EventArgs e)
        {
            CloseForm();
        }
    }

    /// <summary>
    /// A class for managing registry access.
    /// </summary>
    public class RegistryAccess
    {
        private const string SOFTWARE_KEY = "Software";
        private const string COMPANY_NAME = "PNNL";
        private const string APPLICATION_NAME = "Inferno";

        // Method for retrieving a Registry Value.
        public static string GetStringRegistryValue(string key, string defaultValue)
        {
            var softwareKey = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, false);

            if (softwareKey == null)
            {
                return defaultValue;
            }

            var rkCompany = softwareKey.OpenSubKey(COMPANY_NAME, false);
            if (rkCompany == null)
            {
                return defaultValue;
            }

            var rkApplication = rkCompany.OpenSubKey(APPLICATION_NAME, true);
            if (rkApplication == null)
            {
                return defaultValue;
            }

            foreach (var sKey in rkApplication.GetValueNames())
            {
                if (sKey == key)
                {
                    return (string)rkApplication.GetValue(sKey);
                }
            }
            return defaultValue;
        }

        // Method for storing a Registry Value.
        public static void SetStringRegistryValue(string key, string stringValue)
        {
            var rkSoftware = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, true);

            var rkCompany = rkSoftware?.CreateSubKey(COMPANY_NAME);

            var rkApplication = rkCompany?.CreateSubKey(APPLICATION_NAME);
            rkApplication?.SetValue(key, stringValue);
        }
    }
}