using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace gcard_macro
{
    static class EventManager
    {
        static public T Create<T>(IWebDriver driver) where T : Event
        {
            if (typeof(T) == typeof(Raid)) return CreateRaid(driver) as T;
            else if (typeof(T) == typeof(Group)) return CreateGroup(driver) as T;
            else if (typeof(T) == typeof(GShooting)) return CreateGShooting(driver) as T;
            else if (typeof(T) == typeof(ShootingRange)) return CreateShootingRange(driver) as T;
            else if (typeof(T) == typeof(Promotion)) return CreatePromotion(driver) as T;
            else if (typeof(T) == typeof(GTactics)) return CreateGTactics(driver) as T;
            else return default(T);
        } 


        static public Raid CreateRaid(IWebDriver driver)
        {
            DateTime start, end;

            try
            {
                start = Setting.Raid.TimeStart;
                end = Setting.Raid.TimeEnd;
            }
            catch
            {
                start = DateTime.MinValue;
                end = DateTime.MinValue;
            }

            return new Raid(driver, Setting.Raid.Url)
            {
                WaitSearch = Setting.Common.WaitSearch,
                WaitBattle = Setting.Common.WaitBattle,
                WaitAttack = Setting.Common.WaitAttack,
                WaitReceive = Setting.Common.WaitReceive,
                WaitAccessBlock = Setting.Common.WaitAccessBlock,
                WaitMisc = Setting.Common.WaitMisc,
                JoinAssault = Setting.Raid.JoinAssault,
                UseAssaultBE = Setting.Raid.UseAssaultBE,
                Request = Setting.Raid.Request,
                BaseDamage = Setting.Raid.BaseDamage,
                EnemyCount = Setting.Raid.EnemyCount,
                Mode = (Event.AttackMode)Setting.Raid.AttackMode,
                ReceiveCount = Setting.Raid.ReceiveCount + 1,
                OnlySearch = Setting.Raid.OnlySearch,
                NoSearch = Setting.Raid.NoSearch,
                ReceiveReword = Setting.Raid.ReceiveReword,
                ReceivePresent = Setting.Raid.ReceivePresent,
                AimMVP = Setting.Raid.AimMVP,
                OnlyAttackAssultBoss = Setting.Raid.OnlyAttackAssultBoss,
                OnlyAttackAssultEnemy = Setting.Raid.OnlyAttackAssultEnemy,
                WaitRecieveAssult = Setting.Raid.WaitRecieveAssult,
                WaitAtackBattleShip = Setting.Raid.WaitAtackBattleShip,
                StartTime = start,
                EndTime = end,
                SampleCount = Setting.Common.OptimizedWaitEnable ? Setting.Common.OptimizedWaitEnemyCount : 0,
                EnterAdditionalQuest = Setting.Raid.EnterAdditionalQuest,
                PriorityAttackAssultEnemy = (Raid.AttackPriority)Setting.Raid.PriorityAttackAssultEnemy
            };
        }

        static public Group CreateGroup(IWebDriver driver)
        {
            DateTime start, end;

            try
            {
                start = Setting.Group.TimeStart;
                end = Setting.Group.TimeEnd;
            }
            catch
            {
                start = DateTime.MinValue;
                end = DateTime.MinValue;
            }

            return new Group(Webdriver.Instance, Setting.Group.Url)
            {
                WaitSearch = Setting.Common.WaitSearch,
                WaitBattle = Setting.Common.WaitBattle,
                WaitAttack = Setting.Common.WaitAttack,
                WaitReceive = Setting.Common.WaitReceive,
                WaitAccessBlock = Setting.Common.WaitAccessBlock,
                WaitMisc = Setting.Common.WaitMisc,
                BaseDamage = Setting.Group.BaseDamage,
                EnemyCount = Setting.Group.EnemyCount,
                PointDiff = Setting.Group.PointDiff,
                Mode = (Event.AttackMode)Setting.Group.AttackMode,
                ReceiveCount = Setting.Group.ReceiveCount + 1,
                UseBoost = Setting.Group.UseBoost,
                FirstAttackBoss = Setting.Group.FirstAttackBoss,
                ReceiveReword = Setting.Group.ReceiveReword,
                ReceivePresent = Setting.Group.ReceivePresent,
                AutojobLevelUp = Setting.Group.AutoJobLevelUp,
                OnlySearch = Setting.Group.OnlySearch,
                NoSearch = Setting.Group.NoSearch,
                FinalJob = Setting.Group.FinalJob,
                StartTime = start,
                EndTime = end,
                SampleCount = Setting.Common.OptimizedWaitEnable ? Setting.Common.OptimizedWaitEnemyCount : 0,
                Combo30 = new Combo30Button()
                {
                    Type = SpecialAttackButton.AttackType.Combo30,
                    Use = Setting.Group.UseCombo30,
                    Normal = Setting.Group.Combo30Normal,
                    Mira = Setting.Group.Combo30Mira,
                    Boost = Setting.Group.Combo30Boost,
                    FirstAttack = Setting.Group.Combo30FirstAttack
                },
                Attack20 = new Attack20Button()
                {
                    Type = SpecialAttackButton.AttackType.Attack20,
                    Use = Setting.Group.UseAttack20,
                    Normal = Setting.Group.Attack20Normal,
                    Mira = Setting.Group.Attack20Mira,
                    Boost = Setting.Group.Attack20Boost,
                    RequiredRatio = Setting.Group.Attack20RequiredRatio
                },
                Attack10 = new Attack10Button()
                {
                    Type = SpecialAttackButton.AttackType.Attack10,
                    Use = Setting.Group.UseAttack10,
                    Normal = Setting.Group.Attack10Normal,
                    Mira = false,
                    Boost = Setting.Group.Attack10Boost
                },
                BE1 = new BE1Button()
                {
                    Type = SpecialAttackButton.AttackType.BE1,
                    Use = Setting.Group.UseBE1,
                    Normal = Setting.Group.BE1Normal,
                    Mira = Setting.Group.BE1Mira,
                    Boost = true,
                    RequiredRatio = Setting.Group.BE1RequiredRatio
                }
            };
        }

        static public GShooting CreateGShooting(IWebDriver driver)
        {
            DateTime start, end;

            try
            {
                start = Setting.GShooting.TimeStart;
                end = Setting.GShooting.TimeEnd;
            }
            catch
            {
                start = DateTime.MinValue;
                end = DateTime.MinValue;
            }

            return new GShooting(Webdriver.Instance, Setting.GShooting.Url)
            {
                WaitSearch = Setting.Common.WaitSearch,
                WaitBattle = Setting.Common.WaitBattle,
                WaitAttack = Setting.Common.WaitAttack,
                WaitReceive = Setting.Common.WaitReceive,
                WaitAccessBlock = Setting.Common.WaitAccessBlock,
                WaitMisc = Setting.Common.WaitMisc,
                BaseDamage = Setting.GShooting.BaseDamage,
                EnemyCount = Setting.GShooting.EnemyCount,
                Mode = (Event.AttackMode)Setting.GShooting.AttackMode,
                ReceiveCount = Setting.GShooting.ReceiveCount + 1,
                ReceiveReword = Setting.GShooting.ReceiveReword,
                ReceivePresent = Setting.GShooting.ReceivePresent,
                OnlySearch = Setting.GShooting.OnlySearch,
                NoSearch = Setting.GShooting.NoSearch,
                Request = Setting.GShooting.Request,
                StartTime = start,
                EndTime = end,
                SampleCount = Setting.Common.OptimizedWaitEnable ? Setting.Common.OptimizedWaitEnemyCount : 0,
            };
        }

        static public ShootingRange CreateShootingRange(IWebDriver driver)
        {
            return new ShootingRange(Webdriver.Instance, Setting.ShootingRange.Url)
            {
                WaitSearch = Setting.Common.WaitSearch,
                WaitBattle = Setting.Common.WaitBattle,
                WaitAttack = Setting.Common.WaitAttack,
                WaitReceive = Setting.Common.WaitReceive,
                WaitAccessBlock = Setting.Common.WaitAccessBlock,
                WaitMisc = Setting.Common.WaitMisc,
                ThresholdFocusShot = Setting.ShootingRange.ThresholdFocusShot,
                UseFocusShotDuringFever = Setting.ShootingRange.UseFocusShotDuringFever,
                UseFeverTip = Setting.ShootingRange.UseFeverTip,
                AutoStop = Setting.ShootingRange.AutoStop
            };
        }

        static public Promotion CreatePromotion(IWebDriver driver)
        {
            DateTime start, end;

            try
            {
                start = Setting.Promotion.TimeStart;
                end = Setting.Promotion.TimeEnd;
            }
            catch
            {
                start = DateTime.MinValue;
                end = DateTime.MinValue;
            }

            return new Promotion(Webdriver.Instance, Setting.Promotion.Url)
            {
                WaitSearch = Setting.Common.WaitSearch,
                WaitBattle = Setting.Common.WaitBattle,
                WaitAttack = Setting.Common.WaitAttack,
                WaitReceive = Setting.Common.WaitReceive,
                WaitAccessBlock = Setting.Common.WaitAccessBlock,
                WaitMisc = Setting.Common.WaitMisc,
                Mode = (Event.AttackMode)Setting.Promotion.AttackMode + 3,
                WatchRank = Setting.Promotion.WatchRank,
                SallyCount = Setting.Promotion.SallyCount,
                SallyUnlimited = Setting.Promotion.SallyCount == 0 ? true : false,
                StartTime = start,
                EndTime = end
            };
        }

        static public GTactics CreateGTactics(IWebDriver driver)
        {
            DateTime start, end;

            try
            {
                start = Setting.GTactics.TimeStart;
                end = Setting.GTactics.TimeEnd;
            }
            catch
            {
                start = DateTime.MinValue;
                end = DateTime.MinValue;
            }

            return new GTactics(Webdriver.Instance, Setting.GTactics.Url)
            {
                WaitSearch = Setting.Common.WaitSearch,
                WaitBattle = Setting.Common.WaitBattle,
                WaitAttack = Setting.Common.WaitAttack,
                WaitReceive = Setting.Common.WaitReceive,
                WaitAccessBlock = Setting.Common.WaitAccessBlock,
                WaitMisc = Setting.Common.WaitMisc,
                BaseDamage = Setting.GTactics.BaseDamage,
                EnemyCount = Setting.GTactics.EnemyCount,
                Mode = (Event.AttackMode)Setting.GTactics.AttackMode,
                ReceiveCount = Setting.GTactics.ReceiveCount + 1,
                ReceiveReword = Setting.GTactics.ReceiveReword,
                ReceivePresent = Setting.GTactics.ReceivePresent,
                OnlySearch = Setting.GTactics.OnlySearch,
                NoSearch = Setting.GTactics.NoSearch,
                Shield = new List<GTactics.Area>()
                {
                    new GTactics.Area() { Level = Setting.GTactics.StrategicArea, Enable = Setting.GTactics.EnableStrategicArea },
                    new GTactics.Area() { Level = Setting.GTactics.ShieldC3, Enable = Setting.GTactics.EnableRightArea },
                    new GTactics.Area() { Level = Setting.GTactics.ShieldC2, Enable = Setting.GTactics.EnableCenterArea },
                    new GTactics.Area() { Level = Setting.GTactics.ShieldC1, Enable = Setting.GTactics.EnableLeftArea },
                    new GTactics.Area() { Level = Setting.GTactics.ShieldB3, Enable = Setting.GTactics.EnableRightArea },
                    new GTactics.Area() { Level = Setting.GTactics.ShieldB2, Enable = Setting.GTactics.EnableCenterArea },
                    new GTactics.Area() { Level = Setting.GTactics.ShieldB1, Enable = Setting.GTactics.EnableLeftArea },
                    new GTactics.Area() { Level = Setting.GTactics.ShieldA3, Enable = Setting.GTactics.EnableRightArea },
                    new GTactics.Area() { Level = Setting.GTactics.ShieldA2, Enable = Setting.GTactics.EnableCenterArea },
                    new GTactics.Area() { Level = Setting.GTactics.ShieldA1, Enable = Setting.GTactics.EnableLeftArea },
                },
                Priority = (GTactics.AreaPriority)Setting.GTactics.Priority,
                UseForce = Setting.GTactics.UseForce,
                ForceCharge = Setting.GTactics.ForceCharge,
                StrategyAreaForcePattern = (GTactics.ForcePattern)Setting.GTactics.ForcePattern,
                PointDiff = (long)Setting.GTactics.PointDiff,
                Standby = Setting.GTactics.Standby,
                WaitForce = Setting.GTactics.WaitForce,
                StartTime = start,
                EndTime = end,
                SampleCount = Setting.Common.OptimizedWaitEnable ? Setting.Common.OptimizedWaitEnemyCount : 0,
                SearchForceEnemyCount = Setting.GTactics.SearchForce,
                SearchForcePlace = new List<bool>()
                {
                    Setting.GTactics.SearchForceLeft,
                    Setting.GTactics.SearchForceCenter,
                    Setting.GTactics.SearchForceRight,
                }
            };
        }
    }
}
