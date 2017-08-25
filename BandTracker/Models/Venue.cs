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

    public string GetDetails()
    {
      return "ID: " + _id + " Venue Name: " + _name;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"TRUNCATE TABLE venues; TRUNCATE TABLE bands_venues;";

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(Object otherVenue)
    {
      if (!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquality = this._id == newVenue.GetId();
        bool nameEquality = this._name == newVenue.GetName();
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
      cmd.CommandText = @"INSERT INTO venues (name) VALUES (@name);";

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

    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM venues;";

      var rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Venue newVenue = new Venue(name, id);
        allVenues.Add(newVenue);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allVenues;
    }

    public static Venue Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM venues WHERE id = @thisId";

      MySqlParameter idParameter = new MySqlParameter();
      idParameter.ParameterName = "@thisId";
      idParameter.Value = id;
      cmd.Parameters.Add(idParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int venueId = 0;
      string name = "";

      while(rdr.Read())
      {
        venueId = rdr.GetInt32(0);
        name = rdr.GetString(1);
      }
      Venue foundVenue = new Venue(name, venueId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundVenue;
    }

    public void AddBand(Band newBand)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bands_venues (venue_id, band_id) VALUES (@venueId, @bandId);";

      MySqlParameter venueIdParameter = new MySqlParameter();
      venueIdParameter.ParameterName = "@venueId";
      venueIdParameter.Value = this._id;
      cmd.Parameters.Add(venueIdParameter);

      MySqlParameter bandIdParameter = new MySqlParameter();
      bandIdParameter.ParameterName = "@bandId";
      bandIdParameter.Value = newBand.GetId();
      cmd.Parameters.Add(bandIdParameter);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Band> GetBands()
    {
      List<Band> theseBands = new List<Band>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT bands.* FROM venues JOIN bands_venues ON(venues.id = bands_venues.venue_id) JOIN bands ON(bands_venues.band_id = bands.id) WHERE venues.id = @venueId;";

      MySqlParameter venueIdParameter= new MySqlParameter();
      venueIdParameter.ParameterName = "@venueId";
      venueIdParameter.Value = this._id;
      cmd.Parameters.Add(venueIdParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        Band newBand = new Band(bandName, bandId);
        theseBands.Add(newBand);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return theseBands;
    }


    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM venues WHERE id = @thisId; DELETE FROM bands_venues WHERE venue_id = @thisId;";

      MySqlParameter idParameter = new MySqlParameter();
      idParameter.ParameterName = "@thisId";
      idParameter.Value = _id;
      cmd.Parameters.Add(idParameter);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
