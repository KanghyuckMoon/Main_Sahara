using UnityEngine;

public class AdvancedThirdPersonCharacterController : MonoBehaviour
{
    public Transform playerCamera; // 플레이어 카메라
    public Animator animator; // 캐릭터 애니메이션
    public float moveSpeed = 5f;
    public float rotationSpeed = 120f;
    public float jumpForce = 8f;
    public LayerMask groundMask; // 지면을 감지할 레이어 마스크
    public float groundCheckDistance = 0.1f; // 지면 감지 거리
    private CharacterController controller;
    private bool isGrounded;
    private Vector3 moveDirection;
    private Vector3 groundNormal;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 잠금
        Cursor.visible = false; // 마우스 커서 숨김
    }

    private void Update()
    {
        // 이동 입력
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 카메라 회전 입력
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 캐릭터 회전
        transform.Rotate(Vector3.up * mouseX * rotationSpeed * Time.deltaTime);

        // 수직 회전 (카메라 회전)
        playerCamera.localRotation = Quaternion.Euler(
            Mathf.Clamp(playerCamera.localRotation.eulerAngles.x - mouseY * rotationSpeed * Time.deltaTime, -90f, 90f),
            0f,
            0f
        );

        // 이동 벡터 계산
        Vector3 forward = Vector3.ProjectOnPlane(playerCamera.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(playerCamera.right, Vector3.up).normalized;
        moveDirection = forward * verticalInput + right * horizontalInput;

        // 지면 감지
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundCheckDistance, groundMask);
        if (isGrounded)
        {
            groundNormal = hit.normal;
        }
    }

    private void FixedUpdate()
    {
        // 경사면을 내려갈 때 튕김 방지
        if (!isGrounded)
        {
            return;
        }

        Vector3 targetVelocity = (Quaternion.FromToRotation(transform.up, groundNormal) * moveDirection).normalized * moveSpeed;
        Vector3 velocity = controller.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -Mathf.Infinity, Mathf.Infinity);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -Mathf.Infinity, Mathf.Infinity);
        velocityChange.y = 0;

        controller.Move(velocityChange * Time.fixedDeltaTime);
    }
}
