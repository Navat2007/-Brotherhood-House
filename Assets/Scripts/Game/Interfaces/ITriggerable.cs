using System;

public interface ITriggerable
{
    public event Action OnTriggerEnd;
    
    public void Trigger();
}