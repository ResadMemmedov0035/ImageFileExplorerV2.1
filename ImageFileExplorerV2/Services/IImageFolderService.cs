using ImageFileExplorerV2.Models;
using System.Threading.Tasks;

namespace ImageFileExplorerV2.Services
{
    public interface IImageFolderService
    {
        Folder GetFolder(string path);
        Task<Folder> GetFolderAsync(string path);
    }
}
