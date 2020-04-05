using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyWeatherApp.WeatherService.Deserialization;
using System;
using System.IO;

namespace MyWeatherApp.WeatherService.Test
{
    [TestClass]
    public class WeatherQueryServiceDeserializerTest
    {
        private readonly WeatherQueryServiceDeserializer _deserializer;
        private readonly string _emptyJsonTestData;
        private readonly string _validJsonTestData;

        public WeatherQueryServiceDeserializerTest()
        {
            _deserializer = new WeatherQueryServiceDeserializer();
            _emptyJsonTestData = string.Empty;
            var testFile = $"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}/JSon.txt";
            _validJsonTestData = File.ReadAllText(testFile);
        }

        [TestMethod]
        public void TestDeserializationOfEmptyData()
        {
            var result = _deserializer.DeserializeWeatherInformation(_emptyJsonTestData);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Name == null);
        }

        [TestMethod]
        public void TestDeserializationOfValidData()
        {
            var result = _deserializer.DeserializeWeatherInformation(_validJsonTestData);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Name == "Leipzig");
            Assert.IsNotNull(result.WeatherByDay);
            Assert.IsTrue(result.WeatherByDay.Count == 6);
        }
    }
}
