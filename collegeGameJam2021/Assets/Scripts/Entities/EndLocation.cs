using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLocation : MonoBehaviour
{
    [SerializeField] ResultType resultType;

    private void Awake()
    {
        this.gameObject.layer = LayerMask.NameToLayer("EndLocation");
    }

    public ResultType GetResult()
    {
        return resultType;
    }

}
