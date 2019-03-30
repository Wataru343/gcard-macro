using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace gcard_macro
{
    class Cui
    {
        private Event EventObject { get; set; }
        private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private object Obj { get; set; }
        public event Event.LogHandler Log;
        private string UserName { get; set; }
        private IWebDriver Driver { get; set; }

        public Cui()
        {
            Obj = new object();
            Log += OnLog;
        }

        public void Run<T>() where T : Event
        {
            try
            {
                Log?.Invoke(this, "ログイン中");

                Driver = Webdriver.CreateHtmlAgilityPackDriver();

                if (Driver.Url != "http://gcc.sp.mbga.jp/_gcard_my_room")
                {
                    Log?.Invoke(this, "ログインに失敗しました");
                    return;
                }

                try
                {
                    Driver.Navigate().GoToUrl(Driver.FindElement(By.XPath("//a[@class=\"profile\"]")).GetAttribute("href"));
                    UserName = Driver.FindElement(By.XPath("//div[@class=\"name-and-rank\"]")).Text.Split(new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries)[0];

                    try
                    {
                        UserName = UserName.Substring(0, UserName.LastIndexOf(' ')).Trim(new char[] { ' ' });
                    }
                    catch { }


                    Log?.Invoke(this, string.Format("ユーザー名取得({0})", UserName));
                }
                catch
                {
                    Log?.Invoke(this, "ユーザー名取得失敗");
                }
            }
            catch (Exception)
            {
                Log?.Invoke(this, "Chromeの起動に失敗しました");
                return;
            }


#if !DEBUG
            //シリアルキーチェック
            if (Properties.Settings.Default.AccessKey != KeyGenerator.Hash.GenerateHash(UserName))
            {

                string str = Microsoft.VisualBasic.Interaction.InputBox("", "シリアルキーを入力してください", "", -1, -1);
                
                if(str != KeyGenerator.Hash.GenerateHash(""))
                {
                    Log?.Invoke(this, "シリアルキーが正しくありません");
                    return;
                }

                Properties.Settings.Default.AccessKey = str;
                Properties.Settings.Default.Save();
            }
#endif


#if !DEBUG
            if (Webdriver.IsChrome())
            {
                Webdriver.Close();
                Driver = Webdriver.CreateHtmlAgilityPackDriver();
            }
#endif

            if (Webdriver.IsOoen())
            {
                Setting.Settings.Load();

                EventObject = EventManager.Create<T>(Driver);

                if (!Uri.IsWellFormedUriString(EventObject.HomePath, UriKind.Absolute))
                {
                    Log?.Invoke(this, "URLが正しい形式ではありません");
                    return;
                }

                switch (EventObject)
                {
                    case Raid e: break;
                    case Group e: break;
                    case GShooting e: break;
                    case ShootingRange e:
                        e.AutoStopped += OnAutoStop;
                        break;
                    case Promotion e: break;
                    case GTactics e: break;
                    default:
                        Log?.Invoke(this, "イベントオブジェクトの生成に失敗しました。");
                        return;
                }

                EventObject.Log += OnLog;

                Log?.Invoke(this, "マクロ初期化完了");

                EventObject.CreateThread();
            }
            else
            {
                Log?.Invoke(this, "ブラウザが起動していません");

                return;
            }


            System.Timers.Timer TimerRecievePresent = null;

            if (Properties.Settings.Default.UseCycleReceive)
            {
                TimeSpan CycleRecieveTime = TimeSpan.FromHours(Properties.Settings.Default.CycleRecieveTime);


                if ((int)CycleRecieveTime.TotalMilliseconds > 0)
                {
                    TimerRecievePresent = new System.Timers.Timer();
                    TimerRecievePresent.Elapsed += (sender, e) => EventObject?.SendRecievePresentRequest();
                    TimerRecievePresent.Interval = CycleRecieveTime.TotalMilliseconds;
                    TimerRecievePresent.Start();
                }
            }

            while (EventObject.IsRun)
            {
                System.Threading.Thread.Sleep(200);
            }
        }

        private void OnLog(object sender, string text)
        {
            lock (Obj)
            {
                Console.WriteLine(string.Format("{0}: {1}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff"), text));
                Logger.Info(text);
            }
        }

        private void OnAutoStop(object sender) => Log?.Invoke(this, "射撃を終了しました。");
    }
}
