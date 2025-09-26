using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Core.Domain.Entities;

namespace Store.Infrastructure.Persistence.InMemory
{
    internal sealed class InMemoryCollections
    {
        public List<Product> Products { get; } = [];

        public List<ShoppingCart> ShoppingCarts { get; } = [];
    }
}
