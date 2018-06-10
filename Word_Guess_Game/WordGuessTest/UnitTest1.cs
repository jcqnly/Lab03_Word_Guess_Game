using System;
using Xunit;
using Word_Guess_Game;

namespace WordGuessTest
{
    public class UnitTest1
    {
        [Fact]
        public void CanCreateFile()
        {
            Assert.True(Program.CreateFile());
        }

        [Fact]
        public void CanReadFile()
        {
            Assert.True(Program.ReadFile());
        }

        [Theory]
        [InlineData("zoidberg", "File updated!")]
        public void CanUpdateFileWithUserInput(string userInput, string expectedResult)
        {
            Assert.Equal("File updated!", Program.UpdateFile("zoidberg"));
        }

        [Fact]
        public void CanDeleteFile()
        {
            Assert.True(Program.DeleteFile());
        }
    }
}

