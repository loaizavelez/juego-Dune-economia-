using UnityEngine;
using System.Collections.Generic;

public enum Controller
{
    None,
    Player,
    AI
}

public class Casillas : MonoBehaviour
{
    public int x;
    public int y;
    public string TerritoryType;

    public Controller controller = Controller.None;

    void Start()
    {
        // Inicializa el color según el estado del Inspector
        SetController(controller);
    }

    // 🔹 Captura con clic directo en la casilla
    void OnMouseDown()
    {
        GridManager grid = FindObjectOfType<GridManager>();
        GameStats stats = grid.stats;

        // Permitir captura inicial si está vacía y es la primera jugada
        if (controller == Controller.None && EsVecinaDeJugador(grid))
        {
            TryCapture(Controller.Player, grid, stats);
        }
        else if (controller == Controller.None && !HayCasillasDelJugador(grid))
        {
            // Primera casilla del jugador (ej. inicio de partida)
            TryCapture(Controller.Player, grid, stats);
        }
        else
        {
            Debug.Log($"Jugador intentó capturar ({x},{y}) pero no es vecina de ninguna casilla controlada.");
        }
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
                terrainColor = new Color(1f, 0.9f, 0.6f); // arena
                break;
            case "Oasis":
                terrainColor = Color.green; // vegetación
                break;
            case "Plain":
                terrainColor = Color.yellow; // pradera
                break;
            default:
                terrainColor = Color.gray; // fallback
                break;
        }

        // 🔹 Color del controlador
        Color controllerColor = Color.white;
        switch (controller)
        {
            case Controller.Player:
                controllerColor = Color.blue;
                break;
            case Controller.AI:
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

        Debug.Log($"Casilla ({x}, {y}) tipo {TerritoryType} ahora pertenece a {controller} y cambió a color {finalColor}");
    }

    public int GetWaterCost()
    {
        Debug.Log($"Casilla ({x},{y}) tipo: {TerritoryType}");

        switch (TerritoryType)
        {
            case "Desert":
                return 3;
            case "Oasis":
                return 1;
            case "Plain":
                return 2;
            default:
                return 2; // Costo por defecto
        }
    }

    public bool TryCapture(Controller newController, GridManager grid, GameStats stats)
    {
        int cost = GetWaterCost();

        if (newController == Controller.Player)
        {
            if (stats.playerWater >= cost)
            {
                stats.playerWater -= cost;
                SetController(newController);
                Debug.Log($"Jugador capturó ({x},{y}) pagando {cost} agua. Agua restante: {stats.playerWater}");
                return true;
            }
            else
            {
                Debug.Log($"Jugador NO tiene agua suficiente. Costo: {cost}, Agua disponible: {stats.playerWater}");
                return false;
            }
        }
        else if (newController == Controller.AI)
        {
            if (stats.aiWater >= cost)
            {
                stats.aiWater -= cost;
                SetController(newController);
                Debug.Log($"IA capturó ({x},{y}) pagando {cost} agua. Agua restante: {stats.aiWater}");
                return true;
            }
            else
            {
                Debug.Log($"IA NO tiene agua suficiente. Costo: {cost}, Agua disponible: {stats.aiWater}");
                return false;
            }
        }

        return false;
    }

    // 🔹 Verifica si esta casilla es vecina de alguna casilla del jugador
    bool EsVecinaDeJugador(GridManager grid)
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
                if (vecino != null && vecino.controller == Controller.Player)
                {
                    return true;
                }
            }
        }

        return false;
    }

    // 🔹 Verifica si el jugador ya controla alguna casilla
    bool HayCasillasDelJugador(GridManager grid)
    {
        for (int i = 0; i < grid.width; i++)
        {
            for (int j = 0; j < grid.height; j++)
            {
                Casillas c = grid.GetCasilla(i, j);
                if (c != null && c.controller == Controller.Player)
                {
                    return true;
                }
            }
        }
        return false;
    }
}