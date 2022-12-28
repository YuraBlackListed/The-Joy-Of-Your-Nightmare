using System.Collections.Generic;
using UnityEngine;

public class RandomItemSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private List<GameObject> positions;
    private List<int> usedObjectId = new List<int>();
    private List<int> usedPositionId = new List<int>();
    private int id = 0;
    void Start()
    {
        for(int id = 0; id < positions.Count; id++)
        {
            int positionId = Random.Range(0, positions.Count - 1);
            int objectId = Random.Range(0, objects.Count - 1);

            GenerateObject(positionId, objectId);

            usedPositionId.Add(positionId);
            usedObjectId.Add(objectId);
        }
    }
    void GenerateObject(int posId, int objId)
    {
        bool usedObject = false;
        bool usedPosition = false;
        if(usedObjectId != null && usedPositionId != null)
        {
            for(int i = 0; i < usedPositionId.Count; i++)
            {
                if(usedPositionId[i] == posId)
                {
                    usedPosition = true;
                    break;
                }
            }
            for(int i = 0; i < usedObjectId.Count; i++)
            {
                if(usedObjectId[i] == objId)
                {
                    usedObject = true;
                    break;
                }
            }
        }
        if(!usedPosition)
        {
            if(!usedObject)
            {
                Instantiate(objects[objId], positions[posId].transform.position, Quaternion.identity);
            }
            else
            {
                id--;
            }
        }
        else
        {
            id--;
        }
    }
}
