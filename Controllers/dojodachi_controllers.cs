using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using System.Collections.Generic;
// using dojodachi_project;
using System;

namespace dojodachi_project.Controllers
{
    public class dojodachi_project : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            int? happiness = HttpContext.Session.GetInt32("happiness");
            if (happiness == null)
            {
                var dojodachi = new MyDojodachi();
                HttpContext.Session.SetInt32("happiness", dojodachi.happiness);
                HttpContext.Session.SetInt32("fullness", dojodachi.fullness);
                HttpContext.Session.SetInt32("energy", dojodachi.energy);
                HttpContext.Session.SetInt32("meals", dojodachi.meals);
                HttpContext.Session.SetString("lastAction", "Select an option for your Dojodachi!");
            }
            happiness = HttpContext.Session.GetInt32("happiness");
            int? fullness = HttpContext.Session.GetInt32("fullness");
            int? energy = HttpContext.Session.GetInt32("energy");
            int? meals = HttpContext.Session.GetInt32("meals");
            if(happiness >= 100 && energy >= 100 && fullness >= 100)
                HttpContext.Session.SetString("lastAction", "You Won!!! Dojodachi is the fullest, happiest, energetic pet in the world!!");
            string lastAction = HttpContext.Session.GetString("lastAction");

            ViewBag.happiness = happiness;
            ViewBag.fullness = fullness;
            ViewBag.energy = energy;
            ViewBag.meals = meals;
            ViewBag.lastAction = lastAction;

            return View("index");
        }

        [HttpPost]
        [Route("feed")]
        public IActionResult Feed()
        {
            int? fullness = HttpContext.Session.GetInt32("fullness");
            int? meals = HttpContext.Session.GetInt32("meals");
            string result = string.Empty;

            if (meals < 1)
                result = $"Not enough meals to feed Dojodachi! Current meals: {meals}";
            else 
            {
                // 25% chance dojodachi_project doesn't want to eat (meal is lost)
                Random rand = new Random();
                if (rand.Next(1,5)==1)
                {
                    meals -= 1;
                    result = $"Dojodachi doesn't want to eat (sad face!) ... Current meals: {meals}";
                }
                else
                {
                    meals -= 1;

                    // gains a random amount of fullness between 5 and 10
                    Random randAmount = new Random();
                    fullness += randAmount.Next(5,10);
                    result = $"Dojodachi devoured the meal in one bite and now has {fullness} fullness and {meals} meals.";
                }
            }
            
            HttpContext.Session.SetInt32("fullness", (int)fullness);
            HttpContext.Session.SetInt32("meals", (int)meals);
            HttpContext.Session.SetString($"lastAction", result);
 
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("play")]
        public IActionResult Play()
        {
            int? happiness = HttpContext.Session.GetInt32("happiness");
            int? energy = HttpContext.Session.GetInt32("energy");
            string result = string.Empty;
            
             if (energy < 5)
                result = $"Not enough energy to play with Dojodachi! Current energy: {energy}";
            else 
            {
                // 25% chance dojodachi_project doesn't want to eat (meal is lost)
                Random rand = new Random();
                if (rand.Next(1,5)==1)
                    result = $"Dojodachi doesn't want to play (sad face!)... Current energy: {energy}";
                else
                {
                    // Playing with your Dojodachi costs 5 energy
                    energy -= 5;

                    // gains a random amount of happiness between 5 and 10
                    Random randAmount = new Random();
                    happiness += randAmount.Next(5,10);
                    result = $"Dojodachi romped around like a puppy in a ballpit and now has {happiness} happiness and {energy} energy.";
                }
            }

            HttpContext.Session.SetInt32("happiness", (int)happiness);
            HttpContext.Session.SetInt32("energy", (int)energy);
            HttpContext.Session.SetString($"lastAction", result);
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("work")]
        public IActionResult Work()
        {
            int? energy = HttpContext.Session.GetInt32("energy");
            int? meals = HttpContext.Session.GetInt32("meals");
            string result = string.Empty;
            
            if (energy < 5)
                result = $"Not enough energy for Dojodachi to work!  Current energy: {energy}";
            else
            {
                energy -=5;
                Random rand = new Random();
                meals += rand.Next(1,4);
                result = $"Dojodachi questions your use of slave-labor ... Dojodachi now has {meals} meals and {energy} energy.";
            }
            
            HttpContext.Session.SetInt32("energy", (int)energy);
            HttpContext.Session.SetInt32("meals", (int)meals);
            HttpContext.Session.SetString($"lastAction", result);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("sleep")]
        public IActionResult Sleep()
        {
            int? happiness = HttpContext.Session.GetInt32("happiness");
            int? fullness = HttpContext.Session.GetInt32("fullness");
            int? energy = HttpContext.Session.GetInt32("energy");
            string result = string.Empty;

            if (fullness < 5 || happiness < 5)
                result = $"Dojodachi is kinda depressed... maybe feed him or play with him?  Current energy: {energy}, Current happiness: {happiness}";
            else
            {
                energy += 15;
                fullness -= 5;
                happiness -= 5;
                result = $"Dojodachi is rested! Current energy: {energy}; Current fullness: {fullness}; Current happiness: {happiness}";
            }

            HttpContext.Session.SetInt32("happiness", (int)happiness);
            HttpContext.Session.SetInt32("fullness", (int)fullness);
            HttpContext.Session.SetInt32("energy", (int)energy);
            HttpContext.Session.SetString($"lastAction", result);
            
            return RedirectToAction("Index");
        }
    }
    
}