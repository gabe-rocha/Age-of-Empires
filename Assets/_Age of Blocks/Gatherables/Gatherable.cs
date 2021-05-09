using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatherable : MonoBehaviour
{
    public GatherableSO resource;

    public int Gather()
    {
        resource.quantity -= resource.amountPerGather;

        if (resource.quantity <= 0)
        {
            Destroy(gameObject);
        }

        return resource.amountPerGather;
    }
}
