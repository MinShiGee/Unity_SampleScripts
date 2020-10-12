using UnityEngine;

public class Attackable : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected float health = 100f;
    [SerializeField] protected float maxStamina = 100f;
    [SerializeField] protected float stamina = 100f;

    [SerializeField] protected bool isDeath = false;

    public float GetHealth() {return health;}
    public void SetHealth(float hp){ health = (hp > maxHealth) ? maxHealth : hp;}
    public void AddHealth(float hp){
        health += hp;
        if(health > maxHealth)
            health = maxHealth;
        if(health <= 0){
            Death();
        }
    }
    protected virtual void Death(){
        isDeath = true;
    }
    public float GetMaxHealth(){return maxHealth;}
    public void SetMaxHealth(float mHP){maxHealth = mHP;}

    public void GetDamage(float dam){
        AddHealth(-dam);
    }

    public float GetStamina(){return stamina;}
    public void SetStamina(float st){ stamina = (st > maxStamina) ? maxStamina : st;}
    public void AddStamina(float st){
        stamina += st;
        if(stamina > maxStamina)
            stamina = maxStamina;
        if(stamina <= 0){
            stamina = 0;
        }
    }
    public float GetMaxStamina(){return maxStamina;}
    public void SetMaxStamina(float mST){maxStamina = mST;}


}
