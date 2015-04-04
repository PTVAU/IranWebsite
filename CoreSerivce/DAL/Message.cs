using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;

namespace CoreSerivce.DAL
{
    public class Message
    {
        public static List<BO.Message> SelectAllInboxForCurrentUser(int UserId, string Limit)
        {
            int Lmt = 100;
            int.TryParse(Limit, out Lmt);
            if(Lmt==0)
            {
                Lmt = 100;
            }

            var MsgList = new List<BO.Message>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select TOP(" + Lmt + ") * from Message where MessageToId=" + UserId + " Order by MessageSendDate desc";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            sqlCommand.Connection.Open();
            var Dr = sqlCommand.ExecuteReader();
            while (Dr.Read())
            {
                var Msg = new BO.Message();
                Msg.MessageBody = Dr["MessageBody"].ToString();
                Msg.MessageFromId = int.Parse(Dr["MessageFromId"].ToString());
                Msg.MessageId = int.Parse(Dr["MessageId"].ToString());
                Msg.MessageSendDate = Dr["MessageSendDate"].ToString();
                Msg.MessageToId = int.Parse(Dr["MessageToId"].ToString());
                Msg.MessageSeenDate = Dr["MessageSeenDate"].ToString();

                MsgList.Add(Msg);
            }

            sqlCommand.Connection.Close();
            sqlCommand.Dispose();

            return MsgList;
        }
        public static List<BO.Message> SelectAllSentForCurrentUser(int UserId)
        {
            var MsgList = new List<BO.Message>();

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select TOP(200) * from Message where MessageFromId=" + UserId + " Order by MessageSendDate desc";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                var Dr = sqlCommand.ExecuteReader();
                while (Dr.Read())
                {
                    var Msg = new BO.Message();
                    Msg.MessageBody = Dr["MessageBody"].ToString();
                    Msg.MessageFromId = int.Parse(Dr["MessageFromId"].ToString());
                    Msg.MessageId = int.Parse(Dr["MessageId"].ToString());
                    Msg.MessageSendDate = Dr["MessageSendDate"].ToString();
                    Msg.MessageToId = int.Parse(Dr["MessageToId"].ToString());
                    Msg.MessageSeenDate = Dr["MessageSeenDate"].ToString();

                    MsgList.Add(Msg);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Dispose();
            }
            return MsgList;
        }
        public static BO.Message Insert(BO.Message Msg)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"insert into message (MessageBody,MessageFromId,MessageToId) values(N'" + Msg.MessageBody + "'," + Msg.MessageFromId + "," + Msg.MessageToId + ") select @@IDENTITY ";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            try
            {
                sqlCommand.Connection.Open();
                Msg.MessageId = int.Parse(sqlCommand.ExecuteScalar().ToString());
                Msg.MessageSendDate = DateTime.Now.ToString();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Dispose();
            }
            return Msg;
        }
        public static int SelectAllInboxForCurrentUser(int UserId)
        {
            var RetCount = 0;


            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select count(*) from Message where MessageToId=" + UserId;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());

            sqlCommand.Connection.Open();
            RetCount = int.Parse(sqlCommand.ExecuteScalar().ToString());

            sqlCommand.Connection.Close();
            sqlCommand.Dispose();

            return RetCount;
        }
        public static int SelectCountUnread(int UserId)
        {
            var Count = 0;

            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select count(*) from Message where MessageToId=" + UserId + " and MessageSeenDate is null";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());


            sqlCommand.Connection.Open();
            Count = int.Parse(sqlCommand.ExecuteScalar().ToString());
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();

            return Count;
        }
        public static void UpdateSeen(int messageId, int userId)
        {
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"update  Message set MessageSeenDate=getdate() where MessageId=" + messageId + " and MessageToId=" + userId;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = new SqlConnection(WebConfigurationManager.AppSettings["MainConnectionString"].ToString());
            
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlCommand.Connection.Close();
            sqlCommand.Dispose();

        }
    }
}
