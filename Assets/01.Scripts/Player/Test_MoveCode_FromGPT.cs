using UnityEngine;

public class AdvancedThirdPersonCharacterController : MonoBehaviour
{
    public Transform playerCamera; // �÷��̾� ī�޶�
    public Animator animator; // ĳ���� �ִϸ��̼�
    public float moveSpeed = 5f;
    public float rotationSpeed = 120f;
    public float jumpForce = 8f;
    public LayerMask groundMask; // ������ ������ ���̾� ����ũ
    public float groundCheckDistance = 0.1f; // ���� ���� �Ÿ�
    private CharacterController controller;
    private bool isGrounded;
    private Vector3 moveDirection;
    private Vector3 groundNormal;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ���
        Cursor.visible = false; // ���콺 Ŀ�� ����
    }

    private void Update()
    {
        // �̵� �Է�
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // ī�޶� ȸ�� �Է�
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // ĳ���� ȸ��
        transform.Rotate(Vector3.up * mouseX * rotationSpeed * Time.deltaTime);

        // ���� ȸ�� (ī�޶� ȸ��)
        playerCamera.localRotation = Quaternion.Euler(
            Mathf.Clamp(playerCamera.localRotation.eulerAngles.x - mouseY * rotationSpeed * Time.deltaTime, -90f, 90f),
            0f,
            0f
        );

        // �̵� ���� ���
        Vector3 forward = Vector3.ProjectOnPlane(playerCamera.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(playerCamera.right, Vector3.up).normalized;
        moveDirection = forward * verticalInput + right * horizontalInput;

        // ���� ����
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundCheckDistance, groundMask);
        if (isGrounded)
        {
            groundNormal = hit.normal;
        }
    }

    private void FixedUpdate()
    {
        // ������ ������ �� ƨ�� ����
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
