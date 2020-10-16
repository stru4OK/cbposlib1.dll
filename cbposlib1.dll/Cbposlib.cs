using System;
using System.Runtime.InteropServices;

namespace BonusSystem
{
    /*public class CbposlibDynamic
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr LoadLibrary(string lpLibFileName);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule,[MarshalAs(UnmanagedType.LPStr)] string lpProcName);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool FreeLibrary(IntPtr hModule);

        [StructLayout(LayoutKind.Sequential)]
        public struct Arguments
        {
            public string cardNum;
            public string employeeId;
        };

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        delegate int MethodDelegate([MarshalAs(UnmanagedType.Struct)]Arguments args);

        public static int LoadDllMethod(string method, Arguments args)
        {
            int result = 1;
            IntPtr hModule = LoadLibrary("cbposlib1.dll");
            if (hModule == IntPtr.Zero) return result;

            IntPtr intPtr = GetProcAddress(hModule, method);
            MethodDelegate md = (MethodDelegate)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(MethodDelegate));

            result = md(args);

            if (method == "BPOSClean")
            {
                FreeLibrary(hModule);
            }
            return result;
        }
    }*/


    public class Cbposlib
    {
        private const string pathDll = "cbposlib1.dll";
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OpenShift(string shiftid);
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CloseShift();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr BposHashForCardNum(string card_num);
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CreateAmountBill(int amount, string fileName);
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CreateAmountBillCategorised(int amount, string fileName, int category);
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CreateAmountBillEx(int amount, string fileName, int category, string billNum);
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int BPOSInit();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ActivationProc(string activation_type, string bill, int amount, string type, string refer_card_num);
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int BPOSClean();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CreateTransaction(string card_num, string employeeId);
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int BalanceProc();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DepositProc(int amount);
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CloseTransaction();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetBalance();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int PaymentAndConfirmProc(int spend, string bill);
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int PaymentProc(int spend, string bill);
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int PaymentConfirmProc(string rrn);
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CancelProc(string rrn);
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetErrorMessage(int code);
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getResponseState();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getResponseStateCode();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getResponseStateCodeDescription();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int PaymentCancelProc(string rrn);
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int LinkCardProc(string card_num);
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ExchangeProc(string token);
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetEarnBonusAmount();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetSpendBonusAmount();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getRRN();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getRRNForCancel();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SendOfflinePayments();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetReportPtr();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GenerateReport(string begin_time, string end_time);
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GenerateSummaryReport(string begin_time, string end_time);
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getReceiptHeaderPtr();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getReceiptBodyPtr();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getReceiptFooterPtr();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CashBackProc(string rrn, string cashback_file);
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CashBackSumProc(string rrn, int cashback_sum);
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetCashBackResult();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetCashBackDeduct();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetCashBackEarned();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetCashBackSpend();
        [DllImport(pathDll, CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetActivationAmount();
    }
 
}
