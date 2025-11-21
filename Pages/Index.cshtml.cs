using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Pages
{

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IWebHostEnvironment WebHostEnvironment { get; set; }
        public string webRootPath;
        public string[] files;// = Directory.GetFiles(webRootPath);
        public string[] directories;// = Directory.GetDirectories(webRootPath);
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;            
            WebHostEnvironment = GlobalData.Instance.environment;
        }

        public void OnGet()
        {
            webRootPath = WebHostEnvironment.WebRootPath;
            try
            {
                files = Directory.GetFiles(webRootPath);
                directories = Directory.GetDirectories(webRootPath);
            }
            catch(Exception ex) 
            {
                files = new string[0];
                directories = new string[0];
                Debug.WriteLine(ex);
            }
        }
        public async Task<IActionResult> OnPostTextClickAsync([FromBody] string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                    return Content("Error: Text is empty");
                // Обработка полученного текста
                //await Task.Delay(100); // Имитация обработки
                //Debug.WriteLine(text);
                // Возвращаем ответ
                string res = await ReadContent(text);// Task.Delay(1); // Имитация обработки
                return Content(res);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return Content($"Error loading {text}");
            }
        }
        public async Task<string> ReadContent(string content)
        {
            Debug.WriteLine(content);
            string res = $"Ты нажал на: {content}";

            string testContent = content.Trim();

            try
            {
                bool t1 = Directory.Exists(testContent);
                if (!t1)
                {
                    try
                    {
                        t1 = System.IO.File.Exists(testContent);
                        if (t1)
                        {
                            System.IO.FileInfo fileInfo = new System.IO.FileInfo(testContent);
                            string _ext = fileInfo.Extension;                            
                            if (_ext.ToLower()==".txt")
                            {
                                StreamReader reader = fileInfo.OpenText();
                                res = reader.ReadToEnd();
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {

            }

            await Task.Delay(1);
            return res;
        }
    }
}
