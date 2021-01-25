#region Copyright & License

// Copyright © 2020 - 2021 Emmanuel Benitez
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
using System.Reflection;
using Autofac;
using AutoMapper;

namespace BigSolution.Infra.Mapping
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterAutoMapper(this ContainerBuilder builder)
        {
            Requires.Argument(builder, nameof(builder))
                .IsNotNull()
                .Check();

            builder.RegisterGeneric(typeof(AutoMapperMapper<,>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Register(
                    context => {
                        var profiles = context.Resolve<IEnumerable<Profile>>();
                        return new MapperConfiguration(config => config.AddProfiles(profiles));
                    })
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.Register(
                    ctx => {
                        var scope = ctx.Resolve<ILifetimeScope>();
                        return new Mapper(
                            ctx.Resolve<IConfigurationProvider>(),
                            scope.Resolve);
                    })
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }

        public static void RegisterProfiles(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            Requires.Argument(builder, nameof(builder))
                .IsNotNull()
                .Check();

            builder.RegisterAssemblyTypes(assemblies)
                .IsProfile()
                .As<Profile>()
                .SingleInstance();
        }
    }
}
