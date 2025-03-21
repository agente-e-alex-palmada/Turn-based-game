using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState
{
    Start, PlayerTurn, EnemyTurn, Won, Lost, Attack
}

public class Turn_Manager : MonoBehaviour
{
    public BattleState state;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Player player;
    public Enemy enemy;

    // Text components
    public GameObject playerHPTextObject;
    public GameObject enemyHPTextObject;
    public GameObject dialogueBoxTextObject;

    // For the components, to set texts
    private TextMeshProUGUI playerHPText;
    private TextMeshProUGUI enemyHPText;
    private TextMeshProUGUI dialogueBoxText;

    void Start()
    {
        // Setup the battle
        state = BattleState.Start;
        SetupBattle();
    }

    void SetupBattle()
    {
        // Initiate player and get component from player
        GameObject playerGo = Instantiate(playerPrefab);
        player = playerGo.GetComponent<Player>();

        // Initiate enemy and get component from Enemy
        GameObject enemyGo = Instantiate(enemyPrefab);
        enemy = enemyGo.GetComponent<Enemy>();

        // Get text components from the GameObject references
        playerHPText = playerHPTextObject.GetComponent<TextMeshProUGUI>();
        enemyHPText = enemyHPTextObject.GetComponent<TextMeshProUGUI>();
        dialogueBoxText = dialogueBoxTextObject.GetComponent<TextMeshProUGUI>();

        // Update HP text displays
        playerHPText.text = "HP: " + player.currentHP;
        enemyHPText.text = "HP: " + enemy.currentHP;

        // Start the battle, always starts the player
        state = BattleState.PlayerTurn;
        PlayerTurn();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == BattleState.PlayerTurn)
        {
            // Check for player input in Update
            if (Input.GetKeyUp(KeyCode.Z))
            {
                state = BattleState.Attack;
                PlayerAttack();
            }
        }
    }

    void PlayerTurn()
    {
        dialogueBoxText.text = "Press Z to attack";
    }

    void PlayerAttack()
    {
        // Calculate damage
        int dmg = player.attack;

        // Apply damage to enemy
        int enemyHP = enemy.currentHP;
        enemyHP -= dmg;
        if (enemyHP < 0)
        {
            enemyHP = 0;
        }
        enemy.currentHP = enemyHP;

        // Update enemy HP text display
        enemyHPText.text = "HP: " + enemy.currentHP;

        // Display attack message
        dialogueBoxText.text = "You do " + dmg + " points of damage.";

        // Start coroutine to wait before next action
        StartCoroutine(AfterPlayerAttack());
    }

    IEnumerator AfterPlayerAttack()
    {
        // Wait for 4 seconds to show attack message
        yield return new WaitForSeconds(4f);

        // Check if enemy is defeated
        if (enemy.currentHP <= 0)
        {
            // Player won
            state = BattleState.Won;
            dialogueBoxText.text = "You defeated the enemy!";
            enemy.Defeated();
        }
        else
        {
            // Switch to enemy turn
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        // Display enemy attack message
        dialogueBoxText.text = enemy.name + " attacks!";

        // Wait for 2 seconds to show the enemy attack message
        yield return new WaitForSeconds(2f);

        // Calculate damage
        int dmg = enemy.attack;

        // Apply damage to player
        int playerHP = player.currentHP;
        playerHP -= dmg;
        if (playerHP < 0)
        {
            playerHP = 0;
        }
        player.currentHP = playerHP;

        // Update player HP text display
        playerHPText.text = "HP: " + player.currentHP;

        // Display damage message
        dialogueBoxText.text = "You take " + dmg + " points of damage.";

        // Wait for 3 seconds to show damage message
        yield return new WaitForSeconds(3f);

        // Check if player is defeated
        if (player.currentHP <= 0)
        {
            // Player lost
            state = BattleState.Lost;
            dialogueBoxText.text = "You were defeated...";
            player.Defeated();
        }
        else
        {
            // Switch back to player turn
            state = BattleState.PlayerTurn;
            PlayerTurn();
        }
    }
}
