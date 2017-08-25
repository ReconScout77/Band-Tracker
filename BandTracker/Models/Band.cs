using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace BandTracker.Models
{
  public class Band
  {
    private int _id;
    private string _name;

    public Band(string name, int id=0)
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

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"TRUNCATE TABLE bands;";

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(Object otherBand)
    {
      if (!(otherBand is Band))
      {
        return false;
      }
      else
      {
        Band newBand = (Band) otherBand;
        bool idEquality = this._id == newBand.GetId();
        bool nameEquality = this._name == newBand.GetName();
        return (idEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bands(name) VALUES(@name);";

      MySqlParameter nameParameter = new MySqlParameter();
      nameParameter.ParameterName = "@name";
      nameParameter.Value = _name;
      cmd.Parameters.Add(nameParameter);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Band> GetAll()
    {
      List<Band> allBands = new List<Band>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM bands;";

      var rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Band newBand = new Band(name, id);
        allBands.Add(newBand);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allBands;
    }

    public static Band Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM bands WHERE id = @thisId";

      MySqlParameter idParameter = new MySqlParameter();
      idParameter.ParameterName = "@thisId";
      idParameter.Value = id;
      cmd.Parameters.Add(idParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int bandId = 0;
      string name = "";

      while(rdr.Read())
      {
        bandId = rdr.GetInt32(0);
        name = rdr.GetString(1);
      }
      Band foundBand = new Band(name, bandId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundBand;
    }
  }
}
