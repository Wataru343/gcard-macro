using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace gcard_macro
{
    class GTactics : Event
    {
        override public event StateChangedHandler StateChanged;
        override public event MinicapChangedHandler MinicapChanged;
        override public event LogHandler Log;
        public delegate void AreaChangedHandler(object sender, string area);
        public event AreaChangedHandler AreaChanged;

        public List<Area> Shield { get; set; }
        public AreaPriority Priority { get; set; }
        public bool UseForce { get; set; }
        public bool ForceCharge { get; set; }
        public ForcePattern StrategyAreaForcePattern { get; set; }
        public long PointDiff { get; set; }
        public bool Standby { get; set; }
        public double WaitForce { get; set; }
        public ulong SearchForceEnemyCount { get; set; }
        public List<bool> SearchForcePlace { get; set; }

        private bool IsAreaBattle { get; set; }
        private string CurrentArea { get; set; }
        private ulong StrongForceDamage { get; set; }
        private ulong WeakForceDamage { get; set; }
        private Force PrevUsedForce { get; set; }
        private int SearchForceIdx { get; set; }
        private long OurPoint { get; set; }
        private long EnemyPoint { get; set; }
        private int AttackCount { get; set; }
        private bool TargetAllClear { get; set; }
        private bool ShieldClear { get; set; }
        private System.Threading.Thread WatchThread { get; set; }
        private bool ChengeArea { get; set; }
        private int ChangeAreaIdx { get; set; }
        private int Gauge { get; set; }

        public enum Force
        {
            None,
            Weak,
            Strong,
        }

        public enum ForcePattern
        {
            Optimaze,
            StrongWeak,
            WeakWeak
        }

        public enum AreaPriority
        {
            None,
            StrategyArea,
            OnlyStrategyArea
        }

        public struct Area
        {
            public int Level { get; set; }
            public bool Enable { get; set; }
        }


        public GTactics(IWebDriver driver, string home_path) : base(driver, home_path)
        {
            RunObj = new object();
            driver_ = driver;
            driver_.Navigate().GoToUrl(home_path);
            home_path_ = home_path;
            CurrentState = State.Home;
            Exec = SearchState;
            WaitSearch = 0.0;
            WaitBattle = 0.0;
            WaitAttack = 0.0;
            WaitReceive = 0.0;
            WaitAccessBlock = 0.0;
            WaitMisc = 0.0;
            BaseDamage = 0;
            Shield = new List<Area>(10);
            IsAreaBattle = false;
            CurrentArea = "";
            StrongForceDamage = 0;
            WeakForceDamage = 0;
            PrevUsedForce = Force.None;
            SearchForceIdx = -1;
            OurPoint = 0;
            EnemyPoint = 0;
            AttackCount = 0;
            TargetAllClear = false;
            ShieldClear = false;
            WaitForce = 0.0;
            ChengeArea = false;
            ChangeAreaIdx = -1;
            Gauge = 0;
            SearchForceEnemyCount = 0;
            SearchForcePlace = new List<bool>();

            base.StateChanged += StateChangedBase;
            base.MinicapChanged += MiniCapChangedBase;
            base.Log += OnLogBase;
        }

        override public void CreateThread()
        {
            base.CreateThread();
            WatchThread = new System.Threading.Thread(WatchAreaThread);
            WatchThread.Start();
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
                //稼働時間外
                else if (IsOutOfTimeRange())
                {
                    if (CurrentState != State.None)
                    {
                        Log?.Invoke(this, "稼働時間外");
                        driver_.Navigate().GoToUrl(home_path_);
                    }
                    CurrentState = State.None;
                    Wait(1);
                }
                else if (ChengeArea)
                {
                    driver_.Navigate().GoToUrl(home_path_);
                    ChengeArea = false;
                }
                //イベントホーム
                else if (IsHome())
                {
                    if (!IsHomeDuringInterval())
                    {
                        if (CurrentState != State.Home)
                            Log?.Invoke(this, "ページ移動：イベントホーム画面");

                        CurrentState = State.Home;
                        Wait(WaitMisc);
                        Exec = MoveEventHomeToSearch;
                    }
                    else
                    {
                        if (CurrentState != State.Interval)
                            Log?.Invoke(this, "ページ移動：インターバル画面");

                        CurrentState = State.Interval;
                        Wait(10);
                        driver_.Navigate().Refresh();
                    }
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
                //戦闘フラッシュ
                else if (IsFightFlash())
                {
                    if (CurrentState != State.BattleFlash)
                        Log?.Invoke(this, "ページ移動：戦闘演出画面");
                    CurrentState = State.BattleFlash;
                    Wait(WaitMisc);
                    Exec = ClickBattleFlash;
                }
                //敵一覧
                else if (IsEnemyList())
                {
                    Log?.Invoke(this, "ページ移動：敵一覧画面");
                    CurrentState = State.EnemyList;
                    Exec = MoveEnemyListToSearch;

                    if (EnemyFound)
                    {
                        WaitForAccessLimit();
                        EnemyFound = false;
                    }
                }
                //戦闘画面
                else if (IsBattle())
                {
                    Log?.Invoke(this, "ページ移動：戦闘画面");
                    CurrentState = State.Battle;
                    Exec = Battle;
                }
                //戦闘リザルト
                else if (IsResult())
                {
                    Log?.Invoke(this, "ページ移動：戦闘リザルト画面");
                    CurrentState = State.Result;
                    Wait(WaitMisc);
                    Exec = MoveResultToEnemyList;
                }
                //報酬受け取り
                else if (IsReceive())
                {
                    Log?.Invoke(this, "ページ移動：報酬受け取り画面");
                    CurrentState = State.Receive;
                    Exec = MoveReceiveToPresentList;
                }
                //プレゼント一覧
                else if (IsPresentList())
                {
                    Log?.Invoke(this, "ページ移動：プレゼント一覧画面");
                    CurrentState = State.PresentList;
                    Exec = MovePresentListToPresent;
                }
                //レベルアップ
                else if (IsLevelUp())
                {
                    Log?.Invoke(this, "ページ移動：レベルアップ画面");
                    CurrentState = State.LevelUp;
                    Wait(WaitMisc);
                    Exec = MoveLevelUpToSearch;
                }
                //カード入手
                else if (IsGetCard())
                {
                    Log?.Invoke(this, "ページ移動：カード入手画面");
                    CurrentState = State.GetCard;
                    Wait(WaitMisc);
                    Exec = MoveGetCardToSearch;
                }
                //既に戦闘は終了しています
                else if (IsFightAlreadyFinished())
                {
                    Log?.Invoke(this, "ページ移動：戦闘終了済み通知画面");
                    CurrentState = State.FightAlreadyFinished;
                    Wait(WaitMisc);
                    driver_.Navigate().GoToUrl(home_path_);
                    Attacked = false;
                }
                //アクセスを制限
                else if (IsAccessBlock())
                {
                    Log?.Invoke(this, "ページ移動：アクセス制限通知画面");
                    CurrentState = State.AccessBlock;
                    Wait(WaitAccessBlock);
                    driver_.Navigate().GoToUrl(home_path_);
                }
                //不正な画面遷移です
                else if (IsError())
                {
                    Log?.Invoke(this, "ページ移動：不正な画面遷移通知画面");
                    CurrentState = State.Error;
                    Wait(WaitMisc);
                    driver_.Navigate().GoToUrl(home_path_);
                }
                //イベント終了
                else if (IsEventFinished())
                {
                    if (CurrentState != State.EventFinished)
                        Log?.Invoke(this, "ページ移動：イベント終了画面");
                    CurrentState = State.EventFinished;
                    Wait(10);
                }
                //フォース実行失敗
                else if (IsForceFaild())
                {
                    Log?.Invoke(this, "ページ移動：フォースの実行に失敗");
                    driver_.Navigate().GoToUrl(enemy_list_path_);
                    Wait(WaitMisc);
                    Exec = StrategicAreaBattle;
                }
                //戦略拠点
                else if (IsStrategicArea())
                {
                    //if (CurrentState != State.GTacticsStrategicArea)
                    //    Log?.Invoke(this, "ページ移動：戦略拠点");
                    CurrentState = State.GTacticsStrategicArea;
                    Wait(WaitMisc);
                    Exec = StrategicAreaBattle;
                }
                //燃料不足
                else if (IsFuelShortage())
                {
                    Log?.Invoke(this, "警告：燃料不足");
                    CurrentState = State.Home;
                    Wait(10);
                    driver_.Navigate().GoToUrl(home_path_);
                }
                //サーバーエラー
                else if (IsServerError())
                {
                    if (CurrentState != State.Unknown)
                        Log?.Invoke(this, "サーバーエラー");
                    CurrentState = State.Unknown;
                    Wait(5);
                    driver_.Navigate().GoToUrl(home_path_);
                }
                else
                {
                    Log?.Invoke(this, "ページ移動：不明な画面");
                    CurrentState = State.Unknown;
                    Wait(WaitMisc);
                    driver_.Navigate().GoToUrl(home_path_);
                }
            }
            catch
            {
                try
                {
                    driver_.Navigate().GoToUrl(home_path_);
                }
                catch { }
            }

            StateChanged?.Invoke(this, CurrentState);
        }

        /// <summary>
        /// エリア監視
        /// </summary>
        private void WatchAreaThread()
        {

            var cookies = driver_.Manage().Cookies.AllCookies;

            IWebDriver driverArea = new WebDriber.HtmlAgilityPackDriver("Mozilla /5.0 (iPhone; CPU iPhone OS 9_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13B5110e Safari/601.1");

            foreach (var c in cookies)
            {
                try
                {
                    driverArea.Manage().Cookies.AddCookie(c);
                }
                catch { }
            }
            driverArea.Navigate().GoToUrl(home_path_);

            Wait(1);

            while (IsRun)
            {
                if (IsAreaBattle)
                {
                    try
                    {
                        if (driverArea.PageSource.IndexOf("quest_result_effect") >= 0)
                            driverArea.Navigate().GoToUrl(home_path_);

                        IWebElement[] panel, link;
                        List<int> levels;
                        List<string> areaNames;

                        (panel, link, levels, areaNames) = GetAreaLevel(driverArea);


                        int currentAreaIdx = areaNames.IndexOf(CurrentArea);

                        if (Priority != AreaPriority.OnlyStrategyArea)
                        {
                            //中立を最優先する
                            //戦略拠点が先
                            if (Priority == AreaPriority.StrategyArea)
                            {
                                if (Shield[0].Enable && Shield[0].Level > 0 && levels[0] == 0)
                                {
                                    ChengeArea = currentAreaIdx != 0;
                                    ChangeAreaIdx = 0;
                                    goto EXIT;
                                }
                            }

                            //拠点優先の場合エリアレベルを先に上げる
                            if (Priority == AreaPriority.StrategyArea)
                            {
                                if (Shield[0].Enable && levels[0] < 0)
                                {
                                    ChengeArea = currentAreaIdx != 0;
                                    ChangeAreaIdx = 0;
                                    goto EXIT;
                                }
                            }

                            if (Priority == AreaPriority.StrategyArea)
                            {
                                if (Shield[0].Enable)
                                {
                                    if (Shield[0].Level > levels[0])
                                    {
                                        ChengeArea = currentAreaIdx != 0;
                                        ChangeAreaIdx = 0;
                                        goto EXIT;
                                    }
                                    else if (levels[0] == 3 && Gauge < 100)
                                    {
                                        ChengeArea = currentAreaIdx != 0;
                                        ChangeAreaIdx = 0;
                                        goto EXIT;
                                    }
                                }
                            }


                            for (int i = 1; i < panel.Count(); i++)
                            {
                                string className = panel[i].GetAttribute("class");
                                if (className.IndexOf("own") < 0 && className.IndexOf("enemy") < 0)
                                {
                                    if (Shield[i].Enable && Shield[i].Level >= levels[i])
                                    {
                                        ChengeArea = currentAreaIdx != i;
                                        ChangeAreaIdx = i;
                                        goto EXIT;
                                    }
                                }
                            }

                            //戦略拠点は後
                            if (Priority == AreaPriority.None)
                            {
                                if (Shield[0].Enable && Shield[0].Level > 0 && levels[0] == 0)
                                {
                                    ChengeArea = currentAreaIdx != 0;
                                    ChangeAreaIdx = 0;
                                    goto EXIT;
                                }
                            }


                            //次に敵エリアを優先する
                            for (int i = 1; i < panel.Count(); i++)
                            {
                                string className = panel[i].GetAttribute("class");
                                if (Shield[i].Enable && className.IndexOf("enemy") >= 0)
                                {
                                    ChengeArea = currentAreaIdx != i;
                                    ChangeAreaIdx = i;
                                    goto EXIT;
                                }
                            }

                            if (Priority == AreaPriority.None)
                            {
                                if (Shield[0].Enable && levels[0] < 0)
                                {
                                    ChengeArea = currentAreaIdx != 0;
                                    ChangeAreaIdx = 0;
                                    goto EXIT;
                                }
                            }

                            //最後に味方エリア
                            for (int i = panel.Count() - 1; i > 0; i--)
                            {
                                if (Shield[i].Enable && Shield[i].Level > levels[i])
                                {
                                    ChengeArea = currentAreaIdx != i;
                                    ChangeAreaIdx = i;
                                    goto EXIT;
                                }
                            }

                            if (Priority == AreaPriority.None)
                            {
                                if (Shield[0].Enable)
                                {
                                    if (Shield[0].Level > levels[0])
                                    {
                                        ChengeArea = currentAreaIdx != 0;
                                        ChangeAreaIdx = 0;
                                        goto EXIT;
                                    }
                                    else if (levels[0] == 3 && Gauge < 100)
                                    {
                                        ChengeArea = currentAreaIdx != 0;
                                        ChangeAreaIdx = 0;
                                        goto EXIT;
                                    }
                                }
                            }


                            if (Standby && PointDiff <= (OurPoint - EnemyPoint))
                            {
                                ChengeArea = true;
                                ChangeAreaIdx = -1;
                                goto EXIT;
                            }
                            else
                            {
                                int cIdx = currentAreaIdx;

                                if (cIdx <= 0)
                                {
                                    //待機中の場合有効な列を探す
                                    cIdx = Shield.FindIndex(1, e => e.Enable);

                                    if (cIdx == -1)
                                    {
                                        cIdx = Shield[0].Enable ? 0 : -1;
                                    }

                                    cIdx = cIdx == -1 ? -1 : cIdx;
                                }

                                if (cIdx == -1)
                                {
                                    ChengeArea = true;
                                    ChangeAreaIdx = -1;
                                    goto EXIT;
                                }
                                else
                                {
                                    //待機しない場合は有効な列の最上段を攻撃する
                                    //戦略拠点のみ有効の場合戦略拠点を攻撃する
                                    int upAreaIdx = cIdx > 0 ? (10 - (3 - (cIdx - 1) % 3)) : 0;
                                    ChengeArea = currentAreaIdx != upAreaIdx;
                                    ChangeAreaIdx = upAreaIdx;
                                }

                                goto EXIT;
                            }
                        }
                        else
                        {
                            if (!Shield[0].Enable)
                            {
                                ChengeArea = true;
                                ChangeAreaIdx = -1;
                                goto EXIT;
                            }

                            if (Standby)
                            {
                                if (levels[0] >= Shield[0].Level && PointDiff <= (OurPoint - EnemyPoint))
                                {
                                    ChengeArea = true;
                                    ChangeAreaIdx = -1;
                                    goto EXIT;
                                }
                                else
                                {
                                    ChengeArea = currentAreaIdx != 0;
                                    ChangeAreaIdx = 0;
                                    goto EXIT;
                                }
                            }
                            else
                            {
                                //待機設定していない場合はノンストップ
                                ChengeArea = currentAreaIdx != 0;
                                ChangeAreaIdx = 0;
                                goto EXIT;
                            }
                        }

                        EXIT:;
                    }
                    catch
                    {
                        Wait(3);
                    }
                }
                else
                {
                    if(driverArea.Url != home_path_)
                        driverArea.Navigate().GoToUrl(home_path_);
                    ChengeArea = false;
                    Wait(5);
                }
                Wait(1);
                driverArea.Navigate().Refresh();
            }
        }

        /// <summary>
        /// 敵一覧画面判定
        /// </summary>
        /// <returns></returns>
        override protected bool IsEnemyList() => driver_.PageSource.Length > 1536 && driver_.PageSource.IndexOf("戦況を", 1536) >= 0 && driver_.PageSource.IndexOf("更新する", 1536) >= 0;

        /// <summary>
        /// 戦略拠点画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsStrategicArea() => driver_.PageSource.Length > 1536 && driver_.Url.IndexOf("strategic_area") >= 0 && driver_.PageSource.IndexOf("js-force-submit", 1536) >= 0;

        /// <summary>
        /// フォース実行失敗画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsForceFaild() => driver_.PageSource.Length > 1536 && driver_.PageSource.IndexOf("フォースの実行に失敗しました", 1536) >= 0;

        /// <summary>
        /// イベントのホームから探索へ
        /// </summary>
        private void MoveEventHomeToSearch()
        {
            Exec = SearchState;
            TargetAllClear = false;
            ShieldClear = false;
            AttackCount = 0;
            CurrentArea = "";

            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//li[@class=\"search\" or @class=\"attack\"]/a"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                IsAreaBattle = false;

                return;
            }
            catch
            {
                IsAreaBattle = true;
                //戦略拠点
                try
                {
                    IWebElement[] panel, link;
                    List<int> levels;
                    List<string> areaNames;

                    (panel, link, levels, areaNames) = GetAreaLevel(driver_);

                    //エリアチェンジ
                    if (ChangeAreaIdx > -1)
                    {
                        string areaLink = link[ChangeAreaIdx].GetAttribute("data-js-modal-link").Replace("&amp;", "&");
                        driver_.Navigate().GoToUrl("http://gcc.sp.mbga.jp/" + areaLink);
                        Log?.Invoke(this, "エリア移動： " + areaNames[ChangeAreaIdx]);
                        CurrentArea = areaNames[ChangeAreaIdx];
                        AreaChanged?.Invoke(this, CurrentArea);
                        ChengeArea = false;
                        Exec = SearchState;
                        return;
                    }

                    Wait(1);
                    driver_.Navigate().Refresh();
                }
                catch
                {
                    try
                    {
                        IWebElement elm = driver_.FindElement(By.XPath("//div[contains(@class,\"position-9 js-battlemap-btn select_link\")]"));
                        string areaLink = elm.GetAttribute("data-js-modal-link").Replace("&amp;", "&");
                        driver_.Navigate().GoToUrl("http://gcc.sp.mbga.jp/" + areaLink);
                        Exec = SearchState;
                        return;
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// パネル要素、エリアへのリンク、エリアレベル、エリア名を取得
        /// </summary>
        /// <param name="driver">WebDriver</param>
        /// <returns>パネル要素、エリアへのリンク、エリアレベル、エリア名</returns>
        private (IWebElement[] panel, IWebElement[] link, List<int> levels, List<string> areaNames) GetAreaLevel(IWebDriver driver)
        {
            try
            {
                OurPoint = Convert.ToInt64(driver.FindElement(By.XPath("//p[@class=\"own-group-point\"]")).Text.Replace(",", "").Replace("pt", ""));
                EnemyPoint = Convert.ToInt64(driver.FindElement(By.XPath("//p[@class=\"enemy-group-point\"]")).Text.Replace(",", "").Replace("pt", ""));
            }
            catch { }

            IWebElement[] panel = driver.FindElements(By.XPath("//div[contains(@class,\"area-wrap\")]")).Take(10).Reverse().ToArray();
            var button = driver.FindElements(By.XPath("//div[contains(@class,\"area-btn area-btn-\")]")).Take(10).Reverse().ToArray();
            IWebElement[] link = driver.FindElements(By.XPath("//div[contains(@class, \"js-battlemap-btn select_link\")]")).Take(10).Reverse().ToArray();
            List<int> levels = new List<int>();
            List<string> areaNames = new List<string>();
            for (int i = 0; i < panel.Count(); i++)
            {
                try
                {
                    //シェルターレベル取得
                    string aa = panel[i].FindElements(By.XPath("img[contains(@src,\"shelter\")]")).Last().GetAttribute("src");
                    var shelterStr = aa.Split(new char[] { '/' }).Last().Split(new char[] { '_' });
                    string eo = shelterStr[1];
                    if(eo == "our")
                    {
                        string level = shelterStr[2].Substring(0, 1);
                        levels.Add(Convert.ToInt32(level));
                    }
                    else
                    {
                        levels.Add(-1);
                    }

                    areaNames.Add(button[i].Text);
                }
                catch
                {
                    if (i == 0)
                    {
                        try
                        {
                            var strategicAreaLevel = panel[0].FindElement(By.XPath("div")).GetAttribute("class").Split(new char[] { ' ' });

                            if (strategicAreaLevel[0] == "our")
                            {
                                //Shield["戦略拠点"] = Convert.ToInt32(strategicAreaLevel[1].Substring(2, 1));
                                levels.Add(Convert.ToInt32(strategicAreaLevel[1].Substring(2, 1)));
                                areaNames.Add("戦略拠点");
                            }
                            else
                            {
                                //Shield["戦略拠点"] = -1;
                                levels.Add(-1);
                                areaNames.Add("戦略拠点");
                            }
                        }
                        catch
                        {
                            //Shield["戦略拠点"] = -1;
                            levels.Add(-1);
                            areaNames.Add("戦略拠点");
                        }
                    }
                    else
                    {
                        //Shield[button[i].Text] = 0;
                        levels.Add(0);
                        areaNames.Add(button[i].Text);
                    }
                }
            }

            return (panel, link, levels, areaNames);
        }


        /// <summary>
        /// 敵一覧から戦闘へ
        /// </summary>
        private void MoveEnemyListToSearch()
        {
            if (!IsRun)
                return;

            if (enemy_list_path_ == "")
            {
                enemy_list_path_ = driver_.Url;
            }

            //報酬受け取り
            if (ReceiveReword)
            {
                try
                {
                    var rewords = driver_.FindElements(By.XPath("//*[text()=\"報酬を受け取る\"]"));
                    if (rewords.Count() >= ReceiveCount)
                    {
                        IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"報酬をまとめて受け取る\"]"));
                        driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                        Exec = SearchState;
                        return;
                    }
                }
                catch
                { }
            }

            List<IWebElement> enemys = null;

            try
            {
                //全ボタン検索                
                enemys = driver_.FindElements(By.XPath("//a[contains(@class,\"js-show-link\")]")).Where(e => e.GetAttribute("href")?.Length > 0).ToList();
                enemys.OrderBy(i => Guid.NewGuid()).ToList();
                if (enemys.Count == 0) AttackedEnemyId.Clear();
            }
            catch { }


            //探索のみ
            if (!OnlySearch)
            {
                if (UseForce && IsAreaBattle)
                {
                    //残りフォースを確認
                    var forceCounts = driver_.FindElements(By.XPath("//div[@class=\"force-count\"]/span"));
                    int forceCount = 0;
                    foreach (var fc in forceCounts)
                    {
                        if (fc.Text != "0")
                            forceCount++;
                    }

                    int forceChargeCount = 0;
                    try
                    {
                        forceChargeCount = Convert.ToInt32(driver_.FindElement(By.XPath("//span[@class=\"force-charge\"]")).Text);
                    }
                    catch { }


                    if (forceCount > 0 || (forceChargeCount > 0 && ForceCharge))
                    {
                        //フォース取得
                        var forces = driver_.FindElements(By.XPath("//div[contains(@class,\"js-force-submit\")]"));


                        //敵が6体未満なら探索フォースを使う
                        if (enemys.Count <= (int)SearchForceEnemyCount && !NoSearch)
                        {
                            //探索フォース未使用なら探す
                            //if (SearchForceIdx < 0 && enemys.Count == 0)
                            //{
                            //    for (int i = 0; i < 3; i++)
                            //    {
                            //        try
                            //        {
                            //            IWebElement f = forces[i].FindElement(By.XPath("form[@method=\"post\"]"));
                            //            SearchForceIdx = i;                                        
                            //            break;
                            //        }
                            //        catch { }
                            //    }
                            //}

                            //探索フォース使用
                            for(int i = 0; i < SearchForcePlace.Count; i++)
                            {
                                if (SearchForcePlace[i])
                                {
                                    try
                                    {
                                        IWebElement f = forces[i].FindElement(By.XPath("div[@class=\"force-count\"]"));

                                        forces[i].FindElement(By.XPath("form[@method=\"post\"]")).Submit();
                                        Log?.Invoke(this, "探索フォース使用");
                                        Exec = SearchState;
                                        return;
                                    }
                                    catch { }
                                }
                            }

                            //探索フォースが無ければ通常の探索を行う
                            try
                            {
                                if (!NoSearch)
                                {
                                    IWebElement elm = driver_.FindElement(By.XPath("//a[contains(text(), \"敵を\")]"));
                                    Log?.Invoke(this, "探索開始");

                                    string url = elm.GetAttribute("href");

                                    if (url == null)
                                    {
                                        string token = elm.GetAttribute("data-mission-path");
                                        if (token != "" && token != null)
                                            url = "http://gcc.sp.mbga.jp/" + elm.GetAttribute("data-mission-path");
                                    }

                                    if (url != null)
                                    {
                                        CurrentState = State.SearchFlash;
                                        StateChanged?.Invoke(this, CurrentState);
                                        switch (SearchEnemy(url))
                                        {
                                            case SearchResult.Found:
                                                driver_.Navigate().Refresh();
                                                MoveEnemyListToSearch();
                                                Exec = SearchState;
                                                break;
                                            case SearchResult.Card:
                                                Exec = SearchState;
                                                break;
                                            case SearchResult.Continue:
                                                Exec = SearchState;
                                                break;
                                            case SearchResult.Error:
                                                StateChanged?.Invoke(this, State.AccessBlock);
                                                Log?.Invoke(this, "ページ移動：アクセス制限通知画面");
                                                Wait(WaitAccessBlock);
                                                Exec = SearchState;
                                                break;
                                            case SearchResult.FuelShortage:
                                                Log?.Invoke(this, "警告：燃料不足");
                                                Wait(10);
                                                Exec = SearchState;
                                                break;
                                            default:
                                                break;
                                        }
                                        driver_.Navigate().Refresh();
                                        return;
                                    }
                                    else
                                    {
                                        Log?.Invoke(this, "探索失敗：敵出現数が上限に達しています");
                                        Wait(1);
                                    }
                                }
                            }
                            catch { }
                        }
                        else
                        {
                            //攻撃フォースを使う
                            for (int i = 0; i < 3; i++)
                            {
                                try
                                {
                                    IWebElement f = forces[i].FindElement(By.XPath("div[@class=\"force-count\"]"));
                                    if (SearchForceIdx != i)
                                    {
                                        forces[i].FindElement(By.XPath("form[@method=\"post\"]")).Submit();
                                        Log?.Invoke(this, "攻撃フォース使用");
                                        Exec = SearchState;
                                        return;
                                    }
                                }
                                catch { }
                            }

                            //フォースチャージを使用する
                            if (ForceCharge)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    try
                                    {
                                        IWebElement f = forces[i].FindElement(By.XPath("div[@class=\"force-count\"]"));
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            //カウントが無ければフォースチャージを使用する
                                            forces[i].FindElement(By.XPath("form[@method=\"post\"]")).Submit();
                                            Log?.Invoke(this, "フォースチャージ使用");
                                            Exec = SearchState;
                                            return;
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }
                    }
                }


                //コンボ数が多い順に狙う
                int idx = -1;
                Exec = SearchState;

                //一度しか攻撃しない場合
                if (Mode == AttackMode.OneAttack)
                {
                    for(int i = 0; i < enemys.Count(); i++)
                    {
                        try
                        {
                            if (!IsAttacked(enemys[i].GetAttribute("href")))
                            {
                                driver_.Navigate().GoToUrl(enemys[i].GetAttribute("href"));
                                Exec = SearchState;
                                return;
                            }
                        }
                        catch { }
                    }
                }
                //無制限に攻撃する場合
                else if (Mode == AttackMode.Unlimited)
                {
                    try
                    {
                        driver_.Navigate().GoToUrl(enemys[0].GetAttribute("href"));
                        Exec = SearchState;
                        return;
                    }
                    catch { }
                }
                else
                {
                    //コンボチャンス
                    try
                    {
                        var elms = driver_.FindElements(By.XPath("//div[@class=\"target-button js-target-button combo-chance\"]/.."));
                        if (elms.Count > 0)
                        {
                            var combo = driver_.FindElements(By.XPath("//div[@class=\"target-button js-target-button before-attack\"]/../div[@class=\"combo\"]/span[@class=\"js-ms-combo\"]")).Where(e => e.Text.Length > 0).Select(e => Convert.ToInt32(e.Text)).ToList();
                            var buttons = elms.Zip(combo, (b, c) => new Tuple<IWebElement, int>(b, c)).OrderBy(i => Guid.NewGuid()).ToList();
                            idx = buttons.FindIndex(b => b.Item2 == buttons.Max(b2 => b2.Item2));
                            driver_.Navigate().GoToUrl(elms[idx].GetAttribute("href"));
                            Exec = SearchState;
                            IsCombo = true;
                            return;
                        }
                    }
                    catch { }

                    //協力要請
                    try
                    {
                        var elms = driver_.FindElements(By.XPath("//a[@class=\"btn-raidboss-attack request\"]"));
                        if (elms.Count > 0)
                        {
                            var combo = driver_.FindElements(By.XPath("//div[@class=\"target-button js-target-button before-attack\"]/../div[@class=\"combo\"]/span[@class=\"js-ms-combo\"]")).Where(e => e.Text.Length > 0).Select(e => Convert.ToInt32(e.Text)).ToList();
                            var buttons = elms.Zip(combo, (b, c) => new Tuple<IWebElement, int>(b, c)).OrderBy(i => Guid.NewGuid()).ToList();
                            idx = buttons.FindIndex(b => b.Item2 == buttons.Max(b2 => b2.Item2));
                            driver_.Navigate().GoToUrl(elms[idx].GetAttribute("href"));
                            Exec = SearchState;
                            IsCombo = true;
                            RemoveEnemyId(driver_.Url);
                            return;
                        }
                    }
                    catch { }

                    //未攻撃
                    try
                    {
                        var elms = driver_.FindElements(By.XPath("//div[@class=\"target-button js-target-button before-attack\"]/.."));
                        if (elms.Count > 0)
                        {
                            var combo = driver_.FindElements(By.XPath("//div[@class=\"target-button js-target-button before-attack\"]/../div[@class=\"combo\"]/span[@class=\"js-ms-combo\"]")).Where(e => e.Text.Length > 0).Select(e => Convert.ToInt32(e.Text)).ToList();
                            var buttons = elms.Zip(combo, (b, c) => new Tuple<IWebElement, int>(b, c)).OrderBy(i => Guid.NewGuid()).ToList();
                            idx = buttons.FindIndex(b => b.Item2 == buttons.Max(b2 => b2.Item2));
                            driver_.Navigate().GoToUrl(elms[idx].GetAttribute("href"));
                            Exec = SearchState;
                            RemoveEnemyId(driver_.Url);
                            return;
                        }
                    }
                    catch { }
                }
            }


            try
            {
                if (!NoSearch)
                {
                    if (enemys.Count() <= (int)EnemyCount)
                    {
                        IWebElement elm = driver_.FindElement(By.XPath("//a[contains(text(), \"敵を\")]"));
                        Log?.Invoke(this, "探索開始");

                        string url = elm.GetAttribute("href");

                        if (url == null)
                        {
                            string token = elm.GetAttribute("data-mission-path");
                            if(token != "" && token != null)
                                url = "http://gcc.sp.mbga.jp/" + elm.GetAttribute("data-mission-path");
                        }

                        if (url != null)
                        {
                            CurrentState = State.SearchFlash;
                            StateChanged?.Invoke(this, CurrentState);
                            switch (SearchEnemy(url))
                            {
                                case SearchResult.Found:
                                    driver_.Navigate().Refresh();
                                    MoveEnemyListToSearch();
                                    Exec = SearchState;
                                    break;
                                case SearchResult.Card:
                                    Exec = SearchState;
                                    break;
                                case SearchResult.Error:
                                    StateChanged?.Invoke(this, State.AccessBlock);
                                    Log?.Invoke(this, "ページ移動：アクセス制限通知画面");
                                    Wait(WaitAccessBlock);
                                    Exec = SearchState;
                                    break;
                                case SearchResult.FuelShortage:
                                    Log?.Invoke(this, "警告：燃料不足");
                                    Wait(10);
                                    Exec = SearchState;
                                    break;
                                default:
                                    break;
                            }
                            driver_.Navigate().Refresh();
                            return;
                        }
                        else
                        {
                            Log?.Invoke(this, "探索失敗：敵出現数が上限に達しています");
                            Wait(1);
                        }
                    }
                }
            }
            catch { }


            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//a[contains(text(), \"戦況を\")]"));
                Log?.Invoke(this, string.Format("敵出現数： {0}", enemys.Count()));
                Log?.Invoke(this, "攻撃対象無し");
                Log?.Invoke(this, "待機中");
                Wait(2);
                Log?.Invoke(this, "ページ更新");
                driver_.Navigate().Refresh();
                Exec = SearchState;
                return;
            }
            catch { }

            Exec = SearchState;
            try
            {
                driver_.Navigate().GoToUrl(home_path_);
            }
            catch { }
        }


        /// <summary>
        /// 戦闘画面
        /// </summary>
        private void Battle()
        {
            Exec = SearchState;
            Wait(WaitBattle);

            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//div[contains(text(), \"BE回復ミニカプセル\")]/span"));
                if (MinicapCount != Convert.ToInt32(elm.Text))
                {
                    MinicapChanged?.Invoke(this, Convert.ToInt32(elm.Text));
                    MinicapCount = Convert.ToInt32(elm.Text);
                }
            }
            catch { }

            try
            {
                //無制限に攻撃するでない
                if (Mode != AttackMode.Unlimited)
                {
                    if (IsAttacked(driver_.Url) && !IsCombo)
                    {
                        if (Mode != AttackMode.Unlimited)
                        {
                            if (enemy_list_path_ != "")
                            {
                                Attacked = false;
                                driver_.Navigate().GoToUrl(enemy_list_path_);
                                return;
                            }
                        }
                    }
                }
            }
            catch { }


            double boost = 1.0;
            try
            {
                boost = Convert.ToDouble(driver_.FindElement(By.XPath("//div[contains(@class,\"strategic-area-attack\")]")).Text);
            }
            catch
            {
                boost = 1.0;
            }

            try
            {
                string name = "";
                try
                {
                    //敵の名前を取得
                    name = driver_.FindElement(By.XPath("//span[@id=\"boss_name\"]")).Text;
                }
                catch { }

                //敵HPを取得
                IWebElement elm = driver_.FindElement(By.XPath("//div[@class=\"raid_boss_summary_para\" or @class=\"raid_boss_summary_para bac\"]"));
                string strhp = elm.Text;
                string current_hp = strhp.Replace(",", "").Split(new char[] { '/' })[0];
                UInt64 hp = UInt64.Parse(current_hp);


                var elms = driver_.FindElements(By.XPath("//*[@class=\"energy-btn\"]/a"));

                //コンボ数取得
                double combo = 1;

                try
                {
                    elm = driver_.FindElement(By.XPath("//div[@id=\"attc\"]/strong"));
                    combo = Convert.ToDouble(elm.Text);
                }
                catch { }


                //一撃で倒せる必要倍率を計算
                double requiredRatio = Utils.CalcRequiredRatio(hp, BaseDamage, boost, combo);
                int useBe = 0;
                if (requiredRatio == 0) requiredRatio = 1;


                if (requiredRatio > 3) useBe = 3;
                else if (requiredRatio > 1.2) useBe = 2;
                else useBe = 1;

                if (useBe > 0)
                {
                    useBe--;
                    Log?.Invoke(this, "攻撃： " + name);

                    string ret = "";

                    try
                    {
                        Wait(WaitAttack);
                        ret = GetWebClient().DownloadString(elms.ElementAt(useBe).GetAttribute("href"));
                    }
                    catch { }

                    if (ret.IndexOf("swf") < 0)
                    {
                        StateChanged?.Invoke(this, State.AccessBlock);
                        Log?.Invoke(this, "ページ移動：アクセス制限通知画面");
                        Wait(WaitAccessBlock);
                        driver_.Navigate().GoToUrl(home_path_);
                        return;
                    }

                    Log?.Invoke(this, string.Format("BEx{0}使用", useBe + 1));
                    AddEnemyId(driver_.Url);
                    driver_.Navigate().Refresh();
                    Attacked = true;
                    AttackCount++;
                }
            }
            catch { }

            IsCombo = false;
        }

        //戦略拠点での戦闘
        private void StrategicAreaBattle()
        {
            Exec = SearchState;
            Wait(WaitBattle);

            try
            {
                Gauge = Convert.ToInt32(driver_.FindElement(By.XPath("//div[@class=\"gauge-badge\"]")).GetAttribute("style").Substring(6).Replace("%", "").Replace(";", ""));

                string levelAtt = "";
                string side = "";
                int level = 0;
                try
                {
                    IWebElement elm = driver_.FindElement(By.XPath("//div[contains(@class,\"level-\")]"));
                    levelAtt = elm.GetAttribute("class");
                    side = levelAtt.Split(new char[] { ' ' }).Last();

                    level = Convert.ToInt32(levelAtt.Substring(levelAtt.IndexOf("level") + 6, 1));
                }
                catch
                {
                    level = 0;
                    side = "even";
                }




                if (Priority != AreaPriority.OnlyStrategyArea)
                {
                    if (side != "enemy")
                    {
                        //既定の制圧レベル以上なら移動
                        if (level == 3 && Gauge == 100)
                        {
                            Exec = SearchState;
                            return;
                        }
                    }
                }
            }
            catch { }



            if (CurrentArea != "戦略拠点")
            {
                CurrentArea = "戦略拠点";
                AreaChanged?.Invoke(this, CurrentArea);
            }

            enemy_list_path_ = driver_.Url;

            //報酬受け取り
            if (ReceiveReword)
            {
                try
                {
                    var rewords = driver_.FindElements(By.XPath("//*[text()=\"報酬を受け取る\"]"));
                    if (rewords.Count() >= 5)
                    {
                        IWebElement elm = driver_.FindElement(By.XPath("//a[contains(text(),\"報酬をまとめて受け取る\")]"));
                        driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                        return;
                    }
                }
                catch
                { }
            }



            //制圧レベル取得
            try
            {
                try
                {
                    //ミニカプカウント
                    IWebElement mini = driver_.FindElement(By.XPath("//div[contains(text(),\"BE回復ミニカプセル\")]/strong"));

                    if (MinicapCount != Convert.ToInt32(mini.Text))
                    {
                        MinicapChanged?.Invoke(this, Convert.ToInt32(mini.Text));
                        MinicapCount = Convert.ToInt32(mini.Text);
                    }
                }
                catch { }


                try
                {
                    //フォース取得
                    var forces = driver_.FindElements(By.XPath("//div[contains(@class,\"js-force-submit\")]"));
                    var forceCount = driver_.FindElements(By.XPath("//div[contains(@class,\"js-force-submit\")]//span")).Select(e => Convert.ToInt32(e.Text)).ToArray();

                    var forceCount2 = driver_.FindElements(By.XPath("//div[contains(@class,\"js-force-submit\")]")).ToArray();

                    var enemys = driver_.FindElements(By.XPath("//div[contains(@class,\"js-raid-boss-info\")]")).ToList();

                    //HPが残っている敵をフィルタ
                    if (enemys.Count > 0)
                    {
                        enemys = enemys.Where(e => (e.GetAttribute("data-after-hp") ?? e.GetAttribute("data-current-hp")) != "0" && e.GetAttribute("data-current-hp") != null).ToList();
                    }

                    //敵出現中
                    if (enemys.Count() > 0)
                    {
                        if (forceCount[0] > 0)
                        {
                            Wait(WaitForce);
                            switch (StrategyAreaForcePattern)
                            {
                                //攻撃回数最適化
                                case ForcePattern.Optimaze:
                                    List<ulong> damages = new List<ulong>();
                                    List<ulong> hp = new List<ulong>();

                                    foreach (IWebElement enemy in enemys)
                                    {
                                        var currentHp = Convert.ToUInt64(enemy.GetAttribute("data-after-hp") ?? enemy.GetAttribute("data-current-hp"));

                                        if (currentHp > 0)
                                        {
                                            hp.Add(currentHp);
                                            if (enemy.GetAttribute("data-before-hp") == null) continue;
                                            damages.Add(Convert.ToUInt64(enemy.GetAttribute("data-before-hp")) - currentHp);
                                        }
                                    }

                                    if (damages.Count > 0)
                                    {
                                        switch (PrevUsedForce)
                                        {
                                            case Force.Weak:
                                                WeakForceDamage = damages.Max();
                                                break;
                                            case Force.Strong:
                                                StrongForceDamage = damages.Max();
                                                break;
                                            case Force.None:
                                                break;
                                            default:
                                                break;
                                        }
                                    }

                                    if (hp.Count() == 0)
                                    {
                                        driver_.Navigate().Refresh();
                                        return;
                                    }

                                    ulong maxHp = hp.Max();

                                    if (maxHp <= WeakForceDamage && forceCount[0] > 0 || WeakForceDamage == 0)
                                    {
                                        Log?.Invoke(this, "弱フォース使用");
                                        forces[0].FindElement(By.XPath("form")).Submit();
                                        PrevUsedForce = Force.Weak;
                                    }
                                    else
                                    {
                                        Log?.Invoke(this, "強フォース使用");
                                        forces[1].FindElement(By.XPath("form")).Submit();
                                        PrevUsedForce = Force.Strong;
                                    }
                                    break;
                                //強弱弱弱...
                                case ForcePattern.StrongWeak:
                                    switch (PrevUsedForce)
                                    {
                                        case Force.None:
                                            if (forceCount[0] > 0)
                                            {
                                                Log?.Invoke(this, "強フォース使用");
                                                forces[1].FindElement(By.XPath("form")).Submit();
                                                PrevUsedForce = Force.Strong;
                                            }
                                            else
                                            {
                                                Log?.Invoke(this, "弱フォース使用");
                                                forces[0].FindElement(By.XPath("form")).Submit();
                                                PrevUsedForce = Force.Weak;
                                            }
                                            break;
                                        case Force.Weak:
                                            Log?.Invoke(this, "弱フォース使用");
                                            forces[0].FindElement(By.XPath("form")).Submit();
                                            PrevUsedForce = Force.Weak;
                                            break;
                                        case Force.Strong:
                                            Log?.Invoke(this, "弱フォース使用");
                                            forces[0].FindElement(By.XPath("form")).Submit();
                                            PrevUsedForce = Force.Weak;
                                            break;
                                        default:
                                            Log?.Invoke(this, "弱フォース使用");
                                            forces[0].FindElement(By.XPath("form")).Submit();
                                            PrevUsedForce = Force.Weak;
                                            break;
                                    }
                                    break;
                                //弱のみ
                                case ForcePattern.WeakWeak:
                                    Log?.Invoke(this, "弱フォース使用");
                                    forces[0].FindElement(By.XPath("form")).Submit();
                                    PrevUsedForce = Force.Weak;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            Log?.Invoke(this, "弱フォース使用");
                            forces[0].FindElement(By.XPath("form")).Submit();
                            PrevUsedForce = Force.Weak;
                        }
                    }
                    else
                    {
                        if (StrategyAreaForcePattern == ForcePattern.StrongWeak) PrevUsedForce = Force.None;

                        //探索
                        if (forceCount[1] > 0 && StrategyAreaForcePattern != ForcePattern.WeakWeak)
                        {
                            Log?.Invoke(this, "探索フォース使用");
                            forces[2].FindElement(By.XPath("//form")).Submit();
                            return;
                        }
                        else
                        {
                            IWebElement searchButton = driver_.FindElement(By.XPath("//a[contains(@id,\"search_button\")]"));
                            Log?.Invoke(this, "探索開始");
                            driver_.Navigate().GoToUrl(searchButton.GetAttribute("href"));
                            return;
                        }
                    }
                }
                catch
                {
                    driver_.Navigate().GoToUrl(home_path_);
                }
            }
            catch { }
        }

        /// <summary>
        /// StateChanged伝搬用
        /// </summary>
        /// <param name="sender">送信元クラス</param>
        /// <param name="state">状態ID</param>
        private void StateChangedBase(object sender, Event.State state) => this.StateChanged?.Invoke(this, CurrentState);

        /// <summary>
        /// MiniCapChanged伝搬用
        /// </summary>
        /// <param name="sender">送信元クラス</param>
        /// <param name="count">ミニカプ数</param>
        private void MiniCapChangedBase(object sender, int count) => this.MinicapChanged?.Invoke(this, count);

        /// <summary>
        /// OnLog伝搬用
        /// </summary>
        /// <param name="sender">送信元クラス</param>
        /// <param name="text">テキスト</param>
        private void OnLogBase(object sender, string text) => this?.Log(this, text);
    }
}
