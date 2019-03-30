using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenQA.Selenium;

namespace gcard_macro
{
    class RecievePresent : Event
    {
        public RecievePresent(IWebDriver driver, string home_path) : base(driver, home_path)
        {
            RunObj = new object();
            driver_ = driver;
            driver_.Navigate().GoToUrl(home_path);
            HomePath = home_path;
            Exec = SearchState;
        }

        override protected void SearchState()
        {
            try
            {
                //イベントホーム
                if (IsHome())
                {
                    Wait(WaitMisc);
                    driver_.Navigate().GoToUrl("http://gcc.sp.mbga.jp/_gcard_gifts");
                }
                //プレゼント一覧
                else if (IsPresentList())
                {
                    Wait(WaitReceive);
                    Exec = MovePresentListToPresent;
                }
                //不正な画面遷移です
                else if (IsError())
                {
                    Wait(WaitMisc);
                    driver_.Navigate().GoToUrl(HomePath);
                }
                //サーバーエラー
                else if (IsServerError())
                {
                    KillThread();
                }
                else
                {
                    Wait(WaitMisc);
                    driver_.Navigate().GoToUrl(HomePath);
                }
            }
            catch { }
        }

        /// <summary>
        /// ホーム画面判定
        /// </summary>
        /// <returns></returns>
        override protected bool IsHome() => driver_.Url.IndexOf(HomePath) >= 0;


        /// <summary>
        /// プレゼント一覧からプレゼント受け取り or 探索へ
        /// </summary>
        override protected void MovePresentListToPresent()
        {
            Wait(WaitReceive);
            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//input[@value=\"プレゼントをまとめて受け取る\"]/../../../form"));
                elm.Submit();
                Exec = SearchState;
                return;
            }
            catch { }

            KillThread();

            Exec = SearchState;
        }
    }
}
