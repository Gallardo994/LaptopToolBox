using System;

namespace LaptopToolBox.Serialization
{
    public class BinaryDeserializer
    {
        private byte[] _buffer;
        private int _position;
        
        public BinaryDeserializer()
        {
            _buffer = Array.Empty<byte>();
            _position = 0;
        }

        public BinaryDeserializer(byte[] buffer)
        {
            _buffer = buffer;
            _position = 0;
        }
        
        public void SetBuffer(byte[] buffer)
        {
            _position = 0;
            _buffer = buffer;
        }
        
        public void ResetPosition()
        {
            _position = 0;
        }

        private void EnsureBuffer(int requiredLength)
        {
            if (_position + requiredLength > _buffer.Length)
            {
                throw new InvalidOperationException("Attempted to read past the end of the buffer.");
            }
        }

        public byte ReadByte()
        {
            EnsureBuffer(1);
            return _buffer[_position++];
        }

        public byte[] ReadBytes(int count)
        {
            EnsureBuffer(count);
            var value = new byte[count];
            Array.Copy(_buffer, _position, value, 0, count);
            _position += count;
            return value;
        }

        public uint ReadUint()
        {
            EnsureBuffer(4);

            var value = (uint)_buffer[_position++] |
                        (uint)_buffer[_position++] << 8 |
                        (uint)_buffer[_position++] << 16 |
                        (uint)_buffer[_position++] << 24;

            return value;
        }
        
        public int ReadInt()
        {
            return (int) ReadUint();
        }
        
        public ulong ReadULong()
        {
            return (ulong) ReadUint() |
                   (ulong) ReadUint() << 32;
        }
        
        public long ReadLong()
        {
            return (long) ReadULong();
        }

        public bool IsEnd => _position >= _buffer.Length;
    }
}