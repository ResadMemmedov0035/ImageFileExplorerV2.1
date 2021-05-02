using ImageFileExplorerV2.Models;

namespace ImageFileExplorerV2.Services
{
    public interface IImageFolderService
    {
        Folder GetFolder(string path);
    }
}
