using FixtureGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixtureGeneratorTests.Framework
{
    public class Match : IFixture
    {
        public IFixtureEntity? HomeEntity { get; set; }
        public IFixtureEntity? AwayEntity { get; set; }
        public string Code { get; set; } = string.Empty;

        public Match()
        {

        }

        public void SetUniqueMatchCode()
        {
            if (HomeEntity != null && AwayEntity != null)
            {
                Code = $"{HomeEntity.Code}{AwayEntity.Code}";
            }
        }

        public override string ToString()
        {
            if (HomeEntity != null && AwayEntity != null)
            {
                return $"{HomeEntity.Code} v {AwayEntity.Code}";
            }
            return "INVALID";
        }
    }
}
