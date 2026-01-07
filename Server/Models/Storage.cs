using System;
using Yfitops.Shared;

namespace Yfitops.Server.Models;

public class Storage
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public byte[] Data { get; set; }
    public long Size { get; set; }

    public static Storage ToEntity(StorageContract contract)
    {
        return new Storage()
        {
            Id = contract.Id,
            FileName = contract.FileName,
            Data = contract.Data,
            Size = contract.Size
        };
    }

    public static StorageContract ToContract(Storage storage, string currentUserId)
    {
        return new StorageContract()
        {
            Id = storage.Id,
            FileName = storage.FileName,
            Data = storage.Data,
            Size = storage.Size
        };
    }
}
