using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.Qr
{
    public static class QrHelper
    {
        public static string GetContentForQr(Enums.Options.Language lng, string sellerName, string vatRegisterId, DateTime time, decimal vatTotal, decimal TotalInvoice,string Addstext="")
        {
            if(lng== Enums.Options.Language.Ar)
            {
                return "أسم البائع :" + sellerName +
                       Environment.NewLine + 
                       "الرقم الضريبي :" +vatRegisterId + 
                       Environment.NewLine + 
                       "الوقت :" + time.ToString() + 
                       Environment.NewLine + 
                       "مبلغ الضريبة : " + vatTotal + 
                       Environment.NewLine + 
                       "اجمالي الفاتورة : " + TotalInvoice + 
                       Environment.NewLine +
                       "["+Addstext +"]";
            }
            else if(lng== Enums.Options.Language.En)
            {
                return "Seller Name :" + sellerName +
                        Environment.NewLine +
                        "VAT NO :" + vatRegisterId +
                        Environment.NewLine +
                        "DATEIME :" + time.ToString() +
                        Environment.NewLine +
                        "VAT AMOUNT : " + vatTotal +
                        Environment.NewLine +
                        "TOTAL INVOICE : " + TotalInvoice +
                       Environment.NewLine +
                       "[" + Addstext + "]";
            }
            else
            {
                return "";
            }
        }
    }
}
