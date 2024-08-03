using System;
using System.Collections.Generic;

public class CinemaTicketSystem
{
    private Dictionary<int, string> movies = new Dictionary<int, string>();
    private Dictionary<int, string> users = new Dictionary<int, string>();
    private Dictionary<int, (int userId, int movieId)> tickets = new Dictionary<int, (int userId, int movieId)>();

    private int nextMovieId = 1;
    private int nextUserId = 1;
    private int nextTicketId = 1;

    public CinemaTicketSystem() { }

    public int AddMovie(string movieName)
    {
        int movieId = nextMovieId++;
        movies[movieId] = movieName;
        return movieId;
    }

    public void ShowAllMovies()
    {
        foreach (var movie in movies)
        {
            Console.WriteLine($"ID: {movie.Key}, Name: {movie.Value}");
        }
    }

    public int AddUser(string userName)
    {
        int userId = nextUserId++;
        users[userId] = userName;
        return userId;
    }

    public int BuyTicket(int userId, int movieId)
    {
        if (!users.ContainsKey(userId))
        {
            Console.WriteLine("Пользователь не найден!");
            return -1;
        }
        if (!movies.ContainsKey(movieId))
        {
            Console.WriteLine("Фильм не найден!");
            return -1;
        }

        int ticketId = nextTicketId++;
        tickets[ticketId] = (userId, movieId);
        return ticketId;
    }

    public bool CancelTicket(int ticketId)
    {
        if (tickets.ContainsKey(ticketId))
        {
            tickets.Remove(ticketId);
            return true;
        }
        return false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        CinemaTicketSystem system = new CinemaTicketSystem();
        while (true)
        {
            Console.WriteLine("Здравствуйте, у вас есть следующие доступные функции:");
            Console.WriteLine("1. Добавить новый фильм;");
            Console.WriteLine("2. Показать все доступные фильмы;");
            Console.WriteLine("3. Добавить нового пользователя;");
            Console.WriteLine("4. Купить билет;");
            Console.WriteLine("5. Отменить покупку билета;");
            Console.WriteLine("6. Выйти.");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Введите название фильма: ");
                    string movieName = Console.ReadLine();
                    int movieId = system.AddMovie(movieName);
                    Console.WriteLine($"Фильм добавлен с ID: {movieId}");
                    break;

                case "2":
                    Console.WriteLine("Список всех доступных фильмов:");
                    system.ShowAllMovies();
                    break;

                case "3":
                    Console.Write("Введите имя пользователя: ");
                    string userName = Console.ReadLine();
                    int userId = system.AddUser(userName);
                    Console.WriteLine($"Пользователь добавлен с ID: {userId}");
                    break;

                case "4":
                    Console.Write("Введите ID пользователя: ");
                    int userToBuyId = int.Parse(Console.ReadLine());
                    Console.Write("Введите ID фильма: ");
                    int movieToBuyId = int.Parse(Console.ReadLine());
                    int ticketId = system.BuyTicket(userToBuyId, movieToBuyId);
                    if (ticketId != -1)
                    {
                        Console.WriteLine($"Билет куплен с ID: {ticketId}");
                    }
                    break;

                case "5":
                    Console.Write("Введите ID билета: ");
                    int ticketToCancelId = int.Parse(Console.ReadLine());
                    bool isCancelled = system.CancelTicket(ticketToCancelId);
                    if (isCancelled)
                    {
                        Console.WriteLine("Билет успешно отменен.");
                    }
                    else
                    {
                        Console.WriteLine("Билет с таким ID не найден.");
                    }
                    break;

                case "6":
                    return;

                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }
    }
}
