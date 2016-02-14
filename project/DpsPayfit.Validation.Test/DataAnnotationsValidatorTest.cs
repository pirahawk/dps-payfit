using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace DpsPayfit.Validation.Test
{
    public class DataAnnotationsValidatorTest
    {
        [Fact]
        public static void CanGetAllPropertyNames()
        {
            var instance = new MyTestType();
            var publicProperties = instance.GetAllPublicPropertyNamesToValidate();
            var allNames = publicProperties.Select(p => p.Name).ToArray();

            Assert.Contains(nameof(instance.FirstName), allNames);
            Assert.Contains(nameof(instance.LastName), allNames);
            Assert.DoesNotContain("SecretCode", allNames);
        }

        [Fact]
        public static void CanRetrievePropertyValues()
        {
            const string lastName = "bar";
            const string firstName = "Foo";
            var instance = new MyTestType { FirstName = firstName, LastName = lastName };

            var propertyValues = instance.GetPropertyValues();

            Assert.Equal(firstName, propertyValues[nameof(instance.FirstName)]);
            Assert.Equal(lastName, propertyValues[nameof(instance.LastName)]);
        }

        [Fact]
        public void TestYourAssumptionWorks()
        {
            var instance = new MyTestType
            {
                FirstName = "a"
            };
            var context = new ValidationContext(instance)
            {
                MemberName = nameof(instance.FirstName)
            };
            var objectResults = new List<ValidationResult>();
            var isObjectValid = Validator.TryValidateObject(instance, context, objectResults);

            var propertyResults = new List<ValidationResult>();
            var isPropertyValid = Validator.TryValidateProperty(instance.FirstName, context, propertyResults);
        }

        [Theory, MemberData("PropertyValidationData")]
        public void ValidateProperty(MyTestType toValidate,
            string propertyToValidate,
            Func<MyTestType, object> valueProvider,
            bool shouldBeValid)
        {
            var result = DataAnnotationsValidator.ValidateProperty(toValidate, propertyToValidate, valueProvider(toValidate));
            if (shouldBeValid)
            {
                Assert.True(result.IsValid);
            }
            else
            {
                Assert.False(result.IsValid);
                Assert.True(result.ValidationResults.Any());
            }
        }

        public static IEnumerable<object[]> PropertyValidationData
        {
            get
            {
                var fNameFunc = (Func<MyTestType, object>)(val => val.FirstName);
                var lNameFunc = (Func<MyTestType, object>)(val => val.LastName);

                var myTestType = new MyTestType();
                const string firstName = nameof(myTestType.FirstName);
                const string lastName = nameof(myTestType.LastName);

                yield return new object[]
                {
                    new MyTestType (), firstName, fNameFunc, false
                };

                yield return new object[]
                {
                    new MyTestType (), lastName, lNameFunc, false
                };

                yield return new object[]
                {
                    new MyTestType {FirstName = null, LastName = "foo"}, firstName, fNameFunc, false
                };

                yield return new object[]
                {
                    new MyTestType {FirstName = "foo"}, firstName, fNameFunc, true
                };
            }
        } 
    }

    public class MyTestType
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        private int SecretCode { get; set; }
    }
}
