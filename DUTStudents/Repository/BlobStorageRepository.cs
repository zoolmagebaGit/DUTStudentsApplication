using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DUTStudents.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;

namespace DUTStudents.Repository

{

    public class BlobStoragerepository: IBlobStorageRepository
    {
        private StorageCredentials _storageCredentialsx;
        private CloudStorageAccount _cloudStorageAccountx;
        private CloudBlobClient _cloudBlobClientx;
        private CloudBlobContainer _cloudBlobContainerx;

        private string containerNamex = "studentscontainer";
        private string downloadPath = @"C:\\";
    
        public BlobStoragerepository()
        {
            string accontNamex = "studentastoraccount";
            string keyx = "P6vaLnRnlJQlRHuag6D3tCfUF4g7zRIYlUZOZHRFXBswtC+8o396WSo3FtHbR/yktHmBsT0mSVOf5Yif4pM43Q==";

            _storageCredentialsx = new StorageCredentials(accontNamex, keyx);
            _cloudStorageAccountx = new CloudStorageAccount(_storageCredentialsx, true);
            _cloudBlobClientx = _cloudStorageAccountx.CreateCloudBlobClient();
            _cloudBlobContainerx = _cloudBlobClientx.GetContainerReference(containerNamex);


        }
        public bool DeleteBlob(string file, string fileExtension)
        {
            //throw new NotImplementedException();
            _cloudBlobContainerx = _cloudBlobClientx.GetContainerReference(containerNamex);
            CloudBlockBlob blockBlob = _cloudBlobContainerx.GetBlockBlobReference(file + "." + fileExtension);
           bool delete = blockBlob.DeleteIfExists();
            return delete;
        }

        public async Task<bool> DownloadBlobAsync(string file, string fileExtension)
        {
            _cloudBlobContainerx = _cloudBlobClientx.GetContainerReference(containerNamex);
            CloudBlockBlob blockBlob = _cloudBlobContainerx.GetBlockBlobReference(file + "." + fileExtension);

            using (var fileStream = System.IO.File.OpenWrite(downloadPath + file + "." + fileExtension))
            {
                await blockBlob.DownloadToStreamAsync(fileStream);
                return true;
            }


            //throw new NotImplementedException();
        }

        public IEnumerable<BlobViewModel> GetBlobs(string name)
        {
            var context = _cloudBlobContainerx.ListBlobs(name).ToList();
         //   var context = _cloudBlobContainerx.ListBlobs().ToList();
            IEnumerable<BlobViewModel> VM = context.Select(x => new BlobViewModel
            {
                BlobContainerName = x.Container.Name,
                StorageUrl = x.StorageUri.PrimaryUri.ToString(),
                PrimaryUrl = x.StorageUri.PrimaryUri.ToString(),

                ActualFileName = x.Uri.AbsolutePath.Substring(x.Uri.AbsoluteUri.LastIndexOf(".",25) + 1),
                fileExtention = System.IO.Path.GetExtension(x.Uri.AbsoluteUri.Substring(x.Uri.AbsoluteUri.LastIndexOf(".",25) + 1))
            }).ToList();
            return VM;
        }
        
        public bool UploadBlob(HttpPostedFileBase blobfile)
        {
            if (blobfile == null)
            {
                return false;
            }
            // throw new NotImplementedException();
            _cloudBlobContainerx = _cloudBlobClientx.GetContainerReference(containerNamex);
            CloudBlockBlob blockBlob = _cloudBlobContainerx.GetBlockBlobReference(blobfile.FileName);

            using (var fileStream = (blobfile.InputStream))
            {
                blockBlob.UploadFromStream(fileStream);
            }
            return true;
        }
     
    }
}
