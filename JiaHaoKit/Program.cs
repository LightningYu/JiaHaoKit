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
            // // 1. 播放嵌入的MP3
            PlayEmbeddedMusic();

            //2.打开CMD窗口
            OpenCmdWindows();

            // 3. 打开股票网站
            OpenStockWebsite("https://geektyper.com/");
            OpenStockWebsite("https://legulegu.com/mock-trading/a-share");
            //hacker

            Console.WriteLine("套件启动完成！");
            Console.ReadKey();
        }

        static void PlayEmbeddedMusic()
        {
            try
            {
                // 从嵌入资源中提取MP3到临时文件
                Console.WriteLine("正在播放神曲...");
                Thread thread = new Thread(() =>
                {
                    PlayEmbeddedMp3("JiaHaoKit.Resources.jiahao.mp3");
                });
                thread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"播放失败: {ex.Message}");
            }
        }



        static void OpenCmdWindows()
        {
            // 直接打开多个CMD窗口
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
        // 单方法实现：播放嵌入式MP3资源
        static void PlayEmbeddedMp3(string resourceName)
        {
            try
            {
                // 从程序集获取资源流
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        Console.WriteLine($"错误：找不到资源 '{resourceName}'");
                        return;
                    }
                    // 初始化播放器并播放
                    using var reader = new Mp3FileReader(stream);
                    using (var outputDevice = new WaveOutEvent())
                    {
                        outputDevice.Init(reader);
                        outputDevice.Play(); // 异步执行

                        while (outputDevice.PlaybackState == PlaybackState.Playing)
                        {
                            Thread.Sleep(1000);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"播放失败：{ex.Message}");
            }
        }
    }
}