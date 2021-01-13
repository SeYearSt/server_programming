using Lab2.Models;
using Lab2.Patterns.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Lab2.Controllers
{
    public class OrderController : Controller
    {
        private IRepository<Book> repo;
        public OrderController(BookRepository book)
        {
            this.repo = book;
        }
        private bool SessionExists(string key)
        {
            return (Session[key] != null);
        }
        // GET: Order/Index
        public ActionResult Index()
        {
            //List of orders
            List<OrderBook> cart = new List<OrderBook>();
            if(SessionExists("cart"))
            {
                cart = (List<OrderBook>)Session["cart"];
            }
            return View(cart);
        }

        // GET: Order/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (SessionExists("cart"))
            {
                // List of orders
                List<OrderBook> cart = (List<OrderBook>)Session["cart"];
                cart.RemoveAll(x => x.Book.IdBook == id);
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }
        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            OrderBook order = ((List<OrderBook>)Session["cart"]).FirstOrDefault(x => x.Book.IdBook == (int)id);
            if (order == null)
                return RedirectToAction("Index");
            else
                return View(order);
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, int Quantity)
        {
            if (Quantity > 0)
            {
                List<OrderBook> cart = (List<OrderBook>)Session["cart"];
                OrderBook order = cart.FirstOrDefault(x => x.Book.IdBook == id);
                order.Quantity = Quantity;
                Session["cart"] = cart;
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Delete", new { id = id });
        }
    }
}
