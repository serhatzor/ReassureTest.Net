﻿using System;
using ReassureTest.Net.AST;

namespace ReassureTest.Net
{
    /// <summary>
    /// Use this when testing ReassureTest.Net
    /// </summary>
    public class ReassureTestTester
    {
        public void Is(object actual, string expected, Action<string> print, Action</*expected*/object, /*actual*/object> assert)
        {
            IValue astActual = new ObjectVisitor().Visit(actual);

            IValue expectedAst = new DslParser(new DslTokenizer(Console.WriteLine
                                                             )).Parse(expected);

            if (expectedAst == null)
            {
                string graph = new AstPrinter().PrintRoot(astActual);
                print($"Actual is:\n{graph}");

                assert(graph, expected);
                return;
            }

            try
            {
                var executor = new MatchExecutor(assert);
                executor.MatchGraph(expectedAst as IAssertEvaluator, astActual);
            }
            catch (Exception)
            {
                string graph = new AstPrinter().PrintRoot(astActual);
                print($"Actual is:\n{graph}");
                throw;
            }
        }

        public void Is(object actual, string expected, Action</*expected*/object, /*actual*/object> assert) => Is(actual, expected, Console.WriteLine, assert);
    }
}
