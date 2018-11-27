using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace gcard_macro
{
    class Raid : Event
    {
        override public event StateChangedHandler StateChanged;
        override public event MinicapChangedHandler MinicapChanged;
        override public event LogHandler Log;

        public bool JoinAssault { get; set; }
        public bool UseAssaultBE { get; set; }
        public bool Request { get; set; }
        public bool AimMVP { get; set; }
        public bool OnlyAttackAssultBoss { get; set; }
        public bool OnlyAttackAssultEnemy { get; set; }
        public double WaitRecieveAssult { get; set; }
        public double WaitAtackBattleShip { get; set; }

        private bool AssaultOperations { get; set; }
        private bool AssaultOperationsRequest { get; set; }
        private bool AssaultOperationsRequest2 { get; set; }
        private bool FoundAssaultOperations { get; set; }
        private bool FoundAssaultOperationsReword { get; set; }
        private System.Threading.Thread WatchThread { get; set; }
        private bool OneAttack { get; set; }
        private bool IsRareBoss { get; set; }
        private bool IsAssaultBEEnded { get; set; }
        private bool IsBattleShip { get; set; }
        private string AssaultOperationPath { get; set; }
        private bool RecieveAssaultOperationReword { get; set; }
        private List<string> MVPEnemyId { get; set; }

        public Raid(IWebDriver driver, string home_path) : base(driver, home_path)
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
            JoinAssault = false;
            UseAssaultBE = false;
            Request = false;
            AimMVP = false;
            OnlyAttackAssultBoss = false;
            OnlyAttackAssultEnemy = false;
            BaseDamage = 0;
            OneAttack = false;
            IsRareBoss = false;
            IsAssaultBEEnded = false;
            WaitRecieveAssult = 0.0;
            WaitAtackBattleShip = 0.0;
            IsBattleShip = false;
            AssaultOperations = false;
            AssaultOperationsRequest = false;
            AssaultOperationsRequest2 = false;
            AssaultOperationPath = "";
            FoundAssaultOperations = false;
            FoundAssaultOperationsReword = false;
            RecieveAssaultOperationReword = false;
            MVPEnemyId = new List<string>();

            base.StateChanged += StateChangedBase;
            base.MinicapChanged += MiniCapChangedBase;
            base.Log += OnLogBase;
        }

        override public void CreateThread()
        {
            base.CreateThread();
            if (JoinAssault)
            {
                WatchThread = new System.Threading.Thread(WatchAssultOperationThread);
                WatchThread.Start();
            }
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
                //強襲の報酬を発見したら
                else if (FoundAssaultOperationsReword)
                {
                    driver_.Navigate().GoToUrl(home_path_ + "_assault_operation");
                    Log?.Invoke(this, "ページ移動：強襲作戦参TOP画面");
                    CurrentState = State.AssaultOperationRequestSubmit;
                    Wait(WaitMisc);
                    Exec = MoveAssaultOperationRequestToAssaultOperationRequestSubmit;
                }
                //強襲作戦に参加していたら
                else if (!AssaultOperations && FoundAssaultOperations)
                {
                    driver_.Navigate().GoToUrl(home_path_);
                    FoundAssaultOperations = false;
                    AssaultOperations = true;
                    IsAssaultBEEnded = false;
                    AssaultOperationsRequest = false;
                    AssaultOperationsRequest2 = false;
                }
                //イベントホーム
                else if (IsHome())
                {
                    if (JoinAssault && IsAssaultOperationInHome())
                    {
                        if (CurrentState != State.AssaultOperationHome)
                            Log?.Invoke(this, "ページ移動：強襲作戦移動確認画面");
                        Wait(WaitMisc);
                        Exec = MoveHomeToAssaultOperationHome;
                    }

                    Log?.Invoke(this, "ページ移動：イベントホーム画面");
                    CurrentState = State.Home;
                    Wait(WaitMisc);
                    Exec = MoveEventHomeToSearch;
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
                //応援依頼完了
                else if (IsRequestComplete())
                {
                    Log?.Invoke(this, "ページ移動：応援依頼完了画面");
                    CurrentState = State.RequestComplete;
                    Wait(WaitMisc);
                    Exec = MoveRequestCompleteToBattle;
                }
                //レベルアップ
                else if (IsLevelUp())
                {
                    Log?.Invoke(this, "ページ移動：レベルアップ画面");
                    CurrentState = State.LevelUp;
                    Wait(WaitMisc);
                    Exec = MoveLevelUpToSearch;
                }
                //戦闘(強襲作戦)
                else if (IsBattleAssaultOperation())
                {
                    Log?.Invoke(this, "ページ移動：強襲作戦戦闘画面");
                    CurrentState = State.BattleAssaultOperation;
                    Exec = AssaultOperationBattle;
                }
                //戦闘
                else if (IsBattle())
                {
                    if (JoinAssault && IsAssaultOperationRequestInBattle())
                    {
                        Log?.Invoke(this, "ページ移動：強襲作戦参加依頼画面");
                        CurrentState = State.AssaultOperationRequest;
                        Wait(WaitMisc);
                        Exec = MoveBattleToAssaultOperationRequest;
                    }

                    Log?.Invoke(this, "ページ移動：戦闘画面");
                    CurrentState = State.Battle;
                    Exec = Battle;
                }
                //敵一覧
                else if (IsEnemyList())
                {
                    //強襲作戦依頼
                    if (JoinAssault && IsAssaultOperationRequest())
                    {
                        Log?.Invoke(this, "ページ移動：強襲作戦参加依頼画面");
                        CurrentState = State.AssaultOperationRequest;
                        Wait(WaitMisc);
                        Exec = MoveEnemyListToAssaultOperationRequest;
                    }
                    else
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
                //強襲作戦ホーム
                else if (IsAssaultOperationHome())
                {
                    if (CurrentState != State.AssaultOperationHome)
                        Log?.Invoke(this, "ページ移動：強襲作戦ホーム画面");
                    CurrentState = State.AssaultOperationHome;
                    Wait(WaitMisc);
                    Exec = MoveAssaultOperationHomeToAssaultOperationBattle;
                }
                //強襲作戦参加
                else if (IsAssaultOperationRequestSubmit())
                {
                    Log?.Invoke(this, "ページ移動：強襲作戦参TOP画面");
                    CurrentState = State.AssaultOperationRequestSubmit;
                    Wait(WaitMisc);
                    Exec = MoveAssaultOperationRequestToAssaultOperationRequestSubmit;
                }
                //強襲作戦開始
                else if (IsAssaultOperationStarted())
                {
                    Log?.Invoke(this, "ページ移動：強襲作戦参加開始画面");
                    CurrentState = State.AssaultOperationRequestSubmit;
                    Wait(WaitMisc);
                    Exec = MoveAssaultOperationRequestSubmitToAssaultOperationStarted;
                }
                //強襲作戦参加依頼完了
                else if (IsAssaultOperationRequestComplete())
                {
                    Log?.Invoke(this, "ページ移動：強襲作戦参加依頼完了画面");
                    CurrentState = State.AssaultOperationRequestComplete;
                    Wait(WaitMisc);
                    Exec = MoveAssaultOperationRequestCompleteToAssaultOperationHome;
                }
                //強襲作戦未受取報酬一覧
                else if (IsAssaultOperationPresentList())
                {
                    Log?.Invoke(this, "ページ移動：強襲作戦未受取報酬一覧画面");
                    CurrentState = State.AssaultOperationRequestComplete;
                    Wait(WaitMisc);
                    Exec = MoveAssaultOperationPresentListToReceive;
                }
                //強襲作戦成功
                else if (IsAssaultOperationWin())
                {
                    if (CurrentState != State.AssaultOperationWin)
                        Log?.Invoke(this, "ページ移動：強襲作戦成功画面");
                    CurrentState = State.AssaultOperationWin;
                    AssaultOperations = false;
                    AssaultOperationsRequest = false;
                    AssaultOperationsRequest2 = false;
                    Wait(WaitMisc);
                    driver_.Navigate().GoToUrl(home_path_);
                }
                //カード入手
                else if (IsGetCard())
                {
                    if (CurrentState != State.GetCard)
                        Log?.Invoke(this, "ページ移動：カード入手画面");
                    CurrentState = State.GetCard;
                    Wait(WaitMisc);
                    Exec = MoveGetCardToSearch;
                }
                //ボス
                else if (IsBoss())
                {
                    CurrentState = State.Boss;
                    Wait(WaitBattle);
                    Exec = MoveBossToBattle;
                }
                //既に戦闘は終了しています
                else if (IsFightAlreadyFinished())
                {
                    Log?.Invoke(this, "ページ移動：戦闘終了済み通知画面");
                    CurrentState = State.FightAlreadyFinished;
                    Wait(WaitMisc);

                    if (!AssaultOperations)
                    {
                        driver_.Navigate().GoToUrl(enemy_list_path_);
                    }
                    else
                    {
                        driver_.Navigate().GoToUrl(AssaultOperationPath);
                    }
                    Attacked = false;
                }
                //強襲作戦参加依頼失敗
                else if (IsFaildJoinRequestAssaultOperationt())
                {
                    Log?.Invoke(this, "ページ移動：強襲作戦参加依頼失敗通知画面");
                    CurrentState = State.AssaultOperationFaildRequestJoin;
                    Wait(WaitMisc);
                    Exec = MoveFaildJoinRequestAssaultOperation;
                }
                //強襲作戦に参加していません
                else if (IsNotJoinedAssaultOperationt())
                {
                    Log?.Invoke(this, "ページ移動：強襲作戦未参加通知画面");
                    CurrentState = State.FightAlreadyFinished;
                    Wait(WaitMisc);
                    driver_.Navigate().GoToUrl(home_path_);
                }
                //受信クエスト一覧
                else if (IsRecievedQuestlist())
                {
                    Wait(WaitMisc);
                    Exec = MoveQuestListToEnemyList;                    
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
        /// 参加している強襲作戦を監視
        /// </summary>
        private void WatchAssultOperationThread()
        {
            while (IsRun)
            {
                try
                {
                    if (!AssaultOperations)
                    {
                        System.Net.WebClient wc = GetWebClient();
                        wc.Encoding = Encoding.UTF8;
                        string source = wc.DownloadString(home_path_ + "_assault_operation");

                        FoundAssaultOperations = source.IndexOf("参加中の作戦へ戻る") > -1;
                        FoundAssaultOperationsReword = source.IndexOf("未受取報酬を受け取る") > -1;
                    }
                }
                catch
                {
                    Wait(2.5);
                }
                Wait(10);
            }
        }


        /// <summary>
        /// 戦闘画面(強襲作戦)判定
        /// </summary>
        /// <returns></returns>
        private bool IsBattleAssaultOperation() => driver_.PageSource.IndexOf("バトルエネルギー") >= 0 && AssaultOperations;



        /// <summary>
        /// 敵一覧画面(強襲作戦依頼)判定
        /// </summary>
        /// <returns></returns>
        private bool IsAssaultOperationRequest() => driver_.PageSource.IndexOf("敵一覧") >= 0 && (driver_.PageSource.IndexOf("作戦参加依頼") >= 0 || driver_.PageSource.IndexOf("参加中の作戦へ戻る") >= 0);

        /// <summary>
        /// ホーム画面に参加中の強襲作戦が出ているかどうか判定
        /// </summary>
        /// <returns></returns>
        private bool IsAssaultOperationInHome() => driver_.Url == home_path_ && driver_.PageSource.IndexOf("参加中の作戦へ戻る") >= 0 || driver_.Url == home_path_ && driver_.PageSource.IndexOf("依頼を確認する") >= 0;

        /// <summary>
        /// 戦闘画面に強襲作戦参加依頼が出ているかどうか判定
        /// </summary>
        /// <returns></returns>
        private bool IsAssaultOperationRequestInBattle() => driver_.PageSource.IndexOf("バトルエネルギー") >= 0 && driver_.PageSource.IndexOf("撃破報酬をチェック") >= 0 && driver_.PageSource.IndexOf("作戦参加依頼が届いています") >= 0;

        /// <summary>
        /// 強襲作戦参加画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsAssaultOperationRequestSubmit() => driver_.PageSource.IndexOf("強襲作戦に参加せず") >= 0 || (driver_.PageSource.IndexOf("作戦参加") >= 0 && driver_.PageSource.IndexOf("参加依頼を断る") >= 0);

        /// <summary>
        /// 強襲作戦開始画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsAssaultOperationStarted() => driver_.PageSource.IndexOf("強襲作戦開始") >= 0;

        /// <summary>
        /// 強襲作戦ホーム画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsAssaultOperationHome() => driver_.PageSource.IndexOf("防衛網突破を目指せ") >= 0 || driver_.PageSource.IndexOf(" 突破状態を維持しつつ､目標を撃破せよ") >= 0 || driver_.PageSource.IndexOf("継続チャレンジ") >= 0;

        /// <summary>
        /// 強襲作戦作戦成功画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsAssaultOperationWin() => driver_.PageSource.IndexOf("作戦成功") >= 0 && driver_.PageSource.IndexOf("強襲作戦") >= 0;

        /// <summary>
        /// 強襲作戦参加依頼完了画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsAssaultOperationRequestComplete() => driver_.PageSource.IndexOf("作戦参加依頼完了") >= 0 && driver_.PageSource.IndexOf("強襲作戦") >= 0;

        /// <summary>
        /// 強襲作戦報酬一覧画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsAssaultOperationPresentList() => driver_.PageSource.IndexOf("強襲作戦未受取報酬一覧") >= 0;

        /// <summary>
        /// 強襲作戦未受取報酬一覧画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsAssaultOperationReceive() => driver_.PageSource.IndexOf("強襲作戦未受取報酬一覧") >= 0 && driver_.PageSource.IndexOf("報酬を受け取る") >= 0;

        /// <summary>
        /// 強襲作戦に参加していません画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsNotJoinedAssaultOperationt() => driver_.PageSource.IndexOf("強襲作戦に参加していません") >= 0;

        /// <summary>
        /// 強襲作戦参加依頼失敗画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsFaildJoinRequestAssaultOperationt() => driver_.PageSource.IndexOf("作戦参加依頼を送信できません") >= 0;

        /// <summary>
        /// 受信クエスト一覧画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsRecievedQuestlist() => driver_.PageSource.IndexOf("連携クエストに挑戦しました") >= 0;


        /// <summary>
        /// イベントのホームから探索へ
        /// </summary>
        private void MoveEventHomeToSearch()
        {
            Exec = SearchState;

            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"結果を確認する\"]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                return;
            }
            catch { }

            if (JoinAssault)
            {
                try
                {
                    IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"参加中の作戦へ戻る\"]"));
                    driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                    return;
                }
                catch { }
            }

            try
            {
                if(enemy_list_path_.IndexOf("raid_boss") >= 0)
                {
                    driver_.Navigate().GoToUrl(enemy_list_path_);
                    return;
                }

                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"強襲作戦TOP\"]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
            }
            catch { }


        }


        /// <summary>
        /// 敵一覧から戦闘へ
        /// </summary>
        private void MoveEnemyListToSearch()
        {
            enemy_list_path_ = driver_.Url;

            IsRareBoss = false;
            AssaultOperations = false;

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
                        Exec = SearchState;
                        return;
                    }
                }
                catch
                { }
            }


            //探索のみ
            if (!OnlySearch)
            {
                //コンボ数が多い順に狙う
                int idx = -1;
                Exec = SearchState;

                //レアボス相手にMVPを狙う
                if (AimMVP)
                {
                    foreach (var enemy in enemys)
                    {
                        //レアボスなら攻撃する
                        if (enemy.Item2.Text.IndexOf("ﾚｱﾎﾞｽ") >= 0)
                        {
                            string url = enemy.Item1.GetAttribute("href");
                            if (!IsGotMVP(url) && !IsAttacked(url))
                            {
                                Log?.Invoke(this, "攻撃： " + enemy.Item2.Text);
                                driver_.Navigate().GoToUrl(url);
                                Exec = SearchState;
                                IsRareBoss = true;
                                return;
                            }
                        }
                    }
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
                            string token = elm.GetAttribute("data-mission-path");
                            if (token != "" && token != null)
                                url = "http://gcc.sp.mbga.jp/" + elm.GetAttribute("data-mission-path");
                        }

                        if (url != null)
                        {
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
        /// イベントホームから強襲作戦ホームへ
        /// </summary>
        private void MoveHomeToAssaultOperationHome()
        {
            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"依頼を確認する\"]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                Exec = SearchState;
                return;
            }
            catch { }


            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"参加中の作戦へ戻る\"]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                AssaultOperations = true;
                Exec = SearchState;
                return;
            }
            catch { }
        }

        /// <summary>
        /// 戦闘結果から敵一覧へ
        /// </summary>
        override protected void MoveResultToEnemyList()
        {
            Exec = SearchState;


            if (RecieveAssaultOperationReword)
            {
                try
                {
                    var elms = driver_.FindElements(By.XPath("//input[@value=\"報酬を受け取る\"]/.."));
                    foreach (var e in elms)
                    {
                        e.Submit();
                        RecieveAssaultOperationReword = false;
                        return;
                    }
                }
                catch { }
            }

            try
            {
                //var elms = driver_.FindElements(By.XPath("//input[@value=\"報酬を受け取る\"]");
                var elms = driver_.FindElements(By.XPath("//a[text()=\"敵一覧\"]"));
                foreach (var e in elms)
                {
                    //e.Submit();
                    driver_.Navigate().GoToUrl(e.GetAttribute("href"));
                    return;
                }
            }
            catch { }

            try
            {
                //var elms = driver_.FindElements(By.XPath("//input[@value=\"報酬を受け取る\"]");
                var elms = driver_.FindElements(By.XPath("//a[text()=\"強襲作戦敵一覧\"]"));
                foreach (var e in elms)
                {
                    //e.Submit();
                    driver_.Navigate().GoToUrl(e.GetAttribute("href"));
                    return;
                }
            }
            catch { }


            Exec = SearchState;
        }

        /// <summary>
        /// 受信クエスト一覧から敵一覧へ
        /// </summary>
        private void MoveQuestListToEnemyList()
        {
            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"敵一覧\"]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
            }
            catch { }

            Exec = SearchState;
        }

        /// <summary>
        /// 戦闘画面から強襲作戦参加依頼画面へ
        /// </summary>
        private void MoveBattleToAssaultOperationRequest()
        {
            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"依頼確認\"]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
            }
            catch { }

            Exec = SearchState;
        }

        /// <summary>
        /// 敵一覧から強襲作戦依頼へ
        /// </summary>
        private void MoveEnemyListToAssaultOperationRequest()
        {
            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"依頼確認\" or text()=\"参加中の作戦へ戻る\"]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
            }
            catch { }

            Exec = SearchState;
        }


        /// <summary>
        /// 強襲作戦参加から強襲作戦参加同意へ
        /// </summary>
        private void MoveAssaultOperationRequestToAssaultOperationRequestSubmit()
        {
            try
            {
                FoundAssaultOperationsReword = false;
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"未受取報酬を受け取る\"]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                RecieveAssaultOperationReword = true;
                Exec = SearchState;
                return;
            }
            catch { }


            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"作戦参加\" and @href]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                Exec = SearchState;
                return;
            }
            catch { }

            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"敵一覧へ\"]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                Exec = SearchState;
                return;
            }
            catch { }

            Exec = SearchState;
        }



        /// <summary>
        /// 強襲作戦参加同意から強襲作戦開始へ
        /// </summary>
        private void MoveAssaultOperationRequestSubmitToAssaultOperationStarted()
        {
            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"強襲作戦敵一覧へ\"]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                AssaultOperations = true;
                IsAssaultBEEnded = false;
                AssaultOperationsRequest = false;
                AssaultOperationsRequest2 = false;
            }
            catch { }

            Exec = SearchState;
        }

        /// <summary>
        /// 作戦参加依頼完了から強襲作戦開始へ
        /// </summary>
        private void MoveAssaultOperationRequestCompleteToAssaultOperationHome()
        {
            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"強襲作戦敵一覧\"]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                AssaultOperations = true;
                IsAssaultBEEnded = false;
            }
            catch { }

            Exec = SearchState;
        }

        /// <summary>
        /// 強襲作戦参加完了の報酬受け取り
        /// </summary>
        private void MoveAssaultOperationPresentListToReceive()
        {
            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"報酬をまとめて受け取る\" or text()=\"強襲作戦未受取報酬一覧\" or text()=\"報酬を受け取る\"]"));
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
        /// 強襲作戦参加依頼失敗画面
        /// </summary>
        private void MoveFaildJoinRequestAssaultOperation()
        {
            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"強襲作戦敵一覧\"]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
            }
            catch { }

            Exec = SearchState;
        }


        /// <summary>
        /// 強襲作戦ホームから強襲作戦戦闘へ
        /// </summary>
        private void MoveAssaultOperationHomeToAssaultOperationBattle()
        {
            AssaultOperationPath = driver_.Url;
            IsBattleShip = false;

            if (Request)
            {
                if (!AssaultOperationsRequest || !AssaultOperationsRequest2)
                {
                    try
                    {
                        IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"戦友に参加依頼\"]"));
                        driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                        AssaultOperations = true;
                        if (AssaultOperationsRequest) AssaultOperationsRequest2 = true;
                        AssaultOperationsRequest = true;
                    }
                    catch { }

                    Exec = SearchState;
                    return;
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
                        Wait(WaitRecieveAssult);
                        driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                        Exec = SearchState;
                        return;
                    }
                }
                catch
                { }
            }


            if (IsAssaultBEEnded && UseAssaultBE)
            {
                Wait(3);
                driver_.Navigate().Refresh();
                Exec = SearchState;
                return;
            }

            if (!OnlyAttackAssultEnemy)
            {
                try
                {
                    IWebElement elm = driver_.FindElement(By.XPath("//a[@class=\"btn-attack_not\"]"));
                    driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                    IsBattleShip = true;
                    Exec = SearchState;
                    return;
                }
                catch { }
            }

            if (!OnlyAttackAssultBoss)
            {
                try
                {
                    var elms = driver_.FindElements(By.XPath("//div[@class=\"boss-wrap\"]/a[@href]"));
                    driver_.Navigate().GoToUrl(elms[5].GetAttribute("href"));
                    AssaultOperations = true;
                }
                catch { }
            }
            else
            {
                Wait(3);
                driver_.Navigate().Refresh();
                Exec = SearchState;
                return;
            }

            Exec = SearchState;
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


            ulong hp = 0;
            try
            {
                //敵HPを取得
                IWebElement elm = driver_.FindElement(By.XPath("//div[@class=\"raid_boss_summary_para\"]/div[@class=\"flex wb\"]"));
                string strhp = elm.Text;
                string current_hp = strhp.Replace(",", "").Split(new char[] { '/' })[0];
                hp = ulong.Parse(current_hp);

                ulong myDamage = 0;
                try
                {
                    //自分が与えたダメージを取得
                    myDamage = ulong.Parse(driver_.FindElement(By.XPath("//div[contains(text(),\"の総ダメージ\")]/strong")).Text.Replace(",", ""));
                }
                catch
                {
                    try
                    {
                        var elms = driver_.FindElements(By.XPath("//div[contains(text(),\"現在のMVP候補\")]/../div/div/p"));
                        myDamage = ulong.Parse(elms[1].Text.Split(new char[] { ':' })[1].Replace(",", ""));
                    }
                    catch { }
                }

                if (AimMVP && IsRareBoss)
                {
                    if (hp < myDamage * 2)
                    {
                        AddMVPId(driver_.Url);
                        AddEnemyId(driver_.Url);
                        driver_.Navigate().GoToUrl(enemy_list_path_);
                        return;
                    }
                    else
                    {
                        RemoveEnemyId(driver_.Url);
                    }
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
                var elms = driver_.FindElements(By.XPath("//*[@class=\"flex w100p\"]/a"));

                //コンボ数取得
                double combo = 1;

                try
                {
                    IWebElement elm = driver_.FindElement(By.XPath("//div[@id=\"attc\"]/strong"));
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

                    if (!AimMVP || !IsRareBoss)
                    {
                        AddEnemyId(driver_.Url);
                    }

                    Log?.Invoke(this, string.Format("BEx{0}使用", useBe + 1));
                    driver_.Navigate().Refresh();
                    Attacked = true;
                }
            }
            catch { }

            IsCombo = false;
        }

        /// <summary>
        /// 強襲作戦戦闘画面
        /// </summary>
        private void AssaultOperationBattle()
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

            //強襲作戦大ボスが出ていれば移動する
            if (!OnlyAttackAssultEnemy)
            {
                try
                {
                    var assultBtn = driver_.FindElements(By.XPath("//div[@class=\"assault-btn\"]/a"));
                    driver_.Navigate().GoToUrl(assultBtn.ElementAt(1).GetAttribute("href"));
                    IsBattleShip = true;
                    return;
                }
                catch { }
            }

            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//p[contains(text(), \"強襲作戦専用BE\")]/span"));

                if (UseAssaultBE && Convert.ToInt32(elm.Text) == 0)
                {
                    driver_.Navigate().GoToUrl(AssaultOperationPath);
                    IsAssaultBEEnded = true;
                    Exec = SearchState;
                    return;
                }
            }
            catch
            {
                if (UseAssaultBE)
                {
                    driver_.Navigate().GoToUrl(AssaultOperationPath);
                    IsAssaultBEEnded = true;
                    Exec = SearchState;
                    return;
                }
            }

            string name = "";
            try
            {
                //敵の名前を取得
                name = driver_.FindElement(By.XPath("//div[@id=\"boss_name\"]")).Text;
            }
            catch { }


            if (Request)
            {
                try
                {
                    //応援依頼が出ていれば依頼する
                    IWebElement elm = driver_.FindElement(By.XPath("//form[contains(@action, \"request\")]"));
                    elm.Submit();
                    Exec = SearchState;
                    return;
                }
                catch { }
            }


            try
            {
                //敵HPを取得
                IWebElement elm = null;
                try
                {
                    elm = driver_.FindElement(By.XPath("//div[@class=\"raid_boss_summary_para\" or @class=\"raid_boss_summary_para bac\"]"));
                }
                catch
                {
                    try
                    {
                        elm = driver_.FindElement(By.XPath("//span[@class=\"now_hp\"]"));
                    }
                    catch { }
                }
                string strhp = elm.Text;
                string current_hp = strhp.Replace(",", "").Split(new char[] { '/' })[0];
                UInt64 hp = UInt64.Parse(current_hp);


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
                    if (IsBattleShip)
                        Wait(WaitAtackBattleShip);

                    if (name != "")
                        Log?.Invoke(this, "攻撃： " + name);
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
                        driver_.Navigate().GoToUrl(AssaultOperationPath);
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

                    Log?.Invoke(this, string.Format("BEx{0}使用", useBe + 1));
                    driver_.Navigate().Refresh();
                    Attacked = true;
                }
            }
            catch { }

            IsCombo = false;
        }

        /// <summary>
        /// 戦闘画面のurlから敵IDを追加
        /// </summary>
        /// <param name="url">戦闘画面のurl</param>
        private void AddMVPId(string url) => MVPEnemyId.Add(GetEnemyId(url));

        /// <summary>
        /// 戦闘画面のurlの敵IDが攻撃済みか判定
        /// </summary>
        /// <param name="url">戦闘画面のurl</param>
        /// <returns>攻撃済み</returns>
        private bool IsGotMVP(string url) => MVPEnemyId.LastIndexOf(GetEnemyId(url)) >= 0;

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
