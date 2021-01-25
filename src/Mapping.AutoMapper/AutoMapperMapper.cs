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

using AutoMapper;
using JetBrains.Annotations;

namespace BigSolution.Infra.Mapping
{
    public class AutoMapperMapper<TSource, TDestination> : IMapper<TSource, TDestination>
    {
        public AutoMapperMapper([NotNull] IMapper mapper)
        {
            Requires.Argument(mapper, nameof(mapper))
                .IsNotNull()
                .Check();

            _mapper = mapper;
        }

        #region IMapper<TSource,TDestination> Members

        public TDestination Map(TSource source)
        {
            return _mapper.Map<TDestination>(source);
        }

        #endregion

        private readonly IMapper _mapper;
    }
}
