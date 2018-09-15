using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Interactions;

namespace gcard_macro
{
    class GTactics : Event
    {
        public delegate void StateChangedHandler(object sender, State state);
        public event StateChangedHandler StateChanged;
        public delegate void MinicapChangedHandler(object sender, int count);
        public event MinicapChangedHandler MinicapChanged;
        public delegate void AreaChangedHandler(object sender, string area);
        public event AreaChangedHandler AreaChanged;
        public delegate void LogHandler(object sender, string text);
        public event LogHandler Log;

        public Dictionary<string, int> Shield { get; set; }
        public AreaPriority Priority { get; set; }
        public bool UseForce { get; set; }
        public bool ForceCharge { get; set; }
        public ForcePattern StrategyAreaForcePattern { get; set; }
        public long PointDiff { get; set; }
        public bool Standby { get; set; }

        private bool IsAreaBattle { get; set; }
        private string CurrentArea { get; set; }
        private ulong StrongForceDamage { get; set; }
        private ulong WeakForceDamage { get; set; }
        private Force PrevUsedForce { get; set; }
        private int SearchForceIdx { get; set; }
        private long OurPoint { get; set; }
        private long EnemyPoint { get; set; }
        private int AreaMovingCount { get; set; }

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


        public GTactics(RemoteWebDriver driver, string home_path) : base(driver, home_path)
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
            AreaMovingCount = 0;
        }

        override protected void SearchState()
        {
            try
            {
                //イベントホーム
                if (IsHome())
                {
                    if (!IsHomeDuringInterval())
                    {
                        CurrentState = State.Home;
                        Wait(WaitSearch);
                        Exec = MoveEventHomeToSearch;
                    }
                    else
                    {
                        CurrentState = State.Interval;
                        Wait(10);
                    }
                }
                //探索フラッシュ
                else if (IsSearchFlash())
                {
                    CurrentState = State.SearchFlash;
                    Wait(WaitBattle);
                    Exec = ClickSearchFlash;
                }
                //戦闘フラッシュ
                else if (IsFightFlash())
                {
                    CurrentState = State.BattleFlash;
                    Wait(WaitAttack);
                    Exec = ClickBattleFlash;
                }
                //敵一覧
                else if (IsEnemyList())
                {
                    CurrentState = State.EnemyList;
                    Wait(WaitSearch);
                    Exec = MoveEnemyListToSearch;
                }
                //戦闘画面
                else if (IsBattle())
                {
                    CurrentState = State.Battle;
                    Wait(WaitAttack);
                    Exec = Battle;
                }
                //戦闘リザルト
                else if (IsResult())
                {
                    CurrentState = State.Result;
                    Wait(WaitReceive);
                    Exec = MoveResultToEnemyList;
                }
                //報酬受け取り
                else if (IsReceive())
                {
                    CurrentState = State.Receive;
                    Wait(WaitReceive);
                    Exec = MoveReceiveToPresentList;
                }
                //プレゼント一覧
                else if (IsPresentList())
                {
                    CurrentState = State.PresentList;
                    Wait(WaitReceive);
                    Exec = MovePresentListToPresent;
                }
                //レベルアップ
                else if (IsLevelUp())
                {
                    CurrentState = State.LevelUp;
                    Wait(WaitMisc);
                    Exec = MoveLevelUpToSearch;
                }
                //カード入手
                else if (IsGetCard())
                {
                    CurrentState = State.GetCard;
                    Wait(WaitMisc);
                    Exec = MoveGetCardToSearch;
                }
                //既に戦闘は終了しています
                else if (IsFightAlreadyFinished())
                {
                    CurrentState = State.FightAlreadyFinished;
                    Wait(WaitMisc);
                    driver_.Navigate().GoToUrl(home_path_);
                    Attacked = false;
                }
                //不正な画面遷移です
                else if (IsError())
                {
                    CurrentState = State.Error;
                    Wait(WaitMisc);
                    driver_.Navigate().GoToUrl(home_path_);
                }
                //アクセスを制限
                else if (IsAccessBlock())
                {
                    CurrentState = State.AccessBlock;
                    Wait(WaitAccessBlock);
                    driver_.Navigate().GoToUrl(home_path_);
                }
                //イベント終了
                else if (IsEventFinished())
                {
                    CurrentState = State.EventFinished;
                    //IsRun = false;
                }
                //戦略拠点
                else if (IsStrategicArea())
                {
                    CurrentState = State.GTacticsStrategicArea;
                    Wait(WaitMisc);
                    Exec = StrategicAreaBattle;
                }
                else
                {
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
        private bool IsStrategicArea() => driver_.Url.IndexOf("strategic_area") >= 0;

        /// <summary>
        /// イベントのホームから探索へ
        /// </summary>
        private void MoveEventHomeToSearch()
        {
            try
            {
                IWebElement elm = driver_.FindElementByXPath("//li[@class=\"search\" or @class=\"attack\"]/a");
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
            }
            catch
            {
                try
                {
                    try
                    {                        
                        OurPoint = Convert.ToInt64(driver_.FindElementByXPath("//p[@class=\"own-group-point\"]").Text.Replace(",", "").Replace("pt", ""));
                        EnemyPoint = Convert.ToInt64(driver_.FindElementByXPath("//p[@class=\"enemy-group-point\"]").Text.Replace(",", "").Replace("pt", ""));
                    }
                    catch { }

                    var panel = driver_.FindElementsByXPath("//div[contains(@class,\"area-wrap\")]").Take(10).Reverse().ToArray();
                    var button = driver_.FindElementsByXPath("//div[contains(@class,\"area-btn area-btn-\")]").Take(10).Reverse().ToArray();
                    List<int> levels = new List<int>();
                    List<string> areaNames = new List<string>();
                    for (int i = 0; i < panel.Count(); i++)
                    {
                        try
                        {
                            string aa = panel[i].FindElement(By.XPath("img[contains(@src,\"shelter\")]")).GetAttribute("src");
                            var shelterStr = aa.Split(new char[] { '/' }).Last().Split(new char[] { '_' });
                            string eo = shelterStr[1];
                            string level = shelterStr[2].Substring(0, 1);
                            levels.Add(Convert.ToInt32(level));
                            Shield[button[i].Text] = Convert.ToInt32(level);
                        }
                        catch
                        {
                            if (i == 0)
                            {
                                try
                                {
                                    var strategicAreaLevel = panel[0].FindElement(By.XPath("div")).GetAttribute("class").Split(new char[] { ' ' });
                                    
                                    if(strategicAreaLevel[0] == "our")
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
                                foreach (var p in panel.Skip(1))
                                {
                                    string className = p.GetAttribute("class");
                                    if (className.IndexOf("own") < 0 && className.IndexOf("enemy") < 0)
                                    {
                                        IWebElement pd = p.FindElement(By.XPath("div"));
                                        Actions action = new Actions(driver_);
                                        action.MoveToElement(pd).Click().Build().Perform();


                                        pd = driver_.FindElementByXPath("//div[text()=\"移動する\"]");
                                        action = new Actions(driver_);
                                        action.MoveToElement(pd).Click().Build().Perform();
                                        Exec = SearchState;
                                        return;
                                    }
                                }

                                var panel2 = panel.Skip(1).ToList().Concat(panel.Take(1).ToList()).ToArray();
                                for (int i = 0; i < panel2.Count(); i++)
                                {
                                    if (Shield[areaNames[i]] > levels[i])
                                    {
                                        IWebElement pd = panel2[i].FindElement(By.XPath("div"));
                                        Actions action = new Actions(driver_);
                                        action.MoveToElement(pd).Click().Build().Perform();


                                        pd = driver_.FindElementByXPath("//div[text()=\"移動する\"]");
                                        action = new Actions(driver_);
                                        action.MoveToElement(pd).Click().Build().Perform();
                                        Exec = SearchState;
                                        return;
                                    }
                                }
                            }
                            break;
                        case AreaPriority.StrategyArea:
                            {
                                for (int i = 0; i < panel.Count(); i++)
                                {
                                    if (Shield[areaNames[i]] > levels[i])
                                    {
                                        IWebElement pd = panel[9].FindElement(By.XPath("div"));
                                        Actions action = new Actions(driver_);
                                        action.MoveToElement(pd).Click().Build().Perform();


                                        pd = driver_.FindElementByXPath("//div[text()=\"移動する\"]");
                                        action = new Actions(driver_);
                                        action.MoveToElement(pd).Click().Build().Perform();
                                        Exec = SearchState;
                                        return;
                                    }
                                }
                            }
                            break;
                        case AreaPriority.OnlyStrategyArea:
                            {
                                IWebElement pd = panel[0].FindElement(By.XPath("div"));
                                Actions action = new Actions(driver_);
                                action.MoveToElement(pd).Click().Build().Perform();


                                pd = driver_.FindElementByXPath("//div[text()=\"移動する\"]");
                                action = new Actions(driver_);
                                action.MoveToElement(pd).Click().Build().Perform();
                                Exec = SearchState;
                                return;
                            }
                        default:
                            break;
                    }



                    if(PointDiff > (OurPoint - EnemyPoint))
                    {
                        IWebElement pd = panel[1].FindElement(By.XPath("div"));
                        Actions action = new Actions(driver_);
                        action.MoveToElement(pd).Click().Build().Perform();


                        pd = driver_.FindElementByXPath("//div[text()=\"移動する\"]");
                        action = new Actions(driver_);
                        action.MoveToElement(pd).Click().Build().Perform();
                        Exec = SearchState;
                        return;
                    }

                    if (!Standby)
                    {
                        AreaMovingCount = 5;
                        IWebElement pd = panel[1].FindElement(By.XPath("div"));
                        Actions action = new Actions(driver_);
                        action.MoveToElement(pd).Click().Build().Perform();


                        pd = driver_.FindElementByXPath("//div[text()=\"移動する\"]");
                        action = new Actions(driver_);
                        action.MoveToElement(pd).Click().Build().Perform();
                        Exec = SearchState;
                        return;
                    }

                    Wait(5);
                    driver_.Navigate().Refresh();
                }
                catch
                {
                    try
                    {
                        IWebElement elm = driver_.FindElementByXPath("//div[contains(@class,\"position-9 area-wrap\")]/div");
                        Actions action = new Actions(driver_);
                        action.MoveToElement(elm).Click().Build().Perform();


                        elm = driver_.FindElementByXPath("//div[text()=\"移動する\"]");
                        action = new Actions(driver_);
                        action.MoveToElement(elm).Click().Build().Perform();
                    }
                    catch
                    {
                    }
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
                    var rewords = driver_.FindElementsByXPath("//*[text()=\"報酬を受け取る\"]");
                    if (rewords.Count() >= ReceiveCount)
                    {
                        IWebElement elm = driver_.FindElementByXPath("//a[text()=\"報酬をまとめて受け取る\"]");
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
                    var ea = driver_.FindElementsByXPath("//div[contains(@class,\"strategy-bounus-wrap enemy-area\")]");
                    if (ea.Count() == 0)
                    {
                        if (driver_.PageSource.IndexOf("strategy-bounus-wrap own-area") >= 0)
                        {
                            IWebElement elm = driver_.FindElementByXPath("//div[contains(@class,\"strategic-area-lv\")]");
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
                        //ポップアップを出しておく
                        IWebElement popupButton = driver_.FindElementByXPath("//div[@class=\"strategy-move-button\"]");
                        Actions action = new Actions(driver_);
                        action.MoveToElement(popupButton).Click().Build().Perform();

                        //エリア移動ボタン取得
                        var moveButton = driver_.FindElementsByXPath("//div[contains(@class, \"area pos-\")]/a").Reverse().ToArray();

                        driver_.Navigate().GoToUrl(moveButton.First().GetAttribute("href"));
                        Exec = SearchState;
                        return;
                    }
                }
                catch { }
            }
            else if (Priority == AreaPriority.OnlyStrategyArea)
            {
                //ポップアップを出しておく
                IWebElement popupButton = driver_.FindElementByXPath("//div[@class=\"strategy-move-button\"]");
                Actions action = new Actions(driver_);
                action.MoveToElement(popupButton).Click().Build().Perform();

                //エリア移動ボタン取得
                var moveButton = driver_.FindElementsByXPath("//div[contains(@class, \"area pos-\")]/a").Reverse().ToArray();

                driver_.Navigate().GoToUrl(moveButton.First().GetAttribute("href"));
                Exec = SearchState;
                return;
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
                    area = driver_.FindElementByXPath("//div[@class=\"members-wrap\"]/span").Text.Split(new char[] { '<' })[0];
                }
                CurrentArea = area;

                if (CurrentArea.Length > 0)
                {
                    AreaChanged?.Invoke(this, CurrentArea);
                }


                if (driver_.PageSource.IndexOf("相手部隊シェルター展開中") <= 0 && driver_.PageSource.IndexOf("制圧まで あと") <= 0)
                {
                    //シェルターポイント取得              
                    IWebElement elm = driver_.FindElementByXPath("//div[@class=\"shleter-point\"]/span");
                    ulong shelterPt = Convert.ToUInt64(elm.Text.Replace(",", ""));
                    shield = shelterPt / 10000000;

                    //既定のシールドレベル以上なら移動
                    if (Shield[area] <= (int)shield && AreaMovingCount < 1)
                    {
                        driver_.Navigate().GoToUrl(home_path_);
                        Exec = SearchState;
                        return;
                    }
                }
                IsAreaBattle = true;
            }
            catch
            {
                try
                {
                    //未制圧時の判定
                    IWebElement elm = driver_.FindElementByXPath("//div[@class=\"occupied-point\"]");
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
                enemys = driver_.FindElementsByXPath("//a[contains(@class,\"js-show-link\")]").Where(e => e.GetAttribute("href").Length > 0).ToList();
                if (enemys.Count == 0) AttackedEnemyId.Clear();
            }
            catch { }


            //探索のみ
            if (!OnlySearch)
            {
                if (UseForce && IsAreaBattle)
                {
                    //残りフォースを確認
                    var forceCounts = driver_.FindElementsByXPath("//div[@class=\"force-count\"]/span");
                    int forceCount = 0;
                    foreach (var fc in forceCounts)
                    {
                        if (fc.Text != "0")
                            forceCount++;
                    }

                    int forceChargeCount = 0;
                    try
                    {
                        forceChargeCount = Convert.ToInt32(driver_.FindElementByXPath("//span[@class=\"force-charge\"]").Text);
                    }
                    catch { }


                    if (forceCount > 0 || forceChargeCount > 0)
                    {
                        //フォース取得
                        var forces = driver_.FindElementsByXPath("//div[contains(@class,\"js-force-submit\")]");


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
                                IWebElement elm = driver_.FindElementByXPath("//a[contains(text(), \"敵を\")]");
                                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                                Exec = SearchState;
                                return;
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
                                        //カウントが無ければフォースチャージを使用する
                                        forces[i].FindElement(By.XPath("form[@method=\"post\"]")).Submit();
                                        Exec = SearchState;
                                        return;
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
                    foreach (var enemy in enemys)
                    {
                        try
                        {
                            if (AttackedEnemyId.IndexOf(GetEnemyId(enemy.GetAttribute("href"))) < 0)
                            {
                                driver_.Navigate().GoToUrl(enemy.GetAttribute("href"));
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
                        var elms = driver_.FindElementsByXPath("//div[@class=\"target-button js-target-button combo-chance\"]/..");
                        if (elms.Count > 0)
                        {
                            var combo = driver_.FindElementsByXPath("//div[@class=\"target-button js-target-button combo-chance\"]/../div[@class=\"combo\"]/span[@class=\"js-ms-combo\"]").Where(e => e.Text.Length > 0).Select(e => Convert.ToInt32(e.Text)).ToList();
                            idx = combo.IndexOf(combo.Max());
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
                        var elms = driver_.FindElementsByXPath("//a[@class=\"btn-raidboss-attack request\"]");
                        if (elms.Count > 0)
                        {
                            var combo = elms.Select(e => Convert.ToInt32(e.FindElement(By.XPath("//a[@class=\"btn-raidboss-attack request\"]/../dl[@class=\"raidboss-combo\"]//span")).Text)).ToList();
                            idx = combo.IndexOf(combo.Max());
                            driver_.Navigate().GoToUrl(elms[idx].GetAttribute("href"));
                            Exec = SearchState;
                            IsCombo = true;
                            return;
                        }
                    }
                    catch { }

                    //未攻撃
                    try
                    {
                        var elms = driver_.FindElementsByXPath("//div[@class=\"target-button js-target-button before-attack\"]/..");
                        if (elms.Count > 0)
                        {
                            var combo = driver_.FindElementsByXPath("//div[@class=\"target-button js-target-button before-attack\"]/../div[@class=\"combo\"]/span[@class=\"js-ms-combo\"]").Where(e => e.Text.Length > 0).Select(e => Convert.ToInt32(e.Text)).ToList();
                            idx = combo.IndexOf(combo.Max());
                            driver_.Navigate().GoToUrl(elms[idx].GetAttribute("href"));
                            Exec = SearchState;
                            RemoveEnemyId(GetEnemyId(driver_.Url));
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
                    IWebElement elm = driver_.FindElementByXPath("//a[contains(text(), \"敵を\")]");
                    driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                    Exec = SearchState;
                    return;
                }
            }
            catch { }


            try
            {
                IWebElement elm = driver_.FindElementByXPath("//a[contains(text(), \"戦況を\")]");
                Wait(5);
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
            AreaMovingCount--;
            Exec = SearchState;

            try
            {
                IWebElement elm = driver_.FindElementByXPath("//div[contains(text(), \"BE回復ミニカプセル\")]/span");
                if (MinicapCount != Convert.ToInt32(elm.Text)){
                    MinicapChanged?.Invoke(this, Convert.ToInt32(elm.Text));
                    MinicapCount = Convert.ToInt32(elm.Text);
                }
            }
            catch { }

            
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


            try
            {
                //敵HPを取得
                IWebElement elm = driver_.FindElementByXPath("//div[@class=\"raid_boss_summary_para\" or @class=\"raid_boss_summary_para bac\"]");
                string strhp = elm.Text;
                string current_hp = strhp.Replace(",", "").Split(new char[] { '/' })[0];
                UInt64 hp = UInt64.Parse(current_hp);


                var elms = driver_.FindElementsByXPath("//*[@class=\"energy-btn\"]/a");

                //コンボ数取得
                double combo = 1;

                try
                {
                    elm = driver_.FindElementByXPath("//div[@id=\"attc\"]/strong");
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
                    useBe--;
                    AddEnemyId(driver_.Url);
                    driver_.Navigate().GoToUrl(elms.ElementAt(useBe).GetAttribute("href"));
                    Attacked = true;
                }
            }
            catch { }

            IsCombo = false;
        }

        //戦略拠点での戦闘
        private void StrategicAreaBattle()
        {
            CurrentArea = "戦略拠点";
            Exec = SearchState;

            AreaChanged?.Invoke(this, CurrentArea);

            enemy_list_path_ = driver_.Url;

            //報酬受け取り
            if (ReceiveReword)
            {
                try
                {
                    var rewords = driver_.FindElementsByXPath("//*[text()=\"報酬を受け取る\"]");
                    if (rewords.Count() >= 5)
                    {
                        IWebElement elm = driver_.FindElementByXPath("//a[contains(text(),\"報酬をまとめて受け取る\")]");
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
                    IWebElement elm = driver_.FindElementByXPath("//div[contains(@class,\"level-\")]");
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
                    IWebElement mini = driver_.FindElementByXPath("//div[contains(text(),\"BE回復ミニカプセル\")]/strong");

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
                    var forces = driver_.FindElementsByXPath("//div[contains(@class,\"js-force-submit\")]");
                    var forceCount = driver_.FindElementsByXPath("//div[contains(@class,\"js-force-submit\")]//span").Select(e => Convert.ToInt32(e.Text)).ToArray();
                    
                    var enemys = driver_.FindElementsByXPath("//div[contains(@class,\"js-raid-boss-info\") and contains(@style,\"visible\")]");

                    //敵出現中
                    if(enemys.Count() > 0)
                    {
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


                                ulong maxHp = hp.Max();

                                if (maxHp <= WeakForceDamage && forceCount[0] > 0 || WeakForceDamage == 0)
                                {
                                    forces[0].FindElement(By.TagName("form")).Submit();
                                    PrevUsedForce = Force.Weak;
                                }
                                else
                                {
                                    forces[1].FindElement(By.TagName("form")).Submit();
                                    PrevUsedForce = Force.Strong;
                                }
                                break;
                            //強弱弱弱...
                            case ForcePattern.StrongWeak:
                                switch (PrevUsedForce)
                                {
                                    case Force.None:
                                        forces[1].FindElement(By.TagName("form")).Submit();
                                        PrevUsedForce = Force.Strong;
                                        break;
                                    case Force.Weak:
                                        forces[0].FindElement(By.TagName("form")).Submit();
                                        PrevUsedForce = Force.Weak;
                                        break;
                                    case Force.Strong:
                                        forces[0].FindElement(By.TagName("form")).Submit();
                                        PrevUsedForce = Force.Weak;
                                        break;
                                    default:
                                        forces[0].FindElement(By.TagName("form")).Submit();
                                        PrevUsedForce = Force.Weak;
                                        break;
                                }
                                break;
                            //弱のみ
                            case ForcePattern.WeakWeak:
                                forces[0].FindElement(By.TagName("form")).Submit();
                                PrevUsedForce = Force.Weak;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        if (StrategyAreaForcePattern == ForcePattern.StrongWeak) PrevUsedForce = Force.None;

                        //探索
                        if (forceCount[1] > 0 && StrategyAreaForcePattern != ForcePattern.WeakWeak)
                        {
                            forces[2].FindElement(By.TagName("form")).Submit();
                            return;
                        }
                        else
                        {
                            IWebElement searchButton = driver_.FindElementByXPath("//a[contains(@id,\"search_button\")]");
                            driver_.Navigate().GoToUrl(searchButton.GetAttribute("href"));
                            return;
                        }
                    }
                }
                catch { }
            }
            catch { }
        }
    }
}
