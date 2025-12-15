using System;

namespace Yfitops.Server.Models;

public class Storage
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public byte[] Data { get; set; }
    public long Size { get; set; }
    public DateTime UploadedAt { get; set; }
}
