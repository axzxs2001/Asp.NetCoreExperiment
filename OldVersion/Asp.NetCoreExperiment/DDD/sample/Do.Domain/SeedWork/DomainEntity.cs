using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Do.Domain.SeedWork
{
    /// <summary>
    /// 领域对象实体父类
    /// </summary>
    public class DomainEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID
        { get; protected set; }

        /// <summary>
        /// 事件集合
        /// </summary>
        public List<INotification> DomainEvents
        { get; protected set; }
        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="eventItem"></param>
        public void AddDomainEvent(INotification eventItem)
        {
            DomainEvents = DomainEvents ?? new List<INotification>();
            DomainEvents.Add(eventItem);
        }
        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="eventItem"></param>
        public void RemoveDomainEvent(INotification eventItem)
        {
            if (DomainEvents is null) return;
            DomainEvents.Remove(eventItem);
        }
    }
}
