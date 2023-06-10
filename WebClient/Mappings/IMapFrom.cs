using AutoMapper;

namespace WebClient.Mappings;

public interface IMapFrom<T>
{
    void MappingFrom(Profile profile)
    {
        Action<IMemberConfigurationExpression> memConfigExp = (opt) => opt.Condition(
            (srs, dest, sourceMember) => sourceMember != null
        );
        profile.CreateMap(typeof(T), GetType()).ForAllMembers(memConfigExp);
    }
}
