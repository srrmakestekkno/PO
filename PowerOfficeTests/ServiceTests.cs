using Moq;
using PowerOffice_1;
using PowerOffice_1.DataObjects;

namespace PowerOfficeTests
{
    [TestClass]
    public class ServiceTests
    {
        private readonly Mock<IExternalApiProxy> _proxy;
        private readonly IService _service;
        private readonly IFileHandler _fileHandler;
        private readonly string _inputFilePath = CreatePath("test-kunder.csv");
        private readonly string _errorLogFilePath = CreatePath("errorlog-from-service-test.txt");
        private readonly string _outputFilePath = CreatePath("test-output.csv");

        public ServiceTests()
        {
            _proxy = new Mock<IExternalApiProxy>();
            _service = new Service(_proxy.Object, new FileHandler(), _inputFilePath, _outputFilePath, _errorLogFilePath);
            _fileHandler = new FileHandler();
            
        }

        [TestMethod]
        public async Task UpdateFileWithLine_AddedNewBrRegName()
        {
            var orgno = "810463902";
            var expectedNewName = "NyttNavn";

            _proxy.Setup(x => x.GetAsync(orgno))
                .ReturnsAsync(new Data { BrrRegName = expectedNewName });

            await _service.UpdateFileWithData();

            var lines = _fileHandler.ReadFromFile(_outputFilePath).ToList();

            var splittedLine = lines.Last().Split(';');

            Assert.IsTrue(splittedLine.Contains(expectedNewName));    
        }

        [TestMethod]
        public async Task UpdateFileWithLine_WritesToErrorLogFile()
        {
            var statusCode = 410;
            File.WriteAllText(_errorLogFilePath, string.Empty); // clear the file
            var expectedLineCount = 1;            
            var orgno = "810463902";
            var expectedLine = $"{orgno} : {statusCode}";
            _proxy.Setup(x => x.GetAsync(orgno))
                .ReturnsAsync(new Data { StatusCode = 410, OrganizationNumber = orgno, IsOk = false });

            await _service.UpdateFileWithData();

            var lineFromErrorFile = _fileHandler.ReadFromFile(_errorLogFilePath).ToList();
            Assert.IsNotNull(lineFromErrorFile);
            Assert.AreEqual(expectedLineCount, lineFromErrorFile.Count);
            Assert.AreEqual(expectedLine, lineFromErrorFile.First());
        }

        private static string CreatePath(string filename)
        {
            var path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
            return Path.Combine(path, filename);
        }
    }
}
