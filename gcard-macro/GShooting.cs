using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenQA.Selenium;

namespace gcard_macro
{
    class GShooting : Event
    {
        override public event StateChangedHandler StateChanged;
        override public event MinicapChangedHandler MinicapChanged;
        override public event LogHandler Log;

        public bool Request { get; set; }

        public GShooting(IWebDriver driver, string home_path) : base(driver, home_path)
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
                    Log?.Invoke(this, "ページ移動：イベントホーム画面");
                    CurrentState = State.Home;
                    Wait(WaitSearch);
                    Exec = MoveEventHomeToSearch;
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
                    Wait(WaitAttack);
                    Exec = Battle;
                }
                //応援依頼完了
                else if (IsRequestComplete())
                {
                    Log?.Invoke(this, "ページ移動：応援依頼完了画面");
                    CurrentState = State.RequestComplete;
                    Wait(WaitMisc);
                    Exec = MoveRequestCompleteToBattle;
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

            Exec = SearchState;
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
                buttons = driver_.FindElements(By.XPath("//a[contains(@class,\"btn-raidboss-\") and not(contains(@id,\"dummy\"))]")).ToList();
                enemyNames = driver_.FindElements(By.XPath("//p[contains(@class,\"raidboss-name\")]")).Take(buttons.Count()).ToList();

                enemys = buttons.Zip(enemyNames, (b, n) => new Tuple<IWebElement, IWebElement>(b, n)).OrderBy(i => Guid.NewGuid()).ToList();

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
                            var combo = elms.Select(e => Convert.ToInt32(e.FindElement(By.XPath("../dl[@class=\"raidboss-combo\"]/dd/span")).Text)).ToList();

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
                        IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"探索する\"]"));
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
                if (MinicapCount != Convert.ToInt32(elm.Text))
                {
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
                    IWebElement elm = driver_.FindElement(By.XPath("//form[contains(@action, \"request\")]"));
                    elm.Submit();
                    Log?.Invoke(this, "応援依頼");
                    Exec = SearchState;
                    return;
                }
                catch { }
            }


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
                IWebElement elm;
                //敵HPを取得
                try
                {
                    elm = driver_.FindElement(By.XPath("//div[@class=\"raid_boss_summary_para\" or @class=\"raid_boss_summary_para bac\"]/text()"));
                }
                catch
                {
                    elm = driver_.FindElement(By.XPath("//div[@class=\"raid_boss_summary_para\" or @class=\"raid_boss_summary_para bac\"]"));
                }

                string strhp = elm.Text;
                string current_hp = strhp.Replace(",", "").Split(new char[] { '/' })[0];
                ulong hp = ulong.Parse(current_hp);


                var elms = driver_.FindElements(By.XPath("//*[@class=\"flex w100p\"]/a"));

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
                    useBe--;

                    string ret = "";

                    try
                    {
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

                    Log?.Invoke(this, string.Format("BEx{0}使用", useBe));
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
