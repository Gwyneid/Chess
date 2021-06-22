using System;
class Move
{
    string cell_1;
    string cell_2;
    bool real = false;
    public bool PossibleMove(int[] a, string[,] board, int color)
    {
        if ((a[0] > 7 || a[0] < 0) || (a[1] < 0 || a[1] > 7) || (a[3] < 0 || a[3] > 7) || (a[4] < 0 || a[4] > 7))
        {
            return false;
        }
        if (board[a[1], a[0]] == " ⃞")
            return false;
        cell_1 = board[a[1], a[0]];
        cell_2 = board[a[4], a[3]];
        if (cell_1[0] == 'b' && color == 0)
            return false;
        if (cell_2[0] == 'w' && color == 1)
            return false;
        if (a[0] == a[3] && a[1] == a[4])
            return false;
        if (cell_1[0] == cell_2[0])
            return false;
        return true;
    }
    protected bool Straight(int rangeUp, int rangeDown, int rangeLeft, int rangeRight, int[] a, string[,] board)
    {
        bool real = false;
        rangeUp = a[1] + rangeUp;
        rangeDown = a[1] - rangeDown;
        rangeLeft = a[0] - rangeLeft;
        rangeRight = a[0] + rangeRight;
        if (a[4] >= rangeDown && a[4] <= rangeUp && a[0] == a[3])
            real = true;
        if (a[3] >= rangeLeft && a[3] <= rangeRight && a[1] == a[4])
            real = true;
        for (int i = a[1] + 1; i < a[4]; i++)
            if (board[i, a[0]] != " ⃞")
                real = false;
        for (int i = a[4] + 1; i < a[1]; i++)
            if (board[i, a[0]] != " ⃞")
                real = false;
        for (int i = a[0] + 1; i < a[3]; i++)
            if (board[a[1], i] != " ⃞")
                real = false;
        for (int i = a[3] + 1; i < a[0]; i++)
            if (board[a[1], i] != " ⃞")
                real = false;
        return real;
    }
    protected bool Diagonal(int rangeUpRight, int rangeDownRight, int[] a, string[,] board)
    {
        int direction = 0;
        int[] b = new int[5];
        for (int i = 0; i < 5; i++)
            b[i] = a[i];
        if (Math.Abs(a[4] - a[1]) != Math.Abs(a[3] - a[0]))
            return false;
        if ((a[3] - a[0]) < 0 && (a[4] - a[1]) < 0)
        {
            direction = 1;
            b[0] -= 1;
            b[1] -= 1;
        }
        if ((a[3] - a[0]) > 0 && (a[4] - a[1]) > 0)
        {
            direction = 2;
            b[0] += 1;
            b[1] += 1;
        }
        if ((a[3] - a[0]) < 0 && (a[4] - a[1]) > 0)
        {
            direction = 3;
            b[0] -= 1;
            b[1] += 1;
        }
        if ((a[3] - a[0]) > 0 && (a[4] - a[1]) < 0)
        {
            direction = 4;
            b[0] += 1;
            b[1] -= 1;
        }
        while (b[0] != a[3])
        {
            if (direction == 1)
            {
                if (board[b[1], b[0]] != " ⃞")
                    return false;
                b[0] -= 1;
                b[1] -= 1;
            }
            if (direction == 2)
            {
                if (board[b[1], b[0]] != " ⃞")
                    return false;
                b[0] += 1;
                b[1] += 1;
            }
            if (direction == 3)
            {
                if (board[b[1], b[0]] != " ⃞")
                    return false;
                b[0] -= 1;
                b[1] += 1;
            }
            if (direction == 4)
            {
                if (board[b[1], b[0]] != " ⃞")
                    return false;
                b[0] += 1;
                b[1] -= 1;
            }
        }

        return true;
    }
}

class Figures : Move
{
    string cell_1;
    protected bool Rook(int[] a, string[,] board)
    {
        if (Straight(8, 8, 8, 8, a, board))
        {
            return true;
        }
        return false;
    }
    protected bool Pawn(int[] a, string[,] board)
    {
        cell_1 = board[a[1], a[0]];
        if (cell_1[0] == 'w')
        {
            if (board[a[4], a[3]] == " ⃞")
            {
                if (a[1] == 6)
                {
                    if (Straight(0, 2, 0, 0, a, board))
                        return true;
                }
                else
                {
                    if (Straight(0, 1, 0, 0, a, board))
                        return true;
                }
            }
            if (board[a[4], a[3]] != " ⃞" && (((a[1] - 1) == a[4] && (a[0] + 1) == a[3]) || ((a[1] - 1) == a[4] && (a[0] - 1) == a[3])))
                return true;
        }
        else
        {
            if (board[a[4], a[3]] == " ⃞")
            {
                if (a[1] == 1)
                {
                    if (Straight(2, 0, 0, 0, a, board))
                        return true;
                }
                else
                {
                    if (Straight(1, 0, 0, 0, a, board))
                        return true;
                }
            }
            if (board[a[4], a[3]] != " ⃞" && (((a[1] + 1) == a[4] && (a[0] + 1) == a[3]) || ((a[1] + 1) == a[4] && (a[0] - 1) == a[3])))
                return true;
        }
        return false;
    }
    protected bool Bishop(int[] a, string[,] board)
    {
        if (Diagonal(8, 8, a, board))
            return true;
        return false;
    }
    protected bool Queen(int[] a, string[,] board)
    {
        if (Diagonal(8, 8, a, board) || Straight(8, 8, 8, 8, a, board))
            return true;
        return false;
    }
    protected bool Knight(int[] a, string[,] board)
    {
        if ((a[1] - 2 == a[4] && a[0] + 1 == a[3]) || (a[1] - 2 == a[4] && a[0] - 1 == a[3]) || (a[1] + 2 == a[4] && a[0] + 1 == a[3]) || (a[1] + 2 == a[4] && a[0] - 1 == a[3]))
            return true;
        if ((a[1] - 1 == a[4] && a[0] + 2 == a[3]) || (a[1] - 1 == a[4] && a[0] - 2 == a[3]) || (a[1] + 1 == a[4] && a[0] + 2 == a[3]) || (a[1] + 1 == a[4] && a[0] - 2 == a[3]))
            return true;
        return false;
    }
    protected bool King(int[] a, string[,] board)
    {
        if ((Math.Abs(a[3] - a[0]) == 1 || (a[3] - a[0]) == 0) && (Math.Abs(a[4] - a[1]) == 1 || (a[4] - a[1]) == 0))
            return true;
        return false;
    }
}

class Board : Figures
{
    private string tmp;
    private int[] b = new int[5];
    private int color = 0;
    private string cell_1;
    private int madeMove = 0;
    private string[,] board = new string[,]
           {
                {"bR", "bN", "bB", "bQ", "bK", "bB", "bN", "bR"},
                {"bP", "bP", "bP", "bP", "bP", "bP", "bP", "bP"},
                {" ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞"},
                {" ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞"},
                {" ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞"},
                {" ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞"},
                {"wP", "wP", "wP", "wP", "wP", "wP", "wP", "wP"},
                {"wR", "wN", "wB", "wQ", "wK", "wB", "wN", "wR"}
           };
    void changeBoard(int a1, int a2, int b1, int b2)
    {
        tmp = board[a2, a1];
        board[a2, a1] = " ⃞";
        board[b2, b1] = tmp;
        madeMove = 1;
    }
    public void ShowBoard()
    {
        Console.WriteLine();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Console.Write(board[i, j] + " ");
                if (j == 7)
                    Console.WriteLine();
            }
        }
        Console.WriteLine();
        if (madeMove == 1)
        {
            if (color == 0)
            {
                color = 1;
            }
            else
            {
                color = 0;
            }
            madeMove = 0;
        }
        if (color == 0)
        {
            Console.WriteLine("Ход Белых");
        }
        else
        {
            Console.WriteLine("Ход Черных");
        }
    }
    bool DefineAndUseFigure()
    {
        cell_1 = board[b[1], b[0]];
        if (cell_1[1] == 'R')
            if (Rook(b, board))
                return true;
        if (cell_1[1] == 'N')
            if (Knight(b, board))
                return true;
        if (cell_1[1] == 'K')
            if (King(b, board))
                return true;
        if (cell_1[1] == 'Q')
            if (Queen(b, board))
                return true;
        if (cell_1[1] == 'P')
            if (Pawn(b, board))
                return true;
        if (cell_1[1] == 'B')
            if (Bishop(b, board))
                return true;
        return false;
    }
    public void Game()
    {
        string a = Console.ReadLine();
        if (a == "new game")
        {
            board = new string[,]
           {
                {"bR", "bN", "bB", "bQ", "bK", "bB", "bN", "bR"},
                {"bP", "bP", "bP", "bP", "bP", "bP", "bP", "bP"},
                {" ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞"},
                {" ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞"},
                {" ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞"},
                {" ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞", " ⃞"},
                {"wP", "wP", "wP", "wP", "wP", "wP", "wP", "wP"},
                {"wR", "wN", "wB", "wQ", "wK", "wB", "wN", "wR"}
           };
            madeMove = 0;
            color = 0;
            ShowBoard();
        }
        else if (a.Length < 5 || a.Length > 5)
        {
            Console.WriteLine("неверные входные данные");
        }
        else
        {
            b[0] = a[0] - 97;
            b[1] = 7 - (a[1] - 49);
            b[2] = a[2];
            b[3] = a[3] - 97;
            b[4] = 7 - (a[4] - 49);
            if (PossibleMove(b, board, color) && DefineAndUseFigure())
            {
                changeBoard(b[0], b[1], b[3], b[4]);
                ShowBoard();
            }
            else
            {
                Console.WriteLine("неверные входные данные");
            }
        }
    }
}

namespace Chess
{
    class Program : Board
    {
        static void Main()
        {
            int game = 1;
            Board board = new Board();
            board.ShowBoard();
            while (game == 1)
            {
                board.Game();
            }
        }
    }
}