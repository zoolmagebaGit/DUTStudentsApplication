using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DUTStudents.Repository;

namespace DUTStudents.Controllers
{
    public class BlobController : Controller
    {
        public readonly IBlobStorageRepository repo;
        public BlobController(IBlobStorageRepository _repo)
        {
            this.repo = _repo;

        }
        // GET: Blob
        public ActionResult Index(string name)
        {
            var blobVM = repo.GetBlobs(name);
            return View(blobVM);
        }
        public JsonResult RemoveBlob(string file, string extension)
        {
            bool isDeleted = repo.DeleteBlob(file, extension);
            return Json(isDeleted, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> DownloadBlob(string file, string extension)
        {
            bool isDownloaded = await repo.DownloadBlobAsync(file, extension);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UploadBlob()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadBlob(HttpPostedFileBase UploadFileName)
        {
            bool isUploaded = repo.UploadBlob(UploadFileName);
            if (isUploaded == true)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
     
    }
}