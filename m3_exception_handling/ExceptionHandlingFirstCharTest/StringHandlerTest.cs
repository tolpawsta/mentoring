using ExceptionHandlingFirstCharOfString;
using ExceptionHandlingFirstCharOfString.Exceptions;
using System;
using Xunit;

namespace ExceptionHandlingFirstCharTest
{
    public class StringHandlerTest
    {
        private StringHandler _handler;
        private string _testText;
        public StringHandlerTest()
        {
            _handler = new StringHandler();
        }
        [Fact]
        public void GetFirstSymbolFromLine_when_line_is_Null_shoud_be_ArgumentNullException()
        {
            //Array
            _testText = null;

            //Act
            var ex = Record.Exception(() => _handler.GetFirstSymbolFromLine(_testText));

            //Assert
            Assert.IsType<ArgumentNullException>(ex);
        }
        [Fact]
        public void GetFirstSymbolFromLine_when_line_is_Qwerty_shoud_be_Q()
        {
            //Array
            _testText = "Qwerty";
            var actual = 'Q';

            //Act
            var expected=_handler.GetFirstSymbolFromLine(_testText);

            //Assert
            Assert.Equal(expected,actual);
        }
        [Fact]
        public void GetFirstSymbolFromLine_when_line_consist_of_whitesaces_shoud_be_NoElementsException()
        {
            //Array
            _testText = " ";

            //Act
            var ex = Record.Exception(() => _handler.GetFirstSymbolFromLine(_testText));

            //Assert
            Assert.IsType<NoElementsException>(ex);
        }
        [Fact]
        public void GetFirstSymbolFromLine_when_line_is_empty_shoud_be_NoElementsException()
        {
            //Array
            _testText = "";

            //Act
            var ex = Record.Exception(() => _handler.GetFirstSymbolFromLine(_testText));

            //Assert
            Assert.IsType<NoElementsException>(ex);
        }
        [Fact]
        public void GetFirstSymbolFromLine_when_line_is_whitespacesTPS_shoud_be_T()
        {
            //Array
            _testText = "  TPS";
            var actual = 'T';

            //Act
            var expected = _handler.GetFirstSymbolFromLine(_testText);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
