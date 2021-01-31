using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MPA
{
    public class ItemDropManager : MonoBehaviour
    {
        public GameObject[] Items;
        public GameObject GetItem()
        {
            int item = Random.Range(0, 7);
            if (item < 6)
                return Items[0];
            else if (item == 6)
                return Items[1];
            else
                return Items[2];
        }
    }
}
