using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f; // �̵� �ӵ�
    public Rigidbody rb; // Rigidbody ������Ʈ
    public Transform body; // ĳ������ �� �κ� (ȸ�� ó����)
    private CapsuleCollider capsuleCollider; // ĸ�� �ݶ��̴� ������Ʈ
    private Vector3 moveDirection; // �̵� ����


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
        // Rigidbody�� ����� �̵� ó��
        if (moveDirection != Vector3.zero)
        {

            rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
        }



    }

    public void Move()
    {
        // W, A, S, D Ű �Է� �ޱ�
        float horizontal = Input.GetAxis("Horizontal"); // A, D Ű �Է�
        float vertical = Input.GetAxis("Vertical"); // W, S Ű �Է�

        // �Էµ� ������ ����ȭ
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        #region ĳ���� ����


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
            // ���� �� ������ �������� �̵� ���� ���
            moveDirection = body.forward * vertical + body.right * horizontal;
        }
        else
        {
            moveDirection = Vector3.zero; // Ű �Է��� ������ ����
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ���� ��� �ִ��� üũ
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
  ���
��Ʈ������ / �̷¼� / �ڼҼ� / ������� / ������ ���� �̷¼�

���� �÷��� : 3��

������ �ϰ� �޸��忡 ������ �� ���� �Ѵ�.
������¥/ ��ȣ/ ����ȸ��/ ���� ����/ ������ ȸ���� ��ũ

���ڱ� -> �ǵ��
���ڱ� ( �̷¼�, �ڱ�Ұ���, �������)

ȸ�縦 30�� �̻� ������ �ϰ�, 2���̻� ������ ���ٸ� ������.
�ݵ�� ��û

����
���/ �ڽŰ�/ �µ� / �ڼ� / �߼� / ���� / �ü�ó�� / ��ո��


����

1. 111& - ���� Ŭ���̾�Ʈ
[��ũ]
2. �ø�Ʈ -���̵Ǿ��2 [Ŭ���̾�Ʈ �����Ͼ�]
[��ũ]

3. ����ȸ��- ���� [ ����]
[��ũ]



����ȭ ���

1. �������� ���( ������)

2. �̷¼� + �ڼҼ� / �������!!
�� ������� + ��Ʈ�������� ������ ������ ������ ����.

3. �̷¼�(3~4) + �ڼҼ� (1~2)+ �������(10~30)
�� IT����� ������ ���� 
�� ������ ������ ���� �䱸��. 

4. �긴�� ����
�� ���޷��̶�� ���鿡���� ���� ����.
�� ��ũ ���. 


5. ȥ���� (�÷��� ȥ��)


* �Ļ� : Han�ø��� /Pdf /PPT / Notion / Excel
* 
*/
#endregion