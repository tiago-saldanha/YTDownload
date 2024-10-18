namespace WindowsApp
{
    partial class FormApp
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelUrlVideo = new Label();
            textBoxUrlVideo = new TextBox();
            buttonDownloadVideo = new Button();
            labelOutput = new Label();
            textBoxOutput = new TextBox();
            buttonPlay = new Button();
            buttonDownloadAudio = new Button();
            checkBoxConverterMp3Mp4 = new CheckBox();
            checkBoxAutoPlay = new CheckBox();
            SuspendLayout();
            // 
            // labelUrlVideo
            // 
            labelUrlVideo.AutoSize = true;
            labelUrlVideo.Location = new Point(22, 21);
            labelUrlVideo.Name = "labelUrlVideo";
            labelUrlVideo.Size = new Size(78, 15);
            labelUrlVideo.TabIndex = 0;
            labelUrlVideo.Text = "URL do Vídeo";
            // 
            // textBoxUrlVideo
            // 
            textBoxUrlVideo.Location = new Point(24, 50);
            textBoxUrlVideo.Name = "textBoxUrlVideo";
            textBoxUrlVideo.PlaceholderText = "Digite aqui a url do vídeo...";
            textBoxUrlVideo.Size = new Size(748, 23);
            textBoxUrlVideo.TabIndex = 1;
            // 
            // buttonDownloadVideo
            // 
            buttonDownloadVideo.FlatStyle = FlatStyle.System;
            buttonDownloadVideo.Location = new Point(24, 350);
            buttonDownloadVideo.Name = "buttonDownloadVideo";
            buttonDownloadVideo.Size = new Size(141, 34);
            buttonDownloadVideo.TabIndex = 2;
            buttonDownloadVideo.Text = "Download Vídeo";
            buttonDownloadVideo.UseVisualStyleBackColor = true;
            buttonDownloadVideo.Click += DownloadVideo;
            // 
            // labelOutput
            // 
            labelOutput.AutoSize = true;
            labelOutput.Location = new Point(24, 102);
            labelOutput.Name = "labelOutput";
            labelOutput.Size = new Size(59, 15);
            labelOutput.TabIndex = 4;
            labelOutput.Text = "Resultado";
            // 
            // textBoxOutput
            // 
            textBoxOutput.Location = new Point(24, 132);
            textBoxOutput.Multiline = true;
            textBoxOutput.Name = "textBoxOutput";
            textBoxOutput.Size = new Size(748, 143);
            textBoxOutput.TabIndex = 5;
            // 
            // buttonPlay
            // 
            buttonPlay.FlatStyle = FlatStyle.System;
            buttonPlay.Location = new Point(344, 350);
            buttonPlay.Name = "buttonPlay";
            buttonPlay.Size = new Size(141, 34);
            buttonPlay.TabIndex = 6;
            buttonPlay.Text = "Play";
            buttonPlay.UseVisualStyleBackColor = true;
            buttonPlay.Click += Play;
            // 
            // buttonDownloadAudio
            // 
            buttonDownloadAudio.FlatStyle = FlatStyle.System;
            buttonDownloadAudio.Location = new Point(183, 350);
            buttonDownloadAudio.Name = "buttonDownloadAudio";
            buttonDownloadAudio.Size = new Size(141, 34);
            buttonDownloadAudio.TabIndex = 7;
            buttonDownloadAudio.Text = "Download Audio";
            buttonDownloadAudio.UseVisualStyleBackColor = true;
            buttonDownloadAudio.Click += DownloadAudio;
            // 
            // checkBoxConverterMp3Mp4
            // 
            checkBoxConverterMp3Mp4.Location = new Point(501, 356);
            checkBoxConverterMp3Mp4.Name = "checkBoxConverterMp3Mp4";
            checkBoxConverterMp3Mp4.Size = new Size(160, 25);
            checkBoxConverterMp3Mp4.TabIndex = 8;
            checkBoxConverterMp3Mp4.Text = "Converter Para Mp3/Mp4";
            checkBoxConverterMp3Mp4.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoPlay
            // 
            checkBoxAutoPlay.Location = new Point(667, 356);
            checkBoxAutoPlay.Name = "checkBoxAutoPlay";
            checkBoxAutoPlay.Size = new Size(105, 25);
            checkBoxAutoPlay.TabIndex = 9;
            checkBoxAutoPlay.Text = "AutoPlay";
            checkBoxAutoPlay.UseVisualStyleBackColor = true;
            // 
            // FormApp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(checkBoxAutoPlay);
            Controls.Add(checkBoxConverterMp3Mp4);
            Controls.Add(buttonDownloadAudio);
            Controls.Add(buttonPlay);
            Controls.Add(textBoxOutput);
            Controls.Add(labelOutput);
            Controls.Add(buttonDownloadVideo);
            Controls.Add(textBoxUrlVideo);
            Controls.Add(labelUrlVideo);
            Name = "FormApp";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Youtube Download";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelUrlVideo;
        private TextBox textBoxUrlVideo;
        private Button buttonDownloadVideo;
        private Label labelOutput;
        private TextBox textBoxOutput;
        private Button buttonPlay;
        private Button buttonDownloadAudio;
        private CheckBox checkBoxConverterMp3Mp4;
        private CheckBox checkBoxAutoPlay;
    }
}
