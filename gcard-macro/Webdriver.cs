using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using gcard_macro.WebDriber;

namespace gcard_macro
{
    class Webdriver
    {
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int SW_HIDE = 0;

        private static volatile IWebDriver driver_ = null;
        
        private Webdriver()
        {

        }

        static public bool IsBooting;

        static public IWebDriver Instance
        {
            get
            {
                return driver_;
            }
        }

        static public IWebDriver CreateHtmlAgilityPackDriver()
        {
            if (IsOoen()) return driver_;

            IsBooting = true;

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

                //ログイン待機
                if (driver_.Url != "http://gcc.sp.mbga.jp/_gcard_my_room")
                {
                    new FormLogin() { driver = driver_ }.ShowDialog();
                }

                #if !CHROME
                var cookies = driver_.Manage().Cookies.AllCookies;

                driver_.Quit();

                driver_ = new HtmlAgilityPackDriver("Mozilla /5.0 (iPhone; CPU iPhone OS 9_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13B5110e Safari/601.1");

                foreach (var c in cookies)
                {
                    try
                    {
                        driver_.Manage().Cookies.AddCookie(c);
                    }
                    catch { }
                }
                driver_.Navigate().GoToUrl("http://gcc.sp.mbga.jp/_gcard_my_room");
                #endif

                IsBooting = false;

                return driver_;
            }
            catch
            {
                driver_ = null;
            }

            IsBooting = false;

            return driver_;
        }


        static public IWebDriver CreateChrome()
        {
            if (IsOoen()) return driver_;

            IsBooting = true;

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
                //options.AddArgument(@"--ignore-certificate-errors");
                //options.AddArgument(@"--allow-running-insecure-content");
                //options.AddArgument(@"--disable-web-security");
                //options.AddArgument(@"--disable-desktop-notifications");
                //options.AddArgument(@"--disable-extensions");
                //options.AddArgument(@"--blink-settings=imagesEnabled=false");

                driver_ = driver_ ?? new ChromeDriver(driverService, options);

                driver_.Manage().Window.Size = new System.Drawing.Size(600, 800);
                driver_.Navigate().GoToUrl("http://gcc.sp.mbga.jp/_gcard_my_room");

                //ログイン待機
                if (driver_.Url != "http://gcc.sp.mbga.jp/_gcard_my_room")
                {
                    new FormLogin() { driver = driver_ }.ShowDialog();
                }

#if !DEBUG
                //一度example.comに移動してウィンドウハンドルを取得
                driver_.Navigate().GoToUrl("http://example.com/");
                IntPtr[] hwnd = System.Diagnostics.Process.GetProcesses().Where(e => e.MainWindowTitle.IndexOf("Example Domain") >= 0).Select(e => e.MainWindowHandle).ToArray();
                driver_.Navigate().GoToUrl("http://gcc.sp.mbga.jp/_gcard_my_room");
                ShowWindow(hwnd[0], SW_HIDE);
#endif

                IsBooting = false;

                return driver_;
            }
            catch
            {
                driver_ = null;
            }

            IsBooting = false;

            return driver_;
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

        static public bool IsChrome() => driver_ is ChromeDriver;
        static public bool IsHtmlAgilityPackDriver() => driver_ is HtmlAgilityPackDriver;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        static public bool IsOoen() => driver_ != null;
    }
}
