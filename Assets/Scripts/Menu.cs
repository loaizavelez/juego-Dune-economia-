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
    [SerializeField] private TMP_Text instructionsText;

    private void Start()
    {
        instructionsText.text =
        "📖 Instrucciones\n\n" +
        "⚔️ Facciones:\n" +
        "- Fremen: Menor ataque, mayor defensa y menor consumo de agua.\n" +
        "- Harkonnen: Mayor ataque, menor defensa y consumo estándar de agua.\n" +
        "- Cada jugador elige su facción al inicio de la partida.\n\n" +
        "💧 Agua:\n" +
        "- Cada habitante necesita agua para sobrevivir.\n" +
        "- Si hay suficiente agua, la población crece.\n" +
        "- Si falta agua, la población disminuye.\n" +
        "- El consumo depende de la facción elegida.\n\n" +
        "⚔️ Combate:\n" +
        "- Los habitantes pueden convertirse en poder de ataque o defensa.\n" +
        "- El resultado depende de los stats de cada facción.\n" +
        "- La estrategia está en equilibrar población, agua y poder militar.\n\n" +
        "🎮 Cómo Jugar:\n" +
        "1. En el menú de facción, cada jugador elige su bando.\n" +
        "2. Durante la partida, cada jugador tiene 3 movimientos por turno.\n" +
        "3. Usa tus movimientos para recolectar recursos, convertir habitantes en poder militar o defender tu comunidad.\n" +
        "4. Vigila el agua: sin ella, tu población disminuirá.\n" +
        "5. El objetivo es mantener tu facción estable y derrotar al rival en combate.";


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
