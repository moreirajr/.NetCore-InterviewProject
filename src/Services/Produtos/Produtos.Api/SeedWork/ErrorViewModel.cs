using System.Collections.Generic;
using System.Linq;

namespace Produtos.Api.SeedWork
{
    public class ErrorViewModel
    {
        public ICollection<string> Errors { get; }

        public ErrorViewModel(string error)
        {
            Errors = new List<string>();
            Errors.Add(error);
        }

        public ErrorViewModel(IEnumerable<string> errors)
        {
            Errors = new List<string>();
            Errors.Concat(errors);
        }
    }
}