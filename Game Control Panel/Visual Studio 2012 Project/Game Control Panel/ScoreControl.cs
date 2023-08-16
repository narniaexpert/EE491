using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics; //For Debug statement

//This class will read from the score file and keep track of score. The directories and files are defined by constant
//strings. The file interpreter is designed to look for line where the string PlayerX occurs twice. The X is a number.
//The first PlayerX is the player who fired their lazer blaster and the second is the player who recieved the hit.

namespace Game_Control_Panel
{
    public partial class ScoreControl : UserControl
    {
        private Translation Translator = new Translation();
        private int numberOfLives = 6; //Number of lives in the game. Game default 6, but changed when the game starts Valid values 1-6 inclusive
        private int Player1ScoreValue = 0;
        private int Player2ScoreValue = 0;
        private int Player3ScoreValue = 0;
        private int Player4ScoreValue = 0;
        private int Player1LivesCount = 6; //Game default 6, but changed to numberOfLives value in updateGame()
        private int Player2LivesCount = 6; //Game default 6, but changed to numberOfLives value in updateGame()
        private int Player3LivesCount = 6; //Game default 6, but changed to numberOfLives value in updateGame()
        private int Player4LivesCount = 6; //Game default 6, but changed to numberOfLives value in updateGame()
        private const int HITBONUS = 100; //The number of points gained from a successful shot
        private const int DEATHDEDUCTION = 25; //The number of points lost from a player's death
        private int NumberOfTeams = 0; //Defualt value 0, but changed as user changes form settings Valid values are 0, 2, 3, and 4
        public const string SCOREFILEDIRECTORY = "c://Robotag";//This is not translated because the Game Server relies on this constant file path
        public const string SCOREFILENAME = "GameScores.txt";//This is not translated because the Game Server relies on this constant file path
        public const string ARCHIVEDIRECTORY = "Archives";//This is not translated because the Game Server relies on this constant file path
        StreamReader Stream = null;
        Regex expression = new Regex("Player\\d|player\\d|Player \\d|player \\d|Team\\d|team\\d|Team \\d|team \\d|选手 \\d|选手\\d|队 \\d|队\\d|Jugador \\d|Jugador\\d|jugador \\d|jugador\\d|Equipo \\d|Equipo\\d|equipo \\d|equipo\\d"); //Keyword scanning for is PlayerX, TeamX, 选手X, or 队X where X is a number. These forms will also respond if the first letter is not capitalized or if there is a space between the word and X.
        String fileText; //Used to store the contents of the file after reading in updating game or appending text to the file.
        //The next set of variables are used to append the output showing how many times each player has been shot each of their opponents.
        private int Player1ShotsFromPlayer2 = 0;
        private int Player1ShotsFromPlayer3 = 0;
        private int Player1ShotsFromPlayer4 = 0;
        private int Player2ShotsFromPlayer1 = 0;
        private int Player2ShotsFromPlayer3 = 0;
        private int Player2ShotsFromPlayer4 = 0;
        private int Player3ShotsFromPlayer1 = 0;
        private int Player3ShotsFromPlayer2 = 0;
        private int Player3ShotsFromPlayer4 = 0;
        private int Player4ShotsFromPlayer1 = 0;
        private int Player4ShotsFromPlayer2 = 0;
        private int Player4ShotsFromPlayer3 = 0;
        public ScoreControl()
        {
            InitializeComponent();
        }
        public void setNumberOfLives(int lives)
        {
            if (lives >= 0 && lives <= 6)
            {
                numberOfLives = lives;
            }
        }
        public int getNumberOfLives()
        {
            return numberOfLives;
        }
        public void setNumberOfTeams(int teams)
        {
            if (teams >= 0 && teams <= 4)
            {
                NumberOfTeams = teams;
            }
            Debug.WriteLineIf(!(teams >= 0 && teams <= 4), "Attempt to set NumberOfTeams stopped because it is not in the valid range.");
            
            switch (NumberOfTeams)
            {
                case 2:
                    //Players 1 and 2 are not forced to a visible state because they are never hidden
                    Player3Label.Visible=false;
                    Player3Lives.Visible=false;
                    Player3Score.Visible=false;
                    Player4Label.Visible=false;
                    Player4Lives.Visible=false;
                    Player4Score.Visible=false;
                    Player1Label.Text =  Translator.GetWord(Translation.WORDS.Team1);
                    Player2Label.Text =  Translator.GetWord(Translation.WORDS.Team2);
                    Player3Label.Text =  Translator.GetWord(Translation.WORDS.Team3);
                    Player4Label.Text =  Translator.GetWord(Translation.WORDS.Team4);
                    break;
                case 3:
                    //Players 1 and 2 are not forced to a visible state because they are never hidden
                    Player3Label.Visible = true;
                    Player3Lives.Visible = true;
                    Player3Score.Visible = true;
                    Player4Label.Visible = false;
                    Player4Lives.Visible = false;
                    Player4Score.Visible = false;
                    Player1Label.Text =  Translator.GetWord(Translation.WORDS.Team1);
                    Player2Label.Text =  Translator.GetWord(Translation.WORDS.Team2);
                    Player3Label.Text =  Translator.GetWord(Translation.WORDS.Team3);
                    Player4Label.Text =  Translator.GetWord(Translation.WORDS.Team4);
                    break;
                case 4:
                    //Players 1 and 2 are not forced to a visible state because they are never hidden
                    Player3Label.Visible = true;
                    Player3Lives.Visible = true;
                    Player3Score.Visible = true;
                    Player4Label.Visible = true;
                    Player4Lives.Visible = true;
                    Player4Score.Visible = true;
                    Player1Label.Text =  Translator.GetWord(Translation.WORDS.Team1);
                    Player2Label.Text =  Translator.GetWord(Translation.WORDS.Team2);
                    Player3Label.Text =  Translator.GetWord(Translation.WORDS.Team3);
                    Player4Label.Text =  Translator.GetWord(Translation.WORDS.Team4);
                    break;
                default:
                case 0:
                    //Players 1 and 2 are not forced to a visible state because they are never hidden
                    Player3Label.Visible = true;
                    Player3Lives.Visible = true;
                    Player3Score.Visible = true;
                    Player4Label.Visible = true;
                    Player4Lives.Visible = true;
                    Player4Score.Visible = true;
                    Player1Label.Text =  Translator.GetWord(Translation.WORDS.Player1);
                    Player2Label.Text =  Translator.GetWord(Translation.WORDS.Player2);
                    Player3Label.Text =  Translator.GetWord(Translation.WORDS.Player3);
                    Player4Label.Text =  Translator.GetWord(Translation.WORDS.Player4);
                    break;
            }
        }
        public int getNumberOfTeams()
        {
            return NumberOfTeams;
        }
        public void StartGame(int lives, string message)
        {
            setNumberOfLives(lives);

            StreamWriter WriteAccess = null;
            if (!Directory.Exists(SCOREFILEDIRECTORY))
            {
                Directory.CreateDirectory(SCOREFILEDIRECTORY);
            }
            if (!File.Exists(SCOREFILEDIRECTORY + "\\" + SCOREFILENAME))
            {
                File.CreateText(SCOREFILEDIRECTORY + "\\" + SCOREFILENAME).Close();
            }
            try
            {
                using (WriteAccess = new StreamWriter(SCOREFILEDIRECTORY + "\\" + SCOREFILENAME))
                {
                    string[] MessageLines = message.Split('\n');
                    for (int i = 0; i < MessageLines.Length; i++)
                    {
                        WriteAccess.WriteLine(MessageLines[i]); //Outputs game start header
                    }
                    WriteAccess.WriteLine(); //adds blank line for readability
                }
            }
            catch (IOException)
            {
                MessageBox.Show(Translator.GetWord(Translation.WORDS.ErrorWritingToFile), Translator.GetWord(Translation.WORDS.FileError), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            WriteAccess.Close(); //Close write access to file
            updateGame();
        }
        public void updateGame()
        {
            StreamWriter WriteAccess = null;
            if (!Directory.Exists(SCOREFILEDIRECTORY))
            {
                Directory.CreateDirectory(SCOREFILEDIRECTORY);
            }
            if (!File.Exists(SCOREFILEDIRECTORY + "\\" + SCOREFILENAME))
            {
                File.CreateText(SCOREFILEDIRECTORY + "\\" + SCOREFILENAME).Close();
                try
                {
                    using (WriteAccess = new StreamWriter(SCOREFILEDIRECTORY + "\\" + SCOREFILENAME))
                    {
                        WriteAccess.WriteLine("{0}\t{1}", DateTime.Now.ToLongTimeString(), Translator.GetWord(Translation.WORDS.FileNotFoundErrorMessage)); 
                        WriteAccess.Close();
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show(Translator.GetWord(Translation.WORDS.ErrorWritingToFile), Translator.GetWord(Translation.WORDS.FileError), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            try
            {
                using (Stream = new StreamReader(SCOREFILEDIRECTORY + "\\" + SCOREFILENAME))
                {
                    fileText = Stream.ReadToEnd();
                }
            }
            catch (IOException)
            {
                MessageBox.Show(Translator.GetWord(Translation.WORDS.ErrorReadingFromFile), Translator.GetWord(Translation.WORDS.FileError), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Stream.Close();
            string[] fileLines = fileText.Split('\n'); //Used for looking at each line individually
            Match hitData;
            int shootingPlayer;
            int playerHit;
            //Starts reading with the full values of the scores and lives
            Player1ScoreValue = 0;
            Player2ScoreValue = 0;
            Player3ScoreValue = 0;
            Player4ScoreValue = 0;
            Player1LivesCount = numberOfLives;
            Player2LivesCount = numberOfLives;
            Player3LivesCount = numberOfLives;
            Player4LivesCount = numberOfLives;
            Player1ShotsFromPlayer2 = 0;
            Player1ShotsFromPlayer3 = 0;
            Player1ShotsFromPlayer4 = 0;
            Player2ShotsFromPlayer1 = 0;
            Player2ShotsFromPlayer3 = 0;
            Player2ShotsFromPlayer4 = 0;
            Player3ShotsFromPlayer1 = 0;
            Player3ShotsFromPlayer2 = 0;
            Player3ShotsFromPlayer4 = 0;
            Player4ShotsFromPlayer1 = 0;
            Player4ShotsFromPlayer2 = 0;
            Player4ShotsFromPlayer3 = 0;
            for (int i = 0; i < fileLines.Length; i++)
			{
                if (expression.Matches(fileLines[i]).Count==2) //Each line with a hit should have two epressions of PlayerX
                {
                    hitData = expression.Match(fileLines[i]);
                    int firstMatchIndex = hitData.Index;
                    int firstMatchLength = hitData.Length;
                    shootingPlayer = Convert.ToInt32(Regex.Match(hitData.ToString(), @"\d").ToString()); //Get the int form of the shooting player's Player number
                    hitData=hitData.NextMatch(); //The next match is the player who was hit
                    int secondMatchIndex=hitData.Index;
                    int secondMatchLength = hitData.Length;
                    playerHit = Convert.ToInt32(Regex.Match(hitData.ToString(), @"\d").ToString());//Get the int form of the recieving player's Player number
                    if (NumberOfTeams == 0)
                    {
                        if (shootingPlayer >= 1 && shootingPlayer <= 4 && playerHit >= 1 && playerHit <= 4) //Valid values for individual game
                        {
                            ScoreHit(shootingPlayer, playerHit);
                        }
                    }
                    else
                    {
                        if (shootingPlayer >= 1 && shootingPlayer <= NumberOfTeams && playerHit >= 1 && playerHit <= NumberOfTeams) //Valid values for individual game
                        {
                            ScoreHit(shootingPlayer, playerHit);
                        }
                    }
                    //Reformat the line for better readability if nessicary. These steps are performed in a particular order becuase the changes to the string could also change where the index references
                    if (secondMatchIndex + secondMatchLength != fileLines[i].Length)//if there is anything following the end of the line
                    {
                        fileLines[i] = fileLines[i].Remove(secondMatchIndex + secondMatchLength);//Remove anything following the second match
                        fileLines[i] = fileLines[i] + "\r";//Adds the return character back in place so notepad can display a new line (notepad.exe looks for \r\n)
                    }
                    fileLines[i]=fileLines[i].Remove(firstMatchIndex+firstMatchLength,secondMatchIndex-(firstMatchIndex+firstMatchLength)); //Remove anything between the two PlayerX style keywords
                    fileLines[i]=fileLines[i].Insert(firstMatchIndex + firstMatchLength, Translator.GetWord(Translation.WORDS.Shot)); //Insert the word shot to form "PlayerX shot PlayerY"
                    if (firstMatchIndex == 0)
                    {
                        fileLines[i] = DateTime.Now.ToLongTimeString() + "\t" + fileLines[i];//If there is nothing preceeding the PlayerX style keyword, add a timestamp to the line.
                    }
                }
            } 
            fileText = string.Join("\n", fileLines);//Rejoin the string array back into one string

            //Sets all references to the player names into the correct form for the Team/Individual setting and the language of choice
            fileText = fileText.Replace("Player1", Player1Label.Text);
            fileText = fileText.Replace("player1", Player1Label.Text);
            fileText = fileText.Replace("Player 1", Player1Label.Text);
            fileText = fileText.Replace("player 1", Player1Label.Text);
            fileText = fileText.Replace("Team1", Player1Label.Text);
            fileText = fileText.Replace("team1", Player1Label.Text);
            fileText = fileText.Replace("Team 1", Player1Label.Text);
            fileText = fileText.Replace("team 1", Player1Label.Text);
            fileText = fileText.Replace("选手 1", Player1Label.Text);
            fileText = fileText.Replace("选手1", Player1Label.Text);
            fileText = fileText.Replace("队 1", Player1Label.Text);
            fileText = fileText.Replace("队1", Player1Label.Text);
            fileText = fileText.Replace("Jugador1", Player1Label.Text);
            fileText = fileText.Replace("jugador1", Player1Label.Text);
            fileText = fileText.Replace("Jugador 1", Player1Label.Text);
            fileText = fileText.Replace("jugador 1", Player1Label.Text);
            fileText = fileText.Replace("Equipo1", Player1Label.Text);
            fileText = fileText.Replace("equipo1", Player1Label.Text);
            fileText = fileText.Replace("Equipo 1", Player1Label.Text);
            fileText = fileText.Replace("equipo 1", Player1Label.Text);

            fileText = fileText.Replace("Player2", Player2Label.Text);
            fileText = fileText.Replace("player2", Player2Label.Text);
            fileText = fileText.Replace("Player 2", Player2Label.Text);
            fileText = fileText.Replace("player 2", Player2Label.Text);
            fileText = fileText.Replace("Team2", Player2Label.Text);
            fileText = fileText.Replace("team2", Player2Label.Text);
            fileText = fileText.Replace("Team 2", Player2Label.Text);
            fileText = fileText.Replace("team 2", Player2Label.Text);
            fileText = fileText.Replace("选手 2", Player2Label.Text);
            fileText = fileText.Replace("选手2", Player2Label.Text);
            fileText = fileText.Replace("队 2", Player2Label.Text);
            fileText = fileText.Replace("队2", Player2Label.Text);
            fileText = fileText.Replace("Jugador2", Player2Label.Text);
            fileText = fileText.Replace("jugador2", Player2Label.Text);
            fileText = fileText.Replace("Jugador 2", Player2Label.Text);
            fileText = fileText.Replace("jugador 2", Player2Label.Text);
            fileText = fileText.Replace("Equipo2", Player2Label.Text);
            fileText = fileText.Replace("equipo2", Player2Label.Text);
            fileText = fileText.Replace("Equipo 2", Player2Label.Text);
            fileText = fileText.Replace("equipo 2", Player2Label.Text);

            fileText = fileText.Replace("Player3", Player3Label.Text);
            fileText = fileText.Replace("player3", Player3Label.Text);
            fileText = fileText.Replace("Player 3", Player3Label.Text);
            fileText = fileText.Replace("player 3", Player3Label.Text);
            fileText = fileText.Replace("Team3", Player3Label.Text);
            fileText = fileText.Replace("team3", Player3Label.Text);
            fileText = fileText.Replace("Team 3", Player3Label.Text);
            fileText = fileText.Replace("team 3", Player3Label.Text);
            fileText = fileText.Replace("选手 3", Player3Label.Text);
            fileText = fileText.Replace("选手3", Player3Label.Text);
            fileText = fileText.Replace("队 3", Player3Label.Text);
            fileText = fileText.Replace("队3", Player3Label.Text);
            fileText = fileText.Replace("Jugador3", Player3Label.Text);
            fileText = fileText.Replace("jugador3", Player3Label.Text);
            fileText = fileText.Replace("Jugador 3", Player3Label.Text);
            fileText = fileText.Replace("jugador 3", Player3Label.Text);
            fileText = fileText.Replace("Equipo3", Player3Label.Text);
            fileText = fileText.Replace("equipo3", Player3Label.Text);
            fileText = fileText.Replace("Equipo 3", Player3Label.Text);
            fileText = fileText.Replace("equipo 3", Player3Label.Text);

            fileText = fileText.Replace("Player4", Player4Label.Text);
            fileText = fileText.Replace("player4", Player4Label.Text);
            fileText = fileText.Replace("Player 4", Player4Label.Text);
            fileText = fileText.Replace("player 4", Player4Label.Text);
            fileText = fileText.Replace("Team4", Player4Label.Text);
            fileText = fileText.Replace("team4", Player4Label.Text);
            fileText = fileText.Replace("Team 4", Player4Label.Text);
            fileText = fileText.Replace("team 4", Player4Label.Text);
            fileText = fileText.Replace("选手 4", Player4Label.Text);
            fileText = fileText.Replace("选手4", Player4Label.Text);
            fileText = fileText.Replace("队 4", Player4Label.Text);
            fileText = fileText.Replace("队4", Player4Label.Text);
            fileText = fileText.Replace("Jugador4", Player4Label.Text);
            fileText = fileText.Replace("jugador4", Player4Label.Text);
            fileText = fileText.Replace("Jugador 4", Player4Label.Text);
            fileText = fileText.Replace("jugador 4", Player4Label.Text);
            fileText = fileText.Replace("Equipo4", Player4Label.Text);
            fileText = fileText.Replace("equipo4", Player4Label.Text);
            fileText = fileText.Replace("Equipo 4", Player4Label.Text);
            fileText = fileText.Replace("equipo 4", Player4Label.Text);


            //Write the reformatted text back to the file
            if (!Directory.Exists(SCOREFILEDIRECTORY))
            {
                Directory.CreateDirectory(SCOREFILEDIRECTORY);
            }
            if (!File.Exists(SCOREFILEDIRECTORY + "\\" + SCOREFILENAME))
            {
                File.CreateText(SCOREFILEDIRECTORY + "\\" + SCOREFILENAME).Close();
            }
            try
            {
                using (WriteAccess = new StreamWriter(SCOREFILEDIRECTORY + "\\" + SCOREFILENAME))
                {
                    WriteAccess.Write(fileText); //Outputs file for the updated file contents joined together into one string
                }
            }
            catch (IOException)
            {
                MessageBox.Show(Translator.GetWord(Translation.WORDS.ErrorWritingToFile), Translator.GetWord(Translation.WORDS.FileError), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            WriteAccess.Close(); //Close write access to file

            //Update the GameScores window and joining file lines into one string
            GameScores.Text = fileText;

            //Begins code to set all life indicators
            //This is mathimatically equivalent to (Player1LivesCount/numberOfLives)*100, but does not result in remainders
            Player1Lives.Value = (100 * Player1LivesCount) / numberOfLives;
            Player2Lives.Value = (100 * Player2LivesCount) / numberOfLives;
            Player3Lives.Value = (100 * Player3LivesCount) / numberOfLives;
            Player4Lives.Value = (100 * Player4LivesCount) / numberOfLives;
            //Ends code to set all life indicators

            //Begins code to set all score labels
            Player1Score.Text = Translator.GetWord(Translation.WORDS.Points) + " " + Player1ScoreValue.ToString();
            Player2Score.Text = Translator.GetWord(Translation.WORDS.Points) + " " + Player2ScoreValue.ToString();
            Player3Score.Text = Translator.GetWord(Translation.WORDS.Points) + " " + Player3ScoreValue.ToString();
            Player4Score.Text = Translator.GetWord(Translation.WORDS.Points) + " " + Player4ScoreValue.ToString();
            //Ends code to set all score labels
        }
        public bool OnePlayerRemaining() //Returns true if there is only one player alvie so that the game can end
        {
            switch (NumberOfTeams)
            {
                case 2:
                    Player3LivesCount = 0;
                    Player4LivesCount = 0;
                    break;
                case 3:
                    Player4LivesCount = 0;
                    break;
                case 4:
                case 0:
                default:
                    break;
            }
            if ((Player1LivesCount == 0 && Player2LivesCount == 0 && Player3LivesCount == 0 && Player4LivesCount != 0) || (Player1LivesCount == 0 && Player2LivesCount == 0 && Player3LivesCount != 0 && Player4LivesCount == 0) || (Player1LivesCount == 0 && Player2LivesCount != 0 && Player3LivesCount == 0 && Player4LivesCount == 0) || (Player1LivesCount != 0 && Player2LivesCount == 0 && Player3LivesCount == 0 && Player4LivesCount == 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void ScoreHit(int shootingPlayer, int playerHit)
        {
            //These switches and comparisons are made to adjust the score and lives as each hit is made.
            //These cases are made to ignore inaccurate records like:
            //      A player or team can not shoot another player if they are dead.
            //      A player or team can to be shot if they are dead.
            //      A player or team can not shoot themselves.
            //      A player or team can not have negative points.
            //      A player or team can not have negative lives.
            //A successful hit will increase the player's score by a constant HITBONUS
            //A death will result in a deduction of points based on a constant DEATHDEDUCTION, but the score can not go
            //negative.
            //
            //Warning! This function will only respond to valid values of player ID numbers (1-4 inclusive). Check the
            //validity of the numbers before calling this function.
            if (shootingPlayer != playerHit) //Ignore if a player hits themselves
            {
                switch (playerHit)
                {
                    case 1:
                        if (Player1ScoreValue > 0 + DEATHDEDUCTION && Player1LivesCount > 0) //If player is alive and has points to lose, deduct points
                        {
                            switch (shootingPlayer)
                            {
                                case 2:
                                    if (Player2LivesCount > 0) //Shooting player must be alive for the hit to count
                                    {
                                        Player1ScoreValue -= DEATHDEDUCTION;
                                    }
                                    break;
                                case 3:
                                    if (Player3LivesCount > 0)
                                    {
                                        Player1ScoreValue -= DEATHDEDUCTION;
                                    }
                                    break;
                                case 4:
                                    if (Player4LivesCount > 0)
                                    {
                                        Player1ScoreValue -= DEATHDEDUCTION;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            //If the player has less points than DEATHDEDUCTION, then the score will go to zero to
                            //prevent negative scores.
                            switch (shootingPlayer)
                            {
                                case 2:
                                    if (Player2LivesCount > 0)
                                    {
                                        Player1ScoreValue = 0;
                                    }
                                    break;
                                case 3:
                                    if (Player3LivesCount > 0)
                                    {
                                        Player1ScoreValue = 0;
                                    }
                                    break;
                                case 4:
                                    if (Player4LivesCount > 0)
                                    {
                                        Player1ScoreValue = 0;
                                    }
                                    break;
                            }
                        }
                        if (Player1LivesCount > 0) //If shooting player is alive and player shot is alive, award points and deduct a life.
                        {
                            switch (shootingPlayer)
                            {
                                case 2:
                                    if (Player2LivesCount > 0)
                                    {
                                        Player1LivesCount--;
                                        Player2ScoreValue += HITBONUS;
                                        Player1ShotsFromPlayer2++;
                                    }
                                    break;
                                case 3:
                                    if (Player3LivesCount > 0)
                                    {
                                        Player1LivesCount--;
                                        Player3ScoreValue += HITBONUS;
                                        Player1ShotsFromPlayer3++;
                                    }
                                    break;
                                case 4:
                                    if (Player4LivesCount > 0)
                                    {
                                        Player1LivesCount--;
                                        Player4ScoreValue += HITBONUS;
                                        Player1ShotsFromPlayer4++;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Player1LivesCount = 0; //If the number of lives is not greater than 0, it must be equal to 0.
                        }
                        break;
                    case 2:
                        if (Player2ScoreValue > 0 + DEATHDEDUCTION && Player2LivesCount > 0)
                        {
                            switch (shootingPlayer)
                            {
                                case 1:
                                    if (Player1LivesCount > 0)
                                    {
                                        Player2ScoreValue -= DEATHDEDUCTION;
                                    }
                                    break;
                                case 3:
                                    if (Player3LivesCount > 0)
                                    {
                                        Player2ScoreValue -= DEATHDEDUCTION;
                                    }
                                    break;
                                case 4:
                                    if (Player4LivesCount > 0)
                                    {
                                        Player2ScoreValue -= DEATHDEDUCTION;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (shootingPlayer)
                            {
                                case 1:
                                    if (Player1LivesCount > 0)
                                    {
                                        Player2ScoreValue = 0;
                                    }
                                    break;
                                case 3:
                                    if (Player3LivesCount > 0)
                                    {
                                        Player2ScoreValue = 0;
                                    }
                                    break;
                                case 4:
                                    if (Player4LivesCount > 0)
                                    {
                                        Player2ScoreValue = 0;
                                    }
                                    break;
                            }
                        }
                        if (Player2LivesCount > 0)
                        {
                            switch (shootingPlayer)
                            {
                                case 1:
                                    if (Player1LivesCount > 0)
                                    {
                                        Player1ScoreValue += HITBONUS;
                                        Player2LivesCount--;
                                        Player2ShotsFromPlayer1++;
                                    }
                                    break;
                                case 3:
                                    if (Player3LivesCount > 0)
                                    {
                                        Player3ScoreValue += HITBONUS;
                                        Player2LivesCount--;
                                        Player2ShotsFromPlayer3++;
                                    }
                                    break;
                                case 4:
                                    if (Player4LivesCount > 0)
                                    {
                                        Player4ScoreValue += HITBONUS;
                                        Player2LivesCount--;
                                        Player2ShotsFromPlayer4++;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Player2LivesCount = 0;
                        }
                        break;
                    case 3:
                        if (Player3ScoreValue > 0 + DEATHDEDUCTION && Player3LivesCount > 0)
                        {
                            switch (shootingPlayer)
                            {
                                case 1:
                                    if (Player1LivesCount > 0)
                                    {
                                        Player3ScoreValue -= DEATHDEDUCTION;
                                    }
                                    break;
                                case 2:
                                    if (Player2LivesCount > 0)
                                    {
                                        Player3ScoreValue -= DEATHDEDUCTION;
                                    }
                                    break;
                                case 4:
                                    if (Player4LivesCount > 0)
                                    {
                                        Player3ScoreValue -= DEATHDEDUCTION;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (shootingPlayer)
                            {
                                case 1:
                                    if (Player1LivesCount > 0)
                                    {
                                        Player3ScoreValue = 0;
                                    }
                                    break;
                                case 2:
                                    if (Player2LivesCount > 0)
                                    {
                                        Player3ScoreValue = 0;
                                    }
                                    break;
                                case 4:
                                    if (Player4LivesCount > 0)
                                    {
                                        Player3ScoreValue = 0;
                                    }
                                    break;
                            }
                        }
                        if (Player3LivesCount > 0)
                        {
                            switch (shootingPlayer)
                            {
                                case 1:
                                    if (Player1LivesCount > 0)
                                    {
                                        Player1ScoreValue += HITBONUS;
                                        Player3LivesCount--;
                                        Player3ShotsFromPlayer1++;
                                    }
                                    break;
                                case 2:
                                    if (Player2LivesCount > 0)
                                    {
                                        Player2ScoreValue += HITBONUS;
                                        Player3LivesCount--;
                                        Player3ShotsFromPlayer2++;
                                    }
                                    break;
                                case 4:
                                    if (Player4LivesCount > 0)
                                    {
                                        Player4ScoreValue += HITBONUS;
                                        Player3LivesCount--;
                                        Player3ShotsFromPlayer4++;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Player3LivesCount = 0;
                        }
                        break;
                    case 4:
                        if (Player4ScoreValue > 0 + DEATHDEDUCTION && Player4LivesCount > 0)
                        {
                            switch (shootingPlayer)
                            {
                                case 1:
                                    if (Player1LivesCount > 0)
                                    {
                                        Player4ScoreValue -= DEATHDEDUCTION;
                                    }
                                    break;
                                case 2:
                                    if (Player2LivesCount > 0)
                                    {
                                        Player4ScoreValue -= DEATHDEDUCTION;
                                    }
                                    break;
                                case 3:
                                    if (Player3LivesCount > 0)
                                    {
                                        Player4ScoreValue -= DEATHDEDUCTION;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (shootingPlayer)
                            {
                                case 1:
                                    if (Player1LivesCount > 0)
                                    {
                                        Player4ScoreValue = 0;
                                    }
                                    break;
                                case 2:
                                    if (Player2LivesCount > 0)
                                    {
                                        Player4ScoreValue = 0;
                                    }
                                    break;
                                case 3:
                                    if (Player3LivesCount > 0)
                                    {
                                        Player4ScoreValue = 0;
                                    }
                                    break;
                            }
                        }
                        if (Player4LivesCount > 0)
                        {
                            switch (shootingPlayer)
                            {
                                case 1:
                                    if (Player1LivesCount > 0)
                                    {
                                        Player1ScoreValue += HITBONUS;
                                        Player4LivesCount--;
                                        Player4ShotsFromPlayer1++;
                                    }
                                    break;
                                case 2:
                                    if (Player2LivesCount > 0)
                                    {
                                        Player2ScoreValue += HITBONUS;
                                        Player4LivesCount--;
                                        Player4ShotsFromPlayer2++;
                                    }
                                    break;
                                case 3:
                                    if (Player3LivesCount > 0)
                                    {
                                        Player3ScoreValue += HITBONUS;
                                        Player4LivesCount--;
                                        Player4ShotsFromPlayer3++;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Player4LivesCount = 0;
                        }
                        break;
                }
            }
        }
        public void AppendResults() //Appends the game scores to the file
        {
            if (File.Exists(SCOREFILEDIRECTORY + "\\" + SCOREFILENAME))
            {
                StreamWriter WriteAccess = null;
                StreamReader ReadAccess = null;
                try
                {
                    using (ReadAccess = new StreamReader(ScoreControl.SCOREFILEDIRECTORY + "\\" + ScoreControl.SCOREFILENAME))
                    {
                        fileText = ReadAccess.ReadToEnd();
                        ReadAccess.Close();
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show(Translator.GetWord(Translation.WORDS.ErrorReadingFromFile), Translator.GetWord(Translation.WORDS.FileError), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    using (WriteAccess = new StreamWriter(SCOREFILEDIRECTORY + "\\" + SCOREFILENAME))
                    {
                        WriteAccess.Write(fileText);
                        WriteAccess.WriteLine();
                        WriteAccess.WriteLine(Translator.GetWord(Translation.WORDS.FinalGameScores));
                        WriteAccess.WriteLine();
                        WriteAccess.WriteLine(Player1Label.Text);
                        WriteAccess.WriteLine("\t" + Player1Score.Text);
                        WriteAccess.WriteLine("\t" + Translator.NumberOfLives(Player1LivesCount));//Prints a string with the number of lives in the propper language and singular/plural form
                        if (Player1ShotsFromPlayer2 > 0)
                        {
                            WriteAccess.WriteLine("\t" + Translator.ShotsFromPlayerX(Player2Label.Text, Player1ShotsFromPlayer2));
                        }
                        if (Player1ShotsFromPlayer3 > 0)
                        {
                            WriteAccess.WriteLine("\t" + Translator.ShotsFromPlayerX(Player3Label.Text, Player1ShotsFromPlayer3));
                        }
                        if (Player1ShotsFromPlayer4 > 0)
                        {
                            WriteAccess.WriteLine("\t" + Translator.ShotsFromPlayerX(Player4Label.Text, Player1ShotsFromPlayer4));
                        }
                        WriteAccess.WriteLine();

                        WriteAccess.WriteLine(Player2Label.Text);
                        WriteAccess.WriteLine("\t" + Player2Score.Text);
                        WriteAccess.WriteLine("\t" + Translator.NumberOfLives(Player2LivesCount));//Prints a string with the number of lives in the propper language and singular/plural form
                        if (Player2ShotsFromPlayer1 > 0)
                        {
                            WriteAccess.WriteLine("\t" + Translator.ShotsFromPlayerX(Player1Label.Text, Player2ShotsFromPlayer1));
                        }
                        if (Player2ShotsFromPlayer3 > 0)
                        {
                            WriteAccess.WriteLine("\t" + Translator.ShotsFromPlayerX(Player3Label.Text, Player2ShotsFromPlayer3));
                        }
                        if (Player2ShotsFromPlayer4 > 0)
                        {
                            WriteAccess.WriteLine("\t" + Translator.ShotsFromPlayerX(Player4Label.Text, Player2ShotsFromPlayer4));
                        }
                        WriteAccess.WriteLine();

                        if (NumberOfTeams == 3 || NumberOfTeams == 4 || NumberOfTeams == 0)
                        {
                            WriteAccess.WriteLine(Player3Label.Text);
                            WriteAccess.WriteLine("\t" + Player3Score.Text);
                            WriteAccess.WriteLine("\t" + Translator.NumberOfLives(Player3LivesCount));//Prints a string with the number of lives in the propper language and singular/plural form
                            if (Player3ShotsFromPlayer1 > 0)
                            {
                                WriteAccess.WriteLine("\t" + Translator.ShotsFromPlayerX(Player1Label.Text, Player3ShotsFromPlayer1));
                            }
                            if (Player3ShotsFromPlayer2 > 0)
                            {
                                WriteAccess.WriteLine("\t" + Translator.ShotsFromPlayerX(Player2Label.Text, Player3ShotsFromPlayer2));
                            }
                            if (Player3ShotsFromPlayer4 > 0)
                            {
                                WriteAccess.WriteLine("\t" + Translator.ShotsFromPlayerX(Player4Label.Text, Player3ShotsFromPlayer4));
                            }
                            WriteAccess.WriteLine();

                        }
                        if (NumberOfTeams == 4 || NumberOfTeams == 0)
                        {
                            WriteAccess.WriteLine(Player4Label.Text);
                            WriteAccess.WriteLine("\t" + Player4Score.Text);
                            WriteAccess.WriteLine("\t" + Translator.NumberOfLives(Player4LivesCount));//Prints a string with the number of lives in the propper language and singular/plural form
                            if (Player4ShotsFromPlayer1 > 0)
                            {
                                WriteAccess.WriteLine("\t" + Translator.ShotsFromPlayerX(Player1Label.Text, Player4ShotsFromPlayer1));
                            }
                            if (Player4ShotsFromPlayer2 > 0)
                            {
                                WriteAccess.WriteLine("\t" + Translator.ShotsFromPlayerX(Player2Label.Text, Player4ShotsFromPlayer2));
                            }
                            if (Player4ShotsFromPlayer3 > 0)
                            {
                                WriteAccess.WriteLine("\t" + Translator.ShotsFromPlayerX(Player3Label.Text, Player4ShotsFromPlayer3));
                            }
                            WriteAccess.WriteLine();
                        }
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show(Translator.GetWord(Translation.WORDS.ErrorWritingToFile), Translator.GetWord(Translation.WORDS.FileError), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                WriteAccess.Close(); //Close read access to file
            }
        }
        public void ResetGame() //Called to clear all game data
        {
            if (File.Exists(SCOREFILEDIRECTORY + "\\" + SCOREFILENAME))
            {
                AppendResults();
                if (!Directory.Exists(SCOREFILEDIRECTORY + "\\" + ARCHIVEDIRECTORY))
                {
                    Directory.CreateDirectory(SCOREFILEDIRECTORY + "\\" + ARCHIVEDIRECTORY);
                }
                DateTime GameStartTime = File.GetCreationTime(SCOREFILEDIRECTORY + "\\" + SCOREFILENAME);
                string GameStartTimeString = GameStartTime.ToString();
                GameStartTimeString = GameStartTimeString.Replace("/", ".");
                GameStartTimeString = GameStartTimeString.Replace(":", "_");
                GameStartTimeString += " ";
                int duplicateFileNameCounter = 1;
                while (File.Exists(SCOREFILEDIRECTORY + "\\" + ARCHIVEDIRECTORY + "\\Robotag Game " + GameStartTimeString + ".txt"))
                {//If this filename is already in use, try appending a number to the end.
                    GameStartTimeString = GameStartTimeString.TrimEnd(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' });
                    GameStartTimeString += duplicateFileNameCounter.ToString();
                    duplicateFileNameCounter++;
                }
                File.Move(SCOREFILEDIRECTORY + "\\" + SCOREFILENAME, SCOREFILEDIRECTORY + "\\" + ARCHIVEDIRECTORY + "\\Robotag Game " + GameStartTimeString + ".txt");
            }
            //Reset to all default values
            Player1ScoreValue = 0;
            Player2ScoreValue = 0;
            Player3ScoreValue = 0;
            Player4ScoreValue = 0;
            Player1Score.Text = 0 + " " +Translator.GetWord(Translation.WORDS.Points);
            Player2Score.Text = 0 + " " + Translator.GetWord(Translation.WORDS.Points);
            Player3Score.Text = 0 + " " + Translator.GetWord(Translation.WORDS.Points);
            Player4Score.Text = 0 + " " + Translator.GetWord(Translation.WORDS.Points);
            Player1Lives.Value = 0;
            Player2Lives.Value = 0;
            Player3Lives.Value = 0;
            Player4Lives.Value = 0;
            Player1Lives.Value = 0;
            Player2Lives.Value = 0;
            Player3Lives.Value = 0;
            Player4Lives.Value = 0;
            Player1ShotsFromPlayer2 = 0;
            Player1ShotsFromPlayer3 = 0;
            Player1ShotsFromPlayer4 = 0;
            Player2ShotsFromPlayer1 = 0;
            Player2ShotsFromPlayer3 = 0;
            Player2ShotsFromPlayer4 = 0;
            Player3ShotsFromPlayer1 = 0;
            Player3ShotsFromPlayer2 = 0;
            Player3ShotsFromPlayer4 = 0;
            Player4ShotsFromPlayer1 = 0;
            Player4ShotsFromPlayer2 = 0;
            Player4ShotsFromPlayer3 = 0;
            GameScores.Text = "";
        }

        public void updateLanguage()
        {
            GameScoresLabel.Text = Translator.GetWord(Translation.WORDS.GameScores);
            setNumberOfTeams(NumberOfTeams);//To refresh the team and player labels.
            //Begins code to refresh all score labels
            Player1Score.Text = Player1ScoreValue.ToString() + " " + Translator.GetWord(Translation.WORDS.Points);
            Player2Score.Text = Player2ScoreValue.ToString() + " " + Translator.GetWord(Translation.WORDS.Points);
            Player3Score.Text = Player3ScoreValue.ToString() + " " + Translator.GetWord(Translation.WORDS.Points);
            Player4Score.Text = Player4ScoreValue.ToString() + " " + Translator.GetWord(Translation.WORDS.Points);
            //Ends code to refresh all score labels

            if (File.Exists(SCOREFILEDIRECTORY + "\\" + SCOREFILENAME))
            {
                StreamWriter WriteAccess = null;
                StreamReader ReadAccess = null;
                try
                {
                    using (ReadAccess = new StreamReader(ScoreControl.SCOREFILEDIRECTORY + "\\" + ScoreControl.SCOREFILENAME))
                    {
                        fileText = ReadAccess.ReadToEnd();
                        ReadAccess.Close();
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show(Translator.GetWord(Translation.WORDS.ErrorReadingFromFile), Translator.GetWord(Translation.WORDS.ErrorWritingToFile), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    using (WriteAccess = new StreamWriter(SCOREFILEDIRECTORY + "\\" + SCOREFILENAME))
                    {
                        //The order of the replace statements is designed to avoid replacing strings that could be found as substrings in other strings until the end.
                        //For example the Chinese translation for 10 Minutes could be found within the string for 60 Minutes resulting in an erronious conversion if the replacement for 10 Minutes came first
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.RobotagGame), Translator.GetWord(Translation.WORDS.RobotagGame));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.GameControlPanelVersion), Translator.GetWord(Translation.WORDS.GameControlPanelVersion));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.GameStarted), Translator.GetWord(Translation.WORDS.GameStarted));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.GameSettings), Translator.GetWord(Translation.WORDS.GameSettings));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.OneLife), Translator.GetWord(Translation.WORDS.OneLife));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.TwoLives), Translator.GetWord(Translation.WORDS.TwoLives));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.ThreeLives), Translator.GetWord(Translation.WORDS.ThreeLives));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.FourLives), Translator.GetWord(Translation.WORDS.FourLives));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.FiveLives), Translator.GetWord(Translation.WORDS.FiveLives));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.SixLives), Translator.GetWord(Translation.WORDS.SixLives));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.OneMinute), Translator.GetWord(Translation.WORDS.OneMinute));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.TwoMinutes), Translator.GetWord(Translation.WORDS.TwoMinutes));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.ThreeMinutes), Translator.GetWord(Translation.WORDS.ThreeMinutes));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.FourMinutes), Translator.GetWord(Translation.WORDS.FourMinutes));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.SixMinutes), Translator.GetWord(Translation.WORDS.SixMinutes));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.SevenMinutes), Translator.GetWord(Translation.WORDS.SevenMinutes));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.EightMinutes), Translator.GetWord(Translation.WORDS.EightMinutes));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.NineMinutes), Translator.GetWord(Translation.WORDS.NineMinutes));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.FifteenMinutes), Translator.GetWord(Translation.WORDS.FifteenMinutes));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.TwentyMinutes), Translator.GetWord(Translation.WORDS.TwentyMinutes));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.TwentyFiveMinutes), Translator.GetWord(Translation.WORDS.TwentyFiveMinutes));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.ThirtyMinutes), Translator.GetWord(Translation.WORDS.ThirtyMinutes));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.ThirtyFiveMinutes), Translator.GetWord(Translation.WORDS.ThirtyFiveMinutes));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.FourtyMinutes), Translator.GetWord(Translation.WORDS.FourtyMinutes));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.FourtyFiveMinutes), Translator.GetWord(Translation.WORDS.FourtyFiveMinutes));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.FiftyMinutes), Translator.GetWord(Translation.WORDS.FiftyMinutes));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.FiftyFiveMinutes), Translator.GetWord(Translation.WORDS.FiftyFiveMinutes));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.SixtyMinutes), Translator.GetWord(Translation.WORDS.SixtyMinutes));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.STARTKEYWORD), Translator.GetWord(Translation.WORDS.STARTKEYWORD));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.RESUMEKEYWORD), Translator.GetWord(Translation.WORDS.RESUMEKEYWORD));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.ENDKEYWORDBecauseOnlyOnePlayerIsRemaining), "ENDKEYWORDBecauseOnlyOnePlayerIsRemaining"); //Conterting to a dummy variable because this string will always contain the ENDKEYWORD
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.ENDKEYWORD), Translator.GetWord(Translation.WORDS.ENDKEYWORD)); //Placed near the end of the list because in Chinese this is a substring for some of the preceeding strings
                        fileText = fileText.Replace("ENDKEYWORDBecauseOnlyOnePlayerIsRemaining", Translator.GetWord(Translation.WORDS.ENDKEYWORDBecauseOnlyOnePlayerIsRemaining)); //Now that all of the ENDKEYWORD substrings have been translated, the dummy variable can be translated
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.FiveMinutes), Translator.GetWord(Translation.WORDS.FiveMinutes)); //Placed near the end of the list because in Chinese this is a substring for some of the preceeding strings
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.TenMinutes), Translator.GetWord(Translation.WORDS.TenMinutes)); //Placed near the end of the list because in Chinese this is a substring for some of the preceeding strings
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.IndividualGame), Translator.GetWord(Translation.WORDS.IndividualGame));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.TeamGame), Translator.GetWord(Translation.WORDS.TeamGame)); //Placed near the end of the list because in Chinese this is a substring for some of the preceeding strings
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.Shot), Translator.GetWord(Translation.WORDS.Shot));
                        fileText = fileText.Replace(Translator.GetPreviousLanguageWord(Translation.WORDS.FileNotFoundErrorMessage), Translator.GetWord(Translation.WORDS.FileNotFoundErrorMessage));

                        //Sets all references to the player names into the correct form for the Team/Individual setting and the language of choice
                        fileText = fileText.Replace("Player1", Player1Label.Text);
                        fileText = fileText.Replace("player1", Player1Label.Text);
                        fileText = fileText.Replace("Player 1", Player1Label.Text);
                        fileText = fileText.Replace("player 1", Player1Label.Text);
                        fileText = fileText.Replace("Team1", Player1Label.Text);
                        fileText = fileText.Replace("team1", Player1Label.Text);
                        fileText = fileText.Replace("Team 1", Player1Label.Text);
                        fileText = fileText.Replace("team 1", Player1Label.Text);
                        fileText = fileText.Replace("选手 1", Player1Label.Text);
                        fileText = fileText.Replace("选手1", Player1Label.Text);
                        fileText = fileText.Replace("队 1", Player1Label.Text);
                        fileText = fileText.Replace("队1", Player1Label.Text);
                        fileText = fileText.Replace("Jugador1", Player1Label.Text);
                        fileText = fileText.Replace("jugador1", Player1Label.Text);
                        fileText = fileText.Replace("Jugador 1", Player1Label.Text);
                        fileText = fileText.Replace("jugador 1", Player1Label.Text);
                        fileText = fileText.Replace("Equipo1", Player1Label.Text);
                        fileText = fileText.Replace("equipo1", Player1Label.Text);
                        fileText = fileText.Replace("Equipo 1", Player1Label.Text);
                        fileText = fileText.Replace("equipo 1", Player1Label.Text);

                        fileText = fileText.Replace("Player2", Player2Label.Text);
                        fileText = fileText.Replace("player2", Player2Label.Text);
                        fileText = fileText.Replace("Player 2", Player2Label.Text);
                        fileText = fileText.Replace("player 2", Player2Label.Text);
                        fileText = fileText.Replace("Team2", Player2Label.Text);
                        fileText = fileText.Replace("team2", Player2Label.Text);
                        fileText = fileText.Replace("Team 2", Player2Label.Text);
                        fileText = fileText.Replace("team 2", Player2Label.Text);
                        fileText = fileText.Replace("选手 2", Player2Label.Text);
                        fileText = fileText.Replace("选手2", Player2Label.Text);
                        fileText = fileText.Replace("队 2", Player2Label.Text);
                        fileText = fileText.Replace("队2", Player2Label.Text);
                        fileText = fileText.Replace("Jugador2", Player2Label.Text);
                        fileText = fileText.Replace("jugador2", Player2Label.Text);
                        fileText = fileText.Replace("Jugador 2", Player2Label.Text);
                        fileText = fileText.Replace("jugador 2", Player2Label.Text);
                        fileText = fileText.Replace("Equipo2", Player2Label.Text);
                        fileText = fileText.Replace("equipo2", Player2Label.Text);
                        fileText = fileText.Replace("Equipo 2", Player2Label.Text);
                        fileText = fileText.Replace("equipo 2", Player2Label.Text);

                        fileText = fileText.Replace("Player3", Player3Label.Text);
                        fileText = fileText.Replace("player3", Player3Label.Text);
                        fileText = fileText.Replace("Player 3", Player3Label.Text);
                        fileText = fileText.Replace("player 3", Player3Label.Text);
                        fileText = fileText.Replace("Team3", Player3Label.Text);
                        fileText = fileText.Replace("team3", Player3Label.Text);
                        fileText = fileText.Replace("Team 3", Player3Label.Text);
                        fileText = fileText.Replace("team 3", Player3Label.Text);
                        fileText = fileText.Replace("选手 3", Player3Label.Text);
                        fileText = fileText.Replace("选手3", Player3Label.Text);
                        fileText = fileText.Replace("队 3", Player3Label.Text);
                        fileText = fileText.Replace("队3", Player3Label.Text);
                        fileText = fileText.Replace("Jugador3", Player3Label.Text);
                        fileText = fileText.Replace("jugador3", Player3Label.Text);
                        fileText = fileText.Replace("Jugador 3", Player3Label.Text);
                        fileText = fileText.Replace("jugador 3", Player3Label.Text);
                        fileText = fileText.Replace("Equipo3", Player3Label.Text);
                        fileText = fileText.Replace("equipo3", Player3Label.Text);
                        fileText = fileText.Replace("Equipo 3", Player3Label.Text);
                        fileText = fileText.Replace("equipo 3", Player3Label.Text);

                        fileText = fileText.Replace("Player4", Player4Label.Text);
                        fileText = fileText.Replace("player4", Player4Label.Text);
                        fileText = fileText.Replace("Player 4", Player4Label.Text);
                        fileText = fileText.Replace("player 4", Player4Label.Text);
                        fileText = fileText.Replace("Team4", Player4Label.Text);
                        fileText = fileText.Replace("team4", Player4Label.Text);
                        fileText = fileText.Replace("Team 4", Player4Label.Text);
                        fileText = fileText.Replace("team 4", Player4Label.Text);
                        fileText = fileText.Replace("选手 4", Player4Label.Text);
                        fileText = fileText.Replace("选手4", Player4Label.Text);
                        fileText = fileText.Replace("队 4", Player4Label.Text);
                        fileText = fileText.Replace("队4", Player4Label.Text);
                        fileText = fileText.Replace("Jugador4", Player4Label.Text);
                        fileText = fileText.Replace("jugador4", Player4Label.Text);
                        fileText = fileText.Replace("Jugador 4", Player4Label.Text);
                        fileText = fileText.Replace("jugador 4", Player4Label.Text);
                        fileText = fileText.Replace("Equipo4", Player4Label.Text);
                        fileText = fileText.Replace("equipo4", Player4Label.Text);
                        fileText = fileText.Replace("Equipo 4", Player4Label.Text);
                        fileText = fileText.Replace("equipo 4", Player4Label.Text);

                        WriteAccess.Write(fileText);
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show(Translator.GetWord(Translation.WORDS.ErrorWritingToFile), Translator.GetWord(Translation.WORDS.FileError), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                WriteAccess.Close(); //Close read access to file
                GameScores.Text = fileText;
            }
        }
    }
}
