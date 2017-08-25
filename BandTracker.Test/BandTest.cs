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
      //Arrange
      Band testBand = new Band("Rocky Chack");
      testBand.Save();

      //Act
      Band savedBand = Band.GetAll()[0];

      int result = savedBand.GetId();
      int testId = testBand.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
  }
}
