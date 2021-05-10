using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ZettelWirtschaft.Engine.Zettel;

namespace ZettelWirtschaft.Engine.Zettel
{
    public class GetMultipleZettelQueryResponse
    {
        public IEnumerable<ZettelEntity> Zettel { get; }

        public GetMultipleZettelQueryResponse(IEnumerable<ZettelEntity> zettel)
        {
            Zettel = zettel ?? throw new ArgumentNullException(nameof(zettel));
        }
    }

    public class GetMultipleZettelQuery : IRequest<IEnumerable<ZettelEntity>>
    {
    }

    public class GetMultipleeZettelQueryHandler : IRequestHandler<GetMultipleZettelQuery, IEnumerable<ZettelEntity>>
    {
        private readonly IGetMultipleZettelRepository repository;

        public GetMultipleeZettelQueryHandler(IGetMultipleZettelRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<ZettelEntity>> Handle(GetMultipleZettelQuery request, CancellationToken cancellationToken)
        {
            return (await repository.GetMultipleZettel(cancellationToken)) ?? new List<ZettelEntity>() { };
        }
    }
}