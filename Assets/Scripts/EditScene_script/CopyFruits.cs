using System.Collections; using System.Collections.Generic; using UnityEngine;  public class CopyFruits : MonoBehaviour {     public GameObject fruitObj; //このアイコンと同じフルーツのプレハブが入る     private GameObject copy_obj; //コピーしてアイコンの下に置くオブジェクト     bool hasStock = false; //アイコンの下にオブジェクトのストックがあるか      // Use this for initialization     void Start()     {
        //開始時にコピーを一つアイコンの下に置く
        copy_obj = Instantiate(fruitObj, this.transform.position, Quaternion.identity) as GameObject;     }

    // Update is called once per frame
    void Update()     {
        //どこかでマウスを離した時、アイコンの下のストックが無ければ新しく生成
        if (Input.GetMouseButtonUp(0) && !hasStock)         {             copy_obj = Instantiate(fruitObj, this.transform.position, Quaternion.identity) as GameObject;         }     }

    //ストックの有無を、アイコンの当たり判定で区別     //c=接触した相手
    private void OnTriggerEnter2D(Collider2D c)     {         if (c.gameObject.tag == gameObject.tag)         {             hasStock = true;         }     }     private void OnTriggerExit2D(Collider2D c)     {         if (c.gameObject.tag == gameObject.tag)         {             hasStock = false;         }     } } 