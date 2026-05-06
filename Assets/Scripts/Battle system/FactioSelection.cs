using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FactionSelectionUI : MonoBehaviour
{
    [SerializeField] private Button fremenButton;
    [SerializeField] private Button harkonnenButton;
    [SerializeField] private TMP_Text infoText;

    public static GameStats.Faction player1Faction;
    public static GameStats.Faction player2Faction;

    private int currentPlayer = 1; // 1 = Player1, 2 = Player2

    private void Start()
    {
        infoText.text = "Jugador 1: Elige tu facción";

        fremenButton.onClick.AddListener(() => SelectFaction(GameStats.Faction.Fremen));
        harkonnenButton.onClick.AddListener(() => SelectFaction(GameStats.Faction.Harkonnen));
    }

    private void SelectFaction(GameStats.Faction faction)
    {
        if (currentPlayer == 1)
        {
            player1Faction = faction;
            infoText.text = "Jugador 2: Elige tu facción";
            currentPlayer = 2;
        }
        else if (currentPlayer == 2)
        {
            player2Faction = faction;
            Debug.Log($"Jugador 1 eligió: {player1Faction}, Jugador 2 eligió: {player2Faction}");

            // 🔹 Cargar la escena principal
            SceneManager.LoadScene("Mapa");
        }
    }
}
