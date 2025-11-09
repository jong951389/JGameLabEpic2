using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("PlayerControll")]
    [Tooltip("플레이어 움직임 변수")]
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] PlayerInputActions actions;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        actions = new PlayerInputActions(); // 반드시 초기화 필요
    }

    private void OnEnable()
    {
        actions.PlayerActions.Enable(); // Input 활성화
    }

    private void OnDisable()
    {
        actions.PlayerActions.Disable(); // 비활성화 시 Input 해제
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        Vector2 moveInput = actions.PlayerActions.Move.ReadValue<Vector2>();
        Vector2 move = new Vector3(moveInput.x, moveInput.y);

        rb.linearVelocity = move * moveSpeed * Time.fixedDeltaTime; // Rigidbody 물리 이동

        //플레이어가 움직인 쪽을 바라보게
        if (moveInput.sqrMagnitude > 0.001f) // 입력이 있을 때만 회전
        {
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg - 90f;
            rb.SetRotation(angle); // Rigidbody2D의 회전값을 직접 설정
        }
    }

   
}
