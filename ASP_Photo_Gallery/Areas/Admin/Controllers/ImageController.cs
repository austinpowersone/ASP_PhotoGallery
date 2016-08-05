using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ASP_Photo_Gallery.Areas.Admin.Models;
using ASP_Photo_Gallery.Core;
using ASP_Photo_Gallery.DAL.Abstract;
using ASP_Photo_Gallery.DAL.Concrete;

namespace ASP_Photo_Gallery.Areas.Admin.Controllers
{
    [Authorize]
    public class ImageController : Controller
    {
        private readonly IRepository<Image> _imageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ImageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _imageRepository = _unitOfWork.Repository<Image>();
        }

        public ImageController() : this(new UnitOfWork())
        {

        }

        [HttpGet]
        public ActionResult Index()
        {
            var images = _imageRepository.Query().ToList();

            return View(images);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var image = _imageRepository.Query().Find(id);

            return View(image);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var image = _imageRepository.Query().Find(id);

            _imageRepository.Delete(image);
            _unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var image = _imageRepository.Query().Find(id);
            var model = new ImageEditViewModel
            {
                Description = image.Description,
                Id = image.Id
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ImageEditViewModel imageEditViewModel, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                var image = _imageRepository.Query().Find(imageEditViewModel.Id);
                var now = DateTime.Now;
                image.Description = imageEditViewModel.Description;
                image.UpdateDate = now;

                if (imageEditViewModel.File != null)
                {
                    var filename = now.ToString("yyyy-MM-dd-HH-mm-ss") + "-" + Guid.NewGuid() +
                                   Path.GetExtension(imageEditViewModel.File.FileName);

                    image.Path = filename;
                    image.Name = imageEditViewModel.File.FileName;

                    var path = Path.Combine(Server.MapPath(@"~\Content\Images"), filename);
                    imageEditViewModel.File.SaveAs(path);                                        
                }
                _imageRepository.Update(image);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(imageEditViewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImageCreateViewModel imageCreateViewModel, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                if (imageCreateViewModel.File != null)
                {
                    var image = new Image();
                    var now = DateTime.Now;

                    var filename = now.ToString("yyyy-MM-dd-HH-mm-ss") + "-" + Guid.NewGuid() +
                                   Path.GetExtension(imageCreateViewModel.File.FileName);

                    image.Description = imageCreateViewModel.Description;
                    image.UpdateDate = now;
                    image.InsertDate = now;
                    image.Path = filename;
                    image.Name = imageCreateViewModel.File.FileName;

                    var path = Path.Combine(Server.MapPath(@"~\Content\Images"), filename);
                    imageCreateViewModel.File.SaveAs(path);

                    _imageRepository.Insert(image);
                    _unitOfWork.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            return View(imageCreateViewModel);
        }
    }
}