using System.Threading.Tasks;
using CleanArchitecture.DataAccess.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Admin.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockCore StockCore;

        public StockController(IStockCore stockCore)
            => this.StockCore = stockCore;

        // GET: StockController
        public async Task<ActionResult> Index()
        {
            return this.View();
        }

        // GET: StockController/Details/5
        public ActionResult Details(int id)
        {
            return this.View();
        }

        // GET: StockController/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: StockController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }

        // GET: StockController/Edit/5
        public ActionResult Edit(int id)
        {
            return this.View();
        }

        // POST: StockController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }

        // GET: StockController/Delete/5
        public ActionResult Delete(int id)
        {
            return this.View();
        }

        // POST: StockController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return this.RedirectToAction(nameof(Index));
            }
            catch
            {
                return this.View();
            }
        }
    }
}
