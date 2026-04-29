using Unity.VisualScripting;
using UnityEngine;

public enum Controller
{
    None,
    Player1,
    Player2
}

public class TileSetManager : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;

    [Header("Color Jugadores")]
    public Color player1Color;
    public Color player2Color;


    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;

    [Header("Tiles")]
    [SerializeField] GameObject plains;
    [SerializeField] GameObject dunes;
    [SerializeField] GameObject mountains;
    [SerializeField] GameObject oasis;

    GameObject[,] grid;

    private void CreateGrid()
    {
        Controller con = Controller.None;
        float offsetx = width/2;
        float offsety = height/2;
        grid = new GameObject[width,height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0 ; y < height; y++)
            {
                //Centrar el grid en el objeto que lo llama
                Vector2 pos = new Vector2(x-offsetx,y-offsety);
                pos.x += transform.position.x;
                if (width % 2 == 0)
                {
                    pos.x += 0.5f;
                }                
                pos.y += transform.position.y;
                if (height % 2 == 0)
                {
                    pos.y += 0.5f;
                }
                GameObject tileType = plains;

                //Dar el tipo de terreno
                if (x == 0 || y == 0 || x == width-1 || y == height-1)
                {
                    tileType = plains;
                }
                else
                {
                    int RNG = Random.Range(1,4);
                    switch (RNG)
                    {
                        case 1:
                            tileType = dunes;
                            break;
                        case 2:
                            tileType = mountains;
                            break;
                        case 3:
                            tileType = oasis;
                            break;
                        default:
                            tileType = plains;
                            break;
                    }
                }

                if (x==0 && y == 0)
                {
                    con = Controller.Player1;
                }
                else if (x==width-1 && y == height-1)
                {
                    con = Controller.Player2;
                }
                else
                {
                    con = Controller.None;
                }

                GameObject tile = Instantiate(tileType,pos, Quaternion.identity);
                tile.GetComponent<Tile>().controller = con;
                tile.name = $"{x}{y}";
                grid[x,y] = tile;
                
            }
        }
    }

    void Turno(Player player)
    {
        for (int x = 0; x < width ; x++)
        {
            for (int y = 0 ; y < height; y++)
            {
                Tile tile = grid[x,y].GetComponent<Tile>();

                if (tile.controller == player.player)
                {
                    player.GetWater(tile.waterPayout);
                }
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateGrid();
        plains.SetActive(false);
        dunes.SetActive(false);
        mountains.SetActive(false);
        oasis.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null){
                    for (int x = 0; x< width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            if (grid[x,y] != null)
                            {
                                if (hit.collider.gameObject == grid[x,y])
                                {
                                    Debug.Log(grid[x,y].name);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
