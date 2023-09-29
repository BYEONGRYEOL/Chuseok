using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectManager
{
    Dictionary<int, List<GameObject>> _objects = new Dictionary<int, List<GameObject>>();
    public Dictionary<int, int> ObjectSpawnLimit = new Dictionary<int, int>();
    public void Init()
    {
        if (!ObjectSpawnLimit.ContainsKey(2))
        {
            ObjectSpawnLimit.Add(2, 5);

        }
        if (!ObjectSpawnLimit.ContainsKey(107))
        {
            ObjectSpawnLimit.Add(107, 3);

        }

    }
    public void Add(int key, GameObject go)
    {
        if(_objects.ContainsKey(key))
        {
            _objects[key].Add(go);
        }
        else
        {
            List<GameObject> _list = new List<GameObject>();
            _list.Add(go);
            _objects.Add(key, _list);
        }
    }

    public void Remove(int key, GameObject go)
    {
        if (!_objects.ContainsKey(key))
        {
            return;
        }
        _objects[key].Remove(go);
    }

    public int Count(int key)
    {
        if (!_objects.ContainsKey(key))
        {
            return 0;
        }
        return _objects[key].Count();
    }

    public void Clear()
    {
        _objects.Clear();
    }
    

}
