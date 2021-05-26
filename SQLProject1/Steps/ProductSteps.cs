using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SQLProject.Steps
{
    [Binding]
    class ProductSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly SqlHelper _sqlHelper;

        public ProductSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            //var webDriver = _scenarioContext.Get<IWebDriver>(Context.WebDriver);
            _sqlHelper = new SqlHelper("Shop");
           
        }

        [Given(@"Connected to BD")]
        public void GivenConnectedToBd()
        {
            _sqlHelper.OpenConnection();
        }

        [When(@"I send request with valid data to insert in Product table")]
        public void WhenISendRequestWithValidDataToInsertInProductTable()
        {
            _sqlHelper.Insert("Products",
                    new Dictionary<string, string> { { "Id", "15" }, { "Name", "'TestName'" }, { "Count", "44" } });
        }

        [When(@"I send request with valid data to update existing row in Product table")]
        public void WhenISendRequestWithValidDataToUpdateExistingRowInProductTable()
        {
            _sqlHelper.Update("Products", new Dictionary<string, string> { { "Id", "5" }, { "Name", "'TestUpdate'" }, { "Count", "10" } },
               new Dictionary<string, string> { { "Count", "44" } }
               );
        }

        [When(@"I send request with valid data to delete existing row in Product table")]
        public void WhenISendRequestWithValidDataToDeleteExistingRowInProductTable()
        {
            _sqlHelper.Delete("[Shop].[dbo].[Products]", new Dictionary<string, string> { { "Name", "'TestUpdate'" } });
        }
        
    }
}
