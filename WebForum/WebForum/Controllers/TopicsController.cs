using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebForum.Data;
using WebForum.Models;
using WebForum.ViewModels;

namespace WebForum.Controllers
{
    public class TopicsController : Controller
    {
        private readonly WebForumContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TopicsController(WebForumContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Topics
        public async Task<IActionResult> Index(string searchString, int? pageNumber)
        {
            ViewData["SearchString"] = searchString;

            IQueryable<Topic> topicsToShow = _context.Topics
                .Include(t => t.Posts)
                .ThenInclude(p => p.Author);

            if (!string.IsNullOrEmpty(searchString))
            {
                topicsToShow = topicsToShow.Where(t =>
                    t.Title.Contains(searchString) ||
                    t.Posts.First().Author.Login.Contains(searchString)
                );
            }

            topicsToShow = topicsToShow.OrderByDescending(i => i.Posts.OrderBy(p => p.DateTime).Last().DateTime).AsNoTracking();

            int pageSize = 10;
            return View(await PaginatedList<Topic>.CreateAsync(topicsToShow, pageNumber ?? 1, pageSize));
        }

        // GET: Topics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _context.Topics
                .Include(t => t.Posts)
                .ThenInclude(p => p.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // GET: Topics/Create
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

        // POST: Topics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content")] NewTopicViewModel newTopicViewModel)
        {
            string? userId = null;
            if (User?.Identity?.IsAuthenticated == true)
            {
                userId = _userManager.GetUserId(User);
            }

            if (ModelState.IsValid && !string.IsNullOrEmpty(userId))
            {
                Topic topic = new()
                {
                    Title = newTopicViewModel.Title
                };
                _context.Add(topic);
                await _context.SaveChangesAsync();

                Post firstPost = new()
                {
                    AuthorId = userId,
                    Content = newTopicViewModel.Content,
                    DateTime = DateTime.Now,
                    TopicId = topic.Id
                };
                _context.Add(firstPost);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { id = topic.Id });
            }
            return View(newTopicViewModel);
        }
    }
}
