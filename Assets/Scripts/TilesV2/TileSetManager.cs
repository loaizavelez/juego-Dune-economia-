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
    int whoIsPlaying;
    bool activeTurn;

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

                GameObject tile = Instantiate(tileType, pos, Quaternion.identity);

                Tile tileScript = tile.GetComponent<Tile>();

                tileScript.Init(this, new Vector2Int(x, y));
                tileScript.controller = con;

                tile.name = $"{x}{y}";
                grid[x, y] = tile;

            }
        }
    }

    void SetGrid()
    {
        for (int x = 0; x< width; x++)
        {
            for (int y = 0; y< height; y++)
            {
                if (grid[x, y] == null) continue;

                Tile tile = grid[x, y].GetComponent<Tile>();

                if (tile != null)
                {
                    tile.UpdateTile();
                }
            }
        }
    }

    void InicioTurno(Player player)
    {
        for (int x = 0; x < width ; x++)
        {
            for (int y = 0 ; y < height; y++)
            {
                Tile tile = grid[x,y].GetComponent<Tile>();
    
                if (tile.controller == player.player)
                {
                    player.GetWater(tile.waterPayout);
                    player.GetSpice(tile.spicePayout);
                    player.GetMetals(tile.metalsPayout);
                }
            }
        }
        activeTurn = true;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateGrid();
        SetGrid();

        plains.SetActive(false);
        dunes.SetActive(false);
        mountains.SetActive(false);
        oasis.SetActive(false);

        player1.GetComponent<Player>().water = 5;
        player2.GetComponent<Player>().water = 5;

        whoIsPlaying = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (whoIsPlaying == 1)
        {
            
            InicioTurno(player1.GetComponent<Player>());
            do
            {
                if (Input.GetMouseButtonDown(0))
                {
                    checkTile();
                }
            } while (activeTurn);
            whoIsPlaying = 2;
        }
        if (whoIsPlaying == 2)
        {
            InicioTurno(player2.GetComponent<Player>());
            do
            {
                if (Input.GetMouseButtonDown(0))
                {
                    checkTile();
                }
            } while (activeTurn);
            whoIsPlaying = 1;
        }
    }

    void checkTile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider != null)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (grid[x, y] != null)
                        {
                            if (hit.collider.gameObject == grid[x, y])
                            {
                                Debug.Log(grid[x, y].name);
                            }
                        }
                    }
                }
            }
        }
    }

    public Tile GetTile(Vector2Int pos)
    {
        if (pos.x < 0 || pos.y < 0 || pos.x >= width || pos.y >= height)
        {
            return null;
        }            
        return grid[pos.x,pos.y].GetComponent<Tile>();
    }

    public void pass()
    {
        activeTurn = false;
    }
}
