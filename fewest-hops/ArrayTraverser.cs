using System;
using System.Collections.Generic;
using System.Linq;

namespace fewest_hops
{
  public class ArrayTraverser
  {
    int[] _arr;
    Node[] _nodes;
    public ArrayTraverser(int[] arr)
    {
      _arr = arr;
    }

    public int GetMinHops()
    {
      _nodes = new Node[_arr.Length];
      for (int i = 0; i < _arr.Length; i++)
      {
        _nodes[i] = new Node(_arr[i], _nodes, i, true);
      }
      return _nodes[0].FindRouteToFinish(new List<int>());
    }
  }

  public class Node
  {
    public Node(int value, Node[] myArray, int nodeIndex, bool goLeftFirst)
    {
      this.Value = value;
      HopsToEndLeft = int.MaxValue;
      HopsToEndRight = int.MaxValue;
      MyArray = myArray;
      Index = nodeIndex;
      HasBeenTraversed = false;
      GoLeftFirst = goLeftFirst;
    }
    public int Value { get; }
    public int HopsToEndLeft { get; private set; }
    public int HopsToEndRight { get; private set; }
    public bool HasBeenTraversed { get; private set; }
    public Node[] MyArray { get; }
    public int Index { get; }
    public bool GoLeftFirst { get; }

    public int MinHopsToEnd {
      get {
        return Math.Min(HopsToEndLeft, HopsToEndRight);
      }
    }

    public int FindRouteToFinish(List<int> previousIndicesVisited)
    {
      if (HasBeenTraversed) {
        return MinHopsToEnd;
      }
      HasBeenTraversed = true;
      previousIndicesVisited.Add(Index); 
      
      //which direction first?
      if (GoLeftFirst) {
        HopsToEndLeft = TraverseNext(Index - Value, previousIndicesVisited);
        HopsToEndRight = TraverseNext(Index + Value, previousIndicesVisited);        
      } else {
        HopsToEndLeft = TraverseNext(Index - Value, previousIndicesVisited);
        HopsToEndRight = TraverseNext(Index + Value, previousIndicesVisited);
      }     
      
      return MinHopsToEnd;
    }

    private int TraverseNext(int index, List<int> previousIndicesVisited)
    {
      if (index < 0 || index >= MyArray.Length || previousIndicesVisited.Contains(index))
      {
        return int.MaxValue;
      }
      else if (MyArray[index].Value == 0)
      {
        return 1;//found the end of the road`\
      }
      else
      {
        int hopsToFinish = MyArray[index].FindRouteToFinish(previousIndicesVisited);
        return hopsToFinish == int.MaxValue ? int.MaxValue : 1 + hopsToFinish;
      }
    }
    
  }
}