using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject m_SpawnedObjectPrefab;

    private GameObject m_SpawnedObject;

    private void Start()
    {
        m_SpawnedObject = Instantiate(m_SpawnedObjectPrefab, transform.parent.transform.position, transform.parent.transform.rotation, transform.parent);
        m_SpawnedObject.transform.localPosition = transform.localPosition;
        m_SpawnedObject.transform.localRotation = transform.localRotation;
    }
}
