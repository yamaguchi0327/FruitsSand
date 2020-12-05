using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    GameObject creamObj;
    IOrderedEnumerable<KeyValuePair<string, List<double>>> sortedFruits;
    // Start is called before the first frame update
    void Start()
    {
        creamObj = GameObject.Find("Cream");
        Debug.Log(creamObj);
        sortedFruits = creamObj.GetComponent<Cream>().sortedFruits;
        Debug.Log("シーン遷移した!");
        foreach (KeyValuePair<string, List<double>> item in sortedFruits)
        {
            Debug.Log(item.Key + "の座標は" + item.Value[0] + "," + item.Value[1]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
