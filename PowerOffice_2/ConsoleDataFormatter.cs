namespace PowerOffice_2
{
    public static class ConsoleDataFormatter
    {
        private const int TableWidth = 100;

        public static void PrintSeperatorLine()
        {
            Console.WriteLine(new String('-', TableWidth));
        }

        public static void PrintRow2(List<string> columns)
        {

            var columnsLengh = columns.Count;
            int colWidth = (TableWidth - columnsLengh) / columnsLengh;

            const string seed = "|";

            string row = columns.Aggregate(seed, (seperator, colText) => seperator + GetCenterAllignedText(colText, colWidth) + seed);

            Console.WriteLine(row);
        }

        public static void PrintRow(params string[] columns)
        {

            var columnsLengh = columns.Length;
            int colWidth = (TableWidth - columnsLengh) / columnsLengh;

            const string seed = "|";

            string row = columns.Aggregate(seed, (seperator, colText) => seperator + GetCenterAllignedText(colText, colWidth) + seed);

            Console.WriteLine(row);
        }

        public static void FormatTable(List<string> data)
        {
            PrintSeperatorLine();
            PrintRow2(data);
        }

        private static string GetCenterAllignedText(string colText, int colWidth)
        {
            colText = colText.Length > colWidth ? colText.Substring(0, colWidth - 3) + "..." : colText;
            return string.IsNullOrEmpty(colText)
                ? new string(' ', colWidth)
                : colText.PadRight(colWidth - ((colWidth - colText.Length) / 2)).PadLeft(colWidth);
        }
    }
}
