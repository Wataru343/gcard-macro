using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Net;
using System.IO;

namespace gcard_macro
{
    class Event
    {
        virtual protected IWebDriver driver_ { get; set; }
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
        virtual public bool ReceivePresent { get; set; }
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
            Interval,
            EventFinished,
            Unknown,
            AssaultOperationHome,
            AssaultOperationRequest,
            AssaultOperationRequestSubmit,
            AssaultOperationRequestComplete,
            AssaultOperationPresentList,
            AssaultOperationWin,
            AssaultOperationStart,
            AssaultOperationFaildRequestJoin,
            GroupSelectJobs,
            GroupUseBoost,
            PromotionWithdrawalConfirmation,
            PromotionWithdrawalCompletion,
            PromotionSallyConfirmation,
            GTacticsStrategicArea
        }

        public enum AttackMode
        {
            Unlimited,
            ComboOnly,
            OneAttack,
            攻撃力が低い敵を攻撃撤退無し,
            攻撃力が低い敵を攻撃HP20パーセント以下で撤退,
            PTが高い敵を攻撃撤退無し,
            PTが高い敵を攻撃HP20パーセント以下で撤退,
            攻撃力割るMS数が低い敵を攻撃撤退無し,
            攻撃力割るMS数が低い敵を攻撃HP20パーセント以下で撤退
        }

        public Event(IWebDriver driver, string home_path)
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
                System.Threading.Thread.Sleep(1);
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
        virtual protected bool IsEnemyList() => driver_.PageSource.IndexOf("戦況を更新") >= 0 && driver_.PageSource.IndexOf("敵を見つける") >= 0;

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
        /// 応援依頼完了画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsRequestComplete() => driver_.PageSource.IndexOf("応援依頼完了") >= 0;


        /// <summary>
        /// 探索時のFlash画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsSearchFlash() => driver_.PageSource.IndexOf("http://gcc-a.sp.mbga.jp/smart/gcard/js/vendor/screener.min.js") >= 0;

        /// <summary>
        /// 戦闘時のFlash画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsFightFlash() => driver_.PageSource.IndexOf("http://gcc-a.sp.mbga.jp/smart/gcard/js/min/sp.pex.fight") >= 0;

        /// <summary>
        /// レベルアップ画面判定
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
        /// インターバル中判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsHomeDuringInterval() => driver_.PageSource.IndexOf("インターバル中です") >= 0;

        /// <summary>
        /// 不正な画面遷移画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsError() => driver_.PageSource.IndexOf("不正な画面遷移です") >= 0 || driver_.PageSource.IndexOf("エラー") >= 0;

        /// <summary>
        /// 既に戦闘は終了しています画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsFightAlreadyFinished() => driver_.PageSource.IndexOf("既に戦闘は終了しています") >= 0 || driver_.PageSource.IndexOf("このボスと戦うことはできません") >= 0;

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
        virtual protected void EmulateClickFlash()
        {

            try
            {
                string swfUrl = GetSwfURL(driver_.PageSource);
                string swf = new string(GetSwfBinary(swfUrl, 1024 * 1024));

                //WebClient wc = GetWebClient();
                //wc.DownloadFile(swfUrl, string.Format(@"11.swf", DateTime.Now.ToLongTimeString()));
                //wc.Dispose();

                //探索演出
                if (swfUrl.IndexOf("gcard_mission_effect") > 0)
                {
                    SearchEnemy(driver_.Url);
                    driver_.Navigate().GoToUrl(home_path_);
                }
                //カードゲットチャンス
                else if (swfUrl.IndexOf("lucky") > 0)
                {
                    SearchEnemy(driver_.Url);

                }
                //レベルアップ演出
                else if (swfUrl.IndexOf("lvup") > 0)
                {
                    string resultUrl = swfUrl.Replace("effect", "result");
                    driver_.Navigate().GoToUrl(enemy_list_path_ == "" ? home_path_ : enemy_list_path_);
                    Exec = SearchState;
                    return;
                }
                //補給ボス演出
                else if (swfUrl.IndexOf("raid_boss_supply_boss_appear") > 0)
                {
                    string a_point = new string(swf.Substring(swf.IndexOf("a_point") + 12, 10).Where(c => char.IsNumber(c)).ToArray());
                    string b_point = new string(swf.Substring(swf.IndexOf("b_point") + 12, 10).Where(c => char.IsNumber(c)).ToArray());
                    string boss_eids = FilterNumComma(swf.Substring(swf.IndexOf("boss_eids") + 14));
                    string can_r = new string(swf.Substring(swf.IndexOf("can_r") + 10, 2).Where(c => char.IsNumber(c)).ToArray());
                    string g_point = new string(swf.Substring(swf.IndexOf("g_point", 8000) + 12, 10).Where(c => char.IsNumber(c)).ToArray());

                    string resultURL = @"http://gcc.sp.mbga.jp/_gcard_event309_raid_boss_receive_multi_result" +
                        "?a_point=" + a_point +
                        "&b_point=" + b_point +
                        "&boss_eids=" + boss_eids +
                        "&can_r=" + can_r +
                        "&g_point=" + g_point;

                    driver_.Navigate().GoToUrl(resultURL);
                }
                //昇格戦戦闘演出
                else if (swfUrl.IndexOf("promotion_battle") >= 0)
                {
                    string gift_key = new string(swf.Substring(swf.IndexOf("gift_key", 8000) + 13, 6).Where(c => char.IsNumber(c)).ToArray());
                    string new_en = new string(swf.Substring(swf.IndexOf("new_en") + 11, 2).Where(c => char.IsNumber(c)).ToArray());
                    string exchanged_tmp_be_num = new string(swf.Substring(swf.IndexOf("exchanged_tmp_be_num", 8000) + 25, 1).Where(c => char.IsNumber(c)).ToArray());
                    string old_en = new string(swf.Substring(swf.IndexOf("old_en", 8000) + 11, 2).Where(c => char.IsNumber(c)).ToArray());
                    string old_pt = new string(swf.Substring(swf.IndexOf("ol_pt", 8000) + 11, 8).Where(c => char.IsNumber(c)).ToArray());
                    string round_id = new string(swf.Substring(swf.IndexOf("round_id", 8000) + 13, 2).Where(c => char.IsNumber(c)).ToArray());
                    string e_id = new string(swf.Substring(swf.IndexOf("e_id", 8000) + 9, 10).Where(c => char.IsNumber(c)).ToArray());
                    string total_pt = new string(swf.Substring(swf.IndexOf("total_pt", 8000) + 13, 8).Where(c => char.IsNumber(c)).ToArray());
                    string deck_id = new string(swf.Substring(swf.IndexOf("deck_id", 8000) + 12, 1).Where(c => char.IsNumber(c)).ToArray());
                    string nr = new string(swf.Substring(swf.IndexOf("nr", 8000) + 7, 4).Where(c => char.IsNumber(c)).ToArray());
                    string round_gift = new string(swf.Substring(swf.IndexOf("round_gift", 8000) + 15, 9).Where(c => char.IsNumber(c)).ToArray());
                    string win = new string(swf.Substring(swf.IndexOf("win", 8000) + 7, 4).Where(c => char.IsNumber(c)).ToArray());
                    string br = new string(swf.Substring(swf.IndexOf("br", 8000) + 7, 4).Where(c => char.IsNumber(c)).ToArray());
                    string win_all = new string(swf.Substring(swf.IndexOf("win_all", 8000) + 12, 2).Where(c => char.IsNumber(c)).ToArray());
                    string perfect = new string(swf.Substring(swf.IndexOf("perfect", 8000) + 12, 1).Where(c => char.IsNumber(c)).ToArray());
                    string get_pt = new string(swf.Substring(swf.IndexOf("get_pt", 8000) + 11, 7).Where(c => char.IsNumber(c)).ToArray());
                    string rank_up_incentive_flg = new string(swf.Substring(swf.IndexOf("rank_up_incentive_flg", 8000) + 12, 2).Where(c => char.IsNumber(c)).ToArray());
                    string new_pt = new string(swf.Substring(swf.IndexOf("new_pt", 8000) + 11, 8).Where(c => char.IsNumber(c)).ToArray());

                    string resultURL = @"http://gcc.sp.mbga.jp/_gcard_promotion_battle_result?" +
                        "gift_key=" + gift_key +
                        "&new_en=" + new_en +
                        "&exchanged_tmp_be_num=" + exchanged_tmp_be_num +
                        "&old_en=" + old_en +
                        "&old_pt=" + old_pt +
                        "&round_id=" + round_id +
                        "&e_id=" + e_id +
                        "&total_pt=" + total_pt +
                        "&deck_id=" + deck_id +
                        "&nr=" + nr +
                        "&round_gift=" + round_gift +
                        "&win=" + win +
                        "&br=" + br +
                        "&win_all=" + win_all +
                        "&perfect=" + perfect +
                        "&get_pt=" + get_pt +
                        "&rank_up_incentive_flg=" + rank_up_incentive_flg +
                        "&new_pt=" + new_pt;

                    driver_.Navigate().GoToUrl(resultURL);
                }
                else
                {
                    driver_.Navigate().GoToUrl(home_path_);
                }
            }
            catch
            {
                try
                {
                    int retry = 0;
                    while (true)
                    {
                        IWebElement elm = driver_.FindElement(By.ClassName("swf"));

                        Actions action = new Actions(driver_);
                        action.MoveToElement(elm, (int)(elm.Size.Width / 2.6), elm.Size.Height / 6 * 5).Click().Build().Perform();
                        Wait(0.5);
                        retry++;

                        if(retry > 10)
                        {
                            driver_.Navigate().GoToUrl(home_path_);
                            break;
                        }
                    }
                }
                catch
                {
                    driver_.Navigate().GoToUrl(home_path_);
                }
            }

            Exec = SearchState;
        }

        protected string GetSwfURL(string PageSource)
        {
            //var swf = '_gcard_event309_raid_boss_supply_boss_appear_effect?sk=91124';
            Regex r = new Regex(@"(var swf = '|'swf': ')(?<type>.+)(';|',)(\n|\r|.*'target')", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            MatchCollection mc = r.Matches(PageSource.Replace("&amp;", "&"));
            string url = mc.Count > 0 ? @"http://gcc.sp.mbga.jp/" + mc[0].Groups["type"].Value : "";
            return url;
        }

        protected string FilterNumComma(string SwfBinary)
        {
            string ret = "";

            foreach (char c in SwfBinary)
            {
                if (char.IsNumber(c) || c == ',')
                {
                    ret += c;
                }
                else break;
            }

            return System.Web.HttpUtility.UrlEncode(ret);
        }

        protected string GetTid(string SwfBinary)
        {
            return FilterNumComma(SwfBinary.Substring(SwfBinary.IndexOf("t_id") + 9));
        }

        protected string GetMid(string SwfBinary) => SwfBinary.Substring(SwfBinary.IndexOf("m_id") + 9, 8);

        protected string GetToken(string SwfBinary) => new string(SwfBinary.Substring(SwfBinary.IndexOf("token") + 10, 6).Where(c => char.IsNumber(c)).ToArray());

        protected string GetLuckyToken(string SwfBinary) => new string(SwfBinary.Substring(SwfBinary.IndexOf("token") + 6, 8).Where(c => char.IsNumber(c)).ToArray());

        protected string GetType(string SwfBinary) => SwfBinary.Substring(SwfBinary.IndexOf("type") + 9, 1)[0] == 'c' ? "chance" : "return";

        protected WebClient GetWebClient()
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers[HttpRequestHeader.UserAgent] = "Mozilla /5.0 (iPhone; CPU iPhone OS 9_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13B5110e Safari/601.1";
            string cc = string.Join("; ", driver_.Manage().Cookies.AllCookies.Select(c => string.Format("{0}={1}", c.Name, c.Value)));
            client.Headers[HttpRequestHeader.Cookie] = string.Join("; ", driver_.Manage().Cookies.AllCookies.Select(c => string.Format("{0}={1}", c.Name, c.Value)));

            return client;
        }

        /// <summary>
        /// swfのバイナリデータを取得する
        /// </summary>
        /// <param name="swfUrl">swfのurl</param>
        /// <returns>バイナリデータ</returns>
        char[] GetSwfBinary(string swfUrl, int readCount)
        {
            try
            {
                using (WebClient clientSwf = GetWebClient())
                using (Stream streamSwf = clientSwf.OpenRead(swfUrl))
                {
                    streamSwf.ReadTimeout = 5000;
                    using (BinaryReader br = new BinaryReader(streamSwf, Encoding.ASCII))
                    {
                        return br.ReadChars(readCount);
                    }
                }
            }
            catch
            {
                return new char[] { };
            }
        }


        /// <summary>
        /// 敵を出現させる
        /// </summary>
        /// <param name="url">探索FlashページのURL</param>
        /// <returns>成否</returns>
        protected bool SearchEnemy(string url)
        {
            try
            {
                using (WebClient client = GetWebClient())
                using (Stream stream = client.OpenRead(url))
                {
                    stream.ReadTimeout = 5000;
                    using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                    {
                        string pageSource = sr.ReadToEnd();
                        string swfUrl = GetSwfURL(pageSource);

                        string swf = new string(GetSwfBinary(swfUrl, 3000));
                        string resultURL = "";


                        //カードチャンスであれば
                        if (swfUrl.IndexOf("lucky") >= 0)
                        {
                            string token = GetLuckyToken(swf);
                            resultURL = (@"http://gcc.sp.mbga.jp/_gcard_mission_lucky_lot" + "?token=" + token + "&card=1&mekuru=0");
                            driver_.Navigate().GoToUrl(resultURL);
                            return false;
                        }
                        else
                        {
                            string tid = GetTid(swf);
                            string mid = GetMid(swf);
                            string token = GetToken(swf);
                            string type = GetType(swf);
                            string saveURL = @"http://gcc.sp.mbga.jp/_gcard_mission_save";
                            if (type == "return")
                            {
                                resultURL = saveURL + "?t%5Fid=" + tid + "&m%5Fid=" + mid + "&token=" + token;
                            }
                            else
                            {
                                resultURL = saveURL + "?t%5Fid=" + tid + "&m%5Fid=" + mid + "&type=" + type + "&token=" + token;
                            }
                        }

                        using (WebClient clientGet = GetWebClient())
                        {
                            string respons = clientGet.DownloadString(resultURL);

                            driver_.Navigate().Refresh();

                            return true;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 戦闘中のFlashをクリックする
        /// </summary>
        virtual protected void ClickBattleFlash()
        {
            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//*[@id=\"main\"]/canvas"));
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
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"ラッキーチャンスゲームに挑戦\"]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                Exec = SearchState;
                return;
            }
            catch { }


            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//div[text()=\"探索する\"]"));
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
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"売却して探索する\"]"));
                driver_.Navigate().GoToUrl(enemy_list_path_);
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
                //var elms = driver_.FindElements(By.XPath("//input[@value=\"報酬を受け取る\"]"));
                var elms = driver_.FindElements(By.XPath("//a[text()=\"敵一覧\"]"));
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
        /// 応援依頼完了から戦闘画面 or 探索へ
        /// </summary>
        virtual protected void MoveRequestCompleteToBattle()
        {
            try
            {
                driver_.Navigate().GoToUrl(enemy_list_path_);
                //IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"ボス戦に戻る\"]");
                //driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                Exec = SearchState;
                return;
            }
            catch { }

            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"続けて探索する\"]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
            }
            catch
            {
                try
                {
                    driver_.Navigate().GoToUrl(home_path_);
                }
                catch { }
            }


            Exec = SearchState;
        }

        /// <summary>
        /// 受け取りからプレゼントリストへ
        /// </summary>
        virtual protected void MoveReceiveToPresentList()
        {
            try
            {
                if (ReceivePresent)
                {
                    IWebElement elm = driver_.FindElement(By.XPath("//a[@href=\"_gcard_gifts\"]"));
                    driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                }
                else
                {
                    driver_.Navigate().GoToUrl(enemy_list_path_);
                }
            }
            catch
            {
                driver_.Navigate().GoToUrl(home_path_);
            }

            Exec = SearchState;
        }

        /// <summary>
        /// プレゼント一覧からプレゼント受け取り or 探索へ
        /// </summary>
        virtual protected void MovePresentListToPresent()
        {
            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//input[@value=\"プレゼントをまとめて受け取る\"]/../../../form"));
                elm.Submit();
                Exec = SearchState;
                return;
            }
            catch { }


            try
            {
                driver_.Navigate().GoToUrl(enemy_list_path_);
                Exec = SearchState;
                if (enemy_list_path_ == "")
                    throw new Exception();
            }
            catch
            {
                try
                {
                    IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"敵一覧\"]"));
                    driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                }
                catch { }
            }

            Exec = SearchState;
        }

        /// <summary>
        /// ボスから戦闘へ
        /// </summary>
        virtual protected void MoveBossToBattle()
        {
            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"ボスと戦う\"]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
            }
            catch { }

            Exec = SearchState;
        }

        virtual protected bool IsAttacked(string url) => AttackedEnemyId.LastIndexOf(GetEnemyId(url)) >= 0;

        virtual protected void AddEnemyId(string url) => AttackedEnemyId.Add(GetEnemyId(url));

        virtual protected void RemoveEnemyId(string url) => AttackedEnemyId.RemoveAll(elm => elm == GetEnemyId(url));

        virtual protected string GetEnemyId(string url)
        {
            Regex r = new Regex(@"id=(?<id>[0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            MatchCollection mc = r.Matches(url);

            return mc.Count > 0 ? mc[0].Groups["id"].Value : "";
        }

        protected string RemoveTag(string hrml)
        {
            Regex r = new Regex(@"(<.*?>)+");
            MatchCollection mc = r.Matches(hrml);
            StringBuilder sb = new StringBuilder(hrml);

            foreach (Match m in mc)
                for (int i = 0; i < m.Groups.Count; i++)
                    for (int j = 0; j < m.Groups[i].Captures.Count; j++)
                        sb.Replace(m.Groups[i].Captures[j].Value, "");

            return sb.ToString();
        }
    }
}
