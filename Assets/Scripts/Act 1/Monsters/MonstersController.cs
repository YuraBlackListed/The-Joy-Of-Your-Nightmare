using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersController : MonoBehaviour
{
    [SerializeField] private List<MonsterAI> MonsterAIs;

    [SerializeField] private MonsterAI supremeAI;

    private void Start()
    {
        supremeAI = MonsterAIs[0];

        foreach(var ai in MonsterAIs)
        {
            ai.canAttack = false;
        }
    }
    private void Update()
    {
        GiveSupremacy();

        StartCoroutine(FindSupremeAI());
    }

    private IEnumerator FindSupremeAI()
    {
        foreach (var ai in MonsterAIs)
        {
            if(ai.Progress > supremeAI.Progress)
            {
                supremeAI.canAttack = false;

                supremeAI = ai;

                supremeAI.canAttack = true;
            }
        }

        yield return new WaitForSeconds(1f);
    }
    private void GiveSupremacy()
    {
        if(supremeAI == null)
        {
            return;
        }

        supremeAI.canAttack = true;
    }
}
