﻿using GlobalLowLevelHooks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GlobalLowLevelHooks.MouseHook;

namespace RCS
{
    public partial class Form1 : Form
    {
        MouseHook mouseHook;
        bool isLeftDown = false;
        bool isRightDown = false;
        Thread xThread;
        Thread yThread;
        List<Config> configs = new List<Config>();
        Config currentConfig;
        bool isOpne = true;

        IniManager iniManager = new IniManager($"{Environment.CurrentDirectory}\\config.ini");

        GlobalKeyboardHook gHook;
        int kv;//將keyValue轉成整數用的變數

        public Form1()
        {
            InitializeComponent();

            // Create the Mouse Hook
            mouseHook = new MouseHook();
            // Capture the events
            mouseHook.LeftButtonDown += new MouseHookCallback(LeftMouseDown);
            mouseHook.LeftButtonUp += new MouseHookCallback(LeftMouseUp);
            mouseHook.RightButtonDown += new MouseHookCallback(RightMouseDown);
            mouseHook.RightButtonUp += new MouseHookCallback(RightMouseUp);
            //Installing the Mouse Hooks
            mouseHook.Install();

            //init configs
            configs.Add(new Config());
            configs.Add(new Config());
            configs.Add(new Config());

            //init current config
            currentConfig = configs[0];

            ttbWeaponConfigPath.Text = iniManager.ReadIniFile("Path", "WeaponConfig", ttbWeaponConfigPath.Text);

            gHook = new GlobalKeyboardHook(); //根據作者的程式碼(class)創造一個新物件
            gHook.KeyDown += new KeyEventHandler(gHook_KeyDown);// 連結KeyDown事件
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                gHook.HookedKeys.Add(key);
            gHook.hook();//開始監控

            //若程式目標沒有config檔案則自行建立
            if (!System.IO.File.Exists($"{Environment.CurrentDirectory}\\config.ini"))
            {
                var newConfig = System.IO.File.Create($"{Environment.CurrentDirectory}\\config.ini");
                newConfig.Close();
                newConfig.Dispose();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LeftMouseDown(MSLLHOOKSTRUCT mouseStruct)
        {
            isLeftDown = true;
            ThreadClear();
            if (isOpne && (currentConfig.Xconfig.Count > 0 && currentConfig.Yconfig.Count > 0))
                ThreadInit();
        }

        private void LeftMouseUp(MSLLHOOKSTRUCT mouseStruct)
        {
            isLeftDown = false;
            ThreadClear();
        }

        private void RightMouseDown(MSLLHOOKSTRUCT mouseStruct)
        {
            isRightDown = true;
            ThreadClear();
            if (isOpne && (currentConfig.Xconfig.Count > 0 && currentConfig.Yconfig.Count > 0))
                ThreadInit();
        }

        private void RightMouseUp(MSLLHOOKSTRUCT mouseStruct)
        {
            isRightDown = false;
            ThreadClear();
        }

        private void ThreadInit()
        {
            xThread = new Thread(() =>
            {
                bool switchFlag = false;
                while (isLeftDown && isRightDown)
                {
                    RelativeMove(currentConfig.Xconfig[Convert.ToInt32(switchFlag)].Offset, 0);
                    Thread.Sleep(currentConfig.Xconfig[Convert.ToInt32(switchFlag)].Rate);
                    switchFlag = !switchFlag;
                }
            });
            xThread.Start();

            yThread = new Thread(() =>
            {
                var switchFlag = false;
                while (isLeftDown && isRightDown)
                {
                    RelativeMove(0, currentConfig.Yconfig[Convert.ToInt32(switchFlag)].Offset);
                    Thread.Sleep(currentConfig.Yconfig[Convert.ToInt32(switchFlag)].Rate);
                    switchFlag = !switchFlag;
                }
            });
            yThread.Start();
        }

        private void ThreadClear()
        {
            if (xThread != null)
            {
                xThread.Abort();
            }
            xThread = null;

            if (yThread != null)
            {
                yThread.Abort();
            }
            yThread = null;
        }

        //把按下按鍵後要觸發的事件寫在這裡
        public void gHook_KeyDown(object sender, KeyEventArgs e)
        {
            kv = e.KeyValue;//把按下的按鍵號碼轉成整數存在kv中

            lblKeyValueView.Text = $"目前按下的按鍵號碼：{kv}";

            switch (kv)
            {
                case 49://按下1
                    {
                        currentConfig = configs[0];
                        lblCurrnetConfig.Text = "主武器";
                        break;
                    }
                case 50://按下2
                    {
                        currentConfig = configs[1];
                        lblCurrnetConfig.Text = "副武器";
                        break;
                    }
                case 51://按下3
                    {
                        currentConfig = configs[2];
                        lblCurrnetConfig.Text = "腎上腺素";
                        break;
                    }
            }
        }

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        public static void RelativeMove(int relx, int rely)
        {
            mouse_event(0x0001, relx, rely, 0, 0);
        }

        private void ttbWeaponConfigPath_TextChanged(object sender, EventArgs e)
        {
            iniManager.WriteIniFile("Path", "WeaponConfig", ttbWeaponConfigPath.Text);
        }

        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = ttbWeaponConfigPath.Text;
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            foreach (var item in openFileDialog1.FileNames)
            {
                IniManager loadedConfig = new IniManager(item);
                int x1 = Convert.ToInt32(loadedConfig.ReadIniFile("config", "x1", "0"));
                int x2 = Convert.ToInt32(loadedConfig.ReadIniFile("config", "x2", "0"));
                int xRate1 = Convert.ToInt32(loadedConfig.ReadIniFile("config", "xRate1", "1"));
                int xRate2 = Convert.ToInt32(loadedConfig.ReadIniFile("config", "xRate2", "1"));
                int y1 = Convert.ToInt32(loadedConfig.ReadIniFile("config", "y1", "0"));
                int y2 = Convert.ToInt32(loadedConfig.ReadIniFile("config", "y2", "0"));
                int yRate1 = Convert.ToInt32(loadedConfig.ReadIniFile("config", "yRate1", "1"));
                int yRate2 = Convert.ToInt32(loadedConfig.ReadIniFile("config", "yRate2", "1"));

                if (Convert.ToBoolean(loadedConfig.ReadIniFile("type", "forFinka", "false")))
                {
                    configs[2].Xconfig.Clear();
                    configs[2].Xconfig.Add(new ConfigDetail { Offset = x1, Rate = xRate1 });
                    configs[2].Xconfig.Add(new ConfigDetail { Offset = x2, Rate = xRate2 });

                    configs[2].Yconfig.Clear();
                    configs[2].Yconfig.Add(new ConfigDetail { Offset = y1, Rate = yRate1 });
                    configs[2].Yconfig.Add(new ConfigDetail { Offset = y2, Rate = yRate2 });
                }
                else if (Convert.ToBoolean(loadedConfig.ReadIniFile("type", "isMain", "true")))
                {
                    configs[0].Xconfig.Clear();
                    configs[0].Xconfig.Add(new ConfigDetail { Offset = x1, Rate = xRate1 });
                    configs[0].Xconfig.Add(new ConfigDetail { Offset = x2, Rate = xRate2 });

                    configs[0].Yconfig.Clear();
                    configs[0].Yconfig.Add(new ConfigDetail { Offset = y1, Rate = yRate1 });
                    configs[0].Yconfig.Add(new ConfigDetail { Offset = y2, Rate = yRate2 });
                    lblMainConfigName.Text = $"主：{item.Substring(item.LastIndexOf('\\') + 1).Replace(".ini", "")}";
                }
                else
                {
                    configs[1].Xconfig.Clear();
                    configs[1].Xconfig.Add(new ConfigDetail { Offset = x1, Rate = xRate1 });
                    configs[1].Xconfig.Add(new ConfigDetail { Offset = x2, Rate = xRate2 });

                    configs[1].Yconfig.Clear();
                    configs[1].Yconfig.Add(new ConfigDetail { Offset = y1, Rate = yRate1 });
                    configs[1].Yconfig.Add(new ConfigDetail { Offset = y2, Rate = yRate2 });
                    lblSecConfigName.Text = $"副：{item.Substring(item.LastIndexOf('\\') + 1).Replace(".ini", "")}";
                }
            }
        }
    }
}
