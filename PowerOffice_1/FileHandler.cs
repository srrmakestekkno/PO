namespace PowerOffice_1
{
    public class FileHandler : IFileHandler
    {
        public IEnumerable<string> ReadFromFile(string filepath)
        {
            var lines = File.ReadLines(filepath);   
            return lines;
        }

        public async Task WriteAllLinesToFileAsync(string filepath, List<string> lines)
        {            
            await File.WriteAllLinesAsync(filepath, lines);
        }

        public void AppendLineToFile(string filepath, string line)
        {
            if (!File.Exists(filepath))
            {
                using StreamWriter sw = File.CreateText(filepath);
                sw.WriteLine(line);
            }
            else
            {
                using StreamWriter sw = File.AppendText(filepath);
                sw.WriteLine(line);
            }
        }
    }
}
