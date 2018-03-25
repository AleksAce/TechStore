using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TechStore.Models.ViewModels;

namespace TechStore.Tests.ViewModelTests
{
    [TestFixture]
    public class CreateProductViewModelTest
    {
        private List<string> FakeStrings = new List<string>() { "string1", "string2" };
        [Test]
        public void GetSelectListItems_RecievingList_ReturnsProperSelectListItems()
        {
            CreateProductViewModel cvm = new CreateProductViewModel();

            List<SelectListItem> result = cvm.GetSelectListItems(FakeStrings);

            Assert.IsTrue(result.Count == FakeStrings.Count);
            Assert.NotNull(result);
            Assert.IsTrue(result.First().Text == FakeStrings.First());
        }
        [Test]
        public void GetSelectListItems_RecievingEmptyListOrNull_ReturnsNoCategoryString()
        {
            CreateProductViewModel cvm = new CreateProductViewModel();

            List<SelectListItem> result = cvm.GetSelectListItems(new List<string>());

            Assert.IsTrue(result.Count == 1);
            Assert.NotNull(result);
            Assert.IsTrue(result.First().Text.Contains("ERROR"));
            Assert.IsTrue(result.First().Value == null);
        }
        
    }
}
