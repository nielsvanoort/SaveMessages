namespace SaveMessages
{
    partial class SaveMessagesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveMessagesForm));
            this.btnSaveMessages = new System.Windows.Forms.Button();
            this.txtoutput = new System.Windows.Forms.TextBox();
            this.connectionstring = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPipelines = new System.Windows.Forms.ComboBox();
            this.btnExecutePipeline = new System.Windows.Forms.Button();
            this.btnExecuteMap = new System.Windows.Forms.Button();
            this.cbMaps = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cbDirectoryforInstance = new System.Windows.Forms.CheckBox();
            this.cbInstanceType = new System.Windows.Forms.CheckBox();
            this.clbHosts = new System.Windows.Forms.CheckedListBox();
            this.btnFillHosts = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.tbFolderPipeline = new System.Windows.Forms.TextBox();
            this.btnFolderPipeline = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbFolderMaps = new System.Windows.Forms.TextBox();
            this.btnFolderMaps = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.DirectoryForNamespace = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tvInstances = new System.Windows.Forms.TreeView();
            this.cbViews = new System.Windows.Forms.ComboBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.btnLoadHost = new System.Windows.Forms.Button();
            this.cbExportFirst = new System.Windows.Forms.CheckBox();
            this.cbHost = new System.Windows.Forms.ComboBox();
            this.btnFlush = new System.Windows.Forms.Button();
            this.tbWorkingDirectory = new System.Windows.Forms.TextBox();
            this.btnPickMainFolder = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cbIDirectoryInstanceForNamespace = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSaveMessages
            // 
            this.btnSaveMessages.Location = new System.Drawing.Point(506, 363);
            this.btnSaveMessages.Name = "btnSaveMessages";
            this.btnSaveMessages.Size = new System.Drawing.Size(86, 23);
            this.btnSaveMessages.TabIndex = 0;
            this.btnSaveMessages.Text = "save";
            this.btnSaveMessages.UseVisualStyleBackColor = true;
            this.btnSaveMessages.Click += new System.EventHandler(this.btnSaveMessages_Click);
            // 
            // txtoutput
            // 
            this.txtoutput.Location = new System.Drawing.Point(798, 76);
            this.txtoutput.Multiline = true;
            this.txtoutput.Name = "txtoutput";
            this.txtoutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtoutput.Size = new System.Drawing.Size(384, 481);
            this.txtoutput.TabIndex = 1;
            // 
            // connectionstring
            // 
            this.connectionstring.Location = new System.Drawing.Point(116, 75);
            this.connectionstring.Name = "connectionstring";
            this.connectionstring.Size = new System.Drawing.Size(355, 20);
            this.connectionstring.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "ConnectionString";
            // 
            // cbPipelines
            // 
            this.cbPipelines.FormattingEnabled = true;
            this.cbPipelines.Location = new System.Drawing.Point(64, 124);
            this.cbPipelines.Name = "cbPipelines";
            this.cbPipelines.Size = new System.Drawing.Size(557, 21);
            this.cbPipelines.TabIndex = 10;
            // 
            // btnExecutePipeline
            // 
            this.btnExecutePipeline.Location = new System.Drawing.Point(630, 314);
            this.btnExecutePipeline.Name = "btnExecutePipeline";
            this.btnExecutePipeline.Size = new System.Drawing.Size(75, 23);
            this.btnExecutePipeline.TabIndex = 11;
            this.btnExecutePipeline.Text = "Execute";
            this.btnExecutePipeline.UseVisualStyleBackColor = true;
            this.btnExecutePipeline.Click += new System.EventHandler(this.btnExecutePipeline_Click);
            // 
            // btnExecuteMap
            // 
            this.btnExecuteMap.Location = new System.Drawing.Point(613, 236);
            this.btnExecuteMap.Name = "btnExecuteMap";
            this.btnExecuteMap.Size = new System.Drawing.Size(75, 23);
            this.btnExecuteMap.TabIndex = 12;
            this.btnExecuteMap.Text = "Execute";
            this.btnExecuteMap.UseVisualStyleBackColor = true;
            this.btnExecuteMap.Click += new System.EventHandler(this.btnExecuteMap_Click);
            // 
            // cbMaps
            // 
            this.cbMaps.FormattingEnabled = true;
            this.cbMaps.Location = new System.Drawing.Point(50, 99);
            this.cbMaps.Name = "cbMaps";
            this.cbMaps.Size = new System.Drawing.Size(557, 21);
            this.cbMaps.TabIndex = 13;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Location = new System.Drawing.Point(12, 54);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(756, 443);
            this.tabControl1.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.Controls.Add(this.cbIDirectoryInstanceForNamespace);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.cbDirectoryforInstance);
            this.tabPage1.Controls.Add(this.cbInstanceType);
            this.tabPage1.Controls.Add(this.clbHosts);
            this.tabPage1.Controls.Add(this.btnFillHosts);
            this.tabPage1.Controls.Add(this.connectionstring);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnSaveMessages);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(748, 417);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SaveMessages";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(503, 124);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 13);
            this.label14.TabIndex = 23;
            this.label14.Text = "Export properties";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(486, 52);
            this.label13.TabIndex = 22;
            this.label13.Text = resources.GetString("label13.Text");
            // 
            // cbDirectoryforInstance
            // 
            this.cbDirectoryforInstance.AutoSize = true;
            this.cbDirectoryforInstance.Location = new System.Drawing.Point(503, 205);
            this.cbDirectoryforInstance.Name = "cbDirectoryforInstance";
            this.cbDirectoryforInstance.Size = new System.Drawing.Size(130, 17);
            this.cbDirectoryforInstance.TabIndex = 14;
            this.cbDirectoryforInstance.Text = "Directory per Instance";
            this.cbDirectoryforInstance.UseVisualStyleBackColor = true;
            // 
            // cbInstanceType
            // 
            this.cbInstanceType.AutoSize = true;
            this.cbInstanceType.Location = new System.Drawing.Point(503, 172);
            this.cbInstanceType.Name = "cbInstanceType";
            this.cbInstanceType.Size = new System.Drawing.Size(147, 17);
            this.cbInstanceType.TabIndex = 12;
            this.cbInstanceType.Text = "Directory for Instancetype";
            this.cbInstanceType.UseVisualStyleBackColor = true;
            // 
            // clbHosts
            // 
            this.clbHosts.FormattingEnabled = true;
            this.clbHosts.Location = new System.Drawing.Point(116, 124);
            this.clbHosts.Name = "clbHosts";
            this.clbHosts.Size = new System.Drawing.Size(340, 289);
            this.clbHosts.TabIndex = 11;
            // 
            // btnFillHosts
            // 
            this.btnFillHosts.Location = new System.Drawing.Point(492, 70);
            this.btnFillHosts.Name = "btnFillHosts";
            this.btnFillHosts.Size = new System.Drawing.Size(115, 23);
            this.btnFillHosts.TabIndex = 10;
            this.btnFillHosts.Text = "Load Applications";
            this.btnFillHosts.UseVisualStyleBackColor = true;
            this.btnFillHosts.Click += new System.EventHandler(this.btnFillHosts_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.Label2);
            this.tabPage2.Controls.Add(this.tbFolderPipeline);
            this.tabPage2.Controls.Add(this.btnFolderPipeline);
            this.tabPage2.Controls.Add(this.cbPipelines);
            this.tabPage2.Controls.Add(this.btnExecutePipeline);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(748, 417);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Execute Pipeline";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(49, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(656, 39);
            this.label12.TabIndex = 21;
            this.label12.Text = resources.GetString("label12.Text");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Folder";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(61, 92);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(64, 13);
            this.Label2.TabIndex = 14;
            this.Label2.Text = "Pipelinetype";
            // 
            // tbFolderPipeline
            // 
            this.tbFolderPipeline.Location = new System.Drawing.Point(64, 207);
            this.tbFolderPipeline.Name = "tbFolderPipeline";
            this.tbFolderPipeline.Size = new System.Drawing.Size(557, 20);
            this.tbFolderPipeline.TabIndex = 13;
            // 
            // btnFolderPipeline
            // 
            this.btnFolderPipeline.Location = new System.Drawing.Point(630, 205);
            this.btnFolderPipeline.Name = "btnFolderPipeline";
            this.btnFolderPipeline.Size = new System.Drawing.Size(75, 23);
            this.btnFolderPipeline.TabIndex = 12;
            this.btnFolderPipeline.Text = "select ...";
            this.btnFolderPipeline.UseVisualStyleBackColor = true;
            this.btnFolderPipeline.Click += new System.EventHandler(this.btnFolderPipeline_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.tbFolderMaps);
            this.tabPage3.Controls.Add(this.btnFolderMaps);
            this.tabPage3.Controls.Add(this.cbMaps);
            this.tabPage3.Controls.Add(this.btnExecuteMap);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(748, 417);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Execute Maps";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(47, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(612, 39);
            this.label11.TabIndex = 20;
            this.label11.Text = resources.GetString("label11.Text");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Map";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Message Folder";
            // 
            // tbFolderMaps
            // 
            this.tbFolderMaps.Location = new System.Drawing.Point(50, 174);
            this.tbFolderMaps.Name = "tbFolderMaps";
            this.tbFolderMaps.Size = new System.Drawing.Size(557, 20);
            this.tbFolderMaps.TabIndex = 17;
            // 
            // btnFolderMaps
            // 
            this.btnFolderMaps.Location = new System.Drawing.Point(613, 174);
            this.btnFolderMaps.Name = "btnFolderMaps";
            this.btnFolderMaps.Size = new System.Drawing.Size(75, 23);
            this.btnFolderMaps.TabIndex = 16;
            this.btnFolderMaps.Text = "Select ...";
            this.btnFolderMaps.UseVisualStyleBackColor = true;
            this.btnFolderMaps.Click += new System.EventHandler(this.btnFolderMaps_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage4.Controls.Add(this.label10);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.label8);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.DirectoryForNamespace);
            this.tabPage4.Controls.Add(this.button5);
            this.tabPage4.Controls.Add(this.button3);
            this.tabPage4.Controls.Add(this.button2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(748, 417);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Extra Functions";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(142, 265);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(367, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Sets the File Encoding from the files in the workingdirectory to windows 1252";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(142, 194);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(548, 26);
            this.label9.TabIndex = 11;
            this.label9.Text = "Copies the files from the workingdirectory into a directory which has the name of" +
    " the receivelocation or receiveport. \r\nthe directory msgbox is used when not one" +
    " present\r\n";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(142, 127);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(416, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Changes the filenames in workingdirectory into the original filename if present i" +
    "n context";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(142, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(466, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Copies the files from the working directory into directories which have the name " +
    "of the namespace";
            // 
            // DirectoryForNamespace
            // 
            this.DirectoryForNamespace.Location = new System.Drawing.Point(31, 36);
            this.DirectoryForNamespace.Name = "DirectoryForNamespace";
            this.DirectoryForNamespace.Size = new System.Drawing.Size(85, 37);
            this.DirectoryForNamespace.TabIndex = 8;
            this.DirectoryForNamespace.Text = "Directory for Namespace";
            this.DirectoryForNamespace.UseVisualStyleBackColor = true;
            this.DirectoryForNamespace.Click += new System.EventHandler(this.DirectoryForNamespace_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(31, 189);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(85, 23);
            this.button5.TabIndex = 7;
            this.button5.Text = "Set Directory";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.SetFolders);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(31, 260);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(85, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Windows1252";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(30, 122);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Set Filename";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.SetFilenames);
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage5.Controls.Add(this.tvInstances);
            this.tabPage5.Controls.Add(this.cbViews);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(748, 417);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Maintain BizTalk";
            // 
            // tvInstances
            // 
            this.tvInstances.Location = new System.Drawing.Point(22, 82);
            this.tvInstances.Name = "tvInstances";
            this.tvInstances.Size = new System.Drawing.Size(707, 312);
            this.tvInstances.TabIndex = 2;
            // 
            // cbViews
            // 
            this.cbViews.FormattingEnabled = true;
            this.cbViews.Items.AddRange(new object[] {
            "Suspended",
            "Active",
            "Suspended per Application",
            "Active per Application"});
            this.cbViews.Location = new System.Drawing.Point(22, 18);
            this.cbViews.Name = "cbViews";
            this.cbViews.Size = new System.Drawing.Size(275, 21);
            this.cbViews.TabIndex = 0;
            this.cbViews.SelectedIndexChanged += new System.EventHandler(this.cbViews_SelectedIndexChanged);
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage6.Controls.Add(this.btnLoadHost);
            this.tabPage6.Controls.Add(this.cbExportFirst);
            this.tabPage6.Controls.Add(this.cbHost);
            this.tabPage6.Controls.Add(this.btnFlush);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(748, 417);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Host Instance";
            // 
            // btnLoadHost
            // 
            this.btnLoadHost.Location = new System.Drawing.Point(531, 67);
            this.btnLoadHost.Name = "btnLoadHost";
            this.btnLoadHost.Size = new System.Drawing.Size(75, 23);
            this.btnLoadHost.TabIndex = 3;
            this.btnLoadHost.Text = "load hosts";
            this.btnLoadHost.UseVisualStyleBackColor = true;
            this.btnLoadHost.Click += new System.EventHandler(this.btnLoadHost_Click);
            // 
            // cbExportFirst
            // 
            this.cbExportFirst.AutoSize = true;
            this.cbExportFirst.Location = new System.Drawing.Point(72, 158);
            this.cbExportFirst.Name = "cbExportFirst";
            this.cbExportFirst.Size = new System.Drawing.Size(78, 17);
            this.cbExportFirst.TabIndex = 2;
            this.cbExportFirst.Text = "Export First";
            this.cbExportFirst.UseVisualStyleBackColor = true;
            // 
            // cbHost
            // 
            this.cbHost.FormattingEnabled = true;
            this.cbHost.Location = new System.Drawing.Point(61, 70);
            this.cbHost.Name = "cbHost";
            this.cbHost.Size = new System.Drawing.Size(451, 21);
            this.cbHost.TabIndex = 1;
            // 
            // btnFlush
            // 
            this.btnFlush.Location = new System.Drawing.Point(201, 152);
            this.btnFlush.Name = "btnFlush";
            this.btnFlush.Size = new System.Drawing.Size(75, 23);
            this.btnFlush.TabIndex = 0;
            this.btnFlush.Text = "flush";
            this.btnFlush.UseVisualStyleBackColor = true;
            this.btnFlush.Click += new System.EventHandler(this.btnFlush_Click);
            // 
            // tbWorkingDirectory
            // 
            this.tbWorkingDirectory.Location = new System.Drawing.Point(12, 537);
            this.tbWorkingDirectory.Name = "tbWorkingDirectory";
            this.tbWorkingDirectory.Size = new System.Drawing.Size(611, 20);
            this.tbWorkingDirectory.TabIndex = 15;
            // 
            // btnPickMainFolder
            // 
            this.btnPickMainFolder.Location = new System.Drawing.Point(629, 534);
            this.btnPickMainFolder.Name = "btnPickMainFolder";
            this.btnPickMainFolder.Size = new System.Drawing.Size(75, 23);
            this.btnPickMainFolder.TabIndex = 16;
            this.btnPickMainFolder.Text = "select ...";
            this.btnPickMainFolder.UseVisualStyleBackColor = true;
            this.btnPickMainFolder.Click += new System.EventHandler(this.btnPickMainFolder_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 518);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Select the working Directory";
            // 
            // cbIDirectoryInstanceForNamespace
            // 
            this.cbIDirectoryInstanceForNamespace.AutoSize = true;
            this.cbIDirectoryInstanceForNamespace.Location = new System.Drawing.Point(503, 242);
            this.cbIDirectoryInstanceForNamespace.Name = "cbIDirectoryInstanceForNamespace";
            this.cbIDirectoryInstanceForNamespace.Size = new System.Drawing.Size(187, 17);
            this.cbIDirectoryInstanceForNamespace.TabIndex = 24;
            this.cbIDirectoryInstanceForNamespace.Text = "Directory per InstanceNamespace";
            this.cbIDirectoryInstanceForNamespace.UseVisualStyleBackColor = true;
            // 
            // SaveMessagesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 567);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnPickMainFolder);
            this.Controls.Add(this.tbWorkingDirectory);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtoutput);
            this.Name = "SaveMessagesForm";
            this.Text = "SaveMessages";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveMessages;
        private System.Windows.Forms.TextBox txtoutput;
        private System.Windows.Forms.TextBox connectionstring;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPipelines;
        private System.Windows.Forms.Button btnExecutePipeline;
        private System.Windows.Forms.Button btnExecuteMap;
        private System.Windows.Forms.ComboBox cbMaps;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.TextBox tbFolderPipeline;
        private System.Windows.Forms.Button btnFolderPipeline;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.CheckedListBox clbHosts;
        private System.Windows.Forms.Button btnFillHosts;
        private System.Windows.Forms.CheckBox cbInstanceType;
        private System.Windows.Forms.CheckBox cbDirectoryforInstance;
        private System.Windows.Forms.Button DirectoryForNamespace;
        private System.Windows.Forms.TextBox tbWorkingDirectory;
        private System.Windows.Forms.Button btnPickMainFolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbFolderMaps;
        private System.Windows.Forms.Button btnFolderMaps;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ComboBox cbViews;
        private System.Windows.Forms.TreeView tvInstances;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.CheckBox cbExportFirst;
        private System.Windows.Forms.ComboBox cbHost;
        private System.Windows.Forms.Button btnFlush;
   
        private System.Windows.Forms.Button btnLoadHost;
        private System.Windows.Forms.CheckBox cbIDirectoryInstanceForNamespace;
    }
}

