using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace ShotLog
{
    [XmlRoot("VideoItem")]
    public class VideoItem
    {
        //Timestamp
        public string ExposureID { get; set; } = "";
        public string Timestamp { get; set; } = "";

        //Video 1

        public string Filename { get; set; } = "";

        public int Scene { get; set; } = 1;
        public int Shot { get; set; } = 1;
        public int Take { get; set; } = 1;

        //Notes
        public string Notes1 { get; set; } = "";
        public string Notes2 { get; set; } = "";
        public string Notes3 { get; set; } = "";
        public string Notes4 { get; set; } = "";
        public string Notes5 { get; set; } = "";

        //Photometric data
        public bool Photometrics { get; set; } = false;
        public string Fixture { get; set; } = "";
        public int Distance { get; set; } = 0;
        public int LUX { get; set; } = 0;
        public int Kelvin { get; set; } = 0;
        public int CRI { get; set; } = 0;
        public int Zoom { get; set; } = 0;
        public int Dimmer { get; set; } = 0;
        

        
        //Video 2
        public string Camera { get; set; } = "";

        public string Format { get; set; } = "";
        public double Aperture { get; set; } = 0;
        public int Focallength { get; set; } = 0;
        public int Whitebalance { get; set; } = 0;

        public int ISO { get; set; } = 0;
        public int Shutterspeed { get; set; } = 0;


        public int MasterBlack { get; set; } = 0;
        public int Tint { get; set; } = 0;
        public int Saturation { get; set; } = 0;
        public int Gain { get; set; } = 0;
        public string Filter { get; set; } = "";

        public int Gain_Red { get; set; } = 0;
        public int Gain_Green { get; set; } = 0;
        public int Gain_Blue { get; set; } = 0;
        public int Black_Red { get; set; } = 0;
        public int Black_Green { get; set; } = 0;
        public int Black_Blue { get; set; } = 0;

    }
}
