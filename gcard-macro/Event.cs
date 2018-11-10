using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Net;
using System.IO;

namespace gcard_macro
{
    /// <summary>
    /// 各イベントの基底クラス
    /// </summary>
    public class Event
    {
        /// <summary>
        /// webdriver
        /// </summary>
        virtual protected IWebDriver driver_ { get; set; }

        /// <summary>
        /// イベントのホームのパス
        /// </summary>
        virtual protected string home_path_ { get; set; }

        /// <summary>
        /// 敵一覧画面のパス
        /// </summary>
        virtual protected string enemy_list_path_ { get; set; }

        /// <summary>
        /// ワーカースレッド
        /// </summary>
        virtual protected System.Threading.Thread worker_thread { get; set; }

        /// <summary>
        /// IsRunのロックオブジェクト
        /// </summary>
        virtual protected object RunObj { get; set; }

        /// <summary>
        /// 攻撃済みフラグ
        /// </summary>
        virtual protected bool Attacked { get; set; }

        /// <summary>
        /// コンボ中フラグ
        /// </summary>
        virtual protected bool IsCombo { get; set; }

        /// <summary>
        /// 敵IDのリスト
        /// </summary>
        virtual protected List<string> AttackedEnemyId { get; set; }

        /// <summary>
        /// ミニカプのカウント
        /// </summary>
        virtual protected int MinicapCount { get; set; }

        /// <summary>
        /// 敵発見時刻のリスト
        /// </summary>
        virtual protected List<DateTime> EnemyFoundTime { get; set; }

        /// <summary>
        /// 実行オブジェクト
        /// </summary>
        internal Action Exec;

        /// <summary>
        /// 現在の状態
        /// </summary>
        virtual public State CurrentState { get; protected set; }

        /// <summary>
        /// 攻撃モード
        /// </summary>
        virtual public AttackMode Mode { get; set; }

        /// <summary>
        /// スレッド動作中
        /// </summary>
        virtual public bool IsRun { get; set; }

        /// <summary>
        /// 探索前Wait
        /// </summary>
        virtual public double WaitSearch { get; set; }

        /// <summary>
        /// 戦闘前Wait
        /// </summary>
        virtual public double WaitBattle { get; set; }

        /// <summary>
        /// 攻撃前Wait
        /// </summary>
        virtual public double WaitAttack { get; set; }

        /// <summary>
        /// 報酬受け取り前Wait
        /// </summary>
        virtual public double WaitReceive { get; set; }

        /// <summary>
        /// 探索続行時Wait
        /// </summary>
        virtual public double WaitContinueSearch { get; set; }

        /// <summary>
        /// アクセス制限時Wait
        /// </summary>
        virtual public double WaitAccessBlock { get; set; }

        /// <summary>
        /// その他の画面でのWait
        /// </summary>
        virtual public double WaitMisc { get; set; }

        /// <summary>
        /// BEx1のとき敵に与えられるダメージ
        /// </summary>
        virtual public ulong BaseDamage { get; set; }

        /// <summary>
        /// 敵数がこの値以下なら探索する
        /// </summary>
        virtual public ulong EnemyCount { get; set; }

        /// <summary>
        /// 未受け取り報酬がこの値以上なら受け取る
        /// </summary>
        virtual public int ReceiveCount { get; set; }

        /// <summary>
        /// 報酬を受け取る
        /// </summary>
        virtual public bool ReceiveReword { get; set; }

        /// <summary>
        /// プレゼントを受け取る
        /// </summary>
        virtual public bool ReceivePresent { get; set; }

        /// <summary>
        /// 探索のみ
        /// </summary>
        virtual public bool OnlySearch { get; set; }

        /// <summary>
        /// 探索しない
        /// </summary>
        virtual public bool NoSearch { get; set; }

        /// <summary>
        /// 稼働開始時間
        /// </summary>
        virtual public DateTime StartTime { get; set; }

        /// <summary>
        /// 稼働停止時間
        /// </summary>
        virtual public DateTime EndTime { get; set; }

        /// <summary>
        /// 分間平均探索数の基準値
        /// </summary>
        virtual public uint SampleCount { get; set; }

        /// <summary>
        /// 可変Wait
        /// </summary>
        private double WaitVariable { get; set; }

        /// <summary>
        /// 敵発見フラグ
        /// </summary>
        protected bool EnemyFound { get; set; }

        public delegate void StateChangedHandler(object sender, State state);
        virtual public event StateChangedHandler StateChanged;
        public delegate void MinicapChangedHandler(object sender, int count);
        virtual public event MinicapChangedHandler MinicapChanged;
        public delegate void LogHandler(object sender, string text);
        virtual public event LogHandler Log;
        public delegate void SpeedCounterHandler(object sender, int spm);
        virtual public event SpeedCounterHandler SpeedCounter;

        /// <summary>
        /// 状態ID
        /// </summary>
        public enum State
        {
            None,
            Home,
            EventHome,
            Battle,
            BattleAssaultOperation,
            EnemyList,
            EnemyAppearance,
            SearchFlash,
            BattleFlash,
            RequestComplete,
            LevelUp,
            Error,
            Result,
            Receive,
            PresentList,
            FightAlreadyFinished,
            AccessBlock,
            Boss,
            GetCard,
            Interval,
            EventFinished,
            Unknown,
            AssaultOperationHome,
            AssaultOperationRequest,
            AssaultOperationRequestSubmit,
            AssaultOperationRequestComplete,
            AssaultOperationPresentList,
            AssaultOperationWin,
            AssaultOperationStart,
            AssaultOperationFaildRequestJoin,
            GroupSelectJobs,
            GroupUseBoost,
            PromotionWithdrawalConfirmation,
            PromotionWithdrawalCompletion,
            PromotionSallyConfirmation,
            GTacticsStrategicArea
        }

        /// <summary>
        /// 攻撃モード
        /// </summary>
        public enum AttackMode
        {
            Unlimited,
            ComboOnly,
            OneAttack,
            攻撃力が低い敵を攻撃撤退無し,
            攻撃力が低い敵を攻撃HP20パーセント以下で撤退,
            PTが高い敵を攻撃撤退無し,
            PTが高い敵を攻撃HP20パーセント以下で撤退,
            攻撃力割るMS数が低い敵を攻撃撤退無し,
            攻撃力割るMS数が低い敵を攻撃HP20パーセント以下で撤退
        }

        /// <summary>
        /// 探索の結果
        /// </summary>
        protected enum SearchResult
        {
            Found,
            Card,
            Error,
            FuelShortage
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="driver">WebDriver</param>
        /// <param name="home_path">イベントホームのパス</param>
        public Event(IWebDriver driver, string home_path)
        {
            driver_ = driver;
            home_path_ = home_path;
            enemy_list_path_ = "";
            CurrentState = State.None;
            RunObj = new object();
            Exec = SearchState;
            WaitSearch = 0.0;
            WaitBattle = 0.0;
            WaitAttack = 0.0;
            WaitReceive = 0.0;
            WaitContinueSearch = 0.0;
            WaitAccessBlock = 0.0;
            WaitMisc = 0.0;
            BaseDamage = 0;
            AttackedEnemyId = new List<string>();
            EnemyFoundTime = new List<DateTime>();
            SampleCount = 0;
            WaitVariable = 0.0;
            OnlySearch = false;
            NoSearch = false;

            //警告消し
            StateChanged?.Invoke(this, State.None);
            MinicapChanged?.Invoke(this, 0);
            Log?.Invoke(this, "");
            SpeedCounter?.Invoke(this, 0);
        }

        /// <summary>
        /// スレッドを生成する
        /// </summary>
        virtual public void CreateThread()
        {
            worker_thread = new System.Threading.Thread(SeleniumThread);
            lock (RunObj)
                IsRun = true;
            worker_thread.Start();
        }

        /// <summary>
        /// スレッドを終了する
        /// </summary>
        virtual public void KillThread()
        {
            lock (RunObj)
                IsRun = false;
        }

        /// <summary>
        /// メインループ
        /// </summary>
        virtual protected void SeleniumThread()
        {
            driver_.Navigate().GoToUrl(home_path_);
            while (true)
            {
                System.Threading.Thread.Sleep(1);
                Exec();

                lock (RunObj)
                    if (!IsRun)
                        break;
            }
        }

        /// <summary>
        /// 指定秒数待機する
        /// </summary>
        /// <param name="second">待機秒数</param>
        virtual protected void Wait(double second)
        {
            if (second * 1000 > 0)
            {
                System.Threading.Thread.Sleep((int)(second * 1000));
            }
        }

        /// <summary>
        /// 現在の状態を検索する
        /// </summary>
        virtual protected void SearchState()
        {
        }

        /// <summary>
        /// ホーム画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsHome() => driver_.Url == home_path_;

        /// <summary>
        /// 敵一覧画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsEnemyList() => driver_.PageSource.IndexOf("戦況を更新") >= 0 && driver_.PageSource.IndexOf("敵を見つける") >= 0;

        /// <summary>
        /// 敵一覧画面(敵出現)判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsEnemyAppearance() => driver_.PageSource.IndexOf("戦況を更新する") >= 0 && (driver_.PageSource.IndexOf("敵を攻撃") >= 0 || driver_.PageSource.IndexOf("コンボチャンス") >= 0);

        /// <summary>
        /// 戦闘画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsBattle() => driver_.PageSource.IndexOf("バトルエネルギー") >= 0 && driver_.PageSource.IndexOf("撃破報酬をチェック") >= 0;

        /// <summary>
        /// 応援依頼完了画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsRequestComplete() => driver_.PageSource.IndexOf("応援依頼完了") >= 0;


        /// <summary>
        /// 探索時のFlash画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsSearchFlash() => driver_.PageSource.IndexOf("http://gcc-a.sp.mbga.jp/smart/gcard/js/vendor/screener.min.js") >= 0;

        /// <summary>
        /// 戦闘時のFlash画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsFightFlash() => driver_.PageSource.IndexOf("http://gcc-a.sp.mbga.jp/smart/gcard/js/min/sp.pex.fight") >= 0;

        /// <summary>
        /// レベルアップ画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsLevelUp() => driver_.PageSource.IndexOf("heading-aー") >= 0 && driver_.PageSource.IndexOf("レベルアップ") >= 0;

        /// <summary>
        /// リザルト画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsResult() => driver_.PageSource.IndexOf("value=\"報酬を受け取る\"") >= 0;

        /// <summary>
        /// 受け取り画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsReceive() => driver_.PageSource.IndexOf("報酬") >= 0 && driver_.PageSource.IndexOf("受け取り") >= 0 && driver_.PageSource.IndexOf("プレゼントへ") >= 0;

        /// <summary>
        /// プレゼント一覧画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsPresentList() => driver_.PageSource.IndexOf("プレゼント一覧") >= 0;

        /// <summary>
        /// カード獲得画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsGetCard() => driver_.PageSource.IndexOf("カード獲得") >= 0;

        /// <summary>
        /// ボス画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsBoss() => driver_.PageSource.IndexOf("ボスが現れた") >= 0;

        /// <summary>
        /// インターバル中判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsHomeDuringInterval() => driver_.PageSource.IndexOf("インターバル中です") >= 0;

        /// <summary>
        /// 不正な画面遷移画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsError() => driver_.PageSource.IndexOf("不正な画面遷移です") >= 0 || driver_.PageSource.IndexOf("エラー") >= 0;

        /// <summary>
        /// 既に戦闘は終了しています画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsFightAlreadyFinished() => driver_.PageSource.IndexOf("既に戦闘は終了しています") >= 0 || driver_.PageSource.IndexOf("このボスと戦うことはできません") >= 0;

        /// <summary>
        /// アクセス制限画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsAccessBlock() => driver_.PageSource.IndexOf("アクセスを制限") >= 0;

        /// <summary>
        /// イベント終了画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsEventFinished() => driver_.PageSource.IndexOf("イベントは終了しました") >= 0;

        /// <summary>
        /// 燃料不足画面判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsFuelShortage() => driver_.PageSource.IndexOf("燃料不足") >= 0;

        /// <summary>
        /// サーバーエラー判定
        /// </summary>
        /// <returns></returns>
        virtual protected bool IsServerError() => driver_.PageSource == null || driver_.PageSource == "Access error occurred";

        /// <summary>
        /// 探索時のFlashをクリックする
        /// </summary>
        virtual protected void EmulateClickFlash()
        {
            try
            {
                string swfUrl = GetSwfURL(driver_.PageSource);
                string swf = new string(GetSwfBinary(swfUrl, 1024 * 1024));
                //WebClient wc = GetWebClient();
                //wc.DownloadFile(swfUrl, string.Format(@"11.swf", DateTime.Now.ToLongTimeString()));
                //wc.Dispose();

                //探索演出
                if (swfUrl.IndexOf("gcard_mission_effect") > 0)
                {
                    SearchEnemy(driver_.Url);
                    driver_.Navigate().GoToUrl(home_path_);
                }
                //カードゲットチャンス
                else if (swfUrl.IndexOf("lucky_effect") >= 0)
                {
                    SearchEnemy(driver_.Url);
                }
                //カードクエストクリア(部隊戦)
                else if(swfUrl.IndexOf("quest_clear_effect_for_combo") >= 0)
                {
                    string progress = new string(swf.Substring(swf.LastIndexOf("progress") + 13, 5).Where(c => char.IsNumber(c)).ToArray());
                    string id = new string(swf.Substring(swf.LastIndexOf("id") + 7, 9).Where(c => char.IsNumber(c)).ToArray());
                    string sk = new string(swf.Substring(swf.IndexOf("sk") + 7, 8).Where(c => char.IsNumber(c)).ToArray());

                    string resultURL = home_path_ + @"_raid_boss" +
                        "?progress=" + progress +
                        "&id=" + id +
                        "&sk=" + sk;
                    driver_.Navigate().GoToUrl(resultURL);
                }
                //クエストクリア演出
                else if (swfUrl.IndexOf("quest_clear_effect") >= 0)
                {
                    switch (this)
                    {
                        case Raid r:
                            {
                                string sk = new string(swf.Substring(swf.LastIndexOf("sk") + 7, 8).Where(c => char.IsNumber(c)).ToArray());
                                string resultURL = home_path_ + @"_raid_boss_receive_result" +
                                    "?sk=" + sk;
                                driver_.Navigate().GoToUrl(resultURL);
                                break;
                            }
                        case Group g:
                            {
                                string p_q_ss = new string(swf.Substring(swf.LastIndexOf("p_q_ss") + 11, 3).Where(c => char.IsNumber(c)).ToArray());
                                string c_q_ids = new string(swf.Substring(swf.LastIndexOf("c_q_ids") + 12, 3).Where(c => char.IsNumber(c)).ToArray());
                                string r_boss_eids = FilterNumComma(swf.Substring(swf.LastIndexOf("r_boss_eids") + 16));
                                string s_boss_eids = new string(swf.Substring(swf.LastIndexOf("s_boss_eids") + 16, 4).Where(c => char.IsNumber(c)).ToArray());

                                string resultURL = home_path_ + @"_raid_boss_receive_multi_result" +
                                    "?p_q_ss=" + p_q_ss +
                                    "&c_q_ids=" + c_q_ids +
                                    "&r_boss_eids=" + r_boss_eids +
                                    "&s_boss_eids=" + s_boss_eids;
                                driver_.Navigate().GoToUrl(resultURL);
                                break;
                            }
                        case Promotion p: driver_.Navigate().GoToUrl(home_path_); break;
                        case GShooting s: driver_.Navigate().GoToUrl(home_path_); break;
                        case GTactics t:
                            {
                                //http://gcc.sp.mbga.jp/_gcard_event310_raid_boss_receive_multi_result?l_boss_eids=&p_q_ss=1&c_q_ids=3&r_boss_eids=1106723%2C1108205%2C1108461%2C1108716%2C1108974
                                string l_boss_eids = new string(swf.Substring(swf.IndexOf("l_boss_eids", 16 * 3000) + 16, 2).Where(c => char.IsNumber(c)).ToArray());
                                string p_q_ss = new string(swf.Substring(swf.IndexOf("p_q_ss", 16 * 3000) + 11, 3).Where(c => char.IsNumber(c)).ToArray());
                                string c_q_ids = new string(swf.Substring(swf.IndexOf("c_q_ids", 16 * 3000) + 12, 3).Where(c => char.IsNumber(c)).ToArray());
                                string r_boss_eids = FilterNumComma(swf.Substring(swf.IndexOf("r_boss_eids") + 16));

                                string resultURL = home_path_ + @"_raid_boss_receive_multi_result" +
                                    "?l_boss_eids=" + l_boss_eids +
                                    "&p_q_ss=" + p_q_ss +
                                    "&c_q_ids=" + c_q_ids +
                                    "&r_boss_eids=" + r_boss_eids;
                                driver_.Navigate().GoToUrl(resultURL);
                                break;
                            }
                    }

                }
                //レベルアップ演出
                else if (swfUrl.IndexOf("lvup_effect") >= 0)
                {
                    string resultUrl = swfUrl.Replace("effect", "result");
                    driver_.Navigate().GoToUrl(resultUrl);
                    Exec = SearchState;
                    return;
                }
                //補給ボス演出
                else if (swfUrl.IndexOf("raid_boss_supply_boss_appear_effect") > 0)
                {
                    string a_point = new string(swf.Substring(swf.IndexOf("a_point") + 12, 10).Where(c => char.IsNumber(c)).ToArray());
                    string b_point = new string(swf.Substring(swf.IndexOf("b_point") + 12, 10).Where(c => char.IsNumber(c)).ToArray());
                    string boss_eids = FilterNumComma(swf.Substring(swf.IndexOf("boss_eids") + 14));
                    string can_r = new string(swf.Substring(swf.IndexOf("can_r") + 10, 2).Where(c => char.IsNumber(c)).ToArray());
                    string g_point = new string(swf.Substring(swf.IndexOf("g_point", 8000) + 12, 10).Where(c => char.IsNumber(c)).ToArray());

                    string resultURL = home_path_ + @"_raid_boss_receive_multi_result" +
                        "?a_point=" + a_point +
                        "&b_point=" + b_point +
                        "&boss_eids=" + boss_eids +
                        "&can_r=" + can_r +
                        "&g_point=" + g_point;

                    driver_.Navigate().GoToUrl(resultURL);
                }
                //昇格戦戦闘演出
                else if (swfUrl.IndexOf("promotion_battle_effect") >= 0)
                {
                    string gift_key = new string(swf.Substring(swf.IndexOf("gift_key", 8000) + 13, 6).Where(c => char.IsNumber(c)).ToArray());
                    string new_en = new string(swf.Substring(swf.IndexOf("new_en") + 11, 2).Where(c => char.IsNumber(c)).ToArray());
                    string exchanged_tmp_be_num = new string(swf.Substring(swf.IndexOf("exchanged_tmp_be_num", 8000) + 25, 1).Where(c => char.IsNumber(c)).ToArray());
                    string old_en = new string(swf.Substring(swf.IndexOf("old_en", 8000) + 11, 2).Where(c => char.IsNumber(c)).ToArray());
                    string old_pt = new string(swf.Substring(swf.IndexOf("ol_pt", 8000) + 11, 8).Where(c => char.IsNumber(c)).ToArray());
                    string round_id = new string(swf.Substring(swf.IndexOf("round_id", 8000) + 13, 2).Where(c => char.IsNumber(c)).ToArray());
                    string e_id = new string(swf.Substring(swf.IndexOf("e_id", 8000) + 9, 10).Where(c => char.IsNumber(c)).ToArray());
                    string total_pt = new string(swf.Substring(swf.IndexOf("total_pt", 8000) + 13, 8).Where(c => char.IsNumber(c)).ToArray());
                    string deck_id = new string(swf.Substring(swf.IndexOf("deck_id", 8000) + 12, 1).Where(c => char.IsNumber(c)).ToArray());
                    string nr = new string(swf.Substring(swf.IndexOf("nr", 8000) + 7, 4).Where(c => char.IsNumber(c)).ToArray());
                    string round_gift = new string(swf.Substring(swf.IndexOf("round_gift", 8000) + 15, 9).Where(c => char.IsNumber(c)).ToArray());
                    string win = new string(swf.Substring(swf.IndexOf("win", 8000) + 7, 4).Where(c => char.IsNumber(c)).ToArray());
                    string br = new string(swf.Substring(swf.IndexOf("br", 8000) + 7, 4).Where(c => char.IsNumber(c)).ToArray());
                    string win_all = new string(swf.Substring(swf.IndexOf("win_all", 8000) + 12, 2).Where(c => char.IsNumber(c)).ToArray());
                    string perfect = new string(swf.Substring(swf.IndexOf("perfect", 8000) + 12, 1).Where(c => char.IsNumber(c)).ToArray());
                    string get_pt = new string(swf.Substring(swf.IndexOf("get_pt", 8000) + 11, 7).Where(c => char.IsNumber(c)).ToArray());
                    string rank_up_incentive_flg = new string(swf.Substring(swf.IndexOf("rank_up_incentive_flg", 8000) + 12, 2).Where(c => char.IsNumber(c)).ToArray());
                    string new_pt = new string(swf.Substring(swf.IndexOf("new_pt", 8000) + 11, 8).Where(c => char.IsNumber(c)).ToArray());

                    string resultURL = @"http://gcc.sp.mbga.jp/_gcard_promotion_battle_result?" +
                        "gift_key=" + gift_key +
                        "&new_en=" + new_en +
                        "&exchanged_tmp_be_num=" + exchanged_tmp_be_num +
                        "&old_en=" + old_en +
                        "&old_pt=" + old_pt +
                        "&round_id=" + round_id +
                        "&e_id=" + e_id +
                        "&total_pt=" + total_pt +
                        "&deck_id=" + deck_id +
                        "&nr=" + nr +
                        "&round_gift=" + round_gift +
                        "&win=" + win +
                        "&br=" + br +
                        "&win_all=" + win_all +
                        "&perfect=" + perfect +
                        "&get_pt=" + get_pt +
                        "&rank_up_incentive_flg=" + rank_up_incentive_flg +
                        "&new_pt=" + new_pt;

                    driver_.Navigate().GoToUrl(resultURL);
                }
                //中隊メンバー結成演出(G-Tactics)
                else if (swfUrl.IndexOf("team_formation_effect") >= 0)
                {
                    driver_.Navigate().GoToUrl(home_path_ + "_group_members");
                }
                //部隊クエスト終了(G-Tactics)
                else if (swfUrl.IndexOf("group_quest_result_effect") >= 0)
                {
                    string id = swfUrl.Substring(swfUrl.IndexOf("id=") + 3);
                    driver_.Navigate().GoToUrl(home_path_ + "_group_quest_result?id=" + id);
                }
                //ラウンドリザルト(G-Tactics)
                else if (swfUrl.IndexOf("group_result_effect") >= 0)
                {
                    driver_.Navigate().GoToUrl(home_path_ + "_group_records");
                }
                //戦略拠点制圧(G-Tactics)
                else if (swfUrl.IndexOf("field_occupy_strategic_area_effect") >= 0)
                {
                    driver_.Navigate().GoToUrl(enemy_list_path_);
                }
                //強襲作戦結果(レイド)
                else if (swfUrl.IndexOf("assault_operation_result_effec") >= 0)
                {
                    string sk = new string(swf.Substring(swf.IndexOf("sk") + 7, 8).Where(c => char.IsNumber(c)).ToArray());

                    string resultURL = home_path_ + @"_assault_operation_result?" +
                        "sk=" + sk;
                    driver_.Navigate().GoToUrl(resultURL);
                }
                //ジョブレベルUP(部隊戦)
                else if (swfUrl.IndexOf("job_level_up_effec") >= 0)
                {
                    string p_q_ss = new string(swf.Substring(swf.LastIndexOf("p_q_ss") + 11, 3).Where(c => char.IsNumber(c)).ToArray());
                    string c_q_ids = new string(swf.Substring(swf.LastIndexOf("c_q_ids") + 12, 3).Where(c => char.IsNumber(c)).ToArray());
                    string r_boss_eids = FilterNumComma(swf.Substring(swf.LastIndexOf("r_boss_eids") + 16));
                    string s_boss_eids = new string(swf.Substring(swf.LastIndexOf("s_boss_eids") + 16, 4).Where(c => char.IsNumber(c)).ToArray());

                    string resultURL = home_path_ + @"_raid_boss_receive_multi_result" +
                        "?p_q_ss=" + p_q_ss +
                        "&c_q_ids=" + c_q_ids +
                        "&r_boss_eids=" + r_boss_eids +
                        "&s_boss_eids=" + s_boss_eids;
                    driver_.Navigate().GoToUrl(resultURL);
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

            Exec = SearchState;
        }

        /// <summary>
        /// FlashのURLを取得する
        /// </summary>
        /// <param name="PageSource">ページのソース</param>
        /// <returns>FlashのURL</returns>
        protected string GetSwfURL(string PageSource)
        {
            //var swf = '_gcard_event309_raid_boss_supply_boss_appear_effect?sk=91124';
            Regex r = new Regex(@"(var swf = '|'swf': ')(?<type>.+)(';|',)(\n|\r|.*'target')", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            MatchCollection mc = r.Matches(PageSource.Replace("&amp;", "&").Replace(";", ";\n"));
            string url = mc.Count > 0 ? @"http://gcc.sp.mbga.jp/" + mc[0].Groups["type"].Value : "";
            return url;
        }

        /// <summary>
        /// 先頭から数値とカンマを抽出しURLエンコードする
        /// </summary>
        /// <param name="SwfBinary">探索Flashのバイナリ</param>
        /// <returns>出力値</returns>
        protected string FilterNumComma(string SwfBinary)
        {
            string ret = "";

            foreach (char c in SwfBinary)
            {
                if (char.IsNumber(c) || c == ',')
                {
                    ret += c;
                }
                else break;
            }

            return System.Web.HttpUtility.UrlEncode(ret);
        }

        /// <summary>
        /// 探索Flashからt_idを抽出する
        /// </summary>
        /// <param name="SwfBinary">探索Flashのバイナリ</param>
        /// <returns>t_id値</returns>
        protected string GetTid(string SwfBinary)
        {
            return FilterNumComma(SwfBinary.Substring(SwfBinary.IndexOf("t_id") + 9));
        }

        /// <summary>
        /// 探索Flashからm_idを抽出する
        /// </summary>
        /// <param name="SwfBinary">探索Flashのバイナリ</param>
        /// <returns>m_id値</returns>
        protected string GetMid(string SwfBinary) => SwfBinary.Substring(SwfBinary.IndexOf("m_id") + 9, 8);

        /// <summary>
        /// 探索Flashからtokenを抽出する
        /// </summary>
        /// <param name="SwfBinary">探索Flashのバイナリ</param>
        /// <returns>token値</returns>
        protected string GetToken(string SwfBinary) => new string(SwfBinary.Substring(SwfBinary.IndexOf("token") + 10, 6).Where(c => char.IsNumber(c)).ToArray());

        /// <summary>
        /// 探索Flashからカードチャンスのtokenを抽出する
        /// </summary>
        /// <param name="SwfBinary">探索Flashのバイナリ</param>
        /// <returns>カードチャンスのtoken値値</returns>
        protected string GetLuckyToken(string SwfBinary) => new string(SwfBinary.Substring(SwfBinary.IndexOf("token") + 6, 8).Where(c => char.IsNumber(c)).ToArray());

        /// <summary>
        /// 探索Flashからtypeを抽出する
        /// </summary>
        /// <param name="SwfBinary">探索Flashのバイナリ</param>
        /// <returns>type値</returns>
        protected string GetType(string SwfBinary) => SwfBinary.Substring(SwfBinary.IndexOf("type") + 9, 1)[0] == 'c' ? "chance" : "return";

        /// <summary>
        /// WebClientを取得する
        /// </summary>
        /// <returns></returns>
        protected WebClient GetWebClient()
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers[HttpRequestHeader.UserAgent] = "Mozilla /5.0 (iPhone; CPU iPhone OS 9_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13B5110e Safari/601.1";
            string cc = string.Join("; ", driver_.Manage().Cookies.AllCookies.Select(c => string.Format("{0}={1}", c.Name, c.Value)));
            client.Headers[HttpRequestHeader.Cookie] = string.Join("; ", driver_.Manage().Cookies.AllCookies.Select(c => string.Format("{0}={1}", c.Name, c.Value)));

            return client;
        }

        /// <summary>
        /// swfのバイナリデータを取得する
        /// </summary>
        /// <param name="swfUrl">swfのurl</param>
        /// <returns>バイナリデータ</returns>
        char[] GetSwfBinary(string swfUrl, int readCount)
        {
            try
            {
                using (WebClient clientSwf = GetWebClient())
                using (Stream streamSwf = clientSwf.OpenRead(swfUrl))
                {
                    streamSwf.ReadTimeout = 5000;
                    using (BinaryReader br = new BinaryReader(streamSwf, Encoding.ASCII))
                    {
                        return br.ReadChars(readCount);
                    }
                }
            }
            catch
            {
                return new char[] { };
            }
        }


        /// <summary>
        /// 敵を出現させる
        /// </summary>
        /// <param name="url">探索FlashページのURL</param>
        /// <returns>成否</returns>
        protected SearchResult SearchEnemy(string url)
        {
            try
            {
                using (WebClient client = GetWebClient())
                using (Stream stream = client.OpenRead(url))
                {
                    stream.ReadTimeout = 5000;
                    using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                    {
                        string pageSource = sr.ReadToEnd();
                        string swfUrl = GetSwfURL(pageSource);

                        string swf = new string(GetSwfBinary(swfUrl, 3000));
                        string resultURL = "";


                        //カードチャンスであれば
                        if (swfUrl.IndexOf("lucky") >= 0)
                        {
                            string token = GetLuckyToken(swf);
                            resultURL = (@"http://gcc.sp.mbga.jp/_gcard_mission_lucky_lot" + "?token=" + token + "&card=1&mekuru=0");
                            driver_.Navigate().GoToUrl(resultURL);
                            return SearchResult.Card;
                        }
                        else if (swfUrl.IndexOf("mission") >= 0)
                        {
                            string tid = GetTid(swf);
                            string mid = GetMid(swf);
                            string token = GetToken(swf);
                            string type = GetType(swf);
                            string saveURL = @"http://gcc.sp.mbga.jp/_gcard_mission_save";
                            if (type == "return")
                            {
                                resultURL = saveURL + "?t%5Fid=" + tid + "&m%5Fid=" + mid + "&token=" + token;
                            }
                            else
                            {
                                resultURL = saveURL + "?t%5Fid=" + tid + "&m%5Fid=" + mid + "&type=" + type + "&token=" + token;
                            }
                        }
                        else if (pageSource.IndexOf("燃料不足") >= 0)
                        {
                            return SearchResult.FuelShortage;
                        }

                        using (WebClient clientGet = GetWebClient())
                        {
                            string respons = clientGet.DownloadString(resultURL);

                            if (respons.IndexOf("boss_appear") >= 0)
                            {
                                driver_.Navigate().Refresh();
                                EnemyFound = true;

                                //敵発見Flash
                                return  SearchResult.Found;
                            }
                            else if(respons.IndexOf("lucky") >= 0 || respons.IndexOf("mission") >= 0)
                            {
                                //探索続行
                                Wait(WaitContinueSearch);
                                return SearchResult.Found;
                            }
                            else
                            {
                                //アクセス規制
                                return SearchResult.Error;
                            }
                        }
                    }
                }
            }
            catch
            {
                return SearchResult.Error;
            }
        }

        /// <summary>
        /// アクセス制限向け調整用待機
        /// </summary>
        virtual protected void WaitForAccessLimit()
        {
            //分間平均発見数のために発見時刻を記録する
            DateTime now = DateTime.Now;
            EnemyFoundTime.Add(now);
            EnemyFoundTime = EnemyFoundTime.Where(e => (now - e) < TimeSpan.FromMinutes(1)).ToList();

            List<TimeSpan> searchTime = new List<TimeSpan>(EnemyFoundTime.Count);
            for (int i = 1; i < EnemyFoundTime.Count; i++)
            {
                searchTime.Add(EnemyFoundTime[i] - EnemyFoundTime[i - 1]);
            }

            if (searchTime.Count > 0 && SampleCount > 0)
            {
                var values = searchTime.Select(e => e.TotalSeconds);
                double averageSearchTime = WeightedAverage(values);

                WaitVariable += 60.0 / SampleCount - averageSearchTime;

                if (WaitVariable <= 0)
                {
                    WaitVariable = 0;

                }
                else
                {
                    Log?.Invoke(this, string.Format("時間調整：{0:f2}秒待機", WaitVariable));
                    Wait(WaitVariable);
                }
                SpeedCounter?.Invoke(this, values.Count());
            }
        }

        /// <summary>
        /// 戦闘中のFlashをクリックする
        /// </summary>
        virtual protected void ClickBattleFlash()
        {
            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//*[@id=\"main\"]/canvas"));
                Actions action = new Actions(driver_);
                action.MoveToElement(elm, 300, 520).Click().Build().Perform();
            }
            catch { }

            Exec = SearchState;
        }

        /// <summary>
        /// レベルアップからラッキーチャンスゲーム or 探索へ
        /// </summary>
        virtual protected void MoveLevelUpToSearch()
        {
            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"ラッキーチャンスゲームに挑戦\"]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                Exec = SearchState;
                return;
            }
            catch { }


            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//div[text()=\"探索する\"]"));
                elm.Submit();
            }
            catch { }

            Exec = SearchState;
        }

        /// <summary>
        /// カード獲得から探索へ
        /// </summary>
        virtual protected void MoveGetCardToSearch()
        {
            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"売却して探索する\"]"));
                driver_.Navigate().GoToUrl(enemy_list_path_);
            }
            catch { }

            Exec = SearchState;
        }

        /// <summary>
        /// 戦闘結果から敵一覧へ
        /// </summary>
        virtual protected void MoveResultToEnemyList()
        {
            try
            {
                //var elms = driver_.FindElements(By.XPath("//input[@value=\"報酬を受け取る\"]"));
                var elms = driver_.FindElements(By.XPath("//a[text()=\"敵一覧\"]"));
                foreach (var e in elms)
                {
                    //e.Submit();
                    driver_.Navigate().GoToUrl(e.GetAttribute("href"));
                    break;
                }
            }
            catch { }

            Exec = SearchState;
        }

        /// <summary>
        /// 応援依頼完了から戦闘画面 or 探索へ
        /// </summary>
        virtual protected void MoveRequestCompleteToBattle()
        {
            try
            {
                driver_.Navigate().GoToUrl(enemy_list_path_);
                //IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"ボス戦に戻る\"]");
                //driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                Exec = SearchState;
                return;
            }
            catch { }

            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"続けて探索する\"]"));
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
        /// 受け取りからプレゼントリストへ
        /// </summary>
        virtual protected void MoveReceiveToPresentList()
        {
            try
            {
                if (ReceivePresent)
                {
                    IWebElement elm = driver_.FindElement(By.XPath("//a[@href=\"_gcard_gifts\"]"));
                    driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
                }
                else
                {
                    driver_.Navigate().GoToUrl(enemy_list_path_);
                }
            }
            catch
            {
                driver_.Navigate().GoToUrl(home_path_);
            }

            Exec = SearchState;
        }

        /// <summary>
        /// プレゼント一覧からプレゼント受け取り or 探索へ
        /// </summary>
        virtual protected void MovePresentListToPresent()
        {
            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//input[@value=\"プレゼントをまとめて受け取る\"]/../../../form"));
                elm.Submit();
                Exec = SearchState;
                return;
            }
            catch { }


            try
            {
                driver_.Navigate().GoToUrl(enemy_list_path_);
                Exec = SearchState;
                if (enemy_list_path_ == "")
                    throw new Exception();
            }
            catch
            {
                try
                {
                    IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"敵一覧\"]"));
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
            }

            Exec = SearchState;
        }

        /// <summary>
        /// ボスから戦闘へ
        /// </summary>
        virtual protected void MoveBossToBattle()
        {
            try
            {
                IWebElement elm = driver_.FindElement(By.XPath("//a[text()=\"ボスと戦う\"]"));
                driver_.Navigate().GoToUrl(elm.GetAttribute("href"));
            }
            catch { }

            Exec = SearchState;
        }

        /// <summary>
        /// 戦闘画面のurlの敵IDが攻撃済みか判定
        /// </summary>
        /// <param name="url">戦闘画面のurl</param>
        /// <returns>攻撃済み</returns>
        virtual protected bool IsAttacked(string url) => AttackedEnemyId.LastIndexOf(GetEnemyId(url)) >= 0;

        /// <summary>
        /// 戦闘画面のurlから敵IDを追加
        /// </summary>
        /// <param name="url">戦闘画面のurl</param>
        virtual protected void AddEnemyId(string url) => AttackedEnemyId.Add(GetEnemyId(url));

        /// <summary>
        /// 戦闘画面のurlから敵IDを削除
        /// </summary>
        /// <param name="url">戦闘画面のurl</param>
        virtual protected void RemoveEnemyId(string url) => AttackedEnemyId.RemoveAll(elm => elm == GetEnemyId(url));

        /// <summary>
        /// 戦闘画面のurlから敵IDを抽出
        /// </summary>
        /// <param name="url">戦闘画面のurl</param>
        /// <returns>敵ID</returns>
        virtual protected string GetEnemyId(string url)
        {
            Regex r = new Regex(@"id=(?<id>[0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            MatchCollection mc = r.Matches(url);

            return mc.Count > 0 ? mc[0].Groups["id"].Value : "";
        }

        /// <summary>
        /// 時間外判定
        /// </summary>
        /// <returns>時間外</returns>
        virtual protected bool IsOutOfTimeRange()
        {
            TimeSpan now = DateTime.Now.TimeOfDay;
            TimeSpan start = StartTime.TimeOfDay;
            TimeSpan end = EndTime.TimeOfDay;

            if (start >= end)
            {
                end += TimeSpan.FromDays(1);

                if (start > now)
                    now += TimeSpan.FromDays(1);
            }

            return now < start || now >= end;
        }

        /// <summary>
        /// 加重平均計算
        /// </summary>
        /// <param name="values">入力値</param>
        /// <returns>加重平均</returns>
        private double WeightedAverage(IEnumerable<double> values)
        {
            int weight = 0;

            double sum = values.Sum(e => e * (++weight));
            double ave = sum / ((2 + (values.Count() - 1)) * values.Count() * 0.5);

            return ave;
        }
    }
}
