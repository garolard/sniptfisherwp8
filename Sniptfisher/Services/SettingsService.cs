using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.IsolatedStorage;
using System.Text;
using System.Threading.Tasks;

namespace Sniptfisher.Services
{
    public class SettingsService : Interfaces.ISettingsService
    {
        IsolatedStorageSettings settings;

        const string UsernameKeyName = "Username";
        const string ApikeyKeyName = "Apikey";

        const string UsernameKeyDefault = "";
        const string ApikeyKeyDefault = "";

        public SettingsService()
        {
            try
            {
                settings = IsolatedStorageSettings.ApplicationSettings;
            }
            catch (ArgumentNullException ane)
            {
                System.Diagnostics.Debug.WriteLine(ane.Message);
            }
        }

        private bool AddOrUpdateValue(string key, Object value)
        {
            bool valueChanged = false;

            if (settings.Contains(key))
            {
                if (settings[key] != value)
                {
                    settings[key] = value;
                    valueChanged = true;
                }
            }
            else
            {
                settings.Add(key, value);
                valueChanged = true;
            }

            return valueChanged;
        }

        private T GetValueOrDefault<T>(string key, T defaultValue)
        {
            T value;

            if (settings.Contains(key))
            {
                value = (T)settings[key];
            }
            else
            {
                value = defaultValue;
            }

            return value;
        }

        private void Save()
        {
            settings.Save();
        }

        public void Clear()
        {
            settings.Clear();
        }

        public string UsernameSetting
        {
            get
            {
                return GetValueOrDefault<string>(UsernameKeyName, UsernameKeyDefault);
            }
            set
            {
                AddOrUpdateValue(UsernameKeyName, value);
                Save();
            }
        }

        public string ApikeySetting
        {
            get
            {
                return GetValueOrDefault<string>(ApikeyKeyName, ApikeyKeyDefault);
            }
            set
            {
                AddOrUpdateValue(ApikeyKeyName, value);
                Save();
            }
        }
    }
}
