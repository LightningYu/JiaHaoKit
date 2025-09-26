using NAudio.Wave;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace JiaHaoKit
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "嘉豪套件";
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Made by BiliBili 雷霆宇宇侠");
            Console.WriteLine("https://space.bilibili.com/1938216007");
            Console.WriteLine("正在启动...");

            // 1. 播放嵌入的MP3
            PlayEmbeddedMusic();

            //2.打开CMD窗口
            OpenCmdWindows();

            // 3. 打开股票网站
            OpenStockWebsite("https://geektyper.com/");

            // 4. 打开黑客网站
            OpenStockWebsite("https://legulegu.com/mock-trading/a-share");

            // 5. 改壁纸
            ChangeWallpaper();

            Console.WriteLine("套件启动完成！");
            Console.ReadKey();
        }
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        static void ChangeWallpaper()
        {
            string tempPath = null;

            try
            {
                // 创建临时文件
                // 生成临时文件路径（保留原始文件格式，假设是jpg）
                tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".jpg");

                // 直接将字节数组写入文件（不经过Image处理）
                File.WriteAllBytes(tempPath, Properties.Resources.wallpaper);

                // 验证文件是否有效
                if (new FileInfo(tempPath).Length == 0)
                {
                    Console.WriteLine("错误：临时文件生成失败");
                    return;
                }

                // 应用壁纸
                SystemParametersInfo(
                    20,        // 设置桌面壁纸
                    0,            // 保留默认值
                    tempPath,      // 图片路径
                    1   // 更新注册表并广播更改
                );

            }
            catch (Exception ex)
            {
                Console.WriteLine($"错误: {ex.Message}");
            }
            finally
            {
                // 清理临时文件
                if (!string.IsNullOrEmpty(tempPath) && File.Exists(tempPath))
                {
                    new Thread(() =>
                    {
                        File.Delete(tempPath);
                    }).Start();
                }
            }
        }

        static void PlayEmbeddedMusic()
        {
            try
            {
                Console.WriteLine("正在播放神人曲目...");
                new Thread(() =>
                {
                    PlayEmbeddedMp3("JiaHaoKit.Resources.jiahao.mp3");
                }).Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"播放失败: {ex.Message}");
            }
        }

        static void OpenCmdWindows()
        {
            for (int i = 1; i <= 8; i++)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/k title 嘉豪窗口{i} && color 0A && echo 扫描中... && C: && dir /s",
                    UseShellExecute = true
                });
                Console.WriteLine($"已打开窗口 {i}");
            }
        }

        static void OpenStockWebsite(string websiteUrl)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = websiteUrl,
                UseShellExecute = true
            });
        }
        static void PlayEmbeddedMp3(string resourceName)
        {
            try
            {
                using Stream stream = JiaHaoKit.Properties.Resources.jiahao;
                if (stream == null)
                {
                    Console.WriteLine($"错误：找不到资源 '{resourceName}'");
                    return;
                }
                // 初始化播放器并播放
                using Mp3FileReader reader = new(stream);
                using WaveOutEvent outputDevice = new();
                outputDevice.Init(reader);
                outputDevice.Play(); // 异步执行

                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"播放失败：{ex.Message}");
            }
        }
    }
}