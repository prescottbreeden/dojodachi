using System;

namespace dojodachi_project
{
    public class MyDojodachi
    {
        public int happiness { get; set; }
        public int fullness { get; set; }
        public int energy { get; set; }
        public int meals { get; set; }
        
        public MyDojodachi()
        {
            //base constructor
            this.happiness = 20;
            this.fullness = 20;
            this.energy = 50;
            this.meals = 3;
        }
        public MyDojodachi(int happiness, int fullness, int energy, int meals)
        {
            this.happiness = happiness;
            this.fullness = fullness;
            this.energy = energy;
            this.meals = meals;
        }
        public MyDojodachi Feed()
        {
            if (this.meals < 1)
                System.Console.WriteLine("Need more meals to feed Dojodachi");
            else 
            {
                // 25% chance dojodachi_project doesn't want to eat (meal is lost)
                Random rand = new Random();
                if (rand.Next(1,5)==1)
                    System.Console.WriteLine("Dojodachi doesn't want to eat - meal is lost");
                else
                {
                    // Costs 1 meal 
                    this.meals -= 1;

                    // gains a random amount of fullness between 5 and 10
                    Random randAmount = new Random();
                    this.fullness += randAmount.Next(5,10);
                    System.Console.WriteLine($"Dojodachi gained {randAmount} fullness!");
                    System.Console.WriteLine("Meals: " + this.meals);
                    System.Console.WriteLine("Fullness: " + this.fullness);
                }
            }
            return this;
        }
        public void Play()
        {
            // Playing with your Dojodachi costs 5 energy
            // gains a random amount of happiness between 5 and 10
            // 25% chance dojodachi_project doesn't want to play (energy is lost)
        }
        public void Work()
        {
            // costs 5 energy
            // gain 1 - 3   
        }
        public void Sleep()
        {
            // gain 15 energy
            // descrease fullness by 5
            // decrease happiness by 5
        }
    }
}
