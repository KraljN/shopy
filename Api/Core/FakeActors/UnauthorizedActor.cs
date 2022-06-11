using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.FakeActors
{
    public class UnauthorizedActor : IApplicationActor
    {
        public int Id => 17;
        public string Identity => "Unauthorized Actor";
        public IEnumerable<int> AllowedUseCases => Enumerable.Range(1, 5);
    }
}
