using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Texture2D CrossHair;
                     
    [SerializeField] private float MuzzleFlashSpeed;                    //fiamma sparata velocit�

    Vector3 pos;
    Vector3 mouseWorldPosition;



    private void Update()
    {
        pos = Input.mousePosition;
        Vector2 v = new Vector2(pos.x - CrossHair.width/2, pos.y - CrossHair.height/2);
        Cursor.SetCursor(CrossHair, v, CursorMode.Auto);

        Vector3 difference = Camera.main.ScreenToWorldPoint(pos) - transform.position;


            var bullet = Instantiate(bulletPrefab, mouseWorldPosition, Quaternion.Euler(0,90,0), this.transform);
            bullet.SetActive(true);



            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Nota") && hit.collider.CompareTag("Menu"))
            {
                Debug.Log("RAYCAST");

                hit.collider.GetComponent<MoveNota>().setColpito();
            }

            Destroy(bullet, MuzzleFlashSpeed);

    }

    public void OnMouseDown()
    {
        // Destroy the gameObject after clicking on it
        Destroy(gameObject);
    }

}
