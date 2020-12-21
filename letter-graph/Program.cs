using System;

namespace letter_graph
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph g = new Graph("C:\\dev\\advent\\letter-graph\\example.txt");
            Console.WriteLine(g.Solve());

            Graph q = new Graph("C:\\dev\\advent\\letter-graph\\real-quiz.txt");
            Console.WriteLine(q.Solve());
        }
    }
}
