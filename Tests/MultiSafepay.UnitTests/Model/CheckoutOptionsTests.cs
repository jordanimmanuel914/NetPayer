﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiSafepay.Model;
using Newtonsoft.Json;

namespace MultiSafepay.UnitTests.Model
{
    [TestClass]
    public class CheckoutOptionsTests
    {
        [TestMethod]
        public void CheckoutOptions_Serialize_PropertyNamesAsExpected()
        {
            // Arrange
            var checkoutOptions = new CheckoutOptions();

            // Act
            var serializedObject = JsonConvert.SerializeObject(checkoutOptions);

            // Assert
            Assert.AreEqual(@"{
				""tax_tables"": null,
				""shipping_methods"": null,
				""rounding_policy"": null
			}".RemoveWhiteSpace(), serializedObject.RemoveWhiteSpace());
        }
    }
}
