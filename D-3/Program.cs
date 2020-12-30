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

//测试用例
//todo d-3之内的，如果已排班的记录有出表，从待定表入
//todo d-3之内的，当日新增排课，先走d-3，没有的话再入待定表
//教室属性优先未体现(d-3 d-3之内的)
//上午下午要分开，不能出现超过4连标
//是否为节假日 标段规则不同；
//TeachRange没有做处理和限制，教室必须在教学范围内才可以
//专属教师的情况计算
//上下午分开算
//连课计算最后成课时间，目前计算有问题
//TODO 待确认
// 1.老师自动跟上节课的情况，还是一种优先级。对吧？比如只剩下一节课，张三李四都需要，但是上节课是张三的，那么这次机会就是张三的
//    那这条规则是只适用于小组课还是 v1 v2都适用呢？答案应该是不涉及优先级。如果张三李四随机，或者按照排课时间排序，那就是不涉及优先级。
//   1.1 或者说空出来一个房间，谁先进？ 
//   如果是优先级那 这种情况可以视为弱联（走完强联之后，轮训时候可以优先拉一下同教师的）
// 2.
//

