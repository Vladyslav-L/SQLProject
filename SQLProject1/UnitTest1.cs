using NUnit.Framework;
using System.Collections.Generic;

namespace SQLProject
{
    public class Tests
    {
        private SqlHelper _sqlHelper;

        [SetUp]
        public void Setup()
        {
            _sqlHelper = new SqlHelper("Shop");
            _sqlHelper.OpenConnection();
        }

        [TearDown]

        public void TearDown()
        {
            _sqlHelper.Delete("[Shop].[dbo].[Products]", new Dictionary<string, string> { { "Name", "'TestName'" } });
            _sqlHelper.Delete("[Shop].[dbo].[Products]", new Dictionary<string, string> { { "Name", "'TestUpdate'" } });
            _sqlHelper.CloseConnection();
        }

        [Test]
        public void CheckInsert()
        {
            _sqlHelper.Insert("Products",
                    new Dictionary<string, string> { { "Id", "1" }, { "Name", "'TestName'" }, { "Count", "44" } });
            var result = _sqlHelper.IsRowExistedInTable("Products",
                new Dictionary<string, string> { { "Id", "1" }, { "Name", "'TestName'" }, { "Count", "44" } });

            Assert.True(result);
        }

        [Test]
        public void CheckUpdate()
        {
            _sqlHelper.Update("Products", new Dictionary<string, string> { { "Id", "5" }, { "Name", "'TestUpdate'" }, { "Count", "10" } },
                new Dictionary<string, string> { { "Count", "234" } }
                );
            var result = _sqlHelper.IsRowExistedInTable("Products",
               new Dictionary<string, string> { { "Id", "5" }, { "Name", "'TestUpdate'" }, { "Count", "10" } });

            Assert.True(result);
        }
    }
}