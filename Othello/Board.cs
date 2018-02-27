using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using _Othello;

namespace Othello
{
   public partial class Board : Form
   {

       #region global variable

       //global variables
       int[,] table; //row,col to set value at location
       uC[,] bt;
       int Turn = 1; //black 1, white 2
       int Computer = 0;

       //gametype enumeration to set if AI should be active
       enum GameType
       {
           PvP, EasyAI
       }
       GameType gt;

       #endregion

       #region methods
        
        public Board()
        {
            InitializeComponent();
        }

       //redraws all the pieces on the board after a move.
        public void drawing()
        {
            //int i, j;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    //choose image to show on board
                    switch (table[i, j])
                    {
                        case 0: bt[i, j].BackgroundImage = null; break;
                        case 1: bt[i, j].BackgroundImage = _Othello.Properties.Resources.black; break;
                        case 2: bt[i, j].BackgroundImage = _Othello.Properties.Resources.white; break;
                    }
                }
            //updates text score on side
            Score();
        }
 
       /* !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        * 
        * All single direction methods refer to direction the move from point selected and use same structure
        * 
        * !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        */

       //Checks for available moves to left of selected point
       private Boolean CheckHorizontalL(int turn, int i, int j)
        {
            bool result = false;
           //check if select spot is empty
            if (table[i, j] == 0)
            {
                //if in first two columns no move is possible so skip, avoid array out of bounds exception
                if (j > 1)
                {
                    //check adjacent spot is not same piece or spot is empty
                    if (table[i, j - 1] != turn && table[i, j - 1] != 0)
                        //set result so that so varify if a move is possible
                        result = true;
                }                
            }
           //adjacent piece is opponents, possibility of move.
            if (result)
            {
                //skip to 2 positions left of slected spot and iterate to edge of board
                for (int c = j - 2; c >= 0; c--)
                {
                    //if an empty space, valid move not possible, exit.
                    if (table[i, c] == 0)
                    {
                        //move not possible
                        return false;
                    }
                    //found same piece indicating a valid move is possible.
                    if (table[i, c] == turn)
                        return result;
                }
            }
           //move not possible
            return false;
        }
       //Changes pieces to left of selected point
       private Boolean ChangeHorizontalL(int turn, int i, int j)
       {
           bool result = false;
           //start at one position to left of selected point
           for (int c = j - 1; c >= 0; c--)
           {
               //iterate to same color piece
               if (table[i, c] == turn)
               {
                   //starting at left adjacent piece, swap pieces until reach your piece.
                   for (int k = j - 1; k > c; k--)
                   {
                       //set value as players turn
                       table[i, k] = turn;
                       //confirm a piece was switched
                       result = true;
                   }
                   //no more moves possible after you hit your piece, exit for loop
                   break;
               }
           }
           //return whether piece was flipped or not
           return result;
       }

       private Boolean CheckHorizontalR(int turn, int i, int j)
       {
           bool result = false;

           if (table[i, j] == 0)
           {
               if (j < 6)
               {
                   if (table[i, j + 1] != turn && table[i, j + 1] != 0)
                       result = true;
               }
           }
           if (result)
           {
               for (int c = j + 2; c < 8; c++)
               {
                   if (table[i, c] == 0)
                   {
                       return false;
                   }
                   if (table[i, c] == turn)
                       return result;
               }
           }
           return false;
        }
        //Changes pieces to right of selected point
        private Boolean ChangeHorizontalR(int turn, int i, int j)
        {
            bool result = false;
            for (int c = j + 1; c < 8; c++)
            {
                if (table[i, c] == turn)
                {
                    for (int k = j + 1; k < c; k++)
                    {
                        table[i, k] = turn;
                        result = true;
                    }
                    break;
                }
            }
            return result;
        }

        private Boolean CheckVerticalU(int turn, int i, int j)
        {
            bool result = false;

            if (table[i, j] == 0)
            {
                if (i > 1)
                {
                    if (table[i - 1, j] != turn && table[i - 1, j] != 0)
                        result = true;
                }
            }
            if (result)
            {
                for (int r = i - 2; r >= 0; r--)
                {
                    if (table[r, j] == 0)
                    {
                        return false;
                    }
                    if (table[r, j] == turn)
                        return result;
                }
            }
            return false;
        }
        //Changes pieces up from selected point
        private Boolean ChangeVerticalU(int turn, int i, int j)
        {
            bool result = false;
            for (int r = i - 1; r >= 0; r--)
            {
                if (table[r, j] == turn)
                {                
                    for (int k = i - 1; k > r; k--)
                    {
                        table[k, j] = turn;
                        result = true;
                    }
                    break;
                }                
            }
            return result;
        }

        private Boolean CheckVerticalD(int turn, int i, int j)
        {
            bool result = false;
            if (table[i, j] == 0)
            {                
                if (i < 6)
                {
                    if (table[i + 1, j] != turn && table[i + 1, j] != 0)
                        result = true;
                }
            }
            if (result)
            {
                for (int r = i + 2; r < 8; r++)
                {
                    if (table[r, j] == 0)
                    {
                        break;
                    }
                    if (table[r, j] == turn)
                        return result;
                }
            }
            return false;
        }
        //Changes pieces down from selected point
        private Boolean ChangeVerticalD(int turn, int i, int j)
        {
            bool result = false;
            for (int r = i + 1; r < 8; r++)
            {
                if (table[r, j] == turn)
                {
                    for (int k = i + 1; k < r; k++)
                    {
                        table[k, j] = turn;
                        result = true;
                    }
                    break;
                }                    
            }
            return result;
        }

        /* !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        * 
        * All diagonal direction methods refer to direction the move from point selected and use same structure
        * 
        * !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        */

        private Boolean CheckDiagonalUL(int turn, int i, int j)
        {
            bool result = false;
            //check if select spot is empty
            if (table[i, j] == 0)
            {      
                //if within first 2 rows of top/left, no move possible. skip checks. avoids array out of bounds exception
                if (i > 1 && j > 1)
                {
                    //check adjacent spot is not same piece or spot is not empty
                    if (table[i - 1, j - 1] != turn && table[i - 1, j - 1] != 0)
                        result = true;
                }
            }

            if (result)
            {
                //move 2 places from point
                int c = i - 2;
                int r = j - 2;
                //while loop instead of for since incrementing/deincrementing 2 variables at once
                //run check to edge of board
                while (c >= 0 && r >= 0)
                {
                    //empty space found no move possible
                    if (table[c, r] == 0)
                    {
                        break;
                    }
                    //same piece found, valid move possible
                    if (table[c, r] == turn)
                    {
                        return result;
                    }                        
                    c--;
                    r--;
                }
            }
            return false;
        }
        //Changes pieces to up and left of selected point
        private Boolean ChangeDiagonalUL(int turn, int i, int j)
        {
            bool result = false;
            int c, r;
            //start at adjecent to selected spot
            c = i - 1;
            r = j - 1;
            //while loop instead of for since incrementing/deincrementing 2 variables at once
            while (c >= 0 && r >= 0)
            {
                //same piece found
                if (table[c, r] == turn)
                {
                    int k = i - 1;
                    int l = j - 1;
                    //iterate from selected point to matching piece
                    while (k > c && l > r)
                    {
                        //set piece to current players turn
                        table[k, l] = turn;
                        //confirms a piece was flipped
                        result = true; 
                        k--;
                        l--;
                    }
                    break;
                }
                c--;
                r--;
            }
            //verify a piece was flipped
            return result;
        }
        
        private Boolean CheckDiagonalDR(int turn, int i, int j)
        {
            bool result = false;

            if (table[i, j] == 0)
            {
                if (i < 6 && j < 6) 
                {
                    if (table[i + 1, j + 1] != turn && table[i + 1, j + 1] != 0)
                        result = true;
                }
            }
            if (result)
            {
                int c, r;
                c = i + 2;
                r = j + 2;

                while (c < 8 && r < 8)
                {
                    if (table[c, r] == 0)
                    {
                        break;
                    }
                    if (table[c, r] == turn)
                    {
                        return result;
                    }                        
                    c++;
                    r++;
                }
            }
            return false;
        }
        //Changes pieces to down and right of selected point
        private Boolean ChangeDiagonalDR(int turn, int i, int j)
        {
            bool result = false;
            int c, r;
            c = i + 1;
            r = j + 1;

            while (c < 8 && r < 8)
            {
                if (table[c, r] == 0)
                {
                    break;
                }
                if (table[c, r] == turn )
                {
                    int k = i + 1;
                    int l = j + 1;
                    while (k < c && l < r)
                    {
                        table[k, l] = turn;
                        result = true;
                        k++;
                        l++;
                    }
                    break;
                }
                c++;
                r++;
            }
            return result;
        }

        private Boolean CheckDiagonalDL(int turn, int i, int j)
        {
            bool result = false;

            if (table[i, j] == 0)
            {
                if (i < 6 && j > 1)
                {
                    if (table[i + 1, j - 1] != turn && table[i + 1, j - 1] != 0)
                        result = true;
                }
            }

            if (result)
            {
                int c = i + 2;
                int r = j - 2;
                while (c >= 0 && r <= 7)
                {
                    if (table[r, c] == 0)
                        break;
                    if (table[r, c] == turn)
                        return result;

                    c--;
                    r++;
                }
            }
            return false;
        }
        //Changes pieces to down and left of selected point
        private Boolean ChangeDiagonalDL(int turn, int i, int j)
        {
            int c,r;
            bool result = false;

            r = i + 1;
            c = j - 1;
            
            while (r <= 7 && c >= 0)
            {
                if (table[r, c] == turn)
                {
                    int k = i + 1;
                    int l = j - 1;
                    while (k < r && l > c)
                    {
                        table[k, l] = turn;
                        result = true;
                        k++;
                        l--;
                    }
                    break;
                }
                r++;
                c--;
            }
            return result;
        }

        private Boolean CheckDiagonalUR(int turn, int i, int j)
        {
            bool result = false;

            if (table[i, j] == 0)
            {
                if (i > 1 && j < 6)
                {
                    if (table[i - 1, j + 1] != turn && table[i - 1, j + 1] != 0)
                        result = true;
                }
            }

            if (result)
            {
                int r, c;
                r = i - 2;
                c = j + 2;

                while (r >= 0 && c <= 7)
                {
                    if (table[r, c] == 0)
                        break;
                    if (table[r, c] == turn)
                        return result;
                    r--;
                    c++;
                }
            }
            return false;
        }
        //Changes pieces to up and right of selected point
       private Boolean ChangeDiagonalUR(int turn, int i, int j)
        {
            int c, r;
            bool result = false;

            r = i - 1;
            c = j + 1;
            while (r >= 0 && c <= 7)
            {
                if (table[r, c] == turn)
                {
                    int k = i - 1;
                    int l = j + 1;
                    while (k > r && l < c)
                    {
                        table[k, l] = turn;
                        result = true;
                        k--;
                        l++;
                    }
                    break;
                }
                c++;
                r--;
            }
            return result;
        }

        private void Score()
        {
            int black = 0, white = 0;
            //read board and add up score for each player
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (table[i, j] == 1)
                        black++;
                    if (table[i, j] == 2)
                        white++;
                }
            }
            textBox1.Text = "Black: " + black;
            textBox1.Text += "\r\n";
            textBox1.Text += "White: " + white;
            textBox1.Text += "\r\n";
            
            //Check if current player can make a move
            if (!CheckAvailableMove())
            {
                //No move was possible, change player and try again
                ChangeTurn();
                if (!CheckAvailableMove())
                {    
                    //no move possible, ends game.
                    textBox1.Text += "Game Over.";
                    textBox1.Text += "No available moves.\r\n";
                    textBox1.Text += "\r\n";
                    if (black == white)
                    {
                        textBox1.Text += "The game is a draw";
                    }
                    else if (black > white) 
                    {
                        textBox1.Text += "Black wins";
                    }
                    else
                    {
                        textBox1.Text += "White wins";
                    }
                }
                //move was found, notify player no move was possible for previous player and other player will be making another move    
                else
                {
                    if (Turn == 1)
                    {
                        textBox1.Text += "No Moves available for White.";
                        textBox1.Text += "\r\n";
                        textBox1.Text += "Black's turn.";
                    }
                    else
                    {
                        textBox1.Text += "No Moves available for Black";
                        textBox1.Text += "\r\n";
                        textBox1.Text += "White's turn.";
                    }
                }
            }
            //move found, show whose turn is it
            else if (Turn == 1)
                textBox1.Text += "Black's turn";
            else
                textBox1.Text += "White's turn";
            //if playing computer and their turn, AI makes move
            if (gt == GameType.EasyAI && Turn == Computer)
            {
                EasyAI();
            }
        }

        private Boolean CheckAvailableMove()
        {
            //iterate through entire board that a move can be made for current players turn
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    //Only checking that at least one move is possible. Not getting all moves.
                    if (CheckHorizontalR(Turn, i, j) || CheckVerticalU(Turn, i, j) || CheckDiagonalUR(Turn, i, j) || CheckDiagonalUL(Turn, i, j) ||
                        CheckHorizontalL(Turn, i, j) || CheckVerticalD(Turn, i, j) || CheckDiagonalDL(Turn, i, j) || CheckDiagonalDR(Turn, i, j))
                    {
                        //exit after finding 1 available move
                        return true;
                    }
                }
            }
            return false;
       }

        private void ChangeTurn()
        {
            if (Turn == 1)
                Turn = 2;
            else
                Turn = 1;
        }

       //pass coordinates and check if move can be made.
        private Boolean MakeMove(int i, int j)
        {
            bool result = false;
            try
            {
                //all check and change statements boolean, verifies that valid move was made with not changes made.
                if (CheckHorizontalR(Turn, i, j))
                {
                    if (ChangeHorizontalR(Turn, i, j))
                    {
                        result = true;
                    }
                }
                if (CheckHorizontalL(Turn, i, j))
                {
                    if (ChangeHorizontalL(Turn, i, j))
                    {
                        result = true;
                    }
                }
                if (CheckVerticalU(Turn, i, j))
                {
                    if (ChangeVerticalU(Turn, i, j))
                    {
                        result = true;
                    }
                }
                if (CheckVerticalD(Turn, i, j))
                {
                    if (ChangeVerticalD(Turn, i, j))
                    {
                        result = true;
                    }
                }
                if (CheckDiagonalDR(Turn, i, j))
                {
                    if (ChangeDiagonalDR(Turn, i, j))
                    {
                        result = true;
                    }
                }
                if (CheckDiagonalUL(Turn, i, j))
                {
                    if (ChangeDiagonalUL(Turn, i, j))
                    {
                        result = true;
                    }
                }
                if (CheckDiagonalUR(Turn, i, j))
                {
                    if (ChangeDiagonalUR(Turn, i, j))
                    {
                        result = true;
                    }
                }
                if (CheckDiagonalDL(Turn, i, j))
                {
                    if (ChangeDiagonalDL(Turn, i, j))
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //return move made or not. 
            return result;
        }

       //clean up for ending the turn
        private void FinishTurn(int i, int j)
        {
            //change the original piece selected to players turn
            table[i, j] = Turn;

            //switch players turn
            ChangeTurn();
            //update game pieces
            drawing();                            
        }

        private void EasyAI()
        {
            //list for all moves that can be made
            List<int []> Moves = new List<int []>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (CheckHorizontalR(Turn, i, j) || CheckVerticalU(Turn, i, j) || CheckDiagonalUR(Turn, i, j) || CheckDiagonalUL(Turn, i, j) ||
                        CheckHorizontalL(Turn, i, j) || CheckVerticalD(Turn, i, j) || CheckDiagonalDL(Turn, i, j) || CheckDiagonalDR(Turn, i, j))
                    {
                        //add moves in an array to the list.
                        Moves.Add(new int[2] {i ,j});
                    }
                }
            }
            //get random number to choose from list of possible moves.
            Random random = new Random();
            int randnum = random.Next(0, Moves.Count);

            //AI makes moves, equivilent to player clicking, should always have a move to make
            if (Moves.Count > 0)
            {
                MakeMove(Moves[randnum][0], Moves[randnum][1]);
                //finish AI players turn
                FinishTurn(Moves[randnum][0], Moves[randnum][1]);
            }
        }        
        #endregion

        #region Event Handlers

        void uC_Click(object sender, EventArgs e)
        {
            //bool result = false;
            int i, j;
            //get mouse position on grid
            i = (sender as uC).pozY;
            j = (sender as uC).pozX;
                      
            //if move was made switch piece selected, then players turn.
            if (MakeMove(i, j))
            {
                FinishTurn(i, j);
            }
        }

        //button action for player vs player game.
        private void btnPvp_Click(object sender, EventArgs e)
        {
            gt = GameType.PvP;
            Turn = 1;
            ClearBoard();
            CreateBoard();
        }

       //same at PvP except set different gametype
        private void btnEasyAi_Click(object sender, EventArgs e)
        {
            gt = GameType.EasyAI;

            using (Form frm = new ChooseSide())
            {
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.Yes)
                {
                    //Yes = player choose black
                    Computer = 2;
                }
                else
                {
                    Computer = 1;
                }
            }
            //black always starts
            Turn = 1;
            ClearBoard();
            CreateBoard();
        }

        private void ClearBoard()
        {
            //try statement for when running for first time to ignore error.
            try
            {            
                if (bt[0, 0] != null) 
                {
                  for (int i = 0; i < 8; i++)
                  {
                     for (int j = 0; j < 8; j++)
                     {
                        //hide previous board controls, allows new board to made and play consecative games
                        bt[i, j].Hide();
                        textBox1.Text = "";
                       }
                    }
              }
                  }
            catch {}            
        }

        private void CreateBoard()
        {
            //create array for board
            table = new int[8, 8]
            {
                {0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 2, 1, 0, 0, 0},
                {0, 0, 0, 1, 2, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 0},

               //set board to test specific scenarios
               //{2, 2, 2, 2, 2, 2, 2, 1},
               //{2, 2, 1, 0, 0, 0, 2, 2},
               //{2, 0, 2, 0, 0, 2, 0, 2},
               //{2, 0, 0, 2, 2, 0, 0, 2},
               //{2, 0, 0, 2, 2, 0, 0, 2},
               //{2, 0, 2, 0, 0, 2, 0, 2},
               //{2, 2, 0, 0, 0, 0, 2, 2},
               //{1, 2, 2, 2, 2, 2, 2, 0},
            };
            //create user control background of board to make moves
            bt = new uC[8, 8];

            //build uC array of user controls of board for drawing
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    bt[i, j] = new uC();
                    bt[i, j].Parent = this;
                    bt[i, j].Location = new Point(j * 50 + 50, i * 50 + 50);
                    bt[i, j].pozX = j;
                    bt[i, j].pozY = i;
                    bt[i, j].Size = new Size(50, 50);
                    bt[i, j].Click += new EventHandler(uC_Click);
                    if (i % 2 == 0)
                        if (j % 2 == 1)
                            bt[i, j].BackColor = Color.Green;
                        else
                            bt[i, j].BackColor = Color.LightGreen;
                    else
                        if (j % 2 == 1)
                            bt[i, j].BackColor = Color.LightGreen;
                        else
                            bt[i, j].BackColor = Color.Green;
                    bt[i, j].BackgroundImageLayout = ImageLayout.Center;
                }
            }
            drawing();
        }
        #endregion
    }
        
}
