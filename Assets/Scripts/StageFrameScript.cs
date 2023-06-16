using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StageFrameScript : MonoBehaviour
{
    
private int _currentNumber = 0;
private bool _isForest = true;
private bool _isTown = false;
private bool _isFuture = false;

[Header("森")] public GameObject forestStage;
[Header("町")] public GameObject townStage;
[Header("未来")] public GameObject futureStage;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeNumber();
        SetFrame();
    }

    void ChangeNumber()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            _currentNumber++;
            _currentNumber %= 3;
            ChangeBool();
        }
    }
    void SetFrame()
    {
        forestStage.SetActive(_isForest);
        townStage.SetActive(_isTown);
        futureStage.SetActive(_isFuture);
    }
    void ChangeBool()
    {
        if(_currentNumber == 0) 
        {
            _isForest = true;
            _isFuture = false;
        }
        else if(_currentNumber == 1)
        {
            _isForest = false;
            _isTown = true;
        }
        else if(_currentNumber == 2)
        {
            _isTown = false;
            _isFuture = true;
        }
    }
}
