using BulletinBoard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class CategoriesModel : PageModel
{
    private readonly HttpClient _httpClient;

    public CategoriesModel(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public List<CategoryDto> Categories { get; set; } = new();
    public List<AnnouncementDto> Announcements { get; set; } = new();

    [BindProperty]
    public AnnouncementToCreateDto NewAnnouncement { get; set; } = new();

    [BindProperty]
    public AnnouncementToUpdateDto EditableAnnouncement { get; set; } = new();

    public async Task OnGetAsync()
    {
        Categories = await _httpClient.GetFromJsonAsync<List<CategoryDto>>("api/categories");
        Announcements = await _httpClient.GetFromJsonAsync<List<AnnouncementDto>>("Announcements/SelectAllAnnouncements");
    }

    public IEnumerable<AnnouncementDto> GetAnnouncementsForSubcategory(Guid subcategoryId)
    {
        return Announcements.Where(a => a.SubcategoryId == subcategoryId);
    }

    public async Task<IActionResult> OnPostDeleteAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"Announcements/DeleteAnnouncementById?id={id}");
        if (response.IsSuccessStatusCode)
        {
            await OnGetAsync();
            return RedirectToPage();
        }

        ModelState.AddModelError(string.Empty, "Не вдалося видалити оголошення.");
        await OnGetAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostCreateAsync()
    {
        if (NewAnnouncement.SubcategoryId == Guid.Empty)
        {
            ModelState.AddModelError("NewAnnouncement.SubcategoryId", "Підкатегорія обов’язкова.");
            await OnGetAsync();
            return Page();
        }

        NewAnnouncement.CreatedAt = DateTime.UtcNow;

        var response = await _httpClient.PostAsJsonAsync("Announcements/CreateAnnouncement", NewAnnouncement);

        if (response.IsSuccessStatusCode)
            return RedirectToPage();

        ModelState.AddModelError(string.Empty, "Не вдалося створити оголошення.");
        await OnGetAsync();
        return Page();
    }
    public async Task<IActionResult> OnPostEditAsync()
    {
        var response = await _httpClient.PutAsJsonAsync("Announcements/UpdateAnnouncement", EditableAnnouncement);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage();
        }

        ModelState.AddModelError(string.Empty, "Не вдалося оновити оголошення.");
        await OnGetAsync();
        return Page();
    }
}
