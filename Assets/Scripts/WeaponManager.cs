using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("�Z��")]
    public GameObject[] weaponObjects;        // �Z���M��


    [Header("�]�w")]
    public TextMeshProUGUI currentWeapon;

    int weaponNumber = 0;                     // �ثe��ܪZ�������ǽs��
    GameObject weaponInUse;                   // �ثe��ܪZ��
    void Start()
    {
        weaponInUse = weaponObjects[weaponNumber];
        
        updateCurrentWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
        updateCurrentWeapon();

    }
    void updateCurrentWeapon()
    {
        if (currentWeapon != null)
        {
            currentWeapon.SetText($"Weapon:{weaponObjects[weaponNumber].name}");
        }
    }
    void MyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)&&weaponNumber!=0)
        {
            SwitchWeapon(0, 0);
        }


        // �P�_�G���U�Ʀr��2�A�������Z��1
        if (Input.GetKeyDown(KeyCode.Alpha2)&&weaponNumber!=1)
        {
            SwitchWeapon(0, 1);
        }


        // �P�_�G�u�ʷƹ��u��
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) //�ƹ��V�W�u
        {
            SwitchWeapon(1);
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)//�ƹ��V�U�u
        {
            SwitchWeapon(-1);
        }
    }
    void SwitchWeapon(int _addNumber, int _weaponNumber = 0)       
    {
        // �N�Z���M��������áA���@���������áA�A��ܻݭn���Z��
        foreach (GameObject item in weaponObjects)
        {
            item.SetActive(false);
        }

        // switch �P�_���G�H�Ѽ�_addNumber�P�_�n�������Z��
        switch (_addNumber)
        {
            case 0:                                                   // _addNumber == 0�A�N��Ϋ��䪽�����w�Z���}�C��}
                weaponNumber = _weaponNumber;
                break;
            case 1:                                                   // _addNumber == 1�A�N���W�u�ƹ��u��
                if (weaponNumber == weaponObjects.Length - 1)         // ��{�`���Ʀr�A���w�쥻���Z���}�C��}�w�g�O�̫�@�ӪZ���A�h�N�Z���}�C��}�]�w��0
                    weaponNumber = 0;
                else
                    weaponNumber += 1;
                //weaponNumber = (weaponNumber == weaponObjects.Length - 1) ? 0 : weaponNumber += 1; // �]�i�H��H�W���P�_���g���o��
                break;
            case -1:                                                   // _addNumber == -1�A�N���U�u�ƹ��u��
                if (weaponNumber == 0)                                 // ��{�`���Ʀr�A���w�쥻���Z���}�C��}�O�Ĥ@�ӪZ���A�h�N�Z���}�C��}���M�檺�̫�@�Ӧ�}
                    weaponNumber = weaponObjects.Length - 1;
                else
                    weaponNumber -= 1;
                //weaponNumber = (weaponNumber == 0) ? weaponObjects.Length - 1 : weaponNumber -= 1; // �]�i�H��H�W���P�_���g���o��
                break;
        }
        weaponObjects[weaponNumber].SetActive(true);     // ��ܩҫ��w���Z��
        weaponInUse = weaponObjects[weaponNumber];       // �]�w�ثe�ҿ�ܪ��Z������(���ɥi�H�ΨӰ���Z���үS�w����k�A�U�@���`�|����)
        weaponInUse.GetComponent<Animator>().Rebind();   //���s�j�w�ҿ諸�Z���ʵe�A���L��l�ơA�o�˦�m�~���|�ö]
        weaponInUse.GetComponent<Animator>().Update(0f);
        Debug.Log($"���s�j�w{weaponInUse.name}���ʵe");
        
    }
}
