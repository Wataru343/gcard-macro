using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;
using System.IO;
using OpenQA.Selenium;

namespace gcard_macro
{
    class Group : Event
    {
        override public event StateChangedHandler StateChanged;
        override public event MinicapChangedHandler MinicapChanged;
        override public event LogHandler Log;

        public bool FirstAttackBoss { get; set; }
        public bool UseBoost { get; set; }
        public ulong PointDiff { get; set; }
        public bool AutojobLevelUp { get; set; }
        public int FinalJob { get; set; }

        public Combo30Button Combo30 { get; set; }
        public Attack20Button Attack20 { get; set; }
        public Attack10Button Attack10 { get; set; }
        public BE1Button BE1 { get; set; }

        private bool IsMemorialBoss { get; set; }
        private bool BoostActivated { get; set; }
        private bool EnemyMirageColloidActivated { get; set; }
        private double AttackerJobRatio { get; set; }
        private bool AllJobLevelMax { get; set; }
        

        public Group(IWebDriver driver, string home_path) : base(driver, home_path)
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
            AttackerJobRatio = 0.0;
            FirstAttackBoss = false;
            UseBoost = false;
            PointDiff = 0;
            AutojobLevelUp = false;
            FinalJob = 0;
            Combo30 = new Combo30Button();
            Attack20 = new Attack20Button();
            Attack10 = new Attack10Button();
            BE1 = new BE1Button();

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
                //ジョブ選択
                else if (IsSelectJobs())
                {
                    Log?.Invoke(this, "ページ移動：ジョブ選択画面");
                    CurrentState = State.GroupSelectJobs;
                    Wait(WaitMisc);
                    Exec = SelectJobs;
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
                    Exec = ClickBattleFlash;
                }
                //敵一覧
                else if (IsEnemyList())
                {
                    Log?.Invoke(this, "ページ移動：敵一覧画面");
                    CurrentState = State.EnemyList;
                    Wait(WaitMisc);
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
                    Exec = MoveResultToEnemyList;
                    Wait(WaitMisc);
                    IsMemorialBoss = false;
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
                //BOOST使用
                else if (IsUseBoost())
                {
                    Log?.Invoke(this, "ページ移動：BOOST使用画面");
                    CurrentState = State.GroupUseBoost;
                    Wait(WaitMisc);
                    Exec = UseBoostItem;
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
                    try
                    {
                        if (enemy_list_path_ != null)
                        {
                            driver_.Navigate().GoToUrl(enemy_list_path_);
                        }
                        else
                        {
                            driver_.Navigate().GoToUrl(home_path_);

                        }
                    }
                    catch
                    {
                        driver_.Navigate().GoToUrl(home_path_);
                    }

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
                    KillThread();
                    Log?.Invoke(this, "サーバーエラー");
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
            return;
        }


        /// <summary>
        /// ジョブ選択画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsSelectJobs() => driver_.PageSource.IndexOf("ジョブ選択") >= 0 && driver_.PageSource.IndexOf("バトルタイプ") >= 0;

        /// <summary>
        /// BOOST使用画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsUseBoost() => driver_.PageSource.IndexOf("ブースターパック") >= 0;


        /// <summary>
        /// イベントのホームから探索へ
        /// </summary>
        private void MoveEventHomeToSearch()
        {
            Exec = SearchState;

            try
            {
                if (enemy_list_path_ != "")
                {
                    driver_.Navigate().GoToUrl(enemy_list_path_);
                    return;
                }

                IWebElement elm = driver_.FindElement(By.XPath("//a[@class=\"search\" or @class=\"attack\"]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
            }
            catch { }
        }

        /// <summary>
        /// ジョブを選択する
        /// </summary>
        private void SelectJobs()
        {
            List<IWebElement> elms = null;

            try
            {
                string[] jobs = new string[] { "attacker", "commander", "seeker", "master", "supporter" };
                string[] jobnames = new string[] { "アタッカー", "コマンダー", "シーカー", "スーパーエース", "スーパーサポーター" };

                elms = driver_.FindElements(By.XPath("//div[@class=\"job-select-box\"]")).ToList();

                for(int i = 0; i < jobs.Length; i++)
                {
                    try
                    {
                        string level = driver_.FindElement(By.XPath(string.Format("//div[@class=\"job-select-box {0}\"]//p", jobs[i]))).Text;
                        string num = level.Replace("Lv.", "");
                        if (num.IndexOf("5") < 0)
                        {
                            IWebElement selectButton = driver_.FindElement(By.XPath(string.Format("//div[@class=\"job-select-box {0}\"]//a", jobs[i])));
                            driver_.Navigate().GoToUrl(selectButton.GetAttribute("href"));
                            Exec = SearchState;
                            Log?.Invoke(this, "ジョブ変更：" + jobnames[i]);
                            return;
                        }
                    }
                    catch { }
                }

                try
                {
                    IWebElement button = driver_.FindElement(By.XPath(string.Format("//div[@class=\"job-select-box {0}\"]//a", jobs[FinalJob])));
                    driver_.Navigate().GoToUrl(button.GetAttribute("href"));
                }
                catch { }

                driver_.Navigate().GoToUrl(home_path_);
            }
            catch { }

            AllJobLevelMax = true;

            Log?.Invoke(this, "全ジョブレベル最大");



            Exec = SearchState;
            return;
        }

        /// <summary>
        /// BOOSTを使用する
        /// </summary>
        private void UseBoostItem()
        {
            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"使用する\"]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                Log?.Invoke(this, "BOOSTを使用");
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
            return;
        }

        /// <summary>
        /// 敵一覧から戦闘へ
        /// </summary>
        private void MoveEnemyListToSearch()
        {
            if (enemy_list_path_ == "")
            {
                enemy_list_path_ = driver_.Url;
            }


            List<IWebElement> buttons = null;
            List<IWebElement> enemyNames = null;
            List<Tuple<IWebElement, IWebElement>> enemys = null;

            try
            {
                //全ボタン検索                
                buttons = driver_.FindElements(By.XPath("//div[contains(@id,\"raidboss\") and contains(@id,\"wrapper\") and not(contains(@id,\"dummy\"))]//a")).ToList();
                enemyNames = driver_.FindElements(By.XPath("//p[contains(@class,\"raidboss-name\")]")).Take(buttons.Count()).ToList();

                enemys = buttons.Zip(enemyNames, (b, n) => new Tuple<IWebElement, IWebElement>(b, n)).OrderBy(i => Guid.NewGuid()).ToList();

                if (enemys.Count == 0) AttackedEnemyId.Clear();
            }
            catch { }


            //探索のみ
            if (!OnlySearch)
            {
                //ジョブチェンジ
                if (AutojobLevelUp && !AllJobLevelMax)
                {
                    try
                    {
                        //ジョブレベルがマックスなら
                        IWebElement elm = driver_.FindElement(By.XPath("//*[@class=\"level\"]/dd"));

                        if (elm.Text[0] == '5')
                        {
                            IWebElement button = driver_.FindElement(By.XPath("//a[@class=\"btn-change\"]"));
                            driver_.Navigate().GoToUrl(button.GetAttribute("href"));
                            Exec = SearchState;
                            return;
                        }
                    }
                    catch { }
                }


                //コンボ数が多い順に狙う
                int idx = -1;
                Exec = SearchState;


                if (PointDiff > 0)
                {
                    //敵部隊との点数差計算
                    try
                    {
                        string myPointStr = driver_.FindElement(By.XPath("//div[@class=\"round-my-point\"]")).Text;
                        ulong myPoint = Convert.ToUInt64(myPointStr.Replace(",", "").Replace("pt", ""));

                        string enemyPointStr = driver_.FindElement(By.XPath("//div[@class=\"round-enemy-point\"]")).Text;
                        ulong enemyPoint = Convert.ToUInt64(enemyPointStr.Replace(",", "").Replace("pt", ""));

                        EnemyMirageColloidActivated = false;

                        //味方部隊の点数と敵部隊の点数の差が既定値以上なら待機
                        if (myPoint >= enemyPoint && myPoint - enemyPoint >= PointDiff)
                        {
                            IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"戦況を更新する\" or text()=\"戦況を更新\"]"));
                            driver_.Navigate().Refresh();
                            Wait(5);
                            Exec = SearchState;
                            return;
                        }
                    }
                    catch
                    {
                        EnemyMirageColloidActivated = true;
                    }
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
                            Log?.Invoke(this, string.Format("報酬{0}個確認", rewords.Count()));
                            return;
                        }
                    }
                    catch
                    { }
                }


                //ジョブの攻撃力倍率を取得
                //div[@class="job-status attack"]/span
                try
                {
                    IWebElement elm = driver_.FindElement(By.XPath("//div[@class=\"job-status attack\" or @class=\"job-status attack mb4\"]/span"));
                    AttackerJobRatio = Convert.ToDouble(elm.Text);
                }
                catch
                {
                    AttackerJobRatio = 1.0;
                }

                //BOOSTが発動中であれば倍率を3倍にする
                try
                {
                    IWebElement elm = driver_.FindElement(By.XPath("//div[contains(@class, \"boost-banner\")]"));
                    BoostActivated = true;
                }
                catch
                {
                    BoostActivated = false;
                }


                //BOOST使用
                if (UseBoost)
                {
                    try
                    {
                        IWebElement elm = driver_.FindElement(By.XPath("//img[@class=\"use-btn\" and @alt=\"発動する\"]/.."));
                        driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                        return;
                    }
                    catch { }
                }


                //メモリアルボス検索
                if (FirstAttackBoss)
                {
                    try
                    {
                        List<int> indexes = new List<int>();
                        for (int i = 0; i < enemys.Count; i++)
                        {
                            if (enemys[i].Item2.Text.IndexOf("ﾒﾓﾘｱﾙﾎﾞｽ") >= 0)
                                indexes.Add(i);
                        }

                        int maxCombo = 0;
                        int index = indexes[0];

                        try
                        {
                            var combos = driver_.FindElements(By.XPath("//p[contains(@class,\"raidboss-name\")]/../dl[@class=\"raidboss-combo\"]/dd/span"));

                            foreach (int i in indexes)
                            {
                                int conbo = Convert.ToInt32(combos[i].Text);
                                if (maxCombo < conbo)
                                {
                                    maxCombo = conbo;
                                    index = i;
                                }
                            }
                        }
                        catch { }

                        try
                        {
                            var btns = driver_.FindElements(By.XPath("//p[contains(@class,\"raidboss-name\")]/../a"));
                            Log?.Invoke(this, "メモリアルボス確認");
                            Log?.Invoke(this, "攻撃： " + enemys[index].Item2.Text);
                            driver_.Navigate().GoToUrl(btns[index].GetAttribute("href"));
                            Exec = SearchState;
                            IsMemorialBoss = true;
                            return;
                        }
                        catch { }
                    }
                    catch { }
                }


                //一度しか攻撃しない場合
                if (Mode == AttackMode.OneAttack)
                {
                    for (int i = 0; i < enemys.Count(); i++)
                    {
                        try
                        {
                            if (!IsAttacked(enemys[i].Item1.GetAttribute("href")))
                            {
                                Log?.Invoke(this, "攻撃： " + enemys[i].Item2.Text);
                                driver_.Navigate().GoToUrl(enemys[i].Item1.GetAttribute("href"));
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
                        Log?.Invoke(this, "攻撃： " + enemys[0].Item2.Text);
                        driver_.Navigate().GoToUrl(enemys[0].Item1.GetAttribute("href"));
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
                        var elms = driver_.FindElements(By.XPath("//a[@class=\"btn-raidboss-attack chance\"]")).ToList();
                        if (elms.Count > 0)
                        {
                            elms = elms.OrderBy(i => Guid.NewGuid()).ToList();
                            var combo = elms.Select(e => Convert.ToInt32(e.FindElement(By.XPath("//a[@class=\"btn-raidboss-attack chance\"]/../dl[@class=\"raidboss-combo\"]//span")).Text)).ToList();

                            idx = combo.IndexOf(combo.Max());

                            try
                            {
                                Log?.Invoke(this, "攻撃： " + elms[idx].FindElement(By.XPath("../p")).Text);
                            }
                            catch { }

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
                        var elms = driver_.FindElements(By.XPath("//a[@class=\"btn-raidboss-attack request\"]")).ToList();
                        if (elms.Count > 0)
                        {
                            elms = elms.OrderBy(i => Guid.NewGuid()).ToList();
                            var combo = elms.Select(e => Convert.ToInt32(e.FindElement(By.XPath("//a[@class=\"btn-raidboss-attack request\"]/../dl[@class=\"raidboss-combo\"]//span")).Text)).ToList();

                            idx = combo.IndexOf(combo.Max());

                            try
                            {
                                Log?.Invoke(this, "攻撃： " + elms[idx].FindElement(By.XPath("../p")).Text);
                            }
                            catch { }

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
                        var elms = driver_.FindElements(By.XPath("//a[@class=\"btn-raidboss-attack not\"]")).ToList();
                        if (elms.Count > 0)
                        {
                            elms = elms.OrderBy(i => Guid.NewGuid()).ToList();
                            var combo = elms.Select(e => Convert.ToInt32(e.FindElement(By.XPath("//a[@class=\"btn-raidboss-attack not\"]/../dl[@class=\"raidboss-combo\"]//span")).Text)).ToList();

                            idx = combo.IndexOf(combo.Max());

                            try
                            {
                                Log?.Invoke(this, "攻撃： " + elms[idx].FindElement(By.XPath("../p")).Text);
                            }
                            catch { }

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
                        IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"敵を見つける\"]"));
                        Log?.Invoke(this, "探索開始");

                        string url = elm.GetAttribute("href");

                        if (url == null)
                        {
                            url = "http://gcc.sp.mbga.jp/" + elm.GetAttribute("data-mission-path");
                        }

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
                    }
                }
            }
            catch { }


            try
            {
                Log?.Invoke(this, string.Format("敵出現数： {0}", enemys.Count()));
                Log?.Invoke(this, "攻撃対象無し");
                Log?.Invoke(this, "待機中");
                Wait(2);
                Log?.Invoke(this, "ページ更新");
                //AttackedEnemyId.Clear();
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
                //無制限に攻撃するでない or メモリアルボスでない
                if (Mode != AttackMode.Unlimited && !IsMemorialBoss)
                {
                    if (IsAttacked(driver_.Url) && !IsCombo)
                    {
                        if (Mode != AttackMode.Unlimited && !IsMemorialBoss)
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
                //敵HPを取得
                IWebElement elm = driver_.FindElement(By.XPath("//div[@class=\"raid_boss_summary_para\" or @class=\"raid_boss_summary_para bac\"]/p[@class=\"flex\"]/span"));
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
                double boost = BoostActivated ? 3.0 : AttackerJobRatio;
                int requiredRatio = Utils.CalcUseMiniCapsules(hp, BaseDamage, boost, combo);
                int useBe = 0;
                if (requiredRatio == 0) requiredRatio = 1;



                //攻撃処理
                bool func(SpecialAttackButton b)
                {
                    try
                    {
                        IWebElement be = null;
                        try
                        {
                            be = driver_.FindElement(By.XPath(string.Format("//img[contains(@src,\"{0}\") or contains(@src,\"{1}\")]/..", b.DisableLargeButtonName, b.DisableSmallButtonName)));
                        }
                        catch 
                        {
                            be = driver_.FindElement(By.XPath(string.Format("//img[contains(@src,\"{0}\") or contains(@src,\"{1}\")]/..", b.LargeButtonName, b.SmallButtonName)));
                        }

                        Wait(WaitAttack);
                        string ret = GetWebClient().DownloadString(be.GetAttribute("href"));

                        if (ret.IndexOf("戦闘は終了") >= 0 || ret.IndexOf("このボスと") >= 0)
                        {
                            StateChanged?.Invoke(this, State.FightAlreadyFinished);
                            Log?.Invoke(this, "ページ移動：戦闘終了済み通知画面");
                            Wait(WaitMisc);
                            driver_.Navigate().GoToUrl(enemy_list_path_);
                            return true;
                        }
                        else if (ret.IndexOf("swf") < 0)
                        {
                            StateChanged?.Invoke(this, State.AccessBlock);
                            Log?.Invoke(this, "ページ移動：アクセス制限通知画面");
                            Wait(WaitAccessBlock);
                            driver_.Navigate().GoToUrl(home_path_);
                            return true;
                        }

                        Log?.Invoke(this, string.Format("{0}使用", b.ToString()));
                        AddEnemyId(driver_.Url);
                        driver_.Navigate().Refresh();
                        Attacked = true;
                        IsCombo = false;
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }


                //BEx1 1.2倍+30コンボ攻撃
                if (Combo30.Use)
                {
                    bool use = false;
                    if (EnemyMirageColloidActivated && Combo30.Mira) use = true;
                    else if (BoostActivated && Combo30.Boost) use = true;
                    else if (!EnemyMirageColloidActivated && !BoostActivated && Combo30.Normal) use = true;

                    if (use && Combo30.FirstAttack)
                    {
                        use = !IsAttacked(driver_.Url) && !IsCombo;
                    }

                    if (use)
                    {
                        if (func(Combo30)) return;
                    }
                }

                //BEx3 20倍攻撃
                if (Attack20.Use && requiredRatio > 5)
                {
                    bool use = false;
                    if (EnemyMirageColloidActivated && Attack20.Mira) use = true;
                    else if (BoostActivated && Attack20.Boost) use = true;
                    else if (!BoostActivated && Attack20.Normal) use = true;

                    if (Attack20.RequiredRatio)
                    {
                        use = requiredRatio >= 20;
                    }

                    if (use)
                    {
                        if (func(Attack20)) return;
                    }
                }

                //BEカプセルx1 20倍攻撃
                if (BE1.Use && requiredRatio > 5)
                {
                    bool use = false;
                    if (EnemyMirageColloidActivated && BE1.Mira) use = true;
                    else if (!EnemyMirageColloidActivated && BE1.Normal) use = true;

                    if (BE1.RequiredRatio)
                    {
                        use = requiredRatio >= 20;
                    }

                    if (use)
                    {
                        if (func(BE1)) return;
                    }
                }

                //BEx5 10倍攻撃
                if (Attack10.Use && requiredRatio > 5)
                {
                    bool use = false;
                    if (BoostActivated && Attack10.Boost) use = true;
                    else if (!BoostActivated && Attack10.Normal) use = true;

                    if (use)
                    {
                        if (func(Attack10)) return;
                    }
                }


                if (requiredRatio > 3) useBe = 3;
                else if (requiredRatio > 1.2) useBe = 2;
                else useBe = 1;
                //useBe = 1;
                if (useBe > 0)
                {
                    //if (useBe == 1)
                    //{
                        useBe--;

                        string ret = "";

                        try
                        {
                            Wait(WaitAttack);
                            ret = GetWebClient().DownloadString(elms.ElementAt(useBe).GetAttribute("href"));
                        }
                        catch { }

                        if (ret.IndexOf("戦闘は終了") >= 0 || ret.IndexOf("このボスと") >= 0)
                        {
                            StateChanged?.Invoke(this, State.FightAlreadyFinished);
                            Log?.Invoke(this, "ページ移動：戦闘終了済み通知画面");
                            Wait(WaitMisc);
                            driver_.Navigate().GoToUrl(enemy_list_path_);
                            return;
                        }
                        else if (ret.IndexOf("swf") < 0)
                        {
                            StateChanged?.Invoke(this, State.AccessBlock);
                            Log?.Invoke(this, "ページ移動：アクセス制限通知画面");
                            Wait(WaitAccessBlock);
                            driver_.Navigate().GoToUrl(home_path_);
                            return;
                        }
                    //}

                    Log?.Invoke(this, string.Format("BEx{0}使用", useBe + 1));
                    AddEnemyId(driver_.Url);
                    driver_.Navigate().Refresh();
                    Attacked = true;
                }
            }
            catch { }

            IsCombo = false;
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
