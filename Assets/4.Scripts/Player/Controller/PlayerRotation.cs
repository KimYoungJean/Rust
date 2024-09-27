using Unity.Mathematics;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Transform body;  // ĳ������ �� �κ�    
    public Transform head;  // ĳ������ �Ӹ� �κ�    
    public float sensitivity = 1f;  // ���콺 ����
    private float xRotation = 0f;  // �Ӹ��� X�� ȸ����


    float currentY;
    float targetY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // ���콺 Ŀ�� ����
        Cursor.visible = false;  // ���콺 Ŀ�� �����

       
    }

    private void Update()
    {
        currentY = body.position.y + 1.8f;
        targetY = body.position.y + 0.9f;

        // ���콺 �Է� �ޱ�
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Body�� Y�� ȸ�� (���콺 X�� �̵�)
        body.Rotate(Vector3.up * mouseX);

        // Head�� X�� ȸ�� (���콺 Y�� �̵�, ��/�Ʒ��� ����)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);  // ��/�Ʒ� ���� ����
        head.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if(GameManager.instance.playerState ==Define.PlayerState.Crouch)
        {
            
            head.position = new Vector3(head.position.x, Mathf.Lerp(head.position.y, targetY, 0.1f), head.position.z);
        }
        else
        {
            
            head.position = new Vector3(head.position.x, Mathf.Lerp(head.position.y, currentY, 0.1f), head.position.z);
        }

    }
}
