using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    public float speed = 2f; // 이동 속도
    public Rigidbody rb; // Rigidbody 컴포넌트
    public Transform body; // 캐릭터의 몸 부분 (회전 처리됨)
    private CapsuleCollider capsuleCollider; // 캡슐 콜라이더 컴포넌트
    private Vector3 moveDirection; // 이동 방향


    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {

        if(PhotonNetwork.IsConnected && !photonView.IsMine)
        {
            return;
        }

        Debug.Log(GameManager.instance.playerState);
        Move();
        Jump();

        if (GameManager.instance.playerState == Define.PlayerState.Crouch || GameManager.instance.playerState == Define.PlayerState.Jump)
        {
            capsuleCollider.height = 2;

        }

        else
        {
            capsuleCollider.height = 4;

        }


    }

    private void FixedUpdate()
    {
        if (PhotonNetwork.IsConnected && !photonView.IsMine)
        {
            return;
        }

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
       /* else if (!Physics.Raycast(transform.position, Vector3.down, 0.2f))
        {
            Debug.Log("Jump");
            GameManager.instance.playerState = Define.PlayerState.Jump;
            
        }  */      
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

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 땅을 딛고 있는지 체크
            if (Physics.Raycast(transform.position, Vector3.down, 0.2f))
            {

                GameManager.instance.playerState = Define.PlayerState.Jump;
                rb.AddForce(Vector3.up * 4, ForceMode.Impulse);
            }
        }
    }
}

