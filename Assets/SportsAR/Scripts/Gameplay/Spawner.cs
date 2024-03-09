using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject m_SpawnedObject;

    private void Start()
    {
        Instantiate(m_SpawnedObject, transform.localPosition, Quaternion.identity);
    }
}
