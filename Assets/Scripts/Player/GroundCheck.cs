using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    //�n�ʂ̃^�O��
    private string groundTag = "Ground";
    //�ڒn����
    private bool isGround;
    //�ڒn����p�̃t���O
    private bool isGroundEnter, isGroundStay, isGroundEsxit;

    // Start is called before the first frame update
    void Start()
    {
        //�t���O�̏�����
        isGround = false;
        isGroundEnter = isGroundStay = isGroundEsxit = false;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�ڒn�����Ԃ�(��������̍X�V���ƂɌĂԕK�v����)
    public bool IsGround()
    {
        if(isGroundEnter || isGroundStay)
        {
            isGround = true;
        }
        else if (isGroundEsxit)
        {
            isGround = false;
        }

        //�t���O�̃��Z�b�g
        isGroundEnter = isGroundStay = isGroundEsxit = false;
        return isGround;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == groundTag)
        {
            isGroundEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isGroundStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            isGroundEsxit = true;
        }
    }
}
