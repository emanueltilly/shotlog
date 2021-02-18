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
    [XmlRoot("StillItem")]
    public class StillItem
    {
        //Timestamp
        public string ExposureID { get; set; } = "";
        public string Timestamp { get; set; } = "";

        //Stills 1
        public string Filename { get; set; } = "Unknown filename";

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




        //Stills 2


        public string Camera { get; set; } = "";

        public int ISO { get; set; } = 0;
        public double Aperture { get; set; } = 0;
        public int Shutterspeed { get; set; } = 0;
        public int Whitebalance { get; set; } = 0;
        public int Focallength { get; set; } = 0;

        public bool Bracketed { get; set; } = false;
        public string Exposure { get; set; } = "";


    }
}
