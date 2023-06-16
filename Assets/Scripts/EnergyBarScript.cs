using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBarScript : MonoBehaviour
{
    [SerializeField] GameObject GameSceneManager;
    GameSceneManagerScript gameSceneManager;

    [SerializeField] GameObject EnergyBarEnergy;
    [SerializeField] float EnergyBarLeft;
    [SerializeField] float EnergyBarRight;
    float energyBarWidth;

    // Start is called before the first frame update
    void Start()
    {
        // GameSceneManager
        gameSceneManager = GameSceneManager.GetComponent<GameSceneManagerScript>();

        // EnergyBarSpriteの位置変動量
        energyBarWidth = EnergyBarRight - EnergyBarLeft;
    }

    // Update is called once per frame
    void Update()
    {
        var _ratio = gameSceneManager._electricPower;
        var _enePos = EnergyBarEnergy.transform.position;
        var _pos = new Vector3(EnergyBarLeft + _ratio * energyBarWidth, _enePos.y, _enePos.z);
        EnergyBarEnergy.transform.position = _pos;
    }
}
