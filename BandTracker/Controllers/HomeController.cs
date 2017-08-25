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

    [HttpGet("/bandForm")]
    public ActionResult BandForm()
    {
      return View();
    }

    [HttpPost("/bandForm/new")]
    public ActionResult BandAdded()
    {
      Band newBand = new Band(Request.Form("name"));
      newBand.Save();
      List<Band> allBands = Band.GetAll();
      return View("AllBands", allBands);
    }

    [HttpGet("/venueForm")]
    public ActionResult VenueForm()
    {
      return View();
    }
  }
}
