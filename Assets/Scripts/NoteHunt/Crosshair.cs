using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private Texture2D CrossHair;

    private Camera cam;
                     


    Vector3 pos;

    //Parte aggiunta 
    private void Start()
    {
        cam = Camera.main;
    }


    private void OnEnable()
    {
        Vector2 v = new Vector2(CrossHair.width / 2, CrossHair.height / 2);
        Cursor.SetCursor(CrossHair, v, CursorMode.Auto);
    }

    private void Update()
    {
        pos = Input.mousePosition;
        pos.z = cam.nearClipPlane;
        Vector3 difference = Camera.main.ScreenToWorldPoint(pos) - transform.position;

    }


    public void OnMouseDown()
    {
        // Destroy the gameObject after clicking on it
        Destroy(gameObject);
    }

}
