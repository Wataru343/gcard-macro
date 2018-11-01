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

        public Dictionary<string, int> Shield { get; set; }
        public AreaPriority Priority { get; set; }
        public bool UseForce { get; set; }
        public bool ForceCharge { get; set; }
        public ForcePattern StrategyAreaForcePattern { get; set; }
        public long PointDiff { get; set; }
        public bool Standby { get; set; }
        public double WaitForce { get; set; }

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
            Shield = new Dictionary<string, int>();
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

            base.StateChanged += StateChangedBase;
            base.MinicapChanged += MiniCapChangedBase;
            base.Log += OnLogBase;
        }

        override protected void SearchState()
        {
            try
            {
                //稼働時間外
                if (IsOutOfTimeRange())
                {
                    if (CurrentState != State.None)
                    {
                        Log?.Invoke(this, "稼働時間外");
                        driver_.Navigate().GoToUrl(home_path_);
                    }
                    CurrentState = State.None;
                    Wait(1);
                }
                else
                //イベントホーム
                if (IsHome())
                {
                    if (!IsHomeDuringInterval())
                    {
                        Log?.Invoke(this, "ページ移動：イベントホーム画面");
                        CurrentState = State.Home;
                        Wait(WaitSearch);
                        Exec = MoveEventHomeToSearch;
                    }
                    else
                    {
                        if (CurrentState != State.Interval)
                            Log?.Invoke(this, "ページ移動：インターバル画面");

                        CurrentState = State.Interval;
                        Wait(10);
                    }
                }
                //探索フラッシュ
                else if (IsSearchFlash())
                {
                    if (CurrentState != State.SearchFlash)
                        Log?.Invoke(this, "ページ移動：Flash画面");
                    CurrentState = State.SearchFlash;
                    Wait(WaitBattle);
                    Exec = EmulateClickFlash;
                }
                //戦闘フラッシュ
                else if (IsFightFlash())
                {
                    if (CurrentState != State.BattleFlash)
                        Log?.Invoke(this, "ページ移動：戦闘演出画面");
                    CurrentState = State.BattleFlash;
                    Wait(WaitAttack);
                    Exec = ClickBattleFlash;
                }
                //敵一覧
                else if (IsEnemyList())
                {
                    Log?.Invoke(this, "ページ移動：敵一覧画面");
                    CurrentState = State.EnemyList;
                    Wait(WaitSearch);
                    Exec = MoveEnemyListToSearch;
                }
                //戦闘画面
                else if (IsBattle())
                {
                    Log?.Invoke(this, "ページ移動：戦闘画面");
                    CurrentState = State.Battle;
                    Wait(WaitAttack);
                    Exec = Battle;
                }
                //戦闘リザルト
                else if (IsResult())
                {
                    Log?.Invoke(this, "ページ移動：戦闘リザルト画面");
                    CurrentState = State.Result;
                    Wait(WaitReceive);
                    Exec = MoveResultToEnemyList;
                }
                //報酬受け取り
                else if (IsReceive())
                {
                    Log?.Invoke(this, "ページ移動：報酬受け取り画面");
                    CurrentState = State.Receive;
                    Wait(WaitReceive);
                    Exec = MoveReceiveToPresentList;
                }
                //プレゼント一覧
                else if (IsPresentList())
                {
                    Log?.Invoke(this, "ページ移動：プレゼント一覧画面");
                    CurrentState = State.PresentList;
                    Wait(WaitReceive);
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
                    //IsRun = false;
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
                    if (CurrentState != State.GTacticsStrategicArea)
                        Log?.Invoke(this, "ページ移動：戦略拠点");
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
                else
                {
                    Log?.Invoke(this, "ページ移動：不明な画面");
                    CurrentState = State.Unknown;
                    Wait(WaitMisc);
                    driver_.Navigate().GoToUrl(home_path_);
                }
            }
            catch { }

            StateChanged?.Invoke(this, CurrentState);
        }

        /// <summary>
        /// 敵一覧画面判定
        /// </summary>
        /// <returns></returns>
        override protected bool IsEnemyList() => driver_.PageSource.IndexOf("戦況を") >= 0 && driver_.PageSource.IndexOf("更新する") >= 0;

        /// <summary>
        /// 戦略拠点画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsStrategicArea() => driver_.Url.IndexOf("strategic_area") >= 0 && driver_.PageSource.IndexOf("js-force-submit") >= 0;

        /// <summary>
        /// フォース実行失敗画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsForceFaild() => driver_.PageSource.IndexOf("フォースの実行に失敗しました") >= 0;

        /// <summary>
        /// イベントのホームから探索へ
        /// </summary>
        private void MoveEventHomeToSearch()
        {
            TargetAllClear = false;
            ShieldClear = false;
            AttackCount = 0;

            try
            {
                if (enemy_list_path_ != "")
                {
                    driver_.Navigate().GoToUrl(enemy_list_path_);
                    return;
                }

                IWebElement elm = driver_.FindElement(By.XPath("//li[@class=\"search\" or @class=\"attack\"]/a"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
            }
            catch
            {
                //戦略拠点
                try
                {
                    try
                    {
                        OurPoint = Convert.ToInt64(driver_.FindElement(By.XPath("//p[@class=\"own-group-point\"]")).Text.Replace(",", "").Replace("pt", ""));
                        EnemyPoint = Convert.ToInt64(driver_.FindElement(By.XPath("//p[@class=\"enemy-group-point\"]")).Text.Replace(",", "").Replace("pt", ""));
                    }
                    catch { }

                    var panel = driver_.FindElements(By.XPath("//div[contains(@class,\"area-wrap\")]")).Take(10).Reverse().ToArray();
                    var button = driver_.FindElements(By.XPath("//div[contains(@class,\"area-btn area-btn-\")]")).Take(10).Reverse().ToArray();
                    var link = driver_.FindElements(By.XPath("//div[contains(@class, \"js-battlemap-btn select_link\")]")).Take(10).Reverse().ToArray();
                    List<int> levels = new List<int>();
                    List<string> areaNames = new List<string>();
                    for (int i = 0; i < panel.Count(); i++)
                    {
                        try
                        {
                            //シェルターレベル取得
                            string aa = panel[i].FindElement(By.XPath("img[contains(@src,\"shelter\")]")).GetAttribute("src");
                            var shelterStr = aa.Split(new char[] { '/' }).Last().Split(new char[] { '_' });
                            string eo = shelterStr[1];
                            string level = shelterStr[2].Substring(0, 1);
                            levels.Add(Convert.ToInt32(level));
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



                    switch (Priority)
                    {
                        case AreaPriority.None:
                            {
                                foreach (var p in panel.Skip(1).Zip(link.Skip(1), (p, l) => new { panel = p, link = l }))
                                {
                                    string className = p.panel.GetAttribute("class");
                                    if ((className.IndexOf("own") < 0 && className.IndexOf("enemy") < 0) || className.IndexOf("enemy") >= 0)
                                    {
                                        try
                                        {
                                            string areaLink = p.link.GetAttribute("data-js-modal-link").Replace("&amp;", "&");
                                            driver_.Navigate().GoToUrl("http://gcc.sp.mbga.jp/" + areaLink);
                                            Exec = SearchState;
                                            return;
                                        }
                                        catch { }
                                    }
                                }

                                var panel2 = panel.Skip(1).ToList().Concat(panel.Take(1).ToList()).ToArray();
                                var areaNames2 = areaNames.Skip(1).ToList().Concat(areaNames.Take(1).ToList()).ToArray();
                                var levels2 = levels.Skip(1).ToList().Concat(levels.Take(1).ToList()).ToArray();
                                var link2 = link.Skip(1).ToList().Concat(link.Take(1).ToList()).ToArray();

                                for (int i = 0; i < panel2.Count(); i++)
                                {
                                    if (Shield[areaNames2[i]] > levels2[i])
                                    {
                                        try
                                        {
                                            string areaLink = link2[i].GetAttribute("data-js-modal-link").Replace("&amp;", "&");
                                            if (areaLink != "")
                                            {
                                                driver_.Navigate().GoToUrl("http://gcc.sp.mbga.jp/" + areaLink);
                                                Exec = SearchState;
                                                return;
                                            }
                                        }
                                        catch { }
                                    }
                                }
                            }
                            break;
                        case AreaPriority.StrategyArea:
                            {
                                for (int i = 0; i < panel.Count(); i++)
                                {
                                    string className = panel[i].GetAttribute("class");
                                    if (Shield[areaNames[i]] > levels[i] || className.IndexOf("enemy") >= 0)
                                    {
                                        try
                                        {
                                            string areaLink = link[i].GetAttribute("data-js-modal-link").Replace("&amp;", "&");
                                            if (areaLink != "")
                                            {
                                                driver_.Navigate().GoToUrl("http://gcc.sp.mbga.jp/" + areaLink);
                                                Exec = SearchState;
                                                return;
                                            }
                                        }
                                        catch { }
                                    }
                                }
                            }
                            break;
                        case AreaPriority.OnlyStrategyArea:
                            {
                                try
                                {
                                    string areaLink = link[0].GetAttribute("data-js-modal-link").Replace("&amp;", "&");
                                    driver_.Navigate().GoToUrl("http://gcc.sp.mbga.jp/" + areaLink);
                                    Exec = SearchState;
                                    return;
                                }
                                catch { }
                                break;
                            }
                        default:
                            break;
                    }

                    //シェルターレベルクリア
                    ShieldClear = true;

                    if (PointDiff > (OurPoint - EnemyPoint))
                    {
                        try
                        {
                            string areaLink = link[1].GetAttribute("data-js-modal-link").Replace("&amp;", "&");
                            driver_.Navigate().GoToUrl("http://gcc.sp.mbga.jp/" + areaLink);
                            Exec = SearchState;
                            return;
                        }
                        catch { }
                    }

                    //シェルターレベル、点数差全てクリア
                    TargetAllClear = true;

                    if (!Standby)
                    {
                        try
                        {
                            string areaLink = link[1].GetAttribute("data-js-modal-link").Replace("&amp;", "&");
                            driver_.Navigate().GoToUrl("http://gcc.sp.mbga.jp/" + areaLink);
                            Exec = SearchState;
                            return;
                        }
                        catch { }
                    }

                    Wait(5);
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

            Exec = SearchState;
        }


        /// <summary>
        /// 敵一覧から戦闘へ
        /// </summary>
        private void MoveEnemyListToSearch()
        {
            if(enemy_list_path_ == "")
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


            //拠点優先
            if (Priority == AreaPriority.StrategyArea)
            {
                try
                {
                    var areaLevel = 0;
                    var ea = driver_.FindElements(By.XPath("//div[contains(@class,\"strategy-bounus-wrap enemy-area\")]"));
                    if (ea.Count() == 0)
                    {
                        if (driver_.PageSource.IndexOf("strategy-bounus-wrap own-area") >= 0)
                        {
                            IWebElement elm = driver_.FindElement(By.XPath("//div[contains(@class,\"strategic-area-lv\")]"));
                            areaLevel = Convert.ToInt32(elm.GetAttribute("class").Substring(elm.GetAttribute("class").IndexOf("lv") + 2, 1));
                        }
                        else
                        {
                            areaLevel = 0;
                        }
                    }
                    else
                    {
                        areaLevel = -1;
                    }

                    if (areaLevel < Shield["戦略拠点"])
                    {
                        try
                        {
                            var link = driver_.FindElements(By.XPath("//a[contains(@href, \"field_move\")]")).Take(10).Reverse().ToArray();
                            string areaLink = link[0].GetAttribute("href");
                            driver_.Navigate().GoToUrl(areaLink);
                            Exec = SearchState;
                            return;
                        }
                        catch { }
                    }
                }
                catch { }
            }
            else if (Priority == AreaPriority.OnlyStrategyArea)
            {
                ////ポップアップを出しておく
                //IWebElement popupButton = driver_.FindElement(By.XPath("//div[@class=\"strategy-move-button\"]");
                //Actions action = new Actions(driver_);
                //action.MoveToElement(popupButton).Click().Build().Perform();

                ////エリア移動ボタン取得
                //var moveButton = driver_.FindElements(By.XPath("//div[contains(@class, \"area pos-\")]/a").Reverse().ToArray();

                //driver_.Navigate().GoToUrl(moveButton.First().GetAttribute("href"));
                //Exec = SearchState;
                //return;
            }

            

            ulong shield = 0;

            try
            {
                //エリア名取得
                string area = "";
                if (driver_.Url.IndexOf("strategic_area") > 0)
                {
                    area = "戦略拠点";
                }
                else
                {
                    area = driver_.FindElement(By.XPath("//div[@class=\"members-wrap\"]/span")).Text.Split(new char[] { '<' })[0];
                }

                if (CurrentArea != area)
                {
                    CurrentArea = area;

                    if (CurrentArea.Length > 0)
                    {
                        Log?.Invoke(this, "エリア移動： " + CurrentArea);
                        AreaChanged?.Invoke(this, CurrentArea);
                    }
                }


                if (driver_.PageSource.IndexOf("相手部隊シェルター展開中") <= 0 && driver_.PageSource.IndexOf("制圧まで あと") <= 0)
                {
                    //シェルターポイント取得              
                    IWebElement elm = driver_.FindElement(By.XPath("//div[@class=\"shleter-point\"]/span"));
                    ulong shelterPt = Convert.ToUInt64(elm.Text.Replace(",", ""));
                    shield = shelterPt / 10000000;

                    //既定のシールドレベル以上なら移動
                    if (Shield[area] <= (int)shield)
                    {
                        if (ShieldClear)
                        {
                            if (AttackCount > 10)
                            {
                                driver_.Navigate().GoToUrl(home_path_);
                                Exec = SearchState;
                                return;
                            }
                        }
                        else
                        {
                            driver_.Navigate().GoToUrl(home_path_);
                            Exec = SearchState;
                            return;
                        }
                    }
                }
                IsAreaBattle = true;
            }
            catch
            {
                try
                {
                    //未制圧時の判定
                    IWebElement elm = driver_.FindElement(By.XPath("//div[@class=\"occupied-point\"]"));
                    IsAreaBattle = true;
                    shield = 0;
                }
                catch
                {
                    IsAreaBattle = false;
                    AreaChanged?.Invoke(this, "");
                }
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


                    if (forceCount > 0 || forceChargeCount > 0)
                    {
                        //フォース取得
                        var forces = driver_.FindElements(By.XPath("//div[contains(@class,\"js-force-submit\")]"));


                        //敵が6体未満なら探索フォースを使う
                        if (enemys.Count < 6)
                        {
                            //探索フォース未使用なら探す
                            if (SearchForceIdx < 0 && enemys.Count == 0)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    try
                                    {
                                        IWebElement f = forces[i].FindElement(By.XPath("form[@method=\"post\"]"));
                                        SearchForceIdx = i;                                        
                                        break;
                                    }
                                    catch { }
                                }
                            }

                            //探索フォース使用
                            if (SearchForceIdx >= 0)
                            {
                                try
                                {
                                    IWebElement f = forces[SearchForceIdx].FindElement(By.XPath("div[@class=\"force-count\"]"));

                                    forces[SearchForceIdx].FindElement(By.XPath("form[@method=\"post\"]")).Submit();
                                    Log?.Invoke(this, "探索フォース使用");
                                    Exec = SearchState;
                                    return;
                                }
                                catch
                                {
                                    SearchForceIdx = -1;
                                }
                            }

                            //探索フォースが無ければ通常の探索を行う
                            try
                            {
                                IWebElement elm = driver_.FindElement(By.XPath("//a[contains(text(), \"敵を\")]"));
                                Log?.Invoke(this, "探索開始");

                                string url = elm.GetAttribute("href");

                                if (url != null)
                                {
                                    switch (SearchEnemy(url))
                                    {
                                        case SearchResult.Found:
                                            MoveEnemyListToSearch();
                                            Exec = SearchState;
                                            return;
                                        case SearchResult.Card:
                                            Exec = SearchState;
                                            return;
                                        case SearchResult.Error:
                                            StateChanged?.Invoke(this, State.AccessBlock);
                                            Log?.Invoke(this, "ページ移動：アクセス制限通知画面");
                                            Wait(WaitAccessBlock);
                                            Exec = SearchState;
                                            return;
                                        case SearchResult.FuelShortage:
                                            Log?.Invoke(this, "警告：燃料不足");
                                            Wait(10);
                                            Exec = SearchState;
                                            return;
                                        default:
                                            break;
                                    }
                                }
                                else
                                {
                                    Log?.Invoke(this, "敵出現数最大");
                                    Exec = SearchState;
                                    return;
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
                if (enemys.Count() <= (int)EnemyCount)
                {
                    IWebElement elm = driver_.FindElement(By.XPath("//a[contains(text(), \"敵を\")]"));
                    Log?.Invoke(this, "探索開始");

                    string url = elm.GetAttribute("href");

                    if (url != null)
                    {
                        switch (SearchEnemy(url))
                        {
                            case SearchResult.Found:
                                MoveEnemyListToSearch();
                                Exec = SearchState;
                                return;
                            case SearchResult.Card:
                                Exec = SearchState;
                                return;
                            case SearchResult.Error:
                                StateChanged?.Invoke(this, State.AccessBlock);
                                Log?.Invoke(this, "ページ移動：アクセス制限通知画面");
                                Wait(WaitAccessBlock);
                                Exec = SearchState;
                                return;
                            case SearchResult.FuelShortage:
                                Log?.Invoke(this, "警告：燃料不足");
                                Wait(10);
                                Exec = SearchState;
                                return;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        Log?.Invoke(this, "敵出現数最大");
                        Exec = SearchState;
                        return;
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

            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//div[contains(text(), \"BE回復ミニカプセル\")]/span"));
                if (MinicapCount != Convert.ToInt32(elm.Text)){
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
                int requiredRatio = Utils.CalcUseMiniCapsules(hp, BaseDamage, 1.0, combo);
                int useBe = 0;
                if (requiredRatio == 0) requiredRatio = 1;


                if (requiredRatio > 3) useBe = 3;
                else if (requiredRatio > 1.2) useBe = 2;
                else useBe = 1;

                if (useBe > 0)
                {
                    Log?.Invoke(this, "攻撃： " + name);
                    Log?.Invoke(this, string.Format("BEx{0}使用", useBe));
                    useBe--;

                    string ret = "";

                    try
                    {
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

            if (CurrentArea != "戦略拠点")
            {
                CurrentArea = "戦略拠点";
                Log?.Invoke(this, "エリア移動： " + CurrentArea);
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
                        if (Shield[CurrentArea] <= level)
                        {
                            driver_.Navigate().GoToUrl(home_path_);
                            return;
                        }
                    }
                }


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
                                            Log?.Invoke(this, "強フォース使用");
                                            forces[1].FindElement(By.XPath("form")).Submit();
                                            PrevUsedForce = Force.Strong;
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
