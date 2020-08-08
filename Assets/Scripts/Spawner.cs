using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spherePrefab;

    private Vector3 RandomPosition()
    {
        int x = Random.Range(-9, 9);
        int y = Random.Range(-4, 4);
        return new Vector3(x, y, 0);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject prefabInstance = ObjectPool.Instance.ReuseGameObject(spherePrefab);
            prefabInstance.transform.SetPositionAndRotation(RandomPosition(), Quaternion.identity);
        }
    }

}
