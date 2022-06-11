using Application.Commands.CategoryCommands;
using Application.DataTransfer;
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
    public class EFCreateCategoryCommand : ICreateCategoryCommand
    {
        private Context _context;
        private readonly IMapper _mapper;
        private CreateCategoryValidator _validator;

        public EFCreateCategoryCommand(Context context, IMapper mapper, CreateCategoryValidator validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public int Id => 7;

        public string Name => "Create new Category with Entity Framework";

        public void Execute(CategoryDto request)
        {
            _validator.ValidateAndThrow(request);
            _context.Categories.Add(_mapper.Map<Category>(request));
            _context.SaveChanges();
        }
    }
}
