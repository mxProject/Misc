using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDataGenerator
{

    /// <summary>
    /// Easy implementation of Random.
    /// </summary>
    internal static class EasyRandomUtility
    {

        private readonly static Random r = new Random();

        #region Int16

        /// <summary>
        /// 
        /// </summary>
        internal static readonly RandomValueGenerator<Int16> RandomInt16 = new RandomValueGenerator<Int16>(NextRandomInt16, NextRandomInt16, NextNullProbability);

        /// <summary>
        /// Generate the next random value.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        internal static Int16 NextRandomInt16()
        {
            return NextRandomInt16(0, Int16.MaxValue);
        }

        /// <summary>
        /// Generate the next random value.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        internal static Int16 NextRandomInt16(Int16 minValue, Int16 maxValue)
        {
            return (Int16)r.Next(minValue, maxValue + 1);
        }

        #endregion

        #region Int32

        /// <summary>
        /// 
        /// </summary>
        internal static readonly RandomValueGenerator<Int32> RandomInt32 = new RandomValueGenerator<Int32>(NextRandomInt32, NextRandomInt32, NextNullProbability);

        /// <summary>
        /// Generate the next random value.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        internal static Int32 NextRandomInt32()
        {
            return r.Next();
        }

        /// <summary>
        /// Generate the next random value.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        internal static Int32 NextRandomInt32(Int32 minValue, Int32 maxValue)
        {
            return r.Next(minValue, maxValue + 1);
        }

        #endregion

        #region Int64

        /// <summary>
        /// 
        /// </summary>
        internal static readonly RandomValueGenerator<Int64> RandomInt64 = new RandomValueGenerator<Int64>(NextRandomInt64, NextRandomInt64, NextNullProbability);

        /// <summary>
        /// Generate the next random value.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        internal static Int64 NextRandomInt64()
        {
            return NextRandomInt64(0, Int64.MaxValue);
        }

        /// <summary>
        /// Generate the next random value.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        internal static Int64 NextRandomInt64(Int64 minValue, Int64 maxValue)
        {

            ulong uRange = (ulong)(maxValue - minValue);
            ulong ulongRand;
            do
            {
                byte[] buf = new byte[8];
                r.NextBytes(buf);
                ulongRand = (ulong)BitConverter.ToInt64(buf, 0);
            } while (ulongRand > ulong.MaxValue - ((ulong.MaxValue % uRange) + 1) % uRange);

            return (long)(ulongRand % uRange) + minValue;

        }

        #endregion

        #region Single

        /// <summary>
        ///
        /// </summary>
        internal static readonly RandomValueGenerator<Single> RandomSingle = new RandomValueGenerator<Single>(NextRandomSingle, NextRandomSingle, NextNullProbability);

        /// <summary>
        /// Generate the next random value.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        internal static Single NextRandomSingle()
        {
            return NextRandomSingle(0, Single.MaxValue);
        }

        /// <summary>
        /// Generate the next random value.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        internal static Single NextRandomSingle(Single minValue, Single maxValue)
        {
            double rate = r.NextDouble();
            return minValue + (float)((maxValue / 2 - minValue / 2) * rate * 2);
        }

        #endregion

        #region Double

        /// <summary>
        ///
        /// </summary>
        internal static readonly RandomValueGenerator<Double> RandomDouble = new RandomValueGenerator<Double>(NextRandomDouble, NextRandomDouble, NextNullProbability);

        /// <summary>
        /// Generate the next random value.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        internal static Double NextRandomDouble()
        {
            return NextRandomDouble(0, Double.MaxValue);
        }

        /// <summary>
        /// Generate the next random value.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        internal static Double NextRandomDouble(Double minValue, Double maxValue)
        {
            double rate = r.NextDouble();
            return minValue + (maxValue / 2 - minValue / 2) * rate * 2;
        }

        #endregion

        #region Decimal

        /// <summary>
        ///
        /// </summary>
        internal static readonly RandomValueGenerator<Decimal> RandomDecimal = new RandomValueGenerator<Decimal>(NextRandomDecimal, NextRandomDecimal, NextNullProbability);

        /// <summary>
        /// Generate the next random value.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        internal static Decimal NextRandomDecimal()
        {
            return NextRandomDecimal(0, Decimal.MaxValue);
        }


        /// <summary>
        /// Generate the next random value.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        internal static Decimal NextRandomDecimal(Decimal minValue, Decimal maxValue)
        {
            double rate = r.NextDouble();
            return minValue + (maxValue / 2 - minValue / 2) * (decimal)rate * 2;
        }

        #endregion

        #region DateTime

        /// <summary>
        ///
        /// </summary>
        internal static readonly RandomValueGenerator<DateTime> RandomDateTime = new RandomValueGenerator<DateTime>(NextRandomDateTime, NextRandomDateTime, NextNullProbability);

        /// <summary>
        /// Generate the next random value.
        /// </summary>
        /// <returns></returns>
        internal static DateTime NextRandomDateTime()
        {
            return NextRandomDateTime(DateTime.MinValue, DateTime.MaxValue);
        }

        /// <summary>
        /// Generate the next random value.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        internal static DateTime NextRandomDateTime(DateTime minValue, DateTime maxValue)
        {
            return new DateTime(NextRandomInt64(minValue.Ticks, maxValue.Ticks));
        }

        #endregion

        #region Guid

        /// <summary>
        ///
        /// </summary>
        internal static readonly RandomValueGenerator<Guid> RandomGuid = new RandomValueGenerator<Guid>(NextRandomGuid, NextRandomGuid, NextNullProbability);

        /// <summary>
        /// Generate the next random value.
        /// </summary>
        /// <returns></returns>
        internal static Guid NextRandomGuid()
        {
            return Guid.NewGuid();
        }

        /// <summary>
        /// Generate the next random value.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        private static Guid NextRandomGuid(Guid minValue, Guid maxValue)
        {
            while (true)
            {
                Guid value = Guid.NewGuid();
                if (value.CompareTo(minValue) >= 0 && value.CompareTo(maxValue) <= 0) { return value; }
            }
        }

        #endregion


        /// <summary>
        /// Generate the next random value.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        internal static Double NextNullProbability()
        {
            return NextRandomDouble(0, 1);
        }

    }

}
