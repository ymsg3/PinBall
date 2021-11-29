using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointRegulator : MonoBehaviour
{

    // ���_��\������e�L�X�g
    GameObject pointText;

    // ���������̓_��
    int smallStarPoint = 100;
    // �傫�����̓_��
    int largeStarPoint = 250;
    // �������_�̓_��
    int smallCloudPoint = 500;
    // �傫���_�̓_��
    int largeCloudPoint = 1000;

    void Start ()
    {
        // �V�[������PointText�I�u�W�F�N�g���擾
        this.pointText = GameObject.Find ("PointText");
    }

    void Update ()
    {

    }

    void OnCollisionEnter (Collision other)
    {
        // �ՓˑO�̓��_���擾
        Text beforePointText = this.pointText.GetComponent<Text> ();

        // �v�Z�p�ɕϊ�
        int point = int.Parse (beforePointText.text);

        // �^�[�Q�b�g�̎�ނɉ����Ēǉ����链�_��I��
        if (other.transform.tag == "SmallStarTag")
        {
            point += smallStarPoint;
        }
        else if (other.transform.tag == "LargeStarTag")
        {
            point += largeStarPoint;
        }
        else if (other.transform.tag == "SmallCloudTag")
        {
            point += smallCloudPoint;
        }
        else if (other.transform.tag == "LargeCloudTag")
        {
            point += largeCloudPoint;
        }

        // �Փˌ�̓��_��\��
        beforePointText.text = point.ToString ();
    }

}
