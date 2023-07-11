using System;


namespace Interfaces 
{
    public interface IInput
    {
        event Action<float, float> LookDirEvent;
        event Action ShootingEvent;
        event Action ReloadEvent;
        event Action TargetSwitcherEvent;
    }
}


