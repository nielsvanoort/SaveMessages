using MessageExtractor;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.Test.BizTalk.PipelineObjects;
using Microsoft.XLANGs.BaseTypes;
using SaveMessages.ExtensionMethods;
using SaveMessages.HelperClass;
using SaveMessages.MapsExecution;
using SaveMessages.MessageExtraction;
using SaveMessages.Pipeline;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;


namespace SaveMessages
{
    public partial class SaveMessagesForm : Form
    {
        //public SaveMessagesForm()
        //{
        //    try
        //    {

        //        InitializeComponent();
        //        string strconnectionstring =
        //            new AppSettingsReader().GetValue("ConnectionString", typeof (string)).ToString();



        //        if (strconnectionstring != "")
        //        {
        //            connectionstring.Text = strconnectionstring;
        //        }
        //        else
        //        {
        //            connectionstring.Text = "Server=.;Database=BizTalkMsgBoxDb;Trusted_Connection=True;";
        //        }

        //        //zorg voor extra functionaliteit bij het dynamisch laden van de dll's
        //        AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomainReflectionOnlyAssemblyResolve;
        //        AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainAssemblyResolve;

        //        // laad alle pipelinecomponents uit de pipeline map en zet ze in de bijbehorende combobox
        //        DirectoryInfo di = new DirectoryInfo("pipelines");
        //        var directories = di.GetDirectories();
        //        foreach (DirectoryInfo dix in directories)
        //        {
        //            var dlls = dix.GetFiles("*.dll");
        //            foreach (var dll in dlls)
        //            {
        //                try
        //                {
        //                    var pipelineassembly = Assembly.LoadFrom(dll.FullName);
        //                    foreach (Type tp in pipelineassembly.GetTypes())
        //                    {
        //                        if (tp.BaseType.Name.ToLower() == "sendpipeline")
        //                        {
        //                            cbPipelines.Items.Add(new TypeHelper() {theType = tp, path = dix.FullName});
        //                        }
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show(dll.FullName + ">>" + ex.Message + " >> " + ex.StackTrace);
        //                }
        //            }
        //        }

        //        //laad dynamisch alle maps uit de maps map en zet ze in de combobox voor maps
        //        di = new DirectoryInfo("maps");
        //        directories = di.GetDirectories();

        //        foreach (DirectoryInfo dix in directories)
        //        {
        //            var dlls = dix.GetFiles("*.dll");
        //            foreach (var dll in dlls)
        //            {
        //                var pipelineassembly = Assembly.LoadFrom(dll.FullName);
        //                foreach (Type tp in pipelineassembly.GetTypes())
        //                {
        //                    if (tp.BaseType.Name.ToLower() == "TransformBase".ToLower())
        //                        cbMaps.Items.Add(new TypeHelper() {theType = tp, path = dix.FullName});
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message + " >> " + ex.StackTrace);
        //    }
        //}

        public string WorkingDirectory
        {
            get
            {
                if (this.tbWorkingDirectory.Text.Trim() != "")
                    return this.tbWorkingDirectory.Text.Trim();
                if (new AppSettingsReader().GetValue("WorkingDirectory", typeof(string)) != null)
                    return new AppSettingsReader().GetValue("WorkingDirectory", typeof(string)).ToString();
                else
                    return Directory.GetCurrentDirectory();
            }
        }

        public string ConnectionStringBizTalk
        {
            get
            {
                return this.connectionstring.Text;
            }
        }

        public SaveMessagesForm()
        {
            try
            {
                this.InitializeComponent();
                string str = new AppSettingsReader().GetValue("ConnectionString", typeof(string)).ToString();
                if (str != "")
                    this.connectionstring.Text = str;
                else
                    this.connectionstring.Text = "Server=.;Database=BizTalkMsgBoxDb;Trusted_Connection=True;";
                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += new ResolveEventHandler(SaveMessagesForm.CurrentDomainReflectionOnlyAssemblyResolve);
                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(SaveMessagesForm.CurrentDomainAssemblyResolve);
                foreach (DirectoryInfo directoryInfo in new DirectoryInfo("pipelines").GetDirectories())
                {
                    foreach (FileInfo fileInfo in directoryInfo.GetFiles("*.dll"))
                    {
                        try
                        {
                            foreach (System.Type type in Assembly.LoadFrom(fileInfo.FullName).GetTypes())
                            {
                                if (type.BaseType.Name.ToLower() == "sendpipeline")
                                    this.cbPipelines.Items.Add((object)new SaveMessages.HelperClass.TypeHelper()
                                    {
                                        theType = type,
                                        path = directoryInfo.FullName
                                    });
                            }
                        }
                        catch (Exception ex)
                        {
                            int num = (int)MessageBox.Show(fileInfo.FullName + ">>" + ex.Message + " >> " + ex.StackTrace);
                        }
                    }
                }
                foreach (DirectoryInfo directoryInfo in new DirectoryInfo("maps").GetDirectories())
                {
                    foreach (FileSystemInfo fileSystemInfo in directoryInfo.GetFiles("*.dll"))
                    {
                        foreach (System.Type type in Assembly.LoadFrom(fileSystemInfo.FullName).GetTypes())
                        {
                            if (type.BaseType.Name.ToLower() == "TransformBase".ToLower())
                                this.cbMaps.Items.Add((object)new SaveMessages.HelperClass.TypeHelper()
                                {
                                    theType = type,
                                    path = directoryInfo.FullName
                                });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message + " >> " + ex.StackTrace);
            }
        }

        public static Assembly CurrentDomainReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            return Assembly.Load(args.Name);
        }

        public static Assembly CurrentDomainAssemblyResolve(object sender, ResolveEventArgs args)
        {
            string path = PipeLineExecuter.assemblies.Get(args.Name);
            if (path != null)
                return Assembly.LoadFile(path);
            if (File.Exists(args.Name))
                return Assembly.LoadFile(args.Name);
            else
                return (Assembly)null;
        }

        private void btnSaveMessages_Click(object sender, EventArgs e)
        {
            
            MessageExtracter messageExtracter = new MessageExtracter()
            {
                ConnectionString = this.ConnectionStringBizTalk
            };
            foreach (string aplicatienaam in this.clbHosts.CheckedItems)
            {
                txtoutput.Text += "starting " + aplicatienaam + "\r\n";
                messageExtracter.SaveApplicationToFiles(aplicatienaam, this.WorkingDirectory,
                    this.cbInstanceType.Checked, this.cbDirectoryforInstance.Checked, this.cbIDirectoryInstanceForNamespace.Checked);
                txtoutput.Text += "finished" + aplicatienaam + "\r\n";
            }
        }

        private void SetFilenames(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(this.WorkingDirectory);
            foreach (FileInfo bestand in directoryInfo.GetFiles("*.cont.xml"))
            {
                string fullName = directoryInfo.FullName;
                string[] strArray1 = File.ReadAllText(bestand.FullName).Split('\n');
                string str1 = "msgbox";
                string str2 = Enumerable.Last<string>((IEnumerable<string>)bestand.FullName.Split('.')[0].Split('\\')) + ".xml";
                foreach (string str3 in strArray1)
                {
                    char[] chArray = new char[1]
          {
            ';'
          };
                    string[] strArray2 = str3.Split(chArray);
                    if (strArray2[0] == "ReceivedFileName")
                        str2 = Enumerable.Last<string>((IEnumerable<string>)strArray2[1].Split('\\'));
                    if (strArray2[0] == "ReceivePortName" && str1 == "msgbox")
                        str1 = strArray2[1];
                    if (strArray2[0] == "ReceiveLocationName")
                        str1 = strArray2[1];
                }
                try
                {
                    if (!Directory.Exists(fullName + "\\" + str1))
                        Directory.CreateDirectory(fullName + "\\" + str1);
                    File.Copy(this.WorkingDirectory + "\\" + SaveMessagesForm.GetXmlbestandsnaam(bestand), fullName + "\\" + str1 + "\\" + str2);
                }
                catch (Exception ex)
                {
                    TextBox textBox = this.txtoutput;
                    string str3 = textBox.Text + ex.Message;
                    textBox.Text = str3;
                }
            }
            int num = (int)MessageBox.Show("done");
        }

        private void btnExecutePipeline_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(this.WorkingDirectory))
                Directory.CreateDirectory(this.WorkingDirectory);
            try
            {
                foreach (string str in Directory.GetFiles(this.tbFolderPipeline.Text, "*.cont.xml"))
                {
                    SaveMessages.HelperClass.TypeHelper typeHelper = (SaveMessages.HelperClass.TypeHelper)this.cbPipelines.SelectedItem;
                    IBaseMessage fromStream = PipeLineExecuter.CreateFromStream((Stream)File.OpenRead(this.tbFolderPipeline.Text + "\\" + SaveMessagesForm.GetXmlbestandsnaam(new FileInfo(str))));
                    Methods.addContextFromFile(fromStream, str, this.WorkingDirectory);
                    List<IBaseMessage> list = PipeLineExecuter.ExecutePipeline(typeHelper.theType, fromStream, typeHelper.path, new PipelineContext());
                    File.Copy(str, this.WorkingDirectory + "\\" + Enumerable.Last<string>((IEnumerable<string>)str.Split('\\')));
                    foreach (IBaseMessage baseMessage in list)
                    {
                        FileStream fileStream = new FileStream(this.WorkingDirectory + "\\" + Enumerable.First<string>((IEnumerable<string>)Enumerable.Last<string>((IEnumerable<string>)str.Split('\\')).Split('.')) + ".xml", FileMode.CreateNew);
                        baseMessage.BodyPart.Data.CopyTo((Stream)fileStream);
                        StreamWriter streamWriter = new StreamWriter((Stream)fileStream);
                        ((TextWriter)streamWriter).Flush();
                        streamWriter.Close();
                        streamWriter.Dispose();
                        fileStream.Close();
                        fileStream.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                int num = 0;
                for (; exception != null; exception = exception.InnerException)
                {
                    TextBox textBox1 = this.txtoutput;
                    string str1 = textBox1.Text + (object)"layer " + (string)(object)num;
                    textBox1.Text = str1;
                    TextBox textBox2 = this.txtoutput;
                    string str2 = textBox2.Text + exception.Message;
                    textBox2.Text = str2;
                    TextBox textBox3 = this.txtoutput;
                    string str3 = textBox3.Text + exception.StackTrace;
                    textBox3.Text = str3;
                    ++num;
                }
            }
            int num1 = (int)MessageBox.Show("done");
        }

        private void btnExecuteMap_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(this.WorkingDirectory))
                Directory.CreateDirectory(this.WorkingDirectory);
            foreach (string str1 in Directory.GetFiles(this.tbFolderMaps.Text, "*.cont.xml"))
            {
                try
                {
                    SaveMessages.HelperClass.TypeHelper typeHelper = (SaveMessages.HelperClass.TypeHelper)this.cbMaps.SelectedItem;
                    Stream stream = new Mapper()
                    {
                        Transform = ((TransformBase)Activator.CreateInstance(typeHelper.theType))
                    }.PerformTransform((Stream)File.OpenRead(this.tbFolderMaps.Text + "\\" + SaveMessagesForm.GetXmlbestandsnaam(new FileInfo(str1))));
                    FileStream fileStream = File.Create(this.WorkingDirectory + "\\" + SaveMessagesForm.GetXmlbestandsnaam(new FileInfo(str1)));
                    stream.Position = 0L;
                    stream.CopyTo((Stream)fileStream);
                    StreamWriter streamWriter = new StreamWriter((Stream)fileStream);
                    ((TextWriter)streamWriter).Flush();
                    streamWriter.Close();
                    streamWriter.Dispose();
                    stream.Close();
                    stream.Dispose();
                    File.Copy(str1, this.WorkingDirectory + "\\" + Enumerable.Last<string>((IEnumerable<string>)str1.Split('\\')));
                }
                catch (Exception ex)
                {
                    int num = (int)MessageBox.Show(ex.Message);
                    TextBox textBox = this.txtoutput;
                    string str2 = textBox.Text + ex.Message;
                    textBox.Text = str2;
                }
            }
            int num1 = (int)MessageBox.Show("done");
        }

        private void btnFolderPipeline_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            int num = (int)folderBrowserDialog.ShowDialog();
            this.tbFolderPipeline.Text = folderBrowserDialog.SelectedPath;
        }

        private void SetFolders(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(this.WorkingDirectory);
            foreach (FileInfo bestand in directoryInfo.GetFiles("*.cont.xml"))
            {
                string fullName = directoryInfo.FullName;
                string[] strArray1 = File.ReadAllText(bestand.FullName).Split('\n');
                string str1 = "msgbox";
                string xmlbestandsnaam = SaveMessagesForm.GetXmlbestandsnaam(bestand);
                foreach (string str2 in strArray1)
                {
                    char[] chArray = new char[1]
          {
            ';'
          };
                    string[] strArray2 = str2.Split(chArray);
                    if (strArray2[0] == "ReceivePortName" && str1 == "msgbox")
                        str1 = strArray2[1];
                    if (strArray2[0] == "ReceiveLocationName")
                        str1 = strArray2[1];
                }
                try
                {
                    if (!Directory.Exists(fullName + "\\" + str1))
                        Directory.CreateDirectory(fullName + "\\" + str1);
                    File.Copy(this.WorkingDirectory + "\\" + xmlbestandsnaam, fullName + "\\" + str1 + "\\" + Enumerable.Last<string>((IEnumerable<string>)xmlbestandsnaam.Split('\\')));
                    File.Copy(bestand.FullName, fullName + "\\" + str1 + "\\" + Enumerable.Last<string>((IEnumerable<string>)bestand.FullName.Split('\\')));
                }
                catch (Exception ex)
                {
                    TextBox textBox = this.txtoutput;
                    string str2 = textBox.Text + ex.Message;
                    textBox.Text = str2;
                }
            }
            int num = (int)MessageBox.Show("done");
        }



        private void btnFillHosts_Click(object sender, EventArgs e)
        {
            clbHosts.Items.Clear();
            foreach (object obj in new MessageExtracter()
            {
                ConnectionString = this.ConnectionStringBizTalk
            }.getApplications())
                ((ListBox.ObjectCollection)this.clbHosts.Items).Add(obj);
        }

        private void DirectoryForNamespace_Click(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(this.WorkingDirectory);
            foreach (FileInfo bestand in directoryInfo.GetFiles("*.cont.xml"))
            {
                string fullName = directoryInfo.FullName;
                string[] strArray1 = File.ReadAllText(bestand.FullName).Split('\n');
                string str1 = "none";
                string xmlbestandsnaam = SaveMessagesForm.GetXmlbestandsnaam(bestand);
                foreach (string str2 in strArray1)
                {
                    char[] chArray = new char[1]
          {
            ';'
          };
                    string[] strArray2 = str2.Split(chArray);
                    if (strArray2[0] == "MessageType")
                    {
                        string str3 = strArray2[1];
                        foreach (char oldChar in Path.GetInvalidFileNameChars())
                            str3 = str3.Replace(oldChar, '_');
                        str1 = str3.Trim();
                    }
                }
                try
                {
                    if (!Directory.Exists(fullName + "\\" + str1))
                        Directory.CreateDirectory(fullName + "\\" + str1);
                    File.Copy(this.WorkingDirectory + "\\" + xmlbestandsnaam, fullName + "\\" + str1 + "\\" + xmlbestandsnaam);
                    File.Copy(bestand.FullName, fullName + "\\" + str1 + "\\" + Enumerable.Last<string>((IEnumerable<string>)bestand.FullName.Split('\\')));
                }
                catch (Exception ex)
                {
                    TextBox textBox = this.txtoutput;
                    string str2 = textBox.Text + ex.Message;
                    textBox.Text = str2;
                }
            }
            int num = (int)MessageBox.Show("done");
        }

        private static string GetXmlbestandsnaam(FileInfo bestand)
        {
            string str = Enumerable.Last<string>((IEnumerable<string>)bestand.FullName.Split('\\'));
            return str.Substring(0, str.Length - 8) + "xml";
        }

        private void btnPickMainFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = this.WorkingDirectory;
            int num = (int)folderBrowserDialog.ShowDialog();
            this.tbWorkingDirectory.Text = folderBrowserDialog.SelectedPath;
        }

        private void btnFolderMaps_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = this.tbFolderMaps.Text;
            int num = (int)folderBrowserDialog.ShowDialog();
            this.tbFolderMaps.Text = folderBrowserDialog.SelectedPath;
        }

        private void cbViews_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tvInstances.Nodes.Clear();
            MessageExtracter messageExtracter = new MessageExtracter()
            {
                ConnectionString = this.ConnectionStringBizTalk
            };
            if (this.cbViews.Text == "Suspended")
                this.AddInstancesToTree(messageExtracter.getSuspendedInstances());
            if (this.cbViews.Text == "Active")
                this.AddInstancesToTree(messageExtracter.GetActiveInstances());
            if (this.cbViews.Text == "Suspended per Application")
                this.AddInstancesToTreeGroupped(messageExtracter.getSuspendedInstances());
            if (!(this.cbViews.Text == "Active per Application"))
                return;
            this.AddInstancesToTreeGroupped(messageExtracter.GetActiveInstances());
        }

        private void AddInstancesToTree(List<Instance> instances)
        {
            foreach (Instance instance in instances)
                this.tvInstances.Nodes.Add((TreeNode)new ExtendedTreeNode(instance.ToString())
                {
                    theInstance = instance
                });
        }

        private void AddInstancesToTreeGroupped(List<Instance> instances)
        {
            foreach (IGrouping<string, Instance> grouping in Enumerable.GroupBy<Instance, string>((IEnumerable<Instance>)instances, (Func<Instance, string>)(c => c.Application)))
            {
                ExtendedTreeNode extendedTreeNode = new ExtendedTreeNode(grouping.Key);
                foreach (Instance instance in (IEnumerable<Instance>)grouping)
                    extendedTreeNode.Nodes.Add((TreeNode)new ExtendedTreeNode(instance.ToString())
                    {
                        theInstance = instance
                    });
                this.tvInstances.Nodes.Add((TreeNode)extendedTreeNode);
            }
        }

        private void tvInstances_Click(object sender, EventArgs e)
        {
            this.ShowInstance();
        }

        private void ShowInstance()
        {
            if (this.tvInstances.SelectedNode == null || ((ExtendedTreeNode)this.tvInstances.SelectedNode).theInstance == null)
                return;
            int num = (int)new InstanceScreen(((ExtendedTreeNode)this.tvInstances.SelectedNode).theInstance, this.ConnectionStringBizTalk).ShowDialog();
        }

        private void tvInstances_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(((object)e.KeyCode).ToString().ToLower() == "return"))
                return;
            this.ShowInstance();
        }

        private void btnLoadHost_Click(object sender, EventArgs e)
        {
            this.cbHost.Items.Clear();
            foreach (DataRow dataRow in (InternalDataCollectionBase)Methods.getDtaDataSet("select nvcApplicationName from applications", this.ConnectionStringBizTalk).Tables[Methods.BaseTable].Rows)
                this.cbHost.Items.Add((object)dataRow["nvcApplicationName"].ToString());
        }

        private void btnFlush_Click(object sender, EventArgs e)
        {
            if (this.cbHost.SelectedItem != null && !Methods.IsNullOrWhiteSpace(this.cbHost.SelectedItem.ToString()))
            {
                MessageExtracter messageExtracter = new MessageExtracter();
                messageExtracter.logger = (IExceptionLogger)new Logger();
                messageExtracter.ConnectionString = this.ConnectionStringBizTalk;
                if (this.cbExportFirst.Checked)
                    messageExtracter.SaveAndDeleteMessagesFromHost(this.cbHost.SelectedItem.ToString(), this.WorkingDirectory, this.cbInstanceType.Checked, this.cbDirectoryforInstance.Checked, this.cbIDirectoryInstanceForNamespace.Checked);
                else
                    messageExtracter.DeleteMesagesFromHost(this.cbHost.SelectedItem.ToString());
                int num = (int)MessageBox.Show("Done");
            }
            else
            {
                int num1 = (int)MessageBox.Show("Select a host");
            }
        }


    }
}
