using Application.Commands.ProductCommands;
using Application.Exceptions;
using DataAccess;
using Domen.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.ProductCommands
{
    public class EFDeleteProductCommand : IDeleteProductCommand
    {
        private readonly Context _context;

        public EFDeleteProductCommand(Context context)
        {
            _context = context;
        }
        public int Id => 12;

        public string Name => "Delete product with Entity Framework";

        public void Execute(int request)
        {
            var product = _context.Products.Find(request);

            if (product == null)
            {
                throw new EntityNotFoundException(request, typeof(Product));
            }

            product.IsActive = false;
            product.IsDeleted = true;
            product.DeletedAt = DateTime.UtcNow;

            _context.SaveChanges();
        }
    }
}
