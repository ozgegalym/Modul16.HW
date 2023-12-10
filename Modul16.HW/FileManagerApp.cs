using System;

class FileManagerApp
{
    private FileManager fileManager;

    public FileManagerApp()
    {
        fileManager = new FileManager();
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("1. Просмотр содержимого директории");
            Console.WriteLine("2. Создание файла/директории");
            Console.WriteLine("3. Удаление файла/директории");
            Console.WriteLine("4. Копирование и перемещение файлов и директорий");
            Console.WriteLine("5. Чтение и запись в файл");
            Console.WriteLine("6. Логирование действий");
            Console.WriteLine("7. Рекурсивный просмотр содержимого директории");
            Console.WriteLine("8. Поиск файла/директории");
            Console.WriteLine("9. Обработка исключений");
            Console.WriteLine("10. Выход");

            Console.Write("Выберите действие (1-10): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    fileManager.DisplayDirectoryContents();
                    break;

                case "2":
                    fileManager.CreateFileOrDirectory();
                    break;

                case "3":
                    fileManager.DeleteFileOrDirectory();
                    break;

                case "4":
                    fileManager.CopyOrMoveFileOrDirectory();
                    break;

                case "5":
                    fileManager.ReadAndWriteToFile();
                    break;

                case "6":
                    fileManager.LogActions();
                    break;

                case "7":
                    fileManager.RecursiveDisplayDirectoryContents();
                    break;

                case "8":
                    fileManager.SearchFileOrDirectory();
                    break;

                case "9":
                    fileManager.ExceptionHandlingExample();
                    break;

                case "10":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, выберите снова.");
                    break;
            }
        }
    }
}
