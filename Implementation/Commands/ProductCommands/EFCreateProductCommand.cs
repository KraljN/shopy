using Application.Commands.ProductCommands;
using Application.DataTransfer;
using AutoMapper;
using DataAccess;
using Domen.Entities;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Implementation.Commands.ProductCommands
{
    public class EFCreateProductCommand : ICreateProductCommand
    {

        private Context _context;
        private readonly IMapper _mapper;
        private CreateProductValidator _validator;

        public EFCreateProductCommand(Context context, CreateProductValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }
        public int Id => 10;

        public string Name => "Create product with Entity Framework";

        public void Execute(ProductDto request)
        {
            _validator.ValidateAndThrow(request);

            var product = _mapper.Map<Product>(request);

            var guid = Guid.NewGuid();

            var extension = Path.GetExtension(request.Image.FileName);

            var newFileName = guid + "_" + request.Image.FileName;

            var path = Path.Combine("wwwroot", "Images", newFileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                request.Image.CopyTo(fileStream);
            }

            product.Image = newFileName;

            _context.Prices.Add(new Price
            {
                Product = product,
                PriceAmount = request.Price
            });

            _context.Products.Add(product);
            _context.SaveChanges();
        }
    }
}
