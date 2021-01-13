using Lab2.Models;
using Lab2.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Lab2.Controllers.API
{
    public class OrderController : ApiController
    {
        private UnitOfWork _uow;
        private bool SessionExists(string key)
        {
            return (HttpContext.Current.Session != null && HttpContext.Current.Session[key] != null);
        }
        public OrderController()
        {
            this._uow = new UnitOfWork(new Models.SageBookDBContext());
        }

        [HttpPost]
        public HttpResponseMessage OrderBook(BookOrderViewModel model)
        {
            try
            {
                Book Book = this._uow.Books.Get(model.BookId);
                if(Book == null)
                {
                    //Throw 404 (Not Found) exception if File not found.
                    ModelState.AddModelError("Error", string.Format("Book not found: {0} .", model.BookId));
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ModelState);
                }
                OrderBook order = new OrderBook(Book, model.Quantity);
                //List of orders
                List<OrderBook> cart = new List<OrderBook>();
                //If session does not exists then add order
                if (!SessionExists("cart"))
                {
                    cart.Add(order);
                }
                else
                //if session already exists
                {
                    cart = (List<OrderBook>)HttpContext.Current.Session["cart"];
                    var OrderToChange = cart.FirstOrDefault(x => x.Book.IdBook == Book.IdBook);
                    //check if order is already in card
                    //if order is in shopping card increment Quantity if not add new order
                    if (OrderToChange != null)
                        OrderToChange.Quantity++;
                    else
                        cart.Add(order);
                }
                HttpContext.Current.Session["cart"] = cart;
                //successfully ordered
                return Request.CreateResponse(HttpStatusCode.OK, new { Quantity = cart.Sum(item => item.Quantity) });
            }
            catch(Exception e)
            {
                ModelState.AddModelError("Error", "Unrecognised issue: " + e.Message);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

    }
}
