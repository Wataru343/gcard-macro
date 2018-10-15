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
    class Promotion : Event
    {
        public delegate void StateChangedHandler(object sender, State state);
        public event StateChangedHandler StateChanged;
        public delegate void MinicapChangedHandler(object sender, int count);
        public event MinicapChangedHandler MinicapChanged;
        public delegate void SllayCountChangedHandler(object sender, int count);
        public event SllayCountChangedHandler SallyCountChanged;
        public delegate void LogHandler(object sender, string text);
        public event LogHandler Log;

        public int WatchRank { get; set; }
        public int SallyCount { get; set; }
        public DateTime SallyStart { get; set; }
        public DateTime SallyEnd { get; set; }

        public bool SallyUnlimited { get; set; }
        private DateTime prevTime { get; set; }
        private int BaseSallyCount { get; set; }

        public Promotion(IWebDriver driver, string home_path) : base(driver, home_path)
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
            SallyUnlimited = SallyCount == 0 ? true : false;
            prevTime = DateTime.Now;
            BaseSallyCount = -1;
        }

        override protected void SearchState()
        {
            if(BaseSallyCount == -1)
            {
                BaseSallyCount = SallyCount;
            }

            try
            {
                //イベントホーム
                if (IsHome())
                {
                    if (CurrentState != State.Home)
                        Log?.Invoke(this, "ページ移動：イベントホーム画面");
                    CurrentState = State.Home;
                    Wait(WaitSearch);
                    Exec = MoveEventHomeToSearch;
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
                //撤退確認画面
                else if (IsWithdrawalConfirmation())
                {
                    Log?.Invoke(this, "ページ移動：撤退確認画面");
                    CurrentState = State.PromotionWithdrawalConfirmation;
                    Wait(WaitMisc);
                    Exec = ConfirmWithdrawal;
                }
                //撤退完了画面
                else if (IsWithdrawalCompletion())
                {
                    Log?.Invoke(this, "ページ移動：撤退完了画面");
                    CurrentState = State.PromotionWithdrawalCompletion;
                    Wait(WaitMisc);
                    Exec = CompleteWithdrawal;
                }
                //出撃確認画面
                else if (IsSallyConfirmation())
                {
                    Log?.Invoke(this, "ページ移動：出撃確認画面");
                    CurrentState = State.PromotionSallyConfirmation;
                    Wait(WaitMisc);
                    Exec = ConfirmSally;
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
                //既に戦闘は終了しています
                else if (IsFightAlreadyFinished())
                {
                    Log?.Invoke(this, "ページ移動：戦闘終了済み通知画面");
                    CurrentState = State.FightAlreadyFinished;
                    Wait(WaitMisc);
                    driver_.Navigate().GoToUrl(home_path_);
                    Attacked = false;
                }
                //不正な画面遷移です
                else if (IsError())
                {
                    Log?.Invoke(this, "ページ移動：不正な画面遷移通知画面");
                    CurrentState = State.Error;
                    Wait(WaitMisc);
                    driver_.Navigate().GoToUrl(home_path_);
                }
                //アクセスを制限
                else if (IsAccessBlock())
                {
                    Log?.Invoke(this, "ページ移動：アクセス制限通知画面");
                    CurrentState = State.AccessBlock;
                    Wait(WaitAccessBlock);
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

            if(prevTime.Day < DateTime.Now.Day)
            {
                SallyCount = BaseSallyCount;
            }

            prevTime = DateTime.Now;
        }


        /// <summary>
        /// 敵一覧画面判定
        /// </summary>
        /// <returns></returns>
        override protected bool IsEnemyList() => driver_.Url == @"http://gcc.sp.mbga.jp/_gcard_promotion_battles" || driver_.Url == @"http://gcc.sp.mbga.jp/_gcard_regular_promotion_battles" || driver_.Url == @"http://gcc.sp.mbga.jp/_gcard_promotion_battles?ae=1";

        /// <summary>
        /// 戦闘画面判定
        /// </summary>
        /// <returns></returns>
        override protected bool IsBattle() => driver_.PageSource.IndexOf("バトルエネルギー") >= 0 && driver_.PageSource.IndexOf("今回使用するデッキ") >= 0;

        /// <summary>
        /// リザルト画面判定
        /// </summary>
        /// <returns></returns>
        override protected bool IsResult() => driver_.PageSource.IndexOf("次の対戦へ進む") >= 0 || driver_.PageSource.IndexOf("戦闘結果") >= 0;

        /// <summary>
        /// 撤退確認画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsWithdrawalConfirmation() => driver_.PageSource.IndexOf("撤退しますか") >= 0;

        /// <summary>
        /// 出撃確認画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsSallyConfirmation() => driver_.PageSource.IndexOf("出撃チケットを使って出撃しますか") >= 0;

        /// <summary>
        /// 撤退完了画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsWithdrawalCompletion() =>  driver_.PageSource.IndexOf("撤退しました") >= 0;


        /// <summary>
        /// イベントのホームから出撃
        /// </summary>
        private void MoveEventHomeToSearch()
        {
            try
            {
                try
                {
                    IWebElement rank = driver_.FindElement(By.XPath("//div[contains(text(), \"現在\")]/span"));

                    if(Convert.ToInt32(rank.Text) <= WatchRank)
                    {
                        Wait(5);
                        Exec = SearchState;
                        return;
                    }
                }
                catch { }

                TimeSpan now = DateTime.Now.TimeOfDay;
                TimeSpan start = SallyStart.TimeOfDay;
                TimeSpan end = SallyEnd.TimeOfDay;

                if (start >= end)
                {
                    end += TimeSpan.FromDays(1);
                    now += TimeSpan.FromDays(1);
                }

                if (now >= start && now < end)
                {
                    try
                    {
                        IWebElement elm = driver_.FindElement(By.XPath("//a[@class=\"sally now\"]"));
                        driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                    }
                    catch
                    {
                        if (SallyUnlimited || SallyCount > 0)
                        {
                            IWebElement elm = driver_.FindElement(By.XPath("//a[@class=\"sally\" or @class=\"sally now\" or @class=\"sally ticket\"]"));
                            driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                            Log?.Invoke(this, "出撃");
                            SallyCount--;
                        }
                        else
                        {
                            Wait(5);
                            Exec = SearchState;
                            return;
                        }
                    }
                }
                else
                {
                    Wait(5);
                    Exec = SearchState;
                    return;
                }
            }
            catch
            {
                Wait(5);
            }

            Exec = SearchState;
        }


        /// <summary>
        /// 戦闘結果から敵一覧へ
        /// </summary>
        override protected void MoveResultToEnemyList()
        {
            try
            {
                //var elms = driver_.FindElements(By.XPath("//input[@value=\"報酬を受け取る\"]");
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"次の対戦へ進む\"]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                Log?.Invoke(this, "次の対戦へ進む");
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
        /// 敵一覧から戦闘へ
        /// </summary>
        private void MoveEnemyListToSearch()
        {
            SallyCountChanged?.Invoke(this, SallyUnlimited ? 0 : SallyCount);


            if (enemy_list_path_ == "")
            {
                enemy_list_path_ = driver_.Url;
            }


            try
            {
                var enemyButton = driver_.FindElements(By.XPath("//li[@class=\"enemy-detail\"]/a"));
                var enemyPower = driver_.FindElements(By.XPath("//li[@class=\"enemy-detail\"]/a/dl/dd[@class=\"power\"]/span")).Select(e => Convert.ToUInt64(e.Text)).ToList();
                var enemyAlive = driver_.FindElements(By.XPath("//li[@class=\"enemy-detail\"]/a/dl/dd[@class=\"alive\"]/span")).Select(e => Convert.ToUInt64(e.Text)).ToList();
                var enemyPt = driver_.FindElements(By.XPath("//li[@class=\"enemy-detail\"]/a/dl/dd[@class=\"pt\"]/span")).Select(e => Convert.ToUInt64(e.Text.Replace(",", ""))).ToList();


                ulong myHP = 10000;
                ulong maxHP = 10000;

                try
                {
                    var hp = driver_.FindElement(By.XPath("//div[@class=\"hp flex\"]")).Text.Replace(",", "").Split(new char[] { '/' });
                    myHP = Convert.ToUInt64(hp[0]);
                    maxHP = Convert.ToUInt64(hp[1]);
                }
                catch { }

                Log?.Invoke(this, string.Format("残りHP: {0}/{1} ({2}%)", myHP, maxHP, myHP / (maxHP / 100)));

                switch (Mode)
                {
                    case AttackMode.攻撃力が低い敵を攻撃撤退無し:
                        int minIdx = enemyPower.IndexOf(enemyPower.Min());
                        driver_.Navigate().GoToUrl(enemyButton[minIdx].GetAttribute("href"));
                        Log?.Invoke(this, "攻撃");
                        break;

                    case AttackMode.攻撃力が低い敵を攻撃HP20パーセント以下で撤退:
                        if(myHP <= maxHP * 0.2)
                        {
                            IWebElement button = driver_.FindElement(By.XPath("//a[text()=\"撤退する\"]"));
                            driver_.Navigate().GoToUrl(button.GetAttribute("href"));
                            Log?.Invoke(this, "撤退");
                            break;
                        }
                        minIdx = enemyPower.IndexOf(enemyPower.Min());
                        driver_.Navigate().GoToUrl(enemyButton[minIdx].GetAttribute("href"));
                        Log?.Invoke(this, "攻撃");
                        break;

                    case AttackMode.PTが高い敵を攻撃撤退無し:
                        minIdx = enemyPt.IndexOf(enemyPt.Max());
                        driver_.Navigate().GoToUrl(enemyButton[minIdx].GetAttribute("href"));
                        break;

                    case AttackMode.PTが高い敵を攻撃HP20パーセント以下で撤退:
                        if (myHP <= maxHP * 0.2)
                        {
                            IWebElement button = driver_.FindElement(By.XPath("//a[text()=\"撤退する\"]"));
                            driver_.Navigate().GoToUrl(button.GetAttribute("href"));
                            Log?.Invoke(this, "撤退");
                            break;
                        }
                        minIdx = enemyPt.IndexOf(enemyPt.Max());
                        driver_.Navigate().GoToUrl(enemyButton[minIdx].GetAttribute("href"));
                        Log?.Invoke(this, "攻撃");
                        break;

                    case AttackMode.攻撃力割るMS数が低い敵を攻撃撤退無し:
                        var powPerMs = enemyPower.Zip(enemyAlive, (p, a) => new { pow = (double)p, ms = (double)a }).Select(e => e.pow / e.ms).ToList();
                        minIdx = powPerMs.IndexOf(powPerMs.Min());
                        driver_.Navigate().GoToUrl(enemyButton[minIdx].GetAttribute("href"));
                        Log?.Invoke(this, "攻撃");
                        break;

                    case AttackMode.攻撃力割るMS数が低い敵を攻撃HP20パーセント以下で撤退:
                        if (myHP <= maxHP * 0.2)
                        {
                            IWebElement button = driver_.FindElement(By.XPath("//a[text()=\"撤退する\"]"));
                            driver_.Navigate().GoToUrl(button.GetAttribute("href"));
                            Log?.Invoke(this, "撤退");
                            break;
                        }
                        powPerMs = enemyPower.Zip(enemyAlive, (p, a) => new { pow = (double)p, ms = (double)a }).Select(e => e.pow / e.ms).ToList();
                        minIdx = powPerMs.IndexOf(powPerMs.Min());
                        driver_.Navigate().GoToUrl(enemyButton[minIdx].GetAttribute("href"));
                        Log?.Invoke(this, "攻撃");
                        break;
                    default:
                        break;
                }
            }
            catch { }

            Exec = SearchState;
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
                var elms = driver_.FindElements(By.XPath("//*[@class=\"txt-c pb8 pt8\"]/a"));

                if(elms.Count() == 0)
                    elms = driver_.FindElements(By.XPath("//*[@class=\"ml8 fc-white f14 txt-c\"]/div/a"));

                AddEnemyId(driver_.Url);
                driver_.Navigate().GoToUrl(elms.ElementAt(2).GetAttribute("href"));
            }
            catch { }

            IsCombo = false;
        }

        /// <summary>
        /// 撤退確認画面
        /// </summary
        private void ConfirmWithdrawal()
        {
            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//input[@value=\"撤退する\"]"));
                elm.Submit();
            }
            catch { }

            Exec = SearchState;
        }

        /// <summary>
        /// 撤退完了画面
        /// </summary
        private void CompleteWithdrawal()
        {
            try
            {
                driver_.Navigate().GoToUrl(home_path_);
            }
            catch { }

            Exec = SearchState;
        }

        /// <summary>
        /// 出撃確認画面
        /// </summary
        private void ConfirmSally()
        {
            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"出撃する\"]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
            }
            catch { }

            Exec = SearchState;
        }
    }
}
