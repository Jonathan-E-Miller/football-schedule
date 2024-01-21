using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixtureGenerator
{
    public interface IFixture
    {
        public IFixtureEntity? HomeEntity { get; set; }
        public IFixtureEntity? AwayEntity { get; set; }
        public string? Code { get; }
    }
}
