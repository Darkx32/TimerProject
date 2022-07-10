using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace TimerProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isRunning = false;
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerFunction);
            timer.Interval = new TimeSpan(0, 0, 1);
        }

        private void PlayAndPause_Click(object sender, RoutedEventArgs e)
        {
            if (isRunning)
            {
                isRunning = false;
                timer.Stop();

                var bitmap = new BitmapImage();
                using (var stream = new FileStream("Resources\\play.png", FileMode.Open))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                }
                PlayAndPauseImage.Source = bitmap;
            }
            else
            {
                isRunning = true;
                timer.Start();
                var bitmap = new BitmapImage();

                using (var stream = new FileStream("Resources\\pause.png", FileMode.Open))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                }
                PlayAndPauseImage.Source = bitmap;
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            if (isRunning)
            {
                isRunning = false;
                timer.Stop();
                Minutes.Content = "00";
                Seconds.Content = "00";

                var bitmap = new BitmapImage();
                using (var stream = new FileStream("Resources\\play.png", FileMode.Open))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                }
                PlayAndPauseImage.Source = bitmap;
            }
        }

        private void timerFunction(object render, EventArgs e)
        {
            if (!isRunning) return;
            int secs = int.Parse(Seconds.Content.ToString());
            secs++;
            if(secs >= 60)
            {
                secs = 0;
                int minute = int.Parse(Minutes.Content.ToString()) + 1;
                Minutes.Content = transform(minute);
            }
            Seconds.Content = transform(secs);
        }

        private string transform(int value)
        {
            if (value < 10) return "0" + value;
            return value.ToString();
        }
    }
}
