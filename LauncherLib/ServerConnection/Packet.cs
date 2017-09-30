using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LauncherLib.ServerConnection
{
        //00000011      01             00054d4845524f6300000001
        //size?      packet type               data
        internal struct Packet
        {
            [ThreadStatic]
            private int offset;
            private TcpClient client;

            public PacketType PacketType
            {
                get
                {
                    return (PacketType)Buffer.GetByte((Array)this.Bytes.Array, this.Bytes.Offset + 4);
                }
                private set
                {
                    Buffer.SetByte((Array)this.Bytes.Array, this.Bytes.Offset + 4, (byte)value);
                }
            }

            public ArraySegment<byte> Bytes { get; private set; }

            public Packet(ArraySegment<byte> bytes)
            {
                this = new Packet();
                this.Bytes = bytes;
                this.offset = this.Bytes.Offset + 5;
            }


            public Packet(int length, PacketType packetType)
            {
                this = new Packet();
                this.Bytes = ChunkSegment<byte>.Get(5 + length);
                this.PacketType = packetType;
                this.offset = this.Bytes.Offset + 5;


                byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(5 + length));
                Buffer.BlockCopy((Array)bytes, 0, (Array)this.Bytes.Array, 0, bytes.Length);
            }

            public Packet(PacketType packetType)
            {
                this = new Packet();
                this.Bytes = ChunkSegment<byte>.Get(5);
                this.PacketType = packetType;
                this.offset = this.Bytes.Offset + 5;
            }


            public bool ReadBoolean()
            {
                return BitConverter.ToBoolean(this.Bytes.Array, this.offset++);
            }

            public byte ReadByte()
            {
                return Buffer.GetByte((Array)this.Bytes.Array, this.offset++);
            }

            public short ReadInt16()
            {
                short num = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(this.Bytes.Array, this.offset));
                this.offset += 2;
                return num;
            }

            public int ReadInt32()
            {
                int num = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(this.Bytes.Array, this.offset));
                this.offset += 4;
                return num;
            }

            public long ReadInt64()
            {
                long num = IPAddress.NetworkToHostOrder(BitConverter.ToInt64(this.Bytes.Array, this.offset));
                this.offset += 8;
                return num;
            }

            public string ReadString()
            {
                ushort num = (ushort)this.ReadInt16();
                string @string = Encoding.Default.GetString(this.Bytes.Array, this.offset, (int)num);
                this.offset += (int)num;
                return @string;
            }

            public DateTime ReadDateTime()
            {
                return new DateTime(this.ReadInt64());
            }

            public Guid ReadGuid()
            {
                return new Guid(this.ReadString());
            }

            public IPAddress ReadIPAddress()
            {
                IPAddress ipAddress = new IPAddress((long)BitConverter.ToInt32(this.Bytes.Array, this.offset));
                this.offset += 4;
                return ipAddress;
            }

            public void Write(bool value)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                Buffer.BlockCopy((Array)bytes, 0, (Array)this.Bytes.Array, this.offset, bytes.Length);
                this.offset += bytes.Length;
            }

            public void Write(byte value)
            {
                Buffer.SetByte((Array)this.Bytes.Array, this.offset++, value);
            }

            public void Write(short value)
            {
                byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value));
                Buffer.BlockCopy((Array)bytes, 0, (Array)this.Bytes.Array, this.offset, bytes.Length);
                this.offset += bytes.Length;
            }

            public void Write(int value)
            {
                byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value));
                Buffer.BlockCopy((Array)bytes, 0, (Array)this.Bytes.Array, this.offset, bytes.Length);
                this.offset += bytes.Length;
            }

            public void Write(long value)
            {
                byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value));
                Buffer.BlockCopy((Array)bytes, 0, (Array)this.Bytes.Array, this.offset, bytes.Length);
                this.offset += bytes.Length;
            }

            public void Write(string value)
            {
                if (value == null)
                {
                    this.Write((short)0);
                }
                else
                {
                    int bytes = Encoding.Default.GetBytes(value, 0, value.Length, this.Bytes.Array, this.offset + 2);
                    this.Write((short)(ushort)bytes);
                    this.offset += bytes;
                }
            }

            public void Write(DateTime value)
            {
                this.Write(value.Ticks);
            }

            public void Write(Guid value)
            {
                this.Write(value.ToString());
            }

            public void Write(IPAddress value)
            {
                Buffer.BlockCopy((Array)value.GetAddressBytes(), 0, (Array)this.Bytes.Array, this.offset, 4);
                this.offset += 4;
            }

            public void Encrypt(ICryptoTransform crypto)
            {
                if (crypto != null)
                    throw new ArgumentException("ICryptoTransform is not supported.", "crypto");
            }

            public void SetSender(TcpClient tcpClient)
            {
                client = tcpClient;
            }

            public void Send()
            {
                 ServerConn.Instance.Send(Bytes.ToArray());
            }

            public void Resize(int len)
            {
                this.offset = this.Bytes.Offset + 5;
                this.Bytes = ChunkSegment<byte>.Get(5 + len);

                //write packet size!
                byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(5+len));
                Buffer.BlockCopy((Array)bytes, 0, (Array)this.Bytes.Array, 0, bytes.Length);
 
            }

        }
}
