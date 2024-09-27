using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Transform body;  // ĳ������ �� �κ�
    public Transform head;  // ĳ������ �Ӹ� �κ�
    public float sensitivity = 1f;  // ���콺 ����
    private float xRotation = 0f;  // �Ӹ��� X�� ȸ����

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // ���콺 Ŀ�� ����
        Cursor.visible = false;  // ���콺 Ŀ�� �����
    }

    private void Update()
    {
        // ���콺 �Է� �ޱ�
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Body�� Y�� ȸ�� (���콺 X�� �̵�)
        body.Rotate(Vector3.up * mouseX);

        // Head�� X�� ȸ�� (���콺 Y�� �̵�, ��/�Ʒ��� ����)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);  // ��/�Ʒ� ���� ����
        head.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
