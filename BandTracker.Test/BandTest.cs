using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using BandTracker.Models;

namespace BandTracker.Tests
{
  [TestClass]
  public class BandTest : IDisposable
  {
    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }

    public BandTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
    }

    [TestMethod]
    public void Equals_EqualsTrueForSameBandName_Band()
    {
      Band venue1 = new Band("Myth & Roid");
      Band venue2 = new Band("Myth & Roid");

      Assert.AreEqual(venue1, venue2);
    }

    [TestMethod]
    public void GetAll_BandsEmptyAtFirst_Int()
    {
      int result = Band.GetAll().Count;

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SavesBandToDatabase_BandList()
    {
      Band testBand = new Band("Ken Ashcorp");
      testBand.Save();

      List<Band> expected = new List<Band> {testBand};
      List<Band> result = Band.GetAll();

      CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_id()
    {
      Band testBand = new Band("Rocky Chack");
      testBand.Save();

      Band savedBand = Band.GetAll()[0];

      int result = savedBand.GetId();
      int testId = testBand.GetId();

      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsBandInDatabase_Band()
    {
      Band testBand = new Band("Sentimental Bus");
      testBand.Save();

      Band result = Band.Find(testBand.GetId());

      Assert.AreEqual(testBand, result);
    }

    [TestMethod]
    public void AddVenue_AddsVenueToBand_VenueList()
    {
      Band testBand = new Band("Sphere");
      testBand.Save();

      Venue testVenue = new Venue("Venue Again");
      testVenue.Save();

      Venue testVenue2 = new Venue("The Place To Be");
      testVenue2.Save();

      testBand.AddVenue(testVenue);
      testBand.AddVenue(testVenue2);

      List<Venue> result = testBand.GetVenues();
      List<Venue> expected = new List<Venue>{testVenue, testVenue2};

      CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void GetVenues_ReturnsAllVenuesForBand_VenueList()
    {
      Band testBand = new Band("World Order");
      testBand.Save();

      Venue testVenue1 = new Venue("Your City");
      testVenue1.Save();

      Venue testVenue2 = new Venue("My City");
      testVenue2.Save();

      testBand.AddVenue(testVenue1);
      List<Venue> result = testBand.GetVenues();
      List<Venue> expected = new List<Venue> {testVenue1};

      CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void Delete_DeletesBandFromDatabase_BandList()
    {
      Band testBand1 = new Band("Throwaway");
      testBand1.Save();

      Band testBand2 = new Band("Lost Prophets");
      testBand2.Save();

      testBand1.Delete();
      List<Band> resultBandList = Band.GetAll();
      List<Band> testBandList = new List<Band> {testBand2};

      CollectionAssert.AreEqual(testBandList, resultBandList);
    }

    [TestMethod]
    public void Update_UpdatesBandName_Band()
    {
      Band testBand = new Band("Machcco");
      testBand.Save();

      testBand.Update("Machico");
      Band expected = new Band("Machico", testBand.GetId());

      Band actual = Band.Find(testBand.GetId());

      Assert.AreEqual(expected, actual);
    }
  }
}
