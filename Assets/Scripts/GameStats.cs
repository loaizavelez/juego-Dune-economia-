using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        [Header("Resources")]
        public float Spice;
        public float Inhabitants;
        public float Water;

        [Header("Stats")]
        public float AttackPower;
        public float DefensePower;
       // public float MilitaryPower;
        public float CommunitySupport;
        public float Stability;

        public PlayerStats(int initialWater)
        {
            Spice = 0;
            Inhabitants = 0;
            Water = initialWater;
           // MilitaryPower = 0f;
            CommunitySupport = 0f;
            Stability = 50f; // valor inicial neutro
        }
    }

    [Header("Player vs Player")]
    public PlayerStats player1 = new PlayerStats(100);
    public PlayerStats player2 = new PlayerStats(100);

    public ControllerManager.Controller currentTurn = ControllerManager.Controller.Player1;

    // 🔹 Nuevo: sistema de movimientos y contador de turnos
    public int movesRemaining = 3;
    public int turnCounter = 1;

    private void Start()
    {
        StartCoroutine(RecoleccionDeAgua());
    }

    IEnumerator RecoleccionDeAgua()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);

            player1.Water += 1;
            player2.Water += 1;

            Debug.Log($"Recolección: Jugador 1 = {player1.Water}, Jugador 2 = {player2.Water}");
        }
    }

    // 🔹 Consumir movimiento
    public void ConsumeMove()
    {
        movesRemaining--;
        Debug.Log($"Movimientos restantes: {movesRemaining}");

        if (movesRemaining <= 0)
        {
            ChangeTurn();
        }
    }

    // 🔹 Cambiar turno
    public void ChangeTurn()
    {
        currentTurn = (currentTurn == ControllerManager.Controller.Player1) ? ControllerManager.Controller.Player2 : ControllerManager.Controller.Player1;
        movesRemaining = 3;
        turnCounter++;

        Debug.Log($"➡️ Cambio de turno. Ahora juega: {currentTurn}. Turno global: {turnCounter}");
    }

    // 🔹 Métodos auxiliares para HUD
    public int GetMovesRemaining() => movesRemaining;
    public int GetTurnCounter() => turnCounter;

    // 🔹 Métodos para modificar stats de cada jugador
    public void AddSpice(PlayerStats player, int amount)
    {
        player.Spice += amount;
        Debug.Log($"Spice actualizado: {player.Spice}");
    }

    public void AddWater(PlayerStats player, int amount)
    {
        player.Water += amount;
        Debug.Log($"Agua actualizada: {player.Water}");
    }

    public void ChangeInhabitants(PlayerStats player, int amount)
    {
        player.Inhabitants = Mathf.Max(player.Inhabitants + amount, 0);
        Debug.Log($"Habitantes: {player.Inhabitants}");
    }

   /* public void ChangeMilitaryPower(PlayerStats player, float amount)
    {
        player.MilitaryPower = Mathf.Clamp(player.MilitaryPower + amount, 0f, 100f);
        Debug.Log($"Poder militar: {player.MilitaryPower}");
    }*/

    public void ChangeCommunitySupport(PlayerStats player, float amount)
    {
        player.CommunitySupport = Mathf.Clamp(player.CommunitySupport + amount, 0f, 100f);
        Debug.Log($"Apoyo comunitario: {player.CommunitySupport}");
    }

    public void ChangeStability(PlayerStats player, float amount)
    {
        player.Stability = Mathf.Clamp(player.Stability + amount, 0f, 100f);
        Debug.Log($"Estabilidad: {player.Stability}");
    }
}
