using UnityEngine;

namespace JustTanks.GameLogic
{
    public class PrefsManager
    {
        public int LoadInt(string key)
        {
            return PlayerPrefs.GetInt(key, 0);
        }

        public void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public void SaveFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        public float LoadFloat(string key)
        {
            return PlayerPrefs.GetFloat(key, 0f);
        }

        public void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public string LoadString(string key)
        {
            return PlayerPrefs.GetString(key, string.Empty);
        }
    }
}
