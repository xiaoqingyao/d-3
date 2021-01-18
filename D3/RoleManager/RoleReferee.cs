using System;
using System.Collections.Generic;
using System.Text;

namespace D_3.RoleManager
{
    /// <summary>
    /// 裁判
    /// </summary>
    public static class RoleReferee
    {
        /// <summary>
        /// 是否为标段
        /// </summary>
        /// <param name="dtFrom"></param>
        /// <param name="dtTo"></param>
        /// <returns></returns>
        public static bool IsStandard(DateTime dtFrom, DateTime dtTo)
        {
            var isH = isHoliday(dtFrom.Year, dtFrom.Month, dtFrom.Day);
            var ismorning = isMorning(dtFrom);
            var RoleDt = new DateDescriptionModel()
            {
                IsMorning = ismorning,
                IsHoliday = isH,
                dateFrom = new RoleDate()
                {
                    Hour = dtFrom.Hour,
                    Minute = dtFrom.Minute,
                    Second = dtFrom.Second
                },
                dateTo = new RoleDate()
                {
                    Hour = dtTo.Hour,
                    Minute = dtTo.Minute,
                    Second = dtTo.Second
                }
            };
            return DateStandardConfig.IsMatchStandards(RoleDt);
        }
        /// <summary>
        /// 是否是连段
        /// </summary>
        /// <param name="dtFrom1"></param>
        /// <param name="dtTo1"></param>
        /// <param name="dtFrom2"></param>
        /// <param name="dtTo2"></param>
        /// <returns>第一个返回值表示是否连续 第二个返回值表示是否是dt1在前面</returns>
        public static bool[] IsSerial(DateTime dtFrom1, DateTime dtTo1, DateTime dtFrom2, DateTime dtTo2)
        {
            bool boIsSerial = false;
            bool boDt1First = true;//判断dt1是连段的首位
            if (dtTo1 > dtTo2)
            {
                boDt1First = false;
            }
            //日期1在前
            if (boDt1First)
            {
                TimeSpan tsDt1 = new TimeSpan(dtTo1.Ticks);
                TimeSpan tsDf2 = new TimeSpan(dtFrom2.Ticks);
                TimeSpan ts = tsDt1.Subtract(tsDf2).Duration();
                if (ts.TotalMinutes == 0)
                {
                    boIsSerial = true;
                }
            }
            else
            {
                //日期2在前
                TimeSpan tsDt2 = new TimeSpan(dtTo2.Ticks);
                TimeSpan tsDf1 = new TimeSpan(dtFrom1.Ticks);
                TimeSpan ts = tsDt2.Subtract(tsDf1).Duration();
                if (ts.TotalMinutes == 0)
                {
                    boIsSerial = true;
                }
            }
            return new bool[] { boIsSerial, boDt1First };
        }
        /// <summary>
        /// 是否是节假日（对接校历）
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        private static bool isHoliday(int year, int month, int day)
        {
            return true;
        }
        /// <summary>
        /// 是否是上午
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static bool isMorning(DateTime dt)
        {
            return dt.Hour < 12;
        }
    }
}
