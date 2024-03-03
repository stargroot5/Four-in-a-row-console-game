using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2
{
    class GameUserInterface
    {
        public static bool RequestTokenToColumnAndCheckIfWinOrQuit(RoundLogic i_Round, eStatus i_PlayerName, ref bool io_IsQuit)
        {
            int chosenColumn;
            int realColumn;
            int currentRow;
            bool isWinner = false;
            const eStatus k_ComputerPlayer = eStatus.PlayerTwo;

            InputOutputMessagesUI.PrintTurnMessage(i_PlayerName);
            if (i_Round.IsAgainstComputer && i_PlayerName == k_ComputerPlayer)
            {
                realColumn = i_Round.ComputerChosenColumn();
                chosenColumn = realColumn + 1;
            }
            else
            {
                chosenColumn = InputOutputMessagesUI.PrintRequestMessageAndGetChosenColumn(i_Round);
            }

            if (chosenColumn > 0) 
            {
                realColumn = chosenColumn - 1;
                i_Round.RoundBoard.AddTokenToColumn(i_PlayerName, realColumn);
                currentRow = i_Round.RoundBoard.AvailableSpotEachColumn[realColumn] + 1;
                InputOutputMessagesUI.ClearAndPrintRound(i_Round);
                if (i_Round.IsAgainstComputer && i_PlayerName == k_ComputerPlayer)
                {
                    InputOutputMessagesUI.PrintComputerChoiceMessage(chosenColumn);
                }

                isWinner = i_Round.IsWinner(currentRow, realColumn, i_PlayerName);
            }
            else //chosenColumn = -1 -> Player pressed Q
            {
                io_IsQuit = true;
            }
           
            return isWinner;
        }
        
        public static RoundLogic SetUp()
        {
            bool isAgainstComputer;
            int length, width;
            RoundLogic round;

            length = InputOutputMessagesUI.PrintLengthMessageAndGetLength();
            width = InputOutputMessagesUI.PrintWidthMessageAndGetWidth();
            isAgainstComputer = InputOutputMessagesUI.PrintOpponentMessageAndGetIsAgainstComputer();

            round = new RoundLogic(length, width, isAgainstComputer);
            InputOutputMessagesUI.ClearAndPrintRound(round);
            
            return round;
        }

        public static void FullRound(RoundLogic i_Round)
        {
            bool isWinner = false;
            eStatus currentPlayer = eStatus.PlayerOne;
            bool isFullBoard = i_Round.RoundBoard.IsFullBoard();
            bool isQuit = false;

            while (!isFullBoard)
            {
                isWinner = RequestTokenToColumnAndCheckIfWinOrQuit(i_Round, currentPlayer, ref isQuit);
                
                if (isQuit)
                {
                    InputOutputMessagesUI.PrintQuitMessage(currentPlayer);
                    isWinner = true;
                    currentPlayer = i_Round.NextPlayer(currentPlayer);
                }

                if (isWinner)
                {
                    InputOutputMessagesUI.PrintWinMessage(currentPlayer);
                    i_Round.ScoreBoard.UpdateScoreBoard(currentPlayer);
                    break;
                }

                currentPlayer = i_Round.NextPlayer(currentPlayer);
                isFullBoard = i_Round.RoundBoard.IsFullBoard();
            }

            if (isFullBoard)
            {
                InputOutputMessagesUI.PrintDrawMessage();
            } 

        }

        public static void FullGame()
        {
            RoundLogic currentRound = SetUp();
            bool anotherRound = true;

            while (anotherRound)
            {
                currentRound.RoundBoard.InitializeBoard();
                InputOutputMessagesUI.ClearAndPrintRound(currentRound);
                if (currentRound.IsAgainstComputer)
                {
                    InputOutputMessagesUI.PrintComputerPlayerMessage();
                }

                FullRound(currentRound);
                InputOutputMessagesUI.PrintScoreBoard(currentRound);
                anotherRound = InputOutputMessagesUI.PrintAnotherRoundMessageAndGetIsAnotherRound();
            }

        }

    }

}