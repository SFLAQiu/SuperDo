﻿using System;
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
        #region "全局部变量"
        private SuperHander _bllSuper = new SuperHander();
        /// <summary>
        /// 是否停止
        /// </summary>
        private bool _IsStop = false;
        /// <summary>
        /// 初始化绑定我的所有出价是否成功
        /// </summary>
        private bool _IsBindSc = false;
        /// <summary>
        /// 获取积分线程
        /// </summary>
        private Thread _TGetJFRun;
        /// <summary>
        /// 获取积分线程
        /// </summary>
        private Thread _TBindAution;
        #endregion
        public Form1() {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            StartThreedMonitoring();
        }
        #region "辅助操作"
        /// <summary>
        /// 获取物品ID
        /// </summary>
        /// <returns></returns>
        private List<int> GetGoodsIds() {
            return this.textBox2.Text.ToSplitList<int>(',') ?? new List<int>();
        }
        /// <summary>
        /// 开启线程资源监控
        /// </summary>
        private void StartThreedMonitoring() {
            this.t_ThreedMonitoring.Interval = 10000;
            this.t_ThreedMonitoring.Start();
        }
        /// <summary>
        /// 关闭线程资源监控
        /// </summary>
        private void StopThreedMonitoring() {
            this.t_ThreedMonitoring.Stop();
            this.t_ThreedMonitoring.Dispose();
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
        /// <summary>
        /// 刷新出价日志
        /// </summary>
        public void RefleshAutionLog() {
            this.txt_AutionLog.Clear();
            foreach (var item in SuperHander.AutionDatas) {
                this.txt_AutionLog.Text += "商品{0}：\r\n".FormatStr(item.Key);// +"出价:{0}".FormatStr(item) + "\r\n";
                var autionDatas = item.Value;
                if (autionDatas == null) return;
                var datas = autionDatas.OrderBy(d => d);
                foreach (var autionItem in datas) {
                    this.txt_AutionLog.Text += "  出价:{0}".FormatStr(autionItem) + "\r\n";
                }
            }
        }
        /// <summary>
        /// 线程监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e) {
            if (_TBindAution != null && !_TBindAution.IsAlive) {
                _TBindAution.Abort();
            }
            if (_TGetJFRun != null && !_TGetJFRun.IsAlive) {
                _TGetJFRun.Abort();
            }
            if (_TBindAution != null && _TBindAution.IsAlive) {
                return;
            }
            if (_TGetJFRun != null && _TGetJFRun.IsAlive) {
                return;
            }
            StopThreedMonitoring();
        }
        /// <summary>
        /// 出价单人出价
        /// </summary>
        private void SuperDo() {
            string msg;
            bool needGo;
            foreach (var goodId in GetGoodsIds()) {
                WriteLog("物品{0}，正在接受神一般的出价！！！".FormatStr(goodId), true);
                bool isSc = _bllSuper.DoAutionHander(goodId, CommonConfig.GetLoginCookie(), out msg, out needGo);
                WriteLog(msg);
                WriteLog("", false);
                WriteLog("\r\n");
                RefleshAutionLog();
            }
        }
        /// <summary>
        /// 出价单人出价
        /// </summary>
        private void SuperLetDo() {
            string msg;
            bool needGo;
            foreach (var goodId in GetGoodsIds()) {
                WriteLog("开启疯狂模式！！！", true);
                WriteLog("物品{0}，正在接受神一般的出价！！！".FormatStr(goodId));
                var isRandom = this.check_People.Checked;
                var userCookieDatas = CommonConfig.GetLoginCookies(isNeedRandom: isRandom);
                foreach (var item in userCookieDatas) {
                    var personName = item.Key;
                    var cookies = item.Value;
                    WriteLog("正在接受{0},的轰炸！！！！".FormatStr(personName));
                    var jf = _bllSuper.GetMyJiFen(cookies);
                    if (jf <= 0) {
                        CommonConfig.GetLoginCookies().Remove(personName);
                        WriteLog("战士{0},牺牲积分不够！！！！".FormatStr(personName));
                        continue;
                    }
                    bool isSc = _bllSuper.DoAutionHander(goodId, cookies, out msg, out needGo);
                    WriteLog(msg);
                    RefleshAutionLog();
                    if (!needGo) {
                        WriteLog("战士{0},已经拿下！,坐观全局中。。。。".FormatStr(personName));
                        break;
                    }
                }
                WriteLog("", false);
                WriteLog("\r\n");
            }
        }

        private void Go() {
            _IsStop = false;
            _IsBindSc = false;
            //绑定下我的所有账号的出价记录
            BindMyAllAutionData();
            this.timer1.Interval = this.numericUpDown1.Value.GetInt(0, false) * 1000;
            this.timer1.Start();
        }

        private void LetGo() {
            _IsStop = false;
            _IsBindSc = false;
            //绑定下我的所有账号的出价记录
            BindMyAllAutionData();
            this.time_LetGo.Interval = this.numericUpDown1.Value.GetInt(0, false) * 1000;
            this.time_LetGo.Start();
        }

        #endregion
        /// <summary>
        /// 开启单人模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e) {
            Go();
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e) {
            this._IsStop = true;
        }
       
        /// <summary>
        /// 绑定用户所有出价（多线程）
        /// </summary>
        private void BindMyAllAutionData() {
            _TGetJFRun = new Thread(new ParameterizedThreadStart(new Action<object>(delegate(object o) {
                foreach (var goodId in GetGoodsIds()) {
                    WriteLog("正在绑定{0},所有用户的出价数据，请耐心等待！！".FormatStr(goodId));
                    if (!SuperHander.AutionDatas.ContainsKey(goodId.ToString())) SuperHander.AutionDatas.Add(goodId.ToString(), _bllSuper.GetAllAccountAution(goodId));
                }
                WriteLog("正在排除没有积分的用户，请耐心等待！！");
                _bllSuper.DoMoveNoJF();
                _IsBindSc = true;
            })));
            //开启线程监控
            StartThreedMonitoring();
            _TGetJFRun.Start();
        }

        /// <summary>
        /// 获取积分（多线程）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e) {
            string msg;
            _TGetJFRun = new Thread(new ParameterizedThreadStart(new Action<object>(delegate(object o) {
                foreach (var item in CommonConfig.GetLoginCookies()) {
                    var isSc = _bllSuper.DoSign(item.Key, item.Value, out msg);
                    WriteLog(msg);
                }
            })));
            StartThreedMonitoring();
            _TGetJFRun.Start();
            //本地测试
            //var isSc = _bllSuper.DoSign("563644741@qq.com",CommonConfig.GetTestLoginCookie(), out msg);
            //WriteLog(msg);
        }

        /// <summary>
        /// 定时执行单人出价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e) {
            if (_IsStop) {
                this.timer1.Stop();
                return;
            }
            if (!_IsBindSc) {
                WriteLog("绑定数据，还在进行中。。。");
                return;
            }
            SuperDo();
        }
      
      
        /// <summary>
        /// 轰炸(大家一起上)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e) {
            _IsStop = false;
            _IsBindSc = false;
            //绑定下我的所有账号的出价记录
            BindMyAllAutionData();
            this.time_LetGo.Interval = this.numericUpDown1.Value.GetInt(0, false) * 1000;
            this.time_LetGo.Start();
        }
        /// <summary>
        /// 轰炸
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void time_LetGo_Tick(object sender, EventArgs e) {
            if (_IsStop) {
                this.time_LetGo.Stop();
                return;
            }
            if (!_IsBindSc) {
                WriteLog("绑定数据，还在进行中。。。");
                return;
            }
            if (this.ckb_time.Checked) { 
                var nowDate=DateTime.Now;
                var eDateTime = nowDate.Date;
                eDateTime=eDateTime.AddHours(this.txt_hour.Text.GetInt(0, false));
                eDateTime=eDateTime.AddMinutes(this.txt_m.Text.GetInt(0, false));
                if (nowDate.CompareTo(eDateTime) >= 0) {
                    this.time_LetGo.Stop();
                    return;
                }
            }
            SuperLetDo();
        }
        /// <summary>
        /// 获取物品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_GetGoods_Click(object sender, EventArgs e) {
            WriteLog("主人，我正在努力加载物品数据。。。");
            var goodsInfos = _bllSuper.GetGoodsInfo() ?? new Dictionary<int, string>();
            List<int> goodsIds = new List<int>();
            foreach (var item in goodsInfos) {
                goodsIds.Add(item.Key);
                WriteLog("物品ID：{0}，{1}".FormatStr(item.Key,item.Value));
            }
            WriteLog("获取物品结束，获取到的总数为：{0}！！！".FormatStr(goodsInfos.Count));
            this.textBox2.Text = goodsIds.ToJoinStr(",");
        }
        /// <summary>
        /// 隐藏软件
        /// </summary>
        private void DoHideSoft() {
            this.ShowInTaskbar = false;
            this.myIcon.Icon = this.Icon;
            this.Hide();
        }
        /// <summary>
        /// 窗口大小变化出发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e) {
            //窗体最小化时   
            if (this.WindowState == FormWindowState.Minimized) {
                DoHideSoft();
            }  
        }
        /// <summary>
        /// 右下角任务栏，程序被点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myIcon_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
            }
            if (e.Button == MouseButtons.Right) {
                //显示鼠标右击菜单
                this.contextMenuStrip1.Show();
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopToolStripMenuItem_Click(object sender, EventArgs e) {
            this._IsStop = true;
        }
        /// <summary>
        /// 退出 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }
        /// <summary>
        /// GO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goToolStripMenuItem_Click(object sender, EventArgs e) {
            Go();
        }
        /// <summary>
        /// Let GO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void letGoToolStripMenuItem_Click(object sender, EventArgs e) {
            LetGo();
        }
        /// <summary>
        /// 关闭窗体中的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            //当用户点击窗体右上角X按钮或(Alt + F4)时 发生  
            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                DoHideSoft();
            }
        }

    }
}
