using CsvHelper;
using CsvHelper.Configuration;
using LottonRandomNumberGeneratorV2;
using LottonRandomNumberGeneratorV2.Extensions;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Globalization;
using System.Linq;
using Tests.Models;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MyTestMethod()
        {
            var records = new List<CSVModel>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };

            using (var reader = new StreamReader("CSV/thunderball-draw-history.csv"))
            {
                using (var csv = new CsvReader(reader, config))
                {
                    records = csv.GetRecords<CSVModel>().ToList();
                }
            }

            var al = new CombinationAlgorithm();
            var numbers = al.Generate(39, 5);

            var results = new List<OutputModel>();

            foreach (var record in records)
            {
                int index = numbers.FindIndex(x => x.Contains(record.Ball1) && x.Contains(record.Ball2) && x.Contains(record.Ball3) && x.Contains(record.Ball4) && x.Contains(record.Ball5));
                results.Add(new OutputModel { Date = record.Date, Index = index });
            }

            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Combine the base directory with the folder name to get the full path
            string filePath = @"C:\\CSV\\thunderball-output.csv"; // Path.Combine(appDirectory, "CSV/thunderball-output.csv");

            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }


            using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (var writer = new StreamWriter(fs))
                {
                    foreach (var result in results)
                    {
                        writer.WriteLine($"{result.Date},{result.Index}");
                    }
                }
            }



            Assert.AreEqual(103, records?.Count());
        }

        [TestMethod]
        [DataRow(2, 23, 24, 29, 33, 36, 8335461, "2023-12-27")]
        public void LottoWinningNumbers(int n1, int n2, int n3, int n4, int n5, int n6, int expectedIndex, string dateTime)
        {
            //Arrange
            var numbers = this.GetLottoNumbers();

            //Act
            int index = numbers.FindIndex(x => x.Contains(n1) && x.Contains(n2) && x.Contains(n3) && x.Contains(n4) && x.Contains(n5) && x.Contains(n6));

            //Assert
            Assert.AreEqual(expectedIndex, index);
        }

        [TestMethod]
        public void LottoNumberOfCombinations3()
        {
            //Arrange & Act
            var numbers = this.GetLottoNumbers();

            int totalCount = numbers.Count();

            int pageSize = 3;
            int size = totalCount / pageSize;

            var chunkedNumbers = numbers.Chunk(size);
            int controlCount = chunkedNumbers.Sum(x => x.Count());

            var bbb = chunkedNumbers.SelectMany(x => x.OrderBy(_ => Guid.NewGuid()).Take(1).ToList()).ToList();

            //Assert
            Assert.AreEqual(numbers.Count(), controlCount);
        }


        private List<List<int>> GetLottoNumbers()
        {
            var al = new CombinationAlgorithm();
            return al.Generate(59, 6);
        }
    }
}