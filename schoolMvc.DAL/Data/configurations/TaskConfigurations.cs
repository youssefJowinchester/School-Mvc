using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using schoolMvc.DAL.Enums;
using schoolMvc.DAL.Models;

namespace schoolMvc.DAL.Data.configurations
{
    public class TaskConfigurations : IEntityTypeConfiguration<SchoolTask>
    {
        public void Configure(EntityTypeBuilder<SchoolTask> builder)
        {
            builder.Property(O => O.status)
                               .HasConversion(OStatus => OStatus.ToString(), OStatus => (ChosesStatus)Enum.Parse(typeof(ChosesStatus), OStatus));
        }
    }
}
