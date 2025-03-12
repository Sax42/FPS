using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    public Weapon currentWeapon;
    public List<Weapon> weaponList;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void AddWeapon(Weapon pWeapon)
    {
        weaponList.Add(pWeapon);
        if(currentWeapon is null)
        {
            currentWeapon = weaponList[0];
            EquipItem(transform,pWeapon);
        }
    }

    public void ChangeEquipedItem(int pIndex)
    {
    }

    public void ChangeEquipedItem(bool pSide)
    {
    }

    public void EquipItem(Transform pNewParent,Item pItem)
    {
        pItem.transform.SetParent(pNewParent);
        pItem.transform.rotation = pNewParent.rotation;
        if(pItem is Weapon)
        {
            AddWeapon(pItem as Weapon);
        }
    }

    public void EquipItem(Transform pNewParent,Item pItem,Vector3 pNewPos)
    {
        EquipItem(pNewParent,pItem);
        pItem.transform.position = pNewPos;
    }
}