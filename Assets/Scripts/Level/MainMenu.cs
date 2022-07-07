using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        SaveController.Instance.LoadGame();
    }

    public void OnMainMenuClosed()
    {
        SaveController.Instance.SaveGame();
    }
}

