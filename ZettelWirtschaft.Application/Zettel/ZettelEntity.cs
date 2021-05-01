using System;
using ZettelWirtschaft.Application.ValueObject;

namespace ZettelWirtschaft.Application.Zettel
{
    public class ZettelEntity
    {
        private ZettelId id;
        public ZettelId Id { get => id; private set => id = value ?? throw new ArgumentNullException(nameof(Id)); }

        private Title title;
        public Title Title { get => title; private set => title = value ?? throw new ArgumentNullException(nameof(Title)); }

        private ZettelContent content;
        public ZettelContent Content { get => content; private set => content = value ?? throw new ArgumentNullException(nameof(Content)); }

        public ZettelEntity(ZettelId zettelId, Title title, ZettelContent zettelContent)
        {
            this.Id = zettelId;
            this.Title = title;
            this.Content = zettelContent;
        }
    }
}