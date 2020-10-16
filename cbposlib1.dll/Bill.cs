using System;
using System.IO;
using System.Web.Script.Serialization;
using System.Windows;

namespace BonusSystem
{
    public class Bill
    {
        public static int BillMakeFile(string billFile, bill bill)
        {
            string billJson = String.Empty;

            if (!File.Exists(billFile))
                return 1;

            StreamWriter files = new StreamWriter(billFile, false);

            good[] good = new good[bill.goodList.good.Length];
            goodList goodList = new goodList();
            bill billMake = new bill();

            goodList.good = bill.goodList.good;

            billMake.totalSum = bill.totalSum;
            billMake.orderLen = bill.orderLen;
            billMake.billNum = bill.billNum;
            billMake.billDate = String.Format("{0:yyyy-MM-ddTHH:mm:ss}", DateTime.Now);
            billMake.goodList = goodList;

            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                billJson = js.Serialize(billMake);
                files.WriteLine(billJson);

                return 0;
            }

            catch (Exception)
            {
                return 1;
            }
            finally
            {
                files.Flush();
                files.Close();
            }
        }

        public static bill BillParse(string billFile)
        {
            bill bill = new bill();

            if (!File.Exists(billFile))
                return bill;

            StreamReader file = new StreamReader(billFile);

            string billStr = file.ReadToEnd().ToString();

            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                bill = js.Deserialize<bill>(billStr);

                return bill;
            }
            catch (Exception)
            {
                return bill;
            }
            finally
            {
                file.Dispose();
                file.Close();
            }
        }
    }

    public class bill
    {
        public int totalSum { get; set; }
        public int orderLen { get; set; }
        public string billNum { get; set; }
        public string billDate { get; set; }
        public goodList goodList { get; set; }
    }

    public class goodList
    {
        public good[] good { get; set; }
    }

    public class good
    {
        public string title { get; set; }
        public string productCode { get; set; }
        public int positionCost { get; set; }
        public int orderNum { get; set; }
        public int cnt { get; set; }
        public bonus[] bonus { get; set; }
    }

    public class bonus
    {
        public int earned { get; set; }
        public int spend { get; set; }
        public int organizerFee { get; set; }
        public string accountType { get; set; }
    }

    public class dataFromBill
    {
        public int[] earned { get; set; }
        public int[] spend { get; set; }
        public int[] organizerFee { get; set; }
    }
}
