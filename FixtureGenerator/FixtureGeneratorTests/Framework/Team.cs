using FixtureGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixtureGeneratorTests.Framework
{
    public class Team : IFixtureEntity
    {
        public string Code { get; set; }

        public Team(string code)
        {
            Code = code;
        }

        public override string ToString()
        {
            return Code;
        }
    }
}
