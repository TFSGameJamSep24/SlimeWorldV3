using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : MonoBehaviour
{

    [Header("Slime Health Properties")]
    [SerializeField] private float maxHP = 100f;
    [SerializeField] private float regenRate = 5f;
    [SerializeField] private float damageRate = 10f;

    private float currentHP;
    private Coroutine regenCoroutine;
    private Animator anim;

    private void Start()
    {
        currentHP = maxHP;
        anim = GetComponent<Animator>();
    }

    public void Damage()
    {
        currentHP -= damageRate * Time.deltaTime;

        if(currentHP < 0)
        {
            PopSlime();
        }

        if (regenCoroutine != null)
        {
            StopCoroutine(regenCoroutine);
        }
        
    }

    public void StartRegeneration()
    {
        if (regenCoroutine == null)
        {
            regenCoroutine = StartCoroutine(RegenerateHP());
        }
    }

    private IEnumerator RegenerateHP()
    {
        while (currentHP < maxHP)
        {
            currentHP += regenRate * Time.deltaTime;
            yield return null;
        }

        regenCoroutine = null;
    }

    private void PopSlime()
    {
        anim.SetTrigger("Pop");
        Debug.Log("Slime popped");
    }



    //timer for restTime
    //float HP
    //float regen 
    //float damageRate

    //if (!isBlown) restTimer counts down and when it reaches zero it's starts to regen to slime's max HP
    //bool isBlown
    //



}
