using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using test2.Data;
using test2.Models;
using Microsoft.AspNetCore.Http;


namespace test2.Controllers
{
    public class UsersController : Controller
    {
        private readonly test2Context _context;

        public UsersController(test2Context context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Email,Password,Phone")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Email,Password,Phone")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }


        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.User
                .FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("Username", user.Username);
                return RedirectToAction("Index", "Home"); // أو أي صفحة رئيسية
            }

            ViewBag.Error = "اسم المستخدم أو كلمة المرور غير صحيحة.";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");  
        }


        // GET: Users/Register
        public IActionResult Register()
        {
            ViewBag.Role = "User";
            return View();
        }

        // POST: Users/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Username,Email,Password,Phone")] User user)
        {
            if (ModelState.IsValid)
            {
                if (_context.User.Any(u => u.Phone == user.Phone || u.Email == user.Email))
                {
                    ViewBag.Error = "Phone or email already exists.";
                    ViewBag.Role = "User";
                    return View(user);
                }

             //ظ   user.Role = "User";
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }

            ViewBag.Role = "User";
            return View(user);
        }

        // GET: Users/RegisterAdmin
        public IActionResult RegisterAdmin()
        {
            ViewBag.Role = "Admin";
            return View("Register");
        }

        // POST: Users/RegisterAdmin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAdmin([Bind("Username,Email,Password,Phone")] User user)
        {
            if (ModelState.IsValid)
            {
                if (_context.User.Any(u => u.Phone == user.Phone || u.Email == user.Email))
                {
                    ViewBag.Error = "Phone or email already exists.";
                    ViewBag.Role = "Admin";
                    return View("Register", user);
                }

                //user.Role = "Admin";
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }

            ViewBag.Role = "Admin";
            return View("Register", user);
        }

    }
}
