using System.Collections;
using UnityEngine;

public class Sphere : Primitive
{
    private void OnEnable()
    {
        StartCoroutine(LifeTime(4f));
        rb.AddTorque(Vector3.one, ForceMode.Impulse);
    }

    private IEnumerator LifeTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        rb.velocity = Vector3.zero;
        
        ObjectPool.Instance.SetBackToPool(gameObject);
    }
}
