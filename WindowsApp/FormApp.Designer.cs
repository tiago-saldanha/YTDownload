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
            buttonDownloadVideo.Location = new Point(170, 341);
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
            buttonPlay.Location = new Point(490, 341);
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
            buttonDownloadAudio.Location = new Point(329, 341);
            buttonDownloadAudio.Name = "buttonDownloadAudio";
            buttonDownloadAudio.Size = new Size(141, 34);
            buttonDownloadAudio.TabIndex = 7;
            buttonDownloadAudio.Text = "Download Audio";
            buttonDownloadAudio.UseVisualStyleBackColor = true;
            buttonDownloadAudio.Click += DownloadAudio;
            // 
            // FormApp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonDownloadAudio);
            Controls.Add(buttonPlay);
            Controls.Add(textBoxOutput);
            Controls.Add(labelOutput);
            Controls.Add(buttonDownloadVideo);
            Controls.Add(textBoxUrlVideo);
            Controls.Add(labelUrlVideo);
            Name = "FormApp";
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
    }
}
