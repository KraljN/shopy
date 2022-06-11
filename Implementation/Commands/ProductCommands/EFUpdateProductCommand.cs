using Application.Commands.ProductCommands;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domen.Entities;
using FluentValidation;
using Implementation.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Implementation.Commands.ProductCommands
{
    public class EFUpdateProductCommand : IUpdateProductCommand
    {
        private readonly Context _context;
        private readonly UpdateProductValidator _validator;
        private readonly IMapper _mapper;

        public EFUpdateProductCommand(Context context, UpdateProductValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }
        public int Id => 11;

        public string Name => "Update product with Entity Framework";

        public void Execute(ProductDto request)
        {
            var product = _context.Products.Include(p => p.Prices).FirstOrDefault(p => p.Id == request.Id);

            if (product == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Product));
            }

            _validator.ValidateAndThrow(request);

            _mapper.Map(request, product);

            if (request.Image != null)
            {
                var guid = Guid.NewGuid();

                var extension = Path.GetExtension(request.Image.FileName);

                var newFileName = guid + "_" + request.Image.FileName;

                var path = Path.Combine("wwwroot", "Images", newFileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    request.Image.CopyTo(fileStream);
                }

                product.Image = newFileName;
            }

            if (request.Price != product.Prices.OrderByDescending(p => p.CreatedAt).Select(p => p.PriceAmount).First())
            {
                _context.Prices.Add(new Price
                {
                    Product = product,
                    PriceAmount = request.Price
                });
            }
            _context.SaveChanges();
        }
    }
}
