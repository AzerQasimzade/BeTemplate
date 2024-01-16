using ImtahanTest1.DAL;
using Microsoft.EntityFrameworkCore;

namespace ImtahanTest1.Services
{
    public class _LayoutService
    {
        private readonly AppDbContext _context;

        public _LayoutService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Dictionary<string,string>> GetSettingsAsync()
        {
            Dictionary<string, string> settings = await _context.Settings.ToDictionaryAsync(t => t.Key, t => t.Value);
            return settings;
        }
    }
}
