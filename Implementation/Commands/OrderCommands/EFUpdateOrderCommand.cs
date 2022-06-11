using Application.Commands.OrderCommands;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domen.Entities;
using Domen.Enumerations;
using Implementation.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Commands.OrderCommands
{
    public class EFUpdateOrderCommand : IUpdateOrderCommand
    {
        private readonly Context _context;

        public EFUpdateOrderCommand(Context context)
        {
            _context = context;
        }
        public int Id => 16;

        public string Name => "Change order status with Entity Framework";

        public void Execute(ChangeOrderStatusDto request)
        {
            var order = _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).FirstOrDefault(x => x.Id == request.OrderId);

            if (order == null)
            {
                throw new EntityNotFoundException(request.OrderId, typeof(Order));
            }

            if (order.OrderStatus == OrderStatus.Completed)
            {
                throw new ConflictException("Can not change status of delivered order.");
            }

            if (order.OrderStatus == OrderStatus.Canceled)
            {
                throw new ConflictException("Can not change status of canceled order.");
            }

            if (order.OrderStatus == OrderStatus.Hold || order.OrderStatus == OrderStatus.Shipped || order.OrderStatus == OrderStatus.Pending)
            {
                if (request.Status == OrderStatus.Canceled || request.Status == OrderStatus.Shipped)
                {
                    order.OrderStatus = request.Status;

                    if (request.Status == OrderStatus.Canceled)
                    {
                        foreach (var orderItem in order.OrderItems)
                        {
                            orderItem.Product.Stock += orderItem.Quantity;
                        }
                    }
                }
                if (order.OrderStatus == OrderStatus.Hold && request.Status == OrderStatus.Completed)
                {
                    throw new ConflictException("Order can not be transitioned from hold to completed directly.");
                }
                if (order.OrderStatus == OrderStatus.Shipped || request.Status == OrderStatus.Completed)
                {
                    order.OrderStatus = request.Status;
                }
            }

            _context.SaveChanges();

        }
    }
}
