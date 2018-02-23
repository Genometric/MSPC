using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genometric.MSPC.Core.Model
{
    public class ValueEventArgs:EventArgs
    {
        public ProgressReport Value { get; set; }
        public ValueEventArgs(ProgressReport value)
        {
            Value = value;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
