using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2
{
    public class InputOutputMessagesUI
    {
        public static int ValidColumnOrQuit(RoundLogic i_Round, string i_StrInput)
        {
            int columnOrQuit;
            bool isNum = int.TryParse(i_StrInput, out columnOrQuit);

            while ((columnOrQuit < 1) || (columnOrQuit > i_Round.RoundBoard.Width) || !(isNum) ||
                (i_Round.RoundBoard.AvailableSpotEachColumn[columnOrQuit - 1] == -1))
            {
                if (i_StrInput == "Q")
                {
                    columnOrQuit = -1;
                    break;
                }
                else
                {
                    isNum = PrintInvalidMessageAndGetNewValue(ref columnOrQuit);
                }

            }

            return columnOrQuit;
        }

        public static int ValidBoardSizeInput(string i_StrNum)
        {
            int number;
            bool isNum = int.TryParse(i_StrNum, out number);

            while ((number < 4) || (number > 8) || !(isNum))
            {
                isNum = PrintInvalidMessageAndGetNewValue(ref number);
            }

            return number;
        }

        public static bool PrintInvalidMessageAndGetNewValue(ref int io_NewNumber)
        {
            string strNumber;
            bool isNumber;

            System.Console.WriteLine("Invalid input!!! enter again... (and then press 'ENTER')");
            strNumber = System.Console.ReadLine();
            isNumber = int.TryParse(strNumber, out io_NewNumber);

            return isNumber;
        }

        public static void PrintComputerChoiceMessage(int i_ChosenColumn)
        {
            string ComputerMessage;

            ComputerMessage = string.Format("Computer chose {0}", i_ChosenColumn);
            System.Console.WriteLine(ComputerMessage);
        }

        public static void PrintTurnMessage(eStatus i_PlayerName)
        {
            string turnMessage;

            turnMessage = string.Format("{0}'s turn:", i_PlayerName);
            System.Console.WriteLine(turnMessage);
        }

        public static int PrintRequestMessageAndGetChosenColumn(RoundLogic i_Round)
        {
            string strChosenColumn;
            int chosenColumn;

            System.Console.WriteLine("Enter the column for your token or press 'Q' to quit (and then press 'ENTER')");
            strChosenColumn = System.Console.ReadLine();
            chosenColumn = ValidColumnOrQuit(i_Round, strChosenColumn);

            return chosenColumn;
        }

        public static int PrintLengthMessageAndGetLength()
        {
            int length;
            const string k_ValueName = "length";

            length = PrintValueMessageAndGetValue(k_ValueName);

            return length;
        }

        public static int PrintWidthMessageAndGetWidth()
        {
            int width;
            const string k_ValueName = "width";

            width = PrintValueMessageAndGetValue(k_ValueName);

            return width;
        }

        public static int PrintValueMessageAndGetValue(string i_ValueName)
        {
            string valueMessage, strValue;
            int value;

            valueMessage = string.Format("Enter the {0} of the board between 4 to 8 (and then press 'ENTER')", i_ValueName);
            System.Console.WriteLine(valueMessage);
            strValue = System.Console.ReadLine();
            value = ValidBoardSizeInput(strValue);

            return value;
        }

        public static bool PrintOpponentMessageAndGetIsAgainstComputer()
        {
            string strIsAgainstComputer;
            bool isAgainstComputer;

            System.Console.WriteLine("Press '0' to play against another player or" +
                " press any other key to play against computer (and then press 'ENTER')");
            strIsAgainstComputer = System.Console.ReadLine();
            isAgainstComputer = strIsAgainstComputer != "0";

            return isAgainstComputer;
        }

        public static void PrintQuitMessage(eStatus i_QuitPlayer)
        {
            string quitMessage;

            quitMessage = string.Format("{0} quit", i_QuitPlayer);
            System.Console.WriteLine(quitMessage);
        }

        public static void PrintWinMessage(eStatus i_WinnerPlayer)
        {
            string winMessage;

            winMessage = string.Format("{0} is the winner", i_WinnerPlayer);
            System.Console.WriteLine(winMessage);
        }

        public static void PrintDrawMessage()
        {
            System.Console.WriteLine("Draw!!! No winner in this round");
        }

        public static void PrintComputerPlayerMessage()
        {
            System.Console.WriteLine("PlayerTwo is the computer");
        }

        public static void ClearAndPrintRound(RoundLogic i_Round)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            PrintRound(i_Round);
        }

        public static void PrintRound(RoundLogic i_Round)
        {
            PrintColumnsNumbers(i_Round);
            for (int row = 0; row < i_Round.RoundBoard.Length; row++)
            {
                PrintRow(i_Round, row);
            }

        }

        private static void PrintColumnsNumbers(RoundLogic i_Round)
        {
            string message = null;

            for (int column = 1; column <= i_Round.RoundBoard.Width; column++)
            {
                message += (string.Format("  {0} ", column));
            }

            System.Console.WriteLine(message);
        }

        private static string CastToSymbol(eStatus i_CurrentStatus)
        {
            string matchedSymbol = null;

            switch (i_CurrentStatus)
            {
                case eStatus.Empty:
                    matchedSymbol = " ";
                    break;

                case eStatus.PlayerOne:
                    matchedSymbol = "X";
                    break;

                case eStatus.PlayerTwo:
                    matchedSymbol = "O";
                    break;
            }

            return matchedSymbol;
        }

        private static void PrintRow(RoundLogic i_Round, int i_RowIndex)
        {
            string message = null;

            for (int column = 0; column < i_Round.RoundBoard.Width; column++)
            {
                message += string.Format("| {0} ",
                    CastToSymbol(i_Round.RoundBoard.Board[i_RowIndex, column]));
            }

            message += "|";
            System.Console.WriteLine(message);
            message = null;
            for (int column = 0; column < i_Round.RoundBoard.Width; column++)
            {
                message += "====";
            }

            message += "=";
            System.Console.WriteLine(message);
        }

        public static bool PrintAnotherRoundMessageAndGetIsAnotherRound()
        {
            string pressedKey;
            bool isAnotherRound = false;

            System.Console.WriteLine("Press '1' to play another round or press any other key to exit ");
            pressedKey = System.Console.ReadLine();
            if (pressedKey == "1")
            {
                isAnotherRound = true;
            }

            return isAnotherRound;
        }

        public static void PrintScoreBoard(RoundLogic i_Round)
        {
            string scoreBoardMessage;

            scoreBoardMessage = string.Format(@"{0}'s points: {1}
{2}' points: {3}", eStatus.PlayerOne, i_Round.ScoreBoard.FirstPlayerPoints,
eStatus.PlayerTwo, i_Round.ScoreBoard.SecondPlayerPoints);
            System.Console.WriteLine(scoreBoardMessage);
        }
    }
}
