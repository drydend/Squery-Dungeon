using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class SaveData
{
    public SettingsSaveData SettingsData;

    public void InitializeByDefault()
    {
        SettingsData = new SettingsSaveData();
    }
}

