﻿namespace Basket.Api.Entities
{
    public class BasketCheckout
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }

        //Address Part
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        //Payment
        public string BankName { get; set; }
        public int RefCode { get; set; }
        public int PaymentMethod { get; set; }

    }
}
