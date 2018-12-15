using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject
{

    public static class DateTimeExtension
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="this"></param>
        /// <param name="nextDate"></param>
        /// <returns></returns>
        public static int GetMonthDiff(this DateTime @this, DateTime nextDate)
        {
            return DateTimeUtility.GetMonthDiff(@this, nextDate);
        }

        #region fiscal year

        /// <summary>
        /// Gets the fiscal year.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="startMonth">Start month of fiscal year.</param>
        /// <returns></returns>
        public static int GetFiscalYear(this DateTime @this, int startMonth)
        {
            return DateTimeUtility.GetFiscalYear(@this, startMonth);
        }

        /// <summary>
        /// Gets the first date of the fiscal year.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="startMonth">Start month of fiscal year.</param>
        /// <returns></returns>
        public static DateTime GetFirstDateOfFiscalYear(this DateTime @this, int startMonth)
        {
            return DateTimeUtility.GetFirstDateOfFiscalYear(@this, startMonth);
        }

        /// <summary>
        /// Gets the last date of the fiscal year.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="startMonth">Start month of fiscal year.</param>
        /// <returns></returns>
        public static DateTime GetLastDateOfFiscalYear(this DateTime @this, int startMonth)
        {
            return DateTimeUtility.GetLastDateOfFiscalYear(@this, startMonth);
        }

        #endregion

        #region quarter period

        /// <summary>
        /// Gets the quarterly period.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="startMonth">Start month of fiscal year.</param>
        /// <returns></returns>
        public static QuarterPeriod GetQuarterPeriod(this DateTime @this, int startMonth)
        {
            return DateTimeUtility.GetQuarterPeriod(@this, startMonth);
        }

        #endregion

        #region date of quarter

        /// <summary>
        /// Gets the first date of the quarter.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="startMonth">Start month of fiscal year.</param>
        /// <returns></returns>
        public static DateTime GetFirstDateOfQuarter(this DateTime @this, int startMonth)
        {
            return DateTimeUtility.GetFirstDateOfQuarter(@this, startMonth);
        }

        /// <summary>
        /// Gets the last date of the quarter.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="startMonth">Start month of fiscal year.</param>
        /// <returns></returns>
        public static DateTime GetLastDateOfQuarter(this DateTime @this, int startMonth)
        {
            return DateTimeUtility.GetLastDateOfQuarter(@this, startMonth);
        }

        #endregion

        #region date of year

        /// <summary>
        /// Gets the first date of the year.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime GetFirstDateOfYear(this DateTime @this)
        {
            return DateTimeUtility.GetFirstDateOfYear(@this);
        }

        /// <summary>
        /// Gets the last date of the year.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime GetLastDateOfYear(this DateTime @this)
        {
            return DateTimeUtility.GetLastDateOfYear(@this);
        }

        #endregion

        #region date of month

        /// <summary>
        /// Gets the first date of the month.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime GetFirstDateOfMonth(this DateTime @this)
        {
            return DateTimeUtility.GetFirstDateOfMonth(@this);
        }

        /// <summary>
        /// Gets the last date of the month.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime GetLastDateOfMonth(this DateTime @this)
        {
            return DateTimeUtility.GetLastDateOfMonth(@this);
        }

        #endregion

        #region date of week

        /// <summary>
        /// Gets the first date of the week.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime GetFirstDateOfWeek(this DateTime @this)
        {
            return DateTimeUtility.GetFirstDateOfWeek(@this);
        }

        /// <summary>
        /// Gets the last date of the week.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime GetLastDateOfWeek(this DateTime @this)
        {
            return DateTimeUtility.GetLastDateOfWeek(@this);
        }

        /// <summary>
        /// Gets the date of the specified day of the week of the current week.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns></returns>
        public static DateTime GetDateOfCurrentWeek(this DateTime @this, DayOfWeek dayOfWeek)
        {
            return DateTimeUtility.GetDateOfCurrentWeek(@this, dayOfWeek);
        }

        /// <summary>
        /// Gets the date of the specified day of the week of the current week.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns></returns>
        public static DateTime GetDateOfNextWeek(this DateTime @this, DayOfWeek dayOfWeek)
        {
            return DateTimeUtility.GetDateOfNextWeek(@this, dayOfWeek);
        }

        #endregion

        #region unix time

        /// <summary>
        /// Gets the number of seconds that have elapsed since 1970-01-01T00:00:00Z.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static long ToUnixTimeSeconds(this DateTime @this)
        {
            return DateTimeUtility.ToUnixTimeSeconds(@this);
        }

        /// <summary>
        /// Gets the number of milliseconds that have elapsed since 1970-01-01T00:00:00.000Z.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static long ToUnixTimeMilliseconds(this DateTime @this)
        {
            return DateTimeUtility.ToUnixTimeMilliseconds(@this);
        }

        #endregion

        #region dateKind

        /// <summary>
        /// If DateTimeKind is not specified, specify <see cref="DateTime.Local"/>.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime AsLocalIfUnspecified(this DateTime @this)
        {
            return DateTimeUtility.AsLocalIfUnspecified(@this);
        }

        #endregion

    }

}
