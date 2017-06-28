using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace ErrorTracker.Controllers
{
    public class HomeController : Controller
    {
        List<string> stages = new List<string>()
        { "P2p", "RcmGroup", "RcmQa1", "UtasGroup", "UtasQa1", "UtasIhr", "Validate", "RcmGroup2",
    "RcmQa2", "UtasQa2", "RcmDelivery", "Delivered" };

        // GET: Home
        public ActionResult Index()
        {
            if (Session["id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public string ChangeStage(string id, string nextOrLast)
        {
            string stage = GetStage(id);
            int index = stages.IndexOf(stage);
            string newStage = "";
            if (nextOrLast == "next")
            {
                newStage = stages[index + 1];
            }
            else if(nextOrLast == "last")
            {
                newStage = stages[index - 1];
            }
            int id2 = Int32.Parse(id);
            using (ErrorTrackerEntities1 db = new ErrorTrackerEntities1())
            {
                var query = from book in db.Books
                             where book.BookId == id2
                              select book;

                foreach (Book book in query)
                {
                    book.stage = newStage;
                }
                db.SaveChanges();

            }
            Session["Stage"] = newStage;
            return newStage;
        }

        public string GetStage(string id)
        {
            int id2 = Int32.Parse(id);
            var categoryid = "";
            using (ErrorTrackerEntities1 db = new ErrorTrackerEntities1())
            {
                categoryid = (from book in db.Books
                              where book.BookId == id2
                              select book.stage).SingleOrDefault();
            }
            Session["Stage"] = categoryid;
            return (string)categoryid;
        }

        public JsonResult GetChartData()
        {
            ErrorTrackerEntities1 db = new ErrorTrackerEntities1();
            int id = Int32.Parse(Session["BookId"].ToString());
            string stage = Session["Stage"].ToString();
            int validComments = (from comments in db.Comments
                                                where comments.BookId == id && comments.ValidStatus == "V" && comments.Stage == stage
                                 select comments).Count();
            int InvalidComments = (from comments in db.Comments
                                    where comments.BookId == id &&  comments.ValidStatus == "I" && comments.Stage == stage
                                   select comments).Count();
            int OOSComments = (from comments in db.Comments
                                    where comments.BookId == id && comments.ValidStatus == "OOS" && comments.Stage == stage
                               select comments).Count();
            return Json(new { valid=validComments.ToString(),invalid=InvalidComments.ToString(), OOS=OOSComments.ToString() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetComments(string id1, string sidx, string sort, int page, int rows)
        {
            ErrorTrackerEntities1 db = new ErrorTrackerEntities1();
            sort = (sort == null) ? "" : sort;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            int id2 = Int32.Parse(id1);
            var CommentList = from c in db.Comments join b in db.Books on c.BookId equals b.BookId where c.BookId == id2 && c.Stage == b.stage
                              select new {c.CommentId, c.BookId, c.ItemNumber, c.Page, c.Comment1, c.ValidStatus, c.ClassCode,
            c.Stage};
            int totalRecords = CommentList.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = CommentList
            };
            Session["BookId"] = id1;
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string AddComment([Bind(Exclude="CommentId")] Comment comment)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    using (ErrorTrackerEntities1 db = new ErrorTrackerEntities1())
                    {
                        comment.BookId = Int32.Parse(Session["BookId"].ToString());
                        comment.Stage = Session["Stage"].ToString();
                        db.Comments.Add(comment);
                        db.SaveChanges();
                        msg = "Saved Successfully";
                    }
                }
                else
                    msg = "Data invalid";
            }
            catch(Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }

        [HttpPost]
        public string EditComment(Comment comment)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    using (ErrorTrackerEntities1 db = new ErrorTrackerEntities1())
                    {
                        db.Entry(comment).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        msg = "Saved successfully";
                    }
                }
                else
                    msg = "Data invalid";
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }

        public string ExcelExport()
        {
            string[] keys = Request.Form.AllKeys;
            MessageBox.Show(keys.Length.ToString());
            for (int i = 0; i < keys.Length; i++)
            {
                MessageBox.Show(keys[i].ToString());
            }
            return "dave";
        }

        public ActionResult MyBooks()
        {
            if (Session["id"] != null)
            {
                if (Session["id"] != null)
                {
                    using (ErrorTrackerEntities1 db = new ErrorTrackerEntities1())
                    {
                        List<SelectListItem> books = (db.Books.SqlQuery("select * from Book where UserId=" + Session["id"])).Select(x => new SelectListItem
                        {
                            Value = x.BookId.ToString(),
                            Text = x.Title
                        }).ToList();
                        books.Insert(0, new SelectListItem { Text = "Select book", Value = "0" });
                        ViewBag.Books = books;
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult CreateBook()
        {
            if (Session["id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult CreateBook(Book book)
        {
            if(ModelState.IsValid)
            {
                book.UserId = Convert.ToInt32(Session["id"]);
                using (ErrorTrackerEntities1 db = new ErrorTrackerEntities1())
                {
                    db.Books.Add(book);
                    db.SaveChanges();
                    return View();
                }
            }
            else
                return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Login(UserProfile user)
        {
            if (ModelState.IsValid)
            {
                using (ErrorTrackerEntities1 db = new ErrorTrackerEntities1())
                {
                    var obj = db.UserProfiles.Where(a => a.UserName.Equals(user.UserName) && a.Password.Equals(user.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["id"] = obj.UserId.ToString();
                        Session["UserName"] = obj.UserName.ToString();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Incorrect username/password combination");
                        return View(user);
                    }
                }

            }
            return View();
        }
    }
}