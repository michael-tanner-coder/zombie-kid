using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FrameInput {
    public float X;
    public float Y;
    public bool JumpDown;
    public bool JumpUp;
}

public interface IPlayerController {
        Vector3 Velocity { get; }
        FrameInput Input { get; }
        Vector3 RawMovement { get; }
        bool Grounded { get; }
}
