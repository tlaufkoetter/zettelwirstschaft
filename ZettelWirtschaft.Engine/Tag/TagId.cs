using FluentValidation;
using ZettelWirtschaft.Engine.ValueObject;

namespace ZettelWirtschaft.Engine.Tag
{
    public class TagId : StringId
    {
        public TagId(string id) : base(id)
        {
        }
    }
}