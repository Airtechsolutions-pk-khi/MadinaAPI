using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace MadinaAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificationController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        [Route("api/push/android")]
        public void PushNotification()
        {
            try
            {                
                var applicationID = "AAAArEaNSXc:APA91bHaIHE6S4eXqbWpu8sRAvU9TxTkMj-q-bbEW6FMUp3LoEBeexRz3sNtIU7qJBrBkddJRwPD4zDCSYN6V6ygFGzNVmw1kuQEIxvVvqW2yCbPFN24IDSCLdyB_kXnBaCR_3e7zf_u";
                var senderId = "739918039415";
                //var senderId = "42928891220";
                string deviceId = "fQ6krLJRTV6e97TmK-7Hks:APA91bHkUj_Vowc-5mr4epwABKf2EDLjrHhIv3wSP-uTbSWLsYy11j2cCgqwfPtQwuFAxopRo5EdqRp8XNHncPL_3g8NMkit9EcLUAbuYro2_yq09aGsAMXhS2knC5OG1O84buTZemSf";
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = "test",
                        title = "teest",
                        icon = "myicon",
                        sound = "default"

                    }
                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
    }
}
