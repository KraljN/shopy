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
    public class EFDeleteCategoryCommand : IDeleteCategoryCommand
    {
        private Context _context;

        public EFDeleteCategoryCommand(Context context)
        {
            _context = context;
        }
        public int Id => 9;

        public string Name => "Delete category with Entity Framework";

        public void Execute(int request)
        {
            var category = _context.Categories.Find(request);

            if (category == null)
            {
                throw new EntityNotFoundException(request, typeof(Category));
            }

            category.IsActive = false;
            category.IsDeleted = true;
            category.DeletedAt = DateTime.UtcNow;

            _context.SaveChanges();
        }
    }
}
