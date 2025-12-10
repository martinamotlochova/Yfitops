using System;

namespace Yfitops.Server.Models;

public interface IEntityMapper<TEntity,TContract>
{
    public static abstract TEntity ToEntity(TContract contract);
    public static abstract TContract ToContract (TEntity entity, string currentUserId);
}
