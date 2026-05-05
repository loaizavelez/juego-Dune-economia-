using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Tile Data")]
    public Controller controller = Controller.None;
    public int id;

    [Header("Economy")]
    public int waterCost;
    public int waterPayout;
    public int spicePayout;
    public int metalsPayout;

    
    [Header("Grid")]
    public TileSetManager TSM;
    public Vector2Int gridPos;

    [Header("Quadrants")]
    public SpriteRenderer topLeft;
    public SpriteRenderer topRight;
    public SpriteRenderer bottomLeft;
    public SpriteRenderer bottomRight;

    [Header("Sprites")]
    public SubTiles sprites;

    private void Awake()
    {
        topLeft.color = Color.white;
        topRight.color = Color.white;
        bottomLeft.color = Color.white;
        bottomRight.color = Color.white;
    }

    private void Start()
    {
        DrawTile();
    }

    // ----------------------------
    public void Init(TileSetManager manager, Vector2Int pos)
    {
        TSM = manager;
        gridPos = pos;
    }

    bool HasNeighbor(Vector2Int dir)
    {
        if (TSM == null) return false;
        return TSM.GetTile(gridPos + dir) != null && TSM.GetTile(gridPos+dir).id == this.id;
    }
    void DrawTile()
    {
        Color c = Color.white;
        switch (controller)
        {
            case Controller.Player1:
                c = Color.Lerp(Color.white, TSM.player1Color,0.5f);
            break;   
            case Controller.Player2:
                c = Color.Lerp(Color.white, TSM.player2Color,0.5f);
            break;
            case Controller.None:
                c = Color.white;
            break;
        }
        topLeft.color = c;
        topRight.color = c;
        bottomLeft.color = c;
        bottomRight.color = c;
    }
    public void UpdateTile()
    {
        // Cardinal
        bool up = HasNeighbor(Vector2Int.up);
        bool down = HasNeighbor(Vector2Int.down);
        bool left = HasNeighbor(Vector2Int.left);
        bool right = HasNeighbor(Vector2Int.right);

        // Diagonals
        bool upRight = HasNeighbor(new Vector2Int(1, 1));
        bool upLeft = HasNeighbor(new Vector2Int(-1, 1));
        bool downRight = HasNeighbor(new Vector2Int(1, -1));
        bool downLeft = HasNeighbor(new Vector2Int(-1, -1));

        // Filter diagonals
        if (!up || !right) upRight = false;
        if (!up || !left) upLeft = false;
        if (!down || !right) downRight = false;
        if (!down || !left) downLeft = false;

        topLeft.sprite = GetTopLeft(up, left, upLeft);
        topRight.sprite = GetTopRight(up, right, upRight);
        bottomLeft.sprite = GetBottomLeft(down, left, downLeft);
        bottomRight.sprite = GetBottomRight(down, right, downRight);
    }

    // ----------------------------
    // QUADRANTS (directional)
    // ----------------------------

    Sprite GetTopLeft(bool up, bool left, bool diag)
    {
        if (up && left)
        {
            if (diag) return sprites.center;
            else return sprites.innerCornerTL;
        }

        if (up) return sprites.edgeLeft;    // ⬅️ Borde izquierdo (vertical)
        if (left) return sprites.edgeTop;   // ⬅️ Borde superior (horizontal)

        return sprites.outerCornerTL;
    }

    Sprite GetTopRight(bool up, bool right, bool diag)
    {
        if (up && right)
        {
            if (diag) return sprites.center;
            else return sprites.innerCornerTR;
        }

        if (up) return sprites.edgeRight;   // ⬅️ Borde derecho (vertical)
        if (right) return sprites.edgeTop;  // ⬅️ Borde superior (horizontal)

        return sprites.outerCornerTR;
    }

    Sprite GetBottomLeft(bool down, bool left, bool diag)
    {
        if (down && left)
        {
            if (diag) return sprites.center;
            else return sprites.innerCornerBL;
        }

        if (down) return sprites.edgeLeft;    // ⬅️ Borde izquierdo (vertical)
        if (left) return sprites.edgeBottom;  // ⬅️ Borde inferior (horizontal)

        return sprites.outerCornerBL;
    }

    Sprite GetBottomRight(bool down, bool right, bool diag)
    {
        if (down && right)
        {
            if (diag) return sprites.center;
            else return sprites.innerCornerBR;
        }

        if (down) return sprites.edgeRight;   // ⬅️ Borde derecho (vertical)
        if (right) return sprites.edgeBottom; // ⬅️ Borde inferior (horizontal)

        return sprites.outerCornerBR;
    }

    // ----------------------------
    public void UpdateNeighbors()
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Tile t = TSM.GetTile(gridPos + new Vector2Int(x, y));
                if (t != null)
                    t.UpdateTile();
            }
        }
    }

    // ----------------------------
    [System.Serializable]
    public class SubTiles
    {
        [Header("Center")]
        public Sprite center;

        [Header("Edges")]
        public Sprite edgeTop;
        public Sprite edgeBottom;
        public Sprite edgeLeft;
        public Sprite edgeRight;

        [Header("Inner Corners")]
        public Sprite innerCornerTL;
        public Sprite innerCornerTR;
        public Sprite innerCornerBL;
        public Sprite innerCornerBR;

        [Header("Outer Corners")]
        public Sprite outerCornerTL;
        public Sprite outerCornerTR;
        public Sprite outerCornerBL;
        public Sprite outerCornerBR;
    }
}