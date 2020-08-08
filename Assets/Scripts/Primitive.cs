using UnityEngine;

public abstract class Primitive : MonoBehaviour
{
    protected Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}
