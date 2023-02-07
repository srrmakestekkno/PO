namespace PowerOffice_1
{
    public interface IFileHandler
    {
        Task WriteAllLinesToFileAsync(string filepath, List<string> lines);
        IEnumerable<string> ReadFromFile(string filepath);

        public void AppendLineToFile(string filepath, string line);
    }
}
