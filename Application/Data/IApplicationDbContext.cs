using Domain.Customers;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
	public interface IApplicationDbContext
	{
		DbSet<Customer> Customers { get; set; }
		DbSet<Order> Orders { get; set; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
