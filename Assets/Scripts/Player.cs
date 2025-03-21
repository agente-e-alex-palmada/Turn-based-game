using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Player : Character
{
    void Start()
    {       
        this.color = Color.red;
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
