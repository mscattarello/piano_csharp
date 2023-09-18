using System.Data;
using Microsoft.Data.Analysis;


namespace DataManagement
{
    class  DataManager
    {
        public DataFrame ReadCsv(string filePath)
        {
            var dataFrame = DataFrame.LoadCsv(filePath);

            return dataFrame;
        }

        public DataFrame MergeDataFrame(DataFrame df1, DataFrame df2)
        {
            var mergedFrame = df1.Merge<string>(df2, "user_id", "user_id");
            return mergedFrame;
        }

        public void WriteCsv( DataFrame df, string path)
        {
            string[,] dfArray = new string[df.Columns.Count, df.Rows.Count+1];
            for (int colIdx = 0; colIdx < df.Columns.Count; colIdx++)
            {
                dfArray[colIdx,0] = df.Columns[colIdx].Name;
                for (int rowIdx = 0; rowIdx < df.Rows.Count; rowIdx++)
                {
                    dfArray[colIdx,rowIdx+1] = df.Rows[rowIdx].ToString().Split(" ")[colIdx];
                };
            };
            using ( StreamWriter outputFile = new StreamWriter(path))
            {
                for (int rowIdx = 0; rowIdx < df.Rows.Count; rowIdx++)
                {
                    string newLine = "";
                    for (int colIdx = 0; colIdx < df.Columns.Count; colIdx++)
                    {
                        if (newLine != "")
                        {
                            newLine = newLine + "," + dfArray[colIdx,rowIdx];
                        } else
                        {
                            newLine = dfArray[colIdx,rowIdx];
                        }
                    };
                    outputFile.WriteLine(newLine);
                };
            }
        }
    }


    
}