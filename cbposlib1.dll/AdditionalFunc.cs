using System;
using System.IO;
using Newtonsoft.Json;
using System.Windows;
using System.Runtime.InteropServices;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;

namespace BonusSystem
{
    public class AdditionalFunc
    {
        public static string DataBaseSQL(string oracleDBConnection, string sql, bool needResult)
        {
            string result = String.Empty;

            OracleConnection conn = new OracleConnection(oracleDBConnection);

            try
            {
                conn.Open();
                OracleCommand dbcmd = conn.CreateCommand();
                dbcmd.CommandText = sql;
                dbcmd.CommandTimeout = 60;
                OracleDataReader reader = dbcmd.ExecuteReader();

                if (needResult)
                {
                    while (reader.Read())
                    {
                        result = (string)reader["data"].ToString();
                    }
                }

                reader.Close();
                reader = null;

                return result;
            }
            catch (Exception)
            {
                return result;
            }
            finally
            {
                OracleConnection.ClearPool(conn);
                conn.Dispose();
                conn.Close();
                conn = null;
            }
        }

        public static List<string> GetMobileUrls(string oracleDBConnection, string receiver)
        {
            OracleConnection conn = new OracleConnection(oracleDBConnection);

            string sql = "select mobile_push_url from mobile_logins ml "
                + "join mobile_push_names mpn on ml.mobile_name = mpn.mobile_name "
                + "where login_name = '" + receiver + "' and ml.is_delete = 0 ";

            List<string> GetMobileUrls = new List<string>();

            try
            {
                conn.Open();
                OracleCommand dbcmd = conn.CreateCommand();
                dbcmd.CommandText = sql;
                dbcmd.CommandTimeout = 60;
                OracleDataReader reader = dbcmd.ExecuteReader();

                while (reader.Read())
                {
                    GetMobileUrls.Add(reader.IsDBNull(0) ? String.Empty : reader.GetString(0));
                }

                reader.Close();
                reader = null;

                return GetMobileUrls;
            }
            catch (Exception)
            {
                return GetMobileUrls;
            }
            finally
            {
                OracleConnection.ClearPool(conn);
                conn.Dispose();
                conn.Close();
                conn = null;
            }
        }

        public static ResponseBMS BMSRequest(string typeRequest, string request, string data, string login, string password)
        {
            ResponseBMS Response = new ResponseBMS();

            try
            {
                CredentialCache cache = new CredentialCache();
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(request);
                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(login + ":" + password));
                req.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;

                //Игнорируем недостоверный сертификат SSL
                ServicePointManager.ServerCertificateValidationCallback += delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                req.KeepAlive = false;
                req.Method = typeRequest;
                req.Timeout = 20000;

                if (!Equals(typeRequest, "GET"))
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(data);
                    req.ContentType = "application/json";
                    req.ContentLength = byteArray.Length;
                    Stream dataStreamReq = req.GetRequestStream();
                    dataStreamReq.Write(byteArray, 0, byteArray.Length);
                    dataStreamReq.Close();
                }

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                Stream dataStreamResp = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStreamResp, Encoding.Default);
                Response = JsonConvert.DeserializeObject<ResponseBMS>(reader.ReadToEnd());
                
                reader.Close();
                dataStreamResp.Close();
                response.Close();

                return Response;
            }
            catch (WebException e)
            {
                using (Stream streamData = e.Response.GetResponseStream())

                using (var reader = new StreamReader(streamData))
                {
                    Response = JsonConvert.DeserializeObject<ResponseBMS>(reader.ReadToEnd());
                    reader.Close();
                    streamData.Close();
                }
                
                return Response;
            }
            catch (Exception)
            {
                return Response;
            }
        }

        public static HttpResponse HTTPRequest(string typeRequest, string request, string data, string login, string password)
        {
            HttpResponse HttpResponse = new HttpResponse();

            try
            {
                CredentialCache cache = new CredentialCache();
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(request);
                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(login + ":" + password));
                req.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;

                //Игнорируем недостоверный сертификат SSL
                ServicePointManager.ServerCertificateValidationCallback += delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                req.KeepAlive = false;
                req.PreAuthenticate = true;
                req.Method = typeRequest;

                if (!Equals(typeRequest, "GET") & !Equals(typeRequest, "HEAD"))
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(data);
                    req.ContentType = "application/json";
                    req.ContentLength = byteArray.Length;
                    Stream dataStreamReq = req.GetRequestStream();
                    dataStreamReq.Write(byteArray, 0, byteArray.Length);
                    dataStreamReq.Close();
                }

                WebResponse response = req.GetResponse();
                Stream dataStreamResp = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStreamResp, Encoding.Default);
                HttpResponse.response = reader.ReadToEnd();
                reader.Close();
                dataStreamResp.Close();
                response.Close();

                HttpResponse.result = 0;
                return HttpResponse;
            }
            catch (Exception)
            {
                HttpResponse.result = 1;
                return HttpResponse;
            }
        }

        public static Config ReadConfig()
        {
            string configData = String.Empty;
            string configPath = "config.jsn";

            Config Config = new Config();

            if (File.Exists(@"config")) configPath = "config";

            try
            {
                StreamReader file = new StreamReader(configPath);
                configData = file.ReadToEnd();

                Config ConfigParameters = JsonConvert.DeserializeObject<Config>(configData);
                Config = ConfigParameters;

                return Config;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка чтения config.jsn/config", "Ошибка");
                return Config;
            }
        }

        public static int GetResultAPI(int res)
        {
            if (res != 0)
            {
                if (res == -405)
                {
                    MessageBox.Show(StringFromNativeUtf8(Cbposlib.getResponseStateCodeDescription()) + "\n" + ResultStateMessage(), "Ошибка");
                    return 1;
                }

                MessageBox.Show(StringFromNativeUtf8(Cbposlib.GetErrorMessage(res)), "Ошибка");
                return 1;
            }

            return 0;
        }

        public static int GetResultBMSAPI(ResponseBMS Response)
        {
            if (Response == null)
            {
                MessageBox.Show("Ответ не был получен", "Ошибка");
                return 1;
            }
                
            if(Response.BpsResponse.state == "ERROR")
            {
                MessageBox.Show(Response.BpsResponse.stateCode, "Ошибка");
                return 1;
            }
            else
                MessageBox.Show("Операция прошла успешно", "Сообщение");

            return 0;
        }

        public static int GetResult(int result)
        {
            if (result == 1)
            {
                MessageBox.Show("Операция прошла не успешно", "Ошибка");
                return 1;
            }
            else
                MessageBox.Show("Операция прошла успешно", "Сообщение");

            return 0;
        }

        public static string ResultStateMessage()
        {
            string delState = String.Empty;

            if (string.Equals(StringFromNativeUtf8(Cbposlib.getResponseStateCode()), "INCORRECT_REQUEST_ID"))
            {
                File.Delete("STATE");
                delState = "Удаляем файл STATE";
            }
            
            return delState;
        }

        public static string CardNumFromHash(string oracleDBConnection, string hash)
        {
            return DataBaseSQL(oracleDBConnection, "select card_num as data from (select card_num from cards where card_id = " + hash.Substring(38, hash.Length - 39) 
                + " and is_delete = 0 union select card_num from gift_cards where id = " + hash.Substring(38, hash.Length - 39) + " and is_delete = 0)", true);
        }

        public static string HashCardExtraPayment(string oracleDBConnection, string cardNum, bool isEnableExtraPayment)
        {
            string cardId = DataBaseSQL(oracleDBConnection, "select card_id as data from (select card_id from cards where card_num = '" + cardNum 
                + "' and is_delete=0 union select id from gift_cards where card_num = '" + cardNum + "' and is_delete=0)", true);
            string salt = DataBaseSQL(oracleDBConnection, "select qr_salt as data from (select qr_salt from cards where card_num = '" + cardNum
                + "' and is_delete=0 union select qr_salt from cards where cli_id = (select client_id from gift_cards where card_num = '" + cardNum 
                + "' and is_delete = 0) and is_delete = 0)", true);

            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            return "BMSQR_" + StringToMD5(salt + cardNum + unixTimestamp / 120) + cardId + Convert.ToInt16(isEnableExtraPayment);
        }

        public static void HashCardFindTime(string oracleDBConnection, string hashCardNum)
        {
            string timeHashGeneration = String.Empty;
            string cardId = (hashCardNum.Remove(hashCardNum.Length-1, 1)).Remove(0, 38);
            string cardHash = ((hashCardNum.Remove(hashCardNum.Length - 1, 1)).Remove(hashCardNum.Length-1- cardId.Length, cardId.Length)).Remove(0,6);

            string cardNum = DataBaseSQL(oracleDBConnection, "select card_num as data from (select card_num from cards where card_id = " 
                + cardId + " and is_delete = 0 union select card_num from gift_cards where id = " + cardId + " and is_delete = 0)", true);
            string salt = DataBaseSQL(oracleDBConnection, "select qr_salt as data from (select qr_salt from cards where card_num = '" + cardNum
                + "' and is_delete = 0 union select qr_salt from cards where cli_id = (select client_id from gift_cards where card_num = '" + cardNum
                + "' and is_delete = 0) and is_delete = 0)", true);

            if (String.IsNullOrEmpty(salt))
            {
                MessageBox.Show("Соль у карты отсутствует в базе", "Сообщение");
                return;
            }

            Int32 minUnixTimeStamp = (Int32)(DateTime.UtcNow.AddHours(-168).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            //Int32 minUnixTimeStamp = 0;
            Int32 maxUnixTimeStamp = (Int32)(DateTime.UtcNow.AddHours(+168).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            while (minUnixTimeStamp < maxUnixTimeStamp)
            {
                if (String.Equals(cardHash, StringToMD5(salt + cardNum + minUnixTimeStamp / 120)))
                {
                    timeHashGeneration = UnixTimeStampToDateTime(minUnixTimeStamp).ToString("dd.MM.yyyy HH:mm");
                    MessageBox.Show("Время генерации хэша: " + timeHashGeneration, "Сообщение");

                    break;
                }

                minUnixTimeStamp = minUnixTimeStamp + 120;
            }

            if(String.IsNullOrEmpty(timeHashGeneration))
                MessageBox.Show("Не найдено время генерации хэша в пределах ± 1 неделя", "Сообщение");
        }

        public static DateTime UnixTimeStampToDateTime(Int32 unixTimeStamp)
        {
            DateTime DateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            DateTime = DateTime.AddSeconds((unixTimeStamp/120)*120).ToLocalTime();
            return DateTime;
        }

        public static string StringToMD5(string stringToMD5)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] hash = Encoding.UTF8.GetBytes(stringToMD5);
            byte[] hashenc = md5.ComputeHash(hash);
            string result = String.Empty;

            foreach (var b in hashenc)
            {
                result += b.ToString("x2");
            }

            return result;
        }

        public static string StringFromNativeUtf8(IntPtr nativeUtf8)
        {
            int len = 0;
            try
            {
                while (Marshal.ReadByte(nativeUtf8, len) != 0) ++len;
                byte[] buffer = new byte[len];
                Marshal.Copy(nativeUtf8, buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer);
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        public static string SaveConfig(Config Config)
        {
            string configData = String.Empty;
            string configPath = "config.jsn";

            try
            {
                configData = JsonConvert.SerializeObject(Config);

                File.WriteAllText(configPath, configData);
                return configData;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка сохранения config.jsn", "Ошибка");
                return configData;
            }
        }
    }

    public class HttpResponse
    {
        public int result { get; set; }
        public string response { get; set; }
    }

    public class PushResponse
    {
        public int success { get; set; }
        public int failure { get; set; }
        public string sendState { get; set; }
    }

    [JsonObject]
    public class Config
    {
        public string terminalId { get; set; }
        public string protocolVersion { get; set; }
        public string serverAddr { get; set; }
        public string serverAltAddr { get; set; }
        public string retailNetworkName { get; set; }
        public string retailPointAddress { get; set; }
        public int timeout { get; set; }
        public int connect_timeout { get; set; }
        public int retry_pause { get; set; }
        public int offlineMode { get; set; }
        public int orgFeeOffline { get; set; }
        //public int sentSpendOnActivation { get; set; }
        //public int noLocalBonusPrint { get; set; }
        //public int noDiscountBonusPrint { get; set; }
        //public int noGlobalBonusPrint { get; set; }
        public int shiftMode { get; set; }
        public int sslMode { get; set; }
        public string sslKey { get; set; }
        public int logSize { get; set; }
        public int hashCardNum { get; set; }
        //public string receiptHeader { get; set; }
        //public string receiptMainHeader { get; set; }
        //public string receiptOrganizerFee { get; set; }
        //public string receiptFooter { get; set; }
        public string terminalPassword { get; set; }
        public string billXMLConvertTable { get; set; }
        public int timeZone { get; set; }
        public class_card class_card { get; set; }
    }

    [JsonObject]
    public class class_card
    {
        [JsonProperty("1")]
        public _1 _1  {get; set;}
        [JsonProperty("2")]
        public _2 _2  {get; set;}
        [JsonProperty("5")]
        public _5 _5  {get; set;}
        [JsonProperty("6")]
        public _6 _6  {get; set;}
    }

    [JsonObject]
    public class _1
    {
        public int localBonusOffline { get; set; }
        public int globalBonusOffline { get; set; }
    }

    [JsonObject]
    public class _2
    {
        public int localBonusOffline { get; set; }
        public int globalBonusOffline { get; set; }
    }

    [JsonObject]
    public class _5
    {
        public int localBonusOffline { get; set; }
        public int globalBonusOffline { get; set; }
    }

    [JsonObject]
    public class _6
    {
        public int localBonusOffline { get; set; }
        public int globalBonusOffline { get; set; }
    }
}
