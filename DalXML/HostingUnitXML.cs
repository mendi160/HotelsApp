using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DO;

namespace Dal
{
    public class HostingUnitXML
    {

        public int Key { get; set; }
        public int Owner { get; set; }
        public int PricePerNight { get; set; }
        public string HostingUnitName { get; set; }
        public Location Area { get; set; }
        [XmlIgnore]
        public bool[,] Diary { get; set; }
        public Status Status { get; set; }
        public bool WIFI { get; set; }
        public bool SwimmingPool { get; set; }
        public bool Kitchen { get; set; }
        public bool TV { get; set; }
        public bool Jacuzzi { get; set; }
        public string ImageLink1 { get; set; }
        public string ImageLink2 { get; set; }
        public string ImageLink3 { get; set; }
        [XmlArray("Calendar")]
        public bool[] Diary_XML
        {
            get { return Diary.Flatten(); }
            set { Diary = value.Expand(12); }
        }
    }
    public static class Tools
    {
        public static HostingUnit ConvertToDO(this HostingUnitXML hostingUnitXML)
        {
            HostingUnit hostingUnit = new HostingUnit()
            {
                Diary = hostingUnitXML.Diary,
                HostingUnitName = hostingUnitXML.HostingUnitName,
                Key = hostingUnitXML.Key,
                Owner = hostingUnitXML.Owner,
                Status = (DO.Status)hostingUnitXML.Status,
                Area = (DO.Location)hostingUnitXML.Area,
                ImageLink1 = hostingUnitXML.ImageLink1,
                ImageLink2 = hostingUnitXML.ImageLink2,
                ImageLink3 = hostingUnitXML.ImageLink3,
                Jacuzzi = hostingUnitXML.Jacuzzi,
                Kitchen = hostingUnitXML.Kitchen,
                SwimmingPool = hostingUnitXML.SwimmingPool,
                TV = hostingUnitXML.TV,
                WIFI = hostingUnitXML.WIFI,
                PricePerNight = hostingUnitXML.PricePerNight
            };
            return hostingUnit;

        }
        public static HostingUnitXML ConvertToHostingUnitXML(this HostingUnit hostingUnit )
        {
            HostingUnitXML hostingUnitXML = new HostingUnitXML()
            {
               
                HostingUnitName = hostingUnit.HostingUnitName,
                Key = hostingUnit.Key,
                Owner = hostingUnit.Owner,
                Status = (DO.Status)hostingUnit.Status,
                Area = (DO.Location)hostingUnit.Area,
                ImageLink1 = hostingUnit.ImageLink1,
                ImageLink2 = hostingUnit.ImageLink2,
                ImageLink3 = hostingUnit.ImageLink3,
                Jacuzzi = hostingUnit.Jacuzzi,
                Kitchen = hostingUnit.Kitchen,
                SwimmingPool = hostingUnit.SwimmingPool,
                TV = hostingUnit.TV,
                WIFI = hostingUnit.WIFI,
                PricePerNight = hostingUnit.PricePerNight,
                Diary = hostingUnit.Diary
            };
            return hostingUnitXML;
        }
        public static T[] Flatten<T>(this T[,] arr)
        {
            int rows = arr.GetLength(0);
            int columns = arr.GetLength(1);
            T[] arrFlattened = new T[rows * columns];
            for (int j = 0; j < rows; j++)
                for (int i = 0; i < columns; i++)
                {

                    arrFlattened[i + j * columns] = arr[j, i];
                }
            return arrFlattened;
        }
        public static T[,] Expand<T>(this T[] arr, int rows)
        {
            int length = arr.GetLength(0);
            int columns = length / rows;
            T[,] arrExpanded = new T[rows, columns];
            for (int j = 0; j < rows; j++)
                for (int i = 0; i < columns; i++)
                    arrExpanded[j, i] = arr[i + j * columns];
            return arrExpanded;
        }
    }
}

