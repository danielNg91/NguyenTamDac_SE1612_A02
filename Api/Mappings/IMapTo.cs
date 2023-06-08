using AutoMapper;

namespace Api.Mappings;

public interface IMapTo<T>
{
    void MappingTo(Profile profile)
    {
        Action<IMemberConfigurationExpression> memConfigExp = (opt) => opt.Condition(
            (srs, dest, sourceMember) => sourceMember != null
        );
        profile.CreateMap(GetType(), typeof(T)).ForAllMembers(memConfigExp);
    }
}
