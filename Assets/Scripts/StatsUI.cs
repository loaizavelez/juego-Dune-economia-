using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private GameStats stats;

    [Header("HUD Panel")]
    [SerializeField] private GameObject hudPanel;   // ← referencia directa al HUDPanel
    [SerializeField] private Image fondoImage;

    [Header("Sprites de Fondo")]
    [SerializeField] private Sprite fremenSprite;
    [SerializeField] private Sprite harkonnenSprite;
    [SerializeField] private Sprite neutralSprite;

    [Header("Textos")]
    [SerializeField] private TMP_Text turnoText;
    [SerializeField] private TMP_Text especiaText;
    [SerializeField] private TMP_Text aguaText;
    [SerializeField] private TMP_Text habitantesText;
    [SerializeField] private TMP_Text militarText;
    [SerializeField] private TMP_Text comunidadText;
    [SerializeField] private TMP_Text estabilidadText;

    private bool visible = false;

    void Update()
    {
        // Toggle HUDPanel con Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            visible = !visible;
            hudPanel.SetActive(visible);
        }

        if (!visible) return;

        // Jugador actual
        GameStats.PlayerStats jugador =
            stats.currentTurn == Controller.Player1 ? stats.player1 : stats.player2;

        // Actualizar textos
        especiaText.text = "Especia: " + jugador.Spice;
        aguaText.text = "Agua: " + jugador.Water;
        habitantesText.text = "Habitantes: " + jugador.Inhabitants;
        militarText.text = "Militar: " + jugador.MilitaryPower;
        comunidadText.text = "Comunidad: " + jugador.CommunitySupport;
        estabilidadText.text = "Estabilidad: " + jugador.Stability;

        // Fondo y turno
        if (stats.currentTurn == Controller.Player1)
        {
            turnoText.text = "Turno: Fremen";
            turnoText.color = Color.cyan;
            fondoImage.sprite = fremenSprite;
        }
        else if (stats.currentTurn == Controller.Player2)
        {
            turnoText.text = "Turno: Harkonnen";
            turnoText.color = Color.red;
            fondoImage.sprite = harkonnenSprite;
        }
        else
        {
            turnoText.text = "Turno Neutral";
            turnoText.color = Color.white;
            fondoImage.sprite = neutralSprite;
        }
    }
}
