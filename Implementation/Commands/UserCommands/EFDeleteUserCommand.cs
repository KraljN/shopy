using Application.Commands.UserCommands;
using Application.Exceptions;
using DataAccess;
using Domen.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.UserCommands
{
    public class EFDeleteUserCommand : IDeleteUserCommand
    {
        private readonly Context _context;

        public EFDeleteUserCommand(Context context)
        {
            _context = context;
        }
        public int Id => 19;

        public string Name => "Delete user with Entity Framework";

        public void Execute(int request)
        {
            var user = _context.Users.Find(request);

            if (user == null)
            {
                throw new EntityNotFoundException(request, typeof(User));
            }

            user.IsActive = false;
            user.IsDeleted = true;
            user.DeletedAt = DateTime.UtcNow;

            _context.SaveChanges();
        }
    }
}
