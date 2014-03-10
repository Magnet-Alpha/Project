using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Buttons
{
    public class UserSetting
    {
        public bool fullScreen;
        public float musicVolume;
        float soundEffectVolume;

        public UserSetting()
        {

            readFullScreen();
            readMusicVolume();
            readSoundEffectVolume();
        }

        public float SoundEffectVolume
        { 
         get{return soundEffectVolume; }
         set { 
            if (value > 1 || value < 0)
                throw new Exception();
    
            soundEffectVolume = value;
            }
        }

        void readFullScreen()
        {
            StreamReader reader = new StreamReader("preferences.txt");
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
            fullScreen = preferences[0] == 't';
            reader.Close();
        }

        void readMusicVolume()
        {
            StreamReader reader = new StreamReader("preferences.txt");
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

            /*musicVolume = (float)Convert.ToDouble(volume);*/
            reader.Close();
        }

        void readSoundEffectVolume()
        {
            StreamReader reader = new StreamReader("preferences.txt");
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

            /*soundEffectVolume = (float)Convert.ToDouble(volume);*/
            reader.Close();
        }

        public void saveSettings()
        {
            string fs = "fullScreen = \"" + fullScreen + "\"";
            string musicV = "musicVolume = \"" + musicVolume + "\"";
            string effectV = "soundEffectVolume = \"" + soundEffectVolume + "\"";

            System.IO.File.WriteAllLines(@"preferences.txt", new string[3]{fs, musicV, effectV});

        }

        public void resetSettings()
        {
            musicVolume = 0.5f;
            soundEffectVolume = 0.5f;
            fullScreen = false;
            saveSettings();
        }
    


    }
}
