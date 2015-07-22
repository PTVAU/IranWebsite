
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;


namespace TCMSFRONTEND.Core
{
    public class Utility
    {
        public static string GD2JD(DateTime Gregorian)
        {
            PersianCalendar pc = new PersianCalendar();
            int y, m, d;
            y = pc.GetYear(Gregorian);
            m = pc.GetMonth(Gregorian);
            d = pc.GetDayOfMonth(Gregorian);
            string ans = string.Format("{0}/{1:d2}/{2:d2}", y, m, d);
            return ans;
        }
        public static string GD2JD(DateTime Gregorian, bool H)
        {
            PersianCalendar pc = new PersianCalendar();
            int y, m, d, h, M;
            y = pc.GetYear(Gregorian);
            m = pc.GetMonth(Gregorian);
            d = pc.GetDayOfMonth(Gregorian);
            h = pc.GetHour(Gregorian);

            M = pc.GetMinute(Gregorian);

            string ans = string.Format("{0}/{1:d2}/{2:d2} {3:d2}:{4:d2}", y, m, d, h, M);
            return ans;
        }
        public static DateTime JD2GD(string Jalali)
        {
            try
            {
                int y, m, d;
                y = int.Parse(Jalali.Substring(0, 4));
                m = int.Parse(Jalali.Substring(5, 2));
                d = int.Parse(Jalali.Substring(8, 2));
                PersianCalendar pc = new PersianCalendar();
                DateTime ans = new DateTime(y, m, d, pc);
                return ans;
            }
            catch
            {
                return DateTime.Now.AddYears(-100);
            }

        }
        public static string ClearTitle(string Title)
        {
            if (!string.IsNullOrEmpty(Title))
            {
                Title = Title.Replace("?", "");
                Title = Title.Replace("؟", "");
                Title = Title.Replace("+", "");
                Title = Title.Replace("/", "");
                Title = Title.Replace("\\", "");
                Title = Title.Replace("*", "");
                Title = Title.Replace(" ", "-");
                Title = Title.Replace(".", "-");
                Title = Title.Replace("\"", "");
                Title = Title.Replace(":", "");
                Title = Title.Replace("%", "");
                Title = Title.Replace("&", "");
                Title = Regex.Replace(Title, @"[()<>"";+\n\r`]|^&+|&+$", "", RegexOptions.Singleline);
                Title = Regex.Replace(Title, @"/[^a-zA-Z0-9\/_|+ -]/", "", RegexOptions.Singleline);
                Title = Regex.Replace(Title, @"/[\/_|+ -]+/", "", RegexOptions.Singleline);

                string[] a = new string[] { "À", "Á", "Â", "Ã", "Ä", "Å", "Æ", "Ç", "È", "É", "Ê", "Ë", "Ì", "Í", "Î", "Ï", "Ð", "Ñ", "Ò", "Ó", "Ô", "Õ", "Ö", "Ø", "Ù", "Ú", "Û", "Ü", "Ý", "ß", "à", "á", "â", "ã", "ä", "å", "æ", "ç", "è", "é", "ê", "ë", "ì", "í", "î", "ï", "ñ", "ò", "ó", "ô", "õ", "ö", "ø", "ù", "ú", "û", "ü", "ý", "ÿ", "Ā", "ā", "Ă", "ă", "Ą", "ą", "Ć", "ć", "Ĉ", "ĉ", "Ċ", "ċ", "Č", "č", "Ď", "ď", "Đ", "đ", "Ē", "ē", "Ĕ", "ĕ", "Ė", "ė", "Ę", "ę", "Ě", "ě", "Ĝ", "ĝ", "Ğ", "ğ", "Ġ", "ġ", "Ģ", "ģ", "Ĥ", "ĥ", "Ħ", "ħ", "Ĩ", "ĩ", "Ī", "ī", "Ĭ", "ĭ", "Į", "į", "İ", "ı", "Ĳ", "ĳ", "Ĵ", "ĵ", "Ķ", "ķ", "Ĺ", "ĺ", "Ļ", "ļ", "Ľ", "ľ", "Ŀ", "ŀ", "Ł", "ł", "Ń", "ń", "Ņ", "ņ", "Ň", "ň", "ŉ", "Ō", "ō", "Ŏ", "ŏ", "Ő", "ő", "Œ", "œ", "Ŕ", "ŕ", "Ŗ", "ŗ", "Ř", "ř", "Ś", "ś", "Ŝ", "ŝ", "Ş", "ş", "Š", "š", "Ţ", "ţ", "Ť", "ť", "Ŧ", "ŧ", "Ũ", "ũ", "Ū", "ū", "Ŭ", "ŭ", "Ů", "ů", "Ű", "ű", "Ų", "ų", "Ŵ", "ŵ", "Ŷ", "ŷ", "Ÿ", "Ź", "ź", "Ż", "ż", "Ž", "ž", "ſ", "ƒ", "Ơ", "ơ", "Ư", "ư", "Ǎ", "ǎ", "Ǐ", "ǐ", "Ǒ", "ǒ", "Ǔ", "ǔ", "Ǖ", "ǖ", "Ǘ", "ǘ", "Ǚ", "ǚ", "Ǜ", "ǜ", "Ǻ", "ǻ", "Ǽ", "ǽ", "Ǿ", "ǿ" };
                string[] b = new string[] { "A", "A", "A", "A", "A", "A", "AE", "C", "E", "E", "E", "E", "I", "I", "I", "I", "D", "N", "O", "O", "O", "O", "O", "O", "U", "U", "U", "U", "Y", "s", "a", "a", "a", "a", "a", "a", "ae", "c", "e", "e", "e", "e", "i", "i", "i", "i", "n", "o", "o", "o", "o", "o", "o", "u", "u", "u", "u", "y", "y", "A", "a", "A", "a", "A", "a", "C", "c", "C", "c", "C", "c", "C", "c", "D", "d", "D", "d", "E", "e", "E", "e", "E", "e", "E", "e", "E", "e", "G", "g", "G", "g", "G", "g", "G", "g", "H", "h", "H", "h", "I", "i", "I", "i", "I", "i", "I", "i", "I", "i", "IJ", "ij", "J", "j", "K", "k", "L", "l", "L", "l", "L", "l", "L", "l", "l", "l", "N", "n", "N", "n", "N", "n", "n", "O", "o", "O", "o", "O", "o", "OE", "oe", "R", "r", "R", "r", "R", "r", "S", "s", "S", "s", "S", "s", "S", "s", "T", "t", "T", "t", "T", "t", "U", "u", "U", "u", "U", "u", "U", "u", "U", "u", "U", "u", "W", "w", "Y", "y", "Y", "Z", "z", "Z", "z", "Z", "z", "s", "f", "O", "o", "U", "u", "A", "a", "I", "i", "O", "o", "U", "u", "U", "u", "U", "u", "U", "u", "U", "u", "A", "a", "AE", "ae", "O", "o" };
                for (int i = 0; i < a.Length; i++)
                {
                    Title = Title.Replace(a[i], b[i]);
                }
                return Title;
            }
            else
            {
                return "";
            }
            
        }

        public static string ToTimeString(DateTime InPutDate)
        {
            return InPutDate.Hour.ToString("00") + ":" + InPutDate.Minute.ToString("00");
        }
        public static string ToTimeString(DateTime? InPutDate)
        {
            return InPutDate.Value.Hour.ToString("00") + ":" + InPutDate.Value.Minute.ToString("00");
        }
        public static string GD2StringDateTime(DateTime InDate)
        {
            string ReturnDate;

            switch (InDate.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    ReturnDate = "جمعه";
                    break;
                case DayOfWeek.Monday:
                    ReturnDate = "دوشنبه";
                    break;
                case DayOfWeek.Saturday:
                    ReturnDate = "شنبه";
                    break;
                case DayOfWeek.Sunday:
                    ReturnDate = "یکشنبه";
                    break;
                case DayOfWeek.Thursday:
                    ReturnDate = "پنجشنبه";
                    break;
                case DayOfWeek.Tuesday:
                    ReturnDate = "سه شنبه";
                    break;
                case DayOfWeek.Wednesday:
                    ReturnDate = "چهارشنبه";
                    break;
                default:
                    ReturnDate = "----";
                    break;

            }

            string[] Dd = GD2JD(InDate).Split('/');

            string MonthTitle = "";
            switch (Dd[1])
            {
                case "01":
                    MonthTitle = "فروردین";
                    break;

                case "02":
                    MonthTitle = "اردیبهشت";
                    break;

                case "03":
                    MonthTitle = "خرداد";
                    break;

                case "04":
                    MonthTitle = "تیر";
                    break;

                case "05":
                    MonthTitle = "مرداد";
                    break;

                case "06":
                    MonthTitle = "شهریور";
                    break;

                case "07":
                    MonthTitle = "مهر";
                    break;

                case "08":
                    MonthTitle = "آبان";
                    break;

                case "09":
                    MonthTitle = "آذر";
                    break;

                case "10":
                    MonthTitle = "دی";
                    break;

                case "11":
                    MonthTitle = "بهمن";
                    break;

                case "12":
                    MonthTitle = "اسفند";
                    break;
                default:
                    break;
            }

            ReturnDate += " " + Dd[2] + " " + MonthTitle + " " + Dd[0] + " " + string.Format("{0:00}", InDate.Hour) + ":" + string.Format("{0:00}", InDate.Minute);

            return ReturnDate;
        }
        public static string GD2StringDate(DateTime InDate)
        {
            string ReturnDate;

            switch (InDate.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    ReturnDate = "جمعه";
                    break;
                case DayOfWeek.Monday:
                    ReturnDate = "دوشنبه";
                    break;
                case DayOfWeek.Saturday:
                    ReturnDate = "شنبه";
                    break;
                case DayOfWeek.Sunday:
                    ReturnDate = "یکشنبه";
                    break;
                case DayOfWeek.Thursday:
                    ReturnDate = "پنجشنبه";
                    break;
                case DayOfWeek.Tuesday:
                    ReturnDate = "سه شنبه";
                    break;
                case DayOfWeek.Wednesday:
                    ReturnDate = "چهارشنبه";
                    break;
                default:
                    ReturnDate = "----";
                    break;

            }

            string[] Dd = GD2JD(InDate).Split('/');

            string MonthTitle = "";
            switch (Dd[1])
            {
                case "01":
                    MonthTitle = "فروردین";
                    break;

                case "02":
                    MonthTitle = "اردیبهشت";
                    break;

                case "03":
                    MonthTitle = "خرداد";
                    break;

                case "04":
                    MonthTitle = "تیر";
                    break;

                case "05":
                    MonthTitle = "مرداد";
                    break;

                case "06":
                    MonthTitle = "شهریور";
                    break;

                case "07":
                    MonthTitle = "مهر";
                    break;

                case "08":
                    MonthTitle = "آبان";
                    break;

                case "09":
                    MonthTitle = "آذر";
                    break;

                case "10":
                    MonthTitle = "دی";
                    break;

                case "11":
                    MonthTitle = "بهمن";
                    break;

                case "12":
                    MonthTitle = "اسفند";
                    break;
                default:
                    break;
            }

            ReturnDate += " " + Dd[2] + " " + MonthTitle + " " + Dd[0];

            return ReturnDate;
        }
        public static string GD2NameofDay(DateTime InDate)
        {
            string ReturnDate;

            switch (InDate.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    ReturnDate = "جمعه";
                    break;
                case DayOfWeek.Monday:
                    ReturnDate = "دوشنبه";
                    break;
                case DayOfWeek.Saturday:
                    ReturnDate = "شنبه";
                    break;
                case DayOfWeek.Sunday:
                    ReturnDate = "یکشنبه";
                    break;
                case DayOfWeek.Thursday:
                    ReturnDate = "پنجشنبه";
                    break;
                case DayOfWeek.Tuesday:
                    ReturnDate = "سه شنبه";
                    break;
                case DayOfWeek.Wednesday:
                    ReturnDate = "چهارشنبه";
                    break;
                default:
                    ReturnDate = "----";
                    break;

            }
            return ReturnDate;
        }
        public static string GD2NameofMonth(DateTime InDate)
        {
            string ReturnDate = "";
            string[] Dd = GD2JD(InDate).Split('/');

            switch (Dd[1])
            {
                case "01":
                    ReturnDate = "فروردین";
                    break;

                case "02":
                    ReturnDate = "اردیبهشت";
                    break;

                case "03":
                    ReturnDate = "خرداد";
                    break;

                case "04":
                    ReturnDate = "تیر";
                    break;

                case "05":
                    ReturnDate = "مرداد";
                    break;

                case "06":
                    ReturnDate = "شهریور";
                    break;

                case "07":
                    ReturnDate = "مهر";
                    break;

                case "08":
                    ReturnDate = "آبان";
                    break;

                case "09":
                    ReturnDate = "آذر";
                    break;

                case "10":
                    ReturnDate = "دی";
                    break;

                case "11":
                    ReturnDate = "بهمن";
                    break;

                case "12":
                    ReturnDate = "اسفند";
                    break;
                default:
                    break;
            }

            return ReturnDate;
        }
        public static string ReadFile(string path)
        {
            string result = "";
            result = cacheControl(path, null, "R", 0);
            if (string.IsNullOrEmpty(result))
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Server.MapPath(path));
                try
                {
                    result = sr.ReadToEnd();
                }
                finally
                {
                    sr.Close();
                }
                result = cacheControl(path, result, "W", 1);
            }
            result = Regex.Replace(result, @"\s*(<[^>]+>)\s*", "$1", RegexOptions.Singleline);
            if (!path.Contains("index"))
                result = Regex.Replace(result, "<!--.*?-->", "", RegexOptions.Singleline);

            return result;
        }
        public static string cacheControl(string key, string value, string operation, double cacheTime)
        {
            key = key.Replace("/", "-").Replace("\\", "-").Replace(".", "-").ToLower();

            string retValue = null;
            if (!key.Contains("callback"))
            {
                if (operation == "R")
                {
                    try
                    {
                        retValue = HttpRuntime.Cache.Get(key).ToString();
                    }
                    catch
                    {

                    }
                }

                if (operation == "W")
                {
                    HttpRuntime.Cache.Insert(key, value,
                            null, DateTime.Now.AddSeconds(cacheTime),
                           new TimeSpan(0));
                    retValue = value;
                }
            }
            else
            {
                retValue = value;
            }

            return retValue;
        }
        public static string templateDataMerger(string template, object data)
        {
            //Render template with data:
            //FormatCompiler compiler = new FormatCompiler();
            //Generator generator = compiler.Compile(template);
            //return generator.Render(data);
            return Nustache.Core.Render.StringToString(template, data);

        }
        public static string contentAliasGenerator(Bo.Service.Contents cnt)
        {
            string Alias = cnt.Title;
            if (cnt.Alias.Trim().Length > 2)
            {
                Alias = ClearTitle(cnt.Alias.Trim());
            }
            else
            {
                Alias = ClearTitle(cnt.Title.Trim());
            }
            return Alias;
        }
    }
}