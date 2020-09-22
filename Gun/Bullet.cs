using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float damage = default;
    public Rigidbody bulRig = default;

    private void Awake()
    {
        bulRig = GetComponent<Rigidbody>();
    }
    public void Shot(float time)
    {
        StartCoroutine(Timer(time));
    }
    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);//WaitForSeconds객체를 생성해서 반환
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    { 
        gameObject.SetActive(false);
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy == null)
            return;

        enemy.GetDamage(damage);

    }

}
