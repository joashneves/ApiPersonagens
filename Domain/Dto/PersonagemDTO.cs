using System.Diagnostics;
using ApiBotDiscord.Domain.Models;

namespace ApiBotDiscord.Domain.Dto
{

    public class PersonagemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string CaminhoArquivo { get; set; }
        public new Franquia Franquia { get; set; }
    }
    
}
