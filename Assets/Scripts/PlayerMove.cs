using UnityEngine;
[RequireComponent (typeof(CharacterController))]

public class PlayerMove : MonoBehaviour
{
    CharacterController cc;
    public float speed = 15f;
    public float rotateSpeed = 1f;
    public float jumpHeight = 1.5f;
    public float gravityValue = -9.81f;
    public float fall;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    private void Update()
    {

        if (cc.isGrounded && fall < 0f)
        {
            fall = -2f;
        }

        if (cc.isGrounded && Input.GetButtonDown("Jump"))
        {
            fall = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        }

        fall += gravityValue * Time.deltaTime;

        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Horizontal");

        Vector3 dir = transform.forward * v * speed;
        dir.y = fall;

        transform.Rotate(0f, r * rotateSpeed, 0f);
        cc.Move(dir * Time.deltaTime);
    }

}
