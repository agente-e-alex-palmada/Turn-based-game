using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public new string name;
    public int health;
    public int attack;
    public int maxHP;
    public int minHP = 0;
    public int currentHP;
    protected Color color;
    protected SpriteRenderer rendered_sprite;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // To be called if dies
    public virtual void Defeated()
    {
        rendered_sprite = GetComponent<SpriteRenderer>();
        if (rendered_sprite != null)
        {
            rendered_sprite.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
