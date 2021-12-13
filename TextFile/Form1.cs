using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace TextFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region colours

        //used to store all the colours from the list locally
        List<string> colours = new List<string>();

        private void loadColoursButton_Click(object sender, EventArgs e)
        {
            colours = File.ReadAllLines("Colors.txt").ToList();
            DisplayColours();
        }

        private void saveColoursButton_Click(object sender, EventArgs e)
        {
            File.WriteAllLines("Colors.txt", colours);
        }

        private void sortColoursButton_Click(object sender, EventArgs e)
        {
            colours = colours.OrderBy(x => x).ToList();
            DisplayColours();
        }

        private void addColourButton_Click(object sender, EventArgs e)
        {
            colours.Add(colourInput.Text);
            colourInput.Text = "";
            DisplayColours();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            string toRemove = removeInput.Text;
            if (colours.Contains(toRemove))
            {
                colours.Remove(toRemove);
            }
            DisplayColours();
        }

        public void DisplayColours()
        {
            colourOutput.Text = "";

            foreach(string colour in colours)
            {
                colourOutput.Text += $"{colour}\n";
            }
        }

        #endregion


        #region scores

        List<HighScore> scores = new List<HighScore>();

        private void loadScoresButton_Click(object sender, EventArgs e)
        {
            List<string> tempString = File.ReadAllLines("scoreFile.txt").ToList();
            for(int i = 0;i < tempString.Count;i+=2)
            {
                string _name = tempString[i];
                int _score = Convert.ToInt16(tempString[i + 1]);

                scores.Add(new HighScore(_name, _score));
            }

            DisplayScores();
        }

        private void saveScoresButton_Click(object sender, EventArgs e)
        {
            List<string> tempString = new List<string>();
            for(int i = 0;i < scores.Count;i++)
            {
                tempString.Add(scores[i].name);
                tempString.Add(scores[i].score.ToString());
            }

            File.WriteAllLines("scoreFile.txt", tempString);
        }

        private void sortScoresButton_Click(object sender, EventArgs e)
        {
            scores = scores.OrderByDescending(x => x.score).ToList();
            DisplayScores();
        }

        private void addScoreButton_Click(object sender, EventArgs e)
        {
            scores.Add(new HighScore(nameInput.Text, Convert.ToInt16(scoreInput.Text)));
            nameInput.Text = "";
            scoreInput.Text = "";
            DisplayScores();
        }

        private void removeScoresButton_Click(object sender, EventArgs e)
        {
            string toRemove = nameRemove.Text;
            int indexOf = scores.FindIndex(x => x.name == toRemove);
            if(indexOf >= 0)
            {
                 scores.RemoveAt(indexOf);
            }
            DisplayScores();
        }

        public void DisplayScores()
        {
            scoreOutput.Text = "";

            foreach (HighScore hs in scores)
            {
                scoreOutput.Text += $"{hs.name} {hs.score}\n";
            }
        }

        #endregion
    }
}
