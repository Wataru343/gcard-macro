using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace gcard_macro
{
    class Group : Event
    {
        public bool UseAttack20 { get; set; }
        public bool UseAttack10 { get; set; }
        public bool FirstAttackBoss { get; set; }
        public bool UseBoost { get; set; }
        public ulong PointDiff { get; set; }
        public bool AutojobLevelUp { get; set; }
        private bool IsMemorialBoss { get; set; }
        private bool BoostActivated { get; set; }
        private bool EnemyMirageColloidActivated { get; set; }
        private double AttackerJobRatio { get; set; }
        private bool AllJobLevelMax { get; set; }

        public delegate void StateChangedHandler(object sender, State state);
        public event StateChangedHandler StateChanged;
        public delegate void MinicapChangedHandler(object sender, int count);
        public event MinicapChangedHandler MinicapChanged;

        public Group(ChromeDriver driver, string home_path) : base(driver, home_path)
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
                        CurrentState = State.GroupInterval;
                        Wait(10);
                    }
                }
                //ジョブ選択
                else if (IsSelectJobs())
                {
                    CurrentState = State.SelectJobs;
                    Wait(WaitSearch);
                    Exec = SelectJobs;
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
                    IsMemorialBoss = false;
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
                    IsMemorialBoss = false;
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
                //BOOST使用
                else if (IsUseBoost())
                {
                    CurrentState = State.UseBoost;
                    Wait(WaitMisc);
                    Exec = UseBoostItem;                    
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
        /// インターバル中判定
        /// </summary>
        /// <returns></returns>
        private bool IsHomeDuringInterval() => driver_.PageSource.IndexOf("インターバル中です｡お待ち下さい") >= 0;

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
            try
            {
                IWebElement elm = driver_.FindElementByXPath("//a[@class=\"search\" or @class=\"attack\"]");
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
            }
            catch { }

            Exec = SearchState;
        }

        /// <summary>
        /// ジョブを選択する
        /// </summary>
        private void SelectJobs()
        {
            //var elms = driver_.FindElementsByXPath("//p[@class=\"job-level\"]");
            try
            {
                var elms = driver_.FindElementsByXPath("//div[@class=\"job-select-box\"]");

                foreach (var box in elms)
                {
                    try
                    {
                        string level = box.FindElement(By.ClassName("job-level")).Text;
                        string num = level.Replace("Lv.", "");
                        if (num.IndexOf("5") < 0)
                        {
                            IWebElement selectButton = box.FindElement(By.ClassName("btn-select"));
                            driver_.Navigate().GoToUrl(selectButton.GetAttribute("href"));
                            Exec = SearchState;
                            return;
                        }
                    }
                    catch { }
                }
                driver_.Navigate().GoToUrl(home_path_);
            }
            catch { }

            AllJobLevelMax = true;

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
                IWebElement elm = driver_.FindElementByXPath("//a[text()=\"使用する\"]");
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
            return;
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

            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> enemys = null;
            //探索のみ
            if (!OnlySearch)
            {
                //ジョブチェンジ
                if (AutojobLevelUp && !AllJobLevelMax)
                {
                    try
                    {
                        //ジョブレベルがマックスなら
                        IWebElement elm = driver_.FindElementByXPath("//*[@class=\"level\"]/dd");

                        if (elm.Text[0] == '5')
                        {
                            IWebElement button = driver_.FindElementByXPath("//a[@class=\"btn-change\"]");
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
                        string myPointStr = driver_.FindElementByXPath("//div[@class=\"round-my-point\"]").Text;
                        ulong myPoint = Convert.ToUInt64(myPointStr.Replace(",", "").Replace("pt", ""));

                        string enemyPointStr = driver_.FindElementByXPath("//div[@class=\"round-enemy-point\"]").Text;
                        ulong enemyPoint = Convert.ToUInt64(enemyPointStr.Replace(",", "").Replace("pt", ""));

                        EnemyMirageColloidActivated = false;

                        //味方部隊の点数と敵部隊の点数の差が既定値以上なら待機
                        if (myPoint >= enemyPoint && myPoint - enemyPoint >= PointDiff)
                        {
                            IWebElement elm = driver_.FindElementByXPath("//a[text()=\"戦況を更新する\" or text()=\"戦況を更新\"]");
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
                        var rewords = driver_.FindElementsByXPath("//*[text()=\"報酬を受け取る\"]");
                        if (rewords.Count() >= ReceiveCount)
                        {
                            IWebElement elm = driver_.FindElementByXPath("//a[text()=\"報酬をまとめて受け取る\"]");
                            driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
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
                    IWebElement elm = driver_.FindElementByXPath("//div[@class=\"job-status attack\" or @class=\"job-status attack mb4\"]/span");
                    AttackerJobRatio = Convert.ToDouble(elm.Text);
                }
                catch
                {
                    AttackerJobRatio = 1.0;
                }

                //BOOSTが発動中であれば倍率を3倍にする
                try
                {
                    IWebElement elm = driver_.FindElementByXPath("//div[contains(@class, \"boost-banner\")]");
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
                        IWebElement elm = driver_.FindElementByXPath("//img[@class=\"use-btn\" and @alt=\"発動する\"]/..");
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
                        var elms = driver_.FindElementsByXPath("//p[contains(@class,\"raidboss-name\")]");

                        List<int> indexes = new List<int>();
                        for (int i = 0; i < elms.Count; i++)
                        {
                            if (elms.ElementAt(i).Text.IndexOf("ﾒﾓﾘｱﾙﾎﾞｽ") >= 0)
                                indexes.Add(i);
                        }

                        int maxCombo = 0;
                        int index = indexes[0];

                        try
                        {
                            var combos = driver_.FindElementsByXPath("//p[contains(@class,\"raidboss-name\")]/../dl[@class=\"raidboss-combo\"]/dd/span");

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
                            var buttons = driver_.FindElementsByXPath("//p[contains(@class,\"raidboss-name\")]/../a");
                            driver_.Navigate().GoToUrl(buttons[index].GetAttribute("href"));
                            Exec = SearchState;
                            IsMemorialBoss = true;
                            return;
                        }
                        catch { }
                    }
                    catch { }
                }


                //全ボタン検索                
                enemys = driver_.FindElementsByXPath("//div[contains(@id,\"raidboss\") and contains(@id,\"wrapper\")]//a");
                if (enemys.Count == 0) AttackedEnemyId.Clear();


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
                        var elms = driver_.FindElementsByXPath("//a[@class=\"btn-raidboss-attack chance\"]");
                        if (elms.Count > 0)
                        {
                            var combo = elms.Select(e => Convert.ToInt32(e.FindElement(By.XPath("//a[@class=\"btn-raidboss-attack chance\"]/../dl[@class=\"raidboss-combo\"]//span")).Text)).ToList();
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
                        var elms = driver_.FindElementsByXPath("//a[@class=\"btn-raidboss-attack not\"]");
                        if (elms.Count > 0)
                        {
                            var combo = elms.Select(e => Convert.ToInt32(e.FindElement(By.XPath("//a[@class=\"btn-raidboss-attack not\"]/../dl[@class=\"raidboss-combo\"]//span")).Text)).ToList();
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
                    var elms = driver_.FindElementsByXPath("//dl[@class=\"raidboss-combo\"]//span ");

                    IWebElement elm = driver_.FindElementByXPath("//a[text()=\"敵を見つける\"]");
                    driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                    Exec = SearchState;
                    return;
                }
            }
            catch { }


            try
            {
                IWebElement elm = driver_.FindElementByXPath("//a[text()=\"戦況を更新する\" or text()=\"戦況を更新\"]");
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
            

            //無制限に攻撃するでない or メモリアルボスでない
            if (Mode != AttackMode.Unlimited && !IsMemorialBoss)
            {
                if (IsAttacked(driver_.Url) && !IsCombo)
                {
                    if (Mode != AttackMode.Unlimited && !IsMemorialBoss)
                    {
                        Attacked = false;
                        driver_.Navigate().GoToUrl(enemy_list_path_);
                        return;
                    }
                }
            }


            try
            {
                //敵HPを取得
                IWebElement elm = driver_.FindElementByXPath("//div[@class=\"raid_boss_summary_para\" or @class=\"raid_boss_summary_para bac\"]/p[@class=\"flex\"]/span");
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
                double boost = BoostActivated ? 3.0 : AttackerJobRatio;
                int requiredRatio = Utils.CalcUseMiniCapsules(hp, BaseDamage, boost, combo);
                int useBe = 0;
                if (requiredRatio == 0) requiredRatio = 1;

                //BEx5使用10倍攻撃
                if (UseAttack10 && requiredRatio > 5)
                {
                    try
                    {
                        var be = driver_.FindElementsByXPath("//*[@class=\"flex\" or @class=\"mt4\"]/a");
                        AddEnemyId(driver_.Url);
                        driver_.Navigate().GoToUrl(be.ElementAt(0).GetAttribute("href"));
                        Attacked = true;
                    }
                    catch { }
                }

                //BEx3使用20倍攻撃
                if (EnemyMirageColloidActivated && UseAttack20 && requiredRatio > 10)
                {
                    try
                    {
                        var be = driver_.FindElementsByXPath("//*[@class=\"flex txt-c\"]/a");
                        AddEnemyId(driver_.Url);
                        driver_.Navigate().GoToUrl(be.ElementAt(1).GetAttribute("href"));
                        Attacked = true;
                    }
                    catch { }
                }


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
    }
}
