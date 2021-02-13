using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace ShotLog
{
    public class ProjectData
    {
        //Project setting values here

        //Project wide
        public string projectName = "Blank Project";

        public bool videoEnabled = false;
        public bool stillsEnabled = false;
        public bool photometricDataEnabled = false;

        public string notesField1 = "";
        public string notesField2 = "";
        public string notesField3 = "";
        public string notesField4 = "";
        public string notesField5 = "";


        //Video settings
        public bool videoUseSceneNumbering = false;
        public bool videoUseStudioCamSettings = false;

        public string videoPrefix = "VID_";
        public int videoIndexLength = 4;
        public int videoNextIndex = 1;

        public int videoScene = 1;
        public int videoShot = 1;
        public int videoTake = 1;

        public string videoCameraName = "Arri Alexa";

        public string videoFormat = "1080p50";
        public double videoAperture = 4.0;
        public int videoFocalLength = 50;
        public int videoWhiteBalance = 5600;

        public int videoISO = 100;
        public int videoShutterspeed = 50;

        public int videoStudioMasterBlack = 50;
        public int videoStudioTint = 0;
        public int videoStudioSaturation = 100;
        public int videoStudioGain = 0;
        public string videoStudioFilter = "Clear";

        public int videoStudioRedGain = 50;
        public int videoStudioGreenGain = 50;
        public int videoStudioBlueGain = 50;
        public int videoStudioRedBlack = 50;
        public int videoStudioGreenBlack = 50;
        public int videoStudioBlueBlack = 50;





        //Stills settings
        public string stillsPrefix = "IMG_";
        public int stillsIndexLength = 4;
        public int stillsNextIndex = 1;

        public string stillsCameraName = "Canon 1D";

        public int stillsISO = 100;
        public double stillsAperture = 4.0;
        public int stillsShutterspeedBase = 1000;
        public int stillsWhiteBalance = 5600;
        public int stillsFocalLength = 50;

        public bool stillsBracketed = false;
        public int stillsBracketSteps = 1;
        public string stillsBracketOrder = "0+-";



        //Photometric data
        public int photometricsCRI = 90;
        public int photometricsLUX = 1000;
        public int photometricsLUXdistance = 10;
        public int photometricsKelvin = 5600;
        public int photometricsZoom = 100;
        public int photometricDimmer = 100;
        public string photometricsFixtureType = "Arri D12";



        public void SaveToFile(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                XmlSerializer XML = new XmlSerializer(typeof(ProjectData));
                XML.Serialize(stream, this);
            }
        }

        public static ProjectData LoadFromFile(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                XmlSerializer XML = new XmlSerializer(typeof(ProjectData));
                return (ProjectData)XML.Deserialize(stream);
            }
        }




    }
}