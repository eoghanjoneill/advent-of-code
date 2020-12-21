using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace travelling_salesman
{
  public class Salesman
  {
    public Salesman(string[] cityConnections)
    {
      Edges = new List<Edge>();
      Cities = new List<string>();
      ShortestRoute = int.MaxValue;
      ShortestRouteCities = new List<string>();
      foreach (string connection in cityConnections)
      {
        Match m = Regex.Match(connection, @"^([\w\s]+) to ([\w\s]+) = ([0-9]+)");
        if (m.Groups.Count > 0) {
          string c1 = m.Groups[1].Value;
          string c2 = m.Groups[2].Value;
          if (!Cities.Contains(c1)) { Cities.Add(c1); }
          if (!Cities.Contains(c2)) { Cities.Add(c2); }
          Edges.Add(new Edge(new string[2]{c1, c2}, int.Parse(m.Groups[3].Value)));
        }
      }
    }

    public int Solve()
    {      
      foreach(string startCity in Cities)
      {
        Route route = new Route();
        route.CitiesVisited.Add(startCity);
        SolveForRemainingCities(route);
      }      
      return this.ShortestRoute;
    }

    private void SolveForRemainingCities(Route route)
    {
      if (route.CitiesVisited.Count == Cities.Count)
      {
        if (route.Distance < this.ShortestRoute) 
        {
          this.ShortestRoute = route.Distance;
          this.ShortestRouteCities = route.CitiesVisited;
        }
      }
      else
      {
        //loop through options and solve
        //choose shortest first
        string lastCity = route.CitiesVisited.Last();
        foreach (Edge e in this.Edges.Where(e => e.ContainsCity(lastCity)))
        {
          var nextCity = e.GetOtherCity(lastCity);
          if (!route.CitiesVisited.Contains(nextCity))
          {
            Route newRouteBranch = route.Clone();
            newRouteBranch.CitiesVisited.Add(nextCity);
            newRouteBranch.Distance += e.Distance;
            this.SolveForRemainingCities(newRouteBranch);
          }
          
        }
      }
      
    }

    public List<Edge> Edges { get; set; }
    public List<string> Cities { get; set; }

    private int ShortestRoute;
    public List<string> ShortestRouteCities { get; set; }
  }

  public class Route
  {
    public Route()
    {
      this.CitiesVisited = new List<string>();
    }
    public List<string> CitiesVisited { get; set; }
    public int Distance { get; set; }
    public Route Clone()
    {
      var r = new Route();
      r.CitiesVisited = this.CitiesVisited.Select(s => s).ToList();
      r.Distance = this.Distance;
      return r;
    }
  
  }
  public class Edge
  {
    public int Distance { get; set; }
    public string[] Cities { get; set; }

    public Edge(string[] cities, int distance)
    {
      Distance = distance;
      Cities = cities;       
    }

    public bool ContainsCity(string c)
    {
      foreach (string city in Cities)
      {
        if (city == c)
        {
          return true;
        }
      }
      return false;
    }

    public string GetOtherCity(string c)
    {
      if (!this.Cities.Contains(c)) throw new System.Exception("Should contain " + c);
      return this.Cities.First(x => x != c);
    }
  }
}