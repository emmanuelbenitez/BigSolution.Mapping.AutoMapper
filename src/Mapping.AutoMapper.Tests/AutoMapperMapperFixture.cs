using System;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;

namespace BigSolution.Infra.Mapping
{
    public class AutoMapperMapperFixture
    {
        [Fact]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        public void CreateSucceeds()
        {
            var fakeMapper = new Mock<IMapper>();
            Action action = () => new AutoMapperMapper<Source, Destination>(fakeMapper.Object);
            action.Should().NotThrow();

        }

        [Fact]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        public void CreateFailed()
        {
            Action action = () => new AutoMapperMapper<Source, Destination>(null);
            action.Should().ThrowExactly<ArgumentNullException>().Where(exception => exception.ParamName == "mapper");
        }

        [Fact]
        public void MapSucceeds()
        {
            var fakeMapper = new Mock<IMapper>();
            fakeMapper.Setup(m => m.Map<Destination>(It.IsAny<Source>()))
                .Returns(new Destination());
            var mapper = new AutoMapperMapper<Source, Destination>(fakeMapper.Object);
            
            mapper.Map(new Source()).Should().NotBeNull();
            fakeMapper.Verify(m => m.Map<Destination>(It.IsAny<Source>()), Times.Once);
        }

        private class Source { }

        private class Destination { }
    }
}
