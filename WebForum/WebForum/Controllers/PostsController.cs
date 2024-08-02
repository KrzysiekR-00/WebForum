using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebForum.Data;
using WebForum.Models;
using WebForum.ViewModels;

namespace WebForum.Controllers
{
    public class PostsController : Controller
    {
        private readonly WebForumContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostsController(WebForumContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                return View();
            }
            else
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Content,TopicId")] NewPostViewModel newPostViewModel)
        {
            string? userId = null;
            if (User?.Identity?.IsAuthenticated == true)
            {
                userId = _userManager.GetUserId(User);
            }

            if (ModelState.IsValid && !string.IsNullOrEmpty(userId))
            {
                var post = new Post()
                {
                    AuthorId = userId,
                    Content = newPostViewModel.Content,
                    DateTime = DateTime.Now,
                    TopicId = newPostViewModel.TopicId
                };
                _context.Add(post);
                await _context.SaveChangesAsync();

                return RedirectToAction(
                    nameof(TopicsController.Details),
                    "Topics",
                    new { id = newPostViewModel.TopicId }
                    );
            }
            return View(newPostViewModel);
        }
    }
}
