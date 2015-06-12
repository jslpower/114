using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Common.Mail;

namespace EyouSoft.ConsoleApp
{
    /*
     * 定时服务
     */
    class Program
    {
        static void Main()
        {
            //Console.Write("同步MQ好友");
            //SyncMQFriend.Run();
            //Console.Write("删除文件\n");
            //RemoveUploadFile.Run();
            Console.Write("邮件提醒用户\n");
            NoLoginMail.Run();
            //Console.Write("短信提醒长期未登录用户\n");
            //NoLoginSms.Run();

            //酒店缓存程序
            //HotelRQ.HotelRQMain(null);
        }
    }
}
