using System;
using System.Collections.Generic;
using System.Text;

namespace Genometric.MSPC.Core.Model
{
    public struct ProgressReport
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="step">this step number</param>
        /// <param name="stepCount">all steps count</param>
        /// <param name="message">message explaining the step</param>
        public ProgressReport(int step, int stepCount, string message)
        {
            this.step = step;
            this.stepCount = stepCount;
            this.message = message;
        }

        public int step { private set; get; }
        public int stepCount { private set; get; }
        public string message { private set; get; }
        public int percentage { get { return step / stepCount; } }

        public override bool Equals(object obj)
        {
            if (!(obj is ProgressReport))
                return false;

            var that = (ProgressReport)obj;
            return step == that.step && stepCount == that.stepCount && message == that.message ? true : false;
        }

        public override string ToString()
        {
            return step.ToString() + ":" + message;
        }
    }
}
