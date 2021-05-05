using GalaSoft.MvvmLight.Command;
using ImageFileExplorerV2.Models;
using ImageFileExplorerV2.Services;
using Microsoft.WindowsAPICodePack.Dialogs;
using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ImageFileExplorerV2.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainVM
    {
        private IImageFolderService _folderService;
        private CommonOpenFileDialog dialog = new CommonOpenFileDialog { IsFolderPicker = true };

        private int selectedImgIndex = -1;

        public ObservableCollection<Folder> Folders { get; set; } = new ObservableCollection<Folder>();
        public Image SelectedImage { get; set; }
        public Folder SelectedFolder { get; set; }
        public int SelectedImgIndex { get => selectedImgIndex;
            set
            {
                selectedImgIndex = value;
                GoRightCommand.RaiseCanExecuteChanged();
                GoLeftCommand.RaiseCanExecuteChanged();
            }
        }
        //public int ProgressValue { get; set; }


        public RelayCommand AddFolderCommand { get; set; }
        public RelayCommand<Folder> RemoveFolderCommand { get; set; }
        public RelayCommand GoRightCommand { get; set; }
        public RelayCommand GoLeftCommand { get; set; }

        public MainVM(IImageFolderService folderService)
        {
            InitializeCommands();
            _folderService = folderService;
        }

        private void InitializeCommands()
        {
            AddFolderCommand = new RelayCommand(async () => 
            {
                await AddFolderAsync();
            });
            RemoveFolderCommand = new RelayCommand<Folder>(x => Folders.Remove(x));
            GoRightCommand = new RelayCommand(() => SelectedImgIndex++, () => SelectedImgIndex < ImageSourceLength() - 1);
            GoLeftCommand = new RelayCommand(() => SelectedImgIndex--, () => SelectedImgIndex > 0);
        }

        private async Task AddFolderAsync()
        {
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string path = dialog.FileName;

                if (!Folders.Any(x => x.Path == path)) // contains
                {
                    var folder = await _folderService.GetFolderAsync(path);
                    Folders.Add(folder);
                }
                else
                    MessageBox.Show("This folder has already been added");

            }
        }

        private void AddFolder()
        {
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string path = dialog.FileName;

                if (!Folders.Any(x => x.Path == path)) // contains
                {
                    var folder = _folderService.GetFolder(path);
                    Folders.Add(folder);
                }
                else
                    MessageBox.Show("This folder has already been added");
            
            }
        }

        private int ImageSourceLength()
        {
            var len = SelectedFolder?.Images.Length;
            return len.HasValue ? len.Value : default;
        }    
    }

}
