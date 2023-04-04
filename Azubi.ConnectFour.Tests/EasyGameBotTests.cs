using Azubi.ConnectFour.Abstracts;
using FluentAssertions;

namespace Azubi.ConnectFour.Tests;

public class EasyGameBotTests
{
    private char[][] _field;
    private IGameBot _bot;

    [SetUp]
    public void Setup()
    {
        _field = GenerateEmptyField();

        _bot = new GameBot(Difficulty.Easy, true);
    }

    [Test]
    public void CalculateBotPosition_WithEmptyField_PlacesRandom()
    {
        _field = GenerateEmptyField();

        var position = _bot.CalculateBotPosition(_field);

        position.Should().BeInRange(1, 8);
    }

    private char[][] GenerateEmptyField()
    {
        var field = new char[8][];
        for (var i = 0; i < field.Length; i++)
        {
            field[i] = new char[8];
            for (var j = 0; j < field[i].Length; j++)
            {
                field[i][j] = '-';
            }
        }

        return field;
    }
}