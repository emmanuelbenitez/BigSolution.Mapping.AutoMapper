#region Copyright & License

// Copyright © 2020 - 2020 Emmanuel Benitez
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

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
        public void CreateFailed()
        {
            Action action = () => new AutoMapperMapper<Source, Destination>(null);
            action.Should().ThrowExactly<ArgumentNullException>().Where(exception => exception.ParamName == "mapper");
        }

        [Fact]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        public void CreateSucceeds()
        {
            var fakeMapper = new Mock<IMapper>();
            Action action = () => new AutoMapperMapper<Source, Destination>(fakeMapper.Object);
            action.Should().NotThrow();
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
