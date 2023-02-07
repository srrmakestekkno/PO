using PowerOffice_1;

namespace PowerOfficeTests
{
    [TestClass]
    public class FileHandlerTests
    {
        private readonly IFileHandler _filehandler;
        public FileHandlerTests()
        {
            _filehandler = new FileHandler();
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ReadFileFromPath_NoFileExist_ThrowsFileNotFoundException()
        {
            var path = CreatePath(@"nonExistingFile.csv", string.Empty);
            _filehandler.ReadFromFile(path);
        }

        [TestMethod]
        public void ReadFileFromPath_FileContainsRows()
        {
            var expectedCount = 2;
            var path = CreatePath(@"TestfileWithTwoRows.csv", @"..\..\..\");

            var lines = _filehandler.ReadFromFile(path);

            Assert.AreEqual(expectedCount, lines.Count());
        }

        [TestMethod]
        public void WriteAllLinesToFile_VerifyRowsWritten()
        {
            var expectedCount = 3;
            var lines = new List<string>
            {
                "OrgNo;Name", "1;Test1", "2;Test2"
            };

            var fileName = "output.csv";

            var path = CreatePath(fileName, @"..\..\..\..\");

            _filehandler.WriteAllLinesToFileAsync(path, lines);

            var linesFromFile = _filehandler.ReadFromFile(path);

            Assert.AreEqual(expectedCount, linesFromFile.Count());
        }

        [TestMethod]
        public void AppendTextToErrorLogFile_TextIsAppended()
        {
            var expectedCount = 2;
            var filename = "errorlogFromFileHandlerTest.txt";
            var path = CreatePath(filename, @"..\..\..\");
            var line1 = "1 : 410";
            var line2 = "2 : 404";

            // make file empty
            File.WriteAllText(path, string.Empty);

            _filehandler.AppendLineToFile(path, line1);
            _filehandler.AppendLineToFile(path, line2);

            var lines = _filehandler.ReadFromFile(path);

            Assert.AreEqual(expectedCount, lines.Count());
        }

        private static string CreatePath(string filename, string path2)
        {
            var path = Path.GetFullPath(Path.Combine(System.AppContext.BaseDirectory, path2));
            return Path.Combine(path, filename);
        }
    }
}