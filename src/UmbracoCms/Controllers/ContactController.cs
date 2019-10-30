using System.Web.Mvc;
using Umbraco.Web.Mvc;
using UmbracoCms.Models;

namespace UmbracoCms.Controllers
{
    public class ContactController : SurfaceController
    {
        [ChildActionOnly]
        public ActionResult ContactForm()
        {
            return PartialView("_ContactForm", new ContactFormModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleContactForm()
        {
            // Business Logic here
            TempData["ThankYou"] = true;
            return RedirectToCurrentUmbracoPage();
        }
    }
}
