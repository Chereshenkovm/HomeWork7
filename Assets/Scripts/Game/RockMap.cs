using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RockMap : MonoBehaviour
{
    [SerializeField] private GameObject _root;

    private List<Transform> _rockComponents = new List<Transform>();

    private void Awake()
    {
        for(int i=0;i<_root.transform.childCount;i++)
            _rockComponents.Add(_root.gameObject.transform.GetChild(i));
    }

    private void UpdateList()
    {
        _rockComponents.RemoveAll(item => item == null);
    }

    public List<Vector3> RockPositions() => _rockComponents
        .Select(z=>z.position)
        .ToList();

    public List<Transform> RockTransforms()
    {
        UpdateList();
        return _rockComponents.ToList();
    }
}
