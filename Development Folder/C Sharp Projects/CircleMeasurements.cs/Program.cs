using System;

class CircleMeasurements
{
    static void Main(string[] args)
    {
        Console.Write("Enter the radius of the circle in centimeters: ");
        double radius = Convert.ToDouble(Console.ReadLine());

        double pi = 3.14149;
        double diameter = 2 * radius;
        double circumference = 2 * pi * radius;
        double area = pi * radius * radius;

        Console.WriteLine($"Diameter: {diameter} cm");
        Console.WriteLine($"Circumference: {circumference} cm");
        Console.WriteLine($"Area: {area} square cm");
    }
}
