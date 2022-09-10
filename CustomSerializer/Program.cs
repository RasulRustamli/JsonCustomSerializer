using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace CustomSerializer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product> list = new List<Product>
            {
                new Product
                {
                    Name = "ProductTest",
                    Categories = new List<Category>{
                        new Category
                        {
                           Name = "CategoryTest"
                        } }
                }
            };

            //Employee employee = new Employee
            //{
            //    Name = "Asgar",
            //    Surname = "Babayev",
            //    Salary = 1200
            //};

            var customSerialize = CustomJsonSerializer.Serialize(list);
            Console.WriteLine(customSerialize);

            var defaultSerialize = JsonSerializer.Serialize(list);
            Console.WriteLine(defaultSerialize);

        }
    }
}
