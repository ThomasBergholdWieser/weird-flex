using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeirdFlex.Business.Interfaces
{
    public interface IUseCaseResult<T>
    {
        public Result<T> Result { get; }
    }
}
