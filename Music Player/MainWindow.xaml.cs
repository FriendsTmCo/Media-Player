using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;

namespace Music_Player
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private MediaPlayer player = new MediaPlayer();
        List<string> mediasource = new List<string>();
        int mediachange = 0;
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            var openFile = new OpenFileDialog();
            openFile.Multiselect = true;
            if (openFile.ShowDialog() == true)
            {
                foreach (var items in openFile.FileNames)
                {
                    mediasource.Add(items);
                }
                lsMedias.ItemsSource = mediasource;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you Sure To Exit?","Warning",MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (lsMedias.ItemsSource == null)
            {
                btnBrowse_Click(sender, e);
            }
            else
            {
                foreach (var items in lsMedias.ItemsSource)
                {
                    mdMedias.Source = new Uri(items.ToString());
                    player.Play();
                    lsMedias.SelectedItem = items;
                }
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (mdMedias.CanPause)
            {
                mdMedias.Source = null;
                player.Pause();
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (mediasource.Count >= 2)
            {
                if (mediachange < mediasource.Count)
                {
                   mdMedias.Source = new Uri(mediasource[mediachange]);
                   lsMedias.SelectedItem = mediasource[mediachange];
                   mediachange++;
                }
                else
                {
                    mediachange = 0;
                }
            }
        }

        private void btnPreviouse_Click(object sender, RoutedEventArgs e)
        {
            if (mediasource.Count >= 2)
            {
                if (mediachange < mediasource.Count && mediachange != 0)
                {
                    mdMedias.Source = new Uri(mediasource[mediachange]);
                    lsMedias.SelectedItem = mediasource[mediachange];
                    mediachange--;
                }
                else
                {
                    mediachange = 0;
                }
            }
        }

        private void lsMedias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mdMedias.Source = new Uri(lsMedias.SelectedItem.ToString());
            player.Play();
        }


        private void SlVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mdMedias.Volume = SlVolume.Value*10;
        }
    }
}
