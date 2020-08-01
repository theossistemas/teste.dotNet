using AutoMapper;
using CrossCutting.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Services.Test.Base
{
    public abstract class BaseTesteService
    {        
        public IMapper Mapper { get; set; }
        public BaseTesteService()
        {
            Mapper = new AutoMapperFixture().GetMapper();
        }

        public class AutoMapperFixture : IDisposable
        {
            public IMapper GetMapper()
            {
                var config = new MapperConfiguration(c =>
                {
                    c.AddProfile(new EntityToDtoProfile());
                });

                return config.CreateMapper();
            }
            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }
    }
}
