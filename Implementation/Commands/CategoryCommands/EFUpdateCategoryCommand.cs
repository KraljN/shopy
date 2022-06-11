using Application.Commands.CategoryCommands;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domen.Entities;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.CategoryCommands
{
    public class EFUpdateCategoryCommand : IUpdateCategoryCommand
    {
        private Context _context;
        private readonly IMapper _mapper;
        private UpdateCategoryValidator _validator;

        public EFUpdateCategoryCommand(Context context, IMapper mapper, UpdateCategoryValidator validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }
        public int Id => 8;

        public string Name => "Update category with Entity Framework";

        public void Execute(CategoryDto request)
        {
            var category = _context.Categories.Find(request.Id);

            if (category == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Category));
            }

            _validator.ValidateAndThrow(request);

            _mapper.Map(request, category);
            _context.SaveChanges();
        }
    }
}
