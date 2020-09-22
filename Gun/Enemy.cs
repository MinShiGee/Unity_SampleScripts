using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] [Range(0, 1000f)] private float health = 100f;

    // Start is called before the first frame update
    public void GetDamage(float dam)
    {
        health -= dam;

        if (health <= 0)
            Die();
    }
    private void Die()
    {
        Destroy(this.gameObject);
    }
}
