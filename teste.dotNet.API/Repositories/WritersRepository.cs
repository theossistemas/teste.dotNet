using System.Collections.Generic;
using teste.dotNet.API.DTOs.Request;
using teste.dotNet.API.DTOs.Response;

namespace teste.dotNet.API.Repository
{
    public interface WritersRepository
    {
        public WriterResponseDTO Get(int writerId);
        public ICollection<WriterResponseDTO> List();
        public string Add(WriterRequestDTO writer);
        public string Update(int writerId, WriterRequestDTO writer);
        public string Delete(int writerId);
    }

}
