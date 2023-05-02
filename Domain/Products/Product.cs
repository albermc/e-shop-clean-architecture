using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Products;

public class Product
{
	public Guid Id { get; private set; }
	public string Name { get; private set; } = string.Empty;

	public Money Price { get; private set; }
	public Sku Sku { get; private set; }
}
