using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Helper {
    public class CommonConfig {
        /// <summary>
        /// 获取出价数据接口
        /// </summary>
        public const string GetGoodsAuctionPriceDataURL = "http://m.fanhuan.com/faxian/get_auction_report?productId={0}";
        /// <summary>
        /// 出价接口
        /// </summary>
        public const string DoAutionPriceURL= "http://www.fanhuan.com/ajax/DoAuction";
        /// <summary>
        /// 获取自己出过的价格接口
        /// </summary>
        public const string GetMyPirceURL = "http://www.fanhuan.com/ajax/GetNowActionAutionGoodsHtml";
        /// <summary>
        /// 签到
        /// </summary>
        public const string DoSignURL = "http://www.fanhuan.com/ajax/SignIn/?From=1";
        /// <summary>
        /// 获取用户积分
        /// </summary>
        public const string GetUserJiFenURL = "http://www.fanhuan.com/ajax/GetUserActionInfo/";
        /// <summary>
        /// 返还网登陆凭据
        /// </summary>
        /// <returns></returns>
        public static CookieCollection GetLoginCookie() {
            CookieCollection cookies = new CookieCollection();
            cookies.Add(new Cookie {
                Domain = ".fanhuan.com",
                Expires = DateTime.Now.AddYears(10),
                Path = "/",
                Name = "A9D5EMD96D5E5G",
                Value = "dhwOGtZvjjo10vvNq8zS95K92EJU/yc8qf8m1QAFu/k="
            });
            cookies.Add(new Cookie {
                Domain = ".fanhuan.com",
                Expires = DateTime.Now.AddYears(10),
                Path = "/",
                Name = "checkNum",
                Value = "48dce78d8b12ac914fb7686cbf6235ef"
            });
            cookies.Add(new Cookie {
                Domain = ".fanhuan.com",
                Expires = DateTime.Now.AddYears(10),
                Path = "/",
                Name = "userDetial",
                Value = "563644741%40qq.com%7c10158800%7c3"
            });
            cookies.Add(new Cookie {
                Domain = ".fanhuan.com",
                Expires = DateTime.Now.AddYears(10),
                Path = "/",
                Name = "user_name",
                Value = "563644741%40qq.com"
            });
            return cookies;
        }

        /// <summary>
        /// 返还网登陆凭据
        /// </summary>
        /// <returns></returns>
        public static CookieCollection GetTestLoginCookie() {
            CookieCollection cookies = new CookieCollection();
            cookies.Add(new Cookie {
                Domain = ".fanhuan.com",
                Expires = DateTime.Now.AddYears(10),
                Path = "/",
                Name = "A9D5EMD96D5E5G",
                Value = "dhwOGtZvjjo10vvNq8zS99duDaGFH43FqMWi35b0CgA="
            });
            cookies.Add(new Cookie {
                Domain = ".fanhuan.com",
                Expires = DateTime.Now.AddYears(10),
                Path = "/",
                Name = "checkNum",
                Value = "4700d926dd9c1a3824c1f3c9e03ad02a"
            });
            cookies.Add(new Cookie {
                Domain = ".fanhuan.com",
                Expires = DateTime.Now.AddYears(10),
                Path = "/",
                Name = "userDetial",
                Value = "563644741%40qq.com%7c10158800%7c3"
            });
            cookies.Add(new Cookie {
                Domain = ".fanhuan.com",
                Expires = DateTime.Now.AddYears(10),
                Path = "/",
                Name = "user_name",
                Value = "563644741%40qq.com"
            });
            return cookies;
        }
        /// <summary>
        /// 登陆Cookies
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string,CookieCollection> GetLoginCookies() {
            var datas = new  Dictionary<string,CookieCollection>();
            datas.Add("563644741@qq.com",
                new CookieCollection() { 
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "A9D5EMD96D5E5G",
                        Value = "dhwOGtZvjjo10vvNq8zS95K92EJU/yc8qf8m1QAFu/k="
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "checkNum",
                        Value = "48dce78d8b12ac914fb7686cbf6235ef"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "userDetial",
                        Value = "563644741%40qq.com%7c10158800%7c3"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "user_name",
                        Value = "563644741%40qq.com"
                    }
            });
            datas.Add("634246137@qq.com",
                new CookieCollection() { 
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "A9D5EMD96D5E5G",
                        Value = "83OZDnDJthT66F/JWCw1vC2KgC6uamFC4945CEOz6/s="
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "checkNum",
                        Value = "7086e89d95cbbbe692b3b4a2810e4b7d"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "userDetial",
                        Value = "634246137%40qq.com%7c10166332%7c3"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "user_name",
                        Value = "634246137%40qq.com"
                    }
            });
            datas.Add("543035699@qq.com",
                new CookieCollection() { 
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "A9D5EMD96D5E5G",
                        Value = "8nqdujRskaGGTmcZzqF3SNp0YYcGnQHpp3X2Wx/eT4o="
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "checkNum",
                        Value = "87ace9f71af01a908346eff41b3099e0"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "userDetial",
                        Value = "543035699%40qq.com%7c7861939%7c3"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "user_name",
                        Value = "543035699%40qq.com"
                    }
            });
            datas.Add("tmacdream@qq.com",
                new CookieCollection() { 
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "A9D5EMD96D5E5G",
                        Value = "L0kcVqB+LR4lBkQ7VbTLBqEwO3CwlaPanSSR5XGBCRE="
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "checkNum",
                        Value = "a83fbb8ccc3597e178fbac42dc92406d"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "userDetial",
                        Value = "tmacdream%40qq.com%7c10506486%7c3"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "user_name",
                        Value = "tmacdream%40qq.com"
                    }
            });

            datas.Add("2743636241@qq.com",
               new CookieCollection() { 
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "A9D5EMD96D5E5G",
                        Value = "puWafOMoUmGfz6KdvepoVfGPG6lLSvFajJfVwe50o5g="
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "checkNum",
                        Value = "ccc8820c4887fd71f4874b8bbd07c296"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "userDetial",
                        Value = "2743636241%40qq.com%7c10224923%7c3"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "user_name",
                        Value = "2743636241%40qq.com"
                    }
            });
            datas.Add("SFLYQ",
            new CookieCollection() { 
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "A9D5EMD96D5E5G",
                        Value = "LmYteBcwBDaRE+7+RD/BExn5i2KrcVzb"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "checkNum",
                        Value = "aea2a70bcf1fb1a137c0b83594a77d7e"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "userDetial",
                        Value = "SFLYQ%7c10255553%7c3"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "user_name",
                        Value = "SFLYQ"
                    }
            });

            datas.Add("645362641@qq.com",
                 new CookieCollection() { 
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "A9D5EMD96D5E5G",
                        Value = "11YrSuBo4IRDp7zz3FN0IUxG2ixJfKepsMZX+ki320Q="
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "checkNum",
                        Value = "9ff3258d51488a1cbca8c6c4fbbb2d83"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "userDetial",
                        Value = "645362641%40qq.com%7c10506534%7c3"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "user_name",
                        Value = "645362641%40qq.com"
                }
            });
            datas.Add("747882079@qq.com",
                new CookieCollection() { 
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "A9D5EMD96D5E5G",
                        Value = "SrUV05jmBlCqltCYIOqMm3ZbnaNOKtIrSEsbvlORfGc="
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "checkNum",
                        Value = "736b2e8c117d527fe2bbc5534e62b506"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "userDetial",
                        Value = "747882079%40qq.com%7c10506541%7c3"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "user_name",
                        Value = "747882079%40qq.com"
                }
            });
            datas.Add("539653093@qq.com",
                new CookieCollection() { 
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "A9D5EMD96D5E5G",
                        Value = "VqPRXCpDeJn11a5kTcyAoJ0OmMeR4AMKQSwplX7JsiA="
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "checkNum",
                        Value = "f214ba24963e2a4dfd907c80b967a008"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "userDetial",
                        Value = "539653093%40qq.com%7c10506549%7c3"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "user_name",
                        Value = "539653093%40qq.com"
                }
            });
            datas.Add("625087055@qq.com",
               new CookieCollection() { 
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "A9D5EMD96D5E5G",
                        Value = "jX38cu/SK0QuE5uLee+P1Ii9fF1FfEpZVpy3/hfLJaQ="
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "checkNum",
                        Value = "5f24d57b47ad1c43ea2d6f13307867a5"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "userDetial",
                        Value = "625087055%40qq.com%7c10506553%7c3"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "user_name",
                        Value = "625087055%40qq.com"
                }
            });
            datas.Add("348502309@qq.com",
              new CookieCollection() { 
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "A9D5EMD96D5E5G",
                        Value = "jMss2ORD74CMSMpc39sswxPBXtK7HJ+92vlfYcXex44="
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "checkNum",
                        Value = "b2580a9e4d7d7fa8a42662c25fb9fcbd"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "userDetial",
                        Value = "348502309%40qq.com%7c10506558%7c3"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "user_name",
                        Value = "348502309%40qq.com"
                }
            });
            datas.Add("357840734@qq.com",
              new CookieCollection() { 
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "A9D5EMD96D5E5G",
                        Value = "oU7odB9YFala38ZGySoKHf6et2FLGino8PVqBo7RzsY="
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "checkNum",
                        Value = "79052f3999f2557db1e6701b0bfc91a9"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "userDetial",
                        Value = "357840734%40qq.com%7c10506565%7c3"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "user_name",
                        Value = "357840734%40qq.com"
                }
            });

            datas.Add("yyq.maidi@qq.com",
             new CookieCollection() { 
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "A9D5EMD96D5E5G",
                        Value = "CdUsOXeuORO5Lr2FDFvEK/Iz4qVt91MTonfXlDK0IMs="
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "checkNum",
                        Value = "0502784aee5b0e652d987447e15b2547"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "userDetial",
                        Value = "yyq.maidi%40qq.com%7c10506580%7c3"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "user_name",
                        Value = "yyq.maidi%40qq.com"
                }
            });
            datas.Add("408388680@qq.com",
            new CookieCollection() { 
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "A9D5EMD96D5E5G",
                        Value = "WRcWCcbwfIR6OQbckAN5jnm6iTMWTr52j2bCiOInC0A="
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "checkNum",
                        Value = "b2de1903422be0b5d0c012b1e53eeb8e"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "userDetial",
                        Value = "408388680%40qq.com%7c10506591%7c3"
                    },
                    new Cookie {
                        Domain = ".fanhuan.com",
                        Expires = DateTime.Now.AddYears(10),
                        Path = "/",
                        Name = "user_name",
                        Value = "408388680%40qq.com"
                }
            });
            return datas;
        }
    }
}
