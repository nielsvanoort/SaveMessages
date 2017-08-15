using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaveMessages.HelperClass
{
    public class TaskedBackgroundWorker : BackgroundWorker
    {
        public Task TheTask;
        
        private Form ProgressForm = new Form();
        private Label progressLabel = new Label();
        private ProgressBar progressProgressBar = new ProgressBar();

        public delegate void SetMessageHandler(object sender, MessageEventArgs theMessage);
        
        public event SetMessageHandler SetMessage;



        public TaskedBackgroundWorker()
        {
            DoWork += backgroundWorker_DoWork;
            ProgressChanged += backgroundWorker_ProgressChanged;
            RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            SetMessage += TaskedBackgroundWorker_SetMessage;

            ProgressForm = new Form();
            ProgressForm.Height = 150;
            ProgressForm.Width = 640;
            ProgressForm.Show();
            progressLabel = new Label() {Text = "initializing", Left = 10, Top = 10, Width = 500};
            ProgressForm.Controls.Add(progressLabel);

            progressProgressBar = new ProgressBar() { Left = 10, Top =50 , Width = 500};
            ProgressForm.Controls.Add(progressProgressBar);
            progressProgressBar.Maximum = 100;
            progressProgressBar.Step = 1;
            progressProgressBar.Value = 0;
        
        }

        void TaskedBackgroundWorker_SetMessage(object sender, MessageEventArgs e)
        {
            progressLabel.Text = e.TheMessage;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var backgroundWorker = sender as TaskedBackgroundWorker;
            backgroundWorker.TheTask.RunSynchronously();

        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var backgroundWorker = sender as TaskedBackgroundWorker;
            backgroundWorker.progressProgressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgressForm.Close();
        }

        public void setText(string Text)
        {

            this.ProgressForm.Invoke(SetMessage, new object[] { this, new MessageEventArgs() { TheMessage = Text } });
         
        }


    }
}
