using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Delegate 
{
    // 热水器

    public class Heater 
    {
        private int temperature;
        public string type = "RealFire 001"; // 添加型号作为演示
        public string area = "China Xian"; // 添加产地作为演示
        //声明委托
        public delegate void BoiledEventHandler(Object sender, BoiledEventArgs e);
        public event BoiledEventHandler Boiled; //声明事件
        // 定义BoiledEventArgs 类，传递给Observer 所感兴趣的信息
        public class BoiledEventArgs : EventArgs {
            public readonly int temperature;
            public BoiledEventArgs(int temperature) {
            this.temperature = temperature;
            }
        }
        // 可以供继承自 Heater 的类重写，以便继承类拒绝其他对象对它的监视

        protected virtual void OnBoiled(BoiledEventArgs e) {
            if (Boiled != null) { // 如果有对象注册

            Boiled(this, e); // 调用所有注册对象的方法
            }
        }
        // 烧水。

        public void BoilWater() {
            for (int i = 0; i <= 100; i++) {
                temperature = i;
                if (temperature > 95) {
                    //建立BoiledEventArgs 对象。

                    BoiledEventArgs e = new BoiledEventArgs(temperature);
                    OnBoiled(e); // 调用 OnBolied 方法
                }
            }
        }
    }
    // 警报器

    public class Alarm {
        public void MakeAlert(Object sender, Heater.BoiledEventArgs e) {
            Heater heater = (Heater)sender; //这里是不是很熟悉呢？
            //访问 sender 中的公共字段
            Console.WriteLine("Alarm：{0} - {1}: ", heater.area, heater.type);
            Console.WriteLine("Alarm: 嘀嘀嘀，水已经 {0} 度了：", e.temperature);
            Console.WriteLine();
        }
    }
    // 显示器

    public class Display {
        public static void ShowMsg(Object sender, Heater.BoiledEventArgs e) { //静态方法

            Heater heater = (Heater)sender;
            Console.WriteLine("Display：{0} - {1}: ", heater.area, heater.type);
            Console.WriteLine("Display：水快烧开了，当前温度：{0}度。", e.temperature);
            Console.WriteLine();
        }
    }

    enum TicketType
    {
        /// <summary>
        /// 用户
        /// </summary>
        UserCookieName,
        /// <summary>
        /// 管理员

        /// </summary>
        MasterCookieName
    }

    public class CityBase
    {
        /// <summary>
        /// 城市编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 省份编号
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }
    }

    class Program1 {
        static void Main1() {
        Heater heater = new Heater();
        Alarm alarm = new Alarm();
        //heater.Boiled += alarm.MakeAlert; //注册方法
        //heater.Boiled += (new Alarm()).MakeAlert; //给匿名对象注册方法

        heater.Boiled += new Heater.BoiledEventHandler(alarm.MakeAlert); //也可以这么注册

        heater.Boiled += Display.ShowMsg; //注册静态方法

        heater.BoilWater(); //烧水，会自动调用注册过对象的方法

       

        Console.Write( TicketType.MasterCookieName.ToString()=="MasterCookieName");

        string xml = "<xml><row><AreaId>1</AreaId></row><row><AreaId>2</AreaId></row><row><AreaId>3</AreaId></row></xml>";
        System.Xml.XmlDocument xmlString = new System.Xml.XmlDocument();
        xmlString.LoadXml(xml);

        IList<CityBase> city = new List<CityBase>();
        CityBase model = new CityBase();
        model.CityName = "A";
        model.ID = 1;
        CityBase model1 = new CityBase();
        model.CityName = "B";
        model.ID = 2;
        city.Add(model);
        city.Add(model1);

        IList<CityBase> city2 = city.Where(item =>( item.ID == 2 && item.CityName == "C" )).ToArray();
        Console.Write(city2.Count);

        Console.Read();
        }
    }
}

