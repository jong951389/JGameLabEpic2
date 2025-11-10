using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    [Header("Sword")]
    [SerializeField] Transform Scabbard;
    [SerializeField] Transform player;
    [SerializeField] float swordOffset = 0.5f;
    [SerializeField] PlayerInputActions action;
    [SerializeField] GameObject slashPrefab;
    bool isHolding;
    Transform heldObject;
    Vector2 dropPoint;


    private void OnEnable()
    {
        action.SwordActions.Enable();
        action.SwordActions.pick.started += PickSword;
        action.SwordActions.pick.canceled += Baldo;
    }
    private void Awake()
    {
        // null일 경우 새로 생성
        if (action == null)
            action = new PlayerInputActions();
    }

    private void OnDisable()
    {
        action.SwordActions.pick.started -= PickSword;
        action.SwordActions.pick.canceled -= Baldo;
        action.SwordActions.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region 마우스로 검 집기
        if (isHolding && heldObject != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            heldObject.position = mousePos;
        }
        #endregion
    }    

    void PickSword(InputAction.CallbackContext ctx)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject.layer == 3)
        {
            heldObject = hit.transform;
            isHolding = true;
        }
    }

    void DropSword()
    {
        if (heldObject == null) return;

        heldObject.transform.position = new Vector3(Scabbard.position.x, Scabbard.position.y+swordOffset, heldObject.position.z);

        isHolding = false;
        heldObject = null;
    }

    void Baldo(InputAction.CallbackContext ctx)
    {
        if (!isHolding) return;

        //시작 위치 검집
        Vector2 spawnPos = new Vector3(player.position.x, player.position.y);

        //목표 위치(마우스 커서 위치)
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //방향 및 각도
        Vector2 dir = (mouseWorld - spawnPos).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        //프리팹 생성시 회전
        Quaternion rot = Quaternion.Euler(0,0, angle);

        Instantiate(slashPrefab, transform.position, rot);
        DropSword();
    }
}
