namespace PowerOffice_2
{
    public static class Calculator
    {
        public static decimal CalculatePercentage(int numberOfEmployees, int totalNumberOfEmployees)
        {
            if (numberOfEmployees == 0 || totalNumberOfEmployees == 0)
                return 0;

            return Math.Round(((decimal)numberOfEmployees / totalNumberOfEmployees) * 100, 2, MidpointRounding.ToZero);
        }
    }
}
