using BusinessObject.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class incomeDAO
    {
        public List<Decimal> getListTotalEachMonth()
        {
            List<Decimal> totalEachMonth = new List<Decimal>();
            try
            {
                using (var context = new Technology_ManagementContext())
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        var total = from o in context.Orders
                                    where (o.DateRecipt ?? DateTime.MinValue).Month == i && o.Status == 2
                                    select o;
                        Decimal sum = 0;
                        foreach (var o in total)
                        {

                            sum += (decimal)o.TotalPrice;


                        }
                        totalEachMonth.Add(sum);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
             
            return totalEachMonth;
        }
        public decimal getTotalDaily()
        {
            Decimal sumToday = 0;

            try
            {
                using (var context = new Technology_ManagementContext())
                {
                    var today = context.Orders
                           .Where(s => s.DateRecipt.Equals(DateTime.Today) && s.Status == 2);


                    foreach (var order in today)
                    {
                        sumToday += (Decimal)order.TotalPrice;

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            

            return sumToday;
        }
        public decimal getTotalDailyGuest()
        {
            Decimal sumToday = 0;

            try
            {
                using (var context = new Technology_ManagementContext())
                {
                    var today = context.Orders
                            .Include(s => s.Customer)
                            .Where(s => s.DateRecipt.Equals(DateTime.Today) && s.Status == 2 && s.Customer.Role == 3);


                    foreach (var order in today)
                    {
                        sumToday += (Decimal)order.TotalPrice;

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

            

            return sumToday;
        }
        public decimal getTotalDailyCustomer()
        {


            Decimal sumToday = 0;

            try
            {
                using (var context = new Technology_ManagementContext())
                {
                    var today = context.Orders
                           .Include(s => s.Customer)
                           .Where(s => s.DateRecipt.Equals(DateTime.Today) && s.Status == 2 && s.Customer.Role == 0);


                    foreach (var order in today)
                    {
                        sumToday += (Decimal)order.TotalPrice;

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            

            return sumToday;
        }
        public decimal getTotalMonthly()
        {
            Decimal sum = 0;
            var thisMonth = DateTime.Now.Month;
            try
            {
                using (var context = new Technology_ManagementContext())
                {

                    var total = from o in context.Orders
                                where (o.DateRecipt ?? DateTime.MinValue).Month == thisMonth && o.Status == 2
                                select o;

                    foreach (var o in total)
                    {

                        sum += (decimal)o.TotalPrice;


                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            } 
            return sum;
        }
        public decimal getTotalYear()
        {
            Decimal sum = 0;
            int getYear = DateTime.Now.Year;

            try
            {
                using (var context = new Technology_ManagementContext())
                {

                    var total = from o in context.Orders
                                where (o.DateRecipt ?? DateTime.MinValue).Year == getYear && o.Status == 2
                                select o;

                    foreach (var o in total)
                    {

                        sum += (decimal)o.TotalPrice;


                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
             
            return sum;
        }


    }
}
