using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueMagic.SudokuGenerator.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var generator = new Generator();
            var startGenerating = DateTime.Now;
            var sudoku = generator.Generate(3, Level.VeryHard);
            var startSolving = DateTime.Now;
            var result = generator.SolveSolution(sudoku);
            var finished = DateTime.Now;
            Console.WriteLine("Result: " + result);
            if (result)
            {
                Console.WriteLine("Generating: " + (startSolving - startGenerating));
                Console.WriteLine("Solving: " + (finished - startSolving));
            }
        }
    }
}
