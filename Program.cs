
using System.Diagnostics.Tracing;
using DataManagement;
using Microsoft.Data.Analysis;
using HttpRequest;

namespace Pianoio
{
    class FileMerger
    {
        static async Task MergeCsvFiles(string filePathA, string FilePathB)
        {
            DataManager dataManager = new DataManager();

            DataFrame csv1 = dataManager.ReadCsv(filePathA);
            DataFrame csv2 = dataManager.ReadCsv(FilePathB);

            var mergedFrame = dataManager.MergeDataFrame(csv1, csv2);

            var columns = mergedFrame.Columns;

            var user_idCol = columns.GetStringColumn("user_id_left");
            user_idCol.SetName("user_id");
            var emailCol = columns.GetStringColumn("email");
            var first_nameCol = columns.GetStringColumn("first_name");
            var last_nameCol = columns.GetStringColumn("last_name");

            for (int i = 0; i < emailCol.Length; i++)
            {
                PianoioSearch search = await Requests.GetSearch(emailCol[i]);
                if (search.users.Any())
                {
                    user_idCol[i] = search.users[0].uid;
                }
            }

            var mergedFrame2 = new DataFrame(user_idCol, emailCol, first_nameCol, last_nameCol);

            var resultPath = Path.GetFullPath(@"result.csv");

            dataManager.WriteCsv(mergedFrame2, resultPath);

            mergedFrame2.PrettyPrint();

       
        }
        static async Task Main()
        {
            
            var dataPathA = Path.GetFullPath(@"filea.csv");
            var dataPathB = Path.GetFullPath(@"fileb.csv");
            
            await MergeCsvFiles(dataPathA, dataPathB);
            
        }

    }


}