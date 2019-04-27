using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gcard_macro
{
    namespace Setting
    {
        public class Settings
        {
            public static void Load()
            {
                Common.Load();
                Raid.Load();
                Group.Load();
                GShooting.Load();
                ShootingRange.Load();
                Promotion.Load();
                GTactics.Load();
            }
        }

        /// <summary>
        /// 共通の設定
        /// </summary>
        public class Common
        {
            public static double WaitSearch { get; set; }
            public static double WaitBattle { get; set; }
            public static double WaitAttack { get; set; }
            public static double WaitReceive { get; set; }
            public static double WaitAccessBlock { get; set; }
            public static double WaitMisc { get; set; }
            public static bool AutoRun { get; set; }
            public static bool OptimizedWaitEnable { get; set; }
            public static uint OptimizedWaitEnemyCount { get; set; }
            public static bool UseCycleReceive { get; set; }
            public static uint CycleRecieveTime { get; set; }

            /// <summary>
            /// 設定読み込み
            /// </summary>
            public static void Load()
            {
                WaitSearch = Properties.Settings.Default.WaitSearch;
                WaitBattle = Properties.Settings.Default.WaitBattle;
                WaitAttack = Properties.Settings.Default.WaitAttack;
                WaitReceive = Properties.Settings.Default.WaitReceive;
                WaitAccessBlock = Properties.Settings.Default.WaitAccessBlock;
                WaitMisc = Properties.Settings.Default.WaitMisc;
                AutoRun = Properties.Settings.Default.AutoRun;
                OptimizedWaitEnable = Properties.Settings.Default.OptimizedWaitEnable;
                OptimizedWaitEnemyCount = Properties.Settings.Default.OptimizedWaitEnemyCount;
                UseCycleReceive = Properties.Settings.Default.UseCycleReceive;
                CycleRecieveTime = Properties.Settings.Default.CycleRecieveTime;
            }

            /// <summary>
            /// 設定保存
            /// </summary>
            public static void Save()
            {
                Properties.Settings.Default.WaitSearch = WaitSearch;
                Properties.Settings.Default.WaitBattle = WaitBattle;
                Properties.Settings.Default.WaitAttack = WaitAttack;
                Properties.Settings.Default.WaitReceive = WaitReceive;
                Properties.Settings.Default.WaitAccessBlock = WaitAccessBlock;
                Properties.Settings.Default.WaitMisc = WaitMisc;
                Properties.Settings.Default.AutoRun = AutoRun;
                Properties.Settings.Default.OptimizedWaitEnable = OptimizedWaitEnable;
                Properties.Settings.Default.OptimizedWaitEnemyCount = OptimizedWaitEnemyCount;
                Properties.Settings.Default.UseCycleReceive = UseCycleReceive;
                Properties.Settings.Default.CycleRecieveTime = CycleRecieveTime;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// レイドイベントの設定
        /// </summary>
        public class Raid
        {
            /// <summary>
            /// イベントのホームのURL
            /// </summary>
            public static string Url { get; set; }

            /// <summary>
            /// BEx1使用時のダメージ
            /// </summary>
            public static ulong BaseDamage { get; set; }

            /// <summary>
            /// 探索する敵数の条件
            /// </summary>
            public static ulong EnemyCount { get; set; }

            /// <summary>
            /// 攻撃モード
            /// </summary>
            public static int AttackMode { get; set; }

            /// <summary>
            /// 報酬をまとめて受け取る個数
            /// </summary>
            public static int ReceiveCount { get; set; }

            /// <summary>
            /// 敵を攻撃しない
            /// </summary>
            public static bool OnlySearch { get; set; }

            /// <summary>
            /// 探索しない
            /// </summary>
            public static bool NoSearch { get; set; }

            /// <summary>
            /// 報酬を受け取る
            /// </summary>
            public static bool ReceiveReword { get; set; }

            /// <summary>
            /// プレゼントを受け取る
            /// </summary>
            public static bool ReceivePresent { get; set; }

            /// <summary>
            /// 応援依頼を出す
            /// </summary>
            public static bool Request { get; set; }

            /// <summary>
            /// MVPを狙う(レアボスのみ)
            /// </summary>
            public static bool AimMVP { get; set; }

            /// <summary>
            /// 強襲作戦に参加する
            /// </summary>
            public static bool JoinAssault { get; set; }

            /// <summary>
            /// 強襲専用BEのみ使用
            /// </summary>
            public static bool UseAssaultBE { get; set; }

            /// <summary>
            /// 強襲作戦中は雑魚敵を攻撃しない
            /// </summary>
            public static bool OnlyAttackAssultBoss { get; set; }

            /// <summary>
            /// 強襲作戦中は上のボスを攻撃しない
            /// </summary>
            public static bool OnlyAttackAssultEnemy { get; set; }

            /// <summary>
            /// 強襲作戦に参加する
            /// </summary>
            public static int PriorityAttackAssultEnemy { get; set; }

            /// <summary>
            /// 強襲作戦の報酬受け取り時の待機時間
            /// </summary>
            public static double WaitRecieveAssult { get; set; }

            /// <summary>
            /// 強襲作戦の戦艦攻撃時の待機時間
            /// </summary>
            public static double WaitAtackBattleShip { get; set; }

            /// <summary>
            /// アディショナルクエストに突入する
            /// </summary>
            public static bool EnterAdditionalQuest { get; set; }

            /// <summary>
            /// 稼働開始時間
            /// </summary>
            public static DateTime TimeStart { get; set; }

            /// <summary>
            /// 稼働終了時間
            /// </summary>
            public static DateTime TimeEnd { get; set; }

            /// <summary>
            /// 設定読み込み
            /// </summary>
            public static void Load()
            {
                Url = Properties.Settings.Default.RaidURL;
                JoinAssault = Properties.Settings.Default.RaidJoinAssault;
                UseAssaultBE = Properties.Settings.Default.RaidUseAssaultBE;
                Request = Properties.Settings.Default.RaidRequest;
                BaseDamage = Properties.Settings.Default.RaidBaseDamage;
                EnemyCount = Properties.Settings.Default.RaidEnemyCount;
                AttackMode = Properties.Settings.Default.RaidAttackMode;
                ReceiveCount = Properties.Settings.Default.RaidReceiveCount;
                OnlySearch = Properties.Settings.Default.RaidOnlySearch;
                NoSearch = Properties.Settings.Default.RaidNoSearch;
                ReceiveReword = Properties.Settings.Default.RaidReceiveReword;
                ReceivePresent = Properties.Settings.Default.RaidReceivePresent;
                AimMVP = Properties.Settings.Default.RaidAimMVP;
                OnlyAttackAssultBoss = Properties.Settings.Default.RaidOnlyAttackAssultBoss;
                OnlyAttackAssultEnemy = Properties.Settings.Default.RaidOnlyAttackAssultEnemy;
                WaitRecieveAssult = Properties.Settings.Default.RaidWaitRecieveAssult;
                WaitAtackBattleShip = Properties.Settings.Default.RaidWaitAtackBattleShip;
                EnterAdditionalQuest = Properties.Settings.Default.RaidEnterAdditionalQuest;
                PriorityAttackAssultEnemy = Properties.Settings.Default.RaidPriorityAttackAssultEnemy;

                try
                {
                    TimeStart = Properties.Settings.Default.RaidTimeStart;
                    TimeEnd = Properties.Settings.Default.RaidTimeEnd;
                }
                catch
                {
                    TimeStart = DateTime.MinValue;
                    TimeEnd = DateTime.MinValue;
                }
            }

            /// <summary>
            /// 設定保存
            /// </summary>
            public static void Save()
            {
                Properties.Settings.Default.RaidURL = Url;
                Properties.Settings.Default.RaidJoinAssault = JoinAssault;
                Properties.Settings.Default.RaidUseAssaultBE = UseAssaultBE;
                Properties.Settings.Default.RaidRequest = Request;
                Properties.Settings.Default.RaidBaseDamage = BaseDamage;
                Properties.Settings.Default.RaidEnemyCount = EnemyCount;
                Properties.Settings.Default.RaidAttackMode = AttackMode;
                Properties.Settings.Default.RaidReceiveCount = ReceiveCount;
                Properties.Settings.Default.RaidOnlySearch = OnlySearch;
                Properties.Settings.Default.RaidNoSearch = NoSearch;
                Properties.Settings.Default.RaidReceiveReword = ReceiveReword;
                Properties.Settings.Default.RaidReceivePresent = ReceivePresent;
                Properties.Settings.Default.RaidAimMVP = AimMVP;
                Properties.Settings.Default.RaidOnlyAttackAssultBoss = OnlyAttackAssultBoss;
                Properties.Settings.Default.RaidOnlyAttackAssultEnemy = OnlyAttackAssultEnemy;
                Properties.Settings.Default.RaidWaitRecieveAssult = WaitRecieveAssult;
                Properties.Settings.Default.RaidWaitAtackBattleShip = WaitAtackBattleShip;
                Properties.Settings.Default.RaidEnterAdditionalQuest = EnterAdditionalQuest;
                Properties.Settings.Default.RaidPriorityAttackAssultEnemy = PriorityAttackAssultEnemy;
                Properties.Settings.Default.RaidTimeStart = TimeStart;
                Properties.Settings.Default.RaidTimeEnd = TimeEnd;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// 部隊戦イベントの設定
        /// </summary>
        public class Group
        {
            /// <summary>
            /// イベントのホームのURL
            /// </summary>
            public static string Url { get; set; }

            /// <summary>
            /// BEx1使用時のダメージ
            /// </summary>
            public static ulong BaseDamage { get; set; }

            /// <summary>
            /// 探索する敵数の条件
            /// </summary>
            public static ulong EnemyCount { get; set; }

            /// <summary>
            /// 待機する敵部隊との点数差
            /// </summary>
            public static ulong PointDiff { get; set; }

            /// <summary>
            /// 攻撃モード
            /// </summary>
            public static int AttackMode { get; set; }

            /// <summary>
            /// 報酬をまとめて受け取る個数
            /// </summary>
            public static int ReceiveCount { get; set; }

            /// <summary>
            /// Boostを使用する
            /// </summary>
            public static bool UseBoost { get; set; }

            /// <summary>
            /// メモリアルボスを優先して攻撃する
            /// </summary>
            public static bool FirstAttackBoss { get; set; }

            /// <summary>
            /// 報酬を受け取る
            /// </summary>
            public static bool ReceiveReword { get; set; }

            /// <summary>
            /// プレゼントを受け取る
            /// </summary>
            public static bool ReceivePresent { get; set; }

            /// <summary>
            /// 自動ジョブレベル上げ
            /// </summary>
            public static bool AutoJobLevelUp { get; set; }

            /// <summary>
            /// 最終ジョブ
            /// </summary>
            public static int FinalJob { get; set; }

            /// <summary>
            /// 敵を攻撃しない
            /// </summary>
            public static bool OnlySearch { get; set; }

            /// <summary>
            /// 探索しない
            /// </summary>
            public static bool NoSearch { get; set; }

            /// <summary>
            /// BEx1 1.2倍+30コンボ(緑)を使用する
            /// </summary>
            public static bool UseCombo30 { get; set; }

            /// <summary>
            /// BEx1 1.2倍+30コンボ(緑)を通常時に使用する
            /// </summary>
            public static bool Combo30Normal { get; set; }

            /// <summary>
            /// BEx1 1.2倍+30コンボ(緑)を敵ミラージュコロイド時に使用する
            /// </summary>
            public static bool Combo30Mira { get; set; }

            /// <summary>
            /// BEx1 1.2倍+30コンボ(緑)を味方Boost時に使用する
            /// </summary>
            public static bool Combo30Boost { get; set; }

            /// <summary>
            /// BEx1 1.2倍+30コンボ(緑)を初回攻撃時に使用する
            /// </summary>
            public static bool Combo30FirstAttack { get; set; }

            /// <summary>
            /// BEx3 20倍(赤)を使用する
            /// </summary>
            public static bool UseAttack20 { get; set; }

            /// <summary>
            /// BEx3 20倍(赤)を通常時に使用する
            /// </summary>
            public static bool Attack20Normal { get; set; }

            /// <summary>
            /// BEx3 20倍(赤)を敵ミラージュコロイド時に使用する
            /// </summary>
            public static bool Attack20Mira { get; set; }

            /// <summary>
            /// BEx3 20倍(赤)を味方Boost時に使用する
            /// </summary>
            public static bool Attack20Boost { get; set; }

            /// <summary>
            /// BEx3 20倍(赤)を必要倍率が20倍以上の時のみ使用する
            /// </summary>
            public static bool Attack20RequiredRatio { get; set; }

            /// <summary>
            /// BEx5 10倍(黄)を使用する
            /// </summary>
            public static bool UseAttack10 { get; set; }

            /// <summary>
            /// BEx5 10倍(黄)を通常時に使用する
            /// </summary>
            public static bool Attack10Normal { get; set; }

            /// <summary>
            /// BEx5 10倍(黄)を味方Boost時に使用する
            /// </summary>
            public static bool Attack10Boost { get; set; }

            /// <summary>
            /// BEカプセルx1 20倍(紫)を使用する
            /// </summary>
            public static bool UseBE1 { get; set; }

            /// <summary>
            /// BEカプセルx1 20倍(紫)を通常時に使用する
            /// </summary>
            public static bool BE1Normal { get; set; }

            /// <summary>
            /// BEカプセルx1 20倍(紫)を敵ミラージュコロイド時に使用する
            /// </summary>
            public static bool BE1Mira { get; set; }

            /// <summary>
            /// BEカプセルx1 20倍(紫)を必要倍率が20倍以上の時のみ使用する
            /// </summary>
            public static bool BE1RequiredRatio { get; set; }

            /// <summary>
            /// 稼働開始時間
            /// </summary>
            public static DateTime TimeStart { get; set; }

            /// <summary>
            /// 稼働終了時間
            /// </summary>
            public static DateTime TimeEnd { get; set; }

            /// <summary>
            /// 設定読み込み
            /// </summary>
            public static void Load()
            {
                Url = Properties.Settings.Default.GroupURL;
                BaseDamage = Properties.Settings.Default.GroupBaseDamage;
                EnemyCount = Properties.Settings.Default.GroupEnemyCount;
                PointDiff = Properties.Settings.Default.GroupPointDiff;
                AttackMode = Properties.Settings.Default.GroupAttackMode;
                ReceiveCount = Properties.Settings.Default.GroupReceiveCount;
                UseBoost = Properties.Settings.Default.GroupUseBoost;
                FirstAttackBoss = Properties.Settings.Default.GroupFirstAttackBoss;
                ReceiveReword = Properties.Settings.Default.GroupReceiveReword;
                ReceivePresent = Properties.Settings.Default.GroupReceivePresent;
                AutoJobLevelUp = Properties.Settings.Default.GroupAutoJobLevelUp;
                FinalJob = Properties.Settings.Default.GroupFinalJob;
                OnlySearch = Properties.Settings.Default.GroupOnlySearch;
                NoSearch = Properties.Settings.Default.GroupNoSearch;
                UseCombo30 = Properties.Settings.Default.GroupUseCombo30;
                Combo30Normal = Properties.Settings.Default.GroupCombo30Normal;
                Combo30Mira = Properties.Settings.Default.GroupCombo30Mira;
                Combo30Boost = Properties.Settings.Default.GroupCombo30Boost;
                Combo30FirstAttack = Properties.Settings.Default.GroupCombo30FirstAttack;
                UseAttack20 = Properties.Settings.Default.GroupUseAttack20;
                Attack20Normal = Properties.Settings.Default.GroupAttack20Normal;
                Attack20Mira = Properties.Settings.Default.GroupAttack20Mira;
                Attack20Boost = Properties.Settings.Default.GroupAttack20Boost;
                Attack20RequiredRatio = Properties.Settings.Default.GroupAttack20RequiredRatio;
                UseAttack10 = Properties.Settings.Default.GroupUseAttack10;
                Attack10Normal = Properties.Settings.Default.GroupAttack10Normal;
                Attack10Boost = Properties.Settings.Default.GroupAttack10Boost;
                UseBE1 = Properties.Settings.Default.GroupUseBE1;
                BE1Normal = Properties.Settings.Default.GroupBE1Normal;
                BE1Mira = Properties.Settings.Default.GroupBE1Mira;
                BE1RequiredRatio = Properties.Settings.Default.GroupBE1RequiredRatio;

                try
                {
                    TimeStart = Properties.Settings.Default.GroupTimeStart;
                    TimeEnd = Properties.Settings.Default.GroupTimeEnd;
                }
                catch
                {
                    TimeStart = DateTime.MinValue;
                    TimeEnd = DateTime.MinValue;
                }
            }

            /// <summary>
            /// 設定保存
            /// </summary>
            public static void Save()
            {
                Properties.Settings.Default.GroupURL = Url;
                Properties.Settings.Default.GroupBaseDamage = BaseDamage;
                Properties.Settings.Default.GroupEnemyCount = EnemyCount;
                Properties.Settings.Default.GroupPointDiff = PointDiff;
                Properties.Settings.Default.GroupAttackMode = AttackMode;
                Properties.Settings.Default.GroupReceiveCount = ReceiveCount;
                Properties.Settings.Default.GroupUseBoost = UseBoost;
                Properties.Settings.Default.GroupFirstAttackBoss = FirstAttackBoss;
                Properties.Settings.Default.GroupReceiveReword = ReceiveReword;
                Properties.Settings.Default.GroupReceivePresent = ReceivePresent;
                Properties.Settings.Default.GroupAutoJobLevelUp = AutoJobLevelUp;
                Properties.Settings.Default.GroupFinalJob = FinalJob;
                Properties.Settings.Default.GroupOnlySearch = OnlySearch;
                Properties.Settings.Default.GroupNoSearch = NoSearch;
                Properties.Settings.Default.GroupUseCombo30 = UseCombo30;
                Properties.Settings.Default.GroupCombo30Normal = Combo30Normal;
                Properties.Settings.Default.GroupCombo30Mira = Combo30Mira;
                Properties.Settings.Default.GroupCombo30Boost = Combo30Boost;
                Properties.Settings.Default.GroupCombo30FirstAttack = Combo30FirstAttack;
                Properties.Settings.Default.GroupUseAttack20 = UseAttack20;
                Properties.Settings.Default.GroupAttack20Normal = Attack20Normal;
                Properties.Settings.Default.GroupAttack20Mira = Attack20Mira;
                Properties.Settings.Default.GroupAttack20Boost = Attack20Boost;
                Properties.Settings.Default.GroupAttack20RequiredRatio = Attack20RequiredRatio;
                Properties.Settings.Default.GroupUseAttack10 = UseAttack10;
                Properties.Settings.Default.GroupAttack10Normal = Attack10Normal;
                Properties.Settings.Default.GroupAttack10Boost = Attack10Boost;
                Properties.Settings.Default.GroupUseBE1 = UseBE1;
                Properties.Settings.Default.GroupBE1Normal = BE1Normal;
                Properties.Settings.Default.GroupBE1Mira = BE1Mira;
                Properties.Settings.Default.GroupBE1RequiredRatio = BE1RequiredRatio;
                Properties.Settings.Default.GroupTimeStart = TimeStart;
                Properties.Settings.Default.GroupTimeEnd = TimeEnd;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// G-Shootingイベントの設定
        /// </summary>
        public class GShooting
        {
            /// <summary>
            /// イベントのホームのURL
            /// </summary>
            public static string Url { get; set; }

            /// <summary>
            /// BEx1使用時のダメージ
            /// </summary>
            public static ulong BaseDamage { get; set; }

            /// <summary>
            /// 探索する敵数の条件
            /// </summary>
            public static ulong EnemyCount { get; set; }

            /// <summary>
            /// 攻撃モード
            /// </summary>
            public static int AttackMode { get; set; }

            /// <summary>
            /// 報酬をまとめて受け取る個数
            /// </summary>
            public static int ReceiveCount { get; set; }

            /// <summary>
            /// 敵を攻撃しない
            /// </summary>
            public static bool OnlySearch { get; set; }

            /// <summary>
            /// 探索しない
            /// </summary>
            public static bool NoSearch { get; set; }

            /// <summary>
            /// 報酬を受け取る
            /// </summary>
            public static bool ReceiveReword { get; set; }

            /// <summary>
            /// プレゼントを受け取る
            /// </summary>
            public static bool ReceivePresent { get; set; }

            /// <summary>
            /// 応援依頼を出す
            /// </summary>
            public static bool Request { get; set; }

            /// <summary>
            /// 稼働開始時間
            /// </summary>
            public static DateTime TimeStart { get; set; }

            /// <summary>
            /// 稼働終了時間
            /// </summary>
            public static DateTime TimeEnd { get; set; }

            /// <summary>
            /// 設定読み込み
            /// </summary>
            public static void Load()
            {
                Url = Properties.Settings.Default.GShootingURL;
                BaseDamage = Properties.Settings.Default.GShootingBaseDamage;
                EnemyCount = Properties.Settings.Default.GShootingEnemyCount;
                AttackMode = Properties.Settings.Default.GShootingAttackMode;
                ReceiveCount = Properties.Settings.Default.GShootingReceiveCount;
                OnlySearch = Properties.Settings.Default.GShootingOnlySearch;
                NoSearch = Properties.Settings.Default.GShootingNoSearch;
                ReceiveReword = Properties.Settings.Default.GShootingReceiveReword;
                ReceivePresent = Properties.Settings.Default.GShootingReceivePresent;
                Request = Properties.Settings.Default.GShootingRequest;

                try
                {
                    TimeStart = Properties.Settings.Default.GShootingTimeStart;
                    TimeEnd = Properties.Settings.Default.GShootingTimeEnd;
                }
                catch
                {
                    TimeStart = DateTime.MinValue;
                    TimeEnd = DateTime.MinValue;
                }
            }

            /// <summary>
            /// 設定保存
            /// </summary>
            public static void Save()
            {
                Properties.Settings.Default.GShootingURL = Url;
                Properties.Settings.Default.GShootingBaseDamage = BaseDamage;
                Properties.Settings.Default.GShootingEnemyCount = EnemyCount;
                Properties.Settings.Default.GShootingAttackMode = AttackMode;
                Properties.Settings.Default.GShootingReceiveCount = ReceiveCount;
                Properties.Settings.Default.GShootingOnlySearch = OnlySearch;
                Properties.Settings.Default.GShootingNoSearch = NoSearch;
                Properties.Settings.Default.GShootingReceiveReword = ReceiveReword;
                Properties.Settings.Default.GShootingReceivePresent = ReceivePresent;
                Properties.Settings.Default.GShootingRequest = Request;
                Properties.Settings.Default.GShootingTimeStart = TimeStart;
                Properties.Settings.Default.GShootingTimeEnd = TimeEnd;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// GShootingイベントの射撃場の設定
        /// </summary>
        class ShootingRange
        {
            /// <summary>
            /// イベントのホームのURL
            /// </summary>
            public static string Url { get; set; }

            /// <summary>
            /// 集中射撃を使う閾値
            /// </summary>
            public static uint ThresholdFocusShot { get; set; }

            /// <summary>
            /// フィーバー中は集中射撃を使う
            /// </summary>
            public static bool UseFocusShotDuringFever { get; set; }

            /// <summary>
            /// フィーバーチップを使う
            /// </summary>
            public static bool UseFeverTip { get; set; }

            /// <summary>
            /// 全てのクエストクリアで自動的に停止する
            /// </summary>
            public static bool AutoStop { get; set; }

            /// <summary>
            /// 設定読み込み
            /// </summary>
            public static void Load()
            {
                Url = Properties.Settings.Default.ShootingRangeURL;
                ThresholdFocusShot = Properties.Settings.Default.ShootingRangeThresholdFocusShot;
                UseFocusShotDuringFever = Properties.Settings.Default.ShootingRangeUseFocusShotDuringFever;
                UseFeverTip = Properties.Settings.Default.ShootingRangeUseFeverTip;
                AutoStop = Properties.Settings.Default.ShootingRangeAutoStop;
            }

            /// <summary>
            /// 設定保存
            /// </summary>
            public static void Save()
            {
                Properties.Settings.Default.ShootingRangeURL = Url;
                Properties.Settings.Default.ShootingRangeThresholdFocusShot = ThresholdFocusShot;
                Properties.Settings.Default.ShootingRangeUseFocusShotDuringFever = UseFocusShotDuringFever;
                Properties.Settings.Default.ShootingRangeUseFeverTip = UseFeverTip;
                Properties.Settings.Default.ShootingRangeAutoStop = AutoStop;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// 昇格戦の設定
        /// </summary>
        class Promotion
        {
            /// <summary>
            /// イベントのホームのURL
            /// </summary>
            public static string Url { get; set; }

            /// <summary>
            /// 攻撃モード
            /// </summary>
            public static int AttackMode { get; set; }

            /// <summary>
            /// 攻撃モード
            /// </summary>
            public static int WatchRank { get; set; }

            /// <summary>
            /// 攻撃モード
            /// </summary>
            public static int SallyCount { get; set; }

            /// <summary>
            /// 稼働開始時間
            /// </summary>
            public static DateTime TimeStart { get; set; }

            /// <summary>
            /// 稼働終了時間
            /// </summary>
            public static DateTime TimeEnd { get; set; }

            /// <summary>
            /// 設定読み込み
            /// </summary>
            public static void Load()
            {
                Url = Properties.Settings.Default.PromotionURL;
                WatchRank = Properties.Settings.Default.PromotionWatchRank;
                SallyCount = Properties.Settings.Default.PromotionSallyCount;

                try
                {
                    TimeStart = Properties.Settings.Default.PromotionTimeStart;
                    TimeEnd = Properties.Settings.Default.PromotionTimeEnd;
                }
                catch
                {
                    TimeStart = DateTime.MinValue;
                    TimeEnd = DateTime.MinValue;
                }
            }

            /// <summary>
            /// 設定保存
            /// </summary>
            public static void Save()
            {
                Properties.Settings.Default.PromotionURL = Url;
                Properties.Settings.Default.PromotionWatchRank = WatchRank;
                Properties.Settings.Default.PromotionSallyCount = SallyCount;
                Properties.Settings.Default.PromotionTimeStart = TimeStart;
                Properties.Settings.Default.PromotionTimeEnd = TimeEnd;
                Properties.Settings.Default.Save();
            }

        }

        /// <summary>
        /// G-Tacticsイベントの設定
        /// </summary>
        class GTactics
        {
            /// <summary>
            /// イベントのホームのURL
            /// </summary>
            public static string Url { get; set; }

            /// <summary>
            /// BEx1使用時のダメージ
            /// </summary>
            public static ulong BaseDamage { get; set; }

            /// <summary>
            /// 探索する敵数の条件
            /// </summary>
            public static ulong EnemyCount { get; set; }

            /// <summary>
            /// 攻撃モード
            /// </summary>
            public static int AttackMode { get; set; }

            /// <summary>
            /// 報酬をまとめて受け取る個数
            /// </summary>
            public static int ReceiveCount { get; set; }

            /// <summary>
            /// 敵を攻撃しない
            /// </summary>
            public static bool OnlySearch { get; set; }

            /// <summary>
            /// 探索しない
            /// </summary>
            public static bool NoSearch { get; set; }

            /// <summary>
            /// 報酬を受け取る
            /// </summary>
            public static bool ReceiveReword { get; set; }

            /// <summary>
            /// プレゼントを受け取る
            /// </summary>
            public static bool ReceivePresent { get; set; }

            /// <summary>
            /// 戦略拠点を攻撃する
            /// </summary>
            public static bool EnableStrategicArea { get; set; }

            /// <summary>
            /// 右列を攻撃する
            /// </summary>
            public static bool EnableRightArea { get; set; }

            /// <summary>
            /// 中央列を攻撃する
            /// </summary>
            public static bool EnableCenterArea { get; set; }

            /// <summary>
            /// 左列を攻撃する
            /// </summary>
            public static bool EnableLeftArea { get; set; }

            /// <summary>
            /// 戦略拠点の目標レベル
            /// </summary>
            public static int StrategicArea { get; set; }

            /// <summary>
            /// エリアC3の目標レベル
            /// </summary>
            public static int ShieldC3 { get; set; }

            /// <summary>
            /// エリアC2の目標レベル
            /// </summary>
            public static int ShieldC2 { get; set; }

            /// <summary>
            /// エリアC1の目標レベル
            /// </summary>
            public static int ShieldC1 { get; set; }

            /// <summary>
            /// エリアB3の目標レベル
            /// </summary>
            public static int ShieldB3 { get; set; }

            /// <summary>
            /// エリアB2の目標レベル
            /// </summary>
            public static int ShieldB2 { get; set; }

            /// <summary>
            /// エリアB1の目標レベル
            /// </summary>
            public static int ShieldB1 { get; set; }

            /// <summary>
            /// エリアA3の目標レベル
            /// </summary>
            public static int ShieldA3 { get; set; }

            /// <summary>
            /// エリアA2の目標レベル
            /// </summary>
            public static int ShieldA2 { get; set; }

            /// <summary>
            /// エリアA1の目標レベル
            /// </summary>
            public static int ShieldA1 { get; set; }

            /// <summary>
            /// エ戦略拠点の優先度
            /// </summary>
            public static int Priority { get; set; }

            /// <summary>
            /// フォースを使用する
            /// </summary>
            public static bool UseForce { get; set; }

            /// <summary>
            /// フォースチャージを使用する
            /// </summary>
            public static bool ForceCharge { get; set; }

            /// <summary>
            /// 戦略拠点のフォース使用パターン
            /// </summary>
            public static int ForcePattern { get; set; }

            /// <summary>
            /// 待機する敵部隊との点数差
            /// </summary>
            public static ulong PointDiff { get; set; }

            /// <summary>
            /// 目標エリアレベル、敵部隊との点数差を満たしたら待機する
            /// </summary>
            public static bool Standby { get; set; }

            /// <summary>
            /// 戦略拠点のフォース使用前の待機時間
            /// </summary>
            public static double WaitForce { get; set; }

            /// <summary>
            /// 探索フォースを使用する最大敵数
            /// </summary>
            public static ulong SearchForce { get; set; }

            /// <summary>
            /// 探索フォースを左にセット
            /// </summary>
            public static bool SearchForceLeft { get; set; }

            /// <summary>
            /// 探索フォースを中央にセット
            /// </summary>
            public static bool SearchForceCenter { get; set; }

            /// <summary>
            /// 探索フォースを右にセット
            /// </summary>
            public static bool SearchForceRight { get; set; }

            /// <summary>
            /// 稼働開始時間
            /// </summary>
            public static DateTime TimeStart { get; set; }

            /// <summary>
            /// 稼働終了時間
            /// </summary>
            public static DateTime TimeEnd { get; set; }

            /// <summary>
            /// 設定読み込み
            /// </summary>
            public static void Load()
            {
                Url = Properties.Settings.Default.GTacticsURL;
                BaseDamage = Properties.Settings.Default.GTacticsBaseDamage;
                EnemyCount = Properties.Settings.Default.GTacticsEnemyCount;
                AttackMode = Properties.Settings.Default.GTacticsAttackMode;
                ReceiveCount = Properties.Settings.Default.GTacticsReceiveCount;
                OnlySearch = Properties.Settings.Default.GTacticsOnlySearch;
                NoSearch = Properties.Settings.Default.GTacticsNoSearch;
                ReceiveReword = Properties.Settings.Default.GTacticsReceiveReword;
                ReceivePresent = Properties.Settings.Default.GTacticsReceivePresent;
                EnableStrategicArea = Properties.Settings.Default.GTacticsEnableStrategicArea;
                EnableRightArea = Properties.Settings.Default.GTacticsEnableRightArea;
                EnableCenterArea = Properties.Settings.Default.GTacticsEnableCenterArea;
                EnableLeftArea = Properties.Settings.Default.GTacticsEnableLeftArea;
                StrategicArea = Properties.Settings.Default.GTacticsStrategicArea;
                ShieldC3 = Properties.Settings.Default.GTacticsShieldC3;
                ShieldC2 = Properties.Settings.Default.GTacticsShieldC2;
                ShieldC1 = Properties.Settings.Default.GTacticsShieldC1;
                ShieldB3 = Properties.Settings.Default.GTacticsShieldB3;
                ShieldB2 = Properties.Settings.Default.GTacticsShieldB2;
                ShieldB1 = Properties.Settings.Default.GTacticsShieldB1;
                ShieldA3 = Properties.Settings.Default.GTacticsShieldA3;
                ShieldA2 = Properties.Settings.Default.GTacticsShieldA2;
                ShieldA1 = Properties.Settings.Default.GTacticsShieldA1;
                Priority = Properties.Settings.Default.GTacticsPriority;
                UseForce = Properties.Settings.Default.GTacticsUseForce;
                ForceCharge = Properties.Settings.Default.GTacticsForceCharge;
                ForcePattern = Properties.Settings.Default.GTacticsForcePattern;
                PointDiff = Properties.Settings.Default.GTacticsPointDiff;
                Standby = Properties.Settings.Default.GTacticsStandby;
                WaitForce = Properties.Settings.Default.GTacticsWaitForce;
                SearchForce = Properties.Settings.Default.GTacticsSearchForce;
                SearchForceLeft = Properties.Settings.Default.GTacticsSearchForceLeft;
                SearchForceCenter = Properties.Settings.Default.GTacticsSearchForceCenter;
                SearchForceRight = Properties.Settings.Default.GTacticsSearchForceRight;

                try
                {
                    TimeStart = Properties.Settings.Default.GTacticsTimeStart;
                    TimeEnd = Properties.Settings.Default.GTacticsTimeEnd;
                }
                catch
                {
                    TimeStart = DateTime.MinValue;
                    TimeEnd = DateTime.MinValue;
                }
            }

            /// <summary>
            /// 設定保存
            /// </summary>
            public static void Save()
            {
                Properties.Settings.Default.GTacticsURL = Url;
                Properties.Settings.Default.GTacticsBaseDamage = BaseDamage;
                Properties.Settings.Default.GTacticsEnemyCount = EnemyCount;
                Properties.Settings.Default.GTacticsAttackMode = AttackMode;
                Properties.Settings.Default.GTacticsReceiveCount = ReceiveCount;
                Properties.Settings.Default.GTacticsOnlySearch = OnlySearch;
                Properties.Settings.Default.GTacticsNoSearch = NoSearch;
                Properties.Settings.Default.GTacticsReceiveReword = ReceiveReword;
                Properties.Settings.Default.GTacticsReceivePresent = ReceivePresent;
                Properties.Settings.Default.GTacticsEnableStrategicArea = EnableStrategicArea;
                Properties.Settings.Default.GTacticsEnableRightArea = EnableRightArea;
                Properties.Settings.Default.GTacticsEnableCenterArea = EnableCenterArea;
                Properties.Settings.Default.GTacticsEnableLeftArea = EnableLeftArea;
                Properties.Settings.Default.GTacticsStrategicArea = StrategicArea;
                Properties.Settings.Default.GTacticsShieldC3 = ShieldC3;
                Properties.Settings.Default.GTacticsShieldC2 = ShieldC2;
                Properties.Settings.Default.GTacticsShieldC1 = ShieldC1;
                Properties.Settings.Default.GTacticsShieldB3 = ShieldB3;
                Properties.Settings.Default.GTacticsShieldB2 = ShieldB2;
                Properties.Settings.Default.GTacticsShieldB1 = ShieldB1;
                Properties.Settings.Default.GTacticsShieldA3 = ShieldA3;
                Properties.Settings.Default.GTacticsShieldA2 = ShieldA2;
                Properties.Settings.Default.GTacticsShieldA1 = ShieldA1;
                Properties.Settings.Default.GTacticsPriority = Priority;
                Properties.Settings.Default.GTacticsUseForce = UseForce;
                Properties.Settings.Default.GTacticsForceCharge = ForceCharge;
                Properties.Settings.Default.GTacticsForcePattern = ForcePattern;
                Properties.Settings.Default.GTacticsPointDiff = PointDiff;
                Properties.Settings.Default.GTacticsStandby = Standby;
                Properties.Settings.Default.GTacticsWaitForce = WaitForce;
                Properties.Settings.Default.GTacticsSearchForce = SearchForce;
                Properties.Settings.Default.GTacticsSearchForceLeft = SearchForceLeft;
                Properties.Settings.Default.GTacticsSearchForceCenter = SearchForceCenter;
                Properties.Settings.Default.GTacticsSearchForceRight = SearchForceRight;
                Properties.Settings.Default.GTacticsTimeStart = TimeStart;
                Properties.Settings.Default.GTacticsTimeEnd = TimeEnd;
                Properties.Settings.Default.Save();
            }
        }
    }
}
