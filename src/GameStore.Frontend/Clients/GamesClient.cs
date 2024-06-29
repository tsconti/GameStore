using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClient
{
    private readonly List<GameSummary> games = 
    [
        new(){
            Id = 1,
            Name = "Street Fighter II",
            Genre = "Fighting",
            Price = 19.99m,
            ReleaseDate = new DateOnly(1992, 7, 15)
        },
        new(){
            Id = 2,
            Name = "The Witcher 3: Wild Hunt",
            Genre = "RPG",
            Price = 29.99m,
            ReleaseDate = new DateOnly(2015, 5, 19)
        },
        new(){
            Id = 3,
            Name = "FIFA 23",
            Genre = "Sports",
            Price = 69.99m,
            ReleaseDate = new DateOnly(2022, 9, 27)
        },
    ];

    private readonly Genre[] genres = new GenresClient().GetGenres();

    public GameSummary[] GetGames() => [.. games];

    public void AddGame(GameDetails game)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(game.GenreId);        
        var genre  = genres.Single(g => g.Id == int.Parse(game.GenreId));

        var newGame = new GameSummary
        {
            Id = games.Count + 1,
            Name = game.Name,
            Genre = genre.Name,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };

        games.Add(newGame);
    }

    public GameDetails GetGame(int id)
    {
        var game = games.Find(g => g.Id == id);
        ArgumentNullException.ThrowIfNull(game);

        var genre = genres.Single(g => string.Equals(
            g.Name,
            game.Genre,
            StringComparison.InvariantCultureIgnoreCase)
        );

        return new GameDetails
        {
            Id = game.Id,
            Name = game.Name,
            GenreId = genre.Id.ToString(),
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

}
