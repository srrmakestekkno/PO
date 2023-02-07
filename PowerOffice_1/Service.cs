using PowerOffice_1.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PowerOffice_1
{
    public class Service : IService
    {
        private readonly IExternalApiProxy _proxy;
        private readonly IFileHandler _fileHandler;
        private readonly string _inputFilePath;
        private readonly string _outputFilePath;
        private readonly string _errorFilePath;

        private const string OutputFileHeader = "OrgNo;Navn;AntallAnsatte;Naeringskode;Organisasjonsform;brregNavn";

        public Service(IExternalApiProxy proxy, IFileHandler fileHandler, string inputFilePath, string outputFilePath, string errorFilePath)
        {
            _proxy = proxy;
            _fileHandler = fileHandler;
            _inputFilePath = inputFilePath;
            _outputFilePath = outputFilePath;
            _errorFilePath = errorFilePath;
        }
    
        public async Task UpdateFileWithData()
        {
            var lines = new List<string> { OutputFileHeader };

            IEnumerable<string> linesFromFile = GetAllLinesFromInputFile();

            foreach (string line in linesFromFile)
            {
                string[] csvLine = line.Split(';');
                string orgno = csvLine[0];
                string name = csvLine[1];

                Data? data = await _proxy.GetAsync(orgno);

                if (!data.IsOk)
                {
                    WriteToErrorFile($"{orgno} : {data.StatusCode}");
                    continue;
                }

                var brRegName = string.Empty;
                if (!string.IsNullOrEmpty(data?.BrrRegName) && !name.Equals(data?.BrrRegName, StringComparison.InvariantCultureIgnoreCase))
                {
                    brRegName = data.BrrRegName;
                }

                lines.Add($"{orgno};{name};{data?.NumberOfEmployees};{data?.Naeringskode?.Code};{data?.OrganizationForm?.Code};{brRegName}");                
            } // end foreach

            await WriteAllLinesToFileAsync(lines);
        }

        private IEnumerable<string> GetAllLinesFromInputFile()
        {
            return _fileHandler.ReadFromFile(_inputFilePath).Skip(1);
        }

        private void WriteToErrorFile(string text)
        {
            _fileHandler.AppendLineToFile(_errorFilePath, text);
        }

        private async Task WriteAllLinesToFileAsync(List<string> lines) 
        {
            await _fileHandler.WriteAllLinesToFileAsync(_outputFilePath, lines);
        }
    }
}
