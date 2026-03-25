namespace ConsoleApp1 {
    internal class Program {
        static void Main(string[] args) {
            DateOnly do1 = new DateOnly(2026, 3, 12);
            DateOnly do2 = new DateOnly(2026, 3, 15);

            DateTime dt1 = do1.ToDateTime(TimeOnly.MinValue);
            DateTime dt2 = do2.ToDateTime(TimeOnly.MinValue);

            TimeSpan ts1 = dt2 - dt1;
            Console.WriteLine(ts1.Days); // 3

            // Terug naar DateTime met FromDateTime...
            DateOnly otherDateOnly = DateOnly.FromDateTime(dt1);

            Factuur f1 = new Factuur() { Nummer = 123 };
            Factuur f2 = f1 with { Nummer = 234 };
        }
    }
    class Factuur {
        public int Nummer { get; set; }
    }
}
