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
        public void ToInt_input_string_contain_not_digit_symbol_1à_or_empty_shoud_be_FormatException()
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
            long testValue = int.MinValue - 1L;
            _textInput = testValue.ToString();

            //Act

            //Assert
            Assert.Throws<OverflowException>(() => ExHConvert.ToInt(_textInput));
        }
        [Fact]
        public void ToInt_input_string_01_shoud_be_1()
        {
            //Array            
            _textInput = "01";
            var actual = 1;

            //Act
            var expected = ExHConvert.ToInt(_textInput);

            //Assert
            Assert.Equal(expected,actual);
        }
        [Fact]
        public void TryToInt_input_string_is_null_shoud_return_false_out_parametr_0()
        {
            //Array            
            _textInput = null;
            var actualReturn = false;
            var actualOutValue = 0;

            //Act
            var expectedOutValue=0;
            var expectedReturn = ExHConvert.TryToInt(_textInput,out expectedOutValue);

            //Assert
            Assert.Equal(expectedReturn, actualReturn);
            Assert.Equal(expectedOutValue, actualOutValue);
        }
        [Fact]
        public void TryToInt_input_empty_shoud_return_false_out_parametr_0()
        {
            //Array            
            _textInput = "";
            var actualReturn = false;
            var actualOutValue = 0;

            //Act
            var expectedOutValue = 0;
            var expectedReturn = ExHConvert.TryToInt(_textInput, out expectedOutValue);

            //Assert
            Assert.Equal(expectedReturn, actualReturn);
            Assert.Equal(expectedOutValue, actualOutValue);
        }
        [Fact]
        public void TryToInt_input_contain_letter_shoud_return_false_out_parametr_0()
        {
            //Array            
            _textInput = "1a";
            var actualReturn = false;
            var actualOutValue = 0;

            //Act
            var expectedOutValue = 0;
            var expectedReturn = ExHConvert.TryToInt(_textInput, out expectedOutValue);

            //Assert
            Assert.Equal(expectedReturn, actualReturn);
            Assert.Equal(expectedOutValue, actualOutValue);
        }
        [Fact]
        public void TryToInt_input_contain_whitespace_last_char_shoud_return_false_out_parametr_0()
        {
            //Array            
            _textInput = "1 ";
            var actualReturn = false;
            var actualOutValue = 0;

            //Act
            var expectedOutValue = 0;
            var expectedReturn = ExHConvert.TryToInt(_textInput, out expectedOutValue);

            //Assert
            Assert.Equal(expectedReturn, actualReturn);
            Assert.Equal(expectedOutValue, actualOutValue);
        }
        [Fact]
        public void TryToInt_input_contain_whitespace_first_char_shoud_return_false_out_parametr_0()
        {
            //Array            
            _textInput = "1 ";
            var actualReturn = false;
            var actualOutValue = 0;

            //Act
            var expectedOutValue = 0;
            var expectedReturn = ExHConvert.TryToInt(_textInput, out expectedOutValue);

            //Assert
            Assert.Equal(expectedReturn, actualReturn);
            Assert.Equal(expectedOutValue, actualOutValue);
        }
        [Fact]
        public void TryToInt_input_contain_whitespace_in_the_middle_shoud_return_false_out_parametr_0()
        {
            //Array            
            _textInput = "1 1";
            var actualReturn = false;
            var actualOutValue = 0;

            //Act
            var expectedOutValue = 0;
            var expectedReturn = ExHConvert.TryToInt(_textInput, out expectedOutValue);

            //Assert
            Assert.Equal(expectedReturn, actualReturn);
            Assert.Equal(expectedOutValue, actualOutValue);
        }
        [Fact]
        public void TryToInt_input_contain_minus_in_the_middle_shoud_return_false_out_parametr_0()
        {
            //Array            
            _textInput = "1-1";
            var actualReturn = false;
            var actualOutValue = 0;

            //Act
            var expectedOutValue = 0;
            var expectedReturn = ExHConvert.TryToInt(_textInput, out expectedOutValue);

            //Assert
            Assert.Equal(expectedReturn, actualReturn);
            Assert.Equal(expectedOutValue, actualOutValue);
        }
        [Fact]
        public void TryToInt_input_contain_minus_last_char_shoud_return_false_out_parametr_0()
        {
            //Array            
            _textInput = "1-";
            var actualReturn = false;
            var actualOutValue = 0;

            //Act
            var expectedOutValue = 0;
            var expectedReturn = ExHConvert.TryToInt(_textInput, out expectedOutValue);

            //Assert
            Assert.Equal(expectedReturn, actualReturn);
            Assert.Equal(expectedOutValue, actualOutValue);
        }
        [Fact]
        public void TryToInt_input_more_then_Maxint_shoud_return_false_out_parametr_0()
        {
            //Array            
            var maxValuePlus1 = int.MaxValue + 1L;
            _textInput = maxValuePlus1.ToString();
            var actualReturn = false;
            var actualOutValue = 0;

            //Act
            var expectedOutValue = 0;
            var expectedReturn = ExHConvert.TryToInt(_textInput, out expectedOutValue);

            //Assert
            Assert.Equal(expectedReturn, actualReturn);
            Assert.Equal(expectedOutValue, actualOutValue);
        }
        [Fact]
        public void TryToInt_input_less_then_Minint_shoud_return_false_out_parametr_0()
        {
            //Array            
            long testValue = int.MinValue - 1L;
            _textInput = testValue.ToString();
            var actualReturn = false;
            var actualOutValue = 0;

            //Act
            var expectedOutValue = 0;
            var expectedReturn = ExHConvert.TryToInt(_textInput, out expectedOutValue);

            //Assert
            Assert.Equal(expectedReturn, actualReturn);
            Assert.Equal(expectedOutValue, actualOutValue);
        }
        [Fact]
        public void TryToInt_input_Maxint_shoud_return_true_out_parametr_Maxint()
        {
            //Array            
            long testValue = int.MaxValue;
            _textInput = testValue.ToString();
            var actualReturn = true;
            var actualOutValue = int.MaxValue;

            //Act            
            var expectedReturn = ExHConvert.TryToInt(_textInput, out int expectedOutValue);

            //Assert
            Assert.Equal(expectedReturn, actualReturn);
            Assert.Equal(expectedOutValue, actualOutValue);
        }
        [Fact]
        public void TryToInt_input_valid_positive_shoud_return_true_out_parametr_valid()
        {
            //Array            
            _textInput = "40";
            var actualReturn = true;
            var actualOutValue = 40;

            //Act            
            var expectedReturn = ExHConvert.TryToInt(_textInput, out int expectedOutValue);

            //Assert
            Assert.Equal(expectedReturn, actualReturn);
            Assert.Equal(expectedOutValue, actualOutValue);
        }
        [Fact]
        public void TryToInt_input_Minint_shoud_return_true_out_parametr_Maxint()
        {
            //Array            
            long testValue = int.MinValue;
            _textInput = testValue.ToString();
            var actualReturn = true;
            var actualOutValue = int.MinValue;

            //Act            
            var expectedReturn = ExHConvert.TryToInt(_textInput, out int expectedOutValue);

            //Assert
            Assert.Equal(expectedReturn, actualReturn);
            Assert.Equal(expectedOutValue, actualOutValue);
        }
        [Fact]
        public void TryToInt_input_valid_negative_shoud_return_true_out_parametr_valid()
        {
            //Array            
            _textInput = "-40";
            var actualReturn = true;
            var actualOutValue = -40;

            //Act            
            var expectedReturn = ExHConvert.TryToInt(_textInput, out int expectedOutValue);

            //Assert
            Assert.Equal(expectedReturn, actualReturn);
            Assert.Equal(expectedOutValue, actualOutValue);
        }
        [Fact]
        public void TryToInt_input_Zero_shoud_return_true_out_parametr_Zero()
        {
            //Array            
            _textInput = "0";
            var actualReturn = true;
            var actualOutValue = 0;

            //Act            
            var expectedReturn = ExHConvert.TryToInt(_textInput, out int expectedOutValue);

            //Assert
            Assert.Equal(expectedReturn, actualReturn);
            Assert.Equal(expectedOutValue, actualOutValue);
        }
        [Fact]
        public void TryToInt_input_Zeroes_at_start_shoud_return_true_out_parametr_without_Zeroes()
        {
            //Array            
            _textInput = "0001";
            var actualReturn = true;
            var actualOutValue = 1;

            //Act            
            var expectedReturn = ExHConvert.TryToInt(_textInput, out int expectedOutValue);

            //Assert
            Assert.Equal(expectedReturn, actualReturn);
            Assert.Equal(expectedOutValue, actualOutValue);
        }
    }
}

