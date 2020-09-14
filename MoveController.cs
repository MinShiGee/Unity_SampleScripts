using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MoveController : MonoBehaviour
{
    [Header("Movement Speed Setting")]
    [SerializeField] [Range(0, 12)] private float movementSpeed = 4;
    [SerializeField] [Range(0, 5)] private float runMultiplier = 3f;
    [Header("Jumping Data Setting")]
    [SerializeField] [Range(0, 12)] private float JumpForce = 6f;
    [Header("Slope Setting")]
    [SerializeField] [Range(0, 60)] private float slopeForce = 45f;
    [SerializeField] [Range(0, 1.5f)] private float slopeRayDistanceMul = 0.8f;

    [Header("For Checking")]
    [SerializeField] private bool isJumping = false;
    [SerializeField] private bool isSlope = false;

    private float gravity = -9.81f;
    private Vector3 velocity = default;
    private CharacterController characterController = default;
    Vector3 movement = default;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetGravity();
        PlayerMove();
    }
    private void PlayerMove()
    {
        UpdateInit();
        InputData();

        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        movement = transform.right * xInput + transform.forward * zInput;

        if (Input.GetKey(KeyCode.LeftShift))
            characterController.Move(movement * Time.deltaTime * movementSpeed * runMultiplier);
        else
            characterController.Move(movement * movementSpeed * Time.deltaTime);

        if ((xInput != 0 || zInput != 0) && OnSlope())
            characterController.Move(Vector3.down * characterController.height * slopeForce * Time.deltaTime);

    }
    private void InputData()
    {
        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            GetJump(JumpForce);
        }
    }
    private void GetJump(float Force)
    {
        velocity.y = Force;
        isJumping = true;
    }
    private void GetGravity()
    {
        if (characterController.isGrounded && velocity.y < 0f)
            velocity.y = 0f;

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }
    private bool OnSlope()
    {
        if (isJumping)
            return false;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, characterController.height * slopeRayDistanceMul))
            if (hit.normal != Vector3.up)
                return true;

        return false;
    }
    private void UpdateInit()
    {
        JumpInit();
        isSlope = OnSlope();
    }
    private void JumpInit()
    {
        if (characterController.isGrounded)
            isJumping = false;
    }
}
