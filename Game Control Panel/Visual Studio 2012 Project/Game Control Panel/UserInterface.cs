using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Game_Control_Panel
{
    public partial class UserInterface : Form
    {
        const string VERSION = "1.2";//Constant string identifying the current program version. This is not translated because digits are universally recognized
        private int NumberOfLives = 6;
        private int GameLength = 5; //measured in minutes
        private int NumberOfTeams = 0; //Valid values are 0, 2, 3, and 4
        private Timer gameTimer = new Timer();
        public const string STARTKEYWORD = "START GAME WITH LIVES: ";//Keyword is only availible in English because it is needed to be constant for communication with Game Server
        public const string RESUMEKEYWORD = "RESUME GAME";//Keyword is only availible in English because it is needed to be constant for communication with Game Server
        public const string ENDKEYWORD = "END GAME";//Keyword is only availible in English because it is needed to be constant for communication with Game Server
        Thread timerThread;
        Thread ScoreKeeperThread;
        private Translation Translator = new Translation();
        private Form Robot1VideoForm = new RobotVideo("http://192.168.2.153:8080/javascript_simple.html");//This URL is defined for Robot1
        private Form Robot2VideoForm = new RobotVideo("http://192.168.2.145:8080/javascript_simple.html");//This URL is defined for Robot2
        
        public UserInterface()
        {
            InitializeComponent();
            //Begin setting tool tips for controls
            toolTip.SetToolTip(NumberOfLivesComboBox, Translator.GetWord(Translation.WORDS.NumberOfLivesComboBoxToolTip));
            toolTip.SetToolTip(GameLengthComboBox, Translator.GetWord(Translation.WORDS.GameLengthComboBoxToolTip));
            toolTip.SetToolTip(IndividualGameRadioButton, Translator.GetWord(Translation.WORDS.IndividualGameRadioButtonToolTip));
            toolTip.SetToolTip(TeamGameRadioButton, Translator.GetWord(Translation.WORDS.TeamGameRadioButtonToolTip));
            toolTip.SetToolTip(NumberOfTeamsComboBox, Translator.GetWord(Translation.WORDS.NumberOfTeamsComboBoxToolTip));
            toolTip.SetToolTip(StartButton, Translator.GetWord(Translation.WORDS.StartButtonToolTip));
            toolTip.SetToolTip(EStopButton, Translator.GetWord(Translation.WORDS.EStopButtonToolTip));
            //End setting tool tips for controls
            ResetGame(); //Sets default game values
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            if (EStopButton.Enabled)//The game can be resumed when the EStop is enabled. The EStop is disabled when the game is in a fresh start
            {
                //Resume Game

                //Begin code to resume timer
                gameTimer.startTimer();
                if (timerThread.IsAlive)
                {
                    timerThread.Abort();
                }
                timerThread = new Thread(new ThreadStart(updateTimerLabel)); //Sets up a seperate thread
                timerThread.Name = "Timer Thread";//This name is not visible to the user and is therefore not translated
                timerThread.Start(); //Updates the timer in a sperate thread so other things can be done while the clock counts down
                //Ends code to resume timer

                //Begin code to resume scorekeeper
                if (ScoreKeeperThread.IsAlive)
                {
                    ScoreKeeperThread.Abort();
                }
                ScoreKeeperThread = new Thread(new ThreadStart(updateGameData));
                ScoreKeeperThread.Name = "ScoreKeeper Thread";//This name is not visible to the user and is therefore not translated
                ScoreKeeperThread.Start();
                //End code to resume scorekeeper

                //Begin changing Start/Stop button settings
                EStopButton.Enabled = true; //Enabling Emergency Stop button
                EStopButton.Text = Translator.GetWord(Translation.WORDS.EmergencyStop);
                StartButton.Enabled = false; //Disabling Start button because while game is running you can not start another game.
                //End changing Start/Stop button settings

                //Starts code to write RESUMEKEYWORD
                WriteKeyword(Translator.GetWord(Translation.WORDS.RESUMEKEYWORD));
                //Ends code to write RESUMEKEYWORD
            }
            else
            {
                //Start New Game

                //Begins code to start timer
                gameTimer.setMinutes(GameLength);
                gameTimer.startTimer();
                timerThread = new Thread(new ThreadStart(updateTimerLabel)); //Sets up a seperate thread
                timerThread.Name = "Timer Thread";//This name is not visible to the user and is therefore not translated
                timerThread.Start(); //Updates the timer in a sperate thread so other things can be done while the clock counts down
                //Ends code to start timer


                //Begin disabling setting controls
                GameSettingsLabel.Enabled = false;
                NumberOfLivesLabel.Enabled = false;
                NumberOfLivesComboBox.Enabled = false;
                GameLengthLabel.Enabled = false;
                GameLengthComboBox.Enabled = false;
                IndividualGameRadioButton.Enabled = false;
                TeamGameRadioButton.Enabled = false;
                NumberOfLivesLabel.Enabled = false;
                NumberOfTeamsComboBox.Enabled = false;
                //End disabling setting controls

                //Begin changing Start/Stop button settings
                EStopButton.Enabled = true; //Enabling Emergency Stop button
                StartButton.Enabled = false; //Disabling Start button because while game is running you can not start another game.
                StartButton.Text = Translator.GetWord(Translation.WORDS.ResumeGame); //Change Start button to Resume Button
                //End changing Start/Stop button settings

                //Begins code to start scorekeeper
                string message = Translator.GetWord(Translation.WORDS.RobotagGame) + "\n";
                message = message + Translator.GetWord(Translation.WORDS.GameControlPanelVersion) + " " + VERSION + "\n\n";
                message = message + Translator.GetWord(Translation.WORDS.GameStarted) + ": " + DateTime.Now.ToString() + "\n";
                message = message + Translator.GetWord(Translation.WORDS.GameSettings) + ":\n\t";
                message = message + NumberOfLivesComboBox.Text + "\n\t";
                message = message + GameLengthComboBox.Text + "\n\t";
                if (TeamGameRadioButton.Checked)
                {
                    message = message + TeamGameRadioButton.Text;
                }
                else
                {
                    message = message + IndividualGameRadioButton.Text;
                }
                Scorekeeper.Enabled = true;
                Scorekeeper.StartGame(NumberOfLives, message); //Tells the scorekeeping object to start the game will a set number of lives
                ScoreKeeperThread = new Thread(new ThreadStart(updateGameData)); //Runs in seperate thread so that other controls can be used while the game file is scanned
                ScoreKeeperThread.Name = "ScoreKeeper Thread";//This name is not visible to the user and is therefore not translated
                ScoreKeeperThread.Start();
                //End code to start scorekeeper

                //Starts code to write STARTGAMEKEYWORD
                WriteKeyword(Translator.GetWord(Translation.WORDS.STARTKEYWORD) + NumberOfLives.ToString());
                //Ends code to write STARTGAMEKEYWORD
            }
        }

        private void updateGameData()
        {
            DateTime FileLastAccessed=System.IO.File.GetCreationTime(ScoreControl.SCOREFILEDIRECTORY + "\\" + ScoreControl.SCOREFILENAME);
            while (gameTimer.getTimerIsRunning())
            {
                if (DateTime.Compare(FileLastAccessed, System.IO.File.GetLastWriteTime(ScoreControl.SCOREFILEDIRECTORY + "\\" + ScoreControl.SCOREFILENAME)) < 0 || !File.Exists(ScoreControl.SCOREFILEDIRECTORY + "\\" + ScoreControl.SCOREFILENAME)) //Only scans the file if it has changed since it was last accessed. Will also run if the file goes missing
                {
                    if (Scorekeeper.InvokeRequired) //if the thread acessing the object is not the same as the thread that created the text label
                    {
                        Scorekeeper.Invoke((MethodInvoker)(() => Scorekeeper.updateGame())); //Runs this command as if it was running in the parent thread
                        //Invoke command referenced from http://tinyurl.com/m6nz8n8
                    }
                    else
                    {
                        Scorekeeper.updateGame();
                    }
                    FileLastAccessed = DateTime.Now; //Update the veriable to the current time that the file was accessed
                    if (Scorekeeper.OnePlayerRemaining()) //If only one player is alive the game should end.
                    {
                        WriteKeyword(Translator.GetWord(Translation.WORDS.ENDKEYWORDBecauseOnlyOnePlayerIsRemaining));
                        //Then reload the file so that the GameScores textbox will show the ENDKEYWORD
                        if (Scorekeeper.InvokeRequired) //if the thread acessing the object is not the same as the thread that created the text label
                        {
                            Scorekeeper.Invoke((MethodInvoker)(() => Scorekeeper.updateGame())); //Runs this command as if it was running in the parent thread
                            //Invoke command referenced from http://tinyurl.com/m6nz8n8
                        }
                        else
                        {
                            Scorekeeper.updateGame();
                        }
                        timerThread.Abort(); //End the timer because the game is over. Must be manually stopped to prevent duplicate timer threads being created while one thread is in sleep mode.
                        gameTimer.stopTimer();
                        if (EStopButton.InvokeRequired)//if the thread acessing the button is not the same as the thread that created the text label
                        {
                            EStopButton.Invoke((MethodInvoker)(() => EStopButton.Text = Translator.GetWord(Translation.WORDS.RestGame))); //Runs this command as if it was running in the parent thread
                            //Invoke command referenced from http://tinyurl.com/m6nz8n8
                        }
                        else
                        {
                            EStopButton.Text = Translator.GetWord(Translation.WORDS.RestGame); //Change E-Stop button to a reset game button.
                        }
                        break;
                    }
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }

        private void updateTimerLabel()
        {
            gameTimer.updateLabel(TimerLabel);
            if (gameTimer.timerExpired()) //If the timer completed full time without being canceled prematurely.
            {
                if (EStopButton.InvokeRequired) //if the thread acessing the text label is not the same as the thread that created the text label
                {
                    EStopButton.Invoke((MethodInvoker)(() => EStopButton.Text = Translator.GetWord(Translation.WORDS.RestGame))); //Runs this command as if it was running in the parent thread
                    //Invoke command referenced from http://tinyurl.com/m6nz8n8
                }
                else
                {
                    EStopButton.Text = Translator.GetWord(Translation.WORDS.RestGame); //Change E-Stop button to a reset game button.
                }
                ScoreKeeperThread.Abort(); //Manually abort this thread to prevent duplicate threads if a new thread is started while this thread is sleeping.
                WriteKeyword(Translator.GetWord(Translation.WORDS.ENDKEYWORD)); //Signal to the web server that the game has ended.
                //Refresh the score results to show that the key word was written to the file.
                if (EStopButton.InvokeRequired) //if the thread acessing the text label is not the same as the thread that created the text label
                {
                    EStopButton.Invoke((MethodInvoker)(() => Scorekeeper.updateGame()));
                }
                else
                {
                    Scorekeeper.updateGame();
                }
            }
        }

        private void EStopButton_Click(object sender, EventArgs e)
        {
            if ((StartButton.Enabled && gameTimer.timerExpired() == false) || (StartButton.Enabled == false && gameTimer.timerExpired()) || Scorekeeper.OnePlayerRemaining()) //If the start button is enabled then the game can be reset. The game can also be reset when the game is over (one player remaing or time expired).
            {
                ResetGame();
            }
            else //This is the Emergency Stop branch for if a game is stoped that is already in progress
            {
                gameTimer.stopTimer();
                timerThread.Abort();
                ScoreKeeperThread.Abort();
                StartButton.Enabled = true; //Reactivating the start/resume button
                EStopButton.Text = Translator.GetWord(Translation.WORDS.RestGame); //Change E-Stop button to a reset game button.
                //Starts code to write ENDEKEYWORD
                WriteKeyword(Translator.GetWord(Translation.WORDS.ENDKEYWORD));
                Scorekeeper.updateGame(); //Refresh the game's score so that the end keyword is availible.
                //Ends code to write ENDKEYWORD
            }
        }

        private void ResetGame()
        {
            Scorekeeper.ResetGame();
            Scorekeeper.Enabled = false;
            NumberOfLivesComboBox.SelectedIndex = 5;
            GameLengthComboBox.SelectedIndex = 4;
            EStopButton.Enabled = false; //Disabled because while the game is running there is nothing to stop
            EStopButton.Text = Translator.GetWord(Translation.WORDS.EmergencyStop); //Changes text from Reset Game to Emergency Stop
            StartButton.Text = Translator.GetWord(Translation.WORDS.Start); //Changes text from Resume Game to Start
            gameTimer.ClearTimer();
            TimerLabel.Text = gameTimer.ToString();
            IndividualGameRadioButton.Checked = true;
            TeamGameRadioButton.Checked = false;

            //Begin enabling setting controls
            StartButton.Enabled = true;
            GameSettingsLabel.Enabled = true;
            NumberOfLivesLabel.Enabled = true;
            NumberOfLivesComboBox.Enabled = true;
            GameLengthLabel.Enabled = true;
            GameLengthComboBox.Enabled = true;
            IndividualGameRadioButton.Enabled = true;
            TeamGameRadioButton.Enabled = true;
            NumberOfLivesLabel.Enabled = true;
            NumberOfTeamsComboBox.Enabled = true;
            //End enabling setting controls
        }

        private void NumberOfLivesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            NumberOfLives = NumberOfLivesComboBox.SelectedIndex + 1; //adding 1 because we started counting from 1 instead of from 0
        }

        private void GameLengthComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GameLengthComboBox.SelectedIndex < 10) //The first 10 items count sequentally from 1-10
            {
                GameLength = GameLengthComboBox.SelectedIndex + 1; //adding 1 because we started counting from 1 instead of from 0
            }
            else
            {
                GameLength = (GameLengthComboBox.SelectedIndex - 7) * 5; //After item 10 the items count by fives from 15 to 60
            }
        }

        private void NumberOfTeamsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IndividualGameRadioButton.Checked)
            {
                NumberOfTeams = 0;
            }
            else
            {
                NumberOfTeams = NumberOfTeamsComboBox.SelectedIndex + 2; //adding 2 because we started counting from 2 instead of from 0

            }
            Scorekeeper.setNumberOfTeams(NumberOfTeams);
        }

        private void TeamRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (TeamGameRadioButton.Checked)
            {
                NumberOfTeamsLabel.Visible = true;
                NumberOfTeamsComboBox.Visible = true;
                NumberOfTeamsComboBox.SelectedIndex = 0; //Sets the default number of teams as 2
            }
            else
            {
                NumberOfTeams = 0;
                NumberOfTeamsLabel.Visible = false;
                NumberOfTeamsComboBox.Visible = false;
            }
            NumberOfTeamsComboBox_SelectedIndexChanged(sender, e);//This function will also change the labels from Team to Player or Player to Team
        }

        private void WriteKeyword(string KEYWORD)
        {
            string FileText= null;
            StreamReader ReadAccess = null;
            StreamWriter WriteAccess = null;
            if (!Directory.Exists(ScoreControl.SCOREFILEDIRECTORY))
            {
                Directory.CreateDirectory(ScoreControl.SCOREFILEDIRECTORY);
            }
            if (!File.Exists(ScoreControl.SCOREFILEDIRECTORY + "\\" + ScoreControl.SCOREFILENAME))
            {
                File.CreateText(ScoreControl.SCOREFILEDIRECTORY + "\\" + ScoreControl.SCOREFILENAME).Close();
                try
                {
                    using (WriteAccess = new StreamWriter(ScoreControl.SCOREFILEDIRECTORY + "\\" + ScoreControl.SCOREFILENAME))
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
                using (ReadAccess = new StreamReader(ScoreControl.SCOREFILEDIRECTORY + "\\" + ScoreControl.SCOREFILENAME))
                {
                    FileText=ReadAccess.ReadToEnd();
                    ReadAccess.Close();
                }
            }
            catch (IOException)
            {
                MessageBox.Show(Translator.GetWord(Translation.WORDS.ErrorReadingFromFile), Translator.GetWord(Translation.WORDS.FileError), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                using (WriteAccess = new StreamWriter(ScoreControl.SCOREFILEDIRECTORY + "\\" + ScoreControl.SCOREFILENAME))
                {
                    WriteAccess.Write(FileText);
                    if (FileText.ElementAt(FileText.Length-1) != '\n')
                    {
                        WriteAccess.WriteLine();
                    }
                    WriteAccess.WriteLine("{0}\t{1}", DateTime.Now.ToLongTimeString(), KEYWORD);
                    Thread.Sleep(100);//Keeps the keyword in place for 100 miliseconds to ensure that the Game Server has time to read the keyword from the file.
                    WriteAccess.Close();
                }
            }
            catch (IOException)
            {
                MessageBox.Show(Translator.GetWord(Translation.WORDS.ErrorReadingFromFile), Translator.GetWord(Translation.WORDS.FileError), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string VersionText = Translator.GetWord(Translation.WORDS.GameControlPanelVersion) + " " + VERSION;
            MessageBox.Show(VersionText, Translator.GetWord(Translation.WORDS.About), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void openHelpFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "\\GameControlPanelHelp.chm");
        }

        private void viewHelpWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.davidsutton.cn/robotag/help");
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Translator.SetLanguage(Translation.LANGUAGES.English);
            updateLanguage();
        }

        private void chineseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Translator.SetLanguage(Translation.LANGUAGES.Chinese);
            updateLanguage();
        }

        private void espanolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Translator.SetLanguage(Translation.LANGUAGES.Spanish);
            updateLanguage();
        }

        private void updateLanguage()
        {
            //
            // GameSettingsLabel
            //
            this.GameSettingsLabel.Text = Translator.GetWord(Translation.WORDS.GameSettings);
            //
            // NumberOfLivesLabel
            //
            this.NumberOfLivesLabel.Text = Translator.GetWord(Translation.WORDS.NumberOfLives);
            //
            // NumberOfLivesComboBOx
            //
            int NumberOfLivesComboBoxIndex = this.NumberOfLivesComboBox.SelectedIndex;
            this.NumberOfLivesComboBox.Items.Clear();
            this.NumberOfLivesComboBox.Items.AddRange(new object[] {
            Translator.GetWord(Translation.WORDS.OneLife),
            Translator.GetWord(Translation.WORDS.TwoLives),
            Translator.GetWord(Translation.WORDS.ThreeLives),
            Translator.GetWord(Translation.WORDS.FourLives),
            Translator.GetWord(Translation.WORDS.FiveLives),
            Translator.GetWord(Translation.WORDS.SixLives)});
            this.NumberOfLivesComboBox.SelectedIndex = NumberOfLivesComboBoxIndex;
            // 
            // GameLengthLabel
            // 
            this.GameLengthLabel.Text = Translator.GetWord(Translation.WORDS.GameLength);
            // 
            // GameLengthComboBox
            // 
            int GameLenghtComboBoxIndex = this.GameLengthComboBox.SelectedIndex;
            this.GameLengthComboBox.Items.Clear();
            this.GameLengthComboBox.Items.AddRange(new object[] {
            Translator.GetWord(Translation.WORDS.OneMinute),
            Translator.GetWord(Translation.WORDS.TwoMinutes),
            Translator.GetWord(Translation.WORDS.ThreeMinutes),
            Translator.GetWord(Translation.WORDS.FourMinutes),
            Translator.GetWord(Translation.WORDS.FiveMinutes),
            Translator.GetWord(Translation.WORDS.SixMinutes),
            Translator.GetWord(Translation.WORDS.SevenMinutes),
            Translator.GetWord(Translation.WORDS.EightMinutes),
            Translator.GetWord(Translation.WORDS.NineMinutes),
            Translator.GetWord(Translation.WORDS.TenMinutes),
            Translator.GetWord(Translation.WORDS.FifteenMinutes),
            Translator.GetWord(Translation.WORDS.TwentyMinutes),
            Translator.GetWord(Translation.WORDS.TwentyFiveMinutes),
            Translator.GetWord(Translation.WORDS.ThirtyMinutes),
            Translator.GetWord(Translation.WORDS.ThirtyFiveMinutes),
            Translator.GetWord(Translation.WORDS.FourtyMinutes),
            Translator.GetWord(Translation.WORDS.FourtyFiveMinutes),
            Translator.GetWord(Translation.WORDS.FiftyMinutes),
            Translator.GetWord(Translation.WORDS.FiftyFiveMinutes),
            Translator.GetWord(Translation.WORDS.SixtyMinutes)});
            this.GameLengthComboBox.SelectedIndex = GameLenghtComboBoxIndex;
            // 
            // StartButton
            // 
            if (EStopButton.Enabled)//The game can be resumed when the EStop is enabled. The EStop is disabled when the game is in a fresh start
            {
                this.StartButton.Text = Translator.GetWord(Translation.WORDS.ResumeGame);
            }
            else
            {
                this.StartButton.Text = Translator.GetWord(Translation.WORDS.Start);
            }
            // 
            // EStopButton
            // 
            if ((StartButton.Enabled && gameTimer.timerExpired()==false) || (StartButton.Enabled==false && gameTimer.timerExpired()) || Scorekeeper.OnePlayerRemaining()) //If the start button is enabled then the game can be reset. The game can also be reset when the game is over (one player remaing or time expired).
            {
                this.EStopButton.Text = Translator.GetWord(Translation.WORDS.RestGame);
            }
            else
            {
                this.EStopButton.Text = Translator.GetWord(Translation.WORDS.EmergencyStop);
            }
            // 
            // TimeRemainingLabel
            // 
            this.TimeRemainingLabel.Text = Translator.GetWord(Translation.WORDS.TimeRemaining);
            // 
            // IndividualGameRadioButton
            // 
            this.IndividualGameRadioButton.Text = Translator.GetWord(Translation.WORDS.IndividualGame);
            // 
            // TeamGameRadioButton
            // 
            this.TeamGameRadioButton.Text = Translator.GetWord(Translation.WORDS.TeamGame);
            // 
            // NumberOfTeamsLabel
            // 
            this.NumberOfTeamsLabel.Text = Translator.GetWord(Translation.WORDS.NumberOfTeams);
            // 
            // NumberOfTeamsComboBox
            // 
            int NumberOfTeamsComboBoxIndex = this.NumberOfTeamsComboBox.SelectedIndex;
            this.NumberOfTeamsComboBox.Items.Clear();
            this.NumberOfTeamsComboBox.Items.AddRange(new object[] {
            Translator.GetWord(Translation.WORDS.TwoTeams),
            Translator.GetWord(Translation.WORDS.ThreeTeams),
            Translator.GetWord(Translation.WORDS.FourTeams)});
            this.NumberOfTeamsComboBox.SelectedIndex = NumberOfTeamsComboBoxIndex;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Text = Translator.GetWord(Translation.WORDS.File);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Text = Translator.GetWord(Translation.WORDS.About);
            // 
            // languagesToolStripMenuItem
            // 
            this.languagesToolStripMenuItem.Text = Translator.GetWord(Translation.WORDS.Languages);
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Text = Translator.GetWord(Translation.WORDS.English);
            // 
            // chineseToolStripMenuItem
            // 
            this.chineseToolStripMenuItem.Text = Translator.GetWord(Translation.WORDS.Chinese);
            // 
            // espanolToolStripMenuItem
            // 
            this.espanolToolStripMenuItem.Text = Translator.GetWord(Translation.WORDS.Spanish);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Text = Translator.GetWord(Translation.WORDS.Help);
            // 
            // openHelpFileToolStripMenuItem
            // 
            this.openHelpFileToolStripMenuItem.Text = Translator.GetWord(Translation.WORDS.OpenHelpFile);
            // 
            // viewHelpWebsiteToolStripMenuItem
            // 
            this.viewHelpWebsiteToolStripMenuItem.Text = Translator.GetWord(Translation.WORDS.ViewHelpWebsite);
            // 
            // UserInterface
            // 
            this.Text = Translator.GetWord(Translation.WORDS.GameControlPanel);
            //
            // ScoreControl
            //
            Scorekeeper.updateLanguage();
            //
            // Update ToolTips
            //
            toolTip.SetToolTip(NumberOfLivesComboBox, Translator.GetWord(Translation.WORDS.NumberOfLivesComboBoxToolTip));
            toolTip.SetToolTip(GameLengthComboBox, Translator.GetWord(Translation.WORDS.GameLengthComboBoxToolTip));
            toolTip.SetToolTip(IndividualGameRadioButton, Translator.GetWord(Translation.WORDS.IndividualGameRadioButtonToolTip));
            toolTip.SetToolTip(TeamGameRadioButton, Translator.GetWord(Translation.WORDS.TeamGameRadioButtonToolTip));
            toolTip.SetToolTip(NumberOfTeamsComboBox, Translator.GetWord(Translation.WORDS.NumberOfTeamsComboBoxToolTip));
            toolTip.SetToolTip(StartButton, Translator.GetWord(Translation.WORDS.StartButtonToolTip));
            toolTip.SetToolTip(EStopButton, Translator.GetWord(Translation.WORDS.EStopButtonToolTip));
            // 
            // videoFeedToolStripMenuItem
            // 
            this.videoFeedToolStripMenuItem.Text = Translator.GetWord(Translation.WORDS.VideoFeed);
            // 
            // robot1ToolStripMenuItem
            // 
            this.robot1ToolStripMenuItem.Text = Translator.GetWord(Translation.WORDS.Robot1);
            // 
            // robot2ToolStripMenuItem
            // 
            this.robot2ToolStripMenuItem.Text = Translator.GetWord(Translation.WORDS.Robot2);
            //
            // Robot video feed windows
            //
            Robot1VideoForm.Text = Translator.GetWord(Translation.WORDS.Robot1VideoFeed);
            Robot2VideoForm.Text = Translator.GetWord(Translation.WORDS.Robot2VideoFeed);
        }

        private void robot1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Robot1VideoForm.IsDisposed)
            {
                Robot1VideoForm = new RobotVideo("http://192.168.2.153:8080/javascript_simple.html");//This URL is currently undefined
            }
            Robot1VideoForm.Text = Translator.GetWord(Translation.WORDS.Robot1VideoFeed);
            Robot1VideoForm.Show();
            Robot1VideoForm.BringToFront();
        }

        private void robot2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Robot2VideoForm.IsDisposed)
            {
                Robot2VideoForm = new RobotVideo("http://192.168.2.145:8080/javascript_simple.html");
            }
            Robot2VideoForm.Text = Translator.GetWord(Translation.WORDS.Robot2VideoFeed);//This URL is currently undefined
            Robot2VideoForm.Show();
            Robot2VideoForm.BringToFront();
        }
        
    }
}
