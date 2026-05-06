using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject instructionsPage1;
    [SerializeField] private GameObject instructionsPage2;

    [SerializeField] private Button playButton;
    [SerializeField] private Button instructionsButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button backButtonPage1;
    [SerializeField] private Button backButtonPage2;

    private void Start()
    {
        // Estado inicial
        mainPanel.SetActive(true);
        instructionsPage1.SetActive(false);
        instructionsPage2.SetActive(false);

        // Botones principales
        playButton.onClick.AddListener(PlayGame);
        instructionsButton.onClick.AddListener(ShowPage1);
        exitButton.onClick.AddListener(ExitGame);

        // Botones de navegación
        nextButton.onClick.AddListener(ShowPage2);
        prevButton.onClick.AddListener(ShowPage1);
        backButtonPage1.onClick.AddListener(BackToMenu);
        backButtonPage2.onClick.AddListener(BackToMenu);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("FactionSelection");
    }

    private void ShowPage1()
    {
        mainPanel.SetActive(false);
        instructionsPage1.SetActive(true);
        instructionsPage2.SetActive(false);
    }

    private void ShowPage2()
    {
        instructionsPage1.SetActive(false);
        instructionsPage2.SetActive(true);
    }

    private void BackToMenu()
    {
        instructionsPage1.SetActive(false);
        instructionsPage2.SetActive(false);
        mainPanel.SetActive(true);
    }

    private void ExitGame()
    {
        Application.Quit();
        Debug.Log("Juego cerrado.");
    }
}
