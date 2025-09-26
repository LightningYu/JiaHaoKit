using System;
using System.Diagnostics;
using System.Reflection;
using NAudio.Wave;

namespace JiaHaoSimpleKit
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

            Console.WriteLine("套件启动完成！");
            Console.ReadKey();
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
                using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
                if (stream == null)
                {
                    Console.WriteLine($"错误：找不到资源 '{resourceName}'");
                    return;
                }
                // 初始化播放器并播放
                using Mp3FileReader reader = new Mp3FileReader(stream);
                using WaveOutEvent outputDevice = new WaveOutEvent();
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