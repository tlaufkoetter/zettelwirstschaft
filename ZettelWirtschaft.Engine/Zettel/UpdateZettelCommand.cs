using System.Security.Cryptography;
using System.Threading;
using System;
using System.Collections.Generic;
using MediatR;
using System.Threading.Tasks;

namespace ZettelWirtschaft.Engine.Zettel
{
    public class UpdateZettelCommand : IRequest<ZettelEntity>
    {
        private ZettelEntity zettel;
        public ZettelEntity Zettel { get => zettel; set => zettel = value ?? throw new ArgumentNullException(nameof(Zettel)); }

        public UpdateZettelCommand(ZettelEntity zettel)
        {
            Zettel = zettel;
        }
    }

    public class UpdateZettelCommandHandler : IRequestHandler<UpdateZettelCommand, ZettelEntity>
    {
        private readonly IUpdateZettelRepository updateRepository;

        public UpdateZettelCommandHandler(IUpdateZettelRepository updateRepository)
        {
            this.updateRepository = updateRepository;
        }

        public async Task<ZettelEntity> Handle(UpdateZettelCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!(await updateRepository.DoesZettelExist(request.Zettel.Id, cancellationToken)))
            {
                cancellationToken.ThrowIfCancellationRequested();
                throw new ArgumentException($"Zettel with id {request.Zettel.Id} does not exist");
            }

            return await updateRepository.UpdateZettel(request.Zettel, cancellationToken);
        }
    }
}