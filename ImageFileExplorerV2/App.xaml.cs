using System;
using System.Threading;
using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using ImageFileExplorerV2.Services;
using ImageFileExplorerV2.ViewModels;

namespace ImageFileExplorerV2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Mutex mutex;
        public static SimpleIoc Container = new SimpleIoc();

        protected override void OnStartup(StartupEventArgs e)
        {
            if (Mutex.TryOpenExisting("AppMutex", out Mutex m))
            {
                Environment.Exit(0);
            }
            mutex = new Mutex(true, "AppMutex");


            Container.Register<MainVM>();
            Container.Register<IImageFolderService, ImageFolderService>();

            base.OnStartup(e);
        }
    }
}