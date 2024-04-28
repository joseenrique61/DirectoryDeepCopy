using System.Collections.Generic;
using System.IO;

namespace DirectoryDeepCopy
{
    public static class DirectoryDeepCopy
    {
        public static void DeepCopy(string sourceDirectory, string destinationParentDirectory, List<string> omittedFolders, bool overwrite)
        {
            CopyFolder(sourceDirectory, destinationParentDirectory, omittedFolders, overwrite);
            CopyFiles(sourceDirectory, destinationParentDirectory, overwrite);
        }

        private static void CopyFolder(string sourceDirectory, string destinationDirectory, List<string> omittedFolders, bool overwrite)
        {
            foreach (string folderPath in Directory.GetDirectories(sourceDirectory))
            {
                string folderName = Path.GetFileName(folderPath.TrimEnd(Path.DirectorySeparatorChar));

                string newFolderPath = Path.Combine(destinationDirectory, folderName);

                if (omittedFolders.Contains(folderName))
                {
                    return;
                }

                CreateNewFolder(newFolderPath, overwrite);
                CopyFolder(folderPath, newFolderPath, omittedFolders, overwrite);
                CopyFiles(folderPath, newFolderPath, overwrite);
            }
        }

        private static void CreateNewFolder(string newFolderPath, bool overwrite)
        {
            if (!Directory.Exists(newFolderPath))
            {
                Directory.CreateDirectory(newFolderPath);
            }
            else if (overwrite)
            {
                Directory.Delete(newFolderPath, true);
                Directory.CreateDirectory(newFolderPath);
            }
        }

        private static void CopyFiles(string sourceDirectory, string destinationDirectory, bool overwrite)
        {
            foreach (string filePath in Directory.GetFiles(sourceDirectory))
            {
                string newFilePath = Path.Combine(destinationDirectory, Path.GetFileName(filePath));

                File.Copy(filePath, newFilePath, overwrite);
            }
        }
    }
}
