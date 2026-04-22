using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 10;   // número de columnas
    public int height = 10;  // número de filas
    public GameObject casillaPrefab;

    private Casillas[,] grid;

    void Start()
    {
        GenerateGrid();
        CenterCamera();
    }

    void GenerateGrid()
    {
        grid = new Casillas[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Instancia en el plano X–Y (2D)
                GameObject casillaObj = Instantiate(casillaPrefab, new Vector3(x, y, 0), Quaternion.identity);
                casillaObj.name = $"Casilla {x},{y}";
                Casillas casilla = casillaObj.GetComponent<Casillas>();
                casilla.x = x;
                casilla.y = y;
                grid[x, y] = casilla;
            }
        }
    }

    public Casillas GetCasilla(int x, int y)
    {
        return grid[x, y];
    }

    void CenterCamera()
    {
        Camera cam = Camera.main;
        // Colocar la cámara en el centro del tablero
        cam.transform.position = new Vector3(width / 2f, height / 2f, -10);

        // Ajustar el tamaño ortográfico para que se vean todas las casillas
        float aspectRatio = (float)Screen.width / Screen.height;
        float gridRatio = (float)width / height;

        if (gridRatio > aspectRatio)
        {
            cam.orthographicSize = width / 2f / aspectRatio;
        }
        else
        {
            cam.orthographicSize = height / 2f;
        }
    }
}
