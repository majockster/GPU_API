using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace GPU_api.Models
{
    public class GPUContext : DbContext
    {
        public GPUContext(DbContextOptions<GPUContext> options) : base(options) {}

        public DbSet<GPU_model> GPU_Models { get; set; } = null!;
    }
}