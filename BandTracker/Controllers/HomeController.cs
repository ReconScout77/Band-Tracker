using Microsoft.AspNetCore.Mvc;
using BandTracker.Models;
using System.Collections.Generic;
using System;

namespace BandTracker.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet("/bands")]
    public ActionResult AllBands()
    {
      List<Band> allBands = Band.GetAll();
      return View(allBands);
    }

    [HttpGet("/bands/bandForm")]
    public ActionResult BandForm()
    {
      return View();
    }

    [HttpPost("/bands/bandForm/new")]
    public ActionResult BandAdded()
    {
      Band newBand = new Band(Request.Form["name"]);
      newBand.Save();
      return RedirectToAction("AllBands");
    }

    [HttpGet("/bands/{id}")]
    public ActionResult BandDetails(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>{};
      Band thisBand = Band.Find(id);
      model.Add("band", thisBand);
      model.Add("venues", thisBand.GetVenues());
      return View(model);
    }

    [HttpGet("/venues")]
    public ActionResult AllVenues()
    {
      List<Venue> allVenues = Venue.GetAll();
      return View(allVenues);
    }

    [HttpGet("/venues/venueForm")]
    public ActionResult VenueForm()
    {
      return View();
    }

    [HttpPost("/venues/venueForm/new")]
    public ActionResult VenueAdded()
    {
      Venue newVenue = new Venue(Request.Form["name"]);
      newVenue.Save();
      return RedirectToAction("AllVenues");
    }

    [HttpGet("/venues/{id}")]
    public ActionResult VenueDetails(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>{};
      Venue thisVenue = Venue.Find(id);
      model.Add("venue", thisVenue);
      model.Add("bands", thisVenue.GetBands());
      return View(model);
    }

    [HttpGet("/venues/{id}/updateForm")]
    public ActionResult VenueUpdateForm(int id)
    {
      Venue thisVenue = Venue.Find(id);
      return View(thisVenue);
    }

    [HttpPost("/venues/{id}/updateForm/updated")]
    public ActionResult UpdateVenue(int id)
    {
      Venue thisVenue = Venue.Find(id);
      thisVenue.Update(Request.Form["updateName"]);
      return RedirectToAction("VenueDetails");
    }

    [HttpGet("/venues/{id}/deleted")]
    public ActionResult DeleteVenue(int id)
    {
      Venue.Find(id).Delete();
      return RedirectToAction("AllVenues");
    }
  }
}
