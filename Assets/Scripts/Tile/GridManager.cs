using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 10;   // número de columnas
    public int height = 10;  // número de filas
    public GameObject casillaPrefab;

    public GameStats stats; // Referencia a GameStats para actualizar recursos

    private Casillas[,] grid;

    // 🔹 Propiedades para conteo de territorios
    public int Player1Territories { get; private set; }
    public int Player2Territories { get; private set; }
    public int TotalTerritories { get; private set; }

    void Start()
    {
        GenerateGrid();
        CenterCamera();
        TotalTerritories = width * height;
    }

    void GenerateGrid()
    {
        grid = new Casillas[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject casillaObj = Instantiate(casillaPrefab, new Vector3(x, y, 0), Quaternion.identity);
                casillaObj.name = $"Casilla {x},{y}";
                Casillas casilla = casillaObj.GetComponent<Casillas>();
                casilla.x = x;
                casilla.y = y;

                // 🔹 Asignar tipo de territorio
                casilla.TerritoryType = AssignTerritoryType(x, y);

                // 🔹 Inicialmente sin dueño
                casilla.controller = ControllerManager.Controller.None;
                casilla.ActualizarColor();

                grid[x, y] = casilla;
            }
        }
    }

    private string AssignTerritoryType(int x, int y)
    {
        int centerX = width / 2;
        int centerY = height / 2;

        if (Mathf.Abs(x - centerX) + Mathf.Abs(y - centerY) < 2)
            return "Oasis";
        else if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
            return "Desert";
        else
            return "Plain";
    }

    public Casillas GetCasilla(int x, int y)
    {
        return grid[x, y];
    }

    void CenterCamera()
    {
        Camera cam = Camera.main;
        cam.transform.position = new Vector3(width / 2f, height / 2f, -10);

        float aspectRatio = (float)Screen.width / Screen.height;
        float gridRatio = (float)width / height;

        if (gridRatio > aspectRatio)
            cam.orthographicSize = width / 2f / aspectRatio;
        else
            cam.orthographicSize = height / 2f;
    }

    // 🔹 Método para actualizar conteo de territorios
    public void UpdateTerritoryCounts()
    {
        Player1Territories = 0;
        Player2Territories = 0;

        foreach (Casillas casilla in grid)
        {
            if (casilla.controller == ControllerManager.Controller.Player1)
                Player1Territories++;
            else if (casilla.controller == ControllerManager.Controller.Player2)
                Player2Territories++;
        }
    }
}
