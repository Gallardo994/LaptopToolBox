using GHelper.Serialization;

namespace GHelper.IPC;

public interface IIpcMessage
{
    public long Id { get; }
    public void Serialize(BinarySerializer serializer);
    public void Deserialize(BinaryDeserializer deserializer);
}