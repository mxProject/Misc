using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace TestDataGenerator
{

    /// <summary>
    /// 
    /// </summary>
    public sealed class ObservableDataGenerator
    {

        #region Random

        #region Int16

        /// <summary>
        /// Random value generator.
        /// </summary>
        public RandomValueGenerator<Int16> RandomInt16 { get; set; } = s_DefaultRandomInt16;

        /// <summary>
        /// Random value generator.
        /// </summary>
        private static readonly RandomValueGenerator<Int16> s_DefaultRandomInt16 = EasyRandomUtility.RandomInt16;

        /// <summary>
        /// Generate <see cref = "IObservable {Int16}" /> which returns a random value.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<Int16> Random(int count, Int16 minValue, Int16 maxValue)
        {
            return (RandomInt16 ?? s_DefaultRandomInt16).Values(count, minValue, maxValue);
        }

        /// <summary>
        /// Generate <see cref = "IObservable {Int16}" /> which returns either a random value or a default value.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="defaultProbability">Probability to return default value.（0.0～1.0）</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<Int16> RandomOrDefault(int count, double defaultProbability, Int16 minValue, Int16 maxValue)
        {
            return (RandomInt16 ?? s_DefaultRandomInt16).ValuesOrDefault(count, defaultProbability, minValue, maxValue);
        }

        /// <summary>
        /// Generate <see cref = "IObservable {Int16}" /> which returns either a random value or null.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="nullProbability">Probability to return null（0.0～1.0）</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<Int16?> RandomOrNull(int count, double nullProbability, Int16 minValue, Int16 maxValue)
        {
            return (RandomInt16 ?? s_DefaultRandomInt16).ValuesOrNull(count, nullProbability, minValue, maxValue);
        }

        #endregion

        #region Int32

        /// <summary>
        /// Random value generator.
        /// </summary>
        public RandomValueGenerator<Int32> RandomInt32 { get; set; } = s_DefaultRandomInt32;

        /// <summary>
        /// Random value generator.
        /// </summary>
        private static readonly RandomValueGenerator<Int32> s_DefaultRandomInt32 = EasyRandomUtility.RandomInt32;

        /// <summary>
        /// Generate <see cref = "IObservable {Int32}" /> which returns a random value.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<Int32> Random(int count, Int32 minValue, Int32 maxValue)
        {
            return (RandomInt32 ?? s_DefaultRandomInt32).Values(count, minValue, maxValue);
        }

        /// <summary>
        /// Generate <see cref = "IObservable {Int32}" /> which returns either a random value or a default value.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="defaultProbability">Probability to return default value.（0.0～1.0）</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<Int32> RandomOrDefault(int count, double defaultProbability, Int32 minValue, Int32 maxValue)
        {
            return (RandomInt32 ?? s_DefaultRandomInt32).ValuesOrDefault(count, defaultProbability, minValue, maxValue);
        }

        /// <summary>
        /// Generate <see cref = "IObservable {Int32}" /> which returns either a random value or null.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="nullProbability">Probability to return null（0.0～1.0）</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<Int32?> RandomOrNull(int count, double nullProbability, Int32 minValue, Int32 maxValue)
        {
            return (RandomInt32 ?? s_DefaultRandomInt32).ValuesOrNull(count, nullProbability, minValue, maxValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        private int NextRandomInt32(int minValue, int maxValue)
        {
            return (RandomInt32 ?? s_DefaultRandomInt32).NextValue(minValue, maxValue);
        }

        #endregion

        #region Int64

        /// <summary>
        /// Random value generator.
        /// </summary>
        public RandomValueGenerator<Int64> RandomInt64 { get; set; } = s_DefaultRandomInt64;

        /// <summary>
        /// Random value generator.
        /// </summary>
        private static readonly RandomValueGenerator<Int64> s_DefaultRandomInt64 = EasyRandomUtility.RandomInt64;

        /// <summary>
        /// Generate <see cref = "IObservable {Int64}" /> which returns a random value.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<Int64> Random(int count, Int64 minValue, Int64 maxValue)
        {
            return (RandomInt64 ?? s_DefaultRandomInt64).Values(count, minValue, maxValue);
        }

        /// <summary>
        /// Generate <see cref = "IObservable {Int64}" /> which returns either a random value or a default value.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="defaultProbability">Probability to return default value.（0.0～1.0）</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<Int64> RandomOrDefault(int count, double defaultProbability, Int64 minValue, Int64 maxValue)
        {
            return (RandomInt64 ?? s_DefaultRandomInt64).ValuesOrDefault(count, defaultProbability, minValue, maxValue);
        }

        /// <summary>
        /// Generate <see cref = "IObservable {Int64}" /> which returns either a random value or null.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="nullProbability">Probability to return null（0.0～1.0）</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<Int64?> RandomOrNull(int count, double nullProbability, Int64 minValue, Int64 maxValue)
        {
            return (RandomInt64 ?? s_DefaultRandomInt64).ValuesOrNull(count, nullProbability, minValue, maxValue);
        }

        #endregion

        #region Single

        /// <summary>
        /// Random value generator.
        /// </summary>
        public RandomValueGenerator<Single> RandomSingle { get; set; } = s_DefaultRandomSingle;

        /// <summary>
        /// Random value generator.
        /// </summary>
        private static readonly RandomValueGenerator<Single> s_DefaultRandomSingle = EasyRandomUtility.RandomSingle;

        /// <summary>
        /// Generate <see cref = "IObservable {Single}" /> which returns a random value.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<Single> Random(int count, Single minValue, Single maxValue)
        {
            return (RandomSingle ?? s_DefaultRandomSingle).Values(count, minValue, maxValue);
        }

        /// <summary>
        /// Generate <see cref = "IObservable {Single}" /> which returns either a random value or a default value.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="defaultProbability">Probability to return default value.（0.0～1.0）</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<Single> RandomOrDefault(int count, double defaultProbability, Single minValue, Single maxValue)
        {
            return (RandomSingle ?? s_DefaultRandomSingle).ValuesOrDefault(count, defaultProbability, minValue, maxValue);
        }

        /// <summary>
        /// Generate <see cref = "IObservable {Single}" /> which returns either a random value or null.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="nullProbability">Probability to return null（0.0～1.0）</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<Single?> RandomOrNull(int count, double nullProbability, Single minValue, Single maxValue)
        {
            return (RandomSingle ?? s_DefaultRandomSingle).ValuesOrNull(count, nullProbability, minValue, maxValue);
        }

        #endregion

        #region Double

        /// <summary>
        /// Random value generator.
        /// </summary>
        public RandomValueGenerator<Double> RandomDouble { get; set; } = s_DefaultRandomDouble;

        /// <summary>
        /// Random value generator.
        /// </summary>
        private static readonly RandomValueGenerator<Double> s_DefaultRandomDouble = EasyRandomUtility.RandomDouble;

        /// <summary>
        /// Generate <see cref = "IObservable {Double}" /> which returns a random value.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<Double> Random(int count, Double minValue, Double maxValue)
        {
            return (RandomDouble ?? s_DefaultRandomDouble).Values(count, minValue, maxValue);
        }

        /// <summary>
        /// Generate <see cref = "IObservable {Double}" /> which returns either a random value or a default value.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="defaultProbability">Probability to return default value.（0.0～1.0）</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<Double> RandomOrDefault(int count, double defaultProbability, Double minValue, Double maxValue)
        {
            return (RandomDouble ?? s_DefaultRandomDouble).ValuesOrDefault(count, defaultProbability, minValue, maxValue);
        }

        /// <summary>
        /// Generate <see cref = "IObservable {Double}" /> which returns either a random value or null.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="nullProbability">Probability to return null（0.0～1.0）</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<Double?> RandomOrNull(int count, double nullProbability, Double minValue, Double maxValue)
        {
            return (RandomDouble ?? s_DefaultRandomDouble).ValuesOrNull(count, nullProbability, minValue, maxValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        private double NextRandomDouble(double minValue, double maxValue)
        {
            return (RandomDouble ?? s_DefaultRandomDouble).NextValue(minValue, maxValue);
        }

        #endregion

        #region Decimal

        /// <summary>
        /// Random value generator.
        /// </summary>
        public RandomValueGenerator<Decimal> RandomDecimal { get; set; } = s_DefaultRandomDecimal;

        /// <summary>
        /// Random value generator.
        /// </summary>
        private static readonly RandomValueGenerator<Decimal> s_DefaultRandomDecimal = EasyRandomUtility.RandomDecimal;

        /// <summary>
        /// Generate <see cref = "IObservable {Decimal}" /> which returns a random value.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<Decimal> Random(int count, Decimal minValue, Decimal maxValue)
        {
            return (RandomDecimal ?? s_DefaultRandomDecimal).Values(count, minValue, maxValue);
        }

        /// <summary>
        /// Generate <see cref = "IObservable {Decimal}" /> which returns either a random value or a default value.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="defaultProbability">Probability to return default value.（0.0～1.0）</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<Decimal> RandomOrDefault(int count, double defaultProbability, Decimal minValue, Decimal maxValue)
        {
            return (RandomDecimal ?? s_DefaultRandomDecimal).ValuesOrDefault(count, defaultProbability, minValue, maxValue);
        }

        /// <summary>
        /// Generate <see cref = "IObservable {Decimal}" /> which returns either a random value or null.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="nullProbability">Probability to return null（0.0～1.0）</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<Decimal?> RandomOrNull(int count, double nullProbability, Decimal minValue, Decimal maxValue)
        {
            return (RandomDecimal ?? s_DefaultRandomDecimal).ValuesOrNull(count, nullProbability, minValue, maxValue);
        }

        #endregion

        #region DateTime

        /// <summary>
        /// Random value generator.
        /// </summary>
        public RandomValueGenerator<DateTime> RandomDateTime { get; set; } = s_DefaultRandomDateTime;

        /// <summary>
        /// Random value generator.
        /// </summary>
        private static readonly RandomValueGenerator<DateTime> s_DefaultRandomDateTime = EasyRandomUtility.RandomDateTime;

        /// <summary>
        /// Generate <see cref = "IObservable {DateTime}" /> which returns a random value.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<DateTime> Random(int count, DateTime minValue, DateTime maxValue)
        {
            return (RandomDateTime ?? s_DefaultRandomDateTime).Values(count, minValue, maxValue);
        }

        /// <summary>
        /// Generate <see cref = "IObservable {DateTime}" /> which returns either a random value or a default value.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="defaultProbability">Probability to return default value.（0.0～1.0）</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<DateTime> RandomOrDefault(int count, double defaultProbability, DateTime minValue, DateTime maxValue)
        {
            return (RandomDateTime ?? s_DefaultRandomDateTime).ValuesOrDefault(count, defaultProbability, minValue, maxValue);
        }

        /// <summary>
        /// Generate <see cref = "IObservable {DateTime}" /> which returns either a random value or null.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="nullProbability">Probability to return null（0.0～1.0）</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<DateTime?> RandomOrNull(int count, double nullProbability, DateTime minValue, DateTime maxValue)
        {
            return (RandomDateTime ?? s_DefaultRandomDateTime).ValuesOrNull(count, nullProbability, minValue, maxValue);
        }

        #endregion

        #region Guid

        /// <summary>
        /// Random value generator.
        /// </summary>
        private static readonly RandomValueGenerator<Guid> s_DefaultRandomGuid = EasyRandomUtility.RandomGuid;

        /// <summary>
        /// Generate <see cref = "IObservable {Guid}" /> which returns a random value.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <returns></returns>
        public IObservable<Guid> RandomGuid(int count)
        {
            return s_DefaultRandomGuid.Values(count);
        }

        /// <summary>
        /// Generate <see cref = "IObservable {Guid}" /> which returns either a random value or a default value.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="defaultProbability">Probability to return default value.（0.0～1.0）</param>
        /// <returns></returns>
        public IObservable<Guid> RandomGuidOrDefault(int count, double defaultProbability)
        {
            return s_DefaultRandomGuid.ValuesOrDefault(count, defaultProbability);
        }

        /// <summary>
        /// Generate <see cref = "IObservable {Guid}" /> which returns either a random value or null.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="nullProbability">Probability to return null（0.0～1.0）</param>
        /// <returns></returns>
        public IObservable<Guid?> RandomGuidOrNull(int count, double nullProbability)
        {
            return s_DefaultRandomGuid.ValuesOrNull(count, nullProbability);
        }

        #endregion

        #endregion

        #region Any

        /// <summary>
        /// Generate <see cref = "IObservable{T}" /> which returns one of the specified values.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <typeparam name="T">Type of value to generate.</typeparam>
        /// <param name="expectedValues">The expected values.</param>
        /// <returns></returns>
        public IObservable<T> Any<T>(int count, params T[] expectedValues)
        {
            return Observable.Generate(0, i => i < count, i => ++i, i => NextAny(expectedValues));
        }

        /// <summary>
        /// Generate <see cref = "IObservable{T}" /> which returns one of the specified values.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <typeparam name="T">Type of value to generate.</typeparam>
        /// <param name="expectedValues">The expected values.</param>
        /// <returns></returns>
        public IObservable<T> Any<T>(int count, IList<T> expectedValues)
        {
            return Observable.Generate(0, i => i < count, i => ++i, i => NextAny(expectedValues));
        }

        /// <summary>
        /// Returns one of the specified values.
        /// </summary>
        /// <typeparam name="T">Type of value to generate.</typeparam>
        /// <param name="expectedValues">The expected values.</param>
        /// <returns></returns>
        private T NextAny<T>(IList<T> expectedValues)
        {
            return expectedValues[NextRandomInt32(0, expectedValues.Count - 1)];
        }

        #endregion

        #region AnyWithProbability

        /// <summary>
        /// Generate <see cref = "IObservable{T}" /> which returns one of the specified values.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <typeparam name="T">Type of value to generate.</typeparam>
        /// <param name="expectedValues">The expected values.</param>
        /// <param name="probabilities">Probability to return each value.（0.0～1.0）</param>
        /// <returns></returns>
        public IObservable<T> AnyWithProbability<T>(int count, IList<T> expectedValues, IList<double> probabilities)
        {
            return Observable.Generate(0, i => i < count, i => ++i, i => NextAny(expectedValues, probabilities));
        }

        /// <summary>
        /// Returns one of the specified values.
        /// </summary>
        /// <typeparam name="T">Type of value to generate.</typeparam>
        /// <param name="expectedValues">The expected values.</param>
        /// <param name="probabilities">Probability to return each value.（0.0～1.0）</param>
        /// <returns></returns>
        private T NextAny<T>(IList<T> expectedValues, IList<double> probabilities)
        {
            double probability = NextRandomDouble(0, 1);

            double summary = 0;

            for (int i = 0; i < probabilities.Count; ++i)
            {
                summary += probabilities[i];
                if (probability <= summary) { return expectedValues[i]; }
            }

            if (expectedValues.Count > probabilities.Count)
            {
                return expectedValues[probabilities.Count];
            }
            else
            {
                return default(T);
            }
        }

        #endregion

        #region Each

        /// <summary>
        /// Generate <see cref = "IObservable{T}" /> which returns the specified values in order.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <typeparam name="T">Type of value to generate.</typeparam>
        /// <param name="expectedValues">The expected values.</param>
        /// <returns></returns>
        public IObservable<T> Each<T>(int count, params T[] expectedValues)
        {
            return Each(count, (IList<T>)expectedValues);
        }

        /// <summary>
        /// Generate <see cref = "IObservable{T}" /> which returns the specified values in order.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <typeparam name="T">Type of value to generate.</typeparam>
        /// <param name="expectedValues">The expected values.</param>
        /// <returns></returns>
        public IObservable<T> Each<T>(int count, IList<T> expectedValues)
        {
            int valuesCount = expectedValues.Count;
            return Observable.Generate(0, i => i < count, i => ++i, i =>
            {
                Math.DivRem(i, valuesCount, out int r);
                return expectedValues[r];
            }
            );
        }

        #endregion

    }

}
