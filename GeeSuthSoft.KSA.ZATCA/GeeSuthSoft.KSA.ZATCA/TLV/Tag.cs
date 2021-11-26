using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.TLV
{
    public class Tag
    {
        private int _tag;
        private byte[] _value;

        public Tag(int tag, byte[] value)
        {
            _tag = tag;
            _value = value;


        }

        private int GetTag()
        {
            return this._tag;
        }

        private byte[] GetValue()
        {
            return this._value;
        }

        private int GetLength()
        {
            return this._value.Length;
        }

        private string ToHexDecimal(int value)
        {
            string hex = string.Format("{0:X2}", value);
            string input = hex.Length % 2 == 0 ? hex : hex + "0";
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < input.Length; i += 2)
            {
                string str = input.Substring(i, i + 2);
                output.Append((char)int.Parse(str, System.Globalization.NumberStyles.HexNumber));
            }
            return output.ToString();
        }

        public override string ToString()
        {
            return string.Join("",
                this.ToHexDecimal(this.GetTag()),
                this.ToHexDecimal(this.GetLength()),
                Encoding.UTF8.GetString(this.GetValue()));
        }

    }
}
