using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private GameStats stats;
    [SerializeField] private Tile[] tiles;

    public void CaptureTiles()
    {
        // Determinar jugador actual
        GameStats.PlayerStats jugadorActual =
            stats.currentTurn == ControllerManager.Controller.Player1 ? stats.player1 : stats.player2;

        // Calcular cuántas casillas puede capturar según su apoyo comunitario
        int tilesToCapture = Mathf.FloorToInt(jugadorActual.CommunitySupport / 20f);

        for (int i = 0; i < tilesToCapture && i < tiles.Length; i++)
        {
            tiles[i].Capture();
        }
    }
}