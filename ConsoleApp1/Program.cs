namespace ConsoleApp1 {
    internal class Program {
            internal static void Main(string[] args) {
            // programma dat op basis van ingevoerde gewicht
            // en lengte van gebruike zijn bmi berekent

            Console.WriteLine("Geef je gewicht in kg: ");
            string gewichtInKgInput = Console.ReadLine();
            Console.WriteLine("Geef je lengte in cm: ");
            string lengteInCmInput = Console.RReadLine();

            double bmi = BerekenBMI(double.Parse(gewichtInKgInput), double.Parse(lengteInCmInput));
            Console.WriteLine(bmi);
        }
        static void PrintBmi(double bmi) {
            // BMI-categorieën volgens de WHO
        }
        /// <summary>
        /// Calculates the Body Mass Index (BMI) based on the provided weight in kilograms and height in centimeters.
        /// </summary>
        /// <param name="gewichtInKg">The weight of the user in kilograms. Must be a positive value.</param>
        /// <param name="lengteInCm">The height of the user in centimeters. Must be a positive value.</param>
        /// <returns>The calculated BMI as a <see cref="double"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="gewichtInKg"/> or <paramref name="lengteInCm"/> is less than or equal to zero.
        /// </exception>
        static double BerekenBMI(double gewichtInKg, double lengteInCm) {
            if (gewichtInKg <= 0)
                throw new ArgumentOutOfRangeException(nameof(gewichtInKg), "Gewicht moet positief zijn.");
            if (lengteInCm <= 0)
                throw new ArgumentOutOfRangeException(nameof(lengteInCm), "Lengte moet positief zijn.");
            double lengte = lengteInCm / 100; // omzetten naar meters

            double bmi = gewichtInKg / (lengte * lengte);
            return bmi;
        }
    }
}
