using Azubi.ConnectFour.Abstracts;
using FluentAssertions;

namespace Azubi.ConnectFour.Tests;

public class NormalGameBotTests
{
    private char[][] _field;
    private IGameBot _bot;

    [SetUp]
    public void Setup()
    {
        _field = GenerateEmptyField();

        _bot = new GameBot(Difficulty.Normal, true);
    }

    [Test]
    public void CalculateBotPosition_WithEmptyField_PlacesRandom()
    {
        _field = GenerateEmptyField();

        var position = _bot.CalculateBotPosition(_field);

        position.Should().BeInRange(1, 8);
    }

    [Test]
    public void CalculateBotPosition_WithThreeHorizontally_PlacesFourthRight()
    {
        _field = GenerateEmptyField();
        _field[7][0] = 'X';
        _field[7][1] = 'X';
        _field[7][2] = 'X';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().Be(4);
    }

    [Test]
    public void CalculateBotPosition_WithThreeHorizontally_PlacesFourthLeft()
    {
        _field = GenerateEmptyField();
        _field[7][5] = 'X';
        _field[7][6] = 'X';
        _field[7][7] = 'X';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().Be(5);
    }

    [Test]
    public void CalculateBotPosition_WithThreeHorizontallyLevitating_PlacesFourthRandom()
    {
        _field = GenerateEmptyField();
        _field[6][0] = 'X';
        _field[6][1] = 'X';
        _field[6][2] = 'X';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().BeInRange(1, 8);
    }

    [Test]
    public void CalculateBotPosition_WithThreeVertically_PlacesFourthOnTop()
    {
        _field = GenerateEmptyField();
        _field[7][5] = 'X';
        _field[6][5] = 'X';
        _field[5][5] = 'X';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().Be(6);
    }

    [Test]
    public void CalculateBotPosition_WithThreeDiagonally_PlacesFourthTopLeft()
    {
        _field = GenerateEmptyField();
        _field[7][7] = 'X';
        _field[6][6] = 'X';
        _field[5][5] = 'X';
        _field[5][4] = 'O';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().Be(5);
    }

    [Test]
    public void CalculateBotPosition_WithThreeDiagonally_PlacesFourthTopRight()
    {
        _field = GenerateEmptyField();
        _field[7][0] = 'X';
        _field[6][1] = 'X';
        _field[5][2] = 'X';
        _field[5][3] = 'O';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().Be(4);
    }

    [Test]
    public void CalculateBotPosition_WithThreeDiagonally_PlacesFourthBottomLeft()
    {
        _field = GenerateEmptyField();
        _field[4][7] = 'X';
        _field[5][6] = 'X';
        _field[6][5] = 'X';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().Be(5);
    }

    [Test]
    public void CalculateBotPosition_WithThreeDiagonally_PlacesFourthBottomRight()
    {
        _field = GenerateEmptyField();
        _field[4][0] = 'X';
        _field[5][1] = 'X';
        _field[6][2] = 'X';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().Be(4);
    }

    [Test]
    public void CalculateBotPosition_WithTwoHorizontally_PlacesThirdRight()
    {
        _field = GenerateEmptyField();
        _field[7][0] = 'X';
        _field[7][1] = 'X';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().Be(3);
    }

    [Test]
    public void CalculateBotPosition_WithTwoHorizontally_PlacesThirdLeft()
    {
        _field = GenerateEmptyField();
        _field[7][5] = 'X';
        _field[7][6] = 'X';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().Be(5);
    }

    [Test]
    public void CalculateBotPosition_WithTwoHorizontallyLevitating_PlacesThirdRandom()
    {
        _field = GenerateEmptyField();
        _field[6][0] = 'X';
        _field[6][1] = 'X';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().BeInRange(1, 8);
    }

    [Test]
    public void CalculateBotPosition_WithTwoVertically_PlacesThirdOnTop()
    {
        _field = GenerateEmptyField();
        _field[7][5] = 'X';
        _field[6][5] = 'X';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().Be(6);
    }

    [Test]
    public void CalculateBotPosition_WithTwoDiagonally_PlacesThirdTopLeft()
    {
        _field = GenerateEmptyField();
        _field[7][7] = 'X';
        _field[6][6] = 'X';
        _field[6][5] = 'O';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().Be(6);
    }

    [Test]
    public void CalculateBotPosition_WithTwoDiagonally_PlacesThirdTopRight()
    {
        _field = GenerateEmptyField();
        _field[7][0] = 'X';
        _field[6][1] = 'X';
        _field[6][2] = 'O';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().Be(3);
    }

    [Test]
    public void CalculateBotPosition_WithTwoDiagonally_PlacesThirdBottomLeft()
    {
        _field = GenerateEmptyField();
        _field[4][7] = 'X';
        _field[5][6] = 'X';
        _field[7][5] = 'O';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().Be(6);
    }

    [Test]
    public void CalculateBotPosition_WithTwoDiagonally_PlacesThirdBottomRight()
    {
        _field = GenerateEmptyField();
        _field[4][0] = 'X';
        _field[5][1] = 'X';
        _field[7][2] = 'O';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().Be(3);
    }

    [Test]
    public void CalculateBotPosition_WithOneHorizontally_PlacesSecondRight()
    {
        _field = GenerateEmptyField();
        _field[7][0] = 'X';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().Be(2);
    }

    [Test]
    public void CalculateBotPosition_WithOneHorizontally_PlacesSecondLeft()
    {
        _field = GenerateEmptyField();
        _field[7][5] = 'X';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().Be(5);
    }

    [Test]
    public void CalculateBotPosition_WithOneHorizontallyLevitating_PlacesSecondRandom()
    {
        _field = GenerateEmptyField();
        _field[6][0] = 'X';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().BeInRange(1, 8);
    }

    [Test]
    public void CalculateBotPosition_WithOneVertically_PlacesSecondOnTop()
    {
        _field = GenerateEmptyField();
        _field[7][4] = 'O';
        _field[7][5] = 'X';
        _field[7][6] = 'O';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().Be(6);
    }

    [Test]
    public void CalculateBotPosition_WithOneDiagonally_PlacesSecondTopLeft()
    {
        _field = GenerateEmptyField();
        _field[7][7] = 'X';
        _field[6][7] = 'O';
        _field[7][6] = 'O';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().Be(7);
    }

    [Test]
    public void CalculateBotPosition_WithOneDiagonally_PlacesSecondTopRight()
    {
        _field = GenerateEmptyField();
        _field[7][0] = 'X';
        _field[7][1] = 'O';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().Be(2);
    }

    [Test]
    public void CalculateBotPosition_WithOneDiagonally_PlacesSecondBottomLeft()
    {
        _field = GenerateEmptyField();
        _field[4][7] = 'X';
        _field[6][6] = 'O';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().Be(7);
    }

    [Test]
    public void CalculateBotPosition_WithOneDiagonally_PlacesSecondBottomRight()
    {
        _field = GenerateEmptyField();
        _field[4][0] = 'X';
        _field[6][1] = 'O';

        var position = _bot.CalculateBotPosition(_field);

        position.Should().Be(2);
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
