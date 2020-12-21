using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace letter_graph
{
    public class Graph
    {
      public Graph(string path)
      {
        _nodes = new Dictionary<char, Node>();
        _options = new List<Node>();
        foreach(string s in File.ReadLines(path))
        {
          ProcessLine(s);
        }        
      }
      
      private void ProcessLine(string s)
      {
        Match m = Regex.Match(s, @"Jump ([A-Z])");
        char prereqChar = (char) m.Groups[1].Value[0];
        Match m2 = Regex.Match(s, @"before jump ([A-Z])");
        char nodeChar = (char) m2.Groups[1].Value[0];
        if (!_nodes.ContainsKey(prereqChar))
        {
          _nodes.Add(prereqChar, new Node(prereqChar));
        }
        if (!_nodes.ContainsKey(nodeChar))
        {
          _nodes.Add(nodeChar, new Node(nodeChar));
        }
        _nodes[nodeChar].Prereqs.Add(_nodes[prereqChar]);        
      }

      public string Solve()
      {
        //find starting point
        sb = new StringBuilder();
        _options.AddRange(_nodes.Where(kv => kv.Value.Prereqs.Count == 0).Select(kv => kv.Value));
        
        while(_options.Count > 0)
        {
          Node bestChoice = _options.OrderBy(n => n.Value).First();
          CompleteNode(bestChoice);
        }
        return sb.ToString();
      }

      private void CompleteNode(Node node)
      {
        node.Completed = true;
        sb.Append(node.Value);
        if (_options.Contains(node))
        {
          _options.Remove(node);          
        }
        _nodes.Remove(node.Value);
        IEnumerable<Node> descendents = _nodes.Where(kv => kv.Value.Prereqs.Any(pr => pr.Value == node.Value))
                                              .Select(kv => kv.Value);
        foreach (Node d in descendents)
        {
          if (d.Prereqs.All(pr => pr.Completed == true))
          {
            _options.Add(d);
          }
        }        
      }

      private StringBuilder sb;
      private List<Node> _options;
      private Dictionary<char, Node> _nodes;
    }

    class Node
    {
      public Node(char c)
      {
        this.Value = c;
        this.Prereqs = new List<Node>();
        Available = false;
        Completed = false;
      }

      public char Value { get; }
      public List<Node> Prereqs { get; set; }
      public bool Available { get; set; }
      public bool Completed { get; set; }
    }
}
