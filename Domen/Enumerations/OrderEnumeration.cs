using System;
using System.Collections.Generic;
using System.Text;

namespace Domen.Enumerations
{
    public enum OrderStatus
    {
        Hold,
        Completed,
        Shipped,
        Canceled,
        Pending
    }

    public enum PaymentMethod
    {
        Cash,
        Checks,
        Card,
    }
}
