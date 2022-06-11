using Application.Commands.UserCommands;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domen.Entities;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Implementation.Commands.UserCommands
{
    public class EFUpdateUserCommand : IUpdateUserCommand
    {
        private readonly Context _context;
        private readonly UpdateUserValidator _validator;
        private readonly IMapper _mapper;

        public EFUpdateUserCommand(Context context, UpdateUserValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }
        public int Id => 18;

        public string Name => "Update details about user with Entity Framework";

        public void Execute(UserDto request)
        {
            var user = _context.Users.Find(request.Id);
            var oldPassword = user.Password;

            if (user == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(User));
            }

            _validator.ValidateAndThrow(request);

            _mapper.Map(request, user);

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(request.Password));
                byte[] result = md5.Hash;
                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    strBuilder.Append(result[i].ToString("x2"));
                }

                user.Password = strBuilder.ToString();
            }
            else
            {
                user.Password = oldPassword;
            }


            _context.SaveChanges();
        }
    }
}
