using System;

namespace AmmoRule
{
    class Ammofuzzy
    {
        public static double low, high;
        public static int rockets, reload;

        static void Main(string[] args)
        {
            // Define initial ammo level and initial reload quantity
            rockets = 23;
            reload = 5;

            int rocketsfired;
            Random random = new Random();

            while (rockets > 0)
            {
                Console.WriteLine("Rockets remaining: " + rockets);

                // Call rules to determine degree of membership
                rules();

                // Defuzzify to determine reload amount
                reload = Defuzzify();

                Console.WriteLine("Reloading with: " + reload);
                rockets += reload;

                rocketsfired = random.Next(5, 35);
                if (rocketsfired > rockets)
                    rocketsfired = rockets;
                rockets -= rocketsfired;
                Console.WriteLine("Rockets fired: " + rocketsfired);

                System.Threading.Thread.Sleep(1000);
            }

            Console.WriteLine("Out of ammo!!");
            Console.ReadKey();
        }

        public static void rules()
        {
            if (rockets < 40) // Rule 1 (Low state)
            {
                low = 1.0 - (rockets / 40.0);
            }
            else
            {
                low = 0.0;
            }

            if (rockets > 10) // Rule 2 (High state)
            {
                high = (rockets - 10) / 30.0; // Adjusted to 30 since the slope change occurs at 10
            }
            else
            {
                high = 0.0;
            }

            // Ensure the membership degrees do not exceed 1
            low = Math.Min(low, 1.0);
            high = Math.Min(high, 1.0);
        }

        public static int Defuzzify()
        {
            // Assume some defuzzification strategy, for example, taking the weighted average
            double totalWeight = low + high;
            double reloadLow = low * 25.0; // If low is dominant, prefer a larger reload
            double reloadHigh = high * 5.0; // If high is dominant, prefer a smaller reload

            // Avoid division by zero
            if (totalWeight == 0) return 5;

            double weightedAverage = (reloadLow + reloadHigh) / totalWeight;
            return (int)Math.Round(weightedAverage);
        }
    }
}
