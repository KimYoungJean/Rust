using Unity.Mathematics;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Transform body;  // 캐릭터의 몸 부분    
    public Transform head;  // 캐릭터의 머리 부분    
    public float sensitivity = 1f;  // 마우스 감도
    private float xRotation = 0f;  // 머리의 X축 회전값


    float currentY;
    float targetY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // 마우스 커서 고정
        Cursor.visible = false;  // 마우스 커서 숨기기

       
    }

    private void Update()
    {
        currentY = body.position.y + 1.8f;
        targetY = body.position.y + 0.9f;

        // 마우스 입력 받기
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Body의 Y축 회전 (마우스 X축 이동)
        body.Rotate(Vector3.up * mouseX);

        // Head의 X축 회전 (마우스 Y축 이동, 위/아래로 제한)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);  // 위/아래 각도 제한
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
