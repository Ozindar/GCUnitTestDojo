using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Warehouse.Models
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class CurrentWeather
    {
        public string Location { get; set; }
        public string Time { get; set; }
        public string Wind { get; set; }
        public string Visibility { get; set; }
        //  41 F (5 C)
        public string Temperature { get; set; }
        public string DewPoint { get; set; }
        public string RelativeHumidity { get; set; }
        public string Pressure { get; set; }
        public string Status { get; set; }

        public double TemperatureCelcius()
        {
            double temparature = 0;

            int firstBracket = Temperature.IndexOf("(", StringComparison.Ordinal);
            int secondBracket = Temperature.IndexOf(")", StringComparison.Ordinal);
            string temp = Temperature.Substring(firstBracket, secondBracket - firstBracket);
            
            return temparature;
        }

        public static CurrentWeather ParseXml(string s)
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof (CurrentWeather));
            System.IO.TextReader file = new StringReader(s);
            CurrentWeather currentWeather = (CurrentWeather) reader.Deserialize(file);
            file.Close();
            return currentWeather;
        }

    }

}
