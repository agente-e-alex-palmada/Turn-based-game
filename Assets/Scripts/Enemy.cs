using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private int enemy_selector;
    void Start()
    {
        enemy_selector = Random.Range(0, 3);
        switch (enemy_selector)
        {
            case 0:
                this.color = Color.cyan;
                this.name = "RWQFSFASXC";
                this.health = 200;
                this.attack = 13;
                break;
            case 1:
                this.color = Color.gray;
                this.name = "Bill Bullet";
                this.health = 100;
                this.attack = 16;
                break;
            case 2:
                this.color = Color.black;
                this.name = "Bob-omb";
                this.health = 300;
                this.attack = 17;
                break;
            default:
                this.color = Color.white;
                this.name = "Huh?";
                this.health = 300;
                this.attack = 17;
                break;
        }
        this.maxHP = this.health;
        this.currentHP = this.health;
        rendered_sprite = GetComponentInChildren<SpriteRenderer>();
        rendered_sprite.color = this.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Override Defeated
    public override void Defeated()
    {
        base.Defeated(); // Call the parent method first
        // This is overrided
        Debug.Log(name + " has been defeated!");
    }
}
