using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ZettelWirtschaft.Application.ValueObject;

namespace ZettelWirtschaft.Application.Zettel
{
    public interface IZettelCreationRepository
    {
        Task<ZettelId> GetNewZettelId(CancellationToken cancellation);
        Task<ZettelEntity> CreateNewZettel(ZettelEntity zettel, CancellationToken cancellation);
    }

    public class CreateZettelCommand : IRequest<ZettelEntity>
    {
        private Title title;
        public Title Title { get => title; private set => title = value ?? throw new ArgumentNullException(nameof(Title)); }

        private ZettelContent content;
        public ZettelContent Content { get => content; private set => content = value ?? throw new ArgumentNullException(nameof(Content)); }

        public CreateZettelCommand(Title title, ZettelContent content)
        {
            Title = title;
            Content = content;
        }
    }

    public class CreateZettelCommandHandler : IRequestHandler<CreateZettelCommand, ZettelEntity>
    {
        private readonly IZettelCreationRepository zettelCreattionRepository;

        public CreateZettelCommandHandler(IZettelCreationRepository zettelCreattionRepository)
        {
            this.zettelCreattionRepository = zettelCreattionRepository;
        }

        public async Task<ZettelEntity> Handle(CreateZettelCommand request, CancellationToken cancellationToken)
        {
            var zettelId = await zettelCreattionRepository.GetNewZettelId(cancellationToken);
            var zettel = new ZettelEntity(zettelId, request.Title, request.Content);
            return await zettelCreattionRepository.CreateNewZettel(zettel, cancellationToken);
        }
    }
}