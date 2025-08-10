using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using emec.shared.models;

namespace emec.shared.Contracts
{
    public interface IValidator<in T> : IDisposable
    {
        bool Validate(T obj, out ResponseMessage responseMessage);
    }
}
