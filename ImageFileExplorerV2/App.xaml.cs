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
        public static SimpleIoc Container = new SimpleIoc();

        public App() { }

        protected override void OnStartup(StartupEventArgs e)
        {
            Container.Register<MainVM>();
            Container.Register<IImageFolderService, ImageFolderService>();

            base.OnStartup(e);
        }
    }
}