using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileServices.Entities;
using static System.Console;
namespace MobileServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly MobileStoreContext _context;

        public SalesController(MobileStoreContext context)
        {
            _context = context;
        }

        // GET: GetSalesByBrand
        [Route("GetSalesByBrand/{brand_id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sales>>> GetSalesByBrand(int brand_id)
        {
            if (brand_id != 0)
                return await _context.Sales.Where(s => s.BrandId == brand_id).ToListAsync();
            else
                return BadRequest("Enter a valid brand id");
        }
        // GET: api/Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sales>>> GetSales()
        {
            return await _context.Sales.ToListAsync();
        }
        // GET: api/Sales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sales>> GetSales(int id)
        {
            var sales = await _context.Sales.FindAsync(id);

            if (sales == null)
            {
                return NotFound();
            }

            return sales;
        }
        // PUT: api/Sales/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSales(int id, Sales sales)
        {
            if (id != sales.SaleId)
            {
                return BadRequest();
            }

            _context.Entry(sales).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Sales
        [HttpPost]
        public async Task<ActionResult<Sales>> PostSales(Sales sales)
        {
            var brandId = from u in _context.Items.Where(a => a.ItemId == sales.ItemId)
                          select u.BrandId;
            bool isValidSale = false;
            if (brandId.Contains(sales.BrandId))
                isValidSale = true;
            if (isValidSale)
            {
                sales.DateOfSale = DateTime.Now;
                _context.Sales.Add(sales);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetSales", new { id = sales.SaleId }, sales);
            }
            else
            {
                return BadRequest("Item and Brand does not Match or Sale is invalid. Please try again");
            }

        }

        // DELETE: api/Sales/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sales>> DeleteSales(int id)
        {
            var sales = await _context.Sales.FindAsync(id);
            if (sales == null)
            {
                return NotFound();
            }

            _context.Sales.Remove(sales);
            await _context.SaveChangesAsync();

            return sales;
        }

        private bool SalesExists(int id)
        {
            return _context.Sales.Any(e => e.SaleId == id);
        }
        [Route("GetSalesByDateRange")]
        [HttpPost]
        public List<SalesReportResponse> SalesReportByDate(SalesReportRequest payload)
        {
            try
            {
                List<SalesReportResponse> sales = new List<SalesReportResponse>();
                int total_sales = 0, turnover = 0, totalcp = 0, totalsp = 0; TimeSpan duration = TimeSpan.FromDays(0); decimal PL = 0, PLPercent = 0;
                if (payload.EndDate == null || payload.StartDate == null)
                {
                    Response.StatusCode = StatusCodes.Status400BadRequest;
                    return null;
                }
                else
                {
                    var priceDetails = from c in _context.Items
                                       select new
                                       {
                                           c.Price,
                                           c.ItemId
                                       };
                    var recordsBetweenDates = from s in _context.Sales.Where(n => n.DateOfSale >= payload.StartDate && n.DateOfSale < payload.EndDate)
                                              select s;
                    total_sales = recordsBetweenDates.Count();
                    turnover = recordsBetweenDates.Sum(t => t.SellingPrice);
                    duration = payload.EndDate - payload.StartDate;
                    foreach (var sale in recordsBetweenDates)
                    {
                        var record = priceDetails.Where(i => i.ItemId == sale.ItemId).SingleOrDefault();
                        int costprice = record.Price;
                        totalcp += costprice;
                        int sellingprice = sale.SellingPrice;
                        totalsp += sellingprice;
                        PL += (sellingprice - costprice);
                    }
                    PLPercent = CalculateProfitLoss(totalcp, totalsp);
                    sales.Add(new SalesReportResponse
                    {
                        BrandName = "N.A",
                        Turnover = turnover,
                        Duration = (int)duration.TotalDays,
                        TotalSales = total_sales,
                        PLPercent = PLPercent,
                        PL = PL
                    });
                }
                return sales;
            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                WriteLine(e.Message); WriteLine(e.StackTrace);
                return null;
            }
        }

        [Route("GetSalesByBrand")]
        [HttpPost]
        public List<SalesReportResponse> SalesReportByBrand(SalesReportRequest payload)
        {
            try
            {
                List<SalesReportResponse> sales = new List<SalesReportResponse>();
                int total_sales = 0, turnover = 0, totalcp = 0, totalsp = 0; TimeSpan duration = TimeSpan.FromDays(0); decimal PL = 0, PLPercent = 0;

                if (payload.EndDate == null || payload.StartDate == null)
                {
                    Response.StatusCode = StatusCodes.Status400BadRequest;
                    return null;
                }
                else
                {
                    var priceDetails = from c in _context.Items.Where(s => s.BrandId == payload.BrandId)
                                       select new
                                       {
                                           c.Price,
                                           c.ItemId
                                       };
                    var recordsBetweenDates = from s in _context.Sales.Where(n => n.DateOfSale >= payload.StartDate && n.DateOfSale < payload.EndDate && n.BrandId == payload.BrandId)
                                              select s;

                    IQueryable<string> brand_name = from b in _context.Brands.Where(x => x.BrandId == payload.BrandId)
                                                    select b.BrandName.ToString();
                    string brand = brand_name.FirstOrDefault();

                    total_sales = recordsBetweenDates.Count();
                    turnover = recordsBetweenDates.Sum(t => t.SellingPrice);
                    duration = payload.EndDate - payload.StartDate;
                    foreach (var sale in recordsBetweenDates)
                    {
                        var record = priceDetails.Where(i => i.ItemId == sale.ItemId).SingleOrDefault();
                        int costprice = record.Price;
                        totalcp += costprice;
                        int sellingprice = sale.SellingPrice;
                        totalsp += sellingprice;
                        PL += (sellingprice - costprice);
                    }
                    PLPercent = CalculateProfitLoss(totalcp, totalsp);
                    sales.Add(new SalesReportResponse
                    {
                        BrandName = brand,
                        Turnover = turnover,
                        Duration = (int)duration.TotalDays,
                        TotalSales = total_sales,
                        PLPercent = PLPercent,
                        PL = PL
                    });

                }
                return sales;
            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                WriteLine(e.Message); WriteLine(e.StackTrace);
                return null;
            }
        }
        private decimal CalculateProfitLoss(int cp, int sp)
        {
            try
            {
                decimal PL = sp - cp;
                PL = PL / sp;
                PL = Math.Round(PL * 100, 2);
                return PL;
            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                WriteLine(e.Message); WriteLine(e.StackTrace);
                return 0;
            }
        }

    }
}

