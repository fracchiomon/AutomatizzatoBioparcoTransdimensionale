using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private Texture2D CrossHairOn;

    private void OnEnable()
    {
        Vector2 v = new Vector2(CrossHairOn.width / 2, CrossHairOn.height / 2);
        Cursor.SetCursor(CrossHairOn, v, CursorMode.Auto);
    }

    public void OnMouseDown()
    {
        Destroy(gameObject);                 // Destroy the gameObject after clicking on it
    }

}
