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

        public static void RunServer(ProjectData data)
        {
            

            HttpListener server = new HttpListener();
            server.Prefixes.Add("http://127.0.0.1/");
            server.Prefixes.Add("http://localhost/");
            server.Prefixes.Add("http://192.168.20.100/");

            server.Start();

            Console.WriteLine("Listening...");

            while (true)
            {
                

                HttpListenerContext context = server.GetContext();
                HttpListenerResponse response = context.Response;

                string page = GeneratePage(data.webslateRefresh, data.webslateTextsize, data.webslateField1, data.webslateField2, data.webslateField3, data.webslateField4, data.webslateField5);
                
                Console.WriteLine(data.webslateField1);

                //TextReader tr = new StreamReader(page);
                //string msg = tr.ReadToEnd();

                byte[] buffer = Encoding.UTF8.GetBytes(page);

                response.ContentLength64 = buffer.Length;
                Stream st = response.OutputStream;
                st.Write(buffer, 0, buffer.Length);

                context.Response.Close();
            }

        }



        /*
        // This example requires the System and System.Net namespaces.
            public static void SimpleListenerExample(ProjectData data){

            while(true)
            {
                
                string[] prefixes = new string[1];
                prefixes[0] = "http://*:8000/";

                if (!HttpListener.IsSupported)
                {
                    Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                    return;
                }
                // URI prefixes are required,
                // for example "http://contoso.com:8080/index/".
                if (prefixes == null || prefixes.Length == 0)
                    throw new ArgumentException("prefixes");

                // Create a listener.
                HttpListener listener = new HttpListener();
                // Add the prefixes.
                foreach (string s in prefixes)
                {
                    listener.Prefixes.Add(s);
                }
                listener.Start();
                Console.WriteLine("Listening...");
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                // Construct a response.
                string responseString = GeneratePage(data.webslateRefresh, data.webslateTextsize, data.webslateField1, data.webslateField2, data.webslateField3, data.webslateField4, data.webslateField5) +
                    "Note 1: " + data.notesField1 +
                    "<br>Note 2: " + data.notesField2 +
                    "<br>Note 3: " + data.notesField3 +
                    "<br>Note 4: " + data.notesField4 +
                    "<br>Note 5: " + data.notesField5 +
                    "</BODY></HTML>";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
                listener.Stop();
                listener.Close();

            }
          }
            */



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
