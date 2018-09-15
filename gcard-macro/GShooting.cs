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
    class GShooting : Event
    {
        public delegate void StateChangedHandler(object sender, State state);
        public event StateChangedHandler StateChanged;
        public delegate void MinicapChangedHandler(object sender, int count);
        public event MinicapChangedHandler MinicapChanged;
        public delegate void LogHandler(object sender, string text);
        public event LogHandler Log;

        public bool Request { get; set; }

        public GShooting(RemoteWebDriver driver, string home_path) : base(driver, home_path)
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
        }

        override protected void SearchState()
        {
            try
            {
                //イベントホーム
                if (IsHome())
                {
                    CurrentState = State.Home;
                    Wait(WaitSearch);
                    Exec = MoveEventHomeToSearch;
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
                //応援依頼完了
                else if (IsRequestComplete())
                {
                    CurrentState = State.RequestComplete;
                    Wait(WaitMisc);
                    Exec = MoveRequestCompleteToBattle;
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
        override protected bool IsEnemyList() => driver_.PageSource.IndexOf("戦況を更新する") >= 0 && driver_.PageSource.IndexOf("探索する") >= 0;

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
        /// 敵一覧から戦闘へ
        /// </summary>
        private void MoveEnemyListToSearch()
        {
            if(enemy_list_path_ == "")
            {
                enemy_list_path_ = driver_.Url;
            }

            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> enemys = null;

            try
            {
                //全ボタン検索                
                enemys = driver_.FindElementsByXPath("//a[contains(@class,\"btn-raidboss-\")]");
                if (enemys.Count == 0) AttackedEnemyId.Clear();
            }
            catch { }


            //探索のみ
            if (!OnlySearch)
            {
                //コンボ数が多い順に狙う
                int idx = -1;
                Exec = SearchState;


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
                    IWebElement elm = driver_.FindElementByXPath("//a[text()=\"探索する\"]");
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

            if (Request)
            {
                try
                {
                    //応援依頼が出ていれば依頼する
                    IWebElement elm = driver_.FindElementByXPath("//form[contains(@action, \"request\")]");
                    elm.Submit();
                    Exec = SearchState;
                    return;
                }
                catch { }
            }


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
                ulong hp = ulong.Parse(current_hp);


                var elms = driver_.FindElementsByXPath("//*[@class=\"flex w100p\"]/a");

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
    }
}
