using Microsoft.EntityFrameworkCore;
using smartcityapi.Data;

namespace smartcityapi.Context
{
	public class SmartCityDBContext:DbContext
	{
		private readonly IConfiguration _configuration;

		public SmartCityDBContext(IConfiguration configuration)
		{
			_configuration = configuration;

		}

		public DbSet<master_department> master_department { get; set; }
		public DbSet<master_level> master_level { get; set; }
		public DbSet<master_location> master_location { get; set; }
		public DbSet<master_module> master_module { get; set; }
		public DbSet<master_module_pages> master_module_pages { get; set; }
		public DbSet<master_role> master_role { get; set; }

		public DbSet<master_user> master_user { get; set; }
		public DbSet<role_permissions> role_permissions { get; set; }
		public DbSet<department_permissions> department_permissions { get; set; }

		public DbSet<Asset_Type_Master> Asset_Type_Master { get; set; }

		public DbSet<Master_Device> Master_Device { get; set; }

		public DbSet<tbl_eventdata_log> tbl_eventdata_log { get; set; }
		public DbSet<tbl_last_eventdata> tbl_last_eventdata { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var data_string = _configuration.GetConnectionString("MySqlConn");

			object value = optionsBuilder.UseSqlServer(data_string);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			//builder.Entity<customer_device_meta_data>().ToTable(tb => tb.HasTrigger("customer_device_meta_data_AFTER_UPDATE"));

			//builder.Entity<customer_users>().ToTable(tb => tb.HasTrigger("trg_bU_customer_user"));
			//builder.Entity<customer_users>().ToTable(tb => tb.HasTrigger("trg_bI_customer_user"));
			builder.Entity<role_permissions>().HasKey(c => new { c.fk_module_id, c.fk_page_id , c.fk_role_id});

			builder.Entity<Master_Device>().ToTable(tb => tb.HasTrigger("trigger_customer_device_master"));

			//builder.Entity<role_permissions>().HasNoKey();


		}


	}
}
