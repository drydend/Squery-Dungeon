using UnityEngine;

public class App
{
    private static SaveController _saveController;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        _saveController = new SaveController();
        _saveController.Initialize();
        Application.quitting += () => _saveController.SaveGame(); 
    }

    public static void Quit()
    {
        _saveController.SaveGame();
        Application.Quit();
    }
}

