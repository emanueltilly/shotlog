using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


using System.Reflection;
using System.Security.Principal;


namespace ShotLog
{
    public partial class Form1 : Form
    {
        ProjectData data = new ProjectData();

        bool exposurePopupOpenFlag = false;

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
            this.Text = (data.projectName + " - ShotLog 1.1.0.2");
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

            //Webslate
            webslateDropdown1.SelectedIndex = data.webslateIndex1;
            webslateDropdown2.SelectedIndex = data.webslateIndex2;
            webslateDropdown3.SelectedIndex = data.webslateIndex3;
            webslateDropdown4.SelectedIndex = data.webslateIndex4;
            webslateDropdown5.SelectedIndex = data.webslateIndex5;

            webslateFontMenu.Text = data.webslateTextsize.ToString();
            webslateUpdateMenu.Text = data.webslateRefresh.ToString();
            webslatePortMenu.Text = data.webslatePort.ToString();

            WebSlate.SetDataobject(data);


            //Update enabled
            UpdateGUIenabled();

            //Update speadsheets
            ReloadDataGridView();


            UpdateFileMenuFormating();


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
                data.restartWebslateServerFlag = true;
                autosaveTimer.Interval = data.autoSaveDuration;
                autosaveTimer.Start();

            }
        }

        private void NewExposure()
        {
            SaveGUItoData();
            exposurePopupOpenFlag = true;


            ExposurePopup expoPopup = new ExposurePopup();
            expoPopup.SetProjectData(data); //Send project data to popup
            expoPopup.ShowDialog(); //Show popup and wait for popup to close

            expoPopup.Dispose();
            exposurePopupOpenFlag = false;
            LoadGUIfromData();

            //data.ClearWebslate();


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

            //Scroll to bottom
            if (dataGridViewVideo.RowCount > 3) { dataGridViewVideo.FirstDisplayedScrollingRowIndex = dataGridViewVideo.RowCount - 1; }
            if (dataGridViewStills.RowCount > 3) { dataGridViewStills.FirstDisplayedScrollingRowIndex = dataGridViewStills.RowCount - 1; }
            
                
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

        private void UpdateFileMenuFormating()
        {
            Font fontRegular = new Font(saveProjectToolStripMenuItem.Font, FontStyle.Regular);
            Font fontBold = new Font(saveProjectToolStripMenuItem.Font, FontStyle.Bold);

            enableAutoSaveButton.Font = (data.autoSave ? fontBold : fontRegular);
            disableAutoSaveButton.Font = (data.autoSave ? fontRegular : fontBold);

            autosave1m.Font = ((data.autoSaveDuration == (1 * 60000) && data.autoSave == true) ? fontBold : fontRegular);
            autosave5m.Font = ((data.autoSaveDuration == (5 * 60000) && data.autoSave == true) ? fontBold : fontRegular);
            autosave10m.Font = ((data.autoSaveDuration == (10 * 60000) && data.autoSave == true) ? fontBold : fontRegular);
            autosave30m.Font = ((data.autoSaveDuration == (30 * 60000) && data.autoSave == true) ? fontBold : fontRegular);
            
            autosaveNewFilesOn.Font = (data.autoSaveWithNewFilename ? fontBold : fontRegular);
            autosaveNewFilesOff.Font = (data.autoSaveWithNewFilename ? fontRegular : fontBold);
        }

        private void DeleteSelectedRow()
        {
            
            //Video    
            if (tabControl1.SelectedIndex == 1)
            {
                try
                {
                    string box_msg = string.Format("Do you want to DELETE this row from the Video list? \n\n\nExposureID: {0} \n\nFilename: {1} \n\nNote 1: {2} \n\nNote 2: {3} \n\nNote 3: {4} \n\nNote 4: {5} \n\nNote 5: {6}", data.VideoList[dataGridViewVideo.CurrentCell.RowIndex].ExposureID, data.VideoList[dataGridViewVideo.CurrentCell.RowIndex].Filename, data.VideoList[dataGridViewVideo.CurrentCell.RowIndex].Notes1, data.VideoList[dataGridViewVideo.CurrentCell.RowIndex].Notes2, data.VideoList[dataGridViewVideo.CurrentCell.RowIndex].Notes3, data.VideoList[dataGridViewVideo.CurrentCell.RowIndex].Notes4, data.VideoList[dataGridViewVideo.CurrentCell.RowIndex].Notes5);
                    string box_title = "Are you sure?";
                    DialogResult result = MessageBox.Show(box_msg, box_title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        data.VideoList.RemoveAt(dataGridViewVideo.CurrentCell.RowIndex);
                        ReloadDataGridView();
                    }
                } catch
                {
                    MessageBox.Show("No row was selected, or there was a problem deleting the row.", "No row selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                
            }

            //Stills    
            else if (tabControl1.SelectedIndex == 2)
            {
                try
                {
                    string box_msg = string.Format("Do you want to DELETE this row from the Stills list? \n\n\nExposureID: {0} \n\nFilename: {1} \n\nNote 1: {2} \n\nNote 2: {3} \n\nNote 3: {4} \n\nNote 4: {5} \n\nNote 5: {6}", data.StillsList[dataGridViewStills.CurrentCell.RowIndex].ExposureID, data.StillsList[dataGridViewStills.CurrentCell.RowIndex].Filename, data.StillsList[dataGridViewStills.CurrentCell.RowIndex].Notes1, data.StillsList[dataGridViewStills.CurrentCell.RowIndex].Notes2, data.StillsList[dataGridViewStills.CurrentCell.RowIndex].Notes3, data.StillsList[dataGridViewStills.CurrentCell.RowIndex].Notes4, data.StillsList[dataGridViewStills.CurrentCell.RowIndex].Notes5);
                    string box_title = "Are you sure?";
                    DialogResult result = MessageBox.Show(box_msg, box_title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        data.StillsList.RemoveAt(dataGridViewStills.CurrentCell.RowIndex);
                        ReloadDataGridView();
                    }
                }
                catch
                {
                    MessageBox.Show("No row was selected, or there was a problem deleting the row.", "No row selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }

        }

        private void OpenInBrowser(string sourceUrl)
        {
            Process.Start(sourceUrl);
        }

        private void SetWebslateSettings()
        {
            data.webslateIndex1 = webslateDropdown1.SelectedIndex;
            data.webslateIndex2 = webslateDropdown2.SelectedIndex;
            data.webslateIndex3 = webslateDropdown3.SelectedIndex;
            data.webslateIndex4 = webslateDropdown4.SelectedIndex;
            data.webslateIndex5 = webslateDropdown5.SelectedIndex;

            try
            {
                if (data.webslatePort != int.Parse(webslatePortMenu.Text)) { data.restartWebslateServerFlag = true; }
                data.webslatePort = int.Parse(webslatePortMenu.Text);
            } catch
            {
                MessageBox.Show(("Error parsing " + webslatePortMenu.Text + " as a webserver port number. Setting port to default 5588..."), "Error parsing settings");
                if (data.webslatePort != 5588) { data.restartWebslateServerFlag = true; }
                data.webslatePort = 5588;
            }

            try
            {
                data.webslateRefresh = int.Parse(webslateUpdateMenu.Text);
            }
            catch
            {
                MessageBox.Show(("Error parsing " + webslateUpdateMenu.Text + " as a website refresh speed in seconds. Setting refresh speed to default 5..."), "Error parsing settings");
                data.webslateRefresh = 5;
            }

            try
            {
                data.webslateTextsize = int.Parse(webslateFontMenu.Text);
            }
            catch
            {
                MessageBox.Show(("Error parsing " + webslateFontMenu.Text + " as a fontsize. Setting textsize  to default 6..."), "Error parsing settings");
                data.webslateTextsize = 6;
            }




            
        }

        

        private void SaveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveProject();
            LoadGUIfromData();
        }

        private void OpenProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProject();
            UpdateFileMenuFormating();
        }

        private void ApplySettingsButton_Click(object sender, EventArgs e)
        {
            SaveGUItoData();
            LoadGUIfromData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadGUIfromData();
            StartWebslate();
        }

        private void StartWebslate()
        {
            if (!IsRunAsAdmin())
            {
                //MessageBox.Show("ShotLog requires to RUN AS ADMINISTRATOR for the WebSlate feature to be used. To start the WebSlate-server, quit the application and relaunch as administrator.", "Administrator rights required", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                ProcessStartInfo elevated = new ProcessStartInfo(System.Reflection.Assembly.GetEntryAssembly().Location, "run2")
                {
                    UseShellExecute = true,
                    Verb = "runas"
                };
                Process.Start(elevated);
                System.Environment.Exit(0);

            } else
            {
                WebSlate.SetDataobject(data);
                Task.Run(() => WebSlate.RunServer());
            }
        }

        private bool IsRunAsAdmin()
        {
            try
            {
                WindowsIdentity id = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(id);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (Exception)
            {
                return false;
            }
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
            UpdateFileMenuFormating();
        }

        private void ExportStillsLogButton_Click(object sender, EventArgs e)
        {
            ExportCSV.ExportStills(data.StillsList);
            UpdateFileMenuFormating();
        }

        private void EnableAutoSaveButton_Click(object sender, EventArgs e)
        {
            data.autoSave = true;
            UpdateFileMenuFormating();
        }

        private void DisableAutoSaveButton_Click(object sender, EventArgs e)
        {
            data.autoSave = false;
            UpdateFileMenuFormating();
        }

        private void Autosave1m_Click(object sender, EventArgs e)
        {
            UpdateAutosaveTimer(1);
            UpdateFileMenuFormating();

        }

        private void Autosave5m_Click(object sender, EventArgs e)
        {
            UpdateAutosaveTimer(5);
            UpdateFileMenuFormating();
        }

        private void Autosave10m_Click(object sender, EventArgs e)
        {
            UpdateAutosaveTimer(10);
            UpdateFileMenuFormating();
        }

        private void Autosave30m_Click(object sender, EventArgs e)
        {
            UpdateAutosaveTimer(30);
            UpdateFileMenuFormating();
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If user (not system!) wants to close the form
            // but (s)he answered "no", do not close the form
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = MessageBox.Show(@"Are you sure you want to close ShotLog? Any unsaved changes will be lost!",
                                           Application.ProductName,
                                           MessageBoxButtons.OKCancel) == DialogResult.Cancel;
        }

        private void AutosaveNewFilesOn_Click(object sender, EventArgs e)
        {
            data.autoSaveWithNewFilename = true;
            UpdateFileMenuFormating();
        }

        private void AutosaveNewFilesOff_Click(object sender, EventArgs e)
        {
            data.autoSaveWithNewFilename = false;
            UpdateFileMenuFormating();
        }

        private void DeleteRowButton_Click(object sender, EventArgs e)
        {
            DeleteSelectedRow();
        }

        private void GithubButton_Click(object sender, EventArgs e)
        {
            OpenInBrowser("https://github.com/emanueltilly/shotlog");
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ShotLog\n\nVersion 1.0.0.0\n\nLicensed under the Apache 2.0 license.", "ShotLog", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void WebslateTimer_Tick(object sender, EventArgs e)
        {
            
            if (exposurePopupOpenFlag != true)
            {
                data.UpdateWebslateMain();
            }
            
        }

        private void ApplyWebSlateSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetWebslateSettings();
        }

        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            OpenInBrowser(("http://localhost:" + data.webslatePort));
        }
    }
}
