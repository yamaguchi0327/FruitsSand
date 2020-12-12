using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject creamObj;
    GameObject pan;
    public GameObject blankObj1;
    public GameObject blankObj2;
    public GameObject cutLine;
    public Material blankMaterial;

    IOrderedEnumerable<KeyValuePair<string, List<double>>> sortedFruits;
    Dictionary<string, List<double>> FruitsData;
    int fruitsNum;//配列に格納されたフルーツの個数

    int buttonCount = 0; // 何回ボタンを押したか
    int nextFruitId = 0; // 次に降ってくる予定のフルーツの番号

    string fruitsName; //フルーツの名前を代入
    public Text Text; //CookSceneに表示する文字
    public Text Text2; //CookSceneに表示する文字

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
    public GameObject pans;

    bool isIncreasingCream = false; // クリーム増量中かどうか
    float defaultCreameSizeY;
    float creamChangeScale;

    GameObject putFruit = null;



    //一個前においたフルーツを代入
    //GameObject before_putFruit = null;

    void Start()
    {
        creamObj = GameObject.Find("Cream");
        pan = GameObject.Find("pan");
        pan.gameObject.transform.position = new Vector3(6f, -60f, 0);
        //Debug.Log(creamObj);
        sortedFruits = creamObj.GetComponent<Cream>().sortedFruits;
        FruitsData = creamObj.GetComponent<Cream>().fruitsData;
        fruitsNum = FruitsData.Count;

        ////最初に生クリームをぬる
        defaultCreameSizeY = cream.GetComponent<MeshRenderer>().bounds.size.y; //倍率算出のため　scale1の時の実寸サイズ
        creamChangeScale = (float)sortedFruits.ElementAt(0).Value[1] / defaultCreameSizeY;

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
                cream.transform.localScale += new Vector3(0, 0.02f, 0);
            }else{
                isIncreasingCream = false;
            }
        }

    }

    public void switchCream(){
        if(cream.gameObject.activeSelf){
            cream.gameObject.SetActive(false);
        }else{
            cream.gameObject.SetActive(true);
        }
    }

    public void putMaterial()
    {
        //フルーツを全部乗せ終わってからの処理
        if(nextFruitId==fruitsNum){
            //クリームを最後までのせる
            creamChangeScale = creamObj.GetComponent<SpriteRenderer>().bounds.size.x / defaultCreameSizeY+1f;
            isIncreasingCream = true;
            nextFruitId++;
            Text.text = "生クリームを塗ってください";
            return;
        }else if(nextFruitId == fruitsNum + 1){
            //左右の隙間にフルーツとクリーム埋める
            blankObj1.gameObject.SetActive(true);
            blankObj2.gameObject.SetActive(true);
            nextFruitId++;
            Text.text = "左右の隙間に生クリームと余ったフルーツをのせてください";
            return;
        }else if(nextFruitId == fruitsNum + 2){
            blankObj1.GetComponent<Renderer>().material = blankMaterial;
            blankObj2.GetComponent<Renderer>().material = blankMaterial;
            //上にのせるパン表示
            Instantiate(pans, new Vector3(6.19f, (float)(cream.GetComponent<MeshRenderer>().bounds.size.y + pans.GetComponent<MeshRenderer>().bounds.size.y * 0.5), 6.25f), Quaternion.Euler(90f, 0, 0f));
            Text.text = "パンをのせてください"; 
            nextFruitId++;
            return;
        }else if (nextFruitId == fruitsNum + 3)
        {
            //カットライン表示
            cutLine.gameObject.SetActive(true);
            Text.text = "線の通りに包丁でカットしてください";
            nextFruitId++;
            return;
        }


        //フルーツの名前を代入
        string FruitsKey = sortedFruits.ElementAt(nextFruitId).Key;
        //x,y座標をfloatに変換
        float x = (float)sortedFruits.ElementAt(nextFruitId).Value[0];
        float y = (float)sortedFruits.ElementAt(nextFruitId).Value[1];
        //Debug.Log(sortedFruits.ElementAt(ButtonCount).Key + "をおいた");

        //GameObject putFruit = null;

        if(FruitsKey.Contains("Fruits_orange"))
        {
            putFruit = orange;
            fruitsName = "みかん";
        }
        else if(FruitsKey.Contains ("Fruits_kiwi"))
        {
            putFruit = kiwi;
            fruitsName = "キウイ";
        }
        else if (FruitsKey.Contains("Fruits_cutkiwi"))
        {
            putFruit = cut_kiwi;
            fruitsName = "細く切ったキウイ";
        }
        else if (FruitsKey.Contains("Fruits_straw"))
        {
            putFruit = straw;
            fruitsName = "いちご";
        }
        else if (FruitsKey.Contains("Fruits_grapefull"))
        {
            putFruit = grape;
            fruitsName = "ぶどう";
        }
        else if (FruitsKey.Contains("Fruits_graperight"))
        {
            putFruit = right_grape;
            fruitsName = "半分に切ったぶどう";
        }
        else if (FruitsKey.Contains("Fruits_grapeleft"))
        {
            putFruit = left_grape;
            fruitsName = "半分に切ったぶどう";
        }
        else if (FruitsKey.Contains("Fruits_muscatfull"))
        {
            putFruit = muscat;
            fruitsName = "マスカット";
        }
        else if (FruitsKey.Contains("Fruits_muscatright"))
        {
            putFruit = right_muscat;
            fruitsName = "半分に切ったマスカット";
        }
        else if (FruitsKey.Contains("Fruits_muscatleft"))
        {
            putFruit = left_muscat;
            fruitsName = "半分に切ったマスカット";
        }
        else if (FruitsKey.Contains("Fruits_banana"))
        {
            putFruit = banana;
            fruitsName = "バナナ";
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
            Text.text = "生クリームを塗ってください";
            buttonCount++;
            Debug.Log("最初");
            return;  
        }

        float currentCreamSizeY = defaultCreameSizeY * cream.transform.localScale.y;

        Debug.Log(currentCreamSizeY);
        Debug.Log(nextFruitPositionY);
        if (Mathf.Abs(currentCreamSizeY - nextFruitPositionY) < 0.08){
            //フルーツを対角線上におく
            Vector3 panSize = pans.GetComponent<MeshRenderer>().bounds.size; //パンのサイズ取得
            float z = panSize.z * (x / panSize.x); //zをxに比例させる
            float rotateY = Mathf.Atan(panSize.z / panSize.x); //45°回転
            Instantiate(putFruit, new Vector3(x, y + (float)0.5 * putFruit.GetComponent<MeshRenderer>().bounds.size.y, z), Quaternion.Euler(0f, Mathf.Rad2Deg * -rotateY, 0f));
            Text.text = fruitsName + "をおいてください";
            nextFruitId++;
            //Debug.Log("フル");
        }
        else{
            creamChangeScale = nextFruitPositionY  / defaultCreameSizeY;　//scaleを変えるために何倍にすればいいか
            isIncreasingCream = true;
            Text.text = "生クリームを塗ってください";
            //Debug.Log("くり");
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