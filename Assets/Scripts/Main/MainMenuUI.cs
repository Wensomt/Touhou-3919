using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button multiplayerButton;
    public Button optionsButton;
    public Button storyModeButton;
    public Button replaysButton;
    public Button exitButton;

    private void Start()
    {
        multiplayerButton.onClick.AddListener(OpenMultiplayerMenu);
        optionsButton.onClick.AddListener(OpenOptions);
        storyModeButton.onClick.AddListener(StartStoryMode);
        replaysButton.onClick.AddListener(OpenReplays);
        exitButton.onClick.AddListener(ExitGame);
    }

    void OpenMultiplayerMenu()
    {
        SceneManager.LoadScene("MultiplayerMenu");
    }

    void OpenOptions()
    {
        // Implementacja otwierania menu opcji
    }

    void StartStoryMode()
    {
        SceneManager.LoadScene("StoryModeScene");
    }

    void OpenReplays()
    {
        // Implementacja otwierania menu powtórek
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
