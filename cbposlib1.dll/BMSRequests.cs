using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Windows;

namespace BonusSystem
{
    public class BMSRequests
    {
        public static void PushSend(string oracleDBConnection, string receiver, string textMessage)
        {
            HttpResponse HttpResponse = new HttpResponse();
            JavaScriptSerializer js = new JavaScriptSerializer();
            int successSend = 0;

            List<string> MobileUrls = AdditionalFunc.GetMobileUrls(oracleDBConnection, receiver);

            if(MobileUrls.Count == 0)
                MessageBox.Show("Не найден мобильный логин!", "Сообщение");
            else
            {
                for (int i = 0; i < MobileUrls.Count; i++)
                {
                    HttpResponse = AdditionalFunc.HTTPRequest("POST", MobileUrls[i], "{\"phone\": \"" + receiver
                        + "\" , \"data\" : { \"message\": \"" + textMessage + "\"}}", "admin", "admin");

                    if (HttpResponse.result == 0)
                    {
                        PushResponse resp = js.Deserialize<PushResponse>(HttpResponse.response);

                        if (String.Equals(resp.sendState, "SENT") | String.Equals(resp.sendState, "SUCCESS"))
                            successSend++;
                    }
                }

                MessageBox.Show("Успешно отправлено Push-сообщений: " + successSend + " из " + MobileUrls.Count + "", "Сообщение");
            }
        }

        public static void SMSSend(string processingServiceName, string receiver, string textMessage)
        {
            AdditionalFunc.GetResult(AdditionalFunc.HTTPRequest("GET", processingServiceName + "bpsApi/do.SEND_MESSAGE/param={\"MESS_TYPE\":\"SMS\",\"MESS_BODY\":\"" + textMessage
                + "\",\"MESS_SUBJECT\":\"Test Message\",\"MESS_RECEIVER\":\"" + receiver + "\"}", String.Empty, String.Empty, String.Empty).result);
        }

        public static void RefreshActionsOperation(string serverAddr)
        {
            AdditionalFunc.GetResult(AdditionalFunc.HTTPRequest("GET", serverAddr + "refreshActions", String.Empty, String.Empty, String.Empty).result);
        }

        public static void BMSUnLockCardOperation(string oracleDBConnection, string serverAddr, string cardNum)
        {
            string cardId = AdditionalFunc.DataBaseSQL(oracleDBConnection, "select card_id as data from cards where card_num ='" + cardNum + "'", true);

            AdditionalFunc.GetResultBMSAPI(AdditionalFunc.BMSRequest("GET", serverAddr.Remove(serverAddr.Length - 5, 5) + "/do.UNLOCK_CARD/param={\"CARD_ID\":\""
                        + cardId + "\"}", String.Empty, String.Empty, String.Empty));
        }

        public static void BMSLockCardOperation(string oracleDBConnection, string serverAddr, string cardNum, bool isTokenNeed)
        {
            string cardId = AdditionalFunc.DataBaseSQL(oracleDBConnection, "select card_id as data from cards where card_num ='" + cardNum + "'", true);

            AdditionalFunc.GetResultBMSAPI(AdditionalFunc.BMSRequest("GET", serverAddr.Remove(serverAddr.Length - 5, 5) + "/do.LOCK_AND_SEND/param={\"CARD_ID\":\""
                        + cardId + "\",\"IS_TOKEN_NEED\":\"" + isTokenNeed + "\"}", String.Empty, String.Empty, String.Empty));
        }

        public static void BMSSendTokenOperation(string oracleDBConnection, string serverAddr, string cardNum)
        {
            string cardId = AdditionalFunc.DataBaseSQL(oracleDBConnection, "select card_id as data from cards where card_num ='" + cardNum + "'", true);

            AdditionalFunc.GetResultBMSAPI(AdditionalFunc.BMSRequest("GET", serverAddr.Remove(serverAddr.Length - 5, 5) + "/do.SEND_TOKEN/param={\"CARD_ID\":\""
                        + cardId + "\"}", String.Empty, String.Empty, String.Empty));
        }

        public static void BMSActivationOperation(string serverAddr, string terminalId, double amount, string accountType, string cardNum)
        {
            AdditionalFunc.GetResultBMSAPI(AdditionalFunc.BMSRequest("GET", serverAddr.Remove(serverAddr.Length - 5, 5) + "/do.ACTIVATION/param={\"ACTIVATION_UNIT\":{\"CARD_NUM\":\""
                        + cardNum + "\",\"TERM_CODE\":\"" + terminalId + "\",\"AMOUNT\":\"" + amount * 100 + "\",\"TYPE\":\"" + accountType + "\"}}",
                        String.Empty, String.Empty, String.Empty));
        }

        public static void BMSConfirmOperation(string oracleDBConnection, string serverAddr, string rrn)
        {
            string requestId = AdditionalFunc.DataBaseSQL(oracleDBConnection, "select request_id as data from requests where ext_request_id='" + rrn + "'", true);

            AdditionalFunc.GetResultBMSAPI(AdditionalFunc.BMSRequest("GET", serverAddr.Remove(serverAddr.Length - 5, 5) + "/do.PROCESS_REQUEST/param={\"REQUEST_ID\":\"" + requestId
                + "\"}", String.Empty, String.Empty, String.Empty));
        }

        public static void BMSCancelOperation(string oracleDBConnection, string serverAddr, string rrn)
        {
            string requestId = AdditionalFunc.DataBaseSQL(oracleDBConnection, "select request_id as data from requests where ext_request_id='" + rrn + "'", true);

            AdditionalFunc.GetResultBMSAPI(AdditionalFunc.BMSRequest("GET", serverAddr.Remove(serverAddr.Length - 5, 5) + "/do.CANCEL_REQUEST/param={\"REQUEST_ID\":\"" + requestId
                + "\"}", String.Empty, String.Empty, String.Empty));
        }
    }
}
