using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeGameSceneScript : MonoBehaviour
{
    public bool _toStageScene = false;

    [SerializeField] GameObject SEPlayObject;

    
    void Start()
    {
        
    }

    void Update()
    {
        ToStageScene();
    }

    void ToStageScene()
    {
        if(Input.GetKeyDown(KeyCode.Space) | Input.GetKeyDown(KeyCode.Return))
        {
            _toStageScene = true;
            //Instantiate (SEPlayObject, new Vector3(0f, 0f, 0f), Quaternion.identity);
            SceneManager.LoadScene("StageScene");
        }
    }
}
