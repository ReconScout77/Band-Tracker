using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using BandTracker.Models;

namespace BandTracker.Tests
{
  [TestClass]
  public class VenueTest : IDisposable
  {
    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }

    public VenueTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
    }

    [TestMethod]
    public void Equals_EqualsTrueForSameVenueName_Venue()
    {
      Venue venue1 = new Venue("Hip House");
      Venue venue2 = new Venue("Hip House");

      Assert.AreEqual(venue1, venue2);
    }

    [TestMethod]
    public void GetAll_VenuesEmptyAtFirst_Int()
    {
      int result = Venue.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SavesVenueToDatabase_VenueList()
    {
      Venue testVenue = new Venue("Concert Center");
      testVenue.Save();

      List<Venue> expected = new List<Venue> {testVenue};
      List<Venue> result = Venue.GetAll();

      CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_id()
    {
      Venue testVenue = new Venue("Venue 4");
      testVenue.Save();

      Venue savedVenue = Venue.GetAll()[0];

      int result = savedVenue.GetId();
      int testId = testVenue.GetId();

      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsVenueInDatabase_Venue()
    {
      Venue testVenue = new Venue("My House");
      testVenue.Save();

      Venue result = Venue.Find(testVenue.GetId());

      Assert.AreEqual(testVenue, result);
    }

    [TestMethod]
    public void AddBand_AddsBandToVenue_BandList()
    {
      Venue testVenue = new Venue("BandCon");
      testVenue.Save();

      Band testBand = new Band("Kalafina");
      testBand.Save();

      Band testBand2 = new Band("Fhana");
      testBand2.Save();

      testVenue.AddBand(testBand);
      testVenue.AddBand(testBand2);

      List<Band> result = testVenue.GetBands();
      List<Band> expected = new List<Band>{testBand, testBand2};

      CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void GetBands_ReturnsAllBandsForVenue_BandList()
    {
      Venue testVenue = new Venue("Hello Arena");
      testVenue.Save();

      Band testBand1 = new Band("Owl City");
      testBand1.Save();

      Band testBand2 = new Band("BOOM BOOM Satellites");
      testBand2.Save();

      testVenue.AddBand(testBand1);
      List<Band> result = testVenue.GetBands();
      List<Band> expected = new List<Band> {testBand1};

      CollectionAssert.AreEqual(expected, result);
    }
  }
}
