using System;
using System.Collections.Generic;
using System.Text;

namespace Yfitops.Shared
{
    public class StorageContract
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public long Size { get; set; }
    }
}
