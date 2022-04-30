using GlobalLowLevelHooks;
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
        Random random = new Random(Guid.NewGuid().GetHashCode());

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
            configs[0].Xconfig.Add(new ConfigDetail { Offset = 0, Rate = 100 });
            configs[0].Xconfig.Add(new ConfigDetail { Offset = 0, Rate = 100 });
            configs[0].Yconfig.Add(new ConfigDetail { Offset = 0, Rate = 100 });
            configs[0].Yconfig.Add(new ConfigDetail { Offset = 0, Rate = 100 });
            configs[1].Xconfig.Add(new ConfigDetail { Offset = 0, Rate = 100 });
            configs[1].Xconfig.Add(new ConfigDetail { Offset = 0, Rate = 100 });
            configs[1].Yconfig.Add(new ConfigDetail { Offset = 0, Rate = 100 });
            configs[1].Yconfig.Add(new ConfigDetail { Offset = 0, Rate = 100 });
            configs[2].Xconfig.Add(new ConfigDetail { Offset = 0, Rate = 100 });
            configs[2].Xconfig.Add(new ConfigDetail { Offset = 0, Rate = 100 });
            configs[2].Yconfig.Add(new ConfigDetail { Offset = 0, Rate = 100 });
            configs[2].Yconfig.Add(new ConfigDetail { Offset = 0, Rate = 100 });

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

            ThreadInit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LeftMouseDown(MSLLHOOKSTRUCT mouseStruct)
        {
            isLeftDown = true;
            //ThreadClear();
            if (isOpne && (currentConfig.Xconfig.Count > 0 && currentConfig.Yconfig.Count > 0) && (isLeftDown && isRightDown))
            {
                if (!xThread.IsAlive)
                {
                    xThread = new Thread(HandleMyThread);
                    xThread.IsBackground = true;
                    xThread.Start();
                }
            }
        }

        private void LeftMouseUp(MSLLHOOKSTRUCT mouseStruct)
        {
            isLeftDown = false;
            //ThreadClear();
        }

        private void RightMouseDown(MSLLHOOKSTRUCT mouseStruct)
        {
            isRightDown = true;
            //ThreadClear();
            if (isOpne && (currentConfig.Xconfig.Count > 0 && currentConfig.Yconfig.Count > 0) && (isLeftDown && isRightDown))
            {
                if (!xThread.IsAlive)
                {
                    xThread = new Thread(HandleMyThread);
                    xThread.IsBackground = true;
                    xThread.Start();
                }
            }
        }

        private void RightMouseUp(MSLLHOOKSTRUCT mouseStruct)
        {
            isRightDown = false;
            //ThreadClear();
        }

        private void HandleMyThread()
        {
            bool switchFlag = false;
            var randomMax = Convert.ToInt32(nudRandomMax.Value);
            var randomMin = Convert.ToInt32(nudRandomMin.Value);
            int count = 0;
            while (isLeftDown && isRightDown)
            {
                var xOffset = currentConfig.Xconfig[Convert.ToInt32(switchFlag)].Offset;
                var yOffset = currentConfig.Yconfig[Convert.ToInt32(switchFlag)].Offset;
                var rate = currentConfig.Xconfig[Convert.ToInt32(switchFlag)].Rate + random.Next(randomMin, randomMax);
                Thread.Sleep(rate);
                RelativeMove(0, yOffset);
                switchFlag = !switchFlag;
                xOffset = currentConfig.Xconfig[Convert.ToInt32(switchFlag)].Offset;
                yOffset = currentConfig.Yconfig[Convert.ToInt32(switchFlag)].Offset;
                rate = currentConfig.Xconfig[Convert.ToInt32(switchFlag)].Rate + random.Next(randomMin, randomMax);
                Thread.Sleep(rate);
                RelativeMove(0, yOffset);
                count++;
                if (currentConfig.xDelayCount > 0)
                {
                    if(currentConfig.xDelayCount == count)
                    {
                        RelativeMove(xOffset, 0);
                        count = 0;
                    }
                }
            }
        }

        private void ThreadInit()
        {
            xThread = new Thread(HandleMyThread);
            //xThread = new Thread(() =>
            //{
            //    bool switchFlag = false;
            //    while (isLeftDown && isRightDown)
            //    {
            //        var xOffset = currentConfig.Xconfig[Convert.ToInt32(switchFlag)].Offset;
            //        var yOffset = currentConfig.Yconfig[Convert.ToInt32(switchFlag)].Offset;
            //        var rate = currentConfig.Xconfig[Convert.ToInt32(switchFlag)].Rate;
            //        var isFullAuto = currentConfig.isFullAuto;
            //        RelativeMove(xOffset, yOffset);
            //        Thread.Sleep(rate);
            //        switchFlag = !switchFlag;
            //        xOffset = currentConfig.Xconfig[Convert.ToInt32(switchFlag)].Offset;
            //        yOffset = currentConfig.Yconfig[Convert.ToInt32(switchFlag)].Offset;
            //        rate = currentConfig.Xconfig[Convert.ToInt32(switchFlag)].Rate;
            //        RelativeMove(xOffset, yOffset);
            //        Thread.Sleep(rate);
            //        if (!isFullAuto)
            //            break;
            //    }
            //});
            //xThread.Start();

            //yThread = new Thread(() =>
            //{
            //    var switchFlag = false;
            //    while (isLeftDown && isRightDown)
            //    {
            //        RelativeMove(0, currentConfig.Yconfig[Convert.ToInt32(switchFlag)].Offset);
            //        Thread.Sleep(currentConfig.Yconfig[Convert.ToInt32(switchFlag)].Rate);
            //        switchFlag = !switchFlag;
            //    }
            //});
            //yThread.Start();
        }

        private void ThreadClear()
        {
            if (xThread != null)
                xThread.Join();
        }

        private void initConfigInfo()
        {
            label1.Text = $"y = {currentConfig.Yconfig[0].Offset}";
            label2.Text = $"rate1 = {currentConfig.Xconfig[0].Rate}";
            label3.Text = $"rate2 = {currentConfig.Xconfig[1].Rate}";
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
                        initConfigInfo();
                        break;
                    }
                case 50://按下2
                    {
                        currentConfig = configs[1];
                        lblCurrnetConfig.Text = "副武器";
                        initConfigInfo();
                        break;
                    }
                case 51://按下3
                    {
                        //currentConfig = configs[2];
                        //lblCurrnetConfig.Text = "腎上腺素";
                        //initConfigInfo();
                        break;
                    }
                case 120://F9
                    {
                        isOpne = !isOpne;
                        if (isOpne)
                        {
                            lblIsOn.Text = "On";
                        }
                        else
                        {
                            lblIsOn.Text = "Off";
                        }
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
                int xDelayCount = Convert.ToInt32(loadedConfig.ReadIniFile("config", "xDelayCount", "0"));
                bool isFullAuto = Convert.ToBoolean(loadedConfig.ReadIniFile("type", "isFullAuto", "true"));

                if (Convert.ToBoolean(loadedConfig.ReadIniFile("type", "forFinka", "false")))
                {
                    configs[2].Xconfig.Clear();
                    configs[2].Xconfig.Add(new ConfigDetail { Offset = x1, Rate = xRate1 });
                    configs[2].Xconfig.Add(new ConfigDetail { Offset = x2, Rate = xRate2 });

                    configs[2].Yconfig.Clear();
                    configs[2].Yconfig.Add(new ConfigDetail { Offset = y1, Rate = yRate1 });
                    configs[2].Yconfig.Add(new ConfigDetail { Offset = y2, Rate = yRate2 });

                    configs[2].xDelayCount = xDelayCount;
                    configs[2].isFullAuto = isFullAuto;
                }
                else if (Convert.ToBoolean(loadedConfig.ReadIniFile("type", "isMain", "true")))
                {
                    configs[0].Xconfig.Clear();
                    configs[0].Xconfig.Add(new ConfigDetail { Offset = x1, Rate = xRate1 });
                    configs[0].Xconfig.Add(new ConfigDetail { Offset = x2, Rate = xRate2 });

                    configs[0].Yconfig.Clear();
                    configs[0].Yconfig.Add(new ConfigDetail { Offset = y1, Rate = yRate1 });
                    configs[0].Yconfig.Add(new ConfigDetail { Offset = y2, Rate = yRate2 });

                    configs[0].xDelayCount = xDelayCount;
                    configs[0].isFullAuto = isFullAuto;
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

                    configs[1].xDelayCount = xDelayCount;
                    configs[1].isFullAuto = isFullAuto;
                    lblSecConfigName.Text = $"副：{item.Substring(item.LastIndexOf('\\') + 1).Replace(".ini", "")}";
                }

                currentConfig = configs[0];
                initConfigInfo();
            }
        }

        private void dummyFun()
        {
            var a = true;
            if (a)
            {
                var i = 0;
                while (i>=0)
                {
                    var b = "testString";
                }
            }

            var test = new List<object>();
            var test2 = test.Select(x => x).ToList();
        }
    }
}
