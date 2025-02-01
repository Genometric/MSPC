using System;

namespace Genometric.MSPC.Core.Model
{
    public class ValueEventArgs : EventArgs
    {
        public ProgressReport Value { get; set; }
        public ValueEventArgs(ProgressReport value)
        {
            Value = value;
        }
    }
}
