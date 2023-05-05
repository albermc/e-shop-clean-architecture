using Application.Data;
using Domain.Customers;
using Domain.Orders;
using Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
	public class ApplicationDbContext : DbContext, IApplicationDbContext
	{
		private readonly IPublisher _publisher;

		public ApplicationDbContext(DbContextOptions options, IPublisher publisher)
			: base(options)
		{
			_publisher = publisher;
		}

		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var domainEvents = ChangeTracker.Entries<Entity>()
				.Select(e => e.Entity)
				.Where(e => e.GetDomainEvents().Any())
				.SelectMany(e => e.GetDomainEvents());

			var result = await base.SaveChangesAsync(cancellationToken);

			foreach (var domainEvent in domainEvents)
			{
				await _publisher.Publish(domainEvent, cancellationToken);
			}

			return result;
		}
	}
}