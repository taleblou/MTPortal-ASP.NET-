using System;
using System.Globalization;

namespace PayaBL.Common
{
    public class HejriShamsiDateTime
    {
        // Fields
        private readonly PersianCalendar _calender;
        private readonly string[] _dayOfWeekShamsi;
        private DateTime _miladiDate;
        private readonly string[] _monthNameShamsi;

        // Methods
        public HejriShamsiDateTime(DateTime dt)
        {
            _calender = new PersianCalendar();
            _dayOfWeekShamsi = new string[] { "روز", "شنبه", "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه" };
            _monthNameShamsi = new string[] { "ماه", "فروردين", "ارديبهشت", "خرداد", "تير", "مرداد", "شهريور", "مهر", "آبان", "آذر", "دي", "بهمن", "اسفند" };
            MiladiDate = dt;
        }

        public HejriShamsiDateTime(string hejriShamsiDate)
        {
            _calender = new PersianCalendar();
            _dayOfWeekShamsi = new string[] { "روز", "شنبه", "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه" };
            _monthNameShamsi = new string[] { "ماه", "فروردين", "ارديبهشت", "خرداد", "تير", "مرداد", "شهريور", "مهر", "آبان", "آذر", "دي", "بهمن", "اسفند" };
            string[] array = hejriShamsiDate.Split(new char[] { '/' });
            if ((array[2].Length > 2) || (array[1].Length > 2))
            {
                new Exception("رشته تاریخ صحیح نمی باشد");
            }
            int year = int.Parse(array[0]);
            int month = int.Parse(array[1]);
            int day = int.Parse(array[2]);
            DateTime dt = _calender.ToDateTime(year, month, day, 0, 0, 0, 0);
            MiladiDate = dt;
        }

        public HejriShamsiDateTime(int year, int month, int day)
        {
            _calender = new PersianCalendar();
            _dayOfWeekShamsi = new string[] { "روز", "شنبه", "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه" };
            _monthNameShamsi = new string[] { "ماه", "فروردين", "ارديبهشت", "خرداد", "تير", "مرداد", "شهريور", "مهر", "آبان", "آذر", "دي", "بهمن", "اسفند" };
            DateTime dt = this._calender.ToDateTime(year, month, day, 0, 0, 0, 0);
            MiladiDate = dt;
        }

        public HejriShamsiDateTime(int sYear, int sMonth, int sDay, int sHour, int sMinutes, int sSecond, int sMilliSeconds)
        {
            _calender = new PersianCalendar();
            _dayOfWeekShamsi = new string[] { "روز", "شنبه", "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه" };
            _monthNameShamsi = new string[] { "ماه", "فروردين", "ارديبهشت", "خرداد", "تير", "مرداد", "شهريور", "مهر", "آبان", "آذر", "دي", "بهمن", "اسفند" };
            DateTime dt = this._calender.ToDateTime(sYear, sMonth, sDay, sHour, sMinutes, sSecond, sMilliSeconds);
            this.MiladiDate = dt;
        }

        public HejriShamsiDateTime AddDays(int days)
        {
            DateTime dt = this._calender.ToDateTime(this.Year, this.Month, this.Day, this.Hour, this.Minutes, this.Second, (int) this.MilliSecond);
            return new HejriShamsiDateTime(this._calender.AddDays(dt, days));
        }

        public HejriShamsiDateTime AddHours(int hours)
        {
            DateTime dt = this._calender.ToDateTime(this.Year, this.Month, this.Day, this.Hour, this.Minutes, this.Second, (int) this.MilliSecond);
            return new HejriShamsiDateTime(this._calender.AddHours(dt, hours));
        }

        public HejriShamsiDateTime AddMilliSeconds(double milliSeconds)
        {
            DateTime dt = this._calender.ToDateTime(this.Year, this.Month, this.Day, this.Hour, this.Minutes, this.Second, (int) this.MilliSecond);
            return new HejriShamsiDateTime(this._calender.AddMilliseconds(dt, milliSeconds));
        }

        public HejriShamsiDateTime AddMinutes(int minutes)
        {
            DateTime dt = this._calender.ToDateTime(this.Year, this.Month, this.Day, this.Hour, minutes, this.Second, (int) this.MilliSecond);
            return new HejriShamsiDateTime(this._calender.AddMinutes(dt, minutes));
        }

        public HejriShamsiDateTime AddMonths(int months)
        {
            DateTime dt = this._calender.ToDateTime(this.Year, this.Month, this.Day, this.Hour, this.Minutes, this.Second, (int) this.MilliSecond);
            return new HejriShamsiDateTime(this._calender.AddMonths(dt, months));
        }

        public HejriShamsiDateTime AddSeconds(int seconds)
        {
            DateTime dt = this._calender.ToDateTime(this.Year, this.Month, this.Day, this.Hour, this.Minutes, this.Second, (int) this.MilliSecond);
            return new HejriShamsiDateTime(this._calender.AddSeconds(dt, seconds));
        }

        public HejriShamsiDateTime AddWeeks(int weeks)
        {
            DateTime dt = this._calender.ToDateTime(this.Year, this.Month, this.Day, this.Hour, this.Minutes, this.Second, (int) this.MilliSecond);
            return new HejriShamsiDateTime(this._calender.AddWeeks(dt, weeks));
        }

        public HejriShamsiDateTime AddYears(int years)
        {
            DateTime dt = this._calender.ToDateTime(this.Year, this.Month, this.Day, this.Hour, this.Minutes, this.Second, (int) this.MilliSecond);
            return new HejriShamsiDateTime(this._calender.AddYears(dt, years));
        }

        public static int Comparing(HejriShamsiDateTime firstDate, HejriShamsiDateTime secondDate)
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

        public static int GetDayBetweenTwoDate(HejriShamsiDateTime smallerDate, HejriShamsiDateTime biggerDate)
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
            PersianCalendar pc = new PersianCalendar();
            return pc.GetDaysInMonth(year, month);
        }

        public static int GetDayInYear(int year)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetDaysInYear(year);
        }

        public static int GetLeapMonth(int year)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetLeapMonth(year);
        }

        public static int GetMonthInYear(int year)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetMonthsInYear(year);
        }

        public static bool IsLeapDay(int day, int month, int year)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.IsLeapDay(year, month, day);
        }

        public static bool IsLeapMonth(int year, int month)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.IsLeapMonth(year, month);
        }

        public static bool IsLeapYear(int year)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.IsLeapYear(year);
        }

        public static HejriShamsiDateTime Parse(string hejri)
        {
            return new HejriShamsiDateTime(hejri);
        }

        public string ToLongDateString()
        {
            string sYear = this.Year.ToString();
            while (sYear.Length < 4)
            {
                sYear = "0" + sYear;
            }
            string sMonth = this.PersianMonthName;
            string sDay = this.Day.ToString();
            if (sDay.Length < 2)
            {
                sDay = "0" + sDay;
            }
            return (sDay + " " + sMonth + " " + sYear);
        }

        public string ToLongDateStringWithDay()
        {
            string daysName = this.PersianDayOfWeek;
            string sYear = this.Year.ToString();
            while (sYear.Length < 4)
            {
                sYear = "0" + sYear;
            }
            string sMonth = this.PersianMonthName;
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

        public static bool TryParse(string hejri, ref HejriShamsiDateTime hsdt)
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
            hsdt = new HejriShamsiDateTime(hejri);
            return true;
        }

        // Properties
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
                PersianCalendar pc = new PersianCalendar();
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
                PersianCalendar pc = new PersianCalendar();
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

        public static HejriShamsiDateTime Now
        {
            get
            {
                return new HejriShamsiDateTime(DateTime.Now);
            }
        }

        public string PersianDayOfWeek
        {
            get
            {
                switch (DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        return _dayOfWeekShamsi[2];

                    case DayOfWeek.Monday:
                        return _dayOfWeekShamsi[3];

                    case DayOfWeek.Tuesday:
                        return _dayOfWeekShamsi[4];

                    case DayOfWeek.Wednesday:
                        return _dayOfWeekShamsi[5];

                    case DayOfWeek.Thursday:
                        return _dayOfWeekShamsi[6];

                    case DayOfWeek.Friday:
                        return _dayOfWeekShamsi[7];

                    case DayOfWeek.Saturday:
                        return _dayOfWeekShamsi[1];
                }
                return _dayOfWeekShamsi[0];
            }
        }

        public string PersianMonthName
        {
            get
            {
                switch (Month)
                {
                    case 1:
                        return _monthNameShamsi[1];

                    case 2:
                        return _monthNameShamsi[2];

                    case 3:
                        return _monthNameShamsi[3];

                    case 4:
                        return _monthNameShamsi[4];

                    case 5:
                        return _monthNameShamsi[5];

                    case 6:
                        return _monthNameShamsi[6];

                    case 7:
                        return _monthNameShamsi[7];

                    case 8:
                        return _monthNameShamsi[8];

                    case 9:
                        return _monthNameShamsi[9];

                    case 10:
                        return _monthNameShamsi[10];

                    case 11:
                        return _monthNameShamsi[11];

                    case 12:
                        return _monthNameShamsi[12];
                }
                return _monthNameShamsi[0];
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
    }
}

 

 
