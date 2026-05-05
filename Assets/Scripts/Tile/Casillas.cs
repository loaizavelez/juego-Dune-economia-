using UnityEngine;
using System.Collections.Generic;

public enum Controller
{
    None,
    Player1,
    Player2
}

public class Casillas : MonoBehaviour
{
    public int x;
    public int y;
    public string TerritoryType;

    public Controller controller = Controller.None;

    // 🔹 Stats específicos de cada casilla
    public int requiredMilitary;   // poder militar necesario
    public int waterCost;          // agua necesaria
    public bool hasSpice;          // indica si hay especia en la casilla
    public float wormChance;       // probabilidad de gusanos

    void Start()
    {
        InicializarStats();        // asigna stats aleatorios
        SetController(controller); // pinta la casilla
    }

    void OnMouseDown()
    {
        GridManager grid = FindObjectOfType<GridManager>();
        GameStats stats = grid.stats;

        Controller jugadorActual = stats.currentTurn;

        if (controller == Controller.None && EsVecinaDeJugador(grid, jugadorActual))
        {
            if (TryCapture(jugadorActual, grid, stats))
            {
                stats.ChangeTurn();
            }
        }
        else if (controller == Controller.None && !HayCasillasDelJugador(grid, jugadorActual))
        {
            if (TryCapture(jugadorActual, grid, stats))
            {
                stats.ChangeTurn();
            }
        }
        else
        {
            Debug.Log($"Jugador {jugadorActual} intentó capturar ({x},{y}) pero no es vecina de ninguna casilla controlada.");
        }
    }

    void InicializarStats()
    {
        switch (TerritoryType)
        {
            case "Desert":
                waterCost = Random.Range(2, 5);
                requiredMilitary = Random.Range(5, 15);
                wormChance = 0.3f;
                break;
            case "Oasis":
                waterCost = Random.Range(1, 2);
                requiredMilitary = Random.Range(1, 5);
                wormChance = 0.05f;
                break;
            case "Plain":
                waterCost = Random.Range(1, 3);
                requiredMilitary = Random.Range(3, 10);
                wormChance = 0.1f;
                break;
            default:
                waterCost = 2;
                requiredMilitary = 5;
                wormChance = 0.1f;
                break;
        }

        // 🔹 La especia aparece aleatoriamente
        hasSpice = Random.value < 0.2f; // 20% probabilidad
    }

    public void SetController(Controller newController)
    {
        controller = newController;

        var renderer = GetComponent<SpriteRenderer>();

        // 🔹 Color base según tipo de terreno
        Color terrainColor = Color.gray;
        switch (TerritoryType)
        {
            case "Desert":
                terrainColor = new Color(1f, 0.9f, 0.6f);
                break;
            case "Oasis":
                terrainColor = Color.green;
                break;
            case "Plain":
                terrainColor = Color.yellow;
                break;
            default:
                terrainColor = Color.gray;
                break;
        }

        // 🔹 Color del controlador
        Color controllerColor = Color.white;
        switch (controller)
        {
            case Controller.Player1:
                controllerColor = Color.blue;
                break;
            case Controller.Player2:
                controllerColor = Color.red;
                break;
            case Controller.None:
                controllerColor = Color.white;
                break;
        }

        // 🔹 Mezclar terreno + controlador
        Color finalColor = terrainColor;
        if (controller != Controller.None)
        {
            finalColor = Color.Lerp(terrainColor, controllerColor, 0.5f);
        }

        renderer.color = finalColor;
    }

    public bool TryCapture(Controller newController, GridManager grid, GameStats stats)
    {
        // Determinar jugador actual
        GameStats.PlayerStats jugadorActual =
            newController == Controller.Player1 ? stats.player1 : stats.player2;

        bool suficienteAgua = jugadorActual.Water >= waterCost;
        bool suficienteMilitar = jugadorActual.MilitaryPower >= requiredMilitary;

        if (suficienteAgua && suficienteMilitar)
        {
            jugadorActual.Water -= waterCost;
            SetController(newController);

            if (hasSpice)
                stats.AddSpice(jugadorActual, Random.Range(1, 5));

            VerificarEventoGusanos();
            return true;
        }

        Debug.Log($"No se pudo capturar ({x},{y}). Agua o poder militar insuficiente.");
        return false;
    }


    void VerificarEventoGusanos()
    {
        if (Random.value < wormChance)
        {
            Debug.Log($"⚠️ Gusanos de arena emergen en ({x},{y})!");
            // Aquí puedes disparar animación, daño o evento especial
        }
    }

    bool EsVecinaDeJugador(GridManager grid, Controller jugador)
    {
        List<Vector2Int> vecinos = new List<Vector2Int>()
        {
            new Vector2Int(x+1, y),
            new Vector2Int(x-1, y),
            new Vector2Int(x, y+1),
            new Vector2Int(x, y-1)
        };

        foreach (var v in vecinos)
        {
            if (v.x >= 0 && v.x < grid.width && v.y >= 0 && v.y < grid.height)
            {
                Casillas vecino = grid.GetCasilla(v.x, v.y);
                if (vecino != null && vecino.controller == jugador)
                {
                    return true;
                }
            }
        }

        return false;
    }

    bool HayCasillasDelJugador(GridManager grid, Controller jugador)
    {
        for (int i = 0; i < grid.width; i++)
        {
            for (int j = 0; j < grid.height; j++)
            {
                Casillas c = grid.GetCasilla(i, j);
                if (c != null && c.controller == jugador)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
