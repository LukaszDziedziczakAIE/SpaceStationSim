using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cafeteria : MonoBehaviour
{
    [SerializeField] List<GameObject> Tables;

    public bool CanBuildMoreTables
    {
        get
        {
            if (Tables.Count == 0) return false;

            foreach (GameObject table in Tables)
            {
                if (!table.activeSelf) return true;
            }
            return false;
        }
    }

    public void BuildTable()
    {
        if (!CanBuildMoreTables) return;

        foreach (GameObject table in Tables)
        {
            if (!table.activeSelf)
            {
                table.SetActive(true);
                return;
            }
        }
    }
}
