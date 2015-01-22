using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;
using Model;
using LG.Utility;
using System.Text.RegularExpressions;
using System.Net;
namespace BLL
{
    public class SuperHander {

        #region "积分竞拍"
        /// <summary>
        /// 获取商品我的出价记录
        /// </summary>
        /// <param name="goodId"></param>
        /// <returns></returns>
        public List<decimal> GetMyGoods(int goodId,CookieCollection cookie) {
            List<decimal> rtnDatas = new List<decimal>();
            if (goodId <= 0) return null;
            var prizeHtmlStr = HttpAjax.GetHttpContent(
               RequestType.POST,
               CommonConfig.GetMyPirceURL,
               cookie,
               null,
               null,
               timeoutMillisecond: 60000
           );
            if (prizeHtmlStr.IsNullOrWhiteSpace()) return rtnDatas;
            Regex r = new Regex("data-id=\"" + goodId + "\".*?<dl class=\"bid-history J_bid_history\">(.*?)</dl>", RegexOptions.Singleline);
            var match = r.Match(prizeHtmlStr);
            if (match == null || match.Length <= 0) return rtnDatas;
            var g = match.Groups;
            if (g == null || g.Count < 2) return rtnDatas;
            var myHistroyStr = g[1].Value;
            Regex rPrice = new Regex("<span>([^<]*)</span>元", RegexOptions.Singleline);
            var priceMatch = rPrice.Matches(myHistroyStr);
            if (priceMatch == null || priceMatch.Count <= 0) return rtnDatas;
            for (int i = 0; i < priceMatch.Count; i++) {
                var priceG = priceMatch[i].Groups;
                if (priceG == null || priceG.Count < 2) continue;
                rtnDatas.Add(decimal.Parse(priceG[1].Value));
            }
            return rtnDatas;
        }

        /// <summary>
        /// 获取最小出价，如果已经存在最小出价，isHave=true
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="isHave"></param>
        /// <returns></returns>
        public decimal GetMinPirce(List<GoodsAuctionPrice> datas, out bool isHave) {
            isHave = false;
            if (datas == null || datas.Count <= 0) return 0;
            decimal s = (decimal)0.01;
            decimal e = 5;
            for (decimal i = s; i < e; i = i + (decimal)0.01) {
                var dataItem = datas.FirstOrDefault(d => d.AuctionPrice == i);
                if (dataItem == null) return i;
                if (dataItem.AuctionCount > 1) continue;
                isHave = true;
                return i;
            }
            return 0;
        }

        /// <summary>
        /// 做出价操作
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="minPrice"></param>
        /// <returns></returns>
        public bool AutionPrice(int goodsId, decimal minPrice,CookieCollection cookies, out string msg) {
            msg = string.Empty;
            var url = CommonConfig.DoAutionPriceURL;
            var htmlStr = HttpAjax.GetHttpContent(
               RequestType.POST,
               url,
               cookies,
               null,
               new Dictionary<string, string> { 
                    {"GId",goodsId.GetString()},
                    {"AP",minPrice.GetString()}
               },
               timeoutMillisecond: 60000
           );
            if (!htmlStr.Contains("Bingo")) {
                msg = "商品ID={0}出价报错！！！".FormatStr(goodsId);
                return false;
            }
            msg = "商品ID={0},最低出价={1}出价成功！！！".FormatStr(goodsId, minPrice);
            return true;

        }
        /// <summary>
        /// 判断价格是不是我自己出过最低记录
        /// </summary>
        /// <param name="prize"></param>
        /// <returns></returns>
        public bool IsMyMinPrice(int goodsId, decimal prize, CookieCollection cookies) {
            var myList = GetMyGoods(goodsId, cookies);
            return myList.FindIndex(d => d == prize) >= 0;
        }

        /// <summary>
        /// 获取商品所有出价记录
        /// </summary>
        /// <param name="goodsId"></param>
        public List<GoodsAuctionPrice> GetGoodsIdByUrl(int goodsId,CookieCollection cookie, out string errMsg) {
            errMsg = string.Empty;
            if (goodsId <= 0) {
                errMsg = "FK商品ID不存在！！";
                return null;
            }
            var url = CommonConfig.GetGoodsAuctionPriceDataURL.FormatStr(goodsId);
            var htmlStr = HttpAjax.GetHttpContent(
               RequestType.GET,
               url,
               cookie,
               null,
               null,
               timeoutMillisecond: 60000
           );
            if (htmlStr.IsNullOrWhiteSpace()) return null;
            Regex r = new Regex("\"report\":(\\[[^]]*\\])", RegexOptions.Singleline);
            if (r.Match(htmlStr).Length <= 0) return null;
            var g = r.Match(htmlStr).Groups;
            if (g == null || g.Count <= 0) return null;
            return g[1].Value.JSONDeserialize<List<GoodsAuctionPrice>>();
        }
        #endregion

        #region "积分"
        /// <summary>
        /// 获取我的积分
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public int GetMyJiFen(CookieCollection cookies) {
            var getUserJiFenStr = HttpAjax.GetHttpContent(
               RequestType.GET,
               CommonConfig.GetUserJiFenURL,
               cookies,
               null,
               null,
               timeoutMillisecond: 60000
             );
            Regex r = new Regex("\"Integral\":([0-9]*),", RegexOptions.Singleline);
            var match = r.Match(getUserJiFenStr);
            var jf = 0;
            if (match != null && match.Groups != null && match.Groups.Count > 1) jf = match.Groups[1].Value.GetInt(0,false);
            return jf;
        }

        /// <summary>
        /// 签到获取积分
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="cookies"></param>
        /// <param name="msgStr"></param>
        /// <returns></returns>
        public bool DoSign(string userName, CookieCollection cookies,out string msgStr) {
            msgStr = string.Empty;
            StringBuilder msg = new StringBuilder();
            //var cookies=CommonConfig.GetLoginCookies();
            var htmlStr = HttpAjax.GetHttpContent(
               RequestType.GET,
               CommonConfig.DoSignURL,
               cookies,
               null,
               null,
               timeoutMillisecond: 60000
             );
            var jf = GetMyJiFen(cookies); ;
            var isSc = false;
            if (htmlStr.Contains("Bingo")) {
                msg.Append("用户名：{0},签到成功！当前拥有积分：{1}\r\n".FormatStr(userName, jf));
            } else if (htmlStr.Contains("今日已签到")) {
                msg.Append("用户名：{0},已签到！当前拥有积分：{1}\r\n".FormatStr(userName, jf));
            }else {
                msg.Append("用户名：{0},签到失败！当前拥有积分：{1}\r\n".FormatStr(userName, jf));
            }
            msgStr = msg.ToString();
            return isSc;
        }
        #endregion
    }
}
