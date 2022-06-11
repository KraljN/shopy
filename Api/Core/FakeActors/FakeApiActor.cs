using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.FakeActors
{
    public class FakeApiActor : IApplicationActor
    {
        public int Id => 1;

        public string Identity => "Fake User";

        //public IEnumerable<int> AllowedUseCases => new List<int> { 1,2 };
        public IEnumerable<int> AllowedUseCases => Enumerable.Range(1, 1000);
    }

    public class AdminFakeApiActor : IApplicationActor
    {
        public int Id => 2;

        public string Identity => "Fake Admin";

        public IEnumerable<int> AllowedUseCases => Enumerable.Range(1, 1000);
    }
}
