using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace TestDataGenerator
{

    /// <summary>
    /// Method that generate value within the specified range.
    /// </summary>
    /// <typeparam name="T">Type of value to generate.</typeparam>
    /// <param name="minValue">Minimum value.</param>
    /// <param name="maxValue">Maximum value.</param>
    /// <returns>A generated value.</returns>
    public delegate T NextRandomValue<T>(T minValue, T maxValue);

    /// <summary>
    /// Random value generator.
    /// </summary>
    /// <typeparam name="T">Type of value to generate.</typeparam>
    public class RandomValueGenerator<T> where T : struct
    {

        #region ctor

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="generator">Method that generate value.</param>
        /// <param name="generatorInRange">Method that generate value within the specified range.</param>
        /// <param name="nullProbabilityGenerator">Method that generates a probability of null.（0.0～1.0）</param>
        public RandomValueGenerator(Func<T> generator, NextRandomValue<T> generatorInRange, Func<double> nullProbabilityGenerator)
        {
            m_Generator = generator;
            m_GeneratorInRange = generatorInRange;
            m_NullProbabilityGenerator = nullProbabilityGenerator;
        }

        #endregion

        private readonly Func<T> m_Generator;
        private readonly NextRandomValue<T> m_GeneratorInRange;
        private readonly Func<double> m_NullProbabilityGenerator;

        #region NextValue

        /// <summary>
        /// Gets the next value.
        /// </summary>
        /// <returns></returns>
        public T NextValue()
        {
            return m_Generator();
        }

        /// <summary>
        /// Gets the next value.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public T NextValue(T minValue, T maxValue)
        {
            return m_GeneratorInRange(minValue, maxValue);
        }

        #endregion

        #region Values

        /// <summary>
        /// Generate <see cref = "IObservable {T}" /> which returns a random value.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <returns></returns>
        public IObservable<T> Values(int count)
        {
            return Observable.Generate(0, i => i < count, i => ++i, i => m_Generator());
        }

        /// <summary>
        /// Generate <see cref = "IObservable {T}" /> which returns a random value.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<T> Values(int count, T minValue, T maxValue)
        {
            return Observable.Generate(0, i => i < count, i => ++i, i => m_GeneratorInRange(minValue, maxValue));
        }

        #endregion

        #region ValuesOrDefault

        /// <summary>
        /// Generate <see cref = "IObservable {T}" /> which returns either a random value or a default value.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="defaultProbability">Probability to return default value.（0.0～1.0）</param>
        /// <returns></returns>
        public IObservable<T> ValuesOrDefault(int count, double defaultProbability)
        {
            return Observable.Generate(0, i => i < count, i => ++i, i =>
            {
                if (IsNextNull(defaultProbability)) { return default(T); }
                return m_Generator();
            }
            );
        }

        /// <summary>
        /// Generate <see cref = "IObservable {T}" /> which returns either a random value or a default value.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="defaultProbability">Probability to return default value.（0.0～1.0）</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<T> ValuesOrDefault(int count, double defaultProbability, T minValue, T maxValue)
        {
            return Observable.Generate(0, i => i < count, i => ++i, i =>
            {
                if (IsNextNull(defaultProbability)) { return default(T); }
                return m_GeneratorInRange(minValue, maxValue);
            }
            );
        }

        #endregion

        #region ValuesOrNull

        /// <summary>
        /// Generate <see cref = "IObservable {T}" /> which returns either a random value or null.
        /// </summary>
        /// <param name="count">値の個数</param>
        /// <param name="nullProbability">Probability to return null.（0.0～1.0）</param>
        /// <returns></returns>
        public IObservable<T?> ValuesOrNull(int count, double nullProbability)
        {
            return Observable.Generate<int, T?>(0, i => i < count, i => ++i, i =>
            {
                if (IsNextNull(nullProbability)) { return null; }
                return m_Generator();
            }
            );
        }

        /// <summary>
        /// Generate <see cref = "IObservable {T}" /> which returns either a random value or null.
        /// </summary>
        /// <param name="count">Number of values to generate.</param>
        /// <param name="nullProbability">Probability to return null.（0.0～1.0）</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns></returns>
        public IObservable<T?> ValuesOrNull(int count, double nullProbability, T minValue, T maxValue)
        {
            return Observable.Generate<int, T?>(0, i => i < count, i => ++i, i =>
            {
                if (IsNextNull(nullProbability)) { return null; }
                return m_GeneratorInRange(minValue, maxValue);
            }
            );
        }

        #endregion

        /// <summary>
        /// Gets whether null is expected as the next value.
        /// </summary>
        /// <param name="probability">Probability to return null（0.0～1.0）</param>
        /// <returns></returns>
        private bool IsNextNull(double probability)
        {
            if (probability <= 0) { return false; }
            if (probability >= 1) { return true; }
            return m_NullProbabilityGenerator() <= probability;
        }

    }

}
