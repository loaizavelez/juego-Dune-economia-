using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private GameStats stats;

    [Header("Sliders")]
    [SerializeField] private Slider spiceSlider;
    [SerializeField] private Slider waterSlider;
    [SerializeField] private Slider inhabitantsSlider;
    [SerializeField] private Slider militarySlider;
    [SerializeField] private Slider communitySlider;
    [SerializeField] private Slider stabilitySlider;

    [Header("Texts")]
    [SerializeField] private TMP_Text spiceText;
    [SerializeField] private TMP_Text waterText;
    [SerializeField] private TMP_Text inhabitantsText;
    [SerializeField] private TMP_Text militaryText;
    [SerializeField] private TMP_Text communityText;
    [SerializeField] private TMP_Text stabilityText;
    [SerializeField] private TMP_Text turnText;

    void Update()
    {
        // 🔹 Determinar jugador actual
        GameStats.PlayerStats jugadorActual =
            stats.currentTurn == Controller.Player1 ? stats.player1 : stats.player2;

        // 🔹 Actualizar sliders
        spiceSlider.value = jugadorActual.Spice;
        waterSlider.value = jugadorActual.Water;
        inhabitantsSlider.value = jugadorActual.Inhabitants;
        militarySlider.value = jugadorActual.MilitaryPower;
        communitySlider.value = jugadorActual.CommunitySupport;
        stabilitySlider.value = jugadorActual.Stability;

        // 🔹 Actualizar textos
        spiceText.text = $"Especia: {jugadorActual.Spice}";
        waterText.text = $"Agua: {jugadorActual.Water}";
        inhabitantsText.text = $"Habitantes: {jugadorActual.Inhabitants}";
        militaryText.text = $"Poder Militar: {jugadorActual.MilitaryPower}";
        communityText.text = $"Apoyo Comunitario: {jugadorActual.CommunitySupport}";
        stabilityText.text = $"Estabilidad: {jugadorActual.Stability}";

        // 🔹 Mostrar turno actual
        turnText.text = $"Turno: {stats.currentTurn}";
    }
}
