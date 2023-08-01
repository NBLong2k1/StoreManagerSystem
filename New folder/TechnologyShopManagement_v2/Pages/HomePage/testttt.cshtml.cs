using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.HomePage
{

    [BindProperties]
    public class testtttModel : PageModel
    {
        private readonly Technology_ManagementContext _context;
        public testtttModel(Technology_ManagementContext context)
        {
            _context = context; 
        }


        public string name { get; set; }

        public IEnumerable<Product> productList { get; set; }  

        public void OnGet(string searchString)
        {
            name = "longgggggg";

            //display all product
            productList = _context.Products;

            //if searchString not empty
            if (searchString!=null)
            {

                //find product by name
               productList =  from lst in _context.Products
                              where (lst.ProductName.Contains(searchString) || lst.CategoryCode.Contains(searchString)) && lst.Status > 0 && lst.Stock > 0
                              select lst;
            }
            
           
                
        }


        public void OnPost(string productName,string productType, string productPrice, string productImage)
        {
            Console.WriteLine("okay");


            //Product product = new Product
            //{
            //    ProductName = productName,
            //    ProImage = productImage,    
            //    CategoryCode= productType,
            //    Price=Decimal.Parse(productPrice)
            //};

            //_context.Products.Add(product);
            //_context.SaveChanges();


        }
    }
}
