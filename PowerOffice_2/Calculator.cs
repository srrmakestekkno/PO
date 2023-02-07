namespace PowerOffice_2
{
    public static class Calculator
    {
        public static decimal CalculatePercentage(int numberOfEmployees, int totalNumberOfEmployees)
        {


            return Math.Round(((decimal)numberOfEmployees / totalNumberOfEmployees) * 100, 2, MidpointRounding.ToZero);
        }
    }
}
