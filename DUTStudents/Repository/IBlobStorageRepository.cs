
using DUTStudents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DUTStudents.Repository
{

    public interface IBlobStorageRepository
    {
        

            IEnumerable<BlobViewModel> GetBlobs(String name);
            bool DeleteBlob(string file, string fileExtension);
            bool UploadBlob(HttpPostedFileBase blobfile);

            Task<bool> DownloadBlobAsync(string file, string fileExtension);


        
    }
}