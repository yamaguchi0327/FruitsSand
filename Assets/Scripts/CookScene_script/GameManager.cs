using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    GameObject creamObj;
    IOrderedEnumerable<KeyValuePair<string, List<double>>> sortedFruits;
    // Start is called before the first frame update
    int buttonCount = 0; // 何回ボタンを押したか
    int nextFruitId = 0; // 次に降ってくる予定のフルーツの番号

    public GameObject banana;
    public GameObject left_grape;
    public GameObject right_grape;
    public GameObject grape;
    public GameObject left_muscat;
    public GameObject right_muscat;
    public GameObject muscat;
    public GameObject cut_kiwi;
    public GameObject kiwi;
    public GameObject orange;
    public GameObject straw;
    public GameObject big_cream;
    public GameObject cream;
    public GameObject pan;

    bool isIncreasingCream = false; // クリーム増量中かどうか
    float defaultCreameSizeY;
    float creamChangeScale;

    //一個前においたフルーツを代入
    //GameObject before_putFruit = null;

    void Start()
    {
        creamObj = GameObject.Find("Cream");
        Debug.Log(creamObj);
        sortedFruits = creamObj.GetComponent<Cream>().sortedFruits;
        //Debug.Log("シーン遷移した!");
        //foreach (KeyValuePair<string, List<double>> item in sortedFruits)
        //{
        //    Debug.Log(item.Key + "の座標は" + item.Value[0] + "," + item.Value[1]);
        //}

        ////最初に生クリームをぬる
        //float creameSizeY = cream.GetComponent<MeshRenderer>().bounds.size.y;
        defaultCreameSizeY = cream.GetComponent<MeshRenderer>().bounds.size.y; //倍率算出のため　scale1の時の実寸サイズ
        creamChangeScale = (float)sortedFruits.ElementAt(0).Value[1] / defaultCreameSizeY;
        //cream.transform.localScale = new Vector3(1.0f, (float)sortedFruits.ElementAt(0).Value[1]/creameSizeY, 1.0f);

        //cream.transform.localScale += new Vector3(0.0f, (float)sortedFruits.ElementAt(ButtonCount).Value[1] / creameSizeY, 0.0f);
        // デフォルトのサイズをとってから縮めてる
        cream.transform.localScale  = new Vector3(cream.transform.localScale.x, 0.01f, cream.transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(cream_big);
        //クリーム生成
        if(isIncreasingCream){
            if (cream.transform.localScale.y <= creamChangeScale){
                cream.transform.localScale += new Vector3(0, 0.01f, 0);
            }else{
                isIncreasingCream = false;
            }
        }

    }

    public void putMaterial()
    {

        //creamChangeScale = (float)sortedFruits.ElementAt(nextFruitId).Value[1] / defaultCreameSizeY;　//scaleを変えるために何倍にすればいいか

        //フルーツの名前を代入
        string FruitsKey = sortedFruits.ElementAt(nextFruitId).Key;
        //x,y座標をfloatに変換
        float x = (float)sortedFruits.ElementAt(nextFruitId).Value[0];
        float y = (float)sortedFruits.ElementAt(nextFruitId).Value[1];
        //Debug.Log(sortedFruits.ElementAt(ButtonCount).Key + "をおいた");

        GameObject putFruit = null;

        if(FruitsKey.Contains("Fruits_orange"))
        {
            putFruit = orange;
        }
        else if(FruitsKey.Contains ("Fruits_kiwi"))
        {
            putFruit = kiwi;
        }
        else if (FruitsKey.Contains("Fruits_cutkiwi"))
        {
            putFruit = cut_kiwi;
        }
        else if (FruitsKey.Contains("Fruits_straw"))
        {
            putFruit = straw;
        }
        else if (FruitsKey.Contains("Fruits_grapefull"))
        {
            putFruit = grape;
        }
        else if (FruitsKey.Contains("Fruits_graperight"))
        {
            putFruit = right_grape;
        }
        else if (FruitsKey.Contains("Fruits_grapeleft"))
        {
            putFruit = left_grape;
        }
        else if (FruitsKey.Contains("Fruits_muscatfull"))
        {
            putFruit = muscat;
        }
        else if (FruitsKey.Contains("Fruits_muscatright"))
        {
            putFruit = right_muscat;
        }
        else if (FruitsKey.Contains("Fruits_muscatleft"))
        {
            putFruit = left_muscat;
        }
        else if (FruitsKey.Contains("Fruits_banana"))
        {
            putFruit = banana;
        }

        //before_putFruit = putFruit;

        //一個前においたフルーツの右端(下)の座標
        //float before_putFruitRight;
        //次に置く予定のフルーツの右端(下)の座標
        float nextFruitPositionY = (float)(sortedFruits.ElementAt(nextFruitId).Value[1]);

        // 最初にボタンを押した時は確定でクリーム
        if(buttonCount == 0){
            creamChangeScale = nextFruitPositionY / defaultCreameSizeY;　//scaleを変えるために何倍にすればいいか
            isIncreasingCream = true;
            buttonCount++;
            Debug.Log("最初");
            return;  
        }

        float currentCreamSizeY = defaultCreameSizeY * cream.transform.localScale.y;

        Debug.Log(currentCreamSizeY);
        Debug.Log(nextFruitPositionY);
        if (Mathf.Abs(currentCreamSizeY - nextFruitPositionY) < 0.02){
            //フルーツを対角線上におく
            Vector3 panSize = pan.GetComponent<MeshRenderer>().bounds.size; //パンのサイズ取得
            float z = panSize.z * (x / panSize.x); //zをxに比例させる
            float rotateY = Mathf.Atan(panSize.z / panSize.x); //45°回転
            Instantiate(putFruit, new Vector3(x, y + (float)0.5 * putFruit.GetComponent<MeshRenderer>().bounds.size.y, z), Quaternion.Euler(0f, Mathf.Rad2Deg * -rotateY, 0f));

            nextFruitId++;
            Debug.Log("フル");
        }
        else{
            creamChangeScale = nextFruitPositionY  / defaultCreameSizeY;　//scaleを変えるために何倍にすればいいか
            isIncreasingCream = true;
            Debug.Log("くり");
        }

        //フルーツの右端(下)の座標が近かったらクリーム生成をスキップ
        //if (ButtonCount > 0)
        //{
        //    //一個前においたフルーツの右端(下)の座標
        //    before_putFruitRight = (float)(sortedFruits.ElementAt(ButtonCount - 1).Value[1] - before_putFruit.GetComponent<MeshRenderer>().bounds.size.y * 0.5);
        //    Debug.Log(before_putFruitRight);
        //    //今回置くフルーツの右端(下)の座標
        //    this_putFruitRight = (float)(sortedFruits.ElementAt(ButtonCount).Value[1] - putFruit.GetComponent<MeshRenderer>().bounds.size.y * 0.5);
        //    Debug.Log(this_putFruitRight);
        //    //座標の差が0.02以下なら生クリーム生成しない
        //    if (Mathf.Abs((Mathf.Abs(before_putFruitRight) - Mathf.Abs(this_putFruitRight))) < 0.02)
        //    {
        //        cream_big = false;
        //        Debug.Log("生クリーム塗らない");
        //    }
        //    else{
        //        cream_big = true;
        //        Debug.Log("生クリームぬる");
        //    }
        //}

        ////putFruitにフルーツが代入されている時
        //if (putFruit){
        //    //フルーツを対角線上におく
        //    Vector3 panSize = pan.GetComponent<MeshRenderer>().bounds.size; //パンのサイズ取得
        //    float z = panSize.z * (x / panSize.x); //zをxに比例させる
        //    float rotateY = Mathf.Atan(panSize.z / panSize.x); //45°回転
        //    Instantiate(putFruit, new Vector3(x, y + (float)0.5 * putFruit.GetComponent<MeshRenderer>().bounds.size.y, z), Quaternion.Euler(0f, Mathf.Rad2Deg * -rotateY,0f));
        //}

        buttonCount++;

    }
}