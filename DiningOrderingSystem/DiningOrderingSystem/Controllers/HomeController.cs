using DiningOrderingSystem.Data;
using DiningOrderingSystem.Models;
using DiningOrderingSystem.Models.Data;
using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace DiningOrderingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly NoticeDbContext noticeDbContext;

        public HomeController(NoticeDbContext noticeDbContext)
        {
            this.noticeDbContext = noticeDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var noticeList = await noticeDbContext.NoticeItemList.ToListAsync();
            return View(noticeList);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddNotice()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNotice(NoticeViewModel noticeItem)
        {
            
            if (ModelState.IsValid)
            {
                var htmlSanitizer = new HtmlSanitizer();
                htmlSanitizer.AllowedTags.Clear();
                var noticeTitle = htmlSanitizer.Sanitize(noticeItem.NoticeTitle);
                var noticeContent = htmlSanitizer.Sanitize(noticeItem.NoticeContent);
                if(noticeContent != "" & noticeTitle != "")
                {
                    var notice = new NoticeItem()
                    {
                        NoticeTitle = noticeTitle,
                        NoticeContent = noticeContent,
                        NoticeDate = DateTime.Today
                    };

                    try
                    {
                        await noticeDbContext.NoticeItemList.AddAsync(notice);
                        await noticeDbContext.SaveChangesAsync();
                        TempData["Message"] = "Notice added !!!";
                        TempData["Status"] = "200";
                        return RedirectToAction("Index");
                    }
                    catch (Exception sqlErr)
                    {
                        if (sqlErr.InnerException.Message.Contains("UNIQUE"))
                        {
                            TempData["Message"] = "Please use unique Notice Title !!!";
                            TempData["Status"] = "400";
                            return View();
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }      
                }
                else
                {
                    TempData["Message"] = "Enter valid details !!!";
                    TempData["Status"] = "400";
                    return View();
                }
                
            }
            else
            {
                TempData["Message"] = "Please enter all the fields";
                TempData["Status"] = "400";
                return View();
            }
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async new Task<IActionResult> DeleteNotice(String NoticeTitle)
        {
            var htmlSanitizer = new HtmlSanitizer();
            htmlSanitizer.AllowedTags.Clear();
            NoticeTitle = htmlSanitizer.Sanitize(NoticeTitle);
            var notice = await noticeDbContext.NoticeItemList.FirstOrDefaultAsync(x => x.NoticeTitle == NoticeTitle);
            if (notice != null)
            {
                noticeDbContext.NoticeItemList.Remove(notice);
                await noticeDbContext.SaveChangesAsync();

                TempData["Message"] = "Notice Deleted !!!";
                TempData["Status"] = "200";

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult PageNotFoundPage()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}