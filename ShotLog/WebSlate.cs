using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;



namespace ShotLog
{
    class WebSlate
    {
        public static ProjectData data = new ProjectData();

        public static void SetDataobject(ProjectData newData)
        {
            data = newData;
        }

        public static void RunServer()
        {
            

            HttpListener server = new HttpListener();
            
            server.Prefixes.Add(string.Format("http://*:" + data.webslatePort + "/"));

            server.Start();

            Console.WriteLine("Webserver listening on port " + data.webslatePort);

            while (true)
            {
                if (data.restartWebslateServerFlag == true)
                {
                    server.Prefixes.Clear();
                    server.Prefixes.Add(string.Format("http://*:" + data.webslatePort + "/"));
                    data.restartWebslateServerFlag = false;
                }

                HttpListenerContext context = server.GetContext();
                HttpListenerResponse response = context.Response;

                string page = GeneratePage(data.webslateRefresh, data.webslateTextsize, data.webslateField1, data.webslateField2, data.webslateField3, data.webslateField4, data.webslateField5);


                byte[] buffer = Encoding.UTF8.GetBytes(page);

                response.ContentLength64 = buffer.Length;
                Stream st = response.OutputStream;
                st.Write(buffer, 0, buffer.Length);

                context.Response.Close();
            }

        }





        private static string GeneratePage(int refreshSpeed, int textSize, string field1, string field2, string field3, string field4, string field5)
        {
            string page = ""
                + "<!DOCTYPE html>"
+ "<html>"
    + "    <head>"
        + "        <title>ShotLog WebSlate</title>"
        + "        <meta http-equiv=\"refresh\" content=\"" + refreshSpeed.ToString() + "\">"
        + "        <meta http-equiv=\"Cache - Control\" content=\"no-cache, no-store, must-revalidate\" />"
        + "        <meta http-equiv=\"Pragma\" content=\"no-cache\" />"
        + "        <meta http-equiv=\"Expires\" content=\"0\" />"
                   + "        <style>"
 + "            body {background-color: #363636;}"
 + "            * {"
     + "                margin:0;"
     + "               padding:0;"
     + "            }"
     + "           html {"
         + "               height:100%;"
         + "               width:100%;"
         + "           }"
         + "           body {"
             + "                height:100%;"
             + "               width:100%;"
             + "           }"
             + "           .wrapper {"
                 + "               position: relative;"
                 + "               height:100%;"
                 + "               width:100%;"
                 + "               background:gray;"
                 + "           }"
                 + "           .data-container {"
                     + "                position: relative;"
                     + "               height:20%;"
                     + "               width:100%;"
                     + "               background:#363636;"
                     + "               color: #ffffff;"
                     + "               font-family: Arial, Helvetica, sans-serif;"
                     + "               font-weight: 800;"
                     + "               font-size: " + textSize.ToString() + "vw;"
                     + "               text-align: center;"
                     + "               vertical-align: middle;  "
                     + "           }"
                     + "       </style>"
  + "   </head>"
  + "   <body>"
     + "       <div class=\"wrapper\">"
          + "           <div class=\"data-container\">" + field1 + "</div>"
          + "           <div class=\"data-container\">" + field2 + "</div>"
          + "           <div class=\"data-container\">" + field3 + "</div>"
          + "           <div class=\"data-container\">" + field4 + "</div>"
          + "           <div class=\"data-container\">" + field5 + "</div>"


 + "         </div>"
+ "    </body>"
+ "</html>";





            return page;
        }
    }
}
