using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.Extensions
{
    public static class HexExtasion
    {
        public static byte[] HexStringToHex(this string inputHex)
        {
            var resultantArray = new byte[inputHex.Length / 2];
            for (var i = 0; i < resultantArray.Length; i++)
            {
                resultantArray[i] = System.Convert.ToByte(inputHex.Substring(i * 2, 2), 16);
            }
            return resultantArray;
        }
    }
}
