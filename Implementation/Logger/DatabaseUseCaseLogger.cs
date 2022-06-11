using Application;
using Application.Logger;
using DataAccess;
using Domen.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Logger
{
    public class DatabaseUseCaseLogger : IUseCaseLogger
    {
        private readonly Context _context;

        public DatabaseUseCaseLogger(Context context)
        {
            _context = context;
        }

        public void Log(IUseCase useCase, IApplicationActor actor, object useCaseData)
        {
            _context.UseCaseLogs.Add(new UseCaseLog
            {
                CreatedAt = DateTime.UtcNow,
                UseCaseName = useCase.Name,
                Data = JsonConvert.SerializeObject(useCaseData),
                Actor = actor.Identity
            });

            _context.SaveChanges();
        }
    }
}
