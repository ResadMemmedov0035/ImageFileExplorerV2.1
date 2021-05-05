using ImageFileExplorerV2.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
                folder.Images[i] = GetImage(images[i]);
            }

            return folder;
        }

        public async Task<Folder> GetFolderAsync(string path)
        {
            Folder folder = new Folder
            {
                Title = GetDirName(path),
                Path = path
            };

            var images = Directory.GetFiles(path)
                                  .Where(x => x.EndsWith(".jpg") || x.EndsWith(".png"))
                                  .ToArray();

            var tasks = new List<Task<Image>>();

            foreach(string image in images)
            {
                tasks.Add(Task.Run(() => GetImage(image)));
            }

            folder.Images = await Task.WhenAll(tasks);

            return folder;
        }

        private Image GetImage(string imgPath)
        {
            return new Image
            {
                Path = imgPath,
                Name = Path.GetFileName(imgPath),
                ItemType = Path.GetExtension(imgPath).ToUpper().Substring(1),
                Size = new FileInfo(imgPath).Length / 1024D,
                Created = new FileInfo(imgPath).CreationTime
            };
        }

        private string GetDirName(string path)
        {
            string[] dirs = path.Split('\\');
            return dirs[dirs.Length - 1];
        }
    }
}
