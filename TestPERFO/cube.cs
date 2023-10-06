using Godot;
using System;

public partial class cube : RigidBody3D
{
    private void OnTimerTimeOut()
    {
        QueueFree();
    }
}
