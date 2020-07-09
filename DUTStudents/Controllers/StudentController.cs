
using System.Collections.Generic;
using System.Web.Mvc;
using DUTStudents.Models;
using System.Threading.Tasks;
using System.Net;
using DUTStudents.Repository;
using System.Web;

namespace DUTStudents.Controllers
{
    public class StudentController : Controller
    {
        public readonly IBlobStorageRepository repo;
        public StudentController(IBlobStorageRepository _repo)
        {
            this.repo = _repo;

        }

#pragma warning disable 1998
        [ActionName("Create")]
        public async Task<ActionResult> CreateAsync()
        {
            return View();
        }
#pragma warning restore 1998

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind(Include = "Id,StudentNo,Name,Surname,Email,Tel,Mobile,isActive")] Students student, HttpPostedFileBase UploadFileName)
        {
            if (ModelState.IsValid)
            {
                repo.UploadBlob(UploadFileName);
                student.ImageLink = UploadFileName.FileName;
                await DocumentDBRepository<Students>.CreateItemAsync(student);
                
                    return RedirectToAction("Search");
            }

            return View(student);
        }
        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Students student = await DocumentDBRepository<Students>.GetItemAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind(Include = "Id,StudentNo,Name,Surname,Email,Tel,Mobile,isActive")] Students student, HttpPostedFileBase UploadFileName)
        {
            if (ModelState.IsValid)
            {
                repo.UploadBlob(UploadFileName);
                student.ImageLink = UploadFileName.FileName;
                await DocumentDBRepository<Students>.UpdateItemAsync(student.Id, student);
                return RedirectToAction("Search");
            }

            return View(student);
        }
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Students student = await DocumentDBRepository<Students>.GetItemAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind(Include = "StudentNo")] string id)
        {
            await DocumentDBRepository<Students>.DeleteItemAsync(id);
            return RedirectToAction("Search");
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            Students student = await DocumentDBRepository<Students>.GetItemAsync(id);
            return View(student);
        }

        [ActionName("Search")]
        public async Task<ActionResult> IndexSearch(string searchParam, string searchType)
        {
            IEnumerable<Students> students;

            if (searchType == "StudentNo")

            {

                students = await DocumentDBRepository<Students>.GetItemsAsync(s => s.StudentNo.Contains(searchParam) && s.isActive);

            }

            else if (searchType == "Name")

            {

                students = await DocumentDBRepository<Students>.GetItemsAsync(s => s.Name.ToUpper().Contains(searchParam.ToUpper()) && s.isActive);

            }

            else if (searchType == "Surname")

            {

                students = await DocumentDBRepository<Students>.GetItemsAsync(s => s.Surname.ToUpper().Contains(searchParam.ToUpper()) && s.isActive);

            }

            else

            {

                students = await DocumentDBRepository<Students>.GetItemsAsync(s => s.isActive || !s.isActive);

            }

            return View(students);


        }
#pragma warning disable 1998
        [ActionName("Email")]
        public async Task<ActionResult> Email()
        {
            return View();
        }
#pragma warning restore 1998
        [HttpPost]
        [ActionName("Email")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Email([Bind(Include = "Password,Recipient,Name")] Email email, string id)
        {
            if (ModelState.IsValid)
            {
                Students student = await DocumentDBRepository<Students>.GetItemAsync(id);
                email.SendEmail(student);
                return View("Details", student);
            }
            return View("Search");
        }
        public ActionResult Index(string name)
        {
            var blobVM = repo.GetBlobs(name);
            return View(blobVM);
        }

    }   
}