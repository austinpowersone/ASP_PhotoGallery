using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ASP_Photo_Gallery.DAL.Abstract;
using ASP_Photo_Gallery.DAL.Concrete;
using Ionic.Zip;
using Image = ASP_Photo_Gallery.Core.Image;

namespace ASP_Photo_Gallery.Controllers
{
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

        public ActionResult Index()
        {
            var images = _imageRepository.Query().ToList();
            return View(images);
        }

        public ActionResult Details(int id)
        {
            var image = _imageRepository.Query().Find(id);
            return View(image);
        }

        public ActionResult Show(int id)
        {
            var image = _imageRepository.Query().Find(id);
            var path = Path.Combine(Server.MapPath("~/Content/Images"), image.Path);
            try
            {
                var bmp = new Bitmap(path);
                using (var stream = new MemoryStream())
                {
                    bmp.Save(stream, ImageFormat.Jpeg);
                    return File(stream.ToArray(), "image/jpeg");
                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }

        }

        public ActionResult Zip()
        {
            using (var zip = new ZipFile())
            {
                foreach (var image in _imageRepository.Query().ToList())
                {
                    var filepath = Path.Combine(Server.MapPath("~/Content/Images"), image.Path);
                    zip.AddFile(filepath, "images");
                }

                using (var stream = new MemoryStream())
                {
                    zip.Save(stream);
                    return File(stream.ToArray(), "application/zip");
                }
            }
        }
    }
}