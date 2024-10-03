using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : MonoBehaviour
{

    [Header("Slime Health Properties")]
    [SerializeField] private float maxHP = 100f;
    [SerializeField] private float regenRate = 25f;
    [SerializeField] private float damageRate = 10f;

    public float currentHP;
    private Coroutine regenCoroutine;
    private Animator anim;

    private void Start()
    {
        currentHP = maxHP;
        anim = GetComponent<Animator>();
        StartCoroutine(RegenerateHP());
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
        while (true)
        {
            currentHP += regenRate * Time.deltaTime;
            Mathf.Clamp(currentHP, 0, maxHP);
            yield return null;
        }
    }

    private void PopSlime()
    {
        anim.SetTrigger("Pop");
        StopCoroutine(RegenerateHP());
        Debug.Log("Slime popped");
    }

    public float GetHPRatio()
    {
        return currentHP / maxHP;
    }
}
