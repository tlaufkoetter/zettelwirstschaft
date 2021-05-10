using System.Threading;
using System;
using MediatR;
using System.Threading.Tasks;

namespace ZettelWirtschaft.Engine.Zettel
{
    public class GetZettelDetailQuery : IRequest<ZettelEntity>
    {
        private ZettelId id;
        public ZettelId Id { get => id; private set => id = value ?? throw new ArgumentNullException(nameof(Id)); }

        public GetZettelDetailQuery(ZettelId id)
        {
            Id = id;
        }
    }

    public class GetZettelDetailQueryHandler : IRequestHandler<GetZettelDetailQuery, ZettelEntity>
    {
        private readonly IGetZettelDetailQueryRepository repository;

        public GetZettelDetailQueryHandler(IGetZettelDetailQueryRepository repository)
        {
            this.repository = repository;
        }

        public Task<ZettelEntity> Handle(GetZettelDetailQuery request, CancellationToken cancellationToken)
        {
            return repository.GetZettelDetail(request.Id, cancellationToken);
        }
    }
}