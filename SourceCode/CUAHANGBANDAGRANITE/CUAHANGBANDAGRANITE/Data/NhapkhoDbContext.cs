using CUAHANGBANDAGRANITE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUAHANGBANDAGRANITE.Data
{
	public class NhapkhoDbContext : DbContext
	{
		public NhapkhoDbContext(DbContextOptions<NhapkhoDbContext> options) : base(options)
		{
		}
	}
}
