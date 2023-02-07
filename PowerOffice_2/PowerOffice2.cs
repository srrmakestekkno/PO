
using PowerOffice_2;

public static class PowerOffice2
{
    private const string Filename = "po-kunder-oppdatert.csv";
    public static async Task Main()
    {        
        try
        {
            var lines = File.ReadLines(CreatePath(Filename));

            if (lines.Count() < 2)
                return;

            Dictionary<string, int> dict = new();
            int totalEmployees = 0;

            foreach (var line in lines.Skip(1))
            {
                var splittedLine = line.Split(';');                
                string orgForm = splittedLine[4];
                var employees = int.Parse(splittedLine[2]);
                CollectUniqueOrgFormsWithTotalEmployees(orgForm, employees, dict);
                totalEmployees += employees;
            }

            var keys = new List<string>() { string.Empty };
            var emps = new List<string>() { "Ansatte" };
            var pros = new List<string>() { "Prosent" };

            foreach (var valuePair in dict)
            {
                string orgFrom = valuePair.Key;
                keys.Add(orgFrom);
                int numberOfEmployees = valuePair.Value;
                emps.Add(numberOfEmployees.ToString());

                var prosent = Calculator.CalculatePercentage(numberOfEmployees, totalEmployees);
                pros.Add(prosent.ToString());

            }
            ConsoleDataFormatter.FormatTable(keys);
            ConsoleDataFormatter.FormatTable(emps);
            ConsoleDataFormatter.FormatTable(pros);

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        
    }

    

    private static string CreatePath(string filename)
    {
        var path = Path.GetFullPath(Path.Combine(System.AppContext.BaseDirectory, @"..\..\..\..\"));
        return Path.Combine(path, filename);
    }

    private static void CollectUniqueOrgFormsWithTotalEmployees(string orgForm, int employees, Dictionary<string, int> dict)
    {

        if (!dict.ContainsKey(orgForm))
        {
            dict[orgForm] = employees;
        }
        else
        {
            int numberOfEmployees = dict[orgForm];
            numberOfEmployees += employees;
            dict[orgForm] = numberOfEmployees;
        }
    }

}
