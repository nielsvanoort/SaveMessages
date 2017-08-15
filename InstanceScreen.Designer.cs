namespace SaveMessages
{
    partial class InstanceScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblInstanceID = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblApplication = new System.Windows.Forms.Label();
            this.lblhostname = new System.Windows.Forms.Label();
            this.lblinstancetype = new System.Windows.Forms.Label();
            this.lblstate = new System.Windows.Forms.Label();
            this.btnSuspend = new System.Windows.Forms.Button();
            this.btnResume = new System.Windows.Forms.Button();
            this.btnTerminate = new System.Windows.Forms.Button();
            this.tvMessages = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // lblInstanceID
            // 
            this.lblInstanceID.AutoSize = true;
            this.lblInstanceID.Location = new System.Drawing.Point(48, 50);
            this.lblInstanceID.Name = "lblInstanceID";
            this.lblInstanceID.Size = new System.Drawing.Size(68, 13);
            this.lblInstanceID.TabIndex = 0;
            this.lblInstanceID.Text = "InstanceID : ";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(48, 85);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(69, 13);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Description : ";
            // 
            // lblApplication
            // 
            this.lblApplication.AutoSize = true;
            this.lblApplication.Location = new System.Drawing.Point(47, 127);
            this.lblApplication.Name = "lblApplication";
            this.lblApplication.Size = new System.Drawing.Size(68, 13);
            this.lblApplication.TabIndex = 2;
            this.lblApplication.Text = "Application : ";
            // 
            // lblhostname
            // 
            this.lblhostname.AutoSize = true;
            this.lblhostname.Location = new System.Drawing.Point(47, 176);
            this.lblhostname.Name = "lblhostname";
            this.lblhostname.Size = new System.Drawing.Size(64, 13);
            this.lblhostname.TabIndex = 3;
            this.lblhostname.Text = "Hostname : ";
            // 
            // lblinstancetype
            // 
            this.lblinstancetype.AutoSize = true;
            this.lblinstancetype.Location = new System.Drawing.Point(48, 226);
            this.lblinstancetype.Name = "lblinstancetype";
            this.lblinstancetype.Size = new System.Drawing.Size(77, 13);
            this.lblinstancetype.TabIndex = 4;
            this.lblinstancetype.Text = "Instancetype : ";
            // 
            // lblstate
            // 
            this.lblstate.AutoSize = true;
            this.lblstate.Location = new System.Drawing.Point(47, 274);
            this.lblstate.Name = "lblstate";
            this.lblstate.Size = new System.Drawing.Size(41, 13);
            this.lblstate.TabIndex = 5;
            this.lblstate.Text = "State : ";
            // 
            // btnSuspend
            // 
            this.btnSuspend.Location = new System.Drawing.Point(36, 393);
            this.btnSuspend.Name = "btnSuspend";
            this.btnSuspend.Size = new System.Drawing.Size(75, 23);
            this.btnSuspend.TabIndex = 6;
            this.btnSuspend.Text = "Suspend";
            this.btnSuspend.UseVisualStyleBackColor = true;
            this.btnSuspend.Click += new System.EventHandler(this.btnSuspend_Click);
            // 
            // btnResume
            // 
            this.btnResume.Location = new System.Drawing.Point(154, 393);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(75, 23);
            this.btnResume.TabIndex = 7;
            this.btnResume.Text = "Resume";
            this.btnResume.UseVisualStyleBackColor = true;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            // 
            // btnTerminate
            // 
            this.btnTerminate.Location = new System.Drawing.Point(257, 393);
            this.btnTerminate.Name = "btnTerminate";
            this.btnTerminate.Size = new System.Drawing.Size(75, 23);
            this.btnTerminate.TabIndex = 8;
            this.btnTerminate.Text = "Terminate";
            this.btnTerminate.UseVisualStyleBackColor = true;
            this.btnTerminate.Click += new System.EventHandler(this.btnTerminate_Click);
            // 
            // tvMessages
            // 
            this.tvMessages.Dock = System.Windows.Forms.DockStyle.Right;
            this.tvMessages.Location = new System.Drawing.Point(436, 0);
            this.tvMessages.Name = "tvMessages";
            this.tvMessages.Size = new System.Drawing.Size(366, 551);
            this.tvMessages.TabIndex = 9;
            this.tvMessages.Click += new System.EventHandler(this.tvMessages_Click);
            this.tvMessages.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvMessages_KeyDown);
            // 
            // InstanceScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 551);
            this.Controls.Add(this.tvMessages);
            this.Controls.Add(this.btnTerminate);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.btnSuspend);
            this.Controls.Add(this.lblstate);
            this.Controls.Add(this.lblinstancetype);
            this.Controls.Add(this.lblhostname);
            this.Controls.Add(this.lblApplication);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblInstanceID);
            this.Name = "InstanceScreen";
            this.Text = "Instance";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInstanceID;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblApplication;
        private System.Windows.Forms.Label lblhostname;
        private System.Windows.Forms.Label lblinstancetype;
        private System.Windows.Forms.Label lblstate;
        private System.Windows.Forms.Button btnSuspend;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.Button btnTerminate;
        private System.Windows.Forms.TreeView tvMessages;
    }
}