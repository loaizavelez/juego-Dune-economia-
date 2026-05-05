using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public GameStats stats;
    public TextMeshProUGUI playerWaterText;
    public TextMeshProUGUI aiWaterText;

    void Update()
    {
        playerWaterText.text = $"Agua Jugador: {stats.playerWater}";
        aiWaterText.text = $"Agua IA: {stats.aiWater}";
    }
}
