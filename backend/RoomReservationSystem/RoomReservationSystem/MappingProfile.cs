using AutoMapper;
using RoomReservationSystem.Models.Dto;
using RoomReservationSystem.Models.Dto.CreationDto;
using RoomReservationSystem.Models.Dto.Data;
using RoomReservationSystem.Models.Entities;

namespace RoomReservationSystem
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Reservation, ReservationDataDto>();
            CreateMap<Room, RoomDataDto>();
            CreateMap<User, UserDataDto>();
            CreateMap<Layer, LayerDataDto>();


            CreateMap<CreateReservationDto, Reservation>();

            CreateMap<Reservation, ReservationDto>()
                .ForMember(dto => dto.ReservationData, x => x.MapFrom(x => x))
                .ForMember(dto => dto.RoomData, x => x.MapFrom(x => x.Room))
                .ForMember(dto => dto.ParticipantsData, x => x.MapFrom(x => x.Participants))
                .ForMember(dto => dto.LayerData, x => x.MapFrom(x => x.Room.Layer));





        }



    }
}
