using UnityEngine;
using UnityEngine.InputSystem;

public class MouseDrag : MonoBehaviour
{
    private Vector3 offset;

    /*
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */

    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
        Debug.Log("ƒhƒ‰ƒbƒO");
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        Vector3 screenPos = new Vector3(
            mousePos.x,
            mousePos.y,
            -Camera.main.transform.position.z
        );

        return Camera.main.ScreenToWorldPoint(screenPos);
    }
}
