using Application.DataTransfer;
using AutoMapper;
using Domen.Entities;
using Domen.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Implementation.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dto => dto.UserInfo, opt => opt.MapFrom(order => order.User.FirstName + " " + order.User.LastName))
                .ForMember(dto => dto.Status, opt => opt.MapFrom(order => order.OrderStatus.ToString()))
                .ForMember(dto => dto.PaymentMethod, opt => opt.MapFrom(order => order.PaymentMethod.ToString()))
                .ForMember(dto => dto.OrderItems, opt => opt.MapFrom(order => order.OrderItems.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    Name = oi.Name,
                    ProductId = oi.ProductId,
                    Price = oi.Price,
                    Quantity = oi.Quantity
                })))
                .ForMember(dto => dto.TotalPrice, opt => opt.MapFrom(order => order.OrderItems.Sum(oi => oi.Price * oi.Quantity)));
            CreateMap<OrderDto, Order>()
                .ForMember(order => order.OrderItems, opt => opt.Ignore())
                .ForMember(order => order.OrderStatus,opt => opt.MapFrom(orderdto => OrderStatus.Pending));
        }
    }
}
