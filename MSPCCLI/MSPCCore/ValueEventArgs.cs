using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polimi.DEIB.VahidJalili.MSPC.Analyzer
{
    public class ValueEventArgs:EventArgs
    {
        public string Value { get; set; }
        public ValueEventArgs(string value)
        {
            Value = value;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
