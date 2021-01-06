using D_3.Core;
using D_3.DataSource;
using D_3.Models;
using D_3.Models.Business;
using D_3.Models.Entities;
using D_3.RoleManager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace D_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
        }
    }
}


//规则
//教室属性优先
//1、教室可授课类型也有排序，只包含小组课的>包含小组课>1v1的，仅支持小组课的，优先。
//2、课程入教室的时候，优先选择当期上节课的。
//





//取消---- - 
//更改是否专属教师、更改教师 才释放，其他不释放


//2021-1-4 
//教室排课记录的释放
//配置变动带来的释放：按照校区重新计算d-3，d-3内的。通知我们变动的校区或者教学点，我们重新执行d-3和d-3逻辑。

//教室释放接口：按老师释放，按教室释放，按照教学点、按学校释放