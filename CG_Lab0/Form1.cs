namespace CG_Lab0
{
    public partial class Form1 : Form
    {
        Bitmap image;
        public Form1()
        {
            InitializeComponent();
        }

        private void îòêğûòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files|*.png;*.jpg;*.bmp|All files(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(ofd.FileName);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
        }

        private void êîíâåğòàöèÿÂÎòòåíêèÑåğîãîToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GreyFilter filter = new GreyFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void áèíàğèçàöèÿÏîÏîğîãóToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int limit = int.Parse(textBox1.Text);

            BinaryFilter filter = new BinaryFilter(limit);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void óâåëè÷åíèåßğêîñòèÍàÊîíñòàíòóToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int deltaBrightness = int.Parse(textBox1.Text);

            IncBrightnessFilter filter = new IncBrightnessFilter(deltaBrightness);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);
            if (backgroundWorker1.CancellationPending != true)
                image = newImage;
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            progressBar1.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }
    }
}