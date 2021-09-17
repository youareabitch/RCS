
namespace RCS
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblWeaponConfigPathTitle = new System.Windows.Forms.Label();
            this.ttbWeaponConfigPath = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblKeyValueView = new System.Windows.Forms.Label();
            this.btnLoadConfig = new System.Windows.Forms.Button();
            this.lblMainConfigName = new System.Windows.Forms.Label();
            this.lblSecConfigName = new System.Windows.Forms.Label();
            this.lblCurrnetConfig = new System.Windows.Forms.Label();
            this.lblIsOn = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblWeaponConfigPathTitle
            // 
            this.lblWeaponConfigPathTitle.AutoSize = true;
            this.lblWeaponConfigPathTitle.Location = new System.Drawing.Point(13, 64);
            this.lblWeaponConfigPathTitle.Name = "lblWeaponConfigPathTitle";
            this.lblWeaponConfigPathTitle.Size = new System.Drawing.Size(77, 12);
            this.lblWeaponConfigPathTitle.TabIndex = 0;
            this.lblWeaponConfigPathTitle.Text = "武器設定目錄";
            // 
            // ttbWeaponConfigPath
            // 
            this.ttbWeaponConfigPath.Location = new System.Drawing.Point(96, 61);
            this.ttbWeaponConfigPath.Name = "ttbWeaponConfigPath";
            this.ttbWeaponConfigPath.Size = new System.Drawing.Size(372, 22);
            this.ttbWeaponConfigPath.TabIndex = 1;
            this.ttbWeaponConfigPath.TextChanged += new System.EventHandler(this.ttbWeaponConfigPath_TextChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // lblKeyValueView
            // 
            this.lblKeyValueView.AutoSize = true;
            this.lblKeyValueView.Location = new System.Drawing.Point(15, 13);
            this.lblKeyValueView.Name = "lblKeyValueView";
            this.lblKeyValueView.Size = new System.Drawing.Size(0, 12);
            this.lblKeyValueView.TabIndex = 2;
            // 
            // btnLoadConfig
            // 
            this.btnLoadConfig.Location = new System.Drawing.Point(96, 90);
            this.btnLoadConfig.Name = "btnLoadConfig";
            this.btnLoadConfig.Size = new System.Drawing.Size(75, 23);
            this.btnLoadConfig.TabIndex = 3;
            this.btnLoadConfig.Text = "讀取設定";
            this.btnLoadConfig.UseVisualStyleBackColor = true;
            this.btnLoadConfig.Click += new System.EventHandler(this.btnLoadConfig_Click);
            // 
            // lblMainConfigName
            // 
            this.lblMainConfigName.AutoSize = true;
            this.lblMainConfigName.Location = new System.Drawing.Point(177, 13);
            this.lblMainConfigName.Name = "lblMainConfigName";
            this.lblMainConfigName.Size = new System.Drawing.Size(29, 12);
            this.lblMainConfigName.TabIndex = 4;
            this.lblMainConfigName.Text = "主：";
            // 
            // lblSecConfigName
            // 
            this.lblSecConfigName.AutoSize = true;
            this.lblSecConfigName.Location = new System.Drawing.Point(177, 35);
            this.lblSecConfigName.Name = "lblSecConfigName";
            this.lblSecConfigName.Size = new System.Drawing.Size(29, 12);
            this.lblSecConfigName.TabIndex = 5;
            this.lblSecConfigName.Text = "副：";
            // 
            // lblCurrnetConfig
            // 
            this.lblCurrnetConfig.AutoSize = true;
            this.lblCurrnetConfig.Location = new System.Drawing.Point(76, 35);
            this.lblCurrnetConfig.Name = "lblCurrnetConfig";
            this.lblCurrnetConfig.Size = new System.Drawing.Size(41, 12);
            this.lblCurrnetConfig.TabIndex = 6;
            this.lblCurrnetConfig.Text = "主武器";
            // 
            // lblIsOn
            // 
            this.lblIsOn.AutoSize = true;
            this.lblIsOn.Location = new System.Drawing.Point(21, 35);
            this.lblIsOn.Name = "lblIsOn";
            this.lblIsOn.Size = new System.Drawing.Size(19, 12);
            this.lblIsOn.TabIndex = 7;
            this.lblIsOn.Text = "On";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "y = ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "rate1 = ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(131, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "rate2 = ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(307, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(309, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "label5";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 159);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblIsOn);
            this.Controls.Add(this.lblCurrnetConfig);
            this.Controls.Add(this.lblSecConfigName);
            this.Controls.Add(this.lblMainConfigName);
            this.Controls.Add(this.btnLoadConfig);
            this.Controls.Add(this.lblKeyValueView);
            this.Controls.Add(this.ttbWeaponConfigPath);
            this.Controls.Add(this.lblWeaponConfigPathTitle);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWeaponConfigPathTitle;
        private System.Windows.Forms.TextBox ttbWeaponConfigPath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblKeyValueView;
        private System.Windows.Forms.Button btnLoadConfig;
        private System.Windows.Forms.Label lblMainConfigName;
        private System.Windows.Forms.Label lblSecConfigName;
        private System.Windows.Forms.Label lblCurrnetConfig;
        private System.Windows.Forms.Label lblIsOn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

