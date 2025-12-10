using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinalAgenciaTours.WebApp.Models;

namespace AspnetCoreMvcStarter.Controllers;

public class Page2 : Controller
{
  public IActionResult Index() => View();
}
