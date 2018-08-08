// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.


namespace Genometric.MSPC.Model
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
            Step = step;
            StepCount = stepCount;
            Message = message;
        }

        public int Step { private set; get; }
        public int StepCount { private set; get; }
        public string Message { private set; get; }
    }
}
