using AutoMapper;
using VacationHire.AdministrativeApi.Requests;
using VacationHire.AdministrativeApi.Responses;
using VacationHire.Data.Enum;
using VacationHire.Data.Models;

namespace VacationHire.AdministrativeApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CarAsset, CarAssetResponse>()
                .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => src.Asset.AssetName))
                .ForMember(dest => dest.AssetState, opt => opt.MapFrom(src => (AssetState)src.State))
                .ReverseMap();

            CreateMap<CarAsset, CreateCarAssetRequest>()
                .ReverseMap();

            CreateMap<Asset, AssetResponse>()
                .ReverseMap();

            CreateMap<Category, CategoryResponse>()
                .ReverseMap();
        }
    }
}