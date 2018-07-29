// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Genometric.MSPC.XSquaredData
{
    public class ChiSquaredCache
    {
        /// <summary>
        /// Returns the right-tailed probability of the chi-squared distribution.
        /// <para>Is equivalent to Excel CHISQ.DIST.RT(x,deg_freedom) command.</para>
        /// <para>.</para>
        /// <para>[http://office.microsoft.com]</para>
        /// <para>The χ2 distribution is associated with a χ2 test. Use the χ2 test to
        /// compare observed and expected values. For example, a genetic experiment
        /// might hypothesize that the next generation of plants will exhibit a
        /// certain set of colors. By comparing the observed results with the 
        /// expected ones, you can decide whether your original hypothesis is valid.</para>
        /// </summary>
        /// <param name="x">The value at which you want to evaluate the distribution.</param>
        /// <param name="df">The number of degrees of freedom.</param>
        /// <returns></returns>
        public static double ChiSqrdDistRTP(double x, int df)
        {
            switch (df)
            {
                case 04: return new DF04().ChiSqrd(x);
                case 06: return new DF06().ChiSqrd(x);
                case 08: return new DF08().ChiSqrd(x);
                case 10: return new DF10().ChiSqrd(x);
                case 12: return new DF12().ChiSqrd(x);
                case 16: return new DF16().ChiSqrd(x);
                case 18: return new DF18().ChiSqrd(x);
                case 20: return new DF20().ChiSqrd(x);
                default: return 0;
            }
        }


        /// <summary>
        /// Returns the inverse of the right-tailed probability of the chi-squared distribution.
        /// <para>Is equivalent to Excel CHISQ.INV.RT(probability,deg_freedom) command.</para>
        /// <para>.</para>
        /// <para>[http://office.microsoft.com]</para>
        /// <para>If probability = CHISQ.DIST.RT(x,...), then CHISQ.INV.RT(probability,...) = x.
        /// Use this function to compare observed results with expected ones in order to decide
        /// whether your original hypothesis is valid.</para>
        /// <para>Given a value for probability, CHISQ.INV.RT seeks that value x such that 
        /// CHISQ.DIST.RT(x, deg_freedom) = probability. Thus, precision of CHISQ.INV.RT depends
        /// on precision of CHISQ.DIST.RT. CHISQ.INV.RT uses an iterative search technique. </para>
        /// </summary>
        /// <param name="probability">A probability associated with the chi-squared distribution.</param>
        /// <param name="df">The number of degrees of freedom.</param>
        /// <returns></returns>
        public static double ChiSqrdINVRTP(double probability, int df)
        {
            // TODO: to be checked.
            if (probability == 1)
                return 0;

            if ((df / 2) - 1 <= 7 || (df / 2) - 1 < 0)
            {
                byte num_power = (byte)(Math.Ceiling(Math.Abs(Math.Log10(probability))));
                byte num_base = (byte)(Math.Floor(probability * Math.Pow(10, num_power)));

                if (num_base < 0 || num_base >= 9 ||
                    num_power < 0 || num_power >= 50)
                    return double.NaN;

                return INVRTPData.data[num_base - 1, num_power - 1, (df / 2) - 1];
            }
            else
            {
                return double.NaN;
            }
        }
    }
}
