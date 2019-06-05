using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolScript : MonoBehaviour
{
    public UnitManager[] characters;
    public List<UnitManager> pool;
    public UnitManager[] curPool;
    public MinionPoolButtons[] buttons;

    void Start()
    {

    }

    public void InitCharPool()
    {
        buttons = new MinionPoolButtons[6];
        GameObject[] poolButtons = GameObject.FindGameObjectsWithTag("PoolButtons");
 
        for (int i = 0; i < 6; i++)
        {
            buttons[i] = poolButtons[i].GetComponent<MinionPoolButtons>();
        }

        pool = new List<UnitManager>();
        int charIndex = -1;

        for (int i = 0; i < 40; i++)
        {
            if (i % 20 == 0)
            {
                charIndex += 1;
            }
            pool.Add(characters[charIndex]);
        }
    }

    public void RefreshPool()
    {
        if (curPool[0] != null)
        {
            for (int i = 0; i < 6; i++)
            {
                pool.Add(pool[i]);
                curPool[i] = null;
            }
        }
        for (int i = 0; i < 6; i++)
        {
            int selection = Random.Range(0, pool.Count);
            UnitManager unit = pool[selection];
            curPool[i] = unit;
            buttons[i].UpdateButton(unit.icon, unit);
            pool.RemoveAt(selection);
        }
    }
}
