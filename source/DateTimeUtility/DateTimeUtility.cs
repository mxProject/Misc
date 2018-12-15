using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject
{

    public static class DateTimeUtility
    {

        private static readonly string OutOfRangeMessage = "The calculated value is a DateTime that can not be expressed.";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateValue"></param>
        /// <param name="nextDate"></param>
        /// <returns></returns>
        public static int GetMonthDiff(DateTime dateValue, DateTime nextDate)
        {
            return (nextDate.Year - dateValue.Year) * 12 + (nextDate.Month - dateValue.Month);
        }

        #region fiscal year

        /// <summary>
        /// Gets the fiscal year.
        /// </summary>
        /// <param name="dateValue"></param>
        /// <param name="startMonth">Start month of fiscal year.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// Month value is invalid.
        /// </exception>
        public static int GetFiscalYear(DateTime dateValue, int startMonth)
        {
            AssertMonth(startMonth);

            if (dateValue.Month < startMonth)
            {
                return dateValue.Year - 1;
            }
            else
            {
                return dateValue.Year;
            }
        }

        /// <summary>
        /// Gets the first date of the fiscal year.
        /// </summary>
        /// <param name="dateValue"></param>
        /// <param name="startMonth">Start month of fiscal year.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// Month value is invalid.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DateTime GetFirstDateOfFiscalYear(DateTime dateValue, int startMonth)
        {
            AssertMonth(startMonth);

            try
            {
                return new DateTime(GetFiscalYear(dateValue, startMonth), startMonth, 1, 0, 0, 0, dateValue.Kind);
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException(string.Format("{0} dateValue:{1:yyyy/MM/dd} startMonth:{2}", OutOfRangeMessage, dateValue, startMonth), ex);
            }
        }

        /// <summary>
        /// Gets the last date of the fiscal year.
        /// </summary>
        /// <param name="dateValue"></param>
        /// <param name="startMonth">Start month of fiscal year.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DateTime GetLastDateOfFiscalYear(DateTime dateValue, int startMonth)
        {
            AssertMonth(startMonth);

            try
            {
                int year = GetFiscalYear(dateValue, startMonth);

                if (startMonth == 1)
                {
                    return new DateTime(year, 12, 31, 0, 0, 0, dateValue.Kind);
                }
                else
                {
                    return new DateTime(year + 1, startMonth - 1, DateTime.DaysInMonth(year + 1, startMonth - 1), 0, 0, 0, dateValue.Kind);
                }
            }
            catch ( Exception ex)
            {
                throw new ArgumentOutOfRangeException(string.Format("{0} dateValue:{1:yyyy/MM/dd} startMonth:{2}", OutOfRangeMessage, dateValue, startMonth), ex);
            }
        }

        #endregion

        #region quarter period

        /// <summary>
        /// Gets the quarterly period.
        /// </summary>
        /// <param name="dateValue"></param>
        /// <param name="startMonth">Start month of fiscal year.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// Month value is invalid.
        /// </exception>
        public static QuarterPeriod GetQuarterPeriod(DateTime dateValue, int startMonth)
        {
            AssertMonth(startMonth);

            int year = dateValue.GetFiscalYear(startMonth);

            foreach (QuarterPeriod period in EnumerateQuarterPeriods())
            {
                bool validFirstDate = TryGetFirstDateOfQuarter(dateValue.Kind, year, startMonth, period, false, out DateTime firstDate, out string errorMessage);
                bool validLastDate = TryGetLastDateOfQuarter(dateValue.Kind, year, startMonth, period, false, out DateTime lastDate, out errorMessage);

                if (validFirstDate && validLastDate)
                {
                    if (firstDate <= dateValue && dateValue <= lastDate) { return period; }
                }
                else if (validFirstDate)
                {
                    if (firstDate <= dateValue) { return period; }
                }
                else if (validLastDate)
                {
                    if (dateValue <= lastDate) { return period; }
                }
                else
                {
                    continue;
                }
            }

            throw new ArgumentOutOfRangeException(string.Format("{0} dateValue:{1:yyyy/MM/dd} startMonth:{2}", OutOfRangeMessage, dateValue, startMonth));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<QuarterPeriod> EnumerateQuarterPeriods()
        {
            yield return QuarterPeriod.First;
            yield return QuarterPeriod.Second;
            yield return QuarterPeriod.Third;
            yield return QuarterPeriod.Fourth;
        }

        #endregion

        #region date of quarter

        /// <summary>
        /// Gets the first date of the quarter.
        /// </summary>
        /// <param name="fiscalYear">Fiscal year.</param>
        /// <param name="startMonth">Start month of fiscal year.</param>
        /// <param name="period">Quarterly period.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// Year value is invalid.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Month value is invalid.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Period value is invalid.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DateTime GetFirstDateOfQuarter(int fiscalYear, int startMonth, QuarterPeriod period)
        {
            AssertYear(fiscalYear);
            AssertMonth(startMonth);
            AssertPeriod(period);

            if (TryGetFirstDateOfQuarter(DateTimeKind.Unspecified, fiscalYear, startMonth, period, false, out DateTime result, out string errorMessage))
            {
                return result;
            }
            else
            {
                throw new ArgumentOutOfRangeException(string.Format("{0} fiscalYear:{1} startMonth:{2} period:{3}", OutOfRangeMessage, fiscalYear, startMonth, period));
            }
        }

        /// <summary>
        /// Gets the last date of the quarter.
        /// </summary>
        /// <param name="fiscalYear">Fiscal year.</param>
        /// <param name="startMonth">Start month of fiscal year.</param>
        /// <param name="period">Quarterly period.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// Year value is invalid.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Month value is invalid.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Period value is invalid.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DateTime GetLastDateOfQuarter(int fiscalYear, int startMonth, QuarterPeriod period)
        {
            AssertYear(fiscalYear);
            AssertMonth(startMonth);
            AssertPeriod(period);

            if (TryGetLastDateOfQuarter(DateTimeKind.Unspecified, fiscalYear, startMonth, period, false, out DateTime result, out string errorMessage))
            {
                return result;
            }
            else
            {
                throw new ArgumentOutOfRangeException(string.Format("{0} fiscalYear:{1} startMonth:{2} period:{3}", OutOfRangeMessage, fiscalYear, startMonth, period));
            }
        }

        /// <summary>
        /// Gets the first date of the quarter.
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="year"></param>
        /// <param name="startMonth"></param>
        /// <param name="period"></param>
        /// <param name="returnMinimum"></param>
        /// <param name="result"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool TryGetFirstDateOfQuarter(DateTimeKind kind, int year, int startMonth, QuarterPeriod period, bool returnMinimum, out DateTime result, out string errorMessage)
        {

            if (!ValidateMonth(startMonth, out errorMessage))
            {
                result = DateTime.MinValue;
                return false;
            }
            if (!ValidatePeriod(period, out errorMessage))
            {
                result = DateTime.MinValue;
                return false;
            }

            int month = startMonth + ((int)period - 1) * 3;

            if (month > 12)
            {
                ++year;
                month -= 12;
            }

            if (returnMinimum && year < 1)
            {
                result = DateTime.MinValue;
                return true;
            }

            if (!ValidateYear(year, out errorMessage))
            {
                result = DateTime.MinValue;
                return false;
            }

            result = new DateTime(year, month, 1, 0, 0, 0, kind);
            return true;

        }

        /// <summary>
        /// Gets the last date of the quarter.
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="year"></param>
        /// <param name="startMonth"></param>
        /// <param name="period"></param>
        /// <param name="returnMaximum"></param>
        /// <param name="result"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool TryGetLastDateOfQuarter(DateTimeKind kind, int year, int startMonth, QuarterPeriod period, bool returnMaximum, out DateTime result, out string errorMessage)
        {

            if (!ValidateMonth(startMonth, out errorMessage))
            {
                result = DateTime.MinValue;
                return false;
            }
            if (!ValidatePeriod(period, out errorMessage))
            {
                result = DateTime.MinValue;
                return false;
            }

            int month = startMonth + ((int)period - 1) * 3 + 2;

            if (month > 12)
            {
                ++year;
                month -= 12;
            }

            if (returnMaximum && year > 9999)
            {
                result = DateTime.MaxValue;
                return true;
            }

            if (!ValidateYear(year, out errorMessage))
            {
                result = DateTime.MinValue;
                return false;
            }

            result = new DateTime(year, month, DateTime.DaysInMonth(year, month), 0, 0, 0, kind);
            return true;

        }

        /// <summary>
        /// Gets the first date of the quarter.
        /// </summary>
        /// <param name="dateValue"></param>
        /// <param name="startMonth">Start month of fiscal year.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// Month value is invalid.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DateTime GetFirstDateOfQuarter(DateTime dateValue, int startMonth)
        {
            AssertMonth(startMonth);

            int year = dateValue.GetFiscalYear(startMonth);

            foreach (QuarterPeriod period in EnumerateQuarterPeriods())
            {
                bool validFirstDate = TryGetFirstDateOfQuarter(dateValue.Kind, year, startMonth, period, false, out DateTime firstDate, out string errorMessage);
                bool validLastDate = TryGetLastDateOfQuarter(dateValue.Kind, year, startMonth, period, false, out DateTime lastDate, out errorMessage);

                if (validFirstDate && validLastDate)
                {
                    if (firstDate <= dateValue && dateValue <= lastDate) { return firstDate; }
                }
                else if (validFirstDate)
                {
                    if (firstDate <= dateValue) { return firstDate; }
                }
                else if (validLastDate)
                {
                    continue;
                }
                else
                {
                    continue;
                }
            }

            throw new ArgumentOutOfRangeException(string.Format("{0} dateValue:{1:yyyy/MM/dd} startMonth:{2}", OutOfRangeMessage, dateValue, startMonth));
        }

        /// <summary>
        /// Gets the last date of the quarter.
        /// </summary>
        /// <param name="dateValue"></param>
        /// <param name="startMonth">Start month of fiscal year.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// Month value is invalid.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DateTime GetLastDateOfQuarter(DateTime dateValue, int startMonth)
        {
            AssertMonth(startMonth);

            int year = dateValue.GetFiscalYear(startMonth);

            foreach (QuarterPeriod period in EnumerateQuarterPeriods())
            {
                bool validFirstDate = TryGetFirstDateOfQuarter(dateValue.Kind, year, startMonth, period, false, out DateTime firstDate, out string errorMessage);
                bool validLastDate = TryGetLastDateOfQuarter(dateValue.Kind, year, startMonth, period, false, out DateTime lastDate, out errorMessage);

                if (validFirstDate && validLastDate)
                {
                    if (firstDate <= dateValue && dateValue <= lastDate) { return lastDate; }
                }
                else if (validFirstDate)
                {
                    continue;
                }
                else if (validLastDate)
                {
                    if (dateValue <= lastDate) { return lastDate; }
                }
                else
                {
                    continue;
                }
            }

            throw new ArgumentOutOfRangeException(string.Format("{0} dateValue:{1:yyyy/MM/dd} startMonth:{2}", OutOfRangeMessage, dateValue, startMonth));
        }

        #endregion

        #region date of year

        /// <summary>
        /// Gets the first date of the year.
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// Year value is invalid.
        /// </exception>
        public static DateTime GetFirstDateOfYear(int year)
        {
            AssertYear(year);
            return new DateTime(year, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);
        }

        /// <summary>
        /// Gets the first date of the year.
        /// </summary>
        /// <param name="dateValue"></param>
        /// <returns></returns>
        public static DateTime GetFirstDateOfYear(DateTime dateValue)
        {
            return dateValue.Date.AddDays(-dateValue.DayOfYear + 1);
        }

        /// <summary>
        /// Gets the last date of the year.
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// Year value is invalid.
        /// </exception>
        public static DateTime GetLastDateOfYear(int year)
        {
            AssertYear(year);
            return new DateTime(year, 12, 31, 0, 0, 0, DateTimeKind.Unspecified);
        }

        /// <summary>
        /// Gets the last date of the year.
        /// </summary>
        /// <param name="dateValue"></param>
        /// <returns></returns>
        public static DateTime GetLastDateOfYear(DateTime dateValue)
        {
            return dateValue.Date.AddDays(-dateValue.DayOfYear + (DateTime.IsLeapYear(dateValue.Year) ? 366 : 365));
        }

        #endregion

        #region date of month

        /// <summary>
        /// Gets the first date of the month.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// Year value is invalid.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Month value is invalid.
        /// </exception>
        public static DateTime GetFirstDateOfMonth(int year, int month)
        {
            AssertYear(year);
            AssertMonth(month);
            return new DateTime(year, month, 1);
        }

        /// <summary>
        /// Gets the first date of the month.
        /// </summary>
        /// <param name="dateValue"></param>
        /// <returns></returns>
        public static DateTime GetFirstDateOfMonth(DateTime dateValue)
        {
            return dateValue.Date.AddDays(-dateValue.Day + 1);
        }

        /// <summary>
        /// Gets the last date of the month.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// Year value is invalid.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Month value is invalid.
        /// </exception>
        public static DateTime GetLastDateOfMonth(int year, int month)
        {
            AssertYear(year);
            AssertMonth(month);
            return new DateTime(year, month, DateTime.DaysInMonth(year, month));
        }

        /// <summary>
        /// Gets the last date of the month.
        /// </summary>
        /// <param name="dateValue"></param>
        /// <returns></returns>
        public static DateTime GetLastDateOfMonth(DateTime dateValue)
        {
            return dateValue.Date.AddDays(-dateValue.Day + DateTime.DaysInMonth(dateValue.Year, dateValue.Month));
        }

        #endregion

        #region date of week

        /// <summary>
        /// Gets the first date of the week.
        /// </summary>
        /// <param name="dateValue"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DateTime GetFirstDateOfWeek(DateTime dateValue)
        {
            try
            {
                return dateValue.Date.AddDays(-(int)dateValue.DayOfWeek);
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException(string.Format("{0} dateValue:{1:yyyy/MM/dd}", OutOfRangeMessage, dateValue), ex);
            }
        }

        /// <summary>
        /// Gets the last date of the week.
        /// </summary>
        /// <param name="dateValue"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DateTime GetLastDateOfWeek(DateTime dateValue)
        {
            try
            {
                return dateValue.Date.AddDays((int)DayOfWeek.Saturday - (int)dateValue.DayOfWeek);
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException(string.Format("{0} dateValue:{1:yyyy/MM/dd}", OutOfRangeMessage, dateValue), ex);
            }
        }

        /// <summary>
        /// Gets the date of the specified day of the week of the current week.
        /// </summary>
        /// <param name="dateValue"></param>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DateTime GetDateOfCurrentWeek(DateTime dateValue, DayOfWeek dayOfWeek)
        {
            try
            {
                return dateValue.Date.AddDays(-(int)dateValue.DayOfWeek + (int)dayOfWeek);
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException(string.Format("{0} dateValue:{1:yyyy/MM/dd} dayOfWeek:{2}", OutOfRangeMessage, dateValue, dayOfWeek), ex);
            }
        }

        /// <summary>
        /// Gets the date of the specified day of the week of the current week.
        /// </summary>
        /// <param name="dateValue"></param>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public static DateTime GetDateOfNextWeek(DateTime dateValue, DayOfWeek dayOfWeek)
        {
            try
            {
                return dateValue.Date.AddDays(-(int)dateValue.DayOfWeek + (int)dayOfWeek + 7);
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException(string.Format("{0} dateValue:{1:yyyy/MM/dd} dayOfWeek:{2}", OutOfRangeMessage, dateValue, dayOfWeek), ex);
            }
        }

        #endregion

        #region unix time

        /// <summary>
        /// Gets the number of seconds that have elapsed since 1970-01-01T00:00:00Z.
        /// </summary>
        /// <param name="dateValue"></param>
        /// <returns></returns>
        public static long ToUnixTimeSeconds(DateTime dateValue)
        {
            AsLocalIfUnspecified(ref dateValue);
            return new DateTimeOffset(dateValue).ToUnixTimeSeconds();
        }

        /// <summary>
        /// Gets the number of milliseconds that have elapsed since 1970-01-01T00:00:00.000Z.
        /// </summary>
        /// <param name="dateValue"></param>
        /// <returns></returns>
        public static long ToUnixTimeMilliseconds(DateTime dateValue)
        {
            AsLocalIfUnspecified(ref dateValue);
            return new DateTimeOffset(dateValue).ToUnixTimeMilliseconds();
        }

        #endregion

        #region dateKind

        /// <summary>
        /// If DateTimeKind is not specified, specify <see cref="DateTime.Local"/>.
        /// </summary>
        /// <param name="dateValue"></param>
        /// <returns></returns>
        public static DateTime AsLocalIfUnspecified(DateTime dateValue)
        {
            AsLocalIfUnspecified(ref dateValue);
            return dateValue;
        }

        /// <summary>
        /// If DateTimeKind is not specified, specify <see cref="DateTime.Local"/>.
        /// </summary>
        /// <param name="dateValue"></param>
        private static void AsLocalIfUnspecified(ref DateTime dateValue)
        {
            if (dateValue.Kind == DateTimeKind.Unspecified)
            {
                dateValue = DateTime.SpecifyKind(dateValue, DateTimeKind.Local);
            }
        }

        #endregion

        #region validation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="period"></param>
        private static void AssertPeriod(QuarterPeriod period)
        {
            if (!ValidatePeriod(period, out string errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        private static void AssertYear(int year)
        {
            if (!ValidateYear(year, out string errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="month"></param>
        private static void AssertMonth(int month)
        {
            if (!ValidateMonth(month, out string errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="period"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool ValidatePeriod(QuarterPeriod period, out string errorMessage)
        {
            if (period == QuarterPeriod.Unknown)
            {
                errorMessage = "Period value is invalid.";
                return false;
            }
            errorMessage = null;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool ValidateYear(int year, out string errorMessage)
        {
            if (year < 1 || 9999 < year)
            {
                errorMessage = "Year value is invalid.";
                return false;
            }
            errorMessage = null;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="month"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool ValidateMonth(int month, out string errorMessage)
        {
            if (month < 1 || 12 < month)
            {
                errorMessage = "Month value is invalid.";
                return false;
            }
            errorMessage = null;
            return true;
        }

        #endregion

    }

    /// <summary>
    /// Quarterly period
    /// </summary>
    public enum QuarterPeriod
    {
        /// <summary>
        /// 
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 
        /// </summary>
        First = 1,

        /// <summary>
        /// 
        /// </summary>
        Second = 2,

        /// <summary>
        /// 
        /// </summary>
        Third = 3,

        /// <summary>
        /// 
        /// </summary>
        Fourth = 4,
    }

}
