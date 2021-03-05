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
    public partial class ExposurePopup : Form
    {
        ProjectData data = new ProjectData(); 

        public void SetProjectData(ProjectData source)
        {
            data = source;
        }

        public ExposurePopup()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(KeyEvent);
        }

        private void KeyEvent(object sender, KeyEventArgs e) //Keyup Event 
        {
            if (e.KeyCode == Keys.Enter)
            {
                Commit();
            }
            if (e.KeyCode == Keys.F3)
            {
                ToggleVideoEnabled();
            }
            if (e.KeyCode == Keys.F4)
            {
                ToggleStillsEnabled();
            }

        }

        private void ExposurePopup_Load(object sender, EventArgs e)
        {
            notesBox1.Text = data.notesField1;
            notesBox2.Text = data.notesField2;
            notesBox3.Text = data.notesField3;
            notesBox4.Text = data.notesField4;
            notesBox5.Text = data.notesField5;
            UpdateButtonsText();

            fixtureBox.Text = data.photometricsFixtureType;
            luxBox.Value = data.photometricsLUX;
            distanceBox.Value = data.photometricsLUXdistance;
            kelvinBox.Value = data.photometricsKelvin;
            criBox.Value = data.photometricsCRI;
            zoomBox.Value = data.photometricsZoom;
            dimmerBox.Value = data.photometricDimmer;

            groupBox1.Enabled = (data.photometricDataEnabled);

            videoPreview1.Text = data.GetFilename(true, false, 0);
            stillPreview1.Text = data.GetFilename(false, true, 0);
            stillPreview2.Text = data.GetFilename(false, true, 1);
            stillPreview3.Text = data.GetFilename(false, true, 2);




        }

        private void UpdateButtonsText()
        {
            videoEnabledButton.Text = (data.videoEnabled ? "VIDEO ENABLED (F3)" : "VIDEO DISABLED (F3)");
            stillsEnabledButton.Text = (data.stillsEnabled ? "STILLS ENABLED (F4)" : "STILLS DISABLED (F4)");

            videoPreview1.Visible = (data.videoEnabled);
            stillPreview1.Visible = (data.stillsEnabled);
            stillPreview2.Visible = (data.stillsEnabled && data.stillsBracketed);
            stillPreview3.Visible = (data.stillsEnabled && data.stillsBracketed);
        }

        private void VideoEnabledButton_Click(object sender, EventArgs e)
        {
            ToggleVideoEnabled();
        }

        private void ToggleVideoEnabled()
        {
            data.videoEnabled = (data.videoEnabled ? false : true);
            UpdateButtonsText();
        }

        private void StillsEnabledButton_Click(object sender, EventArgs e)
        {
            ToggleStillsEnabled();
        }

        private void ToggleStillsEnabled()
        {
            data.stillsEnabled = (data.stillsEnabled ? false : true);
            UpdateButtonsText();
        }

        private void CommitShotButton_Click(object sender, EventArgs e)
        {
            Commit();
        }

        private void Commit()
        {
            bool commitOK = (data.CommitToList(
                notesBox1.Text,
                notesBox2.Text,
                notesBox3.Text,
                notesBox4.Text,
                notesBox5.Text,
                data.videoEnabled,
                data.stillsEnabled,
                fixtureBox.Text,
                (int)luxBox.Value,
                (int)distanceBox.Value,
                (int)kelvinBox.Value,
                (int)criBox.Value,
                (int)zoomBox.Value,
                (int)dimmerBox.Value,
                (int)data.stillsShutterspeedBase
                ));

            if (commitOK != true)
            {
                MessageBox.Show("A problem occured when commiting the shot to the list. Please check the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.Close();
            }
        }

        private void SaveWithoutCommit()
        {
            bool commitOK = (data.SaveWithoutCommit(
                notesBox1.Text,
                notesBox2.Text,
                notesBox3.Text,
                notesBox4.Text,
                notesBox5.Text,
                data.videoEnabled,
                data.stillsEnabled,
                fixtureBox.Text,
                (int)luxBox.Value,
                (int)distanceBox.Value,
                (int)kelvinBox.Value,
                (int)criBox.Value,
                (int)zoomBox.Value,
                (int)dimmerBox.Value,
                (int)data.stillsShutterspeedBase
                ));

            if (commitOK != true)
            {
                MessageBox.Show("A problem occured when saving the data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.Close();
            }
        }

        private void WebslateTimer_Tick(object sender, EventArgs e)
        {
            data.UpdateWebslateExposurePopup(notesBox1.Text, notesBox2.Text, notesBox3.Text, notesBox4.Text, notesBox5.Text, fixtureBox.Text, (int)dimmerBox.Value, (int)zoomBox.Value);
        }

        private void saveNoCommitButton_Click(object sender, EventArgs e)
        {
            SaveWithoutCommit();
        }
    }
}
