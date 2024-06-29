using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Eyeglasses_StudentName.Pages
{
    public class UpdateEyeglassModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]
        public EyeglassInputModel Input { get; set; }

        // SelectList for populating the LensType dropdown
        public SelectList LensTypeOptions { get; set; }

        public UpdateEyeglassModel(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public IActionResult OnGet(int? id)
        {
            if (id == null) return NotFound();

            var eyeglass = _unitOfWork.EyeglassRepository.GetByID(id.Value);
            if (eyeglass == null) return NotFound();

            Input = new EyeglassInputModel
            {
                EyeglassesId = eyeglass.EyeglassesId,
                EyeglassesName = eyeglass.EyeglassesName,
                EyeglassesDescription = eyeglass.EyeglassesDescription,
                FrameColor = eyeglass.FrameColor,
                Price = eyeglass.Price,
                Quantity = eyeglass.Quantity,
                LensTypeId = eyeglass.LensTypeId
            };

            LensTypeOptions = new SelectList(_unitOfWork.LensTypeRepository.Get(), "LensTypeId", "LensTypeName", eyeglass.LensTypeId);
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (!ModelState.IsValid) return Page();
            if (id == null) return NotFound();

            var eyeglassToUpdate = _unitOfWork.EyeglassRepository.GetByID(id.Value);
            if (eyeglassToUpdate == null) return NotFound();

            // Mapping updated values to the entity
            eyeglassToUpdate.EyeglassesName = Input.EyeglassesName;
            eyeglassToUpdate.EyeglassesDescription = Input.EyeglassesDescription;
            eyeglassToUpdate.FrameColor = Input.FrameColor;
            eyeglassToUpdate.Price = Input.Price ?? default;
            eyeglassToUpdate.Quantity = Input.Quantity ?? default;
            eyeglassToUpdate.LensTypeId = Input.LensTypeId;

            _unitOfWork.Save();
            return RedirectToPage("./EyeglassesList");
        }

        public class EyeglassInputModel
        {
            public int EyeglassesId { get; set; }

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
}
