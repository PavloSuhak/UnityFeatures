using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Tests
{
    public class RemoveBracketsFromFileName : MonoBehaviour
    {
        [FolderPath] public string targetDirectory = "Assets/Your/Target/Directory";

        [Button]
        private void RemoveBrackets()
        {
            // Викликаємо метод для рекурсивного переіменування файлів
            RenameFilesInDirectory(targetDirectory);

            // Повідомлення про завершення операції
            Debug.Log("Операцію завершено!");
        }

        // Метод для рекурсивного переіменування файлів у директорії та її піддиректоріях
        private void RenameFilesInDirectory(string directoryPath)
        {
            // Отримуємо список всіх файлів у директорії
            string[] files = Directory.GetFiles(directoryPath);

            // Перебираємо кожен файл
            foreach (string filePath in files)
            {
                // Переіменовуємо файл, видаляючи дужки
                RenameFile(filePath);
            }

            // Отримуємо список всіх піддиректорій
            string[] subdirectories = Directory.GetDirectories(directoryPath);

            // Рекурсивно викликаємо цей метод для кожної піддиректорії
            foreach (string subdirectory in subdirectories)
            {
                RenameFilesInDirectory(subdirectory);
            }
        }

        // Метод для переіменування файлу, видаляючи дужки з назви
        private void RenameFile(string filePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string newFileName = fileName.Replace("[", "").Replace("]", "");

            // Якщо назва файлу була змінена
            if (newFileName != fileName)
            {
                string directory = Path.GetDirectoryName(filePath);
                string newFilePath = Path.Combine(directory, newFileName + Path.GetExtension(filePath));
                AssetDatabase.RenameAsset(filePath, newFileName);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Debug.Log($"Файл перейменовано з {filePath} на {newFilePath}");
            }
        }
    }
}
