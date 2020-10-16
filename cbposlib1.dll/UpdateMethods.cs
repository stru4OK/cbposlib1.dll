using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace BonusSystem
{
    public class UpdateMethods
    {
        public static bool Update()
        {
            bool isUpdate = false;

            Dictionary<string, string> FilesToDownload = new Dictionary<string, string>();

            FilesToDownload.Add("cbposlib1.dll.exe", "http://192.168.4.150:90/UpdateUtils/cbposlib1/cbposlib1.dll.exe");
            FilesToDownload.Add("cbposlib1.dll", "http://192.168.4.150:90/UpdateUtils/cbposlib1.dll");
            FilesToDownload.Add("Profiles.jsn", "http://192.168.4.150:90/UpdateUtils/Profiles.jsn");

            foreach (KeyValuePair<string, string> kvp in FilesToDownload)
            {
                isUpdate |= DownloadFile(kvp.Value, kvp.Key);
            }

            return isUpdate;
        }

        private static bool DownloadFile(string fileUrl, string fileName)
        {
            if (RemoteFileExists(fileUrl))
            {
                if (isNeedUpdateRemoteFile(fileUrl, fileName))
                {
                    WebClient client = new WebClient();

                    if (File.Exists(fileName + "_old"))
                        File.Delete(fileName + "_old");

                    File.Move(fileName, fileName + "_old");

                    client.DownloadFile(fileUrl, fileName);
                    return true;
                }
            }

            return false;
        }

        private static bool RemoteFileExists(string fileUrl)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(fileUrl) as HttpWebRequest;
                request.Method = "HEAD";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool isNeedUpdateRemoteFile(string fileUrl, string localFile)
        {
            try
            {
                HttpWebRequest file = (HttpWebRequest)WebRequest.Create(fileUrl);
                HttpWebResponse fileResponse = (HttpWebResponse)file.GetResponse();

                fileResponse.Close();

                DateTime localFileModifiedTime = File.GetLastWriteTime(localFile);
                DateTime onlineFileModifiedTime = fileResponse.LastModified;

                if (localFileModifiedTime < onlineFileModifiedTime)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}