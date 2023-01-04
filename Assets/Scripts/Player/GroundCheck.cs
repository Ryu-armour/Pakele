using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    //地面のタグ名
    private string groundTag = "Ground";
    //接地判定
    private bool isGround;
    //接地判定用のフラグ
    private bool isGroundEnter, isGroundStay, isGroundEsxit;

    // Start is called before the first frame update
    void Start()
    {
        //フラグの初期化
        isGround = false;
        isGroundEnter = isGroundStay = isGroundEsxit = false;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //接地判定を返す(物理判定の更新ごとに呼ぶ必要あり)
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

        //フラグのリセット
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
