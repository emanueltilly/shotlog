using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShotLog
{
    public partial class Form1 : Form
    {
        ProjectData data = new ProjectData();
        
        

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(KeyEvent);

        }

        private void KeyEvent(object sender, KeyEventArgs e) //Keyup Event 
        {
            if (e.KeyCode == Keys.F1)
            {
                NewExposure();
            }

        }

        //UPDATE GUI FROM DATA OBJECT
        private void LoadGUIfromData()
        {
            this.Text = (data.projectName + " - ShotLog");
            projectName.Text = data.projectName;
            usePhotometrics.Checked = data.photometricDataEnabled;

            //Stills
            stillsFilenamePrefix.Text = data.stillsPrefix;
            stillsFilenameIndexLength.Value = data.stillsIndexLength;
            stillsNextFileIndex.Value = data.stillsNextIndex;
            stillsCameraName.Text = data.stillsCameraName;
            stillsAperture.Value = (decimal)data.stillsAperture;
            stillsFocalLength.Value = data.stillsFocalLength;
            stillsWhitebalance.Value = data.stillsWhiteBalance;
            stillsISO.Value = data.stillsISO;
            stillsShutterSpeed.Value = data.stillsShutterspeedBase;

            stillsBrackeringEnabled.Checked = data.stillsBracketed;

            stillsBrackeringStops.Value = data.stillsBracketSteps;
            stillsBracketingOrder.Text = data.stillsBracketOrder;

            //Video Settings
            videoUseSceneShotTake.Checked = data.videoUseSceneNumbering;
            videoUseBroadcast.Checked = data.videoUseStudioCamSettings;

            videoNamePrefix.Text = data.videoPrefix;
            videoIndexLength.Value = data.videoIndexLength;
            videoNextFileIndex.Value = data.videoNextIndex;
            videoScene.Value = data.videoScene;
            videoShot.Value = data.videoShot;
            videoTake.Value = data.videoTake;
            videoCameraName.Text = data.videoCameraName;
            videoFormat.Text = data.videoFormat;
            videoAperture.Value = (decimal)data.videoAperture;
            videoFocalLength.Value = data.videoFocalLength;
            videoWhitebalance.Value = data.videoWhiteBalance;
            videoISO.Value = data.videoISO;
            videoShutterspeed.Value = data.videoShutterspeed;

            //Video broadcast
            videoBroadcastMasterBlack.Value = data.videoStudioMasterBlack;
            videoBroadcastGain.Value = data.videoStudioGain;
            videoBroadcastSaturation.Value = data.videoStudioSaturation;
            videoBroadcastTint.Value = data.videoStudioTint;
            videoBroadcastFilter.Text = data.videoStudioFilter;

            videoBroadcastGainRed.Value = data.videoStudioRedGain;
            videoBroadcastGainGreen.Value = data.videoStudioGreenGain;
            videoBroadcastGainBlue.Value = data.videoStudioBlueGain;

            videoBroadcastBlackRed.Value = data.videoStudioRedBlack;
            videoBroadcastBlackGreen.Value = data.videoStudioGreenBlack;
            videoBroadcastBlackBlue.Value = data.videoStudioBlueBlack;

            //Update enabled
            UpdateGUIenabled();

            //Update speadsheets
            ReloadDataGridView();


        }

        private void SaveGUItoData()
        {
            //Project
            data.projectName = projectName.Text;
            data.photometricDataEnabled = usePhotometrics.Checked;

            //Stills Settings
            data.stillsPrefix = stillsFilenamePrefix.Text;
            data.stillsIndexLength = (int)stillsFilenameIndexLength.Value;
            data.stillsNextIndex = (int)stillsNextFileIndex.Value;
            data.stillsCameraName = stillsCameraName.Text;
            data.stillsAperture = (int)stillsAperture.Value;
            data.stillsFocalLength = (int)stillsFocalLength.Value;
            data.stillsWhiteBalance = (int)stillsWhitebalance.Value;
            data.stillsISO = (int)stillsISO.Value;
            data.stillsShutterspeedBase = (int)stillsShutterSpeed.Value;

            data.stillsBracketed = stillsBrackeringEnabled.Checked;

            data.stillsBracketSteps = (int)stillsBrackeringStops.Value;
            data.stillsBracketOrder = stillsBracketingOrder.Text;

            //Video Settings
            data.videoUseSceneNumbering = videoUseSceneShotTake.Checked;
            data.videoUseStudioCamSettings = videoUseBroadcast.Checked;

            data.videoPrefix = videoNamePrefix.Text;
            data.videoIndexLength = (int)videoIndexLength.Value;
            data.videoNextIndex = (int)videoNextFileIndex.Value;

            data.videoScene = (int)videoScene.Value;
            data.videoShot = (int)videoShot.Value;
            data.videoTake = (int)videoTake.Value;

            data.videoCameraName = videoCameraName.Text;
            data.videoFormat = videoFormat.Text;
            data.videoAperture = (int)videoAperture.Value;
            data.videoFocalLength = (int)videoFocalLength.Value;
            data.videoWhiteBalance = (int)videoWhitebalance.Value;
            data.videoISO = (int)videoISO.Value;
            data.videoShutterspeed = (int)videoShutterspeed.Value;

            //Video Broadcast Settings
            data.videoStudioMasterBlack = (int)videoBroadcastMasterBlack.Value;
            data.videoStudioGain = (int)videoBroadcastGain.Value;
            data.videoStudioSaturation = (int)videoBroadcastSaturation.Value;
            data.videoStudioTint = (int)videoBroadcastTint.Value;
            data.videoStudioFilter = videoBroadcastFilter.Text;

            data.videoStudioRedGain = (int)videoBroadcastGainRed.Value;
            data.videoStudioGreenGain = (int)videoBroadcastGainGreen.Value;
            data.videoStudioBlueGain = (int)videoBroadcastGainBlue.Value;

            data.videoStudioRedBlack = (int)videoBroadcastBlackRed.Value;
            data.videoStudioGreenBlack = (int)videoBroadcastBlackGreen.Value;
            data.videoStudioBlueBlack = (int)videoBroadcastBlackBlue.Value;

        }
        
        private void UpdateGUIenabled()
        {
            //Video Studio camera mode
            groupBox3.Enabled = videoUseBroadcast.Checked;
            videoISO.Enabled = (videoUseBroadcast.Checked ? false : true);
            videoShutterspeed.Enabled = (videoUseBroadcast.Checked ? false : true);

            //Scene Shot Take Naming
            videoScene.Enabled = videoUseSceneShotTake.Checked;
            videoShot.Enabled = videoUseSceneShotTake.Checked;
            videoTake.Enabled = videoUseSceneShotTake.Checked;

            videoIndexLength.Enabled = (videoUseSceneShotTake.Checked ? false : true);
            videoNextFileIndex.Enabled = (videoUseSceneShotTake.Checked ? false : true);

            //Bracketing
            stillsBracketingOrder.Enabled = stillsBrackeringEnabled.Checked;
            stillsBrackeringStops.Enabled = stillsBrackeringEnabled.Checked;
        }



        //SAVE & OPEN PROJECT FUNCTIONS
        private void SaveProject()
        {
            SaveGUItoData();

            SaveFileDialog SaveFileDialog1 = new SaveFileDialog
            {
                RestoreDirectory = true,
                Title = "Save Project",
                DefaultExt = "xml",
                Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*"
            };
            //SaveFileDialog1.ShowDialog();

            if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //SAVE TO FILE
                if (data.SaveToFile(SaveFileDialog1.FileName) == true)
                {
                    lastSavedLabel.Text = ("Last saved: " + DateTime.Now.ToString("t"));
                    data.savePath = SaveFileDialog1.FileName;
                }
                
                LoadGUIfromData();
            }
        }

        private void OpenProject()
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Title = "Open Project",
                Filter = "Xml Files (*.xml)|*.xml" + "|" +
                            "All Files (*.*)|*.*"
            };
            if (openDialog.ShowDialog() == DialogResult.OK)
            {

                data = ProjectData.LoadFromFile(openDialog.FileName);
                data.savePath = openDialog.FileName;
                LoadGUIfromData();
                autosaveTimer.Interval = data.autoSaveDuration;
                autosaveTimer.Start();

            }
        }

        private void NewExposure()
        {
            SaveGUItoData();
            
            ExposurePopup expoPopup = new ExposurePopup();
            expoPopup.SetProjectData(data); //Send project data to popup
            expoPopup.ShowDialog(); //Show popup and wait for popup to close

            LoadGUIfromData();

        }

        private void ReloadDataGridView()
        {
            //Link list to data grid view VIDEO
            var source1 = new BindingSource();
            source1.DataSource = data.VideoList;
            dataGridViewVideo.DataSource = source1;

            //Link list to data grid view STILLS
            var source2 = new BindingSource();
            source2.DataSource = data.StillsList;
            dataGridViewStills.DataSource = source2;

            //Colors
            SetDatagridviewColors();
        }

        private void SetDatagridviewColors()
        {
            //VIDEO
            dataGridViewVideo.Columns["ExposureID"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255, 179);
            dataGridViewVideo.Columns["Timestamp"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255, 179);
            dataGridViewVideo.Columns["Filename"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255, 179);

            dataGridViewVideo.Columns["Scene"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 191, 179);
            dataGridViewVideo.Columns["Shot"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 191, 179);
            dataGridViewVideo.Columns["Take"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 191, 179);

            dataGridViewVideo.Columns["Photometrics"].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 179, 255);
            dataGridViewVideo.Columns["Fixture"].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 179, 255);
            dataGridViewVideo.Columns["Distance"].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 179, 255);
            dataGridViewVideo.Columns["LUX"].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 179, 255);
            dataGridViewVideo.Columns["Kelvin"].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 179, 255);
            dataGridViewVideo.Columns["CRI"].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 179, 255);
            dataGridViewVideo.Columns["Zoom"].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 179, 255);
            dataGridViewVideo.Columns["Dimmer"].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 179, 255);

            dataGridViewVideo.Columns["Camera"].DefaultCellStyle.BackColor = Color.FromArgb(255, 179, 255, 255);
            dataGridViewVideo.Columns["Format"].DefaultCellStyle.BackColor = Color.FromArgb(255, 179, 255, 255);
            dataGridViewVideo.Columns["Aperture"].DefaultCellStyle.BackColor = Color.FromArgb(255, 179, 255, 255);
            dataGridViewVideo.Columns["Focallength"].DefaultCellStyle.BackColor = Color.FromArgb(255, 179, 255, 255);
            dataGridViewVideo.Columns["Whitebalance"].DefaultCellStyle.BackColor = Color.FromArgb(255, 179, 255, 255);
            dataGridViewVideo.Columns["ISO"].DefaultCellStyle.BackColor = Color.FromArgb(255, 179, 255, 255);
            dataGridViewVideo.Columns["Shutterspeed"].DefaultCellStyle.BackColor = Color.FromArgb(255, 179, 255, 255);

            dataGridViewVideo.Columns["MasterBlack"].DefaultCellStyle.BackColor = Color.FromArgb(255, 191, 255, 179);
            dataGridViewVideo.Columns["Tint"].DefaultCellStyle.BackColor = Color.FromArgb(255, 191, 255, 179);
            dataGridViewVideo.Columns["Saturation"].DefaultCellStyle.BackColor = Color.FromArgb(255, 191, 255, 179);
            dataGridViewVideo.Columns["Gain"].DefaultCellStyle.BackColor = Color.FromArgb(255, 191, 255, 179);
            dataGridViewVideo.Columns["Filter"].DefaultCellStyle.BackColor = Color.FromArgb(255, 191, 255, 179);
            dataGridViewVideo.Columns["Gain_Red"].DefaultCellStyle.BackColor = Color.FromArgb(255, 191, 255, 179);
            dataGridViewVideo.Columns["Gain_Green"].DefaultCellStyle.BackColor = Color.FromArgb(255, 191, 255, 179);
            dataGridViewVideo.Columns["Gain_Blue"].DefaultCellStyle.BackColor = Color.FromArgb(255, 191, 255, 179);
            dataGridViewVideo.Columns["Black_Red"].DefaultCellStyle.BackColor = Color.FromArgb(255, 191, 255, 179);
            dataGridViewVideo.Columns["Black_Green"].DefaultCellStyle.BackColor = Color.FromArgb(255, 191, 255, 179);
            dataGridViewVideo.Columns["Black_Blue"].DefaultCellStyle.BackColor = Color.FromArgb(255, 191, 255, 179);

            //STILLS
            dataGridViewStills.Columns["ExposureID"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255, 179);
            dataGridViewStills.Columns["Timestamp"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255, 179);
            dataGridViewStills.Columns["Filename"].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255, 179);

            dataGridViewStills.Columns["Photometrics"].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 179, 255);
            dataGridViewStills.Columns["Fixture"].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 179, 255);
            dataGridViewStills.Columns["Distance"].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 179, 255);
            dataGridViewStills.Columns["LUX"].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 179, 255);
            dataGridViewStills.Columns["Kelvin"].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 179, 255);
            dataGridViewStills.Columns["CRI"].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 179, 255);
            dataGridViewStills.Columns["Zoom"].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 179, 255);
            dataGridViewStills.Columns["Dimmer"].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 179, 255);

            dataGridViewStills.Columns["Camera"].DefaultCellStyle.BackColor = Color.FromArgb(255, 179, 255, 255);
            dataGridViewStills.Columns["Aperture"].DefaultCellStyle.BackColor = Color.FromArgb(255, 179, 255, 255);
            dataGridViewStills.Columns["Focallength"].DefaultCellStyle.BackColor = Color.FromArgb(255, 179, 255, 255);
            dataGridViewStills.Columns["Whitebalance"].DefaultCellStyle.BackColor = Color.FromArgb(255, 179, 255, 255);
            dataGridViewStills.Columns["ISO"].DefaultCellStyle.BackColor = Color.FromArgb(255, 179, 255, 255);
            dataGridViewStills.Columns["Shutterspeed"].DefaultCellStyle.BackColor = Color.FromArgb(255, 179, 255, 255);

            dataGridViewStills.Columns["Bracketed"].DefaultCellStyle.BackColor = Color.FromArgb(255, 191, 255, 179);
            dataGridViewStills.Columns["Exposure"].DefaultCellStyle.BackColor = Color.FromArgb(255, 191, 255, 179);

        }

        private void SaveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveProject();
            LoadGUIfromData();
        }

        private void OpenProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProject();
        }

        private void ApplySettingsButton_Click(object sender, EventArgs e)
        {
            SaveGUItoData();
            LoadGUIfromData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadGUIfromData();
        }

        private void NewExposureButton_Click(object sender, EventArgs e)
        {
            NewExposure();
        }

        private void VideoUseSceneShotTake_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGUIenabled();
        }

        private void VideoUseBroadcast_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGUIenabled();
        }

        private void StillsBrackeringEnabled_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGUIenabled();
        }

        private void VideoScene_ValueChanged(object sender, EventArgs e)
        {
            videoTake.Value = 1;
        }

        private void VideoShot_ValueChanged(object sender, EventArgs e)
        {
            videoTake.Value = 1;
        }

        private void ExportVideoLogButton_Click(object sender, EventArgs e)
        {
            ExportCSV.ExportVideo(data.VideoList);
        }

        private void ExportStillsLogButton_Click(object sender, EventArgs e)
        {
            ExportCSV.ExportStills(data.StillsList);
        }

        private void EnableAutoSaveButton_Click(object sender, EventArgs e)
        {
            data.autoSave = true;
        }

        private void DisableAutoSaveButton_Click(object sender, EventArgs e)
        {
            data.autoSave = false;
        }

        private void Autosave1m_Click(object sender, EventArgs e)
        {
            UpdateAutosaveTimer(1);

        }

        private void Autosave5m_Click(object sender, EventArgs e)
        {
            UpdateAutosaveTimer(5);
        }

        private void Autosave10m_Click(object sender, EventArgs e)
        {
            UpdateAutosaveTimer(10);
        }

        private void Autosave30m_Click(object sender, EventArgs e)
        {
            UpdateAutosaveTimer(30);
        }

        private void UpdateAutosaveTimer(int durationMin)
        {
            data.autoSaveDuration = (durationMin * 60000);
            data.autoSave = true;
            autosaveTimer.Interval = data.autoSaveDuration;
            autosaveTimer.Start();
        }

        private void AutosaveTimer_Tick(object sender, EventArgs e)
        {
            if (data.autoSave == true)
            {
                if (data.AutoSaveToFile() == true)
                {
                    lastSavedLabel.Text = ("Last saved: " + DateTime.Now.ToString("t"));
                } else
                {
                    lastSavedLabel.Text = ("AutoSave Error!");
                }
            }
            
        }

        private void AutosaveChecker_Tick(object sender, EventArgs e)
        {

            if (data.autoSave == true && data.savePath != "" && autosaveTimer.Enabled == true)
            {
                autosaveLabel.Text = "Autosave Enabled";
            } else
            {
                autosaveLabel.Text = "Autosave Disabled";
            }
        }
    }
}
