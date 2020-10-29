using JA_Project.Helpers;
using Microsoft.Win32;
using Siema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JA_Project
{
    public class MainWindowViewModel : BaseViewModel
    {

        #region Public Properties

        public long TimeInMiliSec { get; set; } = 20;
        public BitmapImage OriginalImage { get; set; }
        public BitmapImage ExtendedPicture { get; set; }
        public int ThreadsNumber { get; set; } = 1;
        public bool IfAsm { get; set; } 
        public bool IfCSharp { get; set; }
        public double Scale { get; set; } = 2;

        #endregion


        #region Private variables

        private string imagePath;

        #endregion


        #region Commands

        public ICommand LoadPictureCommand { get; set; }
        public ICommand ExtendPictureCommand { get; set; }

        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            LoadPictureCommand = new RelayCommand(() => LoadPicture());
            ExtendPictureCommand = new RelayCommand(() => ExtendPicture());
        }

        #endregion

        #region Private functions
        
        private void LoadPicture()
        {
            OpenFileDialog ofdPicture = new OpenFileDialog();
            ofdPicture.Filter = "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
            ofdPicture.FilterIndex = 1;

            if (ofdPicture.ShowDialog() == true)
            {
                OriginalImage = new BitmapImage(new Uri(ofdPicture.FileName));
                imagePath = ofdPicture.FileName;
            }

            OnPropertyChanged("OriginalImage");
        }

        private void ExtendPicture()
        {

            if (IfCSharp == true)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Bitmap bitmap = ImageExtender.Extend(imagePath, (float)Scale);
                watch.Stop();

                TimeInMiliSec = watch.ElapsedMilliseconds;

                ExtendedPicture = bitmap.Bitmap2BitmapImage();

                OnPropertyChanged("ExtendedPicture");
                OnPropertyChanged("TimeInMiliSec");
            }

            else if (IfAsm == true)
            {
                ThreadsNumber = 40;
                OnPropertyChanged("ThreadsNumber");
            }
        }


        #endregion
    }
}
