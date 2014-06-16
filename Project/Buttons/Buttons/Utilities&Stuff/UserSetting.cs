using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace Buttons
{
    public class UserSetting
    {
        bool fullScreen;
        public float musicVolume;
        float soundEffectVolume;
        public Language language;
        List<HighScore> scores;
        Game1 game;

        public UserSetting(Game1 game)
        {
            this.game = game;
            readFullScreen();
            readMusicVolume();
            readSoundEffectVolume();
            readLanguage();
            readHighScores();
        }

        public bool Fullscreen
        {
            get { return fullScreen; }
            set
            {
                /*if (!value)
                {
                    game.width = 800;
                    game.height = 600;
                }
                else
                {
                    game.width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    game.height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                    game.graphics.ApplyChanges();
                }*/
                
                fullScreen = value;
            }
        }

        public List<HighScore> Scores
        {
            get
            {
                for(int i = 0; i < scores.Count; i++)
                    for (int j = 0; j < i; j++)
                    {
                        if (scores[i].score > scores[j].score)
                        {
                            HighScore s = scores[i];
                            scores[i] = scores[j];
                            scores[j] = s;
                        }
                    }
                return scores;
            }
        }

        public void addScore(HighScore score)
        {
            if (scores.Count > 10)
            {
                scores[scores.Count - 1] = score;
                return;
            }
            int i = 0;
            while (i < scores.Count && scores[i].score > score.score)
            {
                i++;
            }
            scores.Insert(i, score);
        }

        public float SoundEffectVolume
        {
            get { return soundEffectVolume; }
            set
            {
                if (value > 1 || value < 0)
                    throw new Exception();

                soundEffectVolume = value;
            }
        }

        void readFullScreen()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\You'll Catch A Virus\\preferences.txt";
            StreamReader reader = new StreamReader(path);
            string preferences;
            do
            {
                preferences = reader.ReadLine();
            } while (!preferences.Contains("fullScreen"));

            while (preferences[0] != '\"')
            {
                preferences = preferences.Substring(1);
            }
            preferences = preferences.Substring(1);
            fullScreen = preferences[0] == 'T';
            reader.Close();
        }
        void readLanguage()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\You'll Catch A Virus\\preferences.txt";
            StreamReader reader = new StreamReader(path);
            string preferences;
            do
            {
                preferences = reader.ReadLine();
            } while (!preferences.Contains("language"));

            while (preferences[0] != '\"')
            {
                preferences = preferences.Substring(1);
            }
            preferences = preferences.Substring(1, preferences.Length - 2);
            switch (preferences)
            {
                case "English":
                    language = Language.English;
                    break;
                case "French":
                    language = Language.French;
                    break;
                default:
                    throw new Exception("Language " + preferences + " is invalid.");

            }
            reader.Close();
            Strings.Language = language;
        }
        void readMusicVolume()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\You'll Catch A Virus\\preferences.txt";
            StreamReader reader = new StreamReader(path);
            string preferences;
            do
            {
                preferences = reader.ReadLine();
            } while (!preferences.Contains("musicVolume"));
            while (preferences[0] != '\"')
            {
                preferences = preferences.Substring(1);
            }
            preferences = preferences.Substring(1);
            string volume = "";
            foreach (char c in preferences)
            {
                if (c != '\"')
                    volume += c;
            }

            volume = volume.Replace(',', '.');

            try
            {

                musicVolume = (float)Convert.ToDouble(volume);
            }
            catch
            {

                musicVolume = (float)Convert.ToDouble(volume.Replace(".", ","));

            }
            reader.Close();
            Console.WriteLine(musicVolume);
        }

        void readHighScores()
        {
            scores = new List<HighScore>();
            string file = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\You'll Catch A Virus\\scores.txt";
            StreamReader reader = new StreamReader(file);
            while (!reader.EndOfStream)
            {
                string currentScore = reader.ReadLine();
                string name = "";
                int i;
                for (i = 0; currentScore[i] != ' '; i++)
                {
                    name += currentScore[i];
                }
                scores.Add(new HighScore(name, Convert.ToInt64(currentScore.Substring(i + 1))));
            }
            reader.Close();
            

        }

        void readSoundEffectVolume()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\You'll Catch A Virus\\preferences.txt";
            StreamReader reader = new StreamReader(path);
            string preferences = "";
            do
            {
                preferences = reader.ReadLine();
            } while (!preferences.Contains("soundEffectVolume"));

            while (preferences[0] != '\"')
            {
                preferences = preferences.Substring(1);
            }
            string volume = "";
            foreach (char c in preferences)
            {
                if (c != '\"')
                    volume += c;
            }

            volume = volume.Replace(',', '.');

            try
            {
                
                soundEffectVolume = (float)Convert.ToDouble(volume);
            }
            catch
            {

                soundEffectVolume = (float)Convert.ToDouble(volume.Replace(".", ","));

            }
            reader.Close();
        }

        public void saveSettings()
        {
            string fs = "fullScreen = \"" + fullScreen + "\"";
            string musicV = "musicVolume = \"" + musicVolume + "\"";
            string effectV = "soundEffectVolume = \"" + soundEffectVolume + "\"";
            string lang = "language = \"" + language.ToString() + "\"";
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\You'll Catch A Virus\\preferences.txt";
            System.IO.File.WriteAllLines(path, new string[] { fs, musicV, effectV, lang });

        }

        public void resetSettings()
        {
            musicVolume = 0.5f;
            soundEffectVolume = 0.5f;
            fullScreen = false;
            language = Language.English;
            saveSettings();
            Strings.Language = Language.English;
        }

        public void saveHighScores()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\You'll Catch A Virus\\scores.txt";
            string[] lines = new string[scores.Count];
            for (int i = 0; i < scores.Count; i++)
            {
                HighScore highScore = scores[i];
                string line = highScore.name.Replace(" ","") + " " + highScore.score;
                lines[i] = line;
            }
            System.IO.File.WriteAllLines(path, lines);
        }



    }
}
