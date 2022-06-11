using DataAccess;
using Domen.Entities;
using Domen.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitialController : ControllerBase
    {
        private Context _context;
        public InitialController(Context context)
        {
            _context = context;
        }
        // POST api/<InitialController>
        [HttpPost]
        public IActionResult Post()
        {

            var categories = new List<Category>
            {
                new Category
                {
                    Name = "Books"
                },
                new Category
                {
                    Name = "Sports Equipment"
                },
                new Category
                {
                    Name = "Technology"
                },
                new Category
                {
                    Name = "Toys"
                }
            };
            var user = new List<User>
            {
                new User
                {
                    FirstName="Nikola",
                    LastName="Kralj",
                    Email="user@gmail.com",
                    Password="22e5ab5743ea52caf34abcc02c0f161d"
                },
                new User
                {
                    FirstName="Nikola",
                    LastName="Kralj",
                    Email="admin@gmail.com",
                    Password="22e5ab5743ea52caf34abcc02c0f161d"
                }
            };

            var orders = new List<Order>
            {
                new Order
                {
                    OrderDate = new DateTime(2022, 5, 22, 22, 22, 22),
                    Address = "Durmitorska 12",
                    OrderStatus = OrderStatus.Completed,
                    PaymentMethod = PaymentMethod.Cash,
                    User = user.First()
                },
                new Order
                {
                    OrderDate = new DateTime(2022, 6, 1, 12, 21, 0),
                    Address = "Durmitorska 12",
                    OrderStatus = OrderStatus.Shipped,
                    PaymentMethod = PaymentMethod.Checks,
                    User = user.First()
                }
            };

            var products = new List<Product>
            {
                new Product
                {
                    Name = "Ana Karenina",
                    Description = "Anna Karenina is a novel by the Russian author Leo Tolstoy, first published in book form in 1878. Widely considered to be one of the greatest works of literature ever written, Tolstoy himself called it his first true novel. It was initially released in serial installments from 1875 to 1877, all but the last part appearing in the periodical The Russian Messenger.",
                    Image = "ana.png",
                    Stock = 20000,
                    Category = categories.ElementAt(0)
                },
                new Product
                {
                    Name = "War and Peace",
                    Description = "War and Peace is a literary work mixed with chapters on history and philosophy by the Russian author Leo Tolstoy. It was first published serially, then published in its entirety in 1869. It is regarded as one of Tolstoy's finest literary achievements and remains an internationally praised classic of world literature.",
                    Image = "wp.png",
                    Stock = 2500,
                    Category = categories.ElementAt(0)
                },
                new Product
                {
                    Name = "Nike ball",
                    Description = "Great ball to play football with your friends or family, it can be used even for professional matches",
                    Image = "ball.png",
                    Stock = 1000,
                    Category = categories.ElementAt(1)
                },
                new Product
                {
                    Name = "Nike goalkeeper gloves",
                    Description = "Be on the highest level when it is most important with this high-end gloves",
                    Image = "gloves.png",
                    Stock = 150,
                    Category = categories.ElementAt(1)
                },
                new Product
                {
                    Name = "Bratz doll",
                    Description = "Best fun is where Bratz dolls are. Take them as soon as you can and make your day bettter",
                    Image = "doll.png",
                    Stock = 300,
                    Category = categories.ElementAt(3)
                },
                new Product
                {
                    Name = "Apple Airpods",
                    Description = "AirPods models deliver an unparalleled wireless experience, from magical setup to high-quality sound. Available with free engraving.",
                    Image = "pods.png",
                    Stock = 500,
                    Category = categories.ElementAt(2)
                },
            };


            var prices = new List<Price>
            {
                new Price
                {
                    PriceAmount = 10,
                    Product = products.ElementAt(0)
                },
                new Price
                {
                    PriceAmount = 15,
                    Product = products.ElementAt(1)
                },
                new Price
                {
                    PriceAmount = 13,
                    Product = products.ElementAt(2)
                },
                new Price
                {
                    PriceAmount = 20,
                    Product = products.ElementAt(3)
                },
                new Price
                {
                    PriceAmount = 17,
                    Product = products.ElementAt(4)
                },
                new Price
                {
                    PriceAmount = 100,
                    Product = products.ElementAt(5)
                }
            };



            var OrderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    Quantity = 3,
                    Name = products.ElementAt(0).Name,
                    Product = products.ElementAt(0),
                    Price = prices.ElementAt(0).PriceAmount,
                    Order = orders.ElementAt(0)
                },
                new OrderItem
                {
                    Quantity = 1,
                    Name = products.ElementAt(1).Name,
                    Product = products.ElementAt(1),
                    Price = prices.ElementAt(1).PriceAmount,
                    Order = orders.ElementAt(0)
                },
                new OrderItem
                {
                    Quantity = 2,
                    Name = products.ElementAt(2).Name,
                    Product = products.ElementAt(2),
                    Price = prices.ElementAt(2).PriceAmount,
                    Order = orders.ElementAt(0)
                },
                new OrderItem
                {
                    Quantity = 2,
                    Name = products.ElementAt(3).Name,
                    Product = products.ElementAt(3),
                    Price = prices.ElementAt(3).PriceAmount,
                    Order = orders.ElementAt(1)
                },
                new OrderItem
                {
                    Quantity = 3,
                    Name = products.ElementAt(4).Name,
                    Product = products.ElementAt(4),
                    Price = prices.ElementAt(4).PriceAmount,
                    Order = orders.ElementAt(1)
                },
            };

            //user
            var userUseCases = Enumerable.Range(1, 20).ToList();
            //admin
            var adminUseCases = Enumerable.Range(1, 50).ToList();

            userUseCases.ForEach(useCase => _context.UserUseCases.Add(new UserUseCase
            {
                User = user.ElementAt(0),
                UseCaseId = useCase
            }));

            adminUseCases.ForEach(useCase => _context.UserUseCases.Add(new UserUseCase
            {
                User = user.ElementAt(1),
                UseCaseId = useCase
            }));

            _context.Categories.AddRange(categories);
            _context.Users.AddRange(user);
            _context.Orders.AddRange(orders);
            _context.Products.AddRange(products);
            _context.Prices.AddRange(prices);
            _context.OrderItems.AddRange(OrderItems);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
