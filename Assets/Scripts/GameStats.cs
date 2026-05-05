using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        [Header("Resources")]
        public int Spice;
        public int Inhabitants;
        public int Water;

        [Header("Stats")]
        public float MilitaryPower;
        public float CommunitySupport;
        public float Stability;

        public PlayerStats(int initialWater)
        {
            Spice = 0;
            Inhabitants = 0;
            Water = initialWater;
            MilitaryPower = 0f;
            CommunitySupport = 0f;
            Stability = 50f; // valor inicial neutro
        }
    }

    [Header("Player vs Player")]
    public PlayerStats player1 = new PlayerStats(100);
    public PlayerStats player2 = new PlayerStats(100);

    public Controller currentTurn = Controller.Player1;

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

    public void ChangeTurn()
    {
        currentTurn = currentTurn == Controller.Player1 ? Controller.Player2 : Controller.Player1;
        Debug.Log($"Turno cambiado: {currentTurn}");
    }

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

    public void ChangeMilitaryPower(PlayerStats player, float amount)
    {
        player.MilitaryPower = Mathf.Clamp(player.MilitaryPower + amount, 0f, 100f);
        Debug.Log($"Poder militar: {player.MilitaryPower}");
    }

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
