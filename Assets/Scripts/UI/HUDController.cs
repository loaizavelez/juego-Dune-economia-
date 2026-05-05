using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public GameStats stats;
    public TextMeshProUGUI playerWaterText;
    public TextMeshProUGUI aiWaterText;

    void Update()
    {
        playerWaterText.text = $"Agua Jugador: {stats.player1.Water}";
        aiWaterText.text = $"Agua IA: {stats.player2.Water}";
    }
}
