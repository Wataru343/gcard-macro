using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace gcard_macro
{
    class Webdriver
    {
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int SW_HIDE = 0;

        private static ChromeDriver driver_ = null;
        private Webdriver()
        {

        }

        static public ChromeDriver Instance
        {
            get
            {
                if (IsOoen()) return driver_;

                try
                {
                    //ユーザデータフォルダ作成
                    string path = Path.Combine(System.Environment.CurrentDirectory, "userdata");
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    ChromeDriverService driverService = ChromeDriverService.CreateDefaultService();
                    driverService.HideCommandPromptWindow = true;

                    //オプションでユーザーデータフォルダとユーザーエージェント指定
                    ChromeOptions options = new ChromeOptions();
                    options.AddArgument(@"--user-data-dir=" + path);
                    options.AddArgument("--user-agent=\"Mozilla /5.0 (iPhone; CPU iPhone OS 9_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13B5110e Safari/601.1");

                    driver_ = driver_ ?? new ChromeDriver(driverService, options);
                    driver_.Manage().Window.Size = new System.Drawing.Size(600, 800);
                    driver_.Navigate().GoToUrl("http://gcc.sp.mbga.jp/_gcard_my_room");
#if !DEBUG
                    //ログイン待機
                    if (driver_.Url != "http://gcc.sp.mbga.jp/_gcard_my_room")
                    {
                        new FormLogin() { driver = driver_ }.ShowDialog();
                    }

                    //一度example.comに移動してウィンドウハンドルを取得
                    driver_.Navigate().GoToUrl("http://example.com/");
                    IntPtr[] hwnd = System.Diagnostics.Process.GetProcesses().Where(e => e.MainWindowTitle.IndexOf("Example Domain") >= 0).Select(e => e.MainWindowHandle).ToArray();
                    driver_.Navigate().GoToUrl("http://gcc.sp.mbga.jp/_gcard_my_room");
                    ShowWindow(hwnd[0], SW_HIDE);
#endif

                    return driver_;
                }
                catch { }

                return driver_;
            }
        }

        static public void Close()
        {
            try
            {
                driver_.Close();
                driver_.Quit();
            }
            catch
            {
                try
                {
                    driver_.Quit();
                }
                catch { }
            }
            finally
            {
                driver_ = null;
            }
        }

        static public bool IsOoen() => driver_ != null;
    }
}
