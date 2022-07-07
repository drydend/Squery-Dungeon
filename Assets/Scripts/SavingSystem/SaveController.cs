using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class SaveController
{
    private const string FileName = @"/SaveData.txt";

    public static SaveController Instance { get; private set; }

    private SaveData _currentSave;
    private List<ISaveable> _saveables = new List<ISaveable>();

    private string PathToSaveFile => Application.dataPath + FileName;

    public void Initialize()
    {
        if (!File.Exists(PathToSaveFile))
        {
            var createdFileStream = File.Create(PathToSaveFile);
            createdFileStream.Close();

            _currentSave = new SaveData();
            _currentSave.InitializeByDefault();
            SaveGame();
        }

        _currentSave = new SaveData();
        _currentSave.InitializeByDefault();

        Instance = this;
    }

    public void LoadGame()
    {
        using FileStream fileStream = new FileStream(PathToSaveFile, FileMode.Open);
        var dataInBytes = new byte[fileStream.Length];
        fileStream.Read(dataInBytes, 0, (int)fileStream.Length);
        var jsonData = Encoding.Default.GetString(dataInBytes);
        _currentSave = JsonUtility.FromJson<SaveData>(jsonData);

        foreach (var saveable in _saveables)
        {
            saveable.LoadData(_currentSave);
        }
    }

    public void SaveGame()
    {
        foreach (var saveable in _saveables)
        {
            saveable.SaveData(_currentSave);
        }

        using FileStream fileStream = new FileStream(PathToSaveFile, FileMode.OpenOrCreate);
        var jsonData = JsonUtility.ToJson(_currentSave);
        var dataInBytes = Encoding.Default.GetBytes(jsonData);

        fileStream.SetLength(0);
        fileStream.Write(dataInBytes, 0, dataInBytes.Length);
    }

    public void Subscribe(ISaveable saveable)
    {
        _saveables.Add(saveable);
    }

    public void UnSubcribe(ISaveable saveable)
    {
        _saveables.Remove(saveable);
    }
}

