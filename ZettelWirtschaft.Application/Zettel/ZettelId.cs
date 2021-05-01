using System.Text.RegularExpressions;
using System;
using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;
using System.Threading;
using ZettelWirtschaft.Application.ValueObject;

namespace ZettelWirtschaft.Application.Zettel
{
    public class ZettelId : StringId
    {
        public ZettelId(string id) : base(id)
        {
        }
    }
}