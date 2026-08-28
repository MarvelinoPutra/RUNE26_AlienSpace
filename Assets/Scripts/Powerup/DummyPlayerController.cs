using UnityEngine;

public class DummyPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float moveY = Input.GetAxis("Vertical"); // Tombol W/S atau Panah Atas/Bawah
        Vector3 movement = new Vector3(0f, moveY, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
}