using System.IO;

namespace DUTStudents.Models
{

    public class BlobViewModel
    {
        public string BlobContainerName { get; set; }
        public string StorageUrl { get; set; }
        public string ActualFileName { get; set; }
        public string PrimaryUrl { get; set; }
        public string fileExtention { get; set; }

        public string FileNameWithoutExt
        {
            get
            {
                return Path.GetFileNameWithoutExtension(ActualFileName);

            }

        }
        public string FileNameExtensionOnly
        {
            get
            {
                return Path.GetExtension(ActualFileName).Substring(0);

            }

        }
    }
}