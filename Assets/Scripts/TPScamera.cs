using UnityEngine;

public class TPScamera : MonoBehaviour
{
    public float mouseSensitivity = 2f;
    public float minPitch = -85f;
    public float maxPitch = 85f;

    private float pitch;

    void Update()
    {
        if (!GameController.gameController.isPlaying || GameController.gameController.isPaused) return;

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch - mouseY, minPitch, maxPitch);
        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
