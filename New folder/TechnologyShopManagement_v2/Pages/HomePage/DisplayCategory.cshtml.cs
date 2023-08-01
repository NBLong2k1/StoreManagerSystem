using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using System.Text.Json;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.HomePage
{
    public class DisplayCategoryModel : PageModel
    {
        public const string SessionNumber = "numberCart";
        private readonly Technology_ManagementContext _dbContext;


        private readonly HttpClient client = null;
        private string ProductApiUlr = "";
        public IEnumerable<Product> productsList { get; set; }


        public Category categories { get; set; }
        public DisplayCategoryModel(Technology_ManagementContext dbContext)
        {
            _dbContext = dbContext;

            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUlr = "https://localhost:7179/api/Products";

            

        }
        public string stringSearch { get; set; }
        public async Task<IActionResult> OnGet(string productType, string searchString)
        {
             
            productsList = new List<Product>();
            categories = new Category();
            if (productType!=null)
            {
                 

                //productsList = from lst in _dbContext.Products
                //               where lst.CategoryCode.Equals(productType) && lst.Status>0 && lst.Stock > 0
                //               select lst;

                stringSearch = productType;

                ProductApiUlr += "/GetProductByType/" + productType;
                HttpResponseMessage respone = await client.GetAsync(ProductApiUlr);
                string strData = await respone.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                productsList = JsonSerializer.Deserialize<List<Product>>(strData, options);

            }
            else if (searchString != null)
            {


                //productsList = from lst in _dbContext.Products
                //               where (lst.ProductName.Contains(searchString) || lst.CategoryCode.Contains(searchString)) && lst.Status > 0 && lst.Stock > 0
                //               select lst;
                stringSearch = searchString;

                ProductApiUlr += "/GetProductByName/" + searchString;
                HttpResponseMessage respone = await client.GetAsync(ProductApiUlr);
                string strData = await respone.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                productsList = JsonSerializer.Deserialize<List<Product>>(strData, options);

             
            }
       
            return Page();

        }

        public IEnumerable<Product> displayProduct(string searchString, string type)
        {
            IEnumerable<Product> productDisplay = new List<Product>();
            if (searchString != null)
            {
                productDisplay = from product in _dbContext.Products
                                 join catego in _dbContext.Categories
                                on product.CategoryCode equals catego.Code
                                 where product.ProductName.Contains(searchString) || catego.Name.Contains(searchString)
                                 select product;
            }
            else if (type.Length> 0)
            {
                productDisplay = from product in _dbContext.Products
                                 where product.CategoryCode == type
                                 select product;
            }



            return productDisplay;

        }


        public List<(int proId, int proQuantity)> getList()
        {
            List<(int proId, int proQuantity)> list = new List<(int proId, int proQuantity)>();
            var cookie = Request.Cookies["myCard"];
            //empty string to append new cookie
            string newCookie = "";
            if (cookie != null)
            {
                //list to split by ' AND '
                string[] items = cookie.Split("AND");


                foreach (string item in items)
                {
                    //list to split by ' - '
                    string[] pair = item.Split("-");
                    try
                    {
                        //add to list with Key and Value
                        list.Add((int.Parse(pair[0]), int.Parse(pair[1])));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("error " + ex);
                    }
                }

            }
            return list;

        }
    }
}
