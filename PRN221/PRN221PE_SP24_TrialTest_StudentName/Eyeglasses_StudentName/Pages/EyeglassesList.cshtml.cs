using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using Repository.Models;
using System.Collections.Generic;
using System.Linq;

public class EyeglassesListModel : PageModel
{
    private readonly UnitOfWork _unitOfWork;

    public List<Eyeglass> Eyeglasses { get; set; }

    public int CurrentPage { get; set; } = 1;

    public int TotalPages { get; set; }

    // Flag to indicate if the current user has edit capabilities (Admin role)
    public bool CanEdit { get; set; }

    [BindProperty(SupportsGet = true)]
    public string SearchTerm { get; set; }

    public EyeglassesListModel(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void OnGet(int currentPage = 1)
    {
        var userRole = HttpContext.Session.GetString("Role");
        bool isAuthorized = userRole == "1" || userRole == "2"; // Admin = 1, Staff = 2
        CanEdit = userRole == "1"; // Enable edit functionality for admins

        if (!isAuthorized)
        {
            Eyeglasses = new List<Eyeglass>();
            return;
        }

        // Start with a query that includes LensType and is pre-sorted by CreatedDate
        var query = _unitOfWork.EyeglassRepository
                   .Get(includeProperties: "LensType",
                        orderBy: q => q.OrderByDescending(e => e.CreatedDate))
                   .AsQueryable();

        // Apply search filtering if a search term is provided and the user is an admin
        if (CanEdit && !string.IsNullOrEmpty(SearchTerm))
        {
            query = query.Where(e => e.EyeglassesName.Contains(SearchTerm) || e.EyeglassesDescription.Contains(SearchTerm));
        }

        // Pagination logic
        int pageSize = 4; // Define the number of items per page
        TotalPages = (int)Math.Ceiling(query.Count() / (double)pageSize);
        CurrentPage = currentPage;
        Eyeglasses = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
    }

    public IActionResult OnPostDelete(int id)
    {
        var eyeglassToDelete = _unitOfWork.EyeglassRepository.GetByID(id);
        if (eyeglassToDelete != null)
        {
            _unitOfWork.EyeglassRepository.Delete(eyeglassToDelete);
            _unitOfWork.Save();
        }

        return RedirectToPage();
    }

}
