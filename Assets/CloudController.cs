using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{

    // �ŏ��T�C�Y
    private float minimum = 1.0f;
    // �g��k���X�s�[�h
    private float magSpeed = 10.0f;
    // �g�嗦
    private float magnification = 0.07f;

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        // �_���g��k��
        // �g�嗦��this.transform.localScale�ɑ�����Ďw��
        // 3D�Ȃ̂�Vector3���g���ĂR�����̊g�嗦���w��
        // �����P x�������̊g�k�F�ŏ��T�C�Y �{ -1�`1�������I�ɕԂ�Sin�i���W�A���F�o�ߎ��� �~ �X�s�[�h�����Ƃɂ����l�j �~ �g�嗦
        //         �܂�A�ŏ��T�C�Y �{ �g�k�̑��� �~ �g�k�̑傫��
        // �����Q y���F���݂�y�̊g�嗦�iy�������̓T�C�Y�ύX�Ȃ��j
        // �����R z���F�����P�Ɠ���
        this.transform.localScale = new Vector3 (this.minimum + Mathf.Sin (Time.time * this.magSpeed) * this.magnification, this.transform.localScale.y, this.minimum + Mathf.Sin (Time.time * this.magSpeed) * this.magnification);

    }
}
