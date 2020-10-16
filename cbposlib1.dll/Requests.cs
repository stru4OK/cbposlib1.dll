using System.Windows;

namespace BonusSystem
{
    public class Requests
    {
        public static int OpenShift(string shiftid)
        {
            int res = Cbposlib.OpenShift(shiftid);

            return AdditionalFunc.GetResultAPI(res);
        }

        public static int CloseShift()
        {
            int res = Cbposlib.CloseShift();

            return AdditionalFunc.GetResultAPI(res);
        }

        public static string BonusReceipt()
        {
            string check = AdditionalFunc.StringFromNativeUtf8(Cbposlib.getReceiptHeaderPtr());
            check = check + AdditionalFunc.StringFromNativeUtf8(Cbposlib.getReceiptBodyPtr());
            check = check + AdditionalFunc.StringFromNativeUtf8(Cbposlib.getReceiptFooterPtr());

            return check;
        }

        public static int BeforeOperation(string cardNum)
        {
            int result = 0;
            int res = Cbposlib.BPOSInit();

            if (res != 0)
            {
                result = 1;
                MessageBox.Show(AdditionalFunc.StringFromNativeUtf8(Cbposlib.GetErrorMessage(res)), "Ошибка");
            }
            else
            {
                res = Cbposlib.CreateTransaction(cardNum, "TestEmployeeId");

                if (res != 0)
                {
                    result = 1;
                    MessageBox.Show(AdditionalFunc.StringFromNativeUtf8(Cbposlib.GetErrorMessage(res)), "Ошибка");
                }
            }

            return result;
        }

        public static int AfterOperation()
        {
            int result = 0;
            int res = Cbposlib.CloseTransaction();

            if (res != 0)
            {
                result = 1;
                MessageBox.Show(AdditionalFunc.StringFromNativeUtf8(Cbposlib.GetErrorMessage(res)), "Ошибка");
            }

            res = Cbposlib.BPOSClean();

            if (res != 0)
            {
                result = 1;
                MessageBox.Show(AdditionalFunc.StringFromNativeUtf8(Cbposlib.GetErrorMessage(res)), "Ошибка");
            }

            return result;
        }

        public static string GetRRN()
        {
            string rrn = AdditionalFunc.StringFromNativeUtf8(Cbposlib.getRRNForCancel());

            return rrn;
        }

        public static int BalanceOperation()
        {
            int res = Cbposlib.BalanceProc();

            return AdditionalFunc.GetResultAPI(res);
        }

        public static int DepositOperation(int depositSum)
        {
            int res = Cbposlib.DepositProc(depositSum);

            return AdditionalFunc.GetResultAPI(res);
        }

        public static int CancelOperation(string rrn)
        {
            int res = Cbposlib.CancelProc(rrn);

            return AdditionalFunc.GetResultAPI(res);
        }

        public static int ExchangeOperation(string token)
        {
            int res = Cbposlib.ExchangeProc(token);

            return AdditionalFunc.GetResultAPI(res);
        }

        public static int PaymentAndConfirmOperation(int spend, string bill)
        {
            int res = Cbposlib.PaymentAndConfirmProc(spend, bill);

            return AdditionalFunc.GetResultAPI(res);
        }

        public static int PaymentOperation(int spend, string bill)
        {
            int res = Cbposlib.PaymentProc(spend, bill);

            return AdditionalFunc.GetResultAPI(res);
        }

        public static int ConfirmOperation(string rrn)
        {
            int res = Cbposlib.PaymentConfirmProc(rrn);

            return AdditionalFunc.GetResultAPI(res);
        }

        public static string HashCardNumOperation(string cardNum)
        {
            string hash = AdditionalFunc.StringFromNativeUtf8(Cbposlib.BposHashForCardNum(cardNum));

            return hash;
        }

        public static string SummaryReportOperation(string beginDate, string endDate)
        {
            int res = Cbposlib.GenerateSummaryReport(beginDate, endDate);

            if (res < 0)
            {
                MessageBox.Show(AdditionalFunc.StringFromNativeUtf8(Cbposlib.GetErrorMessage(res)), "Ошибка");
                return res.ToString();
            }

            string report = AdditionalFunc.StringFromNativeUtf8(Cbposlib.GetReportPtr());

            return report;
        }

        public static string DetailReportOperation(string beginDate, string endDate)
        {
            int res = Cbposlib.GenerateReport(beginDate, endDate);

            if (res < 0)
            {
                MessageBox.Show(AdditionalFunc.StringFromNativeUtf8(Cbposlib.GetErrorMessage(res)), "Ошибка");
                return res.ToString();
            }

            string report = AdditionalFunc.StringFromNativeUtf8(Cbposlib.GetReportPtr());

            return report;
        }

        public static int SendOfflineOperation()
        {
            int res = Cbposlib.SendOfflinePayments();

            MessageBox.Show("Количество выгруженных оффлайн операций: " + res, "Выгрузка оффлайн операций");
            return res;
        }

        public static int ActivationOperation(string activation_type, string bill, int amount, string type, string refer_card_num)
        {
            int res = Cbposlib.ActivationProc(activation_type, bill, amount, type, refer_card_num);

            return AdditionalFunc.GetResultAPI(res);
        }

        public static int CashBackFirstTypeOperation(string rrn, string cashback_file)
        {
            int res = Cbposlib.CashBackProc(rrn, cashback_file);

            return AdditionalFunc.GetResultAPI(res);
        }

        public static int CashBackSecondTypeOperation(string rrn, int cashback_sum)
        {
            int res = Cbposlib.CashBackSumProc(rrn, cashback_sum);

            return AdditionalFunc.GetResultAPI(res);
        }

        public static int LinkCardOperation(string linkcardNum)
        {
            int res = Cbposlib.LinkCardProc(linkcardNum);

            return AdditionalFunc.GetResultAPI(res);
        }

        public static int PaymentCancelOperation(string rrn)
        {
            int res = Cbposlib.PaymentCancelProc(rrn);

            return AdditionalFunc.GetResultAPI(res);
        }
    }
}
