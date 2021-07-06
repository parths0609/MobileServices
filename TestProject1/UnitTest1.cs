using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileServices.Controllers;
using MobileServices.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckCategoryAddition()
        {
            var categories = new Categories()
            {
                CategoryName = "Apple",
            };

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(categories, new ValidationContext(categories), validationResults, true);


            Assert.IsTrue(actual, "Expected validation to succeed.");
            Assert.AreEqual(0, validationResults.Count, "Unexpected number of validation errors.");
        }
        [TestMethod]
        public void CheckItemAddition()
        {
            var Items = new Items()
            {
               ItemName="Galaxy M30S",
               Price=15000,
               CategoryId=9,
  
            };

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(Items, new ValidationContext(Items), validationResults, true);


            Assert.IsTrue(actual, "Expected validation to succeed.");
            Assert.AreEqual(0, validationResults.Count, "Unexpected number of validation errors.");
        }

        [TestMethod]
        public void CheckSaleAddition()
        {
            var Sales = new Sales()
            {
                DateOfSale= new System.DateTime(2019, 12, 24),
                SellingPrice=15000,
                ItemId=10,
                BrandId=9
            };

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(Sales, new ValidationContext(Sales), validationResults, true);


            Assert.IsTrue(actual, "Expected validation to succeed.");
            Assert.AreEqual(0, validationResults.Count, "Unexpected number of validation errors.");
        }


    }
}
