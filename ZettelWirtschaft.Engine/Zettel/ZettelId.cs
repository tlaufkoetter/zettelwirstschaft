using System.Text.RegularExpressions;
using System;
using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;
using System.Threading;
using ZettelWirtschaft.Engine.ValueObject;

namespace ZettelWirtschaft.Engine.Zettel
{
    public class ZettelId : StringId
    {
        public ZettelId(string id) : base(id)
        {
        }
    }
}