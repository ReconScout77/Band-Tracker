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
      //Arrange
      Venue testVenue = new Venue("Venue 4");
      testVenue.Save();

      //Act
      Venue savedVenue = Venue.GetAll()[0];

      int result = savedVenue.GetId();
      int testId = testVenue.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
  }
}
