using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveItemManager : MonoBehaviour
{
    public void SaveIdList(List<int> idList)
    {
        var path = Application.persistentDataPath + "/IdList.dat";

        using (var fileStream = new FileStream(path, FileMode.Create))
        {
            var binaryFormatter = new BinaryFormatter();
            var data = new IdList { items = idList.ConvertAll(id => new IdItem { id = id }) };

            binaryFormatter.Serialize(fileStream, data);
        }
    }
    public List<int> LoadIdList()
    {
        var path = Application.persistentDataPath + "/IdList.dat";

        if (File.Exists(path))
        {
            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                var binaryFormatter = new BinaryFormatter();
                var data = (IdList)binaryFormatter.Deserialize(fileStream);

                return data.items.ConvertAll(item => item.id);
            }
        }
        else
        {
            return new List<int>();
        }
    }
}
[Serializable]
public class IdItem
{
    public int id;
}

[Serializable]
public class IdList
{
    public List<IdItem> items;
}
