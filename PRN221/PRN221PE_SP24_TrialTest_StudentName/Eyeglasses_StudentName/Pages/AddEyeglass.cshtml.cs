using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using Repository.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

public class AddEyeglassModel : PageModel
{
    private readonly UnitOfWork _unitOfWork;
    public SelectList LensTypeOptions { get; set; }

    [BindProperty]
    public EyeglassInputModel NewEyeglass { get; set; }

    public AddEyeglassModel(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public void OnGet()
    {
        LensTypeOptions = new SelectList(_unitOfWork.LensTypeRepository.Get(), "LensTypeId", "LensTypeName");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        int maxId = _unitOfWork.EyeglassRepository.Get().Max(e => (int?)e.EyeglassesId) ?? 0;
        var newEyeglass = new Eyeglass
        {
            EyeglassesId = maxId + 1, // Manually set the ID to ensure uniqueness
            EyeglassesName = NewEyeglass.EyeglassesName,
            EyeglassesDescription = NewEyeglass.EyeglassesDescription,
            FrameColor = NewEyeglass.FrameColor,
            Price = NewEyeglass.Price ?? 0,
            Quantity = NewEyeglass.Quantity ?? 0,
            CreatedDate = DateTime.UtcNow,
            LensTypeId = NewEyeglass.LensTypeId
        };

        _unitOfWork.EyeglassRepository.Insert(newEyeglass);
        _unitOfWork.Save();
        return RedirectToPage("./EyeglassesList");
    }

    public class EyeglassInputModel
    {
        [Required, MinLength(11, ErrorMessage = "Eyeglasses name must be greater than 10 characters.")]
        [RegularExpression(@"^([A-Z0-9@#$&][a-zA-Z0-9@#$&]*\s?)*$", ErrorMessage = "Each word of the EyeglassesName must begin with a capital letter and can include numbers (0-9) or special characters (@, #, $, &, (, )).")]
        public string EyeglassesName { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string EyeglassesDescription { get; set; }

        [Required(ErrorMessage = "Frame Color is required.")]
        public string FrameColor { get; set; }

        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal? Price { get; set; }

        [Required, Range(0, 999, ErrorMessage = "Quantity must be between 0 and 999.")]
        public int? Quantity { get; set; }

        [Required(ErrorMessage = "Lens Type is required.")]
        public string LensTypeId { get; set; }
    }
}
