using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using mxProject;

namespace Test.mxProject.Common
{

    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class TestDateTimeUtility
    {

        #region GetMonthDiff

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetMonthDiff()
        {
            Assert.AreEqual(0, DateTimeUtility.GetMonthDiff(new DateTime(2018, 1, 1), new DateTime(2018, 1, 1)));
            Assert.AreEqual(11, DateTimeUtility.GetMonthDiff(new DateTime(2018, 1, 1), new DateTime(2018, 12, 1)));
            Assert.AreEqual(12, DateTimeUtility.GetMonthDiff(new DateTime(2018, 1, 1), new DateTime(2019, 1, 1)));
            Assert.AreEqual(-1, DateTimeUtility.GetMonthDiff(new DateTime(2018, 1, 1), new DateTime(2017, 12, 31)));
            Assert.AreEqual(-12, DateTimeUtility.GetMonthDiff(new DateTime(2018, 1, 1), new DateTime(2017, 1, 1)));
        }

        #endregion

        #region GetFiscalYear

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetFiscalYear()
        {
            Assert.AreEqual(2017, DateTimeUtility.GetFiscalYear(new DateTime(2018, 1, 1), 4));
            Assert.AreEqual(2018, DateTimeUtility.GetFiscalYear(new DateTime(2018, 4, 1), 4));
            Assert.AreEqual(2018, DateTimeUtility.GetFiscalYear(new DateTime(2018, 12, 1), 4));
            Assert.AreEqual(0, DateTimeUtility.GetFiscalYear(new DateTime(1, 1, 1), 4));
            Assert.AreEqual(9998, DateTimeUtility.GetFiscalYear(new DateTime(9999, 1, 1), 4));

            Assert.AreEqual(2018, DateTimeUtility.GetFiscalYear(new DateTime(2018, 1, 1), 1));
            Assert.AreEqual(2018, DateTimeUtility.GetFiscalYear(new DateTime(2018, 4, 1), 1));
            Assert.AreEqual(2018, DateTimeUtility.GetFiscalYear(new DateTime(2018, 12, 1), 1));
            Assert.AreEqual(1, DateTimeUtility.GetFiscalYear(new DateTime(1, 1, 1), 1));
            Assert.AreEqual(9999, DateTimeUtility.GetFiscalYear(new DateTime(9999, 1, 1), 1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFiscalYear_Failed1()
        {
            Assert.AreEqual(2017, DateTimeUtility.GetFiscalYear(new DateTime(2018, 1, 1), 0));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFiscalYear_Failed2()
        {
            Assert.AreEqual(2017, DateTimeUtility.GetFiscalYear(new DateTime(2018, 1, 1), 13));
        }

        #endregion

        #region GetFirstDateOfFiscalYear

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetFirstDateOfFiscalYear()
        {
            Assert.AreEqual(new DateTime(2017, 4, 1), DateTimeUtility.GetFirstDateOfFiscalYear(new DateTime(2018, 1, 1), 4));
            Assert.AreEqual(new DateTime(2018, 4, 1), DateTimeUtility.GetFirstDateOfFiscalYear(new DateTime(2018, 4, 1), 4));
            Assert.AreEqual(new DateTime(2018, 4, 1), DateTimeUtility.GetFirstDateOfFiscalYear(new DateTime(2018, 12, 31), 4));
            Assert.AreEqual(new DateTime(1, 4, 1), DateTimeUtility.GetFirstDateOfFiscalYear(new DateTime(1, 4, 1), 4));
            Assert.AreEqual(new DateTime(9999, 4, 1), DateTimeUtility.GetFirstDateOfFiscalYear(new DateTime(9999, 12, 31), 4));

            Assert.AreEqual(new DateTime(2018, 1, 1), DateTimeUtility.GetFirstDateOfFiscalYear(new DateTime(2018, 1, 1), 1));
            Assert.AreEqual(new DateTime(2018, 1, 1), DateTimeUtility.GetFirstDateOfFiscalYear(new DateTime(2018, 4, 1), 1));
            Assert.AreEqual(new DateTime(2018, 1, 1), DateTimeUtility.GetFirstDateOfFiscalYear(new DateTime(2018, 12, 31), 1));
            Assert.AreEqual(new DateTime(1, 1, 1), DateTimeUtility.GetFirstDateOfFiscalYear(new DateTime(1, 1, 1), 1));
            Assert.AreEqual(new DateTime(1, 1, 1), DateTimeUtility.GetFirstDateOfFiscalYear(new DateTime(1, 4, 1), 1));
            Assert.AreEqual(new DateTime(9999, 1, 1), DateTimeUtility.GetFirstDateOfFiscalYear(new DateTime(9999, 12, 31), 1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetFirstDateOfFiscalYear_Kind()
        {
            Assert.AreEqual(DateTimeKind.Unspecified, DateTimeUtility.GetFirstDateOfFiscalYear(new DateTime(2018, 1, 1), 4).Kind);
            Assert.AreEqual(DateTimeKind.Local, DateTimeUtility.GetFirstDateOfFiscalYear(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Local), 4).Kind);
            Assert.AreEqual(DateTimeKind.Utc, DateTimeUtility.GetFirstDateOfFiscalYear(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc), 4).Kind);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFirstDateOfFiscalYear_Failed1()
        {
            Assert.AreEqual(new DateTime(2017, 4, 1), DateTimeUtility.GetFirstDateOfFiscalYear(new DateTime(2018, 1, 1), 0));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFirstDateOfFiscalYear_Failed2()
        {
            Assert.AreEqual(new DateTime(2017, 4, 1), DateTimeUtility.GetFirstDateOfFiscalYear(new DateTime(2018, 1, 1), 13));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetFirstDateOfFiscalYear_Failed3()
        {
            Assert.AreEqual(DateTime.MinValue, DateTimeUtility.GetFirstDateOfFiscalYear(new DateTime(1, 1, 1), 4));
        }

        #endregion

        #region GetLastDateOfFiscalYear

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetLastDateOfFiscalYear()
        {
            Assert.AreEqual(new DateTime(2018, 3, 31), DateTimeUtility.GetLastDateOfFiscalYear(new DateTime(2018, 1, 1), 4));
            Assert.AreEqual(new DateTime(2019, 3, 31), DateTimeUtility.GetLastDateOfFiscalYear(new DateTime(2018, 4, 1), 4));
            Assert.AreEqual(new DateTime(2019, 3, 31), DateTimeUtility.GetLastDateOfFiscalYear(new DateTime(2018, 12, 31), 4));
            Assert.AreEqual(new DateTime(1, 3, 31), DateTimeUtility.GetLastDateOfFiscalYear(new DateTime(1, 1, 1), 4));
            Assert.AreEqual(new DateTime(2, 3, 31), DateTimeUtility.GetLastDateOfFiscalYear(new DateTime(1, 4, 1), 4));
            Assert.AreEqual(new DateTime(9999, 3, 31), DateTimeUtility.GetLastDateOfFiscalYear(new DateTime(9999, 3, 1), 4));

            Assert.AreEqual(new DateTime(2018, 12, 31), DateTimeUtility.GetLastDateOfFiscalYear(new DateTime(2018, 1, 1), 1));
            Assert.AreEqual(new DateTime(2018, 12, 31), DateTimeUtility.GetLastDateOfFiscalYear(new DateTime(2018, 4, 1), 1));
            Assert.AreEqual(new DateTime(2018, 12, 31), DateTimeUtility.GetLastDateOfFiscalYear(new DateTime(2018, 12, 31), 1));
            Assert.AreEqual(new DateTime(1, 12, 31), DateTimeUtility.GetLastDateOfFiscalYear(new DateTime(1, 1, 1), 1));
            Assert.AreEqual(new DateTime(1, 12, 31), DateTimeUtility.GetLastDateOfFiscalYear(new DateTime(1, 4, 1), 1));
            Assert.AreEqual(new DateTime(9999, 12, 31), DateTimeUtility.GetLastDateOfFiscalYear(new DateTime(9999, 3, 1), 1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetLastDateOfFiscalYear_Kind()
        {
            Assert.AreEqual(DateTimeKind.Unspecified, DateTimeUtility.GetLastDateOfFiscalYear(new DateTime(2018, 1, 1), 4).Kind);
            Assert.AreEqual(DateTimeKind.Local, DateTimeUtility.GetLastDateOfFiscalYear(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Local), 4).Kind);
            Assert.AreEqual(DateTimeKind.Utc, DateTimeUtility.GetLastDateOfFiscalYear(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc), 4).Kind);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetLastDateOfFiscalYear_Failed1()
        {
            Assert.AreEqual(new DateTime(2017, 4, 1), DateTimeUtility.GetLastDateOfFiscalYear(new DateTime(2018, 1, 1), 0));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetLastDateOfFiscalYear_Failed2()
        {
            Assert.AreEqual(new DateTime(2017, 4, 1), DateTimeUtility.GetLastDateOfFiscalYear(new DateTime(2018, 1, 1), 13));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetLastDateOfFiscalYear_Failed3()
        {
            Assert.AreEqual(DateTime.MaxValue, DateTimeUtility.GetLastDateOfFiscalYear(new DateTime(9999, 4, 1), 4));
        }

        #endregion

        #region GetFirstDateOfQuarter

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetFirstDateOfQuarter()
        {
            Assert.AreEqual(new DateTime(2018, 4, 1), DateTimeUtility.GetFirstDateOfQuarter(2018, 4, QuarterPeriod.First));
            Assert.AreEqual(new DateTime(2018, 7, 1), DateTimeUtility.GetFirstDateOfQuarter(2018, 4, QuarterPeriod.Second));
            Assert.AreEqual(new DateTime(2018, 10, 1), DateTimeUtility.GetFirstDateOfQuarter(2018, 4, QuarterPeriod.Third));
            Assert.AreEqual(new DateTime(2019, 1, 1), DateTimeUtility.GetFirstDateOfQuarter(2018, 4, QuarterPeriod.Fourth));

            Assert.AreEqual(new DateTime(2018, 1, 1), DateTimeUtility.GetFirstDateOfQuarter(2018, 1, QuarterPeriod.First));
            Assert.AreEqual(new DateTime(2018, 4, 1), DateTimeUtility.GetFirstDateOfQuarter(2018, 1, QuarterPeriod.Second));
            Assert.AreEqual(new DateTime(2018, 7, 1), DateTimeUtility.GetFirstDateOfQuarter(2018, 1, QuarterPeriod.Third));
            Assert.AreEqual(new DateTime(2018, 10, 1), DateTimeUtility.GetFirstDateOfQuarter(2018, 1, QuarterPeriod.Fourth));

            Assert.AreEqual(new DateTime(2018, 1, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(2018, 1, 1), 4));
            Assert.AreEqual(new DateTime(2018, 1, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(2018, 3, 31), 4));
            Assert.AreEqual(new DateTime(2018, 4, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(2018, 4, 1), 4));
            Assert.AreEqual(new DateTime(2018, 4, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(2018, 6, 30), 4));
            Assert.AreEqual(new DateTime(2018, 7, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(2018, 7, 1), 4));
            Assert.AreEqual(new DateTime(2018, 7, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(2018, 9, 30), 4));
            Assert.AreEqual(new DateTime(2018, 10, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(2018, 10, 1), 4));
            Assert.AreEqual(new DateTime(2018, 10, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(2018, 12, 31), 4));
            Assert.AreEqual(new DateTime(1, 1, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(1, 1, 1), 4));
            Assert.AreEqual(new DateTime(9999, 10, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(9999, 12, 31), 4));

            Assert.AreEqual(new DateTime(2017, 11, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(2018, 1, 1), 2));
            Assert.AreEqual(new DateTime(2018, 2, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(2018, 3, 31), 2));
            Assert.AreEqual(new DateTime(2018, 2, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(2018, 4, 1), 2));
            Assert.AreEqual(new DateTime(2018, 5, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(2018, 6, 30), 2));
            Assert.AreEqual(new DateTime(2018, 5, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(2018, 7, 1), 2));
            Assert.AreEqual(new DateTime(2018, 8, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(2018, 9, 30), 2));
            Assert.AreEqual(new DateTime(2018, 8, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(2018, 10, 1), 2));
            Assert.AreEqual(new DateTime(2018, 11, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(2018, 12, 31), 2));
            // Assert.AreEqual(new DateTime(0, 11, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(1, 1, 1), 2));
            Assert.AreEqual(new DateTime(9999, 11, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(9999, 12, 31), 2));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetFirstDateOfQuarter_Kind()
        {
            Assert.AreEqual(DateTimeKind.Unspecified, DateTimeUtility.GetFirstDateOfQuarter(new DateTime(2018, 1, 1), 4).Kind);
            Assert.AreEqual(DateTimeKind.Local, DateTimeUtility.GetFirstDateOfQuarter(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Local), 4).Kind);
            Assert.AreEqual(DateTimeKind.Utc, DateTimeUtility.GetFirstDateOfQuarter(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc), 4).Kind);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFirstDateOfQuarter_Failed1()
        {
            Assert.AreEqual(new DateTime(2018, 1, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(2018, 1, 1), 0));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFirstDateOfQuarter_Failed2()
        {
            Assert.AreEqual(new DateTime(2018, 1, 1), DateTimeUtility.GetFirstDateOfQuarter(new DateTime(2018, 1, 1), 13));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetFirstDateOfQuarter_Failed3()
        {
            Assert.AreEqual(DateTime.MinValue, DateTimeUtility.GetFirstDateOfQuarter(new DateTime(1, 1, 1), 2));
        }

        #endregion

        #region GetLastDateOfQuarter

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetLastDateOfQuarter()
        {
            Assert.AreEqual(new DateTime(2018, 6, 30), DateTimeUtility.GetLastDateOfQuarter(2018, 4, QuarterPeriod.First));
            Assert.AreEqual(new DateTime(2018, 9, 30), DateTimeUtility.GetLastDateOfQuarter(2018, 4, QuarterPeriod.Second));
            Assert.AreEqual(new DateTime(2018, 12, 31), DateTimeUtility.GetLastDateOfQuarter(2018, 4, QuarterPeriod.Third));
            Assert.AreEqual(new DateTime(2019, 3, 31), DateTimeUtility.GetLastDateOfQuarter(2018, 4, QuarterPeriod.Fourth));

            Assert.AreEqual(new DateTime(2018, 3, 31), DateTimeUtility.GetLastDateOfQuarter(2018, 1, QuarterPeriod.First));
            Assert.AreEqual(new DateTime(2018, 6, 30), DateTimeUtility.GetLastDateOfQuarter(2018, 1, QuarterPeriod.Second));
            Assert.AreEqual(new DateTime(2018, 9, 30), DateTimeUtility.GetLastDateOfQuarter(2018, 1, QuarterPeriod.Third));
            Assert.AreEqual(new DateTime(2018, 12, 31), DateTimeUtility.GetLastDateOfQuarter(2018, 1, QuarterPeriod.Fourth));

            Assert.AreEqual(new DateTime(2018, 3, 31), DateTimeUtility.GetLastDateOfQuarter(new DateTime(2018, 1, 1), 4));
            Assert.AreEqual(new DateTime(2018, 3, 31), DateTimeUtility.GetLastDateOfQuarter(new DateTime(2018, 3, 31), 4));
            Assert.AreEqual(new DateTime(2018, 6, 30), DateTimeUtility.GetLastDateOfQuarter(new DateTime(2018, 4, 1), 4));
            Assert.AreEqual(new DateTime(2018, 6, 30), DateTimeUtility.GetLastDateOfQuarter(new DateTime(2018, 6, 30), 4));
            Assert.AreEqual(new DateTime(2018, 9, 30), DateTimeUtility.GetLastDateOfQuarter(new DateTime(2018, 7, 1), 4));
            Assert.AreEqual(new DateTime(2018, 9, 30), DateTimeUtility.GetLastDateOfQuarter(new DateTime(2018, 9, 30), 4));
            Assert.AreEqual(new DateTime(2018, 12, 31), DateTimeUtility.GetLastDateOfQuarter(new DateTime(2018, 10, 1), 4));
            Assert.AreEqual(new DateTime(2018, 12, 31), DateTimeUtility.GetLastDateOfQuarter(new DateTime(2018, 12, 31), 4));
            Assert.AreEqual(new DateTime(1, 3, 31), DateTimeUtility.GetLastDateOfQuarter(new DateTime(1, 1, 1), 4));
            Assert.AreEqual(new DateTime(9999, 12, 31), DateTimeUtility.GetLastDateOfQuarter(new DateTime(9999, 12, 31), 4));

            Assert.AreEqual(new DateTime(2018, 1, 31), DateTimeUtility.GetLastDateOfQuarter(new DateTime(2018, 1, 1), 2));
            Assert.AreEqual(new DateTime(2018, 4, 30), DateTimeUtility.GetLastDateOfQuarter(new DateTime(2018, 3, 31), 2));
            Assert.AreEqual(new DateTime(2018, 4, 30), DateTimeUtility.GetLastDateOfQuarter(new DateTime(2018, 4, 1), 2));
            Assert.AreEqual(new DateTime(2018, 7, 31), DateTimeUtility.GetLastDateOfQuarter(new DateTime(2018, 6, 30), 2));
            Assert.AreEqual(new DateTime(2018, 7, 31), DateTimeUtility.GetLastDateOfQuarter(new DateTime(2018, 7, 1), 2));
            Assert.AreEqual(new DateTime(2018, 10, 31), DateTimeUtility.GetLastDateOfQuarter(new DateTime(2018, 9, 30), 2));
            Assert.AreEqual(new DateTime(2018, 10, 31), DateTimeUtility.GetLastDateOfQuarter(new DateTime(2018, 10, 1), 2));
            Assert.AreEqual(new DateTime(2019, 1, 31), DateTimeUtility.GetLastDateOfQuarter(new DateTime(2018, 12, 31), 2));
            Assert.AreEqual(new DateTime(1, 1, 31), DateTimeUtility.GetLastDateOfQuarter(new DateTime(1, 1, 1), 2));
            //Assert.AreEqual(new DateTime(10000, 1, 31), DateTimeUtility.GetLastDateOfQuarter(new DateTime(9999, 12, 31), 2));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetLastDateOfQuarter_Kind()
        {
            Assert.AreEqual(DateTimeKind.Unspecified, DateTimeUtility.GetLastDateOfQuarter(new DateTime(2018, 1, 1), 4).Kind);
            Assert.AreEqual(DateTimeKind.Local, DateTimeUtility.GetLastDateOfQuarter(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Local), 4).Kind);
            Assert.AreEqual(DateTimeKind.Utc, DateTimeUtility.GetLastDateOfQuarter(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc), 4).Kind);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetLastDateOfQuarter_Failed1()
        {
            Assert.AreEqual(new DateTime(2018, 3, 31), DateTimeUtility.GetLastDateOfQuarter(new DateTime(2018, 1, 1), 0));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetLastDateOfQuarter_Failed2()
        {
            Assert.AreEqual(new DateTime(2018, 3, 31), DateTimeUtility.GetLastDateOfQuarter(new DateTime(2018, 1, 1), 13));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetLastDateOfQuarter_Failed3()
        {
            Assert.AreEqual(DateTime.MaxValue, DateTimeUtility.GetLastDateOfQuarter(new DateTime(9999, 12, 31), 2));
        }

        #endregion

        #region GetFirstDateOfYear

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetFirstDateOfYear()
        {
            Assert.AreEqual(new DateTime(2018, 1, 1), DateTimeUtility.GetFirstDateOfYear(2018));

            Assert.AreEqual(new DateTime(2018, 1, 1), DateTimeUtility.GetFirstDateOfYear(new DateTime(2018, 1, 1)));
            Assert.AreEqual(new DateTime(2018, 1, 1), DateTimeUtility.GetFirstDateOfYear(new DateTime(2018, 12, 31)));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetFirstDateOfYear_Kind()
        {
            Assert.AreEqual(DateTimeKind.Unspecified, DateTimeUtility.GetFirstDateOfYear(new DateTime(2018, 1, 1)).Kind);
            Assert.AreEqual(DateTimeKind.Local, DateTimeUtility.GetFirstDateOfYear(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Local)).Kind);
            Assert.AreEqual(DateTimeKind.Utc, DateTimeUtility.GetFirstDateOfYear(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Kind);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFirstDateOfYear_Failed()
        {
            Assert.AreEqual(new DateTime(2018, 1, 1), DateTimeUtility.GetFirstDateOfYear(0));
        }

        #endregion

        #region GetLastDateOfYear

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetLastDateOfYear()
        {
            Assert.AreEqual(new DateTime(2018, 12, 31), DateTimeUtility.GetLastDateOfYear(2018));

            Assert.AreEqual(new DateTime(2018, 12, 31), DateTimeUtility.GetLastDateOfYear(new DateTime(2018, 1, 1)));
            Assert.AreEqual(new DateTime(2018, 12, 31), DateTimeUtility.GetLastDateOfYear(new DateTime(2018, 12, 31)));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetLastDateOfYear_Kind()
        {
            Assert.AreEqual(DateTimeKind.Unspecified, DateTimeUtility.GetLastDateOfYear(new DateTime(2018, 1, 1)).Kind);
            Assert.AreEqual(DateTimeKind.Local, DateTimeUtility.GetLastDateOfYear(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Local)).Kind);
            Assert.AreEqual(DateTimeKind.Utc, DateTimeUtility.GetLastDateOfYear(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Kind);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetLastDateOfYear_Failed()
        {
            Assert.AreEqual(new DateTime(2018, 1, 1), DateTimeUtility.GetLastDateOfYear(0));
        }

        #endregion

        #region GetFirstDateOfMonth

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetFirstDateOfMonth()
        {
            Assert.AreEqual(new DateTime(2018, 1, 1), DateTimeUtility.GetFirstDateOfMonth(2018, 1));

            Assert.AreEqual(new DateTime(2018, 1, 1), DateTimeUtility.GetFirstDateOfMonth(new DateTime(2018, 1, 1)));
            Assert.AreEqual(new DateTime(2018, 1, 1), DateTimeUtility.GetFirstDateOfMonth(new DateTime(2018, 1, 10)));
            Assert.AreEqual(new DateTime(2018, 1, 1), DateTimeUtility.GetFirstDateOfMonth(new DateTime(2018, 1, 31)));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetFirstDateOfMonth_Kind()
        {
            Assert.AreEqual(DateTimeKind.Unspecified, DateTimeUtility.GetFirstDateOfMonth(new DateTime(2018, 1, 1)).Kind);
            Assert.AreEqual(DateTimeKind.Local, DateTimeUtility.GetFirstDateOfMonth(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Local)).Kind);
            Assert.AreEqual(DateTimeKind.Utc, DateTimeUtility.GetFirstDateOfMonth(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Kind);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFirstDateOfMonth_Failed1()
        {
            Assert.AreEqual(new DateTime(2018, 1, 1), DateTimeUtility.GetFirstDateOfMonth(0, 1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFirstDateOfMonth_Failed2()
        {
            Assert.AreEqual(new DateTime(2018, 1, 1), DateTimeUtility.GetFirstDateOfMonth(2018, 0));
        }

        #endregion

        #region GetLastDateOfMonth

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetLastDateOfMonth()
        {
            Assert.AreEqual(new DateTime(2018, 1, 31), DateTimeUtility.GetLastDateOfMonth(2018, 1));
            Assert.AreEqual(new DateTime(2018, 2, 28), DateTimeUtility.GetLastDateOfMonth(2018, 2));
            Assert.AreEqual(new DateTime(2000, 2, 29), DateTimeUtility.GetLastDateOfMonth(2000, 2));

            Assert.AreEqual(new DateTime(2018, 1, 31), DateTimeUtility.GetLastDateOfMonth(new DateTime(2018, 1, 1)));
            Assert.AreEqual(new DateTime(2018, 1, 31), DateTimeUtility.GetLastDateOfMonth(new DateTime(2018, 1, 10)));
            Assert.AreEqual(new DateTime(2018, 1, 31), DateTimeUtility.GetLastDateOfMonth(new DateTime(2018, 1, 31)));
            Assert.AreEqual(new DateTime(2018, 2, 28), DateTimeUtility.GetLastDateOfMonth(new DateTime(2018, 2, 1)));
            Assert.AreEqual(new DateTime(2000, 2, 29), DateTimeUtility.GetLastDateOfMonth(new DateTime(2000, 2, 1)));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetLastDateOfMonth_Kind()
        {
            Assert.AreEqual(DateTimeKind.Unspecified, DateTimeUtility.GetLastDateOfMonth(new DateTime(2018, 1, 1)).Kind);
            Assert.AreEqual(DateTimeKind.Local, DateTimeUtility.GetLastDateOfMonth(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Local)).Kind);
            Assert.AreEqual(DateTimeKind.Utc, DateTimeUtility.GetLastDateOfMonth(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Kind);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetLastDateOfMonth_Failed1()
        {
            Assert.AreEqual(new DateTime(2018, 1, 1), DateTimeUtility.GetLastDateOfMonth(0, 1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetLastDateOfMonth_Failed2()
        {
            Assert.AreEqual(new DateTime(2018, 1, 1), DateTimeUtility.GetLastDateOfMonth(2018, 0));
        }

        #endregion

        #region GetFirstDateOfWeek

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetFirstDateOfWeek()
        {
            Assert.AreEqual(new DateTime(2017, 12, 31), DateTimeUtility.GetFirstDateOfWeek(new DateTime(2018, 1, 1)));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetFirstDateOfWeek_Kind()
        {
            Assert.AreEqual(DateTimeKind.Unspecified, DateTimeUtility.GetFirstDateOfWeek(new DateTime(2018, 1, 1)).Kind);
            Assert.AreEqual(DateTimeKind.Local, DateTimeUtility.GetFirstDateOfWeek(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Local)).Kind);
            Assert.AreEqual(DateTimeKind.Utc, DateTimeUtility.GetFirstDateOfWeek(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Kind);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetFirstDateOfWeek_Failed()
        {
            Assert.AreEqual(DateTime.MinValue, DateTimeUtility.GetFirstDateOfWeek(new DateTime(1, 1, 1)));
        }

        #endregion

        #region GetLastDateOfWeek

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetLastDateOfWeek()
        {
            Assert.AreEqual(new DateTime(2018, 1, 6), DateTimeUtility.GetLastDateOfWeek(new DateTime(2018, 1, 1)));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetLastDateOfWeek_Kind()
        {
            Assert.AreEqual(DateTimeKind.Unspecified, DateTimeUtility.GetLastDateOfWeek(new DateTime(2018, 1, 1)).Kind);
            Assert.AreEqual(DateTimeKind.Local, DateTimeUtility.GetLastDateOfWeek(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Local)).Kind);
            Assert.AreEqual(DateTimeKind.Utc, DateTimeUtility.GetLastDateOfWeek(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Kind);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetLastDateOfWeek_Failed()
        {
            Assert.AreEqual(DateTime.MaxValue, DateTimeUtility.GetLastDateOfWeek(new DateTime(9999, 12, 31)));
        }

        #endregion

        #region GetDateOfCurrentWeek

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetDateOfCurrentWeek()
        {
            Assert.AreEqual(new DateTime(2017, 12, 31), DateTimeUtility.GetDateOfCurrentWeek(new DateTime(2018, 1, 1), DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2018, 1, 3), DateTimeUtility.GetDateOfCurrentWeek(new DateTime(2018, 1, 1), DayOfWeek.Wednesday));
            Assert.AreEqual(new DateTime(2018, 1, 6), DateTimeUtility.GetDateOfCurrentWeek(new DateTime(2018, 1, 1), DayOfWeek.Saturday));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetDateOfCurrentWeek_Kind()
        {
            Assert.AreEqual(DateTimeKind.Unspecified, DateTimeUtility.GetDateOfCurrentWeek(new DateTime(2018, 1, 1), DayOfWeek.Sunday).Kind);
            Assert.AreEqual(DateTimeKind.Local, DateTimeUtility.GetDateOfCurrentWeek(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Local), DayOfWeek.Sunday).Kind);
            Assert.AreEqual(DateTimeKind.Utc, DateTimeUtility.GetDateOfCurrentWeek(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc), DayOfWeek.Sunday).Kind);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetDateOfCurrentWeek_Failed1()
        {
            Assert.AreEqual(DateTime.MinValue, DateTimeUtility.GetDateOfCurrentWeek(new DateTime(1, 1, 1), DayOfWeek.Sunday));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetDateOfCurrentWeek_Failed2()
        {
            Assert.AreEqual(DateTime.MaxValue, DateTimeUtility.GetDateOfCurrentWeek(new DateTime(9999, 12, 31), DayOfWeek.Saturday));
        }

        #endregion

        #region GetDateOfNextWeek

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetDateOfNextWeek()
        {
            Assert.AreEqual(new DateTime(2018, 1, 7), DateTimeUtility.GetDateOfNextWeek(new DateTime(2018, 1, 1), DayOfWeek.Sunday));
            Assert.AreEqual(new DateTime(2018, 1, 10), DateTimeUtility.GetDateOfNextWeek(new DateTime(2018, 1, 1), DayOfWeek.Wednesday));
            Assert.AreEqual(new DateTime(2018, 1, 13), DateTimeUtility.GetDateOfNextWeek(new DateTime(2018, 1, 1), DayOfWeek.Saturday));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetDateOfNextWeek_Kind()
        {
            Assert.AreEqual(DateTimeKind.Unspecified, DateTimeUtility.GetDateOfNextWeek(new DateTime(2018, 1, 1), DayOfWeek.Sunday).Kind);
            Assert.AreEqual(DateTimeKind.Local, DateTimeUtility.GetDateOfNextWeek(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Local), DayOfWeek.Sunday).Kind);
            Assert.AreEqual(DateTimeKind.Utc, DateTimeUtility.GetDateOfNextWeek(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc), DayOfWeek.Sunday).Kind);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetDateOfNextWeek_Failed1()
        {
            Assert.AreEqual(DateTime.MaxValue, DateTimeUtility.GetDateOfNextWeek(new DateTime(9999, 12, 31), DayOfWeek.Saturday));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetDateOfNextWeek_Failed2()
        {
            Assert.AreEqual(DateTime.MaxValue, DateTimeUtility.GetDateOfNextWeek(new DateTime(9999, 12, 24), DayOfWeek.Saturday));
        }

        #endregion

        #region ToUnixTimeSeconds

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ToUnixTimeSeconds()
        {
            Assert.AreEqual(0, DateTimeUtility.ToUnixTimeSeconds(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)));

            Assert.AreEqual(DateTimeUtility.ToUnixTimeSeconds(new DateTime(2018, 1, 1, 1, 2, 3)), DateTimeUtility.ToUnixTimeSeconds(new DateTime(2018, 1, 1, 1, 2, 3, DateTimeKind.Local).ToUniversalTime()));
            Assert.AreEqual(DateTimeUtility.ToUnixTimeSeconds(new DateTime(2018, 1, 1, 1, 2, 3)), DateTimeUtility.ToUnixTimeSeconds(new DateTime(2018, 1, 1, 1, 2, 3, DateTimeKind.Local).ToLocalTime()));
        }

        #endregion

        #region ToUnixTimeSeconds

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ToUnixTimeMilliseconds()
        {
            Assert.AreEqual(0, DateTimeUtility.ToUnixTimeMilliseconds(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)));

            Assert.AreEqual(DateTimeUtility.ToUnixTimeMilliseconds(new DateTime(2018, 1, 1, 1, 2, 3)), DateTimeUtility.ToUnixTimeMilliseconds(new DateTime(2018, 1, 1, 1, 2, 3, DateTimeKind.Local).ToUniversalTime()));
            Assert.AreEqual(DateTimeUtility.ToUnixTimeMilliseconds(new DateTime(2018, 1, 1, 1, 2, 3)), DateTimeUtility.ToUnixTimeMilliseconds(new DateTime(2018, 1, 1, 1, 2, 3, DateTimeKind.Local).ToLocalTime()));
        }

        #endregion

        #region AsLocalIfUnspecified

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AsLocalIfUnspecified()
        {
            DateTime utc = new DateTime(2018, 1, 1, 1, 2, 3, DateTimeKind.Utc);
            DateTime local = new DateTime(2018, 1, 1, 1, 2, 3, DateTimeKind.Local);
            DateTime unspecified = new DateTime(2018, 1, 1, 1, 2, 3);

            Assert.AreEqual(local, unspecified.AsLocalIfUnspecified());
            Assert.AreEqual(local, local.AsLocalIfUnspecified());
            Assert.AreEqual(utc, utc.AsLocalIfUnspecified());
        }

        #endregion

    }
}
