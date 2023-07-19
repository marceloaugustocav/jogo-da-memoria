using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Jogo da Memória");
        Console.WriteLine("****************");

        // Tamanho do tabuleiro (altere conforme desejado)
        int rows = 4;
        int cols = 4;

        // Inicialização do tabuleiro
        int[,] board = GenerateBoard(rows, cols);

        // Tabuleiro com cartas viradas para baixo
        bool[,] cardsFlipped = new bool[rows, cols];

        // Loop do jogo
        while (true)
        {
            DisplayBoard(board, cardsFlipped);

            // Entrada do jogador
            Console.Write("Digite a linha da carta: ");
            int rowChoice = int.Parse(Console.ReadLine()) - 1; // Subtrai 1 pois as posições do array começam em 0
            Console.Write("Digite a coluna da carta: ");
            int colChoice = int.Parse(Console.ReadLine()) - 1; // Subtrai 1 pois as posições do array começam em 0

            // Verifica se a carta já foi escolhida ou se a posição é inválida
            if (cardsFlipped[rowChoice, colChoice])
            {
                Console.WriteLine("Carta já foi escolhida! Tente novamente.");
                continue;
            }

            // Exibe a carta escolhida
            cardsFlipped[rowChoice, colChoice] = true;
            DisplayBoard(board, cardsFlipped);

            // Verifica se o jogo acabou
            if (IsGameOver(cardsFlipped))
            {
                Console.WriteLine("Parabéns, você venceu!");
                break;
            }

            // Espera um pouco para que o jogador possa memorizar as cartas
            System.Threading.Thread.Sleep(2000);

            // Esconde as cartas escolhidas
            cardsFlipped[rowChoice, colChoice] = false;
            DisplayBoard(board, cardsFlipped);

            // Aguarda um pouco para mostrar a tela com as cartas viradas para baixo
            System.Threading.Thread.Sleep(1000);
        }

        Console.WriteLine("Fim do Jogo!");
    }

    static int[,] GenerateBoard(int rows, int cols)
    {
        int[,] board = new int[rows, cols];
        int cardNumber = 1;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                board[i, j] = cardNumber / 2 + 1;
                cardNumber++;
            }
        }

        // Embaralha as cartas
        Random random = new Random();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int temp = board[i, j];
                int randRow = random.Next(rows);
                int randCol = random.Next(cols);
                board[i, j] = board[randRow, randCol];
                board[randRow, randCol] = temp;
            }
        }

        return board;
    }

    static void DisplayBoard(int[,] board, bool[,] cardsFlipped)
    {
        Console.Clear();
        int rows = board.GetLength(0);
        int cols = board.GetLength(1);

        Console.WriteLine("  1 2 3 4");

        for (int i = 0; i < rows; i++)
        {
            Console.Write($"{i + 1} ");
            for (int j = 0; j < cols; j++)
            {
                if (cardsFlipped[i, j])
                {
                    Console.Write(board[i, j] + " ");
                }
                else
                {
                    Console.Write("* ");
                }
            }
            Console.WriteLine();
        }
    }

    static bool IsGameOver(bool[,] cardsFlipped)
    {
        int rows = cardsFlipped.GetLength(0);
        int cols = cardsFlipped.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (!cardsFlipped[i, j])
                {
                    return false;
                }
            }
        }
        return true;
    }
}
