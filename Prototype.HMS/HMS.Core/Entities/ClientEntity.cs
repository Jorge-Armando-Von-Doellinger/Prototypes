using HMS.Core.Entities.Base;
using System.Reflection.Emit;

namespace HMS.Core.Entities
{
    public class Client : EntityBase
    {
        public Client(string name, long cpf, string email, string phoneNumber, DateTime dateBirth)
        {
            Name = name;
            CPF = cpf;
            Email = email;
            PhoneNumber = phoneNumber;
            DateBirth = dateBirth;
        }
        public Client() { }
        public string Name { get; set; }
        public long CPF { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateBirth { get; set; }

    }
}
