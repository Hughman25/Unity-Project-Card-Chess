using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using bm = BoardManager;

public class MinionPoolButtons : MonoBehaviour
{
    public Button button;
    public UnitManager thisUnit;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
        gm = GameObject.FindGameObjectWithTag("GameManagerTag").GetComponent<GameManager>();
    }

    public void UpdateButton(Sprite unit_sprite, UnitManager unit)
    {
        button.image.sprite = unit_sprite;
        thisUnit = unit;
        Debug.Log(thisUnit);
    }
    void TaskOnClick()
    {
        gm.buyMinion(thisUnit);
    }
}
