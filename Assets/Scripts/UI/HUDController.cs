using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TurnController turnController;
    [SerializeField] private TMP_Text turnText;
    [SerializeField] private TMP_Text playerText;
    [SerializeField] private TMP_Text movesText;

    void Update()
    {
        // Mostrar turno global
        turnText.text = "Turno global: " + turnController.GetTurnCounter();

        // Mostrar jugador actual
        playerText.text = "Jugador actual: " + turnController.currentTurn;

        // Mostrar movimientos restantes
        movesText.text = "Movimientos restantes: " + turnController.GetMovesRemaining();

    }


}