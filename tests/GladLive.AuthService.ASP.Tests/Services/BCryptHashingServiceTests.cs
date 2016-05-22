using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GladLive.AuthService.ASP.Tests
{

	public class BCryptHashingServiceTests
	{
		[Fact]
		public void Test_Ctor_Doesnt_Throw()
		{
			//assert
			BCryptHashingService hashing = new BCryptHashingService();
		}

		[Theory]
		[Xunit.InlineData("Test")]
		[Xunit.InlineData("")]
		[Xunit.InlineData("Tes39359*89y858*&&&*")]
		[Xunit.InlineData("UIBdiugbsduigbsdguibseuibIUBbiu98*(89689(Y^89698^*(*^*(^UIOHIUOIOO")]
		[Xunit.InlineData("1")]
		public void Test_Same_String_Hashes_To_Non_Equal_Hash(string stringToTest)
		{
			//arrange
			BCryptHashingService hasher = new BCryptHashingService();

			//assert
			//Check that the hasher doesn't produce same hashs for indentical strings
			Assert.NotEqual(hasher.Hash(stringToTest), hasher.Hash(stringToTest));
		}

		[Theory]
		[Xunit.InlineData("Test")]
		[Xunit.InlineData("")]
		[Xunit.InlineData("Tes39359*89y858*&&&*")]
		[Xunit.InlineData("UIBdiugbsduigbsdguibseuibIUBbiu98*(89689(Y^89698^*(*^*(^UIOHIUOIOO")]
		[Xunit.InlineData("1")]
		public void Test_Same_String_Hashes_Verify_Against_Eachother(string stringToTest)
		{
			//arrange
			BCryptHashingService hasher = new BCryptHashingService();

			//assert
			//Check that the hasher doesn't produce same hashs for indentical strings
			Assert.True(hasher.isHashValuesEqual(stringToTest, hasher.Hash(stringToTest)));
		}
	}
}
