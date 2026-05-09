using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Audio.ProcessorInstance;

public class MouseDrag : MonoBehaviour
{
    public Vector2Int gridPos;

    private Vector3 offset;

    // 元位置保存
    private Vector2Int startPos;

    void Start()
    {
        // 初期位置登録
        Board.Instance.SetPiece(gridPos,this);

        UpdatePosition();
    }

    void OnMouseDown()
    {
        startPos = gridPos;

        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    void OnMouseUp()
    {
        Vector2Int newPos =
            Board.Instance.WorldToGrid(transform.position);

        MouseDrag target =
            Board.Instance.GetPiece(newPos);

        // 他ピースと交換
        if (target != null && target != this)
        {
            // 自分削除
            Board.Instance.RemovePiece(startPos);

            // 相手削除
            Board.Instance.RemovePiece(newPos);

            // swap
            target.gridPos = startPos;
            gridPos = newPos;

            // 再登録
            Board.Instance.SetPiece(startPos, target);
            Board.Instance.SetPiece(newPos, this);

            // 見た目更新
            target.UpdatePosition();
            UpdatePosition();
        }
        else
        {
            // 空マス移動
            Board.Instance.RemovePiece(startPos);

            gridPos = newPos;

            Board.Instance.SetPiece(gridPos, this);

            UpdatePosition();
        }
    }

    void UpdatePosition()
    {
        transform.position =
            Board.Instance.GridToWorld(gridPos);
    }


    Vector3 GetMouseWorldPos()
    {
        Vector2 mousePos =
            Mouse.current.position.ReadValue();

        Vector3 screenPos = new Vector3(
            mousePos.x,
            mousePos.y,
            -Camera.main.transform.position.z
        );

        return Camera.main.ScreenToWorldPoint(screenPos);
    }
}
