using PowerOffice_1;

public static class PowerOffice1
{
    public static async Task Main()
    {
        var inputFilename = "po-kunder.csv";
        var outputFilename = "po-kunder-oppdatert.csv";
        var errorLogFileName = "po-kunder-error-log.txt";
        var inputFilePath = CreatePath(inputFilename);
        var outputFilePath = CreatePath(outputFilename);
        var errorLogFilePath = CreatePath(errorLogFileName); 

        IService service = new Service(
            new ExternalApiProxy(), 
            new FileHandler(), 
            inputFilePath, 
            outputFilePath, 
            errorLogFilePath);

        try
        {
            //Thread t1 = new Thread(() => service.UpdateFileWithData());
            await service.UpdateFileWithData();

        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            // exit
        }

    }

    private static string CreatePath(string filename)
    {
        var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\"));
        return Path.Combine(path, filename);
    }
}



