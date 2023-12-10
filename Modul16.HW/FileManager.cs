using System;
using System.IO;

class FileManager
{
    private string rootPath = "C:\\"; // Задайте корневой путь по умолчанию
    private string logFilePath = "log.txt";

    public void DisplayDirectoryContents()
    {
        Console.Write("Введите путь к директории: ");
        string path = Console.ReadLine();
        rootPath = path;

        Console.WriteLine($"Содержимое директории {path}:");
        string[] directories = Directory.GetDirectories(path);
        string[] files = Directory.GetFiles(path);

        Console.WriteLine("Директории:");
        foreach (var directory in directories)
        {
            Console.WriteLine(directory);
        }

        Console.WriteLine("\nФайлы:");
        foreach (var file in files)
        {
            Console.WriteLine(file);
        }
    }

    public void CreateFileOrDirectory()
    {
        Console.Write("Введите полный путь к новому файлу/директории: ");
        string newPath = Console.ReadLine();

        if (File.Exists(newPath) || Directory.Exists(newPath))
        {
            Console.WriteLine("Файл/директория уже существует.");
            return;
        }

        Console.Write("Выберите тип (файл - F, директория - D): ");
        string typeChoice = Console.ReadLine();

        if (typeChoice.ToUpper() == "F")
        {
            File.Create(newPath).Close();
            Console.WriteLine($"Файл успешно создан по пути {newPath}");
        }
        else if (typeChoice.ToUpper() == "D")
        {
            Directory.CreateDirectory(newPath);
            Console.WriteLine($"Директория успешно создана по пути {newPath}");
        }
        else
        {
            Console.WriteLine("Неверный выбор типа.");
        }
    }

    public void DeleteFileOrDirectory()
    {
        Console.Write("Введите полный путь к файлу/директории для удаления: ");
        string pathToDelete = Console.ReadLine();

        if (File.Exists(pathToDelete))
        {
            File.Delete(pathToDelete);
            Console.WriteLine($"Файл успешно удален: {pathToDelete}");
        }
        else if (Directory.Exists(pathToDelete))
        {
            Directory.Delete(pathToDelete, true);
            Console.WriteLine($"Директория успешно удалена: {pathToDelete}");
        }
        else
        {
            Console.WriteLine("Файл/директория не существует.");
        }
    }

    public void CopyOrMoveFileOrDirectory()
    {
        Console.Write("Введите полный путь к файлу/директории: ");
        string sourcePath = Console.ReadLine();

        Console.Write("Введите полный путь, куда скопировать/переместить: ");
        string destinationPath = Console.ReadLine();

        Console.Write("Выберите действие (копировать - C, переместить - M): ");
        string actionChoice = Console.ReadLine();

        try
        {
            if (actionChoice.ToUpper() == "C")
            {
                if (File.Exists(sourcePath))
                {
                    File.Copy(sourcePath, destinationPath);
                    Console.WriteLine($"Файл успешно скопирован в {destinationPath}");
                }
                else if (Directory.Exists(sourcePath))
                {
                    CopyDirectory(sourcePath, destinationPath);
                    Console.WriteLine($"Директория успешно скопирована в {destinationPath}");
                }
                else
                {
                    Console.WriteLine("Файл/директория не существует.");
                }
            }
            else if (actionChoice.ToUpper() == "M")
            {
                if (File.Exists(sourcePath))
                {
                    File.Move(sourcePath, destinationPath);
                    Console.WriteLine($"Файл успешно перемещен в {destinationPath}");
                }
                else if (Directory.Exists(sourcePath))
                {
                    Directory.Move(sourcePath, destinationPath);
                    Console.WriteLine($"Директория успешно перемещена в {destinationPath}");
                }
                else
                {
                    Console.WriteLine("Файл/директория не существует.");
                }
            }
            else
            {
                Console.WriteLine("Неверный выбор действия.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    public void ReadAndWriteToFile()
    {
        Console.Write("Введите полный путь к файлу для чтения/записи: ");
        string filePath = Console.ReadLine();

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл не существует.");
            return;
        }

        Console.Write("Выберите действие (чтение - R, запись - W): ");
        string actionChoice = Console.ReadLine();

        try
        {
            if (actionChoice.ToUpper() == "R")
            {
                string content = File.ReadAllText(filePath);
                Console.WriteLine($"Содержимое файла:\n{content}");
            }
            else if (actionChoice.ToUpper() == "W")
            {
                Console.Write("Введите текст для записи в файл: ");
                string textToWrite = Console.ReadLine();
                File.WriteAllText(filePath, textToWrite);
                Console.WriteLine("Текст успешно записан в файл.");
            }
            else
            {
                Console.WriteLine("Неверный выбор действия.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    public void LogActions()
    {
        Console.Write("Введите действие для логирования: ");
        string actionToLog = Console.ReadLine();
        File.AppendAllText(logFilePath, $"{DateTime.Now}: {actionToLog}\n");
        Console.WriteLine("Действие успешно залогировано.");
    }

    public void RecursiveDisplayDirectoryContents()
    {
        Console.Write("Введите путь к директории для рекурсивного просмотра: ");
        string path = Console.ReadLine();

        if (!Directory.Exists(path))
        {
            Console.WriteLine("Директория не существует.");
            return;
        }

        RecursiveDisplayDirectoryContentsHelper(path, 0);
    }

    private void RecursiveDisplayDirectoryContentsHelper(string path, int indentationLevel)
    {
        string indentation = new string(' ', indentationLevel * 2);
        Console.WriteLine($"{indentation}Содержимое директории {path}:");

        string[] directories = Directory.GetDirectories(path);
        string[] files = Directory.GetFiles(path);

        Console.WriteLine($"{indentation}Директории:");
        foreach (var directory in directories)
        {
            Console.WriteLine($"{indentation}{directory}");
            RecursiveDisplayDirectoryContentsHelper(directory, indentationLevel + 1);
        }

        Console.WriteLine($"{indentation}Файлы:");
        foreach (var file in files)
        {
            Console.WriteLine($"{indentation}{file}");
        }
    }

    public void SearchFileOrDirectory()
    {
        Console.Write("Введите имя файла/директории для поиска: ");
        string nameToSearch = Console.ReadLine();

        Console.Write("Введите путь, с которого начать поиск: ");
        string searchPath = Console.ReadLine();

        try
        {
            string[] foundItems = Directory.GetFiles(searchPath, nameToSearch, SearchOption.AllDirectories);
            string[] foundDirectories = Directory.GetDirectories(searchPath, nameToSearch, SearchOption.AllDirectories);

            Console.WriteLine("Найденные файлы:");
            foreach (var file in foundItems)
            {
                Console.WriteLine(file);
            }

            Console.WriteLine("\nНайденные директории:");
            foreach (var directory in foundDirectories)
            {
                Console.WriteLine(directory);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    public void ExceptionHandlingExample()
    {
        try
        {
            // Ваш код, в котором может произойти исключение
            Console.Write("Введите число: ");
            string userInput = Console.ReadLine();
            int number = int.Parse(userInput);
            Console.WriteLine($"Введенное число: {number}");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Ошибка формата: {ex.Message}");
        }
        catch (OverflowException ex)
        {
            Console.WriteLine($"Переполнение: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Общая ошибка: {ex.Message}");
        }
    }
    // Вспомогательный метод для рекурсивного копирования директории
    private void CopyDirectory(string sourcePath, string destinationPath)
    {
        DirectoryInfo sourceDirectory = new DirectoryInfo(sourcePath);
        DirectoryInfo destinationDirectory = new DirectoryInfo(destinationPath);

        if (!destinationDirectory.Exists)
        {
            destinationDirectory.Create();
        }

        foreach (FileInfo file in sourceDirectory.GetFiles())
        {
            string destinationFilePath = Path.Combine(destinationPath, file.Name);
            file.CopyTo(destinationFilePath, true);
        }

        foreach (DirectoryInfo subDirectory in sourceDirectory.GetDirectories())
        {
            string newSubDirectoryPath = Path.Combine(destinationPath, subDirectory.Name);
            CopyDirectory(subDirectory.FullName, newSubDirectoryPath);
        }
    }
}
