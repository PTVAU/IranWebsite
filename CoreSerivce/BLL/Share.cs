using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreSerivce.BLL
{
    public class Share
    {
        public static List<BO.Weather> FrontendWeatherList()
        {
            var WeatherList = DAL.Share.FrontendWeatherList();
            foreach (BO.Weather item in WeatherList)
            {
                switch (item.cssClass.Trim())
                {
                    case "01d":
                        item.cssClass = "sun";
                        break;
                    case "01n":
                        item.cssClass = "moon";
                        break;
                    case "02d":
                        item.cssClass = "cloud-sun";
                        break;
                    case "02n":
                        item.cssClass = "cloud-moon";
                        break;
                    case "03d":
                        item.cssClass = "cloud";
                        break;
                    case "03n":
                        item.cssClass = "cloud";
                        break;
                    case "04d":
                        item.cssClass = "clouds";
                        break;
                    case "04n":
                        item.cssClass = "clouds";
                        break;
                    case "09d":
                        item.cssClass = "rain";
                        break;
                    case "09n":
                        item.cssClass = "rain";
                        break;
                    case "10n":
                        item.cssClass = "rain";
                        break;
                    case "10d":
                        item.cssClass = "rain";
                        break;
                    case "11d":
                        item.cssClass = "cloud-flash";
                        break;
                    case "11n":
                        item.cssClass = "cloud-flash";
                        break;
                    case "13d":
                        item.cssClass = "snow-heavy";
                        break;
                    case "13n":
                        item.cssClass = "snow-heavy";
                        break;
                    case "50n":
                        item.cssClass = "fog";
                        break;
                    case "50d":
                        item.cssClass = "fog";
                        break;
                    default:
                        item.cssClass = "sun";
                        break;
                }

                double temp = 0;
                double.TryParse(item.temp, out temp);
                temp = temp - (273.15);
                item.temp = Math.Round(temp).ToString();
            }
            return WeatherList;
        }
    }
}