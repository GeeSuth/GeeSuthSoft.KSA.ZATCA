using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.Tags
{
    public abstract class ITag
    {
        int TagIndex;
        string value;
        public ITag(int tag,string value)
        {
            this.TagIndex = tag;
            this.value = value;
        }

        private int GetLength()
        {
            return this.value.Length;
        }

        public override string ToString()
        {
            
            if (Regex.IsMatch(this.value, @"\p{IsArabic}"))
            {
                //Is Arabic Language , need to increase char 
                return $"{TagIndex.ToString("X2")}{(GetLength()*2).ToString("X2")}{ToHex(this.value)}";
            }
            else
            {
                return $"{TagIndex.ToString("X2")}{GetLength().ToString("X2")}{ToHex(this.value)}";
            }

        }

        private string ToHex(string val)
        {
            var sb = new StringBuilder();
            var bytes = Encoding.UTF8.GetBytes(val);
            foreach (var bt in bytes)
            {
                sb.Append(bt.ToString("X2"));
            }

            return sb.ToString();
        }

       

        
    }
}
