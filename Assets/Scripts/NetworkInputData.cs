using UnityEngine;
using Fusion;

public struct NetworkInputData : INetworkInput
{
    public Vector3 InputVector;
    public NetworkBool jumpInput;
    public NetworkBool sprintInput;
}
