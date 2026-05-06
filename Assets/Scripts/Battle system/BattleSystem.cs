using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private GameStats stats;
    [SerializeField] private GameObject battlePopup;
    [SerializeField] private TMP_Text battleText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    [SerializeField] private TMP_Text attackerText;
    [SerializeField] private TMP_Text defenderText;
    [SerializeField] private TMP_Text factionText;
    [SerializeField] private TMP_Text questionText;

    private Casillas targetTile;

    // Mostrar popup con stats
    public void ShowBattlePopup(Casillas tile)
    {
        targetTile = tile;

        var atacante = stats.currentTurn == ControllerManager.Controller.Player1 ? stats.player1 : stats.player2;
        var defensor = tile.controller == ControllerManager.Controller.Player1 ? stats.player1 : stats.player2;

        float attackerPower = atacante.AttackPower;
        float defenderPower = defensor.DefensePower;

        // ✅ Mostrar poder militar
        attackerText.text = $"Atacante: {attackerPower:F0}";
        defenderText.text = $"Defensor: {defenderPower:F0}";

        // ✅ Mostrar facción del atacante
        string faccion = stats.currentTurn == ControllerManager.Controller.Player1 ? "Fremen" : "Harkonnen";
        factionText.text = $"Facción atacante: {faccion}";

        // ✅ Pregunta
        questionText.text = "¿Quieres atacar este territorio?";

        battlePopup.SetActive(true);

        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(() => ResolveBattle(attackerPower, defenderPower));

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(() => battlePopup.SetActive(false));
    }

    // Resolver batalla
    private void ResolveBattle(float attackerPower, float defenderPower)
    {
        var atacante = stats.currentTurn == ControllerManager.Controller.Player1 ? stats.player1 : stats.player2;

        if (attackerPower >= defenderPower)
        {
            targetTile.controller = stats.currentTurn;
            targetTile.ActualizarColor();
            Debug.Log("¡Territorio capturado con éxito!");
        }
        else
        {
            atacante.Stability -= 2f; // penalización
            Debug.Log("El ataque falló, pierdes estabilidad.");
        }

        battlePopup.SetActive(false);
        stats.ConsumeMove(); // consumir movimiento tras la batalla
    }

   
}
