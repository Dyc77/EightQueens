var solver = new EightQueens();
solver.Main();

class EightQueens
{
    // 八皇后是二維陣列，我要找出所有解，所以再用一個 List 裝
    List<int[,]> solutions = new List<int[,]>();

    public void Main()
    {
        int[,] board = new int[8, 8];

        // 從 [0, 0] 開始，由上到下找出所有解，所以可以使用 DFS
        DFS(board, 0);

        Console.Write($"共：{solutions.Count}組解");
        Console.WriteLine();

        PrintResult();
    }

    public void DFS(int[,] board, int row)
    {
        // 找完了，它就是解
        if (row == 8)
        {
            solutions.Add((int[,])board.Clone());
            return;
        }

        for (int col = 0; col < 8; col++)
        {
            if (Validate(board, row, col))
            {
                // 先把當前座標註記成 1
                board[row, col] = 1;
                // 準備進入下一層
                DFS(board, row + 1);
                // DFS 回來了，取消原先的註記
                board[row, col] = 0;
            }
        }
    }

    public bool Validate(int[,] board, int row, int col)
    {
        // 因為是由上往下探索，所以需要檢驗三個方向
        // 1. 該 col 在前面幾 row 是否存在 1
        for (int i = 0; i < row; i++)
        {
            int displacement = i + 1;

            // 1. 該 col 在前面幾 row 是否存在 1
            if (board[i, col] == 1)
                return false;

            // 2. 往左上探索，在邊界內才判斷
            if (col - displacement >= 0)
            {
                if (board[row - displacement, col - displacement] == 1)
                    return false;
            }

            // 3. 往右上探索，在邊界內才判斷
            if (col + displacement <= 7)
            {
                if (board[row - displacement, col + displacement] == 1)
                    return false;
            }
        }

        return true;
    }

    public void PrintResult()
    {
        Console.WriteLine("----------------");

        for (int i = 0; i < solutions.Count; i++)
        {
            int[,] board = solutions[i];

            Console.WriteLine($"//Solution {i + 1}");

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Console.Write(board[row, col] == 1 ? "Q " : ". ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}