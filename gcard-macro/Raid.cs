using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace gcard_macro
{
    class Raid : Event
    {
        public bool JoinAssault { get; set; }
        public bool UseAssaultBE { get; set; }
        public bool Request { get; set; }
        private bool AssaultOperations { get; set; }
        private bool AssaultOperationsRequest { get; set; }
        private bool OneAttack { get; set; }


        public Raid(ChromeDriver driver, string home_path): base(driver, home_path)
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
            BaseDamage = 0;
        }



        override protected void SearchState()
        {
            try
            {
                //イベントホーム
                if (IsHome())
                {
                    if (!IsAssaultOperationInHome())
                    {
                        CurrentState = State.Home;
                        Wait(WaitSearch);
                        Exec = MoveEventHomeToSearch;
                    }
                    else
                    {
                        CurrentState = State.AssaultOperationHome;
                        Wait(WaitSearch);
                        Exec = MoveHomeToAssaultOperationHome;
                    }
                    return;
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
                //応援依頼完了
                else if (IsRequestComplete())
                {
                    CurrentState = State.RequestComplete;
                    Wait(WaitMisc);
                    Exec = MoveRequestCompleteToBattle;
                }
                //レベルアップ
                else if (IsLevelUp())
                {
                    CurrentState = State.LevelUp;
                    Wait(WaitMisc);
                    Exec = MoveLevelUpToSearch;
                }
                //戦闘(強襲作戦)
                else if (IsBattleAssaultOperation())
                {
                    CurrentState = State.BattleAssaultOperation;
                    Wait(WaitAttack);
                    Exec = AssaultOperationBattle;
                }
                //戦闘
                else if (IsBattle())
                {
                    if (!IsAssaultOperationRequestInBattle())
                    {
                        CurrentState = State.Battle;
                        Wait(WaitAttack);
                        Exec = Battle;
                    }
                    else
                    {
                        CurrentState = State.AssaultOperationRequest;
                        Wait(WaitMisc);
                        Exec = MoveBattleToAssaultOperationRequest;
                    }
                }
                //敵一覧
                else if (IsEnemyList())
                {
                    //強襲作戦依頼
                    if (JoinAssault && IsAssaultOperationRequest())
                    {
                        CurrentState = State.AssaultOperationRequest;
                        Wait(WaitMisc);
                        Exec = MoveEnemyListToAssaultOperationRequest;
                    }
                    //敵出現
                    else if (IsEnemyAppearance())
                    {
                        CurrentState = State.EnemyAppearance;
                        Wait(WaitBattle);
                        Exec = MoveEnemyAppearanceToBattle;
                    }
                    else
                    {
                        CurrentState = State.EnemyList;
                        Wait(WaitSearch);
                        Exec = MoveEnemyListToSearch;
                    }
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
                //強襲作戦ホーム
                else if (IsAssaultOperationHome())
                {
                    CurrentState = State.AssaultOperationHome;
                    Wait(WaitMisc);
                    Exec = MoveAssaultOperationHomeToAssaultOperationBattle;
                }
                //強襲作戦参加
                else if (IsAssaultOperationRequestSubmit())
                {
                    CurrentState = State.AssaultOperationRequestSubmit;
                    Wait(WaitMisc);
                    Exec = MoveAssaultOperationRequestToAssaultOperationRequestSubmit;
                }
                //強襲作戦開始
                else if (IsAssaultOperationStarted())
                {
                    CurrentState = State.AssaultOperationRequestSubmit;
                    Wait(WaitMisc);
                    Exec = MoveAssaultOperationRequestSubmitToAssaultOperationStarted;
                }
                //強襲作戦参加依頼完了
                else if (IsAssaultOperationRequestComplete())
                {
                    CurrentState = State.AssaultOperationRequestComplete;
                    Wait(WaitMisc);
                    Exec = MoveAssaultOperationRequestCompleteToAssaultOperationHome;
                }
                //強襲作戦成功
                else if (IsAssaultOperationWin())
                {
                    CurrentState = State.AssaultOperationWin;
                    AssaultOperations = false;
                    AssaultOperationsRequest = false;
                    Wait(WaitMisc);
                    driver_.Navigate().GoToUrl(home_path_);
                }
                //強襲作戦未受取報酬一覧
                else if (IsAssaultOperationPresentList()){
                    CurrentState = State.AssaultOperationRequestComplete;
                    Wait(WaitMisc);
                    Exec = MoveAssaultOperationPresentListToReceive;
                }
                //カード入手
                else if (IsGetCard())
                {
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
                    CurrentState = State.FightAlreadyFinished;
                    Wait(WaitMisc);
                    driver_.Navigate().GoToUrl(home_path_);
                    Attacked = false;
                }
                //強襲作戦に参加していません
                else if (IsNotJoinedAssaultOperationt())
                {
                    CurrentState = State.FightAlreadyFinished;
                    Wait(WaitMisc);
                    driver_.Navigate().GoToUrl(home_path_);
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
        }
        

        /// <summary>
        /// 戦闘画面(強襲作戦)判定
        /// </summary>
        /// <returns></returns>
        private bool IsBattleAssaultOperation() => driver_.PageSource.IndexOf("バトルエネルギー") >= 0 && driver_.PageSource.IndexOf("強襲作戦専用BE") >= 0;



        /// <summary>
        /// 敵一覧画面(強襲作戦依頼)判定
        /// </summary>
        /// <returns></returns>
        private bool IsAssaultOperationRequest() => driver_.PageSource.IndexOf("敵一覧") >= 0 && driver_.PageSource.IndexOf("作戦参加依頼") >= 0;

        /// <summary>
        /// ホーム画面に参加中の強襲作戦が出ているかどうか判定
        /// </summary>
        /// <returns></returns>
        private bool IsAssaultOperationInHome() => driver_.Url == home_path_ && driver_.PageSource.IndexOf("参加中の作戦へ戻る") >= 0;

        /// <summary>
        /// 戦闘画面に強襲作戦参加依頼が出ているかどうか判定
        /// </summary>
        /// <returns></returns>
        private bool IsAssaultOperationRequestInBattle() => driver_.PageSource.IndexOf("バトルエネルギー") >= 0 && driver_.PageSource.IndexOf("撃破報酬をチェック") >= 0 && driver_.PageSource.IndexOf("作戦参加依頼が届いています") >= 0;

        /// <summary>
        /// 強襲作戦参加画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsAssaultOperationRequestSubmit() => driver_.PageSource.IndexOf("強襲作戦に参加せず") >= 0 ||(driver_.PageSource.IndexOf("作戦参加") >= 0 && driver_.PageSource.IndexOf("参加依頼を断る") >= 0);

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
        /// 強襲作戦未受取報酬一覧画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsAssaultOperationPresentList() => driver_.PageSource.IndexOf("強襲作戦未受取報酬一覧") >= 0;

        
        /// <summary>
        /// 応援依頼完了画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsRequestComplete() => driver_.PageSource.IndexOf("応援依頼完了") >= 0;

        /// <summary>
        /// 強襲作戦に参加していません画面判定
        /// </summary>
        /// <returns></returns>
        private bool IsNotJoinedAssaultOperationt() => driver_.PageSource.IndexOf("強襲作戦に参加していません") >= 0 || driver_.PageSource.IndexOf("作戦参加依頼を送信できません") >= 0;
        
    

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
        /// 敵一覧から探索へ
        /// </summary>
        private void MoveEnemyListToSearch()
        {
            try
            {
                IWebElement elm = driver_.FindElementByXPath("//a[text()=\"敵を見つける\"]");
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
            }
            catch
            {
                try
                {
                    IWebElement elm = driver_.FindElementByXPath("//a[text()=\"報酬をまとめて受け取る\"]");
                    driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                }
                catch
                { }
            }

            Exec = SearchState;
        }

        /// <summary>
        /// 敵一覧から戦闘へ
        /// </summary>
        private void MoveEnemyAppearanceToBattle()
        {
            //コンボ数が多い順に狙う
            int idx = -1;
            Exec = SearchState;
            try
            {
                var elms = driver_.FindElementsByXPath("//a[@class=\"btn-raidboss-attack chance\"]");
                if (elms.Count > 0)
                {
                    var combo = elms.Select(e => Convert.ToInt32(e.FindElement(By.XPath("//a[@class=\"btn-raidboss-attack chance\"]/../dl[@class=\"raidboss-combo\"]//span")).Text)).ToList();
                    idx = combo.IndexOf(combo.Max());
                    driver_.Navigate().GoToUrl(elms[idx].GetAttribute("href"));
                    Exec = SearchState;
                    OneAttack = true;
                    return;
                }
            }
            catch { }

            try
            {
                var elms = driver_.FindElementsByXPath("//a[@class=\"btn-raidboss-attack not\"]");
                if (elms.Count > 0)
                {
                    var combo = elms.Select(e => Convert.ToInt32(e.FindElement(By.XPath("//a[@class=\"btn-raidboss-attack not\"]/../dl[@class=\"raidboss-combo\"]//span")).Text)).ToList();
                    idx = combo.IndexOf(combo.Max());
                    driver_.Navigate().GoToUrl(elms[idx].GetAttribute("href"));
                    Exec = SearchState;
                    OneAttack = true;
                    return;
                }
            }
            catch { }


            try
            {
                var elms = driver_.FindElementsByXPath("//dl[@class=\"raidboss-combo\"]//span ");

                if(elms.Count == 4)
                {
                    IWebElement elm = driver_.FindElementByXPath("//a[text()=\"戦況を更新\"]");
                    driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                    Exec = SearchState;
                    return;
                }
                else
                {
                    IWebElement elm = driver_.FindElementByXPath("//a[text()=\"敵を見つける\"]");
                    driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                    Exec = SearchState;
                    return;
                }

                //var combo = elms.Select(e => Convert.ToInt32(e.Text)).ToList();
                //idx = combo.IndexOf(combo.Max());
            }
            catch
            {
                Exec = SearchState;
                try
                {
                    driver_.Navigate().GoToUrl(home_path_);
                }
                catch { }
            }
        }

        /// <summary>
        /// イベントホームから強襲作戦ホームへ
        /// </summary>
        private void MoveHomeToAssaultOperationHome()
        {
            try
            {
                IWebElement elm = driver_.FindElementByXPath("//a[text()=\"参加中の作戦へ戻る\"]");
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                AssaultOperations = true;
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
                IWebElement elm = driver_.FindElementByXPath("//a[text()=\"依頼確認\"]");
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
                IWebElement elm = driver_.FindElementByXPath("//a[text()=\"依頼確認\"]");
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
                IWebElement elm = driver_.FindElementByXPath("//a[text()=\"作戦参加\" and @href]");
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                Exec = SearchState;
                return;
            }
            catch { }


            try
            {
                IWebElement elm = driver_.FindElementByXPath("//a[text()=\"未受取報酬を受け取る\"]");
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                Exec = SearchState;
                return;
            }
            catch { }

            try
            {
                driver_.Navigate().GoToUrl(home_path_);
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
                IWebElement elm = driver_.FindElementByXPath("//a[text()=\"強襲作戦敵一覧へ\"]");
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                AssaultOperations = true;
                AssaultOperationsRequest = false;
            }
            catch { }

            Exec = SearchState;
        }

        /// <summary>
        /// 強襲作戦参加完了から強襲作戦開始へ
        /// </summary>
        private void MoveAssaultOperationRequestCompleteToAssaultOperationHome()
        {
            try
            {
                IWebElement elm = driver_.FindElementByXPath("//a[text()=\"強襲作戦敵一覧\"]");
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                AssaultOperations = true;
            }
            catch { }

            Exec = SearchState;
        }

        /// <summary>
        /// 強襲作戦参加完了から強襲作戦開始へ
        /// </summary>
        private void MoveAssaultOperationPresentListToReceive()
        {
            try
            {
                IWebElement elm = driver_.FindElementByXPath("//a[text()=\"報酬をまとめて受け取る\"]");
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
            if (!AssaultOperationsRequest)
            {
                try
                {
                    IWebElement elm = driver_.FindElementByXPath("//a[text()=\"戦友に参加依頼\"]");
                    driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                    AssaultOperations = true;
                    AssaultOperationsRequest = true;
                }
                catch { }

                Exec = SearchState;
                return;
            }
            

            try
            {
                IWebElement elm = driver_.FindElementByXPath("//a[@class=\"btn-attack_not\"]");
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                Exec = SearchState;
                return;
            }
            catch { }


            try
            {
                var elms = driver_.FindElementsByXPath("//div[@class=\"boss-wrap\"]/a[@href]");

                foreach (var elm in elms.Reverse())
                {
                    driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                    break;
                }
                AssaultOperations = true;
            }
            catch { }

            Exec = SearchState;
        }


        /// <summary>
        /// 応援依頼完了から戦闘画面 or 探索へ
        /// </summary>
        private void MoveRequestCompleteToBattle()
        {
            try
            {
                driver_.Navigate().GoToUrl(home_path_);
                IWebElement elm = driver_.FindElementByXPath("//a[text()=\"ボス戦に戻る\"]");
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                Exec = SearchState;
                return;
            }
            catch { }

            try
            {
                IWebElement elm = driver_.FindElementByXPath("//a[text()=\"続けて探索する\"]");
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
        /// プレゼント一覧からプレゼント受け取り or 探索へ
        /// </summary>
        override protected void MovePresentListToPresent()
        {            
            try
            {
                IWebElement elm = driver_.FindElementByXPath("//input[@value=\"プレゼントをまとめて受け取る\"]");
                elm.Submit();
                Exec = SearchState;
                return;
            }
            catch { }

            try
            {
                if (AssaultOperations)
                {
                    driver_.Navigate().GoToUrl(home_path_);
                    Exec = SearchState;
                    return;
                }
            }
            catch { }

            try
            {

                IWebElement elm = driver_.FindElementByXPath("//a[text()=\"探索\"]");
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                Exec = SearchState;
            }
            catch { }            
        }


        /// <summary>
        /// 戦闘画面
        /// </summary>
        private void Battle()
        {
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

            if (Attacked)
            {
                Attacked = false;
                driver_.Navigate().GoToUrl(home_path_);
                return;
            }

            try
            {
                IWebElement elm = driver_.FindElementByXPath("//div[@class=\"raid_boss_summary_para\"]/div[@class=\"flex wb\"]");
                string strhp = elm.Text;
                string current_hp = strhp.Replace(",", "").Split(new char[] { '/' })[0];
                UInt64 hp = UInt64.Parse(current_hp);


                var elms = driver_.FindElementsByXPath("//*[@class=\"flex w100p\"]/a");


                
                double combo = 1;

                try
                {
                    elm = driver_.FindElementByXPath("//div[@id=\"attc\"]/strong");
                    combo = Convert.ToDouble(elm.Text);
                }
                catch { }



                int useBe = Utils.CalcUseMiniCapsules(hp, BaseDamage, 1.0, combo);

                if (useBe == 0) useBe = 1;
                else if (useBe > 3) useBe = 3;

                if (useBe > 0)
                {
                    useBe--;
                    driver_.Navigate().GoToUrl(elms.ElementAt(useBe).GetAttribute("href"));
                    Attacked = true;
                }                
            }
            catch { }

            Exec = SearchState;
        }

        /// <summary>
        /// 強襲作戦戦闘画面
        /// </summary>
        private void AssaultOperationBattle()
        {
            if (!UseAssaultBE)
            {
                Battle();
                return;
            }

            int be = 0;
            try
            {
                IWebElement elm = driver_.FindElementByXPath("//p[text()=\"強襲作戦専用BE:\"]/span");
                be = Convert.ToInt32(elm.Text);

                if (be > 3) be = 3;
            }
            catch { }


            try
            {
                IWebElement elm = driver_.FindElementByXPath("//div[@class=\"raid_boss_summary_para\"]/div[@class=\"flex wb\"]");
                string strhp = elm.Text;
                string current_hp = strhp.Replace(",", "").Split(new char[] { '/' })[0];
                UInt64 hp = UInt64.Parse(current_hp);


                var elms = driver_.FindElementsByXPath("//*[@class=\"flex w100p\"]/a");

                if (elms.Count < be) be = elms.Count;


                
                double combo = 1;

                try
                {
                    elm = driver_.FindElementByXPath("//div[@id=\"attc\"]/strong");
                    combo = Convert.ToDouble(elm.Text);
                }
                catch { }

                int useBe = Utils.CalcUseMiniCapsules(hp, BaseDamage, 1.0, combo);

                if (useBe == 0) useBe = 1;
                else if (useBe > 3) useBe = 3;

                useBe = Math.Min(useBe, be);

                if (useBe > 0) {
                    useBe--;
                    driver_.Navigate().GoToUrl(elms.ElementAt(useBe).GetAttribute("href"));
                }
            }
            catch { }
            
            Exec = SearchState;
        }
    }
}
