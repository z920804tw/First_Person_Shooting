using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("���Y��ʱӷP��")]
    public float camX;   // ���YX�b����F�ӫ�
    public float camY;   // ���YY�b����F�ӫ�

    float xRotation;
    float yRotaiton;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;// ��w�ƹ���Цb�e������
        Cursor.visible = false;                   // ���÷ƹ����
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * camX;   // ���o�ƹ���Ъ�X�b����  �b����=Y
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * camY;   // ���o�ƹ���Ъ�Y�b����  �b����=X

        // �]���w�]��XY�b���ʤ�V�bUNITY�O���઺�A�ڭ̭n�N�ƹ�X�b�ন����Y�b�AY�b�নX�b
        xRotation -= mouseY; // �N�ƹ�Y�b���ʼƭ�"����"�L��(���ܭt�t�ܥ�)
        yRotaiton += mouseX;

        xRotation = Mathf.Clamp(xRotation, -80f,50f); // ���wX�b��ʦb��30�ר�t90�׶�(���Y�M�C�Y�������)

        transform.rotation = Quaternion.Euler(xRotation, yRotaiton, 0); // �]�w��v������
    }
}
