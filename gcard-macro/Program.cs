using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gcard_macro
{
    static class Program
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int dwProcessId);

        private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Logger.Info("===== Program Start =====");

            if (args.ToList().IndexOf("--nogui") >= 0)
            {
                CreateConsole();

                Type mode = null;

                foreach (var arg in args)
                {
                    switch (arg)
                    {
                        case "--raid": mode = typeof(Raid); break;
                        case "--group": mode = typeof(Group); break;
                        case "--gshooting": mode = typeof(GShooting); ; break;
                        case "--shootingrange": mode = typeof(ShootingRange); break;
                        case "--promotion": mode = typeof(Promotion); break;
                        case "--gtactics": mode = typeof(GTactics); break;
                        default: break;
                    }

                    if (mode != null) break; 
                }

                if (mode == null)
                {
                    Console.WriteLine(
                        "番号を入力してモードを選択してください" + Environment.NewLine +
                        "0.レイド" + Environment.NewLine +
                        "1.部隊戦" + Environment.NewLine +
                        "2.G-Shooting" + Environment.NewLine +
                        "3.G-Shooting(射撃場)" + Environment.NewLine +
                        "4.昇格戦" + Environment.NewLine +
                        "5.G-Tactics");

                    string input = Console.ReadLine();

                    Console.WriteLine("");

                    switch (input)
                    {
                        case "0": mode = typeof(Raid); break;
                        case "1": mode = typeof(Group); break;
                        case "2": mode = typeof(GShooting); break;
                        case "3": mode = typeof(ShootingRange); break;
                        case "4": mode = typeof(Promotion); break;
                        case "5": mode = typeof(GTactics); break;
                        default: Console.WriteLine("入力が正しくありません"); break;
                    }

                    Console.WriteLine("");
                }

                if (mode == typeof(Raid))
                {
                    Console.WriteLine("レイドで開始します" + Environment.NewLine);
                    new Cui().Run<Raid>();
                }
                else if (mode == typeof(Group))
                {
                    Console.WriteLine("部隊戦で開始します" + Environment.NewLine);
                    new Cui().Run<Group>();
                }
                else if (mode == typeof(GShooting))
                {
                    Console.WriteLine("G-Shootingで開始します" + Environment.NewLine);
                    new Cui().Run<GShooting>();
                }
                else if (mode == typeof(ShootingRange))
                {
                    Console.WriteLine("G-Shooting(射撃場)で開始します" + Environment.NewLine);
                    new Cui().Run<ShootingRange>();
                }
                else if (mode == typeof(Promotion))
                {
                    Console.WriteLine("昇格戦で開始します" + Environment.NewLine);
                    new Cui().Run<Promotion>();
                }
                else if (mode == typeof(GTactics))
                {
                    Console.WriteLine("G-Tacticsで開始します" + Environment.NewLine);
                    new Cui().Run<GTactics>();
                }
            }
            else
            {
#if DEBUG
                CreateConsole();
#endif
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
            }

            Logger.Info("===== Program End =====");
            Environment.Exit(0);
        }

        static void CreateConsole()
        {
            var attachConsole = AttachConsole(-1);

            if (attachConsole == false)
            {
                AllocConsole();
                System.IO.StreamWriter standard = new System.IO.StreamWriter(Console.OpenStandardOutput());
                standard.AutoFlush = true;
                Console.SetOut(standard);
            }
        }
    }
}
