using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompositeOrderSourceActivation : OrderSourceActivationCondition
{
    [SerializeField]
    private List<OrderSourceActivationCondition> Conditions;

    public override bool IsMeet()
    {
        return Conditions.All(condition => condition.IsMeet());
    }
}
