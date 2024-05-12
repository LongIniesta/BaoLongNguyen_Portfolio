using AutoMapper;
using BusinessObjects;
using DTOs;
using DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AccountDTO, Account>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<Account, RegisterRequest>().ReverseMap();
            CreateMap<AccountDTO, RegisterRequest>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Post, PostDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Gift, GiftDTO>().ReverseMap();
            CreateMap<GiftComment, GiftCommentDTO>().ReverseMap();
            CreateMap<Service, ServiceDTO>().ReverseMap();
            CreateMap<ServiceScheduler, ServiceSchedulerDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
            CreateMap<Item, ItemDTO>().ReverseMap();
            CreateMap<React, ReactDTO>().ReverseMap();
            CreateMap<ReactType, ReactTypeDTO>().ReverseMap();
            CreateMap<Report, ReportDTO>().ReverseMap();

            //Hiep
            CreateMap<AccountUpdateProfileRequest, Account>().ReverseMap();
        }
    }
}
