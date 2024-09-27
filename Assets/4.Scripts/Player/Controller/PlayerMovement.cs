using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
        else if (!Physics.Raycast(transform.position, Vector3.down, 0.5f))
        {
            GameManager.instance.playerState = Define.PlayerState.Jump;
            
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

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 땅을 딛고 있는지 체크
            if (Physics.Raycast(transform.position, Vector3.down, 0.5f))
            {

                GameManager.instance.playerState = Define.PlayerState.Jump;
                rb.AddForce(Vector3.up * 4, ForceMode.Impulse);
            }
        }
    }
}

#region

/*
  취업
포트폴리오 / 이력서 / 자소서 / 기술문서 / 게임잡 전용 이력서

지원 플랫폼 : 3개

지원을 하고 메모장에 정리를 좀 해줘 한다.
지원날짜/ 번호/ 지원회사/ 지원 직군/ 지원한 회사의 링크

이자기 -> 피드백
이자기 ( 이력서, 자기소개서, 기술문서)

회사를 30곳 이상 지원을 하고, 2주이상 연락이 없다면 빨간불.
반드시 요청

면접
용모/ 자신감/ 태도 / 자세 / 발성 / 발음 / 시선처리 / 겸손모드


예시

1. 111& - 게임 클라이언트
[링크]
2. 플린트 -별이되어라2 [클라이언트 엔지니어]
[링크]

3. 게임회사- 게임 [ 직군]
[링크]



문서화 방법

1. 전통적인 방식( 교과서)

2. 이력서 + 자소서 / 기술문서!!
ㄴ 기술스택 + 포트폴리오에 강점이 있을때 유리한 형식.

3. 이력서(3~4) + 자소서 (1~2)+ 기술문서(10~30)
ㄴ IT기업에 적합한 형식 
ㄴ 문서적 센스가 많이 요구됨. 

4. 브릿지 형식
ㄴ 전달력이라는 측면에서는 가장 좋음.
ㄴ 링크 방식. 


5. 혼합형 (플랫폼 혼합)


* 파생 : Han시리즈 /Pdf /PPT / Notion / Excel
* 
*/
#endregion