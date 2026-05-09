using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.Audio.ProcessorInstance;

public class Board : MonoBehaviour
{
    public static Board Instance;

    public float cellSize = 1f;

    // Dictionaryで管理
    public Dictionary<Vector2Int, MouseDrag> board =
        new Dictionary<Vector2Int, MouseDrag>();

    private void Awake()
    {
        Instance = this;
    }

    // マス座標 → ワールド座標
    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(
            gridPos.x * cellSize,
            gridPos.y * cellSize,
            0
        );
    }

    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x / cellSize);
        int y = Mathf.RoundToInt(worldPos.y / cellSize);

        return new Vector2Int(x, y);
    }


    public MouseDrag GetPiece(Vector2Int pos)
    {
        if (board.ContainsKey(pos))
        {
            return board[pos];
        }

        return null;
    }

    // 登録
    public void SetPiece(Vector2Int pos, MouseDrag piece)
    {
        board[pos] = piece;
    }

    // 削除
    public void RemovePiece(Vector2Int pos)
    {
        if (board.ContainsKey(pos))
        {
            board.Remove(pos);
        }
    }
}
