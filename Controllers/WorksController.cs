using Microsoft.AspNetCore.Mvc;
using AgileRap_Process.Models;
using AgileRap_Process.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace AgileRap_Process.Controllers
{
    [Authorize]
    public class WorksController : Controller
    {
        private static List<SelectListItem> _UserList = new List<SelectListItem>();
        private static List<SelectListItem> _StatusList = new List<SelectListItem>();
        private AgileRap_ProcessContext db = new AgileRap_ProcessContext();
        public ActionResult Index()
        {
            var work = db.Work.Where(m => m.IsDelete == false).Include(x => x.Status).
                Include(l => l.Provider).ThenInclude(x => x.User).ToList();
            ViewBag.User = db.User.ToList();
            
            return View(work);
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Users");
        }

        public ActionResult History(int? id)
        {
            ViewBag.HistoryID = id;
            var work = db.Work.Include(s => s.WorkLog).ThenInclude(s => s.ProviderLog).ThenInclude(s => s.User)
                .Include(s => s.WorkLog).ThenInclude(s => s.Status).FirstOrDefault(s => s.ID == id);
            var workDBList = db.Work.Where(m => m.IsDelete == false).Include(x => x.Status).
                Include(l => l.Provider).ThenInclude(x => x.User).ToList();
			DropDown();
			if (work.WorkLog.Count < 2)
            {
                ViewBag.HistoryText = "Project " + work.Project + " has no changed";
                return View(workDBList);
            }

            for (int i = 1; i < work.WorkLog.Count; i++)
            {
                WorkLog workLogNext = GetWorkLogNextByIndex(i, work);
                work.WorkLog.ToList()[i - 1].WorkLogChangeNext = workLogNext;
            }

            var reverseLogList = work.WorkLog.Reverse().ToList();
            work.WorkLog = reverseLogList;
            workDBList[workDBList.FindIndex(s => s.ID == id)] = work;
            
            return View(workDBList);
        }
        public ActionResult Create()
        {
			var standInUser = HttpContext.Session.GetString("UserSession").ToString();
			var users = db.User.Where(x => x.Email.Contains(standInUser)).FirstOrDefault();
			List<Work> worklist = db.Work.Include(x => x.Status).Include(l => l.Provider).ToList();
            Work work = new Work();
            work.CreateDate = DateTime.Now;
            work.CreateBy = users.ID;
            worklist.Add(work);

			DropDown();


            return View(worklist);
        }

        [HttpPost]
        public ActionResult Create(Work work)
        {
			var standInUser = HttpContext.Session.GetString("UserSession").ToString();
			var users = db.User.Where(x => x.Email.Contains(standInUser)).FirstOrDefault();
            work.CreateBy = users.ID;
            work.UpdateBy = users.ID;
			work.CreateDate = DateTime.Now;
            work.UpdateDate = DateTime.Now;
            work.IsDelete = false;
            db.Work.Add(work);
            var user = db.User.ToList();
            work.Provider = new List<Provider>();
            if (work.IsSelectAll == true)
            {
                foreach (var f in user)
                {
                    Provider provider = new Provider();
                    if (provider.UserID == 0)
                    {
                        provider.WorkID = work.ID;
                        provider.UserID = f.ID;
                        provider.IsDelete = false;
                    }
                    db.Provider.Add(provider);
                    work.Provider.Add(provider);
                }
            }
            else
            {
                if (work.ProviderValue == null)
                {
                    return Create();
                }
                int[] listprovider = Array.ConvertAll(work.ProviderValue.Split(','), int.Parse);
                foreach (var i in listprovider)
                {
                    Provider provider = new Provider();
                    if (provider.UserID == 0)
                    {
                        provider.WorkID = work.ID;
                        provider.UserID = i;
                        provider.IsDelete = false;
                    }
                    db.Provider.Add(provider);
                    work.Provider.Add(provider);
                }
            }
            WorkLog workLog = new WorkLog()
            {
                No = 1,
                Project = work.Project,
                Name = work.Name,
                DueDate = work.DueDate,
                StatusID = work.StatusID,
                Remark = work.Remark,
                CreateBy = work.CreateBy,
                UpdateBy = work.UpdateBy,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                IsDelete = false
            };
            workLog.ProviderLog = new List<ProviderLog>();
            work.WorkLog = new List<WorkLog>()
            {
                workLog
            };
            foreach (var i in work.Provider)
            {
                ProviderLog providerLog = new ProviderLog()
                {
                    UserID = i.UserID,
                    CreateBy = i.CreateBy,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsDelete = false
                };
                workLog.ProviderLog.Add(providerLog);
                db.ProviderLog.Add(providerLog);
            }
            db.WorkLog.Add(workLog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            List<Work> worklist = db.Work.Include(x => x.Status).Include(l => l.Provider).ThenInclude(x => x.User).ToList();
            var workEditItem = worklist.Where(s => s.ID == id).First();
            ViewBag.Selected = workEditItem;
            ViewBag.EditID = id;
            DropDown();


            return View(worklist);
        }

        [HttpPost]
        public ActionResult Edit(Work work)
        {
			var standInUser = HttpContext.Session.GetString("UserSession").ToString();
			var users = db.User.Where(x => x.Email.Contains(standInUser)).FirstOrDefault();
			bool hasModifyProvider = false;
            if (work.ProviderValue != null || work.IsSelectAll)
            {
                hasModifyProvider = true;
            }
            if (hasModifyProvider)
            {
                if (work.IsSelectAll)
                {
                    work.ProviderValue = "";
                    var userDBList = db.User.ToList();
                    foreach (var j in userDBList)
                    {
                        if (j == userDBList.Last())
                        {
                            work.ProviderValue += j.ID;
                        }
                        else
                        {
                            work.ProviderValue += j.ID + ",";
                        }
                    }
                }
                List<string> providerIDList = work.ProviderValue.Split(',').ToList();
                foreach (var j in work.Provider)
                {
                    bool isExist = false;
                    foreach (var i in providerIDList)
                    {
                        if (j.UserID == Convert.ToInt32(i))
                        {

                            isExist = true;
                            if (j.IsDelete == true)
                            {
                                db.Entry(j).State = EntityState.Modified;
                                j.UpdateDate = DateTime.Now;
                                j.IsDelete = false;
                            }
                            providerIDList.Remove(i);
                            break;

                        }
                    }

                    if (isExist == false)
                    {
                        db.Entry(j).State = EntityState.Modified;
                        j.IsDelete = true;
                        j.UpdateDate = DateTime.Now;
                    }
                }

                if (providerIDList.Count > 0)
                {
                    foreach (var i in providerIDList)
                    {
                        var j = Convert.ToInt32(i);
                        Provider provider = new Provider()
                        {
                            CreateDate = DateTime.Now,
                            UpdateDate = DateTime.Now,
                            IsDelete = false,
                            UserID = j
                        };
                        db.Provider.Add(provider);
                        work.Provider.Add(provider);
                    }
                }
            }
            else
            {
                Work oldWork = db.Work.Find(work.ID);
                if (work.IsEqual(oldWork, true))
                {
                    return RedirectToAction("Index");
                }
                db.ChangeTracker.Clear();
            }
			work.UpdateBy = users.ID;
			db.Entry(work).State = EntityState.Modified;
            work.UpdateDate = DateTime.Now;


            var workLogDBList = db.WorkLog.Where(s => s.WorkID == work.ID).Include(s => s.ProviderLog).ToList();
            WorkLog workLog = new WorkLog()
            {
                WorkID = work.ID,
                Project = work.Project,
                Name = work.Name,
                No = workLogDBList.Last().No + 1,
                DueDate = work.DueDate,
                StatusID = work.StatusID,
                Remark = work.Remark,
                CreateBy = work.CreateBy,
                UpdateBy = work.UpdateBy,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                IsDelete = work.IsDelete
            };
            workLog.ProviderLog = new List<ProviderLog>();
            if (work.Provider != null)
            {
                foreach (var i in work.Provider)
                {
                    ProviderLog providerLog = new ProviderLog()
                    {
                        UserID = i.UserID,
                        CreateBy = i.CreateBy,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        IsDelete = i.IsDelete
                    };
                    workLog.ProviderLog.Add(providerLog);
                    db.ProviderLog.Add(providerLog);
                }
            }
            else
            {
                foreach (var i in workLog.ProviderLog)
                {
                    ProviderLog providerLog = new ProviderLog()
                    {
                        UserID = i.UserID,
                        CreateBy = i.CreateBy,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        IsDelete = false
                    };
                    workLog.ProviderLog.Add(providerLog);
                    db.ProviderLog.Add(providerLog);
                }
            }
            db.WorkLog.Add(workLog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        private WorkLog GetWorkLogNextByIndex(int index, Work workForLog)
        {
            WorkLog tempWorkLogNext = new WorkLog()
            {
                Project = workForLog.WorkLog.ToList()[index].Project,
                Name = workForLog.WorkLog.ToList()[index].Name,
                DueDate = workForLog.WorkLog.ToList()[index].DueDate,
                UpdateBy = workForLog.WorkLog.ToList()[index].UpdateBy,
                Status = workForLog.WorkLog.ToList()[index].Status,
                StatusID = workForLog.WorkLog.ToList()[index].StatusID,
                Remark = workForLog.WorkLog.ToList()[index].Remark,
                ProviderLog = workForLog.WorkLog.ToList()[index].ProviderLog
            };
            return tempWorkLogNext;
        }
        private void DropDown()
        {
            //List<User> selectuser = db.User.ToList();
            //if (_UserList.Count != selectuser.Count)
            //{
            //    _UserList.Clear();
            //    foreach (var i in selectuser)
            //    {
            //        SelectListItem selectListItem = new SelectListItem() { Text = i.Name, Value = i.ID.ToString() };
            //        _UserList.Add(selectListItem);
            //    }
            //}
            ViewBag.Users = db.User.ToList();

            List<Status> selectstatus = db.Status.ToList();
            if (_StatusList.Count != selectstatus.Count)
            {
                _StatusList.Clear();
                foreach (var i in selectstatus)
                {
                    SelectListItem selectListItem = new SelectListItem() { Text = i.StatusName, Value = i.ID.ToString() };
                    _StatusList.Add(selectListItem);
                }
            }
            ViewBag.Status = _StatusList;
        }
    }
}
