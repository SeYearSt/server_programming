using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab2.Models
{
    public class OrderBook
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public OrderBook(Book book, int quantity = 1)
        {
            this.Book = book;
            this.Quantity = quantity;
        }
    }
}