using UnityEngine;
using TMPro;

public class VictoryCondition : MonoBehaviour
{
    [SerializeField] private GameStats gameStats; // referencia al script GameStats
    [SerializeField] private GameObject victoryPanel; // panel de UI para mostrar victoria
    [SerializeField] private TMP_Text victoryText; // texto dinámico en el panel

    // Método que se llama al final de cada turno
    public void CheckVictoryCondition(int player1Territory, int player2Territory, int totalTerritory)
    {
        // 🔹 Victoria por eliminación
        if (gameStats.player1.Inhabitants <= 0)
        {
            ShowVictory("Jugador 2", "Eliminación");
            return;
        }
        else if (gameStats.player2.Inhabitants <= 0)
        {
            ShowVictory("Jugador 1", "Eliminación");
            return;
        }

        // 🔹 Victoria por estabilidad
        if (gameStats.player1.Stability >= 100)
        {
            ShowVictory("Jugador 1", "Estabilidad");
            return;
        }
        else if (gameStats.player2.Stability >= 100)
        {
            ShowVictory("Jugador 2", "Estabilidad");
            return;
        }

        // 🔹 Victoria por conquista
        float conquestThreshold = totalTerritory * 0.6f; // 60% del mapa
        if (player1Territory >= conquestThreshold)
        {
            ShowVictory("Jugador 1", "Conquista");
            return;
        }
        else if (player2Territory >= conquestThreshold)
        {
            ShowVictory("Jugador 2", "Conquista");
            return;
        }
    }

    private void ShowVictory(string winner, string condition)
    {
        Debug.Log($"🏆 {winner} gana por {condition}!");
        victoryPanel.SetActive(true);
        victoryText.text = $"{winner} gana por {condition}!";

        // Aquí puedes añadir lógica extra:
        // - Detener el juego
        // - Desactivar controles
        // - Mostrar botones de reinicio o volver al menú
    }
}
