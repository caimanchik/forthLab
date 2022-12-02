using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Material ZeroMaterial;
    public Material OneMaterial;
    
    private readonly List<Collider> _cubes = new();

    private readonly double[] _weights =
    {
        1.73263871669769,
        1.72908890247345
    };

    private readonly double _bias = -0.909411787986755;
    
    private void OnTriggerEnter(Collider other)
    {
        _cubes.Add(other);
        
        if (_cubes.Count != 2)
            return;

        gameObject.GetComponent<Renderer>().material = DotProductBias() > 0 ? OneMaterial : ZeroMaterial;
    }

    private double DotProductBias()
    {
        var d = _cubes.Select((e, i) =>
        {
            var col = e.gameObject.GetComponent<Renderer>().material.color == OneMaterial.color ? 1 : 0;
            return col * _weights[i];
        }).Sum();
        
        return d + _bias;
    }
}
