﻿namespace TechZone.Web.Controllers
{
    using System.Web.Mvc;
    using Models.ViewModels.Customer;
    using Microsoft.AspNet.Identity;
    using Attributes;
    using System.IO;
    using System.Web;
    using System;
    using Services.Contracts;

    [RoutePrefix("Customers")]
    [CustomAuthorize(Roles = "Customer")]
    public class CustomersController : Controller
    {
        private readonly ICustomersService _service;

        public CustomersController(ICustomersService service)
        {
            this._service = service;
        }

        [Route("UserProfile")]
        [HandleError(ExceptionType = typeof(ArgumentException), View = "WaitForDownload")]
        public ActionResult UserProfile()
        {
            var currentUserId = this.User.Identity.GetUserId();
            try
            {
                CustomerProfileViewModel customerProfileVm = this._service.GetCurrentUserProfile(currentUserId);
                return View(customerProfileVm);
            }
            catch (Exception)
            {               
                throw new ArgumentException();
            }
        }

        [Route("UploadPicture")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadPicture()
        {
            string currentUserId = User.Identity.GetUserId();
            CustomerProfileViewModel customerProfileVm = this._service.GetCurrentUserProfile(currentUserId);

            HttpPostedFileBase file = this.Request.Files["picture"];

            if (file == null)
            {
                ModelState.AddModelError("", "Please browse for a valid image file, and then click Upload.");
                return this.View("UserProfile", customerProfileVm);
            }

            if (file.ContentLength > 2097152)
            {
                ModelState.AddModelError("", "Image size should be less than 2mb.");
                return this.View("UserProfile", customerProfileVm);
            }

            string fileName = Path.GetFileName(file.FileName);
            if (!fileName.ToLower().EndsWith(".jpg") && !fileName.ToLower().EndsWith(".png") && !fileName.ToLower().EndsWith(".jpeg"))
            {
                ModelState.AddModelError("", "Invalid picture format!");
                return this.View("UserProfile", customerProfileVm);
            }

            MemoryStream memstr = new MemoryStream();
            file.InputStream.CopyTo(memstr);
            byte[] imageData = memstr.ToArray();

            this._service.UploadUserProfilePicture(currentUserId, fileName, imageData);
            return RedirectToAction("UserProfile", "Customers");
        }

        [Route("Order/{id?}")]
        public ActionResult Order(int? id)
        {
            string currentUserId = User.Identity.GetUserId();
            if (id == null || !this._service.OrderBellongsToCurrentUser(currentUserId, id))
            {
                return RedirectToAction("UserProfile", "Customers");
            }

            var pdfFile = this._service.DownloadOrderInvoice(currentUserId, id);
            MemoryStream ms = new MemoryStream(pdfFile);
            return new FileStreamResult(ms, "application/pdf");
        }

        [HttpPost]
        [Route("SubmitChatRequest")]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitChatRequest(string message)
        {
            string currentUserId = User.Identity.GetUserId();
            string roomNumber = this._service.GenerateChatRoom(currentUserId, message);
            return RedirectToAction("ChatRoom", "Maintain", new {area = "Moderator",  roomId = roomNumber });
        }
    }
}