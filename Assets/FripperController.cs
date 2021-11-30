using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour
{

    // HingeJoint�R���|�[�l���g������
    private HingeJoint myHingeJoint;

    // �����̌X��
    private float defaultAngle = 20;
    // �e�������̌X��
    private float flickAngle = -20;

    // Use this for initialization
    void Start ()
    {
        // HingeJoint�R���|�[�l���g�擾
        this.myHingeJoint = GetComponent<HingeJoint> ();

        // �t���b�p�[�̌X����ݒ�
        SetAngle (this.defaultAngle);

    }

    // Update is called once per frame
    void Update ()
    {
        // �y���W�ۑ�z�^�b�`����ɑΉ����邽�߂̏�����ǋL

        // �^�b�`�����擾
        Touch[] touches = Input.touches;

        // �^�b�`�������ɂ��邩�ǂ���
        bool isLeftTouch = false;
        // �^�b�`���E���ɂ��邩�ǂ���
        bool isRightTouch = false;

        // �^�b�`���w�P�{�ȏ゠��ꍇ�A���ׂĂ̎w�̃^�b�`��Ԃ��`�F�b�N
        if (touches.Length > 0)
        {

            for (int i = 0; i < touches.Length; i++)
            {
                // �����̃^�b�`�������o�̏ꍇ�A�����Ƀ^�b�`�����邩���`�F�b�N
                if (!isLeftTouch)
                {
                    // �����Ɏw�����邩�ǂ������擾
                    isLeftTouch = touches[i].position.x < 1080 / 2;
                }

                // �E���̃^�b�`�������o�̏ꍇ�A�E���Ƀ^�b�`�����邩���`�F�b�N
                if (!isRightTouch)
                {
                    // �E���Ɏw�����邩�ǂ������擾
                    isRightTouch = touches[i].position.x >= 1080 / 2;
                }
            }


        }

        // �����L�[�����������A�܂��͍����̃^�b�`�J�n���A���t���b�p�[�𓮂���
        if (tag == "LeftFripperTag" && (Input.GetKeyDown (KeyCode.LeftArrow) || isLeftTouch))
        {
            SetAngle (this.flickAngle);
        }

        // �E���L�[�����������A�܂��͉E���̃^�b�`�J�n���A�E�t���b�p�[�𓮂���
        if (tag == "RightFripperTag" && (Input.GetKeyDown (KeyCode.RightArrow) || isRightTouch))
        {
            SetAngle (this.flickAngle);
        }

        // ���L�[�������ꂽ���A�܂��̓^�b�`�I�����A�t���b�p�[�����ɖ߂�
        if (tag == "LeftFripperTag" && (Input.GetKeyUp (KeyCode.LeftArrow) || !isLeftTouch))
        {
            SetAngle (this.defaultAngle);
        }

        if (tag == "RightFripperTag" && (Input.GetKeyUp (KeyCode.RightArrow) || !isRightTouch))
        {
            SetAngle (this.defaultAngle);
        }

    }

    // �t���b�p�[�̌X����ݒ�
    public void SetAngle (float angle)
    {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;
    }

}
