using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using Model;
using LG.Utility;
using System.Text.RegularExpressions;
using BLL;
using System.Threading;
namespace SuperDo {
    public partial class Form1 : Form {
        private SuperHander _bllSuper = new SuperHander();
        public Form1() {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.timer1.Interval = 1000;
            this.timer2.Interval = 10000;
            this.timer2.Start();
        }

        private bool _IsStop = false;
        //private static List<Goods> _MyData=new List<Goods>();
        /// <summary>
        /// 出价监控
        /// </summary>
        private void DoAutionHander(CookieCollection cookie) {
            //ClearLog();
            string msg;
            var goodsIds = this.textBox2.Text.ToSplitList<int>(',');
            foreach (var item in goodsIds) {
                WriteLog("GoodId="+item, true);
                var goodsPriceDatas = _bllSuper.GetGoodsIdByUrl(item, cookie, out msg);
                bool isHave;
                var minPrice = _bllSuper.GetMinPirce(goodsPriceDatas, out isHave);
                if (minPrice < (decimal)0.01 || minPrice > 1000) {
                    WriteLog("获取最低价格错误！！", false);
                    continue;
                }
                var isMy = _bllSuper.IsMyMinPrice(item, minPrice,cookie);
                if (isMy) {
                    WriteLog("最低价格：" + minPrice + ",是我自己出的！！", false);
                    continue;
                }
                //出价
                var isSc =_bllSuper.AutionPrice(item, minPrice, cookie,out msg);
                if (isSc) {
                    WriteLog("监测出价成功!!");
                } else {
                    WriteLog("监测出价失败!!");
                }
                WriteLog(msg);
                WriteLog("", false);
            }
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="str"></param>
        public void WriteLog(string str,bool? isStart=null) {
            if (isStart.HasValue && isStart.Value) {
                this.textBox1.Text +="--------------开始-----------------\r\n";
            }
            this.textBox1.Text +=  str + "\r\n";
            if (isStart.HasValue && !isStart.Value) {
                this.textBox1.Text += "--------------结束-----------------\r\n";
            }
        }
        /// <summary>
        /// 清理日志
        /// </summary>
        public void ClearLog() {
            this.textBox1.Text = "";
        }
        ///// <summary>
        ///// 添加我的出价记录
        ///// </summary>
        ///// <param name="goodsId"></param>
        ///// <param name="price"></param>
        ///// <returns></returns>
        //public bool AddMyData(int goodsId, decimal price) {
        //    var myData = _MyData.FirstOrDefault(d => d.GoodsId == goodsId);
        //    if (myData == null) {
        //        var model = new Goods { GoodsId = goodsId };
        //        model.Price = new List<decimal> { price };
        //        _MyData.Add(model);
        //        return true;
        //    }
        //    if (myData.Price.FindIndex(d => d == price) >= 0) return false;
        //    myData.Price.Add(price);
        //    return true;
        //}
       
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e) {
            this._IsStop = true;
        }
        /// <summary>
        /// 开启
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e) {
            DoAutionHander(CommonConfig.GetLoginCookie());
            _IsStop = false;
            this.timer1.Start();
        }
        /// <summary>
        /// 定时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e) {
            if (!_IsStop) {
                DoAutionHander(CommonConfig.GetLoginCookie());
            } else {
                this.timer1.Stop();
            }
        }
        private Thread tRun;
        /// <summary>
        /// 获取积分
        /// </summary>A
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e) {
            string msg;
            tRun = new Thread(new ParameterizedThreadStart(new Action<object>(delegate(object o) {
                foreach (var item in CommonConfig.GetLoginCookies()) {
                    var isSc = _bllSuper.DoSign(item.Key, item.Value, out msg);
                    WriteLog(msg);
                }
            })));
            this.timer2.Start();
            tRun.Start();
            //本地测试
            //var isSc = _bllSuper.DoSign("563644741@qq.com",CommonConfig.GetTestLoginCookie(), out msg);
            //WriteLog(msg);
        }
        /// <summary>
        /// 清理日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e) {
            ClearLog();
        }
        /// <summary>
        /// 线程监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e) {
            if (!tRun.IsAlive) {
                tRun.Abort();
                this.timer2.Stop();
            }
        }
    }
}
