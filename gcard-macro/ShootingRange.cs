using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenQA.Selenium;

namespace gcard_macro
{
    class ShootingRange : Event
    {

        override public event LogHandler Log;
        public delegate void AutoStopEventHandler(object sender);
        public event AutoStopEventHandler AutoStopped;

        public uint ThresholdFocusShot { get; set; }
        public bool UseFocusShotDuringFever { get; set; }
        public bool UseFeverTip { get; set; }
        public bool AutoStop { get; set; }
        private bool IsFever { get; set; }

        public ShootingRange(IWebDriver driver, string home_path) : base(driver, home_path)
        {
            RunObj = new object();
            driver_ = driver;
            driver_.Navigate().GoToUrl(home_path);
            HomePath = home_path;
            Exec = SearchState;
            ThresholdFocusShot = 0;
            UseFocusShotDuringFever = false;
            UseFeverTip = false;
            AutoStop = false;
            IsFever = false;

            base.Log += OnLogBase;
        }

        override protected void SearchState()
        {
            try
            {
                //プレゼント受け取りリクエスト
                if (RecievePresentRequest)
                {
                    driver_.Navigate().GoToUrl("http://gcc.sp.mbga.jp/_gcard_gifts");
                    RecievePresentRequest = false;
                }
                //フィーバー突入確認画面
                else　if(IsUseFeverConfirmation())
                {
                    Log?.Invoke(this, "ページ移動：フィーバー突入確認画面");
                    Wait(WaitMisc);
                    Exec = UseFeverConfirmation;
                }
                //イベントホーム
                else if (IsHome())
                {
                    CurrentState = State.Home;
                    Wait(WaitMisc);
                    Exec = Shooting;
                }
                //探索フラッシュ
                else if (IsSearchFlash())
                {
                    if (CurrentState != State.SearchFlash)
                        Log?.Invoke(this, "ページ移動：Flash画面");
                    CurrentState = State.SearchFlash;
                    Wait(WaitMisc);
                    Exec = EmulateClickFlash;
                }
                //アクセスを制限
                else if (IsAccessBlock())
                {
                    Log?.Invoke(this, "ページ移動：アクセス制限通知画面");
                    CurrentState = State.AccessBlock;
                    Wait(WaitAccessBlock);
                    driver_.Navigate().GoToUrl(HomePath);
                }
                //不正な画面遷移です
                else if (IsError())
                {
                    Log?.Invoke(this, "ページ移動：不正な画面遷移通知画面");
                    CurrentState = State.Error;
                    Wait(WaitMisc);
                    driver_.Navigate().GoToUrl(HomePath);
                }
                //イベント終了
                else if (IsEventFinished())
                {
                    if (CurrentState != State.EventFinished)
                        Log?.Invoke(this, "ページ移動：イベント終了画面");
                    CurrentState = State.EventFinished;
                    Wait(10);
                }
                //サーバーエラー
                else if (IsServerError())
                {
                    if (CurrentState != State.Unknown)
                        Log?.Invoke(this, "サーバーエラー");
                    CurrentState = State.Unknown;
                    Wait(5);
                    driver_.Navigate().GoToUrl(HomePath);
                }
                else
                {
                    Log?.Invoke(this, "ページ移動：不明な画面");
                    CurrentState = State.Unknown;
                    Wait(WaitMisc);
                    driver_.Navigate().GoToUrl(HomePath);
                }
            }
            catch
            {
                try
                {
                    driver_.Navigate().GoToUrl(HomePath);
                }
                catch { }
            }
        }

        /// <summary>
        /// ホーム画面判定
        /// </summary>
        /// <returns></returns>
        override protected bool IsHome() => driver_.Url.IndexOf(HomePath) >= 0;

        /// <summary>
        /// フィーバー突入確認画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsUseFeverConfirmation() => driver_.PageSource.IndexOf("フィーバー突入確認") >= 0;

        /// <summary>
        /// イベントのホームから探索へ
        /// </summary>
        private void Shooting()
        {
            Exec = SearchState;

            try
            {
                int beampackNum = 0;

                if (driver_.PageSource.IndexOf("フィーバー中!!") >= 0)
                {
                    if(!IsFever) Log?.Invoke(this, "フィーバー突入");
                    IsFever = true;
                }
                else
                {
                    IsFever = false;
                    beampackNum = Convert.ToInt32(driver_.FindElement(By.XPath("//dt[contains(text(),\"ビームパック所持数\")]/../dd/span")).Text.Replace(",", ""));

                    //ビームパックが無くなったら終了
                    if(beampackNum < 5)
                    {
                        AutoStopped?.Invoke(this);
                        KillThread();
                        return;
                    }
                }

                //クエスト全クリアで終了
                if (AutoStop && (driver_.PageSource.IndexOf("Challengeクエストクリア!!") >= 0 || driver_.PageSource.IndexOf("congratulations") >= 0))
                {
                    AutoStopped?.Invoke(this);
                    KillThread();
                    return;
                }

                int accuracyRate = 0;
                try
                {
                    //命中率取得                
                    accuracyRate = Convert.ToInt32(driver_.FindElement(By.XPath("//div[@class=\"hit-rate\"]/div")).Text.Replace("%", ""));
                }
                catch
                {
                    //フィーバーパネルが出ているときは拡散のみ
                    accuracyRate = 0;
                    IWebElement elm = driver_.FindElement(By.XPath("//p[@class=\"flex w100p\"]/a"));
                    Log?.Invoke(this, "フィーバーパネル出現");
                    driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                    return;
                }

                if (UseFeverTip)
                {
                    try
                    {
                        IWebElement fever = driver_.FindElement(By.XPath("//div[@class=\"flex\"]/a"));
                        driver_.Navigate().GoToUrl(fever.GetAttribute("href"));
                        return;
                    }
                    catch { }
                }

                var elms = driver_.FindElements(By.XPath("//p[@class=\"flex w100p\"]/a"));

                string shotType = "";
                //命中率が閾値以上なら集中射撃
                if (accuracyRate >= ThresholdFocusShot || (IsFever && UseFocusShotDuringFever))
                {
                    shotType = "集中射撃";
                    driver_.Navigate().GoToUrl(elms[0].GetAttribute("href"));
                }
                else
                {
                    shotType = "拡散射撃";
                    driver_.Navigate().GoToUrl(elms[1].GetAttribute("href"));
                }


                if (IsFever)
                {
                    Log?.Invoke(this, string.Format("{0}(フィーバー中, 命中率:{1})", shotType, accuracyRate));
                }
                else
                {
                    Log?.Invoke(this, string.Format("{0}(残ビームパック数:{1}個, 命中率:{2})", shotType, beampackNum, accuracyRate));
                }
            }
            catch
            {
                driver_.Navigate().GoToUrl(HomePath);
            }
        }

        private void UseFeverConfirmation()
        {
            try
            {
                IWebElement fever = driver_.FindElement(By.XPath("//form[contains(@action,\"fever_chip_item_use\")]"));
                fever.Submit();
            }
            catch { }

            Exec = SearchState;
        }

        /// <summary>
        /// OnLog伝搬用
        /// </summary>
        /// <param name="sender">送信元クラス</param>
        /// <param name="text">テキスト</param>
        private void OnLogBase(object sender, string text) => this?.Log(this, text);
    }
}
