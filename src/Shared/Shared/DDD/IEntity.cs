using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DDD
{

    public interface IEntity<T>:IEntity
    {
        T Id { get; set; }
    }

    public interface IEntity
    {
        DateTime? CreatedAt { get; set; }
        string? CreateBY { get; set; }
        DateTime? LastModified { get; set; }
        string? LastModifiedBy { get; set; }
    }
}
