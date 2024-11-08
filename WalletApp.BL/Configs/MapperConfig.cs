using AutoMapper;
using WalletApp.Core.Models;
using WalletApp.Core.Models.DTOs;

namespace WalletApp.BL.Configs;

public static class MapperConfig
{
    public static MapperConfiguration RegisterMapperConfig()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<User, UserRegisterDto>();
            config.CreateMap<UserRegisterDto, User>();

            config.CreateMap<Card, CardDto>();
            config.CreateMap<CardDto, Card>();
        });

        return mappingConfig;
    }
}