using ExceptionHandlingConvertToIntLibruary;
using System;
using Xunit;

namespace ExceptionHandlingConvertToIntLibruaryTests
{
    public class ExHConvertTest
    {
        private string _textInput;
        [Fact]
        public void ToInt_input_string_Null_shoud_be_ArgumentNullException()
        {
            //Array
            _textInput = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() => ExHConvert.ToInt(_textInput));
        }
        [Fact]
        public void ToInt_input_string_contain_not_digit_symbol_or_empty_shoud_be_FormatException()
        {
            //Array
            _textInput = "1a";

            //Act

            //Assert
            Assert.Throws<FormatException>(() => ExHConvert.ToInt(_textInput));
        }
        [Fact]
        public void ToInt_input_string_is_12_shoud_be_12()
        {
            //Array
            _textInput = "12";
            var actual = 12;

            //Act
            var expected = ExHConvert.ToInt(_textInput);

            //Assert
            Assert.Equal(expected,actual);
        }
        [Fact]
        public void ToInt_input_string_more_than_Maxint__shoud_be_OverflowException()
        {
            //Array
            var maxValuePlus1 = int.MaxValue + 1L;
            _textInput = maxValuePlus1.ToString();

            //Act

            //Assert
            Assert.Throws<OverflowException>(() => ExHConvert.ToInt(_textInput));
        }
        [Fact]
        public void ToInt_input_string_less_than_Minint_shoud_be_OverflowException()
        {
            //Array
            var testValue = int.MinValue - 1L;
            _textInput = testValue.ToString();

            //Act

            //Assert
            Assert.Throws<OverflowException>(() => ExHConvert.ToInt(_textInput));
        }
    }
}
