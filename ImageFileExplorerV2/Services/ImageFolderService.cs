using ImageFileExplorerV2.Models;
using System.IO;
using System.Linq;

namespace ImageFileExplorerV2.Services
{
    class ImageFolderService : IImageFolderService
    {
        public Folder GetFolder(string path)
        {
            Folder folder = new Folder
            {
                Title = GetDirName(path),
                Path = path
            };

            var images = Directory.GetFiles(path)
                                  .Where(x => x.EndsWith(".jpg") || x.EndsWith(".png"))
                                  .ToArray();

            folder.Images = new Image[images.Length];

            for (int i = 0; i < images.Length; i++)
            {
                folder.Images[i] = new Image
                {
                    Path = images[i],
                    Name = Path.GetFileName(images[i]),
                    ItemType = Path.GetExtension(images[i]).ToUpper().Substring(1),
                    Size = new FileInfo(images[i]).Length / 1024D,
                    Created = new FileInfo(images[i]).CreationTime
                };
            }

            return folder;
        }

        private string GetDirName(string path)
        {
            string[] dirs = path.Split('\\');
            return dirs[dirs.Length - 1];
        }
    }
}
