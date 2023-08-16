using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Control_Panel
{
    class Timer
    {
        private int hours = 0; //hours remaining
        private int minutes = 0; //minutes remaining
        private int seconds = 0; //seconds remaining
        private volatile bool timerIsRunning = false; //volatile because multiple threads could access and change this property
        public bool getTimerIsRunning()
        {
            return timerIsRunning;
        }
        public void startTimer()
        {
            timerIsRunning=true;
        }
        public void stopTimer()
        {
            timerIsRunning = false;
        }
        public bool timerExpired() //returns true if the timer has reached 00:00:00
        {
            if (seconds == 0 && minutes == 0 && hours == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string getHours() //outputs the two digit string format for the hours column
        {
            if (hours > 9)
            {
                return hours.ToString();
            }
            else
            {
                return "0" + hours.ToString();
            }
        }
        public string getMinutes() //outputs the two digit string format for the minutes column
        {
            if (minutes > 9)
            {
                return minutes.ToString();
            }
            else
            {
                return "0" + minutes.ToString();
            }
        }
        public void setMinutes(int minutesParameter) //the only way to set the timer is by providing a time in minutes. Valid inputs 0-60
        {
            if (minutesParameter >= 0 && minutesParameter <= 59)
            {
                this.ClearTimer(); //This is the only funciton setting the timer, so the current value will be cleared
                minutes = minutesParameter;
            }
            else
            {
                if (minutesParameter == 60)
                {
                    this.ClearTimer(); //This is the only funciton setting the timer, so the current value will be cleared
                    hours++;
                }
                else
                {
                    //ClearTimer() is not called in this branch because if the value is invalid it will be ignored and keep current values 
                    Console.WriteLine("Minutes value invalid because it is greater than 60");
                }
            }
        }
        public string getSeconds() //outputs the two digit string format for the seconds column
        {
            if (seconds > 9)
            {
                return seconds.ToString();
            }
            else
            {
                return "0" + seconds.ToString();
            }
        }
        public void ClearTimer()
        {
            hours = 0;
            minutes = 0;
            seconds = 0;
        }
        public override string ToString()
        {
            return getHours() + ":" + getMinutes() + ":" + getSeconds();
        }
        public void updateLabel(System.Windows.Forms.Label textlabel)
        {
            while (timerIsRunning)
            {
                if (textlabel.InvokeRequired) //if the thread acessing the text label is not the same as the thread that created the text label
                {
                    textlabel.Invoke((MethodInvoker)(() => textlabel.Text = this.ToString())); //Runs this command as if it was running in the parent thread
                    textlabel.Invoke((MethodInvoker)(() => textlabel.Update())); //Runs this command as if it was running in the parent thread
                    //Invoke command referenced from http://tinyurl.com/m6nz8n8
                }
                else
                {
                    textlabel.Text = this.ToString();
                    textlabel.Update();
                }
                if (seconds > 0)
                {
                    seconds--;
                }
                else
                {
                    if (minutes > 0)
                    {
                        minutes--;
                        seconds = 59;
                    }
                    else
                    {
                        if (hours > 0)
                        {
                            hours--;
                            minutes = 59;
                            seconds = 59;
                        }
                        else
                        {
                            this.stopTimer();
                        }
                    }
                }
                System.Threading.Thread.Sleep(1000); //delay for one second (1000 miliseconds)
            }
        }
    }
}
