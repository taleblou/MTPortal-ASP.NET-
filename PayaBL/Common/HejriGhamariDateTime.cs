using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PayaBL.Common
{
    public class HejriGhamariDateTime
    {
        #region Fields
        private readonly HijriCalendar _calender;
        private readonly string[] _dayOfWeekGhamri;
        private DateTime _miladiDate;
        private readonly string[] _monthNameGhamari;
        #endregion

        #region Methods

        public HejriGhamariDateTime(DateTime dt)
        {
            this._calender = new HijriCalendar();
            this._dayOfWeekGhamri = new string[] { "الیوم", "السبت", "الاحد", "الاثنين", "الثلاثاء", "الاربعاء", "الخميس", "الجمعه" };
            this._monthNameGhamari = new string[] { "شهر", "محرم", "صفر", "ربيع الاول", "ربيع الثاني", "جمادي الاول", "جمادي الثاني", "رجب", "شعبان", "رمضان", "شوال", "ذوالقعده", "ذوالحجه" };
            this.MiladiDate = dt;
        }

        public HejriGhamariDateTime(string hejriGhamriDate)
        {
            this._calender = new HijriCalendar();
            this._dayOfWeekGhamri = new string[] { "الیوم", "السبت", "الاحد", "الاثنين", "الثلاثاء", "الاربعاء", "الخميس", "الجمعه" };
            this._monthNameGhamari = new string[] { "شهر", "محرم", "صفر", "ربيع الاول", "ربيع الثاني", "جمادي الاول", "جمادي الثاني", "رجب", "شعبان", "رمضان", "شوال", "ذوالقعده", "ذوالحجه" };
            string[] array = hejriGhamriDate.Split(new char[] { '/' });
            if ((array[2].Length > 2) || (array[1].Length > 2))
            {
                new Exception("رشته تاریخ صحیح نمی باشد");
            }
            int year = int.Parse(array[0]);
            int month = int.Parse(array[1]);
            int day = int.Parse(array[2]);
            DateTime dt = this._calender.ToDateTime(year, month, day, 0, 0, 0, 0);
            this.MiladiDate = dt;
        }

        public HejriGhamariDateTime(int year, int month, int day)
        {
            this._calender = new HijriCalendar();
            this._dayOfWeekGhamri = new string[] { "الیوم", "السبت", "الاحد", "الاثنين", "الثلاثاء", "الاربعاء", "الخميس", "الجمعه" };
            this._monthNameGhamari = new string[] { "شهر", "محرم", "صفر", "ربيع الاول", "ربيع الثاني", "جمادي الاول", "جمادي الثاني", "رجب", "شعبان", "رمضان", "شوال", "ذوالقعده", "ذوالحجه" };
            DateTime dt = this._calender.ToDateTime(year, month, day, 0, 0, 0, 0);
            this.MiladiDate = dt;
        }

        public HejriGhamariDateTime(int sYear, int sMonth, int sDay, int sHour, int sMinutes, int sSecond, int sMilliSeconds)
        {
            this._calender = new HijriCalendar();
            this._dayOfWeekGhamri = new string[] { "الیوم", "السبت", "الاحد", "الاثنين", "الثلاثاء", "الاربعاء", "الخميس", "الجمعه" };
            this._monthNameGhamari = new string[] { "شهر", "محرم", "صفر", "ربيع الاول", "ربيع الثاني", "جمادي الاول", "جمادي الثاني", "رجب", "شعبان", "رمضان", "شوال", "ذوالقعده", "ذوالحجه" };
            DateTime dt = this._calender.ToDateTime(sYear, sMonth, sDay, sHour, sMinutes, sSecond, sMilliSeconds);
            this.MiladiDate = dt;
        }

        public HejriGhamariDateTime AddDays(int days)
        {
            DateTime dt = this._calender.ToDateTime(this.Year, this.Month, this.Day, this.Hour, this.Minutes, this.Second, (int)this.MilliSecond);
            return new HejriGhamariDateTime(this._calender.AddDays(dt, days));
        }

        public HejriGhamariDateTime AddHours(int hours)
        {
            DateTime dt = this._calender.ToDateTime(this.Year, this.Month, this.Day, this.Hour, this.Minutes, this.Second, (int)this.MilliSecond);
            return new HejriGhamariDateTime(this._calender.AddHours(dt, hours));
        }

        public HejriGhamariDateTime AddMilliSeconds(double milliSeconds)
        {
            DateTime dt = this._calender.ToDateTime(this.Year, this.Month, this.Day, this.Hour, this.Minutes, this.Second, (int)this.MilliSecond);
            return new HejriGhamariDateTime(this._calender.AddMilliseconds(dt, milliSeconds));
        }

        public HejriGhamariDateTime AddMinutes(int minutes)
        {
            DateTime dt = this._calender.ToDateTime(this.Year, this.Month, this.Day, this.Hour, this.Minutes, this.Second, (int)this.MilliSecond);
            return new HejriGhamariDateTime(this._calender.AddMinutes(dt, minutes));
        }

        public HejriGhamariDateTime AddMonths(int months)
        {
            DateTime dt = this._calender.ToDateTime(this.Year, this.Month, this.Day, this.Hour, this.Minutes, this.Second, (int)this.MilliSecond);
            return new HejriGhamariDateTime(this._calender.AddMonths(dt, months));
        }

        public HejriGhamariDateTime AddSeconds(int seconds)
        {
            DateTime dt = this._calender.ToDateTime(this.Year, this.Month, this.Day, this.Hour, this.Minutes, this.Second, (int)this.MilliSecond);
            return new HejriGhamariDateTime(this._calender.AddSeconds(dt, seconds));
        }

        public HejriGhamariDateTime AddWeeks(int weeks)
        {
            DateTime dt = this._calender.ToDateTime(this.Year, this.Month, this.Day, this.Hour, this.Minutes, this.Second, (int)this.MilliSecond);
            return new HejriGhamariDateTime(this._calender.AddWeeks(dt, weeks));
        }

        public HejriGhamariDateTime AddYears(int years)
        {
            DateTime dt = this._calender.ToDateTime(this.Year, this.Month, this.Day, this.Hour, this.Minutes, this.Second, (int)this.MilliSecond);
            return new HejriGhamariDateTime(this._calender.AddYears(dt, years));
        }

        public static int Comparing(HejriGhamariDateTime firstDate, HejriGhamariDateTime secondDate)
        {
            int iFirstDate = int.Parse(firstDate.Date.Replace("/", ""));
            int iSecondDate = int.Parse(secondDate.Date.Replace("/", ""));
            if (iFirstDate > iSecondDate)
            {
                return 1;
            }
            if (iFirstDate < iSecondDate)
            {
                return -1;
            }
            return 0;
        }

        public static int GetDayBetweenTwoDate(HejriGhamariDateTime smallerDate, HejriGhamariDateTime biggerDate)
        {
            int iDay = 0;
            while (Comparing(biggerDate, smallerDate) == 1)
            {
                iDay++;
                smallerDate = smallerDate.AddDays(1);
            }
            while (Comparing(smallerDate, biggerDate) == 1)
            {
                iDay++;
                biggerDate = biggerDate.AddDays(1);
            }
            return iDay;
        }

        public static int GetDayInMonth(int year, int month)
        {
            HijriCalendar pc = new HijriCalendar();
            return pc.GetDaysInMonth(year, month);
        }

        public static int GetDayInYear(int year)
        {
            HijriCalendar pc = new HijriCalendar();
            return pc.GetDaysInYear(year);
        }

        public static int GetLeapMonth(int year)
        {
            HijriCalendar pc = new HijriCalendar();
            return pc.GetLeapMonth(year);
        }

        public static int GetMonthInYear(int year)
        {
            HijriCalendar pc = new HijriCalendar();
            return pc.GetMonthsInYear(year);
        }

        public static bool IsLeapDay(int day, int month, int year)
        {
            HijriCalendar pc = new HijriCalendar();
            return pc.IsLeapDay(year, month, day);
        }

        public static bool IsLeapMonth(int year, int month)
        {
            HijriCalendar pc = new HijriCalendar();
            return pc.IsLeapMonth(year, month);
        }

        public static bool IsLeapYear(int year)
        {
            HijriCalendar pc = new HijriCalendar();
            return pc.IsLeapYear(year);
        }

        public static HejriGhamariDateTime Parse(string hejri)
        {
            return new HejriGhamariDateTime(hejri);
        }

        public string ToLongDateString()
        {
            string sYear = this.Year.ToString();
            while (sYear.Length < 4)
            {
                sYear = "0" + sYear;
            }
            string sMonth = this.HejriMonthName;
            string sDay = this.Day.ToString();
            if (sDay.Length < 2)
            {
                sDay = "0" + sDay;
            }
            return (sDay + " " + sMonth + " " + sYear);
        }

        public string ToLongDateStringWithDay()
        {
            string daysName = this.HejriDayOfWeek;
            string sYear = this.Year.ToString();
            while (sYear.Length < 4)
            {
                sYear = "0" + sYear;
            }
            string sMonth = this.HejriMonthName;
            string sDay = this.Day.ToString();
            if (sDay.Length < 2)
            {
                sDay = "0" + sDay;
            }
            return (daysName + " " + sDay + " " + sMonth + " " + sYear);
        }

        public override string ToString()
        {
            string sYear = this.Year.ToString();
            while (sYear.Length < 4)
            {
                sYear = "0" + sYear;
            }
            string sMonth = this.Month.ToString();
            if (sMonth.Length < 2)
            {
                sMonth = "0" + sMonth;
            }
            string sDay = this.Day.ToString();
            if (sDay.Length < 2)
            {
                sDay = "0" + sDay;
            }
            return (sYear + "/" + sMonth + "/" + sDay);
        }

        public static bool TryParse(string hejri, ref HejriGhamariDateTime hsdt)
        {
            if (!hejri.Contains("/"))
            {
                return false;
            }
            if (hejri.IndexOf('/') != 4)
            {
                return false;
            }
            string[] array = hejri.Split(new char[] { '/' });
            if (array.Length < 2)
            {
                return false;
            }
            if (array[0].Length < 4)
            {
                return false;
            }
            if (array[1].Length < 2)
            {
                return false;
            }
            if (array[2].Length < 2)
            {
                return false;
            }
            if ((int.Parse(array[1]) < 1) || (int.Parse(array[1]) > 12))
            {
                return false;
            }
            if ((int.Parse(array[2]) < 1) || (int.Parse(array[2]) > 0x1f))
            {
                return false;
            }
            if (int.Parse(array[0]) < 1)
            {
                return false;
            }
            hsdt = new HejriGhamariDateTime(hejri);
            return true;
        }

        #endregion

        #region Properties

        public string Date
        {
            get
            {
                return this.ToString();
            }
        }

        public int Day
        {
            get
            {
                return this._calender.GetDayOfMonth(this._miladiDate);
            }
        }

        public DayOfWeek DayOfWeek
        {
            get
            {
                return this._calender.GetDayOfWeek(this._miladiDate);
            }
        }

        public int DayOfYear
        {
            get
            {
                return this._calender.GetDayOfYear(this._miladiDate);
            }
        }

        public string HejriDayOfWeek
        {
            get
            {
                switch (this.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        return this._dayOfWeekGhamri[2];

                    case DayOfWeek.Monday:
                        return this._dayOfWeekGhamri[3];

                    case DayOfWeek.Tuesday:
                        return this._dayOfWeekGhamri[4];

                    case DayOfWeek.Wednesday:
                        return this._dayOfWeekGhamri[5];

                    case DayOfWeek.Thursday:
                        return this._dayOfWeekGhamri[6];

                    case DayOfWeek.Friday:
                        return this._dayOfWeekGhamri[7];

                    case DayOfWeek.Saturday:
                        return this._dayOfWeekGhamri[1];
                }
                return this._dayOfWeekGhamri[0];
            }
        }

        public string HejriMonthName
        {
            get
            {
                switch (this.Month)
                {
                    case 1:
                        return this._monthNameGhamari[1];

                    case 2:
                        return this._monthNameGhamari[2];

                    case 3:
                        return this._monthNameGhamari[3];

                    case 4:
                        return this._monthNameGhamari[4];

                    case 5:
                        return this._monthNameGhamari[5];

                    case 6:
                        return this._monthNameGhamari[6];

                    case 7:
                        return this._monthNameGhamari[7];

                    case 8:
                        return this._monthNameGhamari[8];

                    case 9:
                        return this._monthNameGhamari[9];

                    case 10:
                        return this._monthNameGhamari[10];

                    case 11:
                        return this._monthNameGhamari[11];

                    case 12:
                        return this._monthNameGhamari[12];
                }
                return this._monthNameGhamari[0];
            }
        }

        public int Hour
        {
            get
            {
                return this._calender.GetHour(this._miladiDate);
            }
        }

        public static DateTime MaxSupportedDateTime
        {
            get
            {
                HijriCalendar pc = new HijriCalendar();
                return pc.MaxSupportedDateTime;
            }
        }

        public DateTime MiladiDate
        {
            get
            {
                return this._miladiDate;
            }
            set
            {
                this._miladiDate = value;
            }
        }

        public double MilliSecond
        {
            get
            {
                return this._calender.GetMilliseconds(this._miladiDate);
            }
        }

        public static DateTime MinSupportedDateTime
        {
            get
            {
                HijriCalendar pc = new HijriCalendar();
                return pc.MinSupportedDateTime;
            }
        }

        public int Minutes
        {
            get
            {
                return this._calender.GetMinute(this._miladiDate);
            }
        }

        public int Month
        {
            get
            {
                return this._calender.GetMonth(this._miladiDate);
            }
        }

        public static HejriGhamariDateTime Now
        {
            get
            {
                return new HejriGhamariDateTime(DateTime.Now);
            }
        }

        public int Second
        {
            get
            {
                return this._calender.GetSecond(this._miladiDate);
            }
        }

        public int Year
        {
            get
            {
                return this._calender.GetYear(this._miladiDate);
            }
        }

        #endregion
    }
}
