using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostTextClickAsync([FromBody] string text)
        {
            if (string.IsNullOrEmpty(text))
                return Content("Error: Text is empty");
            // Обработка полученного текста
            //await Task.Delay(100); // Имитация обработки
            Debug.WriteLine(text);
            // Возвращаем ответ
            return Content($"You clicked on: {text}");
        }
    }
}
