using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace File_ChangeName
{
    //參考 https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/file-system/how-to-copy-delete-and-move-files-and-folders
    internal class ChangeNameHelper
    {
        public string SourcePath  { get; set; }
        public string DestPath { get;set;}
        public string DestFilePrefix { get;set;}


        public void MoveFiles()
        {
            // To copy a folder's contents to a new location:
            // Create a new target folder.
            // If the directory already exists, this method does not create a new directory.
            Directory.CreateDirectory(DestPath);

            // To copy all the files in one directory to another directory.
            // Get the files in the source folder. (To recursively iterate through
            // all subfolders under the current directory, see
            // "How to: Iterate Through a Directory Tree.")
            // Note: Check for target path was performed previously
            //       in this code example.
            if (Directory.Exists(SourcePath))
            {
                var di = new DirectoryInfo(SourcePath);
                FileInfo[] fileInfos = di.GetFiles("*.*");
                Array.Sort(fileInfos, delegate (FileInfo x, FileInfo y) { return x.CreationTime.CompareTo(y.CreationTime); });

                int i = 1;
                // Copy the files and overwrite destination files if they already exist.
                foreach (var file in fileInfos)
                {
                    string fileName = file.Name;
                    string sourceFile = Path.Combine(SourcePath, fileName);
                    string destFile = Path.Combine(DestPath, $"{DestFilePrefix}_{i++}.json");

                    // To copy a file to another location and
                    // overwrite the destination file if it already exists.
                    System.IO.File.Copy(sourceFile, destFile, true);

                    // To move a file or folder to a new location:
                    //System.IO.File.Move(sourceFile, destFile);


                    //File.Delete(@"C:\Users\Public\DeleteTest\test.txt");
                    //Directory.Delete(sourceFile);
                }
            }
            else
            {
                Console.WriteLine("Source path does not exist!");
            }
        }
    }
}
