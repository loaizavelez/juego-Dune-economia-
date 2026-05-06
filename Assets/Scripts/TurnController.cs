using UnityEngine;
using System.Collections;

public class TurnController : MonoBehaviour
{
    public ControllerManager.Controller currentTurn = ControllerManager.Controller.Player1;
    private int movesRemaining = 3;   // Cada turno empieza con 3 movimientos
    private int turnCounter = 1;      // Contador de turnos global

    // Llamar cuando el jugador hace una acción (mover, capturar, etc.)
    public void OnPlayerAction()
    {
        movesRemaining--;
        Debug.Log($"[Turno {turnCounter}] Acción realizada por {currentTurn}. Movimientos restantes: {movesRemaining}");

        if (movesRemaining <= 0)
        {
            CambiarTurno();
        }
    }

    // Botón de saltar turno
    public void SkipTurn()
    {
        Debug.Log($"[Turno {turnCounter}] {currentTurn} decidió saltar turno.");
        CambiarTurno();
    }

    private void CambiarTurno()
    {
        // Cambiar jugador
        currentTurn = (currentTurn == ControllerManager.Controller.Player1) ? ControllerManager.Controller.Player2 : ControllerManager.Controller.Player1;

        // Reiniciar contador de movimientos
        movesRemaining = 3;

        // Incrementar contador de turnos global
        turnCounter++;

        Debug.Log($"➡️ Cambio de turno. Ahora juega: {currentTurn}. Turno global: {turnCounter}, movimientos reiniciados a {movesRemaining}");
    }

    // Para mostrar en HUD
    public int GetMovesRemaining() => movesRemaining;
    public int GetTurnCounter() => turnCounter;
}
