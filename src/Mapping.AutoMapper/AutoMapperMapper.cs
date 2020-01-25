using AutoMapper;

namespace BigSolution.Infra.Mapping
{
    public class AutoMapperMapper<TSource, TDestination> : IMapper<TSource, TDestination>
    {
        private readonly IMapper _mapper;

        public AutoMapperMapper(IMapper mapper)
        {
            Requires.NotNull(mapper, nameof(mapper));

            _mapper = mapper;
        }

        public TDestination Map(TSource source)
        {
            return _mapper.Map<TDestination>(source);
        }
    }
}