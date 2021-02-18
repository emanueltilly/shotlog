﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace ShotLog
{
    public class ProjectData
    {
        //Lists
        public List<StillItem> StillsList = new List<StillItem>();
        public List<VideoItem> VideoList = new List<VideoItem>();

        //Project setting values here

        //Project wide
        public string savePath = "";
        public bool autoSave = false;
        public int autoSaveDuration = 60000;

        public int exposureIdCounter = 0;
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



        public bool SaveToFile(string fileName)
        {
            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Create))
                {
                    XmlSerializer XML = new XmlSerializer(typeof(ProjectData));
                    XML.Serialize(stream, this);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AutoSaveToFile()
        {
            if (savePath != "")
            {
                try
                {
                    using (FileStream stream = new FileStream(savePath, FileMode.Create))
                    {
                        XmlSerializer XML = new XmlSerializer(typeof(ProjectData));
                        XML.Serialize(stream, this);
                    }
                    return true;

                } catch
                {
                    autoSave = false;
                    MessageBox.Show("Error Auto-saving to filepath: " + savePath + " Please save file in new location to enable auto-saving. AUTO SAVE WILL BE DISABLED UNTIL MANUAL RE-ACTIVATION!");
                    savePath = "";
                    return false;
                }
            } return false;
           
        }

        public static ProjectData LoadFromFile(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                XmlSerializer XML = new XmlSerializer(typeof(ProjectData));
                return (ProjectData)XML.Deserialize(stream);
            }
        }

        public string GetFilename(bool video, bool still, int offset)
        {
            string filename = "";
            if (still == true && video == false)
            {
                filename = (stillsPrefix + IntToStringWithPadding((stillsNextIndex + offset), stillsIndexLength));
                return filename;
            }
            else if (still == false && video == true)
            {
                if (videoUseSceneNumbering)
                {
                    filename = (videoPrefix + "_Scene-" + videoScene + "_Shot-" + videoShot + "_Take-" + videoTake);
                    return filename;

                } else
                {
                    filename = (videoPrefix + IntToStringWithPadding((videoNextIndex + offset), videoIndexLength));
                    return filename;
                }
            }else
            {
                Console.WriteLine(filename);
                return "Unknown filename";
            }
            
        }

        private string IntToStringWithPadding(int number, int length)
        {
            return (number.ToString("D" + length));
        }

        public bool CommitToList(
            string note1,
            string note2,
            string note3,
            string note4,
            string note5,
            bool videoCommit,
            bool stillsCommit,
            string photoFixture,
            int photoLUX,
            int photoLUXdistance,
            int photoKelvin,
            int photoCRI,
            int photoZoom,
            int photoDimmer,
            int stillShutterspeed

            )
        {
            //Update data
            videoEnabled = videoCommit;
            stillsEnabled = stillsCommit;

            notesField1 = note1;
            notesField2 = note2;
            notesField3 = note3;
            notesField4 = note4;
            notesField5 = note5;

            photometricsFixtureType = photoFixture;
            photometricsLUX = photoLUX;
            photometricsLUXdistance = photoLUXdistance;
            photometricsKelvin = photoKelvin;
            photometricsCRI = photoCRI;
            photometricsZoom = photoZoom;
            photometricDimmer = photoDimmer;

            //Get global exposure ID
            string expID = GetExposureID();

            if (videoEnabled)
            {
                VideoItem newVideo = new VideoItem
                {
                    ExposureID = expID,
                    Timestamp = DateTime.Now.ToString("G"),
                    Filename = GetFilename(true, false, 0),

                    Scene = videoScene,
                    Shot = videoShot,
                    Take = videoTake,

                    Notes1 = note1,
                    Notes2 = note2,
                    Notes3 = note3,
                    Notes4 = note4,
                    Notes5 = note5,

                    Photometrics = photometricDataEnabled,
                    Fixture = photometricsFixtureType,
                    Distance = photometricsLUXdistance,
                    LUX = photometricsLUX,
                    Kelvin = photometricsKelvin,
                    CRI = photometricsCRI,
                    Zoom = photometricsZoom,
                    Dimmer = photometricDimmer,

                    Camera = videoCameraName,
                    Format = videoFormat,
                    Aperture = videoAperture,
                    Focallength = videoFocalLength,
                    Whitebalance = videoWhiteBalance,
                    ISO = videoISO,
                    Shutterspeed = videoShutterspeed,
                    MasterBlack = videoStudioMasterBlack,
                    Tint = videoStudioTint,
                    Saturation = videoStudioSaturation,
                    Gain = videoStudioGain,

                    Gain_Red = videoStudioRedGain,
                    Gain_Green = videoStudioGreenGain,
                    Gain_Blue = videoStudioBlueGain,
                    Black_Red = videoStudioRedBlack,
                    Black_Green = videoStudioGreenBlack,
                    Black_Blue = videoStudioBlueBlack
                };



                VideoList.Add(newVideo);

                //INCREMENT COUNTERS
                if (videoUseSceneNumbering)
                {
                    videoTake++;
                }
                else
                {
                    videoNextIndex++;
                }
                
                

            }

            if (stillsEnabled)
            {
                if (stillsBracketed == false) {
                    Console.WriteLine("Appending single exposure...");
                    StillItem newStill = new StillItem
                    {
                        ExposureID = expID,
                        Timestamp = DateTime.Now.ToString("G"),
                        Filename = GetFilename(false, true, 0),

                        Notes1 = note1,
                        Notes2 = note2,
                        Notes3 = note3,
                        Notes4 = note4,
                        Notes5 = note5,

                        Photometrics = photometricDataEnabled,
                        Fixture = photometricsFixtureType,
                        Distance = photometricsLUXdistance,
                        LUX = photometricsLUX,
                        Kelvin = photometricsKelvin,
                        CRI = photometricsCRI,
                        Zoom = photometricsZoom,
                        Dimmer = photometricDimmer,

                        Camera = stillsCameraName,
                        ISO = stillsISO,
                        Aperture = stillsAperture,
                        Shutterspeed = stillsShutterspeedBase,
                        Whitebalance = stillsWhiteBalance,
                        Focallength = stillsFocalLength,
                        Bracketed = stillsBracketed
                    };

                    StillsList.Add(newStill);

                    //INCREMENT COUNTERS
                    stillsNextIndex++;
                } else
                {
                    Console.WriteLine("Appending three bracketed exposures...");
                    string[] exposureNames = new string[3];
                    int[] exposureSteps = new int[3];

                    switch (stillsBracketOrder)
                    {
                        //0 + -
                        //0 - +
                        //-0 +
                        //+0 -
                        case "0+-":
                            Console.WriteLine("Bracket order: " + stillsBracketOrder);
                            exposureNames[0] = "Normal";
                            exposureNames[1] = (stillsBracketSteps.ToString() + " stop over");
                            exposureNames[2] = (stillsBracketSteps.ToString() + " stop under");

                            exposureSteps[0] = stillsShutterspeedBase;
                            exposureSteps[2] = (stillsShutterspeedBase * (stillsBracketSteps * 2));
                            exposureSteps[1] = (RecursiveDivision(stillsShutterspeedBase, stillsBracketSteps));
                             break;

                        case "0-+":
                            Console.WriteLine("Bracket order: " + stillsBracketOrder);
                            exposureNames[0] = "Normal";
                            exposureNames[1] = (stillsBracketSteps.ToString() + " stop over");
                            exposureNames[2] = (stillsBracketSteps.ToString() + " stop under");

                            exposureSteps[0] = stillsShutterspeedBase;
                            exposureSteps[2] = (stillsShutterspeedBase * (stillsBracketSteps * 2));
                            exposureSteps[1] = (RecursiveDivision(stillsShutterspeedBase, stillsBracketSteps));
                            break;

                        case "-0+":
                            Console.WriteLine("Bracket order: " + stillsBracketOrder);
                            exposureNames[1] = "Normal";
                            exposureNames[2] = (stillsBracketSteps.ToString() + " stop over");
                            exposureNames[0] = (stillsBracketSteps.ToString() + " stop under");

                            exposureSteps[1] = stillsShutterspeedBase;
                            exposureSteps[0] = (stillsShutterspeedBase * (stillsBracketSteps * 2));
                            exposureSteps[2] = (RecursiveDivision(stillsShutterspeedBase, stillsBracketSteps));
                            break;

                        case "+0-":
                            Console.WriteLine("Bracket order: " + stillsBracketOrder);
                            exposureNames[1] = "Normal";
                            exposureNames[0] = (stillsBracketSteps.ToString() + " stop over");
                            exposureNames[2] = (stillsBracketSteps.ToString() + " stop under");

                            exposureSteps[1] = stillsShutterspeedBase;
                            exposureSteps[2] = (stillsShutterspeedBase * (stillsBracketSteps * 2));
                            exposureSteps[0] = (RecursiveDivision(stillsShutterspeedBase, stillsBracketSteps));
                            break;
                    }
                    StillItem newStill1 = new StillItem
                    {
                        ExposureID = expID,
                        Timestamp = DateTime.Now.ToString("G"),
                        Filename = GetFilename(false, true, 0),

                        Notes1 = note1,
                        Notes2 = note2,
                        Notes3 = note3,
                        Notes4 = note4,
                        Notes5 = note5,

                        Photometrics = photometricDataEnabled,
                        Fixture = photometricsFixtureType,
                        Distance = photometricsLUXdistance,
                        LUX = photometricsLUX,
                        Kelvin = photometricsKelvin,
                        CRI = photometricsCRI,
                        Zoom = photometricsZoom,
                        Dimmer = photometricDimmer,

                        Camera = stillsCameraName,
                        ISO = stillsISO,
                        Aperture = stillsAperture,
                        Shutterspeed = exposureSteps[0],
                        Whitebalance = stillsWhiteBalance,
                        Focallength = stillsFocalLength,
                        Bracketed = stillsBracketed,
                        Exposure = exposureNames[0]
                        
                    };
                    //INCREMENT COUNTERS
                    stillsNextIndex++;

                    StillItem newStill2 = new StillItem
                    {
                        ExposureID = expID,
                        Timestamp = DateTime.Now.ToString("G"),
                        Filename = GetFilename(false, true, 0),

                        Notes1 = note1,
                        Notes2 = note2,
                        Notes3 = note3,
                        Notes4 = note4,
                        Notes5 = note5,

                        Photometrics = photometricDataEnabled,
                        Fixture = photometricsFixtureType,
                        Distance = photometricsLUXdistance,
                        LUX = photometricsLUX,
                        Kelvin = photometricsKelvin,
                        CRI = photometricsCRI,
                        Zoom = photometricsZoom,
                        Dimmer = photometricDimmer,

                        Camera = stillsCameraName,
                        ISO = stillsISO,
                        Aperture = stillsAperture,
                        Shutterspeed = exposureSteps[1],
                        Whitebalance = stillsWhiteBalance,
                        Focallength = stillsFocalLength,
                        Bracketed = stillsBracketed,
                        Exposure = exposureNames[1]
                    };
                    //INCREMENT COUNTERS
                    stillsNextIndex++;

                    StillItem newStill3 = new StillItem
                    {
                        ExposureID = expID,
                        Timestamp = DateTime.Now.ToString("G"),
                        Filename = GetFilename(false, true, 0),

                        Notes1 = note1,
                        Notes2 = note2,
                        Notes3 = note3,
                        Notes4 = note4,
                        Notes5 = note5,

                        Photometrics = photometricDataEnabled,
                        Fixture = photometricsFixtureType,
                        Distance = photometricsLUXdistance,
                        LUX = photometricsLUX,
                        Kelvin = photometricsKelvin,
                        CRI = photometricsCRI,
                        Zoom = photometricsZoom,
                        Dimmer = photometricDimmer,

                        Camera = stillsCameraName,
                        ISO = stillsISO,
                        Aperture = stillsAperture,
                        Shutterspeed = exposureSteps[2],
                        Whitebalance = stillsWhiteBalance,
                        Focallength = stillsFocalLength,
                        Bracketed = stillsBracketed,
                        Exposure = exposureNames[2]
                    };
                    //INCREMENT COUNTERS
                    stillsNextIndex++;

                    StillsList.Add(newStill1);
                    StillsList.Add(newStill2);
                    StillsList.Add(newStill3);



                }
                

            }

            //Return OK
            return true;

        }

        private int RecursiveDivision(int sourceNumber, int divideXtimes)
        {
            int counter = 0;
            double workValue = sourceNumber;
            while (counter < divideXtimes)
            {
                workValue = (workValue * 0.5);
                counter++;
            }
            return (int)workValue;
        }

        public string GetExposureID()
        {
            exposureIdCounter++;
            return (projectName + "_" + exposureIdCounter);
        }





    }
}