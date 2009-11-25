using System;

namespace FSMGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            FiniteStateMachine.FiniteStateMachine.Compile(System.Console.In).GenerateCode(args[0]);
        }
    }
}
