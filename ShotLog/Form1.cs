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
        }

        //UPDATE GUI FROM DATA OBJECT
        private void loadGUIfromData()
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
            stillsShutterSpeed.Value = data.videoShutterspeed;

            stillsBrackeringEnabled.Checked = data.stillsBracketed;

            stillsBrackeringStops.Value = data.stillsBracketSteps;
            stillsBracketingOrder.Text = data.stillsBracketOrder;

            //Video Settings
            videoUseSceneShotTake.Checked = data.videoUseSceneNumbering;
            videoUseBroadcast.Checked = data.videoUseStudioCamSettings;

            videoNamePrefix.Text = data.videoPrefix;
            videoIndexLength.Value = data.videoIndexLength;
            videoNextFileIndex.Value = data.videoIndexLength;
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


        }

        private void saveGUItoData()
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

        //SAVE & OPEN PROJECT FUNCTIONS
        private void saveProject()
        {
            saveGUItoData();

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
                data.SaveToFile(SaveFileDialog1.FileName);

                loadGUIfromData();
            }
        }

        private void openProject()
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
                loadGUIfromData();

            }
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveProject();
            loadGUIfromData();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openProject();
        }

        private void applySettingsButton_Click(object sender, EventArgs e)
        {
            saveGUItoData();
            loadGUIfromData();
        }
    }
}
