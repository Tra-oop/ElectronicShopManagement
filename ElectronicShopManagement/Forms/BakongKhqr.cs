using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectronicShopManagement.Forms
{
    public partial class BakongKhqr : Form
    {
        private int remainingSeconds = 60; // 1 minutes
        private Timer countdownTimer;
        public  BakongKhqr(Image qrCodeImage,decimal totalAmount)
        {
            InitializeComponent();
            pictureBox1.Image = qrCodeImage;
            labeltotalamountpay.Text = totalAmount.ToString("0.00");

             
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void BakongKhqr_Load(object sender, EventArgs e)
        {
            countdownTimer = new Timer();
            countdownTimer.Interval = 1000; // 1 second
            countdownTimer.Tick += CountdownTimer_Tick;

            lblCountdown.Text = FormatTime(remainingSeconds);
            countdownTimer.Start();
        }
        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (remainingSeconds > 0)
            {
                remainingSeconds--;
                lblCountdown.Text = FormatTime(remainingSeconds);
            }
            else
            {
                countdownTimer.Stop();
                lblCountdown.Text = "⏰ Time’s up!";
            }
        }

        private string FormatTime(int totalSeconds)
        {
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            return $"{minutes:D2}:{seconds:D2}";
        }
    }
}
