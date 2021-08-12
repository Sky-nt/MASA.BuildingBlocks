﻿using MASA.BuildingBlocks.ReadWriteSpliting.CQRS.Events;

namespace MASA.BuildingBlocks.ReadWriteSpliting.CQRS.Abstractions
{
    public interface IEventHandler<TEvent>
        where TEvent : Event
    {
        Task HandleAsync(TEvent @event);
    }
}
