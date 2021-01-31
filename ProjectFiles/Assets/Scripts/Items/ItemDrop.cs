using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MPA
{
    public class ItemDrop : MonoBehaviour
    {
        public void DropItem()
        {
            if (Random.Range(0, 9) < 2)
            {
                GameObject item = Instantiate(GameObject.Find("ItemDropManager").GetComponent<ItemDropManager>().GetItem());
                item.transform.position = this.transform.position;

            }
    

    }
    }
}
