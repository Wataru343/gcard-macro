using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Net;
using System.IO;

namespace gcard_macro
{
    class Event
    {
        virtual protected ChromeDriver driver_ { get; set; }
        virtual protected string home_path_ { get; set; }
        virtual protected string enemy_list_path_ { get; set; }
        virtual protected System.Threading.Thread worker_thread { get; set; }
        virtual protected object RunObj { get; set; }
        virtual protected bool Attacked { get; set; }
        virtual protected bool IsCombo { get; set; }
        virtual protected List<string> AttackedEnemyId {get; set;}
        virtual protected int MinicapCount { get; set; }

        internal Action Exec;

        virtual public State CurrentState { get; protected set; }
        virtual public AttackMode Mode { get; set; }
        virtual public bool IsRun { get; set; }
        virtual public double WaitSearch { get; set; }
        virtual public double WaitBattle { get; set; }
        virtual public double WaitAttack { get; set; }
        virtual public double WaitReceive { get; set; }
        virtual public double WaitAccessBlock { get; set; }
        virtual public double WaitMisc { get; set; }
        virtual public ulong BaseDamage { get; set; }
        virtual public ulong EnemyCount { get; set; }
        virtual public int ReceiveCount { get; set; }
        virtual public bool ReceiveReword { get; set; }
        virtual public bool OnlySearch { get; set; }




        public enum State
        {
            Home,
            EventHome,
            Battle,
            BattleAssaultOperation,
            EnemyList,
            EnemyAppearance,
            SearchFlash,
            BattleFlash,
            RequestComplete,
            LevelUp,
            Error,
            Result,
            Receive,
            PresentList,
            FightAlreadyFinished,
            AccessBlock,
            Boss,
            GetCard,
            EventFinished,
            Unknown,
            AssaultOperationHome,
            AssaultOperationRequest,
            AssaultOperationRequestSubmit,
            AssaultOperationRequestComplete,
            AssaultOperationPresentList,
            AssaultOperationWin,
            AssaultOperationStart,
            GroupInterval,
            SelectJobs,
            UseBoost
        }

        public enum AttackMode
        {
            Unlimited,
            ComboOnly,
            OneAttack
        }

        public Event(ChromeDriver driver, string home_path)
        {
            driver_ = driver;            
            home_path_ = home_path;
            enemy_list_path_ = "";
            CurrentState = State.Home;
            Exec = SearchState;
            WaitSearch = 0.0;
            WaitBattle = 0.0;
            WaitAttack = 0.0;
            WaitReceive = 0.0;
            WaitAccessBlock = 0.0;
            WaitMisc = 0.0;
            BaseDamage = 0;
            AttackedEnemyId = new List<string>();
        }

        virtual public void CreateThread()
        {
            worker_thread = new System.Threading.Thread(SeleniumThread);
            IsRun = true;
            worker_thread.Start();
        }

        virtual public void KillThread()
        {
            lock (RunObj)
                IsRun = false;
        }

        virtual protected void SeleniumThread()
        {
            driver_.Navigate().GoToUrl(home_path_);
            while (IsRun)
            {
                System.Threading.Thread.Sleep(10);
                Exec();
            }
        }

        virtual protected void Wait(double second)
        {
            if (second * 1000 > 0)
            {
                System.Threading.Thread.Sleep((int)(second * 1000));
            }
        }

        virtual protected void SearchState()
        {
        }

        /// <summary>
        /// ホーム画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsHome() => driver_.Url == home_path_;

        /// <summary>
        /// 敵一覧画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsEnemyList() => driver_.PageSource.IndexOf("戦況を更新する") >= 0 && driver_.PageSource.IndexOf("敵を見つける") >= 0;

        /// <summary>
        /// 敵一覧画面(敵出現)判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsEnemyAppearance() => driver_.PageSource.IndexOf("戦況を更新する") >= 0 && (driver_.PageSource.IndexOf("敵を攻撃") >= 0 || driver_.PageSource.IndexOf("コンボチャンス") >= 0);

        /// <summary>
        /// 戦闘画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsBattle() => driver_.PageSource.IndexOf("バトルエネルギー") >= 0 && driver_.PageSource.IndexOf("撃破報酬をチェック") >= 0;

        /// <summary>
        /// 探索時のFlashかどうか
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsSearchFlash() => driver_.PageSource.IndexOf("http://gcc-a.sp.mbga.jp/smart/gcard/js/vendor/screener.min.js") >= 0;

        /// <summary>
        /// 戦闘時のFlashかどうか
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsFightFlash() => driver_.PageSource.IndexOf("http://gcc-a.sp.mbga.jp/smart/gcard/js/min/sp.pex.fight") >= 0;

        /// <summary>
        /// レベルアップ画面かどうか
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsLevelUp() => driver_.PageSource.IndexOf("heading-aー") >= 0 && driver_.PageSource.IndexOf("レベルアップ") >= 0;

        /// <summary>
        /// リザルト画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsResult() => driver_.PageSource.IndexOf("value=\"報酬を受け取る\"") >= 0;

        /// <summary>
        /// 受け取り画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsReceive() => driver_.PageSource.IndexOf("報酬") >= 0 && driver_.PageSource.IndexOf("受け取り") >= 0 && driver_.PageSource.IndexOf("プレゼントへ") >= 0;

        /// <summary>
        /// プレゼント一覧画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsPresentList() => driver_.PageSource.IndexOf("プレゼント一覧") >= 0;

        /// <summary>
        /// カード獲得画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsGetCard() => driver_.PageSource.IndexOf("カード獲得") >= 0;

        /// <summary>
        /// ボス画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsBoss() => driver_.PageSource.IndexOf("ボスが現れた") >= 0;

        /// <summary>
        /// 不正な画面遷移画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsError() => driver_.PageSource.IndexOf("不正な画面遷移です｡") >= 0 || driver_.PageSource.IndexOf("エラー") >= 0;

        /// <summary>
        /// 既に戦闘は終了しています画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsFightAlreadyFinished() => driver_.PageSource.IndexOf("既に戦闘は終了しています") >= 0;

        /// <summary>
        /// アクセス制限画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsAccessBlock() => driver_.PageSource.IndexOf("アクセスを制限") >= 0;

        /// <summary>
        /// イベント終了画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsEventFinished() => driver_.PageSource.IndexOf("イベントは終了しました") >= 0;





        /// <summary>
        /// 探索時のFlashをクリックする
        /// </summary>
        virtual protected void ClickSearchFlash()
        {
            try
            {
                WebClient client = GetWebClient();

                Stream stream = client.OpenRead(GetSwfURL(driver_.PageSource));
                BinaryReader br = new BinaryReader(stream, Encoding.ASCII);
                string source = new string(br.ReadChars(3000));

                string tid = GetTid(source);
                string mid = GetMid(source);
                string token = GetToken(source);
                string type = GetType(source);
                string saveURL = @"http://gcc.sp.mbga.jp/_gcard_mission_save";
                string resultURL = "";
                if (type == "return")
                {
                    resultURL = saveURL + "?t%5Fid=" + tid + "&m%5Fid=" + mid + "&token=" + token;
                }
                else
                {
                    resultURL = saveURL + "?t%5Fid=" + tid + "&m%5Fid=" + mid + "&type=" + type + "&token=" + token;
                }

                client.Dispose();
                client = GetWebClient();
                string respons = client.DownloadString(resultURL);
                driver_.Navigate().GoToUrl(enemy_list_path_);
                Exec = SearchState;

                stream.Close();
                br.Close();
                client.Dispose();
                return;
            }
            catch
            {
                try
                {
                    IWebElement elm = driver_.FindElementByClassName("swf");

                    Actions action = new Actions(driver_);
                    action.MoveToElement(elm, (int)(elm.Size.Width / 2.6), elm.Size.Height / 6 * 5).Click().Build().Perform();
                }
                catch { }
            }

            Exec = SearchState;
        }

        string GetSwfURL(string PageSource)
        {
            Regex r = new Regex(@"_gcard_mission_effect\?sk=(?<id>[0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            MatchCollection mc = r.Matches(PageSource);

            return mc.Count > 0 ? @"http://gcc.sp.mbga.jp/_gcard_mission_effect?sk=" + mc[0].Groups["id"].Value : "";
        }

        string GetTid(string SwfBinary)
        {
            string tidstr = SwfBinary.Substring(SwfBinary.IndexOf("t_id") + 9);
            string tid = "";

            foreach(char c in tidstr)
            {
                if (char.IsNumber(c) || c == ',')
                {
                    tid += c;
                }
                else break;
            }

            return System.Web.HttpUtility.UrlEncode(tid);
        }

        string GetMid(string SwfBinary) => SwfBinary.Substring(SwfBinary.IndexOf("m_id") + 9, 8);

        string GetToken(string SwfBinary) => new string(SwfBinary.Substring(SwfBinary.IndexOf("token") + 10, 6).Where(c => char.IsNumber(c)).ToArray());

        string GetType(string SwfBinary) => SwfBinary.Substring(SwfBinary.IndexOf("type") + 9, 1)[0] == 'c' ? "chance" : "return";

        WebClient GetWebClient()
        {
            WebClient client = new WebClient();
            client.Headers.Add("User-Agent", "Mozilla /5.0 (iPhone; CPU iPhone OS 9_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13B5110e Safari/601.1");
            string cc = string.Join("; ", driver_.Manage().Cookies.AllCookies.Select(c => string.Format("{0}={1}", c.Name, c.Value)));
            client.Headers[HttpRequestHeader.Cookie] = string.Join("; ", driver_.Manage().Cookies.AllCookies.Select(c => string.Format("{0}={1}", c.Name, c.Value)));

            return client;
        }

        /// <summary>
        /// 戦闘中のFlashをクリックする
        /// </summary>
        virtual protected void ClickBattleFlash()
        {
            try
            {
                IWebElement elm = driver_.FindElementByXPath("//*[@id=\"main\"]/canvas");

                Actions action = new Actions(driver_);
                action.MoveToElement(elm, 300, 520).Click().Build().Perform();
            }
            catch { }

            Exec = SearchState;
        }

        /// <summary>
        /// レベルアップからラッキーチャンスゲーム or 探索へ
        /// </summary>
        virtual protected void MoveLevelUpToSearch()
        {
            try
            {
                IWebElement elm = driver_.FindElementByXPath("//a[text()=\"ラッキーチャンスゲームに挑戦\"]");
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                Exec = SearchState;
                return;
            }
            catch { }


            try
            {
                IWebElement elm = driver_.FindElementByXPath("//div[text()=\"探索する\"]");
                elm.Submit();
            }
            catch { }

            Exec = SearchState;
        }

        /// <summary>
        /// カード獲得から探索へ
        /// </summary>
        virtual protected void MoveGetCardToSearch()
        {
            try
            {
                IWebElement elm = driver_.FindElementByXPath("//a[text()=\"売却して探索する\"]");
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
            }
            catch { }

            Exec = SearchState;
        }

        /// <summary>
        /// 戦闘結果から敵一覧へ
        /// </summary>
        virtual protected void MoveResultToEnemyList()
        {
            try
            {
                //var elms = driver_.FindElementsByXPath("//input[@value=\"報酬を受け取る\"]");
                var elms = driver_.FindElementsByXPath("//a[text()=\"敵一覧\"]");
                foreach (var e in elms)
                {
                    //e.Submit();
                    driver_.Navigate().GoToUrl(e.GetAttribute("href"));
                    break;
                }
            }
            catch { }

            Exec = SearchState;
        }

        /// <summary>
        /// 受け取りからプレゼントリストへ
        /// </summary>
        virtual protected void MoveReceiveToPresentList()
        {
            try
            {
                IWebElement elm = driver_.FindElementByXPath("//a[@href=\"_gcard_gifts\"]");
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
            }
            catch { }

            Exec = SearchState;
        }

        /// <summary>
        /// プレゼント一覧からプレゼント受け取り or 探索へ
        /// </summary>
        virtual protected void MovePresentListToPresent()
        {
            try
            {
                IWebElement elm = driver_.FindElementByXPath("//input[@value=\"プレゼントをまとめて受け取る\"]");
                elm.Submit();
                Exec = SearchState;
                return;
            }
            catch { }


            try
            {
                driver_.Navigate().GoToUrl(enemy_list_path_);
                Exec = SearchState;
            }
            catch { }
        }

        /// <summary>
        /// ボスから戦闘へ
        /// </summary>
        virtual protected void MoveBossToBattle()
        {
            try
            {
                IWebElement elm = driver_.FindElementByXPath("//a[text()=\"ボスと戦う\"]");
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
            }
            catch { }

            Exec = SearchState;
        }

        virtual protected bool IsAttacked(string url) => AttackedEnemyId.LastIndexOf(GetEnemyId(url)) >= 0;

        virtual protected void AddEnemyId(string url) => AttackedEnemyId.Add(GetEnemyId(url));

        virtual protected void RemoveEnemyId(string url) => AttackedEnemyId.Remove(GetEnemyId(url));

        virtual protected string GetEnemyId(string url)
        {
            Regex r = new Regex(@"id=(?<id>[0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            MatchCollection mc = r.Matches(url);

            return mc.Count > 0 ? mc[0].Groups["id"].Value : "";
        }
    }
}
