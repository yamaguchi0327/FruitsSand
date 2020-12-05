using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DragFruits : MonoBehaviour
{
    public bool onCream = false; //クリームの上かどうか
    public Vector3 mousePointInWorld; //マウス座標

    // 確定ボタン
    Button button;

    private void Start()
    {
        // ボタンの取得
        GameObject buttonObj = GameObject.Find("Button");
        button = buttonObj.GetComponent<Button>();
        //button.onClick.AddListener(CreateFruitsList);
    }

    void Update()
    {
        if (onCream)
        {
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }

    void OnMouseDrag()
    {
        //isDrag = true;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        //フルーツのオブジェクトをドラッグ&ドロップできる処理
        Vector3 objectPointInScreen = Camera.main.WorldToScreenPoint(this.transform.position);

        Vector3 mousePointInScreen
            = new Vector3(Input.mousePosition.x,
                          Input.mousePosition.y,
                          objectPointInScreen.z);


        mousePointInWorld = Camera.main.ScreenToWorldPoint(mousePointInScreen);//スクリーン座標　transform.positionはworld座標
        mousePointInWorld.z = this.transform.position.z;
        this.transform.position = mousePointInWorld;
    }

    private void OnMouseUp()
    {
        //クリームの外でマウスを離したら、オブジェクトを削除
        if (!onCream)
        {
            Destroy(this.gameObject);
        }
        //クリームの中でマウスを離したら、他のオブジェクトの干渉を受けない(動かなくなる)
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (onCream)
        {
            //フルーツのprefabとpan_arroundと重なっている時
            if (col.gameObject.tag.Contains("Fruits") || col.CompareTag("pan_arround"))
            {
                button.GetComponent<ButtonScript>().fruitsOverlap = true;
                //spriteの透明度を変更
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D col)
    {
        if (onCream)
        {
            //フルーツのprefabとpan_arroundと重なっていない時
            if (col.gameObject.tag.Contains("Fruits") || col.CompareTag("pan_arround"))
            {
                button.GetComponent<ButtonScript>().fruitsOverlap = false;
                //spriteの透明度を変更
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
    }

}