using JetBrains.Annotations;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    private float rotationAmount = 90.0f;
    private float rotationSpeed = 5.0f; // Velocità di rotazione
    private float startTime; // Tempo di inizio della rotazione
    private bool isRotating = false;
    [SerializeField] private float moveSpeed = 6.0f;
    private float minX = -4.0f;
    private float maxX = 4.0f;
    public void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);
        Vector3 newPosition = transform.position + movement;

        // Assicurati che la nuova posizione sia compresa tra minX e maxX
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        transform.position = newPosition;

        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")) && !isRotating)
        {
            isRotating = true;
            startTime = Time.time; // Memorizza il tempo di inizio
        }

        if (isRotating)
        {
            float elapsedTime = Time.time - startTime;
            float rotationAngle;

            if (elapsedTime < Mathf.PI / (2 * rotationSpeed))
            {
                rotationAngle = Mathf.Sin(elapsedTime * rotationSpeed) * rotationAmount;
            }
            else
            {
                float remainingTime = elapsedTime - Mathf.PI / (2 * rotationSpeed);
                rotationAngle = Mathf.Sin(Mathf.PI / 2 + remainingTime * (rotationSpeed / 2)) * rotationAmount;
            }

            if (elapsedTime >= Mathf.PI / rotationSpeed)
            {
                isRotating = false;
                rotationAngle = 0.0f;
            }

            transform.localRotation = Quaternion.Euler(rotationAngle, 0.0f, 0.0f);
        }

    }
}
