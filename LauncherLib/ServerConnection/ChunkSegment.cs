using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherLib.ServerConnection
{
    public static class ChunkSegment<T>
    {
        private static int allocationSize = 65512;
        [ThreadStatic]
        private static ChunkSegment<T>.ChunkSegmentContext context;

        internal static ChunkSegment<T>.ChunkSegmentContext Context
        {
            get
            {
                if (ChunkSegment<T>.context == null)
                    ChunkSegment<T>.context = new ChunkSegment<T>.ChunkSegmentContext();
                return ChunkSegment<T>.context;
            }
        }

        public static int AllocationSize
        {
            get
            {
                return ChunkSegment<T>.allocationSize;
            }
            set
            {
                ChunkSegment<T>.allocationSize = value;
            }
        }

        public static ArraySegment<T> Get(int size)
        {
            return ChunkSegment<T>.Context.Get(size);
        }

        internal class ChunkSegmentContext
        {
            private int index;
            private T[] chunk;

            public ChunkSegmentContext()
            {
                this.chunk = new T[ChunkSegment<T>.AllocationSize];
            }

            public ArraySegment<T> Get(int size)
            {
                if (size >= this.chunk.Length)
                    return new ArraySegment<T>(new T[size]);
                if (this.index + size > this.chunk.Length)
                {
                    this.chunk = new T[ChunkSegment<T>.AllocationSize];
                    this.index = 0;
                }
                this.index += size;
                return new ArraySegment<T>(this.chunk, this.index - size, size);
            }
        }
    }
}
