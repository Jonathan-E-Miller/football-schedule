using FixtureGenerator;

namespace FixtureGeneratorTests.Framework
{
    public class Match : IFixture
    {
        public IFixtureEntity? HomeEntity { get; set; }
        public IFixtureEntity? AwayEntity { get; set; }
        
        public string Code 
        { 
            get
            {
                return $"{HomeEntity?.Code ?? string.Empty}{AwayEntity?.Code ?? string.Empty}";
            }
        }

        public Match()
        {

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
