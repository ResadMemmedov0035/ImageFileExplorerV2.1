using System;

namespace ImageFileExplorerV2.Models
{
    public class Image
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public double Size { get; set; }
        public string ItemType { get; set; }
        public DateTime Created { get; set; }
    }
}
