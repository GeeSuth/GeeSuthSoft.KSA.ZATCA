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
        public ITag(int tag, string value)
        {
            this.TagIndex = tag;
            this.value = value;
        }

        private string GetLength()
        {
            if (Regex.IsMatch(this.value, @"\p{IsArabic}"))
            {
                //The Space Should not increase * 2 only Arabic Char 
                /*
                 1- I remove spaces because spaces is not arabic char
                 2- Count Arabic char only used regex and increase it * 2 
                 3- So I need to get count of space to add count to lenght but not increased 
                */
                return (Regex.Matches(this.value.Replace(" ", ""), @"[ء-ي]").Count * 2 + (this.value.Count(Char.IsWhiteSpace))).ToString("X2");

            }

            return this.value.Length.ToString("X2");
        }

        public override string ToString()
        {

            return $"{TagIndex.ToString("X2")}{GetLength()}{ToHex(this.value)}";

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
