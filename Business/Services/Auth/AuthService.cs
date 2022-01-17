using AutoMapper;
using Business.Abstract;
using Business.Utilities.Hashing.Abstract;
using Business.Utilities.Security.Abstract;
using DataAccess.Abstract;
using Entities;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Auth
{
    public class AuthService : IAuthService
    {
        private IMapper _mapper { get; set; }
        private readonly ITokenHelper _tokenHelper;
        private readonly IHashingHelper _hashingHelper;
        private readonly IUserRepository _userRepository;
        public AuthService(IMapper mapper, ITokenHelper tokenHelper, IHashingHelper hashingHelper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _tokenHelper = tokenHelper;
            _hashingHelper = hashingHelper;
            _userRepository = userRepository;
        }

        public async Task<LoginInfoDto> Login(LoginDto loginDto)
        {
            var result = await _userRepository.GetByFilterAsync(user => user.Username == loginDto.Username);
            var user = result.FirstOrDefault();
            if (user == null)
            {
                return new LoginInfoDto();
            }
            if (!_hashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new LoginInfoDto();
            }
            var mappedData = _mapper.Map<LoginInfoDto>(user);
            mappedData.AccessToken = _tokenHelper.CreateToken(_mapper.Map<User>(user));

            return mappedData;
        }

    }
}


