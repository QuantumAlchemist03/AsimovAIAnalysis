using System;

namespace JourneyRules
{
    class JourneyMode
    {
        // Three-valued logic: a fact is either proven true, proven false,
        // or not yet known. UNKNOWN is what lets the forward-chaining loop
        // tell "not established yet" apart from "established as false".
        public const int UNKNOWN = -1, FALSE = 0, TRUE = 1;

        public static int av_speed;

        // Set by rules() whenever a rule fires; drives the chaining loop.
        public static bool done;

        public static int like_scenery = UNKNOWN, is_pilot = UNKNOWN, fly = UNKNOWN, drive = UNKNOWN,
            fly_airline = UNKNOWN, fly_a_Cessna = UNKNOWN, fly_a_Piper = UNKNOWN, motorbike = UNKNOWN, car = UNKNOWN;

        static void Main(string[] args)
        {
            int distance, time;

            Console.WriteLine("This is a program to help with travel planning.");

            distance = ReadPositiveInt("\nHow far are you going? (miles)");
            time = ReadPositiveInt("\nHow much time do you have for the trip? (hours):");

            av_speed = distance / time;
            Console.WriteLine("Average speed is " + av_speed + "mph");

            like_scenery = ReadYesNo("\nDo you prefer scenery over speed? (Y/N)");
            is_pilot = ReadYesNo("\nAre you a pilot? (Y/N)");

            // Forward chaining: keep re-applying the rule set until a full
            // pass fires nothing new and the fact base is stable.
            do
            {
                done = true;
                rules();
            } while (!done);

            // Default resolution: if flying is indicated but no specific
            // aircraft was selected, fall back to a commercial airline.
            if (fly == TRUE && fly_a_Cessna != TRUE && fly_a_Piper != TRUE)
                fly_airline = TRUE;

            if (fly_airline == TRUE)
                Console.WriteLine("\nFly using a commercial airline.\n");

            if (fly_a_Cessna == TRUE)
                Console.WriteLine("\nRent a Cessna and fly low.\n"); // High wing monoplane

            if (fly_a_Piper == TRUE)
                Console.WriteLine("\nRent a Piper and fly high.\n"); // Low wing monoplane

            if (motorbike == TRUE)
                Console.WriteLine("\nTake your motorbike and ride the country lanes.\n");

            if (car == TRUE)
                Console.WriteLine("\nNo option but to drive a car.\n");

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        public static void rules()
        {
            if (av_speed > 60 && fly == UNKNOWN)
            {
                fly = TRUE;
                done = false;
            }

            if (av_speed <= 60 && drive == UNKNOWN)
            {
                drive = TRUE;
                done = false;
            }

            if (fly == TRUE && is_pilot == TRUE && like_scenery == TRUE && av_speed < 100 && fly_a_Cessna == UNKNOWN)
            {
                fly_a_Cessna = TRUE;
                done = false;
            }

            if (fly == TRUE && is_pilot == TRUE && av_speed >= 100 && av_speed < 200 && fly_a_Piper == UNKNOWN)
            {
                fly_a_Piper = TRUE;
                done = false;
            }

            if (drive == TRUE && like_scenery == TRUE && motorbike == UNKNOWN)
            {
                motorbike = TRUE;
                done = false;
            }

            if (drive == TRUE && like_scenery != TRUE && car == UNKNOWN)
            {
                car = TRUE;
                done = false;
            }
        }

        // Reads an integer strictly greater than zero, re-prompting on bad
        // input. Guards the distance / time division.
        private static int ReadPositiveInt(string prompt)
        {
            int value;
            while (true)
            {
                Console.WriteLine(prompt);
                if (int.TryParse(Console.ReadLine(), out value) && value > 0)
                    return value;

                Console.WriteLine("Please enter a whole number greater than zero.");
            }
        }

        private static int ReadYesNo(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string answer = Console.ReadLine();

                if (answer != null)
                {
                    answer = answer.Trim().ToLower();
                    if (answer == "y" || answer == "yes") return TRUE;
                    if (answer == "n" || answer == "no") return FALSE;
                }

                Console.WriteLine("Please answer Y or N.");
            }
        }
    }
}
