using System;
using NUnit.Framework;

namespace ReassureTest.Tests
{
    public class DateTimeTests
    {
        [Test]
        public void DateTimeTests_now()
        {
            DateTime.Now.Is("now");
            DateTime.Now.AddSeconds(-1.0).Is("now");
            DateTime.Now.AddSeconds(1.0).Is("now");

            var ex = Assert.Throws<AssertException>(() => DateTime.Now.AddSeconds(10.0).Is("now"));
            StringAssert.StartsWith("Path: ''.\r\nExpected: ", ex.Message);
        }

        [Test]
        public void DateTimeTests_dates()
        {
            var time = new DateTime(1999, 9, 19, 12, 54, 02);
            time.Is("1999-09-19T12:54:02");
            time.Is("1999-09-19T12:54:01");
            time.Is("1999-09-19T12:54:03");

            var ex = Assert.Throws<AssertException>(() => time.AddSeconds(10.0).Is("1999-09-19T12:54:02"));
            Assert.AreEqual("Path: ''.\r\nExpected: 1999-09-19T12:54:02\r\nBut was:  1999-09-19T12:54:12", ex.Message);
        }

        [Test]
        public void DateTimeTests_null_vs_value()
        {
            DateTime? t = null;

            var ex = Assert.Throws<AssertException>(() => t.Is("1999-09-19T12:54:02"));

            Assert.AreEqual("Path: ''.\r\nExpected: not null\r\nBut was: null", ex.Message);
        }

        [Test]
        public void DateTimeTests_value_vs_null()
        {
            DateTime t = new DateTime(2020, 3, 4);

            var ex = Assert.Throws<AssertException>(() => t.Is("null"));

            Assert.AreEqual("Path: ''.\r\nExpected: null\r\nBut was:  not null", ex.Message);
        }

        [Test]
        public void DateTimeTests_unequal()
        {
            DateTime t = new DateTime(2020, 2, 2, 2, 2, 2);

            var ex = Assert.Throws<AssertException>(() => t.Is("1999-09-19T12:54:02"));

            Assert.AreEqual("Path: ''.\r\nExpected: 1999-09-19T12:54:02\r\nBut was:  2020-02-02T02:02:02", ex.Message);
        }
    }
}