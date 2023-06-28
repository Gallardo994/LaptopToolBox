using System;

namespace GHelper.Serialization;

public class BinarySerializer
{
    private byte[] _buffer;
    private int _position;
    
    public int Position => _position;
    public byte[] Buffer => _buffer;
    
    public BinarySerializer(int initialCapacity = 8)
    {
        _buffer = new byte[initialCapacity];
        _position = 0;
    }

    private void EnsureBuffer(int additionalLength)
    {
        if (additionalLength + _position > _buffer.Length)
        {
            Array.Resize(ref _buffer, _buffer.Length + additionalLength);
        }
    }
    
    public void WriteByte(byte value)
    {
        EnsureBuffer(1);
        _buffer[_position++] = value;
    }
    
    public void WriteBytes(byte[] value)
    {
        EnsureBuffer(value.Length);
        value.CopyTo(_buffer, _position);
        _position += value.Length;
    }
    
    public void WriteUint(uint value)
    {
        EnsureBuffer(4);
        
        _buffer[_position++] = (byte)(value & 0xFF);
        _buffer[_position++] = (byte)((value >> 8) & 0xFF);
        _buffer[_position++] = (byte)((value >> 16) & 0xFF);
        _buffer[_position++] = (byte)((value >> 24) & 0xFF);
    }
    
    public void WriteInt(int value)
    {
        WriteUint((uint) value);
    }
    
    public void WriteULong(ulong value)
    {
        EnsureBuffer(8);
        
        _buffer[_position++] = (byte)(value & 0xFF);
        _buffer[_position++] = (byte)((value >> 8) & 0xFF);
        _buffer[_position++] = (byte)((value >> 16) & 0xFF);
        _buffer[_position++] = (byte)((value >> 24) & 0xFF);
        _buffer[_position++] = (byte)((value >> 32) & 0xFF);
        _buffer[_position++] = (byte)((value >> 40) & 0xFF);
        _buffer[_position++] = (byte)((value >> 48) & 0xFF);
        _buffer[_position++] = (byte)((value >> 56) & 0xFF);
    }
    
    public void WriteLong(long value)
    {
        WriteULong((ulong) value);
    }

    public unsafe void WriteSizeOf<T>(int count = 1, int extraBytes = 0) where T : unmanaged
    {
        WriteUint((uint) (count * sizeof(T)) + (uint) extraBytes);
    }
    
    public byte[] ToArray()
    {
        return _buffer[.._position];
    }
    
    public Span<byte> AsSpan()
    {
        return _buffer.AsSpan(0, _position);
    }
    
    public void Reset()
    {
        _position = 0;
    }
}