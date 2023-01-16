using System.Media;
using WMPLib;

namespace Alarm
{
    public partial class Form1 : Form
    {
        private int hours, minutes, seconds;
        private long commonTicks, ticks;
        SoundPlayer player;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.hours = this.minutes = this.seconds = 0;
            this.commonTicks = this.ticks = 0;
            player = new(@"../../../sounds/AlarmSound.wav");
            this.maskedTextBox1.Text = "00:00:00";
            this.timer1.Interval = 1000;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (hours == 0 && minutes == 0 && seconds == 0)
                return;
            this.timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.timer1.Stop();
            player.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label1.Text = $"{hours:00}:{minutes:00}:{seconds:00}";
            if (hours == 0 && minutes == 0 && seconds == 0)
            {
                this.timer1.Stop();
                player.Play();
                return;
            }
            if (seconds > 0)
                seconds -= 1;
            else
            {
                if (minutes > 0)
                    minutes -= 1;
                else
                {
                    hours -= 1;
                    minutes = 59;
                }
                seconds = 59;
            }
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            TimeOnly date;
            if (!TimeOnly.TryParse(this.maskedTextBox1.Text, out date))
                return;
            SplitDateByInt(date);
            FromDateToTicks(date);
        }

        private void SplitDateByInt(TimeOnly date)
        {
            this.hours = date.Hour;
            this.minutes = date.Minute;
            this.seconds = date.Second;
        }

        private void FromDateToTicks(TimeOnly date)
        {
            this.commonTicks = 0;
            if (date.Hour == 0 && date.Minute == 0 && date.Second == 0)
                return;
            this.commonTicks += date.Second;
            this.commonTicks += date.Minute * 60;
            this.commonTicks += date.Hour * 60 * 60;
        }
    }
}