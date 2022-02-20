using ApplicationCore.Dto;
using ApplicationCore.Entities;
using ApplicationCore.Services;
using AutoMapper;
using Xunit;

namespace ApplicationCore.Test
{
    public class AutoMappingTest
    {
        [Fact]
        public void MapApplicationUserToUserCorrectory()
        {
            var expect = new ApplicationUser
            {
                UserName = "example user",
                Email = "example@example.com"
            };
            var mapper = CreateMapper();
            var actual = mapper.Map<UserDto>(expect);
            Assert.Equal(expect.Id, actual.Id.ToString());
            Assert.Equal(expect.UserName, actual.UserName);
            Assert.Equal(expect.Email, actual.Email);
        }

        private Mapper CreateMapper()
        {
            var config = new MapperConfiguration(config =>
            {
                config.AddProfile<AutoMapping>();
            });
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
