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

    // 🔹 Botones para conversión
    [SerializeField] private Button convertAttackButton;
    [SerializeField] private Button convertDefenseButton;

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
        confirmButton.onClick.AddListener(() => ResolveBattle(targetTile, attackerPower, defenderPower));

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(() => battlePopup.SetActive(false));

        // 🔹 Configurar conversión de habitantes
        convertAttackButton.onClick.RemoveAllListeners();
        convertAttackButton.onClick.AddListener(() => {
            var jugador = stats.currentTurn == ControllerManager.Controller.Player1 ? stats.player1 : stats.player2;
            jugador.ConvertInhabitantsToAttack(10); // ejemplo: convertir 10 habitantes
            attackerText.text = $"Atacante: {jugador.AttackPower:F0}";
        });

        convertDefenseButton.onClick.RemoveAllListeners();
        convertDefenseButton.onClick.AddListener(() => {
            var jugador = stats.currentTurn == ControllerManager.Controller.Player1 ? stats.player1 : stats.player2;
            jugador.ConvertInhabitantsToDefense(10); // ejemplo: convertir 10 habitantes
            defenderText.text = $"Defensor: {jugador.DefensePower:F0}";
        });
    }

    // Resolver batalla
    private void ResolveBattle(Casillas tile, float attackerPower, float defenderPower)
    {
        var atacante = stats.currentTurn == ControllerManager.Controller.Player1 ? stats.player1 : stats.player2;
        var defensor = tile.controller == ControllerManager.Controller.Player1 ? stats.player1 : stats.player2;

        if (attackerPower >= defenderPower)
        {
            tile.TryCapture(stats.currentTurn); // asegúrate de que TryCapture sea PUBLIC en Casillas.cs
            stats.ConsumeMove();

            atacante.AttackPower -= 1;
            defensor.DefensePower -= 2;

            atacante.Inhabitants -= 10;
            defensor.Inhabitants -= 15;

            Debug.Log("El atacante ganó. Se redujeron AttackPower, DefensePower e Inhabitants.");
        }
        else
        {
            stats.ConsumeMove();

            atacante.AttackPower -= 3;
            defensor.DefensePower -= 1;

            atacante.Inhabitants -= 20;
            defensor.Inhabitants -= 5;

            Debug.Log("El atacante perdió. Se redujeron AttackPower, DefensePower e Inhabitants.");
        }

        battlePopup.SetActive(false);
    }
}
