using FluentValidation;
using ZettelWirtschaft.Application.ValueObject;

namespace ZettelWirtschaft.Application.Tag
{
    public class TagId : StringId
    {
        public TagId(string id) : base(id)
        {
        }
    }
}