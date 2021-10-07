using AutoFixture;
using AutoFixture.Xunit2;
using AutoFixture.AutoMoq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests.Attributes
{
    public class AutoMoqAttribute : AutoDataAttribute
    {
        public AutoMoqAttribute() : base(new Fixture().Customize(new AutoMoqCustomization()))
        {

        }

    }
}
