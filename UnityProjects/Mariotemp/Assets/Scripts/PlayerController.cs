using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string PlayerName = "Mario";
    public int Health = 100;
    public int Attack = 40;
    public float MoveSpeed = 5.5f;
    public bool IsJumping = true;

    public int Coin = 100;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log($"PlayerName: {PlayerName}");
        Debug.Log($"Health: {Health}");
        Debug.Log($"Attack: {Attack}");
        Debug.Log($"MoveSpeed: {MoveSpeed}");
        Debug.Log($"IsJumping: {IsJumping}");
        Debug.Log($"Coin: {Coin}");
        
        //Define Potion Prices
        int redPotionPrice = 50; //Health increase by 30
        int bluePotionPrice = 60;  //Attack increase by 30
        
        //Red Potion Purchase
        if (Coin >= redPotionPrice)
        {
            Health += 30;
            Coin -= redPotionPrice;
            Debug.Log("Mario bought a red potion! Health increased " + Health + ", Coin left: " + Coin);
        }
        else
        {
            Debug.Log("Mario doesn't have enough coins to buy a red potion!");
        }
        
        //Blue Potion Purchase
        if (Coin >= bluePotionPrice)
        {
            Attack += 30;
            Coin -= bluePotionPrice;
            Debug.Log("Mario bought a blue potion! Attack increased " + Attack + ", Coin left: " + Coin);
        }
        else
        {
            Debug.Log("Mario doesn't have enough coins to buy a blue potion!");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
