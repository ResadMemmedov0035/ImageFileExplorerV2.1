using GalaSoft.MvvmLight.Command;
using ImageFileExplorerV2.Models;
using ImageFileExplorerV2.Services;
using Microsoft.WindowsAPICodePack.Dialogs;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ImageFileExplorerV2.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainVM
    {
        private IImageFolderService _folderService;

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
            AddFolderCommand = new RelayCommand(() => AddFolder());
            RemoveFolderCommand = new RelayCommand<Folder>(x => Folders.Remove(x));
            GoRightCommand = new RelayCommand(() => SelectedImgIndex++, () => SelectedImgIndex < ImageSourceLength() - 1);
            GoLeftCommand = new RelayCommand(() => SelectedImgIndex--, () => SelectedImgIndex > 0);
        }

        private void AddFolder()
        {
            var dialog = new CommonOpenFileDialog { IsFolderPicker = true };

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
