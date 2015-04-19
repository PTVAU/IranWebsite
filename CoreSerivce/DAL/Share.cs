using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CoreSerivce.DAL
{
    public class Share
    {
        public static List<BO.Weather> FrontendWeatherList()
        {
            var WeatherList = new List<BO.Weather>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select * from weather order by priority";

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());
            
            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {
                var WeatherObj = new BO.Weather();

                WeatherObj.id = Dr["Id"].ToString();
                WeatherObj.name = Dr["CityName"].ToString();
                WeatherObj.temp = Dr["TmpCurrent"].ToString();
                WeatherObj.cssClass = Dr["statusicon"].ToString();

                WeatherList.Add(WeatherObj);
            }
          
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();
          
            return WeatherList;
        }
    }
}