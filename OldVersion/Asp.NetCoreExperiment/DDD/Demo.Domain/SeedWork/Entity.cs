using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Domain.SeedWork
{
    /// <summary>
    /// 领域对象实体父类
    /// </summary>
    public class Entity
    {
        public virtual int Id
        { get; protected set; }

        int? _requestedHashCode;

        public List<INotification> DomainEvents
        { get; protected set; }

        public void AddDomainEvent(INotification eventItem)
        {
            DomainEvents = DomainEvents ?? new List<INotification>();
            DomainEvents.Add(eventItem);
        }
        public void RemoveDomainEvent(INotification eventItem)
        {
            if (DomainEvents is null) return;
            DomainEvents.Remove(eventItem);
        }
        /// <summary>
        /// 比较ID是不是默认值
        /// </summary>
        /// <returns></returns>
        public bool IsTransient()
        {
            return this.Id == default(Int32);
        }
        /// <summary>
        /// 以ID作为 判断相等的条件
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            Entity item = (Entity)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();

        }

        public static bool operator ==(Entity left, Entity right)
        {
            return Object.Equals(left, null) ?
                  ((Object.Equals(right, null)) ? true : false) : left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
