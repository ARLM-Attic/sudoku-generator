using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TrueMagic.SudokuGenerator.Test
{
    [TestClass]
    public class GeneratorTest
    {
        [TestMethod]
        public void Test()
        {
            var generator = new Generator();
            var sudoku = generator.Generate(3, Level.VeryHard);
            var solved = generator.SolveSolution(sudoku);
            Assert.IsTrue(solved);
        }
    }
}
