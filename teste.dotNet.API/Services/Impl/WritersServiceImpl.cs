using System.Collections.Generic;
using teste.dotNet.API.DTOs.Request;
using teste.dotNet.API.DTOs.Response;
using teste.dotNet.API.Repository;

namespace teste.dotNet.API.Services.Impl {
    public class WritersServiceImpl : WritersService {
        private WritersRepository _writerRepository;

        public WritersServiceImpl(WritersRepository writerRepository)
        {
            _writerRepository = writerRepository;
        }
        public WriterResponseDTO Get(int writerId)
        {
            var writer = _writerRepository.Get(writerId);
            return writer;
        }        
        public ICollection<WriterResponseDTO> List()
        {
            var writers = _writerRepository.List();
            return writers;
        }

        public string Add(WriterRequestDTO writer)
        {
            var message =_writerRepository.Add(writer);
            return message;
        }      

        public string Update(int writerId, WriterRequestDTO writer)
        {
            var message = _writerRepository.Update(writerId, writer);
            return message;
        }        
        public string Delete(int writerId)
        {
            var message =_writerRepository.Delete(writerId);
            return message;
        } 
    }
}
