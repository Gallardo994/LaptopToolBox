using LaptopToolBox.Serialization;

namespace LaptopToolBox.IPC.Messages;

public class IpcBringToFront : IIpcMessage
{
    public long Id => 1;
    
    public void Serialize(BinarySerializer serializer)
    {
        
    }

    public void Deserialize(BinaryDeserializer deserializer)
    {
        
    }
}