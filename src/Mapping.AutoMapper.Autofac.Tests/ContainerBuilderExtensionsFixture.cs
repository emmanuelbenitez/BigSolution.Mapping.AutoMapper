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

using System.Collections.Generic;
using Autofac;
using AutoMapper;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace BigSolution.Infra.Mapping
{
    public class ContainerBuilderExtensionsFixture
    {
        [Fact]
        public void ConfigurationProviderResolved()
        {
            _containerBuilder.RegisterAutoMapper();
            var container = _containerBuilder.Build();

            container.Resolve<IConfigurationProvider>().Should().NotBeNull()
                .And.BeOfType<MapperConfiguration>();
        }

        [Fact]
        public void MapperResolved()
        {
            _containerBuilder.RegisterAutoMapper();

            var container = _containerBuilder.Build();
            container.Resolve<IMapper>().Should().NotBeNull()
                .And.BeOfType<Mapper>();
        }

        [Fact]
        public void ProfilesRegistered()
        {
            _containerBuilder.RegisterProfiles(GetType().Assembly);

            var container = _containerBuilder.Build();
            var profiles = container.Resolve<IEnumerable<Profile>>();
            profiles.Should().ContainSingle().And.AllBeOfType<FakeProfile>();
        }

        [Fact]
        public void RegisterAutoMapperSucceeds()
        {
            _containerBuilder.RegisterAutoMapper();
            var container = _containerBuilder.Build();

            container.IsRegistered(typeof(IMapper<string, string>)).Should().BeTrue();
            container.IsRegistered<IConfigurationProvider>().Should().BeTrue();
            container.IsRegistered<IMapper>().Should().BeTrue();
        }

        private readonly ContainerBuilder _containerBuilder = new ContainerBuilder();

        [UsedImplicitly]
        private sealed class FakeProfile : Profile { }
    }
}
