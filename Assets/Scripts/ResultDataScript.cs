using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultDataScript : MonoBehaviour
{
    [Header("飛行距離")] public Text DistanceText;
    [Header("ポイント")] public Text PointText;
    public bool _toTitleScene = false;
    // Start is called before the first frame update
    void Start()
    {
        DistanceText.text = "到達距離      " + DataScript.Distance.ToString() + " m";
        PointText.text =  "スコア      " + DataScript.Point.ToString() + " pt";
    }

    // Update is called once per frame
    void Update()
    {
        ToTitleScene();
    }

    void ToTitleScene()
    {
        if(Input.GetKeyDown(KeyCode.Space) | Input.GetKeyDown(KeyCode.Return))
        {
            _toTitleScene = true;
            SceneManager.LoadScene("TitleScene");
        }
    }
}
