using System.Collections.Generic;

namespace Business.Models
{
    public interface IModel
    {
        IEnumerable<string> Validate();
    }
}