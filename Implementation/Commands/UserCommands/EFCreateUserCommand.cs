using Application.Commands.UserCommands;
using Application.DataTransfer;
using Application.Email;
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
    public class EFCreateUserCommand : ICreateUserCommand
    {
        private readonly Context _context;
        private readonly CreateUserValidator _validator;
        private readonly IMapper _mapper;
        private readonly IEmailSender _sender;

        public EFCreateUserCommand(Context context, CreateUserValidator validator, IMapper mapper, IEmailSender sender)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
            _sender = sender;
        }

        public int Id => 1;

        public string Name => "Create new User";

        public void Execute(UserDto request)
        {
            _validator.ValidateAndThrow(request);

            var user = _mapper.Map<User>(request);

            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(request.Password));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            user.Password = strBuilder.ToString();

            var userUseCases = new List<int>
            {
                1,//Create user
                15,//Create order
                16,//Change order status
                8,//Categories get
                9,//Category get
                13,//Products get
                14,//Product get
            };

            userUseCases.ForEach(useCase => _context.UserUseCases.Add(new UserUseCase
            {
                User = user,
                UseCaseId = useCase
            }));

            _context.Users.Add(user);
            _context.SaveChanges();

            _sender.Send(new SendEmail
            {
                Content = $"<h1>Hi {user.FirstName} {user.LastName}. Welcome to Shopy!</h1><h2>You have successfully registred.</h2><p>Take a look at our great offer</p>",
                SendTo = request.Email,
                Subject = "Shopy Registration"
            });
        }
    }
}
