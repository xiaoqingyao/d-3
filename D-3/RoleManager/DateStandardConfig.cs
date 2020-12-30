using System;
using System.Collections.Generic;
using System.Text;

namespace D_3.RoleManager
{
    /// <summary>
    /// 上课标段规则配置
    /// </summary>
    public static class DateStandardConfig
    {
        /// <summary>
        /// 标准上课段配置
        /// </summary>
        public static DateDescriptionModel[] Standards = new DateDescriptionModel[] {
            new DateDescriptionModel(){
              IsHoliday=true,
               dateFrom=new RoleDate(){ Hour=8, Minute=0, Second=0 },
               dateTo=new RoleDate(){ Hour=10, Minute=0, Second=0 }
            },
            new DateDescriptionModel(){
              IsHoliday=true,
               dateFrom=new RoleDate(){ Hour=10, Minute=0, Second=0 },
               dateTo=new RoleDate(){ Hour=12, Minute=0, Second=0 }
            },
            new DateDescriptionModel(){
              IsHoliday=true,
               dateFrom=new RoleDate(){ Hour=13, Minute=0, Second=0 },
               dateTo=new RoleDate(){ Hour=15, Minute=0, Second=0 }
            }
            ,
            new DateDescriptionModel(){
              IsHoliday=true,
               dateFrom=new RoleDate(){ Hour=15, Minute=0, Second=0 },
               dateTo=new RoleDate(){ Hour=17, Minute=0, Second=0 }
            }
            ,
            new DateDescriptionModel(){
              IsHoliday=true,
               dateFrom=new RoleDate(){ Hour=17, Minute=0, Second=0 },
               dateTo=new RoleDate(){ Hour=19, Minute=0, Second=0 }
            }
            ,
            new DateDescriptionModel(){
              IsHoliday=false,
               dateFrom=new RoleDate(){ Hour=16, Minute=30, Second=0 },
               dateTo=new RoleDate(){ Hour=18, Minute=30, Second=0 }
            },
            new DateDescriptionModel(){
              IsHoliday=false,
               dateFrom=new RoleDate(){ Hour=18, Minute=30, Second=0 },
               dateTo=new RoleDate(){ Hour=20, Minute=30, Second=0 }
            }
            ,
            new DateDescriptionModel(){
              IsHoliday=false,
               dateFrom=new RoleDate(){ Hour=16, Minute=0, Second=0 },
               dateTo=new RoleDate(){ Hour=18, Minute=30, Second=0 }
            },
            new DateDescriptionModel(){
              IsHoliday=false,
               dateFrom=new RoleDate(){ Hour=18, Minute=0, Second=0 },
               dateTo=new RoleDate(){ Hour=20, Minute=30, Second=0 }
            }
        };

        public static bool IsMatchStandards(DateDescriptionModel roleDate)
        {
            bool exit = false;
            foreach (var standard in Standards)
            {
                if (standard.Equals(roleDate))
                {
                    exit = true;
                    break;
                }
            }
            return exit;
        }
    }
    /// <summary>
    /// 教学时段
    /// </summary>
    public static class TeachTimeRange
    {
        public static int StartHour = 6;
        public static int StartMinute = 0;
        public static int StartSecond = 0;

        public static int EndHour = 18;
        public static int EndMinute = 0;
        public static int EndSecond = 0;
    }
    public class DateDescriptionModel
    {
        public DateDescriptionModel() { }
        public RoleDate dateFrom { get; set; }
        public RoleDate dateTo { get; set; }
        /// <summary>
        /// 是否是节假日
        /// </summary>
        public bool IsHoliday
        {
            get; set;
        }
        public override bool Equals(object obj)
        {
            DateDescriptionModel model2 = (DateDescriptionModel)obj;
            return this.IsHoliday = model2.IsHoliday
                && model2.dateFrom.Hour == this.dateFrom.Hour
                && model2.dateFrom.Minute == this.dateFrom.Minute
                && model2.dateFrom.Second == this.dateFrom.Second
                && model2.dateTo.Hour == this.dateTo.Hour
                && model2.dateTo.Minute == this.dateTo.Minute
                && model2.dateTo.Second == this.dateTo.Second;
        }
        public override int GetHashCode()
        {
            return (dateFrom.ToString() + dateTo.ToString() + IsHoliday.ToString()).GetHashCode();
        }

        public bool IsMorning { get; set; }
    }
    public class RoleDate
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
    }
}
