using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MVC.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        const string SESSIONKEY = "favorites";

        int _userId;

        private readonly IHospitalService _hospitalService;

        public FavoritesController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        public IActionResult GetFavorites()
        {
            _userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);

            var favoritesList = GetSession(_userId);

            return View("Favorites", favoritesList);
        }

        public IActionResult AddToFavorites(int hospitalId)
        {
            _userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);

            var favoritesList = GetSession(_userId);

            var hospital = _hospitalService.Query().SingleOrDefault(h => h.Id == hospitalId);

            if (favoritesList.Any(f => f.HospitalId == hospitalId && f.UserId == _userId))
            {
                TempData["Message"] = $"\"{hospital.Name}\" already added to recorded.";
            }
            else
            {
                var favoritesItem = new FavoritesModel(hospitalId, _userId, hospital.Name);

                favoritesList.Add(favoritesItem);

                SetSession(favoritesList);

                TempData["Message"] = $"\"{hospital.Name}\" added to recorded.";
            }

            return RedirectToAction("Index", "Hospitals");
        }

        public IActionResult RemoveFromFavorites(int hospitalId, int userId)
        {
            _userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);

            var favoritesList = GetSession(_userId);

            favoritesList.RemoveAll(f => f.HospitalId == hospitalId && f.UserId == userId);

            SetSession(favoritesList);

            return RedirectToAction(nameof(GetFavorites));
        }

        public IActionResult ClearFavorites()
        {
            _userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);

            var favoritesList = GetSession(_userId);

            favoritesList.RemoveAll(f => f.UserId == _userId);

            SetSession(favoritesList);

            return RedirectToAction(nameof(GetFavorites));
        }

        private List<FavoritesModel> GetSession(int userId)
        {
            var favoritesList = new List<FavoritesModel>();

            var favoritesJson = HttpContext.Session.GetString(SESSIONKEY);

            if (!string.IsNullOrWhiteSpace(favoritesJson))
            {
                favoritesList = JsonConvert.DeserializeObject<List<FavoritesModel>>(favoritesJson);

                favoritesList = favoritesList.Where(f => f.UserId == userId).ToList();
            }

            return favoritesList;
        }

        private void SetSession(List<FavoritesModel> favoritesList)
        {
            var favoritesJson = JsonConvert.SerializeObject(favoritesList);

            HttpContext.Session.SetString(SESSIONKEY, favoritesJson);
        }
    }
}
