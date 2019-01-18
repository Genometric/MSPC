// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.


namespace Genometric.MSPC.Core.Model
{
    public struct ProgressReport
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="step">The number of current step out of the total steps to be taken.</param>
        /// <param name="stepCount">The number of all the steps that will be taken.</param>
        /// <param name="subStep">Sets if this reports the status of a sub process.</param>
        /// <param name="updatesPrevious">Sets if this report updates the previous report.</param>
        /// <param name="message">A message explaining the step</param>
        public ProgressReport(int step, int stepCount, bool subStep, bool updatesPrevious, string message)
        {
            Step = step;
            StepCount = stepCount;
            Message = message;
            SubStep = subStep;
            UpdatesPrevious = updatesPrevious;
        }

        public int Step { private set; get; }
        public int StepCount { private set; get; }
        public string Message { private set; get; }
        public bool SubStep { get; }
        public bool UpdatesPrevious { get; }
    }
}
