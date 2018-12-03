using UnityEngine;

public class ActivationOrderCondition : OrderCondition
{

    public bool sw = false;

    public override bool CanIssueOrder(OrderSource source, OrderController controller)
    {
        if(sw == true)
        {
            sw = false;
            return true;
        }
        return false;
    }

    public override void Tick(Order currentOrder, float deltaTime)
    {

    }

}
