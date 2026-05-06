using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button instructionsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton;

    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject mainPanel;

    private void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        instructionsButton.onClick.AddListener(ShowInstructions);
        exitButton.onClick.AddListener(ExitGame);
        backButton.onClick.AddListener(BackToMenu);

        instructionsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("FactionSelectScene"); // 🔹 va al menú de facción
    }

    private void ShowInstructions()
    {
        mainPanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }

    private void BackToMenu()
    {
        instructionsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    private void ExitGame()
    {
        Application.Quit();
        Debug.Log("Juego cerrado.");
    }
}
