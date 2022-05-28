using System.Collections;
using UnityEngine;
using System.Runtime.Serialization;

public class Vector3SerializationSurrogate : ISerializationSurrogate
{
    public void GetObjectData(object player, SerializationInfo info, StreamingContext context)
    {
        Vector3 v3 = (Vector3)player;
     
        info.AddValue("x", v3.x);
        info.AddValue("y", v3.y);
        info.AddValue("z", v3.z);
    }

    public object SetObjectData(object player, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
        Vector3 v3 = (Vector3)player;
        v3.x = (float)info.GetValue("x", typeof(float));
        v3.y = (float)info.GetValue("y", typeof(float));
        v3.z = (float)info.GetValue("z", typeof(float));
        player = v3;
        return player;
    }
}
