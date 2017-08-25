using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace BandTracker.Models
{
  public class Venue
  {
    private int _id;
    private string _name;

    public Venue(string name, int id=0)
    {
      _id= id;
      _name = name;
    }
    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }
  }
}
