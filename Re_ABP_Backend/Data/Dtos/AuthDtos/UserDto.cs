using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Re_ABP_Backend.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Re_ABP_Backend.Data.Dtos.AuthDtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
