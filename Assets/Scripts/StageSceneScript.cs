using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StageSceneScript : MonoBehaviour
{
    public bool _toForestScene = false;
    public bool _toTownScene = false;
    public bool _toFutureScene = false;
    private bool _toTitleScene = false;

    //[SerializeField] GameObject SEPlayNut;
    //[SerializeField] GameObject SEPlayBattery;

    
    private Selectable mySelectable;
 
	void Start() {
		mySelectable = GetComponent<Selectable>();
	}
    void Update(){
        toTitleScene();
    }
 
	public void SetSelectable() {
		//タブキーを押されたらSelectOnRightに選択された物をフォーカスする
		if(Input.GetKeyDown(KeyCode.Tab)) {
            //Instantiate (SEPlayNut, new Vector3(0f, 0f, 0f), Quaternion.identity);
			EventSystem.current.SetSelectedGameObject(mySelectable.navigation.selectOnRight.gameObject);
		}
        
	}

    public void ToForestScene()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            //Instantiate (SEPlayBattery, new Vector3(0f, 0f, 0f), Quaternion.identity);
            _toForestScene = true;
            SceneManager.LoadScene("ForestScene");
        }
    }
    public void ToTownScene()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            //Instantiate (SEPlayBattery, new Vector3(0f, 0f, 0f), Quaternion.identity);
            _toTownScene = true;
            SceneManager.LoadScene("TownScene");
        }
    }
    public void ToFutureScene()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            //Instantiate (SEPlayBattery, new Vector3(0f, 0f, 0f), Quaternion.identity);
            _toFutureScene = true;
            SceneManager.LoadScene("FutureScene");
        }
    }
    public void toTitleScene()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            //Instantiate (SEPlayBattery, new Vector3(0f, 0f, 0f), Quaternion.identity);
            _toTitleScene = true;
            SceneManager.LoadScene("TitleScene");
        }
    }
}
