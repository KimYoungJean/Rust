using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f; // 이동 속도
    public Rigidbody rb; // Rigidbody 컴포넌트
    public Transform body; // 캐릭터의 몸 부분 (회전 처리됨)

    private Vector3 moveDirection; // 이동 방향


    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        Move();
    }

    private void FixedUpdate()
    {
        // Rigidbody를 사용해 이동 처리
        if (moveDirection != Vector3.zero)
        {

            rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
        }
    }

    public void Move()
    {
        // W, A, S, D 키 입력 받기
        float horizontal = Input.GetAxis("Horizontal"); // A, D 키 입력
        float vertical = Input.GetAxis("Vertical"); // W, S 키 입력

        // 입력된 방향을 정규화
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        #region 캐릭터 상태
       

        if (Input.GetKey(KeyCode.LeftShift))
        {
            GameManager.instance.playerState = Define.PlayerState.Run;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            GameManager.instance.playerState = Define.PlayerState.Crouch;
        }
        else if (direction.magnitude >= 0.1f)
        {
            GameManager.instance.playerState = Define.PlayerState.Walk;
        }
        else
        {
            GameManager.instance.playerState = Define.PlayerState.Idle;
        }

        switch (GameManager.instance.playerState)
        {
            case Define.PlayerState.Crouch:
                speed = 1;
                break;
            case Define.PlayerState.Walk:
                speed = 2;
                break;
            case Define.PlayerState.Run:
                speed = 3;
                break;
            case Define.PlayerState.Idle:
                speed = 0;
                break;
            default:
                speed = 0;
                break;
        }

        #endregion

        if (direction.magnitude >= 0.1f)
        {
            // 몸의 앞 방향을 기준으로 이동 방향 계산
            moveDirection = body.forward * vertical + body.right * horizontal;
        }
        else
        {
            moveDirection = Vector3.zero; // 키 입력이 없으면 정지
        }
    }

}
