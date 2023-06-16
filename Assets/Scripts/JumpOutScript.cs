using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOutScript : MonoBehaviour
{
    [SerializeField] GameObject Stage;
    StageObjectScript stageScript;
    [SerializeField] GameObject JumpOutObstacle;
    
    [SerializeField] Vector3 StartPos;
    [SerializeField] bool Right = false;
    [SerializeField] bool Left = false;

    GameObject obj;
    bool _create = false;

    [SerializeField] bool isDrone = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0f, 1f) <= 0.5f) {
            if (isDrone) {
                _create = true;
                stageScript = Stage.GetComponent<StageObjectScript>();
                
                var pos = new Vector3(-9f, 2.5f, this.transform.position.z);
                obj = Instantiate (JumpOutObstacle, pos, Quaternion.Euler(0f, -180f, 0f));
                obj.GetComponent<StageObjectScript>().CreatePosition = pos;
                obj.GetComponent<StageObjectScript>()._isMoving = false;
                obj.GetComponent<StageObjectScript>().MoveSuddenlyDirection = new Vector3(3f, 0f, 0f);
            }
            else {
                _create = true;
                stageScript = Stage.GetComponent<StageObjectScript>();
                
                var pos = new Vector3(7f, -1.3f, this.transform.position.z);
                var yRot = 0f;
                if (Right) { 
                    pos = new Vector3(-7f, -1.3f, this.transform.position.z);
                    yRot = 180f; 
                }

                obj = Instantiate (JumpOutObstacle, this.transform.position, Quaternion.Euler(0f, yRot, 90f));
                obj.GetComponent<StageObjectScript>().CreatePosition = pos;
                obj.GetComponent<StageObjectScript>()._isMoving = false;
                if (Right) {
                    obj.GetComponent<StageObjectScript>().MoveSuddenlyDirection = new Vector3(3f, 0f, 0f);
                }
                if (Left) {
                    obj.GetComponent<StageObjectScript>().MoveSuddenlyDirection = new Vector3(-3f, 0f, 0f);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_create) {
            if (stageScript.GetComponent<StageObjectScript>()._isMoving) {
                obj.GetComponent<StageObjectScript>()._isMoving = true;
                _create = false;
            }
        }
    }
}
